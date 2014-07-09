using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.BLL.OMS;
using Semitron_OMS.Common;
using System.Data;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// AdminBindSupplierHandle 的摘要说明
    /// </summary>
    public class AdminBindSupplierHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private AdminBindSupplierBLL _bllAdminBindSupplier = new AdminBindSupplierBLL();
        private log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(AdminBindSupplierHandle));//系统日志对象
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
            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"].Trim();
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //根据供应商ID分页获取采购
                    case "GetAdminBindSupplierList":
                        context.Response.Write(GetAdminBindSupplierList());
                        break;
                    //关联采购
                    case "Bind":
                        context.Response.Write(Bind());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 根据供应商ID分页获取采购
        /// </summary>
        /// <returns></returns>
        private string GetAdminBindSupplierList()
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
            searchInfo.OrderByField = "Valid desc,IndexID2 asc," + DataUtility.GetPageFormValue(request.Form["sortname"], string.Empty);

            ////状态
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("A.AvailFlag", 1, ConditionEnm.Equal));
            
            searchInfo.ConditionFilter = lstFilter;

            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;
            int iCustomerId;
            DataTable dt = new DataTable();
            if (int.TryParse(DataUtility.GetPageFormValue(request.Form["CustomerId"], "-1"), out iCustomerId))
            {
                try
                {
                    //调用通用存储过程
                    DataSet ds = _bllAdminBindSupplier.GetAdminBindSupplierPageData(searchInfo, iCustomerId, out o_RowsCount);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    _myLogger.Error("根据供应商ID分页获取采购出现异常" + ex.Message, ex);
                }
            }
            //rowsCount = dt.Rows.Count;
            //转化JSON格式
            //string strCols = DataUtility.GetPageFormValue(request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt, searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 关联采购
        /// </summary>
        /// <returns></returns>
        private string Bind()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            int codeId;
            if (request.Form["IdList"] == null || request.Form["IdListAll"] == null || !int.TryParse(DataUtility.GetPageFormValue(request.Form["CustomerId"], string.Empty), out codeId))
            {
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }

            string IdList = request.Form["IdList"].ToString().Trim();
            string IdListAll = request.Form["IdListAll"].ToString().Trim();

            result.Info = "关联采购操作失败。";
            try
            {
                if (_bllAdminBindSupplier.AdminBindSupplier(codeId, IdList, IdListAll))
                {
                    result.State = 1;
                    result.Info = "关联采购操作成功。";
                }
            }
            catch (Exception ex)
            {
                _myLogger.Error("关联采购出现异常" + ex.Message, ex);
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