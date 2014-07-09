using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.Model.Site;
using Newtonsoft.Json;
using System.Text;

namespace Semitron_OMS.UI.Handle.Site
{
    /// <summary>
    /// SolutionNoSessionHandle 的摘要说明
    /// </summary>
    public class SolutionNoSessionHandle : IHttpHandler
    {
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(SolutionNoSessionHandle));
        HttpRequest _request = HttpContext.Current.Request;
        Semitron_OMS.BLL.Site.SolutionBLL _bllSolution = new Semitron_OMS.BLL.Site.SolutionBLL();
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
                    //获取解决方案首页列表
                    case "GetSolutionInMain":
                        context.Response.Write(GetSolutionInMain());
                        break;
                    //获取框架导航菜单解决方案HTML
                    case "GetSolutionFrameHtml":
                        context.Response.Write(GetSolutionFrameHtml());
                        break;
                    //获取解决方案详细信息
                    case "GetSolutionById":
                        context.Response.ContentType = "application/json";
                        context.Response.Write(GetSolutionById());
                        break;
                    //获取解决方案树
                    case "GetSolutionTree":
                        context.Response.ContentType = "application/json";
                        context.Response.Write(GetSolutionTree());
                        break;

                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取解决方案树
        /// </summary>
        /// <returns></returns>
        private string GetSolutionTree()
        {
            PageResult result = new PageResult();
            try
            {
                string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
                DataTable dt = this._bllSolution.GetSolutionTree(strLang);
                if (dt == null)
                {
                    result.Info = "获取解决方案树失败。";
                    return result.ToString();
                }
                return ForSolutionList(dt);
            }
            catch (Exception ex)
            {
                result.Info = "获取解决方案树出现异常！";
            }
            return result.ToString();
        }

        private string ForSolutionList(DataTable dt)
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

        /// <summary>
        /// 获取解决方案首页列表
        /// </summary>
        /// <returns></returns>
        private string GetSolutionInMain()
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], string.Empty);
            List<Semitron_OMS.Model.Site.SolutionModel> lstModel = new BLL.Site.SolutionBLL().GetSolutionInMain(strLang);
            //根据语言类别取出解决方案的标题和概述、Id以及页面高度
            string strHtml = "<table width=\"98%\" style=\"text-align: center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tbody> <tr> <td style=\"clear: both; height: 5px  !important;\"> <img src=\"/images/zj_indexfangan01_01.jpg\" /> </td> </tr> <tr> <td> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tbody> <tr> <td width=\"13\"> <img src=\"/images/zj_indexfangan01_02.jpg\" width=\"13\" height=\"196\" /> </td> <td> <table> <tr> <td width=\"140\" height=\"25\" background=\"/images/zj_indexfangan01_03.jpg\" style=\"text-align: center\"> <strong>" + (strLang == "cn" ? "解决方案" : "Solution") + "</strong> </td> <td width=\"450\"> <div class=\"menu\"> <ul> <li> <div id=\"tablink1\" style=\"float: left; width: 124px; height: 25px\"> " + lstModel[0].Name + " </div> </li> <li> <div id=\"tablink2\" style=\"float: left; width: 124px; height: 25px\"> " + lstModel[1].Name + " </div> </li> <li> <div id=\"tablink3\" style=\"float: left; width: 124px; height: 25px\"> " + lstModel[2].Name + " </div> </li> </ul> </div> </td> </tr><tr> <td colspan=\"2\">";
            strHtml += "<div id=\"tabcontent1\"> <table id=\"table1\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tbody> <tr> <td width=\"230\"> <img src=\"/images/P020091203681337022316.jpg\" width=\"230\" height=\"171\" /> </td> <td background=\"/images/zj_indexfangan01_10.jpg\"> <table border=\"0\" cellspacing=\"2\" cellpadding=\"2\" width=\"396\"> <tbody> <tr> <td class=\"textgray12\" height=\"100\" valign=\"top\" align=\"left\"> " + lstModel[0].Description + " </td> </tr> <tr> <td align=\"right\"> <a class=\"gray12\" href=\"#\" onclick=\"GoURL('/solution/solution-page.aspx?id=" + lstModel[0].ID + "'," + lstModel[0].PageHeight + ")\"> " + (strLang == "cn" ? "了解详情" : "Show Detail") + " &gt;&gt;&gt;&nbsp;</a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div> ";
            strHtml += "<div id=\"tabcontent2\"> <table id=\"table2\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tbody> <tr> <td width=\"230\"> <img src=\"/images/P020091203683882013493.jpg\" width=\"230\" height=\"171\" /> </td> <td background=\"/images/zj_indexfangan01_10.jpg\"> <table border=\"0\" cellspacing=\"2\" cellpadding=\"2\" width=\"396\"> <tbody> <tr> <td class=\"textgray12\" height=\"100\" valign=\"top\" align=\"left\"> " + lstModel[1].Description + " </td> </tr> <tr> <td align=\"right\"> <a class=\"gray12\" href=\"#\" onclick=\"GoURL('/solution/solution-page.aspx?id=" + lstModel[1].ID + "'," + lstModel[1].PageHeight + ")\"> " + (strLang == "cn" ? "了解详情" : "Show Detail") + " &gt;&gt;&gt;&nbsp;</a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div> ";
            strHtml += "<div id=\"tabcontent3\"> <table id=\"table3\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tbody> <tr> <td width=\"230\"> <img src=\"/images/P020091203684643080532.jpg\" width=\"230\" height=\"171\"> </td> <td background=\"/images/zj_indexfangan01_10.jpg\"> <table border=\"0\" cellspacing=\"2\" cellpadding=\"2\" width=\"396\"> <tbody> <tr> <td class=\"textgray12\" height=\"100\" valign=\"top\" align=\"left\">" + lstModel[2].Description + " </td> </tr> <tr> <td align=\"right\"> <a class=\"gray12\" href=\"#\" onclick=\"GoURL('/solution/solution-page.aspx?id=" + lstModel[2].ID + "'," + lstModel[2].PageHeight + ")\"> " + (strLang == "cn" ? "了解详情" : "Show Detail") + " &gt;&gt;&gt;&nbsp;</a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div>";
            strHtml += "</td> </tr> </table> </td> <td width=\"14\"> <img src=\"/images/zj_indexfangan01_08.jpg\" width=\"14\" height=\"196\" /> </td> </tr> </tbody> </table> </td> </tr> <tr> <td> <img src=\"/images/zj_indexfangan01_11.jpg\" height=\"16\"> </td> </tr> </tbody> </table>";
            return strHtml;
        }
        /// <summary>
        /// 获取框架导航菜单解决方案HTML
        /// </summary>
        /// <returns></returns>
        private string GetSolutionFrameHtml()
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], "en");
            List<SolutionModel> lstModel = new BLL.Site.SolutionBLL().GetSolutionInMain(strLang);
            string strHtml = "<a href=\"#\">" + (strLang == "cn" ? "解决方案" : "Solution") + "</a>";
            strHtml += " <ul>";
            foreach (SolutionModel model in lstModel)
            {
                if (model.ID != 0)
                {
                    strHtml += "  <li><a href=\"#\" title=\"/solution/solution-page.aspx?lang=" + strLang + "&id=" + model.ID + "\" remark=\"" + model.PageHeight + "\">" + model.Name + "</a></li>";
                }
            }
            strHtml += "</ul>";

            return strHtml;
        }



        /// <summary>
        /// 获取解决方案详细信息
        /// </summary>
        /// <returns></returns>
        private string GetSolutionById()
        {
            PageResult result = new PageResult();
            int iId = -1;
            if (_request.Form["Id"] == null || !int.TryParse(_request.Form["Id"].ToString(), out iId))
            {
                result.State = 0;
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            try
            {
                SolutionModel model = this._bllSolution.GetModel(iId);
                return JsonConvert.SerializeObject(model, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "获取解决方案详细信息出现异常！";
                _myLogger.Error("客户机IP:" + HttpContext.Current.Request.UserHostAddress + "，获取解决方案详细信息出现异常：" + ex.Message, ex);
            }
            return result.ToString();
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