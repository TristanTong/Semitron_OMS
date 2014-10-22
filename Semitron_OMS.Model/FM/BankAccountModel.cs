/**  
* BankAccountModel.cs
*
* 功 能： N/A
* 类 名： BankAccountModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/5 23:43:12   童荣辉    初版
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
	/// BankAccountModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BankAccountModel
	{
		public BankAccountModel()
		{}
		#region Model
		private int _id;
		private string _accountname;
		private string _belongname;
		private string _belongbank;
		private string _cardno;
		private decimal? _realtimebalance;
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
		/// 账户名称
		/// </summary>
		public string AccountName
		{
			set{ _accountname=value;}
			get{return _accountname;}
		}
		/// <summary>
		/// 开户人姓名
		/// </summary>
		public string BelongName
		{
			set{ _belongname=value;}
			get{return _belongname;}
		}
		/// <summary>
		/// 开户行
		/// </summary>
		public string BelongBank
		{
			set{ _belongbank=value;}
			get{return _belongbank;}
		}
		/// <summary>
		/// 银行卡号
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 实时余额
		/// </summary>
		public decimal? RealTimeBalance
		{
			set{ _realtimebalance=value;}
			get{return _realtimebalance;}
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

