/**  
* AttachmentModel.cs
*
* 功 能： N/A
* 类 名： AttachmentModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/21 23:25:57   童荣辉    初版
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
	/// AttachmentModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AttachmentModel
	{
		public AttachmentModel()
		{}
		#region Model
		private int _id;
		private string _objtype;
		private string _objid;
		private string _physicalpath;
		private string _urlpath;
		private bool _availflag= true;
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
		/// 附件对象类型
		/// </summary>
		public string ObjType
		{
			set{ _objtype=value;}
			get{return _objtype;}
		}
		/// <summary>
		/// 附件对象ID或编号
		/// </summary>
		public string ObjId
		{
			set{ _objid=value;}
			get{return _objid;}
		}
		/// <summary>
		/// 物理路径
		/// </summary>
		public string PhysicalPath
		{
			set{ _physicalpath=value;}
			get{return _physicalpath;}
		}
		/// <summary>
		/// Url路径
		/// </summary>
		public string UrlPath
		{
			set{ _urlpath=value;}
			get{return _urlpath;}
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

