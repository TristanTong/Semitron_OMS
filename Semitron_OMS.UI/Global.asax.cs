using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Semitron_OMS.Common;

namespace Semitron_OMS.UI
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            //System.Data.SqlClient.SqlDependency.Start(System.Configuration.ConfigurationManager.ConnectionStrings["strcodematic"].ConnectionString);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            Exception objExp = HttpContext.Current.Server.GetLastError();
            try
            {
                LogHelper.WriteLogError("客户机IP:" + Request.UserHostAddress + "错误地址:" + Request.Url + "异常信息:" + Server.GetLastError().Message, objExp, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.Message, ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw ex;
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码
            Session.Timeout = 2400;
        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}
