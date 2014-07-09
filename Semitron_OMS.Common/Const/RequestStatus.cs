using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Const
{
    /// <summary>
    /// 应用商店请求类型标识
    /// </summary>
    public static class AppRequestStatus
    {
        /// <summary>
        /// 注册
        /// </summary>
        public const int UserRegister = 1001;

        /// <summary>
        /// 登陆
        /// </summary>
        public const int UserLogin = 1002;

        /// <summary>
        /// 同步错误日志
        /// </summary>
        public const int SyncErrLog = 1003;

        /// <summary>
        /// 获取首页信息
        /// </summary>
        public const int GetIndexData = 2001;

        /// <summary>
        /// 获取主分类列表
        /// </summary>
        public const int GetMailCategoryList = 3001;

        /// <summary>
        /// 获取子分类列表
        /// </summary>
        public const int GetChildCategoryList = 3002;

        /// <summary>
        /// 获取类别下应用列表
        /// </summary>
        public const int GetAppInfoListByTypeID = 3003;

        /// <summary>
        /// 获取单应用详细信息
        /// </summary>
        public const int GetAppDetail = 4002;

        /// <summary>
        /// 获取单应用评论列表
        /// </summary>
        public const int GetCommentList = 4003;

        /// <summary>
        /// 用户评论应用
        /// </summary>
        public const int UserCommentingApp = 4004;

        /// <summary>
        /// 用户评分应用
        /// </summary>
        public const int UserGradingApp = 4005;

        /// <summary>
        /// 同步下载中的应用
        /// </summary>
        public const int SyncDownloadingApp = 4006;

        /// <summary>
        /// 获取排行榜信息
        /// </summary>
        public const int GetTopList = 5001;

        /// <summary>
        /// 获取可更新应用列表
        /// </summary>    
        public const int GetUpdateAppList = 6001;

        /// <summary>
        /// 卸载应用
        /// </summary>
        public const int UnInstallApp = 6002;

        /// <summary>
        /// 删除下载中的应用
        /// </summary>
        public const int DeleteAppOfDownload = 6003;

        /// <summary>
        /// 安装应用
        /// </summary>
        public const int InstallApp = 6004;

        /// <summary>
        /// 应用商店升级
        /// </summary>
        public const int AppStoreUpgrade = 6005;

        /// <summary>
        /// 获取今日热词榜
        /// </summary>
        public const int GetHotWordList = 7001;

        /// <summary>
        /// 用户搜索
        /// </summary>
        public const int UserSearch = 7002;

        /// <summary>
        /// 获取专题列表
        /// </summary>
        public const int GetSpecialList = 8001;

        /// <summary>
        /// 获取专题下的应用列表
        /// </summary>
        public const int GetAppInfoListBySpecialID = 8002;
    }

    /// <summary>
    /// 自动安装请求类型标识
    /// </summary>
    public static class AutoRequestStatus
    {
        /// <summary>
        /// 获取自动安装应用列表
        /// </summary>
        public const int AutoDownApp = 10001;
    }
}
