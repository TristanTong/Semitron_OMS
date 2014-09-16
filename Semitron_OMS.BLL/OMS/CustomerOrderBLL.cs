/**  
* CustomerOrderBLL.cs
*
* 功 能： N/A
* 类 名： CustomerOrderBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:30:59   童荣辉    初版
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
    /// 客户订单主表
    /// </summary>
    public partial class CustomerOrderBLL
    {
        private readonly Semitron_OMS.DAL.OMS.CustomerOrderDAL dal = new Semitron_OMS.DAL.OMS.CustomerOrderDAL();
        public CustomerOrderBLL()
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
        public bool Exists(string InnerOrderNO, int ID)
        {
            return dal.Exists(InnerOrderNO, ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.CustomerOrderModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.CustomerOrderModel model)
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
        public bool Delete(string InnerOrderNO, int ID)
        {

            return dal.Delete(InnerOrderNO, ID);
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
        public Semitron_OMS.Model.OMS.CustomerOrderModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.CustomerOrderModel GetModelByCache(int ID)
        {

            string CacheKey = "CustomerOrderModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.CustomerOrderModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.CustomerOrderModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.CustomerOrderModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.CustomerOrderModel> modelList = new List<Semitron_OMS.Model.OMS.CustomerOrderModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.CustomerOrderModel model;
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
        /// 分页查询客户订单数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>客户订单数据</returns>
        public DataSet GetCustomerOrderPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetCustomerOrderPageData(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 验证并新增客户订单
        /// </summary>
        public string ValidateAndAdd(CustomerOrderModel model)
        {
            //判断内部订单号是否存在
            if (this.Exists(model.InnerOrderNO))
            {
                return "此内部订单号的客户订单已新增，请勿重复新增！";
            }
            if (this.Add(model) <= 0)
            {
                return "新增客户订单失败！";
            }
            return "OK";
        }

        /// <summary>
        /// 根据内部订单号判断是否存在记录
        /// </summary>
        /// <param name="strInnerOrderNO">内部订单号</param>
        /// <returns>是否存在</returns>
        public bool Exists(string strInnerOrderNO)
        {
            return dal.Exists(strInnerOrderNO);
        }

        /// <summary>
        /// 验证并编辑客户订单
        /// </summary>
        public string ValidateAndUpdate(CustomerOrderModel model)
        {
            if (model.State == (int)EnumCustomerOrderState.FinishOutStock
                || model.State == (int)EnumCustomerOrderState.Canceled)
            {
                return "此客户订单的状态为已出库或取消状态，不能编辑数据！";
            }
            if (!this.Update(model))
            {
                return "编辑客户订单失败！";
            }
            return "OK";
        }

        /// <summary>
        /// 验证并取消客户订单
        /// </summary>
        public string ValidateAndCancelCustomerOrder(string strCreateUser, int iId)
        {
            return dal.ValidateAndCancelCustomerOrder(strCreateUser, iId);
        }

        /// <summary>
        /// 客户订单出货
        /// </summary>
        public string ValidateAndOutStock(string strCreateUser, int iId, string strShipmentDate)
        {
            return dal.ValidateAndOutStock(strCreateUser, iId, strShipmentDate);
        }

        /// <summary>
        /// 根据内部订单号获得客户订单实体
        /// </summary>
        /// <param name="strInnerOrderNO">内部订单号</param>
        /// <returns>客户订单实体</returns>
        public CustomerOrderModel GetModelByInnerOrderNO(string strInnerOrderNO)
        {
            return dal.GetModelByInnerOrderNO(strInnerOrderNO);
        }

        /// <summary>
        /// 获取产品销售分析列表数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <returns>产品销售分析列表数据</returns>
        public DataSet GetConfirmFirstData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetConfirmFirstData(searchInfo, out o_RowsCount);
        }
        /// <summary>
        /// 执行销售审核
        /// </summary>       
        public string ConfirmFirst(string strUserName, int iId, int iType)
        {
            return dal.ConfirmFirst(strUserName, iId, iType);
        }
        /// <summary>
        /// 根据客户产品清单明细ID设定字段各项值
        /// </summary>
        /// <param name="iCustomerOrderDetailID">客户产品清单明细ID</param>
        /// <param name="iPaymentTypeID">付款方式</param>
        public bool SetItemsByCustomerOrderDetailID(int iCustomerOrderDetailID, int iPaymentTypeID)
        {
            return dal.SetItemsByCustomerOrderDetailID(iCustomerOrderDetailID, iPaymentTypeID) > 0 ? true : false;
        }

        /// <summary>
        /// 根据内部订单号更新客户订单状态
        /// </summary>
        /// <param name="strInnerOrderNo">内部订单号</param>
        /// <param name="strUpdateUser">更新人</param>
        /// <param name="iState">更新状态</param>
        /// <returns>是否成功</returns>
        internal bool UpdateStateByInnerOrderNO(string strInnerOrderNo, string strUpdateUser, int iState)
        {
            return dal.UpdateStateByInnerOrderNO(strInnerOrderNo, strUpdateUser, iState);
        }
        #endregion  ExtensionMethod


    }
}

