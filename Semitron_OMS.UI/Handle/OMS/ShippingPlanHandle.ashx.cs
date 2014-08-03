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
    /// ShippingPlanHandle 的摘要说明
    /// </summary>
    public class ShippingPlanHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //出货计划单对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(ShippingPlanHandle));
        Semitron_OMS.BLL.OMS.ShippingPlanBLL _bllShippingPlan = new Semitron_OMS.BLL.OMS.ShippingPlanBLL();
        HttpRequest _request = HttpContext.Current.Request;
        AdminModel _adminModel = new AdminModel();
        ShippingPlanDetailBLL _bllShippingPlanDetail = new ShippingPlanDetailBLL();

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
                    //获取出货计划单列表
                    case "GetShippingPlan":
                        context.Response.Write(GetShippingPlan());
                        break;
                    //新增出货计划单
                    case "AddShippingPlan":
                        context.Response.Write(AddShippingPlan());
                        break;
                    //修改出货计划单
                    case "EditShippingPlan":
                        context.Response.Write(EditShippingPlan());
                        break;
                    //删除出货计划单
                    case "DelShippingPlan":
                        context.Response.Write(DelShippingPlan());
                        break;
                    //获取出货计划单详细信息
                    case "GetShippingPlanById":
                        context.Response.Write(GetShippingPlanById());
                        break;
                    //生成编码
                    case "GenerateCode":
                        context.Response.Write(GenerateCode());
                        break;
                    //审核出货计划单
                    case "ApproveShippingPlan":
                        context.Response.Write(ApproveShippingPlan());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 审核出货计划单
        /// </summary>
        private string ApproveShippingPlan()
        {
            PageResult result = new PageResult();
            int iId = -1;
            if (_request.Form["ShippingPlanId"] == null || !int.TryParse(_request.Form["ShippingPlanId"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                ShippingPlanModel model = this._bllShippingPlan.GetModel(iId);
                model.IsApproved = true;
                model.ApprovedTime = DateTime.Now;
                model.ApprovedUserID = _adminModel.AdminID;
                model.UpdateTime = DateTime.Now;
                model.UpdateUser = _adminModel.Username;

                string strResult = this._bllShippingPlan.ValidateAndApproveShippingPlan(iId, _adminModel.AdminID);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Info = "审核出货计划单成功。" + strResult.Replace("OK", "");
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
                result.Info = "审核出货计划单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，审核出货计划单出现异常：" + ex.Message, ex);
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
                int iMax = this._bllShippingPlan.GetMaxId();
                ShippingPlanModel model = new ShippingPlanModel();
                string strNow = DateTime.Now.ToString("yyyyMMdd");
                string strNum = "10";
                //首次生成
                if (iMax == 1)
                {
                    model.ShippingPlanNo = "SESP" + strNow + strNum;
                }
                else
                {
                    model = this._bllShippingPlan.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.ShippingPlanNo))
                {
                    //为今日的订单在今日订单最大ID编号上加1递增
                    if (model.ShippingPlanNo.Substring(4, 8) == strNow)
                    {
                        strNum = (int.Parse(model.ShippingPlanNo.Remove(0, 12)) + 1).ToString().PadLeft(2, '0');
                    }

                    result.Remark = "SESP" + strNow + strNum;
                    result.State = 1;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成出货计划单编码与出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成出货计划单编码出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取出货计划单列表
        /// </summary>
        /// <returns></returns>
        private string GetShippingPlan()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ShippingPlanNo", _request.Form["ShippingPlanNo"], ConditionEnm.AllLike));

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
                strTimeField = "G.ShippingPlanDate";
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
                DataSet ds = _bllShippingPlan.GetShippingPlanPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取出货计划单信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="strGetResult"></param>
        /// <returns></returns>
        private ShippingPlanModel GetNewModel(ref string strGetResult)
        {
            string strJson = DataUtility.GetPageFormValue(_request.Form["JsonString"], string.Empty);
            if (string.IsNullOrEmpty(strJson) || _request.Form["JsonStringDetail"] == null)
            {
                strGetResult = "系统错误,参数获取异常。";
                return null;
            }
            strGetResult = "OK";
            return JsonConvert.DeserializeObject<ShippingPlanModel>(strJson);

        }

        /// <summary>
        /// 获取出货计划单单明细实体列表
        /// </summary>
        /// <param name="model">出货计划单列表</param>
        /// <returns>出货计划单单明细实体列表</returns>
        private List<ShippingPlanDetailModel> GetNewDetailModel(ShippingPlanModel model)
        {
            List<ShippingPlanDetailModel> lstShippingPlanDetailModel = null;
            string strJsonDetail = DataUtility.GetPageFormValue(_request.Form["JsonStringDetail"], string.Empty);
            if (string.IsNullOrEmpty(strJsonDetail))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(strJsonDetail))
            {
                lstShippingPlanDetailModel = JsonConvert.DeserializeObject<List<ShippingPlanDetailModel>>(strJsonDetail);
            }

            foreach (ShippingPlanDetailModel m in lstShippingPlanDetailModel)
            {
                m.ShippingPlanID = model.ID;
                m.AvailFlag = true;
            }

            return lstShippingPlanDetailModel;
        }

        /// <summary>
        /// 新增出货计划单
        /// </summary>
        /// <returns></returns>
        private string AddShippingPlan()
        {
            PageResult result = new PageResult();
            new ShippingPlanModel();
            string strGetResult = string.Empty;

            try
            {
                ShippingPlanModel model = this.GetNewModel(ref strGetResult);
                model.ShippingPlanNo = JsonConvert.DeserializeObject<PageResult>(GenerateCode()).Remark;
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
                int iReturnID = this._bllShippingPlan.ValidateAndAdd(model, ref strResult);
                if (strResult.StartsWith("OK"))
                {
                    model.ID = iReturnID;
                    strResult = "新增出货计划单成功！";
                    List<ShippingPlanDetailModel> lstShippingPlanDetailModel = GetNewDetailModel(model);
                    if (lstShippingPlanDetailModel == null)
                    {
                        strResult += "但出货计划单明细数据获取为空。";
                    }
                    if (lstShippingPlanDetailModel != null)
                    {
                        try
                        {
                            _bllShippingPlanDetail.AddList(lstShippingPlanDetailModel);
                        }
                        catch (Exception ex)
                        {
                            strResult += "但出货计划单明细数据保存出现异常：" + ex.Message;
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
                result.Info = "新增出货计划单出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增出货计划单出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改出货计划单
        /// </summary>
        /// <returns></returns>
        private string EditShippingPlan()
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
                ShippingPlanModel model = this.GetNewModel(ref strGetResult);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                ShippingPlanModel oldModel = this._bllShippingPlan.GetModel(iId);
                SQLOperateHelper.SetEntityByOldNoNull(model, oldModel);

                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                model.IsApproved = oldModel.IsApproved;
                model.ApprovedTime = oldModel.ApprovedTime;
                model.ApprovedUserID = oldModel.ApprovedUserID;
                string strResult = this._bllShippingPlan.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑出货计划单成功！";
                    List<ShippingPlanDetailModel> lstShippingPlanDetailModel = GetNewDetailModel(model);

                    if (lstShippingPlanDetailModel != null && lstShippingPlanDetailModel.Count > 0)
                    {
                        try
                        {
                            _bllShippingPlanDetail.AddList(lstShippingPlanDetailModel);
                        }
                        catch (Exception ex)
                        {
                            strResult += "但出货计划单明细数据保存出现异常：" + ex.Message;
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
                result.Info = "编辑出货计划单出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑出货计划单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除出货计划单
        /// </summary>
        /// <returns></returns>
        private string DelShippingPlan()
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
                string strResult = this._bllShippingPlan.ValidateAndDelShippingPlan(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除出货计划单成功。";
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
                result.Info = "删除出货计划单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除出货计划单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取出货计划单详细信息
        /// </summary>
        /// <returns></returns>
        private string GetShippingPlanById()
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
                ShippingPlanModel model = this._bllShippingPlan.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"ByHandUserName\":\""
                  + new BLL.Common.Admin().GetModel((int)model.ByHandUserID).Name + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取出货计划单信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取出货计划单信息出现异常：" + ex.Message, ex);
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