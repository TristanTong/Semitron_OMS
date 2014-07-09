using System;
namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 管理员角色表
    /// </summary>
    [Serializable]
    public partial class UserRoleModel
    {
        public UserRoleModel()
        { }
        #region Model
        private int _userroleid;
        private int _roleid;
        private int _adminid;
        private string _adminname = "1";
        /// <summary>
        /// 管理员角色关联ID
        /// </summary>
        public int UserRoleID
        {
            set { _userroleid = value; }
            get { return _userroleid; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
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
        /// 管理员名称
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        #endregion Model

    }
}

