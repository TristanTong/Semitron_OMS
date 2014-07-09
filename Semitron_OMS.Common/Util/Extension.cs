using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;

/// <summary>
/// 扩展类
/// </summary>
public static class Extension
{
    #region To Int
    #region Boolean To Int

    public static int ToInt(this Boolean value)
    {
        return (value ? 1 : 0);
    }
    #endregion

    #region DateTime To Int

    public static int ToInt(this DateTime value)
    {
        return ((IConvertible)value).ToInt32(null);
    }
    #endregion

    #region Decimal To Int
    public static int ToInt(this Decimal value)
    {
        return decimal.ToInt32(value);
    }
    #endregion

    #region Double To Int
    public static int ToInt(this Double value)
    {
        if (value >= 0.0)
        {
            if (value < 2147483647.5)
            {
                int num = (int)value;
                double num2 = value - num;
                if ((num2 > 0.5) || ((num2 == 0.5) && ((num & 1) != 0)))
                {
                    num++;
                }
                return num;
            }
        }
        else if (value >= -2147483648.5)
        {
            int num3 = (int)value;
            double num4 = value - num3;
            if ((num4 < -0.5) || ((num4 == -0.5) && ((num3 & 1) != 0)))
            {
                num3--;
            }
            return num3;
        }

        throw new OverflowException("Overflow_Int32");
    }
    #endregion

    #region Int64 To Int32
    public static int ToInt(this Int64 value)
    {
        if ((value < -2147483648L) || (value > 0x7fffffffL))
        {
            throw new OverflowException("Overflow_Int32");
        }
        return (int)value;
    }
    #endregion

    #region Ojbect To ToInt
    /// <summary>
    /// 此方法慎用
    /// 将指定的值转换为有符号整数
    /// 此方法不会抛出异常，转换失败将返回defValue
    /// </summary>
    /// <param name="str">指定对象</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static int ToInt(this object value, int defValue)
    {
        try
        {
            if (value != null)
            {
                return ((IConvertible)value).ToInt32(null);
            }
            else
            {
                return defValue;
            }
        }
        catch (Exception)
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将指定的值转换为有符号整数
    /// </summary>
    /// <param name="str">指定对象</param>
    /// <returns></returns>
    public static int ToInt(this object value)
    {
        if (value != null)
        {
            return ((IConvertible)value).ToInt32(null);
        }
        else
        {
            return 0;
        }
    }
    #endregion
    #endregion

    #region ToDecimal
    /// <summary>
    /// 此方法慎用
    /// 将指定的值转换为Decimal
    /// </summary>
    /// <param name="str">指定对象</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static decimal ToDecimal(this object value, decimal defValue)
    {
        try
        {
            if (value != null)
            {
                return ((IConvertible)value).ToDecimal(null);
            }
            return defValue;
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将指定的值转换为Decimal
    /// </summary>
    /// <param name="str">指定对象</param>
    /// <returns></returns>
    public static decimal ToDecimal(this object value)
    {
        if (value == DBNull.Value)
            return new decimal();
        if (value != null)
        {
            return ((IConvertible)value).ToDecimal(null);
        }
        return 0M;
    }
    #endregion ToDecimal

    #region ToDouble
    /// <summary>
    /// 此方法慎用
    /// 将指定的值转换为Double
    /// </summary>
    /// <param name="str">指定对象</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static double ToDouble(this object value, double defValue)
    {
        try
        {
            if (value != null)
            {
                return ((IConvertible)value).ToDouble(null);
            }
            return defValue;
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将指定的值转换为Double
    /// </summary>
    /// <param name="str">指定对象</param>
    /// <returns></returns>
    public static double ToDouble(this object value)
    {
        if (value != null)
        {
            return ((IConvertible)value).ToDouble(null);
        }
        return 0.0;
    }
    #endregion

    #region IsOper
    /// <summary>
    /// 数据集是否存在数据，只查找第一个表有无数据
    /// </summary>
    /// <param name="ds">指定对象</param>
    /// <returns>是否存在</returns>
    public static bool IsExisitedData(this DataSet ds)
    {
        return IsExisitedData(ds, 0);
    }

    /// <summary>
    /// 数据集是否存在数据
    /// </summary>
    /// <param name="ds">指定对象</param>
    /// <param name="i">第几个表(以0开始的索引)</param>
    /// <returns>是否存在</returns>
    public static bool IsExisitedData(this DataSet ds, int i)
    {
        if (ds == null || ds.Tables.Count == 0 || ds.Tables.Count < i + 1 || ds.Tables[i].Rows.Count == 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 判断DataSet是否存在数据
    /// </summary>
    /// <param name="ds"></param>
    /// <returns></returns>
    public static bool IsExsitData(this DataSet ds)
    {
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 将包含“,”的字符串转换为List
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    public static List<string> CommaStringToList(this string strValue)
    {
        if (string.IsNullOrEmpty(strValue)) return null;

        string[] array = strValue.Split(',');
        List<string> retLst = new List<string>();
        foreach (string str in array)
        {
            if (str.Trim() != string.Empty)
            {
                retLst.Add(str.Trim());
            }
        }

        return retLst;
    }

    /// <summary>
    /// 将包含cSplit的字符串转换为List
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    public static List<string> SplitStringToList(this string strValue, char cSplit)
    {
        string[] array = strValue.Split(cSplit);
        List<string> retLst = new List<string>();
        foreach (string str in array)
        {
            if (str.Trim() != string.Empty)
            {
                retLst.Add(str.Trim());
            }
        }

        return retLst;
    }

    /// <summary>
    /// 根据字段顺序对DataTable的列进行排序
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="strCols"></param>
    public static DataTable SortDataTableCols(this DataTable dt, string strCols)
    {
        if (dt == null || string.IsNullOrEmpty(strCols))
        {
            return dt;
        }

        List<string> lstCols = strCols.CommaStringToList();
        int iColCount = dt.Columns.Count;
        int index = 0;
        foreach (string str in lstCols)
        {
            if (dt.Columns.Contains(str))
            {
                dt.Columns[str].SetOrdinal(index);
            }
            index++;
            if (index >= iColCount)
            {
                break;
            }
        }

        return dt;
    }

    /// <summary>
    /// 数据表是否存在数据
    /// </summary>
    /// <param name="dt">指定对象</param>
    /// <returns>是否存在</returns>
    public static bool IsExisitedData(this DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return false;
        }
        return true;
    }
    #endregion IsOper

    #region ControlsOper

    /// <summary>
    /// 遍历元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="action"></param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }
    #endregion ControlsOper
}