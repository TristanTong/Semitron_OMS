using System;
using System.Collections;
using System.Collections.Generic;
namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 管理员
    /// </summary>
    [Serializable]
    public partial class AdminModel
    {
        public AdminModel()
        { }
        #region Model
        private int _adminid;
        private string _username;
        private string _password;
        private string _name;
        private string _phone;
        private string _email;
        private int _availflag = 1;
        private int? _custid;
        private DateTime _createtime = DateTime.Now;
        private DateTime? _lastlogintime;
        //private string[] _module;
        //private string[] _linkUrl;
        //private string[] _pid;
        private string[] _pname;
        private string[] _permissionID;
        private string[] _roleID;
        private int? _type;

        //权限模块
        public Semitron_OMS.Model.Permission.PermissionModule PerModule
        {
            get;
            set;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string[] RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string[] PermissionID
        {
            get { return _permissionID; }
            set { _permissionID = value; }
        }

        public string[] Pname
        {
            get { return _pname; }
            set { _pname = value; }
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
        /// 登录用户名
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 是否有效(0:否1:否)
        /// </summary>
        public int AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }
        /// <summary>
        /// 用户所属
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
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
        /// 最后登陆时间
        /// </summary>
        public DateTime? LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        #endregion Model

    }
}

