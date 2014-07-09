/**  
* GodownEntryDetailModel.cs
*
* 功 能： N/A
* 类 名： GodownEntryDetailModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:18   童荣辉    初版
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
	/// 入库单明细表
	/// </summary>
	[Serializable]
	public partial class GodownEntryDetailModel
	{
		public GodownEntryDetailModel()
		{}
		#region Model
		private int _id;
		private int _godownentryid;
		private string _pono;
		private int? _poplanid;
		private string _productcode;
		private int _inqty;
		private decimal? _price;
		private decimal? _totalprice;
		private string _remark;
		private bool _availflag;
		private DateTime? _createtime= DateTime.Now;
		private string _createuser;
		private DateTime? _updatetime;
		private string _updateuser;
		/// <summary>
		/// 数据唯一标识
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 入库单头ID
		/// </summary>
		public int GodownEntryID
		{
			set{ _godownentryid=value;}
			get{return _godownentryid;}
		}
		/// <summary>
		/// 采购订单号
		/// </summary>
		public string PONo
		{
			set{ _pono=value;}
			get{return _pono;}
		}
		/// <summary>
		///  采购计划ID
		/// </summary>
		public int? POPlanId
		{
			set{ _poplanid=value;}
			get{return _poplanid;}
		}
		/// <summary>
		///  产品编码
		/// </summary>
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		///  入库数量
		/// </summary>
		public int InQty
		{
			set{ _inqty=value;}
			get{return _inqty;}
		}
		/// <summary>
		///  单价
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		///  金额
		/// </summary>
		public decimal? TotalPrice
		{
			set{ _totalprice=value;}
			get{return _totalprice;}
		}
		/// <summary>
		///  备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		///  是否有效
		/// </summary>
		public bool AvailFlag
		{
			set{ _availflag=value;}
			get{return _availflag;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 修改人
		/// </summary>
		public string UpdateUser
		{
			set{ _updateuser=value;}
			get{return _updateuser;}
		}
		#endregion Model

	}

    /// <summary>
    /// 入库单明细显示在界面中实体
    /// </summary>
    [Serializable]
    public partial class GodownEntryDetailDisplayModel
    {
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set;
            get;
        }
        /// <summary>
        /// 入库单头ID
        /// </summary>
        public int GodownEntryID
        {
            set;
            get;
        }
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string PONo
        {
            set;
            get;
        }
        /// <summary>
        ///  采购计划ID
        /// </summary>
        public int POPlanId
        {
            set;
            get;
        }
        /// <summary>
        ///  采购订单数
        /// </summary>
        public int POQuantity
        {
            set;
            get;
        }
        /// <summary>
        ///  到货日期
        /// </summary>
        public DateTime? ShipmentDate
        {
            set;
            get;
        }
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode
        {
            set;
            get;
        }
        /// <summary>
        ///  入库数量
        /// </summary>
        public int InQty
        {
            set;
            get;
        }
        /// <summary>
        ///  单价
        /// </summary>
        public decimal Price
        {
            set;
            get;
        }
        /// <summary>
        ///  金额
        /// </summary>
        public decimal TotalPrice
        {
            set;
            get;
        }
        /// <summary>
        ///  备注
        /// </summary>
        public string Remark
        {
            set;
            get;
        }
        /// <summary>
        ///  是否有效
        /// </summary>
        public bool AvailFlag
        {
            set;
            get;
        }
    }
}

