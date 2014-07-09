<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndustryTrendManager.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.Site.IndustryTrendManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>网站后台系统 - 行业动态管理</title>
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
    <script charset="utf-8" src="/Scripts/kindeditor-4.1.7/kindeditor.js" type="text/javascript"></script>
    <script charset="utf-8" src="/Scripts/kindeditor-4.1.7/lang/zh_CN.js" type="text/javascript"></script>
    <script charset="utf-8" src="/Scripts/kindeditor-4.1.7/plugins/code/prettify.js"
        type="text/javascript"></script>
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
            //初始行业动态代码表格
            $("#FlexigridTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/Site/IndustryTrendHandle.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetIndustryTrend" }, { name: "TimeType", value: $("#sltTimeType").val() }, { name: "startTime", value: $("#txtBeginTime").val() }, { name: "AvailFlag", value: $("#sltAvailFlag").val()}], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                        {display: '行索引', name: 'RowNum', toggle: false, hide: false, iskey: true, width: 40, align: 'center' },
                        { display: '编号', name: 'ID', toggle: false, hide: true, width: 10, align: 'center' },
	    				{ display: '是否有效', name: 'AvailFlag', width: 60, sortable: true, align: 'center' },
                        { display: '编码', name: 'Code', width: 60, sortable: true, align: 'center' },
                        { display: '语言', name: 'Lang', width: 60, sortable: true, align: 'center' },
                        { display: '标题', name: 'Name', width: 200, sortable: true, align: 'left' },
                        { display: '排序编号', name: 'SK', width: 60, sortable: true, align: 'center' },
                        { display: '是否主页显示', name: 'IsShowInMain', width: 80, sortable: true, align: 'center' },
                        { display: '页面高度', name: 'PageHeight', width: 60, sortable: true, align: 'center' },
	    				{ display: 'HTML文件名', name: 'HtmlPath', width: 140, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 120, sortable: true, align: 'center' },
                        { display: '创建人', name: 'CreateUser', width: 60, sortable: true, align: 'center' },
                        { display: '最后修改时间', name: 'UpdateTime', width: 120, sortable: true, align: 'center' },
                        { display: '最后修改人', name: 'UpdateUser', width: 60, sortable: true, align: 'center' }
	    			],
                sortname: "CreateTime",
                sortorder: "DESC",
                title: "行业动态列表",
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
                var obj = GetSelectedRow("FlexigridTable", "行业动态");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前行业动态记录已无效，无法完成此操作。");
                    return false;
                }
                var id = obj[0][1];

                GetIndustryTrendById(id)
                AddOrEdit(id, "Edit");
                return false;
            });
            //删除
            $("#btnDelete").click(function () {
                var obj = GetSelectedRow("FlexigridTable", "行业动态");
                if (!obj) {
                    return false;
                }
                if (obj[0][2] == "无效") {
                    artDialog.tips("当前行业动态已无效，请勿重复操作。");
                    return false;
                }
                var id = obj[0][1];
                DelIndustryTrend(id);
                return false;
            });
        });
    </script>
    <%--查询、新增、编辑、删除行业动态记录--%>
    <script type="text/javascript">
        //查询
        function Search() {
            var TimeType = $("#sltTimeType").val();
            var startTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var Code = $("#txtCode").val();
            var Name = $("#txtName").val();
            var AvailFlag = $("#sltAvailFlag").val();
            var HtmlPath = $("#txtHtmlPath").val();
            var Lang = $("#sltLang").val();

            var p = { extParam: [   //扩展参数
                        {name: "meth", value: "GetIndustryTrend" },
                        { name: "TimeType", value: TimeType },
                        { name: "startTime", value: startTime },
                        { name: "endTime", value: endTime },
                        { name: "Code", value: Code },
                        { name: "Name", value: Name },
                        { name: "AvailFlag", value: AvailFlag },
                        { name: "HtmlPath", value: HtmlPath },
                        { name: "Lang", value: Lang }
                        ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#FlexigridTable").flexOptions(p).flexReload();
        }

        //根据Id获得行业动态记录
        function GetIndustryTrendById(id) {
            var url = "/Handle/Site/IndustryTrendHandle.ashx";
            var data = { "meth": "GetIndustryTrendById", "Id": id };
            var successFun = function (json) {
                if (json.State == "0") {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    $("#txtCodeE").val(json.Code);
                    $("#txtNameE").val(json.Name);
                    $("#txtSKE").val(json.SK);
                    $("#sltIsShowInMainE").val(json.IsShowInMain == true ? "true" : "false");
                    $("#txtHtmlPathE").val(json.HtmlPath);
                    $("#txtPageHeightE").val(json.PageHeight);
                    $("#sltLangE").val(json.Lang);
                    GetHtmlFileCode(json.HtmlPath);
                    return false;
                }
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //新增、编辑、查看行业动态
        function AddOrEdit(id, type) {
            var dialogTitle = "新增行业动态";
            if (type == "Edit") {
                dialogTitle = "编辑行业动态"
            }

            $.dialog({
                id: 'divEdit',
                title: dialogTitle,
                width: 750,
                height: 450,
                padding: 10,
                content: $('#divEdit').get(0),
                init: function () {
                    if (type == "Add") {
                        var url = "/Handle/Site/IndustryTrendHandle.ashx";
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
                                $("#txtCodeE,#txtHtmlPathE").attr("disabled", "disabled");
                                $("#divEdit .txt").each(function () {
                                    $(this).val("");
                                });
                                editor.html('');
                                $("#txtCodeE").val(json.Info);
                                $("#txtHtmlPathE").val(json.Remark);
                                $("#sltIsShowInMainE").val("false");
                                $("#sltLangE").val("en");
                                return true;
                            }
                        };
                        JsAjax(url, data, successFun, errorFun);
                    }
                    if (type == "Edit") {
                        $("#txtCodeE,#txtHtmlPathE").attr("disabled", "disabled");
                    }
                },
                ok: function () {
                    var Code = Trim($("#txtCodeE").val());
                    var Name = Trim($("#txtNameE").val());
                    var SK = Trim($("#txtSKE").val());
                    var IsShowInMain = Trim($("#sltIsShowInMainE").val());
                    var HtmlPath = Trim($("#txtHtmlPathE").val());
                    var PageHeight = Trim($("#txtPageHeightE").val());
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
                    if (parseInt(PageHeight) < 920) {
                        artDialog.tips("页面高度不能小于920，请重新输入!");
                        return false;
                    }

                    var url = "/Handle/Site/IndustryTrendHandle.ashx";
                    //新增行业动态
                    var data = { "meth": "AddIndustryTrend", "Code": Code, "Name": Name, "SK": SK, "IsShowInMain": IsShowInMain, "HtmlPath": HtmlPath, "PageHeight": PageHeight, "Lang": Lang };
                    //编辑行业动态
                    if (id && id != "") {
                        data = { "meth": "EditIndustryTrend", "Code": Code, "Name": Name, "SK": SK, "IsShowInMain": IsShowInMain, "HtmlPath": HtmlPath, "PageHeight": PageHeight, "Lang": Lang, "Id": id };
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
                            SaveHtmlFileCode(HtmlPath);
                            return true;
                        }
                    };
                    JsAjax(url, data, successFun, errorFun);
                    return false;
                },
                cancel: true
            });
        }

        //删除行业动态
        function DelIndustryTrend(id) {
            art.dialog.confirm('行业动态删除后将无法恢复，你确认要删除吗？', function () {
                //调用删除行业动态方法
                var url = "/Handle/Site/IndustryTrendHandle.ashx";
                var data = { "meth": "DelIndustryTrend", "Id": id };
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
    <script type="text/javascript">
        var editor = null;
        //加载富文本编辑框
        KindEditor.ready(function (K) {
            editor = K.create('#content1', {
                cssPath: '/Scripts/kindeditor-4.1.7/plugins/code/prettify.css',
                uploadJson: '/Handle/Site/UploadHandle.ashx?Type=trend',
                fileManagerJson: '/Handle/Site/FileManagerHandle.ashx?Type=trend',
                allowFileManager: true
            });
            K('input[name=clear]').click(function (e) {
                editor.html('');
            });
            prettyPrint();
        });

        //获取HTML文件源代码
        function GetHtmlFileCode(HtmlPath) {
            var url = "/Handle/Site/IndustryTrendHandle.ashx";
            var data = { "meth": "GetHtmlFileCode", "HtmlPath": HtmlPath, "Lang": $("#sltLangE").val() };
            $.ajax({
                url: url,
                type: "POST",
                dataType: "text",
                data: data,
                success: function (data) {
                    editor.html(data);
                }
            });
        }

        //获取HTML文件源代码
        function SaveHtmlFileCode(HtmlPath) {
            var url = "/Handle/Site/IndustryTrendHandle.ashx";
            var data = { "meth": "SaveHtmlFileCode", "HtmlPath": HtmlPath, "FileCode": editor.html(), "Lang": $("#sltLangE").val() };
            var successFun = function (json) {
                if (!json || json.State == 0) {
                    artDialog.alert(json.Info);
                    return false;
                } else {
                    artDialog.tips(json.Info);
                    art.dialog.list["divEdit"].close();
                    $("#FlexigridTable").flexReload();
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
<body>
    <form id="form1" runat="server">
    <div>
        <div class="SearchDiv">
            <table style="width: 100%" id="tableSearch">
                <tr>
                    <td style="text-align: right;">
                        <div id="divCode" style="float: left;">
                            <div class="spandiv">
                                编码：</div>
                            <input style="width: 120px;" id="txtCode" class="txt  " runat="server" maxlength="16" />m
                        </div>
                        <div id="divName" style="float: left;">
                            <div class="spandiv">
                                标题：</div>
                            <input style="width: 120px;" id="txtName" class="txt  " runat="server" maxlength="128" />m
                        </div>
                        <div id="divHtmlPath" style="float: left;">
                            <div class="spandivAdjust">
                                HTML文件名：</div>
                            <input style="width: 120px;" id="txtHtmlPath" class="txt  " runat="server" maxlength="16" />m
                        </div>
                        <div id="divLang" style="float: left;">
                            <div class="spandivAdjust">
                                语言：</div>
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
                                是否有效：</div>
                            <select id="sltAvailFlag" class="txt " runat="server" style="width: 125px">
                                <option value="">--请选择--</option>
                                <option value="1" selected="selected">有效</option>
                                <option value="0">无效</option>
                            </select>
                        </div>
                        <div id="divStartTime" style="float: left;">
                            <div class="spandivAdjust">
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
        <!--新增/编辑/查看行业动态对话框-->
        <div id="divEdit">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        编码：
                    </td>
                    <td class="tdLeft tdRemarkWidth ">
                        <input type="text" id="txtCodeE" class="txt   txtdis" maxlength="16" />
                    </td>
                    <td class="tdRight tdParamDWidth">
                        标题：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtNameE" class="txt   " maxlength="128" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        排序编号：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtSKE" class="txt   " maxlength="16" />
                    </td>
                    <td class="tdRight tdParamDWidth">
                        语言：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <select class="txt" id="sltLangE" runat="server" style="width: 85px">
                            <option value="cn" selected="selected">中文</option>
                            <option value="en">English</option>
                        </select>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        HTML文件名：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtHtmlPathE" class="txt" maxlength="16" />
                    </td>
                    <td class="tdRight tdParamDWidth">
                        页面高度：
                    </td>
                    <td class="tdLeft tdRemarkWidth">
                        <input type="text" id="txtPageHeightE" class="txt tdRight OnlyInt" maxlength="4" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="trMO ">
                    <td class="tdRight tdParamDWidth">
                        是否首页显示：
                    </td>
                    <td class="tdLeft tdRemarkWidth" colspan="3">
                        <select class="txt" id="sltIsShowInMainE" runat="server" style="width: 85px">
                            <option value="true">是</option>
                            <option value="false" selected="selected">否</option>
                        </select>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <div id="KindEditor">
                <textarea id="content1" cols="100" rows="8" style="width: 700px; height: 300px; visibility: hidden;"
                    runat="server"></textarea>
                <input type="button" name="clear" value="清空内容" class="btnHigh" />
                <input type="reset" name="reset" value="重置" class="btnHigh" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
