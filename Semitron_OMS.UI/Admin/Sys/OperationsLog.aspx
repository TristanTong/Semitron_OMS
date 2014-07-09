<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationsLog.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.Sys.OperationsLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单管理系统 - 操作日志</title>
    <%--CSS--%>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <%--JS--%>
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Scripts/public-common-function.js" type="text/javascript"></script>
    <%--初始化，绑定事件--%>
    <script type="text/javascript">
        $(function () {
            parent.document.title = document.title;
            var gridHeight = document.documentElement.clientHeight - 205;
            var bShowCheckBox = true;
            var strExtParam = "{ name: 'meth', value: 'GetOperator' }, { name: 'txtBeginTime', value:'" + $("#txtBeginTime").val() + "' }";
            var strColModel = "{display: '序号', name: 'rownum', toggle: true, hide: true, iskey: true, width: 100, align: 'center' }, { display: '编号', name: 'OperateID', toggle: true, hide: false, width: 60, align: 'center' }, { display: '管理员ID', name: 'AdminID', width: 45, sortable: true, align: 'center' }, { display: '操作类型', name: 'OType', width: 60, sortable: true, align: 'center' }, { display: '操作描述', name: 'OInfo', width: 240, sortable: true, align: 'left' }, { display: '操作信息', name: 'OMsg', width: 320, sortable: true, align: 'left' }, { display: '操作时间', name: 'CreateTime', width: 115, sortable: true, align: 'center' }, { display: '操作人账号', name: 'AdminName', width: 120, sortable: true, align: 'center' }, { display: '操作人昵称', name: 'NickName', width: 120, sortable: true, align: 'center' }, { display: '操作记录ID', name: 'PKID', width: 80, sortable: true, align: 'center' }, { display: '操作对象', name: 'FormObject', width: 200, sortable: true, align: 'center' }";
            //为有带查询字符串的页面访问，即查询的是审核操作记录
            var strFormObject = GetQueryString("FormObject");
            var strPKID = GetQueryString("PKID");
            if (strFormObject != null) {
                $(".SearchDiv").hide();
                $("#divOperation").hide();
                gridHeight = 360;
                bShowCheckBox = false;
                strExtParam = "{ name: 'meth', value: 'GetOperator' }, { name: 'FormObject', value:'" + strFormObject + "' }, { name: 'PKID', value:'" + strPKID + "' }";
                strColModel = "{display: '序号', name: 'rownum', toggle: false, hide: true, iskey: true, width: 1, align: 'center' }, { display: '编号', name: 'OperateID', width: 60,  sortable: true,align: 'center' }, { display: '管理员ID', name: 'AdminID', width: 45, sortable: true, toggle: false, hide: true,align: 'center' }, { display: '操作类型', name: 'OType', width: 60, sortable: true, align: 'center' }, { display: '操作描述', name: 'OInfo', width: 240, sortable: true, align: 'center' }, { display: '操作信息', name: 'OMsg', width: 320, sortable: true, toggle: false, hide: true,align: 'center' }, { display: '操作时间', name: 'CreateTime', width: 115, sortable: true, align: 'center' }, { display: '操作人账号', name: 'AdminName', width: 120, sortable: true, align: 'center' }, { display: '操作人昵称', name: 'NickName', width: 120, sortable: true, align: 'center' }, { display: '操作记录ID', name: 'PKID', width: 80, sortable: true, toggle: false, hide: true,align: 'center' }, { display: '操作对象', name: 'FormObject', width: 200, sortable: true, toggle: false, hide: true, align: 'center' }";
            }
            //初始操作日志代码表格
            $("#OperationsLogTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/Sys/OperationsLogHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: eval("[" + strExtParam + "]"), //扩展参数
                colModel: eval("[" + strColModel + "]"),
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "操作日志",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: bShowCheckBox,
                selectedonclick: true,
                singleselected: true,
                rowbinddata: true
            });

            //初始化查询条件收起张开事件
            InitExpandSearch("OperationsLogTable", "btnSearchOpenClose", "tableSearch", 205, 145);

            //查询
            $("#btnSearch").click(function () {
                Search();
                return false;
            });

            $("#btnDetail").click(function () {
                var rows = GetSelectedRow("OperationsLogTable", "操作");
                if (!rows) {
                    return false;
                }

                var id = rows[0][1];
                $("#txtmsg").val(rows[0][5]);

                dialogTitle = "操作详细";
                $.dialog({
                    id: 'divDetail',
                    padding: 0,
                    content: $("#divDetail").get(0),
                    title: dialogTitle,
                    ok: false,
                    cancel: true
                });
                return false;
            });
        });
    </script>
    <%--查询--%>
    <script type="text/javascript">
        function Search() {
            var txtType = $("#txtType").val();
            switch (txtType) {
                case "增加":
                    txtType = "1";
                    break;
                case "修改":
                    txtType = "2";
                    break;
                case "删除":
                    txtType = "3";
                    break;
            }
            var txtBeginTime = $("#txtBeginTime").val();
            var txtFinishTime = $("#txtFinishTime").val();
            var OperatePerson = $("#OperatePerson").val();
            var FormObject = $("#txtFormObject").val();
            var PKID = $("#txtPKID").val();
            var p = { extParam: [   //扩展参数
                        {name: "meth", value: "GetOperator" },
                        { name: "txtType", value: txtType },
                        { name: "txtBeginTime", value: txtBeginTime },
                        { name: "txtFinishTime", value: txtFinishTime },
                        { name: "OperatePerson", value: OperatePerson },
                        { name: "FormObject", value: FormObject },
                        { name: "PKID", value: PKID }
                        ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#OperationsLogTable").flexOptions(p).flexReload();
        }
    </script>
    <style type="text/css">
        .tbSearch tr td
        {
            padding-left: 10px;
            line-height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="SearchDiv">
            <table id="tableSearch">
                <tr>
                    <td style="text-align: right;" id="tdOperatePerson" runat="server" visible="false">
                        <div class="spandiv">
                            操作人：</div>
                    </td>
                    <td>
                        <asp:TextBox CssClass="txt" ID="OperatePerson" runat="server" Width="80px" Visible="false"
                            onkeyup="javascript:checkFieldLength('OperatePerson',32);"></asp:TextBox>m
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            操作类型：</div>
                    </td>
                    <td>
                        <select id="txtType" class="txt" style="width: 100px">
                            <option value="">--请选择--</option>
                            <option value="1">增加</option>
                            <option value="2">修改</option>
                            <option value="3">删除</option>
                        </select>
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            操作记录ID：</div>
                    </td>
                    <td>
                        <asp:TextBox CssClass="txt OnlyInt" ID="txtPKID" runat="server" Width="40px" onkeyup="javascript:checkFieldLength('txtPKID',32);"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            操作对象：</div>
                    </td>
                    <td>
                        <asp:TextBox CssClass="txt" ID="txtFormObject" runat="server" Width="80px" onkeyup="javascript:checkFieldLength('txtFormObject',64);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            开始时间：</div>
                    </td>
                    <td>
                        <input class="txt" id="txtBeginTime" type="text" style="width: 70px;" onclick="WdatePicker()"
                            runat="server" />
                        <img alt="" onclick="WdatePicker({el:'txtBeginTime'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                            width="16" height="22" align="absmiddle" />
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            结束时间：</div>
                    </td>
                    <td>
                        <input class="txt" id="txtFinishTime" type="text" style="width: 70px;" onclick="WdatePicker()" />
                        <img alt="" onclick="WdatePicker({el:'txtFinishTime'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                            width="16" height="22" align="absmiddle" />
                    </td>
                    <td align="left" colspan="4">
                        <asp:Button CssClass="btnHigh" ID="btnSearch" runat="server" Text="查询" />
                        <input name="重置" type="reset" class="btnHigh" value="重置" id="btnReset" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 34px" id="divOperation">
            <div class="OperationDiv" style="float: left;">
                <input id="btnDetail" type="button" value="详细信息" class="btnHigh" />
            </div>
            <div style="float: right; width: 46px; padding: 10px 5px 5px 5px;">
                <asp:LinkButton ID="btnSearchOpenClose" runat="server" Font-Size="13px" Text="收起△"
                    ForeColor="Blue"></asp:LinkButton>
            </div>
        </div>
        <div class="ListTable">
            <table id="OperationsLogTable" style="display: none;" runat="server">
            </table>
        </div>
        <div id="divDetail" style="display: none">
            <textarea id="txtmsg" cols="105" rows="20">        
        </textarea>
        </div>
    </div>
    </form>
</body>
</html>
