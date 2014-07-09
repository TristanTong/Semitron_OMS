/**  
* CurrencyTypeBLL.cs
*
* 功 能： N/A
* 类 名： CurrencyTypeBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:27   童荣辉    初版
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
using Semitron_OMS.DAL;
namespace Semitron_OMS.BLL.OMS
{
    /// <summary>
    /// 货币种类表
    /// </summary>
    public partial class CurrencyTypeBLL
    {
        private readonly Semitron_OMS.DAL.OMS.CurrencyTypeDAL dal = new Semitron_OMS.DAL.OMS.CurrencyTypeDAL();
        public CurrencyTypeBLL()
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
        public int Add(Semitron_OMS.Model.OMS.CurrencyTypeModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.CurrencyTypeModel model)
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
        public Semitron_OMS.Model.OMS.CurrencyTypeModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.CurrencyTypeModel GetModelByCache(int ID)
        {

            string CacheKey = "CurrencyTypeModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.CurrencyTypeModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.CurrencyTypeModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.CurrencyTypeModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.CurrencyTypeModel> modelList = new List<Semitron_OMS.Model.OMS.CurrencyTypeModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.CurrencyTypeModel model;
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
        /// 根据数据库缓存依赖获得数据
        /// </summary>
        public DataTable GetDataTableByCache()
        {
            return SQLNotifier.GetDataTable(ConstantValue.SQLNotifierDepObj.CurrencyTypeDepSql, ConstantValue.SQLNotifierDepObj.CurrencyTypeDepSql, ConstantValue.TableNames.CurrencyType, null);
        }

        /// <summary>
        /// 分页查询货币种类明细记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetCurrencyTypePageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetCurrencyTypePageData(searchInfo, out o_RowsCount);
        }

        public string ValidateAndAdd(CurrencyTypeModel model)
        {
            //判断编号是否已存在           
            //if (this.ExistsByCode(model.))
            //{
            //    return "同编号的货币种类已存在，请勿重复新增。";
            //}

            if (this.Add(model) <= 0)
            {
                return "新增货币种类失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 根据编号判断是否存在
        /// </summary>
        //private bool ExistsByCode(string strCode)
        //{
        //    return dal.ExistsByCode(strCode);
        //}
        public string ValidateAndUpdate(CurrencyTypeModel model)
        {
            if (!this.Update(model))
            {
                return "编辑货币种类失败。";
            }
            return "OK";
        }
        /// <summary>
        /// 验证与逻辑删除
        /// </summary>
        public string ValidateAndDelCurrencyType(int iId)
        {
            if (!this.SetValid(iId, 0))
            {
                return "删除货币种类失败。";
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
        #endregion  ExtensionMethod
    }
}

