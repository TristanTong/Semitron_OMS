using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Enum
{
    /// <summary>
    /// 客户订单状态枚举
    /// </summary>
    public enum EnumCustomerOrderState : short
    {
        /// <summary>
        /// 待销售审核 100
        /// </summary>
        Added = 100,
        /// <summary>
        /// 销售审核通过 110
        /// </summary>
        SalesManagerChecked = 110,
        /// <summary>
        /// 产品采购中 120
        /// </summary>
        POing = 120,
        /// <summary>
        /// 可建立出货计划 125
        /// </summary>
        CanShipmentPlan = 125,
        /// <summary>
        /// 已建立出货计划 130
        /// </summary>
        FinishShipmentPlan = 130,
        /// <summary>
        /// 出库中 135
        /// </summary>
        DoingOutStock = 135,
        /// <summary>
        /// 已出货
        /// </summary>
        FinishOutStock = 140,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = -100
    }

    /// <summary>
    /// 采购订单状态枚举
    /// </summary>
    public enum EnumPOState : short
    {
        /// <summary>
        /// 待关联计划
        /// </summary>
        Added = 100,
        /// <summary>
        /// 供应商审核 
        /// </summary>
        ConfirmSupplier = 105,
        /// <summary>
        /// 采购计划中 
        /// </summary>
        Planning = 110,
        /// <summary>
        /// 待采购审核
        /// </summary>
        POManagerChecked = 115,
        /// <summary>
        /// 审核计划通过
        /// </summary>
        PlanChecked = 120,
        /// <summary>
        /// 完成采购
        /// </summary>
        Completed = 130,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = -100
    }

    /// <summary>
    /// 采购计划状态枚举
    /// </summary>
    public enum EnumPOPlanState : short
    {
        /// <summary>
        /// 临时草稿
        /// </summary>
        Added = 100,
        /// <summary>
        /// 供应商审核 
        /// </summary>
        ConfirmSupplier = 110,
        /// <summary>
        /// 确认数量/报价 
        /// </summary>
        ConfirmQtyPrice = 115,
        /// <summary>
        /// 待采购主管审核
        /// </summary>
        POManagerChecked = 120,
        /// <summary>
        /// 待QC确认进货
        /// </summary>
        QCCheckStock = 140,
        /// <summary>
        /// 进货中
        /// </summary>
        DoingInStock = 145,
        /// <summary>
        /// 进货完成
        /// </summary>
        FinishInStock = 150,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = -100
    }
}
