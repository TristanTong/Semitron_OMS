using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.UI.Admin.Sys
{
    /// <summary>
    /// 操作日志管理
    /// </summary>
    public partial class OperationsLog : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "Sys/OperationsLog.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                txtBeginTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
                AdminModel adminModel = HttpContext.Current.Session["Admin"] as AdminModel;
                if (adminModel == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>alert(\"由于您长时间未操作，您的登陆已失效。请重新登陆。\");top.location.href = \"/Admin/Login.aspx\";</script>");
                    return;
                }
                List<UserRoleModel> userRoleModel = new Semitron_OMS.BLL.Common.UserRole().GetModelList("AdminID=" + adminModel.AdminID);
                if (userRoleModel != null && userRoleModel.Count > 0 && userRoleModel[0].RoleID == int.Parse(Semitron_OMS.Common.ConfigHelper.GetConfigString("SuperAdminRoleId")))//为超级管理员
                {
                    this.tdOperatePerson.Visible = true;
                    this.OperatePerson.Visible = true;

                }
            }
        }
    }
}