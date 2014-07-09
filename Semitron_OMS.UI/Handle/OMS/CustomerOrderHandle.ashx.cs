using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.Model.OMS;
using Semitron_OMS.Model.Common;
using Newtonsoft.Json;
using Semitron_OMS.Common.Enum;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// CustomerOrderHandle 的摘要说明
    /// </summary>
    public class CustomerOrderHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //客户订单对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(CustomerOrderHandle));
        Semitron_OMS.BLL.OMS.CustomerOrderBLL _bllCustomerOrder = new Semitron_OMS.BLL.OMS.CustomerOrderBLL();
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
                    //获取客户订单列表
                    case "GetCustomerOrder":
                        context.Response.Write(GetCustomerOrder());
                        break;
                    //新增客户订单
                    case "AddCustomerOrder":
                        context.Response.Write(AddCustomerOrder(context));
                        break;
                    //修改客户订单
                    case "EditCustomerOrder":
                        context.Response.Write(EditCustomerOrder(context));
                        break;
                    //取消客户订单
                    case "CancelCustomerOrder":
                        context.Response.Write(CancelCustomerOrder());
                        break;
                    //获取客户订单详细信息
                    case "GetCustomerOrderById":
                        context.Response.Write(GetCustomerOrderById());
                        break;
                    //生成内部订单号
                    case "GenerateInnerOrderNO":
                        context.Response.Write(GenerateInnerOrderNO());
                        break;
                    //获取销售审核参考数据
                    case "GetConfirmFirstData":
                        context.Response.Write(GetConfirmFirstData());
                        break;
                    //执行销售审核
                    case "ConfirmFirst":
                        context.Response.Write(ConfirmFirst());
                        break;
                    //客户订单出货
                    case "CustomerOrderOutStock":
                        context.Response.Write(CustomerOrderOutStock());
                        break;
                    //指定公司采购
                    case "AssignBuyer":
                        context.Response.Write(AssignBuyer());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 指定公司采购
        /// </summary>
        /// <returns></returns>
        private string AssignBuyer()
        {
            PageResult result = new PageResult();
            int iId = -1;
            if (_request.Form["COId"] == null
                || _request.Form["AssignToInnerBuyer"] == null
                || !int.TryParse(_request.Form["COId"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                CustomerOrderModel model = this._bllCustomerOrder.GetModel(iId);
                SQLOperateHelper.SetEntityFiledValue(model, "AssignToInnerBuyer", _request.Form["AssignToInnerBuyer"]);
                SQLOperateHelper.SetEntityFiledValue(model, "State", EnumCustomerOrderState.POing);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllCustomerOrder.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "指定公司采购成功！";
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
                result.Info = "指定公司采购出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username
                    + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress
                    + "，指定公司采购出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取客户订单列表
        /// </summary>
        /// <returns></returns>
        private string GetCustomerOrder()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.InnerOrderNO", _request.Form["InnerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.CustomerOrderNO", _request.Form["CustomerOrderNO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.CustomerID", _request.Form["CustomerID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.State", _request.Form["State"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.PaymentTypeID", _request.Form["PaymentTypeID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.AssignToInnerBuyer", _request.Form["AssignToInnerBuyer"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.InnerSalesMan", _request.Form["InnerSalesMan"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.CustomerBuyer", _request.Form["CustomerBuyer"], ConditionEnm.AllLike));

            //是否分配查看所有客户权限
            if (!PermissionUtility.IsExistDataSetPer(_adminModel.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_CUSTOMER_VIEW))
            {
                //只取出关联的客户数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindCustomerBLL().GetDataTableByCache(_adminModel.AdminID);//使用缓存
                string strIds = "-1";
                dtBind.AsEnumerable().ForEach(r => strIds += "," + r["CustomerID"].ToString());
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("C.CustomerID", strIds, ConditionEnm.IN));
            }
            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "C.CustOrderDate";
            if (strTimeType == "1")
            {
                strTimeField = "C.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "C.UpdateTime";
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

                DataSet ds = _bllCustomerOrder.GetCustomerOrderPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取客户订单信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(CustomerOrderModel model)
        {
            PageResult result = new PageResult();
            if (_request.Form["InnerOrderNO"] == null
                || _request.Form["CustomerOrderNO"] == null
                || _request.Form["CustomerID"] == null
                || _request.Form["CustOrderDate"] == null
                || _request.Form["CustomerBuyer"] == null
                || _request.Form["InnerSalesMan"] == null
                || _request.Form["CorporationID"] == null
                || _request.Form["PaymentTypeID"] == null
                || _request.Form["AssignToInnerBuyer"] == null)
            {

                return "系统错误,参数获取异常。";
            }
            SQLOperateHelper.SetEntityFiledValue(model, "InnerOrderNO", _request.Form["InnerOrderNO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerOrderNO", _request.Form["CustomerOrderNO"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerID", _request.Form["CustomerID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustOrderDate", _request.Form["CustOrderDate"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CustomerBuyer", _request.Form["CustomerBuyer"]);
            SQLOperateHelper.SetEntityFiledValue(model, "InnerSalesMan", _request.Form["InnerSalesMan"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CorporationID", _request.Form["CorporationID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "PaymentTypeID", _request.Form["PaymentTypeID"]);
            SQLOperateHelper.SetEntityFiledValue(model, "AssignToInnerBuyer", _request.Form["AssignToInnerBuyer"]);
            return "OK";
        }

        /// <summary>
        /// 新增客户订单
        /// </summary>
        /// <returns></returns>
        private string AddCustomerOrder(HttpContext context)
        {
            PageResult result = new PageResult();
            CustomerOrderModel model = new CustomerOrderModel();
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
                model.InnerOrderNO = JsonConvert.DeserializeObject<PageResult>(this.GenerateInnerOrderNO()).Remark;
                string strResult = this._bllCustomerOrder.ValidateAndAdd(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "新增成功，您的客户订单已进入待采购环节！";
                    //保存附件列表
                    string strFilePath = context.Server.MapPath("../..");
                    string strFileUrl = DataUtility.GetPageFormValue(_request.Form["AttachmentFiles"], string.Empty).TrimEnd('|');
                    if (!new BLL.OMS.AttachmentBLL().BatchSaveFiles(strFilePath,
                        ConstantValue.TableAttachment.ColumnObjType.CustomerOrder,
                        model.CustomerOrderNO, strFileUrl, _adminModel.Username))
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
                result.Info = "新增客户订单出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" + _request.UserHostAddress + "，新增客户订单出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改客户订单
        /// </summary>
        /// <returns></returns>
        private string EditCustomerOrder(HttpContext context)
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
                CustomerOrderModel model = this._bllCustomerOrder.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "State", EnumCustomerOrderState.Added);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllCustomerOrder.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "编辑客户订单成功！";
                    //保存附件列表
                    string strFilePath = context.Server.MapPath("../..");
                    string strFileUrl = DataUtility.GetPageFormValue(_request.Form["AttachmentFiles"], string.Empty).TrimEnd('|');
                    if (!new BLL.OMS.AttachmentBLL().BatchSaveFiles(strFilePath,
                        ConstantValue.TableAttachment.ColumnObjType.CustomerOrder,
                        model.CustomerOrderNO, strFileUrl, _adminModel.Username))
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
                result.Info = "编辑客户订单出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑客户订单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 取消客户订单
        /// </summary>
        /// <returns></returns>
        private string CancelCustomerOrder()
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
                string strResult = this._bllCustomerOrder.ValidateAndCancelCustomerOrder(_adminModel.Username, iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "取消客户订单成功。";
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
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，取消客户订单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取客户订单详细信息
        /// </summary>
        /// <returns></returns>
        private string GetCustomerOrderById()
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
                CustomerOrderModel model = this._bllCustomerOrder.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"AttachmentFiles\":\""
                    + new BLL.OMS.AttachmentBLL().GetUrlListByObj(ConstantValue.TableAttachment.ColumnObjType.CustomerOrder,
                    model.CustomerOrderNO).Replace('/', '*') + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取客户订单详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取客户订单详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 客户订单出货
        /// </summary>
        /// <returns></returns>
        private string CustomerOrderOutStock()
        {
            PageResult result = new PageResult();
            int iId = -1;
            string strShipmentDate = DataUtility.GetPageFormValue(_request.Form["ShipmentDate"], string.Empty);
            if (_request.Form["Id"] == null || !int.TryParse(_request.Form["Id"].ToString(), out iId) || strShipmentDate == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = this._bllCustomerOrder.ValidateAndOutStock(_adminModel.Username, iId, strShipmentDate);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "客户订单出货成功。";
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
                result.Info = "客户订单出货出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，客户订单出货出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成内部订单号
        /// </summary>
        /// <returns></returns>
        private string GenerateInnerOrderNO()
        {
            PageResult result = new PageResult();
            result.Info = "无法生成内部订单号！";
            try
            {
                int iMax = this._bllCustomerOrder.GetMaxId();
                CustomerOrderModel model = new CustomerOrderModel();
                //首次生成
                if (iMax == 1)
                {
                    model.InnerOrderNO = "OF000000";
                }
                else
                {
                    model = this._bllCustomerOrder.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.InnerOrderNO))
                {
                    result.Remark = "OF" + (int.Parse(model.InnerOrderNO.Remove(0, 2)) + 1).ToString().PadLeft(6, '0');
                    result.State = 1;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成内部订单号出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成内部订单号出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 执行销售审核
        /// </summary>
        /// <returns></returns>
        private string ConfirmFirst()
        {
            PageResult result = new PageResult();
            int iId = -1;
            int iType = -1;
            if (_request.Form["COId"] == null || !int.TryParse(_request.Form["COId"].ToString(), out iId)
                || _request.Form["Type"] == null || !int.TryParse(_request.Form["Type"].ToString(), out iType))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = this._bllCustomerOrder.ConfirmFirst(_adminModel.Username, iId, iType);
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
                result.Info = "执行销售审核出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，执行销售审核出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取产品销售分析列表数据
        /// </summary>
        /// <returns></returns>
        private string GetConfirmFirstData()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("COId", _request.Form["COId"], ConditionEnm.Equal));


            searchInfo.ConditionFilter = lstFilter;

            DataTable dt = new DataTable();
            try
            {
                DataSet ds = _bllCustomerOrder.GetConfirmFirstData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取产品销售分析列表数据出现异常:" + ex.Message, ex);
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