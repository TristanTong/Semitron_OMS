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
    /// 请求记录（每天清空一
    /// </summary>
    public partial class Role
    {
        private readonly RoleDAL dal = new RoleDAL();
        public Role()
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
        public bool Exists(int RoleID)
        {
            return dal.Exists(RoleID);
        }
        /// <summary>
        /// 根据角色名来判断是否存在该记录
        /// </summary>
        public bool ExistsRoleName(string RoleName)
        {
            return dal.ExistsRoleName(RoleName);
        }
        /// <summary>
        /// 修改时，判断是否存在相同的角色名。
        /// </summary>
        public bool ExistsRoleName(string RoleName, int RoleId)
        {
            return dal.ExistsRoleName(RoleName, RoleId);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(RoleModel model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddRole(RoleModel model)
        {
            return dal.AddRole(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(RoleModel model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool UpdateRl(int model)
        {
            return dal.LogicDeleteRl(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RoleID)
        {

            return dal.Delete(RoleID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string RoleIDlist)
        {
            return dal.DeleteList(RoleIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoleModel GetModel(int RoleID)
        {

            return dal.GetModel(RoleID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public RoleModel GetModelByCache(int RoleID)
        {

            string CacheKey = "RoleModel-" + RoleID;
            object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RoleID);
                    if (objModel != null)
                    {
                        int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (RoleModel)objModel;
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
        public List<RoleModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RoleModel> DataTableToList(DataTable dt)
        {
            List<RoleModel> modelList = new List<RoleModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                RoleModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new RoleModel();
                    if (dt.Rows[n]["RoleID"] != null && dt.Rows[n]["RoleID"].ToString() != "")
                    {
                        model.RoleID = int.Parse(dt.Rows[n]["RoleID"].ToString());
                    }
                    if (dt.Rows[n]["RoleName"] != null && dt.Rows[n]["RoleName"].ToString() != "")
                    {
                        model.RoleName = dt.Rows[n]["RoleName"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["AvailFlag"] != null && dt.Rows[n]["AvailFlag"].ToString() != "")
                    {
                        model.AvailFlag = int.Parse(dt.Rows[n]["AvailFlag"].ToString());
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
            return GetList(" [AvailFlag]=1 ");
        }
        /// <summary>
        /// 绑定角色关系
        /// </summary>
        /// <param name="CodeId">代码ID</param>
        /// <param name="AreasId">地区ID集合</param>
        /// <param name="Key1">关键字1</param>
        /// <param name="Key2">关键字2</param>
        /// <param name="RContent">内容</param>
        /// <param name="SuccessRate">成功率</param>
        /// <returns></returns>
        public bool BindFeeCodeInfo(string CodeId, string[] AreasId, string AreaName)
        {
            return dal.BindAdminRole(CodeId, AreasId, AreaName);
        }
        /// <summary>
        /// 根据资费代码ID获取覆盖的通道
        /// </summary>
        public DataSet GetListByFeeCode(int FeeCodeId)
        {
            return dal.GetListByFeeCode(FeeCodeId);
        }
        /// <summary>
        /// 获得分页查询数据
        /// </summary>
        /// <param name="searchInfo">分布查询信息</param>
        /// <param name="o_RowsCount">查询结果行数</param>
        /// <returns>数据集</returns>
        public DataSet GetRolePageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetRolePageData(searchInfo, out o_RowsCount);
        }
        #endregion  Method

       
    }
}

