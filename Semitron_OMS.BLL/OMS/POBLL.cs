/**  
* POBLL.cs
*
* 功 能： N/A
* 类 名： POBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:30:29   童荣辉    初版
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
    /// 采购订单主表
    /// </summary>
    public partial class POBLL
    {
        private readonly Semitron_OMS.DAL.OMS.PODAL dal = new Semitron_OMS.DAL.OMS.PODAL();
        public POBLL()
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
        public bool Exists(string PONO, int ID)
        {
            return dal.Exists(PONO, ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.POModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.POModel model)
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
        public bool Delete(string PONO, int ID)
        {

            return dal.Delete(PONO, ID);
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
        public Semitron_OMS.Model.OMS.POModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.POModel GetModelByCache(int ID)
        {

            string CacheKey = "POModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.POModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.POModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.POModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.POModel> modelList = new List<Semitron_OMS.Model.OMS.POModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.POModel model;
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
        public DataSet GetPOPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetPOPageData(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 验证并新增采购订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ValidateAndAdd(POModel model)
        {
            //判断采购订单号是否存在
            if (this.GetModelByPONO(model.PONO) != null)
            {
                return "相同采购订单号的记录已存在，请勿重复新增！";
            }
            if (this.Add(model) <= 0)
            {
                return "新增采购订单失败！";
            }
            return "OK";
        }

        /// <summary>
        /// 验证并编辑采购订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ValidateAndUpdate(POModel model)
        {
            if (model.State == (int)EnumPOState.Canceled
                || model.State == (int)EnumPOState.Completed)
            {
                return "此采购订单的状态为完成采购或已取消，不允许编辑数据！";
            }
            if (!this.Update(model))
            {
                return "编辑采购订单失败！";
            }
            return "OK";
        }

        /// <summary>
        /// 逻辑删除采购订单
        /// </summary>
        public string ValidateAndDelPO(string strUser, int iId)
        {
            //对采购订单作逻辑取消
            return dal.ValidateAndDelPO(strUser, iId);
        }

        /// <summary>
        /// 生成采购计划
        /// </summary>
        /// <param name="iPOId">采购订单Id</param>
        /// <param name="strCreateUser">操作用户</param>
        /// <returns>>0：成功</returns>
        public string GeneratePOPlan(int iPOId, string strCOIds, string strCreateUser)
        {
            //判断采购订单状态是否为关联待计划或采购计划中
            POModel model = this.GetModel(iPOId);
            if (model == null || !(model.State == (int)EnumPOState.Planning))
            {
                return "ERROR:此采购订单的状态不为关联待计划及采购计划中，无法进行生成采购计划操作！";
            }
            return dal.GeneratePOPlan(iPOId, strCOIds, strCreateUser);
        }

        /// <summary>
        /// 根据PONO获取实体
        /// </summary>
        public POModel GetModelByPONO(string strPONO)
        {
            return dal.GetModelByPONO(strPONO);
        }

        /// <summary>
        /// 获取采购订单关联的客户清单明细
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <returns>客户清单明细数据</returns>
        public DataSet GetPOBindCustomerOrderDetail(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetPOBindCustomerOrderDetail(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 获取产品采购分析列表数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <returns>产品采购分析列表数据</returns>
        public DataSet GetConfirmSecondData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetConfirmSecondData(searchInfo, out o_RowsCount);
        }
        /// <summary>
        /// 执行采购审核
        /// </summary>       
        public string ConfirmSecond(string strUserName, int iId, int iType)
        {
            return dal.ConfirmSecond(strUserName, iId, iType);
        }

        /// <summary>
        /// 根据PONO更新采购订单状态
        /// </summary>
        public bool UpdateStateByPONO(string strPONO, string strUser, int iState)
        {
            return dal.UpdateStateByPONO(strPONO, strUser, iState);
        }

        /// <summary>
        /// 绑定采购计划
        /// </summary>
        /// <param name="iPOId">采购订单ID</param>
        /// <param name="strPOPlanIdList">采购计划ID串</param>
        /// <param name="strUser">操作用户</param>
        /// <returns>是否成功</returns>
        public bool BindPOPlan(int iPOId, string strPOPlanIdList, string strUser)
        {
            return dal.BindPOPlan(iPOId, strPOPlanIdList, strUser) >= 0 ? true : false;
        }

        /// <summary>
        /// 通过采购订单Id获取相关联采购计划列表树
        /// </summary>
        /// <param name="iPOId">采购订单ID</param>
        /// <returns>计划数据表</returns>
        public DataTable GetBindPOPlanByPoId(int iPOId, int iAdminID)
        {
            DataSet ds = dal.GetBindPOPlanByPoId(iPOId, iAdminID);
            if (ds.IsExsitData())
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 供应商审核
        /// </summary>
        /// <param name="iPOId">采购订单ID</param>
        /// <param name="strUser">操作用户</param>
        /// <returns>执行结果</returns>
        public string ConfirmSupplier(int iPOId, string strUser)
        {
            return dal.ConfirmSupplier(iPOId, strUser);
        }
        #endregion  ExtensionMethod


    }
}

