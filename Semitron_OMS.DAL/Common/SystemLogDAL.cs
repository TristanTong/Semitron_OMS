using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Common;
namespace Semitron_OMS.DAL.Common
{
    /// <summary>
    /// 系统日志类
    /// </summary>
    public partial class SystemLogDAL
    {
        public SystemLogDAL()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LogID", "SystemLog");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int LogID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemLog");
            strSql.Append(" where LogID=@LogID");
            SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)
			};
            parameters[0].Value = LogID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemLog(");
            strSql.Append("LogLevel,Msg,LogThread,Exception,Logger,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@LogLevel,@Msg,@LogThread,@Exception,@Logger,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogLevel", SqlDbType.VarChar,16),
					new SqlParameter("@Msg", SqlDbType.NVarChar,1024),
					new SqlParameter("@LogThread", SqlDbType.VarChar,64),
					new SqlParameter("@Exception", SqlDbType.VarChar,1024),
					new SqlParameter("@Logger", SqlDbType.VarChar,32),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.LogLevel;
            parameters[1].Value = model.Msg;
            parameters[2].Value = model.LogThread;
            parameters[3].Value = model.Exception;
            parameters[4].Value = model.Logger;
            parameters[5].Value = model.CreateTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SystemLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemLog set ");
            strSql.Append("LogLevel=@LogLevel,");
            strSql.Append("Msg=@Msg,");
            strSql.Append("LogThread=@LogThread,");
            strSql.Append("Exception=@Exception,");
            strSql.Append("Logger=@Logger,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where LogID=@LogID");
            SqlParameter[] parameters = {
					new SqlParameter("@LogLevel", SqlDbType.VarChar,16),
					new SqlParameter("@Msg", SqlDbType.NVarChar,1024),
					new SqlParameter("@LogThread", SqlDbType.VarChar,64),
					new SqlParameter("@Exception", SqlDbType.VarChar,1024),
					new SqlParameter("@Logger", SqlDbType.VarChar,32),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LogID", SqlDbType.Int,4)};
            parameters[0].Value = model.LogLevel;
            parameters[1].Value = model.Msg;
            parameters[2].Value = model.LogThread;
            parameters[3].Value = model.Exception;
            parameters[4].Value = model.Logger;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.LogID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int LogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SystemLog ");
            strSql.Append(" where LogID=@LogID");
            SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)
			};
            parameters[0].Value = LogID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string LogIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SystemLog ");
            strSql.Append(" where LogID in (" + LogIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public SystemLogModel GetModel(int LogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LogID,LogLevel,Msg,LogThread,Exception,Logger,CreateTime from SystemLog ");
            strSql.Append(" where LogID=@LogID");
            SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)
			};
            parameters[0].Value = LogID;

            SystemLogModel model = new SystemLogModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LogID"] != null && ds.Tables[0].Rows[0]["LogID"].ToString() != "")
                {
                    model.LogID = int.Parse(ds.Tables[0].Rows[0]["LogID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LogLevel"] != null && ds.Tables[0].Rows[0]["LogLevel"].ToString() != "")
                {
                    model.LogLevel = ds.Tables[0].Rows[0]["LogLevel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Msg"] != null && ds.Tables[0].Rows[0]["Msg"].ToString() != "")
                {
                    model.Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LogThread"] != null && ds.Tables[0].Rows[0]["LogThread"].ToString() != "")
                {
                    model.LogThread = ds.Tables[0].Rows[0]["LogThread"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Exception"] != null && ds.Tables[0].Rows[0]["Exception"].ToString() != "")
                {
                    model.Exception = ds.Tables[0].Rows[0]["Exception"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Logger"] != null && ds.Tables[0].Rows[0]["Logger"].ToString() != "")
                {
                    model.Logger = ds.Tables[0].Rows[0]["Logger"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LogID,LogLevel,Msg,LogThread,Exception,Logger,CreateTime ");
            strSql.Append(" FROM SystemLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" LogID,LogLevel,Msg,LogThread,Exception,Logger,CreateTime ");
            strSql.Append(" FROM SystemLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SystemLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.LogID desc");
            }
            strSql.Append(")AS Row, T.*  from SystemLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "SystemLog";
            parameters[1].Value = "LogID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region Add Method

        /// <summary>
        /// 分页获取系统日志信息
        /// </summary>
        /// <param name="pageSearchInfo">SQL条件过滤器</param>
        /// <param name="o_RowsCount">查询总数</param>
        /// <returns></returns>
        public DataSet GetSystemLogPageData(PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            //查询表名称
            string strTableName = "SystemLog";
            //查询列名
            string strGetFields = "Convert(int,LogID) LogID," +
                                  "CONVERT(varchar,CreateTime,120) AS CreateTime," +
                                  "LogLevel,Msg,LogThread,Exception,Logger";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false);

            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(ConstantValue.ProcedureNames.PageProcedureName, strTableName, strGetFields,
                pageSearchInfo.PageSize,
                pageSearchInfo.PageIndex,
                strWhere,
                pageSearchInfo.OrderByField,
                pageSearchInfo.OrderType,
                out o_RowsCount);
        }

        #endregion

    }
}

