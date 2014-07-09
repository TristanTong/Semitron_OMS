<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单管理系统 - 首页</title>
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/function.js" type="text/javascript"></script>
    <link href="../Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/custom-theme/jquery-ui-1.7.2.custom.css" rel="Stylesheet" type="text/css" />
    <link href="Images/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("li a").css("color", "#005eac");
            var url = $("#<%=hidUrl.ClientID %>").val();
            switch (url) {
                case "SPM/FeeCodeInfoManage.aspx":
                    SetLink("Ul1", "计费指令管理", "SP数据管理");
                    break;
            }
            var urlid = $("#<%=HidUrlID.ClientID %>").val();
            if (url != "") {
                $("#sysMain").attr("src", url + "?" + urlid);
            }
            $("#lbtnExit").click(function () {
                if (confirm("是否注销登陆!")) {
                    $("#btnEdit").click();
                    alert("已安全退出系统");
                    window.location.href = '/Admin/Login.aspx';
                    return true;
                } else {
                    return false;
                }
            });
            $("li a").click(function () {
                $("a").css("color", "#005eac");
                $(this).css("color", "red");
            });

            $("#lbnVersionInfo").click(function () {
                return false;
            });

        });
        //        function MenuClick() {
        //            $("#mainLeft").show();
        //            $("#barImg").attr("src", "images/butClose.gif");
        //        }
    </script>
    <script type="text/javascript">
        function SetLink(linkid, linktext, topLink) {
            $("#tabs ul li").removeClass("hover");
            //设置上面的链接样式
            $("#tabs ul li").each(function (j) {
                if ($("#tabs ul li").eq(j).text() == topLink) {
                    $("#tabs ul li").eq(j).addClass("hover");
                }
            })
            //设置左边的链接
            $(".left_menu").css("display", "none");
            $("#" + linkid + "").parent("div").css("display", "block");
            $("#" + linkid).children("li").each(function (i) {
                if ($("#" + linkid).children("li").eq(i).text() == linktext) {
                    $("#" + linkid).children("li").eq(i).find("a").css("color", "red");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hidUrl" runat="server" />
    <input type="hidden" id="HidUrlID" runat="server" />
    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%" style="background: #EBF5FC;">
        <tbody>
            <tr>
                <td height="70" colspan="3" style="background: url(images/head_bg.gif);">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="24%" height="70">
                                <a href="http://www.semitronelec.com" target="_blank">
                                    <img src="Images/logo1.png" alt="森美创" /></a>
                            </td>
                            <td width="76%" valign="bottom">
                                <!--导航菜单,与下面的相关联,修改时注意参数-->
                                <div id="tabs">
                                    <ul runat="server" id="SplitUl">
                                        <%--                                        <li>
                                            <a href="" target="sysMain"><span>系统管理</span></a></li>
                                        <li>
                                            <a href="" target="sysMain"><span>查询统计</span></a></li>--%>
                                        <%--<li onclick='tabs(1);'><a href="AdminManage.aspx" target='sysMain'><span>计费系统管理</span></a></li>
                                        <li onclick='tabs(2);'><a href="AdminManage.aspx" target='sysMain'><span>计费查询统计</span></a></li>
                                        <li onclick='tabs(3);'><a href="AdminManage.aspx" target='sysMain'><span>LBS定位后台</span></a></li>--%>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30" colspan="3" style="padding: 0px 10px; font-size: 12px; background: url(images/navsub_bg.gif) repeat-x;">
                    <div style="float: right; line-height: 20px;">
                        <asp:LinkButton ID="lbtnExit" runat="server">安全退出</asp:LinkButton>
                        <div style="display: none;">
                            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" /></div>
                    </div>
                    <div style="padding-left: 20px; line-height: 20px; background: url(images/siteico.gif) 0px 0px no-repeat;">
                        <font color="#FF0000">
                            <asp:Label ID="lblAdminName" runat="server" Text="Label"></asp:Label></font>
                        ：您好，欢迎使用[<asp:LinkButton ID="lbnVersionInfo" Style="color: Blue" runat="server"></asp:LinkButton>]版订单管理系统。
                    </div>
                </td>
            </tr>
            <tr>
                <td align="middle" id="mainLeft" valign="top" style="background: #FFF;">
                    <div style="text-align: left; width: 150px; height: 100%; font-size: 12px;">
                        <!--导航顶部-->
                        <div style="padding-left: 10px; height: 29px; line-height: 29px; background: url(images/menu_bg.gif) no-repeat;">
                            <span style="padding-left: 15px; font-weight: bold; color: #039; background: url(images/menu_dot.gif) no-repeat;">
                                功能导航</span>
                        </div>
                        <!--导航菜单,修改时注意顺序-->
                        <div class="left_menu" id="DivSplit">
                            <ul runat="server" id="UlSplit">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="SpiUrl">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="Ul1">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="Ul2">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="Ul3">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="Ul4">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="Ul5">
                            </ul>
                        </div>
                        <div class="left_menu">
                            <ul runat="server" id="Ul6">
                            </ul>
                        </div>
                    </div>
                </td>
                <td valign="middle" style="width: 8px; background: url(images/main_cen_bg.gif) repeat-x;">
                    <div id="sysBar" style="cursor: pointer;">
                        <img id="barImg" src="images/butClose.gif" alt="关闭/打开左栏" /></div>
                </td>
                <td style="width: 100%" valign="top">
                    <iframe frameborder="0" id="sysMain" name="sysMain" scrolling="yes" src="" style="height: 100%;
                        visibility: inherit; width: 100%; z-index: 1;"></iframe>
                </td>
            </tr>
            <%-- <tr>
                <td height="28" colspan="3" bgcolor="#EBF5FC" style="padding: 0px 10px; font-size: 10px;
                    color: #2C89AD; background: url(images/foot_bg.gif) repeat-x;">
                </td>
            </tr>--%>
        </tbody>
    </table>
    </form>
</body>
</html>
