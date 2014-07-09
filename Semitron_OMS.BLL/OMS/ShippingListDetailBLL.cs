/**  
* ShippingListDetailBLL.cs
*
* 功 能： N/A
* 类 名： ShippingListDetailBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:28   童荣辉    初版
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
    /// 出货计划表
    /// </summary>
    public partial class ShippingListDetailBLL
    {
        private readonly Semitron_OMS.DAL.OMS.ShippingListDetailDAL dal = new Semitron_OMS.DAL.OMS.ShippingListDetailDAL();
        public ShippingListDetailBLL()
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
        public int Add(Semitron_OMS.Model.OMS.ShippingListDetailModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.ShippingListDetailModel model)
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
        public Semitron_OMS.Model.OMS.ShippingListDetailModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.ShippingListDetailModel GetModelByCache(int ID)
        {

            string CacheKey = "ShippingListDetailModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.ShippingListDetailModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.ShippingListDetailModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.ShippingListDetailModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.ShippingListDetailModel> modelList = new List<Semitron_OMS.Model.OMS.ShippingListDetailModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.ShippingListDetailModel model;
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

        public void AddList(List<ShippingListDetailModel> lstShippingListDetailModel)
        {
            dal.AddList(lstShippingListDetailModel);
        }

        /// <summary>
        /// 分页查询出库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>出库单明细数据</returns>
        public DataSet GetShippingListDetailPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetShippingListDetailPageData(searchInfo, out o_RowsCount);
        }
        /// <summary>
        /// 根据出库单Id获得出库单明细列表记录
        /// </summary>
        /// <param name="iShippingListId">出库单Id</param>
        /// <returns>出库单显示明细数据</returns>
        public List<ShippingListDetailDisplayModel> GetDisplayModelList(int iShippingListId)
        {
            List<ShippingListDetailDisplayModel> listDisplayModel = new List<ShippingListDetailDisplayModel>();
            DataTable dt = dal.GetDisplayModelList(iShippingListId);

            foreach (DataRow dr in dt.Rows)
            {
                ShippingListDetailDisplayModel model = new ShippingListDetailDisplayModel();
                model.ID = dr["ID"].ToInt(0);
                model.AvailFlag = dr["AvailFlag"].ToInt(0) == 1 ? true : false;
                model.ShippingListID = dr["ShippingListID"].ToInt(0);
                model.OutQty = dr["OutQty"].ToInt(0);
                model.ShippingListNo = dr["ShippingListNo"].ToString();
                model.ShippingPlanDetailID = dr["ShippingPlanDetailID"].ToInt(0);
                model.ProductCode = dr["ProductCode"].ToString();
                model.Remark = dr["Remark"].ToString();
                DateTime dtShip = DateTime.Now;
                if (DateTime.TryParse(dr["OutStockDate"].ToString(), out dtShip))
                {
                    model.OutStockDate = dtShip;
                }
                else
                {
                    model.OutStockDate = null;
                }

                listDisplayModel.Add(model);
            }
            return listDisplayModel;
        }
        /// <summary>
        /// 验证并更新出库单明细实体
        /// </summary>
        /// <param name="model">出库单明细实体</param>
        /// <returns>操作结果提示</returns>
        public string ValidateAndUpdate(ShippingListDetailModel model)
        {
            string strResult = "OK";
            if (model.ShippingListID <= 0)
            {
                strResult += "出库单单号不能为空.";
            }
            if (string.IsNullOrEmpty(model.ProductCode))
            {
                strResult += "产品编号不能为空.";
            }
            if (model.OutQty <= 0)
            {
                strResult += "出库数量需大于0.";
            }

            if (strResult == "OK" && !this.Update(model))
            {
                strResult = "更新记录发生数据库异常，请联系管理员。";
            }
            return strResult;
        }
        #endregion  ExtensionMethod
    }
}

