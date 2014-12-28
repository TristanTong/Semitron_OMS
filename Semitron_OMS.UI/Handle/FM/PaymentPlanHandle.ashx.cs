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
    /// PaymentPlanHandle 的摘要说明
    /// </summary>
    public class PaymentPlanHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //付供应商款计划对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(PaymentPlanHandle));
        Semitron_OMS.BLL.FM.PaymentPlanBLL _bllPaymentPlan = new Semitron_OMS.BLL.FM.PaymentPlanBLL();
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
                    //获取付供应商款计划列表
                    case "GetPaymentPlan":
                        context.Response.Write(GetPaymentPlan());
                        break;
                    //编辑付供应商款计划
                    case "EditPaymentPlan":
                        context.Response.Write(EditPaymentPlan());
                        break;
                    //获取付供应商款计划实体
                    case "GetPaymentPlanById":
                        context.Response.Write(GetPaymentPlanById());
                        break;
                    //删除付供应商款计划
                    case "DelPaymentPlan":
                        context.Response.Write(DelPaymentPlan());
                        break;
                    //获取附件列表
                    case "GetPaymentPlanAttachmentHtml":
                        context.Response.ContentType = "text/plain";
                        context.Response.Charset = "utf-8";
                        context.Response.Write(GetPaymentPlanAttachmentHtml());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        private string GetPaymentPlanAttachmentHtml()
        {
            PageResult result = new PageResult();

            try
            {
                List<PaymentPlanModel> lstModel = this._bllPaymentPlan.GetModelList("PONO='" + DataUtility.GetPageFormValue(_request.Form["PONO"], string.Empty) + "'");
                if (lstModel.Count > 0)
                {
                    string strIds = string.Empty;
                    lstModel.ForEach(m => { strIds += m.ID + ","; });
                    strIds = strIds.TrimEnd(',');
                    string strFileServerUrl = ConfigurationManager.AppSettings.Get(ConstantValue.AppSettingsNames.FileServerUrl);
                    return new AttachmentBLL().GetAttachmentHtml(
                        ConstantValue.TableAttachment.ColumnObjType.PaymentPlanFilePath,
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
        /// 删除付供应商款计划
        /// </summary>
        private string DelPaymentPlan()
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
                string strResult = this._bllPaymentPlan.ValidateAndDelPaymentPlan(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除付供应商款计划成功。";
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
                result.Info = "删除付供应商款计划出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除付供应商款计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        //获取付供应商款计划实体
        private string GetPaymentPlanById()
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
                PaymentPlanModel model = this._bllPaymentPlan.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                string strSupplierName = string.Empty, strInnerBuyer = string.Empty;
                SupplierModel sModel = new SupplierBLL().GetModel((int)model.SupplierID);
                if (sModel != null)
                {
                    strSupplierName = sModel.SupplierName;
                }//InnerBuyer
                POModel poModel = new POBLL().GetModelByPONO(model.PONO);
                if (poModel != null)
                {
                    strInnerBuyer = poModel.InnerBuyer;
                }
                string strStandardPrice = string.Empty, strStandardTotalPrice = string.Empty;//应收标准单价(USD)
                POPlanModel plModel = new POPlanBLL().GetModel(model.POPlanID);
                if (plModel != null)
                {
                    strStandardPrice = plModel.BuyPrice.ToString();
                    strStandardTotalPrice = plModel.BuyCost.ToString();
                }
                string strCorporationName = string.Empty;
                CorporationModel corModel = new CorporationBLL().GetModel(model.CorporationID);
                if (corModel != null)
                {
                    strCorporationName = corModel.CompanyName;
                }
                strResult = strResult.Substring(0, strResult.Length - 1)
                    + ",\"SupplierName\":\"" + strSupplierName
                    + "\",\"InnerBuyer\":\"" + strInnerBuyer
                    + "\",\"StandardPrice\":\"" + strStandardPrice
                    + "\",\"StandardTotalPrice\":\"" + strStandardTotalPrice
                    + "\",\"CorporationName\":\"" + strCorporationName
                    + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取付供应商款计划详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username
                    + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress
                    + "，获取付供应商款计划详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 新增或修改付供应商款计划
        /// </summary>
        /// <returns></returns>
        private string EditPaymentPlan()
        {
            PageResultJUI result = new PageResultJUI();
            int iId = -1;
            if (_request.Form["SeletedPaymentPlanId"] == null || !int.TryParse(_request.Form["SeletedPaymentPlanId"].ToString(), out iId))
            {
                result.statusCode = 300;
                result.message = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                PaymentPlanModel model = new PaymentPlanModel();
                if (iId > 0)
                {
                    model = this._bllPaymentPlan.GetModel(iId);
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

                string strResult = this._bllPaymentPlan.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.callbackType = "closeCurrent";
                    result.statusCode = 200;
                    //result.message = "\u64cd\u4f5c\u6210\u529f";
                    //result.Remark = model.ID.ToString();
                    result.message = "编辑付供应商款计划成功！";
                    result.confirmMsg = model.ID.ToString();
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
                result.message = "编辑付供应商款计划出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑付供应商款计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(PaymentPlanModel model)
        {
            if (_request.Form["PaymentPlan_Edit_POPlanLookup.POPlanId"] == null
                || _request.Form["PaymentPlan_Edit_POPlanLookup.CorporationID"] == null
                || _request.Form["PaymentPlan_Edit_POPlanLookup.PONo"] == null
                || _request.Form["PaymentPlan_Edit_POPlanLookup.MPN"] == null
                //||...
                )
            {
                return "系统错误,参数获取异常。";
            }

            SQLOperateHelper.SetEntityFiledValue(model, "POPlanID", _request.Form["PaymentPlan_Edit_POPlanLookup.POPlanId"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CorporationID", _request.Form["PaymentPlan_Edit_POPlanLookup.CorporationID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "PONO", _request.Form["PaymentPlan_Edit_POPlanLookup.PONo"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ProductCode", _request.Form["PaymentPlan_Edit_POPlanLookup.ProductCode"]);
            SQLOperateHelper.SetEntityFiledValue(model, "MPN", _request.Form["PaymentPlan_Edit_POPlanLookup.MPN"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SupplierID", _request.Form["PaymentPlan_Edit_POPlanLookup.SupplierID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Qty", _request.Form["PaymentPlan_Edit_POPlanLookup.POQuantity"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyStandardCurrency", 1);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyPrice", _request.Form["PaymentPlan_Edit_POPlanLookup.Price"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyCost", _request.Form["PaymentPlan_Edit_POPlanLookup.TotalPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyRealCurrency", _request.Form["BuyRealCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyExchangeRate", _request.Form["BuyExchangeRate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyRealPrice", _request.Form["BuyRealPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyRealTotal", _request.Form["BuyRealTotal"]);
            SQLOperateHelper.SetEntityFiledValue(model, "VendorPaymentTypeID", _request.Form["PaymentPlan_Edit_POPlanLookup.VendorPaymentTypeID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsSupplierVATInvoice", _request.Form["IsSupplierVATInvoice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SupplierVATInvoice", _request.Form["SupplierVATInvoice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsPaySupplier", _request.Form["IsPaySupplier"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFee", _request.Form["OtherFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFeeRemark", _request.Form["OtherFeeRemark"]);
            //SQLOperateHelper.SetEntityFiledValue(model, "BuyerProportion", _request.Form["BuyerProportion"]);
            //SQLOperateHelper.SetEntityFiledValue(model, "BuyerPay", _request.Form["BuyerPay"]);
            SQLOperateHelper.SetEntityFiledValue(model, "PaymentPlanDate", _request.Form["PaymentPlanDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "State", _request.Form["State"]);

            SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
            SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
            return "OK";
        }

        /// <summary>
        /// 获取付供应商款计划列表
        /// </summary>
        private string GetPaymentPlan()
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
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.ProductCode",
                _request.Form["ProductCode"], ConditionEnm.AllLike));//这里alllike就是模糊查询了
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.PONO",
                _request.Form["PONO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("S.SCode",
                _request.Form["SCode"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.MPN",
                _request.Form["MPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.BuyRealCurrency",
                _request.Form["BuyRealCurrency"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.IsPaySupplier",
                _request.Form["IsPaySupplier"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.VendorPaymentTypeID",
                _request.Form["VendorPaymentTypeID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.IsSupplierVATInvoice",
                _request.Form["IsSupplierVATInvoice"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("E.EntryNo",
                _request.Form["EntryNo"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("E.IsApproved",
                _request.Form["IsInStock"], ConditionEnm.Equal));
            
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
                strTimeField = "P.PaymentPlanDate";
            }
            if (strTimeType == "4")
            {
                strTimeField = "E.InStockDate";
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
                DataSet ds = _bllPaymentPlan.GetPaymentPlanPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取付供应商款计划信息出现异常:" + ex.Message, ex);
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