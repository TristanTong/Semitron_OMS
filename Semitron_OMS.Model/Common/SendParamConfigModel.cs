using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    [Serializable]
    public class SendParamConfigModel
    {
        public SendParamConfigModel()
        { }
        #region Model
        private int _id;
        private string _configcode;
        private string _ownerservicecode;
        private string _ownerservicename;
        private int _sendtype = 1;
        private int _contenttype = 1;
        private int _sendconfirm = 0;
        private string _restr;
        private int _failhandle = 0;
        private int? _repeatcount;
        private int? _repeatinterval;
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
        /// 配置编码
        /// </summary>
        public string ConfigCode
        {
            set { _configcode = value; }
            get { return _configcode; }
        }
        /// <summary>
        /// 所属子业务编码
        /// </summary>
        public string OwnerServiceCode
        {
            set { _ownerservicecode = value; }
            get { return _ownerservicecode; }
        }
        /// <summary>
        /// 所属子业务名称
        /// </summary>
        public string OwnerServiceName
        {
            set { _ownerservicename = value; }
            get { return _ownerservicename; }
        }
        /// <summary>
        /// 发送方式(1.get 2.post)
        /// </summary>
        public int SendType
        {
            set { _sendtype = value; }
            get { return _sendtype; }
        }
        /// <summary>
        /// 发送内容格式（1. url 字符串2. xml 3.数据流）
        /// </summary>
        public int ContentType
        {
            set { _contenttype = value; }
            get { return _contenttype; }
        }
        /// <summary>
        /// 是否进行返回确认（0 否 1 是）
        /// </summary>
        public int SendConfirm
        {
            set { _sendconfirm = value; }
            get { return _sendconfirm; }
        }
        /// <summary>
        /// 发送成功返回值
        /// </summary>
        public string ReStr
        {
            set { _restr = value; }
            get { return _restr; }
        }
        /// <summary>
        /// 接收失败后处理(0.不处理 1.重复发送 2.手工操作发送)
        /// </summary>
        public int FailHandle
        {
            set { _failhandle = value; }
            get { return _failhandle; }
        }
        /// <summary>
        /// 重复发送次数
        /// </summary>
        public int? RepeatCount
        {
            set { _repeatcount = value; }
            get { return _repeatcount; }
        }
        /// <summary>
        /// 重复发送间隔时间（以秒为单位）
        /// </summary>
        public int? RepeatInterval
        {
            set { _repeatinterval = value; }
            get { return _repeatinterval; }
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
