using System;
namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Serializable]
    public partial class PermissionModel
    {
        public PermissionModel()
        { }
        #region Model
        private int _permissionid;
        private string _name;
        private string _code;
        private string _description;
        private int _type;
        private string _linkurl;
        private int _pid;
        private int _availflag = 1;
        public string _sk;
        private int _ParentSystem;

        /// <summary>
        /// 父系统id
        /// </summary>
        public int ParentSystem
        {
            get
            {
                return _ParentSystem;
            }
            set
            {
                _ParentSystem = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PermissionID
        {
            set { _permissionid = value; }
            get { return _permissionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 权限编码：页面名称，模块名称，按钮名称等。
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 1:模块,2:菜单,3:按钮
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkUrl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 是否有效（0否1是）
        /// </summary>
        public int AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }
        /// <summary>
        /// 排序SK拼音字母
        /// </summary>
        public string SK
        {
            set { _sk = value; }
            get { return _sk; }
        }

        #endregion Model

    }
}

