/****************************Start 审核操作、审核记录Html统一*********************************************/
function InitAuditorHtml() {
    $(".dialogDiv").append(GetAuditorHtml());
}

//编写返回审核操Html源码
function GetAuditorHtml() {
    var strHtml = "<div id=\"divAuditor\"> <div style=\"line-height: 25px;\"> 审核结果：<select id=\"sltAuditorResult\" class=\"txt\" style=\"width: 60px;\" runat=\"server\"> <option value=\"1\">同意</option> <option value=\"-1\">打回</option> <option value=\"-2\">否决</option> </select> </div><div><div style=\"float:left; padding-top:17px;\">审核意见：</div><div style=\"float:left;\"><textarea id=\"txtAreaAuditorInfo\" cols=\"40\" rows=\"3\"></textarea> </div></div></div>"; ;
    return strHtml;
}

function InitAuditorValue() {
    $("#sltAuditorResult").val("1");
    $("#txtAreaAuditorInfo").val("");
}

function InitOperationsLogHtml() {
    $(".dialogDiv").append("<div id=\"divOperationsLog\"> <iframe id=\"iframeOperationsLog\" scrolling=\"no\" frameborder=\"0\" width=\"850px\" height=\"450px\"> </iframe> </div>");
}
/****************************End 审核操作、审核记录Html统一*********************************************/
function PopUploadControlDialog(titleName) {
    $.dialog({
        content: $("#divImportFile").get(0),
        id: 'divImportFile',
        title: titleName,
        padding: 0,
        lock: true,
        init: function () {

        },
        ok: function () {
            //当文件上传后，取得上传成功文件的路径,Type三种取值方式:Physical,Http,PhysicalHttp
            //Physical只取得物理路径 Http只取得文件服务器上文件Url地址 HttpPhysical取得物理路径和Url路径
            //json.Remark存放物理路径 json.Info存放Url路径 多个文件间用$隔开 
            //物理路径注意将$替换成\ Url路径将$替换成/
            var url = "/Handle/Sys/CommonMethodHandle.ashx";
            var data = { "meth": "GetUploadFilePath", "Type": "Http" };
            var successFun = function (json) {
                if (json && json.State == 0) {
                    artDialog.alert(json.Info);
                    return false;
                }
                var urlPaths = json.Info.replace(/\*/g, "/");
                $(".divAttachment").append(GetFileDivHtml(urlPaths));
                return true;
            };
            var errorFun = function (x, e) {
                alert(x.responseText);
                return false;
            };
            JsAjax(url, data, successFun, errorFun);
            return true;

        },
        cancel: function () {
            $.dialog.list["divImportFile"].close();
            return true;
        }
    });
}
//获取附件列表层的HTML
//urlPaths 附件URL地址"|" 分隔符字串
function GetFileDivHtml(urlPaths) {
    var divHtml = "";
    var arrUrl = urlPaths.split("|");
    if (arrUrl.length > 0 && arrUrl != "") {
        for (var i = 0; i < arrUrl.length; i++) {
            var guidObj = Guid.NewGuid();
            var strFileName = arrUrl[i];
            strFileName = strFileName.substr(strFileName.lastIndexOf('/') + 13)
            divHtml += "<div class=\"aFile\" id=\"" + guidObj.ToString("N") + "\"><a target=\"_blank\" href=\"" + arrUrl[i] + "\">" + strFileName + "</a><div class=\"cancelFile\"><a onclick=\"javascript:jQuery('.divAttachment').children('#" + guidObj.ToString("N") + "').remove()\"><img src=\"/Scripts/jquery.uploadify-v2.1.0/cancel.png\" border=\"0\" /></a></div></div>";
        }
    }
    return divHtml;
}

