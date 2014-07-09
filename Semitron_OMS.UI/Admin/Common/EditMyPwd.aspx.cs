using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.UI.Admin.Common
{
    /// <summary>
    /// 修改个人密码
    /// </summary>
    public partial class EditMyPwd : Semitron_OMS.UI.Admin.Base.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "Common/EditMyPwd.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                if (Session["Admin"] == null)
                {
                    Response.Write("<script>top.location.href = '/Admin/Login.aspx'</script>");
                    return;
                }
                AdminModel am = Session["Admin"] as AdminModel;
                txtUserName.Text = am.Username;
            }
        }
        protected void Affirm_Click(object sender, EventArgs e)
        {

        }
    }
}