/**  
* CustomerOrderDetailModel.cs
*
* 功 能： N/A
* 类 名： CustomerOrderDetailModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:28   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
namespace Semitron_OMS.Model.OMS
{
    /// <summary>
    /// 客户订单明细表
    /// </summary>
    [Serializable]
    public partial class CustomerOrderDetailModel
    {
        public CustomerOrderDetailModel()
        { }
        #region Model
        private int _id;
        private string _innerorderno;
        private string _cpn;
        private string _mpn;
        private string _mfg;
        private string _dc;
        private bool _rohs;
        private DateTime? _crd;
        private int? _custquantity = 0;
        private decimal? _saleexchangerate = 0.0000M;
        private string _salerealcurrency;
        private decimal? _salerealprice = 0.0000M;
        private string _salestandardcurrency = "1";
        private decimal? _saleprice = 0.0000M;
        private decimal? _otherfee = 0.0000M;
        private string _otherfeeremark;
        private int? _alreadyqty;
        private DateTime? _shipmentdate;
        private DateTime? _customerinstockdate;
        private bool _iscustomerpay = false;
        private bool _availflag = true;
        private string _createuser;
        private DateTime? _createtime = DateTime.Now;
        private string _updateuser;
        private DateTime? _updatetime;
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司内部单号
        /// </summary>
        public string InnerOrderNO
        {
            set { _innerorderno = value; }
            get { return _innerorderno; }
        }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string CPN
        {
            set { _cpn = value; }
            get { return _cpn; }
        }
        /// <summary>
        /// 厂家标准型号
        /// </summary>
        public string MPN
        {
            set { _mpn = value; }
            get { return _mpn; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string MFG
        {
            set { _mfg = value; }
            get { return _mfg; }
        }
        /// <summary>
        /// 生产年份
        /// </summary>
        public string DC
        {
            set { _dc = value; }
            get { return _dc; }
        }
        /// <summary>
        /// 是否环保
        /// </summary>
        public bool ROHS
        {
            set { _rohs = value; }
            get { return _rohs; }
        }
        /// <summary>
        /// 客户要求的交期
        /// </summary>
        public DateTime? CRD
        {
            set { _crd = value; }
            get { return _crd; }
        }
        /// <summary>
        /// 客户订单数量
        /// </summary>
        public int? CustQuantity
        {
            set { _custquantity = value; }
            get { return _custquantity; }
        }
        /// <summary>
        /// 卖汇率
        /// </summary>
        public decimal? SaleExchangeRate
        {
            set { _saleexchangerate = value; }
            get { return _saleexchangerate; }
        }
        /// <summary>
        /// 实际卖货币
        /// </summary>
        public string SaleRealCurrency
        {
            set { _salerealcurrency = value; }
            get { return _salerealcurrency; }
        }
        /// <summary>
        /// 实际卖价
        /// </summary>
        public decimal? SaleRealPrice
        {
            set { _salerealprice = value; }
            get { return _salerealprice; }
        }
        /// <summary>
        /// 标准卖货币
        /// </summary>
        public string SaleStandardCurrency
        {
            set { _salestandardcurrency = value; }
            get { return _salestandardcurrency; }
        }
        /// <summary>
        /// 卖价
        /// </summary>
        public decimal? SalePrice
        {
            set { _saleprice = value; }
            get { return _saleprice; }
        }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal? OtherFee
        {
            set { _otherfee = value; }
            get { return _otherfee; }
        }
        /// <summary>
        /// 其他费用备注
        /// </summary>
        public string OtherFeeRemark
        {
            set { _otherfeeremark = value; }
            get { return _otherfeeremark; }
        }
        /// <summary>
        /// 已出货数量
        /// </summary>
        public int? AlreadyQty
        {
            set { _alreadyqty = value; }
            get { return _alreadyqty; }
        }
        /// <summary>
        /// 出货日期
        /// </summary>
        public DateTime? ShipmentDate
        {
            set { _shipmentdate = value; }
            get { return _shipmentdate; }
        }
        /// <summary>
        /// 客户入库日期
        /// </summary>
        public DateTime? CustomerInStockDate
        {
            set { _customerinstockdate = value; }
            get { return _customerinstockdate; }
        }
        /// <summary>
        /// 客户是否付款
        /// </summary>
        public bool IsCustomerPay
        {
            set { _iscustomerpay = value; }
            get { return _iscustomerpay; }
        }
        /// <summary>
        /// 有效无效标记(1:有效 0: 无效)
        /// </summary>
        public bool AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }

    [Serializable]
    public partial class CustomerOrderDetailUnOutStockModel
    {
        public int CustomerOrderDetailId { get; set; }

        public string InnerOrderNO { get; set; }

        public string CustomerOrderNO { get; set; }

        public string CPN { get; set; }

        public string CustOrderDate { get; set; }

        public string InnerSalesMan { get; set; }

        public string AssignToInnerBuyer { get; set; }

        public int UnOutStockQty { get; set; }

        public int CustQuantity { get; set; }

        public int DoingOutStockQty { get; set; }

        public int PlaningQty { get; set; }

        public int AlreadyQty { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public int PlanedQty { get; set; }
    }
}

