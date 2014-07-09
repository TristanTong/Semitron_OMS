using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Common;

namespace Semitron_OMS.DAL.Common
{
    public class CommonDAL
    {

        /// <summary>
        /// 调用通用存储过程
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，1为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string tablename, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int intPageCount)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tablename", SqlDbType.VarChar, 255),
                    new SqlParameter("@strGetFields", SqlDbType.VarChar, 2048),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,8000),
                    new SqlParameter("@strOrder", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@intPageCount", SqlDbType.Int),
                    };
            parameters[0].Value = tablename;
            parameters[1].Value = strGetFields;
            parameters[2].Value = pageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = strWhere;
            parameters[5].Value = strOrder;
            parameters[6].Value = OrderType;
            parameters[7].Direction = ParameterDirection.Output;

            return DbHelperSQL.RunProcedure("Pro_GetData", parameters, "ds", out intPageCount, 7);
        }

        /// <summary>
        /// 调用通用存储过程
        /// </summary>
        /// <param name="strTablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="iPageSize">页大小</param>
        /// <param name="iPageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="iOrderType">排序类型，1为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集ds</returns>
        public DataSet GetDataExt(string strTablename,
            string strGetFields,
            int iPageSize,
            int iPageIndex,
            string strWhere,
            string strOrder,
            int iOrderType,
            out int o_RowsCount)
        {
            //SQL参数
            SqlParameter[] parameters = {
                    new SqlParameter("@tableName", SqlDbType.VarChar, 8000),
                    new SqlParameter("@strGetFields", SqlDbType.VarChar, 8000),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,8000),
                    new SqlParameter("@strOrder", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@iRowsCount", SqlDbType.Int),
                    };
            parameters[0].Value = strTablename;
            parameters[1].Value = strGetFields;
            parameters[2].Value = iPageSize;
            parameters[3].Value = iPageIndex;
            parameters[4].Value = strWhere;
            parameters[5].Value = strOrder;
            parameters[6].Value = iOrderType;
            parameters[7].Direction = ParameterDirection.Output;

            return DbHelperSQL.RunProcedure("Pro_GetDataExt", parameters, "ds", out o_RowsCount, 7);
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
        /// <param name="OrderType">排序类型，1为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataP(string tablename, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int intPageCount)
        {
            DbHelperSQLP DbHelper = new DbHelperSQLP(PubConstant.GetLbsGpsConnectionString());
            SqlParameter[] parameters = {
                    new SqlParameter("@tablename", SqlDbType.VarChar, 255),
                    new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,8000),
                    new SqlParameter("@strOrder", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@intPageCount", SqlDbType.Int)
                    };
            parameters[0].Value = tablename;
            parameters[1].Value = strGetFields;
            parameters[2].Value = pageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = strWhere;
            parameters[5].Value = strOrder;
            parameters[6].Value = OrderType;
            parameters[7].Direction = ParameterDirection.Output;
            return DbHelper.RunProcedure("up_GetData", parameters, "ds", out intPageCount, 7);
        }



        /// <summary>
        /// 返回查询的DataSet记录
        /// </summary>
        public DataSet Query(string strSql)
        {
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据表名获取索引值(获取多个)
        /// </summary>
        /// <param name="tblName"></param>
        /// <returns></returns>
        public int[] GetNumIndex(string tblName, int size)
        {
            SqlParameter[] parameters = { new SqlParameter("@tblName", SqlDbType.VarChar, 50),
        new SqlParameter("@size", SqlDbType.Int, 4)};
            parameters[0].Value = tblName;
            parameters[1].Value = size;
            int i = 0;
            int num = DbHelperSQL.RunProcedure("Pro_GetNumIndex", parameters, out i);
            if (i == 0)
            {
                return null;
            }
            int[] indexs = new int[num - size];
            i = 0;
            for (int x = num - size; x < num; x++)
            {
                indexs[i] = x;
                i++;
            }
            return indexs;
        }

        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        public bool ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            int rows = DbHelperSQL.ExecuteSqlTran(cmdList);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        //public bool ExecuteSqlTran(string connectionString, List<CommandInfo> cmdList)
        //{
        //    int rows = DbHelperSQL.ExecuteSqlTran(connectionString, cmdList);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 开启事务。
        /// </summary>
        public void BeginTransaction()
        {
            DbHelperSQL.BeginTransaction();
        }

        /// <summary>
        /// 提交事务。
        /// </summary>
        public void CommitTransaction()
        {
            DbHelperSQL.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务。
        /// </summary>
        public void RollbackTransaction()
        {
            DbHelperSQL.RollbackTransaction();
        }


        public DataSet ExecProcedure(string sql)
        {
            return DbHelperSQL.Query(sql);
        }

        public bool UpdateTable(DataTable dt)
        {

            SqlConnection conn = new SqlConnection(PubConstant.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from Tools.dbo.TemporaryBlackIMSI", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            sda.Fill(ds);
            DataTable oldDT = ds.Tables[0];
            oldDT = dt;
            return sda.Update(ds) > 0;
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
            if (dicSearch != null && dicGroup != null)
            {
                SqlParameter[] parameters = new SqlParameter[dicSearch.Count + dicGroup.Count + 6];
                SqlParameter spTemp;
                int count = 0;
                foreach (var item in dicSearch)
                {
                    spTemp = new SqlParameter("@ps_" + item.Key, SqlDbType.VarChar, 255);
                    spTemp.Value = item.Value;
                    parameters[count] = spTemp;
                    count++;
                }
                foreach (var item in dicGroup)
                {
                    spTemp = new SqlParameter("@pg_" + item.Key, SqlDbType.VarChar, 255);
                    spTemp.Value = item.Value;
                    parameters[count] = spTemp;
                    count++;
                }

                spTemp = new SqlParameter("@pageSize", SqlDbType.Int);
                spTemp.Value = iPageSize;
                parameters[dicSearch.Count + dicGroup.Count] = spTemp;
                spTemp = new SqlParameter("@pageIndex", SqlDbType.Int);
                spTemp.Value = iPageIndex;
                parameters[dicSearch.Count + dicGroup.Count + 1] = spTemp;
                spTemp = new SqlParameter("@strOrder", SqlDbType.VarChar, 255);
                spTemp.Value = strOrderField;
                parameters[dicSearch.Count + dicGroup.Count + 2] = spTemp;
                spTemp = new SqlParameter("@orderType", SqlDbType.Bit);
                spTemp.Value = iOrderType;
                parameters[dicSearch.Count + dicGroup.Count + 3] = spTemp;
                spTemp = new SqlParameter("@intRowsCount", SqlDbType.Int);
                spTemp.Direction = ParameterDirection.Output;
                parameters[dicSearch.Count + dicGroup.Count + 4] = spTemp;

                spTemp = new SqlParameter("@strvalue", SqlDbType.VarChar);
                spTemp.Value = strvalue;
                parameters[dicSearch.Count + dicGroup.Count + 5] = spTemp;
                return DbHelperSQL.RunProcedure(proName, parameters, "ds", out iRowsCount, dicSearch.Count + dicGroup.Count + 4);
            }
            else
            {
                iRowsCount = 0;
                return null;
            }
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
            if (dicSearch != null && dicGroup != null)
            {
                SqlParameter[] parameters = new SqlParameter[dicSearch.Count + dicGroup.Count + 5];
                SqlParameter spTemp;
                int count = 0;
                foreach (var item in dicSearch)
                {
                    spTemp = new SqlParameter("@ps_" + item.Key, SqlDbType.VarChar, 255);
                    spTemp.Value = item.Value;
                    parameters[count] = spTemp;
                    count++;
                }
                foreach (var item in dicGroup)
                {
                    spTemp = new SqlParameter("@pg_" + item.Key, SqlDbType.VarChar, 255);
                    spTemp.Value = item.Value;
                    parameters[count] = spTemp;
                    count++;
                }

                spTemp = new SqlParameter("@pageSize", SqlDbType.Int);
                spTemp.Value = iPageSize;
                parameters[dicSearch.Count + dicGroup.Count] = spTemp;
                spTemp = new SqlParameter("@pageIndex", SqlDbType.Int);
                spTemp.Value = iPageIndex;
                parameters[dicSearch.Count + dicGroup.Count + 1] = spTemp;
                spTemp = new SqlParameter("@strOrder", SqlDbType.VarChar, 255);
                spTemp.Value = strOrderField;
                parameters[dicSearch.Count + dicGroup.Count + 2] = spTemp;
                spTemp = new SqlParameter("@orderType", SqlDbType.Bit);
                spTemp.Value = iOrderType;
                parameters[dicSearch.Count + dicGroup.Count + 3] = spTemp;
                spTemp = new SqlParameter("@intRowsCount", SqlDbType.Int);
                spTemp.Direction = ParameterDirection.Output;
                parameters[dicSearch.Count + dicGroup.Count + 4] = spTemp;
                return DbHelperSQL.RunProcedure(proName, parameters, "ds", out iRowsCount, dicSearch.Count + dicGroup.Count + 4);
            }
            else
            {
                iRowsCount = 0;
                return null;
            }
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
            DbHelperSQLP DbHelper = new DbHelperSQLP(PubConstant.GetConnectionString(strConnectStringName));
            if (dicSearch != null && dicGroup != null)
            {
                SqlParameter[] parameters = new SqlParameter[dicSearch.Count + dicGroup.Count + 5];
                SqlParameter spTemp;
                int count = 0;
                foreach (var item in dicSearch)
                {
                    spTemp = new SqlParameter("@ps_" + item.Key, SqlDbType.VarChar, 255);
                    spTemp.Value = item.Value;
                    parameters[count] = spTemp;
                    count++;
                }
                foreach (var item in dicGroup)
                {
                    spTemp = new SqlParameter("@pg_" + item.Key, SqlDbType.VarChar, 255);
                    spTemp.Value = item.Value;
                    parameters[count] = spTemp;
                    count++;
                }

                spTemp = new SqlParameter("@pageSize", SqlDbType.Int);
                spTemp.Value = iPageSize;
                parameters[dicSearch.Count + dicGroup.Count] = spTemp;
                spTemp = new SqlParameter("@pageIndex", SqlDbType.Int);
                spTemp.Value = iPageIndex;
                parameters[dicSearch.Count + dicGroup.Count + 1] = spTemp;
                spTemp = new SqlParameter("@strOrder", SqlDbType.VarChar, 255);
                spTemp.Value = strOrderField;
                parameters[dicSearch.Count + dicGroup.Count + 2] = spTemp;
                spTemp = new SqlParameter("@orderType", SqlDbType.Bit);
                spTemp.Value = iOrderType;
                parameters[dicSearch.Count + dicGroup.Count + 3] = spTemp;
                spTemp = new SqlParameter("@intRowsCount", SqlDbType.Int);
                spTemp.Direction = ParameterDirection.Output;
                parameters[dicSearch.Count + dicGroup.Count + 4] = spTemp;
                return DbHelper.RunProcedure(proName, parameters, "ds", out iRowsCount, dicSearch.Count + dicGroup.Count + 4);
            }
            else
            {
                iRowsCount = 0;
                return null;
            }
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
        public DataSet GetDataExt(string proName, string tableName, string strGetFields, int pageSize, int PageIndex, string strWhere, string strOrder, int OrderType, out int iRowsCount)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tableName", SqlDbType.VarChar, 8000),
                    new SqlParameter("@strGetFields", SqlDbType.VarChar, 8000),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,8000),
                    new SqlParameter("@strOrder", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@iRowsCount", SqlDbType.Int),
                    };
            parameters[0].Value = tableName;
            parameters[1].Value = strGetFields;
            parameters[2].Value = pageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = strWhere;
            parameters[5].Value = strOrder;
            parameters[6].Value = OrderType;
            parameters[7].Direction = ParameterDirection.Output;
            return DbHelperSQL.RunProcedure(proName, parameters, "ds", out iRowsCount, 7);
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
        public DataSet GetDataExt(string strConnectStringName, string proName, string tableName, string strGetFields, int pageSize, int PageIndex, string strWhere, string strOrder, int OrderType, out int iRowsCount)
        {
            DbHelperSQLP DbHelper = new DbHelperSQLP(PubConstant.GetConnectionString(strConnectStringName));
            SqlParameter[] parameters = {
                    new SqlParameter("@tableName", SqlDbType.VarChar, 8000),
                    new SqlParameter("@strGetFields", SqlDbType.VarChar, 8000),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,8000),
                    new SqlParameter("@strOrder", SqlDbType.VarChar,100),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@iRowsCount", SqlDbType.Int),
                    };
            parameters[0].Value = tableName;
            parameters[1].Value = strGetFields;
            parameters[2].Value = pageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = strWhere;
            parameters[5].Value = strOrder;
            parameters[6].Value = OrderType;
            parameters[7].Direction = ParameterDirection.Output;
            return DbHelper.RunProcedure(proName, parameters, "ds", out iRowsCount, 7);
        }
    }
}
