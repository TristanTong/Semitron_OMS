using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using Semitron_OMS.Common.Logger;
namespace Semitron_OMS.CommonWeb
{
    /**/
    /// <summary>   
    /// LogHelper的摘要说明。   
    /// </summary>   
    public class LogHelper
    {
        //public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

        //public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 

        public LogHelper(log4net.ILog myLogger)
        {
            MyLogger = myLogger;
        }

        public log4net.ILog MyLogger { get; set; }

        public void WriteLog(LogLevel level, string msg, Exception ex)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                msg = "IP:" + HttpContext.Current.Request.UserHostAddress + "，" + msg;
            }
            switch (level)
            {
                case LogLevel.Debug:
                    MyLogger.Debug(msg, ex);
                    break;
                case LogLevel.Info:
                    MyLogger.Info(msg, ex);
                    break;
                case LogLevel.Error:
                    MyLogger.Error(msg, ex);
                    break;
                case LogLevel.Warn:
                    MyLogger.Warn(msg, ex);
                    break;
                case LogLevel.Fatal:
                    MyLogger.Fatal(msg, ex);
                    break;
                default:
                    break;
            }
            MyLogger = null;
        }


        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 调试错误日志
        /// </summary>
        /// <param name="message">调试错误日志信息</param>
        public static void WriteLogDebug(string MsgStr, Type type)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsDebugEnabled)
            {
                log.Debug(MsgStr);
            }
            log = null;
        }
        /// <summary>
        /// 重要错误日志
        /// </summary>
        /// <param name="message">重要错误日志信息</param>
        public static void WriteLogInfo(string MsgStr, Type type)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsInfoEnabled)
            {
                log.Info(MsgStr);
            }
            log = null;
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">错误日志信息</param>
        public static void WriteLogError(string MsgStr, Exception se, Type type)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsErrorEnabled)
            {
                log.Error(MsgStr, se);
            }
            log = null;
        }

        /// <summary>
        /// 警告错误日志
        /// </summary>
        /// <param name="message">警告错误日志信息</param>
        public static void WriteLogWarn(string MsgStr, Type type)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsWarnEnabled)
            {
                log.Warn(MsgStr);
            }
            log = null;
        }

        /// <summary>
        /// 致命错误日志
        /// </summary>
        /// <param name="message">致命错误日志信息</param>
        public static void WriteLogFatal(string MsgStr, Type type)
        {

            log4net.ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsFatalEnabled)
            {
                log.Fatal(MsgStr);
            }
            log = null;
        }

        public static void WriteLogFatal(string MsgStr, Exception se, Type type)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsErrorEnabled)
            {
                log.Fatal(MsgStr, se);
            }
            log = null;
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">错误日志信息</param>
        public static void WriteLogByType(LogLevel level, string msg, Exception ex, Type type)
        {
            if (HttpContext.Current != null)
            {
                msg = "IP:" + HttpContext.Current.Request.UserHostAddress + "，" + msg;
            }
            log4net.ILog myLogger = log4net.LogManager.GetLogger(type);
            switch (level)
            {
                case LogLevel.Debug:
                    myLogger.Debug(msg, ex);
                    break;
                case LogLevel.Info:
                    myLogger.Info(msg, ex);
                    break;
                case LogLevel.Error:
                    myLogger.Error(msg, ex);
                    break;
                case LogLevel.Warn:
                    myLogger.Warn(msg, ex);
                    break;
                case LogLevel.Fatal:
                    myLogger.Fatal(msg, ex);
                    break;
                default:
                    break;
            }
            myLogger = null;
        }
    }
}
