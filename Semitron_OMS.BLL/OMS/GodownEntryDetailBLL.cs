/**  
* GodownEntryDetailBLL.cs
*
* 功 能： N/A
* 类 名： GodownEntryDetailBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:18   童荣辉    初版
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
	/// 入库单明细表
	/// </summary>
	public partial class GodownEntryDetailBLL
	{
		private readonly Semitron_OMS.DAL.OMS.GodownEntryDetailDAL dal=new Semitron_OMS.DAL.OMS.GodownEntryDetailDAL();
		public GodownEntryDetailBLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Semitron_OMS.Model.OMS.GodownEntryDetailModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Semitron_OMS.Model.OMS.GodownEntryDetailModel model)
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
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Semitron_OMS.Model.OMS.GodownEntryDetailModel GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Semitron_OMS.Model.OMS.GodownEntryDetailModel GetModelByCache(int ID)
		{
			
			string CacheKey = "GodownEntryDetailModelModel-" + ID;
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
				catch{}
			}
			return (Semitron_OMS.Model.OMS.GodownEntryDetailModel)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Semitron_OMS.Model.OMS.GodownEntryDetailModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Semitron_OMS.Model.OMS.GodownEntryDetailModel> DataTableToList(DataTable dt)
		{
			List<Semitron_OMS.Model.OMS.GodownEntryDetailModel> modelList = new List<Semitron_OMS.Model.OMS.GodownEntryDetailModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Semitron_OMS.Model.OMS.GodownEntryDetailModel model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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

        public void AddList(List<GodownEntryDetailModel> lstGodownEntryDetailModel)
        {
            dal.AddList(lstGodownEntryDetailModel);
        }

        /// <summary>
        /// 分页查询入库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>入库单明细数据</returns>
        public DataSet GetGodownEntryDetailPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetGodownEntryDetailPageData(searchInfo, out o_RowsCount);
        }
        /// <summary>
        /// 根据入库单Id获得入库单明细列表记录
        /// </summary>
        /// <param name="iEntryId">入库单Id</param>
        /// <returns>入库单显示明细数据</returns>
        public List<GodownEntryDetailDisplayModel> GetDisplayModelList(int iEntryId)
        {
            List<GodownEntryDetailDisplayModel> listDisplayModel = new List<GodownEntryDetailDisplayModel>();
            DataTable dt = dal.GetDisplayModelList(iEntryId);

            foreach (DataRow dr in dt.Rows)
            {
                GodownEntryDetailDisplayModel model = new GodownEntryDetailDisplayModel();
                model.ID = dr["ID"].ToInt(0);
                model.AvailFlag = dr["AvailFlag"].ToInt(0) == 1 ? true : false;
                model.GodownEntryID = dr["GodownEntryID"].ToInt(0);
                model.InQty = dr["InQty"].ToInt(0);
                model.PONo = dr["PONo"].ToString();
                model.POPlanId = dr["POPlanId"].ToInt(0);
                model.POQuantity = dr["POQuantity"].ToInt(0);
                model.Price = dr["Price"].ToDecimal(0);
                model.ProductCode = dr["ProductCode"].ToString();
                model.Remark = dr["Remark"].ToString();
                DateTime dtShip = DateTime.Now;
                if (DateTime.TryParse(dr["ShipmentDate"].ToString(), out dtShip))
                {
                    model.ShipmentDate = dtShip;
                }
                else
                {
                    model.ShipmentDate = null;
                }

                model.TotalPrice = dr["TotalPrice"].ToDecimal(-1);
                listDisplayModel.Add(model);
            }
            return listDisplayModel;
        }
        /// <summary>
        /// 验证并更新入库单明细实体
        /// </summary>
        /// <param name="model">入库单明细实体</param>
        /// <returns>操作结果提示</returns>
        public string ValidateAndUpdate(GodownEntryDetailModel model)
        {
            string strResult = "OK";
            if (model.GodownEntryID <= 0)
            {
                strResult += "入库单单号不能为空.";
            }
            if (string.IsNullOrEmpty(model.ProductCode))
            {
                strResult += "产品编号不能为空.";
            }
            if (model.InQty <= 0)
            {
                strResult += "入库数量需大于0.";
            }
            if (model.Price <= 0)
            {
                strResult += "产品单价需大于0.";
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

