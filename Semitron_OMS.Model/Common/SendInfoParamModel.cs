using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    [Serializable]
    public class SendInfoParamModel
    {
        public SendInfoParamModel()
        { }
        #region Model
        private int _id;
        private int _configid;
        private int _objectid;
        private string _sendurl;
        private string _sendcontent;
        private int _status = 1;
        private string _sendresult;
        private DateTime? _sendtime;
        private int _failrepeatnum = 0;
        private DateTime _createtime = DateTime.Now;
        private DateTime? _updatetime;
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 参数配置ID
        /// </summary>
        public int ConfigId
        {
            set { _configid = value; }
            get { return _configid; }
        }
        /// <summary>
        /// 发送对象ID
        /// </summary>
        public int ObjectId
        {
            set { _objectid = value; }
            get { return _objectid; }
        }
        /// <summary>
        /// 发送URL
        /// </summary>
        public string SendUrl
        {
            set { _sendurl = value; }
            get { return _sendurl; }
        }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string SendContent
        {
            set { _sendcontent = value; }
            get { return _sendcontent; }
        }
        /// <summary>
        /// 发送状态
        /// 0 未发送 1 待发送 2 发送中 3 已发送 -1 接收异常[即接收方出现了异常。如无法连接远程服务器，如500错误。] -2 请求超时[即TimeOut的异常] -3返回不匹配[即对方返回的内容不为正确值。]
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 验证返回内容是否正确
        /// </summary>
        public string SendResult
        {
            set { _sendresult = value; }
            get { return _sendresult; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 还需重复发送次数
        /// </summary>
        public int FailRepeatNum
        {
            set { _failrepeatnum = value; }
            get { return _failrepeatnum; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model
    }
}
