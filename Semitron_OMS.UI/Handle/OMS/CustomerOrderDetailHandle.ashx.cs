using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Common;
using Semitron_OMS.Model.OMS;
using Newtonsoft.Json;
using System.Data;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// CustomerOrderDetailDetailHandle 的摘要说明
    /// </summary>
    public class CustomerOrderDetailHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //产品请单记录对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(CustomerOrderDetailHandle));
        Semitron_OMS.BLL.OMS.CustomerOrderDetailBLL _bllCustomerOrderDetail = new Semitron_OMS.BLL.OMS.CustomerOrderDetailBLL();
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
                    //获取产品请单记录列表
                    case "GetCustomerOrderDetail":
                        context.Response.Write(GetCustomerOrderDetail());
                        break;
                    //新增产品请单记录
                    case "AddCustomerOrderDetail":
                        context.Response.Write(AddCustomerOrderDetail());
                        break;
                    //修改产品请单记录
                    case "EditCustomerOrderDetail":
                        context.Response.Write(EditCustomerOrderDetail());
                        break;
                    //删除产品请单记录
                    case "DelCustomerOrderDetail":
                        context.Response.Write(DelCustomerOrderDetail());
                        break;
                    //获取产品请单记录详细信息
                    case "GetCustomerOrderDetailById":
                        context.Response.Write(GetCustomerOrderDetailById());
                        break;
                    //获取待出货计划的产品清单列表
                    case "GetCustomerOrderDetailUnOutStockList":
                        context.Response.Write(GetCustomerOrderDetailUnOutStockList());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取待出货计划的产品清单列表
        /// </summary>
        private string GetCustomerOrderDetailUnOutStockList()
        {
            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.InnerOrderNo", _request.Form["InnerOrderNo"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.CCode", _request.Form["CustomerCode"], ConditionEnm.Equal));
            string strStartTime = DataUtility.GetPageFormValue(_request.Form["CustOrderDateBegin"], string.Empty);
            if (strStartTime != string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.CustOrderDate", strStartTime, ConditionEnm.GreaterThan));
            }
            string strEndTime = DataUtility.GetPageFormValue(_request.Form["CustOrderDateEnd"], string.Empty);
            if (strEndTime != string.Empty)
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.CustOrderDate", strEndTime, ConditionEnm.LessThan));
            }

            PageResult result = new PageResult();
            try
            {
                List<CustomerOrderDetailUnOutStockModel> listModel = new List<CustomerOrderDetailUnOutStockModel>();
                listModel = this._bllCustomerOrderDetail.GetCustomerOrderDetailUnOutStockList(lstFilter);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取待出货计划的产品清单列表出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取待出货计划的产品清单列表出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取产品请单记录列表
        /// </summary>
        /// <returns></returns>
        private string GetCustomerOrderDetail()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.InnerOrderNO", _request.Form["InnerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.CustomerOrderNO", _request.Form["CustomerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.CustomerID", _request.Form["CustomerID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.MPN", _request.Form["MPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.CPN", _request.Form["CPN"], ConditionEnm.AllLike));
            string strAvailFlag = DataUtility.GetPageFormValue(_request.Form["AvailFlag"], string.Empty);
            if (string.IsNullOrEmpty(strAvailFlag))
            {
                strAvailFlag = "1";
            }
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.AvailFlag", strAvailFlag, ConditionEnm.Equal));

            //是否分配查看所有客户权限
            if (!PermissionUtility.IsExistDataSetPer(_adminModel.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_CUSTOMER_VIEW))
            {
                //只取出关联的客户数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindCustomerBLL().GetDataTableByCache(_adminModel.AdminID);//使用缓存
                string strIds = "-1";
                dtBind.AsEnumerable().ForEach(r => strIds += "," + r["CustomerID"].ToString());
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("O.CustomerID", strIds, ConditionEnm.IN));
            }

            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "D.CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "D.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "D.UpdateTime";
            }
            if (strTimeType == "3")
            {
                strTimeField = "D.ShipmentDate";
            }
            if (strTimeType == "4")
            {
                strTimeField = "D.CustomerInStockDate";
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
            //if (strStartTime == string.Empty && strEndTime == string.Empty)
            //{
            //    SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter(strTimeField, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ConditionEnm.GreaterThan));
            //}

            searchInfo.ConditionFilter = lstFilter;

            DataTable dt = new DataTable();
            try
            {
                DataSet ds = _bllCustomerOrderDetail.GetCustomerOrderDetailPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取产品请单记录信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(CustomerOrderDetailModel model)
        {
            PageResult result = new PageResult();
            if (_request.Form["InnerOrderNO"] == null
                || _request.Form["CPN"] == null
                || _request.Form["MPN"] == null
                || _request.Form["MFG"] == null
                || _request.Form["DC"] == null
                || _request.Form["CRD"] == null
                || _request.Form["CustQuantity"] == null
                || _request.Form["ROHS"] == null
                || _request.Form["SaleExchangeRate"] == null
                || _request.Form["SaleRealCurrency"] == null
                || _request.Form["SaleRealPrice"] == null
                || _request.Form["SaleStandardCurrency"] == null
                || _request.Form["SalePrice"] == null
                || _request.Form["OtherFee"] == null
                || _request.Form["OtherFeeRemark"] == null)
            {

                return "系统错误,参数获取异常。";
            }
            SQLOperateHelper.SetEntityFiledValue(model, "InnerOrderNO", _request.Form["InnerOrderNO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CPN", _request.Form["CPN"]);
            SQLOperateHelper.SetEntityFiledValue(model, "MPN", _request.Form["MPN"]);
            SQLOperateHelper.SetEntityFiledValue(model, "MFG", _request.Form["MFG"]);
            SQLOperateHelper.SetEntityFiledValue(model, "DC", _request.Form["DC"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CRD", _request.Form["CRD"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustQuantity", _request.Form["CustQuantity"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ROHS", _request.Form["ROHS"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleExchangeRate", _request.Form["SaleExchangeRate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleRealCurrency", _request.Form["SaleRealCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleRealPrice", _request.Form["SaleRealPrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SaleStandardCurrency", _request.Form["SaleStandardCurrency"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SalePrice", _request.Form["SalePrice"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFee", _request.Form["OtherFee"]);
            SQLOperateHelper.SetEntityFiledValue(model, "OtherFeeRemark", _request.Form["OtherFeeRemark"]);
            return "OK";
        }

        /// <summary>
        /// 新增产品请单记录
        /// </summary>
        /// <returns></returns>
        private string AddCustomerOrderDetail()
        {
            PageResult result = new PageResult();
            CustomerOrderDetailModel model = new CustomerOrderDetailModel();
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
                string strResult = this._bllCustomerOrderDetail.ValidateAndAdd(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "新增产品请单记录成功！";
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
                result.Info = "新增产品请单记录出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" + _request.UserHostAddress + "，新增产品请单记录出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改产品请单记录
        /// </summary>
        /// <returns></returns>
        private string EditCustomerOrderDetail()
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
                CustomerOrderDetailModel model = this._bllCustomerOrderDetail.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllCustomerOrderDetail.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "编辑产品请单记录成功！";
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
                result.Info = "编辑产品请单记录出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑产品请单记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除产品请单记录
        /// </summary>
        /// <returns></returns>
        private string DelCustomerOrderDetail()
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
                string strResult = this._bllCustomerOrderDetail.ValidateAndDelCustomerOrderDetail(_adminModel.Username, iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除产品请单记录成功。";
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
                result.Info = "删除户订单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除产品请单记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取产品请单记录详细信息
        /// </summary>
        /// <returns></returns>
        private string GetCustomerOrderDetailById()
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
                CustomerOrderDetailModel model = this._bllCustomerOrderDetail.GetModel(iId);
                return JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "删除户订单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取产品请单记录详细信息出现异常：" + ex.Message, ex);
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