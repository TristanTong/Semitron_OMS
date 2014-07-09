$(function () {
    //设置只允许输入数字
    if ($(".OnlyInt").length > 0) {
        $(".OnlyInt").each(function () {
            $(this).keyup(function () {
                this.value = this.value.replace(/\D/g, '');
            });

        });
    }


    //文本框点击事件
    $("form :input.txt").click(function () {
        $(this).addClass("hoverTxt");
    }).blur(function () {
        $(this).removeClass("hoverTxt");
    });

});


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
            url: "/Handel/SYS/Admin.ashx",
            type: "POST",
            dataType: "json",
            data: { "meth": "CheckTimeOut" },
            success: function (json) {
                if (!json || json.State == 0) {
                    alert("由于您长时间未操作，您的登陆已失效。请重新登陆。");
                    top.location.href = "/Admin/Login.aspx";
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
        top.location.href = "/Admin/Login.aspx";
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

        