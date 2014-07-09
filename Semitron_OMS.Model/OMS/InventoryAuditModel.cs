/**  
* InventoryAuditModel.cs
*
* 功 能： N/A
* 类 名： InventoryAuditModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:19   童荣辉    初版
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
	/// 库存变更日志表
	/// </summary>
	[Serializable]
	public partial class InventoryAuditModel
	{
		public InventoryAuditModel()
		{}
		#region Model
		private int _actionid;
		private string _actiontype;
		private DateTime? _actiontime;
		private int _id;
		private string _productcode;
		private string _wcodelocation;
		private string _wcodebelong;
		private int? _wbelonguserid;
		private int _onhandqty=0;
		private int _poqty=0;
		private int _uninqty=0;
		private int _unoutqty=0;
		private DateTime? _createtime;
		private string _createuser;
		private DateTime? _updatetime;
		private string _updateuser;
		/// <summary>
		/// 数据唯一标识
		/// </summary>
		public int ActionID
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		/// <summary>
		/// 记录变更类型
		/// </summary>
		public string ActionType
		{
			set{ _actiontype=value;}
			get{return _actiontype;}
		}
		/// <summary>
		/// 记录变更时间
		/// </summary>
		public DateTime? ActionTime
		{
			set{ _actiontime=value;}
			get{return _actiontime;}
		}
		/// <summary>
		/// 库存表ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		/// 所在地方仓编码
		/// </summary>
		public string WCodeLocation
		{
			set{ _wcodelocation=value;}
			get{return _wcodelocation;}
		}
		/// <summary>
		/// 归属仓编码
		/// </summary>
		public string WCodeBelong
		{
			set{ _wcodebelong=value;}
			get{return _wcodebelong;}
		}
		/// <summary>
		/// 自有仓归属人ID
		/// </summary>
		public int? WBelongUserID
		{
			set{ _wbelonguserid=value;}
			get{return _wbelonguserid;}
		}
		/// <summary>
		/// 在库数量
		/// </summary>
		public int OnhandQty
		{
			set{ _onhandqty=value;}
			get{return _onhandqty;}
		}
		/// <summary>
		/// 采购中数量
		/// </summary>
		public int POQty
		{
			set{ _poqty=value;}
			get{return _poqty;}
		}
		/// <summary>
		/// 入库中数量
		/// </summary>
		public int UnInQty
		{
			set{ _uninqty=value;}
			get{return _uninqty;}
		}
		/// <summary>
		/// 出库中数量
		/// </summary>
		public int UnOutQty
		{
			set{ _unoutqty=value;}
			get{return _unoutqty;}
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

