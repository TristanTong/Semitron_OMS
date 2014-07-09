using System;
namespace Semitron_OMS.Model.Common
{
	/// <summary>
	/// 对象权限关联表
	/// </summary>
	[Serializable]
	public partial class ObjectPermissionModel
	{
		public ObjectPermissionModel()
		{}
		#region Model
		private int _id;
		private int _permissionid;
		private int _objtype;
		private int _objid;
		private int _availflag=1;
		/// <summary>
		/// 关联ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 权限ID
		/// </summary>
		public int PermissionID
		{
			set{ _permissionid=value;}
			get{return _permissionid;}
		}
		/// <summary>
		/// 对象类型:1角色 2用户
		/// </summary>
		public int ObjType
		{
			set{ _objtype=value;}
			get{return _objtype;}
		}
		/// <summary>
		/// 角色ID，或用户ID
		/// </summary>
		public int ObjID
		{
			set{ _objid=value;}
			get{return _objid;}
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

