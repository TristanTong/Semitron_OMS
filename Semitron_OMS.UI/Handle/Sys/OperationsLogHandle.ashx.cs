using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Semitron_OMS.Common;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using System.Data;
using Semitron_OMS.BLL.Common;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// 操作的摘要说明
    /// </summary>
    public class OperationsLogHandle : IHttpHandler, IRequiresSessionState
    {
        OperationsLogBLL opl = new OperationsLogBLL();
        log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(OperationsLogHandle));     
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (context.Session["Admin"] == null)
            {
                PageResult result = new PageResult();
                context.Response.Write(result.ToString());
                return;
            }
            string methStr = string.Empty;
            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //获得操作记录列表
                    case "GetOperator":
                        context.Response.Write(GetOperator());
                        break;

                }
            }
            context.Response.End();
        }
        /// <summary>
        /// 获得操作记录列表
        /// </summary>
        /// <returns></returns>
        private string GetOperator()
        {
            HttpRequest request = HttpContext.Current.Request;
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();
            //返回查询总数总数
            int o_RowsCount = 0;
            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();

            //获取表格提交的参数
            //当前查询页码
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["page"], 1));
            //每页的大小
            searchInfo.PageSize = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["rp"], 20));
            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(request.Form["sortname"], string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            //操作类型
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("OType", request.Form["txtType"], ConditionEnm.Equal));
            //操作人
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("AdminName", request.Form["OperatePerson"], ConditionEnm.AllLike));
            //操作对象
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("FormObject", request.Form["FormObject"], ConditionEnm.Equal));
            //操作记录ID
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("PKID", request.Form["PKID"], ConditionEnm.Equal));

            //公司内部用户都可看到自己的个人操作记录。在我的数据管理中进行查看。使用AdminID进行关联查看。
            AdminModel adminModel = HttpContext.Current.Session["Admin"] as AdminModel;
            List<UserRoleModel> userRoleModel = new Semitron_OMS.BLL.Common.UserRole().GetModelList("AdminID=" + adminModel.AdminID);
            if (!(userRoleModel != null && userRoleModel.Count > 0 && userRoleModel[0].RoleID == int.Parse(Semitron_OMS.Common.ConfigHelper.GetConfigString("SuperAdminRoleId"))))//不为超级管理员
            {
                SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("AdminName", adminModel.Username, ConditionEnm.Equal));
            }
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

            //查询数据
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
           
            try
            {
                //调用通用存储过程
                ds = opl.GetOperationsLogPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                myLogger.Error("获取操作日志列表出现异常" + ex.Message, ex);
            }

            string strCols = DataUtility.GetPageFormValue(request.Form["colNames"], string.Empty);
            //转化JSON格式
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