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
    /// ShippingListHandle 的摘要说明
    /// </summary>
    public class ShippingListHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //出库单对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(ShippingListHandle));
        Semitron_OMS.BLL.OMS.ShippingListBLL _bllShippingList = new Semitron_OMS.BLL.OMS.ShippingListBLL();
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
                    //获取出库单列表
                    case "GetShippingList":
                        context.Response.Write(GetShippingList());
                        break;
                    //新增出库单
                    case "AddShippingList":
                        context.Response.Write(AddShippingList());
                        break;
                    //修改出库单
                    case "EditShippingList":
                        context.Response.Write(EditShippingList());
                        break;
                    //删除出库单
                    case "DelShippingList":
                        context.Response.Write(DelShippingList());
                        break;
                    //获取出库单详细信息
                    case "GetShippingListById":
                        context.Response.Write(GetShippingListById());
                        break;
                    //生成编码
                    case "GenerateCode":
                        context.Response.Write(GenerateCode());
                        break;
                    //审核出库单
                    case "ApproveShippingList":
                        context.Response.Write(ApproveShippingList());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 审核出库单
        /// </summary>
        private string ApproveShippingList()
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
                string strResult = this._bllShippingList.ValidateAndApproveShippingList(iId, _adminModel.AdminID);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Info = "审核出库单成功。" + strResult.Replace("OK", "");
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
                result.Info = "审核出库单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，审核出库单出现异常：" + ex.Message, ex);
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
                int iMax = this._bllShippingList.GetMaxId();
                ShippingListModel model = new ShippingListModel();
                string strNow = DateTime.Now.ToString("yyyyMMdd");
                string strNum = "10";
                //首次生成
                if (iMax == 1)
                {
                    model.ShippingListNo = "SESL" + strNow + strNum;
                }
                else
                {
                    model = this._bllShippingList.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.ShippingListNo))
                {
                    //为今日的订单在今日订单最大ID编号上加1递增
                    if (model.ShippingListNo.Substring(4, 8) == strNow)
                    {
                        strNum = (int.Parse(model.ShippingListNo.Remove(0, 12)) + 1).ToString().PadLeft(2, '0');
                    }

                    result.Remark = "SESL" + strNow + strNum;
                    result.State = 1;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "生成出库单编码与出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，生成出库单编码出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取出库单列表
        /// </summary>
        /// <returns></returns>
        private string GetShippingList()
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

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("G.ShippingListNo", _request.Form["ShippingListNo"], ConditionEnm.AllLike));

            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("A2.AdminId", _request.Form["ApprovedUser"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("A1.AdminId", _request.Form["ByHandUserID"], ConditionEnm.Equal));
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
                strTimeField = "G.OutStockDate";
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
                DataSet ds = _bllShippingList.GetShippingListPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取出库单信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="strGetResult"></param>
        /// <returns></returns>
        private ShippingListModel GetNewModel(ref string strGetResult)
        {
            string strJson = DataUtility.GetPageFormValue(_request.Form["JsonString"], string.Empty);
            if (string.IsNullOrEmpty(strJson) || _request.Form["JsonStringDetail"] == null)
            {
                strGetResult = "系统错误,参数获取异常。";
                return null;
            }
            strGetResult = "OK";
            return JsonConvert.DeserializeObject<ShippingListModel>(strJson);

        }

        /// <summary>
        /// 获取出库单单明细实体列表
        /// </summary>
        /// <param name="model">出库单列表</param>
        /// <returns>出库单单明细实体列表</returns>
        private List<ShippingListDetailModel> GetNewDetailModel(ShippingListModel model)
        {
            List<ShippingListDetailModel> lstShippingListDetailModel = null;
            string strJsonDetail = DataUtility.GetPageFormValue(_request.Form["JsonStringDetail"], string.Empty);
            if (string.IsNullOrEmpty(strJsonDetail))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(strJsonDetail))
            {
                lstShippingListDetailModel = JsonConvert.DeserializeObject<List<ShippingListDetailModel>>(strJsonDetail);
            }

            foreach (ShippingListDetailModel m in lstShippingListDetailModel)
            {
                m.ShippingListID = model.ID;
                m.AvailFlag = true;
            }

            return lstShippingListDetailModel;
        }

        /// <summary>
        /// 新增出库单
        /// </summary>
        /// <returns></returns>
        private string AddShippingList()
        {
            PageResult result = new PageResult();
            new ShippingListModel();
            string strGetResult = string.Empty;

            try
            {
                ShippingListModel model = this.GetNewModel(ref strGetResult);
                model.ShippingListNo = JsonConvert.DeserializeObject<PageResult>(GenerateCode()).Remark;
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
                int iReturnID = this._bllShippingList.ValidateAndAdd(model, ref strResult);
                if (strResult.StartsWith("OK"))
                {
                    model.ID = iReturnID;
                    strResult = "新增出库单成功！";
                    List<ShippingListDetailModel> lstShippingListDetailModel = GetNewDetailModel(model);
                    if (lstShippingListDetailModel == null)
                    {
                        strResult += "但出库单明细数据获取为空。";
                    }
                    if (lstShippingListDetailModel != null)
                    {
                        try
                        {
                            _bllShippingListDetail.AddList(lstShippingListDetailModel);
                        }
                        catch (Exception ex)
                        {
                            strResult += "但出库单明细数据保存出现异常：" + ex.Message;
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
                result.Info = "新增出库单出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增出库单出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改出库单
        /// </summary>
        /// <returns></returns>
        private string EditShippingList()
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
                ShippingListModel model = this.GetNewModel(ref strGetResult);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                ShippingListModel oldModel = this._bllShippingList.GetModel(iId);
                SQLOperateHelper.SetEntityByOldNoNull(model, oldModel);

                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                model.IsApproved = oldModel.IsApproved;
                model.ApprovedTime = oldModel.ApprovedTime;
                model.ApprovedUserID = oldModel.ApprovedUserID;
                string strResult = this._bllShippingList.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑出库单成功！";
                    List<ShippingListDetailModel> lstShippingListDetailModel = GetNewDetailModel(model);

                    if (lstShippingListDetailModel != null && lstShippingListDetailModel.Count > 0)
                    {
                        try
                        {
                            _bllShippingListDetail.AddList(lstShippingListDetailModel);
                        }
                        catch (Exception ex)
                        {
                            strResult += "但出库单明细数据保存出现异常：" + ex.Message;
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
                result.Info = "编辑出库单出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑出库单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除出库单
        /// </summary>
        /// <returns></returns>
        private string DelShippingList()
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
                string strResult = this._bllShippingList.ValidateAndDelShippingList(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除出库单成功。";
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
                result.Info = "删除出库单出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除出库单出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取出库单详细信息
        /// </summary>
        /// <returns></returns>
        private string GetShippingListById()
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
                ShippingListModel model = this._bllShippingList.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"ByHandUserName\":\""
                  + new BLL.Common.Admin().GetModel((int)model.ByHandUserID).Name + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取出库单信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取出库单信息出现异常：" + ex.Message, ex);
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