using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.Common;

namespace Semitron_OMS.UI.Admin.OMS
{
    public partial class POPlanManager : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "OMS/POPlanManager.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch,btnAdd,btnEdit,btnCancel,btnView,btnConfirmPrice,btnQCConfirm";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                //txtBeginTime.Value = DateTime.Now.AddMonths(-6).ToString("yyyy-MM") + "-01 00:00:00";
                txtBeginTime.Value = "2013-01-01 00:00:00";
                this.InitSltSupplier(this.sltSupplierID, true, true);
                this.InitSltSupplier(this.sltSupplierIDE, false, true);
                this.InitSltPaymentType(this.sltPaymentTypeID, true, true);
                this.InitSltPaymentType(this.sltVendorPaymentTypeIDE, false, true);
                this.InitSltState(this.sltState, true, ConstantValue.TableNames.POPlan);
                this.InitSltState(this.sltStateE, false, ConstantValue.TableNames.POPlan);
                this.InitSltCurrencyType(this.sltBuyRealCurrencyE, false, false);
                this.InitSltCurrencyType(this.sltBuyStandardCurrencyE, false, false);
                this.InitSltBrand(this.sltMFGE, false, true);
            }
        }
    }
}