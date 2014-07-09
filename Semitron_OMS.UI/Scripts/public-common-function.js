/**********************Start 基本公用方法 ***************/
$(function () {
    //温馨提示弹出框
    artDialog.notice = function (msg) {
        var opt = {}, api, aConfig, hide, top;
        var config = {
            id: 'Notice',
            title: '温馨提示',
            content: msg,
            icon: 'face-smile',
            fixed: true,
            drag: true,
            resize: true,
            follow: null,
            lock: false,
            okVal: "我知道了",
            ok: true,
            init: function (here) {
                api = this;
                aConfig = api.config;
            },
            close: true
        };
        for (var i in opt) {
            if (config[i] === undefined) config[i] = opt[i];
        };
        return artDialog(config);
    };
    //自定义弹出框
    artDialog.customize = function (options) {
        var opt = options;
        var config = {
            id: 'Customize',
            icon: 'face-smile',
            fixed: true,
            drag: true,
            resize: true,
            follow: null,
            lock: false,
            close: true
        };
        for (var i in opt) {
            if (config[i] === undefined) config[i] = opt[i];
        };
        return artDialog(config);
    };

    //设置只允许输入数字
    if ($(".OnlyInt").length > 0) {
        $(".OnlyInt").each(function () {
            $(this).keyup(function () {
                this.value = this.value.replace(/\D/g, ''); //正整型               
            });
        });
    }

    if ($(".OnlyAllInt").length > 0) {
        $(".OnlyAllInt").each(function () {
            $(this).keyup(function () {
                if (!isAllInt(this.value)) {
                    this.value = "";
                }
            });
        });
    }

    //设置只允许输入两位小数的浮点型
    if ($(".OnlyFloat").length > 0) {
        $(".OnlyFloat").each(function () {
            $(this).keyup(function () {
                if ((!isFloat(this.value) && this.value.indexOf('.') != this.value.length - 1) || (this.value.indexOf('.') != -1 && this.value.indexOf('.') < this.value.length - 5)) {
                    this.value = "";
                }
            });
        });
    }

    //文本框点击事件
    $("form :input.txt").click(function () {
        $(this).addClass("hoverTxt");
    }).blur(function () {
        $(this).removeClass("hoverTxt");
    });

    //文本框点击事件
    $("input.txt").click(function () {
        $(this).addClass("hoverTxt");
    }).blur(function () {
        $(this).removeClass("hoverTxt");
    });

    //为所有下拉框注册选择无效项时的判断与提示
    $("select").each(function () {
        $(this).change(function () {
            var selValue = $(this).val();
            if (selValue == -1 || selValue == -2) {
                artDialog.tips("此为分隔线，选择无效");
                $(this).val("");
                return;
            }
        });
    });
});

//设置日期控件当前默认值
function SetTimeControlDefault(startControlId, endControlId) {
    $("#" + startControlId).val(GetCurrentDateTimeString(1, true));
    $("#" + endControlId).val(GetCurrentDateTimeString(2, true));
}

//取得当天时间字符串
//Type类型 1 起始时间，形如 2000-01-01 00:00:00 2结束时间，形如 2000-01-01 23:59:59 3当前时间，形如 2012-12-17 12:47:
//bWithTime 是否包含时间部分
function GetCurrentDateTimeString(iType, bWithTime) {
    var nowDate = new Date();
    var dateStr = nowDate.getFullYear() + "-" + (nowDate.getMonth() + 1) + "-" + nowDate.getDate();
    var timeStr = "";
    if (bWithTime) {
        switch (iType) {
            case 1:
                timeStr = " 00:00:00";
                break;
            case 2:
                timeStr = " 23:59:59";
                break;
            case 3:
                var hour = nowDate.getHours();
                var minute = nowDate.getMinutes();
                var seconds = nowDate.getSeconds();
                timeStr = " " + (hour < 10 ? "0" + hour.toString() : hour.toString()) + ":" + (minute < 10 ? "0" + minute.toString() : minute.toString()) + ":" + (seconds < 10 ? "0" + seconds.toString() : seconds.toString());
                break;
        }
    }

    return dateStr + timeStr;;
}

//判断是否为浮点型
function isFloat(ch) {
    //var re = /^\d+(\.\d+)?$/;    //正浮点型
    var re = /^(-?\d+)(\.\d+)?$/;
    if (re.test(ch)) {
        return true;
    }
    return false;
}

function isAllInt(ch) {
    var re = /^(-?[1-9]\d{0,9}|0)$/;
    if (re.test(ch)) {
        return true;
    }
    return false;
}

//去掉空格
function Trim(str) {
    if (str && str != "") {
        return str.replace(/(^\s*)|(\s*$)/g, "");
    } else {
        return "";
    }
}
function checkCode(s) {
    if (s.indexOf("'") > -1 || s.indexOf("\"") > -1) {
        return false;
    } return true;
}

//JS封装AJAX
function JsAjax(url, data, successFun, errorFun) {
    try {
        $.ajax({
            url: "/Handle/Sys/Admin.ashx",

            type: "POST",
            dataType: "json",
            data: { "meth": "CheckTimeOut" },
            success: function (json) {
                if (!json || json.State == 0) {
                    alert("由于您长时间未操作，您的登陆已失效。请重新登陆。");

                    //window.open('http://bmp.taichenda.com/Admin/Login.aspx', '_parent');
                    top.location.href = '/Admin/Login.aspx';
                    return false;
                }

                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: data,
                    success: successFun
                });

                return true;
            }
        });
    } catch (err) {
        alert("由于您长时间未操作，您的登陆已失效。请重新登陆。");
        // window.open('http://bmp.taichenda.com/Admin/Login.aspx', '_parent');
        top.location.href = '/Admin/Login.aspx';
    }
}

//JS封装AJAX
function JsAjaxWithNoSession(url, data, type, successFun, errorFun) {
    try {
        $.ajax({
            url: url,
            type: "POST",
            dataType: type,
            data: data,
            success: successFun,
            error: errorFun
        });
    } catch (err) {

    }
}

//验证手机
function checkPhone(phone) {
    if (!phone) return false;
    var reg = /^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[0-9]\d{8}|18[0-9]\d{8}|14[0-9]\d{8}$/;
    var result = reg.test(phone);
    if (!result || phone.length > 11) {
        return false;
    }
    else {
        return true;
    }
}
//验证用户名
function checkUserName(str) {
    if (/^[a-z0-9_-]{4,16}$/.test(str)) {
        return true;
    } else {
        return false;
    }
}

//获取验证码
function reloadCode(img) {
    img.src = "/CheckCodePage.aspx?t=" + Math.random();
}


function Request(name) {
    new RegExp("(^|&)" + name + "=([^&]*)").exec(window.location.search.substr(1));
    return RegExp.$2
}

function replaceQuotes(value) {
    if (value) {
        value = value.replace(/"/ig, "“");
    }
    return value;
}
//加载选择下拉框数据
//url:路径 data:json参数串 selId:下拉框控件Id selValue：下拉框选中值 
//subLength：下拉框文字Title缩写的长度  bShowSelect：是否显示请选择项
function LoadSelect(url, data, selId, selValue, subLength, bShowSelect) {
    var bShowSelectTemp = true;
    if (bShowSelect == true || bShowSelect == false) {
        bShowSelectTemp = bShowSelect;
    }
    $.ajax({
        url: url,
        type: "POST",
        dataType: "text",
        data: data,
        success: function (result) {
            $("#" + selId + "").children("option").remove();
            if (bShowSelectTemp) {
                $("#" + selId + "").append("<option value='' title='请选择'>--请选择--</option>");
            }
            if (result != "") {
                var itemArr = result.split(",");
                if (itemArr.length > 0) {
                    for (var i = 0; i < itemArr.length; i++) {
                        var kv = itemArr[i].split("|");
                        if (kv.length == 2) {
                            $("#" + selId + "").append("<option value='" + kv[1] + "' title='" + kv[0] + "'>" + GetShortDiscription(kv[0], subLength) + "</option>");
                        }
                    }
                    if (selValue != "") {
                        $("#" + selId + "").val(selValue);
                    }
                    //下拉框值0处理，默认JS会识别0 == ""为True
                    //此处处理js会识别0 != ""为false的情况
                    if (parseInt(selValue) == 0) {
                        $("#" + selId + "").val(selValue);
                    }
                }
            }
        }
    });
}

//加载选择下拉框数据,无(--请选择--)项
function LoadSltNoSelect(result, sltid, subLength) {
    $("#" + sltid + "").children("option").remove();
    if (result != "") {
        var itemArr = result.split(",");
        if (itemArr.length > 0) {
            for (var i = 0; i < itemArr.length; i++) {
                var kv = itemArr[i].split("|");
                if (kv.length == 2) {
                    $("#" + sltid + "").append("<option value='" + kv[1] + "' title='" + kv[0] + "'>" + GetShortDiscription(kv[0], subLength) + "</option>");
                }
            }
        }
    }
    else {
        $("#" + sltid + "").append("<option value='' title='未创建项目'>--未创建项目--</option>");
    }
};

//当大于8个字符时,用...代替
function GetShortDiscription(value, SubLength) {
    if (!SubLength || SubLength < 0) {
        SubLength = 8;
    }
    if (value.length > SubLength) {
        value = value.substr(0, SubLength) + "...";
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

//获取选中的行
function SelectRows(id, msg) {
    var obj = $("#" + id).getSelectedRows();
    if (obj.length == 0) {
        msg = " 请在表格中选择一条需要操作的" + msg + "记录。";
        return msg;
    }
    if (obj.length > 1) {
        msg = "只允许对单条" + msg + "记录操作。";
        return msg;
    }
    return obj;
}
///获取表格中一行数据，供页面调用提示
function GetSelectedRow(id, msg) {
    var obj = SelectRows(id, msg);
    if (typeof (obj) == "string") {
        artDialog.tips(obj);
        return false;
    }
    return obj;
}

//正则验证邮箱
function checkEmial(value, id) {
    var pattern = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
    var chkFlag = pattern.test(value);
    if (chkFlag == false) {
        artDialog.tips("邮箱的格式不正确，请重新输入");
        $("#" + id).focus();
        return false;
    } else {
        return true;
    }
}

//验证url地址
function isURL(txt) {
    // return true;
    var re = new RegExp("((^http)|(^https)|(^ftp)):\/\/(\\w)+\.(\\w)+");
    //var re = new RegExp(strRegex);
    //re.test()
    if (re.test(txt)) {
        return (true);
    } else {
        return (false);
    }
}
/**********************End 基本公用方法 ***************/
/**********************Start 日期处理 ***************/
/// <summary>
/// 判定公历闰年遵循的一般规律为：四年一闰，百年不闰，四百年再闰。
/// 公历闰年的精确计算方法：（按一回归年365天5小时48分45.5秒）
/// 普通年能被4整除而不能被100整除的为闰年。 （如2004年就是闰年，1900年不是闰年）
/// 世纪年能被400整除而不能被3200整除的为闰年。 (如2000年是闰年，3200年不是闰年)
/// 对于数值很大的年份能整除3200,但同时又能整除172800则又是闰年。(如172800年是闰年，86400年不是闰年）
/// 
/// 公元前闰年规则如下：
/// 非整百年：年数除4余数为1是闰年，即公元前1、5、9……年；
/// 整百年：年数除400余数为1是闰年，年数除3200余数为1，不是闰年,年数除172800余1又为闰年，即公元前401、801……年。
/// </summary>
/// <param name="yN">年份数字</param>
/// <returns></returns>
function IsLeap(yN) {
    if ((yN % 400 == 0 && yN % 3200 != 0)
               || (yN % 4 == 0 && yN % 100 != 0)
               || (yN % 3200 == 0 && yN % 172800 == 0)) {
        return true;
    }
    else {
        return false;
    }

}

/// <summary>
/// 取得某一日期所在月份的最后一天所表示的数值
/// </summary>
/// <param name="datetime"></param>
/// <returns></returns>
function GetLastDayOfMonth(strdatetime) {
    var datetime = new Date(strdatetime.replace("-", "/"));
    var month = datetime.getMonth() + 1;
    switch (month) {
        case 1:
        case 3:
        case 5:
        case 7:
        case 8:
        case 10:
        case 12:
            return 31;
        case 4:
        case 6:
        case 9:
        case 11:
            return 30;
        case 2:
            if (IsLeap(datetime.getFullYear()) == true) {
                return 29;
            }
            else {
                return 28;
            }
    }
    return 30;
}

/**********************End 日期处理 ***************/

/**********************Start 表格动态分组与权限控制***************************************/
/**/
/**/
var searchItemsForHide = ""; //可能没有权限需要隐藏的查询条件，取值与权限表数据中的代码一致
var columnItemsForHide = ""; //可能没有权限需要隐藏的表格列，取值与权限表数据中的代码一致,代码以Column开始且后编码与表列名一致

function SetValuesForHide() {
    var hidCodes = $("#allHideCodes").val();
    if (hidCodes != null && hidCodes != "") {
        var arrItem = hidCodes.split(",");
        for (var i = 0; i < arrItem.length; i++) {
            if (arrItem[i] != "" && arrItem[i].indexOf("div") != -1) {
                searchItemsForHide += arrItem[i] + ",";
            }
            if (arrItem[i] != "" && arrItem[i].indexOf("Column") != -1) {
                columnItemsForHide += arrItem[i] + ",";
            }
        } //end for
    }
    if (searchItemsForHide != null && searchItemsForHide != "") {
        searchItemsForHide = searchItemsForHide.substring(0, searchItemsForHide.length - 1);
    }
    if (columnItemsForHide != null && columnItemsForHide != "") {
        columnItemsForHide = columnItemsForHide.substring(0, columnItemsForHide.length - 1);
    }
}

//根据权限设置查询条件
function SetSearchItems() {
    var hid = $("#hidDataSource").val(); //获取权限列表
    if (searchItemsForHide != null && searchItemsForHide != "") {
        var arrItem = searchItemsForHide.split(",");
        for (var i = 0; i < arrItem.length; i++) {
            if (arrItem[i] != "") {
                if (hid.indexOf(arrItem[i]) < 0)
                    $("#" + arrItem[i]).hide();
                else
                    $("#" + arrItem[i]).show();
            }
        }
    }
}

//判断是否为隐藏列
function IsHideColumn(columnName) {
    var hid = $("#hidDataSource").val(); //获取权限列表
    if (columnItemsForHide.indexOf("Column" + columnName) != -1 && hid.indexOf("Column" + columnName) == -1) {//为可能需要隐藏的列且又不在权限列表中
        return true; //说明列需要隐藏
    }
    //如果为特殊属性列，带toggle为false属性的列不显示
    var index = IsSpecColumn(columnName)
    if (index != -1) {
        if (arrHeaderStyle[index].length > 2) {
            if (arrHeaderStyle[index][2] == "false") {
                return true; //说明列需要隐藏
            }
        }
    }
    return false;
}

var arrHeaderStyle = [["CpInfo", "80"], ["CPName", "80"],
["SpInfo", "100"], ["SpName", "100"],
["ServiceCode", "140"], ["ServiceName", "140"],
["FeeCode", "140"], ["FeeCodeName", "140"],
["RouterName", "120"], ["RouterCode", "80"],
["StateInfo", "120"], ["Phone", "80"],
["Linkid", "140"], ["Addr", "100"],
["StatusMsg", "100"], ["OnlyState", "50"],
["Valid", "40"], ["ID", "40"],
["MTType", "80"], ["HaveMTMsg", "80"],
["OuterUserDayMax", "80"], ["InnerUserDayMax", "80"],
["OuterUserMonthMax", "80"], ["InnerUserMonthMax", "80"],
["OuterRouterDayMax", "80"], ["InnerRouterDayMax", "80"],
["OuterRouterMonthMax", "80"], ["InnerRouterMonthMax", "80"],
["ProName", "100"], ["AvoidDeductNum", "40"],
["CreateTime", "115"], ["ReportTime", "115"], ["UpdateTime", "115"]
];
//是否为需要设置特殊属性的列
function IsSpecColumn(columnName) {
    for (var i = 0; i < arrHeaderStyle.length; i++) {
        if (arrHeaderStyle[i][0] == columnName) {
            return i; //返回所在数据的位置
        }
    }
    return -1;
}

//取得表格标题行属性参数配置数组,包含权限控制，不包括偏好处理
function GetDefaultArray() {
    var defaultArray = "";
    defaultArray = " {display: '行索引', name: 'rownum', toggle: false, iskey: true, width: 65, align: 'center',hide:true},";
    if (IsSpecColumn("rownum") != -1) {
        defaultArray = " {display: '行索引', name: 'rownum', toggle: true, iskey: true, width: 40, align: 'center',hide:false},";
    }
    if (arrGridHeader.length > 0) {
        for (var i = 0; i < arrGridHeader.length; i++) {
            if (arrGridHeader[i][0] != "" && arrGridHeader[i][1] != "") {
                if (IsHideColumn(arrGridHeader[i][1])) {
                    defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: 65, sortable: true, align: 'right',toggle: false,hide:true },";
                }
                else {
                    var index = IsSpecColumn(arrGridHeader[i][1]);
                    if (index == -1) {
                        defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: 65, sortable: true, align: 'right' },";
                    }
                    else {
                        defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: " + arrHeaderStyle[index][1] + ", sortable: true";
                        if (arrHeaderStyle[index].length > 2 && arrHeaderStyle[index][2] == "false") {
                            defaultArray += ",toggle:false,hide:true";
                        }
                        else {
                            defaultArray += ",toggle:true";
                        }
                        if (arrHeaderStyle[index].length > 3 && arrHeaderStyle[index][3] != "") {
                            defaultArray += ",align:'" + arrHeaderStyle[index][3] + "'";
                        }
                        else {
                            defaultArray += ",align:'center'";
                        }
                        defaultArray += " },";
                    }
                }
            }
        }
    }
    if (defaultArray != "") {
        defaultArray = defaultArray.substring(0, defaultArray.length - 1);
    }
    return defaultArray;
}

/************************************start 偏好************************************************/
//取得表格标题行属性参数配置数组,包含权限控制，偏好处理
function GetDefaultPreferArray(columnPrefer) {
    var defaultArray = "";
    var strPrefer = ",hide:false";
    defaultArray = " {display: '行索引', name: 'rownum', toggle: false, iskey: true, width: 65, align: 'center',hide:true},";
    if (IsSpecColumn("rownum") != -1) {
        defaultArray = " {display: '行索引', name: 'rownum', toggle: true, iskey: true, width: 40, align: 'center',hide:false},";
    }
    if (arrGridHeader.length > 0) {
        for (var i = 0; i < arrGridHeader.length; i++) {
            if (arrGridHeader[i][0] != "" && arrGridHeader[i][1] != "") {
                if (IsHideColumn(arrGridHeader[i][1])) {
                    defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: 65, sortable: true, align: 'right',toggle: false,hide:true },";
                }
                else {
                    //是否为偏好中列
                    if (columnPrefer && columnPrefer != "") {
                        if (columnPrefer.indexOf(arrGridHeader[i][1]) != -1) {
                            strPrefer = ",hide:false";
                        }
                        else {
                            strPrefer = ",hide:true";
                        }
                    }

                    var index = IsSpecColumn(arrGridHeader[i][1]);
                    if (index == -1) {
                        defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: 65, sortable: true, align: 'right'" + strPrefer + " },";
                    }
                    else {
                        defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: " + arrHeaderStyle[index][1] + ", sortable: true";
                        if (arrHeaderStyle[index].length > 2 && arrHeaderStyle[index][2] == "false") {
                            defaultArray += ",toggle:false,hide:true";
                        }
                        else {
                            defaultArray += strPrefer;
                        }
                        if (arrHeaderStyle[index].length > 3 && arrHeaderStyle[index][3] != "") {
                            defaultArray += ",align:'" + arrHeaderStyle[index][3] + "'";
                        }
                        else {
                            defaultArray += ",align:'right'";
                        }
                        defaultArray += " },";
                    }
                }
            }
        }
    }
    if (defaultArray != "") {
        defaultArray = defaultArray.substring(0, defaultArray.length - 1);
    }
    return defaultArray;
}

//取得表格标题行属性参数配置数组,包含权限控制，偏好处理但排除掉恒显示的分组条件隐藏
function GetDefaultPreferArrayWithGroup(columnPrefer, strGroupColumn) {
    var defaultArray = "";
    var strPrefer = ",hide:false";
    defaultArray = " {display: '行索引', name: 'rownum', toggle: false, iskey: true, width: 65, align: 'center',hide:true},";
    if (IsSpecColumn("rownum") != -1) {
        defaultArray = " {display: '行索引', name: 'rownum', toggle: true, iskey: true, width: 40, align: 'center',hide:false},";
    }
    if (arrGridHeader.length > 0) {
        for (var i = 0; i < arrGridHeader.length; i++) {
            if (arrGridHeader[i][0] != "" && arrGridHeader[i][1] != "") {
                if (IsHideColumn(arrGridHeader[i][1])) {//不为权限范围内
                    defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: 65, sortable: true, align: 'right',toggle: false,hide:true },";
                }
                else {
                    if (columnPrefer && columnPrefer != "") {
                        if (columnPrefer.indexOf(arrGridHeader[i][1]) != -1) {//为偏好设定内
                            strPrefer = ",hide:false";
                        }
                        else {//不为偏好设定范围内
                            if (strGroupColumn.indexOf(arrGridHeader[i][1]) != -1)//但为分组列恒显表格列
                            {
                                strPrefer = ",hide:false";
                            }
                            else {
                                strPrefer = ",hide:true";
                            }
                        }
                    }

                    var index = IsSpecColumn(arrGridHeader[i][1]);
                    if (index == -1) {
                        defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: 65, sortable: true, align: 'right'" + strPrefer + " },";
                    }
                    else {
                        defaultArray += "{ display: '" + arrGridHeader[i][0] + "', name: '" + arrGridHeader[i][1] + "', width: " + arrHeaderStyle[index][1] + ", sortable: true, align: 'right'";
                        if (arrHeaderStyle[index].length > 2 && arrHeaderStyle[index][2] == "false") {
                            defaultArray += ",toggle:false,hide:true";
                        }
                        else {
                            defaultArray += strPrefer;
                        }
                        if (arrHeaderStyle[index].length > 3 && arrHeaderStyle[index][3] != "") {
                            defaultArray += ",align:'" + arrHeaderStyle[index][3] + "'";
                        }
                        else {
                            defaultArray += ",align:'right'";
                        }
                        defaultArray += " },";
                    }
                }
            }
        }
    }
    if (defaultArray != "") {
        defaultArray = defaultArray.substring(0, defaultArray.length - 1);
    }
    return defaultArray;
}
//表格列树的显示
var zColumnNodes, zColumnTree, zSearchNodes, zSearchTree;
var setting = {
    check: {
        enable: true,
        chkboxType: { "Y": "s", "N": "s" }
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        onCheck: zTreeOnCheck
    }
};

//每次点击树checkbox后，响应事件处理
function zTreeOnCheck(event, treeId, treeNode) {
    switch (treeId) {
        case "SearchTree":
            if (treeNode.checked == false) {
                $("#" + treeNode.id).hide(); //隐藏查询条件
            }
            else {
                $("#" + treeNode.id).show(); //查询条件
            }
            break;
    }
};

//向某一树增加一根节点
function zTreeAddNode(treeId, nodeId, nodeName, checked) {
    var treeObj = $.fn.zTree.getZTreeObj(treeId);
    var newNode = { id: nodeId, name: nodeName, isParent: false, Code: ',1,', checked: checked };
    treeObj.addNodes(null, newNode);
}

//向某一树删除一根结点
function zTreeDeleteNode(treeId, nodeId) {
    var treeObj = $.fn.zTree.getZTreeObj(treeId);
    var nodes = treeObj.getNodes();
    var index = -1;
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].id == nodeId) {
            treeObj.removeNode(nodes[i]);
            break;
        }
    }
}

//对树结点进行操作
function zTreeNodeManipulate(type, treeId, nodeId, nodeName, checked) {
    //如果操作类型增加
    //1.结点已存在，刚不增加
    //2.结点不存在，增加
    //如果操作类型删除
    //1.结点已存在，则删除
    //2.结点不存存在，不做处理
    //取得所有的结点
    var treeObj = $.fn.zTree.getZTreeObj(treeId);
    if (treeObj) {
        var nodes = treeObj.getNodes();
        var bool = false; //标记结点是否存在
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].id == nodeId) {
                bool = true;
                break;
            }
        }
        switch (type) {
            case "Add":
                if (!bool) {
                    zTreeAddNode(treeId, nodeId, nodeName, checked);
                }
                break;
            case "Delete":
                if (bool) {
                    zTreeDeleteNode(treeId, nodeId);
                }
                break;
        }
    }
};

//根据权限与偏好设定表获取偏好中设定的列
function GetColumnTreesNotes(strColumnConfig) {
    var defaultNotes = "";
    if (arrGridHeader.length > 0) {
        for (var i = 0; i < arrGridHeader.length; i++) {
            if (arrGridHeader[i][0] != "" && arrGridHeader[i][1] != "") {
                if (!IsHideColumn(arrGridHeader[i][1])) { //只显示出有权限的列
                    var isChecked = ", checked: false";
                    if (strColumnConfig) {//有配置过偏好
                        if (strColumnConfig.indexOf(arrGridHeader[i][1]) != -1 || strColumnConfig == "") {
                            isChecked = ", checked: true";
                        }
                        else {
                            isChecked = ", checked: false";
                        }
                    }
                    else {
                        isChecked = ", checked: true"; //默认全部置为选中
                    }
                    defaultNotes += "{ 'id': '" + arrGridHeader[i][1] + "', 'name': '" + arrGridHeader[i][0] + "','isParent': false, 'Code': ',1,'" + isChecked + "},";
                }
            }
        }
    }
    if (defaultNotes != "") {
        defaultNotes = defaultNotes.substring(0, defaultNotes.length - 1);
    }
    return eval("[" + defaultNotes + "]");
}

//判断是否为有权限的查询条件
function IsHideSearch(searchName) {
    var hid = $("#hidDataSource").val(); //获取权限列表
    if (searchItemsForHide.indexOf(searchName) != -1 && hid.indexOf(searchName) == -1) {//为可能需要隐藏的列且又不在权限列表中
        return true; //说明列需要隐藏
    }
    return false;
}

//根据权限与偏好设定表获取偏好中设定的可显示查询条件
function GetSearchTreesNotes(strSearchConfig) {
    var defaultNotes = "";
    if (arrSearchShow.length > 0) {
        for (var i = 0; i < arrSearchShow.length; i++) {
            if (arrSearchShow[i][0] != "" && arrSearchShow[i][1] != "") {
                if (!IsHideSearch(arrSearchShow[i][1])) { //只显示出有权限的查询条件
                    var isChecked = ", checked: false";
                    if (strSearchConfig) {//有配置过偏好
                        if (strSearchConfig.indexOf(arrSearchShow[i][1]) != -1 || strSearchConfig == "") {
                            isChecked = ", checked: true";
                        }
                        else {
                            isChecked = ", checked: false";
                        }
                    }
                    else {
                        isChecked = ", checked: true"; //默认全部置为选中
                    }
                    defaultNotes += "{ 'id': '" + arrSearchShow[i][1] + "', 'name': '" + arrSearchShow[i][0] + "','isParent': false, 'Code': ',1,'" + isChecked + "},";
                }
            }
        }
    }
    if (defaultNotes != "") {
        defaultNotes = defaultNotes.substring(0, defaultNotes.length - 1);
    }
    return eval("[" + defaultNotes + "]");
}

//获取指定主键的查询参数串中的值
//strSearchParam：查询字符串中的查询参数串 strKey：主键字符串
function GetValueInSearchParam(strSearchParam, strKey) {
    //根据保存值设定初始值
    if (strSearchParam && strSearchParam != "") {
        var arrKeyValue = strSearchParam.split(",");
        for (var i = 0; i < arrKeyValue.length; i++) {
            var arrTemp = arrKeyValue[i].split(":");
            if (arrTemp.length == 2) {
                if (arrTemp[0] != "") {
                    if (arrTemp[0] == strKey)
                        return arrTemp[1];
                }
            }
            else if (arrTemp.length > 2) {
                var index = arrKeyValue[i].indexOf(':');
                if (arrTemp[0] == strKey) {
                    return arrKeyValue[i].substring(index + 1);
                }
            }
        }
    }

    return "";
}

//获取指定查询控件id的默认偏好查询条件值
//strSearchParam：带控件Id的默认偏好查询条件串 strControlId：控件Id
function GetSearchControlPreferValue(strSearchParam, strControlId) {
    //根据保存值设定初始值
    if (strSearchParam && strSearchParam != "") {
        var arrKeyValue = strSearchParam.split(",");
        for (var i = 0; i < arrKeyValue.length; i++) {
            var arrTemp = arrKeyValue[i].split(":");
            if (arrTemp.length == 2) {
                if (arrTemp[0] != "") {
                    if (arrTemp[0] == strControlId)
                        return arrTemp[1];
                }
            }
        }
    }

    return "";
}

//初始设定所有查询条件的默认偏好查询条件值
function InitSearchDefaultValue(strSearchParam) {
    //根据保存值设定初始值
    if (strSearchParam && strSearchParam != "") {
        //置所有值为空
        var arrSearchCtrl;
        if (allSearchControlIds && allSearchControlIds != "") {
            arrSearchCtrl = allSearchControlIds.split(',');
            if (arrSearchCtrl.length > 0) {
                for (var i = 0; i < arrSearchCtrl.length; i++) {
                    if (arrSearchCtrl[i] != "") {
                        $("#" + arrSearchCtrl[i]).val("");
                    }
                }
            }
        }

        var arrKeyValue = strSearchParam.split(",");
        for (var i = 0; i < arrKeyValue.length; i++) {
            var arrTemp = arrKeyValue[i].split(":");
            if (arrTemp.length == 2) {
                if (arrTemp[0] != "") {
                    $("#" + arrTemp[0]).val(arrTemp[1]);
                }
            }
            else if (arrTemp.length > 2) {
                var index = arrKeyValue[i].indexOf(':');
                $("#" + arrTemp[0]).val(arrKeyValue[i].substr(index + 1, arrKeyValue[i].length));
            }
        }
    }
}

//在权限范围内加载偏好查询条件
function InitSearchShow(strSearchShow) {
    //在权限范围内加载偏好查询条件
    if (strSearchShow && strSearchShow != "") {
        if (arrSearchShow.length > 0) {
            for (var i = 0; i < arrSearchShow.length; i++) {
                if (arrSearchShow[i][0] != "" && arrSearchShow[i][1] != "") {
                    $("#" + arrSearchShow[i][1]).hide(); //隐藏所有查询条件
                }
            }
        }
        var arrPreferSearchshow = strSearchShow.split(",");
        for (var i = 0; i < arrPreferSearchshow.length; i++) {
            if (!IsHideSearch(arrPreferSearchshow[i])) {
                $("#" + arrPreferSearchshow[i]).show(); //显示有权限及设定为偏好的查询条件
            }
            else {
                $("#" + arrPreferSearchshow[i]).hide();
            }
        }
    }
}

//在权限范围内加载偏好分组条件
function InitGroupParam(strGroupParam) {
    if (strGroupParam && strGroupParam != "") {
        //首先全部反选所有复选框
        var arrGropControl = allGroupControlIds.split(",");
        if (arrGropControl.length > 0) {
            for (var i = 0; i < arrGropControl.length; i++) {
                if (arrGropControl[i] != "") {
                    $("#" + arrGropControl[i])[0].checked = false; //隐藏所有分组条件
                }
            } //for
        } //if

        var arrPreferGroupParam = strGroupParam.split(",");
        for (var i = 0; i < arrPreferGroupParam.length; i++) {
            if (!IsHideSearch(arrPreferGroupParam[i])) {
                $("#" + arrPreferGroupParam[i])[0].checked = true; //显示有权限及设定为偏好的分组条件
            }
            else {
                $("#" + arrPreferGroupParam[i])[0].checked = false;
            }
        }
    } //if (strGroupParam != "")
}

//取得所有查询条件偏好的查询条件keyValue值
function GetSearchPreferKeyValue() {
    var arrSearchCtrl;
    var keyValue = "";
    if (allSearchControlIds && allSearchControlIds != "") {
        arrSearchCtrl = allSearchControlIds.split(',');

        for (var i = 0; i < arrSearchCtrl.length; i++) {
            if (arrSearchCtrl[i] != "" && !$("#" + arrSearchCtrl[i]).is(":hidden")) {
                var selValue = $("#" + arrSearchCtrl[i]).val();
                if (selValue && selValue != "") {
                    keyValue += arrSearchCtrl[i] + ":" + selValue.replace("'", "‘").replace(",", "") + ",";
                }
            };
        }
    }
    if (keyValue != "") {
        keyValue = keyValue.substring(0, keyValue.length - 1);
    }
    return keyValue;
}

//取得偏好的分组条件ids
function GetGroupPrefer() {
    var arrGroupCtrl;
    var key = "";
    if (allGroupControlIds && allGroupControlIds != "") {
        arrGroupCtrl = allGroupControlIds.split(',');
        for (var i = 0; i < arrGroupCtrl.length; i++) {
            if (arrGroupCtrl[i] != "" && $("#" + arrGroupCtrl[i])[0].checked == true) {
                key += arrGroupCtrl[i] + ",";
            }
        }
    }
    if (key != "") {
        key = key.substring(0, key.length - 1);
    }
    return key;
}
/*******************************End 偏好************************************************/

/**/
/**/
/**********************End 表格动态分组与权限控制***************************************/

/****************************Start 树形控件点击一父节点后收缩其他父节点*********************************************/
var zShrinkNodes, zShrinkTree;      //此类树节点
var zShrinkTreeId = "AreaTree";  //树控件Id名称，默认为AreaTree，不同时在页面中重载
var settingShrinkTree = {         //此类树控件设定参数
    check: {
        enable: true,
        chkboxType: { "Y": "s", "N": "s" }
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
function clickTreeShrinkTree(e, treeId, treeNode) {
    var zShrinkTree = $.fn.zTree.getZTreeObj(zShrinkTreeId);
    zShrinkTree.expandNode(treeNode, null, null, null, true);
}
var curExpandNode = null;
function beforeExpandShrinkTree(treeId, treeNode) {
    var pNode = curExpandNode ? curExpandNode.getParentNode() : null;
    var treeNodeP = treeNode.parentTId ? treeNode.getParentNode() : null;
    var zShrinkTree = $.fn.zTree.getZTreeObj(zShrinkTreeId);
    for (var i = 0, l = !treeNodeP ? 0 : treeNodeP.length; i < l; i++) {
        if (treeNode !== treeNodeP.children[i]) {
            zShrinkTree.expandNode(treeNodeP.children[i], false);
        }
    }
    while (pNode) {
        if (pNode === treeNode) {
            break;
        }
        pNode = pNode.getParentNode();
    }
    if (!pNode) {
        singlePathShrinkTree(treeNode);
    }
}

function singlePathShrinkTree(newNode) {
    if (newNode === curExpandNode) return;
    if (curExpandNode && curExpandNode.open == true) {
        var zShrinkTree = $.fn.zTree.getZTreeObj(zShrinkTreeId);
        if (newNode.parentTId === curExpandNode.parentTId) {
            zShrinkTree.expandNode(curExpandNode, false);
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
                            zShrinkTree.expandNode(oldParents[i], false);
                            break;
                        }
                    }
                } else {
                    zShrinkTree.expandNode(oldParents[oldParents.length - 1], false);
                }
            }
        }
    }
    curExpandNode = newNode;
}

function onExpandShrinkTree(event, treeId, treeNode) {
    curExpandNode = treeNode;

    //控制文本框的输入长度
    function ControlTextLength(textBox, maxlimit) {
        var lenght = field.value.replace(/[^\x00-\xff]/g, "**").length;
        var tempString = field.value;
        var temp = "";
        if (lenght > maxlimit) {
            for (var i = 0; i < maxlimit; i++) {
                if (temp.replace(/[^\x00-\xff]/g, "**").length < maxlimit)
                    temp = tempString.substr(0, i + 1);
                else
                    break;
            }
            if (temp.replace(/[^\x00-\xff]/g, "**").length > maxlimit)
                temp = temp.substr(0, tt.length - 1);
            field.value = temp;
        }
        else {
            ;
        }
    }
}
/********************************End  树形控件点击一父节点后收缩其他父节点*******************************************/

/****************************Start 文本框输入长度控制*********************************************/
function CutStrLength(str, Ilength) {
    var tmp = 0;
    var len = 0;
    var okLen = 0
    for (var i = 0; i < Ilength; i++) {
        if (str.charCodeAt(i) > 255)
            tmp += 2
        else
            len += 1
        okLen += 1
        if (tmp + len == Ilength) {
            return (str.substring(0, okLen));
            break;
        }
        if (tmp + len > Ilength) {
            return (str.substring(0, okLen - 1) + "");
            break;
        }
    }
}
function checkFieldLength(fieldName, fieldLength) {
    var str = document.getElementById(fieldName).value;
    var theLen = 0;
    var teststr = '';
    for (i = 0; i < str.length; i++) {
        teststr = str.charAt(i);
        if (str.charCodeAt(i) > 255)
            theLen = theLen + 2;
        else
            theLen = theLen + 1;
    }
    //document.getElementById('showMsg').innerText = theLen;
    if (theLen > fieldLength) {
        //document.getElementById('showMsg').innerText = fieldDesc;
        //alert(fieldDesc+" 的字段长度超过规定长度！");
        //document.getElementById(fieldName).focus();
        document.getElementById(fieldName).value = CutStrLength(str, fieldLength);
        //alert("输入字符串的长度已超过规定长度！");
        return false;
    }
    else {
        return true;
    }
}
/****************************End 文本框输入长度控制*********************************************/

/****************************Start 查询区域展开折叠*********************************************/
//listID:数据列表Table的id名
//btnID：展开或收起的按钮名称
//expandID:隐藏展开的控件IDexpandID
//expReduction：展开时应减去的高度
//narReduction：收起时应减去的高度
function InitExpandSearch(listID, btnID, expandID, expReduction, narReduction) {
    $("#" + btnID).click(function () {
        OpenCloseSearch(listID, btnID, expandID, expReduction, narReduction);
        return false;
    });
}

//
function OpenCloseSearch(listID, btnID, expandID, expReduction, narReduction) {
    var text = document.getElementById(btnID).innerHTML;
    if (text) {
        try {
            if (text.indexOf("收起") >= 0) {
                document.getElementById(btnID).innerHTML = "展开▽"
                $("#" + expandID).hide();
                $("#" + listID).flexReHeight(narReduction, listID);
            }
            else {
                document.getElementById(btnID).innerHTML = "收起△"
                $("#" + expandID).show();
                $("#" + listID).flexReHeight(expReduction, listID);
            }
        }
        catch (e) { }
    }
}
/****************************End 查询区域展开折叠*********************************************/