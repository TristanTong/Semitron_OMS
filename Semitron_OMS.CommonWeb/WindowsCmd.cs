using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web;
using Semitron_OMS.Common;

namespace Semitron_OMS.CommonWeb
{
    /// <summary>
    /// 执行Windows命令
    /// </summary>
    public class WindowsCmd
    {

        /// <summary>
        /// 执行控制台命令
        /// </summary>
        /// <param name="strCmd"></param>
        public static void ExecuteConsole(string strExcCmdPath, string strCmd)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = strExcCmdPath;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = strCmd;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UserName = ConfigHelper.GetConfigString(ConstantValue.AppSettingsNames.SERVER_USERNAME);
                System.Security.SecureString password = new System.Security.SecureString();
                string pwd = ConfigHelper.GetConfigString(ConstantValue.AppSettingsNames.SERVER_PASSWORD);
                for (int i = 0; i < pwd.Length; i++)
                {
                    password.AppendChar(pwd[i]);
                }

                process.StartInfo.Password = password;
                process.Start();

                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                process.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行Windows命令
        /// </summary>
        /// <param name="strCmd">Windows命令</param>
        /// <returns>执行结果</returns>
        /// <param name="strTxtFilePath">执行结果追加的文件路径</param>
        /// <returns></returns>
        public static string ExecuteCmd(string strCmd, string strTxtFilePath)
        {
            string strFolder = System.IO.Path.GetDirectoryName(strTxtFilePath);
            //文件目录不存在则创建一个文件目录
            if (!System.IO.Directory.Exists(strFolder))
            {
                System.IO.Directory.CreateDirectory(strFolder);
            }

            //删除结果文件并创建新的结果文件
            if (System.IO.File.Exists(strTxtFilePath))
            {
                System.IO.File.Delete(strTxtFilePath);
            }

            //请求IP地址
            string strUserHostAddress = HttpContext.Current.Request.UserHostAddress;
            //string strUserName = ConfigHelper.GetConfigString(ConstantValue.AppSettingsNames.SERVER_USERNAME);
            //string strPassword = ConfigHelper.GetConfigString(ConstantValue.AppSettingsNames.SERVER_PASSWORD);
            Process process = new Process();
            try
            {
                //string strCmdPath =  HttpContext.Current.Server.MapPath("../../");
                //strCmdPath = System.IO.Path.Combine(strCmdPath, "bin\\cmd.exe");

                process.StartInfo.FileName = "cmd.exe";

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                #region 暂时注释此部分代码
                //if (!strUserHostAddress.Equals("127.0.0.1"))
                //{
                //    process.StartInfo.UserName = strUserName;
                //    System.Security.SecureString password = new System.Security.SecureString();
                //    string pwd = strPassword;
                //    for (int i = 0; i < pwd.Length; i++)
                //    {
                //        password.AppendChar(pwd[i]);
                //    }
                //    process.StartInfo.Password = password;
                //}
                #endregion

                process.Start();
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine(strCmd);
                process.StandardInput.WriteLine("exit");
                string strRst = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();

                return strRst;
            }
            catch (Exception ex)
            {
                process.Close();
                log4net.ILog _myLogger = log4net.LogManager.GetLogger(typeof(WindowsCmd));
                _myLogger.Error(string.Format("客户机IP:{0}，执行Cmd获取APK文件包信息异常。{1}", strUserHostAddress, ex.Message), ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="ProcName">进程名称</param>
        /// <returns>执行结果</returns>
        public static bool CloseProcess(string ProcName)
        {
            bool result = false;
            System.Collections.ArrayList procList = new System.Collections.ArrayList();
            string tempName = "";
            int begpos;
            int endpos;
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                tempName = thisProc.ToString();
                begpos = tempName.IndexOf("(") + 1;
                endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                    {
                        //当发送关闭窗口命令无效时强行结束进程
                        thisProc.Kill();
                    }
                    result = true;
                }
            }
            return result;
        }
    }
}
