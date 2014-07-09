using System;
namespace Semitron_OMS.Model.Common
{
	/// <summary>
	/// 请求记录（每天清空一次）
	/// </summary>
	[Serializable]
	public partial class RoleModel
	{
		public RoleModel()
		{}
		#region Model
		private int _roleid;
		private string _rolename;
		private string _description;
		private int _availflag=1;
		/// <summary>
		/// 角色ID
		/// </summary>
		public int RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 角色名
		/// </summary>
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 角色描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 是否有效(0:否1:否)
		/// </summary>
		public int AvailFlag
		{
			set{ _availflag=value;}
			get{return _availflag;}
		}
		#endregion Model

	}
}

