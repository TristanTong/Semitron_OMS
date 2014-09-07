using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.Common;

namespace Semitron_OMS.UI.Admin.OMS
{
    public partial class CustomerOrderDetailManager : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "OMS/CustomerOrderDetailManager.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                //txtBeginTime.Value = DateTime.Now.AddMonths(-6).ToString("yyyy-MM") + "-01 00:00:00";
                txtBeginTime.Value = "2013-01-01 00:00:00";
                this.InitSltCustomer(this.sltCustomerID, true, true);
                this.InitSltState(this.sltOrderState, true, ConstantValue.TableNames.CustomerOrder);
            }
        }
    }
}