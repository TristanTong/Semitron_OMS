using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Enum
{
    public enum EnumRoleID : short
    {
        /// <summary>
        /// 业务员
        /// </summary>
        SalesMan = 1,
        /// <summary>
        /// 销售主管
        /// </summary>
        SalesManager = 11,
        /// <summary>
        /// 采购员
        /// </summary>
        InnerBuyer = 5,
        /// <summary>
        /// 采购主管
        /// </summary>
        BuyerManager = 7,
        /// <summary>
        /// 超级管理员
        /// </summary>
        SupperAdmin = 2
    }

}
