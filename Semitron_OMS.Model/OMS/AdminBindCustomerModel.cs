/**  
* AdminBindCustomerModel.cs
*
* 功 能： N/A
* 类 名： AdminBindCustomerModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/22 13:07:19   童荣辉    初版
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
	/// 销售关联负责客户表
	/// </summary>
	[Serializable]
	public partial class AdminBindCustomerModel
	{
		public AdminBindCustomerModel()
		{}
		#region Model
		private int _bindid;
		private int _servicetype=1;
		private int _adminid;
		private int _customerid;
		private DateTime _createtime= DateTime.Now;
		/// <summary>
		/// 关联ID
		/// </summary>
		public int BindID
		{
			set{ _bindid=value;}
			get{return _bindid;}
		}
		/// <summary>
		/// 业务类型 1：关联负责人
		/// </summary>
		public int ServiceType
		{
			set{ _servicetype=value;}
			get{return _servicetype;}
		}
		/// <summary>
		/// 销售ID
		/// </summary>
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
		/// <summary>
		/// 客户ID
		/// </summary>
		public int CustomerID
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

