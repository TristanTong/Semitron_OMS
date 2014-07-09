using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Semitron_OMS.UI.Admin.Base;
using Semitron_OMS.Model;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using Semitron_OMS.Model.Permission;
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.UI.Admin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admin"] == null)
                {
                    MessageBox.ResponseScript(Page, "top.location.href='/Admin/Login.aspx'");
                    //MessageBox.ShowAndRedirects(this.Page, "由于您长时间未操作，您的登陆已实效。请重新登陆。", "Login.aspx");
                    return;
                }
                string urlParam = string.Empty;
                if (Request.Params["linkurl"] != null && Request.Params["linkurl"].ToString() != string.Empty)
                {
                    this.hidUrl.Value = Request.Params["linkurl"];
                }
                if (Request.Params["SP"] != null && Request.Params["SP"].ToString() != string.Empty)
                {
                    urlParam += "SP=" + Server.UrlEncode(Request.Params["SP"]) + "&";
                }
                if (Request.Params["ServiceCode"] != null && Request.Params["ServiceCode"].ToString() != string.Empty)
                {
                    urlParam += "ServiceCode=" + Server.UrlEncode(Request.Params["ServiceCode"]) + "&";
                }
                if (Request.Params["FeeCodeId"] != null && Request.Params["FeeCodeId"].ToString() != string.Empty)
                {
                    urlParam += "FeeCode=" + Server.UrlEncode(Request.Params["FeeCodeId"]) + "&";
                }
                if (Request.Params["FeeType"] != null && Request.Params["FeeType"].ToString() != string.Empty)
                {
                    urlParam += "FeeType=" + Server.UrlEncode(Request.Params["FeeType"]) + "&";
                }
                if (Request.Params["CPName"] != null && Request.Params["CPName"].ToString() != string.Empty)
                {
                    urlParam += "CP=" + Server.UrlEncode(Request.Params["CPName"]) + "&";
                }
                if (Request.Params["Router"] != null && Request.Params["Router"].ToString() != string.Empty)
                {
                    urlParam += "Router=" + Server.UrlEncode(Request.Params["Router"]) + "&";
                }
                if (urlParam.Length > 0 && urlParam.LastIndexOf("&") == (urlParam.Length - 1))
                {
                    urlParam = urlParam.Substring(0, urlParam.Length - 1);
                }
                this.HidUrlID.Value = urlParam;
                lbnVersionInfo.Text = ConfigHelper.GetConfigString("VersionNumber");

                AdminModel am = Session["Admin"] as AdminModel;
                int i = 0;
                string li = "";
                string UlSpli = "";
                string UlSplie = "";
                string ul1 = "";
                string ul2 = "";
                string ul3 = "";
                string ul4 = "";
                string ul5 = "";
                string ul6 = "";
                foreach (ModulePer modPer in PermissionUtility.GetModulePerALL(am.PerModule))
                {
                    li += "<li onclick='tabs(" + (i + 1).ToString() + ");'><a href='javascript:void(0)' target='sysMain'><span>" + modPer.Name + "</span></a></li>";
                    foreach (PagePer pp in modPer.PagePers.Values)
                    {
                        if (pp.Code == "Default.aspx" || pp.Code.Equals(ConstantValue.SystemConst.DATA_PERMISSION))
                        {
                            continue; //在菜单中不显示
                        }

                        if (i == 0)
                        {
                            UlSpli += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 1)
                        {
                            UlSplie += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 2)
                        {
                            ul1 += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 3)
                        {
                            ul2 += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 4)
                        {
                            ul3 += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 5)
                        {
                            ul4 += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 6)
                        {
                            ul5 += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                        if (i == 7)
                        {
                            ul6 += "<li><a href='" + pp.Code + "' target='sysMain'>" + pp.Name + "</a></li>";
                        }
                    }
                    i++;
                }
                lblAdminName.Text = am.Name + "(" + am.Username + ")";
                SpiUrl.InnerHtml = UlSplie;
                SplitUl.InnerHtml = li;
                UlSplit.InnerHtml = UlSpli;
                Ul1.InnerHtml = ul1;
                Ul2.InnerHtml = ul2;
                Ul3.InnerHtml = ul3;
                Ul4.InnerHtml = ul4;
                Ul5.InnerHtml = ul5;
                Ul6.InnerHtml = ul6;
            }
            if (Session["Admin"] == null)
            {
                MessageBox.ResponseScript(Page, "top.location.href='/Admin/Login.aspx'");
                return;
            }
        }



        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Semitron_OMS.BLL.Common.Admin bam = new Semitron_OMS.BLL.Common.Admin();
            if (bam.Quit() > 0)
            {
                Session["Admin"] = null;
                //Response.Redirect("/Admin/Login.aspx", true);
                MessageBox.ResponseScript(Page, "top.location.href='/Admin/Login.aspx'");
            }
        }
    }
}
