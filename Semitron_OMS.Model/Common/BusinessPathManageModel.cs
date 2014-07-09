using System;
namespace Semitron_OMS.Model.Common
{
	/// <summary>
	/// BusinessPathManageModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BusinessPathManageModel
	{
		public BusinessPathManageModel()
		{}
		#region Model
		private int _id;
		private string _savepathname;
        private string _pathurl;
		private string _remark;
        private int _availflag = 1;
        private int _type = 0;
		private DateTime _createtime= DateTime.Now;
		private DateTime? _updatetime;
		/// <summary>
		/// 主键标识
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 存放处理路径名称
		/// </summary>
		public string SavePathName
		{
			set{ _savepathname=value;}
			get{return _savepathname;}
		}
        /// <summary>
        /// 业务路径
        /// </summary>
        public string PathURL
        {
            set { _pathurl = value; }
            get { return _pathurl; }
        }
		/// <summary>
		/// 描述
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 状态：1有效 0无效
		/// </summary>
		public int AvailFlag
		{
			set{ _availflag=value;}
			get{return _availflag;}
		}
        /// <summary>
        /// 类型：1SP 2CP
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
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

