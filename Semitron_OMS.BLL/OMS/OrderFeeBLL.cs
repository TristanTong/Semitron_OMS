/**  
* OrderFeeBLL.cs
*
* 功 能： N/A
* 类 名： OrderFeeBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:29   童荣辉    初版
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
    /// 订单计费明细表
    /// </summary>
    public partial class OrderFeeBLL
    {
        private readonly Semitron_OMS.DAL.OMS.OrderFeeDAL dal = new Semitron_OMS.DAL.OMS.OrderFeeDAL();
        public OrderFeeBLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.OrderFeeModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.OrderFeeModel model)
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
        public Semitron_OMS.Model.OMS.OrderFeeModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.OrderFeeModel GetModelByCache(int ID)
        {

            string CacheKey = "OrderFeeModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.OrderFeeModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.OrderFeeModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.OrderFeeModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.OrderFeeModel> modelList = new List<Semitron_OMS.Model.OMS.OrderFeeModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.OrderFeeModel model;
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
        /// 分页查询订单费用明细记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>采购订单记录数据</returns>
        public DataSet GetOrderFeePageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetOrderFeePageData(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 逻辑删除明细
        /// </summary>
        public string ValidateAndDelOrderFee(int iId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 编辑订单费用
        /// </summary>
        /// <param name="model">订单费用实体</param>
        /// <returns>更新结果</returns>
        public string ValidateAndUpdate(OrderFeeModel model)
        {
            if (!this.Update(model))
            {
                return "编辑订单费用失败！";
            }
            return "OK";
        }

        public string ValidateAndAdd(OrderFeeModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取显示在界面中的实体
        /// </summary>
        /// <param name="iId"></param>
        /// <returns></returns>
        public OrderFeeDisplayModel GetDisplayModel(int iId)
        {
            OrderFeeDisplayModel disModel = new OrderFeeDisplayModel();
            OrderFeeModel model = this.GetModel(iId);
            if (model == null || model.CustomerOrderDetailID <= 0 || model.POPlanID <= 0)
            {
                return null;
            }
            CustomerOrderDetailModel dModel = new CustomerOrderDetailBLL().GetModel(model.CustomerOrderDetailID);
            if (dModel == null)
            {
                return null;
            }
            CustomerOrderModel cModel = new CustomerOrderBLL().GetModelByInnerOrderNO(dModel.InnerOrderNO);
            if (cModel == null)
            {
                cModel = new CustomerOrderModel();
                cModel.CustomerID = -1;
            }

            POPlanModel pModel = new POPlanBLL().GetModel(model.POPlanID);
            if (pModel == null)
            {
                return null;
            }
            POModel oModel = new POBLL().GetModelByPONO(pModel.PONO);
            if (oModel == null)
            {
                return null;
            }
            disModel.BuyCost = DataUtility.ToDecimal(pModel.BuyCost);
            disModel.BuyExchangeRate = DataUtility.ToDecimal(pModel.BuyExchangeRate);
            disModel.BuyPrice = DataUtility.ToDecimal(pModel.BuyPrice);
            disModel.BuyRealCurrency = pModel.BuyRealCurrency;
            disModel.BuyRealPrice = DataUtility.ToDecimal(pModel.BuyRealPrice);
            disModel.BuyStandardCurrency = pModel.BuyStandardCurrency;
            disModel.CPN = dModel.CPN;
            disModel.CustomerFeeIn = DataUtility.ToDecimal(model.CustomerFeeIn);
            disModel.CustomerID = cModel.CustomerID;
            disModel.CustomerOrderDetailID = dModel.ID;
            disModel.CustomerRealPayFee = model.CustomerRealPayFee;
            disModel.GrossProfits = model.GrossProfits;
            disModel.ID = model.ID;
            disModel.IncomeRate = model.IncomeRate;
            disModel.IncomeStandardCurrency = model.IncomeStandardCurrency;
            disModel.InnerOrderNO = dModel.InnerOrderNO;
            disModel.MPN = pModel.MPN;
            disModel.NetProfit = model.NetProfit;
            disModel.OtherFee = model.OtherFee;
            disModel.OtherFee1 = DataUtility.ToDecimal(dModel.OtherFee);
            disModel.OtherFee2 = DataUtility.ToDecimal(pModel.OtherFee);
            disModel.OtherFeeRemark = model.OtherFeeRemark;
            disModel.PONO = pModel.PONO;
            disModel.POPlanID = pModel.ID;
            disModel.ProfitMargin = model.ProfitMargin;
            disModel.RealInCurrencyUnit = model.RealInCurrencyUnit;
            disModel.SaleExchangeRate = DataUtility.ToDecimal(dModel.SaleExchangeRate);
            disModel.SalePrice = DataUtility.ToDecimal(dModel.SalePrice);
            disModel.SaleRealCurrency = dModel.SaleRealCurrency;
            disModel.SaleRealPrice = DataUtility.ToDecimal(dModel.SaleRealPrice);
            disModel.SaleStandardCurrency = dModel.SaleStandardCurrency;
            disModel.StandardCustomerFeeIn = model.StandardCustomerFeeIn;
            disModel.StandardCustomerRealPayFee = model.StandardCustomerRealPayFee;
            disModel.StandardIncomeRealDiff = model.StandardIncomeRealDiff;
            disModel.StandardRealInCurrencyUnit = model.StandardRealInCurrencyUnit;
            disModel.SupplierID = DataUtility.ToInt(pModel.SupplierID);
            disModel.TotalFeeCurrencyUnit = model.TotalFeeCurrencyUnit;
            disModel.POQuantity = (int)pModel.POQuantity;
            disModel.CustQuantity = (int)dModel.CustQuantity;
            disModel.CustomerOrderNO = cModel.CustomerOrderNO;
            disModel.SaleTotalPrice = decimal.Round((decimal)(dModel.SalePrice * dModel.CustQuantity), 4);
            disModel.OtherRemark = model.OtherRemark;
            disModel.IsCustomerPay = dModel.IsCustomerPay;
            disModel.IsPaySupplier = pModel.IsPaySupplier;
            disModel.RealNetProfit = decimal.Round((decimal)(model.NetProfit + model.StandardIncomeRealDiff), 4);
            disModel.IsCustomerVATInvoice = model.IsCustomerVATInvoice;
            disModel.CustomerVATInvoiceNo = model.CustomerVATInvoiceNo;
            disModel.PaymentTypeID = (int)cModel.PaymentTypeID;
            disModel.VendorPaymentTypeID = (int)pModel.VendorPaymentTypeID;
            disModel.IsSupplierVATInvoice = model.IsSupplierVATInvoice;
            disModel.SupplierVATInvoice = model.SupplierVATInvoice;
            disModel.TraceNumber = model.TraceNumber;
            disModel.CustomerInStockDate = dModel.CustomerInStockDate;
            disModel.FeeBackDate = model.FeeBackDate;
            disModel.SalesManProportion = model.SalesManProportion;
            disModel.SalesManPay = model.SalesManPay;
            disModel.BuyerProportion = model.BuyerProportion;
            disModel.BuyerPay = model.BuyerPay;
            disModel.SupplierCorporationID = oModel.CorporationID;

            //CustomerInStockDate, FeeBackDate, SalesManProportion, SalesManPay, BuyerProportion, BuyerPay
            return disModel;
        }

        #endregion  ExtensionMethod


    }
}

