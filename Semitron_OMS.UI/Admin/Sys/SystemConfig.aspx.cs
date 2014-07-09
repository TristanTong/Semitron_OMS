using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Semitron_OMS.CommonWeb;

namespace Semitron_OMS.UI.Admin.Sys
{
    public partial class SystemConfig : System.Web.UI.Page
    {
        private log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(SystemConfig));//系统日志对象

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //将Web.config中的相关值填入TextBox
                System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
                //超级管理员id
                this.txtSuperAdminRoleId.Text = Convert.ToString(appReader.GetValue("SuperAdminRoleId", typeof(string)));
                //系统父级权限ID
                this.txtParentSystem.Text = Convert.ToString(appReader.GetValue("ParentSystem", typeof(string)));
                //系统版本号名称
                this.txtSystemVersionInfo.Text = Convert.ToString(appReader.GetValue("SystemVersionInfo", typeof(string)));
                //上传文件目录地址
                this.txtImgPath.Text = Convert.ToString(appReader.GetValue("ImgPath", typeof(string)));
                this.txtIcoPath.Text = Convert.ToString(appReader.GetValue("IcoPath", typeof(string)));
                this.txtApkPath.Text = Convert.ToString(appReader.GetValue("ApkPath", typeof(string)));
                //文件服务器物理地址
                this.txtFileServerPath.Text = Convert.ToString(appReader.GetValue("FileServerPath", typeof(string)));
                //文件服务器URL地址
                this.txtFileServerUrl.Text = Convert.ToString(appReader.GetValue("FileServerUrl", typeof(string)));
                //Semitron_OMS Servers配置页面地址
                this.txtSemitron_OMSServerUrl.Text = Convert.ToString(appReader.GetValue("Semitron_OMSServerUrl", typeof(string)));
                //设置Servers页面地址
                this.iframeServerUrl.Attributes["src"] = Convert.ToString(appReader.GetValue("Semitron_OMSServerUrl", typeof(string)));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strIp = Request.UserHostAddress; //本地IP地址
            string strUserIdentityName = Request.LogonUserIdentity.Name;//本地计算机名
            try
            {
                //调用
                ConfigurationOperator configOper = new ConfigurationOperator();
                //添加一个节点，如果有就修改
                //超级管理员id
                configOper.SetAppSetting("SuperAdminRoleId", this.txtSuperAdminRoleId.Text);
                //系统父级权限ID
                configOper.SetAppSetting("ParentSystem", this.txtParentSystem.Text);
                //系统版本号名称
                configOper.SetAppSetting("SystemVersionInfo", this.txtSystemVersionInfo.Text);
                //上传文件目录地址
                configOper.SetAppSetting("ImgPath", this.txtImgPath.Text);
                configOper.SetAppSetting("IcoPath", this.txtIcoPath.Text);
                configOper.SetAppSetting("ApkPath", this.txtApkPath.Text);
                //文件服务器物理地址
                configOper.SetAppSetting("FileServerPath", this.txtFileServerPath.Text);
                //文件服务器URL地址
                configOper.SetAppSetting("FileServerUrl", this.txtFileServerUrl.Text);
                //Semitron_OMS Servers配置页面地址
                configOper.SetAppSetting("Semitron_OMSServerUrl", this.txtSemitron_OMSServerUrl.Text);
                configOper.Save();
                ClientScript.RegisterStartupScript(this.GetType(), "SystemConfig", "<script>artDialog.tips(\"保存成功。\");</script>");
                Semitron_OMS.BLL.Common.OperationsLogBLL logBLL = new Semitron_OMS.BLL.Common.OperationsLogBLL();

                logBLL.AddExecute("Semitron_OMS.UI.Admin.Sys.SystemConfig", "进行系统基本设置。" + "IP:" + strIp + "本地计算机名：" + strUserIdentityName, "", (int)Semitron_OMS.Model.Common.OperationsType.Add);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "SystemConfig", "<script>artDialog.tips(\"保存失败。\");</script>");
                _myLogger.Error("Web系统配置页面保存配置信息出现异常。" + "IP:" + strIp + "本地计算机名：" + strUserIdentityName, ex);
            }
        }
    }
}