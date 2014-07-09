using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Semitron_OMS.Common;
using System.Text;

namespace Semitron_OMS.UI.Handle.Site
{
    /// <summary>
    /// ProductShowNoSeesionHandle 的摘要说明
    /// </summary>
    public class ProductShowNoSeesionHandle : IHttpHandler
    {
        HttpRequest _request = HttpContext.Current.Request;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string methStr = string.Empty;
            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //获取展示产品首页列表
                    case "GetProductShowInMain":
                        context.Response.Write(GetProductShowInMain());
                        break;
                    //获取产品列表树
                    case "GetProductTree":
                        context.Response.ContentType = "application/json";
                        context.Response.Write(GetProductTree());
                        break;
                    //获取产品索引页数据
                    case "GetProductShowInIndex":
                        context.Response.Write(GetProductShowInIndex(context));
                        break;

                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取展示产品首页列表
        /// </summary>
        /// <returns></returns>
        private string GetProductShowInMain()
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
            DataTable dt = new BLL.Site.ProductShowBLL().GetProductShowInPage(strLang, true, string.Empty);
            if (dt == null)
            {
                return string.Empty;
            }
            string strHtml = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 5 == 0)
                {
                    strHtml += "<tr>";
                }
                string strHtmlId = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[i]["HtmlPath"].ToString()))
                {
                    strHtmlId = dt.Rows[i]["HtmlPath"].ToString().Remove(dt.Rows[i]["HtmlPath"].ToString().IndexOf('.'));
                }
                string strOnclick = string.Empty;
                if (!string.IsNullOrEmpty(strHtmlId))
                {
                    string strHeight = dt.Rows[i]["PageHeight"].ToString();
                    strOnclick = "onclick=\"GoURL('/product/product-page.aspx?" + (strLang == string.Empty ? string.Empty : ("lang=" + strLang + "&")) + "id=" + strHtmlId + "&height=" + strHeight + "'," + strHeight + ")\"";
                }


                strHtml += "<td> <table><tr> <td> <div style=\"position: relative\" class=\"onHover\"> <a href=\"#\" " + strOnclick + "> <img alt=\"\" src=\"" + dt.Rows[i]["IcoPath"].ToString() + "\" /></a></div> </td> </tr> <tr> <td style=\"text-align:	center\"> <a href=\"#\" " + strOnclick + "> <font style=\"color: black\">" + StringPlus.K8GetStrLength(dt.Rows[i]["Name"].ToString(), 26, true) + " </font></a> </td></tr> </table> </td>";
                if ((i + 1) % 5 == 0)
                {
                    strHtml += "</tr>";
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 获取产品索引页数据
        /// </summary>
        private string GetProductShowInIndex(HttpContext context)
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
            string strType = DataUtility.GetPageFormValue(_request.Form["type"], string.Empty);
            DataTable dt = new BLL.Site.ProductShowBLL().GetProductShowInPage(strLang, false, strType);
            if (dt == null)
            {
                return string.Empty;
            }
            dt = CommonFunction.GetRownumTable(dt);
            int iPageIndex = int.Parse(DataUtility.GetPageFormValue(_request.Form["PageIndex"], "1"));
            dt = CommonFunction.GetCurrentPageTable(dt, 6, iPageIndex);

            string strHtml = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strHtmlId = string.Empty;
                string strHtmlPath = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[i]["HtmlPath"].ToString()))
                {
                    strHtmlPath = dt.Rows[i]["HtmlPath"].ToString();
                    strHtmlId = dt.Rows[i]["HtmlPath"].ToString().Remove(dt.Rows[i]["HtmlPath"].ToString().IndexOf('.'));
                }
                string strOnclick = string.Empty;
                if (!string.IsNullOrEmpty(strHtmlId))
                {
                    string strHeight = dt.Rows[i]["PageHeight"].ToString();
                    string strIcoPath = dt.Rows[i]["IcoPath"].ToString();
                    string strShortName = StringPlus.K8GetStrLength(dt.Rows[i]["Name"].ToString(), 26, true);
                    string strShortDisc = string.Empty;
                    string strFilePath = string.Empty;
                    if (strLang != "cn")
                    {
                        strFilePath = context.Server.MapPath("../../" + strLang + "/product") + "\\" + strHtmlPath;
                    }
                    else
                    {
                        strFilePath = context.Server.MapPath("../../cn/product") + "\\" + strHtmlPath;
                    }
                    string strHtmlCode = ReadTxtFile.ReadFileString(strFilePath);
                    if (!string.IsNullOrEmpty(strHtmlCode))
                    {
                        Semitron_OMS.Common.Util.HtmlToText htt = new Common.Util.HtmlToText();
                        strShortDisc = StringPlus.K8GetStrLength(htt.Convert(strHtmlCode), 280, true);
                    }
                    strHtml += "<div class=\"xinwenlist\"> <div class=\"zleft\"> <a href=\"/Redirect.aspx?lang=" + strLang + "&tp=product-page&item=" + strHtmlId + "&height=" + strHeight + "\" target=\"_top\"> <img src=\"" + strIcoPath + "\" width=\"118\" height=\"160\" /></a> </div> <div class=\"zright\"> <div class=\"fl\"> <a href=\"/Redirect.aspx?lang=" + strLang + "&tp=product-page&item=" + strHtmlId + "&height=" + strHeight + "\" target=\"_top\" class=\"font_gray_b\">" + strShortName + "</a></div> <div class=\"clear\"> </div> <div class=\"dtjj\">" + strShortDisc + " </div> </div> <div class=\"clear\"> </div> </div>";
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 获取产品列表树
        /// </summary>
        /// <returns></returns>
        private string GetProductTree()
        {
            PageResult result = new PageResult();
            try
            {
                string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
                DataTable dt = new BLL.Site.ProductShowBLL().GetProductTree(strLang);
                if (dt == null)
                {
                    result.Info = "获取产品列表树失败。";
                    return result.ToString();
                }
                return ForProductList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取产品列表树出现异常！";
            }
            return result.ToString();
        }

        private string ForProductList(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string strName = dr["Name"].ToString();
                string node = string.Format("{{ \"id\":\"{0}\", \"pId\":\"{1}\", \"name\":\"{2}\", \"height\":\"{3}\", \"fullname\":\"{4}\",\"isParent\":false }}",
                  dr["Id"].ToString(), dr["Pid"].ToString(), StringPlus.K8GetStrLength(strName, 26, true), dr["Height"].ToString(), strName);
                node = node.Substring(0, node.Length - 1);
                node += ", \"open\":true }";
                if (dr["BID"].ToString() != "")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"checked\":true }";
                }
                treenodes.Add(node);
            }

            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}