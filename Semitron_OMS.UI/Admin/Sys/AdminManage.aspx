<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminManage.aspx.cs" Inherits="Semitron_OMS.UI.Admin.Sys.AdminManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单管理系统 - 管理员</title>
    <link href="/Styles/login_style.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/CSS_Wen.css" rel="stylesheet" type="text/css" />
    <link href="../Images/style.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/flexigrid-1.1/css/flexigrid.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/JS_Wen.js" type="text/javascript"></script>
    <script src="/Scripts/flexigrid-1.1/js/flexigrid.js" type="text/javascript"></script>
    <script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.core-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.excheck-3.0.js" type="text/javascript"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.exedit-3.0.js" type="text/javascript"></script>
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/public-common-function.js" type="text/javascript"></script>
    <%--初始化，绑定事件--%>
    <script type="text/javascript">

        $(function () {
            parent.document.title = document.title;
            var gridHeight = document.documentElement.clientHeight - 180;
            //初始管理员代码表格
            $("#AdminTable").flexigrid({
                width: 'auto', //表格宽度
                height: gridHeight, //表格高度
                url: '/Handle/Sys/Admin.ashx', //数据请求地址
                dataType: 'json', //请求数据的格式
                extParam: [{ name: "meth", value: "GetAdminList"}], //扩展参数
                colModel: [//表格的题头与索要填充的内容。
                         {display: '序号', name: 'rownum', toggle: false, hide: true, sortable: true, iskey: true, width: 45, align: 'center' },
                        { display: '管理员编号', name: 'AdminID', toggle: true, width: 60, align: 'center' },
                        { display: '是否有效', name: 'AvailFlag', width: 45, sortable: true, align: 'center' },
                        { display: '用户名', name: 'Username', sortable: true, width: 100, align: 'center' },
	    				{ display: '密码', name: 'Password', width: 100, hide: true, sortable: true, align: 'center' },
	    				{ display: '姓名', name: 'Name', width: 100, sortable: true, align: 'center' },
	    				{ display: '联系电话', name: 'Phone', width: 80, sortable: true, align: 'center' },
                        { display: 'E-mail', name: 'Email', width: 120, sortable: true, align: 'center' },
                        { display: '管理员ID', name: 'CustID', width: 120, hide: true, sortable: true, align: 'center' },
                        { display: '用户所属', name: 'CustName', width: 120, sortable: true, align: 'center' },
                        { display: '创建时间', name: 'CreateTime', width: 115, sortable: true, align: 'center' },
                        { display: '最后登陆时间', name: 'LastLoginTime', width: 115, sortable: true, align: 'center' }
	    			],
                sortname: "AdminID",
                sortorder: "desc",
                title: "管理员列表",
                usepager: true,
                useRp: true,
                rowbinddata: true,
                showcheckbox: true,
                selectedonclick: true,
                singleselected: true,
                rowbinddata: true
            });
            SelectRoleName();

            //新增管理代码
            $("#btnAdd").click(function () {
                AddOrEdit("");
                return false;
            });
            //编辑用户管理代码
            $("#btnEdit").click(function () {
                //获取选中的表格主键
                var obj = GetSelectedRow("AdminTable", "后台用户");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];
                AddOrEdit(id);
                return false;
            });
            //删除用户管理代码
            $("#btnDel").click(function () {
                //获取选中的表格主键
                var obj = GetSelectedRow("AdminTable", "后台用户");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];
                art.dialog.confirm("是否删除此管理员？", function () {
                    DeleteAdmin(id);
                    return;
                }, function () {
                    artDialog.tips("取消删除操作!");
                });
                return false;
            });
            //启用用户管理代码
            $("#btnStar").click(function () {
                //获取选中的表格主键
                var obj = GetSelectedRow("AdminTable", "后台用户");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];
                art.dialog.confirm("是否启用此管理员？", function () {
                    EnabledAdmin(id);
                    return;
                }, function () {
                    artDialog.tips("取消启用操作!");
                });
                return false;
            });
            //关联角色
            $("#btnBindArea").click(function () {
                var obj = GetSelectedRow("AdminTable", "后台用户");
                var text = $("#btnBindArea").val();
                if (!obj) {
                    return false;
                }
                bindArea(obj[0][1], obj[0][5]);
                return false;
            });
            //查看角色关联
            $("#btnArea").click(function () {
                var obj = GetSelectedRow("AdminTable", "后台用户");
                var text = $("#btnArea").val();
                if (!obj) {
                    return false;
                }
                SelectArea(obj[0][1]);
                return false;
            });
            //查询
            $("#SelectBtn").click(function () {
                Search();
                return false;
            });

            //根据角色查询
            $("#SelecRole").change(function () {
                Search();
            });

            //用户类型选择
            $("#SelectUser").change(function () {
                //                if ($("#SelectUser").val() == 1) {
                //                    var data = { "meth": "GetCustList" };
                //                    LoadSelect("/Handle/BMP/Cust.ashx", data, "SelectFirm", "");
                //                }
                //                else if ($("#SelectUser").val() == 2) {
                //                    var data = { "meth": "GetCpInfoList" };
                //                    LoadSelect("/Handle/SPM/CpInfo.ashx", data, "SelectFirm");
                //                }
                //                else {
                //                    $("#SelectFirm").find("option").remove();
                //                    $("<option value=\"\">--请选择--</option>").appendTo($("#SelectFirm"));
                //                }
            });
            //查询中所属用户下拉框
            $("#SelectFirm").change(function () {
                var selValue = $("#SelectFirm").val();
                if (selValue == "-1" || selValue == "-2") {
                    artDialog.tips("此为分隔符，选择无效。");
                    $("#SelectFirm").val("");
                    return false;
                }
                if (selValue != "") Search();
            });
            //编辑中所属用户下拉框
            $("#firm").change(function () {
                var selValue = $("#firm").val();
                if (selValue == "-1" || selValue == "-2") {
                    artDialog.tips("此为分隔符，选择无效。");
                    $("#firm").val("");
                }
            });
            //修改密码
            $("#btnUpdatePwd").click(function () {
                var obj = GetSelectedRow("AdminTable", "后台用户");
                if (!obj) {
                    return false;
                }
                var id = obj[0][1];
                $("#txtNUserName").val(obj[0][3]);
                var id = obj[0][1];
                UpdatePwd(id);
                return false;
            });
            //关联权限
            $("#btnPower").click(function () {
                var obj = GetSelectedRow("AdminTable", "后台用户");
                if (!obj) {
                    return false;
                }
                bindExtent(obj[0][1]);
                return false;
            });
            //查看权限关联
            $("#btnCheck").click(function () {
                var obj = GetSelectedRow("AdminTable", "后台用户");
                if (!obj) {
                    return false;
                }
                SelectExtent(obj[0][1]);
                return false;
            });
        });
    </script>
    <%--获取管理员名--%>
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

        //页面加载角色
        function SelectRoleName() {
            $.ajax({
                type: "Post",
                url: "/Handle/Sys/Admin.ashx",
                data: { "meth": "SelectRoleName" },
                success: function (json) {

                    //每次调用清空子项
                    $("#SelecRole").find("option").remove();
                    $("<option value=''>--请选择--</option>").appendTo($("#SelecRole"));
                    if (json == "") {
                        $("<option value=\"-1\">没有角色信息</option>").appendTo($("#SelecRole"));
                    } else {
                        //添加选择所有项目选项
                        for (var i = 0; i < json.length; i++) {
                            var key = json[i].RoleName;
                            var value = json[i].RoleID;
                            $("<option value='" + value + "'>" + key + "</option>").appendTo($("#SelecRole"));
                        }
                    }
                }
            });
        }
        //删除代码开始
        function DeleteAdmin(id) {
            var url = "/Handle/Sys/Admin.ashx";
            var data = { "meth": "DeleteAdminInfo", "id": id };
            var successFun = function (json) {
                if (json.State == 0) {
                    artDialog.alert(json.Info);
                    return false;
                }
                artDialog.tips(json.Info);
                $("#AdminTable").flexReload();    //刷新表格
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
        //启用代码开始
        function EnabledAdmin(id) {
            var url = "/Handle/Sys/Admin.ashx";
            var data = { "meth": "EnabledAdminInfo", "id": id };
            var successFun = function (json) {
                if (json.State == 0) {
                    artDialog.alert(json.Info);
                    return false;
                }
                artDialog.tips(json.Info);
                $("#AdminTable").flexReload();    //刷新表格
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
    </script>
    <%--新增/修改用户代码--%>
    <script type="text/javascript">
        function SetSltUserType(value) {
            var strTypeRole = "0,10";
            switch (value) {
                case 0:
                    strTypeRole = "0,10";
                    break;
                case 1:
                    strTypeRole = "1,5";
                    break;
                case 2:
                    strTypeRole = "2,16";
                    break;
                case 3:
                    strTypeRole = "3,10";
                    break;
                //case 4: 
                //    strTypeRole = "4,11"; 
                //    break; 
                //case 5: 
                //    strTypeRole = "5,13"; 
                //    break; 
                //case 6: 
                //    strTypeRole = "6,12"; 
                //    break; 
            }

            $("#sltUserType").val(strTypeRole);
        }

        function AddOrEdit(id) {
            var artTitle = "新增管理员代码";
            document.getElementById("txtUserName").disabled = false;
            document.getElementById("txtPwd").disabled = false;
            document.getElementById("txtAgainPwd").disabled = false;
            document.getElementById("sltUserType").disabled = false;
            $("#divCustInfo.txt").not("#txtUserName,#txtPhone,#txtPhone,#txtName").val("");
            $("#sltUserType").val("0,10");
            if (id && id != "") {
                document.getElementById("txtUserName").disabled = true;
                document.getElementById("txtPwd").disabled = true;
                document.getElementById("txtAgainPwd").disabled = true;
                document.getElementById("sltUserType").disabled = true;
                artTitle = "编辑管理员代码";
                $.ajax({
                    type: "Post",
                    url: "/Handle/Sys/Admin.ashx",
                    data: { "meth": "SelectUpdate", "id": id },
                    success: function (json) {
                        //                        var text = msg.split(",");
                        //每次调用清空子项
                        if (json.State == 0) {
                            artDialog.alert(json.Info);
                            return false;
                        }
                        $("#txtUserName").val(json.Username);
                        $("#txtPwd").val(json.Password);
                        $("#txtPhone").val(json.Phone);
                        $("#txtName").val(json.Name);
                        $("#txtAgainPwd").val(json.Password);
                        $("#txtEmail").val(json.Email);

                        SetSltUserType(json.Type);

                        $("#ShowSelect").hide();
                        $("#ShowFirm").hide();
                        if (json.Type != null && json.Type != "") {
                            if (json.Type == "2") {
                                $("#ShowSelect").show();
                                $("#ShowFirm").show();
                                $("#SpanFirm").text("所属合作方：");
                                var data = { "meth": "GetCpInfoList" };
                                LoadSelect("/Handle/SPM/CpInfo.ashx", data, "firm", json.CustID);
                            }
                            if (json.Type == "1") {
                                $("#ShowSelect").show();
                                $("#ShowFirm").show();
                                $("#SpanFirm").text("所属厂商：");
                                var data = { "meth": "GetCustList" };
                                LoadSelect("/Handle/BMP/Cust.ashx", data, "firm", json.CustID);
                            }
                        }
                        alertdivUserInfo(id);
                        return true;

                    }
                });
            } else {
                alertdivUserInfo("");
            }
            //弹出用户代码

        }

        function alertdivUserInfo(id) {
            $.dialog({
                content: $("#divFeeCode").get(0),
                id: 'divFeeCode',
                height: 200,
                init: function () {
                    if (id == "") {
                        $("#divFeeCode .txt").not("").val("");
                        $("#sltUserType").val("0,10");
                        $("#ShowFirm").hide();
                        $("#ShowSelect").hide();
                    }
                },
                ok: function () {
                    var Emila = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
                    var CodeName = $("#txtUserName").val();
                    var Cmd = $("#txtPwd").val();
                    var Addr = $("#txtPhone").val();
                    if ($("#txtPhone").val() == "") {
                        Addr = " ";
                    }
                    var Fee = $("#txtName").val();
                    var Key2 = $("#txtEmail").val();
                    //                    if ($("#txtEmail").val() == "") {
                    //                        Key2 = " ";
                    //                    } else {
                    //                        if (!Emila.test($("#txtEmail").val())) {
                    //                            artDialog.tips("邮箱输入格式有误。请重新输入!");
                    //                            return false;
                    //                        }
                    //                    }
                    var Key1 = $("#txtAgainPwd").val();
                    var slt = $("#sltUserType").val();
                    var firm = "";

                    var arrTR = slt.split(",");
                    if (arrTR.length == 2 && arrTR[0] == "1" || arrTR[0] == "2") {
                        firm = $("#firm").val();
                    }
                    if (CodeName.length < 4) {
                        artDialog.tips("用户名不能小于4位。");
                        return false;
                    }
                    if (Cmd.length < 6) {
                        artDialog.tips("密码不能小于6位。");
                        return false;
                    }
                    if (Fee == "") {
                        artDialog.tips("姓名不能为空。");
                        return false;
                    }
                    if (Key1 == "") {
                        artDialog.tips("确认密码不能为空。");
                        return false;
                    }
                    if (Key1 != Cmd) {
                        artDialog.tips("两次密码不相等。");
                        return false;
                    }
                    var url = "/Handle/Sys/Admin.ashx";

                    if (id && id != "") {   //编辑操作
                        var data = { "meth": "UpdateAdminInfo", "Username": CodeName, "Password": Cmd, "Name": Fee, "Phone": Addr, "Email": Key2, "firm": firm, "slt": slt, "id": id };
                    } else {                //新增操作
                        var data = { "meth": "AddAdminInfo", "Username": CodeName, "Password": Cmd, "Name": Fee, "Phone": Addr, "Email": Key2, "firm": firm, "slt": slt };
                    }
                    var successFun = function (json) {
                        if (json.State == 0) {
                            artDialog.alert(json.Info);
                            return false;
                        }
                        artDialog.tips(json.Info);
                        art.dialog.list["divFeeCode"].close();
                        $("#AdminTable").flexReload();    //刷新表格
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
    <script type="text/javascript">
        function yongh() {
            var arrTR = $("#sltUserType").val().split(",");
            if (arrTR.length == 2 && arrTR[0] == "1" || arrTR[0] == "2") {
                $("#ShowFirm").show();
                $("#ShowSelect").show();
                $("#firm").val("");
                if (arrTR[0] == "1") {
                    $("#SpanFirm").text("所属厂商：");
                    var data = { "meth": "GetCustList" };
                    LoadSelect("/Handle/BMP/Cust.ashx", data, "firm", "");
                }
                if (arrTR[0] == "2") {
                    $("#SpanFirm").text("所属合作方：");
                    var data = { "meth": "GetCpInfoList" };
                    LoadSelect("/Handle/SPM/CpInfo.ashx", data, "firm");
                }
            } else {
                $("#ShowFirm").hide();
                $("#ShowSelect").hide();
            }
        }
    </script>
    <%--关联角色--%>
    <script type="text/javascript">

        var zNodes, zTree;
        var setting = {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            }
        };
        //查看关联角色
        function SelectArea(id) {
            $.dialog({
                content: $("#ztreeDiv").get(0),
                id: 'ztreeDiv',
                title: "查看角色",
                cancel: true
            });
            var url = "/Handle/Sys/Role.ashx";
            var data = { "meth": "GetAllByRole", "CodeId": id };
            var successFun = function (json) {

                zNodes = json;
                $.fn.zTree.init($("#AreaTree"), setting, zNodes);
                zTree = $.fn.zTree.getZTreeObj("AreaTree");
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
        //编辑角色
        function bindArea(id, AdminName) {
            $.dialog({
                content: $("#ztreeDiv").get(0),
                id: 'ztreeDiv',
                title: "关联角色",
                ok: function () {
                    art.dialog.confirm("是否确认关联操作？", function () {
                        var obj = zTree.getCheckedNodes(true);  //获取被选中的节点
                        var AreaIdList = "";
                        var AreaNameList = "";
                        if (obj.length < 0) {
                            artDialog.tips("未选择任何角色。");
                            return false;
                        }
                        for (var i = 0; i < obj.length; i++) {
                            AreaIdList += obj[i].id + ",";
                        }

                        AreaIdList = AreaIdList.substring(0, AreaIdList.length - 1);
                        var FeeCodeId = id;
                        var url = "/Handle/Sys/Role.ashx";
                        var data = { "meth": "Bind", "AreaIdList": AreaIdList, "FeeCodeId": FeeCodeId, "AreaNameList": AdminName };
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
            var url = "/Handle/Sys/Role.ashx";
            var data = { "meth": "GetAllByRole", "CodeId": id };
            var successFun = function (json) {

                zNodes = json;
                $.fn.zTree.init($("#AreaTree"), setting, zNodes);
                zTree = $.fn.zTree.getZTreeObj("AreaTree");
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

    </script>
    <%--查询--%>
    <script type="text/javascript">
        function Search() {
            var Username = $("#txtUser").val();
            var Name = $("#txtSName").val();
            var AvailFlag = $("#SeleValid").val();
            var Type = $("#SelectUser").val();
            var CustID = $("#SelectFirm").val();
            var RoleID = $("#SelecRole").val();
            var p = { extParam: [   //扩展参数
                        {name: "meth", value: "GetAdminList" },
                        { name: "Username", value: Username },
                        { name: "Name", value: Name },
                        { name: "AvailFlag", value: AvailFlag },
                        { name: "Type", value: Type },
                        { name: "CustID", value: CustID },
                        { name: "RoleID", value: RoleID }
                        ]
            };
            p.newp = 1;         //跳转到第一页。
            $("#AdminTable").flexOptions(p).flexReload();
        }
    </script>
    <%--修改密码--%>
    <script type="text/javascript">
        function UpdatePwd(id) {
            var artTitle = "修改密码";
            var yan = /^\w{6,18}$/;
            //弹出修改密码代码
            $.dialog({
                content: $("#Code").get(0),
                id: 'Code',
                title: artTitle,
                ok: function () {
                    var pwd = $("#txtFresh").val();
                    if (yan.test(pwd)) {
                        if ($("#txtFresh").val() == $("#txtFfirm").val()) {
                            var url = "/Handle/Sys/Admin.ashx";
                            var data = { "meth": "UpdatePwd", "txtFresh": pwd, "id": id };
                            var successFun = function (json) {
                                if (json.State == 0) {
                                    artDialog.alert(json.Info);
                                    return false;
                                }
                                artDialog.tips(json.Info);
                                art.dialog.list["Code"].close();
                                $("#AdminTable").flexReload();
                                return true;
                            };
                            var errorFun = function (x, e) {
                                alert(x.responseText);
                            };
                            JsAjax(url, data, successFun, errorFun);
                            return false;
                        } else {
                            artDialog.tips("两次密码输入不相同!");
                            return false;
                        }
                    } else {
                        artDialog.tips("密码不能小于6位或大于18位!");
                        return false;
                    }
                },
                cancel: true
            });
        }
    </script>
    <%--用户关联权限 --%>
    <script type="text/javascript">

        var zNodes, zTree;
        var setting = {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            }
        };
        //查看关联权限
        function SelectExtent(id) {
            $.dialog({
                content: $("#extentDiv").get(0),
                id: 'extentDiv',
                title: "查看权限",
                cancel: true
            });
            var url = "/Handle/Sys/Permission.ashx";
            var data = { "meth": "GetAllPermission", "CodeId": id, "ObjType": 2 };
            var successFun = function (json) {

                zNodes = json;
                $.fn.zTree.init($("#ExtentTree"), setting, zNodes);
                zTree = $.fn.zTree.getZTreeObj("ExtentTree");
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }
        //关联权限
        function bindExtent(id) {
            $.dialog({
                content: $("#extentDiv").get(0),
                id: 'extentDiv',
                title: "关联权限",
                ok: function () {
                    art.dialog.confirm("是否确认关联操作？", function () {
                        var obj = zTree.getCheckedNodes(true);  //获取被选中的节点
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
                        var url = "/Handle/Sys/Permission.ashx";
                        var data = { "meth": "Bind", "AreaIdList": AreaIdList, "FeeCodeId": FeeCodeId, "AreaNameList": AreaNameList, "CodeId": id, "ObjType": 2 };
                        var successFun = function (json) {
                            if (json.State == 0) {
                                artDialog.alert(json.Info);
                                return false;
                            }
                            artDialog.tips(json.Info);
                            art.dialog.list["extentDiv"].close();
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
            var url = "/Handle/Sys/Permission.ashx";
            var data = { "meth": "GetAllPermission", "CodeId": id, "ObjType": 2 };
            var successFun = function (json) {

                zNodes = json;
                $.fn.zTree.init($("#ExtentTree"), setting, zNodes);
                zTree = $.fn.zTree.getZTreeObj("ExtentTree");
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
            };
            JsAjax(url, data, successFun, errorFun);
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 37px;
        }
        .style2
        {
            width: 99px;
        }
        .style3
        {
            width: 14%;
        }
        .style4
        {
            width: 10%;
        }
    </style>
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
    <div>
        <div class="SearchDiv">
            <table>
                <tr>
                    <td style="text-align: right;">
                        用户名：
                    </td>
                    <td>
                        <input id="txtUser" class="txt" style="width: 70px" />
                    </td>
                    <td style="text-align: right;">
                        姓名：
                    </td>
                    <td>
                        <input id="txtSName" class="txt" style="width: 70px" />
                    </td>
                    <td style="text-align: right;">
                        状态：
                    </td>
                    <td>
                        <select id="SeleValid" class="txt">
                            <option value="">--请选择--</option>
                            <option value="1">有效</option>
                            <option value="0">无效</option>
                        </select>
                    </td>
                    <td style="text-align: right;">
                        用户类型：
                    </td>
                    <td>
                        <select id="SelectUser" class="txt">
                            <option value="">--请选择--</option>
                           <%-- <option value="2">合作方</option>
                            <option value="1">厂商</option>--%>
                        </select>
                        <select id="SelectFirm" runat="server" style="width: 130px" class="txt">
                            <option value="">--请先行选择类型--</option>
                        </select>
                    </td>
                    <td style="text-align: right;">
                        角色：
                    </td>
                    <td>
                        <select id="SelecRole" style="width: 100px" class="txt">
                        </select>
                    </td>
                    <td>
                        &nbsp;
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
            <asp:Button CssClass="btnHigh" ID="btnBindArea" runat="server" Text="关联角色" />
            <asp:Button CssClass="btnHigh" ID="btnArea" runat="server" Text="查看角色" />
            <asp:Button CssClass="btnHigh" ID="btnDel" runat="server" Text="删除" />
            <asp:Button CssClass="btnHigh" ID="btnStar" runat="server" Text="启用" />
            <asp:Button CssClass="btnHigh" ID="btnUpdatePwd" runat="server" Text="修改密码" />
            <asp:Button CssClass="btnHigh" ID="btnPower" runat="server" Text="赋予用户权限" />
            <asp:Button CssClass="btnHigh" ID="btnCheck" runat="server" Text="查看用户权限" />
        </div>
        <div class="ListTable">
            <table id="AdminTable" style="display: none;">
            </table>
        </div>
    </div>
    <div class="spClear">
    </div>
    <div class="dialogDiv">
        <div id="divFeeCode" style="width: 620px">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable" id="divCustInfo">
                <tbody>
                    <tr>
                        <td width="20%" align="right">
                            用户名：
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txt " size="25" MaxLength="64"></asp:TextBox>
                            <label class="red">
                                *</label>
                        </td>
                        <td align="right">
                            姓名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txt " size="25" MaxLength="64"></asp:TextBox>
                            <label class="red">
                                *</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            密码：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPwd" runat="server" CssClass="txt " size="27" MaxLength="64"
                                TextMode="Password"></asp:TextBox>
                            <label class="red">
                                *</label>
                        </td>
                        <td width="20%" align="right">
                            联系电话：
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txt " size="25" MaxLength="32"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            确认密码：
                        </td>
                        <td>
                            <asp:TextBox ID="txtAgainPwd" runat="server" CssClass="txt " size="27" MaxLength="64"
                                TextMode="Password"></asp:TextBox>
                            <label class="red">
                                *</label>
                        </td>
                        <td align="right">
                            E-mail：
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt " size="25" MaxLength="64"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trfirm">
                        <td align="right">
                            用户类型：
                        </td>
                        <td>
                            <select id="sltUserType" style="width: 120px" onchange="yongh()">
                                <!--vaule取值为Admin表Type值+‘，’+Role表RoleID值组合-->
                                <option value="0,10">默认用户</option>
                               <%-- <option value="1,22">厂商用户</option>
                                <option value="2,16">CP用户</option>--%>
                                <option value="3,10">内部用户</option>
                            </select>
                        </td>
                        <td align="right" style="display: none" id="ShowFirm">
                            <span id="SpanFirm">所属管理员:</span>
                        </td>
                        <td style="display: none;" id="ShowSelect">
                            <select id="firm" style="width: 160px;">
                            </select>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="Code">
            <table>
                <tr>
                    <td>
                        用户名:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNUserName" CssClass="txt" runat="server" Width="150" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        新密码:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFresh" CssClass="txt" runat="server" Width="150" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        确认新密码:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFfirm" CssClass="txt" runat="server" Width="150" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="ztreeDiv" class="zTreeDemoBackground">
            <ul id="AreaTree" class="ztree" style="height: 300px; width: 300px">
            </ul>
        </div>
        <div id="extentDiv" class="zTreeDemoBackground">
            <ul id="ExtentTree" class="ztree" style="height: 300px; width: 300px">
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
