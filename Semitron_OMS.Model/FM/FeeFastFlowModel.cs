/**  
* FeeFastFlowModel.cs
*
* 功 能： N/A
* 类 名： FeeFastFlowModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/5 23:43:13   童荣辉    初版
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
	/// FeeFastFlowModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FeeFastFlowModel
	{
		public FeeFastFlowModel()
		{}
		#region Model
		private int _id;
		private string _paytypevalue;
		private int? _bankaccountid;
		private string _feetypevalue;
		private int? _currencytypeid;
		private decimal? _fee;
		private decimal? _increase;
		private decimal? _decrease;
		private decimal? _accountbalance;
		private int? _customerid;
		private int? _supplierid;
		private decimal? _netprofitmargin;
		private string _remark;
		private int? _byhanduserid;
		private DateTime? _occurtime;
		private bool _isfollowaction;
		private bool _availflag;
		private DateTime? _createtime;
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
		/// 收支类别
		/// </summary>
		public string PayTypeValue
		{
			set{ _paytypevalue=value;}
			get{return _paytypevalue;}
		}
		/// <summary>
		/// 银行账户ID
		/// </summary>
		public int? BankAccountID
		{
			set{ _bankaccountid=value;}
			get{return _bankaccountid;}
		}
		/// <summary>
		/// 费用类别值
		/// </summary>
		public string FeeTypeValue
		{
			set{ _feetypevalue=value;}
			get{return _feetypevalue;}
		}
		/// <summary>
		/// 货币单位ID
		/// </summary>
		public int? CurrencyTypeID
		{
			set{ _currencytypeid=value;}
			get{return _currencytypeid;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal? Fee
		{
			set{ _fee=value;}
			get{return _fee;}
		}
		/// <summary>
		/// 增加
		/// </summary>
		public decimal? Increase
		{
			set{ _increase=value;}
			get{return _increase;}
		}
		/// <summary>
		/// 减少
		/// </summary>
		public decimal? Decrease
		{
			set{ _decrease=value;}
			get{return _decrease;}
		}
		/// <summary>
		/// 账户余额
		/// </summary>
		public decimal? AccountBalance
		{
			set{ _accountbalance=value;}
			get{return _accountbalance;}
		}
		/// <summary>
		/// 客户ID
		/// </summary>
		public int? CustomerID
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 供应商ID
		/// </summary>
		public int? SupplierID
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 净利润率
		/// </summary>
		public decimal? NetProfitMargin
		{
			set{ _netprofitmargin=value;}
			get{return _netprofitmargin;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 经办人ID
		/// </summary>
		public int? ByHandUserID
		{
			set{ _byhanduserid=value;}
			get{return _byhanduserid;}
		}
		/// <summary>
		/// 账目发生时间
		/// </summary>
		public DateTime? OccurTime
		{
			set{ _occurtime=value;}
			get{return _occurtime;}
		}
		/// <summary>
		/// 是否后续动作
		/// </summary>
		public bool IsFollowAction
		{
			set{ _isfollowaction=value;}
			get{return _isfollowaction;}
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

