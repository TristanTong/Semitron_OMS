/**  
* GodownEntryBLL.cs
*
* 功 能： N/A
* 类 名： GodownEntryBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:17   童荣辉    初版
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
	/// 入库单表
	/// </summary>
	public partial class GodownEntryBLL
	{
		private readonly Semitron_OMS.DAL.OMS.GodownEntryDAL dal=new Semitron_OMS.DAL.OMS.GodownEntryDAL();
		public GodownEntryBLL()
		{}
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
		public int  Add(Semitron_OMS.Model.OMS.GodownEntryModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Semitron_OMS.Model.OMS.GodownEntryModel model)
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
		public Semitron_OMS.Model.OMS.GodownEntryModel GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Semitron_OMS.Model.OMS.GodownEntryModel GetModelByCache(int ID)
		{
			
			string CacheKey = "GodownEntryModelModel-" + ID;
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
			return (Semitron_OMS.Model.OMS.GodownEntryModel)objModel;
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
		public List<Semitron_OMS.Model.OMS.GodownEntryModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Semitron_OMS.Model.OMS.GodownEntryModel> DataTableToList(DataTable dt)
		{
			List<Semitron_OMS.Model.OMS.GodownEntryModel> modelList = new List<Semitron_OMS.Model.OMS.GodownEntryModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Semitron_OMS.Model.OMS.GodownEntryModel model;
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

        /// <summary>
        /// 验证并新增入库单
        /// </summary>
        /// <param name="model">入库单实体</param>
        /// <returns>新增结果</returns>
        public int ValidateAndAdd(GodownEntryModel model, ref string strResult)
        {
            strResult = "OK";
            int iReturn = -1;
            if (string.IsNullOrEmpty(model.EntryNo))
            {
                strResult = "入库单号不能为空";
            }

            if (strResult == "OK")
            {
                iReturn = Add(model);
            }
            return iReturn;
        }

        /// <summary>
        /// 验证并更新入库单
        /// </summary>
        /// <param name="model">入库单实体</param>
        /// <returns>更新结果</returns>
        public string ValidateAndUpdate(GodownEntryModel model)
        {
            string strResult = "OK";
            if (model.State == 0)
            {
                strResult = "更新入库单失败!当前入库单已删除。";
            }

            if (model.IsApproved == true)
            {
                strResult = "更新入库单失败!当前入库单已审核。";
            }

            if (strResult == "OK" && !Update(model))
            {
                strResult = "更新入库单失败!数据库操作异常。";
            }
            return strResult;
        }


        /// <summary>
        /// 验证并逻辑删除入库单
        /// </summary>
        /// <param name="iId">入库单ID</param>
        /// <returns>删除结果</returns>
        public string ValidateAndDelGodownEntry(int iId)
        {
            GodownEntryModel model = GetModel(iId);

            if (model.IsApproved == true)
            {
                return "删除失败,已审核的入库单不允许删除!";
            }

            if (!this.SetValid(iId, 0))
            {
                return "删除失败,系统异常。";
            }
            return "OK";
        }

        /// <summary>
        /// 验证并审核入库单
        /// </summary>
        public string ValidateAndApproveGodownEntry(int iId, int iUser)
        {
            return dal.ApproveGodownEntry(iId, iUser);
        }


        /// <summary>
        /// 设置入库单状态
        /// </summary>
        /// <param name="iId">入库单ID</param>
        /// <param name="iStatus">状态ID</param>
        /// <returns>执行结果是否成功</returns>
        public bool SetValid(int iId, int iStatus)
        {
            return dal.SetValid(iId, iStatus);
        }

        /// <summary>
        /// 分页查询入库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>入库单数据</returns>
        public DataSet GetGodownEntryPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetGodownEntryPageData(searchInfo, out o_RowsCount);
        }
        #endregion  ExtensionMethod



    }
}

