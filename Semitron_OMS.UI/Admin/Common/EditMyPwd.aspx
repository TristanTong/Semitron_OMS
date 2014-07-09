<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMyPwd.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Common.EditMyPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单管理系统 - 修改个人密码</title>
    <link href="/Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/login_style.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <link href="../Images/style.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
    <script src="/Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.core-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.excheck-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.exedit-3.0.js" type="text/javascript"></script>
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            parent.document.title = document.title;
            $("#Affirm").click(function () {
                UpdatePwd();
                return false;
            });
        });
    </script>
    <%--修改密码--%>
    <script type="text/javascript">
        function UpdatePwd() {
            var pwd = $("#txtNewPassword").val();
            var Jpwd = $("#txtPassWord").val();

            if (pwd.length > 5 && pwd.length < 19) {
                if ($("#txtNewPassword").val() ==
$("#txtVerifyNewPassword").val()) {
                    var url = "/Handle/Sys/Admin.ashx";
                    var data = { "meth": "EditMyPwd",
                        "newPassword": pwd, "oldPassWord": Jpwd
                    };
                    var successFun = function (json) {
                        if (json.State == 0) {
                            artDialog.alert(json.Info);
                            return false;
                        }
                        artDialog.tips(json.Info);
                        top.location.href = "/Admin/Login.aspx";
                        return true;
                    };
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    JsAjax(url, data, successFun,
errorFun);
                    return false;
                } else {
                    artDialog.tips("两次密码不相同!");
                    return false;
                }
            } else {
                artDialog.tips("密码不能小于6位或大于18位!");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="padding-top: 100px">
        <table style="height: 25px; line-height: 25px;">
            <tbody>
                <tr>
                    <th colspan="2" align="left">
                        修改个人密码
                    </th>
                </tr>
                <tr>
                    <td width="25%" align="right">
                        用户名：
                    </td>
                    <td width="75%" align="left">
                        <asp:TextBox ID="txtUserName" CssClass="txt" runat="server" Width="150" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%" align="right">
                        请输入旧密码：
                    </td>
                    <td width="75%" align="left">
                        <asp:TextBox ID="txtPassWord" CssClass="txt" runat="server" Width="150" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%" align="right">
                        新密码：
                    </td>
                    <td width="75%" align="left">
                        <asp:TextBox ID="txtNewPassword" CssClass="txt" runat="server" Width="150" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%" align="right">
                        确认新密码：
                    </td>
                    <td width="75%" align="left">
                        <asp:TextBox ID="txtVerifyNewPassword" CssClass="txt" runat="server" Width="150"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        *成功修改个人密码后需要重新登陆。
                    </td>
                </tr>
            </tbody>
            <tr align="center">
                <td colspan="2">
                    <asp:Button CssClass="btnHigh" ID="Affirm" runat="server" Text="确定" OnClick="Affirm_Click" /><asp:Button
                        CssClass="btnHigh" ID="Cancel" runat="server" Text="取消" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
