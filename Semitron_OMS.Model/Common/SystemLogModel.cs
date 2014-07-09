using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [Serializable]
    public partial class SystemLogModel
    {
        public SystemLogModel()
        { }
        #region Model
        private int _logid;
        private string _loglevel;
        private string _msg;
        private string _logthread;
        private string _exception;
        private string _logger;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 系统日志ID
        /// </summary>
        public int LogID
        {
            set { _logid = value; }
            get { return _logid; }
        }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel
        {
            set { _loglevel = value; }
            get { return _loglevel; }
        }
        /// <summary>
        /// 日志描述
        /// </summary>
        public string Msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        /// <summary>
        /// 线程名
        /// </summary>
        public string LogThread
        {
            set { _logthread = value; }
            get { return _logthread; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Exception
        {
            set { _exception = value; }
            get { return _exception; }
        }
        /// <summary>
        /// 日志对象
        /// </summary>
        public string Logger
        {
            set { _logger = value; }
            get { return _logger; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}
