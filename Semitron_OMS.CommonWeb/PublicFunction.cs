using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Xml;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
namespace Semitron_OMS.CommonWeb
{
    public class PublicFunction
    {
        public static string m_Key = "qwertyu1qwertyu3qwertyu6";

        public PublicFunction()
        { }

        #region 获取域名
        /// <summary>
        /// 获取当前站点的域名
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return GetUrl(HttpContext.Current);
        }

        /// <summary>
        /// 获取当前站点的域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUrl(HttpContext context)
        {
            string Url = context.Request.Url.AbsoluteUri.Replace(context.Request.Url.PathAndQuery, "") + context.Request.ApplicationPath;

            if (Url.EndsWith("/"))
            {
                Url = Url.Substring(0, Url.Length - 1);
            }

            return Url;
        }

        /// <summary>
        /// 获取当前站点的域名，但不附带前面的 http://
        /// </summary>
        /// <returns></returns>
        public static string GetUrlWithoutHttp()
        {
            return GetUrlWithoutHttp(HttpContext.Current);
        }

        /// <summary>
        /// 获取当前站点的域名，但不附带前面的 http://
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUrlWithoutHttp(HttpContext context)
        {
            string Url = GetUrl(context);

            if (Url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                Url = Url.Substring(7, Url.Length - 7);
            }

            if (Url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                Url = Url.Substring(8, Url.Length - 8);
            }

            return Url;
        }

        #endregion

        public static void GoError(int ErrorNumber, string Tip, string ClassName)
        {
            System.Web.HttpContext.Current.Response.Redirect("Error.aspx?ErrorNumber=" + ErrorNumber.ToString() + "&Tip=" + System.Web.HttpUtility.UrlEncode(Tip) + "&ClassName=" + System.Web.HttpUtility.UrlEncode(ClassName), true);
        }

        public static void ClearSession(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static object GetSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return HttpContext.Current.Session[key];
            }

            return null;
        }

        public static string GetCheckCode()
        {
            return HttpContext.Current.Session["MyValidateCode"].ToString();
        }

        public static void Alert(string Msg)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + Msg + "');</script>");
        }

        public static void Alert(Page page, string Msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + Msg + "！');</script>");
        }

        public static void Alert(Page page, string Msg, string RedirectPage)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + Msg + "！');window.location='" + RedirectPage + "';</script>");
        }

        public static string GetErrorMessage(short number)
        {
            string Message = "";

            switch (number)
            {
                case 1:
                    Message = "数据读写错误！";
                    break;
                case 2:
                    Message = "参数错误!";
                    break;
                case 3:
                    Message = "没有权限访问!";
                    break;
            }

            return Message;
        }

        //创建分页按钮
        public static void CreatePager(HtmlTableCell divPager, string Url, int currPage, int Pages)
        {
            int indx = currPage / 8;//href='javascript:;'
            // Url += Url.Contains("?") ? "&page=" : "?page=";//href='" + (Pages == 0 ? "#" : (Url + (currPage <= 1 ? "1" : (currPage - 1).ToString()))) + "'

            divPager.InnerHtml = "";
            divPager.InnerHtml += "<span><a style='cursor:pointer;' onclick=\"Paging('" + (currPage <= 1 ? "1" : (currPage - 1).ToString()) + "')\" >&lt; Prev</a></span>";
            int start = indx * 7 + 1;
            int end = indx * 7 + 7 > Pages ? Pages : indx * 7 + 7;

            for (int i = start; i <= end; i++)
            {
                if (i == currPage)//href='" + Url + i.ToString() + "'
                {
                    divPager.InnerHtml += "<a style='cursor:pointer;' onclick=\"Paging('" + i.ToString() + "')\"  class='current'>" + i.ToString() + "</a>";
                }
                else
                {
                    divPager.InnerHtml += "<a style='cursor:pointer;' onclick=\"Paging('" + i.ToString() + "')\" >" + i.ToString() + "</a>"; //href='" + Url + i.ToString() + "'
                }
            }

            divPager.InnerHtml += "<a style='cursor:pointer;' onclick=\"Paging('" + (currPage >= Pages ? Pages.ToString() : (currPage + 1).ToString()) + "')\">Next &gt;</a>";
            // href='" + (Pages == 0 ? "#" : (Url + (currPage >= Pages ? Pages.ToString() : (currPage + 1).ToString()))) + "'
        }

        public static string StringCut(string Str, int CutStrLong)
        {
            if (CutStrLong < 0)
            {
                CutStrLong = 0;
            }

            if (Str.Length <= CutStrLong)
            {
                return Str;
            }

            string Result = "";
            int i = 0;

            while ((Result.Length < CutStrLong) && (i < Str.Length))
            {
                Result += Str[i].ToString();

                i++;
            }

            if (Result != Str)
            {
                Result += "...";
            }

            return Result;
        }

        #region 数据转换

        public static string GetAppSetting(string AppSetting)
        {
            return ConfigurationManager.AppSettings[AppSetting].ToString();
        }

        public static int ParseToInt(object value, int defaultvalue)
        {
            int result = defaultvalue;

            try
            {
                result = int.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static short ParseToShort(object value, short defaultvalue)
        {
            short result = defaultvalue;

            try
            {
                result = short.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static long ParseToLong(object value, long defaultvalue)
        {
            long result = defaultvalue;

            try
            {
                result = long.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static float ParseToFloat(object value, float defaultvalue)
        {
            float result = defaultvalue;

            try
            {
                result = float.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static double ParseToDouble(object value, double defaultvalue)
        {
            double result = defaultvalue;

            try
            {
                result = double.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static bool ParseToBool(object value, bool defaultvalue)
        {
            bool result = defaultvalue;

            try
            {
                result = bool.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static DateTime ParseToDateTime(object value, DateTime defaultvalue)
        {
            DateTime result = defaultvalue;

            try
            {
                result = DateTime.Parse(value.ToString());
            }
            catch
            {
                return defaultvalue;
            }

            return result;
        }

        public static string ReplaceEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            return value.Replace(" ", "");
        }

        //清除最后一个符号。
        public static string RemoveLastCode(string value, string Code)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(Code))
            {
                if (value.EndsWith(Code))
                    value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        /// <summary>
        /// 手机号格式容错
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public static string TolerantPhone(string Phone)
        {
            if (Phone.StartsWith("0"))
            {
                Phone = Phone.Substring(1);

            }

            switch (Phone.Substring(0, 2))
            {
                case "86":
                    Phone = Phone.Substring(2);
                    break;
                case "+86":
                    Phone = Phone.Substring(3);
                    break;
                default:
                    break;
            }
            return Phone;
        }

        /// <summary>
        /// 获取手机号的运营商类型
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public static int GetMobileType(string Phone)
        {
            string PhoneNum = Phone.Substring(0, 3);
            string Nums = ConfigurationManager.AppSettings.Get("YDPhoneNums");
            string[] YDPhoneNums = null;
            string[] LTPhoneNums = null;
            string[] DXPhoneNums = null;
            if (string.IsNullOrEmpty(Nums))
            {
                return 0;
            }
            Nums = Nums.Replace("，", ",");
            YDPhoneNums = Nums.Split(',');
            Nums = ConfigurationManager.AppSettings.Get("LTPhoneNums");
            if (string.IsNullOrEmpty(Nums))
            {
                return 0;
            }
            Nums = Nums.Replace("，", ",");
            LTPhoneNums = Nums.Split(',');
            Nums = ConfigurationManager.AppSettings.Get("DXPhoneNums");
            if (string.IsNullOrEmpty(Nums))
            {
                return 0;
            }

            Nums = Nums.Replace("，", ",");
            DXPhoneNums = Nums.Split(',');

            if (YDPhoneNums.Contains(PhoneNum))
            {
                return 1;
            }
            if (LTPhoneNums.Contains(PhoneNum))
            {
                return 2;
            }
            if (DXPhoneNums.Contains(PhoneNum))
            {
                return 3;
            }
            
            return 0;
        }

        #endregion

        #region 修改配置文件

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="SectionName"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void SetConfigKeyValue(string SectionName, string Key, string Value)
        {
            SetConfigKeyValue(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, SectionName, Key, Value);
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="ConfigFileName"></param>
        /// <param name="SectionName"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void SetConfigKeyValue(string ConfigFileName, string SectionName, string Key, string Value)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(ConfigFileName);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }

            XmlNode node = doc.SelectSingleNode("//" + SectionName);

            if (node == null)
            {
                throw new InvalidOperationException(SectionName + " section not found in config file.");
            }

            try
            {
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", Key));

                if (elem != null)
                {
                    elem.SetAttribute("value", Value);
                }
                else
                {
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", Key);
                    elem.SetAttribute("value", Value);
                    node.AppendChild(elem);
                }
                doc.Save(ConfigFileName);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        ///过滤FreeTextBox中上传图片路径中的域名即将绝对路径替换成相对路径 
        /// </summary>
        /// <param name="strText">要过滤的文档内容</param>
        /// <returns>过滤后的文档内容</returns>
        public static string ReplaceFTBHost(string strText)
        {
            string strRetrun = string.Empty;                                         //返回的字符集
            string strUrl = HttpContext.Current.Request.Url.ToString().ToLower();    //当前的URL路径
            string strHost = HttpContext.Current.Request.Url.Host.ToString();        //当前站点的域名
            string strPort = HttpContext.Current.Request.Url.Port.ToString();        //当前站点端口号

            //判断是否以带端口号的方式
            if (strPort.Length > 0)
            {
                //分别有三种协议来操作
                if (Regex.IsMatch(strUrl, @"^http://.*?") == true)
                {
                    strRetrun = strText.Replace("http://" + strHost + ":" + strPort, "");
                }
                if (Regex.IsMatch(strUrl, @"^https://.*?") == true)
                {
                    strRetrun = strText.Replace("https://" + strHost + ":" + strPort, "");
                }
                if (Regex.IsMatch(strUrl, @"^ftp://.*?") == true)
                {
                    strRetrun = strText.Replace("ftp://" + strHost + ":" + strPort, "");
                }
            }
            else
            {
                //分别有三种协议来操作
                if (Regex.IsMatch(strUrl, @"^http://.*?") == true)
                {
                    strRetrun = strText.Replace("http://" + strHost, "");
                }
                if (Regex.IsMatch(strUrl, @"^https://.*?") == true)
                {
                    strRetrun = strText.Replace("https://" + strHost, "");
                }
                if (Regex.IsMatch(strUrl, @"^ftp://.*?") == true)
                {
                    strRetrun = strText.Replace("ftp://" + strHost, "");
                }
            }

            return strRetrun;
        }
    }
}