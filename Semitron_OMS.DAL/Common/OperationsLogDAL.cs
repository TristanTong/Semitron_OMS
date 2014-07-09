using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Common;
using System.Web;
using Semitron_OMS.Common;
namespace Semitron_OMS.DAL.Common
{
    /// <summary>
    /// 操作日志数据访问类
    /// </summary>
    public partial class OperationsLogDAL
    {
        public OperationsLogDAL()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("OperateID", ConstantValue.TableNames.OperationsLog);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int OperateID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OperationsLog");
            strSql.Append(" where OperateID=@OperateID");
            SqlParameter[] parameters = {
					new SqlParameter("@OperateID", SqlDbType.Int,4)
};
            parameters[0].Value = OperateID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取操作日志SQL
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string InitAddLog(OperationsLogModel model)
        {
            string sql = " insert into OperationsLog(";
            sql += "AdminID,OType,OMsg,CreateTime,AdminName,PKID,FormObject,AvailFlag,OInfo,NickName)";
            sql += "values (";
            sql += "" + model.AdminID + "," + model.OType + ",'" + model.OMsg + "','" + model.CreateTime + "','" + model.AdminName + "','" + model.PKID + "','" + model.FormObject + "'," + model.AvailFlag + ",'" + model.OInfo + "','" + model.NickName + "')";
            return sql;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OperationsLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OperationsLog(");
            strSql.Append("AdminID,OType,OMsg,CreateTime,AdminName,PKID,FormObject,AvailFlag,OInfo,NickName)");
            strSql.Append(" values (");
            strSql.Append("@AdminID,@OType,@OMsg,@CreateTime,@AdminName,@PKID,@FormObject,@AvailFlag,@OInfo,@NickName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@OType", SqlDbType.Int,4),
					new SqlParameter("@OMsg", SqlDbType.NVarChar,1024),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@AdminName", SqlDbType.VarChar,32),
					new SqlParameter("@PKID", SqlDbType.VarChar,32),
					new SqlParameter("@FormObject", SqlDbType.VarChar,64),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@OInfo", SqlDbType.VarChar,256),
					new SqlParameter("@NickName", SqlDbType.VarChar,32)};
            parameters[0].Value = model.AdminID;
            parameters[1].Value = model.OType;
            parameters[2].Value = model.OMsg;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.AdminName;
            parameters[5].Value = model.PKID;
            parameters[6].Value = model.FormObject;
            parameters[7].Value = model.AvailFlag;
            parameters[8].Value = model.OInfo;
            parameters[9].Value = model.NickName;

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
        /// 增加一条数据
        /// </summary>
        public int AddOperationsLog(OperationsLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OperationsLog(");
            strSql.Append("AdminID,OType,OMsg,CreateTime,AdminName,PKID,FormObject,AvailFlag,OInfo,NickName)");
            strSql.Append(" values (");
            strSql.Append("@AdminID,@OType,@OMsg,getdate(),@AdminName,@PKID,@FormObject,1,@OInfo,@NickName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@OType", SqlDbType.Int,4),
					new SqlParameter("@OMsg", SqlDbType.NVarChar,1024),
					new SqlParameter("@AdminName", SqlDbType.VarChar,32),
					new SqlParameter("@PKID", SqlDbType.VarChar,32),
					new SqlParameter("@FormObject", SqlDbType.VarChar,64),
					new SqlParameter("@OInfo", SqlDbType.VarChar,256),
					new SqlParameter("@NickName", SqlDbType.VarChar,32)};
            parameters[0].Value = model.OType;
            parameters[1].Value = model.OMsg;
            parameters[2].Value = model.AdminName;
            parameters[3].Value = model.PKID;
            parameters[4].Value = model.FormObject;
            parameters[5].Value = model.OInfo;
            parameters[6].Value = model.NickName;

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
        public bool Update(OperationsLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OperationsLog set ");
            strSql.Append("AdminID=@AdminID,");
            strSql.Append("OType=@OType,");
            strSql.Append("OMsg=@OMsg,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("AdminName=@AdminName,");
            strSql.Append("PKID=@PKID,");
            strSql.Append("FormObject=@FormObject,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("OInfo=@OInfo,");
            strSql.Append("NickName=@NickName");
            strSql.Append(" where OperateID=@OperateID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@OType", SqlDbType.Int,4),
					new SqlParameter("@OMsg", SqlDbType.NVarChar,1024),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@AdminName", SqlDbType.VarChar,32),
					new SqlParameter("@PKID", SqlDbType.VarChar,32),
					new SqlParameter("@FormObject", SqlDbType.VarChar,64),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
                    new SqlParameter("@OInfo", SqlDbType.VarChar,256),
					new SqlParameter("@NickName", SqlDbType.VarChar,32),
					new SqlParameter("@OperateID", SqlDbType.Int,4)};
            parameters[0].Value = model.AdminID;
            parameters[1].Value = model.OType;
            parameters[2].Value = model.OMsg;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.AdminName;
            parameters[5].Value = model.PKID;
            parameters[6].Value = model.FormObject;
            parameters[7].Value = model.AvailFlag;
            parameters[8].Value = model.OInfo;
            parameters[9].Value = model.NickName;
            parameters[10].Value = model.OperateID;

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateOperations(OperationsLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OperationsLog set ");
            strSql.Append("OType=@OType,");
            strSql.Append("OMsg=@OMsg,");
            strSql.Append("AdminName=@AdminName,");
            strSql.Append("PKID=@PKID,");
            strSql.Append("FormObject=@FormObject,");
            strSql.Append("OInfo=@OInfo,");
            strSql.Append("NickName=@NickName");
            strSql.Append(" where OperateID=@OperateID");
            SqlParameter[] parameters = {
					new SqlParameter("@OType", SqlDbType.Int,4),
					new SqlParameter("@OMsg", SqlDbType.NVarChar,1024),
					new SqlParameter("@AdminName", SqlDbType.VarChar,32),
					new SqlParameter("@PKID", SqlDbType.VarChar,32),
					new SqlParameter("@FormObject", SqlDbType.VarChar,64),
                    new SqlParameter("@OInfo", SqlDbType.VarChar,256),
					new SqlParameter("@NickName", SqlDbType.VarChar,32),
					new SqlParameter("@OperateID", SqlDbType.Int,4)};
            parameters[0].Value = model.OType;
            parameters[1].Value = model.OMsg;
            parameters[2].Value = model.AdminName;
            parameters[3].Value = model.PKID;
            parameters[4].Value = model.FormObject;
            parameters[5].Value = model.OInfo;
            parameters[6].Value = model.NickName;
            parameters[7].Value = model.OperateID;

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
        /// 逻辑删除一条语句
        /// </summary>
        public bool DeleteOperations(int model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OperationsLog set ");
            strSql.Append("AvailFlag=0,");
            strSql.Append(" where OperateID=@OperateID");
            SqlParameter[] parameters = {
					new SqlParameter("@OperateID", SqlDbType.Int,4)};
            parameters[0].Value = model;

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
        public bool Delete(int OperateID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OperationsLog ");
            strSql.Append(" where OperateID=@OperateID");
            SqlParameter[] parameters = {
					new SqlParameter("@OperateID", SqlDbType.Int,4)
};
            parameters[0].Value = OperateID;

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
        public bool DeleteList(string OperateIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OperationsLog ");
            strSql.Append(" where OperateID in (" + OperateIDlist + ")  ");
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
        public OperationsLogModel GetModel(int OperateID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OperateID,AdminID,OType,OMsg,CreateTime,AdminName,PKID,FormObject,AvailFlag,OInfo,NickName from OperationsLog ");
            strSql.Append(" where OperateID=@OperateID");
            SqlParameter[] parameters = {
					new SqlParameter("@OperateID", SqlDbType.Int,4)
};
            parameters[0].Value = OperateID;

            OperationsLogModel model = new OperationsLogModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OperateID"] != null && ds.Tables[0].Rows[0]["OperateID"].ToString() != "")
                {
                    model.OperateID = int.Parse(ds.Tables[0].Rows[0]["OperateID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OType"] != null && ds.Tables[0].Rows[0]["OType"].ToString() != "")
                {
                    model.OType = int.Parse(ds.Tables[0].Rows[0]["OType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OMsg"] != null && ds.Tables[0].Rows[0]["OMsg"].ToString() != "")
                {
                    model.OMsg = ds.Tables[0].Rows[0]["OMsg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PKID"] != null && ds.Tables[0].Rows[0]["PKID"].ToString() != "")
                {
                    model.PKID = ds.Tables[0].Rows[0]["PKID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FormObject"] != null && ds.Tables[0].Rows[0]["FormObject"].ToString() != "")
                {
                    model.FormObject = ds.Tables[0].Rows[0]["FormObject"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OInfo"] != null && ds.Tables[0].Rows[0]["OInfo"].ToString() != "")
                {
                    model.OInfo = ds.Tables[0].Rows[0]["OInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NickName"] != null && ds.Tables[0].Rows[0]["NickName"].ToString() != "")
                {
                    model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString();
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
            strSql.Append("select OperateID,AdminID,OType,OMsg,CreateTime,AdminName,PKID,FormObject,AvailFlag,OInfo,NickName ");
            strSql.Append(" FROM OperationsLog ");
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
            strSql.Append(" OperateID,AdminID,OType,OMsg,CreateTime,AdminName,PKID,FormObject,AvailFlag,OInfo,NickName ");
            strSql.Append(" FROM OperationsLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 增加操作日志。
        /// </summary>
        public string Add(string FormObject, string Msg, SqlParameter[] param, string PKID, int OperationsType)
        {
            //增加操作日志
            Semitron_OMS.Model.Common.AdminModel adminModel = new Semitron_OMS.Model.Common.AdminModel();
            adminModel.AdminID = -1;
            adminModel.Username = "";
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["Admin"] != null)
            {
                adminModel = (HttpContext.Current.Session["Admin"] as Semitron_OMS.Model.Common.AdminModel);
            }
            for (int i = 0; i < param.Length; i++)
            {
                if (i == param.Length - 1) //最后一个参数
                {
                    Msg = Msg.Replace(param[i].ParameterName, param[i].Value.ToString().Replace('\'', '，'));
                }
                else
                {
                    Msg = Msg.Replace(param[i].ParameterName + ",", param[i].Value.ToString().Replace('\'', '，') + ",");
                }
            }

            OperationsLogModel LogModel = new OperationsLogModel();
            LogModel.AdminID = adminModel.AdminID;
            LogModel.AdminName = adminModel.Username;
            LogModel.NickName = adminModel.Name;
            LogModel.FormObject = FormObject;
            LogModel.OMsg = Msg;
            LogModel.PKID = PKID;
            LogModel.OType = OperationsType;
            return InitAddLog(LogModel);
        }

        /// <summary>
        /// 增加操作日志。
        /// </summary>
        public string Add(string FormObject, string Msg, string OInfo, SqlParameter[] param, string PKID, int OperationsType)
        {
            //增加操作日志
            Semitron_OMS.Model.Common.AdminModel adminModel = new Semitron_OMS.Model.Common.AdminModel();
            adminModel.AdminID = -1;
            adminModel.Username = "";
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["Admin"] != null)
            {
                adminModel = (HttpContext.Current.Session["Admin"] as Semitron_OMS.Model.Common.AdminModel);
            }
            for (int i = 0; i < param.Length; i++)
            {
                if (i == param.Length - 1) //最后一个参数
                {
                    Msg = Msg.Replace(param[i].ParameterName, param[i].Value.ToString().Replace('\'', '，'));
                }
                else
                {
                    Msg = Msg.Replace(param[i].ParameterName + ",", param[i].Value.ToString().Replace('\'', '，') + ",");
                }
            }

            OperationsLogModel LogModel = new OperationsLogModel();
            LogModel.AdminID = adminModel.AdminID;
            LogModel.AdminName = adminModel.Username;
            LogModel.NickName = adminModel.Name;
            LogModel.FormObject = FormObject;
            LogModel.OMsg = Msg;
            LogModel.OInfo = OInfo;
            LogModel.PKID = PKID;
            LogModel.OType = OperationsType;
            return InitAddLog(LogModel);
        }

        /// <summary>
        /// 增加操作日志。
        /// </summary>
        public string Add(string FormObject, string Msg, string PKID, int OperationsType)
        {
            //增加操作日志
            Semitron_OMS.Model.Common.AdminModel adminModel = new Semitron_OMS.Model.Common.AdminModel();
            adminModel.AdminID = -1;
            adminModel.Username = "";
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["Admin"] != null)
                {
                    adminModel = (HttpContext.Current.Session["Admin"] as Semitron_OMS.Model.Common.AdminModel);
                }
            }
            catch
            {
                adminModel.AdminID = -1;
                adminModel.Username = "System";
                adminModel.Name = "System";
            }

            OperationsLogModel LogModel = new OperationsLogModel();
            LogModel.AdminID = adminModel.AdminID;
            LogModel.AdminName = adminModel.Username;
            LogModel.NickName = adminModel.Name;
            LogModel.FormObject = FormObject;
            LogModel.OMsg = Msg;
            LogModel.PKID = PKID;
            LogModel.OType = OperationsType;
            return InitAddLog(LogModel);
        }

        /// <summary>
        /// 增加操作日志。
        /// </summary>
        public string Add(string FormObject, string OInfo, string Msg, string PKID, int OperationsType)
        {
            //增加操作日志
            Semitron_OMS.Model.Common.AdminModel adminModel = new Semitron_OMS.Model.Common.AdminModel();
            adminModel.AdminID = -1;
            adminModel.Username = "";
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["Admin"] != null)
                {
                    adminModel = (HttpContext.Current.Session["Admin"] as Semitron_OMS.Model.Common.AdminModel);
                }
            }
            catch
            {
                adminModel.AdminID = -1;
                adminModel.Username = "System";
                adminModel.Name = "System";
            }

            OperationsLogModel LogModel = new OperationsLogModel();
            LogModel.AdminID = adminModel.AdminID;
            LogModel.AdminName = adminModel.Username;
            LogModel.NickName = adminModel.Name;
            LogModel.FormObject = FormObject;
            LogModel.OMsg = Msg;
            LogModel.OInfo = OInfo;
            LogModel.PKID = PKID;
            LogModel.OType = OperationsType;
            return InitAddLog(LogModel);
        }

        /// <summary>
        /// 增加操作日志。
        /// </summary>
        public string Add(string FormObject, string Msg, string OInfo, string PKID, int OperationsType, Semitron_OMS.Model.Common.AdminModel adminModel)
        {
            //增加操作日志
            //adminModel.AdminID = -1;
            //adminModel.Username = "";
            OperationsLogModel LogModel = new OperationsLogModel();
            LogModel.AdminID = adminModel.AdminID;
            LogModel.AdminName = adminModel.Username;
            LogModel.NickName = adminModel.Name;
            LogModel.FormObject = FormObject;
            LogModel.OMsg = Msg;
            LogModel.OInfo = OInfo;
            LogModel.PKID = PKID;
            LogModel.OType = OperationsType;
            return InitAddLog(LogModel);
        }

        /// <summary>
        /// 增加操作日志。
        /// </summary>
        public string Add(string FormObject, string Msg, string PKID, int OperationsType, Semitron_OMS.Model.Common.AdminModel adminModel)
        {
            //增加操作日志
            //adminModel.AdminID = -1;
            //adminModel.Username = "";
            OperationsLogModel LogModel = new OperationsLogModel();
            LogModel.AdminID = adminModel.AdminID;
            LogModel.AdminName = adminModel.Username;
            LogModel.NickName = adminModel.Name;
            LogModel.FormObject = FormObject;
            LogModel.OMsg = Msg;
            LogModel.PKID = PKID;
            LogModel.OType = OperationsType;
            return InitAddLog(LogModel);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="FormObject">模块</param>
        /// <param name="Msg">信息</param>
        /// <param name="PKID">主键</param>
        /// <param name="OperationsType">操作类型</param>
        /// <returns>是否成功，true：成功，false：失败</returns>
        public bool AddExecute(string FormObject, string Msg, string PKID, int OperationsType)
        {
            string logSql = Add(FormObject, Msg, PKID, OperationsType);

            int rows = DbHelperSQL.ExecuteSql(logSql.ToString());
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
        /// 增加一条数据
        /// </summary>
        /// <param name="FormObject">模块</param>
        /// <param name="Msg">信息</param>
        /// <param name="PKID">主键</param>
        /// <param name="OperationsType">操作类型</param>
        /// <returns>是否成功，true：成功，false：失败</returns>
        public bool AddExecute(string FormObject, string OInfo, string Msg, string PKID, int OperationsType)
        {
            string logSql = Add(FormObject, OInfo, Msg, PKID, OperationsType);

            int rows = DbHelperSQL.ExecuteSql(logSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion  Method

        #region Add Method

        /// <summary>
        /// 分页获取操作日志信息
        /// </summary>
        /// <param name="pageSearchInfo">分布查询信息</param>
        /// <param name="o_RowsCount">查询结果行数</param>
        /// <returns>数据集</returns>
        public DataSet GetOperationsLogPageData(Semitron_OMS.Common.PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            //查询表名称
            string strTableName = ConstantValue.TableNames.OperationsLog;
            //查询列名
            string strGetFields = " CONVERT(int,OperateID) OperateID,AdminID," +
            "CASE OType WHEN '1' THEN '增加' WHEN '2' THEN '修改' WHEN '3' THEN '删除' END AS OType,OInfo,OMsg," +
            "CONVERT(varchar,CreateTime,120) AS CreateTime,AdminName,NickName,PKID,FormObject ";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false);

            //数据查询
            Semitron_OMS.DAL.Common.CommonDAL commonDAL = new Semitron_OMS.DAL.Common.CommonDAL();
            return commonDAL.GetDataExt(ConstantValue.ProcedureNames.PageProcedureName, strTableName,
                strGetFields,
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

