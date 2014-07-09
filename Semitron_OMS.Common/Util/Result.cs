using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// LBS请求返回类
    /// </summary>
    public class Result
    {
        public string FeeMode { get; set; } //计费模块。
        public string Cmd { get; set; } //指令
        public string Addr { get; set; }//通道号
        public string Key1 { get; set; }//关键字1
        public string Key2 { get; set; }//关键字2
        public string RContent { get; set; }//回复
        public string Fee { get; set; } //终端允许上行次数。
        public string AutoFee { get; set; } //连扣次数。
        public string RId { get; set; } //计费请求记录ID。
        public string CallTime { get; set; }//IVR呼叫时长
        public string PressKeyTime { get; set; }//IVR按键时间
        public string PressKeyNum { get; set; }//IVR模拟按键
        public string TimeKeys { get; set; }//IVR按键集。秒|键,秒|键
        public string ListUrl { get; set; } //WAP列表URL。
        public string ListName { get; set; }//WAP点播名称
        public string OrderKey { get; set; } //WAP订购关键字
        public string WaitTime { get; set; }//WAP等待时间(秒)
        public string DownloadSize { get; set; }//WAP下载大小（KB）
        public string ConfirmOrderKey { get; set; }//WAP确认订购关键字
        public string DownloadKey { get; set; } //WAP下载关键字
        public string FreeKey { get; set; } //WAP免费关键字
        public string JARKey { get; set; }//WAPJAR地址
        public string InstallKey { get; set; }//WAP安装报告关键字
        public string BusinessKey { get; set; } //WAP业务关键字
        public string PortShield { get; set; }//屏蔽端口号
        

        public new string ToString()
        {
            PropertyInfo[] Properts = this.GetType().GetProperties();
            object PropertyValue;
            string result = "";
            for (int i = 0; i < Properts.Length; i++)
            {
                PropertyValue = Properts[i].GetValue(this, null);
                PropertyValue = PropertyValue == null ? "" : PropertyValue.ToString();
                if (!string.IsNullOrEmpty(PropertyValue.ToString()))
                {
                    result += "@" + Properts[i].Name + "=" + PropertyValue;
                }
            }
            if (result.StartsWith("@"))
            {
                result = result.Substring(1);
            }
            return result;
        }

        /// <summary>
        /// 响应单状态
        /// </summary>
        /// <param name="State">单状态</param>
        /// <param name="RequestID">请求记录ID -1 则为未保存</param>
        /// <returns></returns>
        public static string ToString(string State, int RequestID)
        {
            string result = "State=" + State + "@Rid=" + RequestID.ToString();
            return result;
        }

    }

}
