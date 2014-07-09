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
    /// IndustryTrendNoSessionHandle 的摘要说明
    /// </summary>
    public class IndustryTrendNoSessionHandle : IHttpHandler
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
                    //获取行业动态首页列表
                    case "GetIndustryTrendInMain":
                        context.Response.Write(GetIndustryTrendInMain());
                        break;
                    //获取行业动态树
                    case "GetTrendTree":
                        context.Response.ContentType = "application/json";
                        context.Response.Write(GetTrendTree());
                        break;
                    //获取行业动态索引页数据
                    case "GetIndustryTrendInIndex":
                        context.Response.Write(GetIndustryTrendInIndex(context));
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取行业动态首页列表
        /// </summary>
        /// <returns></returns>
        private string GetIndustryTrendInMain()
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
            DataTable dt = new BLL.Site.IndustryTrendBLL().GetIndustryTrendInMain(strLang);
            if (dt == null)
            {
                return string.Empty;
            }
            string strHtml = "<ul class=\"ltr\">";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strHtmlId = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[i]["HtmlPath"].ToString()))
                {
                    strHtmlId = dt.Rows[i]["HtmlPath"].ToString().Remove(dt.Rows[i]["HtmlPath"].ToString().IndexOf('.'));
                }
                string strOnclick = string.Empty;
                if (!string.IsNullOrEmpty(strHtmlId))
                {
                    string strHeight = dt.Rows[i]["PageHeight"].ToString();
                    strOnclick = "onclick=\"GoURL('/trend/trend-page.aspx?" + (strLang == string.Empty ? string.Empty : ("lang=" + strLang + "&")) + "id=" + strHtmlId + "&height=" + strHeight + "'," + dt.Rows[i]["PageHeight"].ToString() + ")\"";
                }
                string strName = dt.Rows[i]["Name"].ToString();
                strHtml += "<li><a class=\"textgray12\" href=\"#\" title=\"" + strName + "\" " + strOnclick + "> " + StringPlus.K8GetStrLength(strName, 40, true) + " </a></li>";
            }
            strHtml += "</ul>";
            return strHtml;
        }

        /// <summary>
        /// 获取行业动态索引页数据
        /// </summary>
        private string GetIndustryTrendInIndex(HttpContext context)
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
            DataTable dt = new BLL.Site.IndustryTrendBLL().GetIndustryTrendInIndex(strLang);
            if (dt == null)
            {
                return string.Empty;
            }
            dt = CommonFunction.GetRownumTable(dt);
            int iPageIndex = int.Parse(DataUtility.GetPageFormValue(_request.Form["PageIndex"], "1"));
            dt = CommonFunction.GetCurrentPageTable(dt, 15, iPageIndex);

            string strHtml = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strHtmlId = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[i]["HtmlPath"].ToString()))
                {
                    strHtmlId = dt.Rows[i]["HtmlPath"].ToString().Remove(dt.Rows[i]["HtmlPath"].ToString().IndexOf('.'));
                }
                string strOnclick = string.Empty;
                if (!string.IsNullOrEmpty(strHtmlId))
                {
                    string strHeight = dt.Rows[i]["PageHeight"].ToString();
                    string strShortName = StringPlus.K8GetStrLength(dt.Rows[i]["Name"].ToString(), 126, true);

                    strHtml += "<div><span class=\"font_gray_b\">" + dt.Rows[i]["CreateDate"].ToString() + "</span><a href=\"/Redirect.aspx?lang=" + strLang + "&tp=trend-page&item=" + strHtmlId + "&height=" + strHeight + "\" target=\"_top\">" + strShortName + "</a></div>";
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 获取行业动态树
        /// </summary>
        /// <returns></returns>
        private string GetTrendTree()
        {
            PageResult result = new PageResult();
            try
            {
                string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
                DataTable dt = new BLL.Site.IndustryTrendBLL().GetTrendTree(strLang);
                if (dt == null)
                {
                    result.Info = "获取行业动态树失败。";
                    return result.ToString();
                }
                return ForTrendList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取行业动态树出现异常！";
            }
            return result.ToString();
        }

        private string ForTrendList(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string strName = dr["Name"].ToString();
                string node = string.Format("{{ \"id\":\"{0}\", \"pId\":\"{1}\", \"name\":\"{2}\", \"height\":\"{3}\", \"fullname\":\"{4}\",\"isParent\":false }}",
                  dr["Id"].ToString(), dr["Pid"].ToString(), StringPlus.K8GetStrLength(strName, 26, true), dr["Height"].ToString(), strName);
                if (dr["pId"].ToString() == "0")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ", \"open\":true }";
                }
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