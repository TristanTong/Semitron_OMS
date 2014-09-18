/**  
* PaymentPlanModel.cs
*
* 功 能： N/A
* 类 名： PaymentPlanModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/21 22:00:31   童荣辉    初版
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
	/// 付款计划表
	/// </summary>
	[Serializable]
	public partial class PaymentPlanModel
	{
		public PaymentPlanModel()
		{}
        #region Model
        private int _id;
        private int _corporationid;
        private string _pono;
        private int _poplanid;
        private DateTime? _paymentplandate;
        private string _productcode;
        private string _mpn;
        private int? _supplierid;
        private int _qty;
        private int? _buystandardcurrency;
        private decimal _buyprice;
        private decimal? _buycost;
        private int? _buyrealcurrency;
        private decimal? _buyexchangerate;
        private decimal? _buyrealprice;
        private decimal? _buyrealtotal;
        private int? _vendorpaymenttypeid;
        private bool _issuppliervatinvoice;
        private string _suppliervatinvoice;
        private bool _ispaysupplier;
        private decimal? _otherfee;
        private string _otherfeeremark;
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
        /// 采购订单号
        /// </summary>
        public string PONO
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 采购计划ID
        /// </summary>
        public int POPlanID
        {
            set { _poplanid = value; }
            get { return _poplanid; }
        }
        /// <summary>
        /// 付款计划时间
        /// </summary>
        public DateTime? PaymentPlanDate
        {
            set { _paymentplandate = value; }
            get { return _paymentplandate; }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 厂商型号
        /// </summary>
        public string MPN
        {
            set { _mpn = value; }
            get { return _mpn; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public int? SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 付款数量
        /// </summary>
        public int Qty
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 付款标准货币
        /// </summary>
        public int? BuyStandardCurrency
        {
            set { _buystandardcurrency = value; }
            get { return _buystandardcurrency; }
        }
        /// <summary>
        /// 付款标准单价
        /// </summary>
        public decimal BuyPrice
        {
            set { _buyprice = value; }
            get { return _buyprice; }
        }
        /// <summary>
        /// 付款标准总额
        /// </summary>
        public decimal? BuyCost
        {
            set { _buycost = value; }
            get { return _buycost; }
        }
        /// <summary>
        /// 实际付款货币
        /// </summary>
        public int? BuyRealCurrency
        {
            set { _buyrealcurrency = value; }
            get { return _buyrealcurrency; }
        }
        /// <summary>
        /// 实际付款汇率
        /// </summary>
        public decimal? BuyExchangeRate
        {
            set { _buyexchangerate = value; }
            get { return _buyexchangerate; }
        }
        /// <summary>
        /// 实际付款单价
        /// </summary>
        public decimal? BuyRealPrice
        {
            set { _buyrealprice = value; }
            get { return _buyrealprice; }
        }
        /// <summary>
        /// 实际付款总额
        /// </summary>
        public decimal? BuyRealTotal
        {
            set { _buyrealtotal = value; }
            get { return _buyrealtotal; }
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public int? VendorPaymentTypeID
        {
            set { _vendorpaymenttypeid = value; }
            get { return _vendorpaymenttypeid; }
        }
        /// <summary>
        /// 是否开增票
        /// </summary>
        public bool IsSupplierVATInvoice
        {
            set { _issuppliervatinvoice = value; }
            get { return _issuppliervatinvoice; }
        }
        /// <summary>
        /// 增票号
        /// </summary>
        public string SupplierVATInvoice
        {
            set { _suppliervatinvoice = value; }
            get { return _suppliervatinvoice; }
        }
        /// <summary>
        /// 是否向供应商付款
        /// </summary>
        public bool IsPaySupplier
        {
            set { _ispaysupplier = value; }
            get { return _ispaysupplier; }
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
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        #endregion Model

	}
}

