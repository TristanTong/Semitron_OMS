using System;
namespace Semitron_OMS.Model.Common
{
	/// <summary>
	/// 运营厂商关联表
	/// </summary>
	[Serializable]
	public partial class OperationsBindCustInfoModel
	{
		public OperationsBindCustInfoModel()
		{}
		#region Model
		private int _bindid;
		private int _custid;
		private int _adminid;
		private int _state=1;
		private DateTime _createtime= DateTime.Now;
		private DateTime? _updatetime;
		/// <summary>
		/// 关联ID
		/// </summary>
		public int BindID
		{
			set{ _bindid=value;}
			get{return _bindid;}
		}
		/// <summary>
		/// 厂商ID
		/// </summary>
		public int CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// 运营帐号ID
		/// </summary>
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
		/// <summary>
		/// 状态：1有效，0无效
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

