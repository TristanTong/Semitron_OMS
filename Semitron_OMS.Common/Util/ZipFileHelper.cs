using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ZipFile.ZipHelper.Zip;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// Zip文件压缩解压类
    /// </summary>
    public class ZipFileHelper
    {
        /// <summary>
        ///  压缩文件
        /// </summary>
        /// <param name="files">文件列表信息</param>
        /// <param name="zipFilePath">要压缩的文件路径</param>
        public static void CreateZipFile(string[] files, string zipFilePath)
        {
            try
            {
                string[] filenames = files;
                using (ZipOutputStream stream = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    stream.SetLevel(9); // 压缩级别 0-9
                    //s.Password = "123"; //Zip压缩文件密码
                    byte[] buffer = new byte[4096]; //缓冲区大小
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(System.IO.Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        stream.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                stream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    stream.Finish();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("压缩文件出现异常。\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipFilePath">Zip文件源路径</param>
        /// <param name="folderPath">解压的文件路径</param>
        public static void UnZipFile(string zipFilePath, string folderPath)
        {
            if (!File.Exists(zipFilePath))
            {
                throw new Exception("找不到文件：" + zipFilePath);
            }

            try
            {
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                using (ZipInputStream zipStr = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = zipStr.GetNextEntry()) != null)
                    {
                        string fileName = System.IO.Path.GetFileName(theEntry.Name);
                        if (fileName != string.Empty)
                        {
                            fileName = theEntry.Name.Replace("/", "\\");
                            string strPath = System.IO.Path.Combine(folderPath, fileName);
                            string stFolderPath = System.IO.Path.GetDirectoryName(strPath);
                            if (!System.IO.Directory.Exists(stFolderPath))
                            {
                                System.IO.Directory.CreateDirectory(stFolderPath);
                            }
                            using (FileStream streamWriter = File.Create(strPath))
                            {
                                int size = 2048;
                                byte[] data = new byte[size];
                                while (true)
                                {
                                    size = zipStr.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("解压文件出现异常。\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 根据指定的文件名解压文件
        /// </summary>
        /// <param name="zipFilePath">Zip文件源路径</param>
        /// <param name="folderPath">解压的文件路径</param>
        public static void UnZipFileByFileName(string zipFilePath, string folderPath,string strFileName)
        {
            if (!File.Exists(zipFilePath))
            {
                throw new Exception("找不到文件：" + zipFilePath);
            }
            
            try
            {
                strFileName = strFileName.Replace("/","\\");
                //if (!System.IO.Directory.Exists(folderPath))
                //{
                //    System.IO.Directory.CreateDirectory(folderPath);
                //}
                using (ZipInputStream zipStr = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = zipStr.GetNextEntry()) != null)
                    {
                        string fileName = System.IO.Path.GetFileName(theEntry.Name);
                        if (fileName != string.Empty)
                        {
                            fileName = theEntry.Name.Replace("/", "\\");
                            string strPath = System.IO.Path.Combine(folderPath, fileName);
                            if (strPath.Contains(strFileName))
                            {
                                string stFolderPath = System.IO.Path.GetDirectoryName(strPath);
                                if (!System.IO.Directory.Exists(stFolderPath))
                                {
                                    System.IO.Directory.CreateDirectory(stFolderPath);
                                }
                                using (FileStream streamWriter = File.Create(strPath))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[size];
                                    while (true)
                                    {
                                        size = zipStr.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("解压文件出现异常。\r\n" + ex.Message);
            }
        }
    }
}
