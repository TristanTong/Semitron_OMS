using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    public class ConstantValue
    {
        /// <summary>
        /// 系统定义常量
        /// </summary>
        public struct SystemConst
        {
            /// <summary>
            /// 数据权限代码
            /// </summary>
            public const string DATA_PERMISSION = "Admin/MyDataPermission.aspx";

            /// <summary>
            /// 所有客户数据
            /// </summary>
            public const string ALL_CUSTOMER_VIEW = "AllCustomerView";

            /// <summary>
            /// 所有供应商数据
            /// </summary>
            public const string ALL_SUPPLIER_VIEW = "AllSupplierView";
        }

        /// <summary>
        /// 存储过程名
        /// </summary>
        public struct ProcedureNames
        {
            /// <summary>
            /// 批量保存附件文件
            /// </summary>
            public static string AttachmentBatchSaveFiles = "Pro_Attachment_BatchSaveFiles";
            /// <summary>
            /// 分页存储过程名
            /// </summary>
            public const string PageProcedureName = "Pro_GetDataExt";
            /// <summary>
            /// 绑定客户订单
            /// </summary>
            public const string BindCustiomerOrder = "Pro_CustomerOrder_BindCustiomerOrder";
            /// <summary>
            /// 执行销售审核
            /// </summary>
            public const string ConfirmFirst = "Pro_CustomerOrder_ConfirmFirst";
            /// <summary>
            /// 获取产品销售分析列表数据
            /// </summary>
            public const string GetConfirmFirstData = "Pro_CustomerOrder_GetConfirmFirstData";
            /// <summary>
            /// 通过采购订单Id获取相关联及待关联的客户订单
            /// </summary>
            public const string GetCustomerOrderByPoId = "Pro_CustomerOrder_GetCustomerOrderByPoId";
            /// <summary>
            /// 取消客户订单
            /// </summary>
            public const string ValidateAndCancelCustomerOrder = "Pro_CustomerOrder_ValidateAndCancelCustomerOrder";
            /// <summary>
            /// 客户订单出货
            /// </summary>
            public const string ValidateAndOutStock = "Pro_CustomerOrder_ValidateAndOutStock";
            /// <summary>
            /// 取消客户订单明细
            /// </summary>
            public const string ValidateAndDelCustomerOrderDetail = "Pro_CustomerOrderDetail_ValidateAndDelCustomerOrderDetail";
            /// <summary>
            /// 审核入库单
            /// </summary>
            public const string ApproveGodownEntry = "Pro_GodownEntry_Approve";
            /// <summary>
            /// 绑定采购计划
            /// </summary>
            public static string BindPOPlan = "Pro_PO_BindPOPlan";
            /// <summary>
            /// 执行采购审核
            /// </summary>
            public const string ConfirmSecond = "Pro_PO_ConfirmSecond";
            /// <summary>
            /// 执行供应商审核
            /// </summary>
            public const string ConfirmSupplier = "Pro_PO_ConfirmSupplier";
            /// <summary>
            /// 通过采购订单Id获取相关联采购计划列表树
            /// </summary>
            public const string GetBindPOPlanByPoId = "Pro_PO_GetBindPOPlanByPoId";
            /// <summary>
            /// 获取产品采购分析列表数据
            /// </summary>
            public const string GetConfirmSecondData = "Pro_PO_GetConfirmSecondData";
            /// <summary>
            /// 获取采购订单关联的客户清单明细
            /// </summary>
            public const string GetPOBindCustomerOrderDetail = "Pro_PO_GetPOBindCustomerOrderDetail";
            /// <summary>
            /// 生成采购计划
            /// </summary>
            public const string GeneratePOPlan = "Pro_PO_GeneratePOPlan";
            /// <summary>
            /// 删除采购订单
            /// </summary>
            public const string ValidateAndDelPO = "Pro_PO_ValidateAndDelPO";
            /// <summary>
            /// 自动生成采购计划
            /// </summary>
            public static string AutoGeneratePOPlan = "Pro_POPlan_AutoGeneratePOPlan";
            /// <summary>
            /// 取消采购计划
            /// </summary>
            public const string ValidateAndCancelPOPlan = "Pro_POPlan_ValidateAndCancelPOPlan";
            /// <summary>
            /// 确认采购计划价格、数量
            /// </summary>
            public const string ValidateAndConfirmPrice = "Pro_POPlan_ValidateAndConfirmPrice";
            /// <summary>
            /// QC确认
            /// </summary>
            public const string ValidateAndQCConfirm = "Pro_POPlan_ValidateAndQCConfirm";
            /// <summary>
            /// 商务总表
            /// </summary>
            public const string BusinessTransactionsReport = "Pro_Report_BusinessTransactions";
            /// <summary>
            /// 客户交易总表
            /// </summary>
            public const string CustomerOrderTransactionReport = "Pro_Report_CustomerOrderTransaction";
            /// <summary>
            /// 账务出库单数据
            /// </summary>
            public static string ShippingListForFinanceReport = "Pro_Report_ShippingListForFinance";
            /// <summary>
            /// 出库单审核
            /// </summary>
            public const string ApproveShippingList = "Pro_ShippingList_Approve";
            /// <summary>
            /// 出货计划审核
            /// </summary>
            public const string ApproveShippingPlan = "Pro_ShippingPlan_Approve";
            /// <summary>
            /// 网站搜索页面查询随机树
            /// </summary>
            public const string GetSearchTree = "Pro_SiteSearch_GetSearchTree";
            /// <summary>
            /// 网站搜索页面结果列表
            /// </summary>
            public const string GetSearchInIndex = "Pro_SiteSearch_GetSearchInIndex";


        }
        /// <summary>
        /// 缓存数据库实体名
        /// </summary>
        public struct DataBaseEntryNames
        {
            public const string codematic = "codematic";
        }

        /// <summary>
        /// Session会话主键
        /// </summary>
        public struct SessionKeys
        {
            /// <summary>
            /// 数据库连接字符串
            /// </summary>
            public const string UploadifyFilePath = "UploadifyFilePath";

            /// <summary>
            /// 登录用户绑定的厂商ID
            /// </summary>
            public const string ADMIN_CUSTIDS = "AdminCustIDs";
        }

        /// <summary>
        /// Cookie缓存主键
        /// </summary>
        public struct CookieKeys
        {
            /// <summary>
            /// 登陆实体缓存主键
            /// </summary>
            public const string LoginAdminModel = "LoginAdminModel";
        }

        /// <summary>
        /// AppSetting应用程序设置项常量值
        /// </summary>
        public struct AppSettingsNames
        {
            /// <summary>
            /// 数据库连接字符串
            /// </summary>
            public const string ConnectionString = "ConnectionString";
            /// <summary>
            /// 数据库连接字符串
            /// </summary>
            public const string BMPConnectionString = "BMPConnectionString";
            /// <summary>
            /// LbsGps数据库连接字符串
            /// </summary>
            public const string LbsGpsConnecString = "LbsGpsConnecString";
            /// <summary>
            /// APP数据库连接字符串
            /// </summary>
            public const string APPConnectionString = "APPConnectionString";
            /// <summary>
            /// SMS短信客户端数据库连接字符串
            /// </summary>
            public const string SMSConnectionString = "SMSConnectionString";
            /// <summary>
            /// 客户订单上传文件目录地址
            /// </summary>
            public const string CustomerFilePath = "CustomerFilePath";
            /// <summary>
            /// 下载中心文件目录地址
            /// </summary>
            public const string DownloadCenterFilePath = "DownloadCenterFilePath";
            /// <summary>
            /// 计费明细财务附件目录地址
            /// </summary>
            public const string OrderFeeFilePath = "OrderFeeFilePath";
            /// <summary>
            /// QC确认上传文件目录地址
            /// </summary>
            public const string QCFilePath = "QCFilePath";
            /// <summary>
            /// 文件服务器物理地址
            /// </summary>
            public const string FileServerPath = "FileServerPath";
            /// <summary>
            /// 文件服务器URL地址
            /// </summary>
            public const string FileServerUrl = "FileServerUrl";

            /// <summary>
            /// 执行APK文件的控制台应用程序的路径
            /// </summary>
            public const string APKCONSOLEPATH = "ApkConsolePath";

            /// <summary>
            /// IIS用户名
            /// </summary>
            public const string SERVER_USERNAME = "ServerUserName";

            /// <summary>
            /// IIS密码
            /// </summary>
            public const string SERVER_PASSWORD = "ServerPassword";
        }

        /// <summary>
        /// 查询字符串值
        /// </summary>
        public struct QueryStringValues
        {
            /// <summary>
            /// 缓存键值，缓存表时以表名为键值
            /// </summary>
            public const string Key = "key";
            /// <summary>
            /// 缓存下拉框名称
            /// </summary>
            public const string Text = "Text";
            /// <summary>
            /// 缓存下拉框值
            /// </summary>
            public const string Value = "value";
        }

        /// <summary>
        /// 表名常量值
        /// 注意按表名首字母ABCD依次排序
        /// </summary>
        public struct TableNames
        {
            /// <summary>
            /// 用户表
            /// </summary>
            public const string Admin = "Admin";
            /// <summary>
            /// 采购绑定供应商表
            /// </summary>
            public static string AdminBindSupplier = "AdminBindSupplier";
            /// <summary>
            /// 销售关联客户表
            /// </summary>
            public static string AdminBindCustomer = "AdminBindCustomer";
            /// <summary>
            /// 品牌表
            /// </summary>
            public const string Brand = "Brand";
            /// <summary>
            /// 数据公用表
            /// </summary>
            public const string CommonTable = "CommonTable";
            /// <summary>
            /// 公司表
            /// </summary>
            public const string Corporation = "Corporation";
            /// <summary>
            /// 货币种类表
            /// </summary>
            public const string CurrencyType = "CurrencyType";
            /// <summary>
            /// 客户表
            /// </summary>
            public const string Customer = "Customer";
            /// <summary>
            /// 客户订单表
            /// </summary>
            public const string CustomerOrder = "CustomerOrder";
            /// <summary>
            /// 下载中心文件信息表
            /// </summary>
            public const string DownloadCenter = "DownloadCenter";
            /// <summary>
            /// 操作日志表
            /// </summary>
            public const string OperationsLog = "OperationsLog";
            /// <summary>
            /// 付款方式表
            /// </summary>
            public const string PaymentType = "PaymentType";
            /// <summary>
            /// 采购订单表
            /// </summary>
            public const string PO = "PO";
            /// <summary>
            /// 采购计划表
            /// </summary>
            public const string POPlan = "POPlan";
            /// <summary>
            /// 解决方案表
            /// </summary>
            public const string Solution = "Solution";
            /// <summary>
            /// 供应商表
            /// </summary>
            public const string Supplier = "Supplier";
            /// <summary>
            /// 行业动态表
            /// </summary>
            public const string IndustryTrend = "IndustryTrend";
            /// <summary>
            /// 产品展示表
            /// </summary>
            public const string ProductShow = "ProductShow";
            /// <summary>
            /// 产品类型表
            /// </summary>
            public const string ProductType = "ProductType";
            /// <summary>
            /// 用户角色关联表
            /// </summary>
            public const string UserRole = "UserRole";

        }

        /// <summary>
        /// 视图名常量值
        /// </summary>
        public struct ViewNames
        {
            /// <summary>
            /// 地区视图
            /// </summary>
            public const string V_Area = "V_Area";

        }

        #region TableColumns 数据库表列

        /// <summary>
        /// 系统日志表
        /// </summary>
        public struct TableSystemLog
        {
            #region ColumnNames
            /// <summary>
            /// 系统级别列
            /// </summary>
            public const string LogLevel = "LogLevel";
            #endregion ColumnNames

            #region ColumnValues

            /// <summary>
            /// 系统级别列取值
            /// </summary>
            public struct ColumnLogLevel
            {
                /// <summary>
                ///调试信息
                /// </summary>
                public const string Debug = "DEBUG";

                /// <summary>
                /// 运行信息
                /// </summary>
                public const string Info = "INFO";

                /// <summary>
                /// 一般错误信息
                /// </summary>
                public const string Error = "ERROR";

                /// <summary>
                /// 警告错误信息
                /// </summary>
                public const string Warn = "WARN";

                /// <summary>
                /// 致命错误信息
                /// </summary>
                public const string Fatal = "FATAL";
            }
            #endregion ColumnValues
        }

        /// <summary>
        /// 系统附件表
        /// </summary>
        public struct TableAttachment
        {
            #region ColumnNames
            /// <summary>
            /// 系统级别列
            /// </summary>
            public const string ObjType = "ObjType";
            #endregion ColumnNames

            #region ColumnValues

            /// <summary>
            /// 附件对象列取值
            /// </summary>
            public struct ColumnObjType
            {
                /// <summary>
                ///客户订单附件
                /// </summary>
                public const string CustomerOrder = "CustomerOrder";
                /// <summary>
                /// 下载中心文件
                /// </summary>
                public const string DownloadCenter = "DownloadCenter";
                /// <summary>
                /// 采购计划QC确认进货
                /// </summary>
                public const string POPlanQC = "POPlanQC";
                /// <summary>
                /// 计费明细财务附件
                /// </summary>
                public const string OrderFee = "OrderFee";
                /// <summary>
                /// 付款计划附件
                /// </summary>
                public const string PaymentPlanFilePath = "PaymentPlanFilePath";
                /// <summary>
                /// 收款计划附件
                /// </summary>
                public const string GatheringPlanFilePath = "GatheringPlanFilePath";
            }
            #endregion ColumnValues
        }

        #endregion TableColumns

        /// <summary>
        /// SQL通知依赖语句
        /// </summary>
        public struct SQLNotifierDepObj
        {
            /// <summary>
            /// 客户表的通知依赖
            /// </summary>
            public const string AdminDepSql = "SELECT  AdminID ,Username ,Name ,Phone ,Email ,AvailFlag,Password ,CustID ,CreateTime ,LastLoginTime ,Type FROM dbo.Admin";
            /// <summary>
            /// 采购关联供应商通知依赖
            /// </summary>
            public const string AdminBindSupplierDepSql = "SELECT [BindID],[ServiceType],[AdminID],[SupplierID],[CreateTime] FROM [dbo].[AdminBindSupplier] WHERE [AdminID]=@AdminID";
            /// <summary>
            /// 销售关联客户通知依赖
            /// </summary>
            public const string AdminBindCustomerDepSql = "SELECT [BindID],[ServiceType],[AdminID],[CustomerID],[CreateTime] FROM [dbo].[AdminBindCustomer]  WHERE [AdminID]=@AdminID";
            /// <summary>
            /// 根据角色ID取得对应的用户通知依赖
            /// </summary>
            public const string AdminDepGetByRoleIds = "SELECT A.AdminID,A.NAME,A.AvailFlag,A.CustID,A.Username,A.Type FROM Admin AS A INNER JOIN UserRole AS U ON A.AdminID=U.AdminID WHERE U.RoleID IN (@RoleIds)";
            /// <summary>
            /// 品牌表的通知依赖
            /// </summary>
            public const string BrandTableDepSql = "SELECT  ID ,BrandName ,Code ,SK ,AvailFlag  FROM Brand";
            /// <summary>
            /// 公共数据表的通知依赖
            /// </summary>
            public const string CommonTableDepSql = "SELECT [TableName],[FieldID],[Key],[Value],[Desc] FROM CommonTable WHERE TableName=@TableName";
            /// <summary>
            /// 公司表的通知依赖
            /// </summary>
            public const string CorporationSql = "SELECT  ID,CompanyName,SK,AvailFlag FROM dbo.Corporation";
            /// <summary>
            /// 客户表的通知依赖
            /// </summary>
            public const string CustomerDepSql = "SELECT  ID,CCode,CustomerName,SK,AvailFlag FROM dbo.Customer";
            /// <summary>
            /// 货币种类表的通知依赖
            /// </summary>
            public const string CurrencyTypeDepSql = "SELECT  ID ,CurrencyName ,ShortName ,CountryName ,SK ,AvailFlag  FROM dbo.CurrencyType";
            /// <summary>
            /// 付款方式表的通知依赖
            /// </summary>
            public const string PaymentTypeDepSql = "SELECT  ID,PaymentType,SK,AvailFlag FROM dbo.PaymentType";
            /// <summary>
            /// 产品类型表的通知依赖
            /// </summary>
            public const string ProductTypeDepSql = "SELECT ID ,TypeName ,AvailFlag,SK FROM dbo.ProductType";
            /// <summary>
            /// 供应商表的通知依赖
            /// </summary>
            public const string SupplierDepSql = "SELECT  ID,SCode,SupplierName,SK,AvailFlag FROM dbo.Supplier";
        }
    }
}
