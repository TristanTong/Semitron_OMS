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
    /// 数据访问类:AdminDAL
    /// </summary>
    public partial class AdminDAL
    {
        public AdminDAL()
        { }
        #region  Method
        OperationsLogDAL logDal = new OperationsLogDAL();
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AdminID", "Admin");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AdminID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Admin");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
};
            parameters[0].Value = AdminID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByName(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Admin");
            strSql.Append(" where [Username]=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,40)};
            parameters[0].Value = UserName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AdminModel model, string RoleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Admin(");
            strSql.Append("Username,Password,Name,Phone,Email,AvailFlag,CustID,CreateTime,LastLoginTime,Type)");
            strSql.Append(" values (");
            strSql.Append("@Username,@Password,@Name,@Phone,@Email,@AvailFlag,@CustID,@CreateTime,@LastLoginTime,@Type)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.VarChar,32),
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Phone", SqlDbType.VarChar,32),
					new SqlParameter("@Email", SqlDbType.VarChar,32),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = model.Username;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.AvailFlag;
            parameters[6].Value = model.CustID;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.LastLoginTime;
            parameters[9].Value = model.Type;
            int AddID = Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (AddID > 0)
            {
                UserRoleModel userRoleModel = new UserRoleModel();
                userRoleModel.AdminID = AddID;
                userRoleModel.AdminName = model.Name;
                userRoleModel.RoleID = int.Parse(RoleId);
                new UserRoleDAL().Add(userRoleModel);
                //增加操作日志
                string logSql = logDal.Add("Admin", "新增管理员：" + strSql.ToString(), parameters, AddID.ToString(), (int)OperationsType.Add);
                DbHelperSQL.ExecuteSql(logSql);
                return AddID;
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AdminModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("Username=@Username,");
            strSql.Append("Password=@Password,");
            strSql.Append("Name=@Name,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Email=@Email,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CustID=@CustID,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("Type=@Type");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.VarChar,32),
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Phone", SqlDbType.VarChar,32),
					new SqlParameter("@Email", SqlDbType.VarChar,32),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = model.Username;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.AvailFlag;
            parameters[6].Value = model.CustID;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.LastLoginTime;
            parameters[9].Value = model.Type;
            parameters[10].Value = model.AdminID;
            //增加操作日志
            string msg = "修改管理员信息。";
            AdminModel oldModel = GetModel(model.AdminID);
            msg += CommonFunction.ContrastTowObj(oldModel, model);  //获取修改前和修改后的数据。
            strSql.Append(logDal.Add("Admin", msg, model.AdminID.ToString(), (int)OperationsType.Edit));
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
        public bool UpdateAdmin(AdminModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("Username=@Username,");
            strSql.Append("Password=@Password,");
            strSql.Append("Name=@Name,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Email=@Email,");
            strSql.Append("Type=@Type,");
            strSql.Append("CustID=@CustID");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.VarChar,32),
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Phone", SqlDbType.VarChar,11),
					new SqlParameter("@Email", SqlDbType.VarChar,32),
					new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = model.Username;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Email; ;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.CustID;
            parameters[7].Value = model.AdminID;
            //增加操作日志
            string msg = "修改管理员信息。";
            AdminModel oldModel = GetModel(model.AdminID);
            msg += CommonFunction.ContrastTowObj(oldModel, model);  //获取修改前和修改后的数据。
            strSql.Append(logDal.Add("Admin", msg, model.AdminID.ToString(), (int)OperationsType.Edit));
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
        /// 修改密码
        /// </summary>
        public bool UpdateNamePwd(string Pwd, int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("Password=@Password");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = Pwd;
            parameters[1].Value = id;
            //增加操作日志
            string msg = "修改密码。";
            AdminModel oldModel = GetModel(id);
            msg += "修改前：" + oldModel.Password + ",修改后：" + Pwd;
            strSql.Append(logDal.Add("Admin", msg, id.ToString(), (int)OperationsType.Edit));
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
        /// 修改密码（重构后）
        /// </summary>
        /// <param name="strPwd">密码</param>
        /// <param name="iID">用户ID</param>
        /// <returns>是否修改成功，true：成功，false：失败</returns>
        public bool UpdatePwdRS(string strPwd, int iID)
        {

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("UPDATE Admin SET ");
            sbSql.AppendLine("    Password=@Password");
            sbSql.AppendLine("    WHERE AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = strPwd;
            parameters[1].Value = iID;
            //增加操作日志
            string strMsg = "修改密码。";
            AdminModel oldModel = GetModel(iID);
            strMsg += "修改前：" + oldModel.Password + ",修改后：" + strPwd;
            sbSql.AppendLine(" " + logDal.Add("Admin", strMsg, iID.ToString(), (int)OperationsType.Edit));
            int rows = DbHelperSQL.ExecuteSql(sbSql.ToString(), parameters);
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
        /// 逻辑删除用户
        /// </summary>
        public bool LogicDeleteAdmin(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("AvailFlag=@AvailFlag");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
                    new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = 0;
            parameters[1].Value = id;

            //增加操作日志
            string logSql = logDal.Add("Admin", "删除管理员，ID：" + id, id.ToString(), (int)OperationsType.Del);

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
        /// 启用用户
        /// </summary>
        public bool EnabledAdmin(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("AvailFlag=@AvailFlag");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
                    new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = 1;
            parameters[1].Value = id;
            //增加操作日志
            string logSql = logDal.Add("Admin", "启用管理员，ID：" + id, id.ToString(), (int)OperationsType.Edit);
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
        public bool Delete(int AdminID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Admin ");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
};
            parameters[0].Value = AdminID;
            //增加操作日志
            string logSql = logDal.Add("Admin", "删除管理员，ID：" + AdminID, AdminID.ToString(), (int)OperationsType.Del);
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string AdminIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Admin ");
            strSql.Append(" where AdminID in (" + AdminIDlist + ")  ");
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
        /// <param name="_username">账户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public AdminModel GetModel(string _username, string pwd)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AdminID,Username,Password,Name,Phone,Email,AvailFlag,CustID,CreateTime,LastLoginTime,Type from Admin ");
            strSql.Append(" where Username=@_username and Password=@Password and AvailFlag=1");
            SqlParameter[] parameters = {
					new SqlParameter("@_username", _username),
                    new SqlParameter("@Password", pwd)
};
            AdminModel model = new AdminModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Username"] != null && ds.Tables[0].Rows[0]["Username"].ToString() != "")
                {
                    model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Password"] != null && ds.Tables[0].Rows[0]["Password"].ToString() != "")
                {
                    model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
                {
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CustID"] != null && ds.Tables[0].Rows[0]["CustID"].ToString() != "")
                {
                    model.CustID = int.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"] != null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
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
        public AdminModel GetModel(int AdminID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AdminID,Username,Password,Name,Phone,Email,AvailFlag,CustID,CreateTime,LastLoginTime,Type from Admin ");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
};
            parameters[0].Value = AdminID;

            AdminModel model = new AdminModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Username"] != null && ds.Tables[0].Rows[0]["Username"].ToString() != "")
                {
                    model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Password"] != null && ds.Tables[0].Rows[0]["Password"].ToString() != "")
                {
                    model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
                {
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CustID"] != null && ds.Tables[0].Rows[0]["CustID"].ToString() != "")
                {
                    model.CustID = int.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"] != null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
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
            strSql.Append("select AdminID,Username,Password,Name,Phone,Email,AvailFlag,CustID,CreateTime,LastLoginTime,Type ");
            strSql.Append(" FROM Admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得权限数据列表
        /// </summary>
        public DataSet GetExtent(string strWhere, string user, string pwd, string ip)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.AdminID,A.Username,A.Password,A.Name,A.Phone,A.Email,A.AvailFlag,");
            strSql.Append("A.CustID,A.CreateTime,A.LastLoginTime,A.Type UserType,P.Code,P.PermissionID,P.Type,P.Pid,P.Name as PName,P.LinkUrl,U.AdminName,R.RoleID,P.ParentSystem");
            strSql.Append(" from dbo.Admin A inner join UserRole U on A.AdminID=U.AdminID");
            strSql.Append(" inner join dbo.Role R on R.RoleID=U.RoleID left JOIN");
            strSql.Append(" ObjectPermission O on R.RoleID=O.ObjID or A.AdminID=O.ObjID inner join");
            strSql.Append(" Permission P on P.PermissionID=O.PermissionID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY P.SK;update Admin set LastLoginTime=getdate() where Username='" + user + "' and Password='" + pwd + "' and AvailFlag=1");
            ////增加操作日志
            // string msg = "登陆记录:" + "登陆名:" + user + "登陆IP:" + ip;
            //string logSql = logDal.Add("Admin", msg, "", (int)OperationsType.Add);
            //strSql.Append(logSql);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds;
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
            strSql.Append(" AdminID,Username,Password,Name,Phone,Email,AvailFlag,CustID,CreateTime,LastLoginTime,Type ");
            strSql.Append(" FROM Admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 退出日志
        /// </summary>
        /// <returns></returns>
        public int Quit()
        {
            if (HttpContext.Current.Session["Admin"] != null)
            {
                //增加操作日志
                string logSql = logDal.Add("Admin", "安全退出记录：", "", (int)OperationsType.Del);
                return DbHelperSQL.ExecuteSql(logSql);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获得分页查询数据
        /// </summary>
        /// <param name="pageSearchInfo">分布查询信息</param>
        /// <param name="o_RowsCount">查询结果行数</param>
        /// <returns>数据集</returns>
        public DataSet GetAdminPageData(PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            //查询表名称
            string strTableName = "Admin A";
            //查询列名
            string strGetFields = " AdminID , CASE AvailFlag WHEN '1' THEN '有效' WHEN '0' THEN '无效' END AS AvailFlag , Username , Password , Name , Phone , Email , custId ,'' CustName , CreateTime , LastLoginTime";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false);

            bool isRoleId = false;
            foreach (SQLConditionFilter sc in pageSearchInfo.ConditionFilter)
            {
                if (sc.FiledName == "RoleID")
                {
                    isRoleId = true;
                    break;
                }
            }
            if (isRoleId)
            {
                strTableName = "V_AdminRole";
                strGetFields = "AdminID , CASE AvailFlag WHEN '1' THEN '有效' WHEN '0' THEN '无效' END AS AvailFlag , Username , Password , Name , Phone , Email , custId ,'' CustName, CreateTime , LastLoginTime";
            }

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

        #region  ExtensionMethod
        /// <summary>
        /// 根据角色ID从缓存中取出对应的用户名
        /// </summary>
        /// <param name="strRoleIds">角色ID逗号字符串</param>
        /// <returns>数据集</returns>
        public DataTable GetByRoleIds(string strRoleIds)
        {
            string strSql = ConstantValue.SQLNotifierDepObj.AdminDepGetByRoleIds.Replace("@RoleIds", strRoleIds);

            return Semitron_OMS.DAL.SQLNotifier.GetDataTable(strSql, strSql, ConstantValue.TableNames.Admin + "_" + ConstantValue.TableNames.UserRole, null);
        }
        #endregion  ExtensionMethod
    }
}

