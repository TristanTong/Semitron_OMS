using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Common;
using Semitron_OMS.BLL.Common;
using System.Web.SessionState;
using Newtonsoft.Json.Converters;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// PreferenceConfig 的摘要说明
    /// </summary>
    public class PreferenceConfig : IHttpHandler, IRequiresSessionState
    {
        private log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(PreferenceConfig));//系统日志对象
        private PreferenceConfigBLL _bllSPServiceCode = new PreferenceConfigBLL();
        private AdminModel adminModel = null;
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

            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"].Trim();
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    case "AddOrUpdatePreferenceConfig":
                        context.Response.Write(AddOrUpdatePreferenceConfig());
                        break;
                    case "GetPreferenceConfigByCode":
                        context.Response.Write(GetPreferenceConfigByCode());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 新增SP下行内容
        /// </summary>
        /// <returns></returns>
        private string AddOrUpdatePreferenceConfig()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            PreferenceConfigModel model = new PreferenceConfigModel();
            PreferenceConfigBLL bll = new PreferenceConfigBLL();

            if (request.Form["PageCode"] == null || request.Form["SearchShow"] == null || request.Form["ColumnShow"] == null
                 || request.Form["GroupParam"] == null || request.Form["SearchParam"] == null)
            {
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }

            model.AdminID = adminModel.AdminID;

            //页面代码
            SQLOperateHelper.SetEntityFiledValue(model, "PageCode", request.Form["PageCode"]);
            //查询显示控件id
            SQLOperateHelper.SetEntityFiledValue(model, "SearchShow", request.Form["SearchShow"]);
            //列显示
            SQLOperateHelper.SetEntityFiledValue(model, "ColumnShow", request.Form["ColumnShow"]);
            //分组条件显示
            SQLOperateHelper.SetEntityFiledValue(model, "GroupParam", request.Form["GroupParam"]);
            //查询条件默认参数值
            SQLOperateHelper.SetEntityFiledValue(model, "SearchParam", request.Form["SearchParam"]);
            result.Info = "设定个人偏好失败。";
            if (bll.AddOrUpdatePreferenceConfig(model))
            {
                result.State = 1;
                result.Info = "设定个人偏好成功。";
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取对象方法
        /// </summary>
        private string GetPreferenceConfigByCode()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            PreferenceConfigBLL bll = new PreferenceConfigBLL();
            if (request.Form["PageCode"] == null || request.Form["PageCode"].ToString() == string.Empty)
            {
                result.Info = "ID参数异常。";
                return result.ToString();
            }
            PreferenceConfigModel model = bll.GetModel(adminModel.AdminID, request.Form["PageCode"].ToString().Trim());

            result.State = 0;
            result.Info = "出现异常，未能从数据库中找到对应记录。";
            try
            {
                if (model != null)
                {
                    result.State = 1;
                    string strResult = JsonConvert.SerializeObject(model, Formatting.Indented, new IsoDateTimeConverter());

                    return strResult;
                }
            }
            catch (Exception ex)
            {
                myLogger.Error("从数据库中找到对应用户的偏好设定出现异常" + ex.Message, ex);
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