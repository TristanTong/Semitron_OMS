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
    /// 数据访问类:权限访问。
    /// </summary>
    public partial class PermissionDAL
    {
        public PermissionDAL()
        { }
        #region  Method
        OperationsLogDAL logDal = new OperationsLogDAL();
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PermissionID", "Permission");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PermissionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Permission");
            strSql.Append(" where PermissionID=@PermissionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PermissionID", SqlDbType.Int,4)};
            parameters[0].Value = PermissionID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(PermissionModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Permission(");
            strSql.Append("PermissionID,Name,Code,Description,Type,LinkUrl,Pid,AvailFlag,ParentSystem)");
            strSql.Append(" values (");
            strSql.Append("@PermissionID,@Name,@Code,@Description,@Type,@LinkUrl,@Pid,@AvailFlag,@ParentSystem)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PermissionID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Code", SqlDbType.VarChar,64),
					new SqlParameter("@Description", SqlDbType.NVarChar,128),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LinkUrl", SqlDbType.VarChar,256),
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
                    new SqlParameter("@ParentSystem",SqlDbType.VarChar,4)   
                                        };
            parameters[0].Value = model.PermissionID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Code;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.LinkUrl;
            parameters[6].Value = model.Pid;
            parameters[7].Value = model.AvailFlag;
            parameters[8].Value = model.Pid;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                string logSql = logDal.Add("Permission", "新增权限：" + strSql.ToString(), Convert.ToInt32(obj).ToString(), (int)OperationsType.Add);
                DbHelperSQL.ExecuteSql(logSql);
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
        public bool AddPermissionManage(PermissionModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Permission(");
            strSql.Append("Name,Code,Description,Type,LinkUrl,Pid,AvailFlag,SK,ParentSystem)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Code,@Description,@Type,@LinkUrl,@Pid,@AvailFlag,@SK,@ParentSystem)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Code", SqlDbType.VarChar,64),
					new SqlParameter("@Description", SqlDbType.NVarChar,128),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LinkUrl", SqlDbType.VarChar,256),
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@SK", SqlDbType.VarChar,32),
                   new SqlParameter("@ParentSystem",SqlDbType.Int,4)  
                                        };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.LinkUrl;
            parameters[5].Value = model.Pid;
            parameters[6].Value = model.AvailFlag;
            parameters[7].Value = model.SK;
            parameters[8].Value = model.Pid;

            //增加操作日志
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                string logSql = logDal.Add("Permission", "新增权限：" + strSql.ToString(), Convert.ToInt32(obj).ToString(), (int)OperationsType.Add);
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
        public bool Update(PermissionModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Permission set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Code=@Code,");
            strSql.Append("Description=@Description,");
            strSql.Append("Type=@Type,");
            strSql.Append("LinkUrl=@LinkUrl,");
            strSql.Append("Pid=@Pid,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("ParentSystem=@ParentSystem");
            strSql.Append(" where PermissionID=@PermissionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Code", SqlDbType.VarChar,64),
					new SqlParameter("@Description", SqlDbType.NVarChar,128),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LinkUrl", SqlDbType.VarChar,256),
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@PermissionID", SqlDbType.Int,4),
                    new SqlParameter("@ParentSystem",SqlDbType.Int,4)                    };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.LinkUrl;
            parameters[5].Value = model.Pid;
            parameters[6].Value = model.AvailFlag;
            parameters[7].Value = model.PermissionID;
            parameters[8].Value = model.Pid;
            //增加操作日志
            string msg = "修改权限。";
            PermissionModel oldModel = GetModel(model.PermissionID);
            msg += CommonFunction.ContrastTowObj(oldModel, model);  //获取修改前和修改后的数据。
            strSql.Append(logDal.Add("Permission", msg, model.PermissionID.ToString(), (int)OperationsType.Edit));
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
        public bool UpdatePermission(PermissionModel model)
        {
            if (HttpContext.Current.Session["Admin"] != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Permission set ");
                strSql.Append("Name=@Name,");
                strSql.Append("Code=@Code,");
                strSql.Append("Description=@Description,");
                strSql.Append("Type=@Type,");
                strSql.Append("LinkUrl=@LinkUrl,");
                strSql.Append("SK=@SK,");
                strSql.Append("ParentSystem=@ParentSystem");
                strSql.Append(" where PermissionID=@PermissionID ");
                SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,16),
					new SqlParameter("@Code", SqlDbType.VarChar,64),
					new SqlParameter("@Description", SqlDbType.NVarChar,128),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LinkUrl", SqlDbType.VarChar,256),
                    new SqlParameter("@SK", SqlDbType.VarChar,32),
					new SqlParameter("@PermissionID", SqlDbType.Int,4),
                    new SqlParameter("@ParentSystem",SqlDbType.Int,4)                        };
                parameters[0].Value = model.Name;
                parameters[1].Value = model.Code;
                parameters[2].Value = model.Description;
                parameters[3].Value = model.Type;
                parameters[4].Value = model.LinkUrl;
                parameters[5].Value = model.SK;
                parameters[6].Value = model.PermissionID;
                parameters[7].Value = model.Pid;
                //增加操作日志
                string msg = "修改权限。";
                PermissionModel oldModel = GetModel(model.PermissionID);
                msg += CommonFunction.ContrastTowObj(oldModel, model);  //获取修改前和修改后的数据。
                strSql.Append(logDal.Add("Permission", msg, model.PermissionID.ToString(), (int)OperationsType.Edit));
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
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 逻辑删除一条数据
        /// </summary>
        public bool DeletePermission(int id)
        {
            if (HttpContext.Current.Session["Admin"] != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Permission set ");
                strSql.Append("AvailFlag=@AvailFlag");
                strSql.Append(" where PermissionID=@PermissionID ");
                SqlParameter[] parameters = {
					new SqlParameter("@AvailFlag", SqlDbType.NVarChar,16),
					new SqlParameter("@PermissionID", SqlDbType.Int,4)};
                parameters[0].Value = 0;
                parameters[1].Value = id;
                //增加操作日志
                string logSql = logDal.Add("Permission", "删除ID：" + id, id.ToString(), (int)OperationsType.Del);

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
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PermissionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Permission ");
            strSql.Append(" where PermissionID=@PermissionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PermissionID", SqlDbType.Int,4)};
            parameters[0].Value = PermissionID;
            //增加操作日志
            string logSql = logDal.Add("Permission", "删除ID：" + PermissionID, PermissionID.ToString(), (int)OperationsType.Del);

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
        public bool DeleteList(string PermissionIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Permission ");
            strSql.Append(" where PermissionID in (" + PermissionIDlist + ")  ");
            //增加操作日志
            string logSql = logDal.Add("Permission", "删除ID：" + PermissionIDlist, PermissionIDlist.ToString(), (int)OperationsType.Del);
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
        public PermissionModel GetModel(int PermissionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PermissionID,Name,Code,Description,Type,LinkUrl,Pid,AvailFlag,ParentSystem from Permission ");
            strSql.Append(" where PermissionID=@PermissionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PermissionID", SqlDbType.Int,4)};
            parameters[0].Value = PermissionID;

            PermissionModel model = new PermissionModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PermissionID"] != null && ds.Tables[0].Rows[0]["PermissionID"].ToString() != "")
                {
                    model.PermissionID = int.Parse(ds.Tables[0].Rows[0]["PermissionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Code"] != null && ds.Tables[0].Rows[0]["Code"].ToString() != "")
                {
                    model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LinkUrl"] != null && ds.Tables[0].Rows[0]["LinkUrl"].ToString() != "")
                {
                    model.LinkUrl = ds.Tables[0].Rows[0]["LinkUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Pid"] != null && ds.Tables[0].Rows[0]["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(ds.Tables[0].Rows[0]["Pid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentSystem"] != null && ds.Tables[0].Rows[0]["ParentSystem"].ToString() != "")
                {
                    model.ParentSystem = int.Parse(ds.Tables[0].Rows[0]["ParentSystem"].ToString());
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
            strSql.Append("select PermissionID,Name,Code,Description,Type,LinkUrl,Pid,AvailFlag,ParentSystem ");
            strSql.Append(" FROM Permission ");
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
            strSql.Append(" PermissionID,Name,Code,Description,Type,LinkUrl,Pid,AvailFlag,ParentSystem ");
            strSql.Append(" FROM Permission ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 绑定角色关系
        /// </summary>
        /// <param name="AdminId">代码ID</param>
        /// <param name="RoleId">地区ID集合</param>
        /// <param name="Key1">关键字1</param>
        /// <param name="Key2">关键字2</param>
        /// <param name="RContent">内容</param>
        /// <param name="SuccessRate">成功率</param>
        /// <returns></returns>
        public bool BindFeeCodeInfo(string AdminId, string[] RoleId, string[] RoleNameList)
        {

            if (RoleId.Length > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from UserRole where AdminID=" + AdminId + "; ");
                strSql.Append("insert into UserRole ");
                string RoleNames = "";
                for (int i = 0; i < RoleId.Length; i++)
                {
                    strSql.Append(" select " + RoleId[i] + "," + AdminId + ",'" + RoleNameList[i] + "'");
                    if (i != RoleId.Length - 1)
                    {
                        strSql.Append(" union all ");
                    }
                    RoleNames += RoleNameList[i] + ",";

                }
                //增加操作日志
                string logSql = logDal.Add("UserRole", "修改用户角色关联：" + RoleNames, AdminId, (int)OperationsType.Edit);
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
        /// 根据角色ID获取其权限
        /// </summary>
        public DataSet GetListByFeeCode(int FeeCodeId, int ObjType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.PermissionID,[Name],Code,[Description],[Type],");
            strSql.Append("LinkUrl,Pid,AvailFlag,BID,ParentSystem from Permission A");
            strSql.Append(" left join (select PermissionID as BID");
            strSql.Append(" from ObjectPermission where [ObjID]=" + FeeCodeId + " and [ObjType]=" + ObjType + ")");
            strSql.Append(" B on A.PermissionID=B.BID");
            strSql.Append(" where AvailFlag=1");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 查询出页面权限的ID
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public object CoutGetSingle(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PermissionID from Permission where Code='" + Code + "'");
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
        /// <summary>
        /// 查询出页面权限的名称
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public DataSet CoutGetCode(string pid, string AdminId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Permission.Code,Permission.Pid" +
" FROM ObjectPermission INNER JOIN" +
" Permission ON ObjectPermission.PermissionID = Permission.PermissionID left JOIN" +
" Role on ObjectPermission.ObjID=Role.RoleID" +
" where Permission.Pid='" + pid + "' and Role.RoleID=" + AdminId + "");
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
            parameters[0].Value = "Permission";
            parameters[1].Value = "PermissionID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        /// <summary>
        /// 得到子系统权限父节点下的所有子权限id
        /// </summary>
        /// <param name="iParentId"></param>
        /// <returns></returns>
        public DataSet GetSubPermissionIds(int iParentId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@PermissionID", SqlDbType.Int,4)};
            parameters[0].Value = iParentId;
            string strSql = " WITH  total ( PermissionID, Name, Pid ) AS ( SELECT PermissionID ,Name,Pid FROM Permission a WHERE  a.PermissionID = " + iParentId + " UNION ALL SELECT a.PermissionID , a.Name , a.Pid FROM  total , dbo.Permission a WHERE  a.Pid = total.PermissionID AND a.AvailFlag = 1 ) SELECT  PermissionID FROM  total";

            return DbHelperSQL.Query(strSql, parameters);
        }
    }
}

