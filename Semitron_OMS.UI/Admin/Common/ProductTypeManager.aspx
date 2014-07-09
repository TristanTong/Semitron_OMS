﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductTypeManager.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Common.ProductTypeManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>网站后台系统 - 产品类型管理</title>
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
        .tdParamDWidth {
            width: 150px;
        }

        .tdRemarkWidth {
            width: 90px;
        }

        .backYellow {
            background: #FFFF00;
        }

        .tdCenter {
            text-align: center;
        }

        .tdLeft {
            text-align: left;
        }

        .tdRight {
            text-align: right;
        }

        .tdHead {
            background: #00B0F0;
            font-weight: bold;
            height: 25px;
        }

        .trMO {
            line-height: 28px;
        }

            .trMO input {
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
            //初始产品类型代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/Common/ProductTypeHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetProductType" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }, { name: "AvailFlag", value: $("#sltAvailFlag").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                        { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                        { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '是否有效', name: 'AvailFlag', width: 60, sortable: true, align: 'center' },
                        { display: '分类名称', name: 'Name', width: 200, sortable: true, align: 'left' },
                        { display: '编码', name: 'Code', width: 60, sortable: true, align: 'center' },
                        { display: '语言', name: 'Lang', width: 60, sortable: true, align: 'center' },
                        { display: '排序编号', name: 'SK', width: 60, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '创建人', name: 'CreateUser', width: 60, sortable: true, align: 'center' },
                        { display: '最后修改时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' },
                        { display: '最后修改人', name: 'UpdateUser', width: 60, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "产品类型列表",
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
                var obj = GetSelectedRow("FlexigridTable", "产品类型");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前产品类型记录已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetProductTypeById(id)
                AddOrEdit(id, "Edit");
                return false;
            });
            //删除
            $("#btnDelete").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "产品类型");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前产品类型已无效，请勿重复操作。");
                    return false;
                }
                var id = obj[0][1];
                DelProductType(id);
                return false;
            });
        });
    </script>
    <%--查询、新增、编辑、删除产品类型记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var Code = $("#txtCode").val();
            var Name = $("#txtName").val();
            var AvailFlag = $("#sltAvailFlag").val();
            var Lang = $("#sltLang").val();

            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetProductType" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "Code", value: Code },
                  { name: "Name", value: Name },
                  { name: "AvailFlag", value: AvailFlag },
                  { name: "Lang", value: Lang }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }

        //根据Id获得产品类型记录
        function GetProductTypeById(id) {
            var url = "/Handle/Common/ProductTypeHandle.ashx";
            var data = { "meth": "GetProductTypeById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtCodeE").val(json.Code);
                    $("#txtNameE").val(json.TypeName);
                    $("#txtSKE").val(json.SK);
                    $("#sltLangE").val(json.Lang);
                    return false;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看产品类型
        function AddOrEdit(id, type) {
            var dialogTitle = "新增产品类型";
            if (type == "Edit") {
                dialogTitle = "编辑产品类型"
            }

            $.dialog({
                id: 'divEdit',
                title: dialogTitle,
                width: 450,
                height: 100,
                padding: 10,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        var url = "/Handle/Common/ProductTypeHandle.ashx";
                        var data = { "meth": "GenerateCodeAndPath" };
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
                                $("#txtCodeE").attr("disabled", "disabled");
                                $("#divEdit .txt").each(function () {
                                    $(this).val("");
                                });
                                $("#txtCodeE").val(json.Info);
                                $("#sltLangE").val("cn");
                                return true;
                            }
                        };
                        JsAjax(url, data, successFun, errorFun);
                    }
                    if (type == "Edit") {
                        $("#txtCodeE").attr("disabled", "disabled");
                    }
                },
                ok: function () {
                    var Code = Trim($("#txtCodeE").val());
                    var Name = Trim($("#txtNameE").val());
                    var SK = Trim($("#txtSKE").val());

                    var Lang = Trim($("#sltLangE").val());

                    var isPass = true;
                    $("#divEdit .txt").each(function () {
                        if ($(this).val() == "") {
                            isPass = false;
                            return;
                        }
                    });
                    if (!isPass) {
                        artDialog.tips("所有输入项不能为空!");
                        return false;
                    }


                    var url = "/Handle/Common/ProductTypeHandle.ashx";
                    //新增产品类型
                    var data = { "meth": "AddProductType", "Code": Code, "Name": Name, "SK": SK, "Lang": Lang };
                    //编辑产品类型
                    if (id && id != "") {
                        data = { "meth": "EditProductType", "Code": Code, "Name": Name, "SK": SK, "Lang": Lang, "Id": id };
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

        //删除产品类型
        function DelProductType(id) {
            art.dialog.confirm('产品类型删除后将无法恢复，你确认要删除吗？', function () {
                //调用删除产品类型方法
                var url = "/Handle/Common/ProductTypeHandle.ashx";
                var data = { "meth": "DelProductType", "Id": id };
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="SearchDiv">
                <table style="width: 100%" id="tableSearch">
                    <tr>
                        <td style="text-align: right;">
                            <div id="divCode" style="float: left;">
                                <div class="spandiv">
                                    编码：
                                </div>
                                <input style="width: 120px;" id="txtCode" class="txt  " runat="server" maxlength="16" />m
                            </div>
                            <div id="divName" style="float: left;">
                                <div class="spandiv">
                                    标题：
                                </div>
                                <input style="width: 120px;" id="txtName" class="txt  " runat="server" maxlength="128" />m
                            </div>
                            <div id="divLang" style="float: left;">
                                <div class="spandivAdjust">
                                    语言：
                                </div>
                                <select id="sltLang" class="txt " runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="cn">中文</option>
                                    <option value="en">English</option>
                                </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <div id="divAvailFlag" style="float: left;">
                                <div class="spandiv">
                                    是否有效：
                                </div>
                                <select id="sltAvailFlag" class="txt " runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="1" selected="selected">有效</option>
                                    <option value="0">无效</option>
                                </select>
                            </div>
                            <div id="divStartTime" style="float: left;">
                                <div class="spandivAdjust">
                                    开始时间：
                                </div>
                                <input class="txt  " runat="server" id="txtBeginTime" type="text" style="width: 120px"
                                    onclick="WdatePicker({ startDate: '%y-%M-%d 00:00:00', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })" />
                                <img alt="" onclick="WdatePicker({el:'txtBeginTime',startDate:'%y-%M-%d 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                    src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                    align="absmiddle" />
                            </div>
                            <div id="tdTimeBtn" style="float: left;">
                                <div id="divEndTime" style="float: left;">
                                    <div class="spandiv">
                                        结束时间：
                                    </div>
                                    <input class="txt  " id="txtEndTime" type="text" style="width: 120px" onclick="WdatePicker({ startDate: '%y-%M-%d 23:59:59', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })"
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
            <!--新增/编辑/查看产品类型对话框-->
            <div id="divEdit">
                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">编码：
                        </td>
                        <td class="tdLeft tdRemarkWidth ">
                            <input type="text" id="txtCodeE" class="txt   txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">标题：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtNameE" class="txt   " maxlength="128" />
                        </td>
                        <td></td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">排序编号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSKE" class="txt   " maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">语言：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltLangE" runat="server" style="width: 85px">
                                <option value="cn">中文</option>
                                <option value="en" selected="selected">English</option>
                            </select>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
