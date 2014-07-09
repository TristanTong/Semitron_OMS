using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Permission
{
    /// <summary>
    /// 模块权限
    /// </summary>
    [Serializable]
    public class ModulePer : BasePer
    {
        /// <summary>
        /// 拥有的页面权限集,K：Code，V：PagePer
        /// </summary>
        public Dictionary<int, PagePer> PagePers = new Dictionary<int, PagePer>();

    }
}
