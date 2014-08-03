using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using Semitron_OMS.Model.OMS;
using Semitron_OMS.Model.Common;
using System.Data;
using Newtonsoft.Json;
using Semitron_OMS.Common.Enum;
using Semitron_OMS.BLL.OMS;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// POPlanHandle 的摘要说明
    /// </summary>
    public class POPlanHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //采购计划对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(CustomerOrderDetailHandle));
        Semitron_OMS.BLL.OMS.POPlanBLL _bllPOPlan = new Semitron_OMS.BLL.OMS.POPlanBLL();
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
                    //获取采购计划列表
                    case "GetPOPlan":
                        context.Response.Write(GetPOPlan());
                        break;
                    //新增采购计划
                    case "AddPOPlan":
                        context.Response.Write(AddPOPlan());
                        break;
                    //修改采购计划
                    case "EditPOPlan":
                        context.Response.Write(EditPOPlan());
                        break;
                    //获取采购计划详细信息
                    case "GetPOPlanById":
                        context.Response.Write(GetPOPlanById());
                        break;
                    //取消采购计划
                    case "CancelPOPlan":
                        context.Response.Write(CommonFunction("CancelPOPlan", "取消采购计划"));
                        break;
                    //确认采购计划价格、数量
                    case "ConfirmPrice":
                        context.Response.Write(CommonFunction("ConfirmPrice", "确认采购计划价格、数量"));
                        break;
                    //QC确认
                    case "QCConfirm":
                        context.Response.Write(QCConfirm(context));
                        break;
                    //获取采购计划查找列表
                    case "GetPOPlanLookupList":
                        context.Response.Write(GetPOPlanLookupList());
                        break;
                    //自动生成采购计划
                    case "GeneratePOPlan":
                        context.Response.Write(GeneratePOPlan());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 自动生成采购计划
        /// </summary>
        private string GeneratePOPlan()
        {
            PageResult result = new PageResult();
            try
            {
                string strResult = this._bllPOPlan.GeneratePOPlan(_adminModel.AdminID);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Info = "自动生成采购计划成功！" + strResult;
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
                result.Info = "自动生成采购计划出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，自动生成采购计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取采购计划查找列表
        /// </summary>
        /// <returns></returns>
        private string GetPOPlanLookupList()
        {

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.PONO", _request.Form["PONo"], ConditionEnm.Equal));
            string strQueryType = DataUtility.GetPageFormValue(_request.Form["QueryType"], string.Empty);
            string strStartTime = DataUtility.GetPageFormValue(_request.Form["ArrivedBeginDate"], string.Empty);
            if (strStartTime != string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.ShipmentDate", strStartTime, ConditionEnm.GreaterThan));
            }
            string strEndTime = DataUtility.GetPageFormValue(_request.Form["ArrivedEndDate"], string.Empty);
            if (strEndTime != string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.ShipmentDate", strEndTime, ConditionEnm.LessThan));
            }
            //if (strStartTime == string.Empty && strEndTime == string.Empty)
            //{
            //    SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.ShipmentDate", DateTime.Now.ToString("yyyy-MM-dd"), ConditionEnm.GreaterThan));
            //}

            PageResult result = new PageResult();
            try
            {
                List<POPlanUnInStockModel> listModel = new List<POPlanUnInStockModel>();
                listModel = this._bllPOPlan.GetPOPlanLookupList(lstFilter, strQueryType);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取获取已到货未出库的采购计划出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取已到货未出库的采购计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取采购计划列表
        /// </summary>
        /// <returns></returns>
        private string GetPOPlan()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.PONO", _request.Form["PONO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.SupplierID", _request.Form["SupplierID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.IsPaySupplier", _request.Form["IsPaySupplier"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.State", _request.Form["State"], ConditionEnm.Equal));
            if (DataUtility.GetPageFormValue(_request.Form["State"], string.Empty) == string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.State", "P.State!=-100", ConditionEnm.None));
            }
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.CPN", _request.Form["CPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.MPN", _request.Form["MPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.VendorPaymentTypeID", _request.Form["VendorPaymentTypeID"], ConditionEnm.Equal));

            //如果传进来的是采购订单id，先找到采购订单号
            string strId = DataUtility.GetPageFormValue(_request.Form["POId"], string.Empty);
            if (!string.IsNullOrEmpty(strId))
            {
                POModel pModel = new POBLL().GetModel(strId.ToInt(0));
                if (pModel != null)
                {
                    SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.PONO", pModel.PONO, ConditionEnm.Equal));
                }
            }


            //是否分配查看所有供应商权限
            if (!PermissionUtility.IsExistDataSetPer(_adminModel.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_SUPPLIER_VIEW))
            {
                //只取出关联的供应商数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindSupplierBLL().GetDataTableByCache(_adminModel.AdminID);//使用缓存
                string strIds = "-1";
                dtBind.AsEnumerable().ForEach(r => strIds += "," + r["SupplierID"].ToString());
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.SupplierID", strIds, ConditionEnm.IN));
            }
            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "P.ShipmentDate";
            if (strTimeType == "0")
            {
                strTimeField = "P.VCD";
            }
            if (strTimeType == "2")
            {
                strTimeField = "P.CreateTime";
            }
            if (strTimeType == "3")
            {
                strTimeField = "P.UpdateTime";
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
                DataSet ds = _bllPOPlan.GetPOPlanPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取采购计划号信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(POPlanModel model)
        {

            PageResult result = new PageResult();
            if (_request.Form["PONO"] == null
                || _request.Form["CPN"] == null
                || _request.Form["MPN"] == null
                || _request.Form["MFG"] == null
                || _request.Form["DC"] == null
                || _request.Form["POQuantity"] == null
                || _request.Form["ArrivedQty"] == null
                || _request.Form["AlreadyQty"] == null
                || _request.Form["StockQty"] == null
                || _request.Form["BuyStandardCurrency"] == null
                || _request.Form["BuyPrice"] == null
                || _request.Form["BuyCost"] == null
                || _request.Form["ROHS"] == null
                || _request.Form["SupplierID"] == null
                || _request.Form["VCD"] == null
                || _request.Form["VendorPaymentTypeID"] == null
                || _request.Form["BuyExchangeRate"] == null
                || _request.Form["BuyRealCurrency"] == null
                || _request.Form["BuyRealPrice"] == null
                || _request.Form["OtherFee"] == null
                || _request.Form["OtherFeeRemark"] == null
                || _request.Form["IsPaySupplier"] == null
                || _request.Form["ShipmentDate"] == null)
            {

                return "系统错误,参数获取异常。";
            }

            SQLOperateHelper.SetEntityFiledValue(model, "PONO", _request.Form["PONO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CPN", _request.Form["CPN"]);
            SQLOperateHelper.SetEntityFiledValue(model, "MPN", _request.Form["MPN"]);
            SQLOperateHelper.SetEntityFiledValue(model, "MFG", _request.Form["MFG"]);
            SQLOperateHelper.SetEntityFiledValue(model, "DC", _request.Form["DC"]);
            SQLOperateHelper.SetEntityFiledValue(model, "POQuantity", _request.Form["POQuantity"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ArrivedQty", _request.Form["ArrivedQty"]);
            SQLOperateHelper.SetEntityFiledValue(model, "AlreadyQty", _request.Form["AlreadyQty"]);
            SQLOperateHelper.SetEntityFiledValue(model, "StockQty", _request.Form["StockQty"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyStandardCurrency", _request.Form["BuyStandardCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyPrice", _request.Form["BuyPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyCost", _request.Form["BuyCost"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ROHS", _request.Form["ROHS"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SupplierID", _request.Form["SupplierID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "VCD", _request.Form["VCD"]);
            SQLOperateHelper.SetEntityFiledValue(model, "VendorPaymentTypeID", _request.Form["VendorPaymentTypeID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyExchangeRate", _request.Form["BuyExchangeRate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyRealCurrency", _request.Form["BuyRealCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BuyRealPrice", _request.Form["BuyRealPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFee", _request.Form["OtherFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFeeRemark", _request.Form["OtherFeeRemark"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsPaySupplier", _request.Form["IsPaySupplier"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ShipmentDate", _request.Form["ShipmentDate"]);
            return "OK";
        }

        /// <summary>
        /// 新增采购计划
        /// </summary>
        /// <returns></returns>
        private string AddPOPlan()
        {
            PageResult result = new PageResult();
            POPlanModel model = new POPlanModel();
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
                string strResult = this._bllPOPlan.ValidateAndAdd(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "新增采购计划成功！";
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
                result.Info = "新增采购计划出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增采购计划出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改采购计划
        /// </summary>
        /// <returns></returns>
        private string EditPOPlan()
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
                POPlanModel model = this._bllPOPlan.GetModel(iId);

                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                SQLOperateHelper.SetEntityFiledValue(model, "State", (int)EnumPOPlanState.Added); //重置状态为草稿
                string strResult = this._bllPOPlan.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "编辑采购计划成功！";
                    if (!string.IsNullOrEmpty(model.PONO)
                        && !new POBLL().UpdateStateByPONO(model.PONO, _adminModel.Username, (int)EnumPOState.Added))
                    {
                        result.Info = "但更新对应采购订单状态失败,请重新操作！";
                    }
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
                result.Info = "编辑采购计划出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑采购计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 取消采购计划
        /// </summary>
        /// <returns></returns>
        private string CommonFunction(string strType, string strFuncMeth)
        {
            PageResult result = new PageResult();
            string strIdList = DataUtility.GetPageFormValue(_request.Form["IdList"], string.Empty);
            if (strIdList == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = string.Empty;
                switch (strType)
                {
                    case "CancelPOPlan":
                        strResult = this._bllPOPlan.ValidateAndCancelPOPlan(_adminModel.Username, strIdList);
                        break;
                    case "ConfirmPrice":
                        strResult = this._bllPOPlan.ValidateAndConfirmPrice(_adminModel.Username, strIdList);
                        break;
                }
                if (!strResult.StartsWith("ERROR"))
                {
                    result.State = 1;
                    result.Info = strResult;
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
                result.Info = strFuncMeth + "出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，" + strFuncMeth + "出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取采购计划详细信息
        /// </summary>
        /// <returns></returns>
        private string GetPOPlanById()
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
                POPlanModel model = this._bllPOPlan.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"AttachmentFiles\":\""
                    + new BLL.OMS.AttachmentBLL().GetUrlListByObj(ConstantValue.TableAttachment.ColumnObjType.POPlanQC,
                    iId.ToString()).Replace('/', '*') + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取采购计划详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取采购计划详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// QC确认进货
        /// </summary>
        private string QCConfirm(HttpContext context)
        {
            PageResult result = new PageResult();
            string strId = DataUtility.GetPageFormValue(_request.Form["Id"], string.Empty);
            string strArrivedQty = DataUtility.GetPageFormValue(_request.Form["ArrivedQty"], string.Empty);
            string strShipmentDate = DataUtility.GetPageFormValue(_request.Form["ShipmentDate"], string.Empty);

            if (strId == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = string.Empty;
                strResult = this._bllPOPlan.ValidateAndQCConfirm(_adminModel.Username, strId, strArrivedQty, strShipmentDate);
                if (!strResult.StartsWith("ERROR"))
                {
                    result.State = 1;
                    result.Info = strResult;
                    //保存附件列表
                    string strFilePath = context.Server.MapPath("../..");
                    string strFileUrl = DataUtility.GetPageFormValue(_request.Form["AttachmentFiles"], string.Empty).TrimEnd('|');
                    if (!new BLL.OMS.AttachmentBLL().BatchSaveFiles(strFilePath,
                        ConstantValue.TableAttachment.ColumnObjType.POPlanQC,
                        strId, strFileUrl, _adminModel.Username))
                    {
                        result.Info += "但保存上传附件失败，请重新上传。";
                    }
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
                result.Info = "QC确认进货出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，QC确认进货出现异常：" + ex.Message, ex);
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