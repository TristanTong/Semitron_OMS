using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Model;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.BLL;
using System.Data;
using Semitron_OMS.Model.Common;
using Semitron_OMS.BLL.Common;
using System.Web.UI.HtmlControls;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Permission;
using Semitron_OMS.BLL.OMS;
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.UI.Admin.Base
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            // 
            //TODO: 在此处添加构造函数逻辑 
            // 
        }
        //protected string ModelCode { get; set; } //模块名称
        protected string PageCode { get; set; } //页面名称
        protected string[] currButtonCodes { get; set; }//按钮集合
        protected string currDataCodes { get; set; }
        protected string HidenAllCodeId { get; set; }//所有数据集集合CODE的传值隐藏域ID,取得当前PageCode下所有的权限数据(div显示查询条件及Column显示表格列)
        protected string HidenId { get; set; }//有权限的数据集CODE传值隐藏域ID
        protected string HidenMenuContext { get; set; }//右键菜单

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Permission ps = new Permission();

            if (Session["Admin"] == null)
            {
                string strUser = string.Empty;
                string strPwd = string.Empty;
                HttpCookie Cookie = Semitron_OMS.CommonWeb.CookiesHelper.GetCookie(ConstantValue.CookieKeys.LoginAdminModel);
                if (Cookie != null)
                {
                    strUser = Cookie.Values["uName"];
                    strPwd = Cookie.Values["uPassword"];
                }
                if (!string.IsNullOrEmpty(strUser) && !string.IsNullOrEmpty(strPwd))
                {
                    Login(strUser, strPwd, false);
                }
            }

            if (Session["Admin"] == null)
            {
                Semitron_OMS.CommonWeb.MessageBox.ShowAndRedirects(this.Page, "由于您长时间未操作，您的登陆已失效。请重新登陆。", "/Admin/Login.aspx");
                return;
            }
            AdminModel adminModel = Session["Admin"] as AdminModel;
            if (adminModel == null)
            {
                Semitron_OMS.CommonWeb.MessageBox.ShowAndRedirects(this.Page, "由于您长时间未操作，您的登陆已失效。请重新登陆。", "/Admin/Login.aspx");
                return;
            }
            string strSubSystems = System.Configuration.ConfigurationManager.AppSettings["ParenSystem"].ToString();
            int iSubSystem = -1;
            //得到子系统权限父节点下的所有子权限id
            foreach (string strSys in strSubSystems.Split(','))
            {
                iSubSystem = Convert.ToInt32(strSys);

                //当前继承页所传递的页面代码是否在权限系统中
                if (PermissionUtility.IsExistPageCode(adminModel.PerModule.SubSystemPers[iSubSystem], PageCode))
                {
                    //获得数据库中当前继承页的所有数据集权限集合
                    DataSet ds = ps.GetList(1000, "LinkUrl = '" + PageCode + "' AND Type = 4 AND AvailFlag = 1", "SK");
                    string strDataCodes = string.Empty;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            strDataCodes += dr["Code"].ToString().Trim() + ",";
                        }
                        if (strDataCodes.EndsWith(","))
                        {
                            strDataCodes = strDataCodes.Substring(0, strDataCodes.Length - 1);
                        }
                        //当前继承页的所有数据集CODE传值隐藏域
                        if (HidenAllCodeId != null && HidenAllCodeId != string.Empty)
                        {
                            HiddenField hid = this.Page.FindControl(HidenAllCodeId) as HiddenField;
                            if (hid != null) hid.Value = strDataCodes;
                        }
                    }

                    //遍历分配到当前用户的按钮、数据集和右键菜单

                    //将加入权限控制的所有按钮属性先设置为不可见。
                    if (currButtonCodes != null)
                    {
                        for (int c = 0; c < currButtonCodes.Length; c++)
                        {
                            Control control = this.FindControl(currButtonCodes[c]);
                            if (control is Button)
                                (control as Button).Visible = false;
                        }
                    }
                    //有权限的按钮Code
                    List<ButtonPer> lstbtnPer = PermissionUtility.GetButtonPer(adminModel.PerModule.SubSystemPers[iSubSystem], PageCode);
                    foreach (ButtonPer btnPer in lstbtnPer)
                    {
                        Control control = this.FindControl(btnPer.Code);
                        //是否为按钮
                        if (control is Button)
                            (control as Button).Visible = true;
                    }

                    string Viewarray = string.Empty;
                    string Datasrouce = string.Empty; //记录分配到当前用户的数据集权限Code 
                    //有权限的数据集CODE传值隐藏域
                    List<DataSetPer> lstDsPer = PermissionUtility.GetDataSetPer(adminModel.PerModule.SubSystemPers[iSubSystem], PageCode);
                    //当前继承页的所有数据集CODE传值隐藏域
                    foreach (DataSetPer dsp in lstDsPer)
                    {
                        Datasrouce += dsp.Code.Trim() + ",";
                    }
                    if (Datasrouce.EndsWith(","))
                    {
                        Datasrouce = Datasrouce.Substring(0, Datasrouce.Length - 1);
                    }
                    if (Datasrouce != string.Empty)
                    {
                        if (HidenId != null && HidenId != string.Empty)
                        {
                            HiddenField hid = this.Page.FindControl(HidenId) as HiddenField;
                            if (hid != null) hid.Value = Datasrouce;
                        }
                    }

                    //有权限的右键菜单CODE传值隐藏域
                    List<RightMenuPer> lstRmPer = PermissionUtility.GetRightMenuPer(adminModel.PerModule.SubSystemPers[iSubSystem], PageCode);
                    foreach (RightMenuPer rmp in lstRmPer)
                    {
                        Viewarray += rmp.Name.Trim() + ":" + rmp.Code.Trim() + ",";
                    }
                    if (Viewarray.EndsWith(","))
                    {
                        Viewarray = Viewarray.Substring(0, Viewarray.Length - 1);
                    }
                    if (HidenMenuContext != null && HidenMenuContext != string.Empty)
                    {
                        HiddenField hid = this.Page.FindControl(HidenMenuContext) as HiddenField;
                        if (hid != null) hid.Value = Viewarray;
                    }
                }
            }
        }

        /// <summary>
        /// session失效时，如果cookie未失效，则重登陆
        /// </summary>
        private string Login(string username, string pwd, bool bFirst)
        {
            Semitron_OMS.BLL.Common.Admin ad = new Semitron_OMS.BLL.Common.Admin();
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            string IP = request.UserHostAddress;
            string ParentSystem = System.Configuration.ConfigurationManager.AppSettings["ParenSystem"].ToString();
            AdminModel model = new AdminModel();
            try
            {
                model = ad.Login(ParentSystem, IP, username, pwd, out  result);
                if ((model == null || model.AdminID <= 0) && string.IsNullOrEmpty(result.Info))
                {
                    result.State = 0;
                    result.Info = "对不起，程序内部错误，无法登陆。";
                    return result.ToString();
                }

                if (model != null && model.AdminID > 0)
                {
                    Session["Admin"] = model;
                    if (bFirst || CookiesHelper.GetCookie(ConstantValue.CookieKeys.LoginAdminModel) == null)
                    {
                        //增加Cookies，当下一次超时时，自动登陆
                        CookiesHelper.AddLoginCookie(ConstantValue.CookieKeys.LoginAdminModel, username, pwd);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(result.Info))
                    {
                        result.State = 0;
                        result.Info = "登陆失败，未知错误。";
                    }
                }
            }
            catch (Exception)
            {
                result.State = 0;
                result.Info = "登陆异常，请联系管理员。";
            }
            return result.ToString();
        }

        /// <summary>
        /// 状态下拉框加载数据
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        protected void InitSltState(HtmlSelect slt, bool isShowSelect, string strTableName)
        {
            DataTable dt = new CommonTableBLL().GetDataTableByCache(strTableName);//公用表加载缓存数据
            DataRow[] drValid = dt.Select("FieldID='State'", "Desc Asc,Key Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }

            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];
                string strSort = drCurrent["Desc"].ToString();
                //ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["Value"].ToString(), drCurrent["Key"].ToString());
                ListItem item = new ListItem(drCurrent["Value"].ToString(), drCurrent["Key"].ToString());
                item.Attributes["title"] = drCurrent["Value"].ToString();
                slt.Items.Add(item);
            }
        }

        /// <summary>
        /// 客户表下拉框加载数据
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        /// <param name="isShowAll">是否显示所有</param>
        /// <param name="isShowSelect">是否显示请选择</param>
        protected void InitSltCustomer(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dtCustomer = new Semitron_OMS.BLL.CRM.CustomerBLL().GetDataTableByCache();//使用缓存
            DataTable dt = dtCustomer.Clone();
            AdminModel am = Session["Admin"] as AdminModel;
            if (am == null || am.PerModule == null)
            {
                return;
            }
            //是否分配查看所有供应商权限
            if (PermissionUtility.IsExistDataSetPer(am.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_CUSTOMER_VIEW))
            {
                dt = dtCustomer;
            }
            else
            {
                //只取出关联的供应商数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindCustomerBLL().GetDataTableByCache(am.AdminID);//使用缓存
                dtCustomer.AsEnumerable().ForEach(r =>
                {
                    foreach (DataRow dr in dtBind.Rows)
                    {
                        if (r["ID"].ToString() == dr["CustomerID"].ToString())
                        {
                            dt.Rows.Add(r.ItemArray);
                            break;
                        }
                    }
                });
            }

            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,CustomerName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,CustomerName Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];
                string strSort = drCurrent["SK"].ToString();
                ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["CustomerName"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["CustomerName"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    string strSort = drCurrent["SK"].ToString();
                    ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["CustomerName"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["CustomerName"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 供应商表下拉框加载数据
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        /// <param name="isShowAll">是否显示所有</param>
        /// <param name="isShowSelect">是否显示请选择</param>
        protected void InitSltSupplier(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dtSupplier = new Semitron_OMS.BLL.CRM.SupplierBLL().GetDataTableByCache();//使用缓存
            DataTable dt = dtSupplier.Clone();
            AdminModel am = Session["Admin"] as AdminModel;
            if (am == null || am.PerModule == null)
            {
                return;
            }
            //是否分配查看所有供应商权限
            if (PermissionUtility.IsExistDataSetPer(am.PerModule, ConstantValue.SystemConst.DATA_PERMISSION, ConstantValue.SystemConst.ALL_SUPPLIER_VIEW))
            {
                dt = dtSupplier;
            }
            else
            {
                //只取出关联的供应商数据
                DataTable dtBind = new Semitron_OMS.BLL.OMS.AdminBindSupplierBLL().GetDataTableByCache(am.AdminID);//使用缓存
                dtSupplier.AsEnumerable().ForEach(r =>
                {
                    foreach (DataRow dr in dtBind.Rows)
                    {
                        if (r["ID"].ToString() == dr["SupplierID"].ToString())
                        {
                            dt.Rows.Add(r.ItemArray);
                            break;
                        }
                    }
                });
            }
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,SupplierName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,SupplierName Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];
                string strSort = drCurrent["SK"].ToString();
                ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["SupplierName"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["SupplierName"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    string strSort = drCurrent["SK"].ToString();
                    ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["SupplierName"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["SupplierName"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 公司表下拉框加载数据
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        /// <param name="isShowAll">是否显示所有</param>
        /// <param name="isShowSelect">是否显示请选择</param>
        protected void InitSltCorporation(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dt = new CorporationBLL().GetDataTableByCache();//使用缓存
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,CompanyName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,CompanyName Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];
                string strSort = drCurrent["SK"].ToString();
                ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["CompanyName"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["CompanyName"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    string strSort = drCurrent["SK"].ToString();
                    ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["CompanyName"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["CompanyName"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 品牌表下拉框加载数据
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        /// <param name="isShowAll">是否显示所有</param>
        /// <param name="isShowSelect">是否显示请选择</param>
        protected void InitSltBrand(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dt = new BrandBLL().GetDataTableByCache();//使用缓存
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,BrandName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,BrandName Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];
                string strSort = "";//drCurrent["SK"].ToString();
                ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["BrandName"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["BrandName"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    string strSort = "";//drCurrent["SK"].ToString();
                    ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["BrandName"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["BrandName"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 获取付款方式
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        protected void InitSltPaymentType(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dt = new PaymentTypeBLL().GetDataTableByCache();//使用缓存
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,PaymentType Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,PaymentType Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];
                string strSort = drCurrent["SK"].ToString();
                ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["PaymentType"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["PaymentType"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    string strSort = drCurrent["SK"].ToString();
                    ListItem item = new ListItem((string.IsNullOrEmpty(strSort) ? "" : (strSort + "-")) + drCurrent["PaymentType"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["PaymentType"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 获取货币种类
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        protected void InitSltCurrencyType(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dt = new CurrencyTypeBLL().GetDataTableByCache();//使用缓存
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,ShortName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,ShortName Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];

                ListItem item = new ListItem(drCurrent["ShortName"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["ShortName"].ToString() + "-" + drCurrent["CurrencyName"].ToString() + "-" + drCurrent["CountryName"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    ListItem item = new ListItem(drCurrent["ShortName"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["ShortName"].ToString() + "-" + drCurrent["CurrencyName"].ToString() + "-" + drCurrent["CountryName"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 获取产品种类
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        protected void InitSltProductType(HtmlSelect slt, bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dt = new Semitron_OMS.BLL.Common.ProductTypeBLL().GetDataTableByCache();//使用缓存
            DataRow[] drValid = dt.Select("AvailFlag=1", "SK Asc,TypeName Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "SK Asc,TypeName Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];

                ListItem item = new ListItem(drCurrent["TypeName"].ToString(), drCurrent["ID"].ToString());
                item.Attributes["title"] = drCurrent["TypeName"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    ListItem item = new ListItem(drCurrent["TypeName"].ToString(), drCurrent["ID"].ToString());
                    item.Attributes["title"] = drCurrent["TypeName"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 根据指定的角色ID获取相应用户名称
        /// </summary>
        /// <param name="slt">下拉框控件</param>
        /// <param name="strRoleIds">角色IDs</param>
        /// <param name="isShowAll">是否显示所有数据</param>
        /// <param name="isShowSelect">是否显示--请选择--</param>
        protected void InitSltRolesAdmin(System.Web.UI.HtmlControls.HtmlSelect slt, string strRoleIds,
            bool isShowAll, bool isShowSelect)
        {
            //分别获取有效和无效的数据。
            DataTable dt = new Semitron_OMS.BLL.Common.Admin().GetByRoleIds(strRoleIds);//使用缓存
            DataRow[] drValid = dt.Select("AvailFlag=1", "Name Asc");
            DataRow[] drUnValid = dt.Select("AvailFlag=0", "Name Asc");

            //先清空所有的下拉框内容，避免在页面上的数据也填充进来。
            slt.Items.Clear();
            if (isShowSelect)
            {
                slt.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－有效－－", "-1"));
            }
            for (int i = 0; i < drValid.Count(); i++)
            {
                DataRow drCurrent = drValid[i];

                ListItem item = new ListItem(drCurrent["Name"].ToString(), drCurrent["AdminID"].ToString());
                item.Attributes["title"] = drCurrent["Name"].ToString();
                slt.Items.Add(item);
            }
            if (isShowAll)
            {
                slt.Items.Add(new ListItem("－－无效－－", "-2"));
                for (int i = 0; i < drUnValid.Count(); i++)
                {
                    DataRow drCurrent = drUnValid[i];
                    ListItem item = new ListItem(drCurrent["Name"].ToString(), drCurrent["AdminID"].ToString());
                    item.Attributes["title"] = drCurrent["Name"].ToString();
                    slt.Items.Add(item);
                }
            }
        }

        private string GetDiscription(string value)
        {
            if (value.Length > 8)
                value = value.Substring(0, 8) + "...";
            return value;
        }
    }
}