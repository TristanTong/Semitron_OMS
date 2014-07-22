using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using System;
using System.Collections.Generic;
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
        //付款计划对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(PaymentPlanHandle));
        Semitron_OMS.BLL.FM.PaymentPlanBLL _bllProductType = new Semitron_OMS.BLL.FM.PaymentPlanBLL();
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
                    //获取付款计划列表
                    case "GetPaymentPlan":
                        context.Response.Write(GetPaymentPlan());
                        break;
                    //生成付款计划
                    case "GenerateCode":
                        context.Response.Write(GenerateCode());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取付款计划列表
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
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.AvailFlag",
              _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.ProductCode",
                _request.Form["ProductCode"], ConditionEnm.AllLike));//这里alllike就是模糊查询了
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.PONO",
                _request.Form["PONO"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("S.SCode",
                _request.Form["SupplierCode"], ConditionEnm.Equal));

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
                DataSet ds = _bllProductType.GetPaymentPlanPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取付款计划信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        private string GenerateCode()
        {
            throw new NotImplementedException();
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