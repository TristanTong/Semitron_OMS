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
    /// 权限表
    /// </summary>
    public partial class Permission
    {
        private readonly PermissionDAL dal = new PermissionDAL();
        public Permission()
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
        public bool Exists(int PermissionID)
        {
            return dal.Exists(PermissionID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(PermissionModel model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddPermissionManage(PermissionModel model)
        {
            return dal.AddPermissionManage(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PermissionModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdatePermission(PermissionModel model)
        {
            return dal.UpdatePermission(model);
        }
        /// <summary>
        /// 逻辑删除一条数据
        /// </summary>
        public bool DeletePermission(int id)
        {
            return dal.DeletePermission(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PermissionID)
        {

            return dal.Delete(PermissionID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PermissionIDlist)
        {
            return dal.DeleteList(PermissionIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PermissionModel GetModel(int PermissionID)
        {

            return dal.GetModel(PermissionID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public PermissionModel GetModelByCache(int PermissionID)
        {

            string CacheKey = "PermissionModel-" + PermissionID;
            object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PermissionID);
                    if (objModel != null)
                    {
                        int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (PermissionModel)objModel;
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
        public List<PermissionModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PermissionModel> DataTableToList(DataTable dt)
        {
            List<PermissionModel> modelList = new List<PermissionModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PermissionModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new PermissionModel();
                    if (dt.Rows[n]["PermissionID"] != null && dt.Rows[n]["PermissionID"].ToString() != "")
                    {
                        model.PermissionID = int.Parse(dt.Rows[n]["PermissionID"].ToString());
                    }
                    if (dt.Rows[n]["Name"] != null && dt.Rows[n]["Name"].ToString() != "")
                    {
                        model.Name = dt.Rows[n]["Name"].ToString();
                    }
                    if (dt.Rows[n]["Code"] != null && dt.Rows[n]["Code"].ToString() != "")
                    {
                        model.Code = dt.Rows[n]["Code"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["Type"] != null && dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Rows[n]["LinkUrl"] != null && dt.Rows[n]["LinkUrl"].ToString() != "")
                    {
                        model.LinkUrl = dt.Rows[n]["LinkUrl"].ToString();
                    }
                    if (dt.Rows[n]["Pid"] != null && dt.Rows[n]["Pid"].ToString() != "")
                    {
                        model.Pid = int.Parse(dt.Rows[n]["Pid"].ToString());
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
            return GetList("");
        }
        /// <summary>
        /// 根据资费代码ID获取覆盖的通道
        /// </summary>
        public DataSet GetListByFeeCode(int FeeCodeId, int ObjType)
        {
            return dal.GetListByFeeCode(FeeCodeId, ObjType);
        }
        /// <summary>
        /// 查询出页面权限的ID
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public object CoutGetSingle(string Code)
        {
            return dal.CoutGetSingle(Code);
        }
        /// <summary>
        /// 查询出页面权限的名称
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public DataSet CoutGetCode(string pid, string AdminId)
        {
            return dal.CoutGetCode(pid, AdminId);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        /// <summary>
        /// 得到子系统权限父节点下的所有子权限id
        /// </summary>
        /// <param name="iParentId"></param>
        public DataTable GetSubPermissionIds(int iParentId)
        {
            DataSet ds = dal.GetSubPermissionIds(iParentId);
            DataTable dtPermission = new DataTable(); ;
            if (ds != null && ds.Tables.Count > 0)
            {
                dtPermission = ds.Tables[0];
            }
            return dtPermission;
        }
    }
}

