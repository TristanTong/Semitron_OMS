using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Permission
{
    /// <summary>
    /// 权限模块
    /// </summary>
    [Serializable]
    public class PermissionModule : BasePer
    {
        /// <summary>
        /// 拥有的子系统权限集,K：Code，V：SubSystemPer
        /// </summary>
        public Dictionary<int, SubSystemPer> SubSystemPers = new Dictionary<int, SubSystemPer>();
    }
}
