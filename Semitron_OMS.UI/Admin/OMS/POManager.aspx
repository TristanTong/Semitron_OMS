<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POManager.aspx.cs" Inherits="Semitron_OMS.UI.Admin.OMS.POManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 采购订单管理</title>
    <%--CSS Ref--%>
    <link rel="stylesheet" type="text/css" href="/Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/fancybox.css" />
    <link rel="stylesheet" href="/Styles/custom.css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/zTree3.5/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <%--JS Ref--%>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/zTree3.5/js/jquery.ztree.core-3.5.js" type="text/javascript"></script>
    <script src="/Scripts/zTree3.5/js/jquery.ztree.excheck-3.5.js" type="text/javascript"></script>
    <script src="/Scripts/zTree3.5/js/jquery.ztree.exedit-3.5.js" type="text/javascript"></script>
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
    <%--CSS Customize--%>
    <style type="text/css">
        /*编辑对话框*/
        .tdParamDWidth {
            width: 120px;
        }

        .tdRemarkWidth {
            width: 200px;
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
                width: 120px;
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
            //初始采购订单代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/POHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetPO" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                        { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '采购订单号', name: 'PONO', width: 100, sortable: true, align: 'center' },
                        { display: '状态', name: 'State', width: 100, sortable: true, align: 'center' },
	    				{ display: '订单日期', name: 'POOrderDate', width: 65, sortable: true, align: 'left' },
	    				{ display: '供应商', name: 'SupplierName', width: 160, sortable: true, align: 'left' },
                        { display: '公司抬头', name: 'CompanyName', width: 160, sortable: true, align: 'left' },
                        { display: '总计金额', name: 'TotalFee', width: 60, sortable: true, align: 'right' },
                        { display: '账单负责人', name: 'BillManager', width: 80, sortable: true, align: 'left' },
                        { display: '送货负责人', name: 'ShipManager', width: 80, sortable: true, align: 'left' },
                        { display: '付款条例', name: 'PaymentTerms', width: 160, sortable: true, align: 'left' },
                        { display: '交货条款', name: 'Shipping', width: 80, sortable: true, align: 'left' },
                        { display: '发起采购日期', name: 'IssuedDate', width: 80, sortable: true, align: 'center' },
                        { display: '送货日期', name: 'DeliveryDate', width: 80, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '更新时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "采购订单列表",
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
                var obj = GetSelectedRow("FlexigridTable", "采购订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前采购订单已取消，无法完成此操作。");
                    return false;
                }
                if (obj[0][3] == "已完成") {
                    artDialog.tips("当前采购订单的状态为已完成，不能编辑数据。");
                    return false;
                }

                var id = obj[0][1];
                art.dialog.confirm('采购订单编辑后将重置为待关联计划状态，流程将重新走一遍，你确认继续编辑吗？', function () {
                    GetPOById(id)
                    AddOrEdit(id, "Edit");
                });
                return false;
            });
            //查看按钮
            $("#btnView").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购订单");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];

                GetPOById(id)
                AddOrEdit(id, "View");
                return false;
            });
            //取消
            $("#btnDel").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前采购订单已取消，请勿重复操作。");
                    return false;
                }
                var id = obj[0][1];
                DelPO(id);
                return false;
            });
            //关联采购计划
            $("#btnBindPOPlan").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] != "待关联计划") {
                    artDialog.tips("当前采购订单不为待关联计划，无法完成关联采购计划操作。");
                    return false;
                }
                var id = obj[0][1];
                BindPOPlan(id, "Bind");
                return false;
            });
            //供应商审核
            $("#btnConfirmSupplier").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] != "供应商审核") {
                    artDialog.tips("当前采购订单不为供应商审核状态，无法完成供应商审核操作。");
                    return false;
                }
                var id = obj[0][1];
                ConfirmSupplier(id);
                return false;
            });

            //关联客户订单
            //$("#btnBindCustiomerOrder").click(function () {
            //    var obj = GetSelectedRow("FlexigridTable", "采购订单");
            //    if (!obj) {
            //        return false;
            //    }
            //    var id = obj[0][1];
            //    //BindCustiomerOrder(id, "Bind");
            //    return false;
            //});
            //查看客户订单
            //$("#btnViewCustiomerOrder").click(function () {
            //    var obj = GetSelectedRow("FlexigridTable", "采购订单");
            //    if (!obj) {
            //        return false;
            //    }
            //    var id = obj[0][1];
            //    BindCustiomerOrder(id, "View");
            //    return false;
            //});
            //生成采购计划
            //$("#btnGeneratePOPlan").click(function () {
            //    var obj = GetSelectedRow("FlexigridTable", "采购订单");
            //    if (!obj) {
            //        return false;
            //    }
            //    if (obj[0][3] == "已取消") {
            //        artDialog.tips("当前采购订单已取消，无法完成此操作。");
            //        return false;
            //    }

            //    $("#spanSupplier1").text(obj[0][5]);
            //    $("#spanPONO1").text(obj[0][2]);
            //    $("#spanCorporation1").text(obj[0][6]);
            //    $("#spanBuyer1").text(obj[0][8]);
            //    var id = obj[0][1];
            //    GeneratePOPlan(id, "Generate");
            //    return false;
            //});
            //查看采购计划
            $("#btnViewPOPlan").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购订单");
                if (!obj) {
                    return false;
                }

                $("#spanSupplier1").text(obj[0][5]);
                $("#spanPONO1").text(obj[0][2]);
                $("#spanCorporation1").text(obj[0][6]);
                $("#spanBuyer1").text(obj[0][8]);
                var id = obj[0][1];
                ViewPOPlan(id, "View");
                return false;
            });
            //采购审核按钮
            $("#btnConfirmSecond").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "客户订单");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前采购订单已取消，无法完成此操作。");
                    return false;
                }
                if (obj[0][3] != "待采购审核") {
                    artDialog.tips("当前采购订单状态不为待采购审核，无法完成此操作。");
                    return false;
                }

                $("#spanSupplier").text(obj[0][5]);
                $("#spanPONO").text(obj[0][2]);
                $("#spanCorporation").text(obj[0][6]);
                $("#spanBuyer").text(obj[0][8]);
                var id = obj[0][1];
                ConfirmSecond(id);
                return false;
            });
            //选择公司抬头后
            $("#sltCorporationIDE").change(function () {
                var CorporationID = $("#sltCorporationIDE").val();
                if (CorporationID == "") {
                    artDialog.tips("请选择公司抬头！");
                    return false;
                }
                FillCorporationData(CorporationID);
            });
            //选择编辑供应商时
            $("#sltSupplierIDE").change(function () {
                var SupplierID = $("#sltSupplierIDE").val();
                if (SupplierID == "") {
                    artDialog.tips("请选择供应商！");
                    return false;
                    hfAdminInfo
                }
                var url = "/Handle/CRM/SupplierHandle.ashx";
                var data = { "meth": "GetSupplierById", "Id": SupplierID };
                var successFun = function (json) {
                    if (json.State == "0") {
                        artDialog.alert(json.Info);
                        return false;
                    } else {
                        $("#txtContactPersonE").val(json.ContactPerson);
                        $("#txtTelE").val(json.Tel);
                        return false;
                    }
                };
                var errorFun = function (x, e) {
                    alert(x.responseText);
                };
                JsAjax(url, data, successFun, errorFun);
            });
        });
    </script>
    <%--查询、新增、编辑、取消采购计划记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var PONO = $("#txtPONO").val();
            var SupplierID = $("#sltSupplierID").val();
            var CorporationID = $("#sltCorporationID").val();
            var State = $("#sltState").val();

            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetPO" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "PONO", value: PONO },
                  { name: "SupplierID", value: SupplierID },
                  { name: "CorporationID", value: CorporationID },
                  { name: "State", value: State }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }
        //根据Id获得订单记录
        function GetPOById(id) {
            var url = "/Handle/OMS/POHandle.ashx";
            var data = { "meth": "GetPOById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtPONOE").val(json.PONO);
                    $("#txtInnerBuyerE").val(json.InnerBuyer);
                    $("#sltSupplierIDE").val(json.SupplierID);
                    var issuedDate = json.IssuedDate;
                    if (issuedDate && issuedDate.length > 10) {
                        issuedDate = issuedDate.substr(0, 10);
                        if (issuedDate == "0001-01-01") {
                            issuedDate = "";
                        }
                    }
                    else {
                        issuedDate = "";
                    }
                    $("#txtIssuedDateE").val(issuedDate);
                    $("#txtContactPersonE").val(json.ContactPerson);
                    $("#txtTelE").val(json.Tel);
                    $("#txtBillManagerE").val(json.BillManager);
                    $("#txtBillManagerTelE").val(json.BillManagerTel);
                    $("#sltCorporationIDE").val(json.CorporationID);
                    $("#txtareaBillToE").text(json.BillTo);
                    var deliveryDate = json.DeliveryDate;
                    if (deliveryDate && deliveryDate.length > 10) {
                        deliveryDate = deliveryDate.substr(0, 10);
                        if (deliveryDate == "0001-01-01") {
                            deliveryDate = "";
                        }
                    }
                    else {
                        deliveryDate = "";
                    }
                    $("#txtDeliveryDateE").val(deliveryDate);
                    $("#txtShipping").val(json.Shipping);
                    $("#txtShipManagerE").val(json.ShipManager);
                    $("#txtShipManagerTelE").val(json.ShipManagerTel);
                    $("#sltPaymentTermsE").val(json.PaymentTerms);
                    $("#txtareaShipToE").text(json.ShipTo);
                    $("#txtTotalFeeE").val(json.TotalFee);
                    $("#txtPOPlanNumE").val(json.POPlanNum);
                    $("#sltStateE").val(json.State);

                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看采购订单
        function AddOrEdit(id, type) {
            var dialogTitle = "新增采购订单";
            if (type == "Edit") {
                dialogTitle = "编辑采购订单"
            }
            if (type == "View") {
                dialogTitle = "查看采购订单"
            }

            $.dialog({
                id: 'divEdit',
                title: "采购订单管理>>" + dialogTitle,
                width: 600,
                height: 450,
                padding: 10,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        //生成内部订单号
                        var url = "/Handle/OMS/POHandle.ashx";
                        //新增采购订单
                        var data = { "meth": "GeneratePONO" };
                        var errorFun = function (x, e) {
                            alert(x.responseText);
                        };
                        var successFun = function (json) {
                            if (!json || json.State == 0) {
                                artDialog.alert(json.Info);
                                art.dialog.list["divEdit"].close();
                                return false;
                            } else {
                                $(".trHide").hide();
                                $("#divEdit .txt").each(function () {
                                    $(this).removeAttr("disabled");
                                });
                                $("#divEdit .txt").each(function () {
                                    $(this).val("");
                                });
                                $("#txtareaBillToE,#txtareaShipToE").text("");
                                $("#txtPONOE").val(json.Remark);
                                $("#txtPONOE").attr("disabled", "disabled");
                                $("#txtareaBillToE,#txtareaShipToE").removeAttr("disabled");
                                AssignRolePermission();
                                return true;
                            }
                        };
                        JsAjax(url, data, successFun, errorFun);
                    }
                    if (type == "Edit") {
                        $(".trHide").show();
                        $("#divEdit .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $(".trHide input").attr("disabled", "disabled");
                        $("#txtPONOE").attr("disabled", "disabled");
                        $("#sltSupplierIDE").attr("disabled", "disabled");
                        $("#sltStateE").attr("disabled", "disabled");
                        $("#txtPOPlanNum").attr("disabled", "disabled");
                        $("#txtTotalFee").attr("disabled", "disabled");
                        $("#txtareaBillToE,#txtareaShipToE").removeAttr("disabled");
                        AssignRolePermission();
                    }

                    if (type == "View") {
                        $(".trHide").show();
                        $("#divEdit .txt").each(function () {
                            $(this).attr("disabled", "disabled");
                        });
                        $("#txtareaBillToE,#txtareaShipToE").attr("disabled", "disabled");
                    }
                    $("#txtInnerOrderNOE").attr("disabled", "disabled");
                },
                ok: function () {
                    if (type == "View") {
                        return true;
                    }
                    var PONO = Trim($("#txtPONOE").val());
                    var InnerBuyer = Trim($("#txtInnerBuyerE").val());
                    var SupplierID = Trim($("#sltSupplierIDE").val());
                    var IssuedDate = Trim($("#txtIssuedDateE").val());
                    var ContactPerson = Trim($("#txtContactPersonE").val());
                    var Tel = Trim($("#txtTelE").val());
                    var BillManager = Trim($("#txtBillManagerE").val());
                    var BillManagerTel = Trim($("#txtBillManagerTelE").val());
                    var CorporationID = Trim($("#sltCorporationIDE").val());
                    var BillTo = Trim($("#txtareaBillToE").text());
                    var DeliveryDate = Trim($("#txtDeliveryDateE").val());
                    var Shipping = Trim($("#txtShipping").val());
                    var ShipManager = Trim($("#txtShipManagerE").val());
                    var ShipManagerTel = Trim($("#txtShipManagerTelE").val());
                    var PaymentTerms = Trim($("#sltPaymentTermsE").val());
                    var ShipTo = Trim($("#txtareaShipToE").text());

                    if (CorporationID == "") {
                        artDialog.tips("公司抬头不能为空!");
                        return false;
                    }
                    if (PONO == "") {
                        artDialog.tips("采购订单号不能为空!");
                        return false;
                    }
                    if (SupplierID == "") {
                        artDialog.tips("供应商不能为空!");
                        return false;
                    }
                    if (IssuedDate == "") {
                        artDialog.tips("采购日期不能为空!");
                        return false;
                    }
                    if (PaymentTerms == "") {
                        artDialog.tips("付款条例不能为空!");
                        return false;
                    }
                    if (BillTo == "") {
                        artDialog.tips("账单地址不能为空!");
                        return false;
                    }
                    if (ShipTo == "") {
                        artDialog.tips("送货地址不能为空!");
                        return false;
                    }

                    var url = "/Handle/OMS/POHandle.ashx";
                    //新增采购订单
                    var data = { "meth": "AddPO", "PONO": PONO, "InnerBuyer": InnerBuyer, "SupplierID": SupplierID, "IssuedDate": IssuedDate, "ContactPerson": ContactPerson, "Tel": Tel, "BillManager": BillManager, "BillManagerTel": BillManagerTel, "CorporationID": CorporationID, "BillTo": BillTo, "DeliveryDate": DeliveryDate, "Shipping": Shipping, "ShipManager": ShipManager, "ShipManagerTel": ShipManagerTel, "PaymentTerms": PaymentTerms, "ShipTo": ShipTo };
                    //编辑采购订单
                    if (id && id != "") {
                        data = { "meth": "EditPO", "PONO": PONO, "InnerBuyer": InnerBuyer, "SupplierID": SupplierID, "IssuedDate": IssuedDate, "ContactPerson": ContactPerson, "Tel": Tel, "BillManager": BillManager, "BillManagerTel": BillManagerTel, "CorporationID": CorporationID, "BillTo": BillTo, "DeliveryDate": DeliveryDate, "Shipping": Shipping, "ShipManager": ShipManager, "ShipManagerTel": ShipManagerTel, "PaymentTerms": PaymentTerms, "ShipTo": ShipTo, "Id": id };
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
        }

        function AssignRolePermission() {
            var strAInfo = $("#hfAdminInfo").val();
            var arrInfo = strAInfo.split("$");
            if (arrInfo.length > 0) {
                $("#txtInnerBuyerE,#txtShipManagerE,#txtShipManagerE,#txtBillManagerE").val(arrInfo[0]);
            }
            if (arrInfo.length > 1) {
                $("#txtShipManagerTelE,#txtBillManagerTelE").val(arrInfo[1]);
            }
            if (arrInfo.length > 2) {
                if (arrInfo[2].indexOf(',2,') > -1) {
                    //是超级管理员可编辑
                    $("#txtInnerBuyerE").removeAttr("disabled");
                }
                else {
                    $("#txtInnerBuyerE").attr("disabled", "disabled");
                }
            }
        }

        //取消采购订单
        function DelPO(id) {
            art.dialog.confirm('订单取消后将无法恢复，你确认要取消吗？', function () {
                //调用取消采购订单方法
                var url = "/Handle/OMS/POHandle.ashx";
                var data = { "meth": "DelPO", "Id": id };
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

        //供应商审核
        function ConfirmSupplier(id) {
            art.dialog.confirm('供应商审核后，系统会自动生成产品编码，供应商与厂商型号唯一标识一个产品编码。<br/>采购计划以及客户产品清单将使用此产品编码。<br/>您确认进行此操作吗？', function () {
                //调用取消采购订单方法
                var url = "/Handle/OMS/POHandle.ashx";
                var data = { "meth": "ConfirmSupplier", "POId": id };
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

        //填充
        function FillCorporationData(CorporationID) {
            $("#txtareaBillToE,#txtareaShipToE").text("");
            //调用取消采购订单方法
            var url = "/Handle/OMS/CorporationHandle.ashx";
            var data = { "meth": "GetCorporationById", "Id": CorporationID };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    //$("#txtBillManagerE,#txtShipManagerE").val(json.Corporator);
                    $("#txtareaBillToE,#txtareaShipToE").text(json.CompanyAddr);
                    artDialog.tips("自动填充公司信息成功，如需变更请修改。");
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
    </script>
    <!--关联客户订单、生成采购计划、采购审核-->
    <script type="text/javascript">
        //初始化客户订单关联树
        //function InitBindCOTree(poId) {
        //    //表格列树的显示
        //    var zBindCONodes;
        //    var settingBindCO = {
        //        check: {
        //            enable: true,
        //            chkDisabledInherit: true,
        //            chkboxType: { "Y": "s", "N": "s" }
        //        },
        //        data: {
        //            simpleData: {
        //                enable: true
        //            },
        //            key: {
        //                title: "fullname"
        //            }
        //        }
        //    };

        //    //调用关联客户订单方法
        //    var url = "/Handle/OMS/POHandle.ashx";
        //    var data = { "meth": "GetBindCOTree", "POId": poId };
        //    var successFun = function (json) {
        //        if (!json || (json.State && json.State == "0")) {
        //            artDialog.alert(json.Info);
        //            return false;
        //        } else {
        //            //加载客户订单树
        //            zBindCONodes = json;
        //            $.fn.zTree.init($("#BindCOTree"), settingBindCO, zBindCONodes);
        //            return true;
        //        }
        //    };
        //    var errorFun = function (x, e) {
        //        alert(x.responseText);
        //    };
        //    JsAjax(url, data, successFun, errorFun);
        //}

        //关联客户订单
        //id:采购订单Id
        //function BindCustiomerOrder(id, type) {
        //    var strTitle = "采购订单管理>>关联客户订单";
        //    if (type == "View") {
        //        strTitle = "采购订单管理>>查看客户订单";
        //    }
        //    $.dialog({
        //        id: 'divBindCO',
        //        title: strTitle,
        //        width: 'auto',
        //        height: 'auto',
        //        padding: 10,
        //        content: $('#divBindCO').get(0),
        //        init: function () {
        //            InitBindCOTree(id);
        //        },
        //        ok: function () {
        //            if (type == "View") {
        //                return true;
        //            }
        //            art.dialog.confirm('关联客户订单后，对应的客户订单状态将变更为采购流程中，<br>采购订单状态变更为关联待采购，请在确定后进行生成采购计划操作。您确定要关联吗？',
        //             function () {
        //                 var zBindCOTree = $.fn.zTree.getZTreeObj("BindCOTree");
        //                 var obj = zBindCOTree.getCheckedNodes(true);  //获取被选中的客户订单列表树结点
        //                 var orderIdList = "";
        //                 //                         if (obj.length <= 0) {
        //                 //                             artDialog.tips("未选择任何列名。");
        //                 //                             return false;
        //                 //                         }

        //                 for (var i = 0; i < obj.length; i++) {
        //                     orderIdList += obj[i].id + ",";
        //                 }
        //                 if (orderIdList != "") {
        //                     orderIdList = orderIdList.substring(0, orderIdList.length - 1); //所有的列
        //                 }

        //                 //调用关联客户订单方法
        //                 var url = "/Handle/OMS/POHandle.ashx";
        //                 var data = { "meth": "BindCustiomerOrder", "POId": id, "CustomerOrderIdList": orderIdList };
        //                 var successFun = function (json) {
        //                     if (json.State == "0") {
        //                         artDialog.alert(json.Info);
        //                         return false;
        //                     } else {
        //                         artDialog.tips(json.Info);
        //                         art.dialog.list["divBindCO"].close();
        //                         //刷新表格
        //                         $("#FlexigridTable").flexReload();
        //                         return true;
        //                     }
        //                 };
        //                 var errorFun = function (x, e) {
        //                     alert(x.responseText);
        //                 };
        //                 JsAjax(url, data, successFun, errorFun);
        //             }, function () {
        //                 art.dialog.tips('取消操作');
        //                 return true;
        //             });
        //            return false;
        //        },
        //        cancel: true
        //    });
        //}

        //加载客户产品清单
        //function InitCustomerOrderDetail(poId) {
        //    var gridWidth = document.documentElement.clientWidth - 50;
        //    var gridHeight = document.documentElement.clientHeight - 225;
        //    $("#tbCustomerOrderDetail").flexigrid({
        //        width: gridWidth, //表格宽度
        //        height: gridHeight, //表格高度
        //        url: '/Handle/OMS/POHandle.ashx', //数据请求地址
        //        dataType: 'json', //请求数据的格式
        //        extParam: [{ name: "meth", value: "GetPOBindCustomerOrderDetail" }, { name: "POId", value: poId }], //扩展参数
        //        colModel: [//表格的题头与索要填充的内容。
        //           { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
        //                { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
        //				{ display: '内部订单号', name: 'InnerOrderNO', width: 100, sortable: true, align: 'center' },
        //                { display: '客户订单号', name: 'CustomerOrderNO', width: 100, sortable: true, align: 'center' },
        //                { display: '客户名称', name: 'CustomerName', width: 140, sortable: true, align: 'left' },
        //                { display: '客户订单状态', name: 'CustomerOrderState', width: 100, sortable: true, align: 'center' },
        //                { display: '订单日期', name: 'CustOrderDate', width: 100, sortable: true, align: 'center' },
        //				{ display: '要求交期', name: 'CRD', width: 100, sortable: true, align: 'center' },
        //                { display: '客户型号', name: 'CPN', width: 80, sortable: true, align: 'left' },
        //                { display: '厂商型号', name: 'MPN', width: 80, sortable: true, align: 'left' },
        //				{ display: '客户订单数量', name: 'CustQuantity', width: 90, sortable: true, align: 'right' },
        //                { display: '采购计划数量', name: 'AlreadyPurchaseNum', width: 80, sortable: true, align: 'right' },
        //                { display: '使用库存数量', name: 'UseStockNum', width: 80, sortable: true, align: 'right' },
        //                { display: '已出货数量', name: 'AlreadyShippingNum', width: 80, sortable: true, align: 'right' },
        //                { display: '客户订单关联的采购订单', name: 'RefPONO', width: 240, sortable: true, align: 'left' },
        //                { display: '已关联', name: 'checked999', width: 10, toggle: false, hide: true, align: 'left' }
        //        ],
        //        sortname: "InnerOrderNO",
        //        sortorder: "ASC",
        //        title: "关联客户订单清单明细",
        //        usepager: true,
        //        useRp: true,
        //        rowbinddata: true,
        //        showcheckbox: true,
        //        selectedonclick: true,
        //        singleselected: false,
        //        rowbinddata: true
        //    });
        //}

        //生成采购计划
        //function GeneratePOPlan(poId, type) {
        //    var strTitle = "采购订单管理>>生成采购计划";
        //    if (type == "View") {
        //        strTitle = "采购订单管理>>查看采购计划";
        //    }
        //    var gridWidth = document.documentElement.clientWidth - 50;
        //    var gridHeight = document.documentElement.clientHeight - 225;
        //    $.dialog({
        //        id: 'divGeneratePOPlan',
        //        title: strTitle,
        //        width: gridWidth,
        //        height: gridHeight,
        //        padding: 5,
        //        top: 0,
        //        content: $('#divGeneratePOPlan').get(0),
        //        init: function () {
        //            if ($("#tbCustomerOrderDetail").GetOptions() == null) {
        //                InitCustomerOrderDetail(poId);
        //            }
        //            else {
        //                var p = { extParam: [{ name: "meth", value: "GetPOBindCustomerOrderDetail" }, { name: "POId", value: poId }] }; //扩展参数
        //                p.newp = 1;         //跳转到第一页。
        //                $("#tbCustomerOrderDetail").flexOptions(p).flexReload();
        //            }
        //        },
        //        ok: function () {
        //            if (type == "View") {
        //                return true;
        //            }
        //            art.dialog.confirm('系统将根据该采购订单关联的客户订单自动智能分析，生成对应的计划进行产品采购；<br/>生成后，采购订单状态将变更为采购计划中，您确定要生成采购计划吗？', function () {
        //                var obj = $("#tbCustomerOrderDetail").getSelectedRows();
        //                var idList = "";

        //                for (var i = 0; i < obj.length; i++) {
        //                    idList += obj[i][1] + ",";
        //                }
        //                if (idList != "") {
        //                    idList = idList.substring(0, idList.length - 1); //取得选择行ID
        //                }

        //                //调用取消采购订单方法
        //                var url = "/Handle/OMS/POHandle.ashx";
        //                var data = { "meth": "GeneratePOPlan", "POId": poId, "COIds": idList };
        //                var successFun = function (json) {
        //                    if (json.State == "0") {
        //                        artDialog.alert(json.Info);
        //                        return false;
        //                    } else {
        //                        art.dialog.confirm(json.Info + "是否退出生成采购计划操作？<br/>“确定”退出，“取消”继续选择其他产品清单关联。", function () {
        //                            art.dialog.list["divGeneratePOPlan"].close();
        //                            $("#FlexigridTable").flexReload(); //刷新表格
        //                        }, function () {
        //                            $("#tbCustomerOrderDetail").flexReload();
        //                        });
        //                        return true;
        //                    }
        //                };
        //                var errorFun = function (x, e) {
        //                    alert(x.responseText);
        //                };
        //                JsAjax(url, data, successFun, errorFun);
        //                return true;
        //            }, function () {
        //                art.dialog.tips('取消操作');
        //            });
        //            return false;
        //        },
        //        cancel: function () {
        //            art.dialog.tips('取消操作');
        //        }
        //    });
        //}
        //加载产品采购分析列表
        //function InitConfirmSecond(poId) {
        //    var gridWidth = document.documentElement.clientWidth - 60;
        //    var gridHeight = document.documentElement.clientHeight - 225;
        //    $("#tbConfirmSecond").flexigrid({
        //        width: gridWidth, //表格宽度
        //        height: gridHeight, //表格高度
        //        url: '/Handle/OMS/POHandle.ashx', //数据请求地址
        //        dataType: 'json', //请求数据的格式
        //        extParam: [{ name: "meth", value: "GetConfirmSecondData" }, { name: "POId", value: poId }], //扩展参数
        //        colModel: [//表格的题头与索要填充的内容。
        //           { display: '行索引', name: 'RowNum', toggle: false, hide: true, iskey: true, width: 40, align: 'center' },
        //                { display: '毛利润(USD)', name: 'GrossProfits', width: 80, toggle: false, hide: false, align: 'right' },
        //                { display: '利润率', name: 'ProfitMargin', width: 60, toggle: false, hide: false, align: 'right' },
        //                { display: '客户型号', name: 'CPN', toggle: false, hide: false, width: 140, align: 'left' },
        //				{ display: '厂商型号', name: 'MPN', width: 140, sortable: true, align: 'left' },
        //                { display: '采购数量', name: 'POQuantity', width: 80, sortable: true, align: 'right' },
        //                { display: '实际买货币', name: 'BuyRealCurrency', width: 80, sortable: true, align: 'center' },
        //                { display: '买汇率', name: 'BuyExchangeRate', width: 80, sortable: true, align: 'right' },
        //                { display: '实际买价', name: 'BuyRealPrice', width: 80, sortable: true, align: 'right' },
        //                { display: '标准实价(USD)', name: 'BuyPrice', width: 100, sortable: true, align: 'right' },
        //                { display: '买货成本(USD)', name: 'BuyCost', width: 100, sortable: true, align: 'right' },
        //                { display: '内部订单号', name: 'InnerOrderNO', width: 80, sortable: true, align: 'center' },
        //                { display: '客户订单号', name: 'CustomerOrderNO', width: 120, sortable: true, align: 'center' },
        //                { display: '客户名称', name: 'CustomerName', width: 240, sortable: true, align: 'left' },
        //                { display: '客户订单时间', name: 'CustOrderDate', width: 80, sortable: true, align: 'center' },
        //                { display: '订单数量', name: 'CustQuantity', width: 80, sortable: true, align: 'right' },
        //                { display: '实际卖货币', name: 'SaleRealCurrency', width: 80, sortable: true, align: 'center' },
        //                { display: '卖汇率', name: 'SaleExchangeRate', width: 60, sortable: true, align: 'right' },
        //                { display: '实际卖价', name: 'SaleRealPrice', width: 80, sortable: true, align: 'right' },
        //                { display: '标准卖价(USD)', name: 'SalePrice', width: 100, sortable: true, align: 'right' },
        //				{ display: '标准售价总金额(USD)', name: 'SaleStandardTotal', width: 120, sortable: true, align: 'right' }
        //        ],
        //        sortname: "InnerOrderNO",
        //        sortorder: "ASC",
        //        title: "产品采购分析列表",
        //        usepager: true,
        //        useRp: true,
        //        rp: 1000,
        //        rowbinddata: true,
        //        showcheckbox: false,
        //        selectedonclick: true,
        //        singleselected: false,
        //        rowbinddata: true
        //    });
        //}
    </script>
    <!--关联采购计划、确认供应商-->
    <script type="text/javascript">
        //初始化客户订单关联树
        function InitBindPoPlanTree(poId) {
            //表格列树的显示
            var zBindCONodes;
            var settingBindCO = {
                check: {
                    enable: true,
                    chkDisabledInherit: true,
                    chkboxType: { "Y": "s", "N": "s" }
                },
                data: {
                    simpleData: {
                        enable: true
                    },
                    key: {
                        title: "fullname"
                    }
                }
            };

            //调用关联客户订单方法
            var url = "/Handle/OMS/POHandle.ashx";
            var data = { "meth": "GetBindPOPlanTree", "POId": poId };
            var successFun = function (json) {
                if (!json || (json.State && json.State == "0")) {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    //加载客户订单树
                    zBindCONodes = json;
                    $.fn.zTree.init($("#BindPOPlanTree"), settingBindCO, zBindCONodes);
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //关联采购计划
        //id:采购订单Id
        function BindPOPlan(id, type) {
            var strTitle = "采购订单管理>>关联采购计划";
            if (type == "View") {
                strTitle = "采购订单管理>>查看采购计划";
            }
            $.dialog({
                id: 'divBindPOPlan',
                title: strTitle,
                width: 'auto',
                height: 'auto',
                padding: 10,
                content: $('#divBindPOPlan').get(0),
                init: function () {
                    InitBindPoPlanTree(id);
                },
                ok: function () {
                    if (type == "View") {
                        return true;
                    }
                    art.dialog.confirm('关联采购计划后，相关联的采购订单与采购计划状态将变更为供应商审核状态，<br>您可以在关联后通过查看采购计划查看关联结果。您确定要关联吗？',
                     function () {
                         var zBindPOPlan = $.fn.zTree.getZTreeObj("BindPOPlanTree");
                         var obj = zBindPOPlan.getCheckedNodes(true);  //获取被选中的客户订单列表树结点
                         var idList = "";
                         //                         if (obj.length <= 0) {
                         //                             artDialog.tips("未选择任何列名。");
                         //                             return false;
                         //                         }

                         for (var i = 0; i < obj.length; i++) {
                             idList += obj[i].id + ",";
                         }
                         if (idList != "") {
                             idList = idList.substring(0, idList.length - 1); //所有的列
                         }

                         //调用关联客户订单方法
                         var url = "/Handle/OMS/POHandle.ashx";
                         var data = { "meth": "BindPOPlan", "POId": id, "POPlanIdList": idList };
                         var successFun = function (json) {
                             if (json.State == "0") {
                                 artDialog.alert(json.Info);
                                 return false;
                             } else {
                                 artDialog.tips(json.Info);
                                 art.dialog.list["divBindPOPlan"].close();
                                 //刷新表格
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
                         return true;
                     });
                    return false;
                },
                cancel: true
            });
        }

        //查看采购计划
        function ViewPOPlan(poId, type) {
            var strTitle = "采购订单管理>>生成采购计划";
            if (type == "View") {
                strTitle = "采购订单管理>>查看采购计划";
            }
            var gridWidth = document.documentElement.clientWidth - 50;
            var gridHeight = document.documentElement.clientHeight - 225;
            $.dialog({
                id: 'divViewPOPlan',
                title: strTitle,
                width: gridWidth,
                height: gridHeight,
                padding: 5,
                top: 0,
                content: $('#divViewPOPlan').get(0),
                init: function () {
                    if ($("#tbPOPlan").GetOptions() == null) {
                        InitPOPlan(poId);
                    }
                    else {
                        var p = {
                            extParam: [{ name: "meth", value: "GetPOPlan" },
                                { name: "POId", value: poId },
                                { name: "State", value: 1 },
                                { name: "startTime", value: '2013-01-01' },
                                { name: "TimeType", value: "2" }]
                        }; //扩展参数
                        p.newp = 1;         //跳转到第一页。
                        $("#tbPOPlan").flexOptions(p).flexReload();
                    }
                },
                ok: false,
                cancel: function () {
                    art.dialog.tips('取消操作');
                }
            });
        }
        //加载采购计划
        function InitPOPlan(poId) {
            var gridWidth = document.documentElement.clientWidth - 50;
            var gridHeight = document.documentElement.clientHeight - 225;
            $("#tbPOPlan").flexigrid({
                width: gridWidth, //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/POPlanHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetPOPlan" },
                    { name: "POId", value: poId },
                    { name: "startTime", value: '2013-01-01' },
                    { name: "TimeType", value: "2" }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                  { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '采购订单号', name: 'PONO', width: 70, sortable: true, align: 'center' },
                        { display: '状态', name: 'State', width: 100, sortable: true, align: 'center' },
                        { display: '产品编码', name: 'ProductCode', width: 80, sortable: true, align: 'left' },
                        { display: '供应商', name: 'SupplierName', width: 160, sortable: true, align: 'left' },
                        { display: '客户型号', name: 'CPN', width: 140, sortable: true, align: 'left' },
                        { display: '厂家标准型号', name: 'MPN', width: 140, sortable: true, align: 'left' },
                        { display: '品牌名称', name: 'MFG', width: 80, sortable: true, align: 'left' },
                        { display: '生产年份', name: 'DC', width: 80, sortable: true, align: 'center' },
                        { display: '是否环保', name: 'ROHS', width: 100, sortable: true, align: 'center' },
                        { display: '采购订单数量', name: 'POQuantity', width: 80, sortable: true, align: 'right' },
                        { display: ' 已到货数量', name: 'ArrivedQty', width: 80, sortable: true, align: 'right' },
                        { display: '已入库存数量', name: 'StockQty', width: 80, sortable: true, align: 'right' },
                        { display: '供应商付款方式', name: 'VendorPaymentType', width: 80, sortable: true, align: 'center' },
                        { display: '是否向供应商付款', name: 'IsPaySupplier', width: 80, sortable: true, align: 'center' },
                        { display: '买汇率', name: 'BuyExchangeRate', width: 80, sortable: true, align: 'right' },
                        { display: '实际买货币', name: 'BuyRealCurrency', width: 80, sortable: true, align: 'center' },
                        { display: '实际买价', name: 'BuyRealPrice', width: 80, sortable: true, align: 'right' },
                        { display: '标准买货币', name: 'BuyStandardCurrency', width: 80, sortable: true, align: 'center' },
                        { display: '标准买价', name: 'BuyPrice', width: 80, sortable: true, align: 'right' },
                        { display: '标准买货总成本', name: 'BuyCost', width: 80, sortable: true, align: 'right' },
                        { display: '其他费用', name: 'OtherFee', width: 80, sortable: true, align: 'right' },
                        { display: '供应商答复交期', name: 'VCD', width: 80, sortable: true, align: 'left' },
                        { display: '到货日期', name: 'DeliveryDate', width: 80, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '更新时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "ASC",
                title: "查看采购计划",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: false,
                selectedonclick: true,
                singleselected: false,
                rowbinddata: true
            });
        }
    </script>
    <!--采购审核-->
    <script type="text/javascript">
        //加载产品采购分析列表
        function InitConfirmSecond(poId) {
            var gridWidth = document.documentElement.clientWidth - 60;
            var gridHeight = document.documentElement.clientHeight - 225;
            $("#tbConfirmSecond").flexigrid({
                width: gridWidth, //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/POHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetConfirmSecondData" }, { name: "POId", value: poId }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: true, iskey: true, width: 40, align: 'center' },
                        { display: '产品编码', name: 'ProductCode', width: 70, sortable: true, align: 'left' },
                        { display: '厂商型号', name: 'MPN', width: 140, sortable: true, align: 'left' },
                        { display: '在库数量', name: 'OnhandQty', width: 80, sortable: true, align: 'right' },
                        { display: '采购中数量', name: 'POQty', width: 80, sortable: true, align: 'right' },
                        { display: '入库中数量', name: 'UnInQty', width: 80, sortable: true, align: 'right' },
                        { display: '出库中数量', name: 'UnOutQty', width: 80, sortable: true, align: 'right' },
	    				{ display: '历史最低采购价(USD)', name: 'MinBuyPrice', width: 80, sortable: true, align: 'right' },
                        { display: '此次采购数量', name: 'POQuantity', width: 80, sortable: true, align: 'right' },
                        { display: '实际买货币', name: 'BuyRealCurrency', width: 80, sortable: true, align: 'center' },
                        { display: '买汇率', name: 'BuyExchangeRate', width: 80, sortable: true, align: 'right' },
                        { display: '实际买价', name: 'BuyRealPrice', width: 80, sortable: true, align: 'right' },
                        { display: '标准买价(USD)', name: 'BuyPrice', width: 100, sortable: true, align: 'right' },
                        { display: '买货成本(USD)', name: 'BuyCost', width: 100, sortable: true, align: 'right' }
                ],
                sortname: "ProductCode",
                sortorder: "ASC",
                title: "产品采购分析列表",
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

        //采购审核
        function ConfirmSecond(poId) {
            var gridWidth = document.documentElement.clientWidth - 50;
            var gridHeight = document.documentElement.clientHeight - 225;
            art.dialog.confirm('系统将根据此采购订单中各产品采购信息与历史采购数据进行分析对比，<br/>您确认参考这些数据进行采购审核吗？', function () {
                $.dialog({
                    id: 'divConfirmSecond',
                    title: '采购订单管理>>采购审核',
                    width: gridWidth,
                    height: gridHeight,
                    padding: 5,
                    top: 0,
                    left: 0,
                    content: $('#divConfirmSecond').get(0),
                    init: function () {
                        if ($("#tbConfirmSecond").GetOptions() == null) {
                            InitConfirmSecond(poId);
                        }
                        else {
                            var p = { extParam: [{ name: "meth", value: "GetConfirmSecondData" }, { name: "POId", value: poId }] }; //扩展参数
                            p.newp = 1;         //跳转到第一页。
                            $("#tbConfirmSecond").flexOptions(p).flexReload();
                        }
                    },
                    button: [
                    {
                        name: '同意',
                        callback: function () {
                            DoConfirmSecond(poId, 1);
                            return true;
                        }
                    },
                    {
                        name: '否决',
                        callback: function () {
                            DoConfirmSecond(poId, 0);
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
            }, function () {
                art.dialog.tips('取消操作');
            });
        }
        //执行审核操作 type 1:同意 0:否决
        function DoConfirmSecond(poId, type) {
            //调用取消采购订单方法
            var url = "/Handle/OMS/POHandle.ashx";
            var data = { "meth": "ConfirmSecond", "POId": poId, "Type": type };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    artDialog.tips(json.Info);
                    $("#FlexigridTable").flexReload(); //刷新表格
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
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
                        <td style="text-align: right;">
                            <div id="divPONO" style="float: left;">
                                <div class="spandiv">
                                    采购订单号：
                                </div>
                                <input style="width: 120px;" id="txtPONO" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divSupplierID" style="float: left;">
                                <div class="spandivAdjust">
                                    供应商：
                                </div>
                                <select id="sltSupplierID" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divCorporationID" style="float: left;">
                                <div class="spandiv">
                                    公司抬头：
                                </div>
                                <select id="sltCorporationID" class="txt" runat="server" style="width: 125px;">
                                    <option value="">--请选择--</option>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <div id="divStartTime" style="float: left;">
                                <div class="spandiv">
                                    开始时间：
                                </div>
                                <input class="txt" runat="server" id="txtBeginTime" type="text" style="width: 120px"
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
                                    <input class="txt" id="txtEndTime" type="text" style="width: 120px" onclick="WdatePicker({ startDate: '%y-%M-%d 23:59:59', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })"
                                        runat="server" />
                                    <img alt="" onclick="WdatePicker({el:'txtEndTime',startDate:'%y-%M-%d 23:59:59',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                        src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                        align="absmiddle" />
                                </div>
                                <select class="txt" id="sltTimeType" runat="server" style="width: 100px">
                                    <option value="0" title="采购订单日期">采购订单日期</option>
                                    <option value="1" title="发行采购日期">发行采购日期</option>
                                    <option value="2" title="投递日期">投递日期</option>
                                    <option value="3" title="创建时间" selected="selected">创建时间</option>
                                    <option value="4" title="更新时间">更新时间</option>
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
                    <asp:Button runat="server" Text="取消" CssClass="btnHigh" ID="btnDel" />
                    <%--<asp:Button runat="server" Text="关联客户订单" CssClass="btnHigh" ID="btnBindCustiomerOrder" />--%>
                    <%--<asp:Button runat="server" Text="生成采购计划" CssClass="btnHigh" ID="btnGeneratePOPlan" />--%>
                    <asp:Button runat="server" Text="关联采购计划" CssClass="btnHigh" ID="btnBindPOPlan" />
                    <asp:Button runat="server" Text="供应商审核" CssClass="btnHigh" ID="btnConfirmSupplier" />
                    <asp:Button runat="server" Text="采购审核" CssClass="btnHigh" ID="btnConfirmSecond" />
                    <%-- <asp:Button runat="server" Text="生成PDF" CssClass="btnHigh" ID="btnPdf" />--%>
                    <asp:Button runat="server" Text="查看" CssClass="btnHigh" ID="btnView" />
                    <%--<asp:Button runat="server" Text="查看客户订单" CssClass="btnHigh" ID="btnViewCustiomerOrder" />--%>
                    <asp:Button runat="server" Text="查看采购计划" CssClass="btnHigh" ID="btnViewPOPlan" />
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
            <!--新增/编辑/查看采购订单对话框-->
            <div id="divEdit">
                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                    <tr class="trMO">
                        <td colspan="4" style="text-align: center;">公司抬头：<select class="txt" id="sltCorporationIDE" runat="server" style="width: 300px">
                        </select>
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">采购订单号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtPONOE" class="txt " maxlength="16" />
                            <label class="red">
                                *</label>
                        </td>
                        <td class="tdRight tdParamDWidth">公司采购：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtInnerBuyerE" class="txt" maxlength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft tdHead" colspan="4">供应商信息
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">供应商：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltSupplierIDE" runat="server" style="width: 200px">
                            </select>
                            <label class="red">
                                *</label>
                        </td>
                        <td class="tdRight tdParamDWidth">采购日期：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input class="txt" id="txtIssuedDateE" type="text" onclick="WdatePicker()" style="width: 100px" />
                            <img alt="" onclick="WdatePicker({el:'txtIssuedDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                width="16" height="22" align="absmiddle" />
                            <label class="red">
                                *</label>
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">联系人：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtContactPersonE" class="txt" maxlength="32" />
                        </td>
                        <td class="tdRight tdParamDWidth">联系电话：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtTelE" class="txt " maxlength="32" />
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">付款条例：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltPaymentTermsE" runat="server" style="width: 260px">
                            </select>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdLeft tdHead" colspan="4">账单信息
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">负责人：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBillManagerE" class="txt " maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">联系电话：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBillManagerTelE" class="txt " maxlength="32" />
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">中英文地址：
                        </td>
                        <td colspan="3">
                            <textarea id="txtareaBillToE" cols="51" rows="3"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft tdHead" colspan="4">交货信息
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">交货日期：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input class="txt" id="txtDeliveryDateE" type="text" onclick="WdatePicker()" style="width: 100px" />
                            <img alt="" onclick="WdatePicker({el:'txtDeliveryDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                width="16" height="22" align="absmiddle" />
                        </td>
                        <td class="tdRight tdParamDWidth">交货条款：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtShipping" class="txt " maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">负责人：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtShipManagerE" class="txt " maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">联系电话：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtShipManagerTelE" class="txt " maxlength="32" />
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">中英文地址：
                        </td>
                        <td colspan="3">
                            <textarea id="txtareaShipToE" cols="51" rows="3"></textarea>
                        </td>
                    </tr>
                    <tr class="trHide">
                        <td class="tdLeft tdHead" colspan="4">综合信息
                        </td>
                    </tr>
                    <tr class="trMO trHide">
                        <td class="tdRight tdParamDWidth">采购计划数：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtPOPlanNumE" class="txt OnlyInt tdRight" />
                        </td>
                        <td class="tdRight tdParamDWidth">合计USD：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtTotalFeeE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO trHide">
                        <td class="tdRight tdParamDWidth">订单状态：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltStateE" runat="server" style="width: 125px">
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth"></td>
                        <td class="tdLeft tdRemarkWidth"></td>
                    </tr>
                </table>
            </div>
            <!--关联客户订单-->
            <%-- <div class="zTreeDemoBackground" align="center" style="float: left" id="divBindCO">
                <div class="tdHead" style="width: 430px;">
                    待采购客户订单列表
                </div>
                <ul id="BindCOTree" class="ztree" style="width: 420px; height: 250px;">
                </ul>
            </div>--%>
            <div class="zTreeDemoBackground" align="center" style="float: left" id="divBindPOPlan">
                <div class="tdHead" style="width: 620px;">
                    待关联确认供应商的采购计划列表
                </div>
                <ul id="BindPOPlanTree" class="ztree" style="width: 620px; height: 250px;">
                </ul>
            </div>

            <!--生成采购计划-->
            <%--<div id="divGeneratePOPlan">
                <table>
                    <tr>
                        <td>
                            <div id="div1" style="float: left; width: 200px;">
                                <div class="spandiv">
                                    采购订单号：
                                </div>
                                <span style="width: 120px;" id="spanPONO1"></span>
                            </div>
                            <div id="div2" style="float: left; width: 280px;">
                                <div class="spandivAdjust">
                                    公司抬头：
                                </div>
                                <span style="width: 220px;" id="spanCorporation1"></span>
                            </div>
                            <div id="div3" style="float: left; width: 165px;">
                                <div class="spandivAdjust">
                                    公司采购：
                                </div>
                                <span style="width: 120px;" id="spanBuyer1"></span>
                            </div>
                            <div id="div4" style="float: left; width: 250px;">
                                <div class="spandivAdjust">
                                    供应商：
                                </div>
                                <span style="width: 180px;" id="spanSupplier1"></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tbCustomerOrderDetail" style="float: left;">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>--%>
            <!--查看采购计划-->
            <div id="divViewPOPlan">
                <table>
                    <tr>
                        <td>
                            <div id="div1" style="float: left; width: 200px;">
                                <div class="spandiv">
                                    采购订单号：
                                </div>
                                <span style="width: 120px;" id="spanPONO1"></span>
                            </div>
                            <div id="div2" style="float: left; width: 280px;">
                                <div class="spandivAdjust">
                                    公司抬头：
                                </div>
                                <span style="width: 220px;" id="spanCorporation1"></span>
                            </div>
                            <div id="div3" style="float: left; width: 165px;">
                                <div class="spandivAdjust">
                                    公司采购：
                                </div>
                                <span style="width: 120px;" id="spanBuyer1"></span>
                            </div>
                            <div id="div4" style="float: left; width: 250px;">
                                <div class="spandivAdjust">
                                    供应商：
                                </div>
                                <span style="width: 180px;" id="spanSupplier1"></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tbPOPlan" style="float: left;">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <%--采购审核--%>
            <div id="divConfirmSecond">
                <table>
                    <tr>
                        <td>
                            <div id="div10" style="float: left; width: 200px;">
                                <div class="spandiv">
                                    采购订单号：
                                </div>
                                <span style="width: 120px;" id="spanPONO"></span>
                            </div>
                            <div id="div11" style="float: left; width: 280px;">
                                <div class="spandivAdjust">
                                    公司抬头：
                                </div>
                                <span style="width: 220px;" id="spanCorporation"></span>
                            </div>
                            <div id="div12" style="float: left; width: 165px;">
                                <div class="spandivAdjust">
                                    公司采购：
                                </div>
                                <span style="width: 120px;" id="spanBuyer"></span>
                            </div>
                            <div id="div13" style="float: left; width: 250px;">
                                <div class="spandivAdjust">
                                    供应商：
                                </div>
                                <span style="width: 180px;" id="spanSupplier"></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tbConfirmSecond" style="float: left;">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
