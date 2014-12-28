using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Const
{
    public class ConstPermission
    {

        /// <summary>
        /// 模块代码常量
        /// </summary>
        public struct ModulePerConst
        {

        }

        /// <summary>
        /// 页面代码常量
        /// </summary>
        public struct PagePerConst
        {
            /// <summary>
            /// 出货计划按钮代码
            /// </summary>
            public const string PAGE_SHIPPING_PLAN = "OMS/ShippingPlan/Main.html";
            /// <summary>
            /// 审核出库单按钮代码
            /// </summary>
            public const string PAGE_SHIPPING_LIST = "OMS/ShippingList/Main.html";
            /// <summary>
            /// 审核入库单按钮代码
            /// </summary>
            public const string PAGE_GODOWN_ENTRY = "OMS/GodownEntry/Main.html";
        }

        /// <summary>
        /// 按钮权限代码常量
        /// </summary>
        public struct ButtonPerConst
        {
            /// <summary>
            /// 审核出货计划按钮代码
            /// </summary>
            public const string BTN_APPROVE_SHIPPING_PLAN = "btnApprove";
            /// <summary>
            /// 审核出库单按钮代码
            /// </summary>
            public const string BTN_APPROVE_SHIPPING_LIST = "btnApprove";
            /// <summary>
            /// 审核入库单按钮代码
            /// </summary>
            public const string BTN_APPROVE_GODOWN_ENTRY = "btnApprove";
        }

        /// <summary>
        /// 数据集代码常量
        /// </summary>
        public struct DataSetPerConst
        {

        }

    }
}
