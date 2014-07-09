using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Enum
{
    /// <summary>
    /// 各POST请求待验证长度
    /// </summary>
    public enum CheckPostLength : short
    {
        /// <summary>
        /// 协议头固定长度
        /// </summary>
        Header = 150,

        /// <summary>
        /// 用户管理
        /// </summary>
        UserManage = 214,

        /// <summary>
        /// 首页
        /// </summary>
        Index = 168,

        /// <summary>
        /// 分类
        /// </summary>
        Category = 172,

        /// <summary>
        /// 应用管理
        /// </summary>
        APPManage = 189,

        /// <summary>
        /// 评论应用
        /// </summary>
        CommentApp = 293,

        /// <summary>
        /// 排行榜
        /// </summary>
        TopList = 169,

        /// <summary>
        /// 本地管理
        /// </summary>
        LocalManage = 174,

        /// <summary>
        /// 今日热词榜
        /// </summary>
        HotwordList = 200,

        /// <summary>
        /// 错误日志
        /// </summary>
        ErrorLog = 224
    }
}
