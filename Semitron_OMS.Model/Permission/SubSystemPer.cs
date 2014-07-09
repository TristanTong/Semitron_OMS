using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Permission
{
    /// <summary>
    /// 子系统权限
    /// </summary>
    [Serializable]
    public class SubSystemPer : BasePer
    {
        /// <summary>
        /// 拥有的模块权限集,K：Code，V：ModulePer
        /// </summary>
        public Dictionary<int, ModulePer> ModulePers = new Dictionary<int, ModulePer>();

    }
}
