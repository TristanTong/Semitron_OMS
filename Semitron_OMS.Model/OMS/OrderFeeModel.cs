/**  
* OrderFeeModel.cs
*
* 功 能： N/A
* 类 名： OrderFeeModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:29   童荣辉    初版
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
    /// 订单计费明细表
    /// </summary>
    [Serializable]
    public partial class OrderFeeModel
    {
        public OrderFeeModel()
        { }
        #region Model
        private int _id;
        private int _customerorderdetailid = -1;
        private int _poplanid = -1;
        private string _totalfeecurrencyunit = "1";
        private decimal _customerfeein = 0.0000M;
        private decimal _incomerate = 0.0000M;
        private string _incomestandardcurrency = "1";
        private decimal _standardcustomerfeein = 0.0000M;
        private string _realincurrencyunit = "1";
        private decimal _customerrealpayfee = 0.0000M;
        private string _standardrealincurrencyunit = "1";
        private decimal _standardcustomerrealpayfee = 0.0000M;
        private decimal _standardincomerealdiff = 0.0000M;
        private decimal _grossprofits = 0.00M;
        private decimal _otherfee = 0.0000M;
        private string _otherfeeremark;
        private decimal _netprofit = 0.00M;
        private decimal _profitmargin = 0.00M;
        private string _otherremark;
        private bool _iscustomervatinvoice;
        private string _customervatinvoiceno;
        private bool _issuppliervatinvoice;
        private string _suppliervatinvoice;
        private string _tracenumber;
        private DateTime? _feebackdate;
        private decimal? _salesmanproportion;
        private decimal? _salesmanpay;
        private decimal? _buyerproportion;
        private decimal? _buyerpay;
        private bool _availflag = true;
        private string _createuser;
        private DateTime _createtime = DateTime.Now;
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
        /// 客户订单ID
        /// </summary>
        public int CustomerOrderDetailID
        {
            set { _customerorderdetailid = value; }
            get { return _customerorderdetailid; }
        }
        /// <summary>
        /// 采购订单ID
        /// </summary>
        public int POPlanID
        {
            set { _poplanid = value; }
            get { return _poplanid; }
        }
        /// <summary>
        /// 实际收款总金额货币单位
        /// </summary>
        public string TotalFeeCurrencyUnit
        {
            set { _totalfeecurrencyunit = value; }
            get { return _totalfeecurrencyunit; }
        }
        /// <summary>
        /// 应收客户款
        /// </summary>
        public decimal CustomerFeeIn
        {
            set { _customerfeein = value; }
            get { return _customerfeein; }
        }
        /// <summary>
        /// 收款汇率
        /// </summary>
        public decimal IncomeRate
        {
            set { _incomerate = value; }
            get { return _incomerate; }
        }
        /// <summary>
        /// 标准应收货币单位
        /// </summary>
        public string IncomeStandardCurrency
        {
            set { _incomestandardcurrency = value; }
            get { return _incomestandardcurrency; }
        }
        /// <summary>
        /// 标准应收客户款
        /// </summary>
        public decimal StandardCustomerFeeIn
        {
            set { _standardcustomerfeein = value; }
            get { return _standardcustomerfeein; }
        }
        /// <summary>
        /// 实际收款货币单位
        /// </summary>
        public string RealInCurrencyUnit
        {
            set { _realincurrencyunit = value; }
            get { return _realincurrencyunit; }
        }
        /// <summary>
        /// 客户实际付款金额
        /// </summary>
        public decimal CustomerRealPayFee
        {
            set { _customerrealpayfee = value; }
            get { return _customerrealpayfee; }
        }
        /// <summary>
        /// 标准实收货币单位
        /// </summary>
        public string StandardRealInCurrencyUnit
        {
            set { _standardrealincurrencyunit = value; }
            get { return _standardrealincurrencyunit; }
        }
        /// <summary>
        /// 标准客户实际付款金额
        /// </summary>
        public decimal StandardCustomerRealPayFee
        {
            set { _standardcustomerrealpayfee = value; }
            get { return _standardcustomerrealpayfee; }
        }
        /// <summary>
        /// 标准应收实收美金差额
        /// </summary>
        public decimal StandardIncomeRealDiff
        {
            set { _standardincomerealdiff = value; }
            get { return _standardincomerealdiff; }
        }
        /// <summary>
        /// 毛利润(USD)
        /// </summary>
        public decimal GrossProfits
        {
            set { _grossprofits = value; }
            get { return _grossprofits; }
        }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherFee
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
        /// 净利润(USD)
        /// </summary>
        public decimal NetProfit
        {
            set { _netprofit = value; }
            get { return _netprofit; }
        }
        /// <summary>
        /// 利润率
        /// </summary>
        public decimal ProfitMargin
        {
            set { _profitmargin = value; }
            get { return _profitmargin; }
        }
        /// <summary>
        /// 其他信息备注
        /// </summary>
        public string OtherRemark
        {
            set { _otherremark = value; }
            get { return _otherremark; }
        }
        /// <summary>
        /// 客户是否开增票
        /// </summary>
        public bool IsCustomerVATInvoice
        {
            set { _iscustomervatinvoice = value; }
            get { return _iscustomervatinvoice; }
        }
        /// <summary>
        /// 客户增票号
        /// </summary>
        public string CustomerVATInvoiceNo
        {
            set { _customervatinvoiceno = value; }
            get { return _customervatinvoiceno; }
        }
        /// <summary>
        /// 供应商是否开增票
        /// </summary>
        public bool IsSupplierVATInvoice
        {
            set { _issuppliervatinvoice = value; }
            get { return _issuppliervatinvoice; }
        }
        /// <summary>
        /// 供应商增票号
        /// </summary>
        public string SupplierVATInvoice
        {
            set { _suppliervatinvoice = value; }
            get { return _suppliervatinvoice; }
        }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string TraceNumber
        {
            set { _tracenumber = value; }
            get { return _tracenumber; }
        }
        /// <summary>
        /// 回款日期
        /// </summary>
        public DateTime? FeeBackDate
        {
            set { _feebackdate = value; }
            get { return _feebackdate; }
        }
        /// <summary>
        /// 销售提成比例
        /// </summary>
        public decimal? SalesManProportion
        {
            set { _salesmanproportion = value; }
            get { return _salesmanproportion; }
        }
        /// <summary>
        /// 销售提成
        /// </summary>
        public decimal? SalesManPay
        {
            set { _salesmanpay = value; }
            get { return _salesmanpay; }
        }
        /// <summary>
        /// 采购提成比例
        /// </summary>
        public decimal? BuyerProportion
        {
            set { _buyerproportion = value; }
            get { return _buyerproportion; }
        }
        /// <summary>
        /// 采购提成
        /// </summary>
        public decimal? BuyerPay
        {
            set { _buyerpay = value; }
            get { return _buyerpay; }
        }
        /// <summary>
        /// 有效无效标记
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
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 更新人
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

    /// <summary>
    /// 订单计费显示在界面中实体
    /// </summary>
    [Serializable]
    public partial class OrderFeeDisplayModel
    {
        public OrderFeeDisplayModel()
        { }
        #region Model
        private int _id;
        private int _customerorderdetailid = -1;
        private int _poplanid = -1;
        private string _totalfeecurrencyunit = "1";
        private decimal _customerfeein = 0.0000M;
        private decimal _incomerate = 0.0000M;
        private string _incomestandardcurrency = "1";
        private decimal _standardcustomerfeein = 0.0000M;
        private string _realincurrencyunit = "1";
        private decimal _customerrealpayfee = 0.0000M;
        private string _standardrealincurrencyunit = "1";
        private decimal _standardcustomerrealpayfee = 0.0000M;
        private decimal _standardincomerealdiff = 0.0000M;
        private decimal _grossprofits = 0.00M;
        private decimal _otherfee = 0.0000M;
        private string _otherfeeremark;
        private decimal _netprofit = 0.00M;
        private decimal _profitmargin = 0.00M;

        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 客户订单ID
        /// </summary>
        public int CustomerOrderDetailID
        {
            set { _customerorderdetailid = value; }
            get { return _customerorderdetailid; }
        }
        /// <summary>
        /// 采购订单ID
        /// </summary>
        public int POPlanID
        {
            set { _poplanid = value; }
            get { return _poplanid; }
        }
        /// <summary>
        /// 实际收款总金额货币单位
        /// </summary>
        public string TotalFeeCurrencyUnit
        {
            set { _totalfeecurrencyunit = value; }
            get { return _totalfeecurrencyunit; }
        }
        /// <summary>
        /// 应收客户款
        /// </summary>
        public decimal CustomerFeeIn
        {
            set { _customerfeein = value; }
            get { return _customerfeein; }
        }
        /// <summary>
        /// 收款汇率
        /// </summary>
        public decimal IncomeRate
        {
            set { _incomerate = value; }
            get { return _incomerate; }
        }
        /// <summary>
        /// 标准应收货币单位
        /// </summary>
        public string IncomeStandardCurrency
        {
            set { _incomestandardcurrency = value; }
            get { return _incomestandardcurrency; }
        }
        /// <summary>
        /// 标准应收客户款
        /// </summary>
        public decimal StandardCustomerFeeIn
        {
            set { _standardcustomerfeein = value; }
            get { return _standardcustomerfeein; }
        }
        /// <summary>
        /// 实际收款货币单位
        /// </summary>
        public string RealInCurrencyUnit
        {
            set { _realincurrencyunit = value; }
            get { return _realincurrencyunit; }
        }
        /// <summary>
        /// 客户实际付款金额
        /// </summary>
        public decimal CustomerRealPayFee
        {
            set { _customerrealpayfee = value; }
            get { return _customerrealpayfee; }
        }
        /// <summary>
        /// 标准实收货币单位
        /// </summary>
        public string StandardRealInCurrencyUnit
        {
            set { _standardrealincurrencyunit = value; }
            get { return _standardrealincurrencyunit; }
        }
        /// <summary>
        /// 标准客户实际付款金额
        /// </summary>
        public decimal StandardCustomerRealPayFee
        {
            set { _standardcustomerrealpayfee = value; }
            get { return _standardcustomerrealpayfee; }
        }
        /// <summary>
        /// 标准应收实收美金差额
        /// </summary>
        public decimal StandardIncomeRealDiff
        {
            set { _standardincomerealdiff = value; }
            get { return _standardincomerealdiff; }
        }
        /// <summary>
        /// 毛利润(USD)
        /// </summary>
        public decimal GrossProfits
        {
            set { _grossprofits = value; }
            get { return _grossprofits; }
        }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherFee
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
        /// 净利润(USD)
        /// </summary>
        public decimal NetProfit
        {
            set { _netprofit = value; }
            get { return _netprofit; }
        }
        /// <summary>
        /// 利润率
        /// </summary>
        public decimal ProfitMargin
        {
            set { _profitmargin = value; }
            get { return _profitmargin; }
        }

        public int CustomerID
        {
            get;
            set;
        }

        public string InnerOrderNO
        {
            get;
            set;
        }
        public string CPN
        {
            get;
            set;
        }
        public decimal SaleExchangeRate
        {
            get;
            set;
        }
        public string SaleRealCurrency
        {
            get;
            set;
        }
        public decimal SaleRealPrice
        {
            get;
            set;
        }
        public string SaleStandardCurrency
        {
            get;
            set;
        }
        public decimal SalePrice
        {
            get;
            set;
        }
        public decimal OtherFee1
        {
            get;
            set;
        }
        public int SupplierID
        {
            get;
            set;
        }
        public string PONO
        {
            get;
            set;
        }
        public string MPN
        {
            get;
            set;
        }
        public decimal BuyExchangeRate
        {
            get;
            set;
        }
        public string BuyRealCurrency
        {
            get;
            set;
        }
        public decimal BuyRealPrice
        {
            get;
            set;
        }
        public string BuyStandardCurrency
        {
            get;
            set;
        }
        public decimal BuyPrice
        {
            get;
            set;
        }
        public decimal BuyCost
        {
            get;
            set;
        }
        public decimal OtherFee2
        {
            get;
            set;
        }
        public int POQuantity
        {
            get;
            set;
        }
        public int CustQuantity
        {
            get;
            set;
        }
        public string CustomerOrderNO
        {
            get;
            set;
        }
        public decimal SaleTotalPrice
        {
            get;
            set;
        }
        public string OtherRemark
        {
            get;
            set;
        }
        public bool IsCustomerPay
        {
            get;
            set;
        }
        public bool IsPaySupplier
        {
            get;
            set;
        }
        public decimal RealNetProfit
        {
            get;
            set;
        }
        public bool IsCustomerVATInvoice
        {
            get;
            set;
        }
        public string CustomerVATInvoiceNo
        {
            get;
            set;
        }
        public bool IsSupplierVATInvoice
        {
            get;
            set;
        }
        public string SupplierVATInvoice
        {
            get;
            set;
        }
        public string TraceNumber
        {
            get;
            set;
        }
        public int PaymentTypeID
        {
            get;
            set;
        }
        public int VendorPaymentTypeID
        {
            get;
            set;
        }
        public DateTime? CustomerInStockDate
        {
            get;
            set;
        }
        public DateTime? FeeBackDate
        {
            get;
            set;
        }
        public decimal? SalesManProportion
        {
            get;
            set;
        }
        public decimal? SalesManPay
        {
            get;
            set;
        }
        public decimal? BuyerProportion
        {
            get;
            set;
        }
        public decimal? BuyerPay
        {
            get;
            set;
        }
        /// <summary>
        /// 采购单公司法人ID
        /// </summary>
        public int? SupplierCorporationID { get; set; }
        #endregion Model


    }
}

