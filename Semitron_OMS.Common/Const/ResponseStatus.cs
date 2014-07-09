using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Const
{
    /// <summary>
    /// 响应状态
    /// </summary>
    public class ResponseStatus
    {
        /// <summary>
        /// 下行成功
        /// </summary>
        public const int MTSuccess = 1;

        /// <summary>
        /// 系统异常
        /// </summary>
        public const int SystemErr = -9999;

        #region 协议头 -1 至 -100

        /// <summary>
        /// 协议头长度异常
        /// </summary>
        public const int HeaderLengthErr = -1;

        /// <summary>
        /// 请求类型异常
        /// </summary>
        public const int RequestTypeErr = -2;

        /// <summary>
        /// 软件标识无效
        /// </summary>
        public const int SoftwareTypeErr = -3;

        /// <summary>
        /// 协议标识无效
        /// </summary>
        public const int ProtocolTypeInvalid = -4;

        /// <summary>
        /// 请求类型无效
        /// </summary>
        public const int RequestTypeInvalid = -5;

        /// <summary>
        /// 非法用户，令牌错误
        /// </summary>
        public const int TokenErr = -11;

        /// <summary>
        /// 非法用户，厂商项目不存在
        /// </summary>
        public const int IllegalUser = -12;

        /// <summary>
        /// 当前项目不支持应用商店。
        /// </summary>
        public const int DoNotSupportAppStore = -13;

        /// <summary>
        /// 当前项目不支持自动安装。
        /// </summary>
        public const int DoNotSupportAutoInstaller = -14;

        /// <summary>
        /// 数据异常：协议头
        /// </summary>
        public const int DataErr_Header = -15;

        #endregion

        #region 用户管理 -101 至 -200

        /// <summary>
        /// 协议请求类型无效：用户管理
        /// </summary>
        public const int ProtecolRequestTypeInvalid_UserManage = -101;

        /// <summary>
        /// 协议长度异常：用户注册。
        /// </summary>
        public const int ProtocolPostLengthErr_UserRegister = -102;

        /// <summary>
        /// 协议长度异常：用户登陆。
        /// </summary>
        public const int ProtocolPostLengthErr_UserLogin = -103;

        /// <summary>
        /// 协议长度异常：同步错误日志。
        /// </summary>
        public const int ProtocolPostLengthErr_SyncErrLog = -104;

        /// <summary>
        /// 数据异常，注册。
        /// </summary>
        public const int DataErr_UserRegister = -111;

        /// <summary>
        /// 数据异常，登陆。
        /// </summary>
        public const int DataErr_UserLogin = -112;

        /// <summary>
        /// 数据异常，同步终端错误日志。
        /// </summary>
        public const int DataErr_SyncErrLog = -113;

        /// <summary>
        /// 业务异常：用户名已存在
        /// </summary>
        public const int BusinessErr_UserNameAlreadyExists = -151;

        /// <summary>
        /// 业务异常：用户名或密码错误
        /// </summary>
        public const int BusinessErr_UserNameOrPasswordErr = -152;

        /// <summary>
        /// 业务异常：用户名不合法。
        /// </summary>
        public const int BusinessErr_UserNameNotValidate = -153;

        /// <summary>
        /// 业务异常：密码不合法。
        /// </summary>
        public const int BusinessErr_PasswordNotValidate = -154;

        #endregion

        #region 首页 -201 至 -300

        /// <summary>
        /// 协议请求类型无效：首页信息
        /// </summary>
        public const int ProtocolRequestTypeInvalid_Index = -201;

        /// <summary>
        /// 协议长度异常：首页信息。
        /// </summary>
        public const int ProtocolPostLengthErr_GetIndexData = -202;

        /// <summary>
        /// 数据异常，首页列表。
        /// </summary>
        public const int DataErr_GetIndexData = -211;
        #endregion

        #region 分类 -301 至 -400
        /// <summary>
        /// 协议请求类型无效：应用分类信息
        /// </summary>
        public const int ProtecolRequestTypeInvalid_Category = -301;

        /// <summary>
        /// 协议长度异常：主分类列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetMailCategoryList = -302;

        /// <summary>
        /// 协议长度异常：子分类列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetChildCategoryList = -303;

        /// <summary>
        /// 协议长度异常：获取类别下的应用列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetAppInfoListByTypeID = -304;

        /// <summary>
        /// 数据异常，分类列表。
        /// </summary>
        public const int DataErr_CategoryList = -311;

        /// <summary>
        /// 数据异常，获取类别下的应用列表。
        /// </summary>
        public const int DataErr_GetAppInfoListByTypeID = -312;

        #endregion

        #region 应用管理 -401 至 -500

        /// <summary>
        /// 协议请求类型无效：应用管理
        /// </summary>
        public const int ProtecolRequestTypeInvalid_AppManage = -401;

        /// <summary>
        /// 协议长度异常：获取单应用详细信息。
        /// </summary>
        public const int ProtocolPostLengthErr_GetAppDetail = -402;

        /// <summary>
        /// 协议长度异常：获取评论列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetCommentList = -403;

        /// <summary>
        /// 协议长度异常：用户评论应用。
        /// </summary>
        public const int ProtocolPostLengthErr_UserCommentingApp = -404;

        /// <summary>
        /// 协议长度异常：用户评分。
        /// </summary>
        public const int ProtocolPostLengthErr_UserGradingApp = -405;

        /// <summary>
        /// 协议长度异常：同步下载中的应用。
        /// </summary>
        public const int ProtocolPostLengthErr_SyncDownloadingApp = -406;

        /// <summary>
        /// 数据异常，获取单应用详细信息。
        /// </summary>
        public const int DataErr_GetAppDetail = -411;

        /// <summary>
        /// 数据异常，获取应用评论列表。
        /// </summary>
        public const int DataErr_GetCommentedList = -412;

        /// <summary>
        /// 数据异常，用户评论应用。
        /// </summary>
        public const int DataErr_UserCommentingApp = -413;

        /// <summary>
        /// 数据异常，用户评分应用。
        /// </summary>
        public const int DataErr_UserGradingApp = -414;

        /// <summary>
        /// 数据异常，同步应用下载信息。
        /// </summary>
        public const int DataErr_SyncDownloadingApp = -415;

        /// <summary>
        /// 业务异常，应用不存在。
        /// </summary>
        public const int BusinessErr_AppNotExist = -416;

        #endregion

        #region 排行 -501 至 -600
        /// <summary>
        /// 协议请求类型无效：排行榜。
        /// </summary>
        public const int ProtecolRequestTypeInvalid_Top = -501;

        /// <summary>
        /// 协议长度异常：获取排行信息。
        /// </summary>
        public const int ProtocolPostLengthErr_GetTopList = -502;

        /// <summary>
        /// 数据异常，获取排行榜信息。
        /// </summary>
        public const int DataErr_GetTopList = -511;

        #endregion

        #region 本地管理 -601 至 -700
        /// <summary>
        /// 协议请求类型无效：本地管理
        /// </summary>
        public const int ProtecolRequestTypeInvalid_LocalManage = -601;

        /// <summary>
        /// 协议长度异常：获取可更新应用列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetUpdateAppList = -602;

        /// <summary>
        /// 协议长度异常：卸载应用。
        /// </summary>
        public const int ProtocolPostLengthErr_UnInstallApp = -603;

        /// <summary>
        /// 协议长度异常：删除下载中的应用。
        /// </summary>
        public const int ProtocolPostLengthErr_DeleteAppOfDownload = -604;

        /// <summary>
        /// 协议长度异常：安装应用。
        /// </summary>
        public const int ProtocolPostLengthErr_InstallApp = -605;

        /// <summary>
        /// 协议长度异常：应用商店升级。
        /// </summary>
        public const int ProtocolPostLengthErr_AppStoreUpgrade = -606;

        /// <summary>
        /// 数据异常，获取可更新应用列表。
        /// </summary>
        public const int DataErr_GetUpdateAppList = -611;

        /// <summary>
        /// 数据异常，卸载应用。
        /// </summary>
        public const int DataErr_UninstallApp = -612;

        /// <summary>
        /// 数据异常，删除下载中的应用。
        /// </summary>
        public const int DataErr_DeleteAppOfDownload = -613;

        /// <summary>
        /// 数据异常，安装应用。
        /// </summary>
        public const int DataErr_InstallApp = -614;

        /// <summary>
        /// 数据异常，应用商店升级。
        /// </summary>
        public const int DataErr_AppStoreUpgrade = -615;

        /// <summary>
        /// 业务异常，删除下载中的应用。
        /// </summary>
        public const int BusinessErr_DeleteAppOfDownload = -620;

        /// <summary>
        /// 业务异常，安装应用。
        /// </summary>
        public const int BusinessErr_InstallApp = -621;

    
        #endregion

        #region 搜索 -701 至 -800

        /// <summary>
        /// 协议请求类型无效：搜索
        /// </summary>
        public const int ProtecolRequestTypeInvalid_Search = -701;

        /// <summary>
        /// 协议长度异常：获取今日热词榜信息。
        /// </summary>
        public const int ProtocolPostLengthErr_GetHotWordList = -702;

        /// <summary>
        /// 协议长度异常：搜索功能。
        /// </summary>
        public const int ProtocolPostLengthErr_UserSearch = -703;

        /// <summary>
        /// 数据异常，获取今日热词榜。
        /// </summary>
        public const int DataErr_GetHotWordList = -711;

        /// <summary>
        /// 数据异常，用户搜索。
        /// </summary>
        public const int DataErr_UserSearch = -712;

        #endregion

        #region 专题 -801 至 -900

        /// <summary>
        /// 协议请求类型无效：专题。
        /// </summary>
        public const int ProtocolRequestTypeInvalid_Special = -801;

        /// <summary>
        /// 协议长度异常：获取专题列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetSpecialList = -802;

        /// <summary>
        /// 协议长度异常：获取专题下的应用列表。
        /// </summary>
        public const int ProtocolPostLengthErr_GetAppInfoListBySpecialID = -803;

        /// <summary>
        /// 数据异常，获取专题列表。
        /// </summary>
        public const int DataErr_GetSpecialList = -811;

        /// <summary>
        /// 数据异常，获取专题下应用列表。
        /// </summary>
        public const int DataErr_GetAppInfoListBySpecialID = -812;

        #endregion

        #region 自动安装 5000 start  
        /// <summary>
        /// 协议请求类型无效：自动安装。
        /// </summary>
        public const int ProtocolRequestTypeInvalid_AutoDownApp = -5001;

        /// <summary>
        /// 协议长度异常：获取自动安装应用列表
        /// </summary>
        public const int ProtocolPostLengthErr_GetAutoDownAppList = -5002;

        /// <summary>
        /// 数据异常：获取自动安装应用列表
        /// </summary>
        public const int DataErr_GetAutoDownAppList = -5003;

        /// <summary>
        /// 业务异常：自动安装黑名单
        /// </summary>
        public const int BusinessErr_AutoBlack = -5004;
        #endregion
    }
}
