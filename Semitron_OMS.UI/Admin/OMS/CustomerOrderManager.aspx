<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrderManager.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.OMS.CustomerOrderManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 客户订单管理</title>
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
    <script src="/Scripts/Utilities/Guid.js" type="text/javascript"></script>
    <script src="/Scripts/Utilities/UserControl.js" type="text/javascript"></script>
    <%--CSS Customize--%>
    <style type="text/css">
        /*编辑对话框*/
        .tdParamDWidth {
            width: 120px;
        }

        .tdRemarkWidth {
            width: 220px;
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

        .divAttachment {
            height: 25px;
            float: left;
        }

        #divUpload {
            text-decoration: underline;
            float: left;
            cursor: pointer;
        }

        .aFile {
            height: 25px;
            float: left;
            padding-left: 20px;
            font-weight: bold;
        }
    </style>
    <%--JS Customize--%>
    <%--初始化，绑定事件--%>
    <script type="text/javascript">
        $(function () {
            parent.document.title = document.title;
            var gridHeight = document.documentElement.clientHeight - 224;
            //初始化查询条件收起张开事件
            InitExpandSearch("FlexigridTable", "btnSearchOpenClose", "tableSearch", 224, 144);
            InitExpandSearch("FlexiTableDetail", "btnSearchOpenCloseDetail", "tableOrderInfo", 437, 293);
            //初始客户订单代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/CustomerOrderHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetCustomerOrder" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                  { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '内部订单号', name: 'InnerOrderNO', width: 100, sortable: true, align: 'center' },
                        { display: '状态', name: 'State', width: 100, sortable: true, align: 'center' },
	    				{ display: '订单日期', name: 'CustOrderDate', width: 65, sortable: true, align: 'center' },
                        { display: '出货日期', name: 'ShipmentDate', width: 65, sortable: true, align: 'center' },
	    				{ display: '客户名称', name: 'CustomerName', width: 160, sortable: true, align: 'left' },
                        { display: '客户订单号', name: 'CustomerOrderNO', width: 100, sortable: true, align: 'left' },
                        { display: '客户采购', name: 'CustomerBuyer', width: 60, sortable: true, align: 'left' },
                        { display: '公司抬头', name: 'CompanyName', width: 160, sortable: true, align: 'left' },
                        { display: '公司销售', name: 'InnerSalesMan', width: 60, sortable: true, align: 'left' },
                        { display: '指定公司采购', name: 'AssignToInnerBuyer', width: 80, sortable: true, align: 'left' },
                        { display: '客户付款方式', name: 'PaymentType', width: 200, sortable: true, align: 'left' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '更新时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "客户订单列表",
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
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                if (obj[0][3] == "已出库") {
                    artDialog.tips("当前客户订单状态为已出库，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetCustomerOrderById(id, "Edit")
                AddOrEdit(id, "Edit");
                return false;
            });
            //查看
            $("#btnView").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetCustomerOrderById(id, "View")
                AddOrEdit(id, "View");
                return false;
            });
            //取消按钮
            $("#btnCancel").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前客户订单已取消，请勿重复操作。");
                    return false;
                }
                if (obj[0][3] == "已出货") {
                    artDialog.tips("当前客户订单已出货，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                CancelCustomerOrder(id);
                return false;
            });
            //销售审核按钮
            $("#btnConfirmFirst").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                if (obj[0][3] != "待销售审核") {
                    artDialog.tips("当前客户订单状态不为待销售审核状态，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                $("#spanInnerOrderNO").text(obj[0][2]);
                $("#spanCustomerOrderNO").text(obj[0][7]);
                $("#spanCustName").text(obj[0][6]);
                $("#spanCustDate").text(obj[0][4]);
                $("#spanCorporation").text(obj[0][9]);
                GetCustomerOrderById(id, "Confirm");
                BuyerManagerChecked(id);
                return false;
            });
            //指定公司采购按钮
            $("#btnAssignBuyer").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                if (obj[0][3] != "销售审核通过") {
                    artDialog.tips("当前客户订单状态不为销售审核通过，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                $("#spanInnerOrderNO").text(obj[0][2]);
                $("#spanCustomerOrderNO").text(obj[0][7]);
                $("#spanCustName").text(obj[0][6]);
                $("#spanCustDate").text(obj[0][4]);
                $("#spanCorporation").text(obj[0][9]);
                GetCustomerOrderById(id, "Assign");
                AssignBuyer(id);
                return false;
            });

            //出货
            $("#btnOutStock").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                //                if (obj[0][3] == "已出货") {
                //                    artDialog.tips("当前客户订单已出货，请勿重复操作。");
                //                    return false;
                //                }
                var id = obj[0][1];
                CustomerOrderOutStock(id);
                return false;
            });
            //新增明细按钮
            $("#btnAddDetail").click(function () {
                AddOrEditDetail("", "Add");
                return false;
            });
            //编辑明细按钮
            $("#btnEditDetail").click(function () {
                var obj = GetSelectedRow("FlexiTableDetail", "产品清单");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetCustomerOrderDetailById(id)
                AddOrEditDetail(id, "Edit");
                return false;
            });
            $("#divViewDetail").click(function () {
                var obj = GetSelectedRow("FlexiTableDetail", "产品清单");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetCustomerOrderDetailById(id)
                AddOrEditDetail(id, "View");
                return false;
            });

            //删除明细按钮
            $("#btnDeleteDetail").click(function () {
                var obj = GetSelectedRow("FlexiTableDetail", "产品清单");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "已取消") {
                    artDialog.tips("当前客户订单已取消，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                DelCustomerOrderDetail(id);
                return false;
            });
            //上传附件
            $("#divUpload").click(function () {
                $("#frameImportFile").attr("src", "/Admin/UserControl/UploadifyPage.aspx?ButtonText=Browse&FileExt=*.*;&FileDesc=File Formart(.*)&Folder=CustomerFilePath&IsAutoDisappear=true&IsMulti=true");
                PopUploadControlDialog("客户订单管理>>上传附件");
                return false;
            });
            //计算标准卖价
            $("#txtSaleRealPriceE,#txtSaleExchangeRateE").blur(function () {
                var value = parseFloat($("#txtSaleRealPriceE").val()) / parseFloat($("#txtSaleExchangeRateE").val());
                var strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtSalePriceE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));

                value = parseFloat($("#txtSalePriceE").val()) * parseInt($("#txtCustQuantityE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtSalesTotalFeeE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));
                return false;
            });
            //选择历史订单
            $("#btnHistoryOrder").click(function () {
                var CustomerOrderNO = Trim($("#txtCustomerOrderNOE").val());
                if (CustomerOrderNO == "") {
                    artDialog.tips("请先输入客户订单号！");
                    return;
                }
                ShowHistoryOrder(CustomerOrderNO);
            });
        });
    </script>
    <%--查询、新增、编辑、查看订单记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var InnerOrderNO = $("#txtInnerOrderNO").val();
            var CustomerOrderNO = $("#txtCustomerOrderNO").val();
            var CustomerID = $("#sltCustomerID").val();
            var State = $("#sltState").val();
            var PaymentTypeID = $("#sltPaymentTypeID").val();
            var InnerSalesMan = $("#txtInnerSalesMan").val();
            var CustomerBuyer = $("#txtCustomerBuyer").val();
            var AssignToInnerBuyer = $("#sltAssignToInnerBuyer").val();

            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetCustomerOrder" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "InnerOrderNO", value: InnerOrderNO },
                  { name: "CustomerOrderNO", value: CustomerOrderNO },
                  { name: "CustomerID", value: CustomerID },
                  { name: "State", value: State },
                  { name: "PaymentTypeID", value: PaymentTypeID },
                  { name: "InnerSalesMan", value: InnerSalesMan },
                  { name: "CustomerBuyer", value: CustomerBuyer },
                  { name: "AssignToInnerBuyer", value: AssignToInnerBuyer }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        } //end Search()

        //根据Id获得订单记录
        function GetCustomerOrderById(id, type) {
            var url = "/Handle/OMS/CustomerOrderHandle.ashx";
            var data = { "meth": "GetCustomerOrderById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtInnerOrderNOE").val(json.InnerOrderNO);
                    $("#txtCustomerOrderNOE").val(json.CustomerOrderNO);
                    $("#sltCustomerIDE").val(json.CustomerID);
                    $("#sltStateE").val(json.State);
                    var orderDate = json.CustOrderDate;
                    if (orderDate && orderDate.length > 10) {
                        orderDate = orderDate.substr(0, 10);
                        if (orderDate == "0001-01-01") {
                            orderDate = "";
                        }
                    }
                    else {
                        orderDate = "";
                    }
                    $("#txtCustOrderDateE").val(orderDate);
                    $("#txtCustomerBuyerE").val(json.CustomerBuyer);
                    $("#txtInnerSalesManE").val(json.InnerSalesMan);
                    $("#sltPaymentTypeIDE").val(json.PaymentTypeID);
                    $("#sltCorporationIDE").val(json.CorporationID);
                    $("#sltAssignToInnerBuyerE").val(json.AssignToInnerBuyer);
                    var urlPaths = json.AttachmentFiles.replace(/\*/g, "/");
                    $(".divAttachment").append(GetFileDivHtml(urlPaths));
                    if (type == "Edit") {
                        $(".cancelFile").show();
                    }
                    else {
                        $(".cancelFile").hide();
                    }
                    //初始化明细表格
                    InitLoadFlexiTableDetail(json.InnerOrderNO);
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        } //GetCustomerOrderById

        //新增、编辑、查看客户订单
        function AddOrEdit(id, type) {
            var dialogTitle = "新增客户订单";
            if (type == "Edit") {
                dialogTitle = "编辑客户订单"
            }
            if (type == "View") {
                dialogTitle = "查看客户订单"
            }
            dialogTitle = "客户订单管理>>" + dialogTitle;
            var gridWidth = document.documentElement.clientWidth - 60;
            var gridHeight = document.documentElement.clientHeight - 105;
            $.dialog({
                id: 'divEdit',
                title: dialogTitle,
                width: gridWidth,
                height: gridHeight,
                top: 0,
                padding: 5,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        //生成内部订单号
                        var url = "/Handle/OMS/CustomerOrderHandle.ashx";
                        //新增客户订单
                        var data = { "meth": "GenerateInnerOrderNO" };
                        var errorFun = function (x, e) {
                            alert(x.responseText);
                        };
                        var successFun = function (json) {
                            if (!json || json.State == 0) {
                                artDialog.alert(json.Info);
                                art.dialog.list["divEdit"].close();
                                return false;
                            } else {
                                $("#sltStateE,#spanStateE,#divViewDetail,#spanAssignToInnerBuyerE,#sltAssignToInnerBuyerE").hide();
                                $("#divDetailOper,#divUpload").show();
                                $("#divEdit .txt").each(function () {
                                    $(this).removeAttr("disabled");
                                });
                                $("#divEdit .txt").each(function () {
                                    $(this).val("");
                                });
                                $("#txtInnerOrderNOE").val(json.Remark);
                                $("#txtInnerOrderNOE").attr("disabled", "disabled");
                                //初始化明细表格
                                InitLoadFlexiTableDetail($("#txtInnerOrderNOE").val());
                                $("#sltSaleStandardCurrencyE").val("1");
                                $("#sltSaleStandardCurrencyE").attr("disabled", "disabled");
                                AssignRolePermission();
                                return true;
                            }
                        };
                        JsAjax(url, data, successFun, errorFun);
                    }
                    if (type == "Edit") {
                        $("#sltStateE,#spanStateE,#divDetailOper,#divUpload").show();
                        $("#divViewDetail,#spanAssignToInnerBuyerE,#sltAssignToInnerBuyerE").hide();
                        $("#divEdit .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $("#txtCustomerOrderNOE").attr("disabled", "disabled");
                        $("#sltCustomerIDE").attr("disabled", "disabled");
                        $("#sltStateE").attr("disabled", "disabled");
                        $("#sltSaleStandardCurrencyE").attr("disabled", "disabled");
                        AssignRolePermission();
                    }

                    if (type == "View") {
                        $("#divEdit .txt").each(function () {
                            $(this).attr("disabled", "disabled");
                        });
                        $("#sltStateE,#spanStateE,#spanAssignToInnerBuyerE,#sltAssignToInnerBuyerE").show();
                        $("#divDetailOper,#divUpload").hide();
                        $("#divViewDetail").show();
                    }
                    $("#txtInnerOrderNOE").attr("disabled", "disabled");
                    //清除所有附件
                    $(".divAttachment").empty();
                },
                ok: function () {
                    if (type == "View") {
                        return true;
                    }
                    var InnerOrderNO = Trim($("#txtInnerOrderNOE").val());
                    var CustomerOrderNO = Trim($("#txtCustomerOrderNOE").val());
                    var CustomerID = Trim($("#sltCustomerIDE").val());
                    var CustOrderDate = Trim($("#txtCustOrderDateE").val());
                    var CustomerBuyer = Trim($("#txtCustomerBuyerE").val());
                    var InnerSalesMan = Trim($("#txtInnerSalesManE").val());
                    var PaymentTypeID = Trim($("#sltPaymentTypeIDE").val());
                    var CorporationID = Trim($("#sltCorporationIDE").val());
                    var AssignToInnerBuyer = Trim($("#sltAssignToInnerBuyerE").val());

                    if (InnerOrderNO == "") {
                        artDialog.tips("内部订单号不能为空!");
                        return false;
                    }
                    if (CustomerOrderNO == "") {
                        artDialog.tips("客户订单号不能为空!");
                        return false;
                    }
                    if (CustomerID == "") {
                        artDialog.tips("客户名称不能为空!");
                        return false;
                    }
                    //if (AssignToInnerBuyer == "") {
                    //    artDialog.tips("指定公司采购不能为空!");
                    //    return false;
                    //}

                    //取得所有上传的附件列表，文件URL,以'|'分隔
                    var AttachmentFiles = "";
                    $(".aFile").each(function () {
                        AttachmentFiles += $(this).find("a").attr("href") + "|";
                    });

                    var url = "/Handle/OMS/CustomerOrderHandle.ashx";
                    //新增客户订单
                    var data = { "meth": "AddCustomerOrder", "InnerOrderNO": InnerOrderNO, "CustomerOrderNO": CustomerOrderNO, "CustomerID": CustomerID, "CustOrderDate": CustOrderDate, "CustomerBuyer": CustomerBuyer, "InnerSalesMan": InnerSalesMan, "PaymentTypeID": PaymentTypeID, "CorporationID": CorporationID, "AttachmentFiles": AttachmentFiles, "AssignToInnerBuyer": AssignToInnerBuyer };
                    //编辑客户订单
                    if (id && id != "") {
                        data = { "meth": "EditCustomerOrder", "InnerOrderNO": InnerOrderNO, "CustomerOrderNO": CustomerOrderNO, "CustomerID": CustomerID, "CustOrderDate": CustOrderDate, "CustomerBuyer": CustomerBuyer, "InnerSalesMan": InnerSalesMan, "PaymentTypeID": PaymentTypeID, "CorporationID": CorporationID, "AttachmentFiles": AttachmentFiles, "AssignToInnerBuyer": AssignToInnerBuyer, "Id": id };
                    }
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    var successFun = function (json) {
                        if (json.State == "0") {
                            artDialog.alert(json.Info);
                            return false;
                        } else {
                            art.dialog.list["divEdit"].close();
                            artDialog.tips(json.Info);
                            $("#FlexigridTable").flexReload();
                            return true;
                        }
                    };
                    JsAjax(url, data, successFun, errorFun);
                    return false;
                },
                cancel: true
            });
        } //AddOrEdit

        function AssignRolePermission() {
            var strAInfo = $("#hfAdminInfo").val();
            var arrInfo = strAInfo.split("$");
            if (arrInfo.length > 0) {
                $("#txtInnerSalesManE").val(arrInfo[0]);
            }
            if (arrInfo.length > 1) {

            }
            if (arrInfo.length > 2) {
                if (arrInfo[2].indexOf(',2,') > -1) {
                    //是超级管理员可编辑
                    $("#txtInnerSalesManE").removeAttr("disabled");
                }
                else {
                    $("#txtInnerSalesManE").attr("disabled", "disabled");
                }
            }
        }

        //取消客户订单
        function CancelCustomerOrder(id) {
            art.dialog.confirm('你确认要取消吗？', function () {
                //调用取消客户订单方法
                var url = "/Handle/OMS/CustomerOrderHandle.ashx";
                var data = { "meth": "CancelCustomerOrder", "Id": id };
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
        } //CancelCustomerOrder
    </script>
    <%--选择历史订单--%>
    <script type="text/javascript">
        //显示客户历史订单
        function ShowHistoryOrder(CustomerOrderNO) {

        }
    </script>

    <%--销售审核、出货--%>
    <script type="text/javascript">
        //加载产品销售分析列表
        function InitConfirmFirst(coId) {
            var gridWidth = document.documentElement.clientWidth - 60;
            var gridHeight = document.documentElement.clientHeight - 225;
            $("#tbConfirmFirst").flexigrid({
                width: gridWidth, //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/CustomerOrderHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetConfirmFirstData" }, { name: "COId", value: coId }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: true, iskey: true, width: 40, align: 'right' },
                        { display: '毛利润(USD)', name: 'GrossProfits', width: 80, toggle: false, hide: false, align: 'right' },
                        { display: '利润率', name: 'ProfitMargin', width: 60, toggle: false, hide: false, align: 'right' },
                        { display: '客户型号', name: 'CPN', toggle: false, hide: true, width: 80, align: 'left' },
	    				{ display: '厂商型号', name: 'MPN', width: 80, sortable: true, align: 'left' },
                        { display: '订单数量', name: 'CustQuantity', width: 80, sortable: true, align: 'right' },
                        { display: '实际卖货币', name: 'SaleRealCurrency', width: 80, sortable: true, align: 'center' },
                        { display: '卖汇率', name: 'SaleExchangeRate', width: 80, sortable: true, align: 'right' },
                        { display: '实际卖价', name: 'SaleRealPrice', width: 80, sortable: true, align: 'right' },
                        { display: '标准卖价(USD)', name: 'SalePrice', width: 100, sortable: true, align: 'right' },
	    				{ display: '标准售价总金额(USD)', name: 'SaleStandardTotal', width: 120, sortable: true, align: 'right' },
                        { display: '采购订单号', name: 'PONO', width: 100, sortable: true, align: 'center' },
                        { display: '供应商', name: 'SupplierName', width: 160, sortable: true, align: 'left' },
                        { display: '采购数量', name: 'POQuantity', width: 80, sortable: true, align: 'right' },
                        { display: '实际买货币', name: 'BuyRealCurrency', width: 90, sortable: true, align: 'center' },
                        { display: '买汇率', name: 'BuyExchangeRate', width: 80, sortable: true, align: 'right' },
                        { display: '实际买价', name: 'BuyRealPrice', width: 80, sortable: true, align: 'right' },
                        { display: '标准买价(USD)', name: 'BuyPrice', width: 100, sortable: true, align: 'right' },
                        { display: '标准买货总成本(USD)', name: 'BuyCost', width: 100, sortable: true, align: 'right' }
                ],
                sortname: "PONO",
                sortorder: "ASC",
                title: "产品销售分析列表",
                usepager: true,
                useRp: true,
                rp: 1000,
                rowbinddata: true,
                showcheckbox: false,
                selectedonclick: true,
                singleselected: false,
                rowbinddata: true
            });
        }

        //销售审核
        function BuyerManagerChecked(coId) {
            $.dialog({
                id: 'divConfirmFirst',
                title: '客户订单管理>>销售审核',
                width: 'auto',
                height: 'auto',
                padding: 5,
                top: 0,
                left: 0,
                content: $('#divEdit').get(0),
                init: function () {
                    $("#divEdit .txt").each(function () {
                        $(this).attr("disabled", "disabled");
                    });
                    $("#sltStateE,#spanStateE").show();
                    $("#divDetailOper,#divUpload,#spanAssignToInnerBuyerE,#sltAssignToInnerBuyerE").hide();
                    $("#divViewDetail").show();

                    $("#txtInnerOrderNOE").attr("disabled", "disabled");
                    //清除所有附件
                    $(".divAttachment").empty();
                },
                button: [
                {
                    name: '同意',
                    callback: function () {
                        DoConfirmFirst(coId, 1);
                        return true;
                    }
                },
                {
                    name: '否决',
                    callback: function () {
                        DoConfirmFirst(coId, 0);
                        return true;
                    },
                    disabled: false
                },
                {
                    name: '取消',
                    callback: function () {
                        art.dialog.tips('取消操作');
                    },
                    disabled: false,
                    focus: true
                }
                ]
            });
        }

        //执行审核操作 type 1:同意 0:否决
        function DoConfirmFirst(coId, type) {
            //调用方法
            var url = "/Handle/OMS/CustomerOrderHandle.ashx";
            var data = { "meth": "ConfirmFirst", "COId": coId, "Type": type };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    artDialog.tips(json.Info);
                    $("#FlexigridTable").flexReload(); //刷新表格
                    art.dialog.list["divEdit"].close();
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //指定公司采购
        function AssignBuyer(coId) {
            $.dialog({
                id: 'divConfirmFirst',
                title: '客户订单管理>>指定公司采购',
                width: 'auto',
                height: 'auto',
                padding: 5,
                top: 0,
                left: 0,
                content: $('#divEdit').get(0),
                init: function () {
                    $("#divEdit .txt").each(function () {
                        $(this).attr("disabled", "disabled");
                    });
                    $("#sltStateE,#spanStateE").show();
                    $("#divDetailOper,#divUpload").hide();
                    $("#divViewDetail,#spanAssignToInnerBuyerE,#sltAssignToInnerBuyerE").show();

                    $("#txtInnerOrderNOE").attr("disabled", "disabled");
                    //清除所有附件
                    $(".divAttachment").empty();
                    $("#spanAssignToInnerBuyerE,#sltAssignToInnerBuyerE").removeAttr("disabled");
                },
                ok: function () {
                    var AssignToInnerBuyer = $("#sltAssignToInnerBuyerE").val();
                    //调用方法
                    var url = "/Handle/OMS/CustomerOrderHandle.ashx";
                    var data = { "meth": "AssignBuyer", "COId": coId, "AssignToInnerBuyer": AssignToInnerBuyer };
                    var successFun = function (json) {
                        if (json.State == "0") {
                            artDialog.alert(json.Info);
                            return false;
                        } else {
                            artDialog.tips(json.Info);
                            $("#FlexigridTable").flexReload(); //刷新表格
                            art.dialog.list["divEdit"].close();
                            return true;
                        }
                    };
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    JsAjax(url, data, successFun, errorFun);
                    return true;
                },
                cancel: true
            });
        }

        //出货确认
        function CustomerOrderOutStock(id) {
            var dialogTitle = "客户订单管理>>客户订单>>出货";
            $.dialog({
                id: 'divEditShipmentDate',
                title: dialogTitle,
                width: 300,
                height: 100,
                lock: true,
                padding: 5,
                content: $('#divEditShipmentDate').get(0),
                init: function () {
                    $("#txtShipmentDateE").val("");
                },
                ok: function () {
                    var ShipmentDate = Trim($("#txtShipmentDateE").val());
                    if (ShipmentDate == "") {
                        artDialog.alert("出货日期不能为空");
                        return false;
                    }

                    art.dialog.confirm('您确认将此客户订单已完成采购，QC确认完成并进行出货操作吗？', function () {
                        //调用删除客户订单方法
                        var url = "/Handle/OMS/CustomerOrderHandle.ashx";
                        var data = { "meth": "CustomerOrderOutStock", "Id": id, "ShipmentDate": ShipmentDate };
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
                },
                cancel: true
            });
        } //CustomerOrderOutStock       
    </script>
    <%--加载、新增、编辑、删除订单明细--%>
    <script type="text/javascript">
        //加载订单明细（产品清单）列表
        function LoadFlexiTableDetail(InnerOrderNO, w, h) {
            $("#FlexiTableDetail").flexigrid({
                width: w, //表格宽度
                height: h, //表格高度
                url: '/Handle/OMS/CustomerOrderDetailHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetCustomerOrderDetail" },
                    { name: "InnerOrderNO", value: InnerOrderNO },
                    { name: "AvailFlag", value: "1" }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                  { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '内部订单号', name: 'InnerOrderNO', width: 100, sortable: true, align: 'center' },
                        { display: '客户型号', name: 'CPN', width: 100, sortable: true, align: 'left' },
	    				{ display: '厂家标准型号', name: 'MPN', width: 100, sortable: true, align: 'left' },
	    				{ display: '品牌名称', name: 'MFG', width: 100, sortable: true, align: 'left' },
                        { display: '生产年份', name: 'DC', width: 100, sortable: true, align: 'center' },
                        { display: '是否环保', name: 'ROHS', width: 60, sortable: true, align: 'center' },
                        { display: '要求交期', name: 'CRD', width: 60, sortable: true, align: 'center' },
                        { display: '数量', name: 'CustQuantity', width: 60, sortable: true, align: 'left' },
                        { display: '卖汇率', name: 'SaleExchangeRate', width: 60, sortable: true, align: 'left' },
                         { display: '实际卖货币', name: 'SaleRealCurrency', width: 60, sortable: true, align: 'left' },
                        { display: '实际卖价', name: 'SaleRealPrice', width: 60, sortable: true, align: 'left' },
                        { display: '标准卖货币', name: 'SaleStandardCurrency', width: 80, sortable: true, align: 'center' },
                        { display: '卖价', name: 'SalePrice', width: 60, sortable: true, align: 'left' },
                        { display: '其他费用', name: 'OtherFee', width: 100, sortable: true, align: 'center' },
                        { display: '其他费用备注', name: 'OtherFeeRemark', width: 100, sortable: true, align: 'left' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '更新时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "产品清单列表",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: true,
                selectedonclick: true,
                singleselected: true,
                rowbinddata: true
            });
        } // LoadFlexiTableDetail
        //查询订单明细
        function SearchFlexiTableDetail(InnerOrderNO) {
            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetCustomerOrderDetail" },
                  { name: "InnerOrderNO", value: InnerOrderNO }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexiTableDetail").flexOptions(p).flexReload();
        }

        //初始化加载明细表格
        function InitLoadFlexiTableDetail(InnerOrderNO) {
            var options = $("#FlexiTableDetail").GetOptions();
            if (options == null) {
                var w = document.documentElement.clientWidth - 100;
                var h = document.documentElement.clientHeight - 450;
                LoadFlexiTableDetail(InnerOrderNO, w, h);
            }
            else {
                SearchFlexiTableDetail(InnerOrderNO);
            }
        }

        //根据Id获得订单记录
        function GetCustomerOrderDetailById(id) {
            var url = "/Handle/OMS/CustomerOrderDetailHandle.ashx";
            var data = { "meth": "GetCustomerOrderDetailById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtInnerOrderNOEDetail").val(json.InnerOrderNO);
                    $("#txtCPNE").val(json.CPN);
                    $("#txtMPNE").val(json.MPN);
                    $("#sltMFGE").val(json.MFG);
                    $("#txtDCE").val(json.DC);
                    var cRD = json.CRD;
                    if (cRD && cRD.length > 10) {
                        cRD = cRD.substr(0, 10);
                        if (cRD == "0001-01-01") {
                            cRD = "";
                        }
                    }
                    else {
                        cRD = "";
                    }
                    $("#txtCRDE").val(cRD);
                    $("#txtCustQuantityE").val(json.CustQuantity);
                    $("#sltROHSE").val(json.ROHS);
                    $("#txtSaleExchangeRateE").val(json.SaleExchangeRate);
                    $("#sltSaleRealCurrencyE").val(json.SaleRealCurrency);
                    $("#txtSaleRealPriceE").val(json.SaleRealPrice);
                    $("#sltSaleStandardCurrencyE").val(json.SaleStandardCurrency);
                    $("#txtSalePriceE").val(json.SalePrice);

                    var value = json.SalePrice * json.CustQuantity;
                    var strvalue = value.toString();
                    if (strvalue == "NaN") strvalue = "0";
                    $("#txtSalesTotalFeeE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));

                    $("#txtOtherFeeE").val(json.OtherFee);
                    $("#txtOtherFeeRemarkE").val(json.OtherFeeRemark);
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看产品清单记录
        function AddOrEditDetail(id, type) {
            var dialogTitle = "新增产品清单记录";
            if (type == "Edit") {
                dialogTitle = "编辑产品清单记录"
            }
            if (type == "View") {
                dialogTitle = "查看产品清单记录"
            }
            dialogTitle = "客户订单管理>>客户订单>>" + dialogTitle;
            $.dialog({
                id: 'divEditDetail',
                title: dialogTitle,
                width: 600,
                height: 150,
                lock: true,
                padding: 5,
                content: $('#divEditDetail').get(0),
                init: function () {
                    if (type == "Add") {
                        $("#divEditDetail .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $("#divEditDetail .txt").each(function () {
                            $(this).val("");
                        });
                        $("#txtInnerOrderNOEDetail").val($("#txtInnerOrderNOE").val());
                        $("#sltSaleStandardCurrencyE").val("1");
                    }
                    if (type == "Edit") {
                        $("#divEditDetail .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $("#txtCPNE").attr("disabled", "disabled");
                    }
                    if (type == "View") {
                        $("#divEditDetail .txt").each(function () {
                            $(this).attr("disabled", "disabled");
                        });
                    }
                    $("#txtInnerOrderNOEDetail").attr("disabled", "disabled");
                    $("#sltSaleStandardCurrencyE").attr("disabled", "disabled");
                },
                ok: function () {
                    if (type == "View") {
                        return true;
                    }
                    var InnerOrderNO = Trim($("#txtInnerOrderNOEDetail").val());
                    var CPN = Trim($("#txtCPNE").val());
                    var MPN = Trim($("#txtMPNE").val());
                    var MFG = Trim($("#sltMFGE").val());
                    var DC = Trim($("#txtDCE").val());
                    var CRD = Trim($("#txtCRDE").val());
                    var CustQuantity = Trim($("#txtCustQuantityE").val());
                    var ROHS = Trim($("#sltROHSE").val());
                    var SaleExchangeRate = Trim($("#txtSaleExchangeRateE").val());
                    var SaleRealCurrency = Trim($("#sltSaleRealCurrencyE").val());
                    var SaleRealPrice = Trim($("#txtSaleRealPriceE").val());
                    var SaleStandardCurrency = Trim($("#sltSaleStandardCurrencyE").val());
                    var SalePrice = Trim($("#txtSalePriceE").val());
                    var OtherFee = Trim($("#txtOtherFeeE").val());
                    var OtherFeeRemark = Trim($("#txtOtherFeeRemarkE").val());

                    if (InnerOrderNO == "") {
                        artDialog.tips("内部订单号不能为空!");
                        return false;
                    }
                    if (CPN == "") {
                        artDialog.tips("客户型号不能为空!");
                        return false;
                    }
                    if (MFG == "") {
                        artDialog.tips("厂家标准型号不能为空!");
                        return false;
                    }

                    var url = "/Handle/OMS/CustomerOrderDetailHandle.ashx";
                    //新增产品清单记录
                    var data = { "meth": "AddCustomerOrderDetail", "InnerOrderNO": InnerOrderNO, "CPN": CPN, "MPN": MPN, "MFG": MFG, "DC": DC, "CRD": CRD, "CustQuantity": CustQuantity, "ROHS": ROHS, "SaleExchangeRate": SaleExchangeRate, "SaleRealCurrency": SaleRealCurrency, "SaleRealPrice": SaleRealPrice, "SaleStandardCurrency": SaleStandardCurrency, "SalePrice": SalePrice, "OtherFee": OtherFee, "OtherFeeRemark": OtherFeeRemark };
                    //编辑产品清单记录
                    if (id && id != "") {
                        data = { "meth": "EditCustomerOrderDetail", "InnerOrderNO": InnerOrderNO, "CPN": CPN, "MPN": MPN, "MFG": MFG, "DC": DC, "CRD": CRD, "CustQuantity": CustQuantity, "ROHS": ROHS, "SaleExchangeRate": SaleExchangeRate, "SaleRealCurrency": SaleRealCurrency, "SaleRealPrice": SaleRealPrice, "SaleStandardCurrency": SaleStandardCurrency, "SalePrice": SalePrice, "OtherFee": OtherFee, "OtherFeeRemark": OtherFeeRemark, "Id": id };
                    }
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    var successFun = function (json) {
                        if (json.State == "0") {
                            artDialog.alert(json.Info);
                            return false;
                        } else {
                            art.dialog.list["divEditDetail"].close();
                            artDialog.tips(json.Info);
                            $("#FlexiTableDetail").flexReload();
                            return true;
                        }
                    };
                    JsAjax(url, data, successFun, errorFun);
                    return false;
                },
                cancel: true
            });
        }

        //删除产品清单记录
        function DelCustomerOrderDetail(id) {
            art.dialog.confirm('记录删除后将无法恢复，你确认要删除吗？', function () {
                //调用删除产品清单记录方法
                var url = "/Handle/OMS/CustomerOrderDetailHandle.ashx";
                var data = { "meth": "DelCustomerOrderDetail", "Id": id };
                var successFun = function (json) {
                    if (json.State == "0") {
                        artDialog.alert(json.Info);
                        return false;
                    } else {
                        //刷新表格
                        artDialog.tips(json.Info);
                        $("#FlexiTableDetail").flexReload();
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
<body style="padding: 0px;">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfAdminInfo" runat="server" />
        <div>
            <div class="SearchDiv">
                <table style="width: 100%" id="tableSearch">
                    <tr>
                        <td style="text-align: right; line-height: 18px;">
                            <div id="divInnerOrderNO" style="float: left;">
                                <div class="spandivAdjust">
                                    内部订单号：
                                </div>
                                <input style="width: 110px;" id="txtInnerOrderNO" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divCustomerOrderNO" style="float: left;">
                                <div class="spandivAdjust">
                                    客户订单号：
                                </div>
                                <input style="width: 110px;" id="txtCustomerOrderNO" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divCustomerID" style="float: left;">
                                <div class="spandivAdjust">
                                    客户名称：
                                </div>
                                <select id="sltCustomerID" class="txt" runat="server" style="width: 125px;">
                                </select>
                            </div>
                            <div id="divState" style="float: left;">
                                <div class="spandivAdjust">
                                    状态：
                                </div>
                                <select id="sltState" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>

                            <div id="divCustomerBuyer" style="float: left;">
                                <div class="spandivAdjust">
                                    客户采购：
                                </div>
                                <input style="width: 110px;" id="txtCustomerBuyer" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divInnerSalesMan" style="float: left;">
                                <div class="spandivAdjust">
                                    公司销售：
                                </div>
                                <input style="width: 110px;" id="txtInnerSalesMan" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divAssignToInnerBuyer" style="float: left;">
                                <div class="spandivAdjust">
                                    指定公司采购：
                                </div>
                                <select id="sltAssignToInnerBuyer" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divPaymentTypeID" style="float: left;">
                                <div class="spandivAdjust">
                                    付款方式：
                                </div>
                                <select id="sltPaymentTypeID" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divStartTime" style="float: left; width: 1024px;">
                                <div class="spandivAdjust" style="float: left;">
                                    开始时间：
                                </div>
                                <div style="float: left;">
                                    <input class="txt" runat="server" id="txtBeginTime" type="text" style="width: 120px"
                                        onclick="WdatePicker({ startDate: '%y-%M-%d 00:00:00', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })" />
                                    <img alt="" onclick="WdatePicker({el:'txtBeginTime',startDate:'%y-%M-%d 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                        src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                        align="absmiddle" />
                                </div>
                                <div class="spandiv" style="float: left;">
                                    结束时间：
                                </div>
                                <div style="float: left;">
                                    <input class="txt" id="txtEndTime" type="text" style="width: 120px" onclick="WdatePicker({ startDate: '%y-%M-%d 23:59:59', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })"
                                        runat="server" />
                                    <img alt="" onclick="WdatePicker({el:'txtEndTime',startDate:'%y-%M-%d 23:59:59',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                        src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                        align="absmiddle" />
                                </div>
                                <div style="float: left;">
                                    <select class="txt" id="sltTimeType" runat="server" style="width: 80px">
                                        <option value="0" title="订单日期">订单日期</option>
                                        <option value="1" title="创建时间" selected="selected">创建时间</option>
                                        <option value="2" title="更新时间">更新时间</option>
                                    </select>
                                    <asp:Button CssClass="btnHigh" ID="btnSearch" runat="server" Text="查询" />
                                    <input name="重置" type="reset" class="btnHigh" value="重置" id="btnReset" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 34px" id="divOperation">
                <div class="OperationDiv" style="float: left;">
                    <asp:Button runat="server" Text="新增" CssClass="btnHigh" ID="btnAdd" />
                    <asp:Button runat="server" Text="编辑" CssClass="btnHigh" ID="btnEdit" />
                    <asp:Button runat="server" Text="取消" CssClass="btnHigh" ID="btnCancel" />
                    <asp:Button runat="server" Text="销售审核" CssClass="btnHigh" ID="btnConfirmFirst" />
                    <asp:Button runat="server" Text="指定公司采购" CssClass="btnHigh" ID="btnAssignBuyer" />
                    <%-- <asp:Button runat="server" Text="出货" CssClass="btnHigh" ID="btnOutStock" />--%>
                    <asp:Button runat="server" Text="查看" CssClass="btnHigh" ID="btnView" />
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
            <div class="dialogDiv">
                <%--新增/编辑/查看客户订单对话框--%>
                <div id="divEdit">
                    <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                        <tr id="trSelect">
                            <td class="tdRight tdParamDWidth">内部订单号：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtInnerOrderNOE" class="txt " maxlength="16" style="width: 100px" />
                                <label class="red">
                                    *</label>
                            </td>
                            <td class="tdRight tdParamDWidth">客户订单号：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtCustomerOrderNOE" class="txt " maxlength="16" style="width: 100px" />
                                <label class="red">
                                    *</label>
                            </td>
                            <td class="tdRight tdParamDWidth">
                                <%--<asp:Button runat="server" Text="选择历史订单" CssClass="btnHigh" ID="btnHistoryOrder" />--%>

                            </td>
                            <td class="tdLeft tdRemarkWidth"></td>
                            <td></td>
                        </tr>
                    </table>
                    <table style="width: 100%" border="0" cellspacing="0" cellpadding="0" id="tableOrderInfo">
                        <tbody>
                            <tr>
                                <td class="tdLeft tdHead" colspan="7">订单信息
                                </td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">客户名称：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltCustomerIDE" runat="server" style="width: 220px">
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth">订单日期：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input class="txt" id="txtCustOrderDateE" type="text" onclick="WdatePicker()" style="width: 85px" />
                                    <img alt="" onclick="WdatePicker({el:'txtCustOrderDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                        width="16" height="22" align="absmiddle" />
                                </td>
                                <td class="tdRight tdParamDWidth">客户采购：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtCustomerBuyerE" class="txt " maxlength="32" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">付款方式：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltPaymentTypeIDE" runat="server" style="width: 220px">
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth">公司抬头：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltCorporationIDE" runat="server" style="width: 220px">
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth">公司销售：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtInnerSalesManE" class="txt " maxlength="32" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">
                                    <span id="spanStateE">订单状态：</span>
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltStateE" runat="server" style="width: 105px">
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth"><span id="spanAssignToInnerBuyerE">指定公司采购：</span>
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltAssignToInnerBuyerE" runat="server" style="width: 220px">
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth"></td>
                                <td class="tdLeft tdRemarkWidth"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="tdLeft tdHead" colspan="7">附件信息
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="padding-top: 5px;">
                                    <div id="divUpload">
                                        上传附件
                                    </div>
                                    <div class="divAttachment">
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr class="trMR">
                                <td class="tdLeft tdHead" colspan="7">产品清单
                                </td>
                            </tr>
                            <tr class="trMR">
                                <td class="tdLeft" colspan="7">
                                    <div style="float: left;" id="divDetailOper">
                                        <div class="OperationDiv" style="float: left;">
                                            <asp:Button runat="server" Text="新增" CssClass="btnHigh" ID="btnAddDetail" />
                                            <asp:Button runat="server" Text="编辑" CssClass="btnHigh" ID="btnEditDetail" />
                                            <asp:Button runat="server" Text="取消" CssClass="btnHigh" ID="btnDeleteDetail" />
                                        </div>
                                    </div>
                                    <div style="float: left; display: none;" id="divViewDetail">
                                        <div class="OperationDiv" style="float: left;">
                                            <asp:Button runat="server" Text="查看" CssClass="btnHigh" ID="btnViewDetail" />
                                        </div>
                                    </div>
                                    <div style="float: right; width: 46px; padding: 5px 5px 0px 5px;">
                                        <asp:LinkButton ID="btnSearchOpenCloseDetail" runat="server" Font-Size="13px" Text="收起△"
                                            ForeColor="Blue"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <div class="ListTable">
                                        <table id="FlexiTableDetail" style="display: none;">
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <%--上传附件--%>
                <div id="divImportFile" style="display: none;">
                    <iframe id="frameImportFile" src="" frameborder="0" scrolling="no" style="width: 475px; height: 360px;"></iframe>
                    <br />
                    <span class="red">温馨提示：附件支持多文件上传，请点击Upload图标选择本地文件。单个文件最大5M。</span>
                </div>
                <%--新增/编辑/查看产品清单记录对话框--%>
                <div id="divEditDetail">
                    <table>
                        <tr id="tr1">
                            <td colspan="5" style="height: 20px">
                                <div id="divInnerOrderNOEDetail" runat="server">
                                    内部订单号：
                                <input type="text" id="txtInnerOrderNOEDetail" class="txt " maxlength="16" style="width: 100px" />
                                    <label class="red">
                                        *</label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%" border="0" cellspacing="0" cellpadding="0" id="table1">
                        <tbody>
                            <tr>
                                <td class="tdLeft tdHead" colspan="5">产品清单记录
                                </td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">客户型号：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtCPNE" class="txt " maxlength="32" />
                                </td>
                                <td class="tdRight tdParamDWidth">厂家标准型号：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtMPNE" class="txt " maxlength="32" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">品牌名称：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltMFGE" runat="server" style="width: 105px">
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth">生产年份：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtDCE" class="txt " maxlength="32" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">要求交期：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input class="txt" id="txtCRDE" type="text" onclick="WdatePicker()" style="width: 85px" />
                                    <img alt="" onclick="WdatePicker({el:'txtCRDE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                        width="16" height="22" align="absmiddle" />
                                </td>
                                <td class="tdRight tdParamDWidth">数量：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtCustQuantityE" class="txt OnlyInt tdRight" maxlength="9" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">是否环保：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select id="sltROHSE" class="txt" style="width: 105px">
                                        <option value="true" selected="selected">是</option>
                                        <option value="false">否</option>
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth">实际卖货币：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltSaleRealCurrencyE" runat="server" style="width: 105px">
                                        <option value="">--请选择--</option>
                                    </select>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">实际卖价：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtSaleRealPriceE" class="txt OnlyFloat tdRight" maxlength="14" />
                                </td>
                                <td class="tdRight tdParamDWidth">卖汇率：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtSaleExchangeRateE" class="txt OnlyFloat tdRight" maxlength="14" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">标准卖货币：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <select class="txt" id="sltSaleStandardCurrencyE" runat="server" style="width: 105px">
                                        <option value="">--请选择--</option>
                                    </select>
                                </td>
                                <td class="tdRight tdParamDWidth">标准卖价：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtSalePriceE" class="txt OnlyFloat tdRight" maxlength="14" />
                                </td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">标准销售总额：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtSalesTotalFeeE" class="txt OnlyFloat tdRight" maxlength="32" />
                                </td>
                                <td class="tdRight tdParamDWidth"></td>
                                <td class="tdLeft tdRemarkWidth"></td>
                                <td></td>
                            </tr>
                            <tr class="trMO">
                                <td class="tdRight tdParamDWidth">其他费用(USD)：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtOtherFeeE" class="txt OnlyFloat tdRight" maxlength="32" />
                                </td>
                                <td class="tdRight tdParamDWidth">其他费用备注：
                                </td>
                                <td class="tdLeft tdRemarkWidth">
                                    <input type="text" id="txtOtherFeeRemarkE" class="txt " maxlength="256" />
                                </td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <%--销售审核--%>
                <div id="divConfirmFirst">
                    <table>
                        <tr>
                            <td>
                                <div id="div5" style="float: left; width: 160px;">
                                    内部订单号： <span id="spanInnerOrderNO" style="text-align: left"></span>
                                </div>
                                <div id="div1" style="float: left; width: 200px;">
                                    客户订单号： <span id="spanCustomerOrderNO" style="text-align: left"></span>
                                </div>
                                <div id="div4" style="float: left; width: 250px;">
                                    客户名称： <span id="spanCustName" style="text-align: left"></span>
                                </div>
                                <div id="div3" style="float: left; width: 160px;">
                                    订单日期： <span id="spanCustDate" style="text-align: left"></span>
                                </div>
                                <div id="div2" style="float: left; width: 250px;">
                                    公司抬头： <span id="spanCorporation" style="text-align: left"></span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="tbConfirmFirst" style="float: left;">
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--出货--%>
                <div id="divEditShipmentDate">
                    <table>
                        <tr>
                            <td class="tdRight tdParamDWidth">出货日期：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input class="txt" id="txtShipmentDateE" type="text" onclick="WdatePicker()" style="width: 85px" />
                                <img alt="" onclick="WdatePicker({el:'txtShipmentDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                    width="16" height="22" align="absmiddle" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
