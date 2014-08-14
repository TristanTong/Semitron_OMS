<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderFeeManager.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.OMS.OrderFeeManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 订单计费管理</title>
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
                width: 80px;
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
            var gridHeight = document.documentElement.clientHeight - 245;
            //初始化查询条件收起张开事件
            InitExpandSearch("FlexigridTable", "btnSearchOpenClose", "tableSearch", 245, 143);
            //初始订单费用代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/OrderFeeHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetOrderFee" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }, { name: "AvailFlag", value: $("#sltAvailFlag").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                        { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                        { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '是否有效', name: 'AvailFlag', width: 60, sortable: true, align: 'center' },
                        { display: '公司抬头', name: 'CompanyName', width: 180, sortable: true, align: 'left' },
                        { display: '附件数', name: 'FileNum', width: 40, sortable: true, align: 'right' },
                        { display: '客户是否付款', name: 'IsCustomerPay', width: 80, sortable: true, align: 'center' },
                        { display: '是否对客户开增票', name: 'IsCustomerVATInvoice', width: 100, sortable: true, align: 'center' },
                        { display: '客户名称', name: 'CustomerName', width: 160, sortable: true, align: 'left' },
                        { display: '内部订单号', name: 'InnerOrderNO', width: 100, sortable: true, align: 'center' },
                        { display: '客户订单号', name: 'CustomerOrderNO', width: 100, sortable: true, align: 'left' },
                        { display: '客户型号', name: 'CPN', width: 140, sortable: true, align: 'left' },
                        { display: '订单数量', name: 'CustQuantity', width: 100, sortable: true, align: 'right' },
	    				{ display: '是否向供应商付款', name: 'IsPaySupplier', width: 100, sortable: true, align: 'center' },
                        { display: '供应商是否开增票', name: 'IsSupplierVATInvoice', width: 100, sortable: true, align: 'center' },
                        { display: '供应商名称', name: 'SupplierName', width: 160, sortable: true, align: 'left' },
                        { display: '采购订单号', name: 'PONO', width: 100, sortable: true, align: 'center' },
                        { display: '厂商型号', name: 'MPN', width: 140, sortable: true, align: 'left' },
                        { display: '采购数量', name: 'POQuantity', width: 100, sortable: true, align: 'right' },
                        { display: '毛利润(USD)', name: 'GrossProfits', width: 80, sortable: true, align: 'right' },
                        { display: '净利润(USD)', name: 'NetProfit', width: 80, sortable: true, align: 'right' },
                        { display: '利润率(%)', name: 'ProfitMargin', width: 80, sortable: true, align: 'right' },
                        { display: '应收总金额货币单位', name: 'TotalFeeCurrencyUnit', width: 130, sortable: true, align: 'center' },
                        { display: '应收客户款', name: 'CustomerFeeIn', width: 80, sortable: true, align: 'right' },
                        { display: '收款汇率', name: 'IncomeRate', width: 80, sortable: true, align: 'right' },
                        { display: '标准应收货币单位', name: 'IncomeStandardCurrency', width: 100, sortable: true, align: 'center' },
                        { display: '标准应收客户款', name: 'StandardCustomerFeeIn', width: 100, sortable: true, align: 'right' },
                        { display: '实际收款货币单位', name: 'RealInCurrencyUnit', width: 100, sortable: true, align: 'center' },
                        { display: '客户实际付款金额', name: 'CustomerRealPayFee', width: 100, sortable: true, align: 'right' },
                        { display: '标准实收货币单位', name: 'StandardRealInCurrencyUnit', width: 100, sortable: true, align: 'center' },
                        { display: '标准客户实际付款金额', name: 'StandardCustomerRealPayFee', width: 120, sortable: true, align: 'right' },
                        { display: '标准应收实收美金差额', name: 'StandardIncomeRealDiff', width: 120, sortable: true, align: 'right' },
                        { display: '其他费用', name: 'OtherFee', width: 80, sortable: true, align: 'right' },
                        { display: '回款日期', name: 'FeeBackDate', width: 65, sortable: true, align: 'center' },
                        { display: '客户入库日期', name: 'CustomerInStockDate', width: 80, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '更新时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "订单费用列表",
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
                AddOrEdit("", "Add", "");
                return false;
            });
            //编辑按钮
            $("#btnEdit").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "订单费用");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前订单费用记录已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetOrderFeeById(id, "Edit")
                AddOrEdit(id, "Edit", obj[0][3]);
                return false;
            });
            //查看按钮
            $("#btnView").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "订单费用");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];

                GetOrderFeeById(id, "View")
                AddOrEdit(id, "View", obj[0][3]);
                return false;
            });
            //删除
            $("#btnDel").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "订单费用");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前订单费用无效，请勿重复操作。");
                    return false;
                }
                var id = obj[0][1];
                DelOrderFee(id);
                return false;
            });
            //上传附件
            $("#btnUpload").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "订单费用");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前订单费用无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];
                UploadDownload(id, "Upload");
                GetOrderFeeById(id, "Upload");
                $("#divUploadInfo").show();
                return false;
            });
            //下载QC附件
            $("#btnDownload").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "订单费用");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];
                UploadDownload(id, "Download");
                GetOrderFeeById(id, "Download");
                $("#divUploadInfo").hide();
                return false;
            });
            //上传附件
            $("#btnUploadFile").click(function () {
                $("#frameImportFile").attr("src", "/Admin/UserControl/UploadifyPage.aspx?ButtonText=Browse&FileExt=*.*;&FileDesc=File Formart(.*)&Folder=OrderFeeFilePath&IsAutoDisappear=true&IsMulti=true");
                PopUploadControlDialog("订单计费管理>>上传附件");
            });
            //计算标准价
            $("#txtCustomerFeeInE,#txtIncomeRateE,#txtCustomerRealPayFeeE,#txtOtherFeeE").blur(function () {
                var value = parseFloat($("#txtCustomerFeeInE").val()) / parseFloat($("#txtIncomeRateE").val());
                var strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtStandardCustomerFeeInE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));

                value = parseFloat($("#txtCustomerRealPayFeeE").val()) / parseFloat($("#txtIncomeRateE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtStandardCustomerRealPayFeeE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));

                value = parseFloat($("#txtStandardCustomerRealPayFeeE").val()) - parseFloat($("#txtStandardCustomerFeeInE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtStandardIncomeRealDiffE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));

                //毛利润=标准售价总金额-标准买货总金额
                value = parseFloat($("#txtSaleTotalPriceE").val()) - parseFloat($("#txtBuyCostE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtGrossProfitsE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));
                //净利润=标准售价总金额-标准买货总金额-其他物流费用
                value = parseFloat($("#txtSaleTotalPriceE").val()) - parseFloat($("#txtBuyCostE").val()) - parseFloat($("#txtOtherFeeE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtNetProfitE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));
                //实际净利润=净利润+实收应收差额(USD)
                value = parseFloat($("#txtNetProfitE").val()) + parseFloat($("#txtStandardIncomeRealDiffE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                $("#txtRealNetProfitE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));
                value = parseFloat($("#txtSalePriceE").val()) / parseFloat($("#txtBuyPriceE").val());
                strvalue = value.toString();
                if (strvalue == "NaN") strvalue = "0";
                else strvalue = ((value - 1) * 100).toString();
                $("#txtProfitMarginE").val(strvalue.substr(0, (strvalue.indexOf('.') == -1 ? (strvalue.length) : (strvalue.indexOf('.') + 5))));

                return false;
            });

            //弹出统计帮助文档
            $("#aHelpStatistic").click(function () {
                $.dialog({
                    id: 'divHelpStatistic',
                    title: "统计说明",
                    width: 650,
                    padding: 5,
                    content: $('#divHelpStatistic').get(0),
                    cancel: true
                });
            });
        });
    </script>
    <%--查询、新增、编辑、删除订单费用记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var CustomerID = $("#sltCustomerID").val();
            var SupplierID = $("#sltSupplierID").val();
            var AvailFlag = $("#sltAvailFlag").val();
            var InnerOrderNO = $("#txtInnerOrderNO").val();
            var PONO = $("#txtPONO").val();
            var CPN = $("#txtCPN").val();
            var MPN = $("#txtMPN").val();
            var CustomerOrderNO = $("#txtCustomerOrderNO").val();
            var IsCustomerPay = $("#sltIsCustomerPay").val();
            var IsPaySupplier = $("#sltIsPaySupplier").val();
            var CorporationID = $("#sltCorporationID").val();
            var IsCustomerVATInvoice = $("#sltIsCustomerVATInvoice").val();
            var IsSupplierVATInvoice = $("#sltIsSupplierVATInvoice").val();

            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetOrderFee" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "CustomerID", value: CustomerID },
                  { name: "SupplierID", value: SupplierID },
                  { name: "AvailFlag", value: AvailFlag },
                  { name: "InnerOrderNO", value: InnerOrderNO },
                  { name: "PONO", value: PONO },
                  { name: "CPN", value: CPN },
                  { name: "MPN", value: MPN },
                  { name: "CustomerOrderNO", value: CustomerOrderNO },
                  { name: "IsCustomerPay", value: IsCustomerPay },
                  { name: "IsPaySupplier", value: IsPaySupplier },
                  { name: "CorporationID", value: CorporationID },
                  { name: "IsCustomerVATInvoice", value: IsCustomerVATInvoice },
                  { name: "IsSupplierVATInvoice", value: IsSupplierVATInvoice }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }
        //根据Id获得订单记录
        function GetOrderFeeById(id, type) {
            var url = "/Handle/OMS/OrderFeeHandle.ashx";
            var data = { "meth": "GetOrderFeeById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#sltCustomerIDE").val(json.CustomerID);
                    $("#txtInnerOrderNOE").val(json.InnerOrderNO);
                    $("#txtCPNE").val(json.CPN);
                    $("#txtSaleExchangeRateE").val(json.SaleExchangeRate);
                    $("#sltSaleRealCurrencyE").val(json.SaleRealCurrency);
                    $("#txtSaleRealPriceE").val(json.SaleRealPrice);
                    $("#txtSaleRealTotalPriceE").val(json.SaleRealPrice * json.CustQuantity);
                    $("#sltSaleStandardCurrencyE").val(json.SaleStandardCurrency);
                    $("#txtSalePriceE").val(json.SalePrice);
                    $("#txtOtherFee1E").val(json.OtherFee1);
                    $("#sltSupplierIDE").val(json.SupplierID);
                    $("#txtPONOE").val(json.PONO);
                    $("#txtMPNE").val(json.MPN);
                    $("#txtBuyExchangeRateE").val(json.BuyExchangeRate);
                    $("#sltBuyRealCurrencyE").val(json.BuyRealCurrency);
                    $("#txtBuyRealPriceE").val(json.BuyRealPrice);
                    $("#txtBuyRealTotalPriceE").val(json.BuyRealPrice * json.POQuantity);
                    $("#sltBuyStandardCurrencyE").val(json.BuyStandardCurrency);
                    $("#txtBuyPriceE").val(json.BuyPrice);
                    $("#txtBuyCostE").val(json.BuyCost);
                    $("#txtOtherFee2E").val(json.OtherFee2);
                    $("#sltTotalFeeCurrencyUnitE").val(json.TotalFeeCurrencyUnit);
                    $("#txtCustomerFeeInE").val(json.CustomerFeeIn);
                    $("#txtIncomeRateE").val(json.IncomeRate);
                    $("#sltIncomeStandardCurrencyE").val(json.IncomeStandardCurrency);
                    $("#txtStandardCustomerFeeInE").val(json.StandardCustomerFeeIn);
                    $("#sltRealInCurrencyUnitE").val(json.RealInCurrencyUnit != "" ? json.RealInCurrencyUnit : json.IncomeStandardCurrency);
                    $("#txtCustomerRealPayFeeE").val(json.CustomerRealPayFee);
                    $("#sltStandardRealInCurrencyUnitE").val(json.StandardRealInCurrencyUnit);
                    $("#txtStandardCustomerRealPayFeeE").val(json.StandardCustomerRealPayFee);
                    $("#txtStandardIncomeRealDiffE").val(json.StandardIncomeRealDiff);
                    $("#txtGrossProfitsE").val(json.GrossProfits);
                    $("#txtNetProfitE").val(json.NetProfit);
                    $("#txtRealNetProfitE").val(json.RealNetProfit);
                    $("#txtProfitMarginE").val(json.ProfitMargin);
                    $("#txtOtherFeeE").val(json.OtherFee);
                    $("#txtareaOtherFeeRemarkE").val(json.OtherFeeRemark);
                    $("#txtPOQuantityE").val(json.POQuantity);
                    $("#txtCustQuantityE").val(json.CustQuantity);
                    $("#txtCustomerOrderNOE").val(json.CustomerOrderNO);
                    $("#txtSaleTotalPriceE").val(json.SaleTotalPrice);
                    $("#txtareaOtherRemarkE").val(json.OtherRemark);
                    $("#sltIsCustomerPayE").val(json.IsCustomerPay == true ? "true" : "false");
                    $("#sltIsPaySupplierE").val(json.IsPaySupplier == true ? "true" : "false");
                    $("#sltPaymentTypeIDE").val(json.PaymentTypeID);
                    $("#sltIsCustomerVATInvoiceE").val(json.IsCustomerVATInvoice == true ? "true" : "false");
                    $("#txtCustomerVATInvoiceNoE").val(json.CustomerVATInvoiceNo);
                    $("#sltVendorPaymentTypeIDE").val(json.VendorPaymentTypeID);
                    $("#sltIsSupplierVATInvoiceE").val(json.IsSupplierVATInvoice == true ? "true" : "false");
                    $("#txtSupplierVATInvoiceE").val(json.SupplierVATInvoice);
                    $("#txtTraceNumberE").val(json.TraceNumber);
                    $("#txtCustomerInStockDateE").val(json.CustomerInStockDate == null ? "" : json.CustomerInStockDate.toString().substr(0, 10));
                    $("#txtFeeBackDateE").val(json.FeeBackDate == null ? "" : json.FeeBackDate.toString().substr(0, 10));
                    $("#txtSalesManProportionE").val(json.SalesManProportion);
                    $("#txtSalesManPayE").val(json.SalesManPay);
                    $("#txtBuyerProportionE").val(json.BuyerProportion);
                    $("#txtBuyerPayE").val(json.BuyerPay);
                    $("#sltSupplierCorporationIDE").val(json.SupplierCorporationID);

                    if (type == "Download" || type == "Upload") {
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
                    return true;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看订单费用
        function AddOrEdit(id, type, corName) {
            var dialogTitle = "订单计费管理>>新增订单费用";
            if (type == "Edit") {
                dialogTitle = "订单计费管理>>编辑订单费用>>" + corName;
            }
            if (type == "View") {
                dialogTitle = "订单计费管理>>查看订单费用>>" + corName;
            }

            $.dialog({
                id: 'divEdit',
                title: dialogTitle,
                width: 900,
                height: 450,
                padding: 10,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        $("#sltSaleStandardCurrencyE").val("1");
                        $("#sltBuyStandardCurrencyE").val("1");
                        $("#sltIncomeStandardCurrencyE").val("1");
                        $("#sltStandardRealInCurrencyUnitE").val("1");
                    }
                    if (type == "Edit") {
                        $("#divEdit .txt").each(function () {
                            $(this).removeAttr("disabled");
                        });
                        $("#divEdit .txtdis").each(function () {
                            $(this).attr("disabled", "disabled");
                        });
                        $("#txtareaOtherFeeRemarkE,#txtareaOtherRemarkE").removeAttr("disabled");
                    }
                    if (type == "View") {
                        $("#divEdit .txt").each(function () {
                            $(this).attr("disabled", "disabled");
                        });

                        $("#txtareaOtherFeeRemarkE,#txtareaOtherRemarkE").attr("disabled", "disabled");
                    }
                    $("#sltSaleStandardCurrencyE").attr("disabled", "disabled");
                    $("#sltBuyStandardCurrencyE").attr("disabled", "disabled");
                    $("#sltIncomeStandardCurrencyE").attr("disabled", "disabled");
                    $("#sltStandardRealInCurrencyUnitE").attr("disabled", "disabled");
                },
                ok: function () {
                    if (type == "View") {
                        return true;
                    }
                    var CustomerID = Trim($("#sltCustomerIDE").val());
                    var InnerOrderNO = Trim($("#txtInnerOrderNOE").val());
                    var CPN = Trim($("#txtCPNE").val());
                    var SaleExchangeRate = Trim($("#txtSaleExchangeRateE").val());
                    var SaleRealCurrency = Trim($("#sltSaleRealCurrencyE").val());
                    var SaleRealPrice = Trim($("#txtSaleRealPriceE").val());
                    var SaleStandardCurrency = Trim($("#sltSaleStandardCurrencyE").val());
                    var SalePrice = Trim($("#txtSalePriceE").val());
                    var OtherFee1 = Trim($("#txtOtherFee1E").val());
                    var SupplierID = Trim($("#sltSupplierIDE").text());
                    var PONO = Trim($("#txtPONOE").val());
                    var MPN = Trim($("#txtMPNE").val());
                    var BuyExchangeRate = Trim($("#txtBuyExchangeRateE").val());
                    var BuyRealCurrency = Trim($("#sltBuyRealCurrencyE").val());
                    var BuyRealPrice = Trim($("#txtBuyRealPriceE").val());
                    var BuyStandardCurrency = Trim($("#sltBuyStandardCurrencyE").val());
                    var BuyPrice = Trim($("#txtBuyPriceE").val());
                    var BuyCost = Trim($("#txtBuyCostE").val());
                    var OtherFee2 = Trim($("#txtOtherFee2E").val());
                    var TotalFeeCurrencyUnit = Trim($("#sltTotalFeeCurrencyUnitE").val());
                    var CustomerFeeIn = Trim($("#txtCustomerFeeInE").val());
                    var CustomerRealPayFee = Trim($("#txtCustomerRealPayFeeE").val());
                    var IncomeRate = Trim($("#txtIncomeRateE").val());
                    var IncomeStandardCurrency = Trim($("#sltIncomeStandardCurrencyE").val());
                    var StandardCustomerFeeIn = Trim($("#txtStandardCustomerFeeInE").val());
                    var RealInCurrencyUnit = Trim($("#sltRealInCurrencyUnitE").val());
                    var CustomerRealPayFee = Trim($("#txtCustomerRealPayFeeE").val());
                    var StandardRealInCurrencyUnit = Trim($("#sltStandardRealInCurrencyUnitE").val());
                    var StandardCustomerRealPayFee = Trim($("#txtStandardCustomerRealPayFeeE").val());
                    var StandardIncomeRealDiff = Trim($("#txtStandardIncomeRealDiffE").val());
                    var GrossProfits = Trim($("#txtGrossProfitsE").val());
                    var NetProfit = Trim($("#txtNetProfitE").val());
                    var ProfitMargin = Trim($("#txtProfitMarginE").val());
                    var OtherFee = Trim($("#txtOtherFeeE").val());
                    var OtherFeeRemark = Trim($("#txtareaOtherFeeRemarkE").val());
                    var OtherRemark = Trim($("#txtareaOtherRemarkE").val());
                    var IsCustomerPay = Trim($("#sltIsCustomerPayE").val());
                    var IsPaySupplier = Trim($("#sltIsPaySupplierE").val());
                    var PaymentTypeID = Trim($("#sltPaymentTypeIDE").val());
                    var IsCustomerVATInvoice = Trim($("#sltIsCustomerVATInvoiceE").val());
                    var CustomerVATInvoiceNo = Trim($("#txtCustomerVATInvoiceNoE").val());
                    var VendorPaymentTypeID = Trim($("#sltVendorPaymentTypeIDE").val());
                    var IsSupplierVATInvoice = Trim($("#sltIsSupplierVATInvoiceE").val());
                    var SupplierVATInvoice = Trim($("#txtSupplierVATInvoiceE").val());
                    var TraceNumber = Trim($("#txtTraceNumberE").val());
                    var CustomerInStockDate = Trim($("#txtCustomerInStockDateE").val());
                    var FeeBackDate = Trim($("#txtFeeBackDateE").val());
                    var SalesManProportion = Trim($("#txtSalesManProportionE").val());
                    var SalesManPay = Trim($("#txtSalesManPayE").val());
                    var BuyerProportion = Trim($("#txtBuyerProportionE").val());
                    var BuyerPay = Trim($("#txtBuyerPayE").val());

                    if (PaymentTypeID == "") {
                        artDialog.tips("客户付款方式不能为空!");
                        return false;
                    }
                    if (IsCustomerVATInvoice == "") {
                        artDialog.tips("客户是否开增票不能为空!");
                        return false;
                    }
                    if (IsCustomerPay == "") {
                        artDialog.tips("客户是否付款不能为空!");
                        return false;
                    }
                    if (VendorPaymentTypeID == "") {
                        artDialog.tips("供应商付款方式不能为空!");
                        return false;
                    }
                    if (IsSupplierVATInvoice == "") {
                        artDialog.tips("供应商是否开增票不能为空!");
                        return false;
                    }
                    if (IsPaySupplier == "") {
                        artDialog.tips("是否向供应商付款不能为空!");
                        return false;
                    }
                    if (TotalFeeCurrencyUnit == "") {
                        artDialog.tips("应收货币单位不能为空!");
                        return false;
                    }
                    if (CustomerFeeIn == "") {
                        artDialog.tips("应收客户款不能为空!");
                        return false;
                    }
                    if (IncomeRate == "") {
                        artDialog.tips("收款汇率不能为空!");
                        return false;
                    }
                    if (StandardCustomerFeeIn == "") {
                        artDialog.tips("标准应收客户款不能为空!");
                        return false;
                    }
                    if (RealInCurrencyUnit == "") {
                        artDialog.tips("实际收款货币单位不能为空!");
                        return false;
                    }
                    if (CustomerRealPayFee == "") {
                        artDialog.tips("客户实际付款金额不能为空!");
                        return false;
                    }
                    if (StandardCustomerRealPayFee == "") {
                        artDialog.tips("标准客户实际付款金额不能为空!");
                        return false;
                    }
                    if (StandardIncomeRealDiff == "") {
                        artDialog.tips("实收应收差额(USD)不能为空!");
                        return false;
                    }
                    if (GrossProfits == "") {
                        artDialog.tips("毛利润(USD)不能为空!");
                        return false;
                    }
                    if (NetProfit == "") {
                        artDialog.tips("净利润(USD)不能为空!");
                        return false;
                    }
                    if (ProfitMargin == "") {
                        artDialog.tips("利润率(%)不能为空!");
                        return false;
                    }
                    //                    var isPass = true;
                    //                    $("#divEdit .txt").each(function () {
                    //                        if ($(this).val() == "") {
                    //                            isPass = false;
                    //                            return false;
                    //                        }
                    //                    });
                    //                    if (!isPass) {
                    //                        artDialog.tips("所有输入项不能为空!");
                    //                        return false;
                    //                    }

                    var url = "/Handle/OMS/OrderFeeHandle.ashx";
                    //新增订单费用
                    var data = { "meth": "AddOrderFee", "TotalFeeCurrencyUnit": TotalFeeCurrencyUnit, "CustomerFeeIn": CustomerFeeIn, "CustomerRealPayFee": CustomerRealPayFee, "IncomeRate": IncomeRate, "IncomeStandardCurrency": IncomeStandardCurrency, "StandardCustomerFeeIn": StandardCustomerFeeIn, "RealInCurrencyUnit": RealInCurrencyUnit, "CustomerRealPayFee": CustomerRealPayFee, "StandardRealInCurrencyUnit": StandardRealInCurrencyUnit, "StandardCustomerRealPayFee": StandardCustomerRealPayFee, "StandardIncomeRealDiff": StandardIncomeRealDiff, "GrossProfits": GrossProfits, "NetProfit": NetProfit, "ProfitMargin": ProfitMargin, "OtherFee": OtherFee, "OtherFeeRemark": OtherFeeRemark, "OtherRemark": OtherRemark, "IsCustomerPay": IsCustomerPay, "IsPaySupplier": IsPaySupplier, "PaymentTypeID": PaymentTypeID, "IsCustomerVATInvoice": IsCustomerVATInvoice, "CustomerVATInvoiceNo": CustomerVATInvoiceNo, "VendorPaymentTypeID": VendorPaymentTypeID, "IsSupplierVATInvoice": IsSupplierVATInvoice, "SupplierVATInvoice": SupplierVATInvoice, "TraceNumber": TraceNumber, "CustomerInStockDate": CustomerInStockDate, "FeeBackDate": FeeBackDate, "SalesManProportion": SalesManProportion, "SalesManPay": SalesManPay, "BuyerProportion": BuyerProportion, "BuyerPay": BuyerPay };
                    //编辑订单费用
                    if (id && id != "") {
                        data = { "meth": "EditOrderFee", "TotalFeeCurrencyUnit": TotalFeeCurrencyUnit, "CustomerFeeIn": CustomerFeeIn, "CustomerRealPayFee": CustomerRealPayFee, "IncomeRate": IncomeRate, "IncomeStandardCurrency": IncomeStandardCurrency, "StandardCustomerFeeIn": StandardCustomerFeeIn, "RealInCurrencyUnit": RealInCurrencyUnit, "CustomerRealPayFee": CustomerRealPayFee, "StandardRealInCurrencyUnit": StandardRealInCurrencyUnit, "StandardCustomerRealPayFee": StandardCustomerRealPayFee, "StandardIncomeRealDiff": StandardIncomeRealDiff, "GrossProfits": GrossProfits, "NetProfit": NetProfit, "ProfitMargin": ProfitMargin, "OtherFee": OtherFee, "OtherFeeRemark": OtherFeeRemark, "OtherRemark": OtherRemark, "IsCustomerPay": IsCustomerPay, "IsPaySupplier": IsPaySupplier, "PaymentTypeID": PaymentTypeID, "IsCustomerVATInvoice": IsCustomerVATInvoice, "CustomerVATInvoiceNo": CustomerVATInvoiceNo, "VendorPaymentTypeID": VendorPaymentTypeID, "IsSupplierVATInvoice": IsSupplierVATInvoice, "SupplierVATInvoice": SupplierVATInvoice, "TraceNumber": TraceNumber, "CustomerInStockDate": CustomerInStockDate, "FeeBackDate": FeeBackDate, "SalesManProportion": SalesManProportion, "SalesManPay": SalesManPay, "BuyerProportion": BuyerProportion, "BuyerPay": BuyerPay, "Id": id };
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

        //删除订单费用
        function DelOrderFee(id) {
            art.dialog.confirm('订单费用删除后将无法恢复，你确认要删除吗？', function () {
                //调用删除订单费用方法
                var url = "/Handle/OMS/OrderFeeHandle.ashx";
                var data = { "meth": "DelOrderFee", "Id": id };
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
        //上传下载附件
        function UploadDownload(Id, type) {
            var titleValue = "订单计费管理>>上传附件";
            if (type == "Upload")
                titleValue = "订单计费管理>>上传附件";
            else
                titleValue = "订单计费管理>>下载附件";
            $.dialog({
                id: 'divUploadDownload',
                title: titleValue,
                width: 600,
                top: 10,
                padding: 5,
                content: $('#divUploadDownload').get(0),
                init: function () {
                    if (type == "Upload") {
                        $("#btnPackageDown").hide();
                    }
                    else {
                        $("#btnPackageDown").show();
                    }
                },
                ok: function () {
                    if (type != "Upload")
                        return true;
                    //取得所有上传的附件列表，文件URL,以'|'分隔
                    var AttachmentFiles = "";
                    $(".aFile").each(function () {
                        AttachmentFiles += $(this).find("a").attr("href") + "|";
                    });

                    var url = "/Handle/OMS/OrderFeeHandle.ashx";
                    //新增客户订单
                    var data = { "meth": "UploadFile", "AttachmentFiles": AttachmentFiles, "Id": Id };
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    var successFun = function (json) {
                        if (json.State == "0") {
                            artDialog.alert(json.Info);
                            return false;
                        } else {
                            art.dialog.list["divUploadDownload"].close();
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
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
        <div>
            <div class="SearchDiv">
                <table style="width: 100%" id="tableSearch">
                    <tr>
                        <td style="text-align: right; line-height: 18px;">
                            <div id="divCorporationID" style="float: left;">
                                <div class="spandivAdjust">
                                    公司法人：
                                </div>
                                <select id="sltCorporationID" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divCustomerID" style="float: left;">
                                <div class="spandivAdjust">
                                    客户名称：
                                </div>
                                <select id="sltCustomerID" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divIsCustomerPay" style="float: left;">
                                <div class="spandivAdjust">
                                    客户是否付款：
                                </div>
                                <select id="sltIsCustomerPay" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="1">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                            <div id="divSupplierID" style="float: left;">
                                <div class="spandivAdjust">
                                    供应商名称：
                                </div>
                                <select id="sltSupplierID" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divIsPaySupplier" style="float: left;">
                                <div class="spandivAdjust">
                                    向供应商付款：
                                </div>
                                <select id="sltIsPaySupplier" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="1">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                            <div id="divIsCustomerVATInvoice" style="float: left;">
                                <div class="spandivAdjust">
                                    对客户开增票：
                                </div>
                                <select id="sltIsCustomerVATInvoice" class="txt" runat="server" style="width: 125px">
                                    <option value="" selected="selected">--请选择--</option>
                                    <option value="1">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                            <div id="divIsSupplierVATInvoice" style="float: left;">
                                <div class="spandivAdjust">
                                    供应商开增票：
                                </div>
                                <select id="sltIsSupplierVATInvoice" class="txt" runat="server" style="width: 125px">
                                    <option value="" selected="selected">--请选择--</option>
                                    <option value="1">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                            <div id="divAvailFlag" style="float: left;">
                                <div class="spandivAdjust">
                                    是否有效：
                                </div>
                                <select id="sltAvailFlag" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="1" selected="selected">有效</option>
                                    <option value="0">无效</option>
                                </select>
                            </div>
                            <div id="divInnerOrderNO" style="float: left;">
                                <div class="spandivAdjust">
                                    内部订单号：
                                </div>
                                <input style="width: 110px;" id="txtInnerOrderNO" class="txt  " runat="server" maxlength="16" />m
                            </div>
                            <div id="divCustomerOrderNO" style="float: left;">
                                <div class="spandivAdjust">
                                    客户订单号：
                                </div>
                                <input style="width: 110px;" id="txtCustomerOrderNO" class="txt  " runat="server"
                                    maxlength="16" />m
                            </div>
                            <div id="divCPN" style="float: left;">
                                <div class="spandivAdjust">
                                    客户型号：
                                </div>
                                <input style="width: 110px;" id="txtCPN" class="txt  " runat="server" maxlength="16" />m
                            </div>
                            <div id="divPONO" style="float: left;">
                                <div class="spandivAdjust">
                                    采购订单号：
                                </div>
                                <input style="width: 110px;" id="txtPONO" class="txt  " runat="server" maxlength="16" />m
                            </div>
                            <div id="divMPN" style="float: left;">
                                <div class="spandivAdjust">
                                    厂商型号：
                                </div>
                                <input style="width: 110px;" id="txtMPN" class="txt  " runat="server" maxlength="16" />m
                            </div>
                            <div id="divStartTime" style="float: left; width: 1024px;">
                                <div class="spandivAdjust" style="float: left;">
                                    开始时间：
                                </div>
                                <div style="float: left;">
                                    <input class="txt  " runat="server" id="txtBeginTime" type="text" style="width: 120px;"
                                        onclick="WdatePicker({ startDate: '%y-%M-%d 00:00:00', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })" />
                                    <img alt="" onclick="WdatePicker({el:'txtBeginTime',startDate:'%y-%M-%d 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                        src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                        align="absmiddle" />
                                </div>
                                <div class="spandiv" style="float: left;">
                                    结束时间：
                                </div>
                                <div style="float: left;">
                                    <input class="txt " id="txtEndTime" type="text" style="width: 120px;" onclick="WdatePicker({ startDate: '%y-%M-%d 23:59:59', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })"
                                        runat="server" />
                                    <img alt="" onclick="WdatePicker({el:'txtEndTime',startDate:'%y-%M-%d 23:59:59',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                        src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                        align="absmiddle" />
                                </div>
                                <div style="float: left;">
                                    <select class="txt" id="sltTimeType" runat="server" style="width: 100px;">
                                        <option value="1" title="创建时间" selected="selected">创建时间</option>
                                        <option value="2" title="更新时间">更新时间</option>
                                        <option value="3" title="回款日期">回款日期</option>
                                        <option value="4" title="客户入库日期">客户入库日期</option>
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
                    <%--<asp:Button runat="server" Text="新增" CssClass="btnHigh" ID="btnAdd" />--%>
                    <asp:Button runat="server" Text="编辑" CssClass="btnHigh" ID="btnEdit" />
                    <%--<asp:Button runat="server" Text="删除" CssClass="btnHigh" ID="btnDel" />--%>
                    <asp:Button runat="server" Text="上传附件" CssClass="btnHigh" ID="btnUpload" ToolTip="客户付款发票，采购支付发票，国税发票，快递回执等电子档" />
                    <asp:Button runat="server" Text="下载附件" CssClass="btnHigh" ID="btnDownload" ToolTip="客户付款发票，采购支付发票，国税发票，快递回执等电子档" />
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
        </div>
        <div class="dialogDiv">
            <!--新增/编辑/查看订单费用对话框-->
            <div id="divEdit">
                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="tdLeft tdHead" colspan="2">客户款项
                        </td>
                        <td class="tdRight tdParamDWidth tdHead">客户名称：
                        </td>
                        <td class="tdLeft tdRemarkWidth tdHead" colspan="5">
                            <select class="txt txtdis " id="sltCustomerIDE" runat="server" style="width: 185px">
                            </select>
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">付款方式：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltPaymentTypeIDE" runat="server" style="width: 85px">
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">是否开增票：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltIsCustomerVATInvoiceE" runat="server" style="width: 85px">
                                <option value="false">否</option>
                                <option value="true">是</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">增票号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCustomerVATInvoiceNoE" class="txt  " maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">快递单号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtTraceNumberE" class="txt  " maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">客户是否付款：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltIsCustomerPayE" runat="server" style="width: 85px">
                                <option value="false">否</option>
                                <option value="true">是</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">客户入库日期：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input class="txt" id="txtCustomerInStockDateE" type="text" onclick="WdatePicker()"
                                style="width: 65px" />
                            <img alt="" onclick="WdatePicker({el:'txtCustomerInStockDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                width="16" height="22" align="absmiddle" />
                        </td>
                        <td class="tdRight tdParamDWidth">内部订单号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtInnerOrderNOE" class="txt txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">客户订单号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCustomerOrderNOE" class="txt txtdis" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">客户型号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCPNE" class="txt txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">订单数量：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCustQuantityE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">实际卖货币：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt txtdis" id="sltSaleRealCurrencyE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">实际卖价：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSaleRealPriceE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">实际售价总金额：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSaleRealTotalPriceE" class="txt OnlyFloat tdRight txtdis"
                                maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">卖汇率：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSaleExchangeRateE" class="txt OnlyFloat tdRight txtdis"
                                maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">标准卖货币：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt txtdis" id="sltSaleStandardCurrencyE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">标准卖价：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSalePriceE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">标准售价总金额：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSaleTotalPriceE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">其他费用：
                        </td>
                        <td class="tdLeft tdRemarkWidth" colspan="5">
                            <input type="text" id="txtOtherFee1E" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft tdHead" colspan="2">采购款项
                        </td>
                        <td class="tdRight tdParamDWidth  tdHead">公司抬头：
                        </td>
                        <td class="tdLeft tdRemarkWidth tdHead" colspan="2">
                            <select class="txt" id="sltSupplierCorporationIDE" runat="server" style="width: 220px">
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth tdHead">供应商名称：
                        </td>
                        <td class="tdLeft tdRemarkWidth tdHead" colspan="2">
                            <select class="txt txtdis" id="sltSupplierIDE" runat="server" style="width: 185px">
                            </select>
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">付款方式：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltVendorPaymentTypeIDE" runat="server" style="width: 85px">
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">是否开增票：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltIsSupplierVATInvoiceE" runat="server" style="width: 85px">
                                <option value="false">否</option>
                                <option value="true">是</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">增票号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSupplierVATInvoiceE" class="txt  " maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth"></td>
                        <td class="tdLeft tdRemarkWidth"></td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">是否向供应商付款：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt " id="sltIsPaySupplierE" runat="server" style="width: 85px">
                                <option value="false">否</option>
                                <option value="true">是</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">采购订单号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtPONOE" class="txt txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">厂商型号：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtMPNE" class="txt txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">采购数量：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtPOQuantityE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">实际买货币：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt txtdis" id="sltBuyRealCurrencyE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">实际买价：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBuyRealPriceE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">实际买货总金额：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBuyRealTotalPriceE" class="txt OnlyFloat tdRight txtdis"
                                maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">买汇率：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBuyExchangeRateE" class="txt OnlyFloat tdRight txtdis"
                                maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">标准买货币：
                        </td>
                        <td class="tdLeft">
                            <select class="txt txtdis" id="sltBuyStandardCurrencyE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">标准买价：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBuyPriceE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">标准买货总金额：
                        </td>
                        <td class="tdLeft">
                            <input type="text" id="txtBuyCostE" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">其他费用：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtOtherFee2E" class="txt OnlyFloat tdRight txtdis" maxlength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft tdHead" colspan="8">应收款项
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">应收货币单位：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt " id="sltTotalFeeCurrencyUnitE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">应收客户款：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCustomerFeeInE" class="txt OnlyFloat tdRight " maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">收款汇率：
                        </td>
                        <td class="tdLeft tdRemarkWidth" colspan="3">
                            <input type="text" id="txtIncomeRateE" class="txt OnlyFloat tdRight " maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO">
                        <td class="tdRight tdParamDWidth">标准应收货币单位：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt " id="sltIncomeStandardCurrencyE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">标准应收客户款：
                        </td>
                        <td class="tdLeft tdRemarkWidth" colspan="5">
                            <input type="text" id="txtStandardCustomerFeeInE" class="txt OnlyFloat tdRight "
                                maxlength="16" />
                        </td>
                    </tr>
                    <tr class="">
                        <td class="tdLeft tdHead" colspan="8">实收款项
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">实际收款货币单位：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltRealInCurrencyUnitE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">客户实际付款金额：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtCustomerRealPayFeeE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">标准实收货币单位：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <select class="txt" id="sltStandardRealInCurrencyUnitE" runat="server" style="width: 85px">
                                <option value="">--请选择--</option>
                            </select>
                        </td>
                        <td class="tdRight tdParamDWidth">标准客户实际付款金额：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtStandardCustomerRealPayFeeE" class="txt OnlyFloat tdRight"
                                maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">回款日期：
                        </td>
                        <td class="tdLeft tdRemarkWidth" colspan="7">
                            <input id="txtFeeBackDateE" type="text" onclick="WdatePicker()" class="txt" style="width: 65px" />
                            <img alt="" onclick="WdatePicker({el:'txtFeeBackDateE'})" src="/Scripts/My97DatePicker/skin/datePicker.gif"
                                width="16" height="22" align="absmiddle" />
                        </td>
                    </tr>
                    <tr class="">
                        <td class="tdLeft tdHead" colspan="8">
                            <div>
                                <div style="float: left;">
                                    款项统计
                                </div>
                                <div style="float: right;">
                                    <a href="#" id="aHelpStatistic" style="color: Blue; width: 50px;">>>Help</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">实收应收差额(USD)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtStandardIncomeRealDiffE" class="txt OnlyFloat tdRight"
                                maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">毛利润(USD)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtGrossProfitsE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">净利润(USD)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtNetProfitE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">实际净利润(USD)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtRealNetProfitE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">利润率(%)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtProfitMarginE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">其它物流费用(USD)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtOtherFeeE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">销售提成比例(%)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSalesManProportionE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">销售提成：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtSalesManPayE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight tdParamDWidth">采购提成比例(%)：
                        </td>
                        <td class="tdLeft tdRemarkWidth">
                            <input type="text" id="txtBuyerProportionE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                        <td class="tdRight tdParamDWidth">采购提成：
                        </td>
                        <td class="tdLeft tdRemarkWidth" colspan="5">
                            <input type="text" id="txtBuyerPayE" class="txt OnlyFloat tdRight" maxlength="16" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight" colspan="2" style="line-height: 25px;">其它费用备注：
                        </td>
                        <td colspan="6" style="padding-top: 5px;">
                            <input type="text" id="txtareaOtherFeeRemarkE" maxlength="1024" class="txt" style="width: 500px;" />
                        </td>
                    </tr>
                    <tr class="trMO ">
                        <td class="tdRight" colspan="2" style="line-height: 25px;">其他信息备注(发票号、追踪号等)：
                        </td>
                        <td colspan="6" style="padding-top: 5px;">
                            <input type="text" id="txtareaOtherRemarkE" maxlength="1024" class="txt" style="width: 500px;" />
                        </td>
                    </tr>
                </table>
            </div>
            <%--上传附件--%>
            <div id="divImportFile" style="display: none;">
                <iframe id="frameImportFile" src="" frameborder="0" scrolling="no" style="width: 475px; height: 360px;"></iframe>
                <br />
                <span class="red">温馨提示：附件支持多文件上传，请点击Upload图标选择本地文件。单个文件最大5M。</span>
            </div>
            <%--上传/下载附件--%>
            <div id="divUploadDownload" style="display: none;">
                <div style="height: 30px;" id="divUploadInfo">
                    <div style="float: left; padding-left: 50px; height: 30px; padding-top: 5px;">
                        <input id="btnUploadFile" type="button" class="btnHigh" value="上传附件" />
                    </div>
                </div>
                <div class="divAttachment">
                </div>
                <div>
                    <input id="btnPackageDown" type="button" class="btnHigh" value="下载打包文件" />
                </div>
            </div>
            <div id="divHelpStatistic" style="display: none;">
                <div>
                    毛利润(USD)=标准售价总金额-标准买货总金额<br />
                    净利润(USD)=标准售价总金额-标准买货总金额-其他物流费用(USD)<br />
                    实际净利润(USD)=净利润(USD)+实收应收差额(USD)<br />
                    利润率(%)=(标准卖价/标准买价-1)*100<br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
