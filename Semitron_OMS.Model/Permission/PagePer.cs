using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Permission
{
    /// <summary>
    /// 页面权限
    /// </summary>
    [Serializable]
    public class PagePer : BasePer
    {
        /// <summary>
        /// 拥有的按钮权限集,K：Code，V：ButtonPer
        /// </summary>
        public Dictionary<int, ButtonPer> ButtonPers = new Dictionary<int, ButtonPer>();

        /// <summary>
        /// 拥有的数据集权限集,K：Code，V：DataSetPer
        /// </summary>
        public Dictionary<int, DataSetPer> DataSetsPers = new Dictionary<int, DataSetPer>();

        /// <summary>
        /// 拥有的右键菜单权限集,K：Code，V：RightMenuPer
        /// </summary>
        public Dictionary<int, RightMenuPer> RightMenuPer = new Dictionary<int, RightMenuPer>();
    }
}
