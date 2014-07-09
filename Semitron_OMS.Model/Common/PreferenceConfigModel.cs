using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 个人偏好配置
    /// </summary>
    public class PreferenceConfigModel
    {
        public PreferenceConfigModel()
        { }
        #region Model
        private int _id;
        private int _adminid;
        private string _pagecode;
        private string _searchshow;
        private string _searchparam;
        private string _groupparam;
        private string _columnshow;
        private DateTime _createtime = DateTime.Now;
        private DateTime _updatetime = DateTime.Now;
        /// <summary>
        /// 自增序号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public int AdminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }       
        /// <summary>
        /// 配置页面编码
        /// </summary>
        public string PageCode
        {
            get
            {
                return _pagecode;
            }
            set
            {
                _pagecode = value;
            }
        }
        /// <summary>
        /// 查询条件显示配置项
        /// </summary>
        public string SearchShow
        {
            get
            {
                return _searchshow;
            }
            set
            {
                _searchshow = value;
            }
        }
        /// <summary>
        /// 查询条件默认参数值配置项
        /// </summary>
        public string SearchParam
        {
            get
            {
                return _searchparam;
            }
            set
            {
                _searchparam = value;
            }
        }
        /// <summary>
        /// 分组条件默认勾选配置项
        /// </summary>
        public string GroupParam
        {
            get
            {
                return _groupparam;
            }
            set
            {
                _groupparam = value;
            }
        }
        /// <summary>
        /// 表格列名默认显示配置项
        /// </summary>
        public string ColumnShow
        {
            get
            {
                return _columnshow;
            }
            set
            {
                _columnshow = value;
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model
    }
}
