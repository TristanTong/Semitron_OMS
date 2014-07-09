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
    /// SearchNoSessionHandle 的摘要说明
    /// </summary>
    public class SearchNoSessionHandle : IHttpHandler
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
                    //获取查询结果树
                    case "GetSearchTree":
                        context.Response.ContentType = "application/json";
                        context.Response.Write(GetSearchTree());
                        break;
                    //获取查询结果索引页数据
                    case "GetSearchInIndex":
                        context.Response.Write(GetSearchInIndex(context));
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取查询结果树
        /// </summary>
        /// <returns></returns>
        private string GetSearchTree()
        {
            PageResult result = new PageResult();
            try
            {
                string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
                DataTable dt = new BLL.Site.SearchBLL().GetSearchTree(strLang);
                if (dt == null)
                {
                    result.Info = "获取查询结果树失败。";
                    return result.ToString();
                }
                return ForSearchList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取查询结果树出现异常！";
            }
            return result.ToString();
        }

        private string ForSearchList(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string strName = dr["Name"].ToString();
                string node = string.Format("{{ \"id\":\"{0}\", \"pId\":\"{1}\", \"name\":\"{2}\", \"height\":\"{3}\", \"fullname\":\"{4}\",  \"TreeType\":\"{5}\",\"isParent\":false }}",
                  dr["Id"].ToString(), dr["Pid"].ToString(), StringPlus.K8GetStrLength(strName, 26, true), dr["Height"].ToString(), strName, dr["TreeType"].ToString());
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