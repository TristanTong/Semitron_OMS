/**  
* WarehouseModel.cs
*
* 功 能： N/A
* 类 名： WarehouseModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:41:50   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
namespace Semitron_OMS.Model.Common
{
	/// <summary>
	/// 出货计划表
	/// </summary>
	[Serializable]
	public partial class WarehouseModel
	{
		public WarehouseModel()
		{}
		#region Model
		private int _id;
		private string _wcode;
		private string _wname;
		private int? _wtype;
		private int? _pid;
		private int? _wbelonguserid;
		private string _sk;
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
		/// 仓库编码
		/// </summary>
		public string WCode
		{
			set{ _wcode=value;}
			get{return _wcode;}
		}
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WName
		{
			set{ _wname=value;}
			get{return _wname;}
		}
		/// <summary>
		/// 仓库分类
		/// </summary>
		public int? WType
		{
			set{ _wtype=value;}
			get{return _wtype;}
		}
		/// <summary>
		/// 仓库父节点ID
		/// </summary>
		public int? PId
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 个人仓关联用户ID
		/// </summary>
		public int? WBelongUserID
		{
			set{ _wbelonguserid=value;}
			get{return _wbelonguserid;}
		}
		/// <summary>
		/// 排序编码
		/// </summary>
		public string SK
		{
			set{ _sk=value;}
			get{return _sk;}
		}
		/// <summary>
		/// 是否有效
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
}

