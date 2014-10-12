/**  
* CommonTableBLL.cs
*
* 功 能： N/A
* 类 名： CommonTableBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 12:05:52   童荣辉    初版
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
    /// 公共值表(数据表字段
    /// </summary>
    public partial class CommonTableBLL
    {
        private readonly Semitron_OMS.DAL.OMS.CommonTableDAL dal = new Semitron_OMS.DAL.OMS.CommonTableDAL();
        public CommonTableBLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.CommonTableModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.CommonTableModel model)
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
        public Semitron_OMS.Model.OMS.CommonTableModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.CommonTableModel GetModelByCache(int ID)
        {

            string CacheKey = "CommonTableModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.CommonTableModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.CommonTableModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.CommonTableModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.CommonTableModel> modelList = new List<Semitron_OMS.Model.OMS.CommonTableModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.CommonTableModel model;
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
        /// 获得指定条件的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strField">字段名</param>
        /// <param name="strKey">字段值</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="strOrder">降序还是升序</param>
        /// <returns>数据集合</returns>
        public DataSet GetListByWhere(string strTableName, string strField, string strKey, string strOrderField, string strOrder)
        {
            return dal.GetListByWhere(strTableName, strField, strKey, strOrderField, strOrder);
        }

        /// <summary>
        /// 获得指定条件的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strField">字段名</param>
        /// <param name="strKey">字段值</param>
        /// <returns>数据表</returns>
        public DataTable GetListByWhere(string strTableName, string strField, string strKey)
        {
            return dal.GetListByWhere(strTableName, strField, strKey, string.Empty, string.Empty).Tables[0];
        }

        /// <summary>
        /// 根据数据库缓存依赖获得数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableByCache(string strTableName)
        {
            return dal.GetDataTableByCache(strTableName);
        }

        /// <summary>
        /// 分页查询记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetCommonTablePageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetCommonTablePageData(searchInfo, out o_RowsCount);
        }

        /// <summary>
        /// 验证并删除数据
        /// </summary>
        public string ValidateAndDelCommonTable(int iId)
        {
            return this.Delete(iId) ? "OK" : "ERROR";
        }

        /// <summary>
        /// 验证并新增或修改数据项
        /// </summary>
        public string ValidateAndEdit(CommonTableModel model)
        {
            string strResult = "发生错误";
            if (String.IsNullOrEmpty(model.TableName))
            {
                return "数据库表值不能为空";
            }
            if (String.IsNullOrEmpty(model.FieldID))
            {
                return "字段表值不能为空";
            }
            if (String.IsNullOrEmpty(model.Key))
            {
                return "键值不能为空";
            }
            if (GetModelList("TableName='" + model.TableName
                + "' AND FieldID='" + model.FieldID
                + "' AND [Key]='" + model.Key
                + "' AND ID !='" + model.ID + "'").Count > 0)
            {
                return "相同表值数据项已配置，请确认后修改。";
            }
            if (model.ID > 0)
            {
                strResult = this.Update(model) ? "OK" : "修改数据字典项失败";
            }
            else
            {
                strResult = this.Add(model) > 0 ? "OK" : "新增数据字典项失败";
            }
            return strResult;
        }
        #endregion  ExtensionMethod
    }
}

