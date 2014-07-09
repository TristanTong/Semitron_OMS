using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Semitron_OMS.Common
{
    /// <summary>
    ///  数据装换类
    /// </summary>
    public static class DataUtility
    {
        /// <summary>
        /// 转换为整形
        /// </summary>
        /// <param name="value"></param>
        /// <returns>整形数据</returns>
        public static int ToInt(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为整形
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static int ToInt(object value, int defaultvalue)
        {
            int result = defaultvalue;

            try
            {
                result = int.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }


        /// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Double类型数据</returns>
        public static double ToDouble(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0d;
            }
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0d;
            }
        }

        /// <summary>
        /// 转换为Decimal类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal类型数据</returns>
        public static decimal ToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0m;
            }
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0m;
            }
        }

        /// <summary>
        /// 转换为字符型数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns>字符型数据</returns>
        public static string ToStr(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }
            try
            {
                return value.ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 转换为时间类型数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>时间类型数据</returns>
        public static DateTime ToDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return Convert.ToDateTime("1900-01-01 00:00:00");
            }
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return Convert.ToDateTime("1900-01-01 00:00:00");
            }
        }

        /// <summary>
        /// 判断是否为时间数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }

            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetPageFormValue(object objValue, object objDefaultValue)
        {
            string strRetValue = string.Empty;
           
            if (objValue != null)
            {
                strRetValue = ToStr(objValue);
            }
            if (string.IsNullOrEmpty(strRetValue) && objDefaultValue != null)
            {
                strRetValue = ToStr(objDefaultValue);
            }

            return strRetValue;
        }

        /// <summary>
        /// 判断DataSet是否包含列，并且该列的值是否为空
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="strColumnName">列名称</param>
        /// <returns></returns>
        public static bool IsColumnHasValue(DataRow dr, string strColumnName)
        {
            if (dr != null)
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    if (col.ColumnName == strColumnName)
                    {
                        if (ToStr(dr[strColumnName]) != string.Empty)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
