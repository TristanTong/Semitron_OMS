/**  
* AdminBindSupplierBLL.cs
*
* 功 能： N/A
* 类 名： AdminBindSupplierBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/24 22:27:48   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.Model.OMS;
using System.Data.SqlClient;
namespace Semitron_OMS.BLL.OMS
{
    /// <summary>
    /// 采购关联负责供应商表
    /// </summary>
    public partial class AdminBindSupplierBLL
    {
        private readonly Semitron_OMS.DAL.OMS.AdminBindSupplierDAL dal = new Semitron_OMS.DAL.OMS.AdminBindSupplierDAL();
        public AdminBindSupplierBLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.AdminBindSupplierModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.AdminBindSupplierModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int BindID)
        {

            return dal.Delete(BindID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string BindIDlist)
        {
            return dal.DeleteList(BindIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Semitron_OMS.Model.OMS.AdminBindSupplierModel GetModel(int BindID)
        {

            return dal.GetModel(BindID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.AdminBindSupplierModel GetModelByCache(int BindID)
        {

            string CacheKey = "AdminBindSupplierModelModel-" + BindID;
            object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BindID);
                    if (objModel != null)
                    {
                        int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Semitron_OMS.Model.OMS.AdminBindSupplierModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.AdminBindSupplierModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.AdminBindSupplierModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.AdminBindSupplierModel> modelList = new List<Semitron_OMS.Model.OMS.AdminBindSupplierModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.AdminBindSupplierModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
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
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 分页获取信息
        /// </summary>
        /// <param name="searchInfo">SQL条件过滤器</param>
        /// <param name="iCustomerId">客户Id</param>
        /// <param name="o_RowsCount">查询总数</param>
        /// <returns></returns>
        public DataSet GetAdminBindSupplierPageData(PageSearchInfo searchInfo, int iSupplierId, out int o_RowsCount)
        {
            return dal.GetAdminBindSupplierPageData(searchInfo, iSupplierId, out o_RowsCount);
        }
        /// <summary>
        /// 采购关联供应商
        /// </summary>
        /// <param name="codeId">采购Id</param>
        /// <param name="IdList">选中的Id串</param>
        /// <param name="IdListAll">页面中所有的Id串</param>
        /// <returns></returns>
        public bool AdminBindSupplier(int iSupplierId, string IdList, string IdListAll)
        {
            return dal.AdminBindSupplier(iSupplierId, IdList, IdListAll);
        }

        /// <summary>
        /// 使用缓存读取绑定数据
        /// </summary>
        /// <param name="iAdminId">用户ID</param>
        /// <returns>绑定数据</returns>
        public DataTable GetDataTableByCache(int iAdminId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@AdminID", SqlDbType.Int)
                    };
            parameters[0].Value = iAdminId;
            string strSql = ConstantValue.SQLNotifierDepObj.AdminBindSupplierDepSql.Replace("@AdminID", "'" + iAdminId + "'");
            return Semitron_OMS.DAL.SQLNotifier.GetDataTable(ConstantValue.SQLNotifierDepObj.AdminBindSupplierDepSql, strSql, ConstantValue.TableNames.AdminBindSupplier, parameters);
        }
        #endregion  ExtensionMethod


    }
}

