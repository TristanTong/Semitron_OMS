using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.UI.Admin.Base;

namespace Semitron_OMS.UI.Admin.Sys
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class RoleManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "SYS/RoleManage.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "SelectBtn,btnAdd,btnEdit,btnDel,btnBindArea,btnArea";
            currButtonCodes = curr.Split(',');
        }
    }
}