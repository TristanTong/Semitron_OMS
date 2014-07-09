/**  
* ProductTypeModel.cs
*
* 功 能： N/A
* 类 名： ProductTypeModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/9 19:15:45   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
namespace Semitron_OMS.Model.Site
{
	/// <summary>
	/// ProductTypeModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductTypeModel
	{
		public ProductTypeModel()
		{}
		#region Model
		private int _id;
		private int? _pid;
		private string _code;
		private string _typename;
		private string _lang;
		private string _sk= "1";
		private bool _availflag= true;
		private string _createuser;
		private DateTime? _createtime= DateTime.Now;
		private string _updateuser;
		private DateTime? _updatetime;
		/// <summary>
		/// 类别ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 父类ID
		/// </summary>
		public int? Pid
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 类别编码
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 所属语言
		/// </summary>
		public string Lang
		{
			set{ _lang=value;}
			get{return _lang;}
		}
		/// <summary>
		/// 排序编号
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

