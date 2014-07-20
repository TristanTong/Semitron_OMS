/**  
* ShippingListModel.cs
*
* 功 能： N/A
* 类 名： ShippingListModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:28   童荣辉    初版
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
	/// 出库单表
	/// </summary>
	[Serializable]
	public partial class ShippingListModel
	{
		public ShippingListModel()
		{}
        #region Model
        private int _id;
        private string _shippinglistno;
        private DateTime? _shippinglistdate;
        private DateTime? _outstockdate;
        private string _shippingmethod;
        private int? _byhanduserid;
        private DateTime? _estimatearriveddate;
        private DateTime? _realarriveddate;
        private bool _isapproved;
        private int? _approveduserid;
        private DateTime? _approvedtime;
        private string _destination;
        private int? _customerid;
        private string _trackingno;
        private string _logisticsline;
        private decimal? _logisticsfee;
        private int? _state;
        private DateTime? _createtime = DateTime.Now;
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
        /// 出库单号
        /// </summary>
        public string ShippingListNo
        {
            set { _shippinglistno = value; }
            get { return _shippinglistno; }
        }
        /// <summary>
        ///  制单日期
        /// </summary>
        public DateTime? ShippingListDate
        {
            set { _shippinglistdate = value; }
            get { return _shippinglistdate; }
        }
        /// <summary>
        /// 实际出库日期
        /// </summary>
        public DateTime? OutStockDate
        {
            set { _outstockdate = value; }
            get { return _outstockdate; }
        }
        /// <summary>
        ///  运输方式
        /// </summary>
        public string ShippingMethod
        {
            set { _shippingmethod = value; }
            get { return _shippingmethod; }
        }
        /// <summary>
        ///  制单人账号ID
        /// </summary>
        public int? ByHandUserID
        {
            set { _byhanduserid = value; }
            get { return _byhanduserid; }
        }
        /// <summary>
        ///  预计到货日期
        /// </summary>
        public DateTime? EstimateArrivedDate
        {
            set { _estimatearriveddate = value; }
            get { return _estimatearriveddate; }
        }
        /// <summary>
        ///  实际到货日期
        /// </summary>
        public DateTime? RealArrivedDate
        {
            set { _realarriveddate = value; }
            get { return _realarriveddate; }
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsApproved
        {
            set { _isapproved = value; }
            get { return _isapproved; }
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public int? ApprovedUserID
        {
            set { _approveduserid = value; }
            get { return _approveduserid; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovedTime
        {
            set { _approvedtime = value; }
            get { return _approvedtime; }
        }
        /// <summary>
        ///  发货目的地
        /// </summary>
        public string Destination
        {
            set { _destination = value; }
            get { return _destination; }
        }
        /// <summary>
        ///  目的地客户ID
        /// </summary>
        public int? CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        ///  快递单号
        /// </summary>
        public string TrackingNo
        {
            set { _trackingno = value; }
            get { return _trackingno; }
        }
        /// <summary>
        ///  物流线路
        /// </summary>
        public string LogisticsLine
        {
            set { _logisticsline = value; }
            get { return _logisticsline; }
        }
        /// <summary>
        ///  物流费用
        /// </summary>
        public decimal? LogisticsFee
        {
            set { _logisticsfee = value; }
            get { return _logisticsfee; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int? State
        {
            set { _state = value; }
            get { return _state; }
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

