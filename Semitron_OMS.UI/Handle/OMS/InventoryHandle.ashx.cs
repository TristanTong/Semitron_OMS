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
using Semitron_OMS.BLL.OMS;
using Semitron_OMS.BLL.Common;
using Semitron_OMS.DAL.Common;
using System.Data.SqlClient;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// InventoryHandle 的摘要说明
    /// </summary>
    public class InventoryHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(InventoryHandle));
        Semitron_OMS.BLL.OMS.InventoryBLL _bllInventory = new Semitron_OMS.BLL.OMS.InventoryBLL();
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
                    //获取列表
                    case "GetInventory":
                        context.Response.Write(GetInventory());
                        break;
                }
            }
        }

        private string GetInventory()
        {
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();

            //返回查询总数
            int o_RowsCount = 0;

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
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
            string strType = DataUtility.GetPageFormValue(_request.Form["Type"], string.Empty);

            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(strOrder, string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(_request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.MPN", _request.Form["MPN"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ProductCode", _request.Form["ProductCode"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.WCodeBelong", _request.Form["StockCode"], ConditionEnm.Equal));

            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "G.CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "G.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "G.UpdateTime";
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
                DataSet ds = _bllInventory.GetInventoryPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取库存信息出现异常:" + ex.Message, ex);
            }

            if (string.IsNullOrEmpty(strType))
            {
                string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
                return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
            }

            //导出Excel
            PageResult result = new PageResult();
            if (strType == "ExportExcel")
            {
                //判断是否有审核权限
                if (!PermissionUtility.IsExistButtonPer(this._adminModel.PerModule,
                    Semitron_OMS.Common.Const.ConstPermission.PagePerConst.PAGE_INVENTORY,
                    Semitron_OMS.Common.Const.ConstPermission.ButtonPerConst.BTN_EXPORT_INVENTORY))
                {
                    result.State = 0;
                    result.Info = "未分配导出库存数据权限，操作无效。";
                    return result.ToString();
                }

                try
                {
                    FileExcelDAl fileDLL = new FileExcelDAl();
                    string strTableName = "库存记录";
                    string filename = strTableName + "导出" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    fileDLL.CreateFile(strTableName + "导出", filename, dt);
                    string filepath = "/file_system/ExportExcelFile/" + filename;
                    //增加操作日志
                    OperationsLogBLL bllOL = new OperationsLogBLL();
                    bool bA = bllOL.AddExecute("InventoryHandle", filename, "", (int)OperationsType.Export);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}