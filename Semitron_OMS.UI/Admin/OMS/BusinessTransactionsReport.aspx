<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessTransactionsReport.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.OMS.BusinessTransactionsReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 商务交易总表</title>
    <%--CSS Ref--%>
    <link rel="stylesheet" type="text/css" href="/Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/fancybox.css" />
    <link rel="stylesheet" href="/Styles/custom.css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <%--JS Ref--%>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.core-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.excheck-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.exedit-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
    <%--初始表格值与权限偏好控制--%>
    <script type="text/javascript">
        var arrGridHeader = [["公司抬头", "CompanyName"], ["内部单号", "InnerOrderNO"], ["销 售", "InnerSalesMan"], ["客户名称", "CustomerName"],
            ["客户采购", "CustomerBuyer"], ["客户订单号", "CustomerOrderNO"], ["客户订单日期", "CustOrderDate"], ["客户型号", "COCPN"],
            ["厂商型号", "COMPN"], ["品牌", "COMFG"], ["DC", "CODC"], ["ROHS要求", "COROHS"],
            ["订单数量", "CustQuantity"], ["semitron未下采购单数量", "XQuantity"], ["客户期望交期", "CRD"], ["卖汇率", "SaleExchangeRate"],
            ["实际卖货币", "SaleRealCurrency"], ["实际卖价", "SaleRealPrice"], ["标准卖货币", "SaleStandardCurrency"], ["卖价（USD）", "SalePrice"], ["标准售价总金额（USD）", "SaleStandardTotal"], ["客户付款方式", "COPaymentType"], ["客户付款", "IsCustomerPay"], ["是否对客户开增票", "IsCustomerVATInvoice"],
            ["采购订单号", "PONO"], ["采购订单日期", "POOrderDate"], ["采购", "InnerBuyer"], ["供 应 商", "SupplierName"], ["供应商付款方式", "POPaymentType"], ["供应商付款", "IsPaySupplier"], ["供应商是否开增票", "IsSupplierVATInvoice"], ["厂商型号", "POMPN"], ["品牌", "POMFG"], ["DC", "PODC"], ["ROHS", "POROHS"], ["采购数量", "POQuantity"], ["订单交期", "POCDate"], ["买汇率", "BuyExchangeRate"], ["实际买货币", "BuyRealCurrency"], ["实际买价", "BuyRealPrice"], ["标准买货币", "BuyStandardCurrency"], ["买价(USD)", "BuyPrice"], ["买货成本(USD)", "BuyCost"], ["供应商确认交期", "VCD"], ["已到货数量", "ArrivedQty"], ["已发货数量", "AlreadyQty"], ["未交货数量", "WaitQty"], ["semitron库存", "StockQty"], ["收款汇率", "IncomeRate"], ["实际收款总金额货币单位", "TotalFeeCurrencyUnit"], ["应收客户款", "CustomerFeeIn"], ["标准货币单位", "IncomeStandardCurrency"], ["标准应收客户款", "StandardCustomerFeeIn"], ["实际收款货币单位", "RealInCurrencyUnit"], ["客户实际付款金额", "CustomerRealPayFee"], ["标准实收货币单位", "StandardRealInCurrencyUnit"], ["标准客户实际付款金额", "StandardCustomerRealPayFee"], ["应收实收差额(USD)", "StandardIncomeRealDiff"], ["毛利润(USD)", "GrossProfits"], ["其它物流费用(USD)", "OtherFee"], ["其它费用备注", "OtherFeeRemark"], ["净利润(USD)", "NetProfit"], ["利润率", "ProfitMargin"], ["实际净利润(USD)", "RealNetProfit"], ["QC是否通过", "QCPass"], ["到货日期", "ReceiveDate"], ["出货日期", "ShipmentDate"], ["订单完成", "IsComplete"], ["其他发票信息备注", "OtherRemark"]
        ]; //所有要显示的列名.以默认值设定表格的标题

        var arrSearchShow = [["公司抬头", "divCorporationID"], ["客户名称", "divCustomerID"], ["客户订单状态", "divCOState"], ["供应商", "divSupplierID"], ["采购订单状态", "divPOState"], ["内部订单号", "divInnerOrderNO"], ["客户订单号", "divCustomerOrderNO"], ["客户采购", "divCustomerBuyer"], ["公司销售", "divInnerSalesMan"], ["采购订单号", "divPONO"], ["客户订单品牌", "divCustBrand"]]; //所有待显示的查询条件div布局id

        var allSearchControlIds = "sltCorporationID,sltCustomerID,sltCOState,sltSupplierID,sltPOState,stlCustBrand,txtInnerOrderNO,txtCustomerOrderNO,txtCustomerBuyer,txtInnerSalesMan,txtPONO"; //所有查询条件的控件id,偏好设定时以key:value形式存入数据库保存默认查询条件值

        arrHeaderStyle = [["CompanyName", "160", "", "left"], ["InnerOrderNO", "80", "", "center"], ["InnerSalesMan", "50", "", "left"], ["CustomerName", "160", "", "left"], ["CustomerBuyer", "50", "", "left"], ["CustomerOrderNO", "80", "", "center"], ["COCPN", "120", "", "left"], ["COMPN", "120", "", "left"], ["COMFG", "80", "", "left"], ["XQuantity", "130", "", "right"], ["SaleStandardTotal", "130", "", "right"], ["COPaymentType", "130", "", "left"], ["PONO", "80", "", "center"], ["POPaymentType", "200", "", "left"], ["TotalFeeCurrencyUnit", "130", "", "center"], ["RealInCurrencyUnit", "100", "", "center"], ["CustomerRealPayFee", "120", "", "right"], ["StandardRealInCurrencyUnit", "110", "", "center"], ["StandardCustomerRealPayFee", "120", "", "right"], ["StandardIncomeRealDiff", "120", "", "right"], ["OtherRemark", "300", "", "left"], ["OtherFee", "90", "", "right"], ["SupplierName", "160", "", "left"], ["IsCustomerVATInvoice", "100", "", "center"], ["IsSupplierVATInvoice", "100", "", "center"]]

        var allGroupControlIds = "";  //所有的分组查询条件控件id,偏好设定时以"，"将id分开保存
        var strPreferGroupParam = ""; //设定全局偏好分组条件id字串,用于当前页面查询使用
        var strPreferSearchParam = ""; //所有的查询条件控件偏好设置默认值,以"Key:Value,"组合
        var o_columnPrefer = "";      //全局偏好显示列
    </script>
    <script type="text/javascript">
        $(function () {
            parent.document.title = document.title;
            SetValuesForHide(); //设定权限值
            SetSearchItems(); //根据权限设置查询条件
            LoadPreferenceCondition("OMS/BusinessTransactionsReport.aspx");  //加载所有使用的个人偏好（查询条件与表格列） 

            //查询
            $("#btnSearch").click(function () {
                Search();
                return false;
            });

            //偏好设定
            $("#btnPreference").click(function () {
                SetPreference("OMS/BusinessTransactionsReport.aspx");
                return false;
            });

            //导出
            $("#btnExportExcel").click(function () {
                //控制查询需带上时间
                var startTime = $("#txtBeginTime").val();
                var endTime = $("#txtEndTime").val();
                if (startTime == "") {
                    artDialog.tips("请选择开始时间。");
                    return false;
                }

                var dialogTitle = "商务交易总表>>导出商务交易总表到Excel";
                //查询条件值
                var searchStr = GetSearchKeyValueString(false);
                var groupStr = GetGroupKeyValueString();

                $("#divWaiting")[0].style.display = "";
                $("#divDisable")[0].style.display = "";
                var url = "/Handle/OMS/BusinessTransactionsReportHandle.ashx";
                var data = {
                    "meth": "GetReportList", "SearchParam": searchStr, "GroupParam": groupStr, "Type": "ExportExcel", "sortname": "CustOrderDate", "sortorder": "desc", "rp": "100000", "page": "1"
                };
                var errorFun = function (x, e) {
                    alert(x.responseText);
                };
                var successFun = function (json) {
                    $("#divWaiting")[0].style.display = "none";
                    $("#divDisable")[0].style.display = "none";
                    if (json.State == "0") {
                        artDialog.alert(json.Info);
                        return false;
                    } else {
                        $("#ExcelUrl").attr("href", json.Remark)
                        $.dialog({
                            id: 'ExportFile',
                            title: dialogTitle,
                            content: $('#divExportFile').get(0)

                        });
                        return false;
                    }
                    return false;
                };
                JsAjax(url, data, successFun, errorFun);
                return false;
            });
        });                        //end $ 

        function windowsclose() {
            art.dialog.list['ExportFile'].close();
        }
    </script>
    <%--查询--%>
    <script type="text/javascript">
        //取得分组条件键值对字符串,以此传递哪些分组条件被勾选上
        function GetGroupKeyValueString() {
            var groupStr = "";

            return groupStr;
        }

        //通过偏好字条串值得到分组条件
        function GetPreferGroupKeyValueString(strPreferGroup) {
            var groupStr = "";

            return groupStr;
        }

        //取得分组条件所选项，无论偏好如何恒显示对应的列名
        function GetGroupConditionColumn(groupStr) {
            var strColumn = "";
            if (groupStr != null) {

            }
            return strColumn;
        }

        function Search() {
            //控制查询需带上时间
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            if (startTime == "") {
                artDialog.tips("请选择开始时间。");
                return false;
            }
            //查询条件值
            var searchStr = GetSearchKeyValueString(false);
            var groupStr = GetGroupKeyValueString();
            var p = {
                extParam: [{ name: "meth", value: "GetReportList" }, { name: "SearchParam", value: searchStr }, { name: "GroupParam", value: groupStr }]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }

        //获取查询条件，传递给后台存储过程
        function GetSearchKeyValueString(isLoadFirst) {
            var CorporationID = $("#sltCorporationID").val();
            var CustomerID = $("#sltCustomerID").val();
            var COState = $("#sltCOState").val();
            var SupplierID = $("#sltSupplierID").val();
            var POState = $("#sltPOState").val();
            var InnerOrderNO = $("#txtInnerOrderNO").val();
            var CustomerOrderNO = $("#txtCustomerOrderNO").val();
            var CustomerBuyer = $("#txtCustomerBuyer").val();
            var InnerSalesMan = $("#txtInnerSalesMan").val();
            var PONO = $("#txtPONO").val();
            var BeginTime = $("#txtBeginTime").val();
            var EndTime = $("#txtEndTime").val();
            var TimeType = $("#sltTimeType").val();
            var CustBrand = $("#stlCustBrand").val();
            //查询条件值
            var searchStr = "CorporationID:" + CorporationID + ",CustomerID:" + CustomerID + ",COState:" + COState + ",SupplierID:" + SupplierID + ",POState:" + POState + ",InnerOrderNO:" + InnerOrderNO + ",CustomerOrderNO:" + CustomerOrderNO + ",CustomerBuyer:" + CustomerBuyer + ",InnerSalesMan:" + InnerSalesMan + ",PONO:" + PONO + ",BeginTime:" + BeginTime + ",EndTime:" + EndTime + ",TimeType:" + TimeType + ",CustBrand:" + CustBrand;
            return searchStr;
        }

        function LoadFlexiTable(columnPrefer, strPreferGroup) {
            //查询条件值
            var searchStr = GetSearchKeyValueString(true);
            var groupStr = "";

            //SetGridHeader(groupStr); //设定表格标题数组

            var gridHeight = document.documentElement.clientHeight - 230;
            //初始化查询条件收起张开事件
            InitExpandSearch("FlexigridTable", "btnSearchOpenClose", "tableSearch", 230, 140);
            //初始化流量数据统计表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/BusinessTransactionsReportHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetReportList" }, { name: "SearchParam", value: searchStr }, { name: "GroupParam", value: groupStr }], //扩展参数
                colModel: eval("[" + GetDefaultPreferArray(columnPrefer) + "]"), //表格的题头与所要填充的内容。 
                sortname: "CustOrderDate",
                sortorder: "desc",
                title: "商务交易总表",
                usepager: true,
                showToggleBtn: true,
                useRp: true,
                showcheckbox: false,
                selectedonclick: true,
                singleselected: true,
                rowbinddata: true
            });
        }
    </script>
    <%-- 权限控制与偏好设定 --%>
    <script type="text/javascript">
        //初始加载偏好控件值
        function InitPreferenceControls(searchParam) {

        }

        //根据偏好分组条件设定表格标题数组,通过更改全局变量数组达到动态变更表格列标题
        function SetGridHeader(groupStr) {

        }

        //加载个人偏好查询条件显示、查询条件默认值及分组条件默认值。表格列控制直接在GetDefaultPreferArray()中。
        function LoadPreferenceCondition(pageCode) {
            //根据登陆帐号与页面取得所有设定项
            var url = "/Handle/Sys/PreferenceConfig.ashx";
            var data = { "meth": "GetPreferenceConfigByCode", "PageCode": pageCode };
            var successFun = function (json) {
                var columnPrefer = "";
                if (json) {
                    //在权限范围内加载偏好查询条件
                    InitSearchShow(json.SearchShow);
                    //在权限范围内加载偏好查询条件默认值
                    InitSearchDefaultValue(json.SearchParam);
                    //在权限范围内加载偏好分组条件
                    InitGroupParam(json.GroupParam);
                    strPreferGroupParam = json.GroupParam; //设定全局偏好分组条件id字串,用于当前页面查询使用
                    strPreferSearchParam = json.SearchParam; //设定全局偏好
                    columnPrefer = json.ColumnShow;
                    o_columnPrefer = json.ColumnShow;
                    InitPreferenceControls(strPreferSearchParam); //按偏好加载设定级联控件值
                }

                //需加载偏好列,分组条件
                LoadFlexiTable(columnPrefer, strPreferGroupParam);
                //根据分组条件,控制查询条件是否启用
                InitSearchControlByGroup();
                return;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
                return;
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //设置个人偏好弹出框弹出时控件的状态
        function ShowControlStatus(bool) {
            if (!bool) {
                $("#divTime").show();
                $("#divBtn2").show();
                $("#tdColumnTree").hide();
                $("#tdSearchTree").hide();
            }
            else {
                $("#divTime").hide();
                $("#divBtn2").hide();
                $("#tdColumnTree").show();
                $("#tdSearchTree").show();
            }
        }

        //设定个人偏好各查询条件及值与分组条件
        function SetPreference(pageCode) {
            //根据登陆帐号与页面取得所有设定项
            var url = "/Handle/Sys/PreferenceConfig.ashx";
            var data = { "meth": "GetPreferenceConfigByCode", "PageCode": pageCode };
            var successFun = function (json) {
                if (json) {
                    //初如化显示表格列树
                    zColumnNodes = GetColumnTreesNotes(json.ColumnShow);
                    $.fn.zTree.init($("#ColumnTree"), setting, zColumnNodes);
                    zColumnTree = $.fn.zTree.getZTreeObj("ColumnTree");
                    //初如化显示查询条件树
                    zSearchNodes = GetSearchTreesNotes(json.SearchShow);
                    $.fn.zTree.init($("#SearchTree"), setting, zSearchNodes);
                    zSearchTree = $.fn.zTree.getZTreeObj("SearchTree");

                    //在权限范围内加载偏好查询条件
                    InitSearchShow(json.SearchShow);
                    //在权限范围内加载偏好查询条件默认值
                    InitSearchDefaultValue(json.SearchParam);
                    //在权限范围内加载偏好分组条件
                    InitGroupParam(json.GroupParam);
                    InitSearchControlByGroup();
                    return true;
                }
                else {
                    artDialog.tips("未配置个人偏好项。");
                    return false;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
                return false;
            };
            JsAjax(url, data, successFun, errorFun);

            ShowControlStatus(true); //控件的状态
            $.dialog({
                content: $("#tableSearch").get(0),
                id: 'tableSearch',
                title: "用户个人偏好设定",
                width: 450,
                padding: 0,
                ok: function () {
                    var obj = zColumnTree.getCheckedNodes(true);  //获取被选中的表格列树结点
                    var columnIdList = "";
                    if (obj.length <= 0) {
                        artDialog.tips("未选择任何列名。");
                        return false;
                    }

                    for (var i = 0; i < obj.length; i++) {
                        columnIdList += obj[i].id + ",";
                    }
                    columnIdList = columnIdList.substring(0, columnIdList.length - 1); //所有的偏好显示列

                    var objSearch = zSearchTree.getCheckedNodes(true);  //获取被选中的查询条件树结点
                    if (objSearch.length <= 0) {
                        artDialog.tips("未选择任何查询条件。");
                        return false;
                    }
                    ShowControlStatus(false);
                    var searchIdList = "";
                    for (var i = 0; i < objSearch.length; i++) {
                        searchIdList += objSearch[i].id + ",";
                    }
                    searchIdList = searchIdList.substring(0, searchIdList.length - 1); //所有的偏好查询条件

                    //所有查询条件的key:value值
                    var searchKeyValue = GetSearchPreferKeyValue();
                    //所有分组条件的偏好id值
                    var groupPrefer = GetGroupPrefer();

                    //保存个人偏好配置项
                    var url = "/Handle/Sys/PreferenceConfig.ashx";
                    var data = { "meth": "AddOrUpdatePreferenceConfig", "PageCode": pageCode, "SearchShow": searchIdList, "SearchParam": searchKeyValue, "ColumnShow": columnIdList, "GroupParam": groupPrefer };
                    var successFun = function (json) {
                        if (json.State == 0) {
                            artDialog.alert(json.Info);
                            return false;
                        }
                        artDialog.tips(json.Info);
                        //重新加载整个页面
                        window.location.href = "BusinessTransactionsReport.aspx";
                        return true;
                    };
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    JsAjax(url, data, successFun, errorFun);
                },
                cancel: function () {
                    ShowControlStatus(false);
                    LoadPreferenceCondition(pageCode);
                    Search();
                    return true;
                }
            });
        }

        //根据分组条件,控制查询条件是否启用
        function InitSearchControlByGroup() {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" runat="server" id="hidUrlParam" />
            <asp:HiddenField ID="hidDataSource" runat="server" />
            <asp:HiddenField ID="allHideCodes" runat="server" />
            <asp:HiddenField ID="hfSearchParams" runat="server" />
            <div class="SearchDiv">
                <table style="width: 100%" id="tableSearch">
                    <tr>
                        <td style="text-align: right;">
                            <div id="divCorporationID" style="float: left;">
                                <div class="spandivAdjust">
                                    公司抬头：
                                </div>
                                <select id="sltCorporationID" class="txt" runat="server" style="width: 125px;">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divCustomerID" style="float: left;">
                                <div class="spandivAdjust">
                                    客户名称：
                                </div>
                                <select id="sltCustomerID" class="txt" runat="server" style="width: 125px;">
                                </select>
                            </div>
                            <div id="divCOState" style="float: left;">
                                <div class="spandivAdjust">
                                    客户订单状态：
                                </div>
                                <select id="sltCOState" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divCustBrand" style="float: left;">
                                <div class="spandivAdjust">
                                    客户订单品牌：
                                </div>
                                <select id="stlCustBrand" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divSupplierID" style="float: left;">
                                <div class="spandivAdjust">
                                    供应商：
                                </div>
                                <select id="sltSupplierID" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
                            <div id="divPOState" style="float: left;">
                                <div class="spandivAdjust">
                                    采购订单状态：
                                </div>
                                <select id="sltPOState" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                </select>
                            </div>
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
                            <div id="divPONO" style="float: left;">
                                <div class="spandivAdjust">
                                    采购订单号：
                                </div>
                                <input style="width: 110px;" id="txtPONO" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divTime" style="float: left;">
                                <div class="spandivAdjust">
                                    开始日期：
                                </div>
                                <input id="txtBeginTime" runat="server" type="text" style="width: 120px" onclick="WdatePicker({ startDate: '%y-%M-%d 00:00:00', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })" />
                                <img alt="" onclick="WdatePicker({el:'txtBeginTime',startDate:'%y-%M-%d 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                    src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                    align="absmiddle" />
                                结束日期：
                            <input id="txtEndTime" runat="server" type="text" style="width: 120px" onclick="WdatePicker({ startDate: '%y-%M-%d 23:59:59', dateFmt: 'yyyy-MM-dd HH:mm:ss', alwaysUseStartDate: true })" />
                                <img alt="" onclick="WdatePicker({el:'txtEndTime',startDate:'%y-%M-%d 23:59:59',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"
                                    src="../../Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22"
                                    align="absmiddle" />
                                <select id="sltTimeType">
                                    <option value="1" selected="selected">客户订单日期 </option>
                                    <option value="2">采购订单日期 </option>
                                    <option value="3">客户期望交期 </option>
                                    <option value="4">供应商确认交期 </option>
                                </select>
                            </div>
                            <div style="float: left;" id="divBtn2">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button CssClass="btnHigh" ID="btnSearch" runat="server" Text="查询" />
                                <input id="btnPreference" type="button" value="偏好设定" class="btnHigh" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="zTreeDemoBackground" align="center" style="display: none; width: 45%; float: left"
                                id="tdSearchTree">
                                <span style="font-weight: 700;">显示查询条件:</span>
                                <ul id="SearchTree" class="ztree" style="height: 250px; width: 200px">
                                </ul>
                            </div>
                            <div class="zTreeDemoBackground" align="center" style="display: none; width: 45%; float: left"
                                id="tdColumnTree">
                                <span style="font-weight: 700;">显示表格列:</span>
                                <ul id="ColumnTree" class="ztree" style="height: 250px; width: 200px">
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 30px" id="divOperation">
                <div class="OperationDiv" style="float: left;">
                    <asp:Button runat="server" Text="导出到Excel" CssClass="btnHigh" ID="btnExportExcel" />
                    <asp:Button runat="server" Text="打印PDF" CssClass="btnHigh" ID="btnPrintPdf" />
                </div>
                <div style="float: right; width: 46px; padding: 10px 5px 5px 5px;">
                    <asp:LinkButton ID="btnSearchOpenClose" runat="server" Font-Size="13px" Text="收起△"
                        ForeColor="Blue"></asp:LinkButton>
                </div>
            </div>
            <div class="ListTable">
                <table id="FlexigridTable" style="float: left;">
                </table>
            </div>
        </div>
        <div class="spClear">
        </div>
        <div class="dialogDiv">
            <%--导出--%>
            <div id="divExportFile" style="display: none;">
                <strong>Excel文件下载地址</strong>
                <br />
                <a id="ExcelUrl" target="_blank" href="" onclick="return windowsclose();">下载</a>
            </div>
        </div>
        <div id="divWaiting" style="display: none; z-index: 1100; left: 25%; right: 25%; top: 25%; position: absolute; text-align: center; width: 50%;">
            <img src="/Scripts/flexigrid-1.1/css/images/flexigrid/indicator_waitanim.gif" alt="" /><span
                style="color: Red;">正在生成Excel数据文件，请勿进行其他操作...</span>
        </div>
        <div id="divDisable" style="display: none; width: 100%; height: 100%; z-index: 1000; position: absolute; left: 0px; top: 0px; filter: alpha(opacity=50); background-color: White">
        </div>
    </form>
</body>
</html>
