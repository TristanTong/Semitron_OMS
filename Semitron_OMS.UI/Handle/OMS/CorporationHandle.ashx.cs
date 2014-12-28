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
using System.Text;


namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// CorporationHandle 的摘要说明
    /// </summary>
    public class CorporationHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //公司法人对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(CorporationHandle));
        Semitron_OMS.BLL.OMS.CorporationBLL _bllCorporation = new Semitron_OMS.BLL.OMS.CorporationBLL();
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
                    //获取公司法人列表
                    case "GetCorporation":
                        context.Response.Write(GetCorporation());
                        break;
                    //新增公司法人
                    case "AddCorporation":
                        context.Response.Write(AddCorporation());
                        break;
                    //修改公司法人
                    case "EditCorporation":
                        context.Response.Write(EditCorporation());
                        break;
                    //删除公司法人
                    case "DelCorporation":
                        context.Response.Write(DelCorporation());
                        break;
                    //获取公司法人详细信息
                    case "GetCorporationById":
                        context.Response.Write(GetCorporationById());
                        break;
                    //获取公司法人下拉框列表字符串
                    case "GetCorporationSelectList":
                        context.Response.ContentType = "text/plain";
                        context.Response.Charset = "utf-8";
                        context.Response.Write(GetCorporationSelectList());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取公司法人下拉框列表字符串
        /// </summary>
        /// <returns></returns>
        private string GetCorporationSelectList()
        {
            string strShow = DataUtility.GetPageFormValue(_request.Form["IsShowAll"], string.Empty);
            DataTable dt = this._bllCorporation.GetDataTableByCache();
            //分别获取有效和无效的计费代码。
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,CompanyName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,CompanyName Asc");
            StringBuilder sb = new StringBuilder();
            if (strShow == "true")
            {
                sb.AppendFormat("{0}|{1},", "－－有效－－", "-1");
            }
            foreach (DataRow dr in drValid)
            {
                sb.AppendFormat("{0}|{1},", dr["CompanyName"].ToString().Replace(',', '，').Replace('|', ' '), dr["ID"].ToString());
            }
            if (strShow == "true")
            {
                sb.AppendFormat("{0}|{1},", "－－无效－－", "-2");
                foreach (DataRow dr in drUnValid)
                {
                    sb.AppendFormat("{0}|{1},", dr["CompanyName"].ToString().Replace(',', '，').Replace('|', ' '), dr["ID"].ToString());
                }
            }
            if (sb.ToString().EndsWith(","))
            {
                //移除最后一个逗号
                sb.Remove(sb.ToString().Length - 1, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取公司法人列表
        /// </summary>
        /// <returns></returns>
        private string GetCorporation()
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
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CompanyName", _request.Form["CompanyName"], ConditionEnm.AllLike));

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
                DataSet ds = _bllCorporation.GetCorporationPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取公司法人信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(CorporationModel model)
        {
            if (_request.Form["CompanyName"] == null
                || _request.Form["Corporator"] == null
                || _request.Form["CompanyAddr"] == null
                || _request.Form["SK"] == null)
            {

                return "系统错误,参数获取异常。";
            }

            SQLOperateHelper.SetEntityFiledValue(model, "CompanyName", _request.Form["CompanyName"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Corporator", _request.Form["Corporator"]);
            SQLOperateHelper.SetEntityFiledValue(model, "CompanyAddr", _request.Form["CompanyAddr"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Email", _request.Form["Email"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Fax", _request.Form["Fax"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Phone", _request.Form["Phone"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SK", _request.Form["SK"]);

            return "OK";
        }

        /// <summary>
        /// 新增公司法人
        /// </summary>
        /// <returns></returns>
        private string AddCorporation()
        {
            PageResult result = new PageResult();
            CorporationModel model = new CorporationModel();
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
                string strResult = this._bllCorporation.ValidateAndAdd(model);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Remark = strResult.Replace("OK", "");
                    result.Info = "新增公司法人成功！";
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
                result.Info = "新增公司法人出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增公司法人出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改公司法人
        /// </summary>
        /// <returns></returns>
        private string EditCorporation()
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
                CorporationModel model = this._bllCorporation.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllCorporation.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑公司法人成功！";
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
                result.Info = "编辑公司法人出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑公司法人出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除公司法人
        /// </summary>
        /// <returns></returns>
        private string DelCorporation()
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
                string strResult = this._bllCorporation.ValidateAndDelCorporation(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除公司法人成功。";
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
                result.Info = "删除公司法人出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除公司法人出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取公司法人详细信息
        /// </summary>
        /// <returns></returns>
        private string GetCorporationById()
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
                CorporationModel model = this._bllCorporation.GetModel(iId);
                return JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取公司法人详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取公司法人详细信息出现异常：" + ex.Message, ex);
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