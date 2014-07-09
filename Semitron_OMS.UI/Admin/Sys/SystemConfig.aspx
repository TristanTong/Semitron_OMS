<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemConfig.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Sys.SystemConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单管理系统-系统基本设置</title>
    <%--CSS Ref--%>
    <link rel="stylesheet" type="text/css" href="/Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/fancybox.css" />
    <link rel="stylesheet" href="/Styles/custom.css" />
    <%--JS Ref--%>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div style="font-size: large; font-weight: bold; text-align: center;">
        系统基本设置（注意：如果你不是专业人员请勿改动。）</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tbody>
            <tr>
                <th colspan="2" align="left">
                    Web站点配置参数
                </th>
            </tr>
            <tr>
                <td align="right">
                    超级管理员角色ID：
                </td>
                <td>
                    <asp:TextBox ID="txtSuperAdminRoleId" runat="server" CssClass="txt OnlyInt" size="48"
                        MaxLength="100" HintTitle="超级管理员角色ID" HintInfo="请设置好超级管理员角色ID" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    系统父级权限ID：
                </td>
                <td width="75%">
                    <asp:TextBox ID="txtParentSystem" runat="server" CssClass="txt" size="48" MaxLength="50"
                        HintTitle="系统父级权限ID" HintInfo="请设置好系统父级权限ID" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    系统版本号：
                </td>
                <td width="75%">
                    <asp:TextBox ID="txtSystemVersionInfo" runat="server" CssClass="txt" size="48" MaxLength="50"
                        HintTitle="系统版本号" HintInfo="请设置好系统版本号" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    上传Img文件目录路径：
                </td>
                <td>
                    <asp:TextBox ID="txtImgPath" runat="server" CssClass="txt" size="25" MaxLength="50"
                        HintTitle="上传Img文件目录路径" HintInfo="请设置好上传Img文件目录路径" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    上传Ico文件目录路径：
                </td>
                <td>
                    <asp:TextBox ID="txtIcoPath" runat="server" CssClass="txt" size="25" MaxLength="50"
                        HintTitle="上传Ico文件目录路径" HintInfo="请设置好上传Ico文件目录路径" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    上传Apk文件目录路径：
                </td>
                <td>
                    <asp:TextBox ID="txtApkPath" runat="server" CssClass="txt" size="25" MaxLength="50"
                        HintTitle="上传Apk文件目录路径" HintInfo="请设置好上传Apk文件目录路径" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    文件服务器物理地址：
                </td>
                <td>
                    <asp:TextBox ID="txtFileServerPath" runat="server" CssClass="txt" size="25" MaxLength="50"
                        HintTitle="文件服务器物理地址" HintInfo="请设置好文件服务器物理地址" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    文件服务器URL地址：
                </td>
                <td>
                    <asp:TextBox ID="txtFileServerUrl" runat="server" CssClass="txt" size="25" MaxLength="50"
                        HintTitle="文件服务器URL地址" HintInfo="请设置好文件服务器URL地址" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Semitron_OMS Server配置页面地址：
                </td>
                <td>
                    <asp:TextBox ID="txtSemitron_OMSServerUrl" runat="server" CssClass="txt" size="25" MaxLength="50"
                        HintTitle="Semitron_OMS Server配置页面地址" HintInfo="请设置好Semitron_OMS Server配置页面地址" Width="250px"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%">
        <tr>
            <td width="25%" align="right">
            </td>
            <td width="75%" style="padding-top: 10px; padding-left: 2px;">
                <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="btnHigh" OnClick="btnSave_Click" />
                &nbsp;
                <input name="重置" type="reset" class="btnHigh" value="重置" />
            </td>
        </tr>
    </table>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tbody>
            <tr>
                <td width="100%" align="left" colspan="2">
                    <iframe scrolling="no" frameborder="0" style="background-color: #ffffff; height: 250px;
                        width: 100%" src="" runat="server" id="iframeServerUrl"></iframe>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
