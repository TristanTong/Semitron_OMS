/**  
* ProductShowModel.cs
*
* 功 能： N/A
* 类 名： ProductShowModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/30 10:59:30   童荣辉    初版
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
	/// ProductShowModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductShowModel
	{
		public ProductShowModel()
		{}

        #region Model
        private int _id;
        private string _code;
        private string _name;
        private string _lang = "cn";
        private int? _typeid = 1;
        private string _sk;
        private bool _isshowinmain = false;
        private string _icopath;
        private string _htmlpath;
        private int? _pageheight;
        private bool _availflag = true;
        private string _createuser;
        private DateTime? _createtime = DateTime.Now;
        private string _updateuser;
        private DateTime? _updatetime;
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 所属语言版本
        /// </summary>
        public string Lang
        {
            set { _lang = value; }
            get { return _lang; }
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int? TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 排序编号
        /// </summary>
        public string SK
        {
            set { _sk = value; }
            get { return _sk; }
        }
        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public bool IsShowInMain
        {
            set { _isshowinmain = value; }
            get { return _isshowinmain; }
        }
        /// <summary>
        /// 图标路径
        /// </summary>
        public string IcoPath
        {
            set { _icopath = value; }
            get { return _icopath; }
        }
        /// <summary>
        /// Html路径
        /// </summary>
        public string HtmlPath
        {
            set { _htmlpath = value; }
            get { return _htmlpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PageHeight
        {
            set { _pageheight = value; }
            get { return _pageheight; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model
	}
}

