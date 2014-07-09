/**  
* BrandBLL.cs
*
* 功 能： N/A
* 类 名： BrandBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/4/6 22:06:02   童荣辉    初版
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
    /// 品牌表
    /// </summary>
    public partial class BrandBLL
    {
        private readonly Semitron_OMS.DAL.OMS.BrandDAL dal = new Semitron_OMS.DAL.OMS.BrandDAL();
        public BrandBLL()
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
        public int Add(Semitron_OMS.Model.OMS.BrandModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.BrandModel model)
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
        public Semitron_OMS.Model.OMS.BrandModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.BrandModel GetModelByCache(int ID)
        {

            string CacheKey = "BrandModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.BrandModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.BrandModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.BrandModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.BrandModel> modelList = new List<Semitron_OMS.Model.OMS.BrandModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.BrandModel model;
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
        /// 分页查询品牌明细记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetBrandPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetBrandPageData(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 验证并新增
        /// </summary>
        public string ValidateAndAdd(BrandModel model)
        {
            //判断品牌名称是否已存在           
            if (this.ExistsByBrandName(model.ID, model.BrandName))
            {
                return "同名称的品牌已存在，请勿重复新增。";
            }
            //判断编号是否已存在           
            if (this.ExistsByCode(model.ID, model.Code))
            {
                return "同编号的品牌已存在，请勿重复新增。";
            }

            if (this.Add(model) <= 0)
            {
                return "新增品牌失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 验证并更新
        /// </summary>
        public string ValidateAndUpdate(BrandModel model)
        {
            //判断品牌名称是否已存在           
            if (this.ExistsByBrandName(model.ID, model.BrandName))
            {
                return "同名称的品牌已存在，请勿重复新增。";
            }
            //判断编号是否已存在           
            if (this.ExistsByCode(model.ID, model.Code))
            {
                return "同编号的品牌已存在，请勿重复新增。";
            }
            if (!this.Update(model))
            {
                return "编辑品牌失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 是否存在指定编码
        /// </summary>
        private bool ExistsByCode(int iID, string strCode)
        {
            return dal.ExistsByCode(iID, strCode);
        }
        /// <summary>
        /// 是否存在指定品牌名称
        /// </summary>
        private bool ExistsByBrandName(int iID, string strName)
        {
            return dal.ExistsByBrandName(iID, strName);
        }
        /// <summary>
        /// 验证并删除品牌
        /// </summary>
        public string ValidateAndDelBrand(int iId)
        {
            if (!this.SetValid(iId, 0))
            {
                return "删除品牌失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 设置记录的有效无效状态
        /// </summary>
        private bool SetValid(int iId, int iStatus)
        {
            return dal.SetValid(iId, iStatus);
        }

        /// <summary>
        /// 从缓存中取出品牌
        /// </summary>
        public DataTable GetDataTableByCache()
        {
            return dal.GetDataTableByCache();
        }
        #endregion  ExtensionMethod
    }
}

