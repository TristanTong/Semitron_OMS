/**  
* AttachmentBLL.cs
*
* 功 能： N/A
* 类 名： AttachmentBLL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/21 23:25:58   童荣辉    初版
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
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.BLL.OMS
{
    /// <summary>
    /// AttachmentBLL
    /// </summary>
    public partial class AttachmentBLL
    {
        private readonly Semitron_OMS.DAL.OMS.AttachmentDAL dal = new Semitron_OMS.DAL.OMS.AttachmentDAL();
        public AttachmentBLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.AttachmentModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.AttachmentModel model)
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
        public Semitron_OMS.Model.OMS.AttachmentModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Semitron_OMS.Model.OMS.AttachmentModel GetModelByCache(int ID)
        {

            string CacheKey = "AttachmentModelModel-" + ID;
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
            return (Semitron_OMS.Model.OMS.AttachmentModel)objModel;
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
        public List<Semitron_OMS.Model.OMS.AttachmentModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Semitron_OMS.Model.OMS.AttachmentModel> DataTableToList(DataTable dt)
        {
            List<Semitron_OMS.Model.OMS.AttachmentModel> modelList = new List<Semitron_OMS.Model.OMS.AttachmentModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Semitron_OMS.Model.OMS.AttachmentModel model;
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
        /// 批量保存附件文件
        /// </summary>
        /// <param name="strObjType">对象类型</param>
        /// <param name="strObjId">对象Id或编号</param>
        /// <param name="strFileUrl">附件HTTP地址列表</param>
        /// <returns>批量保存是否成功</returns>
        public bool BatchSaveFiles(string strMapPath, string strObjType, string strObjId, string strFileUrl, string strUserName)
        {
            //对于同一对象类型与对象Id或编号的附件文件
            //1.库中存在但不存在于strFileUrl中的附件设置为无效
            //2.库中存在但也存在于strFileUrl中的附件不做操作，不重复添加
            //3.库中不存在但存在于strFileUrl中的附件做新增操作
            List<string> lstSuccessSave = new List<string>();
            foreach (string strPath in CommonMethods.GetPhysicalByUrl(strFileUrl).SplitStringToList('|'))
            {
                AppFileManage fm = AppFileManage.CreateInstance;
                if (!string.IsNullOrEmpty(strPath))
                {
                    lstSuccessSave.Add(fm.SaveAppUploadFile(strMapPath + strPath));
                }
            }
            return dal.BatchSaveFiles(strObjType, strObjId, lstSuccessSave.ListToSplitString('|'), strUserName);
        }

        /// <summary>
        /// 根据附件类型和对象Id获取UrlPath列表
        /// </summary>
        public string GetUrlListByObj(string strObjType, string strObjId)
        {
            string strUrlPaths = string.Empty;
            DataSet ds = dal.GetList("AvailFlag=1 AND ObjType='" + strObjType + "' AND ObjId IN (SELECT value FROM dbo.SplitToTable('" + strObjId + "',','))");
            if (ds.IsExsitData())
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    strUrlPaths += dr["UrlPath"].ToString().Trim() + "|";
            }
            return strUrlPaths.TrimEnd('|');
        }

        /// <summary>
        /// 获取附件Html
        /// </summary>
        public string GetAttachmentHtml(string strObjType, string strIds, string strFileServerUrl)
        {
            string strUrlPath = this.GetUrlListByObj(strObjType, strIds);
            string[] arrUrlPath = strUrlPath.Split('|');
            string strHtml = string.Empty;
            foreach (string strUrl in arrUrlPath)
            {
                int index = strUrl.LastIndexOf("/");
                string strImageUrl = strUrl;
                if (strUrl.EndsWith(".txt"))
                {
                    strImageUrl = strFileServerUrl + "Icon/" + "txtIcon.jpg";
                }
                else if (strUrl.EndsWith(".pdf"))
                {
                    strImageUrl = strFileServerUrl + "Icon/" + "pdfIcon.jpg";
                }
                else if (strUrl.EndsWith(".doc") || strUrl.EndsWith(".docx"))
                {
                    strImageUrl = strFileServerUrl + "Icon/" + "docIcon.jpg";
                }
                else if (strUrl.EndsWith(".xls") || strUrl.EndsWith(".xlsx"))
                {
                    strImageUrl = strFileServerUrl + "Icon/" + "excelIcon.jpg";
                }
                strHtml += "<div style=\"width: 200px; height: 150px; padding: 20px 10px 10px  10px; float: left; text-align: center;\"> <a href=\"" + strUrl + "\" target=\"_blank\"><img alt=\"\" src=\""
                    + strImageUrl + "\" style=\"width: 200px; height: 150px;\" />"
                    + strUrl.Substring((index > -1 ? (index + 1) : 0)) + "</a><div class=\"cancelFile\"><a onclick=\"DeleteAttach('" + strUrl.Replace("/", "*").Trim() + "')\"><img src=\"/Scripts/jquery.uploadify-v2.1.0/cancel.png\" border=\"0\"></a></div></div>";
            }
            return strHtml;
        }

        /// <summary>
        /// 验证并删除附件
        /// </summary>
        /// <param name="strUrl">地址url</param>
        /// <returns>操作结果</returns>
        public string ValidateAndDelAttachment(string strUrl)
        {
            string strResult = "OK";
            if (!dal.DeleteByUrl(strUrl))
            {
                strResult = "删除附件失败！";
            }
            return strResult;
        }
        #endregion  ExtensionMethod

    }
}

