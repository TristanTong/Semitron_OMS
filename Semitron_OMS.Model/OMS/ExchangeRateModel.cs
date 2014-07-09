/**  
* ExchangeRateModel.cs
*
* 功 能： N/A
* 类 名： ExchangeRateModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/4 20:47:47   童荣辉    初版
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
	/// 参考汇率表
	/// </summary>
	[Serializable]
	public partial class ExchangeRateModel
	{
		public ExchangeRateModel()
		{}
		#region Model
		private int _id;
		private DateTime _ratedate;
		private int? _currencytypeid;
		private decimal? _refrate;
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
		/// 汇率日期
		/// </summary>
		public DateTime RateDate
		{
			set{ _ratedate=value;}
			get{return _ratedate;}
		}
		/// <summary>
		/// 货币种类ID
		/// </summary>
		public int? CurrencyTypeID
		{
			set{ _currencytypeid=value;}
			get{return _currencytypeid;}
		}
		/// <summary>
		/// 转换为美元汇率
		/// </summary>
		public decimal? RefRate
		{
			set{ _refrate=value;}
			get{return _refrate;}
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

