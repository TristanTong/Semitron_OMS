/**  
* ShippingPlanDetailBLL.cs
*
* 功 能： N/A
* 类 名： ShippingPlanDetailBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:27   童荣辉    初版
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
namespace Semitron_OMS.BLL.OMS
{
    /// <summary>
    /// ShippingPlanDetailBLL
    /// </summary>
    public partial class ShippingPlanDetailBLL
    {
        private readonly Semitron_OMS.DAL.OMS.ShippingPlanDetailDAL dal = new Semitron_OMS.DAL.OMS.ShippingPlanDetailDAL();
        public ShippingPlanDetailBLL()
        { }
        #region  BasicMethod

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
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.ShippingPlanDetailModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.ShippingPlanDetailModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Semitron_OMS.Model.OMS.ShippingPlanDetailModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.ShippingPlanDetailModel GetModelByCache(int ID)
        {

            string CacheKey = "ShippingPlanDetailModelModel-" + ID;
            object objModel = Semitron_OMS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Semitron_OMS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Semitron_OMS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Semitron_OMS.Model.OMS.ShippingPlanDetailModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.ShippingPlanDetailModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.ShippingPlanDetailModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.ShippingPlanDetailModel> modelList = new List<Semitron_OMS.Model.OMS.ShippingPlanDetailModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.ShippingPlanDetailModel model;
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

        public void AddList(List<ShippingPlanDetailModel> lstShippingPlanDetailModel)
        {
            dal.AddList(lstShippingPlanDetailModel);
        }

        /// <summary>
        /// 分页查询出货计划单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>出货计划单明细数据</returns>
        public DataSet GetShippingPlanDetailPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetShippingPlanDetailPageData(searchInfo, out o_RowsCount);
        }
        /// <summary>
        /// 根据出货计划单Id获得出货计划单明细列表记录
        /// </summary>
        /// <param name="iPlanId">出货计划单Id</param>
        /// <returns>出货计划单显示明细数据</returns>
        public List<ShippingPlanDetailDisplayModel> GetDisplayModelList(int iPlanId)
        {
            List<ShippingPlanDetailDisplayModel> listDisplayModel = new List<ShippingPlanDetailDisplayModel>();
            DataTable dt = dal.GetDisplayModelList(iPlanId);

            foreach (DataRow dr in dt.Rows)
            {
                ShippingPlanDetailDisplayModel model = new ShippingPlanDetailDisplayModel();
                model.ID = dr["ID"].ToInt(0);
                model.AvailFlag = dr["AvailFlag"].ToInt(0) == 1 ? true : false;
                model.ShippingPlanID = dr["ShippingPlanID"].ToInt(0);
                model.ShippingPlanNo = dr["ShippingPlanNo"].ToString();
                model.CustomerOrderDetailId = dr["CustomerOrderDetailId"].ToInt(0);
                model.InnerOrderNO = dr["InnerOrderNO"].ToString();
                model.CustomerOrderNO = dr["CustomerOrderNO"].ToString();
                model.CPN = dr["CPN"].ToString();
                model.CustQuantity = dr["CustQuantity"].ToInt(0);
                model.PlanedQty = dr["PlanedQty"].ToInt(0);
                model.WCode = dr["WCode"].ToString();
                model.WName = dr["WName"].ToString();
                model.ProductCode = dr["ProductCode"].ToString();
                model.MPN = dr["MPN"].ToString();
                model.PlanQty = dr["PlanQty"].ToInt(0);
                model.Remark = dr["Remark"].ToString();
                listDisplayModel.Add(model);
            }
            return listDisplayModel;
        }
        /// <summary>
        /// 验证并更新出货计划单明细实体
        /// </summary>
        /// <param name="model">出货计划单明细实体</param>
        /// <returns>操作结果提示</returns>
        public string ValidateAndUpdate(ShippingPlanDetailModel model)
        {
            string strResult = "OK";
            if (model.ShippingPlanID <= 0)
            {
                strResult += "出货计划单单号不能为空.";
            }
            if (string.IsNullOrEmpty(model.ProductCode))
            {
                strResult += "产品编号不能为空.";
            }
            if (model.PlanQty <= 0)
            {
                strResult += "出货计划数量需大于0.";
            }
            ShippingPlanModel pModel = new ShippingPlanBLL().GetModel((int)model.ShippingPlanID);
            if (pModel != null && pModel.IsApproved == true)
            {
                strResult += "出货计划已审核,无法完成编辑操作.";
            }

            if (strResult == "OK" && !this.Update(model))
            {
                strResult = "更新记录发生数据库异常，请联系管理员。";
            }
            return strResult;
        }

        /// <summary>
        /// 获得未进行出货的出货计划数据
        /// </summary>
        /// <param name="lstFilter"></param>
        /// <returns></returns>
        public List<ShippingPlanDetailDisplayModel> GetShippingPlanDetailUnOutStockList(List<SQLConditionFilter> lstFilter)
        {
            List<ShippingPlanDetailDisplayModel> listDisplayModel = new List<ShippingPlanDetailDisplayModel>();
            DataTable dt = dal.GetShippingPlanDetailUnOutStockList(lstFilter);
            foreach (DataRow dr in dt.Rows)
            {
                ShippingPlanDetailDisplayModel model = new ShippingPlanDetailDisplayModel();
                model.ID = dr["ID"].ToInt(0);
                model.ShippingPlanNo = dr["ShippingPlanNo"].ToString();
                model.CustomerName = dr["CustomerName"].ToString();
                model.InnerOrderNO = dr["InnerOrderNO"].ToString();
                model.CustomerOrderNO = dr["CustomerOrderNO"].ToString();
                model.CPN = dr["CPN"].ToString();
                model.FinishOutQty = dr["FinishOutQty"].ToInt(0);
                model.PlanStockCode = dr["PlanStockCode"].ToString();
                model.PlanStockName = dr["PlanStockName"].ToString();
                model.ProductCode = dr["ProductCode"].ToString();
                model.MPN = dr["MPN"].ToString();
                model.PlanQty = dr["PlanQty"].ToInt(0);
                model.SupplierCode = dr["SupplierCode"].ToString();
                model.SupplierName = dr["SupplierName"].ToString();
                model.OutStockQty = model.PlanQty - model.FinishOutQty;
                //如果为可出货的记录，则显示
                if (model.OutStockQty > 0)
                {
                    listDisplayModel.Add(model);
                }
            }
            return listDisplayModel;
        }
        #endregion  ExtensionMethod


    }
}

