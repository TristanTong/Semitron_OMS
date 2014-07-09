/**  
* GatheringPlanModel.cs
*
* 功 能： N/A
* 类 名： GatheringPlanModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:58   童荣辉    初版
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
		private string _shippinglistno;
		private DateTime? _gatheringplandate;
		private string _productcode;
		private int? _qty;
		private decimal? _price;
		private decimal? _totalgathering;
		private int? _state;
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
		/// 出库单号
		/// </summary>
		public string ShippingListNo
		{
			set{ _shippinglistno=value;}
			get{return _shippinglistno;}
		}
		/// <summary>
		/// 收款计划时间
		/// </summary>
		public DateTime? GatheringPlanDate
		{
			set{ _gatheringplandate=value;}
			get{return _gatheringplandate;}
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
		/// 产品数量
		/// </summary>
		public int? Qty
		{
			set{ _qty=value;}
			get{return _qty;}
		}
		/// <summary>
		/// 产品标准售价
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 总收款金额
		/// </summary>
		public decimal? TotalGathering
		{
			set{ _totalgathering=value;}
			get{return _totalgathering;}
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
}

