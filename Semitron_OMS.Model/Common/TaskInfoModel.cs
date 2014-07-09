using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    [Serializable]
    public class TaskInfoModel
    {
        public TaskInfoModel()
        { }
        #region Model
        private int _id;
        private string _taskcode;
        public string _objecttable;
        private string _taskname;
        private string _pagecode;
        private int _valid = 1;
        private DateTime _createtime = DateTime.Now;
        private DateTime? _updatetime;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode
        {
            set { _taskcode = value; }
            get { return _taskcode; }
        }

        /// <summary>
        /// 任务主对象表名
        /// </summary>
        public string ObjectTable
        {
            set { _objecttable = value; }
            get { return _objecttable; }
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            set { _taskname = value; }
            get { return _taskname; }
        }
        /// <summary>
        /// 所在页面代码
        /// </summary>
        public string PageCode
        {
            set { _pagecode = value; }
            get { return _pagecode; }
        }
        /// <summary>
        /// 是否有效：0无效 1有效
        /// </summary>
        public int Valid
        {
            set { _valid = value; }
            get { return _valid; }
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
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model
    }
}
