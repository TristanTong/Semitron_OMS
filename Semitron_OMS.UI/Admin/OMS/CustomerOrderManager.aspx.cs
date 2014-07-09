using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.UI.Admin.OMS
{
    public partial class CustomerOrderManager : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "OMS/CustomerOrderManager.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch,btnAdd,btnEdit,btnView,btnCancel,btnConfirmFirst,btnOutStock,btnAssignBuyer";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                //txtBeginTime.Value = DateTime.Now.AddMonths(-6).ToString("yyyy-MM") + "-01 00:00:00";
                txtBeginTime.Value = "2013-01-01 00:00:00";
                this.InitSltPaymentType(this.sltPaymentTypeID, true, true);
                this.InitSltPaymentType(this.sltPaymentTypeIDE, false, true);
                this.InitSltCustomer(this.sltCustomerID, true, true);
                this.InitSltCustomer(this.sltCustomerIDE, false, true);
                this.InitSltState(this.sltState, true, ConstantValue.TableNames.CustomerOrder);
                this.InitSltState(this.sltStateE, false, ConstantValue.TableNames.CustomerOrder);
                this.InitSltCurrencyType(this.sltSaleStandardCurrencyE, false, false);
                this.InitSltCurrencyType(this.sltSaleRealCurrencyE, false, false);
                this.InitSltCorporation(this.sltCorporationIDE, false, false);
                this.InitSltRolesAdmin(this.sltAssignToInnerBuyer,
                   (int)Semitron_OMS.Common.Enum.EnumRoleID.InnerBuyer + ","
                   + (int)Semitron_OMS.Common.Enum.EnumRoleID.BuyerManager,
                   false, true);
                this.InitSltRolesAdmin(this.sltAssignToInnerBuyerE,
                    (int)Semitron_OMS.Common.Enum.EnumRoleID.InnerBuyer + ","
                    + (int)Semitron_OMS.Common.Enum.EnumRoleID.BuyerManager,
                    false, true);
                this.InitSltBrand(this.sltMFGE, false, true);

                AdminModel aModel = Session["Admin"] as AdminModel;
                if (aModel != null)
                    hfAdminInfo.Value = aModel.Username + "$" + aModel.Phone + "$" + string.Join(",", aModel.RoleID);
            }
        }
    }
}