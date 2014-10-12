using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Caching;
using System.Web;
using System.Diagnostics;
using System.Configuration;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Common.Logger;
using Semitron_OMS.Common;

namespace Semitron_OMS.DAL
{
    public class SQLNotifier
    {
        public SQLNotifier()
        {
        }

        private static List<string> lstSqlDependencySql = new List<string>();

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        protected static void ClearCache(string cacheName)
        {
            System.Collections.IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                // 只清除与此业务相关的缓存，根据表名
                if (!string.IsNullOrEmpty(cacheName) && cacheEnum.Key.ToString().ToLower().IndexOf(cacheName.ToLower()) > 0)
                    HttpRuntime.Cache.Remove(cacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// 创建Service Borker通知(请确认Service Borker已开启)，自动响应程序表发生的更改，自动设定缓存机制
        /// ALTER DATABASE DatabaseName SET NEW_BROKER WITH ROLLBACK IMMEDIATE;
        /// ALTER DATABASE Databasename SET ENABLE_BROKER;
        /// SELECT is_broker_enabled FROM sys.databases WHERE name = 'DBNAME'
        /// </summary>
        /// <param name="pageCache">System.Web.Caching.Cache对象</param>
        /// <param name="selectSql">查询数据的sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="notifyDepSql">通知依赖SQL，用于监控数据变更</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string selectSql, string notifyDepSql, string tableName, SqlParameter[] parameters)
        {
            // 用于Service Broker跟踪的表范围sql
            DataTable dt = new DataTable();
            return dt = DbHelperSQL.GetDataTable(selectSql, parameters);

            //暂停使用
            if (HttpRuntime.Cache[notifyDepSql] != null)
            {
                dt = HttpRuntime.Cache[notifyDepSql] as DataTable;
                LogHelper.WriteLogInfo(notifyDepSql + "从缓存中取出数据", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            else
            {
                //兼容多个查询条件，绑定一个表通知情况。
                //当通知依赖语句不存在时，才创建通知依赖。
                if (!lstSqlDependencySql.Contains(notifyDepSql))
                {
                    // 触发行级依赖，如果该表的指定范围内的行被修改，则会收到SqlServer的通知，并且清空相应缓存
                    SqlDependency sqlDep = DbHelperSQL.AddSqlDependency(notifyDepSql,
                        delegate(object sender, SqlNotificationEventArgs e)
                        {
                            //LogHelper.WriteLogByType(LogLevel.Info, "缓存清理对象：" + e.Source + "，通知类型：" + e.Type + "，清理原因：" + e.Info, null, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                            //清理缓存
                            ClearCache(tableName);
                            //清理通知依赖语句。
                            lstSqlDependencySql.Remove(notifyDepSql);
                        });
                    //将新的通知依赖加入列表中
                    lstSqlDependencySql.Add(notifyDepSql);
                }
                dt = DbHelperSQL.GetDataTable(selectSql, parameters);

                //设定最大缓存时间为20分钟.
                HttpRuntime.Cache.Insert(notifyDepSql, dt, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));
            }
            return dt;

        }


    }
}
