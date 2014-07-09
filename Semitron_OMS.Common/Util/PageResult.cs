using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Semitron_OMS.Common
{
    /// <summary>
    /// 返回信息
    /// </summary>
    public class PageResult
    {
        public PageResult()
        {
            State = 0;
        }

        /// <summary>
        /// 返回状态，0为正常
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public new string ToString()
        {
            return "{\"State\":\"" + State + "\",\"Info\":\"" + Info + "\",\"Remark\":\"" + Remark + "\"}";
        }

    }





}