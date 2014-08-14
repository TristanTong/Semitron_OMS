using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semitron_OMS.UI.Handle.Common
{
    /// <summary>
    /// AttachmentHandle 的摘要说明
    /// </summary>
    public class AttachmentHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //附件对象
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(AttachmentHandle));
        Semitron_OMS.BLL.OMS.AttachmentBLL _bllAttachment = new Semitron_OMS.BLL.OMS.AttachmentBLL();
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
                    //删除附件
                    case "DelAttachment":
                        context.Response.Write(DelAttachment());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        private string DelAttachment()
        {
            PageResult result = new PageResult();
            string strUrl = DataUtility.GetPageFormValue(_request.Form["Url"], string.Empty);
            if (strUrl == string.Empty)
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                string strResult = this._bllAttachment.ValidateAndDelAttachment(strUrl.Replace('*', '/'));
                if (strResult == "OK")
                {
                    result.State = 1;
                    result.Info = "删除附件成功。";
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
                result.Info = "删除附件出现异常！";
                _myLogger.Error("登陆用户名：" + _adminModel.Username + "，客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，删除附件出现异常：" + ex.Message, ex);
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