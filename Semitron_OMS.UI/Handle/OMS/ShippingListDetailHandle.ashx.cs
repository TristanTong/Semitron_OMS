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
using Semitron_OMS.DAL.Common;
using Semitron_OMS.BLL.Common;
using System.Data.SqlClient;

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// ShippingListDetailHandle 的摘要说明
    /// </summary>
    public class ShippingListDetailHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //出库明细对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(ShippingListDetailHandle));
        HttpRequest _request = HttpContext.Current.Request;
        AdminModel _adminModel = new AdminModel();
        ShippingListDetailBLL _bllShippingListDetail = new ShippingListDetailBLL();

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
                    //获取出库明细列表
                    case "GetShippingListDetail":
                        context.Response.Write(GetShippingListDetail());
                        break;
                    //根据出库单Id获得出库单明细列表记录
                    case "GetShippingListDetailByShippingListId":
                        context.Response.Write(GetShippingListDetailByShippingListId());
                        break;
                    //根据ID获取出库单明细记录
                    case "GetShippingListDetailById":
                        context.Response.Write(GetShippingListDetailById());
                        break;
                    //修改出库单
                    case "EditShippingListDetail":
                        context.Response.Write(EditShippingListDetail());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 修改出库单明细
        /// </summary>
        /// <returns></returns>
        private string EditShippingListDetail()
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
                ShippingListDetailModel model = this._bllShippingListDetail.GetModel(iId);

                if (model == null || model.ID <= 0)
                {
                    result.State = 0;
                    result.Info = "获取数据对应ID的明细数据异常,请重新操作.";
                    return result.ToString();
                }

                SQLOperateHelper.SetEntityFiledValue(model, "OutQty", _request.Form["OutQty"]);
                SQLOperateHelper.SetEntityFiledValue(model, "Remark", _request.Form["Remark"]);
                SQLOperateHelper.SetEntityFiledValue(model, "AvailFlag", _request.Form["AvailFlag"]);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllShippingListDetail.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑出库单明细记录成功！";
                }
                else
                {
                    result.State = 0;
                    result.Info = strResult.Replace("OK", "");
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "编辑出库单明细记录出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑出库单明细记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据ID获取出库单明细记录
        /// </summary>
        /// <returns></returns>
        private string GetShippingListDetailById()
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
                ShippingListDetailModel model = this._bllShippingListDetail.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());

                ShippingListModel lModel = new BLL.OMS.ShippingListBLL().GetModel(model.ShippingListID);
                string strShippingListNo = string.Empty;
                if (lModel != null)
                {
                    strShippingListNo = lModel.ShippingListNo;
                }

                WarehouseModel wModel = new BLL.Common.WarehouseBLL().GetModelByCode(model.StockCode);
                string strStockName = string.Empty;
                if (wModel != null)
                {
                    strStockName = wModel.WName;
                }

                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"ShippingListNo\":\""
                  + strShippingListNo + "\",\"StockName\":\""
                  + strStockName + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取出库单明细记录信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取出库单明细记录信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 根据出库单Id获得出库单明细列表记录
        /// </summary>
        /// <returns></returns>
        private string GetShippingListDetailByShippingListId()
        {
            PageResult result = new PageResult();
            int iListId = -1;
            if (_request.Form["ListId"] == null || !int.TryParse(_request.Form["ListId"].ToString(), out iListId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                List<ShippingListDetailDisplayModel> listModel = this._bllShippingListDetail.GetDisplayModelList(iListId);
                return JsonConvert.SerializeObject(listModel, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "根据出库单Id获得出库单明细列表记录出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，根据出库单Id获得出库单明细列表记录出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取出库明细列表
        /// </summary>
        /// <returns></returns>
        private string GetShippingListDetail()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ShippingListNo", _request.Form["ShippingListNo"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.StockCode", _request.Form["StockCode"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ApprovedUser", _request.Form["ApprovedUser"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ByHandUserID", _request.Form["ByHandUserID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.IsApproved", _request.Form["IsApproved"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.AvailFlag", _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("D.ProductCode", _request.Form["ProductCode"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.MPN", _request.Form["MPN"], ConditionEnm.AllLike));

            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            //查询条件：开始时间，结束时间
            string strTimeField = "D.CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "D.CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "D.UpdateTime";
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
                DataSet ds = _bllShippingListDetail.GetShippingListDetailPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:"
                    + _request.UserHostAddress + "，获取出库明细信息出现异常:" + ex.Message, ex);
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
                //判断是否有导出权限
                if (!PermissionUtility.IsExistButtonPer(this._adminModel.PerModule,
                    Semitron_OMS.Common.Const.ConstPermission.PagePerConst.PAGE_SHIPPING_LIST,
                    Semitron_OMS.Common.Const.ConstPermission.ButtonPerConst.BTN_EXPORT_SHIPPING_LIST))
                {
                    result.State = 0;
                    result.Info = "未分配出库单导出数据权限，操作无效。";
                    return result.ToString();
                }
                try
                {
                    FileExcelDAl fileDLL = new FileExcelDAl();
                    string strTableName = "出库单记录";
                    string filename = strTableName + "导出" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    fileDLL.CreateFile(strTableName + "导出", filename, dt);
                    string filepath = "/file_system/ExportExcelFile/" + filename;
                    //增加操作日志
                    OperationsLogBLL bllOL = new OperationsLogBLL();
                    bool bA = bllOL.AddExecute("ShippingListDetailHandle", filename, "", (int)OperationsType.Export);
                    if (bA)
                    {
                        result.State = 1;
                        result.Remark = filepath;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = 0;
                    result.Info = "数据量过大，无法生成相应Excel文件。请细化查询后导出。";
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