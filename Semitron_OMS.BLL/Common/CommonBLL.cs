using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Common;

namespace Semitron_OMS.BLL.Common
{
    public class CommonBLL
    {
        CommonDAL dal = new CommonDAL();
        /// <summary>
        /// 调用通用存储过程
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string tablename, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int intPageCount)
        {
            if (string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(strGetFields) || string.IsNullOrEmpty(strOrder))
            {
                intPageCount = 0;
                return null;
            }

            return dal.GetData(tablename, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  intPageCount);
        }

        /// <summary>
        /// 调用通用存储过程
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataP(string tablename, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int intPageCount)
        {
            if (string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(strGetFields) || string.IsNullOrEmpty(strOrder))
            {
                intPageCount = 0;
                return null;
            }

            return dal.GetDataP(tablename, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  intPageCount);
        }

        /// <summary>
        /// 开启事务。
        /// </summary>
        public void BeginTransaction()
        {
            dal.BeginTransaction();
        }

        /// <summary>
        /// 提交事务。
        /// </summary>
        public void CommitTransaction()
        {
            dal.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务。
        /// </summary>
        public void RollbackTransaction()
        {
            dal.RollbackTransaction();
        }


        public DataSet Query(string strSql)
        {
            return dal.Query(strSql);
        }
        /// <summary>
        /// 根据表名获取索引
        /// </summary>
        /// <param name="tblName"></param>
        /// <returns></returns>
        public int[] GetNumIndex(string tblName, int size)
        {
            return dal.GetNumIndex(tblName, size);
        }


        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        public bool ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            return dal.ExecuteSqlTran(cmdList);
        }

        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        //public bool ExecuteSqlTran(string connectionstring, List<CommandInfo> cmdList)
        //{
        //    return dal.ExecuteSqlTran(connectionstring, cmdList);
        //}

        public bool UpdateTable(DataTable dt)
        {
            return dal.UpdateTable(dt);
        }    

        public DataSet ExecProcedure(string sql)
        {
            return dal.ExecProcedure(sql);
        }

        /// <summary>
        /// 执行支持分页的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="iRowsCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataExt(string proName, string tableName, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }

            return dal.GetDataExt(proName, tableName, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的存储过程
        /// </summary>
        /// <param name="strConnectStringName">数据库连接字符串名称</param>
        /// <param name="proName">存储过程名</param>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="iRowsCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataExt(string strConnectStringName, string proName, string tableName, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }

            return dal.GetDataExt(strConnectStringName, proName, tableName, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的参数可动态组建的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="dicSearch">查询条件键值对字典</param>
        /// <param name="dicGroup">组条件键值对字典</param>
        /// <param name="iPageSize">每页条数</param>
        /// <param name="iPageIndex">页索引</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="iOrderType">排序类型 0:升序 1:降序</param>
        /// <param name="iRowsCount">数据总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string proName, Dictionary<string, string> dicSearch, Dictionary<string, string> dicGroup, int iPageSize, int iPageIndex, string strOrderField, int iOrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }
            return dal.GetData(proName, dicSearch, dicGroup, iPageSize, iPageIndex, strOrderField, iOrderType, out iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的参数可动态组建的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="dicSearch">查询条件键值对字典</param>
        /// <param name="dicGroup">组条件键值对字典</param>
        /// <param name="iPageSize">每页条数</param>
        /// <param name="iPageIndex">页索引</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="iOrderType">排序类型 0:升序 1:降序</param>
        /// <param name="iRowsCount">数据总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string proName, string strvalue, Dictionary<string, string> dicSearch, Dictionary<string, string> dicGroup, int iPageSize, int iPageIndex, string strOrderField, int iOrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }
            return dal.GetData(proName, strvalue, dicSearch, dicGroup, iPageSize, iPageIndex, strOrderField, iOrderType, out iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的参数可动态组建的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="dicSearch">查询条件键值对字典</param>
        /// <param name="dicGroup">组条件键值对字典</param>
        /// <param name="iPageSize">每页条数</param>
        /// <param name="iPageIndex">页索引</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="iOrderType">排序类型 0:升序 1:降序</param>
        /// <param name="iRowsCount">数据总数</param>
        /// <param name="strConnectStringName">数据库查询字符串</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string proName, Dictionary<string, string> dicSearch, Dictionary<string, string> dicGroup, int iPageSize, int iPageIndex, string strOrderField, int iOrderType, out int iRowsCount, string strConnectStringName)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }
            return dal.GetData(proName, dicSearch, dicGroup, iPageSize, iPageIndex, strOrderField, iOrderType, out iRowsCount, strConnectStringName);
        }
    }
}
