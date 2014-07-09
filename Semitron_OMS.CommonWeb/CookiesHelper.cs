using Semitron_OMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Semitron_OMS.CommonWeb
{
    public class CookiesHelper
    {
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string cookieName, string key, string value, DateTime? expires)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                        cookie.Values.Set(key, value);
                    else
                        if (!string.IsNullOrEmpty(value))
                            cookie.Value = value;
                    if (expires != null)
                        cookie.Expires = expires.Value;
                    response.SetCookie(cookie);
                }
            }
        }

        /// <summary>
        /// 插入保存到Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void AddLoginCookie(string cookieName, string strName, string strPwd)
        {
            HttpCookie Cookie = CookiesHelper.GetCookie(cookieName);
            if (Cookie == null || Cookie.Domain == null)
            {
                Cookie = new HttpCookie(cookieName);
                Cookie.Values.Add("uName", strName);
                Cookie.Values.Add("uPassword", strPwd);
                //设置Cookie过期时间
                Cookie.Expires = DateTime.Now.AddHours(4);
                CookiesHelper.AddCookie(Cookie);
            }
            else
            {
                SetCookie(cookieName, "uName", strName, DateTime.Now.AddHours(4));
                SetCookie(cookieName, "uPassword", strPwd, DateTime.Now.AddHours(4));
            }
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void AddCookie(HttpCookie cookie)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                //指定客户端脚本是否可以访问[默认为false]
                cookie.HttpOnly = true;
                //指定统一的Path，比便能通存通取
                cookie.Path = "/";
                //设置跨域,这样在其它二级域名下就都可以访问到了
                //cookie.Domain = "chinesecoo.com";
                response.AppendCookie(cookie);
            }
        }
        /// <summary>
        /// 根据名称获取指定的Cookies
        /// </summary>
        public static HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
                return request.Cookies[cookieName];
            return null;
        }
    }
}
