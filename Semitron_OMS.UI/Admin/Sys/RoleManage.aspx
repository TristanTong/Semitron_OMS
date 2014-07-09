<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Sys.RoleManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 角色管理</title>
    <%--CSS Ref--%>
    <link href="/Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <link href="../Images/style.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <%--JS Ref--%>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/JS_Wen.js" type="text/javascript"></script>
    <script src="/Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.core-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.excheck-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.exedit-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
    <%--初始化，绑定事件--%>
    <script type="text/javascript">

        var settingShrinkTree = {         //此类树控件设定参数
            check: {
                enable: true,
                chkboxType: { "Y": "ps", "N": "ps" }
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                beforeExpand: beforeExpandShrinkTree,
                onExpand: onExpandShrinkTree,
                onClick: clickTreeShrinkTree
            }
        };

        $(function () {
            parent.document.title = document.title;

            var gridHeight = document.documentElement.clientHeight - 180;
            //初始角色代码表格
            $("#RoleTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '../../Handle/Sys/Role.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetRole" }], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                        { display: '序号', name: 'rownum', toggle: false, hide: true, sortable: true, iskey: true, width: 45, align: 'center' },
                        { display: '角色编号', name: 'RoleID', sortable: true, width: 45, align: 'center' },
                        { display: '角色名', name: 'RoleName', sortable: true, width: 315, align: 'center' },
	    				{ display: '角色描述', name: 'Description', width: 315, sortable: true, align: 'center' },
                        { display: '是否有效', name: 'AvailFlag', width: 315, sortable: true, align: 'center' }
                ],
                sortname: "RoleID",
                sortorder: "asc",
                title: "角色列表",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: true,
                selectedonclick: true,
                singleselected: true,
                rowbinddata: true
            });

            //新增角色管理代码
            $("#btnAdd").click(function () {
                AddOrEdit("");
                return false;
            });

            //编辑角色
            $("#btnEdit").click(function () {
                //获取选中的表格主键
                var obj = GetSelectedRow("RoleTable", "角色");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];
                AddOrEdit(id);
                return false;
            });

            //删除角色代码
            $("#btnDel").click(function () {
                //获取选中的表格主键
                var obj = GetSelectedRow("RoleTable", "角色");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1]
                art.dialog.confirm("是否删除此管理员？", function () {
                    DeleteAdmin(id);
                    return;
                }, function () {
                    artDialog.tips("取消删除操作!");
                });
                return false;
            });

            //查询
            $("#SelectBtn").click(function () {
                Search();
                return false;
            });

            //关联权限
            $("#btnBindRole").click(function () {
                var obj = GetSelectedRow("RoleTable", "角色");
                if (!obj) {
                    return false;
                }
                bindArea(obj[0][1]);
                return false;
            });

            //查看权限关联
            $("#btnRole").click(function () {
                var obj = GetSelectedRow("RoleTable", "角色");
                if (!obj) {
                    return false;
                }
                SelectArea(obj[0][1]);
                return false;
            });
        });
    </script>
    <%--获取角色名--%>
    <script type="text/javascript">
        //封装AJAX
        function JsAjax(url, data, successFun, errorFun) {
            try {
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: data,
                    success: successFun,
                    error: errorFun
                });
            } catch (err) {
                alert(err);
            }
        }
        //删除代码开始
        function DeleteAdmin(id) {
            var url = "../../Handle/Sys/Role.ashx";
            var data = { "meth": "DeleteRole", "id": id };
            var successFun = function (json) {
                if (json.State == 0) {
                    artDialog.alert(json.Info);
                    return false;
                }
                artDialog.tips(json.Info);
                $("#RoleTable").flexReload();    //刷新表格
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
        //删除代码结束
    </script>
    <%--新增/修改角色代码--%>
    <script type="text/javascript">
        function AddOrEdit(id) {
            var artTitle = "新增角色代码";
            if (id && id != "") {
                artTitle = "编辑角色代码";
                $.ajax({
                    type: "Post",
                    url: "../../Handle/Sys/Role.ashx",
                    data: { "meth": "SelectUpdate", "id": id },
                    success: function (json) {
                        $("#txtCodeName").val(json.RoleName);
                        $("#txtDescribe").val(json.Description);
                    }
                });
            }
            //弹出新增角色代码
            $.dialog({
                content: $("#divFeeCode").get(0),
                id: 'divFeeCode',
                title: artTitle,
                init: function () {
                    if (id == "") {
                        $("#divFeeCode .txt").not("").val("");
                    }
                },
                ok: function () {
                    var CodeName = $("#txtCodeName").val();
                    var Cmd = $("#txtDescribe").val();
                    if (Cmd == "") {
                        Cmd = " ";
                    }
                    var url = "../../Handle/Sys/Role.ashx";

                    if (id && id != "") {   //编辑操作
                        var data = { "meth": "UpdateRole", "RoleName": CodeName, "Description": Cmd, "id": id };
                    } else {                //新增操作
                        var data = { "meth": "AddRole", "RoleName": CodeName, "Description": Cmd };
                    }
                    var successFun = function (json) {
                        if (json.State == 0) {
                            artDialog.alert(json.Info);
                            return false;
                        }
                        artDialog.tips(json.Info);
                        art.dialog.list["divFeeCode"].close();
                        $("#RoleTable").flexReload();    //刷新表格
                        return true;
                    };
                    var errorFun = function (x, e) {
                        alert(x.responseText);
                    };
                    JsAjax(url, data, successFun, errorFun);
                    return false;
                },
                cancel: true
            });
        }
    </script>
    <%--查询--%>
    <script type="text/javascript">
        function Search() {
            var User = $("#txtUser").val();
            var Valid = $("#SeleValid").val();
            var p = {
                extParam: [   //扩展参数
                  { name: "meth", value: "GetRole" },
                  { name: "User", value: User },
                  { name: "Valid", value: Valid }
                ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#RoleTable").flexOptions(p).flexReload();
        }
    </script>
    <%--关联角色--%>
    <script type="text/javascript">
        var zShrinkTreeId = "RoleTree"; //重新赋值控件ID值
        //查看关联角色
        function SelectArea(id) {
            $.dialog({
                content: $("#ztreeDiv").get(0),
                id: 'ztreeDiv',
                title: "关联权限",
                cancel: true
            });
            LoadRoleTree(id);
        }
        //关联权限
        function bindArea(id) {
            $.dialog({
                content: $("#ztreeDiv").get(0),
                id: 'ztreeDiv',
                title: "关联权限",
                ok: function () {
                    art.dialog.confirm("是否确认关联操作？", function () {
                        var obj = zShrinkTree.getCheckedNodes(true);  //获取被选中的节点                        
                        var AreaIdList = "";
                        var AreaNameList = "";
                        if (obj.length < 0) {
                            artDialog.tips("未选择任何权限。");
                            return false;
                        }
                        for (var i = 0; i < obj.length; i++) {
                            AreaIdList += obj[i].id + ",";
                            AreaNameList += obj[i].name + ",";
                        }
                        AreaIdList = AreaIdList.substring(0, AreaIdList.length - 1);
                        var FeeCodeId = id;
                        var url = "../../Handle/Sys/Permission.ashx";
                        var data = { "meth": "Bind", "AreaIdList": AreaIdList, "FeeCodeId": FeeCodeId, "AreaNameList": AreaNameList, "ObjType": 1 };
                        var successFun = function (json) {
                            if (json.State == 0) {
                                artDialog.alert(json.Info);
                                return false;
                            }
                            artDialog.tips(json.Info);
                            art.dialog.list["ztreeDiv"].close();
                            return true;
                        };
                        var errorFun = function (x, e) {
                            alert(x.responseText);
                        };
                        JsAjax(url, data, successFun, errorFun);
                        return;
                    }, function () { artDialog.tips("取消操作。"); });
                    return false;
                },
                cancel: true
            });
            LoadRoleTree(id);
        }
        //加载关联角色树
        function LoadRoleTree(id) {
            var url = "../../Handle/Sys/Permission.ashx";
            var data = { "meth": "GetAllPermission", "CodeId": id, "ObjType": 1 };
            var successFun = function (json) {
                zShrinkNodes = json;
                $.fn.zTree.init($("#" + zShrinkTreeId), settingShrinkTree, zShrinkNodes); //设定参数初始化树
                zShrinkTree = $.fn.zTree.getZTreeObj(zShrinkTreeId);
                zShrinkTree.expandAll(false);
                return true;
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
        <div>
            <div class="SearchDiv">
                <table>
                    <tr>
                        <td style="text-align: right;" class="style4">角色名：
                        </td>
                        <td class="style3">
                            <input id="txtUser" class="txt" style="width: 70px" />
                        </td>
                        <td>状态:
                        </td>
                        <td>
                            <select id="SeleValid" class="txt">
                                <option value="">--请选择--</option>
                                <option value="1">有效</option>
                                <option value="0">无效</option>
                            </select>
                        </td>
                        <td>
                            <asp:Button CssClass="btnHigh" ID="SelectBtn" runat="server" Text="查询" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="OperationDiv">
                <asp:Button CssClass="btnHigh" ID="btnAdd" runat="server" Text="新增" />
                <asp:Button CssClass="btnHigh" ID="btnEdit" runat="server" Text="编辑" />
                <asp:Button CssClass="btnHigh" ID="btnDel" runat="server" Text="删除" />
                <asp:Button CssClass="btnHigh" ID="btnBindRole" runat="server" Text="赋予权限" />
                <asp:Button CssClass="btnHigh" ID="btnRole" runat="server" Text="查看权限" />
            </div>
            <div class="ListTable">
                <table id="RoleTable" style="display: none;">
                </table>
            </div>
        </div>
        <div class="dialogDiv">
            <div id="divFeeCode" style="width: 620px">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable" id="divCustInfo">
                    <tbody>
                        <tr>
                            <td width="20%" align="right">角色名：
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtCodeName" runat="server" CssClass="txt " size="25" MaxLength="64"></asp:TextBox>
                                <label class="red">
                                    *</label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" align="right">角色描述：
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtDescribe" runat="server" CssClass="txt " size="25" MaxLength="64"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="ztreeDiv" class="zTreeDemoBackground">
                <ul id="RoleTree" class="ztree" style="height: 300px; width: 300px">
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
