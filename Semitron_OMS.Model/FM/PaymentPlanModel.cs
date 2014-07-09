/**  
* PaymentPlanModel.cs
*
* 功 能： N/A
* 类 名： PaymentPlanModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:57   童荣辉    初版
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
		private string _entryno;
		private DateTime? _paymentplandate;
		private string _productcode;
		private string _mpn;
		private int? _supplierid;
		private int? _qty;
		private decimal? _price;
		private decimal? _totalpayment;
		private int? _state;
		private DateTime? _createtime;
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
		/// 入库单号
		/// </summary>
		public string EntryNo
		{
			set{ _entryno=value;}
			get{return _entryno;}
		}
		/// <summary>
		/// 付款计划时间
		/// </summary>
		public DateTime? PaymentPlanDate
		{
			set{ _paymentplandate=value;}
			get{return _paymentplandate;}
		}
		/// <summary>
		/// 产品编码
		/// </summary>
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		/// 厂商型号
		/// </summary>
		public string MPN
		{
			set{ _mpn=value;}
			get{return _mpn;}
		}
		/// <summary>
		/// 供应商ID
		/// </summary>
		public int? SupplierID
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 产品数量
		/// </summary>
		public int? Qty
		{
			set{ _qty=value;}
			get{return _qty;}
		}
		/// <summary>
		/// 产品标准单价
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 总付款金额
		/// </summary>
		public decimal? TotalPayment
		{
			set{ _totalpayment=value;}
			get{return _totalpayment;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
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
		/// 更新时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 更新人
		/// </summary>
		public string UpdateUser
		{
			set{ _updateuser=value;}
			get{return _updateuser;}
		}
		#endregion Model

	}
}

