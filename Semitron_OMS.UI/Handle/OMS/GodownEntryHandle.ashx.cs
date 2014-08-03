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

namespace Semitron_OMS.UI.Handle.OMS
{
    /// <summary>
    /// GodownEntryHandle 的摘要说明
    /// </summary>
    public class GodownEntryHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //入库单对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(GodownEntryHandle));
        Semitron_OMS.BLL.OMS.GodownEntryBLL _bllGodownEntry = new Semitron_OMS.BLL.OMS.GodownEntryBLL();
        HttpRequest _request = HttpContext.Current.Request;
        AdminModel _adminModel = new AdminModel();
        GodownEntryDetailBLL _bllGodownEntryDetail = new GodownEntryDetailBLL();

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
                    //获取入库单列表
                    case "GetGodownEntry":
                        context.Response.Write(GetGodownEntry());
                        break;
                    //新增入库单
                    case "AddGodownEntry":
                        context.Response.Write(AddGodownEntry());
                        break;
                    //修改入库单
                    case "EditGodownEntry":
                        context.Response.Write(EditGodownEntry());
                        break;
                    //删除入库单
                    case "DelGodownEntry":
                        context.Response.Write(DelGodownEntry());
                        break;
                    //获取入库单详细信息
                    case "GetGodownEntryById":
                        context.Response.Write(GetGodownEntryById());
                        break;
                    //生成编码
                    case "GenerateCode":
                        context.Response.Write(GenerateCode());
                        break;
                    //审核入库单
                    case "ApproveGodownEntry":
                        context.Response.Write(ApproveGodownEntry());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 审核入库单
        /// </summary>
        private string ApproveGodownEntry()
        {
            PageResult result = new PageResult();
            int iId = -1;
            if (_request.Form["EntryId"] == null || !int.TryParse(_request.Form["EntryId"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = this._bllGodownEntry.ValidateAndApproveGodownEntry(iId, _adminModel.AdminID);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Info = "审核入库单成功。" + strResult.Replace("OK", "");
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
                result.Info = "审核入库单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，审核入库单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成编码
        /// </summary>
        private string GenerateCode()
        {
            PageResult result = new PageResult();
            result.Info = "无法生成编码！";

            try
            {
                int iMax = 1;//this._bllGodownEntry.GetMaxId();
                GodownEntryModel model = new GodownEntryModel();
                string strNow = DateTime.Now.ToString("yyyyMMdd");
                string strNum = "10";
                //首次生成
                if (iMax == 1)
                {
                    model.EntryNo = "SEGE" + strNow + strNum;
                }
                else
                {
                    model = this._bllGodownEntry.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.EntryNo))
                {
                    //为今日的订单在今日订单最大ID编号上加1递增
                    if (model.EntryNo.Substring(4, 8) == strNow)
                    {
                        strNum = (int.Parse(model.EntryNo.Remove(0, 12)) + 1).ToString().PadLeft(2, '0');
                    }

                    result.Remark = "SEGE" + strNow + strNum;
                    result.State = 1;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成入库单编码与出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成入库单编码出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取入库单列表
        /// </summary>
        /// <returns></returns>
        private string GetGodownEntry()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.EntryNo", _request.Form["EntryNo"], ConditionEnm.AllLike));

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.InWarehouseCode", _request.Form["InWarehouseCode"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("A2.AdminID", _request.Form["ApprovedUser"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("A1.AdminID", _request.Form["ByHandUserID"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.IsApproved", _request.Form["IsApproved"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.State", _request.Form["State"], ConditionEnm.Equal));

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
            if (strTimeType == "3")
            {
                strTimeField = "G.InStockDate";
            }
            if (strTimeType == "4")
            {
                strTimeField = "G.ApprovedTime";
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
                DataSet ds = _bllGodownEntry.GetGodownEntryPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取入库单信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="strGetResult"></param>
        /// <returns></returns>
        private GodownEntryModel GetNewModel(ref string strGetResult)
        {
            string strJson = DataUtility.GetPageFormValue(_request.Form["JsonString"], string.Empty);
            if (string.IsNullOrEmpty(strJson) || _request.Form["JsonStringDetail"] == null)
            {
                strGetResult = "系统错误,参数获取异常。";
                return null;
            }
            strGetResult = "OK";
            return JsonConvert.DeserializeObject<GodownEntryModel>(strJson);

        }

        /// <summary>
        /// 获取入库单单明细实体列表
        /// </summary>
        /// <param name="model">入库单列表</param>
        /// <returns>入库单单明细实体列表</returns>
        private List<GodownEntryDetailModel> GetNewDetailModel(GodownEntryModel model)
        {
            List<GodownEntryDetailModel> lstGodownEntryDetailModel = null;
            string strJsonDetail = DataUtility.GetPageFormValue(_request.Form["JsonStringDetail"], string.Empty);
            if (string.IsNullOrEmpty(strJsonDetail))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(strJsonDetail))
            {
                lstGodownEntryDetailModel = JsonConvert.DeserializeObject<List<GodownEntryDetailModel>>(strJsonDetail);
            }

            foreach (GodownEntryDetailModel m in lstGodownEntryDetailModel)
            {
                m.GodownEntryID = model.ID;
                m.AvailFlag = true;
            }

            return lstGodownEntryDetailModel;
        }

        /// <summary>
        /// 新增入库单
        /// </summary>
        /// <returns></returns>
        private string AddGodownEntry()
        {
            PageResult result = new PageResult();
            new GodownEntryModel();
            string strGetResult = string.Empty;

            try
            {
                GodownEntryModel model = this.GetNewModel(ref strGetResult);
                model.EntryNo = JsonConvert.DeserializeObject<PageResult>(GenerateCode()).Remark;
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }

                SQLOperateHelper.SetEntityFiledValue(model, "CreateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "CreateTime", DateTime.Now);
                SQLOperateHelper.SetEntityFiledValue(model, "State", "1");
                string strResult = string.Empty;
                int iReturnID = this._bllGodownEntry.ValidateAndAdd(model, ref strResult);
                if (strResult.StartsWith("OK"))
                {
                    model.ID = iReturnID;
                    strResult = "新增入库单成功！";
                    List<GodownEntryDetailModel> lstGodownEntryDetailModel = GetNewDetailModel(model);
                    if (lstGodownEntryDetailModel == null)
                    {
                        strResult += "但入库单明细数据获取为空。";
                    }
                    if (lstGodownEntryDetailModel != null)
                    {
                        try
                        {
                            _bllGodownEntryDetail.AddList(lstGodownEntryDetailModel);
                        }
                        catch (Exception ex)
                        {
                            strResult += "但入库单明细数据保存出现异常：" + ex.Message;
                        }
                    }

                    result.State = 1;
                    result.Remark = iReturnID.ToString();
                    result.Info = strResult;
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
                result.Info = "新增入库单出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增入库单出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <returns></returns>
        private string EditGodownEntry()
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
                string strGetResult = string.Empty;
                GodownEntryModel model = this.GetNewModel(ref strGetResult);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                GodownEntryModel oldModel = this._bllGodownEntry.GetModel(iId);
                SQLOperateHelper.SetEntityByOldNoNull(model, oldModel);

                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                model.IsApproved = oldModel.IsApproved;
                model.ApprovedTime = oldModel.ApprovedTime;
                model.ApprovedUser = oldModel.ApprovedUser;
                string strResult = this._bllGodownEntry.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑入库单成功！";
                    List<GodownEntryDetailModel> lstGodownEntryDetailModel = GetNewDetailModel(model);

                    if (lstGodownEntryDetailModel != null && lstGodownEntryDetailModel.Count > 0)
                    {
                        try
                        {
                            _bllGodownEntryDetail.AddList(lstGodownEntryDetailModel);
                        }
                        catch (Exception ex)
                        {
                            strResult += "但入库单明细数据保存出现异常：" + ex.Message;
                        }
                    }
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
                result.Info = "编辑入库单出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑入库单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <returns></returns>
        private string DelGodownEntry()
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
                string strResult = this._bllGodownEntry.ValidateAndDelGodownEntry(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除入库单成功。";
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
                result.Info = "删除入库单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除入库单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取入库单详细信息
        /// </summary>
        /// <returns></returns>
        private string GetGodownEntryById()
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
                GodownEntryModel model = this._bllGodownEntry.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"InWarehouseName\":\""
                  + new BLL.Common.WarehouseBLL().GetModelByCode(model.InWarehouseCode).WName + "\",\"ByHandUserName\":\""
                  + new BLL.Common.Admin().GetModel((int)model.ByHandUserID).Name + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取入库单信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取入库单信息出现异常：" + ex.Message, ex);
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