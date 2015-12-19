using Newtonsoft.Json;
using Semitron_OMS.BLL.Common;
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Semitron_OMS.UI.Handle.Common
{
    /// <summary>
    /// ProductInfoHandle 的摘要说明
    /// </summary>
    public class ProductInfoHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //产品编码对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(ProductInfoHandle));
        Semitron_OMS.BLL.Common.ProductInfoBLL _bllProductInfo = new Semitron_OMS.BLL.Common.ProductInfoBLL();
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
                    //获取产品编码列表
                    case "GetProductInfo":
                        context.Response.Write(GetProductInfo());
                        break;
                    //生成产品编码
                    case "GenerateCode":
                        context.Response.Write(GenerateCode());
                        break;
                    //修改产品编码信息
                    case "EditProductInfo":
                        context.Response.Write(EditProductInfo());
                        break;
                    //删除产品编码信息
                    case "DelProductInfo":
                        context.Response.Write(DelProductInfo());
                        break;
                    //获取产品编码信息详细信息
                    case "GetProductInfoById":
                        context.Response.Write(GetProductInfoById());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 修改产品编码信息
        /// </summary>
        /// <returns></returns>
        private string EditProductInfo()
        {
            PageResultJUI result = new PageResultJUI();
            int iId = -1;
            if (_request.Form["SeletedProductInfoId"] == null ||
                !int.TryParse(_request.Form["SeletedProductInfoId"].ToString(), out iId))
            {
                result.statusCode = 300;
                result.message = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                ProductInfoModel model = new ProductInfoModel();
                if (iId > 0)
                {
                    model = this._bllProductInfo.GetModel(iId);
                }
                else
                {
                    SQLOperateHelper.SetEntityFiledValue(model, "ProductCode", JsonConvert.DeserializeObject<PageResult>(this.GenerateCode()).Info);
                    SQLOperateHelper.SetEntityFiledValue(model, "CreateUser", _adminModel.Username);
                    SQLOperateHelper.SetEntityFiledValue(model, "CreateTime", DateTime.Now);
                    model.AvailFlag = true;
                }
                SQLOperateHelper.SetEntityFiledValue(model, "SupplierID", _request.Form["ProductInfo_Edit_SearchSupplier.SupplierID"]);
                SQLOperateHelper.SetEntityFiledValue(model, "MPN", _request.Form["MPN"]);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllProductInfo.ValidateAndEdit(model);
                if (strResult == "OK")
                {
                    //result.callbackType = "closeCurrent";
                    result.statusCode = 200;
                    result.message = "编辑产品编码信息成功！";
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
                result.message = "编辑产品编码信息出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑产品编码信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 逻辑删除产品编码信息
        /// </summary>
        /// <returns></returns>
        private string DelProductInfo()
        {
            PageResult result = new PageResult();
            //判断是否有审核权限
            if (!PermissionUtility.IsExistButtonPer(this._adminModel.PerModule,
                Semitron_OMS.Common.Const.ConstPermission.PagePerConst.PAGE_PRODUCT_INFO,
                Semitron_OMS.Common.Const.ConstPermission.ButtonPerConst.BTN_DELETE_PRODUCT_INFO))
            {
                result.State = 0;
                result.Info = "未分配权限，操作无效。";
                return result.ToString();
            }
            int iId = -1;
            if (_request.Form["Id"] == null || !int.TryParse(_request.Form["Id"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = this._bllProductInfo.ValidateAndDelProductInfo(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除产品编码信息成功。";
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
                result.Info = "删除产品编码信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除产品编码信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取产品编码信息详细信息
        /// </summary>
        /// <returns></returns>
        private string GetProductInfoById()
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
                ProductInfoModel model = this._bllProductInfo.GetModel(iId);
                string strJsonReturn = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                Semitron_OMS.Model.CRM.SupplierModel sModel = new BLL.CRM.SupplierBLL().GetModel((int)model.SupplierID);
                string strSCode = string.Empty;
                string strSName = string.Empty;
                if (sModel != null && !sModel.SCode.Equals(""))
                {
                    strSCode = sModel.SCode;
                    strSName = sModel.SupplierName;
                }
                return strJsonReturn = strJsonReturn.Substring(0, strJsonReturn.Length - 1)
                        + ",\"SCode\":\"" + strSCode + "\""
                        + ",\"SName\":\"" + strSName + "\""
                        + "}";
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取产品编码信息详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取产品编码信息详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取产品编码列表
        /// </summary>
        private string GetProductInfo()
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

            string strType = DataUtility.GetPageFormValue(_request.Form["Type"], string.Empty);

            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(strOrder, string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(_request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.AvailFlag",
              _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.ProductCode",
                _request.Form["ProductCode"], ConditionEnm.AllLike));//这里alllike就是模糊查询了
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("P.MPN",
                _request.Form["MPN"], ConditionEnm.AllLike));
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
                DataSet ds = _bllProductInfo.GetProductInfoPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取产品编码信息出现异常:" + ex.Message, ex);
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
                    Semitron_OMS.Common.Const.ConstPermission.PagePerConst.PAGE_PRODUCT_INFO,
                    Semitron_OMS.Common.Const.ConstPermission.ButtonPerConst.BTN_EXPORT_PRODUCT_INFO))
                {
                    result.State = 0;
                    result.Info = "未分配导出数据权限，操作无效。";
                    return result.ToString();
                }
                try
                {
                    FileExcelDAl fileDLL = new FileExcelDAl();
                    string strTableName = "产品信息记录";
                    string filename = strTableName + "导出" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    fileDLL.CreateFile(strTableName + "导出", filename, dt);
                    string filepath = "/file_system/ExportExcelFile/" + filename;
                    //增加操作日志
                    OperationsLogBLL bllOL = new OperationsLogBLL();
                    bool bA = bllOL.AddExecute("ProductInfoHandle", filename, "", (int)OperationsType.Export);
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

        private string GenerateCode()
        {
            PageResult result = new PageResult();
            result.Info = "无法生成编码！";

            try
            {
                int iMax = this._bllProductInfo.GetMaxId();
                ProductInfoModel model = new ProductInfoModel();
                //首次生成
                if (iMax == 1)
                {
                    model.ProductCode = "PA000001";
                }
                else
                {
                    model = this._bllProductInfo.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.ProductCode))
                {
                    result.Info = "PA" + (int.Parse(model.ProductCode.Remove(0, 2)) + 1).ToString().PadLeft(6, '0');
                    result.State = 1;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成编码与出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成编码出现异常：" + ex.Message, ex);
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