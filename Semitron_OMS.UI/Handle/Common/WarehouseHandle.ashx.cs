using Newtonsoft.Json;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Semitron_OMS.UI.Handle.Common
{
    /// <summary>
    /// WarehouseHandle 的摘要说明
    /// </summary>
    public class WarehouseHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //仓库对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(WarehouseHandle));
        Semitron_OMS.BLL.Common.ProductTypeBLL _bllProductType = new Semitron_OMS.BLL.Common.ProductTypeBLL();
        Semitron_OMS.BLL.Common.WarehouseBLL _bllWarehouse = new Semitron_OMS.BLL.Common.WarehouseBLL();
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
                    //获取仓库树形HTML
                    case "GetWarehouseTree":
                        context.Response.ContentType = "application/text";
                        context.Response.Write(GetWarehouseTree());
                        break;
                    //获取仓库列表
                    case "GetProductType":
                        context.Response.Write(GetProductType());
                        break;
                    //新增仓库
                    case "AddProductType":
                        context.Response.Write(AddProductType());
                        break;
                    //修改仓库
                    case "EditProductType":
                        context.Response.Write(EditProductType());
                        break;
                    //删除仓库
                    case "DelProductType":
                        context.Response.Write(DelProductType());
                        break;
                    //获取仓库详细信息
                    case "GetProductTypeById":
                        context.Response.Write(GetProductTypeById());
                        break;
                    //生成编码
                    case "GenerateCodeAndPath":
                        context.Response.Write(GenerateCodeAndPath());
                        break;
                }
            }
            context.Response.End();
        }

        //获取仓库树形HTML
        private string GetWarehouseTree()
        {
            PageResult result = new PageResult();
            try
            {
                List<WarehouseModel> lstModel = this._bllWarehouse.GetModelList("AvailFlag=1");
                if (lstModel.Count > 0)
                {
                    result.State = 1;
                    result.Info = "获取仓库树形结构成功。";

                    string strRemark = "<li><a href=\"javascript:\" onclick=\"return false;\">所有仓库</a><ul>";
                    lstModel.Where(r => r.PId == 0).OrderBy(o => o.SK).ForEach(e =>
                    {
                        strRemark += "<li><a href=\"javascript:\" onclick=\"$.bringBack({id:'" + e.ID + "', WCode:'" + e.WCode + "', WName:'" + e.WName + "',WType:'" + e.WType + "',PId:'" + e.PId + "',WBelongUserID:'" + (e.WBelongUserID == null ? "0" : e.WBelongUserID.ToString()) + "'})\">" + e.WName + "</a><ul>";
                        lstModel.Where(s => s.PId == e.ID).OrderBy(o => o.SK).ForEach(f =>
                        {
                            strRemark += "<li><a href=\"javascript:\" onclick=\"$.bringBack({id:'" + f.ID + "', WCode:'" + f.WCode + "', WName:'" + f.WName + "',WType:'" + f.WType + "',PId:'" + f.PId + "',WBelongUserID:'" + (f.WBelongUserID == null ? "0" : f.WBelongUserID.ToString()) + "'})\">" + f.WName + "</a></li>";
                        });
                        strRemark += "</ul></li>";
                    });
                    return strRemark + "</ul></li>";
                }
                else
                {
                    result.Info = "ERROR";
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "ERROR";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取仓库树形结构异常出现异常：" + ex.Message, ex);
            }
            return result.Info;
        }

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        private string GetProductType()
        {
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();

            //返回查询总数
            int o_RowsCount = 0;

            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();
            //获取表格提交参数
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(_request.Form["page"], 1));
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
            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(strOrder, string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(_request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("AvailFlag", _request.Form["AvailFlag"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Lang", _request.Form["Lang"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("TypeName", _request.Form["Name"], ConditionEnm.AllLike));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Code", _request.Form["Code"], ConditionEnm.AllLike));

            //查询条件：开始时间，结束时间
            //时间类型
            string strTimeType = DataUtility.GetPageFormValue(_request.Form["TimeType"], string.Empty);
            string strTimeField = "CreateTime";
            if (strTimeType == "1")
            {
                strTimeField = "CreateTime";
            }
            if (strTimeType == "2")
            {
                strTimeField = "UpdateTime";
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
                DataSet ds = _bllProductType.GetProductTypePageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取仓库信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(ProductTypeModel model)
        {
            PageResult result = new PageResult();
            if (_request.Form["Code"] == null
                || _request.Form["Name"] == null
                || _request.Form["Lang"] == null
                || _request.Form["SK"] == null)
            {

                return "系统错误,参数获取异常。";
            }
            SQLOperateHelper.SetEntityFiledValue(model, "Code", _request.Form["Code"]);
            SQLOperateHelper.SetEntityFiledValue(model, "TypeName", _request.Form["Name"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Lang", _request.Form["Lang"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SK", _request.Form["SK"]);

            return "OK";
        }

        /// <summary>
        /// 新增仓库
        /// </summary>
        /// <returns></returns>
        private string AddProductType()
        {
            PageResult result = new PageResult();
            ProductTypeModel model = new ProductTypeModel();
            string strGetResult = this.GetNewModel(model);
            if (strGetResult != "OK")
            {
                result.State = 0;
                result.Info = strGetResult;
                return result.ToString();
            }
            try
            {
                SQLOperateHelper.SetEntityFiledValue(model, "CreateUser", _adminModel.Username);
                string strResult = this._bllProductType.ValidateAndAdd(model);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Remark = strResult.Replace("OK", "");
                    result.Info = "新增仓库成功！";
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
                result.Info = "新增仓库出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增仓库出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <returns></returns>
        private string EditProductType()
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
                ProductTypeModel model = this._bllProductType.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllProductType.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑仓库成功！";
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
                result.Info = "编辑仓库出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑仓库出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <returns></returns>
        private string DelProductType()
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
                string strResult = this._bllProductType.ValidateAndDelProductType(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除仓库成功。";
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
                result.Info = "删除仓库出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除仓库出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取仓库详细信息
        /// </summary>
        /// <returns></returns>
        private string GetProductTypeById()
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
                ProductTypeModel model = this._bllProductType.GetModel(iId);
                return JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取仓库详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取仓库详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成编码
        /// </summary>
        /// <returns></returns>
        private string GenerateCodeAndPath()
        {
            PageResult result = new PageResult();
            result.Info = "无法生成编码！";

            try
            {
                int iMax = this._bllProductType.GetMaxId();
                ProductTypeModel model = new ProductTypeModel();
                //首次生成
                if (iMax == 1)
                {
                    model.Code = "T0001";
                }
                else
                {
                    model = this._bllProductType.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.Code))
                {
                    result.Info = "T" + (int.Parse(model.Code.Remove(0, 1)) + 1).ToString().PadLeft(4, '0');
                    result.State = 1;
                    result.Remark = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".htm";
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