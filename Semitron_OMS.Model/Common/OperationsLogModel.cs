using System;
namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    [Serializable]
    public partial class OperationsLogModel
    {
        public OperationsLogModel()
        { }
        #region Model
        private int _operateid;
        private int _adminid;
        private int _otype;
        private string _oinfo;
        private string _omsg;
        private DateTime _createtime = DateTime.Now;
        private string _adminname;
        private string _nickname;
        private string _pkid;
        private string _formobject;
        private int _availflag = 1;
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public int OperateID
        {
            set { _operateid = value; }
            get { return _operateid; }
        }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 操作类型（1.增加 2.修改 3.删除）
        /// </summary>
        public int OType
        {
            set { _otype = value; }
            get { return _otype; }
        }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string OInfo
        {
            set { _oinfo = value; }
            get { return _oinfo; }
        }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string OMsg
        {
            set { _omsg = value; }
            get { return _omsg; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 操作人帐号
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 操作人昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 操作记录ID
        /// </summary>
        public string PKID
        {
            set { _pkid = value; }
            get { return _pkid; }
        }
        /// <summary>
        /// 操作对象
        /// </summary>
        public string FormObject
        {
            set { _formobject = value; }
            get { return _formobject; }
        }
        /// <summary>
        /// 是否有效(0:否1:是)
        /// </summary>
        public int AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }
        #endregion Model

    }

    public enum OperationsType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Edit = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Del = 3,

        /// <summary>
        /// 导出历史
        /// </summary>
        Export = 4
    }
}

