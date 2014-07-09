using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// 分页查询信息(条件传输)
    /// </summary>
    public class PageSearchInfo
    {
        /// <summary>
        /// 条件过滤集合
        /// </summary>
        public List<SQLConditionFilter> ConditionFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderByField
        {
            get;
            set;
        }

        /// <summary>
        /// 排序类型(0:升序；1:降序)
        /// </summary>
        public int OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 页码数
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageSearchInfo()
        {
            ConditionFilter = new List<SQLConditionFilter>();
            OrderByField = string.Empty;
            OrderType = 1;
            PageSize = 20;
            PageIndex = 1;
        }
    }
}
