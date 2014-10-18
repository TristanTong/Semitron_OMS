using Newtonsoft.Json;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Model.FM;
using Semitron_OMS.Model.OMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Semitron_OMS.UI.Handle.FM
{
    /// <summary>
    /// BankAccountHandle 的摘要说明
    /// </summary>
    public class BankAccountHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //公司法人对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(BankAccountHandle));
        Semitron_OMS.BLL.FM.BankAccountBLL _bllBankAccount = new Semitron_OMS.BLL.FM.BankAccountBLL();
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
                    //获取下拉框列表字符串
                    case "GetBankAccountSelectList":
                        context.Response.ContentType = "text/plain";
                        context.Response.Charset = "utf-8";
                        context.Response.Write(GetBankAccountSelectList());
                        break;
                    //获取银行账户列表
                    case "GetBankAccount":
                        context.Response.Write(GetBankAccount());
                        break;
                    //修改银行账户
                    case "EditBankAccount":
                        context.Response.Write(EditBankAccount());
                        break;
                    //删除银行账户
                    case "DelBankAccount":
                        context.Response.Write(DelBankAccount());
                        break;
                    //获取银行账户详细信息
                    case "GetBankAccountById":
                        context.Response.Write(GetBankAccountById());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 修改银行账户
        /// </summary>
        /// <returns></returns>
        private string EditBankAccount()
        {
            PageResultJUI result = new PageResultJUI();
            int iId = -1;
            if (_request.Form["SeletedBankAccountId"] == null ||
                !int.TryParse(_request.Form["SeletedBankAccountId"].ToString(), out iId))
            {
                result.statusCode = 300;
                result.message = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                BankAccountModel model = new BankAccountModel();
                if (iId > 0)
                {
                    model = this._bllBankAccount.GetModel(iId);
                }
                else
                {
                    model.AvailFlag = true;
                    SQLOperateHelper.SetEntityFiledValue(model, "CreateUser", _adminModel.Username);
                    SQLOperateHelper.SetEntityFiledValue(model, "CreateTime", DateTime.Now);
                }
                SQLOperateHelper.SetEntityFiledValue(model, "AccountName", _request.Form["AccountName"]);
                SQLOperateHelper.SetEntityFiledValue(model, "BelongName", _request.Form["BelongName"]);
                SQLOperateHelper.SetEntityFiledValue(model, "BelongBank", _request.Form["BelongBank"]);
                SQLOperateHelper.SetEntityFiledValue(model, "CardNo", _request.Form["CardNo"]);
                SQLOperateHelper.SetEntityFiledValue(model, "RealTimeBalance", _request.Form["RealTimeBalance"]);
                SQLOperateHelper.SetEntityFiledValue(model, "SK", _request.Form["SK"]);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllBankAccount.ValidateAndEdit(model);
                if (strResult == "OK")
                {
                    //result.callbackType = "closeCurrent";
                    result.statusCode = 200;
                    result.message = "编辑银行账户成功！";
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
                result.message = "编辑银行账户出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑银行账户出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 物理删除银行账户
        /// </summary>
        /// <returns></returns>
        private string DelBankAccount()
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
                string strResult = this._bllBankAccount.ValidateAndDelBankAccount(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除银行账户成功。";
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
                result.Info = "删除银行账户出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除银行账户出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取银行账户详细信息
        /// </summary>
        /// <returns></returns>
        private string GetBankAccountById()
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
                BankAccountModel model = this._bllBankAccount.GetModel(iId);
                return JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取银行账户详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取银行账户详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取银行账户列表
        /// </summary>
        /// <returns></returns>
        private string GetBankAccount()
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
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.AccountName",
              _request.Form["AccountName"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.BelongName",
                _request.Form["BelongName"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.CardNo",
               _request.Form["CardNo"], ConditionEnm.AllLike));

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
                DataSet ds = _bllBankAccount.GetBankAccountPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取银行账户信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获取下拉框列表字符串
        /// </summary>
        /// <returns></returns>
        private string GetBankAccountSelectList()
        {
            string strTableName = DataUtility.GetPageFormValue(_request.Form["TableName"], string.Empty);
            DataTable dt = this._bllBankAccount.GetDataTableByCache(strTableName);

            DataRow[] drValid = dt.Select("1=1", "[Desc] Asc");
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in drValid)
            {
                sb.AppendFormat("{0}|{1},", dr["Value"].ToString(), dr["Key"].ToString());
            }
            if (sb.ToString().EndsWith(","))
            {
                //移除最后一个逗号
                sb.Remove(sb.ToString().Length - 1, 1);
            }
            return sb.ToString();
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