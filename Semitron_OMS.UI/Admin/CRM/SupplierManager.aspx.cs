using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Semitron_OMS.UI.Admin.CRM
{
    public partial class SupplierManager : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "CRM/SupplierManager.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch,btnAdd,btnEdit,btnDelete,btnBindInnerBuyer";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                //txtBeginTime.Value = DateTime.Now.AddMonths(-6).ToString("yyyy-MM") + "-01 00:00:00";
                txtBeginTime.Value = "2013-01-01 00:00:00";
            }
        }
    }
}