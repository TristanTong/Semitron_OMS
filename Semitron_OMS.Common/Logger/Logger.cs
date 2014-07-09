using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Logger
{
    public class Logger
    {
        class set
        {
            static set()
            {
            }
            internal static readonly Logger Instance = new Logger();
        }
        public static Logger Current
        {
            get { return set.Instance; }
        }
        public static ILog CurrentLog
        {
            get { return set.Instance.CurrentLogger; }
        }
        /// <summary>
        /// 获取当前配置的日志
        /// </summary>
        /// <param name="LogName"></param>
        /// <returns></returns>
        public ILog GetLog(LoggerType type)
        {
            switch (type)
            {
                case LoggerType.Nlog:
                    CurrentLogger = new Nlog();
                    return CurrentLogger;
                case LoggerType.ConSole:
                    CurrentLogger = new ConsoleLog();
                    return CurrentLogger;
                default:
                case LoggerType.LogNet:
                    CurrentLogger = new Log4net();
                    return CurrentLogger;
            }
        }
        /// <summary>
        /// 当前的日志
        /// </summary>
        private ILog CurrentLogger { get; set; }
        /// <summary>
        /// 是否启动日志
        /// </summary>
        public bool IsOpenLog { get; set; }
    }
}
