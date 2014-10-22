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
    /// ShippingPlanDetailHandle 的摘要说明
    /// </summary>
    public class ShippingPlanDetailHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //出货计划明细对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(ShippingPlanDetailHandle));
        HttpRequest _request = HttpContext.Current.Request;
        AdminModel _adminModel = new AdminModel();
        ShippingPlanDetailBLL _bllShippingPlanDetail = new ShippingPlanDetailBLL();

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
                    //获取出货计划明细列表
                    case "GetShippingPlanDetail":
                        context.Response.Write(GetShippingPlanDetail());
                        break;
                    //根据出货计划单Id获得出货计划单明细列表记录
                    case "GetShippingPlanDetailByShippingPlanId":
                        context.Response.Write(GetShippingPlanDetailByShippingPlanId());
                        break;
                    //根据ID获取出货计划单明细记录
                    case "GetShippingPlanDetailById":
                        context.Response.Write(GetShippingPlanDetailById());
                        break;
                    //修改出货计划单
                    case "EditShippingPlanDetail":
                        context.Response.Write(EditShippingPlanDetail());
                        break;
                    //获得未进行出货的出货计划数据
                    case "GetShippingPlanDetailUnOutStockList":
                        context.Response.Write(GetShippingPlanDetailUnOutStockList());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获得未进行出货的出货计划数据
        /// </summary>
        /// <returns></returns>
        private string GetShippingPlanDetailUnOutStockList()
        {
            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("SP.ShippingPlanNo", _request.Form["ShippingPlanNo"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.ProductCode", _request.Form["ProductCode"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("SP.IsApproved", "SP.IsApproved=1", ConditionEnm.None));

            PageResult result = new PageResult();
            try
            {
                List<ShippingPlanDetailDisplayModel> listModel = new List<ShippingPlanDetailDisplayModel>();
                listModel = this._bllShippingPlanDetail.GetShippingPlanDetailUnOutStockList(lstFilter);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获得未进行出货的出货计划数据出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获得未进行出货的出货计划数据出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改出货计划单明细
        /// </summary>
        /// <returns></returns>
        private string EditShippingPlanDetail()
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
                ShippingPlanDetailModel model = this._bllShippingPlanDetail.GetModel(iId);

                if (model == null || model.ID <= 0)
                {
                    result.State = 0;
                    result.Info = "获取数据对应ID的明细数据异常,请重新操作.";
                    return result.ToString();
                }

                SQLOperateHelper.SetEntityFiledValue(model, "PlanQty", _request.Form["PlanQty"]);
                SQLOperateHelper.SetEntityFiledValue(model, "PlanStockCode", _request.Form["PlanStockCode"]);
                SQLOperateHelper.SetEntityFiledValue(model, "ProductCode", _request.Form["ProductCode"]);
                SQLOperateHelper.SetEntityFiledValue(model, "Remark", _request.Form["Remark"]);
                SQLOperateHelper.SetEntityFiledValue(model, "AvailFlag", _request.Form["AvailFlag"]);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllShippingPlanDetail.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑出货计划单明细记录成功！";
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
                result.Info = "编辑出货计划单明细记录出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑出货计划单明细记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据ID获取出货计划单明细记录
        /// </summary>
        /// <returns></returns>
        private string GetShippingPlanDetailById()
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
                ShippingPlanDetailModel model = this._bllShippingPlanDetail.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());

                ShippingPlanModel sModel = new BLL.OMS.ShippingPlanBLL().GetModel((int)model.ShippingPlanID);
                string strShippingPlanNo = string.Empty;
                if (sModel != null)
                    strShippingPlanNo = sModel.ShippingPlanNo;

                WarehouseModel wModel = new BLL.Common.WarehouseBLL().GetModelByCode(model.PlanStockCode);
                string strPlanStockName = string.Empty;
                if (wModel != null)
                    strPlanStockName = wModel.WName;

                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"ShippingPlanNo\":\""
                  + strShippingPlanNo + "\",\"PlanStockName\":\""
                  + strPlanStockName + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取出货计划单明细记录信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取出货计划单明细记录信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据出货计划单Id获得出货计划单明细列表记录
        /// </summary>
        /// <returns></returns>
        private string GetShippingPlanDetailByShippingPlanId()
        {
            PageResult result = new PageResult();
            int iShippingPlanId = -1;
            if (_request.Form["PlanId"] == null || !int.TryParse(_request.Form["PlanId"].ToString(), out iShippingPlanId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                List<ShippingPlanDetailDisplayModel> listModel = this._bllShippingPlanDetail.GetDisplayModelList(iShippingPlanId);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "根据出货计划单Id获得出货计划单明细列表记录出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，根据出货计划单Id获得出货计划单明细列表记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取出货计划明细列表
        /// </summary>
        /// <returns></returns>
        private string GetShippingPlanDetail()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ShippingPlanNo", _request.Form["ShippingPlanNo"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.PlanStockCode", _request.Form["PlanStockCode"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ApprovedUserID", _request.Form["ApprovedUser"], ConditionEnm.Equal));
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
                DataSet ds = _bllShippingPlanDetail.GetShippingPlanDetailPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:"
                    + _request.UserHostAddress + "，获取出货计划明细信息出现异常:" + ex.Message, ex);
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