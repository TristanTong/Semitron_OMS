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
    /// DownloadNoSessionHandle 的摘要说明
    /// </summary>
    public class DownloadNoSessionHandle : IHttpHandler
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
                    //获取下载列表树
                    case "GetDownloadTree":
                        context.Response.ContentType = "application/json";
                        context.Response.Write(GetDownloadTree());
                        break;
                    //获取下载列表索引页数据
                    case "GetDownloadInIndex":
                        context.Response.Write(GetDownloadInIndex(context));
                        break;
                    //根据文件ID获取下载路径
                    case "GetFileUrlPathById":
                        context.Response.Write(GetFileUrlPathById());
                        break;
                }
            }
            context.Response.End();
        }

        private string GetFileUrlPathById()
        {
            string strId = DataUtility.GetPageFormValue(_request.Form["Id"], string.Empty);
            string strFileUrlPath = string.Empty;
            if (strId == string.Empty)
            {
                return strFileUrlPath;
            }
            strFileUrlPath = new BLL.OMS.AttachmentBLL().GetUrlListByObj("DownloadCenter", strId);
            return strFileUrlPath;
        }

        /// <summary>
        /// 获取下载列表索引页数据
        /// </summary>
        private string GetDownloadInIndex(HttpContext context)
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], "en");
            DataTable dt = new BLL.Site.DownloadCenterBLL().GetDownloadInIndex(strLang);

            dt = CommonFunction.GetRownumTable(dt);
            int iPageIndex = int.Parse(DataUtility.GetPageFormValue(_request.Form["PageIndex"], "1"));
            dt = CommonFunction.GetCurrentPageTable(dt, 15, iPageIndex);

            string strHtml = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strShortName = dt.Rows[i]["Description"].ToString();
                //strShortName = StringPlus.K8GetStrLength(strShortName, 252, true);
                string strUrl = dt.Rows[i]["UrlPath"].ToString().Trim();
                strHtml += "<div><a  href=\"" + dt.Rows[i]["UrlPath"].ToString() + "\" target=\"_balck\"><span class=\"font_gray_b\">" + StringPlus.K8GetStrLength(dt.Rows[i]["Name"].ToString(), 120, false) + "［" + dt.Rows[i]["CreateTime"].ToString() + "］" + "</span></a>";
                strHtml += "<a  href=\"" + dt.Rows[i]["UrlPath"].ToString() + "\" target=\"_balck\"><i class=\"dl-ico\"></i></a>" + strShortName + "</div>";
            }
            return strHtml;
        }

        /// <summary>
        /// 获取下载列表树
        /// </summary>
        /// <returns></returns>
        private string GetDownloadTree()
        {
            PageResult result = new PageResult();
            try
            {
                string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
                DataTable dt = new BLL.Site.DownloadCenterBLL().GetDownloadTree(strLang);
                if (dt == null)
                {
                    result.Info = "获取下载列表树失败。";
                    return result.ToString();
                }
                return ForDownloadList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取下载列表树出现异常！";
            }
            return result.ToString();
        }

        private string ForDownloadList(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string strName = dr["Name"].ToString();
                string node = string.Format("{{ \"id\":\"{0}\", \"pId\":\"0\", \"name\":\"{1}\",  \"fullname\":\"{2}\",\"isParent\":false }}",
                  dr["Id"].ToString(), StringPlus.K8GetStrLength(strName, 26, true), strName);
                node = node.Substring(0, node.Length - 1);
                node += ", \"open\":true }";

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