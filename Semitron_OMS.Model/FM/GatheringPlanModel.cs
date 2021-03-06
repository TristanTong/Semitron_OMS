﻿/**  
* GatheringPlanModel.cs
*
* 功 能： N/A
* 类 名： GatheringPlanModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/21 22:00:30   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
namespace Semitron_OMS.Model.FM
{
	/// <summary>
	/// 出货计划表
	/// </summary>
	[Serializable]
	public partial class GatheringPlanModel
	{
		public GatheringPlanModel()
		{}
        #region Model
        private int _id;
        private int _corporationid;
        private DateTime? _gatheringplandate;
        private int? _customerid;
        private string _innerorderno;
        private string _customerorderno;
        private int _customerorderdetailid;
        private int? _paymenttypeid;
        private bool _iscustomervatinvoice;
        private string _customervatinvoiceno;
        private string _trackingnumber;
        private bool _iscustomerpay;
        private int _qty;
        private int? _salestandardcurrency;
        private decimal? _saleprice;
        private decimal? _saletotal;
        private int? _salerealcurrency;
        private decimal? _saleexchangerate;
        private decimal? _salerealprice;
        private decimal? _salerealtotal;
        private decimal? _otherfee;
        private string _otherfeeremark;
        private decimal? _channelfee;
        private string _channelfeeremark;
        private decimal? _logisticsfee;
        private string _logisticsfeeremark;
        private decimal? _operatingfee;
        private string _operatingfeeremark;
        private decimal? _standardincomerealdiff;
        private string _salesman;
        private decimal? _salesmanproportion;
        private decimal? _salesmanpay;
        private string _buyerman;
        private decimal? _buyerproportion;
        private decimal? _buyerpay;
        private decimal? _poprice;
        private decimal? _grossprofits;
        private decimal? _profitmargin;
        private decimal? _netprofit;
        private decimal? _netprofitmargin;
        private decimal? _realnetprofit;
        private DateTime? _feebackdate;
        private string _productcodes;
        private int _state = 1;
        private DateTime _createtime = DateTime.Now;
        private string _createuser;
        private DateTime? _updatetime;
        private string _updateuser;
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司抬头
        /// </summary>
        public int CorporationID
        {
            set { _corporationid = value; }
            get { return _corporationid; }
        }
        /// <summary>
        /// 收款计划时间
        /// </summary>
        public DateTime? GatheringPlanDate
        {
            set { _gatheringplandate = value; }
            get { return _gatheringplandate; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int? CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 内部订单号
        /// </summary>
        public string InnerOrderNO
        {
            set { _innerorderno = value; }
            get { return _innerorderno; }
        }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string CustomerOrderNO
        {
            set { _customerorderno = value; }
            get { return _customerorderno; }
        }
        /// <summary>
        /// 产品清单ID
        /// </summary>
        public int CustomerOrderDetailID
        {
            set { _customerorderdetailid = value; }
            get { return _customerorderdetailid; }
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public int? PaymentTypeID
        {
            set { _paymenttypeid = value; }
            get { return _paymenttypeid; }
        }
        /// <summary>
        /// 是否开增票
        /// </summary>
        public bool IsCustomerVATInvoice
        {
            set { _iscustomervatinvoice = value; }
            get { return _iscustomervatinvoice; }
        }
        /// <summary>
        /// 增票号
        /// </summary>
        public string CustomerVATInvoiceNo
        {
            set { _customervatinvoiceno = value; }
            get { return _customervatinvoiceno; }
        }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNumber
        {
            set { _trackingnumber = value; }
            get { return _trackingnumber; }
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
        /// 收款数量
        /// </summary>
        public int Qty
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 收款标准货币
        /// </summary>
        public int? SaleStandardCurrency
        {
            set { _salestandardcurrency = value; }
            get { return _salestandardcurrency; }
        }
        /// <summary>
        /// 收款标准单价
        /// </summary>
        public decimal? SalePrice
        {
            set { _saleprice = value; }
            get { return _saleprice; }
        }
        /// <summary>
        /// 收款标准售价总额
        /// </summary>
        public decimal? SaleTotal
        {
            set { _saletotal = value; }
            get { return _saletotal; }
        }
        /// <summary>
        /// 实际收款货币
        /// </summary>
        public int? SaleRealCurrency
        {
            set { _salerealcurrency = value; }
            get { return _salerealcurrency; }
        }
        /// <summary>
        /// 实际收款汇率
        /// </summary>
        public decimal? SaleExchangeRate
        {
            set { _saleexchangerate = value; }
            get { return _saleexchangerate; }
        }
        /// <summary>
        /// 实际收款单价
        /// </summary>
        public decimal? SaleRealPrice
        {
            set { _salerealprice = value; }
            get { return _salerealprice; }
        }
        /// <summary>
        /// 实际收款总额
        /// </summary>
        public decimal? SaleRealTotal
        {
            set { _salerealtotal = value; }
            get { return _salerealtotal; }
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
        /// 渠道费用
        /// </summary>
        public decimal? ChannelFee
        {
            set { _channelfee = value; }
            get { return _channelfee; }
        }
        /// <summary>
        /// 渠道费用备注
        /// </summary>
        public string ChannelFeeRemark
        {
            set { _channelfeeremark = value; }
            get { return _channelfeeremark; }
        }
        /// <summary>
        /// 物流费用
        /// </summary>
        public decimal? LogisticsFee
        {
            set { _logisticsfee = value; }
            get { return _logisticsfee; }
        }
        /// <summary>
        /// 物流费用备注
        /// </summary>
        public string LogisticsFeeRemark
        {
            set { _logisticsfeeremark = value; }
            get { return _logisticsfeeremark; }
        }
        /// <summary>
        /// 运营费用
        /// </summary>
        public decimal? OperatingFee
        {
            set { _operatingfee = value; }
            get { return _operatingfee; }
        }
        /// <summary>
        /// 运营费用备注
        /// </summary>
        public string OperatingFeeRemark
        {
            set { _operatingfeeremark = value; }
            get { return _operatingfeeremark; }
        }
        /// <summary>
        /// 标准应收实收美金差额
        /// </summary>
        public decimal? StandardIncomeRealDiff
        {
            set { _standardincomerealdiff = value; }
            get { return _standardincomerealdiff; }
        }
        /// <summary>
        /// 销售
        /// </summary>
        public string SalesMan
        {
            set { _salesman = value; }
            get { return _salesman; }
        }
        /// <summary>
        /// 销售提成比例(%)
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
        /// 采购
        /// </summary>
        public string BuyerMan
        {
            set { _buyerman = value; }
            get { return _buyerman; }
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
        /// 采购单价(USD)
        /// </summary>
        public decimal? POPrice
        {
            set { _poprice = value; }
            get { return _poprice; }
        }
        /// <summary>
        /// 毛利润(USD)
        /// </summary>
        public decimal? GrossProfits
        {
            set { _grossprofits = value; }
            get { return _grossprofits; }
        }
        /// <summary>
        /// 毛利润率
        /// </summary>
        public decimal? ProfitMargin
        {
            set { _profitmargin = value; }
            get { return _profitmargin; }
        }
        /// <summary>
        /// 净利润(USD)
        /// </summary>
        public decimal? NetProfit
        {
            set { _netprofit = value; }
            get { return _netprofit; }
        }
        /// <summary>
        /// 净利润率(%)
        /// </summary>
        public decimal? NetProfitMargin
        {
            set { _netprofitmargin = value; }
            get { return _netprofitmargin; }
        }
        /// <summary>
        /// 实际净利润(USD)
        /// </summary>
        public decimal? RealNetProfit
        {
            set { _realnetprofit = value; }
            get { return _realnetprofit; }
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
        /// 相关的产品编码字符串
        /// </summary>
        public string ProductCodes
        {
            set { _productcodes = value; }
            get { return _productcodes; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
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
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        #endregion Model

	}
}

