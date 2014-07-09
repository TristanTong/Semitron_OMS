using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// 读取txt文件信息
    /// </summary>
    public class ReadTxtFile
    {
        /// <summary>
        /// 读取txt文件信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件内容</returns>
        public static string ReadFileString(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                //throw new Exception(string.Format("文件{0}不存在。", filePath));
                return string.Empty;
            }
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    byte[] buffur = new byte[fs.Length];
                    fs.Read(buffur, 0, buffur.Length);
                    string strResult = System.Text.Encoding.UTF8.GetString(buffur);
                    return strResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取文件失败。\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 读取txt文件信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件内容</returns>
        public static List<string> ReadFileList(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new Exception(string.Format("文件{0}不存在。", filePath));
            }

            try
            {
                List<string> lstString = new List<string>();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    string strLine = string.Empty;
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        lstString.Add(strLine);
                    }

                    return lstString;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取文件失败。\r\n" + ex.Message);
            }
        }
    }
}
