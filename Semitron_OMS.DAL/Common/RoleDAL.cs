using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using System.Web;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Common;

namespace Semitron_OMS.DAL.Common
{
    /// <summary>
    /// 数据访问类:RoleDAL
    /// </summary>
    public partial class RoleDAL
    {
        public RoleDAL()
        { }
        #region  Method
        OperationsLogDAL logDal = new OperationsLogDAL();
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RoleID", "Role");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Role");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = RoleID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsRoleName(string RoleName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Role");
            strSql.Append(" where RoleName=@RoleName and AvailFlag=1");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,32)};
            parameters[0].Value = RoleName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsRoleName(string RoleName, int RoleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Role");
            strSql.Append(" where RoleName=@RoleName and AvailFlag=1 and RoleId!=@RoleId");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,32),
                    new SqlParameter("@RoleId", SqlDbType.NVarChar,32)};
            parameters[0].Value = RoleName;
            parameters[1].Value = RoleId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(RoleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Role(");
            strSql.Append("RoleID,RoleName,Description,AvailFlag)");
            strSql.Append(" values (");
            strSql.Append("@RoleID,@RoleName,@Description,@AvailFlag)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,32),
					new SqlParameter("@Description", SqlDbType.NVarChar,64),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.AvailFlag;

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
        /// 增加一条数据
        /// </summary>
        public bool AddRole(RoleModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Role(");
            strSql.Append("RoleName,Description,AvailFlag)");
            strSql.Append(" values (");
            strSql.Append("@RoleName,@Description,@AvailFlag)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,32),
					new SqlParameter("@Description", SqlDbType.NVarChar,64),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.AvailFlag;
            int AddID = Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (AddID > 0)
            {
                //增加操作日志
                string logSql = logDal.Add("Role", "新增角色：" + strSql.ToString(), AddID.ToString(), (int)OperationsType.Add);
                DbHelperSQL.ExecuteSql(logSql);
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
        public bool Update(RoleModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Role set ");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("Description=@Description,");
            strSql.Append("AvailFlag=@AvailFlag");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,32),
					new SqlParameter("@Description", SqlDbType.NVarChar,64),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.AvailFlag;
            parameters[3].Value = model.RoleID;

            //增加操作日志
            string msg = "修改角色。";
            RoleModel oldModel = GetModel(model.RoleID);
            msg += CommonFunction.ContrastTowObj(oldModel, model);  //获取修改前和修改后的数据。
            strSql.Append(logDal.Add("Role", msg, model.RoleID.ToString(), (int)OperationsType.Edit));
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
        /// 逻辑删除
        /// </summary>
        public bool LogicDeleteRl(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Role set ");
            strSql.Append("AvailFlag=@AvailFlag");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = 0;
            parameters[1].Value = id;
            //增加操作日志
            string logSql = logDal.Add("Role", "删除ID：" + id, id.ToString(), (int)OperationsType.Edit);

            strSql.Append(logSql);
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
        public bool Delete(int RoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Role ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = RoleID;

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
        public bool DeleteList(string RoleIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Role ");
            strSql.Append(" where RoleID in (" + RoleIDlist + ")  ");

            //增加操作日志
            string logSql = logDal.Add("Role", "删除ID：" + RoleIDlist, "", (int)OperationsType.Edit);
            strSql.Append(logSql);

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
        public RoleModel GetModel(int RoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RoleID,RoleName,Description,AvailFlag from Role ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = RoleID;

            RoleModel model = new RoleModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RoleID"] != null && ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(ds.Tables[0].Rows[0]["RoleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleName"] != null && ds.Tables[0].Rows[0]["RoleName"].ToString() != "")
                {
                    model.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
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
            strSql.Append("select RoleID,RoleName,Description,AvailFlag ");
            strSql.Append(" FROM Role ");
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
            strSql.Append(" RoleID,RoleName,Description,AvailFlag ");
            strSql.Append(" FROM Role ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
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
            parameters[0].Value = "Role";
            parameters[1].Value = "RoleID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
        /// <summary>
        /// 绑定角色关系
        /// </summary>
        public bool BindAdminRole(string AdminID, string[] RoleIDList, string AdminName)
        {
            if (RoleIDList.Length > 0)
            {
                string Roleids = "";
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from UserRole where AdminID=" + AdminID + "; ");
                for (int i = 0; i < RoleIDList.Length; i++)
                {
                    if (RoleIDList[i] != "" && AdminName != "")
                    {
                        if (i == 0)
                        {
                            strSql.Append("insert into UserRole ");
                        }
                        strSql.Append(" select " + RoleIDList[i] + "," + AdminID + ",'" + AdminName + "'");
                        if (i != RoleIDList.Length - 1)
                        {
                            strSql.Append(" union all ");
                        }
                        Roleids += RoleIDList[i] + ",";
                    }
                }
                //增加操作日志
                string logSql = logDal.Add("UserRole", "给：" + AdminName + "关联角色：", AdminID.ToString(), (int)OperationsType.Edit);
                strSql.Append(logSql);
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
            return false;

        }
        /// <summary>
        /// 根据资费代码ID获取覆盖的通道
        /// </summary>
        public DataSet GetListByFeeCode(int FeeCodeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.RoleID, RoleName, [Description], AvailFlag,BID");
            strSql.Append(" from dbo.Role A  left join (select RoleID");
            strSql.Append(" as BID from UserRole  where AdminID=" + FeeCodeId + ")");
            strSql.Append(" B on A.RoleID=B.BID where AvailFlag=1");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得分页查询数据
        /// </summary>
        /// <param name="pageSearchInfo">分布查询信息</param>
        /// <param name="o_RowsCount">查询结果行数</param>
        /// <returns>数据集</returns>
        public DataSet GetRolePageData(PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            //查询表名称
            string strTableName = "Role";
            //查询列名
            string strGetFields = " RoleID,RoleName,Description,case AvailFlag when '1' then '有效' when '0' then '无效' end as AvailFlag";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false);

            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(strTableName,
                strGetFields,
                pageSearchInfo.PageSize,
                pageSearchInfo.PageIndex,
                strWhere,
                pageSearchInfo.OrderByField,
                pageSearchInfo.OrderType,
                out o_RowsCount);
        }
        #endregion  Method
    }
}

