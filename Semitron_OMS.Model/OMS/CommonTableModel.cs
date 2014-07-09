/**  
* CommonTableModel.cs
*
* 功 能： N/A
* 类 名： CommonTableModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 12:05:52   童荣辉    初版
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
	/// 公共值表(数据表字段常量值)
	/// </summary>
	[Serializable]
	public partial class CommonTableModel
	{
		public CommonTableModel()
		{}
		#region Model
		private int _id;
		private string _tablename;
		private string _fieldid;
		private string _key;
		private string _value;
		private string _desc;
		private string _createuser;
		private DateTime? _createtime= DateTime.Now;
		private string _updateuser;
		private DateTime? _updatetime;
		/// <summary>
		/// 数据唯一标识
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 表字段名
		/// </summary>
		public string FieldID
		{
			set{ _fieldid=value;}
			get{return _fieldid;}
		}
		/// <summary>
		/// 表字段值
		/// </summary>
		public string Key
		{
			set{ _key=value;}
			get{return _key;}
		}
		/// <summary>
		/// 表字段值对应的文本
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Desc
		{
			set{ _desc=value;}
			get{return _desc;}
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
		/// <summary>
		/// 更新人
		/// </summary>
		public string UpdateUser
		{
			set{ _updateuser=value;}
			get{return _updateuser;}
		}
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

