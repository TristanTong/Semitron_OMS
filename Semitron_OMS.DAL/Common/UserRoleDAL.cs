using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.DAL.Common
{
    /// <summary>
    /// 数据访问类:用户角色关联。
    /// </summary>
    public partial class UserRoleDAL
    {
        public UserRoleDAL()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserRoleID", "UserRole");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserRoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserRole");
            strSql.Append(" where UserRoleID=@UserRoleID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserRoleID", SqlDbType.Int,4)
};
            parameters[0].Value = UserRoleID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserRoleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserRole(");
            strSql.Append("RoleID,AdminID,AdminName)");
            strSql.Append(" values (");
            strSql.Append("@RoleID,@AdminID,@AdminName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,32)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.AdminID;
            parameters[2].Value = model.AdminName;

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
        public bool Update(UserRoleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserRole set ");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("AdminID=@AdminID,");
            strSql.Append("AdminName=@AdminName");
            strSql.Append(" where UserRoleID=@UserRoleID");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,32),
					new SqlParameter("@UserRoleID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.AdminID;
            parameters[2].Value = model.AdminName;
            parameters[3].Value = model.UserRoleID;

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
        public bool Delete(int UserRoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserRole ");
            strSql.Append(" where UserRoleID=@UserRoleID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserRoleID", SqlDbType.Int,4)
};
            parameters[0].Value = UserRoleID;

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
        public bool DeleteList(string UserRoleIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserRole ");
            strSql.Append(" where UserRoleID in (" + UserRoleIDlist + ")  ");
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
        public UserRoleModel GetModel(string AdminID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserRoleID,RoleID,AdminID,AdminName from UserRole ");
            strSql.Append(" where AdminID=@AdminID and  RoleId=1");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
};
            parameters[0].Value = AdminID;

            UserRoleModel model = new UserRoleModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserRoleID"] != null && ds.Tables[0].Rows[0]["UserRoleID"].ToString() != "")
                {
                    model.UserRoleID = int.Parse(ds.Tables[0].Rows[0]["UserRoleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleID"] != null && ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(ds.Tables[0].Rows[0]["RoleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserRoleModel GetModel(int UserRoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserRoleID,RoleID,AdminID,AdminName from UserRole ");
            strSql.Append(" where UserRoleID=@UserRoleID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserRoleID", SqlDbType.Int,4)
};
            parameters[0].Value = UserRoleID;

            UserRoleModel model = new UserRoleModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserRoleID"] != null && ds.Tables[0].Rows[0]["UserRoleID"].ToString() != "")
                {
                    model.UserRoleID = int.Parse(ds.Tables[0].Rows[0]["UserRoleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleID"] != null && ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(ds.Tables[0].Rows[0]["RoleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
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
            strSql.Append("select UserRoleID,RoleID,AdminID,AdminName ");
            strSql.Append(" FROM UserRole ");
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
            strSql.Append(" UserRoleID,RoleID,AdminID,AdminName ");
            strSql.Append(" FROM UserRole ");
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
            parameters[0].Value = "UserRole";
            parameters[1].Value = "UserRoleID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}

