using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// SQL相关操作公用类
    /// </summary>
    public static class SQLOperateHelper
    {
        /// <summary>
        /// 用旧实体非空字段值填充设置新实体类字段的值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="oldModel"></param>
        public static void SetEntityByOldNoNull<T>(T model, T oldModel)
        {
            Type type = model.GetType();
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                if (propInfo == null)
                {
                    continue;
                }
                Type changeType = propInfo.PropertyType;

                if (changeType.Equals(typeof(Nullable<>)))
                {
                    if (propInfo.GetValue(model, null) == null && propInfo.GetValue(oldModel, null) != null)
                    {
                        propInfo.SetValue(model, propInfo.GetValue(oldModel, null), null);
                    }
                }
                //泛型空类型
                if (changeType.IsGenericType && changeType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (propInfo.GetValue(model, null) == null && propInfo.GetValue(oldModel, null) != null)
                    {
                        propInfo.SetValue(model, propInfo.GetValue(oldModel, null), null);
                    }
                }

                //整形或数值型，0为属性默认值,当为0时即需要对新实体赋值旧实体值
                if (changeType == typeof(Int32) || changeType == typeof(Decimal))
                {
                    //新实体为默认值0且旧实体不为默认值0时,需给新实体值
                    if (propInfo.GetValue(model, null).ToString() == Convert.ChangeType(0, changeType, CultureInfo.CurrentCulture).ToString()
                        && propInfo.GetValue(oldModel, null).ToString() != Convert.ChangeType(0, changeType, CultureInfo.CurrentCulture).ToString())
                    {
                        propInfo.SetValue(model, propInfo.GetValue(oldModel, null), null);
                    }
                }

                if (changeType.ToString().ToUpper().Contains("SYSTEM.DATETIME"))
                {

                }

            }
        }
        /// <summary>
        /// 设置实体类字段的值
        /// </summary>
        /// <param name="objField">实体类</param>
        /// <param name="strFieldName">字段名称</param>
        /// <param name="objValue">要设置的值</param>
        /// <param name="objDefaultValue">默认值</param>
        public static void SetEntityFiledValue(object objField,
                                              string strFieldName,
                                              object objValue,
                                              params object[] objDefaultValue)
        {
            if (objValue == null || string.IsNullOrEmpty(strFieldName))
            {
                return;
            }
            Type type = objField.GetType();
            PropertyInfo propInfo = type.GetProperty(strFieldName);
            if (propInfo == null)
            {
                return;
            }
            Type changeType = propInfo.PropertyType;

            if (changeType.IsGenericType && changeType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                //对空时间类型，将空字符串转换为null
                if (changeType.ToString().ToUpper().Contains("SYSTEM.DATETIME") && objValue.ToString() == string.Empty)
                {
                    propInfo.SetValue(objField, null, null);
                    return;
                }
                //对可空类型进行转换为基础类型
                NullableConverter nullableConverter = new NullableConverter(changeType);
                changeType = nullableConverter.UnderlyingType;
            }

            //对默认值进行判断，并赋值
            if (string.IsNullOrEmpty(objValue.ToString().Trim()))
            {
                if (objDefaultValue != null && objDefaultValue.Length > 0)
                {
                    //对属性进行赋值
                    propInfo.SetValue(objField, Convert.ChangeType(objDefaultValue[0], changeType, CultureInfo.CurrentCulture), null);
                }
                else
                {
                    string strValue = string.Empty;
                    if (changeType == typeof(Int32) || changeType == typeof(Decimal))
                    {
                        int o_Result;
                        int.TryParse(objValue.ToString().Trim(), out o_Result);
                        strValue = o_Result.ToString();
                    }
                    else if (changeType.ToString().ToUpper().Contains("SYSTEM.DATETIME"))
                    {
                        DateTime o_Result;
                        DateTime.TryParse(objValue.ToString().Trim(), out o_Result);
                        strValue = o_Result.ToString();
                    }

                    propInfo.SetValue(objField, Convert.ChangeType(strValue, changeType, CultureInfo.CurrentCulture), null);
                }
            }
            else//没有默认值的情况
            {
                propInfo.SetValue(objField, Convert.ChangeType(objValue, changeType, CultureInfo.CurrentCulture), null);
            }
        }

        /// <summary>
        /// 添加SQL条件过滤器
        /// </summary>
        /// <param name="lstFilter"></param>
        /// <param name="conditionFilter"></param>
        public static void AddSQLFilter(List<SQLConditionFilter> lstFilter, SQLConditionFilter conditionFilter)
        {
            if (conditionFilter != null)
            {
                if (!lstFilter.Contains(conditionFilter))
                {
                    lstFilter.Add(conditionFilter);
                }
            }
        }

        /// <summary>
        /// 生成条件过滤器
        /// </summary>
        /// <param name="strFieldName">字段名</param>
        /// <param name="objValue">条件值</param>
        /// <param name="conditionEnm">条件枚举类型</param>
        /// <returns></returns>
        public static SQLConditionFilter GetSQLFilter(string strFieldName, object objValue, ConditionEnm conditionEnm)
        {
            return GetSQLFilter(string.Empty, strFieldName, objValue, conditionEnm);
        }

        /// <summary>
        /// 生成条件过滤器
        /// </summary>
        /// <param name="strFieldName">字段名</param>
        /// <param name="objValue">条件值</param>
        /// <param name="conditionEnm">第二个条件值</param>
        /// <returns></returns>
        public static SQLConditionFilter GetSQLFilter(string strFieldName, object objValue, object objSecondValue)
        {
            return GetSQLFilter(string.Empty, strFieldName, objValue, objSecondValue, ConditionEnm.Between);
        }

        /// <summary>
        /// 生成条件过滤器
        /// </summary>
        /// <param name="strFieldName">数据表别名</param>
        /// <param name="strFieldName">字段名</param>
        /// <param name="objValue">条件值</param>
        /// <param name="conditionEnm">第二个条件值</param>
        /// <returns></returns>
        public static SQLConditionFilter GetSQLFilter(string strAliasTableName,
            string strFieldName,
            object objValue,
            object objSecondValue)
        {
            return GetSQLFilter(strAliasTableName, strFieldName, objValue, objSecondValue, ConditionEnm.Between);
        }

        /// <summary>
        /// 生成条件过滤器
        /// </summary>
        /// <param name="strAliasTableName">数据表别名</param>
        /// <param name="strFieldName">字段名</param>
        /// <param name="objValue">条件值</param>
        /// <param name="conditionEnm">条件枚举类型</param>
        /// <returns></returns>
        public static SQLConditionFilter GetSQLFilter(string strAliasTableName,
            string strFieldName,
            object objValue,
            ConditionEnm conditionEnm)
        {
            return GetSQLFilter(string.Empty, strFieldName, objValue, null, conditionEnm);
        }

        /// <summary>
        /// 生成条件过滤器
        /// </summary>
        /// <param name="strAliasTableName">数据表别名</param>
        /// <param name="strFieldName">字段名</param>
        /// <param name="objValue">条件值</param>
        /// <param name="conditionEnm">条件枚举类型</param>
        /// <returns></returns>
        public static SQLConditionFilter GetSQLFilter(string strAliasTableName,
            string strFieldName,
            object objValue,
            object objSecondValue,
            ConditionEnm conditionEnm)
        {
            SQLConditionFilter conditionFilter = null;
            string strValue = DataUtility.ToStr(objValue);
            if (string.IsNullOrEmpty(strFieldName) ||
                (DataUtility.ToStr(objValue) == string.Empty &&
                DataUtility.ToStr(objSecondValue) == string.Empty))
            {
                return conditionFilter;
            }

            conditionFilter = new SQLConditionFilter()
            {
                AliasTableName = strAliasTableName,
                FiledName = strFieldName,
                Value = objValue,
                SecondValue = objSecondValue,
                ConditionEnm = conditionEnm
            };
            return conditionFilter;
        }

        /// <summary>
        /// 根据查询条件过滤器生成查询条件语句
        /// </summary>
        /// <param name="lstConditionFilter"></param>
        /// <returns></returns>
        public static string GetSQLCondition(List<SQLConditionFilter> lstConditionFilter, bool bIsAddWhere)
        {
            string strConditon = string.Empty;
            if (bIsAddWhere == true)
            {
                strConditon += " WHERE 1=1 ";
            }
            else
            {
                strConditon += " 1=1 ";
            }
            if (lstConditionFilter.Count > 0)
            {
                foreach (SQLConditionFilter conditionFilter in lstConditionFilter)
                {
                    if (conditionFilter != null)
                    {
                        string strRetConditon = conditionFilter.GetCondition();
                        if (!string.IsNullOrEmpty(strRetConditon))
                        {
                            strConditon += "\r\n    " + strRetConditon;
                        }
                    }
                }
            }
            return strConditon;
        }


    }
}
