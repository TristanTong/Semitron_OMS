using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Web.SessionState;
using System.Data;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// 系统日志的摘要说明
    /// </summary>
    public class SystemLogHandle : IHttpHandler, IRequiresSessionState
    {
        //系统日志对象
        log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(SystemLogHandle));
        Semitron_OMS.BLL.SystemLog bll = new Semitron_OMS.BLL.SystemLog();
        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Admin"] == null)
            {
                PageResult result = new PageResult();
                context.Response.Write(result.ToString());
                return;
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
                    //获取系统日志列表
                    case "GetSystemLog":
                        context.Response.Write(GetSystemLog());
                        break;
                }
            }
            context.Response.End();
        }
        /// <summary>
        /// 获取系统日志列表
        /// </summary>
        /// <returns></returns>
        private string GetSystemLog()
        {
            HttpRequest request = HttpContext.Current.Request;

            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();

            //返回查询总数
            int o_RowCount = 0;

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            //获取表格提交参数
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["page"], 1));
            //获取表格提交的参数
            //当前查询页码
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["page"], 1));
            //每页大小
            searchInfo.PageSize = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["rp"], 20));
            string strPK = "LogID";
            string strOrder = strPK;
            if (request.Form["sortname"] != null)
            {
                strOrder = request.Form["sortname"].ToString();
            }
            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(strOrder, string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Logger", request.Form["txtObject"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Msg", request.Form["txtDescribe"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("LogThread", request.Form["txtThreadName"], ConditionEnm.AllLike));

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("LogLevel", request.Form["SelLevel"], ConditionEnm.Equal));
            //开始时间与结束时间            
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CreateTime", request.Form["txtBeginTime"], ConditionEnm.GreaterEqual));
            if (request.Form["txtFinishTime"] != null && request.Form["txtFinishTime"].ToString().Trim() != string.Empty)
            {
                DateTime endTime = DateTime.Parse(request.Form["txtFinishTime"].ToString().Trim()).AddDays(1);
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CreateTime", endTime.ToString("yyyy-MM-dd"), ConditionEnm.LessThan));
            }
            //bool isCheckTime = false;
            foreach (SQLConditionFilter sc in lstFilter)
            {
                if (sc.FiledName == "CreateTime")
                {
                    //isCheckTime = true;
                    break;
                }
            }
            //if (!isCheckTime)
            //{
            //    SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CreateTime", DateTime.Now.ToString("yyyy-MM-dd"), ConditionEnm.GreaterEqual));
            //}
            searchInfo.ConditionFilter = lstFilter;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                //调用通用存储过程
                ds = bll.GetSystemLogPageData(searchInfo, out o_RowCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                myLogger.Error("获取系统日志列表出现异常" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowCount);
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