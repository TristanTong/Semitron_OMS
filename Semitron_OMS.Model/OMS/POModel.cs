/**  
* POModel.cs
*
* 功 能： N/A
* 类 名： POModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/17 19:08:23   童荣辉    初版
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
	/// 采购订单主表
	/// </summary>
	[Serializable]
	public partial class POModel
	{
		public POModel()
		{}
        #region Model
        private int _id;
        private string _pono;
        private DateTime? _poorderdate;
        private int? _supplierid;
        private string _contactperson;
        private string _tel;
        private DateTime? _issueddate;
        private string _innerbuyer;
        private int? _corporationid;
        private string _billto;
        private string _billmanager;
        private string _billmanagertel;
        private string _shipto;
        private string _shipmanager;
        private string _shipmanagertel;
        private DateTime? _deliverydate;
        private string _paymentterms;
        private string _shipping;
        private decimal? _totalfee = 0.0000M;
        private string _acceptedby;
        private string _issuedby;
        private int? _state = 100;
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
        /// PO#采购订单号
        /// </summary>
        public string PONO
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 采购订单日期
        /// </summary>
        public DateTime? POOrderDate
        {
            set { _poorderdate = value; }
            get { return _poorderdate; }
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
        /// 联系人
        /// </summary>
        public string ContactPerson
        {
            set { _contactperson = value; }
            get { return _contactperson; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 发行采购日期
        /// </summary>
        public DateTime? IssuedDate
        {
            set { _issueddate = value; }
            get { return _issueddate; }
        }
        /// <summary>
        /// 公司内部采购
        /// </summary>
        public string InnerBuyer
        {
            set { _innerbuyer = value; }
            get { return _innerbuyer; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int? CorporationID
        {
            set { _corporationid = value; }
            get { return _corporationid; }
        }
        /// <summary>
        /// 帐单地址(冗余字段)
        /// </summary>
        public string BillTo
        {
            set { _billto = value; }
            get { return _billto; }
        }
        /// <summary>
        /// 帐单负责人
        /// </summary>
        public string BillManager
        {
            set { _billmanager = value; }
            get { return _billmanager; }
        }
        /// <summary>
        /// 帐单负责人电话
        /// </summary>
        public string BillManagerTel
        {
            set { _billmanagertel = value; }
            get { return _billmanagertel; }
        }
        /// <summary>
        /// 送货地址
        /// </summary>
        public string ShipTo
        {
            set { _shipto = value; }
            get { return _shipto; }
        }
        /// <summary>
        /// 送货负责人
        /// </summary>
        public string ShipManager
        {
            set { _shipmanager = value; }
            get { return _shipmanager; }
        }
        /// <summary>
        /// 送货负责人电话
        /// </summary>
        public string ShipManagerTel
        {
            set { _shipmanagertel = value; }
            get { return _shipmanagertel; }
        }
        /// <summary>
        /// 投递日期
        /// </summary>
        public DateTime? DeliveryDate
        {
            set { _deliverydate = value; }
            get { return _deliverydate; }
        }
        /// <summary>
        /// 付款条例
        /// </summary>
        public string PaymentTerms
        {
            set { _paymentterms = value; }
            get { return _paymentterms; }
        }
        /// <summary>
        /// 海运
        /// </summary>
        public string Shipping
        {
            set { _shipping = value; }
            get { return _shipping; }
        }
        /// <summary>
        /// 订单总计金额
        /// </summary>
        public decimal? TotalFee
        {
            set { _totalfee = value; }
            get { return _totalfee; }
        }
        /// <summary>
        /// 接收签名
        /// </summary>
        public string AcceptedBy
        {
            set { _acceptedby = value; }
            get { return _acceptedby; }
        }
        /// <summary>
        /// 发行采购签名
        /// </summary>
        public string IssuedBy
        {
            set { _issuedby = value; }
            get { return _issuedby; }
        }
        /// <summary>
        /// 状态(100 新增待关联,110 正在采购计划,120 待生效,130 QC确认,140 出货,150 无效)
        /// </summary>
        public int? State
        {
            set { _state = value; }
            get { return _state; }
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
}

