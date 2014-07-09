using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    public class SQLUtility
    {
        /// <summary>
        /// 消除SQL的注入攻击
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertSQL(string value)
        {
            string sql = ConvertToSQL(value);

            return string.Format("'{0}'", sql);
        }

        /// <summary>
        /// 消除SQL的注入攻击
        /// </summary>
        /// <param name="i_sql"></param>
        /// <returns></returns>
        public static string ConvertToSQL(string i_sql)
        {
            string retSql = i_sql;

            retSql = retSql.Replace("'", "''");
            retSql = retSql.Replace("/", "//");
            retSql = retSql.Replace("%", "/%");
            retSql = retSql.Replace("[", "/[");
            retSql = retSql.Replace("_", "/_");

            return retSql;
        }
    }
}
