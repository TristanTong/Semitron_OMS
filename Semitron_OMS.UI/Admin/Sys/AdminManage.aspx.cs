using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.UI.Admin.Base;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.UI.Admin.Sys
{
    /// <summary>
    /// 管理员管理
    /// </summary>
    public partial class AdminManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面编码。
            PageCode = "SYS/AdminManage.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "SelectBtn,btnAdd,btnEdit,btnBindArea,btnArea,btnDel,btnUpdatePwd,btnPower,btnCheck";
            currButtonCodes = curr.Split(',');
        }
    }
}