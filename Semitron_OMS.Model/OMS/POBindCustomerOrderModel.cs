/**  
* POBindCustomerOrderModel.cs
*
* 功 能： N/A
* 类 名： POBindCustomerOrderModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/4 20:47:48   童荣辉    初版
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
	/// 采购订单客户关联表
	/// </summary>
	[Serializable]
	public partial class POBindCustomerOrderModel
	{
		public POBindCustomerOrderModel()
		{}
		#region Model
		private int _id;
		private int _poid;
		private int _customerorderid;
		private string _createuser;
		private DateTime? _createtime= DateTime.Now;
		/// <summary>
		/// 数据唯一标识
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 采购订单ID
		/// </summary>
		public int POID
		{
			set{ _poid=value;}
			get{return _poid;}
		}
		/// <summary>
		/// 客户订单ID
		/// </summary>
		public int CustomerOrderID
		{
			set{ _customerorderid=value;}
			get{return _customerorderid;}
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
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

