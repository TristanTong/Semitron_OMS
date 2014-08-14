<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadifyControl.ascx.cs"
    Inherits="Semitron_OMS.UI.Admin.UserControl.UploadifyControl" %>
<%--CSS Ref--%>
<link href="/Scripts/jquery.uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />
<link href="/Scripts/jquery.uploadify-v2.1.0/example/css/default.css" rel="stylesheet"
    type="text/css" />
<%--JS Ref--%>
<script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
<script src="/Scripts/jquery.uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
<script src="/Scripts/jquery.uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
<script src="/Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
<script src="/Scripts/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script src="/Scripts/public-common-function.js" type="text/javascript"></script>
<script type="text/javascript">
    //从设定字符串找出对应值
    function FindValueInSetting(settingChangeParams, key) {
        var value = "";
        if (settingChangeParams != "") {
            var arraySetting = settingChangeParams.split("|");
            if (arraySetting.length > 0) {
                for (var i = 0; i < arraySetting.length; i++) {
                    if (arraySetting[i].indexOf(key) != -1) {
                        var array = arraySetting[i].split("^");
                        if (array.length == 2 && array[0] == key) {
                            value = array[1];
                            break;
                        }
                    }
                }
            }
        }
        return value;
    }
    //js获取url参数值 
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }
    $(document).ready(function () {
        //设定样式
        //高度
        var height = 60;
        var width = 380; //width: 400px;
        var heightTemp = GetQueryString("Height");
        var widthTemp = GetQueryString("Width");
        if (heightTemp != null) {
            height = heightTemp;
        }
        if (widthTemp != null) {
            width = widthTemp;
        }
        $("#fileQueue").attr("style", "height: " + height + "px" + ";width: " + width + "px");

        var fileExt = '*.xlsx;*.xls;'; //选择文件类型
        var fileDesc = '请选择excel格式文件'; //选择文件内容描述
        var buttonText = 'Browse File'; //按钮文本
        var isMulti = false; //是否允许上传多个文件
        var isAuto = true; //true当选择文件后就直接上传了，为false需要点击上传按钮才上传
        var simUploadLimit = 1; //允许同时上传的文件个数 
        var queueSizeLimit = 999; //单次上传的文件个数 
        var folder = '/Upload'; //上传文件夹
        var isAutoDisappear = false; //完成后是否自动消失
        var sizeLimit = 1048576 * 1000; //5M 上传文件的大小限制

        //根据控件属性设定值设定Js变量
        var settingChangeParams = $("#<%=hfSettingChangeParams.ClientID%>").val();
        var temp = "";
        if (settingChangeParams.indexOf("FileExt") != -1) {
            temp = FindValueInSetting(settingChangeParams, "FileExt");
            if (temp && temp != "") {
                fileExt = temp;
            }
        }
        if (settingChangeParams.indexOf("FileDesc") != -1) {
            temp = FindValueInSetting(settingChangeParams, "FileDesc");
            if (temp && temp != "") {
                fileDesc = temp;
            }
        }
        if (settingChangeParams.indexOf("ButtonText") != -1) {
            temp = FindValueInSetting(settingChangeParams, "ButtonText");
            if (temp && temp != "") {
                buttonText = temp;
            }
        }
        if (settingChangeParams.indexOf("IsMulti") != -1) {
            temp = FindValueInSetting(settingChangeParams, "IsMulti");
            if (temp && temp != "") {
                if (temp == "false") {
                    isMulti = false;
                }
                else {
                    isMulti = true;
                    //设置高度和宽度
                    $("#fileQueue").attr("style", "height: 260px;width: 460px");
                }
            }
        }
        if (settingChangeParams.indexOf("IsAuto") != -1) {
            temp = FindValueInSetting(settingChangeParams, "IsAuto");
            if (temp && temp != "") {
                if (temp == "false")
                    isAuto = false;
                else
                    isAuto = true;
            }
        }
        if (settingChangeParams.indexOf("SimUploadLimit") != -1) {
            temp = FindValueInSetting(settingChangeParams, "SimUploadLimit");
            if (temp && temp != "") {
                simUploadLimit = parseInt(temp);
            }
        }
        if (settingChangeParams.indexOf("QueueSizeLimit") != -1) {
            temp = FindValueInSetting(settingChangeParams, "QueueSizeLimit");
            if (temp && temp != "") {
                queueSizeLimit = parseInt(temp);
            }
        }
        if (settingChangeParams.indexOf("Folder") != -1) {
            temp = FindValueInSetting(settingChangeParams, "Folder");
            if (temp && temp != "") {
                folder = temp;
            }
        }
        if (settingChangeParams.indexOf("IsAutoDisappear") != -1) {
            temp = FindValueInSetting(settingChangeParams, "IsAutoDisappear");
            if (temp && temp != "") {
                if (temp == "false")
                    isAutoDisappear = false;
                else
                    isAutoDisappear = true;
            }
        }
        if (settingChangeParams.indexOf("SizeLimit") != -1) {
            temp = FindValueInSetting(settingChangeParams, "SizeLimit");
            if (temp && temp != "") {
                sizeLimit = parseInt(temp);
            }
        }
        if (isAutoDisappear == false)
            temp = "false";
        else
            temp = "true";
        $("#uploadify").uploadify({
            'uploader': '/Scripts/jquery.uploadify-v2.1.0/uploadify.swf',
            'script': "/Handle/Sys/CommonMethodHandle.ashx?meth=UploadifySaveFile$isAutoDisappear=" + temp,
            'cancelImg': '/Scripts/jquery.uploadify-v2.1.0/cancel.png',
            'folder': folder,                       //上传文件存放的目录 
            'queueID': 'fileQueue',                 //文件队列的ID
            'auto': isAuto,                         //设置为true当选择文件后就直接上传了，为false需要点击上传按钮才上传
            'multi': isMulti,                       //设置为true时可以上传多个文件
            'fileExt': fileExt,                     //设置可以选择的文件的类型，格式如：'*.doc;*.pdf;*.rar'
            'fileDesc': fileDesc,                   //这个属性值必须设置fileExt属性后才有效，用来设置选择文件对话框中的提示文本
            'buttonText': buttonText,               //浏览按钮的文本，默认值：BROWSE
            'buttonImg': "/Scripts/jquery.uploadify-v2.1.0/uploadFile.png", //浏览按钮的图片的路径 
            //'rollover': false,                         //值为true和false，设置为true时当鼠标移到浏览按钮上时有反转效果
            //'hideButton': false,                     //设置为true则隐藏浏览按钮的图片
            'simUploadLimit': simUploadLimit,       //允许同时上传的个数 默认值：1 
            'queueSizeLimit': queueSizeLimit,       //当允许多文件生成时，设置选择文件的个数，默认值：999
            'sizeLimit': sizeLimit,                 //上传文件的大小限制 
            'onSelect': function (event, queueId, fileObj) {
                if (isMulti == true) {
                    document.getElementById("lblUploadAllComplete").innerHTML = "";
                }
            },
            'onComplete': function (event, ID, fileObj, response, data) {
                if (fileObj) {
                    //artDialog.tips(fileObj.filePath + "上传成功,剩余未上传文件" + data.fileCount + "个.");
                    $("#<%=hfAllFilePath.ClientID%>").val($("#<%=hfAllFilePath.ClientID%>").val() + fileObj.filePath + "|");
                    info = fileObj.name + "上传成功,剩余未上传文件" + data.fileCount + "个.<br/>";
                }
                else {
                    artDialog.alert("onComplete参数出错.");
                    info = "onComplete参数出错.<br/>"
                }
                $("#fileQueue").append(info);
            },
            'onAllComplete': function (event, data) {
                if (data) {
                    //$("#<%=btnHidden.ClientID%>").click();
                    //artDialog.alert("所有文件上传完毕.本次共上传:" + data.filesUploaded + "个,出现错误:" + data.errors + "个,平均速度:" + data.speed.toFixed(2) + "kb/s,所有文件占用字节：" + data.allBytesLoaded + ".");
                    if (isMulti == true) {
                        document.getElementById("lblUploadAllComplete").innerHTML = "文件已上传完毕"
                    }
                }
                else {
                    artDialog.alert("onAllComplete参数出错.<br/>");
                }
            },
            'onCancel': function (event, queueId, fileObj, data) {
                if (fileObj && data) {
                    artDialog.tips("已从文件队列中移去一个文件.");
                }
                else {
                    artDialog.alert("onCancel参数出错.");
                }
            },
            'onQueueFull': function (event, queueSizeLimit) {
                if (queueSizeLimit) {
                    artDialog.tips("当前设定单次只允许上传" + queueSizeLimit + "个文件.");
                }
                else {
                    artDialog.alert("onQueueFull参数出错.");
                }
            },
            'onError': function (event, queueId, fileObj, errorObj) {
                if (fileObj && errorObj) {
                    var errorDesc = "文件:" + fileObj.name + ",错误类型:" + errorObj.type + ",错误描述:" + errorObj.info
                    artDialog.tips("上传出现错误," + errorDesc + ".");
                    $("#<%=hfAllErrInfo.ClientID%>").val(errorDesc + "|");
                    info = "上传出现错误," + errorDesc + "\n<br/>";
                }
                else {
                    artDialog.alert("onError参数出错.");
                    info = "onError参数出错.\n<br/>";
                }
                $("#fileQueue").append(info);

                //清空Session中保存的路径的值，避免再次上传时获取这个路径
                var url = "../../Handle/Sys/CommonMethodHandle.ashx";
                var data = { "meth": "ClearSessionOnUploadError" };
                var successFun = function (json) {
                };
                var errorFun = function (x, e) {
                };
                JsAjax(url, data, successFun, errorFun);
            }
        });
        if (isAuto) {
            $("#aUpload").hide();
            $("#spanSplit").hide();
            $("#aCancelUp").hide();
        }
        else {
            $("#aUpload").show();
            $("#spanSplit").show();
            $("#aCancelUp").show();
        }
    });  
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="fileQueue">
</div>
<div>
    <label id="lblUploadAllComplete" style="font-size: 14px; color: #FF0000; font-weight: bold;">
    </label>
</div>
<p style="font-size: large; font-weight: bold;">
    <input type="file" name="uploadify" id="uploadify" />
    <a href="javascript:$('#uploadify').uploadifyUpload()" id="aUpload">上传</a> <span
        id="spanSplit">|</span> <a href="javascript:$('#uploadify').uploadifyClearQueue()"
            id="aCancelUp">取消上传</a>
</p>
<!--<asp:UpdatePanel ID="upButton" runat="server">
    <ContentTemplate>
        <asp:Button ID="btnHidden" runat="server" Text="Button" OnClick="btnHidden_Click"
            Style="display: none;" />
    </ContentTemplate>
</asp:UpdatePanel>-->
<!--设定项参数传值隐藏域,只传递不为默认值的参数设定-->
<asp:HiddenField ID="hfSettingChangeParams" runat="server" />
<!--所有上传文件的路径-->
<input id="hfAllFilePath" type="hidden" runat="server" />
<!--上传文件过程中所有的错误信息-->
<input id="hfAllErrInfo" type="hidden" runat="server" />