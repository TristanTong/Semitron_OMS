<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierManager.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.CRM.SupplierManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 供应商管理</title>
    <%--CSS Ref--%>
    <link rel="stylesheet" type="text/css" href="/Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/fancybox.css" />
    <link rel="stylesheet" href="/Styles/custom.css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Scripts/kindeditor-4.1.7/themes/default/default.css" />
    <link rel="stylesheet" href="/Scripts/kindeditor-4.1.7/plugins/code/prettify.css" />
    <%--JS Ref--%>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
    <%--CSS Customize--%>
    <style type="text/css">
        /*编辑对话框*/
        .tdParamDWidth
        {
            width: 150px;
        }
        
        .tdRemarkWidth
        {
            width: 90px;
        }
        
        .backYellow
        {
            background: #FFFF00;
        }
        
        .tdCenter
        {
            text-align: center;
        }
        
        .tdLeft
        {
            text-align: left;
        }
        
        .tdRight
        {
            text-align: right;
        }
        
        .tdHead
        {
            background: #00B0F0;
            font-weight: bold;
            height: 25px;
        }
        
        .trMO
        {
            line-height: 28px;
        }
        
        .trMO input
        {
            width: 140px;
        }
    </style>
    <%--JS Customize--%>
    <%--初始化，绑定事件--%>
    <script type="text/javascript">
        $(function () {
            parent.document.title = document.title;
            var gridHeight = document.documentElement.clientHeight - 204;
            //初始化查询条件收起张开事件
            InitExpandSearch("FlexigridTable", "btnSearchOpenClose", "tableSearch", 204, 144);
            //初始供应商代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/CRM/SupplierHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetSupplier" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }, { name: "AvailFlag", value: $("#sltAvailFlag").val()}], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                        {display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                        { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '是否有效', name: 'AvailFlag', width: 60, sortable: true, align: 'center' },
                        { display: '供应商名称', name: 'SupplierName', width: 200, sortable: true, align: 'left' },
                        { display: '供应商编码', name: 'SCode', width: 60, sortable: true, align: 'center' },
                        { display: '联系电话', name: 'Tel', width: 100, sortable: true, align: 'left' },
                        { display: '联系人', name: 'ContactPerson', width: 60, sortable: true, align: 'left' },
                        { display: '排序编号', name: 'SK', width: 60, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '创建人', name: 'CreateUser', width: 60, sortable: true, align: 'center' },
                        { display: '最后修改时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' },
                        { display: '最后修改人', name: 'UpdateUser', width: 60, sortable: true, align: 'center' }
	    			],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "供应商列表",
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
            //新增按钮
            $("#btnAdd").click(function () {
                AddOrEdit("", "Add");
                return false;
            });
            //编辑按钮
            $("#btnEdit").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "供应商");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前供应商记录已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetSupplierById(id)
                AddOrEdit(id, "Edit");
                return false;
            });
            //删除
            $("#btnDelete").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "供应商");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前供应商已无效，请勿重复操作。");
                    return false;
                }
                var id = obj[0][1];
                DelSupplier(id);
                return false;
            });
            //关联供应商
            $("#btnBindInnerBuyer").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "供应商");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前供应商已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                $("#span1").text(obj[0][4]);
                $("#span2").text(obj[0][3]);
                LoadAdminBindSupplierTable(id);
                BindOperations(id);
                return false;
            });
        });
    </script>
    <%--查询、新增、编辑、删除供应商记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var SCode = $("#txtSCode").val();
            var SupplierName = $("#txtSupplierName").val();
            var AvailFlag = $("#sltAvailFlag").val();

            var p = { extParam: [   //扩展参数
                        {name: "meth", value: "GetSupplier" },
                        { name: "TimeType", value: TimeType },
                        { name: "startTime", value: startTime },
                        { name: "endTime", value: endTime },
                        { name: "SCode", value: SCode },
                        { name: "SupplierName", value: SupplierName },
                        { name: "AvailFlag", value: AvailFlag }
                        ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }

        //根据Id获得供应商记录
        function GetSupplierById(id) {
            var url = "/Handle/CRM/SupplierHandle.ashx";
            var data = { "meth": "GetSupplierById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtSCodeE").val(json.SCode);
                    $("#txtSupplierNameE").val(json.SupplierName);
                    $("#txtSKE").val(json.SK);
                    $("#txtContactPersonE").val(json.ContactPerson);
                    $("#txtTelE").val(json.Tel);
                    $("#txtareaSupplierAddressE").val(json.SupplierAddress);
                    return false;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看供应商
        function AddOrEdit(id, type) {
            var dialogTitle = "供应商管理>>新增供应商";
            if (type == "Edit") {
                dialogTitle = "供应商管理>>编辑供应商"
            }

            $.dialog({
                id: 'divEdit',
                title: dialogTitle,
                width: 550,
                height: 100,
                padding: 10,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        var url = "/Handle/CRM/SupplierHandle.ashx";
                        var data = { "meth": "GenerateSCodeAndPath" };
                        var errorFun = function (x, e) {
                            alert(x.responseText);
                        };
                        var successFun = function (json) {
                            if (!json || json.State == 0) {
                                artDialog.alert(json.Info);
                                art.dialog.list["divEdit"].close();
                                return false;
                            } else {
                                $("#divEdit .txt").each(function () {
                                    $(this).removeAttr("disabled");
                                });
                                $("#txtSCodeE").attr("disabled", "disabled");
                                $("#divEdit .txt").each(function () {
                                    $(this).val("");
                                });
                                $("#txtSCodeE").val(json.Info);
                                $("#txtareaSupplierAddressE").val("");
                                return true;
                            }
                        };
                        JsAjax(url, data, successFun, errorFun);
                    }
                    if (type == "Edit") {
                        $("#txtSCodeE").attr("disabled", "disabled");
                    }
                },
                ok: function () {
                    var SCode = Trim($("#txtSCodeE").val());
                    var SupplierName = Trim($("#txtSupplierNameE").val());
                    var Tel = Trim($("#txtTelE").val());
                    var ContactPerson = Trim($("#txtContactPersonE").val());
                    var SupplierAddress = Trim($("#txtareaSupplierAddressE").val());
                    var SK = Trim($("#txtSKE").val());

                    //                    var isPass = true;
                    //                    $("#divEdit .txt").each(function () {
                    //                        if ($(this).val() == "s") {
                    //                            isPass = false;
                    //                            return;
                    //                        }
                    //                    });
                    if (SCode == "") {
                        artDialog.tips("供应商编码不能为空!");
                        return false;
                    }
                    if (SupplierName == "") {
                        artDialog.tips("供应商名称不能为空!");
                        return false;
                    }
                    if (SupplierAddress == "") {
                        artDialog.tips("供应商地址不能为空!");
                        return false;
                    }


                    var url = "/Handle/CRM/SupplierHandle.ashx";
                    //新增供应商
                    var data = { "meth": "AddSupplier", "SCode": SCode, "SupplierName": SupplierName, "Tel": Tel, "ContactPerson": ContactPerson, "SupplierAddress": SupplierAddress, "SK": SK };
                    //编辑供应商
                    if (id && id != "") {
                        data = { "meth": "EditSupplier", "SCode": SCode, "SupplierName": SupplierName, "Tel": Tel, "ContactPerson": ContactPerson, "SupplierAddress": SupplierAddress, "SK": SK, "Id": id };
                    }
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    var successFun = function (json) {
                        if (json.State == "0") {
                            artDialog.alert(json.Info);
                            return false;
                        } else {
                            artDialog.tips(json.Info);
                            art.dialog.list["divEdit"].close();
                            $("#FlexigridTable").flexReload();
                            return true;
                        }
                    };
                    JsAjax(url, data, successFun, errorFun);
                    return false;
                },
                cancel: true
            });
        }

        //删除供应商
        function DelSupplier(id) {
            art.dialog.confirm('供应商删除后将无法恢复，你确认要删除吗？', function () {
                //调用删除供应商方法
                var url = "/Handle/CRM/SupplierHandle.ashx";
                var data = { "meth": "DelSupplier", "Id": id };
                var successFun = function (json) {
                    if (json.State == "0") {
                        artDialog.alert(json.Info);
                        return false;
                    } else {
                        //刷新表格
                        artDialog.tips(json.Info);
                        $("#FlexigridTable").flexReload();
                        return true;
                    }
                };
                var errorFun = function (x, e) {
                    alert(x.responseText);
                };
                JsAjax(url, data, successFun, errorFun);
            }, function () {
                art.dialog.tips('取消操作');
            });
        }
    </script>
    <%--绑定采购人员--%>
    <script type="text/javascript">
        //加载采购人员表格
        function LoadAdminBindSupplierTable(CustomerId) {
            $("#AdminBindSupplierTable").flexigrid({
                width: 580, //表格宽度
                height: 250, //表格高度
                url: '/Handle/OMS/AdminBindSupplierHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetAdminBindSupplierList" }, { name: "CustomerId", value: CustomerId}], //扩展参数
                colModel: [//表格的题头与索要填充的内容。           
                {display: '序号', name: 'rownum', toggle: false, iskey: true, sortable: true, width: 60, align: 'center', hide: true },
                        { display: '是否关联', name: 'Valid', width: 50, sortable: true, align: 'center' },
                        { display: '采购人员ID', name: 'AdminID', width: 60, sortable: true, align: 'center' },
                        { display: '采购人员', name: 'Name', width: 120, sortable: true, align: 'left' },
                        { display: '类型', name: 'ServiceType', width: 100, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 115, sortable: true, align: 'center' },
                        { display: '是否选择勾选框', name: 'checked999', toggle: false, sortable: true, hide: true }
	    			],
                sortname: "AdminID",
                sortorder: "asc",
                title: "采购人员列表",
                usepager: true,
                useRp: true,
                showcheckbox: true,
                selectedonclick: true,
                singleselected: false,
                rowbinddata: true
            });
        }

        //刷新列表
        function RefreshAdminBindSupplierTable(CustomerId) {
            var p = { extParam: [{ name: "meth", value: "GetAdminBindSupplierList" }, { name: "CustomerId", value: CustomerId}] //扩展参数 
            };
            p.newp = 1;         //跳转到第一页。
            $("#AdminBindSupplierTable").flexOptions(p).flexReload();
        }

        //关联采购人员
        function BindOperations(CustomerId) {
            $.dialog({
                content: $("#divAdminBindSupplier").get(0),
                id: 'divAdminBindSupplier',
                title: "关联采购人员",
                init: function () {
                },
                ok: function () {
                    art.dialog.confirm("是否确认关联操作？", function () {
                        var obj = $("#AdminBindSupplierTable").getSelectedRows();  //获取当前页被勾选的行
                        var objAll = $("#AdminBindSupplierTable").getAllRows();  //获取当前页的所有的行
                        var IdList = "";
                        var IdListAll = "";
                        if (obj.length < 0) {
                            artDialog.tips("获取采购人员行数据异常。");
                            return false;
                        }

                        for (var i = 0; i < obj.length; i++) {
                            IdList += obj[i][2] + ",";
                        }

                        for (var i = 0; i < objAll.length; i++) {
                            IdListAll += objAll[i][2] + ",";
                        }

                        IdList = IdList.substring(0, IdList.length - 1);
                        IdListAll = IdListAll.substring(0, IdListAll.length - 1);
                        var url = "/Handle/OMS/AdminBindSupplierHandle.ashx";
                        var data = { "meth": "Bind", "IdList": IdList, "IdListAll": IdListAll, "CustomerId": CustomerId };
                        var successFun = function (json) {
                            if (json.State == 0) {
                                artDialog.alert(json.Info);
                                return false;
                            }
                            artDialog.confirm(json.Info + "是否继续关联其它采购人员？",
                             function () {
                                 RefreshAdminBindSupplierTable(CustomerId);    //刷新表格
                             },
                            function () {
                                artDialog.tips("退出关联操作。");
                                art.dialog.list["divAdminBindSupplier"].close();
                                $("#AdminBindSupplierTable").flexReload();
                            }); //end artDialog.confirm

                            return true;
                        };
                        var errorFun = function (x, e) {
                            alert(x.responseText);
                        };
                        JsAjax(url, data, successFun, errorFun);
                        return true;
                    }, function () { artDialog.tips("取消操作。"); });
                    return false;
                },
                cancel: function () {
                    artDialog.tips("取消操作。");
                    return true;
                }
            });
            RefreshAdminBindSupplierTable(CustomerId);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="SearchDiv">
            <table style="width: 100%" id="tableSearch">
                <tr>
                    <td style="text-align: right;">
                        <div id="divSCode" style="float: left;">
                            <div class="spandiv">
                                编码：</div>
                            <input style="width: 120px;" id="txtSCode" class="txt  " runat="server" maxlength="16" />m
                        </div>
                        <div id="divSupplierName" style="float: left;">
                            <div class="spandiv">
                                供应商名称：</div>
                            <input style="width: 120px;" id="txtSupplierName" class="txt  " runat="server" maxlength="128" />m
                        </div>
                        <div id="divAvailFlag" style="float: left;">
                            <div class="spandiv">
                                是否有效：</div>
                            <select id="sltAvailFlag" class="txt " runat="server" style="width: 125px">
                                <option value="">--请选择--</option>
                                <option value="1" selected="selected">有效</option>
                                <option value="0">无效</option>
                            </select>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <div id="divStartTime" style="float: left;">
                            <div class="spandiv">
                                开始时间：</div>
                            <input class="txt  " runat="server" id="txtBeginTime" type="text" style="width: 120px"
                                onclick="WdatePicker({startDate:'%y-%M-%d 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})" />
                            <img alt="" onclick="WdatePicker({el:'txtBeginTime',startDate:'%y-%M-%d 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                align="absmiddle" />
                        </div>
                        <div id="tdTimeBtn" style="float: left;">
                            <div id="divEndTime" style="float: left;">
                                <div class="spandiv">
                                    结束时间：</div>
                                <input class="txt  " id="txtEndTime" type="text" style="width: 120px" onclick="WdatePicker({startDate:'%y-%M-%d 23:59:59',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                    runat="server" />
                                <img alt="" onclick="WdatePicker({el:'txtEndTime',startDate:'%y-%M-%d 23:59:59',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                    src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                    align="absmiddle" />
                            </div>
                            <select class="txt  " id="sltTimeType" runat="server" style="width: 100px">
                                <option value="1" title="创建时间" selected="selected">创建时间</option>
                                <option value="2" title="更新时间">更新时间</option>
                            </select>
                            <asp:Button CssClass="btnHigh" ID="btnSearch" runat="server" Text="查询" />
                            <input name="重置" type="reset" class="btnHigh" value="重置" id="btnReset" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 34px" id="divOperation">
            <div class="OperationDiv" style="float: left;">
                <asp:Button runat="server" Text="新增" CssClass="btnHigh" ID="btnAdd" />
                <asp:Button runat="server" Text="编辑" CssClass="btnHigh" ID="btnEdit" />
                <asp:Button runat="server" Text="删除" CssClass="btnHigh" ID="btnDelete" />
                <asp:Button runat="server" Text="关联采购" CssClass="btnHigh" ID="btnBindInnerBuyer" />
            </div>
            <div style="float: right; width: 46px; padding: 10px 5px 5px 5px;">
                <asp:LinkButton ID="btnSearchOpenClose" runat="server" Font-Size="13px" Text="收起△"
                    ForeColor="Blue"></asp:LinkButton>
            </div>
        </div>
        <div class="ListTable">
            <table id="FlexigridTable" style="display: none;">
            </table>
        </div>
    </div>
    <div class="dialogDiv">
        <!--新增/编辑/查看供应商对话框-->
        <div id="divEdit">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        编码：
                    </td>
                    <td class="tdLeft tdRemarkWidth ">
                        <input type="text" id="txtSCodeE" class="txt   txtdis" maxlength="16" />
                    </td>
                    <td class="tdRight tdParamDWidth">
                        名称：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtSupplierNameE" class="txt   " maxlength="128" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        联系地址：
                    </td>
                    <td class="tdLeft tdRemarkWidth" colspan="3">
                        <input type="text"  id="txtareaSupplierAddressE" maxlength="1024" style="width:430px"/>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        联系人：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtContactPersonE" class="txt" maxlength="64" />
                    </td>
                    <td class="tdRight tdParamDWidth">
                        联系电话：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtTelE" class="txt   " maxlength="16" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        排序编号：
                    </td>
                    <td class="tdLeft tdRemarkWidth" colspan="3">
                        <input type="text" id="txtSKE" class="txt   " maxlength="16" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <%--关联采购--%>
        <div id="divAdminBindSupplier">
            <table>
                <tr>
                    <td>
                        <span style="font-weight: bold;">供应商编码:</span><span id="span1"></span>&nbsp;&nbsp;&nbsp;&nbsp;
                        <span style="font-weight: bold;">供应商名称:</span><span id="span2"></span>&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table id="AdminBindSupplierTable" style="display: none;">
            </table>
        </div>
    </div>
    </form>
</body>
</html>
