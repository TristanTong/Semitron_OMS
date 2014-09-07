<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POPlanManager.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.OMS.POPlanManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 采购计划管理</title>
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

        .divAttachment {
            min-height: 70px;
            float: left;
        }

        #divUpload {
            text-decoration: underline;
            float: left;
            cursor: pointer;
        }

        .aFile {
            height: 40px;
            width: 120px;
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
            var gridHeight = document.documentElement.clientHeight - 204;
            //初始化查询条件收起张开事件
            InitExpandSearch("FlexigridTable", "btnSearchOpenClose", "tableSearch", 204, 144);
            //初始采购计划表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/POPlanHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetPOPlan" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                  { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '采购订单号', name: 'PONO', width: 100, sortable: true, align: 'center' },
                        { display: '状态', name: 'State', width: 100, sortable: true, align: 'center' },
                        { display: '附件数', name: 'FileNum', width: 40, sortable: true, align: 'right' },
                        { display: '产品编码', name: 'ProductCode', width: 70, sortable: true, align: 'left' },
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
                sortorder: "DESC",
                title: "采购计划列表",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: true,
                selectedonclick: true,
                singleselected: false,
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
                var obj = GetSelectedRow("FlexigridTable", "采购计划");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前采购计划已取消，无法完成此操作。");
                    return false;
                }
                if (obj[0][3] == "已完成进货") {
                    artDialog.tips("当前采购计划已完成进货，不能编辑数据。");
                    return false;
                }
                var id = obj[0][1];
                art.dialog.confirm("编辑完成后，采购计划状态将重置为临时草稿状态，<br/>采购订单重置为待关系计划状态,是否继续？", function () {
                    GetPOPlanById(id, "Edit");
                    AddOrEdit(id, "Edit");
                }, function () {
                    art.dialog.tips('取消操作');
                });

                return false;
            });
            //查看按钮
            $("#btnView").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购计划");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] == "已取消") {
                    artDialog.tips("当前采购计划已取消，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetPOPlanById(id, "View");
                AddOrEdit(id, "View");
                return false;
            });
            //取消
            $("#btnCancel").click(function () {
                CancelPOPlan();
                return false;
            });
            //自动生成采购计划
            $("#btnGeneratePOPlan").click(function () {
                GeneratePOPlan();
                return false;
            });

            //确认采购报价/数量按钮
            $("#btnConfirmPrice").click(function () {
                ConfirmPrice();
                return false;
            });
            //QC确认按钮
            $("#btnQCConfirm").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购计划");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] != "待QC确认进货" && obj[0][3] != "已完成进货" && obj[0][3] != "进货中") {
                    artDialog.tips("当前采购计划状态不为待QC确认进货、进货中或已完成进货，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                art.dialog.confirm("QC确认后，说明产品部分或完全到货，请通知业务员确认客户订单，并制作出货计划发货。<br/>您确认此操作吗？",
                function () {
                    QCConfirm(id, "Confirm");
                    GetPOPlanById(id, "Confirm");
                    $("#divQCInfo").show();
                },
                function () {
                    art.dialog.tips('取消操作');
                });

                return false;
            });
            //下载QC附件
            $("#btnDownload").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "采购计划");
                if (!obj) {
                    return false;
                }
                if (obj[0][3] != "已进货") {
                    artDialog.tips("当前采购计划状态不为已进货，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                QCConfirm(id, "Download");
                GetPOPlanById(id, "Download");
                $("#divQCInfo").hide();
                return false;
            });
            //调整库存数量时触发
            $("#txtStockQtyE").blur(function () {
                $("#txtPOQuantityE").val(parseInt($("#txtCustQuantityE").val()) - parseInt($("#txtStockQtyE").val()));
                return false;
            });
            //上传附件
            $("#btnUpload").click(function () {
                $("#frameImportFile").attr("src", "/Admin/UserControl/UploadifyPage.aspx?ButtonText=Browse&FileExt=*.*;&FileDesc=File Formart(.*)&Folder=QCFilePath&IsAutoDisappear=true&IsMulti=true");
                PopUploadControlDialog("采购计划管理>>上传附件");
            });
            //计算标准买价
            $("#txtBuyRealPriceE,#txtBuyExchangeRateE").blur(function () {
                var value = parseFloat($("#txtBuyRealPriceE").val()) / parseFloat($("#txtBuyExchangeRateE").val());
                var strvalue = value.toString();
                var strTotal = (value * parseInt($("#txtPOQuantityE").val())).toString();
                if (strvalue == "NaN") strvalue = "0";
                if (strTotal == "NaN") strTotal = "0";
                $("#txtBuyPriceE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));
                $("#txtBuyCostE").val(strTotal.substr(0, (strTotal.indexOf('.') == -1 ? (strTotal.length) : (strTotal.indexOf('.') + 5))));
                return false;
            });
        });
    </script>
    <%--查询、新增、编辑、删除采购计划记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var PONO = $("#txtPONO").val();
            var SupplierID = $("#sltSupplierID").val();
            var IsPaySupplier = $("#sltIsPaySupplier").val();
            var State = $("#sltState").val();
            var CPN = $("#txtCPN").val();
            var MPN = $("#txtMPN").val();
            var VendorPaymentTypeID = $("#sltPaymentTypeID").val();

            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetPOPlan" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "PONO", value: PONO },
                  { name: "SupplierID", value: SupplierID },
                  { name: "IsPaySupplier", value: IsPaySupplier },
                  { name: "State", value: State },
                  { name: "CPN", value: CPN },
                  { name: "MPN", value: MPN },
                  { name: "VendorPaymentTypeID", value: VendorPaymentTypeID }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }
        //根据Id获得订单记录
        function GetPOPlanById(id, type) {
            var url = "/Handle/OMS/POPlanHandle.ashx";
            var data = { "meth": "GetPOPlanById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtPONOE").val(json.PONO);
                    $("#txtCPNE").val(json.CPN);
                    $("#txtMPNE").val(json.MPN);
                    $("#sltMFGE").val(json.MFG);
                    $("#txtDCE").val(json.DC);
                    $("#txtPOQuantityE").val(json.POQuantity);
                    $("#txtStockQtyE").val(!json.StockQty ? 0 : json.StockQty);
                    $("#txtCustQuantityE").val(parseInt($("#txtPOQuantityE").val()) + parseInt($("#txtStockQtyE").val()));
                    $("#txtArrivedQtyE").val(json.ArrivedQty);
                    $("#txtAlreadyQtyE").val(json.AlreadyQty);
                    $("#sltBuyStandardCurrencyE").val(json.BuyStandardCurrency);
                    $("#txtBuyPriceE").val(json.BuyPrice);
                    $("#txtBuyCostE").val(json.BuyCost);
                    $("#sltROHSE").val(json.ROHS == false ? "false" : (json.ROHS == true ? "true" : ""));
                    $("#sltSupplierIDE").val(json.SupplierID);
                    var vCD = json.VCD;
                    if (vCD && vCD.length > 10) {
                        vCD = vCD.substr(0, 10);
                        if (vCD == "0001-01-01") {
                            vCD = "";
                        }
                    }
                    else {
                        vCD = "";
                    }
                    $("#txtVCDE").val(vCD);
                    $("#sltVendorPaymentTypeIDE").val(json.VendorPaymentTypeID);
                    $("#txtBuyExchangeRateE").val(json.BuyExchangeRate);
                    $("#sltBuyRealCurrencyE").val(json.BuyRealCurrency);
                    $("#txtBuyRealPriceE").val(json.BuyRealPrice);
                    $("#txtOtherFeeE").val(json.OtherFee);
                    $("#txtareaOtherFeeRemarkE").val(json.OtherFeeRemark);
                    $("#sltIsPaySupplierE").val(json.IsPaySupplier == false ? "false" : "true");
                    var shipmentDate = json.ShipmentDate;
                    if (shipmentDate && shipmentDate.length > 10) {
                        shipmentDate = shipmentDate.substr(0, 10);
                        if (shipmentDate == "0001-01-01") {
                            shipmentDate = "";
                        }
                    }
                    else {
                        shipmentDate = "";
                    }
                    $("#txtShipmentDateE").val(shipmentDate);
                    $("#sltStateE").val(json.State);
                    $("#txtShipmentDateQC").val(shipmentDate);
                    $("#txtArrivedQtyQC").val(json.ArrivedQty);
                    if (type == "Download" || type == "Confirm") {
                        //清除所有附件
                        $(".divAttachment").empty();
                        var urlPaths = json.AttachmentFiles.replace(/\*/g, "/");
                        $(".divAttachment").append(GetFileDivHtml(urlPaths));
                        if (type == "Download") {
                            $(".cancelFile").hide();
                        }
                        else {
                            $(".cancelFile").show();
                        }
                    }

                    if (json.ProductCode && json.ProductCode != "") {
                        $("#spanProductCodeE").text(",编码:" + json.ProductCode);
                    }

                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看采购计划
        function AddOrEdit(id, type) {
            var dialogTitle = "新增采购计划草稿";
            if (type == "Edit") {
                dialogTitle = "编辑采购计划"
            }
            if (type == "View") {
                dialogTitle = "查看采购计划"
            }

            $.dialog({
                id: 'divEdit',
                title: "采购计划管理>>" + dialogTitle,
                width: 600,
                height: 450,
                padding: 10,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        $(".trHide").hide();
                        $("#divEdit .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $("#divEdit .txt").each(function () {
                            $(this).val("");
                        });
                        $("#txtareaOtherFeeRemarkE").val("");
                        $("#txtareaOtherFeeRemarkE").removeAttr("disabled");
                        $("#sltBuyStandardCurrencyE").val("1");
                        $("#txtPONOE").addClass("disEdit");
                    }
                    if (type == "Edit") {
                        $(".trHide").show();
                        $("#divEdit .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $("#txtPONOE,#sltStateE").attr("disabled", "disabled");
                        $("#txtareaOtherFeeRemarkE").removeAttr("disabled");
                    }
                    if (type == "View") {
                        $(".trHide").show();
                        $("#divEdit .txt").each(function () {
                            $(this).attr("disabled", "disabled");
                        });
                        $("#txtareaOtherFeeRemarkE").attr("disabled", "disabled");
                    }
                    $("#divEdit .disEdit").each(function () {
                        $(this).attr("disabled", "disabled");
                    });
                    $("#sltBuyStandardCurrencyE").attr("disabled", "disabled");
                },
                ok: function () {
                    if (type == "View") {
                        return true;
                    }
                    var PONO = Trim($("#txtPONOE").val());
                    var CPN = Trim($("#txtCPNE").val());
                    var MPN = Trim($("#txtMPNE").val());
                    var MFG = Trim($("#sltMFGE").val());
                    var DC = Trim($("#txtDCE").val());
                    var POQuantity = Trim($("#txtPOQuantityE").val());
                    var ArrivedQty = Trim($("#txtArrivedQtyE").val());
                    var AlreadyQty = Trim($("#txtAlreadyQtyE").val());
                    var StockQty = Trim($("#txtStockQtyE").val());
                    var BuyStandardCurrency = Trim($("#sltBuyStandardCurrencyE").val());
                    var BuyPrice = Trim($("#txtBuyPriceE").val());
                    var BuyCost = Trim($("#txtBuyCostE").val());
                    var ROHS = Trim($("#sltROHSE").val());
                    var SupplierID = Trim($("#sltSupplierIDE").val());
                    var VCD = Trim($("#txtVCDE").val());
                    var VendorPaymentTypeID = Trim($("#sltVendorPaymentTypeIDE").val());
                    var BuyExchangeRate = Trim($("#txtBuyExchangeRateE").val());
                    var BuyRealCurrency = Trim($("#sltBuyRealCurrencyE").val());
                    var BuyRealPrice = Trim($("#txtBuyRealPriceE").val());
                    var OtherFee = Trim($("#txtOtherFeeE").val());
                    var OtherFeeRemark = Trim($("#txtareaOtherFeeRemarkE").val());
                    var IsPaySupplier = Trim($("#sltIsPaySupplierE").val());
                    var ShipmentDate = Trim($("#txtShipmentDateE").val());
                    //var State = Trim($("#sltStateE").val());

                    //if (type != "Add" && PONO == "") {
                    //    artDialog.tips("采购订单号不能为空!");
                    //    return false;
                    //}
                    if (MPN == "") {
                        artDialog.tips("厂家标准型号不能为空!");
                        return false;
                    }

                    if (parseInt(POQuantity) <= 0) {
                        artDialog.tips("采购数量必填且为大于0的整数!");
                        return false;
                    }

                    if (ROHS == "") {
                        artDialog.tips("是否环保不能为空!");
                        return false;
                    }

                    var url = "/Handle/OMS/POPlanHandle.ashx";
                    //新增采购计划
                    var data = { "meth": "AddPOPlan", "PONO": PONO, "CPN": CPN, "MPN": MPN, "MFG": MFG, "DC": DC, "POQuantity": POQuantity, "ArrivedQty": ArrivedQty, "AlreadyQty": AlreadyQty, "StockQty": StockQty, "BuyStandardCurrency": BuyStandardCurrency, "BuyPrice": BuyPrice, "BuyCost": BuyCost, "ROHS": ROHS, "SupplierID": SupplierID, "VCD": VCD, "VendorPaymentTypeID": VendorPaymentTypeID, "BuyExchangeRate": BuyExchangeRate, "BuyRealCurrency": BuyRealCurrency, "BuyRealPrice": BuyRealPrice, "OtherFee": OtherFee, "OtherFeeRemark": OtherFeeRemark, "IsPaySupplier": IsPaySupplier, "ShipmentDate": ShipmentDate };
                    //编辑采购计划
                    if (id && id != "") {
                        data = { "meth": "EditPOPlan", "PONO": PONO, "CPN": CPN, "MPN": MPN, "MFG": MFG, "DC": DC, "POQuantity": POQuantity, "ArrivedQty": ArrivedQty, "AlreadyQty": AlreadyQty, "StockQty": StockQty, "BuyStandardCurrency": BuyStandardCurrency, "BuyPrice": BuyPrice, "BuyCost": BuyCost, "ROHS": ROHS, "SupplierID": SupplierID, "VCD": VCD, "VendorPaymentTypeID": VendorPaymentTypeID, "BuyExchangeRate": BuyExchangeRate, "BuyRealCurrency": BuyRealCurrency, "BuyRealPrice": BuyRealPrice, "OtherFee": OtherFee, "OtherFeeRemark": OtherFeeRemark, "IsPaySupplier": IsPaySupplier, "ShipmentDate": ShipmentDate, "Id": id };
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
    </script>
    <!--确认采购报价/数量、QC确认出货等操作-->
    <script type="text/javascript">
        //操作公共函数
        function CommonFunction(msg, funcMeth) {
            art.dialog.confirm(msg, function () {
                var obj = $("#FlexigridTable").getSelectedRows();
                var idList = "";
                if (obj.length <= 0) {
                    artDialog.tips("未选择任何行。");
                    return true;
                }

                for (var i = 0; i < obj.length; i++) {
                    idList += obj[i][1] + ",";
                }
                idList = idList.substring(0, idList.length - 1); //取得选择行ID
                //调用取消采购计划方法
                var url = "/Handle/OMS/POPlanHandle.ashx";
                var data = { "meth": funcMeth, "IdList": idList };
                var successFun = function (json) {
                    if (!json) {
                        artDialog.alert("网络异常，请稍后重试。");
                        return false;
                    }
                    if (json.State == "0") {
                        if (funcMeth == "CancelPOPlan") {
                            artDialog.notice("已完成进货与已取消的计划不能进行取消操作，请知悉。<br>当前" + json.Info);
                            $("#FlexigridTable").flexReload();
                            return true;
                        }
                        artDialog.alert(json.Info);

                        return false;
                    } else {
                        //刷新表格
                        artDialog.notice(json.Info);
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

        //自动生成采购计划
        function GeneratePOPlan() {

            var msg = "系统将对未完成出库的客户订单进行比对库存、检索当前有效采购计划、<br/>智能分析当前还需进行采购的产品以及自动生成采购计划数据。<br/>您确认要由系统完成这一操作吗？";
            art.dialog.confirm(msg, function () {
                //调用取消采购计划方法
                var url = "/Handle/OMS/POPlanHandle.ashx";
                var data = { "meth": "GeneratePOPlan" };
                var successFun = function (json) {
                    if (!json) {
                        artDialog.alert("网络异常，请稍后重试。");
                        return false;
                    }
                    if (json.State == "0") {
                        artDialog.alert(json.Info);
                        return false;
                    } else {
                        //刷新表格
                        artDialog.notice(json.Info);
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

        //取消采购计划
        function CancelPOPlan() {
            CommonFunction("采购计划取消后将无法恢复，您确认要取消吗？", "CancelPOPlan");
        }
        //确认采购报价/数量        
        function ConfirmPrice() {
            CommonFunction("确认采购报价/数量后，采购计划将进入待采购审核状态。<br>您确认此操作吗？", "ConfirmPrice");
        }
        //QC审核
        function QCConfirm(Id, type) {
            var titleValue = "采购计划管理>>QC确认进货";
            if (type == "Confirm")
                titleValue = "采购计划管理>>QC确认进货";
            else
                titleValue = "采购计划管理>>下载QC附件";
            $.dialog({
                id: 'divQCConfirm',
                title: titleValue,
                width: 600,
                top: 10,
                padding: 5,
                content: $('#divQCConfirm').get(0),
                init: function () {
                    if (type == "Confirm") {
                        $("#btnPackageDown").hide();
                    }
                    else {
                        $("#btnPackageDown").show();
                    }
                    $("#txtArrivedQtyQC").val("0");
                    $("#txtShipmentDateQC").val("")
                },
                ok: function () {
                    if (type != "Confirm")
                        return true;

                    var ArrivedQty = Trim($("#txtArrivedQtyQC").val());
                    var ShipmentDate = Trim($("#txtShipmentDateQC").val());

                    if (ArrivedQty == "") {
                        artDialog.tips("到货数量不能为空!");
                        return false;
                    }
                    if (ShipmentDate == "") {
                        artDialog.tips("进货日期不能为空!");
                        return false;
                    }

                    //取得所有上传的附件列表，文件URL,以'|'分隔
                    var AttachmentFiles = "";
                    $(".aFile").each(function () {
                        AttachmentFiles += $(this).find("a").attr("href") + "|";
                    });

                    var url = "/Handle/OMS/POPlanHandle.ashx";
                    //新增客户订单
                    var data = { "meth": "QCConfirm", "AttachmentFiles": AttachmentFiles, "ArrivedQty": ArrivedQty, "ShipmentDate": ShipmentDate, "Id": Id };
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    var successFun = function (json) {
                        if (json.State == "0") {
                            artDialog.alert(json.Info);
                            return false;
                        } else {
                            art.dialog.list["divQCConfirm"].close();
                            artDialog.notice(json.Info);
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
    </script>
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
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
                            <div id="divIsPaySupplier" style="float: left;">
                                <div class="spandivAdjust">
                                    供应商付款：
                                </div>
                                <select id="sltIsPaySupplier" class="txt" runat="server" style="width: 125px;">
                                    <option value="">--请选择--</option>
                                    <option value="1">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                            <div id="divPaymentTypeID" style="float: left;">
                                <div class="spandiv">
                                    付款方式：
                                </div>
                                <select id="sltPaymentTypeID" class="txt" runat="server" style="width: 125px">
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
                            <div id="divCPN" style="float: left;">
                                <div class="spandiv">
                                    客户型号：
                                </div>
                                <input style="width: 120px;" id="txtCPN" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divMPN" style="float: left;">
                                <div class="spandivAdjust">
                                    厂家标准型号：
                                </div>
                                <input style="width: 120px;" id="txtMPN" class="txt" runat="server" maxlength="16" />m
                            </div>
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
                                    <option value="0" title="答复交期">答复交期</option>
                                    <option value="1" title="出货日期">出货日期</option>
                                    <option value="2" title="创建时间" selected="selected">创建时间</option>
                                    <option value="3" title="更新时间">更新时间</option>
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
                    <asp:Button runat="server" Text="自动生成采购计划" CssClass="btnHigh" ID="btnGeneratePOPlan" />
                    <asp:Button runat="server" Text="新增临时草稿" CssClass="btnHigh" ID="btnAdd" />
                    <asp:Button runat="server" Text="编辑" CssClass="btnHigh" ID="btnEdit" />
                    <asp:Button runat="server" Text="取消" CssClass="btnHigh" ID="btnCancel" />
                    <asp:Button runat="server" Text="确认采购报价/数量" CssClass="btnHigh" ID="btnConfirmPrice" />
                    <asp:Button runat="server" Text="QC确认" CssClass="btnHigh" ID="btnQCConfirm" />
                    <asp:Button runat="server" Text="下载QC附件" CssClass="btnHigh" ID="btnDownload" />
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
                <%--新增/编辑/查看采购计划对话框--%>
                <div id="divEdit">
                    <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">采购订单号：
                            </td>
                            <td class="tdLeft
tdRemarkWidth">
                                <input type="text" id="txtPONOE" class="txt " maxlength="16" />
                                <label class="red">
                                    *</label>
                            </td>
                            <td class="tdLeft" colspan="2">温馨提示：关联采购订单后自动填充采购订单号</td>
                        </tr>
                        <tr>
                            <td class="tdLeft tdHead" colspan="4">产品信息<span id="spanProductCodeE"></span>
                            </td>
                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">厂家标准型号：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtMPNE" class="txt" maxlength="32" />
                                <label class="red">
                                    *</label>
                            </td>
                            <td class="tdRight tdParamDWidth">客户型号：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtCPNE" class="txt" maxlength="32" />
                            </td>
                        </tr>
                        <tr class="trMO ">
                            <td class="tdRight tdParamDWidth">生产年份：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtDCE" class="txt " maxlength="16" />
                            </td>
                            <td class="tdRight tdParamDWidth">是否环保：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <select class="txt" id="sltROHSE" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="true">是</option>
                                    <option value="false">否</option>
                                </select>
                            </td>
                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">品牌名称：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <select class="txt" id="sltMFGE" runat="server" style="width: 105px">
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth">采购数量：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtPOQuantityE" class="txt OnlyInt tdRight" maxlength="9" />
                            </td>

                        </tr>
                        <tr class="trMO">

                            <td class="tdRight tdParamDWidth">已到货数量：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtArrivedQtyE" class="txt OnlyInt tdRight disEdit" maxlength="9" />
                            </td>
                            <td class="tdRight tdParamDWidth">已入库数量：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtStockQtyE" class="txt OnlyAllInt tdRight disEdit" maxlength="9" />
                            </td>
                        </tr>

                        <%--<tr class="trMO">
                             <td class="tdRight tdParamDWidth">客户订单数量：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtCustQuantityE" class="txt OnlyInt tdRight disEdit" maxlength="9" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdLeft tdHead" colspan="4">向供应商购买信息
                            </td>
                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">供应商：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <select id="sltSupplierIDE" class="txt disEdit" runat="server" style="width: 200px">
                                    <option value="">--请选择--</option>
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth">供应商交期：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input class="txt" id="txtVCDE" type="text" onclick="WdatePicker()" style="width: 100px" />
                                <img alt="" onclick="WdatePicker({el:'txtVCDE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                    width="16" height="22" align="absmiddle" />
                            </td>
                        </tr>
                        <tr class="trMO">
                            <%--<td class="tdRight tdParamDWidth">付款方式：
                            </td>--%>
                            <td class="tdLeft tdRemarkWidth" style="display: none;">
                                <select class="txt" id="sltVendorPaymentTypeIDE" runat="server" style="width: 200px">
                                    <option value="">--请选择--</option>
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth">买汇率：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtBuyExchangeRateE" class="txt OnlyFloat tdRight" maxlength="9" />
                            </td>
                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">实际买货币：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <select class="txt" id="sltBuyRealCurrencyE" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth">实际买价：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtBuyRealPriceE" class="txt OnlyFloat tdRight" maxlength="23" />
                            </td>

                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">标准买货币：
                            </td>
                            <td class="tdLeft">
                                <select class="txt" id="sltBuyStandardCurrencyE" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth">标准买价：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtBuyPriceE" class="txt OnlyFloat tdRight" maxlength="23" />
                            </td>
                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">标准买货成本：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtBuyCostE" class="txt OnlyFloat tdRight" maxlength="23" />
                            </td>
                            <td class="tdRight tdParamDWidth">其他费用(USD)：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input type="text" id="txtOtherFeeE" class="txt OnlyFloat tdRight" maxlength="23" />
                            </td>
                        </tr>
                        <tr class="trMO">
                            <td class="tdRight tdParamDWidth">其他费用备注：
                            </td>
                            <td colspan="3">
                                <input type="text" id="txtareaOtherFeeRemarkE" class="txt" style="width: 430px" maxlength="1024" />
                            </td>
                        </tr>
                        <tr class="trHide">
                            <td class="tdLeft tdHead" colspan="4">其他信息
                            </td>
                        </tr>
                        <tr class="trMO trHide">
                            <td class="tdRight tdParamDWidth">向供应商付款：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <select id="sltIsPaySupplierE" class="txt disEdit" runat="server" style="width: 125px">
                                    <option value="true">是</option>
                                    <option value="false" selected="selected">否</option>
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth">进货日期：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <input class="txt disEdit" id="txtShipmentDateE" type="text" onclick="WdatePicker()"
                                    style="width: 100px" />
                                <img alt="" onclick="WdatePicker({el:'txtShipmentDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                    width="16" height="22" align="absmiddle" />
                            </td>
                        </tr>
                        <tr class="trMO trHide">
                            <td class="tdRight tdParamDWidth">订单状态：
                            </td>
                            <td class="tdLeft tdRemarkWidth">
                                <select class="txt" id="sltStateE" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </td>
                            <td class="tdRight tdParamDWidth"></td>
                            <td class="tdLeft tdRemarkWidth"></td>
                        </tr>
                    </table>
                </div>
                <%--上传附件--%>
                <div id="divImportFile" style="display: none;">
                    <iframe id="frameImportFile" src="" frameborder="0" scrolling="no" style="width: 475px; height: 360px;"></iframe>
                    <br />
                    <span class="red">温馨提示：附件支持多文件上传，请点击Upload图标选择本地文件。单个文件最大5M。</span>
                </div>
                <%--QC确认/下载附件--%>
                <div id="divQCConfirm" style="display: none;">
                    <div style="height: 30px;" id="divQCInfo">
                        <%--<div id="divUpload" style="float: left; height: 30px; padding-left: 10px; vertical-align: bottom;">
                        上传附件《点击
                    </div>--%>
                        <div style="float: left; padding-left: 50px; height: 30px; padding-top: 5px;">
                            <input id="btnUpload" type="button" class="btnHigh" value="上传附件" />
                            已到货数量：
                        <input type="text" id="txtArrivedQtyQC" class="txt OnlyInt tdRight" style="width: 80px"
                            maxlength="9" />
                            进货日期：
                        <input class="txt disEdit" id="txtShipmentDateQC" type="text" onclick="WdatePicker()"
                            style="width: 100px" />
                            <img alt="" onclick="WdatePicker({el:'txtShipmentDateQC'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                width="16" height="22" align="absmiddle" />
                        </div>
                    </div>
                    <div class="divAttachment">
                    </div>
                    <div>
                        <input id="btnPackageDown" type="button" class="btnHigh" value="下载打包文件" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
