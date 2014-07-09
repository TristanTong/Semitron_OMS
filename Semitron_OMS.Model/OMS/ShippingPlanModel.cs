/**  
* ShippingPlanModel.cs
*
* 功 能： N/A
* 类 名： ShippingPlanModel
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
	/// 出货计划表
	/// </summary>
	[Serializable]
	public partial class ShippingPlanModel
	{
		public ShippingPlanModel()
		{}
		#region Model
		private int _id;
		private string _shippingplanno;
		private DateTime? _shippingplandate;
		private int? _byhanduserid;
		private bool _isapproved;
		private int? _approveduserid;
		private DateTime? _approvedtime;
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
		/// 出货计划单号
		/// </summary>
		public string ShippingPlanNo
		{
			set{ _shippingplanno=value;}
			get{return _shippingplanno;}
		}
		/// <summary>
		/// 出货计划时间
		/// </summary>
		public DateTime? ShippingPlanDate
		{
			set{ _shippingplandate=value;}
			get{return _shippingplandate;}
		}
		/// <summary>
		/// 经手人账号ID
		/// </summary>
		public int? ByHandUserID
		{
			set{ _byhanduserid=value;}
			get{return _byhanduserid;}
		}
		/// <summary>
		/// 是否审核
		/// </summary>
		public bool IsApproved
		{
			set{ _isapproved=value;}
			get{return _isapproved;}
		}
		/// <summary>
		/// 审核人ID
		/// </summary>
		public int? ApprovedUserID
		{
			set{ _approveduserid=value;}
			get{return _approveduserid;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? ApprovedTime
		{
			set{ _approvedtime=value;}
			get{return _approvedtime;}
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

