using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.Model.OMS;
using Semitron_OMS.Model.Common;
using Newtonsoft.Json;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// OrderFeeHandle 的摘要说明
    /// </summary>
    public class OrderFeeHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //订单计费对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(OrderFeeHandle));
        Semitron_OMS.BLL.OMS.OrderFeeBLL _bllOrderFee = new Semitron_OMS.BLL.OMS.OrderFeeBLL();
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
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //获取订单计费列表
                    case "GetOrderFee":
                        context.Response.Write(GetOrderFee());
                        break;
                    //新增订单计费
                    case "AddOrderFee":
                        context.Response.Write(AddOrderFee());
                        break;
                    //修改订单计费
                    case "EditOrderFee":
                        context.Response.Write(EditOrderFee());
                        break;
                    //删除订单计费
                    case "DelOrderFee":
                        context.Response.Write(DelOrderFee());
                        break;
                    //获取订单计费详细信息
                    case "GetOrderFeeById":
                        context.Response.Write(GetOrderFeeById());
                        break;
                    //上传附件
                    case "UploadFile":
                        context.Response.Write(UploadFile(context));
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取订单计费列表
        /// </summary>
        /// <returns></returns>
        private string GetOrderFee()
        {
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();

            //返回查询总数
            int o_RowsCount = 0;

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            //获取表格提交参数
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(_request.Form["page"], 1));
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
            //CustomerID,SupplierID,InnerOrderNO,PONO,CPN,MPN
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CO.CustomerID", _request.Form["CustomerID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.SupplierID", _request.Form["SupplierID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.AvailFlag", _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.IsCustomerPay", _request.Form["IsCustomerPay"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.IsPaySupplier", _request.Form["IsPaySupplier"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.InnerOrderNO", _request.Form["InnerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.PONO", _request.Form["PONO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.CPN", _request.Form["CPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.MPN", _request.Form["MPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CO.CustomerOrderNO", _request.Form["CustomerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CO.CorporationID", _request.Form["CorporationID"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.IsCustomerVATInvoice", _request.Form["IsCustomerVATInvoice"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.IsSupplierVATInvoice", _request.Form["IsSupplierVATInvoice"], ConditionEnm.Equal));
            //是否分配查看所有供应商权限
            if (!PermissionUtility.IsExistDataSetPer(_adminModel.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_SUPPLIER_VIEW))
            {
                //只取出关联的供应商数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindSupplierBLL().GetDataTableByCache(_adminModel.AdminID);//使用缓存
                string strIds = "-1";
                dtBind.AsEnumerable().ForEach(r => strIds += "," + r["SupplierID"].ToString());
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.SupplierID", strIds, ConditionEnm.IN));
            }
            //是否分配查看所有客户权限
            if (!PermissionUtility.IsExistDataSetPer(_adminModel.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_CUSTOMER_VIEW))
            {
                //只取出关联的客户数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindCustomerBLL().GetDataTableByCache(_adminModel.AdminID);//使用缓存
                string strIds = "-1";
                dtBind.AsEnumerable().ForEach(r => strIds += "," + r["CustomerID"].ToString());
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CO.CustomerID", strIds, ConditionEnm.IN));
            }
            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "O.CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "O.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "O.UpdateTime";
            }
            if (strTimeType == "3")
            {
                strTimeField = "O.FeeBackDate";
            }
            if (strTimeType == "4")
            {
                strTimeField = "C.CustomerInStockDate";
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
                DataSet ds = _bllOrderFee.GetOrderFeePageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取订单计费号信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(OrderFeeModel model)
        {
            PageResult result = new PageResult();
            if (_request.Form["TotalFeeCurrencyUnit"] == null
                || _request.Form["CustomerFeeIn"] == null
                || _request.Form["CustomerRealPayFee"] == null
                || _request.Form["IncomeRate"] == null
                || _request.Form["IncomeStandardCurrency"] == null
                || _request.Form["StandardCustomerFeeIn"] == null
                || _request.Form["RealInCurrencyUnit"] == null
                || _request.Form["CustomerRealPayFee"] == null
                || _request.Form["StandardRealInCurrencyUnit"] == null
                || _request.Form["StandardCustomerRealPayFee"] == null
                || _request.Form["StandardIncomeRealDiff"] == null
                || _request.Form["GrossProfits"] == null
                || _request.Form["NetProfit"] == null
                || _request.Form["ProfitMargin"] == null
                || _request.Form["OtherFee"] == null
                || _request.Form["OtherFeeRemark"] == null
                || _request.Form["OtherRemark"] == null
                || _request.Form["IsCustomerVATInvoice"] == null
                || _request.Form["CustomerVATInvoiceNo"] == null
                || _request.Form["IsSupplierVATInvoice"] == null
                || _request.Form["SupplierVATInvoice"] == null
                || _request.Form["TraceNumber"] == null
                || _request.Form["FeeBackDate"] == null
                || _request.Form["SalesManProportion"] == null
                || _request.Form["SalesManPay"] == null
                || _request.Form["BuyerProportion"] == null
                || _request.Form["BuyerPay"] == null)
            {

                return "系统错误,参数获取异常。";
            }

            SQLOperateHelper.SetEntityFiledValue(model, "TotalFeeCurrencyUnit", _request.Form["TotalFeeCurrencyUnit"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerFeeIn", _request.Form["CustomerFeeIn"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerRealPayFee", _request.Form["CustomerRealPayFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IncomeRate", _request.Form["IncomeRate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IncomeStandardCurrency", _request.Form["IncomeStandardCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "StandardCustomerFeeIn", _request.Form["StandardCustomerFeeIn"]);
            SQLOperateHelper.SetEntityFiledValue(model, "RealInCurrencyUnit", _request.Form["RealInCurrencyUnit"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerRealPayFee", _request.Form["CustomerRealPayFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "StandardRealInCurrencyUnit", _request.Form["StandardRealInCurrencyUnit"]);
            SQLOperateHelper.SetEntityFiledValue(model, "StandardCustomerRealPayFee", _request.Form["StandardCustomerRealPayFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "StandardIncomeRealDiff", _request.Form["StandardIncomeRealDiff"]);
            SQLOperateHelper.SetEntityFiledValue(model, "GrossProfits", _request.Form["GrossProfits"]);
            SQLOperateHelper.SetEntityFiledValue(model, "NetProfit", _request.Form["NetProfit"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ProfitMargin", _request.Form["ProfitMargin"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFee", _request.Form["OtherFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFeeRemark", _request.Form["OtherFeeRemark"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherRemark", _request.Form["OtherRemark"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsCustomerVATInvoice", _request.Form["IsCustomerVATInvoice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerVATInvoiceNo", _request.Form["CustomerVATInvoiceNo"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsSupplierVATInvoice", _request.Form["IsSupplierVATInvoice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SupplierVATInvoice", _request.Form["SupplierVATInvoice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "TraceNumber", _request.Form["TraceNumber"]);
            SQLOperateHelper.SetEntityFiledValue(model, "FeeBackDate", _request.Form["FeeBackDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SalesManProportion", _request.Form["SalesManProportion"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SalesManPay", _request.Form["SalesManPay"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyerProportion", _request.Form["BuyerProportion"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyerPay", _request.Form["BuyerPay"]);

            return "OK";
        }

        /// <summary>
        /// 新增订单计费
        /// </summary>
        /// <returns></returns>
        private string AddOrderFee()
        {
            PageResult result = new PageResult();
            OrderFeeModel model = new OrderFeeModel();
            string strGetResult = this.GetNewModel(model);
            if (strGetResult != "OK")
            {
                result.State = 0;
                result.Info = strGetResult;
                return result.ToString();
            }
            try
            {
                SQLOperateHelper.SetEntityFiledValue(model, "CreateUser", _adminModel.Username);
                string strResult = this._bllOrderFee.ValidateAndAdd(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "新增订单计费成功！";
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
                result.Info = "新增订单计费出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增订单计费出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改订单计费
        /// </summary>
        /// <returns></returns>
        private string EditOrderFee()
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
                OrderFeeModel model = this._bllOrderFee.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllOrderFee.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "编辑订单计费成功！";

                    DateTime dtTemp = DateTime.MinValue;
                    DateTime? dtCustomerInStockDate = null;
                    if (!DateTime.TryParse(DataUtility.GetPageFormValue(_request.Form["CustomerInStockDate"], DateTime.MinValue.ToString()), out dtTemp))
                    {
                        dtTemp = DateTime.MinValue;
                    }
                    if (dtTemp == DateTime.MinValue)
                    {
                        dtCustomerInStockDate = null;
                    }
                    else
                    {
                        dtCustomerInStockDate = (DateTime)dtTemp;
                    }
                    new BLL.OMS.CustomerOrderDetailBLL().SetCustomerOrderDetailItems(model.CustomerOrderDetailID, bool.Parse(DataUtility.GetPageFormValue(_request.Form["IsCustomerPay"], "false")), dtCustomerInStockDate);

                    new BLL.OMS.CustomerOrderBLL().SetItemsByCustomerOrderDetailID(model.CustomerOrderDetailID, int.Parse(DataUtility.GetPageFormValue(_request.Form["PaymentTypeID"], -1)));
                    new BLL.OMS.POPlanBLL().SetPOPlanItems(model.POPlanID, bool.Parse(DataUtility.GetPageFormValue(_request.Form["IsPaySupplier"], "false")), int.Parse(DataUtility.GetPageFormValue(_request.Form["VendorPaymentTypeID"], -1)));
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
                result.Info = "编辑订单计费出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑订单计费出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除订单计费
        /// </summary>
        /// <returns></returns>
        private string DelOrderFee()
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
                string strResult = this._bllOrderFee.ValidateAndDelOrderFee(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除订单计费成功。";
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
                result.Info = "删除订单计费出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除订单计费出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取订单计费详细信息
        /// </summary>
        /// <returns></returns>
        private string GetOrderFeeById()
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
                OrderFeeDisplayModel model = this._bllOrderFee.GetDisplayModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"AttachmentFiles\":\""
                    + new BLL.OMS.AttachmentBLL().GetUrlListByObj(ConstantValue.TableAttachment.ColumnObjType.OrderFee,
                    iId.ToString()).Replace('/', '*') + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取订单计费详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取订单计费详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        private string UploadFile(HttpContext context)
        {
            PageResult result = new PageResult();
            string strId = DataUtility.GetPageFormValue(_request.Form["Id"], string.Empty);
            if (strId == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                //保存附件列表
                string strFilePath = context.Server.MapPath("../..");
                string strFileUrl = DataUtility.GetPageFormValue(_request.Form["AttachmentFiles"], string.Empty).TrimEnd('|');
                if (new BLL.OMS.AttachmentBLL().BatchSaveFiles(strFilePath,
                    ConstantValue.TableAttachment.ColumnObjType.OrderFee,
                    strId, strFileUrl, _adminModel.Username))
                {
                    result.State = 1;
                    result.Info = "保存上传附件成功";
                }

                else
                {
                    result.State = 0;
                    result.Info = "保存上传附件失败，请重新上传。";
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "上传财务附件出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，上传财务附件出现异常：" + ex.Message, ex);
            }
            return result.ToString();
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