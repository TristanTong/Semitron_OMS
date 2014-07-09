using System;
using System.Data;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.Model;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Model.Common;
namespace Semitron_OMS.BLL.Common
{
    /// <summary>
    /// 管理员角色表
    /// </summary>
    public partial class UserRole
    {
        private readonly UserRoleDAL dal = new UserRoleDAL();
        public UserRole()
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
        public bool Exists(int UserRoleID)
        {
            return dal.Exists(UserRoleID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserRoleModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserRoleModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UserRoleID)
        {

            return dal.Delete(UserRoleID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string UserRoleIDlist)
        {
            return dal.DeleteList(UserRoleIDlist);
        }

         /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserRoleModel GetModel(string AdminID)
        {
            return dal.GetModel(AdminID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserRoleModel GetModel(int UserRoleID)
        {

            return dal.GetModel(UserRoleID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public UserRoleModel GetModelByCache(int UserRoleID)
        {

            string CacheKey = "UserRoleModelModel-" + UserRoleID;
            object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(UserRoleID);
                    if (objModel != null)
                    {
                        int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (UserRoleModel)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        public List<UserRoleModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UserRoleModel> DataTableToList(DataTable dt)
        {
            List<UserRoleModel> modelList = new List<UserRoleModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                UserRoleModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UserRoleModel();
                    if (dt.Rows[n]["UserRoleID"] != null && dt.Rows[n]["UserRoleID"].ToString() != "")
                    {
                        model.UserRoleID = int.Parse(dt.Rows[n]["UserRoleID"].ToString());
                    }
                    if (dt.Rows[n]["RoleID"] != null && dt.Rows[n]["RoleID"].ToString() != "")
                    {
                        model.RoleID = int.Parse(dt.Rows[n]["RoleID"].ToString());
                    }
                    if (dt.Rows[n]["AdminID"] != null && dt.Rows[n]["AdminID"].ToString() != "")
                    {
                        model.AdminID = int.Parse(dt.Rows[n]["AdminID"].ToString());
                    }
                    if (dt.Rows[n]["AdminName"] != null && dt.Rows[n]["AdminName"].ToString() != "")
                    {
                        model.AdminName = dt.Rows[n]["AdminName"].ToString();
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
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}

