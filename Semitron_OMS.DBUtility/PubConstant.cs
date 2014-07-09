using System;
using System.Configuration;
namespace Semitron_OMS.DBUtility
{

    public class PubConstant
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings[Semitron_OMS.Common.ConstantValue.AppSettingsNames.ConnectionString];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 获取短信营销连接字符串。
        /// </summary>
        /// <returns></returns>
        public static string GetAPPConnectionString()
        {
            return GetConnectionString(Semitron_OMS.Common.ConstantValue.AppSettingsNames.APPConnectionString);
        }

        /// <summary>
        /// 获取LbsGps连接字符串。
        /// </summary>
        /// <returns></returns>
        public static string GetLbsGpsConnectionString()
        {
            return GetConnectionString(Semitron_OMS.Common.ConstantValue.AppSettingsNames.LbsGpsConnecString);
        }


        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


    }
}
