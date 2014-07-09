using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Site;

namespace Semitron_OMS.UI.Handle.Site
{
    /// <summary>
    /// ProductTypeNoSessionHandle 的摘要说明
    /// </summary>
    public class ProductTypeNoSessionHandle : IHttpHandler
    {
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(ProductTypeNoSessionHandle));
        HttpRequest _request = HttpContext.Current.Request;
        Semitron_OMS.BLL.Site.ProductTypeBLL _bllProductType = new Semitron_OMS.BLL.Site.ProductTypeBLL();
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
                    //获取框架导航菜单产品分类HTML
                    case "GetProductTypeFrameHtml":
                        context.Response.Write(GetProductTypeFrameHtml());
                        break;
                }
            }
            context.Response.End();
        }

        /// <summary>
        /// 获取框架导航菜单产品分类HTML
        /// </summary>
        /// <returns></returns>
        private string GetProductTypeFrameHtml()
        {
            string strLang = DataUtility.GetPageFormValue(_request.Form["lang"], "en");
            List<ProductTypeModel> lstModel = new BLL.Site.ProductTypeBLL().GetProductTypeInMain(strLang);

            string strHtml = "<a href=\"#\" title=\"/product/product-index.aspx?lang=" + strLang + "\" remark=\"920\">" + (strLang == "cn" ? "产品展示" : "Product") + "</a>";
            strHtml += " <ul>";
            foreach (ProductTypeModel model in lstModel)
            {
                strHtml += "  <li><a href=\"#\" title=\"/product/product-index.aspx?lang=" + strLang + "&Type=" + model.ID + "\" remark=\"920\">" + model.TypeName + "</a></li>";
            }
            strHtml += "</ul>";
            return strHtml;
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