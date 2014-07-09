using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// 协议响应列表参数对象（封装成对象，便于扩展）
    /// </summary>
    public class RespListParam
    {
        public RespListParam() { }

        public RespListParam(int PageSize,int TopSize, int PageIndex, bool IsOpenCache, bool IsOpenAppOEM, int OEMType, int AppTypeID, int ProID)
        {
            this.PageSize = PageSize;
            this.TopSize = TopSize;
            this.PageIndex = PageIndex;
            this.IsOpenCache = IsOpenCache;
            this.IsOpenAppOEM = IsOpenAppOEM;
            this.OEMType = OEMType;
            this.ProID = ProID;
            this.AppTypeID = AppTypeID;
        }

        //public RespListParam(int StartIndex, int EndIndex, int ProID)
        //{
        //    this.StartIndex = StartIndex;
        //    this.EndIndex = EndIndex;
        //    this.ProID = ProID;
        //}

        //public RespListParam(int StartIndex, int EndIndex)
        //{
        //    this.StartIndex = StartIndex;
        //    this.EndIndex = EndIndex;
        //}

        /// <summary>
        /// 最大获取数量
        /// </summary>
        public int TopSize { get; set; }

        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 起始索引（包含）
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// 截至索引（包含）
        /// </summary>
        public int EndIndex { get; set; }

        /// <summary>
        /// 是否开启缓存
        /// </summary>
        public bool IsOpenCache { get; set; }

        /// <summary>
        /// OEM类型 1：首页置顶OEM，2：首页推荐OEM，3：类别应用OEM，4：专题列表OEM，5：专题应用OEM，7：排行榜OEM。
        /// </summary>
        public int OEMType { get; set; }

        /// <summary>
        /// 应用类别ID
        /// </summary>
        public int AppTypeID { get; set; }

        /// <summary>
        /// 应用类别排序类型。1：最热应用，2：上升最快。
        /// </summary>
        public int AppTypeSortType { get; set; }

        /// <summary>
        /// 排行榜排序类型：
        /// </summary>
        public int TopListSortType { get; set; }

        /// <summary>
        /// OEM项目ID
        /// </summary>
        public int ProID { get; set; }

        /// <summary>
        /// 项目属性，是否开启AppOEM
        /// </summary>
        public bool IsOpenAppOEM { get; set; }
    }
}
