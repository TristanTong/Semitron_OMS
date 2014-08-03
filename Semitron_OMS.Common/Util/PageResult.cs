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

    /**
     * navTabAjaxDone是DWZ框架中预定义的表单提交回调函数．
     * 服务器转回navTabId可以把那个navTab标记为reloadFlag=1, 下次切换到那个navTab时会重新载入内容. 
     * callbackType如果是closeCurrent就会关闭当前tab
     * 只有callbackType="forward"时需要forwardUrl值
     * navTabAjaxDone这个回调函数基本可以通用了，如果还有特殊需要也可以自定义回调函数.
     * 如果表单提交只提示操作是否成功, 就可以不指定回调函数. 框架会默认调用DWZ.ajaxDone()
     * <form action="/user.do?method=save" onsubmit="return validateCallback(this, navTabAjaxDone)">
     * 
     * form提交后返回json数据结构statusCode=DWZ.statusCode.ok表示操作成功, 做页面跳转等操作. statusCode=DWZ.statusCode.error表示操作失败, 提示错误原因. 
     * statusCode=DWZ.statusCode.timeout表示session超时，下次点击时跳转到DWZ.loginUrl
     * {"statusCode":"200", "message":"操作成功", "navTabId":"navNewsLi", "forwardUrl":"", "callbackType":"closeCurrent", "rel"."xxxId"}
     * {"statusCode":"300", "message":"操作失败"}
     * {"statusCode":"301", "message":"会话超时"}
     * 
     */
    public class PageResultJUI
    {
        public PageResultJUI()
        {
            statusCode = 200;
        }
        /// <summary>
        /// 返回状态 ok:200, error:300, timeout:301
        /// </summary>
        public int statusCode { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string navTabId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string rel { get; set; }
        /// <summary>
        /// closeCurrent
        /// </summary>
        public string callbackType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string forwardUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string confirmMsg { get; set; }

        public new string ToString()
        {
            return "{\"statusCode\":\"" + statusCode
                + "\",\"message\":\"" + message
                + "\",\"navTabId\":\"" + navTabId
                + "\",\"rel\":\"" + rel
                + "\",\"callbackType\":\"" + callbackType
                + "\",\"forwardUrl\":\"" + forwardUrl
                + "\",\"confirmMsg\":\"" + confirmMsg + "\"}";
        }

    }



}