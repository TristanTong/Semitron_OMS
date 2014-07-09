using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Semitron_OMS.Common;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Configuration;
using Semitron_OMS.BLL.Common;
using Semitron_OMS.Model.Common;
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// CommonMethod 的摘要说明
    /// </summary>
    public class CommonMethodHandle : IHttpHandler, IRequiresSessionState
    {
        //系统日志对象
        private log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(CommonMethodHandle));
        private HttpRequest request = HttpContext.Current.Request;
        private Semitron_OMS.Model.Common.AdminModel _adminModel = new Semitron_OMS.Model.Common.AdminModel();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string methStr = string.Empty;
            if (context.Session["Admin"] == null)
            {
                PageResult result = new PageResult();
                result.Info = "网络异常，请刷新重试。";
                context.Response.Write(result.ToString());
                return;
            }
            else
            {
                _adminModel = context.Session["Admin"] as Semitron_OMS.Model.Common.AdminModel;
            }
            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"].Trim();
            }
            if (context.Request.QueryString["meth"] != null)
            {
                methStr = context.Request.QueryString["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //新增偏好设定项
                    case "AddOrUpdatePreferenceConfig":
                        context.Response.Write(AddOrUpdatePreferenceConfig());
                        break;
                    //根据页面代码取得偏好设定项
                    case "GetPreferenceConfigByCode":
                        context.Response.Write(GetPreferenceConfigByCode());
                        break;
                    //根据厂商ID获得厂商的项目信息
                    //case "GetProjectByCustID":
                    //    context.Response.ContentType = "text/plain";
                    //    context.Response.Write(GetProjectByCustID());
                    //    break;
                    //从Session中取出上传成功的文件路径
                    case "GetUploadFilePath":
                        context.Response.Write(GetUploadFilePath(context));
                        break;
                    //上传文件发送错误时，把Session保存的文件路径清空
                    case "ClearSessionOnUploadError":
                        context.Response.Write(ClearSessionOnUploadError(context));
                        break;
                }
                //Uploadify上传控件保存文件
                if (methStr.IndexOf("UploadifySaveFile") != -1)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Charset = "utf-8";
                    string[] strArrTemp = methStr.Split('$');
                    string strResult = UploadifySaveFile(context);
                    if (strArrTemp.Length == 2 && strArrTemp[1].Trim().ToLower().EndsWith("true"))
                    {
                        //控制上传控件文件完成后是否自动从列表中消失
                        context.Response.Write(strResult);
                    }

                }
            }
            context.Response.End();
        }

        #region 偏好设定
        /// <summary>
        /// 新增偏好设定项
        /// </summary>
        /// <returns></returns>
        private string AddOrUpdatePreferenceConfig()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            PreferenceConfigModel model = new PreferenceConfigModel();
            PreferenceConfigBLL bll = new PreferenceConfigBLL();

            if (request.Form["PageCode"] == null || request.Form["SearchShow"] == null || request.Form["ColumnShow"] == null
                 || request.Form["GroupParam"] == null || request.Form["SearchParam"] == null)
            {
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }

            model.AdminID = _adminModel.AdminID;

            //页面代码
            SQLOperateHelper.SetEntityFiledValue(model, "PageCode", request.Form["PageCode"]);
            //查询显示控件id
            SQLOperateHelper.SetEntityFiledValue(model, "SearchShow", request.Form["SearchShow"]);
            //列显示
            SQLOperateHelper.SetEntityFiledValue(model, "ColumnShow", request.Form["ColumnShow"]);
            //分组条件显示
            SQLOperateHelper.SetEntityFiledValue(model, "GroupParam", request.Form["GroupParam"]);
            //查询条件默认参数值
            SQLOperateHelper.SetEntityFiledValue(model, "SearchParam", request.Form["SearchParam"]);
            result.Info = "设定个人偏好失败。";
            if (bll.AddOrUpdatePreferenceConfig(model))
            {
                result.State = 1;
                result.Info = "设定个人偏好成功。";
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据页面代码取得偏好设定项
        /// </summary>
        private string GetPreferenceConfigByCode()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            PreferenceConfigBLL bll = new PreferenceConfigBLL();
            if (request.Form["PageCode"] == null || request.Form["PageCode"].ToString() == string.Empty)
            {
                result.Info = "ID参数异常。";
                return result.ToString();
            }
            PreferenceConfigModel model = bll.GetModel(_adminModel.AdminID, request.Form["PageCode"].ToString().Trim());

            result.State = 0;
            result.Info = "出现异常，未能从数据库中找到对应记录。";
            try
            {
                if (model != null)
                {
                    result.State = 1;
                    string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new IsoDateTimeConverter());
                    return strResult;
                }
            }
            catch (Exception ex)
            {
                myLogger.Error("从数据库中找到对应用户的偏好设定出现异常" + ex.Message, ex);
            }

            return result.ToString();
        }
        #endregion 偏好设定

        #region 下拉框控件级联选择数据加载
        ///// <summary>
        ///// 根据厂商ID获得厂商的项目信息
        ///// </summary>
        ///// <returns></returns>
        //private string GetProjectByCustID()
        //{
        //    PageResult result = new PageResult();
        //    HttpRequest request = HttpContext.Current.Request;
        //    int custID;
        //    DataSet ds;
        //    bool bShowAll = true;
        //    if (request.Params["custID"] == null || !int.TryParse(request.Params["custID"].ToString(), out custID))
        //    {
        //        result.Info = "数据异常，请联系管理员。";
        //        if (request.Params["custID"] == null)
        //        {
        //            myLogger.Info("GetProjectByCustID获取参数icustID异常。");
        //        }

        //        return "";
        //    }
        //    if (request.Params["ShowAll"] != null)
        //    {
        //        if (request.Params["ShowAll"].ToString() == "0")
        //        {
        //            bShowAll = false;
        //        }
        //    }

        //    //判断是否开启AppOEM
        //    if (request.Form["IsOpen"] != null && request.Form["IsOpen"] != "")
        //    {
        //        ds = new OSS.DBCommon.BLL.ProjectBLL().GetList("CustId=" + custID + " AND IsOpenAppOEM = 1");
        //    }
        //    else
        //    {
        //        ds = new OSS.DBCommon.BLL.ProjectBLL().GetList("CustId=" + custID);
        //    }
        //    //DataTable dt = CommonMethods.GetDataTableCache(ConstantValue.TableNames.Project);
        //    //DataSet ds = Semitron_OMS.DBUtility.DbHelperSQL.Query("SELECT ProjectID,ProCode,CustId,SK,AvailFlag FROM Project WHERE CustId=" + icustID);      
        //    DataTable dt = new DataTable();
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }
        //    //分别获取有效和无效的项目编号。
        //    DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,ProCode Asc");
        //    DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,ProCode Asc");
        //    StringBuilder sb = new StringBuilder();
        //    if (bShowAll)
        //    {
        //        sb.AppendFormat("{0}|{1},", "－－有效－－", "-1");
        //    }
        //    foreach (DataRow dr in drValid)
        //    {
        //        sb.AppendFormat("{0}|{1},", dr["SK"].ToString() + "-" + dr["ProCode"].ToString(), dr["ProjectID"].ToString());
        //    }
        //    if (bShowAll)
        //    {
        //        sb.AppendFormat("{0}|{1},", "－－无效－－", "-2");
        //        foreach (DataRow dr in drUnValid)
        //        {
        //            sb.AppendFormat("{0}|{1},", dr["SK"].ToString() + "-" + dr["ProCode"].ToString(), dr["ProjectID"].ToString());
        //        }
        //    }
        //    if (sb.ToString().EndsWith(","))
        //    {
        //        //移除最后一个逗号
        //        sb.Remove(sb.ToString().Length - 1, 1);
        //    }

        //    if (sb.ToString().Length > 0)
        //    {
        //        return sb.ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }

        //}
        #endregion 下拉框控件级联选择数据加载

        #region Uploadify上传控件相关操作
        /// <summary>
        /// Uploadify上传控件保存文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string UploadifySaveFile(HttpContext context)
        {
            #region 以下代码已移到AppFileManage.cs中进行处理
            //HttpPostedFile file = context.Request.Files["Filedata"];
            //string strFolder = @context.Request["folder"].ToString().Trim();
            //string strFileType = string.Empty;
            //if (strFolder.IndexOf("/Admin/UserControl/") != -1)
            //{
            //    strFileType = strFolder.Remove(0, "/Admin/UserControl/".Length);
            //}
            //string uploadPath = string.Empty;
            ////文件扩展名
            //string strFileExtension = string.Empty;
            ////文件名称
            //string strFileName = string.Empty;
            //if (file != null && !string.IsNullOrEmpty(file.FileName))
            //{
            //    strFileExtension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //    strFileName = file.FileName.Remove(file.FileName.LastIndexOf('.'));
            //}
            ////系统默认的图片文件路径(放入图片文件服务器)
            //switch (strFileType)
            //{
            //    case ConstantValue.AppSettingsNames.ApkPath:
            //        uploadPath = CommonMethods.GetUploadFilePath(ConstantValue.AppSettingsNames.ApkPath) + strFileExtension;
            //        break;
            //    case ConstantValue.AppSettingsNames.IcoPath:
            //        uploadPath = CommonMethods.GetUploadFilePath(ConstantValue.AppSettingsNames.IcoPath) + strFileExtension;
            //        break;
            //    case ConstantValue.AppSettingsNames.ImgPath:
            //        uploadPath = CommonMethods.GetUploadFilePath(ConstantValue.AppSettingsNames.ImgPath) + strFileExtension;
            //        break;
            //    default:
            //        uploadPath = HttpContext.Current.Server.MapPath(strFolder) + "\\";
            //        break;
            //}


            //if (file != null && uploadPath != string.Empty)
            //{
            //    //非图片文件服务器文件，指定文件名
            //    if (string.IsNullOrEmpty(strFileType))
            //    {
            //        uploadPath += DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + strFileName + strFileExtension;
            //    }
            //    string strDirecPath = uploadPath.Substring(0, uploadPath.LastIndexOf('\\'));
            //    if (!Directory.Exists(strDirecPath))
            //    {
            //        Directory.CreateDirectory(strDirecPath);
            //    }
            //    file.SaveAs(uploadPath);
            //    List<string> lstFilePath = new List<string>();

            //    if (context.Session[ConstantValue.SessionKeys.UploadifyFilePath] == null)
            //    {
            //        //设定上传成功文件路径Session
            //        lstFilePath.Add(uploadPath);
            //        context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = lstFilePath;
            //    }
            //    else
            //    {
            //        //设定上传成功文件路径Session
            //        lstFilePath = (List<string>)context.Session[ConstantValue.SessionKeys.UploadifyFilePath];
            //        if (lstFilePath == null)
            //        {
            //            lstFilePath = new List<string>();
            //        }
            //        else
            //        {
            //            //是否多文件上传(0:当文件上传；1：多文件上传)
            //            int iMulti = -1;
            //            //是否单文件还是多文件上传
            //            if (request.Form["IsMulti"] != null)
            //            {
            //                iMulti = Convert.ToInt32(DataUtility.GetPageFormValue(request.Form["IsMulti"], "0"));
            //            }

            //            //单文件上传
            //            if (iMulti == 0)
            //            {
            //                //容错，清空列表中所有对象，确保只有一个路径
            //                lstFilePath.Clear();
            //            }
            //        }
            //        lstFilePath.Add(uploadPath);
            //        context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = lstFilePath;
            //    }

            //    //释放文件流缓存，释放资源
            //    file.InputStream.Flush();
            //    file.InputStream.Dispose();

            //    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            //    return "1";
            //}
            //else
            //{
            //    return "0";
            //}
            #endregion

            return AppFileManage.CreateInstance.UploadAppTempFile(context);
        }

        /// <summary>
        /// 取得上传成功文件的路径
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetUploadFilePath(HttpContext context)
        {
            //当文件上传后，取得上传成功文件的路径,Type三种取值方式:Physical,Http,PhysicalHttp
            //Physical只取得物理路径 Http只取得文件服务器上文件Url地址 HttpPhysical取得物理路径和Url路径
            //json.Remark存放物理路径 json.Info存放Url路径 多个文件间后台字符中用$隔开 
            //前台处理时物理路径注意将$替换成\ Url路径将$替换成/
            PageResult result = new PageResult();
            result.State = 0;
            result.Info = "未能从会话中取得上传文件路径。";
            result.Remark = "ERROR";

            if (context.Session[ConstantValue.SessionKeys.UploadifyFilePath] == null)
            {
                return result.ToString();
            }

            HttpRequest request = HttpContext.Current.Request;
            List<string> lstFilePath = new List<string>();
            //是否多文件上传(0:当文件上传；1：多文件上传)
            int iMulti = -1;

            lstFilePath = (List<string>)context.Session[ConstantValue.SessionKeys.UploadifyFilePath];
            if (lstFilePath == null || lstFilePath.Count == 0)
            {
                //未找到值释放Session
                context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = null;
                return result.ToString();
            }

            string strFilePath = string.Empty;

            //是否单文件还是多文件上传
            if (request.Form["IsMulti"] != null)
            {
                iMulti = Convert.ToInt32(DataUtility.GetPageFormValue(request.Form["IsMulti"], "0"));
            }

            //获取文件路径
            foreach (string str in lstFilePath)
            {
                if (str.Trim() != string.Empty)
                {
                    strFilePath += str + "|";
                }
            }

            if (strFilePath.EndsWith("|"))
            {
                strFilePath = strFilePath.Remove(strFilePath.LastIndexOf("|"), 1);
            }

            //单个文件上传规格及大小验证
            if (iMulti == 0)
            {
                //文件上传大小验证
                //if (!CheckFileSize(request, strFilePath, ref result))
                //{
                //    //文件验证不通过，删除文件
                //    DeleteUploadFile(strFilePath);

                //    //文件验证不通过,释放Session保存的路径
                //    context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = null;
                //    return result.ToString();
                //}
            }

            //返回路径类型
            string strType = DataUtility.GetPageFormValue(request.Form["Type"], string.Empty);
            string strPhysical = context.Server.MapPath("../..") + ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerPath);
            if (strPhysical == null)
            {
                strPhysical = string.Empty;
            }
            string strUrl = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerUrl);
            if (strUrl == null)
            {
                strUrl = string.Empty;
            }
            if (strType != string.Empty)
            {
                result.State = 1;
                result.Info = "取得上传文件路径成功。";
                switch (strType)
                {
                    case "Physical":
                        result.Remark = strFilePath.Replace("\\", "*");
                        break;
                    case "Http":
                        strFilePath = strFilePath.Replace("\\", "*");
                        result.Info = strFilePath.Replace(strPhysical.Replace("\\", "*"), strUrl.Replace("/", "*"));
                        break;
                    case "PhysicalHttp":
                        strFilePath = strFilePath.Replace("\\", "*");
                        result.Remark = strFilePath;
                        result.Info = strFilePath.Replace(strPhysical.Replace("\\", "*"), strUrl.Replace("/", "*"));
                        break;
                }
            }

            //取完路径后释放Session
            context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = null;
            return result.ToString();
        }

        /// <summary>
        /// 上传文件发送错误时，把Session保存的文件路径清空
        /// </summary>
        /// <returns></returns>
        private string ClearSessionOnUploadError(HttpContext context)
        {
            PageResult result = new PageResult();
            result.State = 0;
            if (context.Session[ConstantValue.SessionKeys.UploadifyFilePath] != null)
            {
                context.Session[ConstantValue.SessionKeys.UploadifyFilePath] = null;
            }

            return result.ToString();
        }

        /// <summary>
        /// 上传文件规格及大小验证
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="result">检验结果</param>
        /// <returns>是否通过true:通过；false:不通过</returns>
        private bool CheckFileSize(HttpRequest request, string strFilePath, ref PageResult result)
        {
            try
            {
                string[] strFilePaths = strFilePath.Split('|');
                foreach (string filePath in strFilePaths)
                {
                    if (!System.IO.File.Exists(filePath))
                    {
                        result.State = 0;
                        result.Info = "文件不存在。";
                        return false;
                    }
                    System.Drawing.Image img = null;
                    //最小图片像素
                    if (request.Form["MinPixels"] != null)
                    {
                        string[] strMinPixels = request.Form["MinPixels"].ToString().Trim().Split('*');
                        int iMinWidthPixels = DataUtility.ToInt(strMinPixels[0]);
                        int iMinHeightPixels = DataUtility.ToInt(strMinPixels[1]);

                        img = System.Drawing.Image.FromFile(filePath);
                        if (img.Width < iMinWidthPixels || img.Height < iMinHeightPixels)
                        {
                            result.State = 0;
                            result.Info = string.Format("上传图片的像素不能小于{0}，现上传图片的像素为{1}*{2}", request.Form["MinPixels"].ToString().Trim(), img.Width, img.Height);
                            return false;
                        }
                    }
                    //最大文件规格验证
                    if (request.Form["MaxPixels"] != null)
                    {
                        string[] strMaxPixels = request.Form["MaxPixels"].ToString().Trim().Split('*');
                        int iMaxWidthPixels = DataUtility.ToInt(strMaxPixels[0]);
                        int iMaxHeightPixels = DataUtility.ToInt(strMaxPixels[1]);

                        img = System.Drawing.Image.FromFile(filePath);
                        if (img.Width > iMaxWidthPixels || img.Height > iMaxHeightPixels)
                        {
                            result.State = 0;
                            result.Info = string.Format("上传图片的像素不能大于{0}，现上传图片的像素为{1}*{2}", request.Form["MaxPixels"].ToString().Trim(), img.Width, img.Height);
                            return false;
                        }
                    }

                    //最大文件大小验证
                    if (request.Form["MaxSize"] != null)
                    {
                        //文件大小，单位为KB
                        double dMaxFileSize = DataUtility.ToDouble(request.Form["MaxSize"].ToString().Trim());
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
                        if (fileInfo.Length > dMaxFileSize * 1024)
                        {
                            string strMaxSizeInfo = dMaxFileSize + "KB";
                            double dFileSize = Math.Round(fileInfo.Length / 1024d, 2);
                            string strFileSizeInfo = dFileSize + "KB";
                            //转换为MB
                            if (dFileSize > 1024)
                            {
                                strFileSizeInfo = Math.Round(dFileSize / 1024d, 2) + "MB"; ;
                            }

                            //转换为MB
                            if (dMaxFileSize > 1024)
                            {
                                strMaxSizeInfo = Math.Round(dMaxFileSize / 1024d, 2) + "MB";
                            }

                            result.State = 0;
                            result.Info = string.Format("上传的文件大小不能大于{0}，现上传的文件大小为{1}", strMaxSizeInfo, strFileSizeInfo);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "读取上传文件信息失败。";
                myLogger.Error("读取上传文件信息失败。" + ex.Message, ex);
                return false;
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteUploadFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch
            { }
        }
        #endregion Uploadify上传控件相关操作

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}