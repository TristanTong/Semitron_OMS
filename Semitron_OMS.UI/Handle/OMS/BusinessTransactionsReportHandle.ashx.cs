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
            return GetReportCommon(ConstantValue.ProcedureNames.CustomerOrderTransactionReport, "客户交易总表");
        }

        /// <summary>
        /// 分页获取商务交易总表
        /// </summary>
        /// <returns></returns>
        private string GetReportList()
        {
            return GetReportCommon(ConstantValue.ProcedureNames.BusinessTransactionsReport, "商务交易总表");
        }
        /// <summary>
        /// 分页获取商务交易总表
        /// </summary>
        /// <returns></returns>
        private string GetReportCommon(string proName, string strTableName)
        {
            int iPageIndex = 1;          //页码
            int iPageSize = 1;           //页大小
            int iRowsCount = 0;
            string strOrderField = string.Empty;
            int iOrderType = 0;

            string strSearchParam = string.Empty;
            string strGroupParam = string.Empty;
            Dictionary<string, string> dicSearch = new Dictionary<string, string>();
            Dictionary<string, string> dicGroup = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            DataTable dtCurrentPage = new DataTable();
            //获取表格提交的参数
            if (request.Form["page"] != null)
            {
                iPageIndex = int.Parse(request.Form["page"].ToString().Trim());
            }
            if (request.Form["rp"] != null)
            {
                iPageSize = int.Parse(request.Form["rp"].ToString().Trim());
            }
            if (request.Form["sortname"] != null)
            {
                strOrderField = request.Form["sortname"].ToString().Trim();
            }
            if (request.Form["sortorder"] != null)
            {
                iOrderType = request.Form["sortorder"].ToString().ToLower() == "asc" ? 0 : 1;
            }

            string strType = DataUtility.GetPageFormValue(request.Form["Type"], string.Empty);
            dicSearch.Add("SearchType", strType);
            //获取查询与分组的参数字符串键值对形式，用“，”分开，值用':'分开。
            //如：SpID:,ServiceCodeID:,FeeCodeID:,RouterInfoID:,BeginTime:,EndTime:,TimeType:1,AreaId:1,OperatesType:,FeeType:
            //Province:1,City:1,Cp:1,Sp:1,ServiceCode:1,FeeCode:1,Router:1,OperateType:1,FeeType:1,Date:1

            if (request.Form["SearchParam"] != null && request.Form["GroupParam"] != null)
            {
                strSearchParam = request.Form["SearchParam"].ToString().Trim();
                strGroupParam = request.Form["GroupParam"].ToString().Trim();
                string[] arrStrSearch = strSearchParam.Split(',');
                if (strSearchParam.Length > 0)
                {
                    foreach (string strSearch in arrStrSearch)
                    {
                        string[] arrStrTemp = strSearch.Split(':');
                        if (arrStrTemp.Length == 2)
                        {
                            dicSearch.Add(arrStrTemp[0], arrStrTemp[1]);
                        }
                        else if (arrStrTemp.Length > 2)
                        {
                            int index = strSearch.IndexOf(':');
                            dicSearch.Add(arrStrTemp[0], strSearch.Substring(index + 1));
                        }
                    }
                }
                string[] arrStrGroup = strGroupParam.Split(',');
                if (arrStrGroup.Length > 0)
                {
                    foreach (string strGroup in arrStrGroup)
                    {
                        string[] arrStrTemp = strGroup.Split(':');
                        if (arrStrTemp.Length == 2)
                        {
                            dicGroup.Add(arrStrTemp[0], arrStrTemp[1]);
                        }
                    }
                }

                try
                {
                    DataSet ds = new DataSet();
                    CommonBLL bll = new CommonBLL();
                    ds = bll.GetData(proName, dicSearch, dicGroup, 0, 0, strOrderField, iOrderType, out iRowsCount);//使用从DataTable提取分页数据
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        iRowsCount = dt.Rows.Count;
                        //SumDataTable(dtCurrentPage, string.Empty, "本页小计");
                        //dt = SumTotalDataTable(dt, dtCurrentPage, string.Empty, "合计");
                    }
                }
                catch (Exception ex)
                {
                    dt = new DataTable();
                    myLogger.Error("分页获取" + strTableName + "出现异常，IP:" + HttpContext.Current.Request.UserHostAddress + "登陆用户：" + adminModel.Username, ex);
                }
            }

            if (string.IsNullOrEmpty(strType))
            {
                dtCurrentPage = CommonFunction.GetCurrentPageTable(dt, iPageSize, iPageIndex);
                string strCols = DataUtility.GetPageFormValue(request.Form["colNames"], string.Empty);
                return JsonJqgrid.JsonForJqgrid(dtCurrentPage.SortDataTableCols(strCols), iPageIndex, iRowsCount);
            }

            //导出Excel
            PageResult result = new PageResult();
            if (strType == "ExportExcel")
            {
                try
                {
                    FileExcelDAl fileDLL = new FileExcelDAl();
                    string filename = strTableName + "导出" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    fileDLL.CreateFile(strTableName + "导出", filename, dt);
                    string filepath = "/file_system/ExportExcelFile/" + filename;
                    //增加操作日志
                    OperationsLogBLL bllOL = new OperationsLogBLL();
                    bool bA = bllOL.AddExecute("BusinessTransactionsReport", filename, "", (int)OperationsType.Export);
                    if (bA)
                    {
                        result.State = 1;
                        result.Remark = filepath;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = 0;
                    result.Info = "数据量过大，无法生成相应Excel文件。请细化地区查询后导出。";
                }
                catch (Exception e)
                {
                    result.State = 0;
                    result.Info = "无法生成相应Excel文件。出现异常：" + e.Message;
                }
            }
            return result.ToString();
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