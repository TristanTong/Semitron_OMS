<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrderDetailManager.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.OMS.CustomerOrderDetailManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 客户清单明细</title>
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
            var gridHeight = document.documentElement.clientHeight - 198;
            //初始化查询条件收起张开事件
            InitExpandSearch("FlexigridTable", "btnSearchOpenClose", "tableSearch", 198, 144);
            //初始代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/OMS/CustomerOrderDetailHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetCustomerOrderDetail" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }, { name: "AvailFlag", value: $("#sltAvailFlag").val() }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                   { display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                  { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
                        { display: '公司抬头', name: 'CompanyName', width: 160, sortable: true, align: 'left' },
                        { display: '客户订单状态', name: 'State', width: 100, sortable: true, align: 'center' },
                        { display: '附件数', name: 'FileNum', width: 40, sortable: true, align: 'right' },
                        { display: '内部订单号', name: 'InnerOrderNO', width: 100, sortable: true, align: 'center' },
                        { display: '客户单号', name: 'CustomerOrderNO', width: 100, sortable: true, align: 'center' },
                        { display: '公司销售', name: 'InnerSalesMan', width: 60, sortable: true, align: 'left' },
                        { display: '指定公司采购', name: 'AssignToInnerBuyer', width: 80, sortable: true, align: 'left' },
                        { display: '客户名称', name: 'CustomerName', width: 160, sortable: true, align: 'left' },
                        { display: '客户型号', name: 'CPN', width: 100, sortable: true, align: 'left' },
	    				{ display: '厂家标准型号', name: 'MPN', width: 100, sortable: true, align: 'left' },
	    				{ display: '品牌名称', name: 'MFG', width: 100, sortable: true, align: 'left' },
                        { display: '生产年份', name: 'DC', width: 100, sortable: true, align: 'center' },
                        { display: '是否环保', name: 'ROHS', width: 60, sortable: true, align: 'center' },
                        { display: '要求交期', name: 'CRD', width: 65, sortable: true, align: 'center' },
                        { display: '出货日期', name: 'ShipmentDate', width: 65, sortable: true, align: 'center' },
                        { display: '客户入库日期', name: 'CustomerInStockDate', width: 80, sortable: true, align: 'center' },
                        { display: '购买数量', name: 'CustQuantity', width: 60, sortable: true, align: 'left' },
                        { display: '卖汇率', name: 'SaleExchangeRate', width: 60, sortable: true, align: 'left' },
                        { display: '实际卖货币', name: 'SaleRealCurrency', width: 60, sortable: true, align: 'left' },
                        { display: '实际卖价', name: 'SaleRealPrice', width: 60, sortable: true, align: 'left' },
                        { display: '标准卖货币', name: 'SaleStandardCurrency', width: 80, sortable: true, align: 'center' },
                        { display: '卖价', name: 'SalePrice', width: 60, sortable: true, align: 'left' },
                        //{ display: '其他费用', name: 'OtherFee', width: 100, sortable: true, align: 'center' },
                        //{ display: '其他费用备注', name: 'OtherFeeRemark', width: 100, sortable: true, align: 'left' },
                        { display: '产品编码', name: 'ProductCode', width: 60, sortable: true, align: 'left' },
                        { display: '供应商名称', name: 'SupplierName', width: 100, sortable: true, align: 'left' },
                        { display: '产品厂商型号', name: 'ProductMPN', width: 120, sortable: true, align: 'left' },
                        { display: '出货计划单号', name: 'ShippingPlanNo', width: 100, sortable: true, align: 'left' },
                        { display: '出货计划数量', name: 'PlanQty', width: 70, sortable: true, align: 'right' },
                        { display: '出库单号', name: 'ShippingListNo', width: 100, sortable: true, align: 'left' },
                        { display: '出库单数量', name: 'OutQty', width: 60, sortable: true, align: 'right' },
                        { display: '出库单时间', name: 'OutStockDate', width: 70, sortable: true, align: 'left' },
                        { display: '出库仓编码', name: 'StockCode', width: 60, sortable: true, align: 'left' },
                        { display: '出库仓名称', name: 'WName', width: 100, sortable: true, align: 'left' },
                        { display: '清单创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '清单更新时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' }
                ],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "客户清单明细列表",
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
                var obj = GetSelectedRow("FlexigridTable", "");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前记录已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetCurrencyTypeById(id)
                AddOrEdit(id, "Edit");
                return false;
            });
            //删除
            $("#btnDelete").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前已无效，无法完成操作。");
                    return false;
                }
                var id = obj[0][1];
                DelCurrencyType(id);
                return false;
            });
        });
    </script>
    <%--查询、新增、编辑、删除记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var InnerOrderNO = $("#txtInnerOrderNO").val();
            var CustomerOrderNO = $("#txtCustomerOrderNO").val();
            var CustomerID = $("#sltCustomerID").val();
            var AvailFlag = $("#sltAvailFlag").val();
            var MPN = $("#txtMPN").val();
            var CPN = $("#txtCPN").val();
            var ShippingPlanNo = $("#txtShippingPlanNo").val();
            var ShippingListNo = $("#txtShippingListNo").val();
            var ProductCode = $("#txtProductCode").val();
            var OrderState = $("#sltOrderState").val();
            
            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetCustomerOrderDetail" },
                  { name: "TimeType", value: TimeType },
                  { name: "startTime", value: startTime },
                  { name: "endTime", value: endTime },
                  { name: "InnerOrderNO", value: InnerOrderNO },
                  { name: "CustomerOrderNO", value: CustomerOrderNO },
                  { name: "CustomerID", value: CustomerID },
                  { name: "AvailFlag", value: AvailFlag },
                  { name: "MPN", value: MPN },
                  { name: "CPN", value: CPN },
                  { name: "ShippingPlanNo", value: ShippingPlanNo },
                  { name: "ShippingListNo", value: ShippingListNo },
                  { name: "ProductCode", value: ProductCode },
                  { name: "OrderState", value: OrderState }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
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
                            <div id="divInnerOrderNO" style="float: left;">
                                <div class="spandiv">
                                    内部订单号：
                                </div>
                                <input style="width: 120px;" id="txtInnerOrderNO" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divCustomerOrderNO" style="float: left;">
                                <div class="spandivAdjust">
                                    客户订单号：
                                </div>
                                <input style="width: 120px;" id="txtCustomerOrderNO" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divCustomerID" style="float: left;">
                                <div class="spandiv">
                                    客户名称：
                                </div>
                                <select id="sltCustomerID" class="txt" runat="server" style="width: 125px;">
                                </select>
                            </div>
                            <div id="divState" style="float: left;">
                                <div class="spandivAdjust">
                                    是否有效：
                                </div>
                                <select id="sltAvailFlag" class="txt" runat="server" style="width: 125px">
                                    <option value="">--请选择--</option>
                                    <option value="1" selected="selected">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <div id="divShippingPlanNo" style="float: left;">
                                <div class="spandiv">
                                    出货计划号：
                                </div>
                                <input style="width: 120px;" id="txtShippingPlanNo" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divShippingListNo" style="float: left;">
                                <div class="spandivAdjust">
                                    出库单号：
                                </div>
                                <input style="width: 120px;" id="txtShippingListNo" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="divProductCode" style="float: left;">
                                <div class="spandiv">
                                    产品编码：
                                </div>
                                <input style="width: 120px;" id="txtProductCode" class="txt" runat="server" maxlength="16" />m
                            </div>
                            <div id="div4" style="float: left;">
                                <div class="spandivAdjust">
                                    订单状态：
                                </div>
                                <select id="sltOrderState" class="txt" runat="server" style="width: 125px">
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
                                <select class="txt" id="sltTimeType" runat="server" style="width: 80px">
                                    <option value="1" title="创建时间" selected="selected">创建时间</option>
                                    <option value="2" title="更新时间">更新时间</option>
                                    <option value="3" title="出货日期">出货日期</option>
                                    <option value="4" title="客户入库日期">客户入库日期</option>
                                </select>
                                <asp:Button CssClass="btnHigh" ID="btnSearch" runat="server" Text="查询" />
                                <input name="重置" type="reset" class="btnHigh" value="重置" id="btnReset" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 34px; display: none;" id="divOperation">
                <div class="OperationDiv" style="float: left;">
                    <%--<asp:Button runat="server" Text="新增" CssClass="btnHigh" ID="btnAdd" />
                <asp:Button runat="server" Text="编辑" CssClass="btnHigh" ID="btnEdit" />
                <asp:Button runat="server" Text="删除" CssClass="btnHigh" ID="btnDelete" />--%>
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
            <!--新增/编辑/查看对话框-->
            <div id="divEdit">
            </div>
        </div>
    </form>
</body>
</html>
