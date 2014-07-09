<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLog.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Sys.SystemLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 系统日志</title>
    <%--Css Ref --%>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <%--JS Ref --%>
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
            var gridHeight = document.documentElement.clientHeight - 204;
            //初始化查询条件收起张开事件
            InitExpandSearch("SystemLogTable", "btnSearchOpenClose","tableSearch", 204, 144);

            //初始系统日志代码表格
            $("#SystemLogTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '../../Handle/Sys/SystemLogHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetSystemLog" }, { name: "txtBeginTime", value: $("#txtBeginTime").val() }, { name: "SelLevel", value: $("#SelLevel").val()}], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   {display: '序号', name: 'RowNum', toggle: true, hide: true, iskey: true, width: 1, align: 'center' },
                  { display: '编号', name: 'LogID', toggle: true, hide: false, width: 100, align: 'center' },
	    				{ display: '创建时间', name: 'CreateTime', width: 140, sortable: true, align: 'center' },
                        { display: '日志级别', name: 'LogLevel', width: 100, sortable: true, align: 'center' },
	    				{ display: '日志描述', name: 'Msg', width: 300, sortable: true, align: 'left' },
	    				{ display: '线程名', name: 'LogThread', width: 40, sortable: true, align: 'center' },
                        { display: '错误信息', name: 'Exception', width: 300, sortable: true, align: 'left' },
                        { display: '日志对象', name: 'Logger', width: 200, sortable: true, align: 'center' }

	    			],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "系统日志",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: true,
                selectedonclick: true,
                singleselected: true,
                rowbinddata: true
            });
            //查询按钮
            $("#btnSearch").click(function () {
                Search();
                return false;
            });
            //详细按钮
            $("#btnDetail").click(function () {
                var rows = GetSelectedRow("SystemLogTable", "系统日志");
                if (!rows) {
                    return false;
                }

                var id = rows[0][0];
                $("#txtException").val(rows[0][6]);
                $("#txtmsg").val(rows[0][4]);
                dialogTitle = "异常详细";
                $.dialog({
                    id: 'dialogException',
                    padding: 0,
                    content: $("#divException").get(0),
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
            var txtThreadName = $("#txtThreadName").val();
            var txtBeginTime = $("#txtBeginTime").val();
            var txtFinishTime = $("#txtFinishTime").val();
            var SelLevel = $("#SelLevel").val();
            var txtDescribe = $("#txtDescribe").val();
            var txtObject = $("#txtObject").val();
            var p = { extParam: [   //扩展参数
                        {name: "meth", value: "GetSystemLog" },
                        { name: "txtThreadName", value: txtThreadName },
                        { name: "txtBeginTime", value: txtBeginTime },
                        { name: "txtFinishTime", value: txtFinishTime },
                        { name: "SelLevel", value: SelLevel },
                        { name: "txtDescribe", value: txtDescribe },
                        { name: "txtObject", value: txtObject }
                        ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#SystemLogTable").flexOptions(p).flexReload();
        }
    </script>
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
    <div>
        <div class="SearchDiv">
            <table class="tbSearch" id="tableSearch">
                <tr>
                    <td>
                        <div class="spandiv">
                            日志对象：</div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtObject" class="txt" runat="server" Style="width: 90px" onkeyup="javascript:checkFieldLength('txtObject',512);"></asp:TextBox>m
                    </td>
                    <td>
                        <div class="spandiv">
                            日志描述：</div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescribe" class="txt" runat="server" Style="width: 100px" onkeyup="javascript:checkFieldLength('txtDescribe',4000);"></asp:TextBox>m
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            线程名：</div>
                    </td>
                    <td>
                        <input id="txtThreadName" class="txt" style="width: 70px" onkeyup="javascript:checkFieldLength('txtThreadName',64);" />m
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            日志级别：</div>
                    </td>
                    <td>
                        <select id="SelLevel" class="txt" style="width: 110px">
                            <option value="">--请选择--</option>
                            <option value="Debug">调试信息</option>
                            <option value="Info">运行信息</option>
                            <option value="Error">一般错误信息</option>
                            <option value="Warn">重要错误信息</option>
                            <option value="Fatal" selected="selected">致命错误信息</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            开始时间：</div>
                    </td>
                    <td>
                        <input class="txt" id="txtBeginTime" type="text" style="width: 70px" onclick="WdatePicker()"
                            runat="server">
                        <img alt="" onclick="WdatePicker({el:'txtBeginTime'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                            width="16" height="22" align="absmiddle" />
                    </td>
                    <td style="text-align: right;">
                        <div class="spandiv">
                            结束时间：</div>
                    </td>
                    <td>
                        <input class="txt" id="txtFinishTime" type="text" style="width: 70px" onclick="WdatePicker()" />
                        <img alt="" onclick="WdatePicker({el:'txtBeginTime'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                            width="16" height="22" align="absmiddle" />
                    </td>
                    <td align="left" colspan="4">
                        <asp:Button runat="server" Text="查询" CssClass="btnHigh" ID="btnSearch" />
                        <input name="重置" type="reset" class="btnHigh" value="重置" id="btnReset" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 26px" id="divOperation">
            <div class="OperationDiv" style="float: left;">
                <asp:Button runat="server" Text="详细" CssClass="btnHigh" ID="btnDetail" />
            </div>
            <div style="float: right; width: 46px; padding: 10px 5px 5px 5px;">
                <asp:LinkButton ID="btnSearchOpenClose" runat="server" Font-Size="13px" Text="收起△"
                    ForeColor="Blue"></asp:LinkButton>
            </div>
        </div>
        <div style="margin: 10px">
            <table id="SystemLogTable" style="display: none;">
            </table>
        </div>
    </div>
    <div id="divException" class="dialogDiv">
        <table>
            <tr>
                <td>
                    <textarea id="txtmsg" cols="105" rows="10"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <textarea id="txtException" cols="105" rows="10"></textarea>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
