/**  
* IndustryTrendBLL.cs
*
* 功 能： N/A
* 类 名： IndustryTrendBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/30 18:19:19   童荣辉    初版
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
using Semitron_OMS.Model.Site;
namespace Semitron_OMS.BLL.Site
{
    /// <summary>
    /// IndustryTrendBLL
    /// </summary>
    public partial class IndustryTrendBLL
    {
        private readonly Semitron_OMS.DAL.Site.IndustryTrendDAL dal = new Semitron_OMS.DAL.Site.IndustryTrendDAL();
        public IndustryTrendBLL()
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
        public int Add(Semitron_OMS.Model.Site.IndustryTrendModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.Site.IndustryTrendModel model)
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
        public Semitron_OMS.Model.Site.IndustryTrendModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.Site.IndustryTrendModel GetModelByCache(int ID)
        {

            string CacheKey = "IndustryTrendModelModel-" + ID;
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
            return (Semitron_OMS.Model.Site.IndustryTrendModel)objModel;
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
        public List<Semitron_OMS.Model.Site.IndustryTrendModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.Site.IndustryTrendModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.Site.IndustryTrendModel> modelList = new List<Semitron_OMS.Model.Site.IndustryTrendModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.Site.IndustryTrendModel model;
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
        /// 获取行业动态首页列表
        /// </summary>
        public DataTable GetIndustryTrendInMain(string strLang)
        {
            DataTable dt = dal.GetIndustryTrendInMain(strLang);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取行业动态索引页数据
        /// </summary>
        public DataTable GetIndustryTrendInIndex(string strLang)
        {
            DataTable dt = dal.GetIndustryTrendInIndex(strLang);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取行业动态列表树
        /// </summary>
        public DataTable GetTrendTree(string strLang)
        {
            DataTable dt = dal.GetTrendTree(strLang);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 验证与逻辑删除
        /// </summary>
        public string ValidateAndDelIndustryTrend(int iId)
        {
            if (!this.SetValid(iId, 0))
            {
                return "删除行业动态失败。";
            }
            return "OK";
        }

        /// <summary>
        /// 设置记录为无效
        /// </summary>
        private bool SetValid(int iId, int iStatus)
        {
            return dal.SetValid(iId, iStatus);
        }
        /// <summary>
        /// 验证与编辑
        /// </summary>
        public string ValidateAndUpdate(IndustryTrendModel model)
        {
            if (!this.Update(model))
            {
                return "编辑行业动态失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 验证与新增
        /// </summary>
        public string ValidateAndAdd(IndustryTrendModel model)
        {
            //判断编号是否已存在           
            if (this.ExistsByCode(model.Code))
            {
                return "同编号的展示产品已存在，请勿重复新增。";
            }
            if (this.Add(model) <= 0)
            {
                return "新增行业动态失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 根据编号判断是否存在
        /// </summary>
        private bool ExistsByCode(string strCode)
        {
            return dal.ExistsByCode(strCode);
        }
        /// <summary>
        /// 分页查询行业动态明细记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetIndustryTrendPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetIndustryTrendPageData(searchInfo, out o_RowsCount);
        }
        
        #endregion  ExtensionMethod        
    }
}

