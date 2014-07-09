<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionManage.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.Sys.PermissionManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 权限管理</title>
    <link href="/Styles/login_style.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <link href="../Images/style.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeIcons.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/zTree/js/jquery.ztree.core-3.0.js"></script>
    <script type="text/javascript" src="/Scripts/zTree/js/jquery.ztree.excheck-3.0.js"></script>
    <script type="text/javascript" src="/Scripts/zTree/js/jquery.ztree.exedit-3.0.js"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/JS_Wen.js" type="text/javascript"></script>
    <script type="text/javascript">
        var setting = {
            view: {
                selectedMulti: false
            },
            edit: {
                enable: true,
                showRemoveBtn: false,
                showRenameBtn: false
            },
            data: {
                keep: {
                    parent: true,
                    leaf: true
                },
                simpleData: {
                    enable: true
                }
            },
            callback: {
                beforeDrag: beforeDrag,
                beforeRename: beforeRename,
                beforeExpand: beforeExpand,
                onExpand: onExpand,
                onClick: onClick
            }
        };

        var zNodes = [
			{ PermissionID: 1, pId: 0, Name: "系统管理", open: true },
			{ PermissionID: 2, pId: 0, Name: "查询统计", open: true },
        ];

        var curExpandNode = null;
        function beforeExpand(treeId, treeNode) {
            var pNode = curExpandNode ? curExpandNode.getParentNode() : null;
            var treeNodeP = treeNode.parentTId ? treeNode.getParentNode() : null;
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            for (var i = 0, l = !treeNodeP ? 0 : treeNodeP.childs.length; i < l; i++) {
                if (treeNode !== treeNodeP.childs[i]) {
                    zTree.expandNode(treeNodeP.childs[i], false);
                }
            }
            while (pNode) {
                if (pNode === treeNode) {
                    break;
                }
                pNode = pNode.getParentNode();
            }
            if (!pNode) {
                singlePath(treeNode);
            }

        }

        function singlePath(newNode) {
            if (newNode === curExpandNode) return;
            if (curExpandNode && curExpandNode.open == true) {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                if (newNode.parentTId === curExpandNode.parentTId) {
                    zTree.expandNode(curExpandNode, false);
                } else {
                    var newParents = [];
                    while (newNode) {
                        newNode = newNode.getParentNode();
                        if (newNode === curExpandNode) {
                            newParents = null;
                            break;
                        } else if (newNode) {
                            newParents.push(newNode);
                        }
                    }
                    if (newParents != null) {
                        var oldNode = curExpandNode;
                        var oldParents = [];
                        while (oldNode) {
                            oldNode = oldNode.getParentNode();
                            if (oldNode) {
                                oldParents.push(oldNode);
                            }
                        }
                        if (newParents.length > 0) {
                            for (var i = Math.min(newParents.length, oldParents.length) - 1; i >= 0; i--) {
                                if (newParents[i] !== oldParents[i]) {
                                    zTree.expandNode(oldParents[i], false);
                                    break;
                                }
                            }
                        } else {
                            zTree.expandNode(oldParents[oldParents.length - 1], false);
                        }
                    }
                }
            }
            curExpandNode = newNode;
        }

        function onExpand(event, treeId, treeNode) {
            curExpandNode = treeNode;
        }

        function onClick(e, treeId, treeNode) {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            zTree.expandNode(treeNode, null, null, null, true);
        }

        function beforeDrag(treeId, treeNodes) {
            return false;
        }

        function beforeRename(treeId, treeNode, newName) {
            if (newName.length == 0) {
                alert("节点名称不能为空.");
                return false;
            }
            return true;
        }

        $(document).ready(function () {
            LoadTree();
            parent.document.title = document.title;
            var treeHeight = document.documentElement.clientHeight - 60;
            $("#treeDemo").attr("style", "height:" + treeHeight + "px");
            //下拉框选中的值
            $("#sltPerssion").change(function () {
                if ($("#sltPerssion").val() != "other") {
                    $("#<%=txtName.ClientID %>").attr("disabled", "true");
                    $("#<%=txtControl.ClientID %>").attr("disabled", "true");
                    $("#<%=txtName.ClientID %>").val($("#sltPerssion").find("option:selected").text());
                    $("#<%=txtControl.ClientID %>").val($("#sltPerssion").val());

                } else {
                    $("#<%=txtName.ClientID %>").attr("disabled", "");
                    $("#<%=txtControl.ClientID %>").attr("disabled", "");
                    $("#<%=txtName.ClientID %>").val("");
                    $("#<%=txtControl.ClientID %>").val("");
                }
            });
            //添加权限信息
            $("#btnAdd").click(function () {
                //加载权限信息
                AddAreaGroup("");
                return false;
            });
            //编辑权限信息
            $("#btnEdit").click(function () {
                EditAreaGroup("");
                return false;
            });
            //删除权限信息
            $("#btnDel").click(function () {
                //加载权限信息
                RemoveAreaGroup("");
                return false;
            });
            $("#btnSel").click(function () {
                SeletlPrim("");
                return false;
            });
        });

        function LoadTree1(node, type) {
            var url = "/Handle/Sys/Permission.ashx";
            var data = { "meth": "AreaListForJson" };
            var successFun = function (json) {
                zNodes = json;
                $.fn.zTree.init($("#treeDemo"), setting, zNodes);
                zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodef;
                if (type == "id") {
                    nodef = zTree.getNodeByParam("id", node.id);
                }
                if (type == "pid") {
                    nodef = zTree.getNodeByParam("id", node.pId);
                }
                if (nodef != undefined && node.level >= 2) {
                    zTree.expandNode(nodef, true, true, true);
                }
                return false;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

        //加载Tree
        function LoadTree() {
            var url = "/Handle/Sys/Permission.ashx";
            var data = { "meth": "AreaListForJson" };
            var successFun = function (json) {
                zNodes = json;
                $.fn.zTree.init($("#treeDemo"), setting, zNodes);
                zTree = $.fn.zTree.getZTreeObj("treeDemo");
                zTree.expandAll(false);
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
    </script>
    <%--新增或者修改权限信息--%>
    <script type="text/javascript">
        //绑定JS代码
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
        function AddAreaGroup(id) {
            var artTitle = "新增权限信息";
            var nodes = zTree.getSelectedNodes();
            var id = 0;
            //验证是否选择父节点
            if (nodes && nodes.length > 0) {
                id = nodes[0].id;
            }
            var node = zTree.getSelectedNodes()[0];
            if (nodes[0] == undefined || nodes[0].Type != 3) {
                //弹出新增层
                $.dialog({
                    content: $("#divArea").get(0),
                    id: 'divArea',
                    title: artTitle,
                    init: function () {
                        $("#tdName").hide();
                        $("#tdPerssion").hide();
                        //  $("#divArea .txt").val("");
                        $("#divArea .txt").val("");
                        $("#sltPerssion").val("other");
                        //id=0,则没选中值，新增则是模块权限
                        $("#SelectType").attr("disabled", "true");
                        SetControlAttr();
                        if (id == 0) {
                            $("#SelectType").val(1);
                        }
                        if (nodes[0] != undefined && nodes[0] != null) {
                            if (nodes[0].Type == 1) {
                                $("#SelectType").val(2);
                            }
                            if (nodes[0].Type == 2) {
                                $("#SelectType option").each(function (i) {
                                    if ($("#SelectType option").eq(i).val() == 1) {
                                        $("#SelectType option").eq(i).remove();
                                    }
                                    if ($("#SelectType option").eq(i).val() == 2) {
                                        $("#SelectType option").eq(i).remove();
                                    }
                                })
                                $("#SelectType").removeAttr("disabled");
                                $("#SelectType").val(3);
                                $("#txtSite").val(nodes[0].LinkUrl);
                                $("#tdName").show();
                                $("#tdPerssion").show();
                            }

                        }
                    },
                    ok: function () {
                        var Name = $("#txtName").val();
                        var Pid = id;
                        var Control = $("#txtControl").val();
                        var Describe = $("#txtDescribe").val();
                        var Site = $("#txtSite").val();
                        var Type = $("#SelectType").val();
                        //为空验证
                        if (Name == "") {
                            artDialog.tips("权限名称不能为空。");
                            return false;
                        }
                        if (Control == "") {
                            artDialog.tips("权限控制不能为空。");
                            return false;
                        }
                        if (Describe == "") {
                            Describe = " ";
                        }
                        if (Site == "") {
                            Site == " "
                        }
                        var url = "/Handle/Sys/Permission.ashx";
                        var data = { "meth": "InsertPermission", "Name": Name, "Pid": Pid, "Describe": Describe, "Control": Control, "Type": Type, "Site": Site };
                        var successFun = function (json) {
                            if (json.State == 0) {
                                artDialog.alert(json.Info);
                                return false;
                            }
                            artDialog.tips(json.Info);
                            art.dialog.list["divArea"].close();
                            LoadTree1(node, "id");
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
        }
        //编辑权限信息
        function EditAreaGroup() {
            var nodes = zTree.getSelectedNodes();
            //验证是否选择父节点
            if (nodes && nodes.length < 1) {
                artDialog.tips("请先选择一个权限点。");
                return;
            }
            //将信息加载到弹出层中
            var node = zTree.getSelectedNodes()[0];
            artDialogTitle = "编辑权限信息";
            var url = "/Handle/Sys/Permission.ashx";
            var data = { "meth": "SelectUpdate", "id": node.id };
            var successFun = function (json) {
                if (json.State == 0) {
                    artDialog.alert(json.Info);
                    return false;
                }
                $("#sltPerssion").val(json.Code);
                $("#txtName").val(json.Name);
                $("#txtControl").val(json.Code);
                $("#txtSite").val(json.LinkUrl);
                $("#txtDescribe").val(json.Description);
                $("#SelectType").val(json.Type);
                $("#tdName").hide();
                $("#tdPerssion").hide();
                SetControlAttr();
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
            //弹出编辑层
            $.dialog({
                content: $("#divArea").get(0),
                id: 'divArea',
                title: "编辑权限信息",
                init: function () { $("#divArea .txt").val(""); },
                ok: function () {
                    //获取编辑后的值
                    var txtName = $("#txtName").val();
                    var txtControl = $("#txtControl").val();
                    var txtSite = $("#txtSite").val();
                    var txtDescribe = $("#txtDescribe").val();
                    var SelectType = $("#SelectType").val();
                    //验证地区名称是否为空
                    //addNnodes(父节点，新节点，是否自动展开父节点)
                    if (txtName == "") {
                        artDialog.tips("权限名称不能为空！");
                        return false;
                    }
                    if (txtControl == "") {
                        artDialog.tips("权限控制不能为空。");
                        return false;
                    }
                    if (txtDescribe == "") {
                        txtDescribe = " ";
                    }
                    if (txtSite == "") {
                        txtSite = " ";
                    }
                    var url = "/Handle/Sys/Permission.ashx";
                    var data;  //编辑操作
                    data = { "meth": "UpdatePermission", "txtName": txtName, "txtControl": txtControl, "txtSite": txtSite, "id": node.id, "txtDescribe": txtDescribe, "SelectType": SelectType };

                    var successFun = function (json) {
                        if (json.State == 0) {
                            artDialog.alert(json.Info);
                            return false;
                        }
                        artDialog.tips(json.Info);
                        art.dialog.list["divArea"].close();
                        //刷新Tree
                        LoadTree1(node, "pid");

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

        function SetControlAttr() {
            $("#<%=txtName.ClientID %>").attr("disabled", "");
            $("#<%=txtControl.ClientID %>").attr("disabled", "");
        }

        //删除权限
        function RemoveAreaGroup() {
            var nodes = zTree.getSelectedNodes();
            var node = zTree.getSelectedNodes()[0];
            if (nodes && nodes.length < 1) {
                artDialog.alert("请先选择待删除的权限！");
                return false;
            }
            if (zTree.getSelectedNodes()[0].childs && zTree.getSelectedNodes()[0].childs.length > 0) {
                artDialog.alert("该节点下存在子节点，不能删除！");
                return false;
            }
            var msg = "是否确定删除权限？";

            art.dialog.confirm(msg, function () {
                //调用删除方法
                var url = "/Handle/Sys/Permission.ashx";
                var data = { "meth": "Delete", "id": nodes[0].id };
                var successFun = function (json) {
                    if (json.State == "0") {
                        artDialog.alert(json.Info);
                        return false;
                    } else {
                        artDialog.tips(json.Info);
                        LoadTree1(node, "pid");
                    }
                };
                var errorFun = function (x, e) {
                    alert(x.responseText);
                };
                JsAjax(url, data, successFun, errorFun);
            }, function () {
                art.dialog.tips('你取消了删除操作');
            });
        }

        //查看权限关联
        function SeletlPrim(id) {
            artDialogTitle = "查看权限信息";
            var nodes = zTree.getSelectedNodes();
            //验证是否选择父节点
            if (nodes && nodes.length < 1) {
                artDialog.tips("请先选择一个地区分组。");
                return;
            }
            var node = zTree.getSelectedNodes()[0];
            //将信息加载到弹出层中
            $("#txtName").val(node.name);
            $("#txtControl").val(node.Code);
            $("#txtSite").val(node.LinkUrl);
            $("#txtDescribe").val(node.Description);
            $("#SelectType").val(node.Type);
            $("#tdName").hide();
            $("#tdPerssion").hide();
            //弹出新增层
            $.dialog({
                content: $("#divArea").get(0),
                id: 'divArea',
                title: "查看权限信息",
                cancel: true
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="OperationDiv">
            <asp:Button CssClass="btnHigh" ID="btnAdd" runat="server" Text="新增" />
            <asp:Button CssClass="btnHigh" ID="btnEdit" runat="server" Text="编辑" />
            <asp:Button CssClass="btnHigh" ID="btnDel" runat="server" Text="删除" />
            <asp:Button CssClass="btnHigh" ID="btnSel" runat="server" Text="查看" />
        </div>
        <div class="zTreeDemoBackground" style="padding-top: 5px">
            <ul id="treeDemo" class="ztree" style="height: 460px;">
            </ul>
        </div>
        <div id="divArea"  style="display:none;">
            <table border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tbody>
                    <tr>
                        <td align="right" id='tdName' style="width:100px;display: none">权限标识：
                        </td>
                        <td id="tdPerssion" style="width:200px;display: none">
                            <select id="sltPerssion" style="width: 150px" class="txt">
                                <option value="other">其他 </option>
                                <option value="btnAdd">新增 </option>
                                <option value="btnSearch">查询</option>
                                <option value="btnEdit">编辑 </option>
                                <option value="btnStop">停用 </option>
                                <option value="btnOpen">启用 </option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px;" align="right">权限名称：
                        </td>
                        <td style="width:200px;">
                            <asp:TextBox ID="txtName" runat="server" CssClass="txt " size="25" MaxLength="16"></asp:TextBox>
                            <span class="red">*</span>
                        </td>
                    </tr>
                    <tr>
                         <td align="right">权限代码：
                        </td>
                        <td style="width:260px;">
                            <asp:TextBox ID="txtControl" Width="230px" runat="server" CssClass="txt" size="25" MaxLength="64"></asp:TextBox>
                            <label class="red">
                                *</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">链接地址：
                        </td>
                        <td>
                            <label class="red">
                                <asp:TextBox ID="txtSite" Width="230px" runat="server" CssClass="txt" size="25" MaxLength="256"></asp:TextBox>
                                *</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">权限描述：
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescribe" Width="150px" runat="server" CssClass="txt" size="25" MaxLength="128">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">权限类型：
                        </td>
                        <td>
                            <select id="SelectType">
                                <option value="1">模块</option>
                                <option value="2">菜单</option>
                                <option value="3">按钮</option>
                                <option value="4">数据集权限</option>
                                <option value="5">右键菜单</option>
                            </select>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
