using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.Model.Common;
using Newtonsoft.Json;
using Semitron_OMS.Model.Site;
using System.IO;

namespace Semitron_OMS.UI.Handle.Site
{
    /// <summary>
    /// DownloadHandle 的摘要说明
    /// </summary>
    public class DownloadHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //下载中心数据对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(DownloadHandle));
        Semitron_OMS.BLL.Site.DownloadCenterBLL _bllDownloadCenter = new Semitron_OMS.BLL.Site.DownloadCenterBLL();
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
                    //获取下载中心数据列表
                    case "GetDownloadCenter":
                        context.Response.Write(GetDownloadCenter());
                        break;
                    //新增下载中心数据
                    case "AddDownloadCenter":
                        context.Response.Write(AddDownloadCenter(context));
                        break;
                    //修改下载中心数据
                    case "EditDownloadCenter":
                        context.Response.Write(EditDownloadCenter(context));
                        break;
                    //删除下载中心数据
                    case "DelDownloadCenter":
                        context.Response.Write(DelDownloadCenter());
                        break;
                    //获取下载中心数据详细信息
                    case "GetDownloadCenterById":
                        context.Response.Write(GetDownloadCenterById());
                        break;
                    //生成编码与HTML文件名称
                    case "GenerateCodeAndPath":
                        context.Response.Write(GenerateCodeAndPath());
                        break;
                    //获取HTML文件源代码
                    case "GetHtmlFileCode":
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(GetHtmlFileCode(context));
                        break;
                    //保存HTML文件源代码
                    case "SaveHtmlFileCode":
                        context.Response.Write(SaveHtmlFileCode(context));
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取下载中心数据列表
        /// </summary>
        /// <returns></returns>
        private string GetDownloadCenter()
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
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("IsRecommend", _request.Form["IsRecommend"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Lang", _request.Form["Lang"], ConditionEnm.Equal));
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Name", _request.Form["Name"], ConditionEnm.AllLike));

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
                DataSet ds = _bllDownloadCenter.GetDownloadCenterPageData(searchInfo, out o_RowsCount);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "客户机IP:" + _request.UserHostAddress + "，获取下载中心数据信息出现异常:" + ex.Message, ex);
            }
            string strCols = DataUtility.GetPageFormValue(_request.Form["colNames"], string.Empty);
            return JsonJqgrid.JsonForJqgrid(dt.SortDataTableCols(strCols), searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 获得新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetNewModel(DownloadCenterModel model)
        {
            PageResult result = new PageResult();
            if (_request.Form["Code"] == null
                || _request.Form["Name"] == null
                || _request.Form["Lang"] == null
                || _request.Form["SK"] == null
                || _request.Form["IsRecommend"] == null
                || _request.Form["Description"] == null)
            {

                return "系统错误,参数获取异常。";
            }
            SQLOperateHelper.SetEntityFiledValue(model, "Code", _request.Form["Code"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Name", _request.Form["Name"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Lang", _request.Form["Lang"]);
            SQLOperateHelper.SetEntityFiledValue(model, "SK", _request.Form["SK"]);
            SQLOperateHelper.SetEntityFiledValue(model, "IsRecommend", _request.Form["IsRecommend"]);
            SQLOperateHelper.SetEntityFiledValue(model, "Description", _request.Form["Description"]);
            return "OK";
        }

        /// <summary>
        /// 新增下载中心数据
        /// </summary>
        /// <returns></returns>
        private string AddDownloadCenter(HttpContext context)
        {
            PageResult result = new PageResult();
            DownloadCenterModel model = new DownloadCenterModel();
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
                string strResult = this._bllDownloadCenter.ValidateAndAdd(model);
                if (strResult.StartsWith("OK"))
                {
                    result.State = 1;
                    result.Remark = strResult.Replace("OK", "");
                    result.Info = "新增下载中心数据成功！";
                    //保存附件列表
                    string strFilePath = context.Server.MapPath("../..");
                    string strFileUrl = DataUtility.GetPageFormValue(_request.Form["AttachmentFiles"], string.Empty).TrimEnd('|');
                    if (!new BLL.OMS.AttachmentBLL().BatchSaveFiles(strFilePath,
                        ConstantValue.TableAttachment.ColumnObjType.DownloadCenter,
                        strResult.Replace("OK", ""), strFileUrl, _adminModel.Username))
                    {
                        result.Info += "但保存上传文件失败，请重新上传。";
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
                result.Info = "新增下载中心数据出现异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + ",客户机IP:" +
                    _request.UserHostAddress + "，新增下载中心数据出现异常:" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改下载中心数据
        /// </summary>
        /// <returns></returns>
        private string EditDownloadCenter(HttpContext context)
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
                DownloadCenterModel model = this._bllDownloadCenter.GetModel(iId);
                string strGetResult = this.GetNewModel(model);
                if (strGetResult != "OK")
                {
                    result.State = 0;
                    result.Info = strGetResult;
                    return result.ToString();
                }
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateUser", _adminModel.Username);
                SQLOperateHelper.SetEntityFiledValue(model, "UpdateTime", DateTime.Now);
                string strResult = this._bllDownloadCenter.ValidateAndUpdate(model);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Remark = model.ID.ToString();
                    result.Info = "编辑下载中心数据成功！";
                    //保存附件列表
                    string strFilePath = context.Server.MapPath("../..");
                    string strFileUrl = DataUtility.GetPageFormValue(_request.Form["AttachmentFiles"], string.Empty).TrimEnd('|');
                    if (!new BLL.OMS.AttachmentBLL().BatchSaveFiles(strFilePath,
                        ConstantValue.TableAttachment.ColumnObjType.DownloadCenter,
                        model.ID.ToString(), strFileUrl, _adminModel.Username))
                    {
                        result.Info += "但保存上传文件失败，请重新上传。";
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
                result.Info = "编辑下载中心数据出现异常，请联系管理员！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，编辑下载中心数据出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除下载中心数据
        /// </summary>
        /// <returns></returns>
        private string DelDownloadCenter()
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
                string strResult = this._bllDownloadCenter.ValidateAndDelDownloadCenter(iId);
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除下载中心数据成功。";
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
                result.Info = "删除下载中心数据出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除下载中心数据出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取下载中心数据详细信息
        /// </summary>
        /// <returns></returns>
        private string GetDownloadCenterById()
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
                DownloadCenterModel model = this._bllDownloadCenter.GetModel(iId);
                string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                strResult = strResult.Substring(0, strResult.Length - 1) + ",\"AttachmentFiles\":\""
                    + new BLL.OMS.AttachmentBLL().GetUrlListByObj(ConstantValue.TableAttachment.ColumnObjType.DownloadCenter,
                    model.ID.ToString()).Replace('/', '*') + "\"}";
                return strResult;
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取下载中心数据详细信息出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取下载中心数据详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成编码与HTML文件名称
        /// </summary>
        /// <returns></returns>
        private string GenerateCodeAndPath()
        {
            PageResult result = new PageResult();
            result.Info = "无法生成编码！";

            try
            {
                int iMax = this._bllDownloadCenter.GetMaxId();
                DownloadCenterModel model = new DownloadCenterModel();
                //首次生成
                if (iMax == 1)
                {
                    model.Code = "D0001";
                }
                else
                {
                    model = this._bllDownloadCenter.GetModel(iMax - 1);
                }
                if (model != null && !string.IsNullOrEmpty(model.Code))
                {
                    result.Info = "D" + (int.Parse(model.Code.Remove(0, 1)) + 1).ToString().PadLeft(4, '0');
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

        /// <summary>
        /// 获取HTML文件源代码
        /// </summary>
        /// <returns></returns>
        private string GetHtmlFileCode(HttpContext context)
        {
            PageResult result = new PageResult();
            result.Info = "无法获取HTML文件源代码！";
            string strId = DataUtility.GetPageFormValue(_request.Form["Id"], string.Empty);
            string strLang = DataUtility.GetPageFormValue(_request.Form["Lang"], string.Empty);

            if (strId == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strFilePath = string.Empty;
                if (strLang != "cn")
                {
                    strFilePath = context.Server.MapPath("../../" + strLang + "/DownloadCenter") + "\\" + strId + ".htm";
                }
                else
                {
                    strFilePath = context.Server.MapPath("../../cn/DownloadCenter") + "\\" + strId + ".htm";
                }
                string strHtmlCode = ReadTxtFile.ReadFileString(strFilePath);
                if (!string.IsNullOrEmpty(strHtmlCode))
                {
                    //result.State = 1;
                    //result.Remark = strHtmlCode;
                    return strHtmlCode;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取HTML文件源代码出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取HTML文件源代码出现异常：" + ex.Message, ex);
            }
            return result.ToString();
        }

        /// <summary>
        /// 保存HTML文件源代码
        /// </summary>
        /// <returns></returns>
        private string SaveHtmlFileCode(HttpContext context)
        {
            PageResult result = new PageResult();
            result.Info = "无法保存HTML文件源代码！";
            string strId = DataUtility.GetPageFormValue(_request.Form["Id"], string.Empty);
            string strFileCode = DataUtility.GetPageFormValue(_request.Form["FileCode"], string.Empty);
            string strLang = DataUtility.GetPageFormValue(_request.Form["Lang"], string.Empty);
            if (strId == string.Empty || strFileCode == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strFilePath = string.Empty;
                if (strLang != "cn")
                {
                    strFilePath = context.Server.MapPath("../../" + strLang + "/DownloadCenter") + "\\" + strId + ".htm";
                }
                else
                {
                    strFilePath = context.Server.MapPath("../../cn/DownloadCenter") + "\\" + strId + ".htm";
                }
                try
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                    {
                        //fs.BeginWrite(System.Text.Encoding.UTF8.GetBytes(strFileCode), 0, int.MaxValue, null, null);
                        //fs.EndWrite(null);
                        Byte[] arrByte = System.Text.Encoding.UTF8.GetBytes(strFileCode);
                        fs.Write(arrByte, 0, arrByte.Length);
                    }
                }
                catch
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(strFilePath, false, System.Text.Encoding.GetEncoding(-0));
                    sw.AutoFlush = true;
                    System.IO.TextWriter aWriter = System.IO.TextWriter.Synchronized(sw);
                    aWriter.Write(strFileCode);
                }

                if (System.IO.File.Exists(strFilePath))
                {
                    result.State = 1;
                    result.Info = "保存HTML源代码文件成功。";
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "保存HTML文件源代码出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，保存HTML文件源代码出现异常：" + ex.Message, ex);
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