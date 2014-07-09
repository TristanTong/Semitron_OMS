/**  
* GodownEntryModel.cs
*
* 功 能： N/A
* 类 名： GodownEntryModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:17   童荣辉    初版
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
	/// 入库单表
	/// </summary>
	[Serializable]
	public partial class GodownEntryModel
	{
		public GodownEntryModel()
		{}
		#region Model
		private int _id;
		private string _entryno;
		private int? _byhanduserid;
		private DateTime? _instockdate;
		private string _inwarehousecode;
		private bool _isapproved;
		private int? _approveduser;
		private DateTime? _approvedtime;
		private string _description;
		private int _state=1;
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
		/// 入库单号
		/// </summary>
		public string EntryNo
		{
			set{ _entryno=value;}
			get{return _entryno;}
		}
		/// <summary>
		///  经手人ID
		/// </summary>
		public int? ByHandUserID
		{
			set{ _byhanduserid=value;}
			get{return _byhanduserid;}
		}
		/// <summary>
		/// 入库时间
		/// </summary>
		public DateTime? InStockDate
		{
			set{ _instockdate=value;}
			get{return _instockdate;}
		}
		/// <summary>
		///  收货仓库编码
		/// </summary>
		public string InWarehouseCode
		{
			set{ _inwarehousecode=value;}
			get{return _inwarehousecode;}
		}
		/// <summary>
		/// 是否已审核
		/// </summary>
		public bool IsApproved
		{
			set{ _isapproved=value;}
			get{return _isapproved;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public int? ApprovedUser
		{
			set{ _approveduser=value;}
			get{return _approveduser;}
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
		///  附加说明
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int State
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

