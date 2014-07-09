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
    public partial class POManager : Semitron_OMS.UI.Admin.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //本页面名称
            PageCode = "OMS/POManager.aspx";
            //继承BasePage类将本页面的按钮集合传过去
            string curr = "btnSearch,btnAdd,btnEdit,btnDel,btnBindCustiomerOrder,btnGeneratePOPlan,btnConfirmSecond,btnView,btnViewCustiomerOrder,btnViewPOPlan";
            currButtonCodes = curr.Split(',');
            if (!IsPostBack)
            {
                //txtBeginTime.Value = DateTime.Now.AddMonths(-6).ToString("yyyy-MM") + "-01 00:00:00";
                txtBeginTime.Value = "2013-01-01 00:00:00";
                this.InitSltCorporation(this.sltCorporationID, true, true);
                this.InitSltCorporation(this.sltCorporationIDE, false, true);
                this.InitSltSupplier(this.sltSupplierID, true, true);
                this.InitSltSupplier(this.sltSupplierIDE, false, true);
                this.InitSltPaymentType(this.sltPaymentTermsE, false, true);
                this.InitSltState(this.sltState, true, ConstantValue.TableNames.PO);
                this.InitSltState(this.sltStateE, false, ConstantValue.TableNames.PO);

                AdminModel aModel = Session["Admin"] as AdminModel;
                if (aModel != null)
                    hfAdminInfo.Value = aModel.Username + "$" + aModel.Phone + "$" + string.Join(",", aModel.RoleID);
            }
        }
    }
}