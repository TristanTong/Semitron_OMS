using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Model.OMS;
using Newtonsoft.Json;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.BLL.OMS;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// POHandle 的摘要说明
    /// </summary>
    public class POHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //采购订单对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(CustomerOrderDetailHandle));
        Semitron_OMS.BLL.OMS.POBLL _bllPO = new Semitron_OMS.BLL.OMS.POBLL();
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
                    //获取采购订单列表
                    case "GetPO":
                        context.Response.Write(GetPO());
                        break;
                    //新增采购订单
                    case "AddPO":
                        context.Response.Write(AddPO());
                        break;
                    //修改采购订单
                    case "EditPO":
                        context.Response.Write(EditPO());
                        break;
                    //取消采购订单
                    case "DelPO":
                        context.Response.Write(DelPO());
                        break;
                    //获取采购订单详细信息
                    case "GetPOById":
                        context.Response.Write(GetPOById());
                        break;
                    //生成采购订单号
                    case "GeneratePONO":
                        context.Response.Write(GeneratePONO());
                        break;
                    ////通过采购订单Id获取相关联及待关联的客户订单列表树
                    //case "GetBindCOTree":
                    //    context.Response.Write(GetBindCOTree());
                    //    break;
                    ////关联客户订单
                    //case "BindCustiomerOrder":
                    //    context.Response.Write(BindCustiomerOrder());
                    //    break;
                    ////生成采购计划
                    //case "GeneratePOPlan":
                    //    context.Response.Write(GeneratePOPlan());
                    //    break;
                    ////获取采购订单关联的客户清单明细
                    //case "GetPOBindCustomerOrderDetail":
                    //    context.Response.Write(GetPOBindCustomerOrderDetail());
                    //    break;
                    //获取采购审核参考数据
                    case "GetConfirmSecondData":
                        context.Response.Write(GetConfirmSecondData());
                        break;
                    //执行采购审核
                    case "ConfirmSecond":
                        context.Response.Write(ConfirmSecond());
                        break;
                    //通过采购订单Id获取相关联采购计划列表树
                    case "GetBindPOPlanTree":
                        context.Response.Write(GetBindPOPlanTree());
                        break;
                    //关联采购计划
                    case "BindPOPlan":
                        context.Response.Write(BindPOPlan());
                        break;
                    //供应商审核
                    case "ConfirmSupplier":
                        context.Response.Write(ConfirmSupplier());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 供应商审核
        /// </summary>
        private string ConfirmSupplier()
        {
            PageResult result = new PageResult();
            int iPOId = -1;
            if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString(), out iPOId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = _bllPO.ConfirmSupplier(iPOId, _adminModel.Username);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Info = "供应商审核成功！";
                }
                else
                {
                    result.State = 0;
                    result.Info = "供应商审核失败！" + strResult;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "供应商审核出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，供应商审核出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取采购订单列表
        /// </summary>
        /// <returns></returns>
        private string GetPO()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("PONO", _request.Form["PONO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.SupplierID", _request.Form["SupplierID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.CorporationID", _request.Form["CorporationID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.State", _request.Form["State"], ConditionEnm.Equal));

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
            string strTimeField = "P.POOrderDate";
            if (strTimeType == "1")
            {
                strTimeField = "P.IssuedDate";
            }
            if (strTimeType == "2")
            {
                strTimeField = "P.DeliveryDate";
            }
            if (strTimeType == "3")
            {
                strTimeField = "P.CreateTime";
            }
            if (strTimeType == "4")
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
                DataSet ds = _bllPO.GetPOPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取采购订单号信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(POModel model)
        {

            PageResult result = new PageResult();
            if (_request.Form["PONO"] == null
                || _request.Form["InnerBuyer"] == null
                || _request.Form["SupplierID"] == null
                || _request.Form["IssuedDate"] == null
                || _request.Form["ContactPerson"] == null
                || _request.Form["Tel"] == null
                || _request.Form["BillManager"] == null
                || _request.Form["BillManagerTel"] == null
                || _request.Form["CorporationID"] == null
                || _request.Form["BillTo"] == null
                || _request.Form["DeliveryDate"] == null
                || _request.Form["Shipping"] == null
                || _request.Form["ShipManager"] == null
                || _request.Form["ShipManagerTel"] == null
                || _request.Form["PaymentTerms"] == null
                || _request.Form["ShipTo"] == null)
            {

                return "系统错误,参数获取异常。";
            }
            SQLOperateHelper.SetEntityFiledValue(model, "PONO", _request.Form["PONO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "POOrderDate", _request.Form["IssuedDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "InnerBuyer", _request.Form["InnerBuyer"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SupplierID", _request.Form["SupplierID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IssuedDate", _request.Form["IssuedDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ContactPerson", _request.Form["ContactPerson"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Tel", _request.Form["Tel"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BillManager", _request.Form["BillManager"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BillManagerTel", _request.Form["BillManagerTel"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CorporationID", _request.Form["CorporationID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "BillTo", _request.Form["BillTo"]);
            SQLOperateHelper.SetEntityFiledValue(model, "DeliveryDate", _request.Form["DeliveryDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Shipping", _request.Form["Shipping"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ShipManager", _request.Form["ShipManager"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ShipManagerTel", _request.Form["ShipManagerTel"]);
            SQLOperateHelper.SetEntityFiledValue(model, "PaymentTerms", _request.Form["PaymentTerms"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ShipTo", _request.Form["ShipTo"]);
            return "OK";
        }

        /// <summary>
        /// 新增采购订单
        /// </summary>
        /// <returns></returns>
        private string AddPO()
        {
            PageResult result = new PageResult();
            POModel model = new POModel();
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
                model.PONO = JsonConvert.DeserializeObject<PageResult>(this.GeneratePONO()).Remark;
                string strResult = this._bllPO.ValidateAndAdd(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "新增采购订单成功！";
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
                result.Info = "新增采购订单出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增采购订单出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改采购订单
        /// </summary>
        /// <returns></returns>
        private string EditPO()
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
                POModel model = this._bllPO.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllPO.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "编辑采购订单成功！";
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
                result.Info = "编辑采购订单出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑采购订单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 取消采购订单
        /// </summary>
        /// <returns></returns>
        private string DelPO()
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
                string strResult = this._bllPO.ValidateAndDelPO(_adminModel.Username, iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "取消采购订单成功。";
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
                result.Info = "取消户订单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，取消采购订单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取采购订单详细信息
        /// </summary>
        /// <returns></returns>
        private string GetPOById()
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
                POModel model = this._bllPO.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                int iCount = new POPlanBLL().GetModelList("PONO='" + model.PONO + "' AND State!=-100").Count;
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"POPlanNum\":" + iCount.ToString() + "}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取采购订单详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取采购订单详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成采购订单号
        /// </summary>
        /// <returns></returns>
        private string GeneratePONO()
        {
            PageResult result = new PageResult();
            result.Info = "无法生成采购订单号！";
            try
            {
                int iMax = this._bllPO.GetMaxId();
                string strNow = DateTime.Now.ToString("yyyyMMdd");
                POModel model = new POModel();
                string strNum = "10";
                //首次生成
                if (iMax == 1)
                {
                    model.PONO = "SEPO" + strNow + strNum;
                }
                else
                {
                    model = this._bllPO.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.PONO))
                {
                    if (model.PONO.Substring(4, 8) == strNow)
                    {
                        strNum = (int.Parse(model.PONO.Remove(0, 12)) + 1).ToString().PadLeft(2, '0');
                    }

                    result.Remark = "SEPO" + strNow + strNum;
                    result.State = 1;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成采购订单号出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成采购订单号出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 通过采购订单Id获取相关联采购计划列表树
        /// </summary>
        /// <returns></returns>
        private string GetBindPOPlanTree()
        {
            PageResult result = new PageResult();
            try
            {
                int iPOId;
                if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString().Trim(), out iPOId))
                {
                    result.Info = "系统错误,参数获取异常。";
                    return result.ToString();
                }
                DataTable dt = _bllPO.GetBindPOPlanByPoId(iPOId, _adminModel.AdminID);
                if (dt == null)
                {
                    result.Info = "获取采购计划树失败。";
                    return result.ToString();
                }
                return JsonZTree.ForTreeList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取采购计划树出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取采购计划树出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 关联采购计划
        /// </summary>
        private string BindPOPlan()
        {
            PageResult result = new PageResult();
            int iPOId = -1;
            string strPOPlanIdList = DataUtility.GetPageFormValue(_request.Form["POPlanIdList"], string.Empty);
            if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString(), out iPOId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                if (_bllPO.BindPOPlan(iPOId, strPOPlanIdList, _adminModel.Username))
                {
                    result.State = 1;
                    result.Info = "关联采购计划成功！";
                }
                else
                {
                    result.State = 0;
                    result.Info = "关联采购计划失败！";
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "关联采购计划出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，关联采购计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /*/// <summary>
        /// 通过采购订单Id获取相关联及待关联的客户订单列表树
        /// </summary>
        /// <returns></returns>
        private string GetBindCOTree()
        {
            PageResult result = new PageResult();
            try
            {
                Semitron_OMS.BLL.OMS.POBindCustomerOrderBLL _bllBind = new BLL.OMS.POBindCustomerOrderBLL();
                int iPOId;
                if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString().Trim(), out iPOId))
                {
                    result.Info = "系统错误,参数获取异常。";
                    return result.ToString();
                }
                DataTable dt = _bllBind.GetCustomerOrderByPoId(iPOId, _adminModel.AdminID);
                if (dt == null)
                {
                    result.Info = "获取客户订单树失败。";
                    return result.ToString();
                }
                return JsonZTree.ForTreeList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取客户订单树出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取客户订单树出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 关联客户订单
        /// </summary>
        private string BindCustiomerOrder()
        {
            PageResult result = new PageResult();
            int iPOId = -1;
            string strCustomerOrderIdList = DataUtility.GetPageFormValue(_request.Form["CustomerOrderIdList"], string.Empty);
            if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString(), out iPOId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                if (new Semitron_OMS.BLL.OMS.POBindCustomerOrderBLL().BindCustiomerOrder(iPOId, strCustomerOrderIdList, _adminModel.Username))
                {
                    result.State = 1;
                    result.Info = "关联客户订单成功！";
                }
                else
                {
                    result.State = 0;
                    result.Info = "关联客户订单失败！";
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "关联客户订单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，关联客户订单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成采购计划
        /// </summary>
        /// <returns></returns>
        private string GeneratePOPlan()
        {
            PageResult result = new PageResult();
            int iPOId = -1;
            if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString(), out iPOId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            string strCOIds = DataUtility.GetPageFormValue(_request.Form["COIds"], string.Empty);
            try
            {
                string strResult = this._bllPO.GeneratePOPlan(iPOId, strCOIds, this._adminModel.Username);
                result.Info = strResult;
                if (!strResult.StartsWith("ERROR"))
                {
                    result.State = 1;
                }
                else
                {
                    result.State = 0;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成采购计划出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成采购计划出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取采购订单关联的客户清单明细
        /// </summary>
        /// <returns></returns>
        private string GetPOBindCustomerOrderDetail()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("POId", _request.Form["POId"], ConditionEnm.Equal));


            searchInfo.ConditionFilter = lstFilter;

            DataTable dt = new DataTable();
            try
            {
                DataSet ds = _bllPO.GetPOBindCustomerOrderDetail(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取采购订单关联的客户清单明细出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }*/

        /// <summary>
        /// 执行采购审核
        /// </summary>
        /// <returns></returns>
        private string ConfirmSecond()
        {
            PageResult result = new PageResult();
            int iId = -1;
            int iType = -1;
            if (_request.Form["POId"] == null || !int.TryParse(_request.Form["POId"].ToString(), out iId)
                || _request.Form["Type"] == null || !int.TryParse(_request.Form["Type"].ToString(), out iType))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = this._bllPO.ConfirmSecond(_adminModel.Username, iId, iType);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "操作成功完成。";
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
                result.Info = "执行采购审核出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，执行采购审核出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取产品采购分析列表数据
        /// </summary>
        /// <returns></returns>
        private string GetConfirmSecondData()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("POId", _request.Form["POId"], ConditionEnm.Equal));


            searchInfo.ConditionFilter = lstFilter;

            DataTable dt = new DataTable();
            try
            {
                DataSet ds = _bllPO.GetConfirmSecondData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取产品采购分析列表数据出现异常:" + ex.Message, ex);
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