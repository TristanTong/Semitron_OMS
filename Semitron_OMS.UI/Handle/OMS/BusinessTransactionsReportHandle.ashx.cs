using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Semitron_OMS.Common;
using Semitron_OMS.BLL.Common;
using Semitron_OMS.Model.Common;
using System.Data.SqlClient;
using Semitron_OMS.DAL.Common;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// BusinessTransactionsReport 的摘要说明
    /// </summary>
    public class BusinessTransactionsReportHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private HttpRequest request = HttpContext.Current.Request;
        private AdminModel adminModel = null;
        log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(BusinessTransactionsReportHandle));//系统日志对象

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string methStr = string.Empty;
            if (context.Session["Admin"] == null)
            {
                PageResult result = new PageResult();
                result.Info = "网络异常，请刷新重试。";
                context.Response.Write(result.ToString());
                return;
            }
            else
            {
                adminModel = context.Session["Admin"] as AdminModel;
            }

            if (context.Request.Params["meth"] != null)
            {
                methStr = context.Request.Params["meth"].Trim();
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    case "GetReportList":
                        context.Response.Write(GetReportList());
                        break;
                    case "GetReporCustomerOrderTransactionList":
                        context.Response.Write(GetReporCustomerOrderTransactionList());
                        break;

                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 分页获取客户交易总表
        /// </summary>
        /// <returns></returns>
        private string GetReporCustomerOrderTransactionList()
        {
            return new CommonBLL().GetReportCommon(request, ConstantValue.ProcedureNames.CustomerOrderTransactionReport, "客户交易总表");
        }

        /// <summary>
        /// 分页获取商务交易总表
        /// </summary>
        /// <returns></returns>
        private string GetReportList()
        {
            return new CommonBLL().GetReportCommon(request, ConstantValue.ProcedureNames.BusinessTransactionsReport, "商务交易总表");
        }




        /// <summary>
        /// 向现有数据表中加入小计行
        /// </summary>
        /// <param name="dt">待处理的数据表</param>
        /// <param name="strContentColumnName">显示小计文本的列名</param>
        /// <param name="strContent">显示内容</param>
        private void SumDataTable(DataTable dt, string strContentColumnName, string strContent)
        {
            object[] arrObj = CommonFunction.SumDataNewRow(dt, strContentColumnName, strContent);

            if (dt.Rows.Count > 0)
            {
                DataRow drCurrent = dt.NewRow();
                drCurrent.ItemArray = arrObj;
                //#region 百分比计算
                //if (dt.Columns.Contains("MTPercentage") && dt.Columns.Contains("PlanMT") && dt.Columns.Contains("RealMT"))
                //{
                //    if (Convert.ToDouble(drCurrent["PlanMT"]) == 0.0)
                //    {
                //        drCurrent["MTPercentage"] = 0;
                //    }
                //    else
                //    {
                //        drCurrent["MTPercentage"] = Math.Round(Convert.ToDouble(drCurrent["RealMT"]) / Convert.ToDouble(drCurrent["PlanMT"]) * 100, 0);
                //    }
                //}
                //if (dt.Columns.Contains("SuccessPercentage") && dt.Columns.Contains("SuccessCount") && dt.Columns.Contains("RealMT"))
                //{
                //    if (Convert.ToDouble(drCurrent["RealMT"]) == 0.0)
                //    {
                //        drCurrent["SuccessPercentage"] = 0;
                //    }
                //    else
                //    {
                //        drCurrent["SuccessPercentage"] = Math.Round(Convert.ToDouble(drCurrent["SuccessCount"]) / Convert.ToDouble(drCurrent["RealMT"]) * 100, 0);
                //    }
                //}
                //if (dt.Columns.Contains("BeDeterminedPercentage") && dt.Columns.Contains("PlanSyncCount") && dt.Columns.Contains("BeDeterminedCount"))
                //{
                //    if (Convert.ToDouble(drCurrent["PlanSyncCount"]) == 0.0)
                //    {
                //        drCurrent["BeDeterminedPercentage"] = 0;
                //    }
                //    else
                //    {
                //        drCurrent["BeDeterminedPercentage"] = Math.Round(Convert.ToDouble(drCurrent["BeDeterminedCount"]) / Convert.ToDouble(drCurrent["PlanSyncCount"]) * 100, 2).ToString("###.00");
                //    }
                //}
                //#endregion 百分比计算
                dt.Rows.Add(drCurrent);
            }
        }

        /// <summary>
        /// 向现有数据表中加入总计行
        /// </summary>
        /// <param name="dt">待处理的数据源表</param>
        /// <param name="dtTemp">已经过小计处理的数据表</param>
        /// <param name="strContentColumnName">显示总计文本的列名</param>
        /// <param name="strContent">显示内容</param>
        /// <returns>转换好的数据表</returns>
        private DataTable SumTotalDataTable(DataTable dt, DataTable dtTemp, string strContentColumnName, string strContent)
        {
            DataTable dtTotalTemp = dtTemp;
            object[] arrObj = CommonFunction.SumTotalDataNewRow(dt, strContentColumnName, strContent);

            if (dtTotalTemp.Rows.Count > 0)
            {
                DataRow drCurrent = dtTotalTemp.NewRow();
                drCurrent.ItemArray = arrObj;
                //#region 百分比计算
                //if (dtTotalTemp.Columns.Contains("MTPercentage") && dtTotalTemp.Columns.Contains("PlanMT") && dtTotalTemp.Columns.Contains("RealMT"))
                //{
                //    if (Convert.ToDouble(drCurrent["PlanMT"]) == 0.0)
                //    {
                //        drCurrent["MTPercentage"] = 0;
                //    }
                //    else
                //    {
                //        drCurrent["MTPercentage"] = Math.Round(Convert.ToDouble(drCurrent["RealMT"]) / Convert.ToDouble(drCurrent["PlanMT"]) * 100, 0);
                //    }
                //}
                //if (dtTotalTemp.Columns.Contains("SuccessPercentage") && dtTotalTemp.Columns.Contains("SuccessCount") && dtTotalTemp.Columns.Contains("RealMT"))
                //{
                //    if (Convert.ToDouble(drCurrent["RealMT"]) == 0.0)
                //    {
                //        drCurrent["SuccessPercentage"] = 0;
                //    }
                //    else
                //    {
                //        drCurrent["SuccessPercentage"] = Math.Round(Convert.ToDouble(drCurrent["SuccessCount"]) / Convert.ToDouble(drCurrent["RealMT"]) * 100, 0);
                //    }
                //}
                //if (dtTotalTemp.Columns.Contains("BeDeterminedPercentage") && dtTotalTemp.Columns.Contains("PlanSyncCount") && dtTotalTemp.Columns.Contains("BeDeterminedCount"))
                //{
                //    if (Convert.ToDouble(drCurrent["PlanSyncCount"]) == 0.0)
                //    {
                //        drCurrent["BeDeterminedPercentage"] = 0;
                //    }
                //    else
                //    {
                //        drCurrent["BeDeterminedPercentage"] = Math.Round(Convert.ToDouble(drCurrent["BeDeterminedCount"]) / Convert.ToDouble(drCurrent["PlanSyncCount"]) * 100, 2).ToString("###.00");
                //    }
                //}
                //#endregion 百分比计算
                dtTotalTemp.Rows.Add(drCurrent);
            }
            return dtTotalTemp;
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