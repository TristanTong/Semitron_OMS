using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace Semitron_OMS.Common
{
    public class PhoneNumDAQ
    {
        //public List<string> NumberBelong(string Pone)
        //{
        //    if (Pone.Length > 7)
        //    {
        //        Pone = Pone.Substring(0, 7);
        //    }
        //    log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(PhoneNumDAQ));
        //    string strurl = "http://www.ip138.com:8080/search.asp?mobile=" + Pone + "&action=mobile";    //欲获取的网页地址
        //    List<string> listResult = new List<string>();
        //    List<string> Record = new List<string>();
        //    System.Net.WebClient myWebClient = new System.Net.WebClient();    //创建WebClient实例myWebClient
        //    //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。
        //    myWebClient.Credentials = CredentialCache.DefaultCredentials;
        //    //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号）
        //    string result = "";

        //    try
        //    {
        //        byte[] pagedata = myWebClient.DownloadData(strurl);
        //        result = Encoding.Default.GetString(pagedata);
        //        if (result.IndexOf("刷新太频繁") > -1)
        //        {
        //            myLogger.Info("刷新太频繁。Num:" + Pone);
        //            Debug.Write("!!!!!!!!!!!刷新太频繁!!!!!!!!");
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        myLogger.Fatal("采集速度过快，", ex);
        //    }

        //    //功能是一样是用来转换字符集，根据获取网站页面的字符编码选择

        //    string str = @"" + result + "";
        //    //比对<table>标签的正则表达式
        //    Regex re = new Regex(@"(?is)<table[^>]*>(?><table[^>]*>(?<o>)|</table>(?<-o>)|(?:(?!</?table\b).)*)*(?(o)(?!))</table>");
        //    string res = "";
        //    //找到所有符合<table>标签的字符
        //    MatchCollection mc = re.Matches(str);
        //    foreach (Match m in mc)
        //    {
        //        res += m.Value + "\n--------------------\n";
        //    }
        //    //将所有<td>中的数据都读出来
        //    MatchCollection match = Regex.Matches(res, @"<td.*?>(?<content>[^<>]+)</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        //    //循环将<td>中的数据加到list集合里面
        //    foreach (Match m in match)
        //    {
        //        listResult.Add(m.Groups["content"].Value);
        //    }
        //    //数据完整性验证。
        //    if (listResult.Count < 10)
        //    {
        //        string error = "";
        //        foreach (var item in listResult)
        //        {
        //            error += item + ",";
        //        }
        //        myLogger.Fatal("号码段数据采集错误。listResult:" + error);
        //        return null;
        //    }
        //    //如果数据完整
        //    if (listResult[5] == "卡号归属地")
        //    {
        //        if (listResult[6] == "未知")
        //        {
        //            Record.Add("未知");
        //            return Record;
        //        }
        //        //将地区的&nbsp;用空格代替
        //        string area = Regex.Replace(listResult[6], "&nbsp;", " ");
        //        //分段地区的省份和城市
        //        string[] Province = area.Split(' ');
        //        //将要用的值放入Record List的集合里面
        //        Record.Add(Province[0]);    //省份
        //        Record.Add(Province[1]);    //城市
        //        Record.Add(listResult[8]);  //卡类型
        //        if (listResult.Count > 10)
        //        {
        //            Record.Add(listResult[10]); //区号
        //        }
        //        return Record;
        //    }
        //    else
        //    {
        //        if (listResult[5] == "未知")
        //        {
        //            Record.Add("未知");
        //            return Record;
        //        }
        //        //将地区的&nbsp;用空格代替
        //        string area = Regex.Replace(listResult[5], "&nbsp;", " ");
        //        //分段地区的省份和城市
        //        string[] Province = area.Split(' ');
        //        //将要用的值放入Record List的集合里面
        //        Record.Add(Province[0]);    //省份
        //        Record.Add(Province[1]);    //城市
        //        Record.Add(listResult[7]);  //卡类型
        //        Record.Add(listResult[9]);  //区号
        //        return Record;
        //    }
        //}

        public List<string> NumberBelong(string Phone)
        {
            GeneratorTools gt = new GeneratorTools();
            List<string> lst = new List<string>();
            log4net.ILog myLogger = log4net.LogManager.GetLogger(typeof(PhoneNumDAQ));
            int temp = 0;
            if (!string.IsNullOrEmpty(Phone) && int.TryParse(Phone, out temp))//校验号段是否有效并是否为纯数字
            {
                lst = gt.HandleShowji(Phone);

                if (lst == null || lst.Count != 4 || string.IsNullOrEmpty(lst[1]))
                {
                    myLogger.Fatal("无法从网站中获取号码段:" + Phone + "信息。");
                    lst = null;
                }
            }
            else
            {
                myLogger.Fatal(Phone + "不是有效的号码段。");
                lst = null;
            }
            return lst;
        }
    }

    public class GeneratorTools
    {
        public string GetWebSiteHtmlCode(string url, int timeout, string encodingName)
        {
            string strResult = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //声明一个HttpWebRequest请求
            request.Timeout = timeout;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding(encodingName);
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            strResult = streamReader.ReadToEnd();
            return strResult;
        }

        public List<string> HandleIp138(string strPhone)
        {
            if (strPhone.Length > 7)
            {
                strPhone = strPhone.Substring(0, 7);
            }

            string strurl = "http://www.ip138.com:8080/search.asp?mobile=" + strPhone + "&action=mobile";    //欲获取的网页地址
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("UrlIp138")))
            {
                strurl = string.Format(ConfigurationManager.AppSettings.Get("UrlIp138").Replace('#', '&'), strPhone);
            }
            List<string> listResult = new List<string>();
            List<string> Record = new List<string>();
            string result = "";
            try
            {
                result = GetWebSiteHtmlCode(strurl, 30000, "GB2312");
                if (result.IndexOf("刷新太频繁") > -1)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            //功能是一样是用来转换字符集，根据获取网站页面的字符编码选择
            string str = @"" + result + "";
            //比对<table>标签的正则表达式
            Regex re = new Regex(@"(?is)<table[^>]*>(?><table[^>]*>(?<o>)|</table>(?<-o>)|(?:(?!</?table\b).)*)*(?(o)(?!))</table>");
            string res = "";
            //找到所有符合<table>标签的字符
            MatchCollection mc = re.Matches(str);
            foreach (Match m in mc)
            {
                res += m.Value + "\n--------------------\n";
            }
            //将所有<td>中的数据都读出来
            MatchCollection match = Regex.Matches(res, @"<td.*?>(?<content>[^<>]+)</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            //循环将<td>中的数据加到list集合里面
            foreach (Match m in match)
            {
                listResult.Add(m.Groups["content"].Value);
            }
            //数据完整性验证。
            if (listResult.Count < 10)
            {
                string error = "";
                foreach (var item in listResult)
                {
                    error += item + ",";
                }

                return null;
            }

            //如果数据完整
            if (listResult[3] == "卡号归属地")
            {
                if (listResult[4] == "未知")
                {
                    Record.Add("未知");
                    return Record;
                }
                //将地区的&nbsp;用空格代替
                string area = Regex.Replace(listResult[4], "&nbsp;", " ");
                //分段地区的省份和城市
                string[] Province = area.Split(' ');
                //将要用的值放入Record List的集合里面
                if (Province.Length == 2)
                {
                    Record.Add(Province[0].Trim());    //省份
                    Record.Add(Province[1].Trim());    //城市
                    Record.Add(listResult[6]);  //卡类型
                    Record.Add(listResult[8]); //区号
                    return Record;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (listResult[4] == "未知")
                {
                    Record.Add("未知");
                }
                return Record;
            }
        }

        public List<string> HandleShowji(string strPhone)
        {
            if (strPhone.Length > 7)
            {
                strPhone = strPhone.Substring(0, 7);
            }

            //string strurl = "http://api.showji.com/Locating/zhoumoyukuai.aspx?m=" + strPhone + "&output=json&callback=querycallback";    //欲获取的网页地址
            string strurl = "http://api.showji.com/Locating/20080808.aspx?m=" + strPhone + "&output=json&callback=querycallback";
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("UrlShowji")))
            {
                strurl = string.Format(ConfigurationManager.AppSettings.Get("UrlShowji").Replace('#', '&'), strPhone);
            }
            List<KeyValuePair<string, string>> listResult = new List<KeyValuePair<string, string>>();
            List<string> Record = new List<string>();
            string result = "";
            try
            {
                result = GetWebSiteHtmlCode(strurl, 30000, "UTF-8");
            }
            catch (Exception)
            {
                return null;
            }

            //querycallback({"Mobile":"1868898","QueryResult":"True","Province":"广东","City":"深圳","AreaCode":"0755","PostCode":"518000","Corp":"中国联通","Card":"GSM/3G"});
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            else
            {
                result = result.Replace("querycallback({", "").Replace("});", "");
                string[] strArr = result.Split(',');
                if (strArr.Length > 0)
                {
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strArr[i]))
                        {
                            string[] arrTemp = strArr[i].Split(':');
                            if (arrTemp.Length == 2)
                            {
                                KeyValuePair<string, string> kv = new KeyValuePair<string, string>(arrTemp[0].Replace("\"", ""), arrTemp[1].Replace("\"", ""));
                                listResult.Add(kv);
                            }
                        }
                    }
                }
            }
            //数据完整性验证。
            if (listResult.Count < 8)
            {
                return null;
            }
            //如果数据完整
            if (listResult[1].Key == "QueryResult" && listResult[1].Value == "True")
            {
                if (listResult[2].Key == "Province")
                {
                    Record.Add(listResult[2].Value);    //省份
                }
                if (listResult[3].Key == "City")
                {
                    Record.Add(listResult[3].Value);    //城市
                }
                if (listResult[6].Key == "Corp")
                {
                    string stCardType = listResult[6].Value;
                    if (listResult[7].Key == "Card")
                        stCardType += listResult[7].Value;
                    Record.Add(stCardType);             //卡类型 
                }
                if (listResult[4].Key == "AreaCode")
                {
                    Record.Add(listResult[4].Value);    //区号    
                }
                return Record;
            }
            return null;
        }

        public List<string> Handle114la(string strPhone)
        {
            if (strPhone.Length > 7)
            {
                strPhone = strPhone.Substring(0, 7);
            }

            string strurl = "http://tool.114la.com/shouji/" + strPhone;    //欲获取的网页地址
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("Url114la")))
            {
                strurl = string.Format(ConfigurationManager.AppSettings.Get("Url114la").Replace('#', '&'), strPhone);
            }
            List<string> listResult = new List<string>();
            List<string> Record = new List<string>();
            string result = "";
            try
            {
                result = GetWebSiteHtmlCode(strurl, 30000, "UTF-8");
            }
            catch (Exception)
            {
                return null;
            }

            //功能是一样是用来转换字符集，根据获取网站页面的字符编码选择
            string str = @"" + result + "";
            //比对<table>标签的正则表达式
            Regex re = new Regex(@"(?is)<table[^>]*>(?><table[^>]*>(?<o>)|</table>(?<-o>)|(?:(?!</?table\b).)*)*(?(o)(?!))</table>");
            string res = "";
            //找到所有符合<table>标签的字符
            MatchCollection mc = re.Matches(str);
            foreach (Match m in mc)
            {
                res += m.Value + "\n--------------------\n";
            }
            //将所有<td>中的数据都读出来
            MatchCollection match = Regex.Matches(res, @"<td.*?>(?<content>[^<>]+)</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            //循环将<td>中的数据加到list集合里面
            foreach (Match m in match)
            {
                listResult.Add(m.Groups["content"].Value);
            }
            //数据完整性验证。
            if (listResult.Count < 4)
            {
                string error = "";
                foreach (var item in listResult)
                {
                    error += item + ",";
                }

                return null;
            }
            //如果数据完整
            if (!string.IsNullOrEmpty(listResult[1]))
            {
                //分段地区的省份和城市
                string[] Province = listResult[1].Split(' ');
                //将要用的值放入Record List的集合里面
                Record.Add(Province[0].Trim());    //省份
                if (Province.Length == 2)
                {
                    Record.Add(Province[1].Trim());    //城市
                }
                else
                {
                    Record.Add(Province[0].Trim());    //省份
                }
                Record.Add(listResult[2]);  //卡类型                
                Record.Add(listResult[3]); //区号                
                return Record;
            }
            return null;
        }

    }
}
