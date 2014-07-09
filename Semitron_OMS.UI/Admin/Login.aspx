<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单管理系统 - 登陆</title>
    <%--CSS Ref--%>
    <link href="/Styles/login_style.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <%--JS Ref--%>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        $(document).ready(function () {
        //            $.formValidator.initConfig({ formID: "myform", theme: 'SolidBox', mode: 'FixTip', onError: function (msg) { }, inIframe: true });
        //            $("#txtUserName").formValidator({ onShow: "请输入您的登录用户名", onFocus: "长度为4-18位，不能包含等特殊字符", onCorrect: "用户名输入正确" }).inputValidator({ min: 4,
        //                onError: "用户名输入位数错误"
        //            }).regexValidator({ regExp: "username", dataType: "enum", onError: "用户名格式不正确" });
        //            $("#txtPassWord").formValidator({ onShow: "请输入您的登录密码", onFocus: "长度为6-18位，不能包含等特殊字符", onCorrect: "密码输入正确" }).inputValidator({ min: 6,
        //                onError: "密码输入位数错误"
        //            }).regexValidator({ regExp: "username", dataType: "enum", onError: "密码名格式不正确" });
        //            $("#txtLCode").formValidator({ onShow: "请输入验证码", onFocus: "长度为4位，不能包含等特殊字符", onCorrect: "输入位数正确" }).inputValidator({ min: 4,
        //                onError: "输入位数错误"
        //            }).regexValidator({ regExp: "username", dataType: "enum", onError: "验证名格式不正确" });
        //        });


        /*实现在IE6中png图片无阴影*/
        function fixPNG(myImage) {
            var arVersion = navigator.appVersion.split("MSIE");
            var version = parseFloat(arVersion[1]);
            if ((version >= 5.5) && (version < 7) && (document.body.filters)) {
                var imgID = (myImage.id) ? "id='" + myImage.id + "' " : "";
                var imgClass = (myImage.className) ? "class='" + myImage.className + "' " : "";
                var imgTitle = (myImage.title) ? "title='" + myImage.title + "' " : "title='" + myImage.alt + "' ";
                var imgStyle = "display:inline-block;" + myImage.style.cssText;
                var strNewHTML = "<span " + imgID + imgClass + imgTitle + " style=\"" + "width:" + myImage.width + "px; height:" + myImage.height + "px;" + imgStyle + ";" + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader" + "(src=\'" + myImage.src + "\', sizingMethod='scale');\"></span>"; myImage.outerHTML = strNewHTML;
            }
        }
        //获取验证码
        function reloadCode(img) {
            img.src = "CheckCodePage.aspx?t=" + Math.random();
        }
        //加载出验证码
        $(function () {
            $("#divLogin .checkCode").click(function () {
                reloadCode($("#imgLCode").get(0));
            });
        });
    </script>
    <%--登陆操作--%>
    <script type="text/javascript">
        //封装AJAX
        function JsAjaxLogin(url, data, successFun, errorFun) {
            try {

                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: data,
                    success: successFun,
                    error: errorFun
                });
            } catch (err) {
                alert(err);
            }
        }
        //获取验证码
        function reloadCode(img) {
            img.src = "CheckCodePage.aspx?t=" + Math.random();
        }
        //加载出验证码
        $(function () {
            $("#divLogin .checkCode").click(function () {
                reloadCode($("#imgLCode").get(0));
            });

            //登陆开始
            $('#btnLogin').click(function () {
                var userName = $.trim($("#txtUserName").val());
                var pwd = $("#txtPassWord").val();
                var code = $("#txtLCode").val();
                //                userName = "admin";
                //                pwd = "aaaaaa";
                if (userName == "" || pwd == "") {
                    $("#lblLShow").text("请您输入正确的用户名和密码后再登陆。");
                    $("#lblLShow").show();
                    return false;
                }
                if (code.length < 4 || code.length > 4) {
                    $("#lblLShow").text("请您输入正确的验证码。");
                    $("#lblLShow").show();
                    return false;
                }
                userName = userName.replace(/"/ig, "“");
                pwd = pwd.replace(/"/ig, "“");
                code = code.replace(/"/ig, "“");

                var url = "/Handle/Sys/Admin.ashx";
                var data = { "meth": "GetUserlogin", "_username": userName, "_password": pwd, "code": code, "remUsername": false };
                var successFun = function (json) {
                    if (json.State == 0) {
                        artDialog.alert(json.Info);
                        return false;
                    }
                    //默认进入OMOP系统
                    artDialog.tips(json.Info);
                    window.location.href = '/index.html';
                };
                var errorFun = function (x, e) {
                    alert(x.responseText);
                };
                JsAjaxLogin(url, data, successFun, errorFun);
                return false;
            });
        });
    </script>
    <script type="text/javascript">

        //右下角滑动通知
        $(function () {
            artDialog.notice = function (options) {
                var opt = options || {},
        api, aConfig, hide, wrap, top,
        duration = 800;

                var config = {
                    id: 'Notice',
                    left: '100%',
                    top: '100%',
                    fixed: true,
                    drag: false,
                    resize: false,
                    follow: null,
                    lock: false,
                    init: function (here) {
                        api = this;
                        aConfig = api.config;
                        wrap = api.DOM.wrap;
                        top = parseInt(wrap[0].style.top);
                        hide = top + wrap[0].offsetHeight;

                        wrap.css('top', hide + 'px')
                .animate({ top: top + 'px' }, duration, function () {
                    opt.init && opt.init.call(api, here);
                });
                    },
                    close: function (here) {
                        wrap.animate({ top: hide + 'px' }, duration, function () {
                            opt.close && opt.close.call(this, here);
                            aConfig.close = $.noop;
                            api.close();
                        });

                        return false;
                    }
                };

                for (var i in opt) {
                    if (config[i] === undefined) config[i] = opt[i];
                };

                return artDialog(config);
            };


            art.dialog.notice({
                title: '温馨提示',
                width: 220,
                content: $("#divMsg").get(0),
                icon: 'face-smile',
                time: 10
            });
        });
    </script>
</head>
<body>
    <div id="header" style="height: 70px; background: url(images/head_bg.gif);">
        <div class="content">
            <div class="logo">
                <a href="http://www.semitronelec.com" target="_blank">
                    <img src="Images/logo1.png" alt="森美创" /></a>
            </div>
            <span class="title">
                <img src="Images/title.png" onload="fixPNG(this)" alt="" /></span>
        </div>
    </div>
    <div id="content" style="padding: 100px;">
        <form runat="server" id="myform">
            <table>
                <tr style="height: 60px;">
                    <td colspan="3">
                        <h2>欢迎使用订单管理系统</h2>
                    </td>
                </tr>
                <tr style="height: 40px;">
                    <td>
                        <label class="label">
                            用户名：</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" CssClass="txt" runat="server" Width="150" Text=""></asp:TextBox>
                    </td>
                    <td>
                        <div id="txtUserNameTip" style="width: 280px">
                        </div>
                    </td>
                </tr>
                <tr style="height: 40px;">
                    <td>
                        <label class="label">
                            密&nbsp;&nbsp;&nbsp;码：</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassWord" CssClass="txt" runat="server" Width="150" TextMode="Password"
                            Text="aaaaaa"></asp:TextBox>
                    </td>
                    <td>
                        <div id="txtPassWordTip" style="width: 280px">
                        </div>
                    </td>
                </tr>
                <tr style="height: 40px;">
                    <td>
                        <label class="label">
                            验证码：</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLCode" CssClass="txt" runat="server" Width="150"></asp:TextBox>
                    </td>
                    <td id="divLogin">
                        <img class="checkCode" id="imgLCode" align="absMiddle" alt="" src="CheckCodePage.aspx" />
                        <a class="checkCode" href="#">看不清楚?</a> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:CheckBox ID="cborember" runat="server" Checked="true" />记住登录信息--%>
                    </td>
                    <td colspan="2">
                        <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/Admin/Images/login.gif"
                            OnClick="btnLogin_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="2">
                        <label id="lblLShow" style="color: Red">
                        </label>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <%--<div id="footer">
        Copyright <%--<strong><a href="http://www.taichenda.com/" target="_blank">TAICHENDA</a></strong>--%>
    <%--     © 2013-2013
    </div>--%>
    <div>
        <div id="divMsg" style="display: none">
            <asp:Label ID="lblMsgShow" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="dialogDiv">
        <div id="divRedirectSubSystem" style="width: 280px">
            <iframe scrolling="no" frameborder="0" style="background-color: #ececec; height: 180px; width: 100%"
                src="" id="iframeRedirectSubSystem"></iframe>
        </div>
    </div>
</body>
</html>
