using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.Common;

namespace Semitron_OMS.UI.Admin.OMS
{
    public partial class OrderFeeManager : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "OMS/POManager.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch,btnEdit,btnDelete";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                //txtBeginTime.Value = DateTime.Now.AddMonths(-6).ToString("yyyy-MM") + "-01 00:00:00";
                txtBeginTime.Value = "2013-01-01 00:00:00";
                this.InitSltCustomer(this.sltCustomerID, true, true);
                this.InitSltCustomer(this.sltCustomerIDE, false, true);
                this.InitSltSupplier(this.sltSupplierID, true, true);
                this.InitSltSupplier(this.sltSupplierIDE, false, true);
                this.InitSltCurrencyType(this.sltSaleRealCurrencyE, false, true);
                this.InitSltCurrencyType(this.sltSaleStandardCurrencyE, false, false);
                this.InitSltCurrencyType(this.sltBuyRealCurrencyE, false, true);
                this.InitSltCurrencyType(this.sltBuyStandardCurrencyE, false, false);
                this.InitSltCurrencyType(this.sltTotalFeeCurrencyUnitE, false, true);
                this.InitSltCurrencyType(this.sltIncomeStandardCurrencyE, false, false);
                this.InitSltCurrencyType(this.sltRealInCurrencyUnitE, false, true);
                this.InitSltCurrencyType(this.sltStandardRealInCurrencyUnitE, false, false);
                this.InitSltPaymentType(this.sltPaymentTypeIDE, false, true);
                this.InitSltPaymentType(this.sltVendorPaymentTypeIDE, false, true);
                this.InitSltCorporation(this.sltCorporationID, true, true);
                this.InitSltCorporation(this.sltSupplierCorporationIDE, true, true);
            }
        }
    }
}