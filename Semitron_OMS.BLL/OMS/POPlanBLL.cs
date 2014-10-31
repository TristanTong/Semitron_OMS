/**  
* POPlanBLL.cs
*
* 功 能： N/A
* 类 名： POPlanBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:42:38   童荣辉    初版
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
using Semitron_OMS.Common.Enum;
namespace Semitron_OMS.BLL.OMS
{
    /// <summary>
    /// 采购订单明细表(采购
    /// </summary>
    public partial class POPlanBLL
    {
        private readonly Semitron_OMS.DAL.OMS.POPlanDAL dal = new Semitron_OMS.DAL.OMS.POPlanDAL();
        public POPlanBLL()
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
        public int Add(Semitron_OMS.Model.OMS.POPlanModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.POPlanModel model)
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
        public Semitron_OMS.Model.OMS.POPlanModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.POPlanModel GetModelByCache(int ID)
        {

            string CacheKey = "POPlanModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.POPlanModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.POPlanModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.POPlanModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.POPlanModel> modelList = new List<Semitron_OMS.Model.OMS.POPlanModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.POPlanModel model;
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
        /// 分页查询采购订单数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>采购订单数据</returns>
        public DataSet GetPOPlanPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetPOPlanPageData(searchInfo, out o_RowsCount);
        }
        /// <summary>
        /// 验证并新增采购计划
        /// </summary>
        /// <param name="model">采购计划实体</param>
        /// <returns>操作结果</returns>
        public string ValidateAndAdd(POPlanModel model)
        {
            if (string.IsNullOrEmpty(model.MPN))
            {
                return "厂商型号不能为空！";
            }
            if (this.Add(model) <= 0)
            {
                return "新增采购计划失败！";
            }
            return "OK";
        }
        /// <summary>
        /// 验证并编辑采购计划
        /// </summary>
        /// <param name="model">采购计划实体</param>
        /// <returns>操作结果</returns>
        public string ValidateAndUpdate(POPlanModel model)
        {
            if (model.State == (int)EnumPOPlanState.FinishInStock
                || model.State == (int)EnumPOPlanState.Canceled)
            {
                return "此采购计划状态为进货完成或已被取消，不允许编辑数据。";
            }
            if (!this.Update(model))
            {
                return "编辑采购计划失败！";
            }
            if (!new POBLL().UpdateStateByPONO(model.PONO, model.UpdateUser, (int)EnumPOState.Added))
            {
                return "编辑采购计划成功，但级联修改采购订单状态失败，请手动编辑采购订单状态！";
            }
            return "OK";
        }
        /// <summary>
        /// 取消采购计划
        /// </summary>
        /// <param name="strUser">操作人员</param>
        /// <param name="strIdList">采购计划ID逗号字符串</param>
        /// <returns>操作结果</returns>
        public string ValidateAndCancelPOPlan(string strUser, string strIdList)
        {
            return dal.ValidateAndCancelPOPlan(strUser, strIdList);
        }
        /// <summary>
        /// 确认采购计划价格、数量
        /// </summary>
        /// <param name="strUser">操作人员</param>
        /// <param name="strIdList">采购计划ID逗号字符串</param>
        /// <returns>操作结果</returns>
        public string ValidateAndConfirmPrice(string strUser, string strIdList)
        {
            return dal.ValidateAndConfirmPrice(strUser, strIdList);
        }

        /// <summary>
        /// QC确认
        /// </summary>
        /// <param name="strUser">操作人员</param>
        /// <param name="strId">采购计划ID</param>
        /// <returns>操作结果</returns>
        public string ValidateAndQCConfirm(string strUser, string strId, string strArrivedQty, string strShipmentDate)
        {
            return dal.ValidateAndQCConfirm(strUser, strId, strArrivedQty, strShipmentDate);
        }

        /// <summary>
        /// 设置字段各项值
        /// </summary>
        /// <param name="iId">采购计划Id</param>
        /// <param name="bIsCustomerPay">是否已付款 1 已付款 0 未付款</param>
        /// <param name="iVendorPaymentTypeID">供应商付款方式</param>
        /// <returns>是否成功</returns>
        public bool SetPOPlanItems(int iId, bool bIsPaySupplier, int iVendorPaymentTypeID)
        {
            return dal.SetPOPlanItems(iId, bIsPaySupplier, iVendorPaymentTypeID) > 0 ? true : false;
        }

        /// <summary>
        /// 获取采购计划查找列表
        /// </summary>
        /// <returns></returns>
        public List<POPlanUnInStockModel> GetPOPlanLookupList(List<SQLConditionFilter> lstFilter, string strQueryType)
        {
            List<POPlanUnInStockModel> listModel = new List<POPlanUnInStockModel>();
            DataTable dt = dal.GetPOPlanLookupList(lstFilter, strQueryType);
            foreach (DataRow dr in dt.Rows)
            {
                POPlanUnInStockModel model = new POPlanUnInStockModel();
                model.POPlanId = dr["POPlanId"].ToInt();
                model.PONo = dr["PONo"].ToString();
                model.ProductCode = dr["ProductCode"].ToString();
                model.InnerBuyer = dr["InnerBuyer"].ToString();
                model.MPN = dr["MPN"].ToString();
                model.POQuantity = dr["POQuantity"].ToInt();
                model.BuyPrice = dr["BuyPrice"].ToDecimal();
                model.BuyCost = dr["BuyCost"].ToDecimal();
                model.StandardPrice = dr["BuyPrice"].ToDecimal();
                model.StandardTotalPrice = dr["BuyCost"].ToDecimal();
                model.ArrivedDate = dr["ArrivedDate"].ToString();
                model.CorporationID = dr["CorporationID"].ToInt(-1);
                model.CorporationName = dr["CorporationName"].ToString();
                model.SupplierID = dr["SupplierID"].ToInt(-1);
                model.SupplierCode = dr["SupplierCode"].ToString();
                model.SupplierName = dr["SupplierName"].ToString();
                model.ArrivedQty = dr["ArrivedQty"].ToInt();
                model.StockQty = dr["StockQty"].ToInt();
                model.UnInStockQty = model.ArrivedQty - model.StockQty;
                listModel.Add(model);
            }
            return listModel;
        }

        /// <summary>
        /// 自动生成采购计划
        /// </summary>
        /// <returns>数据库生成操作结果</returns>
        public string GeneratePOPlan(int iUserId)
        {
            return dal.GeneratePOPlan(iUserId);
        }
        #endregion  ExtensionMethod
    }
}

