using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Semitron_OMS.Common;

namespace Semitron_OMS.CommonWeb
{
    public static class CommonMethods
    {
        /// <summary>
        /// 移除中文中超出长度的文本
        /// </summary>
        /// <param name="str">中文文本</param>
        /// <param name="length">限定长度</param>
        /// <returns></returns>
        public static string RemoveCHN(string str, int length)
        {
            string strDes = str;
            if (!string.IsNullOrEmpty(strDes) && strDes.Length * 2 > length)
            {
                strDes = strDes.Remove((int)length / 2 - 1);
            }
            return strDes;
        }
        /// <summary>
        /// 初始化短信中心格式
        /// </summary>
        /// <param name="SMSC">短信中心号码</param>
        /// <returns>运营商类型，1 移动 2 联通 3电信</returns>
        public static byte InitSMSC(ref string SMSC, string IMSI)
        {
            byte OperatorType = 1;//运营商类型 138是移动 130是联通
            if (!string.IsNullOrEmpty(SMSC))
            {//008613800737500 ,08613800737500 ,+8613800737500
                if (SMSC.IndexOf("+") == 0)
                {
                    SMSC = SMSC.Substring(1);
                }
                if (SMSC.IndexOf("00") == 0)
                {
                    SMSC = SMSC.Substring(2);
                }
                if (SMSC.IndexOf("0") == 0)
                {
                    SMSC = SMSC.Substring(1);
                }
                if (SMSC.IndexOf("86") == 0)
                {
                    SMSC = SMSC.Substring(2);
                }
                if (SMSC.IndexOf("130") == 0)
                {
                    OperatorType = 2;
                }
                if (!string.IsNullOrEmpty(IMSI) && IMSI.Length > 14)
                {
                    string MNC = IMSI.Substring(3, 2);
                    switch (MNC)
                    {
                        case "00":
                        case "02":
                        case "07":
                            OperatorType = 1;
                            break;
                        case "01":
                            OperatorType = 2;
                            break;
                        case "03":
                            OperatorType = 3;
                            break;
                        default:
                            break;
                    }
                }

            }
            return OperatorType;
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            string temp = "";
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                temp = s[i].ToString("X");
                temp = temp.PadLeft(2, '0');
                pwd = pwd + temp;
            }
            pwd = pwd.PadRight(32, '0');
            return pwd;
        }

        /// <summary>
        /// 取得上传文件路径名
        /// </summary>
        /// <param name="strType">配置文件ID</param>
        /// <returns></returns>
        public static string GetUploadFilePath(System.Web.HttpContext context, string strType)
        {
            string strFilePath = context.Server.MapPath("../..");
            strFilePath += ConfigurationManager.AppSettings.Get(strType);
            //生成随机数
            Random random = new Random();
            int iAdd = random.Next(-100, 500);
            DateTime dt = DateTime.Now.AddMilliseconds(iAdd);
            strFilePath += dt.Year + "\\" + dt.Month + "\\" + dt.ToString("ddHHmmssfff");
            return strFilePath;
        }

        /// <summary>
        /// 将list数据装换为以“,”为分割的字符串
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string ListToCommaString(this List<string> lstObj)
        {
            string strRet = string.Empty;

            if (lstObj != null)
            {
                foreach (var obj in lstObj)
                {
                    strRet += "," + obj.ToString();
                }
            }
            if (strRet != string.Empty)
            {
                strRet = strRet.Substring(1);
            }
            return strRet;
        }

        

        /// <summary>
        /// 将List转换为包含cSplit的字符串
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ListToSplitString(this List<string> lstObj, char cSplit)
        {
            string strRet = string.Empty;

            if (lstObj != null)
            {
                foreach (var obj in lstObj)
                {
                    strRet += cSplit + obj.ToString();
                }
            }
            if (strRet != string.Empty)
            {
                strRet = strRet.Substring(1);
            }
            return strRet;
        }

        /// <summary>
        /// 将包含“,”的字符串转换为List
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ListToInSQL(this List<string> lst)
        {
            string strRet = string.Empty;

            if (lst != null)
            {
                foreach (var obj in lst)
                {
                    strRet += ",'" + obj.ToString() + "'";
                }
            }
            if (strRet != string.Empty)
            {
                strRet = strRet.Substring(1);
            }
            return strRet;
        }

        /// <summary>
        /// 根据物理路径获取http地址
        /// </summary>
        /// <param name="strBannerUrl"></param>
        /// <returns></returns>
        public static string GetUrlByPhysical(string strPhysicalAddr)
        {
            if (string.IsNullOrEmpty(strPhysicalAddr)) return string.Empty;

            if (!strPhysicalAddr.Contains("http://"))
            {
                string strUrl = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerUrl);

                //物理路径
                string strPhysical = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerPath);
                string strHttpUrl = strPhysicalAddr.Replace(strPhysical, strUrl).Replace("\\", "/");
                return strHttpUrl;
            }

            return strPhysicalAddr;
        }

        /// <summary>
        /// 根据http获取物理地址
        /// </summary>
        /// <param name="strUrlAddr"></param>
        /// <returns></returns>
        public static string GetPhysicalByUrl(string strUrlAddr)
        {
            if (string.IsNullOrEmpty(strUrlAddr)) return string.Empty;

            if (strUrlAddr.Contains("http://"))
            {
                string strUrl = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerUrl);

                //物理路径
                string strPhysical = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerPath);
                strPhysical = strUrlAddr.Replace(strUrl, strPhysical).Replace("/", "\\");

                return strPhysical;
            }

            return strUrlAddr;
        }

        /// <summary>      
        /// dataTable转换成Json格式      
        /// </summary>      
        /// <param name="dt"></param>      
        /// <returns></returns>      
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            //jsonBuilder.Append("{\"");
            //jsonBuilder.Append(dt.TableName.ToString());
            //jsonBuilder.Append("\":[");
            jsonBuilder.Append("\"[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            //jsonBuilder.Append("}");

            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 移除最后一个字符
        /// </summary>
        public static string RemoveLastChar(string value, string lastChar)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.EndsWith(lastChar))
                {
                    value = value.Remove(value.Length - 1);
                }
            }
            return value;
        }        

        /// <summary>
        /// 从数据库中的标量函数获取汉字字符串拼音首字母串
        /// </summary>
        /// <param name="strText">汉字字符串</param>
        /// <param name="start">起始位置，索引从1开始</param>
        /// <param name="length">获取首字母长度</param>
        /// <returns>拼音首字母串</returns>
        public static string GetSpellDbFunc(string strText, int start, int length)
        {
            try
            {
                string strSql = "SELECT SK=SUBSTRING(UPPER(dbo.fun_getPY('" + strText + "')), " + start + ", " + length + ")";
                object strResult = "";// Semitron_OMS.DBUtility.DbHelperSQL.GetSingle(strSql);
                if (strResult != null)
                {
                    return strResult.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 生成随机数序列
        /// </summary>
        /// <param name="count">生成数量</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机数序列</returns>
        public static int[] GeneratingRandomNums(int count, int minValue, int maxValue)
        {
            int temp = maxValue - minValue;
            if (count > temp || minValue >= maxValue)
            {
                return null;
            }

            List<int> lstNums = new List<int>();
            for (int i = minValue; i < maxValue; i++)
            {
                lstNums.Add(i);
            }

            Random r = new Random();
            int[] randomNums = new int[count];
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                index = r.Next(0, lstNums.Count);
                randomNums[i] = lstNums[index];
                lstNums.RemoveAt(index);
            }

            return randomNums;
        }

        /// <summary>
        /// 随机排序List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(List<T> inputList)
        {
            //Copy to a array    
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);
            //Add range   
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);
            //Set outputList and random    
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);
            while (copyList.Count > 0)
            {
                //Select an index and item       
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];
                //remove it from copyList and add it to output       
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }

        /// <summary>
        /// 初始化存储过程Pro_GetDataByRownumExt的SQL参数数组。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="getFields">查询列名</param>
        /// <param name="startSize">起始行数：大于等于</param>
        /// <param name="endSize">截至行数：小于等于</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrders">排序字符串</param>
        /// <returns>SQL参数数组</returns>
        public static SqlParameter[] InitPro_GetDataByRownumExtSqlParameter(string tableName, string getFields, int startSize, int endSize, string strWhere, string strOrders, int topSize)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tableName", SqlDbType.VarChar, 8000),
                    new SqlParameter("@getFields", SqlDbType.VarChar, 8000),
                    new SqlParameter("@startRownum", SqlDbType.Int,4),
                    new SqlParameter("@endRownum", SqlDbType.Int,4),
                    new SqlParameter("@strWhere", SqlDbType.VarChar, 8000),
                    new SqlParameter("@strOrders", SqlDbType.VarChar, 100),
                    new SqlParameter("@topSize", SqlDbType.Int, 4)
                    };
            parameters[0].Value = tableName;
            parameters[1].Value = getFields;
            parameters[2].Value = startSize;
            parameters[3].Value = endSize;
            parameters[4].Value = strWhere;
            parameters[5].Value = strOrders;
            parameters[6].Value = topSize;
            return parameters;
        }
    }

}
