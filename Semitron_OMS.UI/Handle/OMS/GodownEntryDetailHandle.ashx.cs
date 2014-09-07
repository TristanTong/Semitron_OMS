using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.Model.Common;
using Newtonsoft.Json;
using Semitron_OMS.Model.OMS;
using System.IO;
using Semitron_OMS.BLL.OMS;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// GodownEntryDetailHandle 的摘要说明
    /// </summary>
    public class GodownEntryDetailHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //入库明细对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(GodownEntryDetailHandle));
        HttpRequest _request = HttpContext.Current.Request;
        AdminModel _adminModel = new AdminModel();
        GodownEntryDetailBLL _bllGodownEntryDetail = new GodownEntryDetailBLL();

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Admin"] == null)
            {
                PageResult result = new PageResult();
                context.Response.Write(result.ToString());
                return;
            }
            else
            {
                _adminModel = context.Session["Admin"] as AdminModel;
            }
            context.Response.ContentType = "application/json";
            string methStr = string.Empty;
            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //获取入库明细列表
                    case "GetGodownEntryDetail":
                        context.Response.Write(GetGodownEntryDetail());
                        break;
                    //根据入库单Id获得入库单明细列表记录
                    case "GetGodownEntryDetailByEntryId":
                        context.Response.Write(GetGodownEntryDetailByEntryId());
                        break;
                    //根据ID获取入库单明细记录
                    case "GetGodownEntryDetailById":
                        context.Response.Write(GetGodownEntryDetailById());
                        break;
                    //修改入库单
                    case "EditGodownEntryDetail":
                        context.Response.Write(EditGodownEntryDetail());
                        break;
                    //获取入库单查找明细，得历史入库单价
                    case "GetGodownEntryDetailLookupList":
                        context.Response.Write(GetGodownEntryDetailLookupList());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取入库单查找明细，得历史入库单价
        /// </summary>
        private string GetGodownEntryDetailLookupList()
        {
            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CustomerOrderDetailId", _request.Form["CustomerOrderDetailId"], ConditionEnm.None));
            string strQueryType = DataUtility.GetPageFormValue(_request.Form["QueryType"], string.Empty);

            PageResult result = new PageResult();
            try
            {
                List<GodownEntryDetailLookupModel> listModel = new List<GodownEntryDetailLookupModel>();
                listModel = this._bllGodownEntryDetail.GetGodownEntryDetailLookupList(lstFilter, strQueryType);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取历史入库单价列表出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取历史入库单价列表出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改入库单明细
        /// </summary>
        /// <returns></returns>
        private string EditGodownEntryDetail()
        {
            PageResult result = new PageResult();
            int iId = -1;
            if (_request.Form["Id"] == null || !int.TryParse(_request.Form["Id"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                GodownEntryDetailModel model = this._bllGodownEntryDetail.GetModel(iId);

                if (model == null || model.ID <= 0)
                {
                    result.State = 0;
                    result.Info = "获取数据对应ID的明细数据异常,请重新操作.";
                    return result.ToString();
                }

                SQLOperateHelper.SetEntityFiledValue(model, "InQty", _request.Form["InQty"]);
                SQLOperateHelper.SetEntityFiledValue(model, "Price", _request.Form["Price"]);
                SQLOperateHelper.SetEntityFiledValue(model, "TotalPrice", _request.Form["TotalPrice"]);
                SQLOperateHelper.SetEntityFiledValue(model, "Remark", _request.Form["Remark"]);
                SQLOperateHelper.SetEntityFiledValue(model, "AvailFlag", _request.Form["AvailFlag"]);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllGodownEntryDetail.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑入库单明细记录成功！";
                }
                else
                {
                    result.State = 0;
                    result.Info = strResult.Replace("OK", "");
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "编辑入库单明细记录出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑入库单明细记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据ID获取入库单明细记录
        /// </summary>
        /// <returns></returns>
        private string GetGodownEntryDetailById()
        {
            PageResult result = new PageResult();
            int iId = -1;
            if (_request.Form["Id"] == null || !int.TryParse(_request.Form["Id"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                GodownEntryDetailModel model = this._bllGodownEntryDetail.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"EntryNo\":\""
                  + new BLL.OMS.GodownEntryBLL().GetModel(model.GodownEntryID).EntryNo + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取入库单明细记录信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取入库单明细记录信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据入库单Id获得入库单明细列表记录
        /// </summary>
        /// <returns></returns>
        private string GetGodownEntryDetailByEntryId()
        {
            PageResult result = new PageResult();
            int iEntryId = -1;
            if (_request.Form["EntryId"] == null || !int.TryParse(_request.Form["EntryId"].ToString(), out iEntryId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                List<GodownEntryDetailDisplayModel> listModel = this._bllGodownEntryDetail.GetDisplayModelList(iEntryId);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "根据入库单Id获得入库单明细列表记录出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，根据入库单Id获得入库单明细列表记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取入库明细列表
        /// </summary>
        /// <returns></returns>
        private string GetGodownEntryDetail()
        {
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();

            //返回查询总数
            int o_RowsCount = 0;

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            //获取表格提交的参数
            //当前查询页码
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(_request.Form["page"], 1));
            //每页大小
            searchInfo.PageSize = DataUtility.ToInt(DataUtility.GetPageFormValue(_request.Form["rp"], 20));
            string strPK = "ID";
            string strOrder = strPK;
            if (_request.Form["sortname"] != null)
            {
                strOrder = _request.Form["sortname"].ToString();
            }
            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(strOrder, string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(_request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.EntryNo", _request.Form["EntryNo"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.InWarehouseCode", _request.Form["InWarehouseCode"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ApprovedUser", _request.Form["ApprovedUser"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ByHandUserID", _request.Form["ByHandUserID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.IsApproved", _request.Form["IsApproved"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.AvailFlag", _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.ProductCode", _request.Form["ProductCode"], ConditionEnm.AllLike));

            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            //查询条件：开始时间，结束时间
            string strTimeField = "D.CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "D.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "D.UpdateTime";
            }
            string strStartTime = DataUtility.GetPageFormValue(_request.Form["startTime"], string.Empty);
            if (strStartTime != string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter(strTimeField, strStartTime, ConditionEnm.GreaterThan));
            }
            string strEndTime = DataUtility.GetPageFormValue(_request.Form["endTime"], string.Empty);
            if (_request.Form["endTime"] != null && _request.Form["endTime"].ToString() != string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter(strTimeField, strEndTime, ConditionEnm.LessThan));
            }
            if (strStartTime == string.Empty && strEndTime == string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter(strTimeField, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ConditionEnm.GreaterThan));
            }
            searchInfo.ConditionFilter = lstFilter;

            DataTable dt = new DataTable();
            try
            {
                DataSet ds = _bllGodownEntryDetail.GetGodownEntryDetailPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:"
                    + _request.UserHostAddress + "，获取入库明细信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}