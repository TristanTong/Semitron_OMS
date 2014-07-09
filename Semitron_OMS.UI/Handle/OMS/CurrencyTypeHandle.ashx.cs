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

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// CurrencyTypeHandle 的摘要说明
    /// </summary>
    public class CurrencyTypeHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //货币种类对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(CurrencyTypeHandle));
        Semitron_OMS.BLL.OMS.CurrencyTypeBLL _bllCurrencyType = new Semitron_OMS.BLL.OMS.CurrencyTypeBLL();
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
                    //获取货币种类列表
                    case "GetCurrencyType":
                        context.Response.Write(GetCurrencyType());
                        break;
                    //新增货币种类
                    case "AddCurrencyType":
                        context.Response.Write(AddCurrencyType());
                        break;
                    //修改货币种类
                    case "EditCurrencyType":
                        context.Response.Write(EditCurrencyType());
                        break;
                    //删除货币种类
                    case "DelCurrencyType":
                        context.Response.Write(DelCurrencyType());
                        break;
                    //获取货币种类详细信息
                    case "GetCurrencyTypeById":
                        context.Response.Write(GetCurrencyTypeById());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取货币种类列表
        /// </summary>
        /// <returns></returns>
        private string GetCurrencyType()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("AvailFlag", _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CurrencyName", _request.Form["CurrencyName"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("ShortName", _request.Form["ShortName"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CountryName", _request.Form["CountryName"], ConditionEnm.AllLike));

            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "UpdateTime";
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
                DataSet ds = _bllCurrencyType.GetCurrencyTypePageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取货币种类信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(CurrencyTypeModel model)
        {
            PageResult result = new PageResult();
            if (_request.Form["CurrencyName"] == null
                || _request.Form["ShortName"] == null
                || _request.Form["CountryName"] == null
                || _request.Form["SK"] == null)
            {

                return "系统错误,参数获取异常。";
            }

            SQLOperateHelper.SetEntityFiledValue(model, "CurrencyName", _request.Form["CurrencyName"]);
            SQLOperateHelper.SetEntityFiledValue(model, "ShortName", _request.Form["ShortName"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CountryName", _request.Form["CountryName"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SK", _request.Form["SK"]);
            SQLOperateHelper.SetEntityFiledValue(model, "AvailFlag", 1);

            return "OK";
        }

        /// <summary>
        /// 新增货币种类
        /// </summary>
        /// <returns></returns>
        private string AddCurrencyType()
        {
            PageResult result = new PageResult();
            CurrencyTypeModel model = new CurrencyTypeModel();
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
                string strResult = this._bllCurrencyType.ValidateAndAdd(model);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Remark = strResult.Replace("OK", "");
                    result.Info = "新增货币种类成功！";
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
                result.Info = "新增货币种类出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增货币种类出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改货币种类
        /// </summary>
        /// <returns></returns>
        private string EditCurrencyType()
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
                CurrencyTypeModel model = this._bllCurrencyType.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllCurrencyType.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑货币种类成功！";
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
                result.Info = "编辑货币种类出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑货币种类出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除货币种类
        /// </summary>
        /// <returns></returns>
        private string DelCurrencyType()
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
                string strResult = this._bllCurrencyType.ValidateAndDelCurrencyType(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除货币种类成功。";
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
                result.Info = "删除货币种类出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除货币种类出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取货币种类详细信息
        /// </summary>
        /// <returns></returns>
        private string GetCurrencyTypeById()
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
                CurrencyTypeModel model = this._bllCurrencyType.GetModel(iId);
                return JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取货币种类详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取货币种类详细信息出现异常：" + ex.Message, ex);
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