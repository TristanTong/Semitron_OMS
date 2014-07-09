<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrandManager.aspx.cs" Inherits="Semitron_OMS.UI.Admin.OMS.BrandManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 品牌管理</title>
    <%--CSS Ref--%>
    <link rel="stylesheet" type="text/css" href="/Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/fancybox.css" />
    <link rel="stylesheet" href="/Styles/custom.css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
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
            //初始品牌编码代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/BrandHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetBrand" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }, { name: "AvailFlag", value: $("#sltAvailFlag").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                        { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                        { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '是否有效', name: 'AvailFlag', width: 60, sortable: true, align: 'center' },
                        { display: '品牌名称', name: 'BrandName', width: 160, sortable: true, align: 'left' },
                        { display: '品牌编码', name: 'Code', width: 60, sortable: true, align: 'left' },
                        { display: '排序编号', name: 'SK', width: 60, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '创建人', name: 'CreateUser', width: 60, sortable: true, align: 'center' },
                        { display: '最后修改时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' },
                        { display: '最后修改人', name: 'UpdateUser', width: 60, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "品牌列表",
                showTableToggleBtn: true,
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
                var obj = GetSelectedRow("FlexigridTable", "品牌编码");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前品牌编码记录已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetBrandById(id)
                AddOrEdit(id, "Edit");
                return false;
            });
            //删除
            $("#btnDelete").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "品牌编码");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前品牌编码已无效，请勿重复操作。");
                    return false;
                }
                var id = obj[0][1];
                DelBrand(id);
                return false;
            });
        });
    </script>
    <%--查询、新增、编辑、删除品牌编码记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var BrandName = $("#txtBrandName").val();
            var AvailFlag = $("#sltAvailFlag").val();

            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetBrand" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "BrandName", value: BrandName },
                  { name: "AvailFlag", value: AvailFlag }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }

        //根据Id获得品牌编码记录
        function GetBrandById(id) {
            var url = "/Handle/OMS/BrandHandle.ashx";
            var data = { "meth": "GetBrandById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtBrandNameE").val(json.BrandName);
                    $("#txtCodeE").val(json.Code);
                    $("#txtSKE").val(json.SK);
                    return false;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看品牌编码
        function AddOrEdit(id, type) {
            var dialogTitle = "品牌管理>>新增品牌";
            if (type == "Edit") {
                dialogTitle = "品牌管理>>编辑品牌"
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
                        var url = "/Handle/OMS/BrandHandle.ashx";
                        var data = { "meth": "GenerateCode" };
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
                                return true;
                            }
                        };
                        JsAjax(url, data, successFun, errorFun);

                    }
                },
                ok: function () {
                    var BrandName = Trim($("#txtBrandNameE").val());
                    var Code = Trim($("#txtCodeE").val());
                    var SK = Trim($("#txtSKE").val());

                    if (BrandName == "") {
                        artDialog.tips("品牌名称不能为空!");
                        return false;
                    }
                    if (Code == "") {
                        artDialog.tips("品牌编码不能为空!");
                        return false;
                    }

                    var url = "/Handle/OMS/BrandHandle.ashx";
                    //新增品牌编码
                    var data = { "meth": "AddBrand", "BrandName": BrandName, "Code": Code, "SK": SK };
                    //编辑品牌编码
                    if (id && id != "") {
                        data = { "meth": "EditBrand", "BrandName": BrandName, "Code": Code, "SK": SK, "Id": id };
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

        //删除品牌编码
        function DelBrand(id) {
            art.dialog.confirm('品牌删除后将无法恢复，你确认要删除吗？', function () {
                //调用删除品牌编码方法
                var url = "/Handle/OMS/BrandHandle.ashx";
                var data = { "meth": "DelBrand", "Id": id };
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
                            <div id="divBrandName" style="float: left;">
                                <div class="spandiv">
                                    品牌名称：
                                </div>
                                <input style="width: 120px;" id="txtBrandName" class="txt  " runat="server" maxlength="128" />m
                            </div>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <div id="divStartTime" style="float: left;">
                                <div class="spandiv">
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
            <!--新增/编辑/查看品牌编码对话框-->
            <div id="divEdit">
                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">品牌名称：
                        </td>
                        <td class="tdLeft tdRemarkWidth ">
                            <input type="text" id="txtBrandNameE" class="txt   txtdis" maxlength="128" />
                        </td>
                        <td class="tdRight tdParamDWidth">品牌编码：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCodeE" class="txt   " maxlength="128" />
                        </td>
                        <td></td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">排序编号：
                        </td>
                        <td class="tdLeft tdRemarkWidth" colspan="3">
                            <input type="text" id="txtSKE" class="txt   " maxlength="16" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
