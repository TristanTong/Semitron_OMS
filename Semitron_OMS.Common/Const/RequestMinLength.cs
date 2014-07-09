using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Const
{
    /// <summary>
    /// 应用商店各请求类型最小长度
    /// </summary>
    public static class AppRequestMinLength
    {

        /// <summary>
        /// 协议头固定长度
        /// </summary>
        public const int Header = 150;

        /// <summary>
        /// 用户注册
        /// </summary>
        public const int UserRegister = 229;

        /// <summary>
        /// 用户登陆
        /// </summary>
        public const int UserLogin = 229;

        /// <summary>
        /// 同步错误日志
        /// </summary>
        public const int SyncErrLog = 151;

        /// <summary>
        /// 获取首页信息
        /// </summary>
        public const int GetIndexData = 168;

        /// <summary>
        /// 获取主分类列表
        /// </summary>
        public const int GetMailCategoryList = 173;

        /// <summary>
        /// 获取子分类列表
        /// </summary>
        public const int GetChildCategoryList = 173;

        /// <summary>
        /// 获取类别下应用列表
        /// </summary>
        public const int GetAppInfoListByTypeID = 173;

        /// <summary>
        /// 获取单应用详细信息
        /// </summary>
        public const int GetAppDetail = 177;

        /// <summary>
        /// 获取单应用评论列表
        /// </summary>
        public const int GetCommentList = 177;

        /// <summary>
        /// 用户评论应用
        /// </summary>
        public const int UserCommentingApp = 162;

        /// <summary>
        /// 用户评分应用
        /// </summary>
        public const int UserGradingApp = 177;

        /// <summary>
        /// 同步下载中的应用
        /// </summary>
        public const int SyncDownloadingApp = 177;

        /// <summary>
        /// 获取排行榜信息
        /// </summary>
        public const int GetTopList = 169;

        /// <summary>
        /// 获取可更新应用列表
        /// </summary>    
        public const int GetUpdateAppList = 168;

        /// <summary>
        /// 卸载应用
        /// </summary>
        public const int UnInstallApp = 166;

        /// <summary>
        /// 删除下载中的应用
        /// </summary>
        public const int DeleteAppOfDownload = 166;

        /// <summary>
        /// 安装应用
        /// </summary>
        public const int InstallApp = 166;

        /// <summary>
        /// 应用商店升级
        /// </summary>
        public const int AppStoreUpgrade = 166;

        /// <summary>
        /// 获取今日热词榜
        /// </summary>
        public const int GetHotWordList = 200;

        /// <summary>
        /// 用户搜索
        /// </summary>
        public const int UserSearch = 200;

        /// <summary>
        /// 获取专题列表
        /// </summary>
        public const int GetSpecialList = 172;

        /// <summary>
        /// 获取专题下应用列表
        /// </summary>
        public const int GetAppInfoListBySpecialID = 172;
    }

    /// <summary>
    /// 自动安装各请求类型最小长度
    /// </summary>
    public static class AutoRequestMinLength 
    {
        /// <summary>
        /// 获取自动安装列表
        /// </summary>
        public const int GetDownAppList = 168;
    }
}
