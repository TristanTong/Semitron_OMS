using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    public class SQLConditionFilter
    {
        /// <summary>
        /// 数据表别名
        /// </summary>
        public string AliasTableName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FiledName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 第二个值，用于Between条件生成条件语句
        /// </summary>
        public object SecondValue
        {
            get;
            set;
        }

        /// <summary>
        /// 条件
        /// </summary>
        public ConditionEnm ConditionEnm
        {
            get;
            set;
        }

        /// <summary>
        /// 子集合
        /// </summary>
        public List<SQLConditionFilter> Items
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLConditionFilter()
        {
            AliasTableName = string.Empty;
            Items = new List<SQLConditionFilter>();
        }

        /// <summary>
        /// 生成获取条件
        /// </summary>
        /// <returns></returns>
        public string GetCondition()
        {
            return GetCondition(this);
        }

       /// <summary>
       /// 生成查询条件
       /// </summary>
       /// <param name="filter"></param>
       /// <returns></returns>
        private string GetCondition(SQLConditionFilter filter)
        {
            string strCondition = string.Empty;
            string strValue = filter.Value.ToString().Trim();
            switch (filter.ConditionEnm)
            {
                //不等于
                case Common.ConditionEnm.NoEqual:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " != " + SQLUtility.ConvertSQL(strValue);
                    break;
                //相等
                case ConditionEnm.Equal:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " = " + SQLUtility.ConvertSQL(strValue);
                    break;
                //大于
                case ConditionEnm.GreaterThan:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " > " + SQLUtility.ConvertSQL(strValue);
                    break;
                //小于
                case ConditionEnm.LessThan:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " < " + SQLUtility.ConvertSQL(strValue);
                    break;
                //范围检索
                case ConditionEnm.Between:
                    if (!IsValueEmpty(Value) && !IsValueEmpty(SecondValue))
                    {
                        strCondition += " AND " + ConvertSQLFiled(filter) + " BETWEEN "
                            + SQLUtility.ConvertSQL(strValue) + " AND " + SQLUtility.ConvertSQL(filter.SecondValue.ToString().Trim());
                    }
                    else if (!IsValueEmpty(Value))
                    {
                        strCondition += " AND " + ConvertSQLFiled(filter) + " >= " + SQLUtility.ConvertSQL(strValue);
                    }
                    else if (!IsValueEmpty(SecondValue))
                    {
                        strCondition += " AND " + ConvertSQLFiled(filter) + " <= " + SQLUtility.ConvertSQL(filter.SecondValue.ToString().Trim());
                    }
                    break;
                //大于等于
                case ConditionEnm.GreaterEqual:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " >= " + SQLUtility.ConvertSQL(strValue);
                    break;
                //小于等于等于
                case ConditionEnm.LessEqual:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " <= " + SQLUtility.ConvertSQL(strValue);
                    break;
                //左模糊
                case ConditionEnm.LeftLike:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " LIKE '" + SQLUtility.ConvertToSQL(strValue) + "%'";
                    break;
                //右模糊
                case ConditionEnm.RightLike:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " LIKE '%" + SQLUtility.ConvertToSQL(strValue) + "'";
                    break;
                //全字段模糊匹配
                case ConditionEnm.AllLike:
                    strCondition += " AND " + ConvertSQLFiled(filter) + " LIKE '%" + SQLUtility.ConvertToSQL(strValue) + "%'";
                    break;
                //或者
                case Common.ConditionEnm.OR:
                    strCondition += " OR " + ConvertSQLFiled(filter) + " = " + SQLUtility.ConvertSQL(strValue);
                    break;
                    //IN(各个值以符号 ”|“ 相隔开)
                case Common.ConditionEnm.IN:
                    if (!IsValueEmpty(filter.Value))
                    {
                        strCondition += " AND " + ConvertSQLFiled(filter) + " IN (" + GetInSQL(filter.Value.ToString()) + ")";
                    }
                    break;
                case Common.ConditionEnm.NoFieldAnd:
                    //是否有子集合条件
                    if (filter.Items != null && filter.Items.Count > 0)
                    {
                        strCondition += " AND (";
                        foreach (SQLConditionFilter subFilter in filter.Items)
                        {
                            strCondition += GetCondition(subFilter);
                        }
                        strCondition = strCondition.Replace(" AND ( AND"," AND ( ");
                        strCondition = strCondition.Replace(" AND ( OR", " AND ( ");
                        strCondition += ")";
                    }
                    break;
                case Common.ConditionEnm.NoFieldOr:
                    //是否有子集合条件
                    if (filter.Items != null && filter.Items.Count > 0)
                    {
                        strCondition += " OR (";
                        foreach (SQLConditionFilter subFilter in filter.Items)
                        {
                            strCondition += GetCondition(subFilter);
                        }
                        strCondition = strCondition.Replace(" OR ( AND", " OR (");
                        strCondition = strCondition.Replace(" OR ( OR", " OR (");
                        strCondition += ")";
                    }
                    break;
                //不匹配任何的类型
                case Common.ConditionEnm.None:
                    
                    if (!IsValueEmpty(filter.Value))
                    {
                        strCondition += " AND " + filter.Value.ToString().Trim();
                    }
                    break;
                default:
                    break;
            }
            return strCondition;
        }

        /// <summary>
        /// 判断值是否为空
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public bool IsValueEmpty(object objValue)
        {
            if (objValue == null)
            {
                return true;
            }
            if (objValue.ToString().Trim() == string.Empty)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 生成IN条件语句
        /// </summary>
        /// <param name="strINValue">要传入的IN值集合，以符号 "," 隔开</param>
        /// <returns></returns>
        private string GetInSQL(string strINValue)
        {
            string strRetValue = string.Empty;
            string[] strValues = strINValue.Split(',');
            foreach (string strValue in strValues)
            {
                strRetValue += "," + SQLUtility.ConvertSQL(strValue);
            }

            return strRetValue == string.Empty ? string.Empty : strRetValue.Substring(1);
        }

        /// <summary>
        /// 生成Sql的拼接字段
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        private string ConvertSQLFiled(SQLConditionFilter filter)
        {
            string strRetValue = string.Empty;
            if (string.IsNullOrEmpty(filter.AliasTableName))
            {
                strRetValue = filter.FiledName;
            }
            else
            {
                strRetValue = filter.AliasTableName + "." + filter.FiledName;
            }

            return strRetValue;
        }
    }

    /// <summary>
    /// 数据查询条件枚举类型
    /// </summary>
    public enum ConditionEnm : int
    {
        /// <summary>
        /// 不匹配任何数据
        /// </summary>
        None = -2,

        /// <summary>
        /// 不相等
        /// </summary>
        NoEqual = -1,

        /// <summary>
        /// 等于
        /// </summary>
        Equal = 0,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 1,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 2,

        /// <summary>
        /// 条件范围
        /// </summary>
        Between = 3,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterEqual = 4,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual = 5,

        /// <summary>
        /// 左模糊
        /// </summary>
        LeftLike = 6,

        /// <summary>
        /// 右模糊
        /// </summary>
        RightLike = 7,

        /// <summary>
        /// 全匹配模糊
        /// </summary>
        AllLike = 8,

        /// <summary>
        /// 或者
        /// </summary>
        OR = 9,

        //集合
        IN = 10,

        NoFieldAnd = 11,

        NoFieldOr =12,
    }
}
