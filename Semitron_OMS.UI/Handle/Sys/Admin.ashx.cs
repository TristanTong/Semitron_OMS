using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Text;
using Newtonsoft.Json.Converters;
using Semitron_OMS.Common;
using Semitron_OMS.BLL;
using Semitron_OMS.Model;
using Semitron_OMS.Model.Common;
using Semitron_OMS.BLL.Common;
using System.Configuration;
using Semitron_OMS.Model.Permission;
using Semitron_OMS.Common.DEncrypt;
using Semitron_OMS.CommonWeb;
using System.IO;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// Admin管理
    /// </summary>
    public class Admin : IHttpHandler, IRequiresSessionState
    {
        log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(Admin));

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string methStr = string.Empty;

            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"];
            }
            if (context.Request.QueryString["meth"] != null)
            {
                methStr = context.Request.QueryString["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //登陆验证
                    case "GetUserlogin":
                        context.Response.Write(GetUserlogin());
                        break;
                    //用户的显示
                    case "GetAdminList":
                        context.Response.Write(GetAdminList());
                        break;
                    //添加新用户
                    case "AddAdminInfo":
                        context.Response.Write(AddAdminInfo());
                        break;
                    //修改后台用户
                    case "UpdateAdminInfo":
                        context.Response.Write(UpdateAdminInfo());
                        break;
                    //删除后台用户
                    case "DeleteAdminInfo":
                        context.Response.Write(DeleteAdminInfo());
                        break;
                    //修改密码
                    case "UpdatePwd":
                        context.Response.Write(UpdatePwd());
                        break;
                    //修改是查询最新数据
                    case "SelectUpdate":
                        context.Response.Write(SelectUpdate(context));
                        break;
                    //启用后台用户
                    case "EnabledAdminInfo":
                        context.Response.Write(EnabledAdminInfo());
                        break;
                    //查询角色名
                    case "SelectRoleName":
                        context.Response.Write(SelectRoleName(context));
                        break;
                    //修改个人密码
                    case "EditMyPwd":
                        context.Response.Write(EditMyPwd());
                        break;
                    //获取跟进厂商业务员
                    case "GetBusineessMan":
                        context.Response.Write(GetBusinessMan());
                        break;
                    //根据SPId获取业务员
                    case "GetBusineessManBySPID":
                        context.Response.Write(GetBusineessManBySPID());
                        break;
                    case "GetAdminByCpId":
                        context.Response.Write(GetAdminByCpId());
                        break;
                    //从缓存中得到用户名
                    case "GetCookiesByName":
                        context.Response.Write(GetCookiesByName());
                        break;
                    //自动登陆填写帐号密码
                    case "autoCompleteUserName":
                        context.Response.Write(AutoCompleteUserName());
                        break;
                    //验证是否登陆超时
                    case "CheckTimeOut":
                        context.Response.Write(CheckTimeOut(context));
                        break;
                    //获得用于实现单点登陆加密的字串
                    case "GetEncryptString":
                        context.Response.Write(GetEncryptString(context));
                        break;
                    //获取登陆用户信息
                    case "GetAdminInfo":
                        context.Response.Write(GetAdminInfo(context));
                        break;
                    //获取登陆系统版本当前信息
                    case "GetVersionInfo":
                        context.Response.Write(GetVersionInfo());
                        break;
                    //获取主页中的导航索引菜单
                    case "GetIndexMenu":
                        context.Response.ContentType = "text/plain";
                        context.Response.Charset = "utf-8";
                        context.Response.Write(GetIndexMenu(context));
                        break;
                    //获取主页中的导航索引菜单
                    case "GetIndexMenuFM":
                        context.Response.ContentType = "text/plain";
                        context.Response.Charset = "utf-8";
                        context.Response.Write(GetIndexMenuFM(context));
                        break;
                }
            }
            context.Response.End();
        }

        private string GetIndexMenu(HttpContext context)
        {
            return GetIndexHtml(context);
        }

        private string GetIndexMenuFM(HttpContext context)
        {
            return GetIndexHtml(context);
        }


        private string GetIndexHtml(HttpContext context)
        {
            AdminModel am = context.Session["Admin"] as AdminModel;
            string strHtmlMenu = string.Empty;
            int iParentSystem = 0;
            int.TryParse(HttpContext.Current.Request.Form["ParentSystem"], out iParentSystem);
            SubSystemPer ssp = new SubSystemPer();
            ssp = am.PerModule.SubSystemPers.Where(item => item.Key == iParentSystem).First().Value;
            foreach (ModulePer modPer in PermissionUtility.GetModulePer(ssp, string.Empty))
            {
                strHtmlMenu += "<li><a href=\"#\">" + modPer.Name + "</a><ul>";
                foreach (PagePer pp in modPer.PagePers.Values)
                {
                    if (pp.Code == "Default.aspx" || pp.Code.Equals(ConstantValue.SystemConst.DATA_PERMISSION))
                    {
                        continue; //在菜单中不显示
                    }

                    if (pp.Code.IndexOf(".aspx") > -1)
                    {
                        strHtmlMenu += "<li><a href=\"" + "/Admin/" + pp.Code
                            + "\" target=\"navTab\" rel=\"" + pp.ID
                            + "\" external=\"true\">" + pp.Name + "</a></li>";
                    }

                    //<a href="w_removeSelected.html" target="navTab" rel="w_table">表格数据库排序+批量删除</a>
                    if (pp.Code.IndexOf(".html") > -1)
                    {
                        strHtmlMenu += "<li><a href=\"" + "/Admin/" + pp.Code
                            + "\" target=\"navTab\" rel=\"" + pp.ID
                            + "\">" + pp.Name + "</a></li>";
                    }
                }
                strHtmlMenu += "</ul></li>";
            }
            return strHtmlMenu;
        }
        /// <summary>
        /// 获取登陆用户信息
        /// </summary>
        /// <returns></returns>
        private string GetVersionInfo()
        {

            PageResult result = new PageResult();
            result.State = 0;
            result.Info = "出现异常，获取系统版本信息失败。";

            string path = AppDomain.CurrentDomain.BaseDirectory + "/VersionInfo.txt";
            if (File.Exists(path))
            {
                result.State = 1;
                result.Info = "获取系统版本信息成功。";
                result.Remark = string.Empty;
                foreach (string s in File.ReadAllLines(path, System.Text.Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        result.Remark += s.Trim() + "<br>";
                    }
                }
                return result.ToString();
            }
            else
            {
                result.Info = "无系统版本信息文件内容，获取失败。";
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取登陆用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetAdminInfo(HttpContext context)
        {
            PageResult result = new PageResult();
            if (context.Session["Admin"] == null)
            {
                result.State = 0;
                result.Info = "网络异常，请刷新重试。";
                return result.ToString();
            }
            AdminModel model = context.Session["Admin"] as AdminModel;
            result.State = 1;
            result.Info = "获取用户信息成功。";
            result.Remark = "<p><span>标识主键ID：" + model.AdminID + "</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            result.Remark += "<span>登陆账号：" + model.Username + "</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            result.Remark += "<span>登陆用户：" + model.Name + "</span></p>";
            string strRoleNames = string.Empty;
            if (model.RoleID.Length > 0)
            {
                foreach (string str in model.RoleID)
                {
                    if (strRoleNames.IndexOf(str) == -1)
                    {
                        strRoleNames += str + ",";
                    }
                }
                if (strRoleNames != string.Empty)
                {
                    if (strRoleNames.EndsWith(","))
                    {
                        strRoleNames = strRoleNames.Remove(strRoleNames.Length - 1);
                    }
                    Semitron_OMS.BLL.Common.Role bllRole = new Semitron_OMS.BLL.Common.Role();
                    List<RoleModel> lstRoleModel = bllRole.GetModelList("RoleID in (" + strRoleNames + ")");
                    strRoleNames = string.Empty;
                    foreach (RoleModel rm in lstRoleModel)
                    {
                        strRoleNames += rm.RoleName + "，";
                    }
                }
            }
            if (strRoleNames.EndsWith("，"))
            {
                strRoleNames = strRoleNames.Remove(strRoleNames.Length - 1);
            }
            result.Remark += "<p><span>所属角色：" + strRoleNames + "</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            result.Remark += "<span>最后登陆时间：" + model.LastLoginTime + "</span></p>";
            return result.ToString();
        }

        /// <summary>
        /// 获得用于实现单点登陆加密的字串
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetEncryptString(HttpContext context)
        {
            PageResult result = new PageResult();
            if (context.Session["Admin"] == null)
            {
                result.State = 0;
                result.Info = "连接超时，请重新登陆。";
                return result.ToString();
            }
            AdminModel model = context.Session["Admin"] as AdminModel;
            string strPassword = DESEncrypt.Decrypt(model.Password);//登陆密码
            string strUserName = model.Username; //登陆用户
            string strSubSys = DataUtility.GetPageFormValue(HttpContext.Current.Request.Form["Type"], string.Empty);
            string strIp = context.Request.UserHostAddress; //本地IP地址

            if (!string.IsNullOrEmpty(strPassword) && !string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strIp))
            {
                result.State = 1;
                result.Info = "OK";
                result.Remark = strUserName + "$$$" + model.Password + "$$$" + DESEncrypt.Encrypt(strSubSys + strIp + strUserName + strPassword);
                return result.ToString();
            }
            result.State = 0;
            result.Info = "登陆验证失败，未知错误，请联系管理员。";
            return result.ToString();
        }

        /// <summary>
        /// 验证是否登陆超时
        /// </summary>
        /// <returns></returns>
        private string CheckTimeOut(HttpContext context)
        {
            PageResult result = new PageResult();
            if (context.Session["Admin"] == null)
            {
                string strUser = string.Empty;
                string strPwd = string.Empty;
                HttpCookie Cookie = CookiesHelper.GetCookie(ConstantValue.CookieKeys.LoginAdminModel);
                if (Cookie != null)
                {
                    strUser = Cookie.Values["uName"];
                    strPwd = Cookie.Values["uPassword"];
                }
                if (!string.IsNullOrEmpty(strUser) && !string.IsNullOrEmpty(strPwd))
                {
                    return Login(strUser, strPwd, false);
                }
                result.State = 0;
                result.Info = "网络异常，请刷新重试。";
                return result.ToString();
            }
            result.State = 1;
            result.Info = "OK";
            return result.ToString();
        }

        /// <summary>
        /// 根据SPId获取业务员
        /// </summary>
        /// <returns></returns>
        private string GetBusineessManBySPID()
        {
            HttpRequest request = HttpContext.Current.Request;
            PageResult resut = new PageResult();
            string spId = string.Empty;
            if (request.Form["spids"] == null || request.Form["spids"].ToString() == string.Empty)
            {
                resut.Info = "参数异常，";
                return resut.ToString();
            }
            spId = request.Form["spids"].ToString();
            int PageIndex = 1;          //页码
            int pageSize = 1;           //页大小
            string tblName = " Admin as A left join AdminBindSPInfo as B on A.AdminID=B.AdminId and b.SPID=" + spId + " and b.Valid=1  left join SPInfo s on b.SPID=s.id left join UserRole ur on ur.AdminID=a.AdminID";  //表名
            string strGetFields = " a.AdminID,a.Username ,Name ,a.Phone,a.Email,case when s.id is null then 'false' else  'true' end as checked999";      //查询列名
            string strOrder = string.Empty; //排序字段
            int OrderType = 1;              //排序类型,1降序，0升序
            string condition = string.Empty;
            condition = " RoleID=11 ";       //查询条件
            if (request.Form["RoleType"] != null && request.Form["RoleType"].ToString() != string.Empty)
            {
                condition = " RoleID= " + request.Form["RoleType"];
            }
            int rowsCount = 0;
            PageResult result = new PageResult();
            Semitron_OMS.BLL.Common.UserRole ur = new UserRole();
            string fuzeren = string.Empty;
            if (request.Form["fuzeren"] != null)
            {
                fuzeren = request.Form["fuzeren"].ToString();
            }
            if (request.Form["page"] != null)
            {
                PageIndex = int.Parse(request.Form["page"].ToString());
            }
            if (request.Form["rp"] != null)
            {
                pageSize = int.Parse(request.Form["rp"].ToString());
            }
            if (PageIndex == 1)
            {
                strOrder = "checked999";
            }
            else
            {
                if (request.Form["sortname"] != null)
                {
                    strOrder = request.Form["sortname"].ToString();
                }
            }
            if (request.Form["sortorder"] != null)
            {
                OrderType = request.Form["sortorder"].ToString().ToLower() == "asc" ? 0 : 1;
            }
            if (request.Form["Username"] != null && request.Form["Username"].ToString() != string.Empty)
            {
                condition += " and Username like'%" + request.Form["Username"].ToString() + "%'";
            }
            if (request.Form["Name"] != null && request.Form["Name"].ToString() != string.Empty)
            {
                condition += " and Name like '%" + request.Form["Name"].ToString() + "%'";
            }
            if (request.Form["Phone"] != null && request.Form["Phone"].ToString() != string.Empty)
            {
                condition += " and Phone like '%" + request.Form["Phone"].ToString() + "%'";
            }
            if (request.Form["Email"] != null && request.Form["Email"].ToString() != string.Empty)
            {
                condition += " and Email like '%" + request.Form["Email"].ToString() + "%'";
            }
            DataSet ds = new DataSet();
            CommonBLL bll = new Semitron_OMS.BLL.Common.CommonBLL();
            //调用通用存储过程
            ds = bll.GetData(tblName, strGetFields, pageSize, PageIndex, condition, strOrder, OrderType, out rowsCount);
            //转化JSON格式
            return JsonJqgrid.JsonForJqgrid(ds.Tables[0], PageIndex, rowsCount);
        }
        /// <summary>
        /// 自动登陆填写帐号密码
        /// </summary>
        /// <returns></returns>
        public string AutoCompleteUserName()
        {
            HttpRequest requst = HttpContext.Current.Request;
            string UserName = "";
            if (requst.Params["q"] != null && requst.Params["q"].ToString() != string.Empty)
            {
                string userName = requst.Params["q"].ToString();
                for (int i = 0; i < 30; i++)
                {
                    if (requst.Cookies.Count > i)
                    {
                        if (requst.Cookies["UserInfo" + i] != null && requst.Cookies["UserInfo" + i]["uName"] != null && requst.Cookies["UserInfo" + i]["uName"].IndexOf(userName) >= 0)
                        {
                            UserName += "{\"UserName\":\"" + requst.Cookies["UserInfo" + i]["uName"] + "\"},";
                        }
                    }
                }
                if (UserName.EndsWith(","))
                {
                    UserName = "[" + UserName.Substring(0, UserName.Length - 1) + "]";
                }
            }
            return UserName;
        }
        /// <summary>
        /// 从缓存中得到用户名
        /// </summary>
        /// <returns></returns>
        public string GetCookiesByName()
        {
            HttpRequest requst = HttpContext.Current.Request;
            string pwd = string.Empty;

            if (requst.Form["Username"] != null && requst.Form["Username"].ToString() != string.Empty)
            {
                string userName = requst.Form["Username"].ToString();
                for (int i = 0; i < 30; i++)
                {
                    if (requst.Cookies.Count > i)
                    {
                        if (requst.Cookies[i] != null && requst.Cookies[i]["uName"] == userName)
                        {
                            HttpCookie cookie = requst.Cookies[i];
                            pwd = cookie["uPassword"].ToString();
                            break;
                        }
                    }
                }

            }
            return pwd;
        }
        /// <summary>
        /// 通过CpId获取厂商的登陆名和密码
        /// </summary>
        /// <returns></returns>
        public string GetAdminByCpId()
        {
            HttpRequest requst = HttpContext.Current.Request;
            PageResult result = new PageResult();
            Semitron_OMS.BLL.Common.Admin adminbll = new BLL.Common.Admin();

            if (requst.Form["CPId"] == null && requst.Form["CPId"].ToString() == string.Empty)
            {
                result.Info = "参数异常";
                return result.ToString();
            }
            string cpId = requst.Form["CpId"].ToString();
            DataSet ds = adminbll.GetList(" type=2 and CustID=" + cpId);
            //string str = string.Empty;
            //string userName = string.Empty;
            //string password = string.Empty;
            //string condition = "A.AvailFlag=1 and R.AvailFlag=1 and P.AvailFlag=1 and O.AvailFlag=1";
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
            //        userName = row["Username"].ToString();
            //        password = row["Password"].ToString();
            //    }

            //}
            string returnValue = string.Empty;
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.State = 1;
                    returnValue = "{\"UserName\":\"" + row["Username"] + "\",\"Password\":\"" + row["Password"] + "\"}";
                }
                return returnValue;
            }
            else
            {
                result.State = -1;
                result.Info = "暂时无此账号的cp用户，请注册";
                return result.ToString();
            }
        }
        /// <summary>
        /// 获取跟进厂商业务员
        /// </summary>
        /// <returns></returns>
        private string GetBusinessMan()
        {
            HttpRequest request = HttpContext.Current.Request;
            PageResult resut = new PageResult();
            string cpId = string.Empty;
            if (request.Form["cpids"] == null || request.Form["cpids"].ToString() == string.Empty)
            {
                resut.Info = "参数异常，";
                return resut.ToString();
            }
            cpId = request.Form["cpids"].ToString();
            int PageIndex = 1;          //页码
            int pageSize = 1;           //页大小
            string tblName = " Admin as A left join AdminBindCPInfo as B on A.AdminID=B.AdminId and b.cpid=" + cpId + " and b.Valid=1  left join cpinfo c on b.cpid=c.id left join UserRole ur on ur.AdminID=a.AdminID";  //表名
            string strGetFields = " a.AdminID,a.Username ,Name ,a.Phone,a.Email,case when c.id is null then 'false' else  'true' end as checked999";      //查询列名
            string strOrder = string.Empty; //排序字段
            int OrderType = 1;              //排序类型,1降序，0升序
            string condition = string.Empty;
            condition = " RoleID=11 ";       //查询条件
            if (request.Form["RoleType"] != null && request.Form["RoleType"].ToString() != string.Empty)
            {
                condition = " RoleID= " + request.Form["RoleType"];
            }
            int rowsCount = 0;
            PageResult result = new PageResult();
            Semitron_OMS.BLL.Common.UserRole ur = new UserRole();
            string fuzeren = string.Empty;
            if (request.Form["fuzeren"] != null)
            {
                fuzeren = request.Form["fuzeren"].ToString();
            }
            if (request.Form["page"] != null)
            {
                PageIndex = int.Parse(request.Form["page"].ToString());
            }
            if (request.Form["rp"] != null)
            {
                pageSize = int.Parse(request.Form["rp"].ToString());
            }
            if (PageIndex == 1)
            {
                strOrder = "checked999";
            }
            else
            {
                if (request.Form["sortname"] != null)
                {
                    strOrder = request.Form["sortname"].ToString();
                }
            }
            if (request.Form["sortorder"] != null)
            {
                OrderType = request.Form["sortorder"].ToString().ToLower() == "asc" ? 0 : 1;
            }
            if (request.Form["Username"] != null && request.Form["Username"].ToString() != string.Empty)
            {
                condition += " and a.Username like'%" + request.Form["Username"].ToString() + "%'";
            }
            if (request.Form["Name"] != null && request.Form["Name"].ToString() != string.Empty)
            {
                condition += " and Name like '%" + request.Form["Name"].ToString() + "%'";
            }
            if (request.Form["Phone"] != null && request.Form["Phone"].ToString() != string.Empty)
            {
                condition += " and a.Phone like '%" + request.Form["Phone"].ToString() + "%'";
            }
            if (request.Form["Email"] != null && request.Form["Email"].ToString() != string.Empty)
            {
                condition += " and a.Email like '%" + request.Form["Email"].ToString() + "%'";
            }
            DataSet ds = new DataSet();
            CommonBLL bll = new Semitron_OMS.BLL.Common.CommonBLL();
            //调用通用存储过程
            ds = bll.GetData(tblName, strGetFields, pageSize, PageIndex, condition, strOrder, OrderType, out rowsCount);
            //转化JSON格式
            return JsonJqgrid.JsonForJqgrid(ds.Tables[0], PageIndex, rowsCount);

        }
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <returns></returns>
        private string GetUserlogin()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            StringBuilder sb = new StringBuilder();
            string code = "";

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["CheckCode"] != null)
            {
                code = HttpContext.Current.Session["CheckCode"].ToString();
            }
            else
            {
                result.Info = "网络异常，请刷新重试。";
                return result.ToString();
            }
            if (code.ToLower() != request.Form["code"].ToString().ToLower())
            {
                result.Info = "请输入正确的验证码。";
                return result.ToString();
            }

            string pwd = DESEncrypt.Encrypt(request.Form["_password"].ToString());
            string user = request.Form["_username"].ToString();


            if (Convert.ToBoolean(request.Form["remUsername"]))
            {
                //int num = -1;
                //for (int i = 0; i < 30; i++)
                //{
                //    HttpCookie Cookie = CookiesHelper.GetCookie("UserInfo" + i);
                //    if (Cookie != null)
                //    {
                //        if (Cookie.Values["uName"] == null || !Cookie.Values["uName"].Equals(user))
                //        {
                //            continue;
                //        }
                //        else
                //        {
                //            CookiesHelper.SetCookie("UserInfo" + i, "uPassword", request.Form["_password"].ToString(), null);
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        num = i;
                //        break;
                //    }

                //}
                //if (num != -1)
                //{
                //    HttpCookie Cookie = CookiesHelper.GetCookie("UserInfo" + num);
                //    if (Cookie == null)
                //    {
                //        Cookie = new HttpCookie("UserInfo" + num);
                //        Cookie.Values.Add("uName", user);
                //        Cookie.Values.Add("uPassword", request.Form["_password"].ToString());
                //        //设置Cookie过期时间
                //        Cookie.Expires = DateTime.Now.AddDays(365);
                //        CookiesHelper.AddCookie(Cookie);
                //    }
                //}
            }
            return Login(user, pwd, true);
        }

        /// <summary>
        /// 登陆
        /// </summary>
        private string Login(string username, string pwd, bool bFirst)
        {
            Semitron_OMS.BLL.Common.Admin ad = new Semitron_OMS.BLL.Common.Admin();
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            string IP = request.UserHostAddress;
            string ParentSystem = ConfigurationManager.AppSettings["ParenSystem"].ToString();
            AdminModel model = new AdminModel();
            try
            {
                model = ad.Login(ParentSystem, IP, username, pwd, out  result);
                if ((model == null || model.AdminID <= 0) && string.IsNullOrEmpty(result.Info))
                {
                    result.State = 0;
                    result.Info = "对不起，程序内部错误，无法登陆。";
                    return result.ToString();
                }

                if (model != null && model.AdminID > 0)
                {
                    HttpContext.Current.Session["Admin"] = model;
                    if (bFirst || CookiesHelper.GetCookie(ConstantValue.CookieKeys.LoginAdminModel) == null)
                    {
                        //增加Cookies，当下一次超时时，自动登陆
                        CookiesHelper.AddLoginCookie(ConstantValue.CookieKeys.LoginAdminModel, username, pwd);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(result.Info))
                    {
                        result.State = 0;
                        result.Info = "登陆失败，未知错误。";
                    }
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Info = "登陆异常，请联系管理员。";
                _myLogger.Error("登陆用户名：" + username + ",客户机IP:" +
                    IP + "，登陆异常:" + ex.Message, ex);
            }
            return result.ToString();
        }
        /// <summary>
        /// 用户的显示
        /// </summary>
        /// <returns></returns>
        private string GetAdminList()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return "";
            }
            HttpRequest request = HttpContext.Current.Request;
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();
            //返回查询总数总数
            int o_RowsCount = 0;
            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();

            //获取表格提交的参数
            //当前查询页码
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["page"], 1));
            //每页的大小
            searchInfo.PageSize = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["rp"], 20));
            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(request.Form["sortname"], string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            //用户名
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Username", request.Form["Username"], ConditionEnm.Equal));
            //姓名
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Name", request.Form["Name"], ConditionEnm.AllLike));
            //状态
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("AvailFlag", request.Form["AvailFlag"], ConditionEnm.Equal));
            //用户类型
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("Type", request.Form["Type"], ConditionEnm.Equal));
            //用户所属
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("CustID", request.Form["CustID"], ConditionEnm.Equal));
            //角色
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("RoleID", request.Form["RoleID"], ConditionEnm.Equal));

            searchInfo.ConditionFilter = lstFilter;

            //查询数据
            Semitron_OMS.BLL.Common.Admin bllAdmin = new BLL.Common.Admin();
            DataSet ds = bllAdmin.GetAdminPageData(searchInfo, out o_RowsCount);
            //转化JSON格式
            return JsonJqgrid.JsonForJqgrid(ds.Tables[0], searchInfo.PageIndex, o_RowsCount);
        }

        /// <summary>
        /// 查询角色名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string SelectRoleName(HttpContext context)
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return "";
            }
            CustInfoModel model = new CustInfoModel();
            Semitron_OMS.BLL.Common.Role cf = new Semitron_OMS.BLL.Common.Role();
            DataTable dt = cf.GetAllList().Tables[0];
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{\"RoleName\":\"" + dr["RoleName"].ToString() + "\",\"RoleID\":\"" + dr["RoleID"].ToString() + "\"},");
                }

                if (sb.ToString().EndsWith(","))
                {
                    //移除最后一个逗号
                    sb.Remove(sb.ToString().Length - 1, 1);
                }
                sb.Append("]");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <returns></returns>
        public string AddAdminInfo()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return new Result().ToString();
            }
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            AdminModel model = new AdminModel();
            Semitron_OMS.BLL.Common.Admin adminModel = new Semitron_OMS.BLL.Common.Admin();
            if (request.Form["Username"] == null || request.Form["Password"] == null || request.Form["Name"] == null || request.Form["Phone"] == null)
            {
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }
            //用户名
            SQLOperateHelper.SetEntityFiledValue(model, "Username", request.Form["Username"]);
            string pwd = DESEncrypt.Encrypt(request.Form["Password"].ToString());
            //密码
            SQLOperateHelper.SetEntityFiledValue(model, "Password", pwd);
            //姓名
            SQLOperateHelper.SetEntityFiledValue(model, "Name", request.Form["Name"]);
            //手机号
            SQLOperateHelper.SetEntityFiledValue(model, "Phone", request.Form["Phone"]);
            //邮箱地址
            SQLOperateHelper.SetEntityFiledValue(model, "Email", request.Form["Email"]);
            //用户所属
            SQLOperateHelper.SetEntityFiledValue(model, "CustID", request.Form["firm"]);

            result.Info = "新增后台用户失败!错误原因：可能是用户登陆超时!";
            string[] arrStrTemp = null;
            if (request.Form["slt"] != null && request.Form["slt"].ToString() != string.Empty)
            {
                arrStrTemp = request.Form["slt"].ToString().Trim().Split(',');
                if (arrStrTemp.Length == 2)
                {
                    model.Type = int.Parse(arrStrTemp[0]); //用户类型
                    if (adminModel.ExistsByName(request.Form["Username"].ToString()))
                    {
                        result.State = 0;
                        result.Info = "用户已存在。";
                    }
                    else
                    {
                        if (adminModel.Add(model, arrStrTemp[1]) > 1)
                        {
                            result.State = 1;
                            result.Info = "新增成功。";
                        }
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改后台用户
        /// </summary>
        /// <returns></returns>
        public string UpdateAdminInfo()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return new Result().ToString();
            }
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;

            Semitron_OMS.BLL.Common.Admin adminModel = new Semitron_OMS.BLL.Common.Admin();
            if (request.Form["Username"] == null || request.Form["Password"] == null || request.Form["Name"] == null || request.Form["Phone"] == null)
            {
                result.Info = "系统错误,参数获取异常。";
                return result.ToString();
            }

            int Id = int.Parse(request.Form["id"].ToString());

            AdminModel model = adminModel.GetModel(Id);

            //用户名
            SQLOperateHelper.SetEntityFiledValue(model, "Username", request.Form["Username"]);
            //string pwd = DESEncrypt.Encrypt(request.Form["Password"].ToString());
            //密码
            //SQLOperateHelper.SetEntityFiledValue(model, "Password", pwd);
            //姓名
            SQLOperateHelper.SetEntityFiledValue(model, "Name", request.Form["Name"]);
            //手机号
            SQLOperateHelper.SetEntityFiledValue(model, "Phone", request.Form["Phone"]);
            //邮箱地址
            SQLOperateHelper.SetEntityFiledValue(model, "Email", request.Form["Email"]);
            //用户所属
            SQLOperateHelper.SetEntityFiledValue(model, "CustID", request.Form["firm"]);

            if (request.Form["slt"] != null && request.Form["slt"].ToString() != string.Empty)
            {
                string[] arrStrTemp = request.Form["slt"].ToString().Trim().Split(',');
                if (arrStrTemp.Length == 2)
                {
                    model.Type = int.Parse(arrStrTemp[0]); //用户类型
                }
            }
            result.Info = "修改后台用户失败。";

            if (adminModel.UpdateAdmin(model))
            {
                result.State = 1;
                result.Info = "修改成功。";
            }
            return result.ToString();
        }
        /// <summary>
        /// 删除后台用户
        /// </summary>
        /// <returns></returns>
        private string DeleteAdminInfo()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return new Result().ToString();
            }
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            Semitron_OMS.BLL.Common.Admin adminModel = new Semitron_OMS.BLL.Common.Admin();
            int id = 0;
            if (request.Form["id"].ToString() != string.Empty)
            {
                id = int.Parse(request.Form["id"].ToString());
            }
            result.Info = "删除后台用户错误!";
            if (adminModel.UpdateAdmin(id))
            {
                result.State = 1;
                result.Info = "删除成功。";
            }
            return result.ToString();
        }
        /// <summary>
        /// 启用后台用户
        /// </summary>
        /// <returns></returns>
        private string EnabledAdminInfo()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return new Result().ToString();
            }
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            Semitron_OMS.BLL.Common.Admin adminModel = new Semitron_OMS.BLL.Common.Admin();
            int id = 0;
            if (request.Form["id"].ToString() != string.Empty)
            {
                id = int.Parse(request.Form["id"].ToString());
            }
            result.Info = "启用后台用户错误!";
            if (adminModel.EnabledAdmin(id))
            {
                result.State = 1;
                result.Info = "启用成功。";
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        private string UpdatePwd()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return new Result().ToString();
            }
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            AdminModel am = HttpContext.Current.Session["Admin"] as AdminModel;
            Semitron_OMS.BLL.Common.Admin ad = new Semitron_OMS.BLL.Common.Admin();
            int id = 0;
            string pwd = "";
            if (request.Form["txtFresh"] != null && request.Form["txtFresh"].ToString() != string.Empty)
            {
                pwd = DESEncrypt.Encrypt(request.Form["txtFresh"].ToString());
            }

            if (request.Form["id"] != null && request.Form["id"].ToString() != string.Empty)
            {
                id = int.Parse(request.Form["id"]);
            }
            else
            {
                id = am.AdminID;
            }
            result.Info = "修改密码失败!";
            if (ad.UpdateNamePwd(pwd, id))
            {
                result.State = 1;
                result.Info = "修改密码成功!";
            }
            return result.ToString();
        }

        /// <summary>
        /// 修改个人密码
        /// </summary>
        /// <returns></returns>
        private string EditMyPwd()
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                return new Result().ToString();
            }
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            AdminModel adminModel = HttpContext.Current.Session["Admin"] as AdminModel;
            Semitron_OMS.BLL.Common.Admin admin = new Semitron_OMS.BLL.Common.Admin();
            int id = 0;
            string oldPwd = "";
            string newPwd = "";
            if (request.Form["oldPassWord"] == null || request.Form["oldPassWord"].ToString() == string.Empty
               || request.Form["newPassword"] == null || request.Form["newPassword"].ToString() == string.Empty)
            {
                result.Info = "密码数据异常，请联系后台用户。";
                return result.ToString();
            }
            oldPwd = DESEncrypt.Encrypt(request.Form["oldPassWord"].ToString());
            newPwd = DESEncrypt.Encrypt(request.Form["newPassword"].ToString());
            if (oldPwd != adminModel.Password)
            {
                result.Info = "旧密码输入错误，请重新输入。";
                return result.ToString();
            }
            id = adminModel.AdminID;
            result.Info = "修改密码失败!";
            if (admin.UpdateNamePwd(newPwd, id))
            {
                result.State = 1;
                result.Info = "修改密码成功!";
            }
            return result.ToString();
        }
        /// <summary>
        /// 修改是查询最新数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string SelectUpdate(HttpContext context)
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            Semitron_OMS.BLL.Common.Admin admin = new Semitron_OMS.BLL.Common.Admin();
            if (request.Form["id"] == null || request.Form["id"].ToString() == string.Empty)
            {
                result.Info = "ID参数异常。";
                return result.ToString();
            }
            int adminId = int.Parse(request.Form["id"].ToString());
            AdminModel model = admin.GetModel(adminId);
            result.State = 1;
            result.Remark = JsonConvert.SerializeObject(model, Formatting.Indented, new IsoDateTimeConverter());
            return result.Remark.ToString();
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