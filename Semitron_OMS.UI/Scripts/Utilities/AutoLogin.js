﻿//==========================页面加载时JS函数开始===============================


//主框架切换及显示首次快捷菜单
$(function () {
    //关闭打开左栏目
    $("#sysBar").toggle(function () {
        $("#mainLeft").hide();
        $("#barImg").attr("src", "images/Web/butOpen.gif");
    }, function () {
        $("#mainLeft").show();
        $("#barImg").attr("src", "images/Web/butClose.gif");
    });
    //页面加载完毕，显示第一个子菜单
    $(".left_menu").hide();
    $(".left_menu:eq(0)").show();
});
//==========================页面加载时JS函数结束===============================

//===========================系统管理JS函数开始================================

//后台主菜单控制函数
function tabs(tabNum) {
    //设置点击后的切换样式
    $("#tabs ul li").removeClass("hover");
    $("#tabs ul li").eq(tabNum - 1).addClass("hover");
    //根据参数决定显示子菜单
    $(".left_menu").hide();
    $(".left_menu").eq(tabNum - 1).show();

    if ($("#barImg").attr("src") == "images/Web/butOpen.gif") {//点击主菜单，若子菜单为隐藏则显示
        $("#mainLeft").show();
        $("#barImg").attr("src", "images/Web/butClose.gif");
    }
}

//全选取消按钮函数，调用样式如：
function checkAll(chkobj) {
    if ($(chkobj).text() == "全选") {
        $(chkobj).text("取消");
        $(".checkall input").attr("checked", true);
    } else {
        $(chkobj).text("全选");
        $(".checkall input").attr("checked", false);
    }
}

//===========================系统管理JS函数结束================================
