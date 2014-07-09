using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Semitron_OMS.UI.Admin.UserControl
{
    public partial class UploadifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strQuery = string.Empty;
                strQuery = Request.QueryString.Get("FileExt");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.FileExt = strQuery;
                }
                strQuery = Request.QueryString.Get("FileDesc");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.FileDesc = strQuery;
                }
                strQuery = Request.QueryString.Get("ButtonText");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.ButtonText = strQuery;
                }
                strQuery = Request.QueryString.Get("IsMulti");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.IsMulti = bool.Parse(strQuery);
                }
                strQuery = Request.QueryString.Get("IsAuto");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.IsAuto = bool.Parse(strQuery);
                }
                strQuery = Request.QueryString.Get("SimUploadLimit");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.SimUploadLimit = int.Parse(strQuery);
                }
                strQuery = Request.QueryString.Get("QueueSizeLimit");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.QueueSizeLimit = int.Parse(strQuery);
                }
                strQuery = Request.QueryString.Get("Folder");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.Folder = strQuery.Replace('$', '/');
                }
                strQuery = Request.QueryString.Get("IsAutoDisappear");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.IsAutoDisappear = bool.Parse(strQuery);
                }
                strQuery = Request.QueryString.Get("SizeLimit");
                if (!string.IsNullOrEmpty(strQuery))
                {
                    UploadifyControl1.SizeLimit = long.Parse(strQuery);
                }
            }
        }
    }
}