using Newtonsoft.Json;
using Semitron_OMS.BLL.CRM;
using Semitron_OMS.BLL.OMS;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Model.CRM;
using Semitron_OMS.Model.FM;
using Semitron_OMS.Model.OMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
namespace Semitron_OMS.UI.Handle.FM
{
    /// <summary>
    /// GatheringPlanHandle 的摘要说明
    /// </summary>
    public class GatheringPlanHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //收款计划对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(GatheringPlanHandle));
        Semitron_OMS.BLL.FM.GatheringPlanBLL _bllGatheringPlan = new Semitron_OMS.BLL.FM.GatheringPlanBLL();
        HttpRequest _request = HttpContext.Current.Request;
        AdminModel _adminModel = new AdminModel();

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
            if (context.Request.QueryString["meth"] != null)
            {
                methStr = context.Request.QueryString["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //获取收款计划列表
                    case "GetGatheringPlan":
                        context.Response.Write(GetGatheringPlan());
                        break;
                    //编辑收款计划
                    case "EditGatheringPlan":
                        context.Response.Write(EditGatheringPlan());
                        break;
                    //获取收款计划实体
                    case "GetGatheringPlanById":
                        context.Response.Write(GetGatheringPlanById());
                        break;
                    //删除收款计划
                    case "DelGatheringPlan":
                        context.Response.Write(DelGatheringPlan());
                        break;
                    //获取附件列表
                    case "GetGatheringPlanAttachmentHtml":
                        context.Response.ContentType = "text/plain";
                        context.Response.Charset = "utf-8";
                        context.Response.Write(GetGatheringPlanAttachmentHtml());
                        break;
                }
            }

            context.Response.End();
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        private string GetGatheringPlanAttachmentHtml()
        {
            PageResult result = new PageResult();

            try
            {
                List<GatheringPlanModel> lstModel = this._bllGatheringPlan.GetModelList("InnerOrderNO='" + DataUtility.GetPageFormValue(_request.Form["InnerOrderNO"], string.Empty) + "'");
                if (lstModel.Count > 0)
                {
                    string strIds = string.Empty;
                    lstModel.ForEach(m => { strIds += m.ID + ","; });
                    strIds = strIds.TrimEnd(',');
                    string strFileServerUrl = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerUrl);
                    return new AttachmentBLL().GetAttachmentHtml(
                        ConstantValue.TableAttachment.ColumnObjType.GatheringPlanFilePath,
                        strIds,
                        strFileServerUrl);
                }
                else
                {
                    result.State = 0;
                    result.Info = "获取附件列表为空。";
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取附件列表出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取附件列表出现异常：" + ex.Message, ex);
            }

            return result.ToString();
        }

        /// <summary>
        /// 删除收款计划
        /// </summary>
        private string DelGatheringPlan()
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
                string strResult = this._bllGatheringPlan.ValidateAndDelGatheringPlan(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除收款计划成功。";
                }
                else
                {
                    result.State = 0;
                    result.Info = strResult;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "删除收款计划出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除收款计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        //获取收款计划实体
        private string GetGatheringPlanById()
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
                GatheringPlanModel model = this._bllGatheringPlan.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                string strCPN = string.Empty;
                string strSaleStandardPrice = string.Empty;
                CustomerOrderDetailModel dModel = new CustomerOrderDetailBLL().GetModel(model.CustomerOrderDetailID);
                if (dModel != null)
                {
                    strCPN = dModel.CPN;
                    strSaleStandardPrice = dModel.SalePrice.ToString();
                }
                string strCustomerName = string.Empty;
                CustomerModel cModel = new CustomerBLL().GetModel((int)model.CustomerID);
                if (cModel != null)
                {
                    strCustomerName = cModel.CustomerName;
                }
                string strCorporationName = string.Empty;
                CorporationModel corModel = new CorporationBLL().GetModel(model.CorporationID);
                if (corModel != null)
                {
                    strCorporationName = corModel.CompanyName;
                }
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"CPN\":\"" + strCPN
                    + "\",\"SaleStandardPrice\":\"" + strSaleStandardPrice
                    + "\",\"CustomerName\":\"" + strCustomerName
                    + "\",\"CorporationName\":\"" + strCorporationName
                    + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取收款计划详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取收款计划详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 新增或修改收款计划
        /// </summary>
        /// <returns></returns>
        private string EditGatheringPlan()
        {
            PageResultJUI result = new PageResultJUI();
            int iId = -1;
            if (_request.Form["SeletedGatheringPlanId"] == null || !int.TryParse(_request.Form["SeletedGatheringPlanId"].ToString(), out iId))
            {
                result.statusCode = 300;
                result.message = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                GatheringPlanModel model = new GatheringPlanModel();
                if (iId > 0)
                {
                    model = this._bllGatheringPlan.GetModel(iId);
                }
                else
                {
                    model.ID = iId;
                    SQLOperateHelper.SetEntityFiledValue(model, "CreateUser", _adminModel.Username);
                    SQLOperateHelper.SetEntityFiledValue(model, "CreateTime", DateTime.Now);
                }
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.statusCode = 300;
                    result.message = strGetResult;
                    return result.ToString();
                }

                string strResult = this._bllGatheringPlan.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.callbackType = "closeCurrent";
                    result.statusCode = 200;
                    //result.message = "\u64cd\u4f5c\u6210\u529f";
                    //result.Remark = model.ID.ToString();
                    result.message = "编辑收款计划成功！";
                }
                else
                {
                    result.statusCode = 300;
                    result.message = strResult;
                }
            }
            catch (Exception ex)
            {
                result.statusCode = 300;
                result.message = "编辑收款计划出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑收款计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(GatheringPlanModel model)
        {
            if (_request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CustomerOrderDetailId"] == null
                || _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CorporationID"] == null
                || _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.InnerOrderNO"] == null
                || _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CustomerOrderNO"] == null
                //||...
                )
            {
                return "系统错误,参数获取异常。";
            }

            SQLOperateHelper.SetEntityFiledValue(model, "CustomerOrderDetailID", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CustomerOrderDetailId"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CorporationID", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CorporationID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "InnerOrderNO", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.InnerOrderNO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerOrderNO", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CustomerOrderNO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerID", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CustomerID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Qty", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.CustQuantity"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleStandardCurrency", 1);
            SQLOperateHelper.SetEntityFiledValue(model, "SalePrice", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.SalePrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleTotal", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.SaleTotal"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleRealCurrency", _request.Form["SaleRealCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleExchangeRate", _request.Form["SaleExchangeRate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleRealPrice", _request.Form["SaleRealPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleRealTotal", _request.Form["SaleRealTotal"]);
            SQLOperateHelper.SetEntityFiledValue(model, "PaymentTypeID", _request.Form["GatheringPlan_Edit_CustomerOrderDetailLookup.PaymentTypeID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsCustomerVATInvoice", _request.Form["IsCustomerVATInvoice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerVATInvoiceNo", _request.Form["CustomerVATInvoiceNo"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsCustomerPay", _request.Form["IsCustomerPay"]);
            SQLOperateHelper.SetEntityFiledValue(model, "TrackingNumber", _request.Form["TrackingNumber"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFee", _request.Form["OtherFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFeeRemark", _request.Form["OtherFeeRemark"]);
            SQLOperateHelper.SetEntityFiledValue(model, "StandardIncomeRealDiff", _request.Form["StandardIncomeRealDiff"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SalesManProportion", _request.Form["SalesManProportion"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SalesManPay", _request.Form["SalesManPay"]);
            SQLOperateHelper.SetEntityFiledValue(model, "POPrice", _request.Form["POPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "GrossProfits", _request.Form["GrossProfits"]);
            SQLOperateHelper.SetEntityFiledValue(model, "NetProfit", _request.Form["NetProfit"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ProfitMargin", _request.Form["ProfitMargin"]);
            SQLOperateHelper.SetEntityFiledValue(model, "GatheringPlanDate", _request.Form["GatheringPlanDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "FeeBackDate", _request.Form["FeeBackDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "State", _request.Form["State"]);

            SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
            SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);

            return "OK";
        }

        /// <summary>
        /// 获取收款计划列表
        /// </summary>
        private string GetGatheringPlan()
        {
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();

            //返回查询总数
            int o_RowsCount = 0;

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            //获取表格提交参数
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
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.State",
              _request.Form["State"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.InnerOrderNO",
                _request.Form["InnerOrderNO"], ConditionEnm.AllLike));//这里alllike就是模糊查询了
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.CustomerOrderNO",
                _request.Form["CustomerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("S.CCode",
                _request.Form["CustomerCode"], ConditionEnm.Equal));

            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "P.CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "P.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "P.UpdateTime";
            }
            if (strTimeType == "3")
            {
                strTimeField = "P.GatheringPlanDate";
            }
            if (strTimeType == "4")
            {
                strTimeField = "P.FeeBackDate";
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
                DataSet ds = _bllGatheringPlan.GetGatheringPlanPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取收款计划信息出现异常:" + ex.Message, ex);
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