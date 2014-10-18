/**  
* BankAccountBLL.cs
*
* 功 能： N/A
* 类 名： BankAccountBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/5 23:43:12   童荣辉    初版
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
using Semitron_OMS.Model.FM;
namespace Semitron_OMS.BLL.FM
{
    /// <summary>
    /// BankAccountBLL
    /// </summary>
    public partial class BankAccountBLL
    {
        private readonly Semitron_OMS.DAL.FM.BankAccountDAL dal = new Semitron_OMS.DAL.FM.BankAccountDAL();
        public BankAccountBLL()
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
        public int Add(Semitron_OMS.Model.FM.BankAccountModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.FM.BankAccountModel model)
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
        public Semitron_OMS.Model.FM.BankAccountModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.FM.BankAccountModel GetModelByCache(int ID)
        {

            string CacheKey = "BankAccountModelModel-" + ID;
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
            return (Semitron_OMS.Model.FM.BankAccountModel)objModel;
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
        public List<Semitron_OMS.Model.FM.BankAccountModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.FM.BankAccountModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.FM.BankAccountModel> modelList = new List<Semitron_OMS.Model.FM.BankAccountModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.FM.BankAccountModel model;
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
        public string ValidateAndEdit(BankAccountModel model)
        {
            string strResult = "发生错误";
            if (String.IsNullOrEmpty(model.AccountName))
            {
                return "账户名称不能为空";
            }
            if (String.IsNullOrEmpty(model.CardNo))
            {
                return "卡号不能为空";
            }
            if (!model.AvailFlag)
            {
                return "当前记录已无效,无法完成编辑操作.";
            }
            if (GetModelList("(AccountName='" + model.AccountName
                + "' OR CardNo='" + model.CardNo
                + "') AND ID!='" + model.ID + "'").Count > 0)
            {
                return "此账户名称或卡号已增加，请确认后修改。";
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

        public string ValidateAndDelBankAccount(int iId)
        {
            BankAccountModel model = this.GetModel(iId);
            if (model.AvailFlag == false)
            {
                return "当前记录已无效,请勿重复操作!";
            }
            model.AvailFlag = false;
            return this.Update(model) ? "OK" : "ERROR";
        }
        /// <summary>
        /// 分页查询记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetBankAccountPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetBankAccountPageData(searchInfo, out o_RowsCount);
        }

        public DataTable GetDataTableByCache(string strTableName)
        {
            throw new NotImplementedException();
        }
        #endregion  ExtensionMethod


    }
}

