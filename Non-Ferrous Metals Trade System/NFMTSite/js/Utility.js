

var cssUrl = "../jqwidgets/styles/jqx." + $.jqx.theme + ".css";
var link = $('<link rel="stylesheet" href="' + cssUrl + '" media="screen" />');

$(document).find('head').append(link);

window.onload = function () {
    var f = $.getUrlParam("f");
    //判断参数中是否存在f
    if (f != null && f.length > 0) {
        $("#jqNavigation").jqxPanel("destroy");
        document.getElementById("buttons").style.display = "none";
        $("#jqxAuditExpander").jqxExpander("destroy");
    }
}

function buildQueryString(data) {
    var str = "";
    for (var prop in data) {
        if (data.hasOwnProperty(prop)) {
            str += prop + "=" + data[prop] + "&";
        }
    }
    return str.substr(0, str.length - 1);
}

//获取当前URL的参数值
(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
})(jQuery);

function ShowModalDialog(params, e) {

    // 数据参数
    var paramObj = params || {};
    //// 模态窗口高度和宽度
    //var whparamObj = { width: $(window).width() / 3, height: $(window).height() / 2 };
    //// 相对于浏览器的居中位置
    //var bleft = ($(window).width() - 500) / 2;
    //var btop = ($(window).height() - 500) / 2;
    //// 根据鼠标点击位置算出绝对位置
    //var tleft = e.screenX - e.clientX;
    //var ttop = e.screenY - e.clientY;
    //// 最终模态窗口的位置
    //var left = bleft + tleft;
    //var top = btop + ttop;


    var width;
    var height;
    var left;
    var top;
    if (document.body.scrollWidth > (window.screen.availWidth - 100)) {
        width = (window.screen.availWidth - 100).toString()
    } else {
        width = (document.body.scrollWidth + 50).toString()
    }

    if (document.body.scrollHeight > (window.screen.availHeight - 70)) {
        height = (window.screen.availHeight - 50).toString()
    } else {
        height = (document.body.scrollHeight + 115).toString()
    }

    left = ((document.documentElement.clientWidth - 650) / 2).toString()
    top = ((document.documentElement.clientHeight - 300) / 2).toString()

    if (Sys.chrome) {
        // 页面参数
        var p = "toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no,";
        p += "width= 650,";
        p += "height=300,";
        p += "left=" + ((screen.width - 650) / 2).toString() + ",";
        p += "top=" + ((screen.height - 300) / 2).toString();

        return window.open("../WorkFlow/SubmitAudit.aspx?mid=" + paramObj.mid + "&model=" + paramObj.model, "_blank", p);
    }
    else {
        // 页面参数
        var p = "center=yes;status=no;help=no;scroll=no;resizable=no;location=no;toolbar=no;";
        p += "dialogWidth=650px;";
        p += "dialogHeight=300px;";
        p += "dialogLeft=" + left + "px;";
        p += "dialogTop=" + top + "px;";

        return window.showModalDialog("../WorkFlow/SubmitAudit.aspx?mid=" + paramObj.mid, paramObj, p);
    }
};

///////////////////////////判断浏览器并重写window.showModelessDialog方法///////////////////////////
var Sys = {};
var ua = navigator.userAgent.toLowerCase();
var s;
(s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
(s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
(s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
(s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
(s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
if (Sys.ie) {
    var originFn = window.showModelessDialog;
    window.showModelessDialog = function (url) {
        var OpenedWindow = originFn(url, arguments[1], arguments[2]);
        OpenedWindow.opener = window;
    }
    //if (Sys.ie == '9.0') {//Js判断为IE 9
    //} else if (Sys.ie == '8.0') {//Js判断为IE 8
    //} else {
    //}
}
if (Sys.firefox) {
    window.showModelessDialog = function (url) {
        var windowName = (arguments[1] == null ? "" : arguments[1].toString());
        var feature = (arguments[2] == null ? "" : arguments[2].toString());
        var OpenedWindow = window.open(url, windowName, feature);
        //window.addEventListener("click", function () { OpenedWindow.focus(); }, false);
        return OpenedWindow;
    }
}
if (Sys.chrome) {
}
if (Sys.opera) {
}
if (Sys.safari) {
}

function ShowModelessDialog(url) {
    var width = document.documentElement.clientWidth * 0.8;
    var height = document.documentElement.clientHeight * 0.7;
    var left = document.documentElement.clientWidth * 0.1;
    var top = document.documentElement.clientHeight * 0.15;

    if (Sys.firefox) {
        return window.showModelessDialog(url, "", "width=" + width + ",height=" + height + ",left = " + left + ",top =" + top);
    }
    else if (Sys.ie) {
        return window.showModelessDialog(url, "", "dialogHeight:" + height + "px;dialogWidth:" + width + "px;");
    }
    else if (Sys.chrome) {
        return window.open(url, "", "width=" + width + ",height=" + height + ",left = " + left + ",top =" + top);
    }
};

function AjaxFileUpload(fileIds, id, type) {
    try {
        $.ajaxFileUpload({
            url: "../Files/Handler/FileUpLoadHandler.ashx?id=" + id + "&t=" + type,
            secureuri: false,
            fileElementId: fileIds,
            ContentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
            },
            error: function (s, xml, status, e) {
                alert(e);
            }
        })
    } catch (e) {
        //alert(e);
    }
    return false;
}

var line = 1;
function addli(i) {
    if (i > line) {
        var nli = $("<li><strong>附件" + i + "：</strong><input id=\"file" + i + "\" type=\"file\" name=\"file" + i + "\" onchange=\"addli(" + (i + 1) + ");\" /></li>");
        $("#ulAttach").append(nli);
        line++;
    }
}

function AutoSubmitAudit(masterId, model) {
    try {
        $.post("../WorkFlow/Handler/AutoSubmitAuditHandler.ashx", { model: model, masterId: masterId },
                function (result) { })
    } catch (e) {

    }
}