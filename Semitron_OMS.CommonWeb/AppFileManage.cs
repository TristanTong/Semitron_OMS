using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;
using System.Threading;
using Semitron_OMS.Common;

namespace Semitron_OMS.CommonWeb
{
    /// <summary>
    /// 文件操作管理
    /// </summary>
    public class AppFileManage
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        public static AppFileManage CreateInstance
        {
            get
            {
                return new AppFileManage();
            }
        }

        private const string TEMP_FORDER = "_temp\\";

        /// <summary>
        /// 上传文件，保存到临时文件夹中
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string UploadAppTempFile(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            string strFolder = @context.Request["folder"].ToString().Trim();
            string strFileType = string.Empty;
            if (strFolder.IndexOf("/Admin/UserControl/") != -1)
            {
                strFileType = strFolder.Remove(0, "/Admin/UserControl/".Length);
            }
            string uploadPath = string.Empty;
            //文件扩展名
            string strFileExtension = string.Empty;
            //文件名称
            string strFileName = string.Empty;
            if (file != null && !string.IsNullOrEmpty(file.FileName))
            {
                strFileExtension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                strFileName = file.FileName.Remove(file.FileName.LastIndexOf('.'));
            }
            //系统默认的图片文件路径(放入图片文件服务器)
            switch (strFileType)
            {
                case ConstantValue.AppSettingsNames.CustomerFilePath:
                case ConstantValue.AppSettingsNames.QCFilePath:
                case ConstantValue.AppSettingsNames.DownloadCenterFilePath:
                case ConstantValue.AppSettingsNames.OrderFeeFilePath:
                    uploadPath = (CommonMethods.GetUploadFilePath(context, strFileType) + "_" + strFileName + strFileExtension).Replace("\\" + strFileType + "\\", "\\" + strFileType + TEMP_FORDER);
                    break;
                default:
                    uploadPath = HttpContext.Current.Server.MapPath(strFolder) + "\\";
                    break;
            }

            if (file != null && uploadPath != string.Empty)
            {
                //非图片文件服务器文件，指定文件名
                if (string.IsNullOrEmpty(strFileType))
                {
                    uploadPath += DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + strFileName + strFileExtension;
                }
                string strDirecPath = uploadPath.Substring(0, uploadPath.LastIndexOf('\\'));
                if (!Directory.Exists(strDirecPath))
                {
                    Directory.CreateDirectory(strDirecPath);
                }
                file.SaveAs(uploadPath);

                List<string> lstFilePath = new List<string>();

                if (context.Session[ConstantValue.SessionKeys.UploadifyFilePath] == null)
                {
                    //设定上传成功文件路径Session
                    lstFilePath.Add(uploadPath);
                    context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = lstFilePath;
                }
                else
                {
                    //设定上传成功文件路径Session
                    lstFilePath = (List<string>)context.Session[ConstantValue.SessionKeys.UploadifyFilePath];
                    if (lstFilePath == null)
                    {
                        lstFilePath = new List<string>();
                    }
                    else
                    {
                        //是否多文件上传(0:当文件上传；1：多文件上传)
                        int iMulti = -1;
                        //是否单文件还是多文件上传
                        if (HttpContext.Current.Request.Form["IsMulti"] != null)
                        {
                            int.TryParse(HttpContext.Current.Request.Form["IsMulti"], out iMulti);
                        }

                        //单文件上传
                        if (iMulti == 0)
                        {
                            //容错，清空列表中所有对象，确保只有一个路径
                            lstFilePath.Clear();
                        }
                    }
                    lstFilePath.Add(uploadPath);
                    context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = lstFilePath;
                }

                //释放文件流缓存，释放资源
                file.InputStream.Flush();
                file.InputStream.Dispose();

                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                return "1";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 将上传到临时文件夹中的文件剪切到真实的文件夹中
        /// </summary>
        /// <param name="strTempFilePath">临时文件路径</param>
        /// <returns></returns>
        public string SaveAppUploadFile(string strTempFilePath)
        {
            strTempFilePath = CommonMethods.GetPhysicalByUrl(strTempFilePath);

            //如果上传文件的临时文件夹中不存在该文件，则此文件不是新上传的文件
            if (strTempFilePath == null ||
                !System.IO.File.Exists(strTempFilePath))
            {
                return string.Empty;
            }

            if (!strTempFilePath.Contains(TEMP_FORDER))
            {
                return strTempFilePath;
            }

            try
            {
                string strSaveFilePath = strTempFilePath.Replace(TEMP_FORDER, "\\");
                string strSavePath = System.IO.Path.GetDirectoryName(strSaveFilePath);
                if (!System.IO.Directory.Exists(strSavePath))
                {
                    System.IO.Directory.CreateDirectory(strSavePath);
                }
                System.IO.File.Copy(strTempFilePath, strSaveFilePath);

                //粉碎临时文件
                ShredFiles(strTempFilePath);

                return strSaveFilePath;
            }
            catch (Exception ex)
            {
                throw new Exception("移动文件发生错误 " + ex.Message);
            }
        }

        /// <summary>
        /// 删除文件，比较两个文件名称是否相等，不相等的话将旧的文件移动到删除(DEL)的文件夹中
        /// </summary>
        /// <param name="strOldFilePath">原文件路径</param>
        /// <param name="strNewFilePath">新的文件路径</param>
        /// <returns></returns>
        public bool DeleteAppFile(string strOldFilePath, string strNewFilePath)
        {
            strOldFilePath = CommonMethods.GetPhysicalByUrl(strOldFilePath);
            strNewFilePath = CommonMethods.GetPhysicalByUrl(strNewFilePath);

            if (!System.IO.File.Exists(strOldFilePath) ||
                !System.IO.File.Exists(strNewFilePath))
            {
                return false;
            }

            try
            {
                //原文件名称
                string strOldFileName = System.IO.Path.GetFileName(strOldFilePath);
                //新文件名称
                string strNewFileName = System.IO.Path.GetFileName(strNewFilePath);

                if (strOldFileName != strNewFileName)
                {
                    string strMoveFileName = strOldFilePath.Replace("\\APP\\", "\\Del\\");
                    string strMovePath = System.IO.Path.GetDirectoryName(strMoveFileName);

                    if (!System.IO.Directory.Exists(strMovePath))
                    {
                        System.IO.Directory.CreateDirectory(strMovePath);
                    }

                    System.IO.File.Copy(strOldFilePath, strMoveFileName);

                    //粉碎旧文件
                    ShredFiles(strOldFilePath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("移动文件发生错误 " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 删除APP相关文件，将文件移动到（Del）文件夹中
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public bool DeleteAppFile(string strFileName)
        {
            strFileName = CommonMethods.GetPhysicalByUrl(strFileName);
            if (!System.IO.File.Exists(strFileName))
            {
                return false;
            }

            try
            {
                string strMoveFileName = strFileName.Replace("\\APP\\", "\\Del\\");
                string strMovePath = System.IO.Path.GetDirectoryName(strMoveFileName);

                if (!System.IO.Directory.Exists(strMovePath))
                {
                    System.IO.Directory.CreateDirectory(strMovePath);
                }

                System.IO.File.Copy(strFileName, strMoveFileName);

                //粉碎文件
                ShredFiles(strFileName);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("移动文件发生错误 " + ex.Message);
            }
        }

        /// <summary>
        /// 删除临时文件夹中的文件
        /// </summary>
        /// <param name="strTempFilePath"></param>
        private void DeleteFileAsyn(string strTempFilePath)
        {
            if (string.IsNullOrEmpty(strTempFilePath))
            {
                return;
            }

            Thread thead = new Thread(new ThreadStart(() =>
            {
                try
                {
                    //休眠30分钟
                    System.Threading.Thread.Sleep(1000 * 60 * 30);

                    System.IO.File.Delete(strTempFilePath);
                }
                catch
                { }
            }));
            thead.Start();
        }

        /// <summary>
        /// 粉碎文件，由于有进程在使用文件而无法直接删除，使用粉碎文件的方法进行文件删除
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        private void ShredFiles(string strFilePath)
        {
            FileStream filestream = null;
            BinaryWriter objBinaryWriter = null;
            try
            {
                if (File.Exists(strFilePath))
                {
                    //设置文件属性
                    System.IO.FileInfo fileInfo = new FileInfo(strFilePath);
                    fileInfo.Attributes = FileAttributes.Normal;

                    //打开文件
                    filestream = new FileStream(strFilePath, FileMode.Create);

                    //建立写入文件流
                    objBinaryWriter = new BinaryWriter(filestream);
                    //以字节流方式写入文件
                    byte[] filecontent = new UTF8Encoding(true).GetBytes("");
                    //path.Length可以直接读取已接收文件的物理长度
                    for (int index = 0; index < strFilePath.Length; index++)
                    {
                        objBinaryWriter.Write(filecontent);
                    }

                    //关闭文件操作流
                    objBinaryWriter.Close();
                    filestream.Close();

                    //删除文件
                    File.Delete(strFilePath);
                }
            }
            catch
            {
                DeleteFileAsyn(strFilePath);
            }
        }
    }
}
