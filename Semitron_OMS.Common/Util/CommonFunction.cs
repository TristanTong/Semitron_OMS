using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Linq.Expressions;
using System.Data;

namespace Semitron_OMS.Common
{
    public class CommonFunction
    {
        /// <summary>
        /// 比对两个对象。
        /// </summary>
        /// <param name="oldObj">修改前</param>
        /// <param name="newObj">修改后</param>
        /// <returns>两对象不同的信息。</returns>
        public static string ContrastTowObj(object oldObj, object newObj)
        {
            string msg = "";
            PropertyInfo[] oldProperts = oldObj.GetType().GetProperties();
            PropertyInfo[] newProperts = newObj.GetType().GetProperties();
            object oldValue = "";
            object newValue = "";
            for (int i = 0; i < oldProperts.Length; i++)
            {
                oldValue = oldProperts[i].GetValue(oldObj, null);
                newValue = newProperts[i].GetValue(newObj, null);
                oldValue = oldValue == null ? "" : oldValue.ToString();
                newValue = newValue == null ? "" : newValue.ToString();
                if (oldValue.ToString() != newValue.ToString())
                {
                    msg += "修改前," + oldProperts[i].Name + ":" + oldValue + ",";
                    msg += "修改后," + newProperts[i].Name + ":" + newValue + "；";
                }
            }
            if (msg.EndsWith("；"))
            {
                msg.Remove(msg.Length - 1);
            }
            return msg;
        }

        #region 数据集数据表分页处理

        /// <summary>
        /// 为一个DataTable添加rownum列
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetRownumTable(DataTable dt)
        {
            DataTable dtTemp = dt.Copy();
            if (!dtTemp.Columns.Contains("rownum"))
            {
                DataColumn dc = new DataColumn("rownum", typeof(int));
                dtTemp.Columns.Add(dc);
            }
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                dtTemp.Rows[i - 1]["rownum"] = i;
            }

            return dtTemp;
        }

        /// <summary>
        /// 对数据表进行分布数据提取返回
        /// </summary>
        /// <param name="dt">源数据表</param>
        /// <param name="iPageSize">页面显示数据行数</param>
        /// <param name="iPageIndex">页面索引</param>
        /// <returns>分页后的单页数据表</returns>
        public static DataTable GetCurrentPageTable(DataTable dt, int iPageSize, int iPageIndex)
        {
            DataTable dtTemp = dt.Clone();
            dtTemp.Clear();
            DataRow[] arrDataRow = dt.Select("rownum>" + (iPageIndex - 1) * iPageSize + "and rownum<=" + iPageIndex * iPageSize);
            foreach (DataRow row in arrDataRow)
                dtTemp.ImportRow(row);

            return dtTemp;
        }

        /// <summary>
        /// 由现有数据表得到当前页小计行的各列汇总数据值
        /// </summary>
        /// <param name="dt">待处理的数据表</param>
        /// <param name="strContentColumnName">显示小计文本的列名</param>
        /// <param name="strContent">显示内容</param>
        /// <returns>数据行各列数据数组</returns>
        public static object[] SumDataNewRow(DataTable dt, string strContentColumnName, string strContent)
        {
            object[] arrObj = new object[dt.Columns.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == typeof(int))
                    {
                        if (dt.Columns[j].ColumnName.ToUpper() == "ID" || dt.Columns[j].ColumnName.ToUpper() == "ROWNUM")
                        {
                            arrObj[j] = 0;
                        }
                        else
                        {
                            long intTemp = 0;
                            if (long.TryParse(dt.Rows[i][j].ToString().Trim(), out intTemp))
                            {
                                intTemp += Convert.ToInt64(arrObj[j]);
                                arrObj[j] = intTemp;
                            }
                        }
                    }
                    if (dt.Columns[j].DataType == typeof(double))
                    {
                        double doubleTemp = 0;
                        if (double.TryParse(dt.Rows[i][j].ToString().Trim(), out doubleTemp))
                        {
                            doubleTemp += Convert.ToDouble(arrObj[j]);
                            arrObj[j] = doubleTemp;
                        }
                    }
                    if (dt.Columns[j].DataType == typeof(decimal))
                    {
                        decimal decimalTemp = 0;
                        if (decimal.TryParse(dt.Rows[i][j].ToString().Trim(), out decimalTemp))
                        {
                            decimalTemp += Convert.ToDecimal(arrObj[j]);
                            arrObj[j] = decimalTemp;
                        }
                    }
                    if (dt.Columns[j].DataType == typeof(string))
                    {
                        if (string.IsNullOrEmpty(strContentColumnName))
                        {
                            arrObj[j] = strContent;
                        }
                        else
                        {
                            if (dt.Columns[j].ColumnName == strContentColumnName)
                            {
                                arrObj[j] = strContent;
                            }
                        }
                    }
                }
            }
            return arrObj;
        }

        /// <summary>
        /// 由现有数据表得到总计行的各列汇总数据值
        /// </summary>
        /// <param name="dt">待处理的数据源表</param>
        /// <param name="strContentColumnName">显示总计文本的列名</param>
        /// <param name="strContent">显示内容</param>
        /// <returns>数据行各列数据数组</returns>
        public static object[] SumTotalDataNewRow(DataTable dt, string strContentColumnName, string strContent)
        {
            object[] arrObj = new object[dt.Columns.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].DataType == typeof(int))
                    {
                        if (dt.Columns[j].ColumnName.ToUpper() == "ID" || dt.Columns[j].ColumnName.ToUpper() == "ROWNUM")
                        {
                            arrObj[j] = 0;
                        }
                        else
                        {
                            long intTemp = 0;
                            if (long.TryParse(dt.Rows[i][j].ToString().Trim(), out intTemp))
                            {
                                intTemp += Convert.ToInt64(arrObj[j]);
                                arrObj[j] = intTemp;
                            }
                        }
                    }
                    if (dt.Columns[j].DataType == typeof(double))
                    {
                        double doubleTemp = 0;
                        if (double.TryParse(dt.Rows[i][j].ToString().Trim(), out doubleTemp))
                        {
                            doubleTemp += Convert.ToDouble(arrObj[j]);
                            arrObj[j] = doubleTemp;
                        }
                    }
                    if (dt.Columns[j].DataType == typeof(decimal))
                    {
                        decimal decimalTemp = 0;
                        if (decimal.TryParse(dt.Rows[i][j].ToString().Trim(), out decimalTemp))
                        {
                            decimalTemp += Convert.ToDecimal(arrObj[j]);
                            arrObj[j] = decimalTemp;
                        }
                    }
                    if (dt.Columns[j].DataType == typeof(string))
                    {
                        if (string.IsNullOrEmpty(strContentColumnName))
                        {
                            arrObj[j] = strContent;
                        }
                        else
                        {
                            if (dt.Columns[j].ColumnName == strContentColumnName)
                            {
                                arrObj[j] = strContent;
                            }
                        }
                    }
                }
            }
            return arrObj;
        }

        /// <summary>
        /// 向现有数据表中加入小计行
        /// </summary>
        /// <param name="dt">待处理的数据表</param>
        /// <param name="strContentColumnName">显示小计文本的列名</param>
        /// <param name="strContent">显示内容</param>
        public static void SumDataTable(DataTable dt, string strContentColumnName, string strContent)
        {
            object[] arrObj = SumDataNewRow(dt, strContentColumnName, strContent);

            if (dt.Rows.Count > 0)
            {
                DataRow drCurrent = dt.NewRow();
                drCurrent.ItemArray = arrObj;
                dt.Rows.Add(drCurrent);
            }
        }

        /// <summary>
        /// 向现有数据表中加入总计行
        /// </summary>
        /// <param name="dt">待处理的数据源表</param>
        /// <param name="dtTemp">已经过小计处理的数据表</param>
        /// <param name="strContentColumnName">显示总计文本的列名</param>
        /// <param name="strContent">显示内容</param>
        /// <returns>转换好的数据表</returns>
        public static DataTable SumTotalDataTable(DataTable dt, DataTable dtTemp, string strContentColumnName, string strContent)
        {
            DataTable dtTotalTemp = dtTemp;
            object[] arrObj = SumTotalDataNewRow(dt, strContentColumnName, strContent);

            if (dtTotalTemp.Rows.Count > 0)
            {
                DataRow drCurrent = dtTotalTemp.NewRow();
                drCurrent.ItemArray = arrObj;
                dtTotalTemp.Rows.Add(drCurrent);
            }
            return dtTotalTemp;
        }
        #endregion 数据集数据表分页处理

        #region 获得汉字或汉字字符串拼音首字母
        /// <summary>
        /// 用来获得一个字符串的每个字的拼音首字母组成所需的字符串
        /// </summary>
        /// <param name="strText">汉字字符串</param>
        /// <param name="iLength">首字母组合长度</param>
        /// <returns>字符串每个字的拼音首字母组合</returns>
        public static string GetChineseSpell(string strText, int iLength)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetSpell(strText.Substring(i, 1));
            }
            if (myStr.Length > iLength)
            {
                myStr = myStr.Substring(0, iLength);
            }
            return myStr;
        }

        /// <summary>
        /// 用来获得一个汉字的拼音首字母
        /// </summary>
        /// <param name="cnChar">汉字</param>
        /// <returns>拼音首字母</returns>
        public static string GetSpell(string cnChar)
        {
            //将汉字转化为ASNI码,二进制序列
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);

            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = {45217,45253,45761,46318,46826,47010,47297,47614,48119,48119,49062,
            49324,49896,50371,50614,50622,50906,51387,51446,52218,52698,52698,52698,52980,53689,
            54481};
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) }).ToUpper();
                    }
                }
                return "";
            }
            else return cnChar.ToUpper();
        }

        #endregion 获得汉字或汉字语句拼音首字母

        #region 日期处理
        /// <summary>
        /// 判定公历闰年遵循的一般规律为：四年一闰，百年不闰，四百年再闰。
        /// 公历闰年的精确计算方法：（按一回归年365天5小时48分45.5秒）
        /// 普通年能被4整除而不能被100整除的为闰年。 （如2004年就是闰年，1900年不是闰年）
        /// 世纪年能被400整除而不能被3200整除的为闰年。 (如2000年是闰年，3200年不是闰年)
        /// 对于数值很大的年份能整除3200,但同时又能整除172800则又是闰年。(如172800年是闰年，86400年不是闰年）
        /// 
        /// 公元前闰年规则如下：
        /// 非整百年：年数除4余数为1是闰年，即公元前1、5、9……年；
        /// 整百年：年数除400余数为1是闰年，年数除3200余数为1，不是闰年,年数除172800余1又为闰年，即公元前401、801……年。
        /// </summary>
        /// <param name="yN">年份数字</param>
        /// <returns></returns>
        public static bool IsLeap(int yN)
        {

            if ((yN % 400 == 0 && yN % 3200 != 0)
               || (yN % 4 == 0 && yN % 100 != 0)
               || (yN % 3200 == 0 && yN % 172800 == 0))
                return true;
            else
                return false;

        }

        /// <summary>
        /// 取得某一日期所在月份的最后一天所表示的数值
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int GetLastDayOfMonth(DateTime datetime)
        {
            int month = datetime.Month;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if (IsLeap(datetime.Year) == true)
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }
            }
            return 30;
        }

        #endregion 日期处理
    }


    public static class PropertySupport
    {
        /// <summary>
        /// lambda,使用帮助:PropertySupport.ExtractPropertyName(() => Object_X.Property_X);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                return null;
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                return null;
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                return null;
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
                return null;
            }

            return memberExpression.Member.Name;
        }
    }
}
