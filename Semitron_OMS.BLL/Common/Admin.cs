using System;
using System.Data;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.Model;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Model.Permission;
namespace Semitron_OMS.BLL.Common
{
    /// <summary>
    /// 管理员
    /// </summary>
    public partial class Admin
    {
        private readonly AdminDAL dal = new AdminDAL();
        public Admin()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByName(string AdminName)
        {
            return dal.ExistsByName(AdminName);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AdminID)
        {
            return dal.Exists(AdminID);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AdminModel model, string slt)
        {
            return dal.Add(model, slt);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AdminModel model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAdmin(AdminModel model)
        {
            return dal.UpdateAdmin(model);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        public bool UpdateNamePwd(string name, int id)
        {
            return dal.UpdateNamePwd(name, id);
        }

        /// <summary>
        /// 修改密码（重构后）
        /// </summary>
        /// <param name="strPwd">密码</param>
        /// <param name="iID">用户ID</param>
        /// <returns>是否修改成功，true：成功，false：失败</returns>
        public bool UpdatePwdRS(string strPwd, int iID)
        {
            return dal.UpdatePwdRS(strPwd, iID);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAdmin(int model)
        {
            return dal.LogicDeleteAdmin(model);
        }
        /// <summary>
        /// 启用用户
        /// </summary>
        public bool EnabledAdmin(int id)
        {
            return dal.EnabledAdmin(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AdminID)
        {

            return dal.Delete(AdminID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AdminIDlist)
        {
            return dal.DeleteList(AdminIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AdminModel GetModel(int AdminID)
        {

            return dal.GetModel(AdminID);
        }
        /// <summary>
        /// 得到一个实体类
        /// </summary>
        /// <param name="_username">账户</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public AdminModel GetModel(string _username, string pwd)
        {
            return dal.GetModel(_username, pwd);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public AdminModel GetModelByCache(int AdminID)
        {

            string CacheKey = "AdminModel-" + AdminID;
            object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AdminID);
                    if (objModel != null)
                    {
                        int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (AdminModel)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得权限数据列表
        /// </summary>
        public DataSet GetExtent(string strWhere, string user, string pwd, string ip)
        {
            return dal.GetExtent(strWhere, user, pwd, ip);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AdminModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AdminModel> DataTableToList(DataTable dt)
        {
            List<AdminModel> modelList = new List<AdminModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AdminModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new AdminModel();
                    if (dt.Rows[n]["AdminID"] != null && dt.Rows[n]["AdminID"].ToString() != "")
                    {
                        model.AdminID = int.Parse(dt.Rows[n]["AdminID"].ToString());
                    }
                    if (dt.Rows[n]["Username"] != null && dt.Rows[n]["Username"].ToString() != "")
                    {
                        model.Username = dt.Rows[n]["Username"].ToString();
                    }
                    if (dt.Rows[n]["Password"] != null && dt.Rows[n]["Password"].ToString() != "")
                    {
                        model.Password = dt.Rows[n]["Password"].ToString();
                    }
                    if (dt.Rows[n]["Name"] != null && dt.Rows[n]["Name"].ToString() != "")
                    {
                        model.Name = dt.Rows[n]["Name"].ToString();
                    }
                    if (dt.Rows[n]["Phone"] != null && dt.Rows[n]["Phone"].ToString() != "")
                    {
                        model.Phone = dt.Rows[n]["Phone"].ToString();
                    }
                    if (dt.Rows[n]["Email"] != null && dt.Rows[n]["Email"].ToString() != "")
                    {
                        model.Email = dt.Rows[n]["Email"].ToString();
                    }
                    if (dt.Rows[n]["AvailFlag"] != null && dt.Rows[n]["AvailFlag"].ToString() != "")
                    {
                        model.AvailFlag = int.Parse(dt.Rows[n]["AvailFlag"].ToString());
                    }
                    if (dt.Rows[n]["CustID"] != null && dt.Rows[n]["CustID"].ToString() != "")
                    {
                        model.CustID = int.Parse(dt.Rows[n]["CustID"].ToString());
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["LastLoginTime"] != null && dt.Rows[n]["LastLoginTime"].ToString() != "")
                    {
                        model.LastLoginTime = DateTime.Parse(dt.Rows[n]["LastLoginTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 退出日志
        /// </summary>
        /// <returns></returns>
        public int Quit()
        {
            return dal.Quit();
        }
        /// <summary>
        /// 获得分页查询数据
        /// </summary>
        /// <param name="searchInfo">分布查询信息</param>
        /// <param name="o_RowsCount">查询结果行数</param>
        /// <returns>数据集</returns>
        public DataSet GetAdminPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetAdminPageData(searchInfo, out o_RowsCount);
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
            if (string.IsNullOrEmpty(strRoleIds))
            {
                return null;
            }
            return dal.GetByRoleIds(strRoleIds);
        }

        /// <summary>
        /// 登陆
        /// </summary>
        public AdminModel Login(string strParentSystem, string IP, string username, string pwd, out PageResult result)
        {
            AdminModel model = new AdminModel();

            result = new PageResult();

            bool isExist = this.ExistsByName(username);
            if (!isExist)
            {
                result.Info = "当前用户名不存在，请重新输入。";
            }

            if (isExist)
            {
                string condition = "A.AvailFlag=1 and R.AvailFlag=1 and P.AvailFlag=1 and O.AvailFlag=1";
                //得到子系统权限父节点下的所有子权限id
                if (string.IsNullOrEmpty(strParentSystem))
                {
                    strParentSystem = "-1";
                }

                Semitron_OMS.BLL.Common.Permission bllPermission = new BLL.Common.Permission();

                DataTable dtPermission = bllPermission.GetSubPermissionIds(strParentSystem);

                //得到所有拥有的权限id
                condition += " and A.Username='" + username + "' and A.Password='" + pwd + "'";
                DataTable dtTotal = this.GetExtent(condition, username, pwd, IP).Tables[0];

                //从所有权限id中取出该子系统下的权限
                DataTable dt = dtTotal.Clone();
                foreach (DataRow dr in dtTotal.Rows)
                {
                    foreach (DataRow drTemp in dtPermission.Rows)
                    {
                        if (dr["PermissionID"].ToString().Trim() == drTemp["PermissionID"].ToString().Trim())
                        {
                            dt.ImportRow(dr);
                            break;
                        }
                    }
                }
                if (dt.Rows.Count <= 0)
                {
                    result.Info = "您未曾分配登陆该系统的权限，请确认。";
                }
                if (dtTotal.Rows.Count <= 0)
                {
                    result.Info = "您输入的密码错误，请重新确认。";
                }

                if (dt.Rows.Count > 0)
                {
                    string PName = ",";
                    string PermissionID = ",";
                    string RoleID = ",";
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        model.AdminID = Convert.ToInt32(dr["AdminID"]);
                        model.AvailFlag = Convert.ToInt32(dr["AvailFlag"]);
                        model.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                        if (dr["UserType"] != null && dr["UserType"].ToString() != string.Empty)
                        {
                            model.Type = Convert.ToInt32(dr["UserType"].ToString());
                        }
                        if (dr["CustID"].ToString() == "")
                        {
                            model.CustID = null;
                        }
                        else
                        {
                            model.CustID = Convert.ToInt32(dr["CustID"]);
                        }
                        model.Email = dr["Email"].ToString();
                        if (dr["LastLoginTime"].ToString() == "")
                        {
                            model.LastLoginTime = null;
                        }
                        else
                        {
                            model.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
                        }
                        model.Name = dr["Name"].ToString();
                        model.Password = dr["Password"].ToString();
                        model.Phone = dr["Phone"].ToString();
                        model.Username = dr["Username"].ToString();
                    }
                    foreach (DataRow drEach in dt.Rows)
                    {
                        if (RoleID.IndexOf("," + drEach["RoleID"].ToString() + ",") == -1)
                        {
                            RoleID += drEach["RoleID"].ToString() + ",";
                        }
                        if (PermissionID.IndexOf("," + drEach["PermissionID"].ToString() + ",") == -1)
                        {
                            PermissionID += drEach["PermissionID"].ToString() + ",";
                        }

                        if (PName.IndexOf("," + drEach["PName"].ToString() + ",") == -1)
                        {
                            PName += drEach["PName"].ToString() + ",";
                        }
                    }
                    model.RoleID = RoleID.Split(',');
                    model.PermissionID = PermissionID.Split(',');
                    model.Pname = PName.Split(',');

                    PermissionModule perModule = new PermissionModule();
                    perModule.ID = -1;
                    perModule.Code = "-1";
                    perModule.Name = "-1";

                    List<BasePer> bpList = new List<BasePer>();
                    foreach (string strParentId in strParentSystem.Split(','))
                    {
                        BasePer bp = new BasePer();
                        bp.ID = int.Parse(strParentId);
                        bp.Name = "Semitron_Sys_" + strParentId;
                        bp.Code = "Semitron_Sys_" + strParentId;

                        bpList.Add(bp);
                    }
                    PermissionUtility.GetPermissionModuleWithMultiSubSys(bpList, dt, perModule);
                    model.PerModule = perModule;

                    result.State = 1;
                    result.Info = "登陆成功!";

                    foreach (BasePer bp in bpList)
                    {
                        if (PermissionUtility.IsExistPageCode(perModule.SubSystemPers[bp.ID], "SYS/AdminManage.aspx"))
                        {
                            result.Remark = PermissionUtility.ToCodeCommaString(PermissionUtility.GetButtonPer(perModule.SubSystemPers[bp.ID], "SYS/AdminManage.aspx"));
                        }
                    }
                }
            }

            string msg = "重登陆记录:" + "登陆名:" + model.Username + "登陆IP:" + IP;
            OperationsLogBLL logBll = new OperationsLogBLL();
            logBll.AddExecute("Admin", msg, "", (int)OperationsType.Add);
            return model;
        }

        /// <summary>
        /// 根据数据库缓存依赖获得数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableByCache()
        {
            return Semitron_OMS.DAL.SQLNotifier.GetDataTable(ConstantValue.SQLNotifierDepObj.AdminDepSql, ConstantValue.SQLNotifierDepObj.AdminDepSql, ConstantValue.TableNames.Admin, null);
        }
        #endregion  ExtensionMethod


    }
}

