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
            /// 出货计划页面
            /// </summary>
            public const string PAGE_SHIPPING_PLAN = "OMS/ShippingPlan/Main.html";
            /// <summary>
            /// 出库单页面
            /// </summary>
            public const string PAGE_SHIPPING_LIST = "OMS/ShippingList/Main.html";
            /// <summary>
            /// 入库单页面
            /// </summary>
            public const string PAGE_GODOWN_ENTRY = "OMS/GodownEntry/Main.html";
            /// <summary>
            /// 库存页面
            /// </summary>
            public const string PAGE_INVENTORY = "OMS/Inventory/Main.html";
            /// <summary>
            /// 收客户款计划页面
            /// </summary>
            public const string PAGE_GATHERING_PLAN = "FM/GatheringPlan/Main.html";
            /// <summary>
            /// 付供应商款计划
            /// </summary>
            public const string PAGE_PAYMENT_PLAN = "FM/PaymentPlan/Main.html";
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
            /// 导出账务出库单Excel按钮代码
            /// </summary>
            public const string BTN_EXPORT_FINANCE_SHIPPING_LIST = "btnExportFinance";
            /// <summary>
            /// 审核入库单按钮代码
            /// </summary>
            public const string BTN_APPROVE_GODOWN_ENTRY = "btnApprove";
            /// <summary>
            /// 导出库存按钮代码
            /// </summary>
            public const string BTN_EXPORT_INVENTORY = "btnExportInventory";
            /// <summary>
            /// 导出入库单按钮代码
            /// </summary>
            public const string BTN_EXPORT_GODOWN_ENTRY = "btnExportGodownEntry";
            /// <summary>
            /// 导出收客户款计划按钮代码
            /// </summary>
            public const string BTN_EXPORT_GATHERING_PLAN = "btnExportGatheringPlan";
            /// <summary>
            /// 导出付供应商款计划按钮代码
            /// </summary
            public const string BTN_EXPORT_PAYMENT_PLAN = "btnExportPaymentPlan";
        }
        /// <summary>
        /// 数据集代码常量
        /// </summary>
        public struct DataSetPerConst
        {

        }

    }
}
