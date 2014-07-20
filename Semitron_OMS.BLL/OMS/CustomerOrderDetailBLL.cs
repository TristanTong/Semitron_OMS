/**  
* CustomerOrderDetailBLL.cs
*
* 功 能： N/A
* 类 名： CustomerOrderDetailBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:28   童荣辉    初版
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
    /// 产品清单记录明细表
    /// </summary>
    public partial class CustomerOrderDetailBLL
    {
        private readonly Semitron_OMS.DAL.OMS.CustomerOrderDetailDAL dal = new Semitron_OMS.DAL.OMS.CustomerOrderDetailDAL();
        public CustomerOrderDetailBLL()
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
        public int Add(Semitron_OMS.Model.OMS.CustomerOrderDetailModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.CustomerOrderDetailModel model)
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
        public Semitron_OMS.Model.OMS.CustomerOrderDetailModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.CustomerOrderDetailModel GetModelByCache(int ID)
        {

            string CacheKey = "CustomerOrderDetailModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.CustomerOrderDetailModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.CustomerOrderDetailModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.CustomerOrderDetailModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.CustomerOrderDetailModel> modelList = new List<Semitron_OMS.Model.OMS.CustomerOrderDetailModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.CustomerOrderDetailModel model;
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
        /// 分页查询产品清单记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>产品清单记录数据</returns>
        public DataSet GetCustomerOrderDetailPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetCustomerOrderDetailPageData(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 根据内部订单号与客户型号判断是否存在记录
        /// </summary>
        /// <param name="strInnerOrderNO">内部订单号</param>
        /// <param name="strCPN">客户型号</param>
        /// <returns>是否存在</returns>
        private bool Exists(string strInnerOrderNO, string strCPN)
        {
            return dal.Exists(strInnerOrderNO, strCPN);
        }

        /// <summary>
        /// 验证并新增产品清单记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ValidateAndAdd(CustomerOrderDetailModel model)
        {
            //判断内部订单号与客户型号的组合是否存在
            if (this.Exists(model.InnerOrderNO, model.CPN))
            {
                return "该客户订单已新增此客户型号的产品清单记录，请勿重复新增！";
            }
            if (this.Add(model) <= 0)
            {
                return "新增产品清单记录失败！";
            }
            return "OK";
        }

        /// <summary>
        /// 验证并编辑产品清单记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ValidateAndUpdate(CustomerOrderDetailModel model)
        {
            if (!this.Update(model))
            {
                return "编辑产品清单记录失败！";
            }
            return "OK";
        }

        /// <summary>
        /// 逻辑删除明细
        /// </summary>
        public string ValidateAndDelCustomerOrderDetail(string strUser, int iId)
        {
            return dal.ValidateAndDelCustomerOrderDetail(strUser, iId);
        }

        /// <summary>
        /// 设置明细状态为无效
        /// </summary>
        /// <param name="iId">明细数据Id</param>
        /// <param name="iStatus">有效无效状态 1 有效 0 无效</param>
        /// <returns>是否成功</returns>
        private bool SetValid(int iId, int iStatus)
        {
            return dal.SetValid(iId, iStatus) > 0 ? true : false;
        }
        /// <summary>
        /// 设置客户是否已付款
        /// </summary>
        /// <param name="iId">明细数据Id</param>
        /// <param name="bIsCustomerPay">是否已付款 1 已付款 0 未付款</param>
        /// <param name="dtCustomerInStockDate">客户入库时间</param>
        /// <returns>是否成功</returns>
        public bool SetCustomerOrderDetailItems(int iId, bool bIsCustomerPay, DateTime? dtCustomerInStockDate)
        {
            return dal.SetCustomerOrderDetailItems(iId, bIsCustomerPay, dtCustomerInStockDate) > 0 ? true : false;
        }

        /// <summary>
        /// 获取待出货计划的产品清单列表
        /// </summary>
        /// <param name="lstFilter">过滤条件</param>
        public List<CustomerOrderDetailUnOutStockModel> GetCustomerOrderDetailUnOutStockList(List<SQLConditionFilter> lstFilter)
        {
            //CustomerOrderDetailId,InnerOrderNo,CustomerOrderNO,CPN,CustOrderDate,InnerSalesMan,AssignToInnerBuyer,UnOutStockQty,CustQuantity,DoingOutStockQty,PlaningQty,AlreadyQty,CustomerCode,CustomerName
            List<CustomerOrderDetailUnOutStockModel> listModel = new List<CustomerOrderDetailUnOutStockModel>();
            DataTable dt = dal.GetCustomerOrderDetailUnOutStockList(lstFilter);
            foreach (DataRow dr in dt.Rows)
            {
                CustomerOrderDetailUnOutStockModel model = new CustomerOrderDetailUnOutStockModel();
                model.CustomerOrderDetailId = dr["CustomerOrderDetailId"].ToInt(-1);
                model.InnerOrderNO = dr["InnerOrderNo"].ToString();
                model.CustomerOrderNO = dr["CustomerOrderNO"].ToString();
                model.CPN = dr["CPN"].ToString();
                model.CustOrderDate = dr["CustOrderDate"].ToString();
                model.InnerSalesMan = dr["InnerSalesMan"].ToString();
                model.AssignToInnerBuyer = dr["AssignToInnerBuyer"].ToString();

                model.CustQuantity = dr["CustQuantity"].ToInt(0);
                model.DoingOutStockQty = dr["DoingOutStockQty"].ToInt(0);
                model.PlaningQty = dr["PlaningQty"].ToInt(0);
                model.AlreadyQty = dr["AlreadyQty"].ToInt(0);
                model.CustomerCode = dr["CustomerCode"].ToString();
                model.CustomerName = dr["CustomerName"].ToString();

                model.UnOutStockQty = model.CustQuantity - model.DoingOutStockQty - model.PlaningQty - model.AlreadyQty;

                model.PlanedQty = model.DoingOutStockQty + model.PlaningQty + model.AlreadyQty;

                //如果为可出货的记录，则显示
                if (model.UnOutStockQty > 0)
                {
                    listModel.Add(model);
                }
            }
            return listModel;
        }
        #endregion  ExtensionMethod

    }
}

