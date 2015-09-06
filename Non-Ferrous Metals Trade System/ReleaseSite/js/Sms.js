window.onload = function () {
    GetSms();
    window.setInterval(GetSms, 5000);//5秒心跳
};

//**************************************************************************************************************//

var perHeight = 30;//每个元素的高度

function CLASS_MSN(sms) {
    this.sms = sms;
    this.typeName = "";
}

CLASS_MSN.prototype.show = function () {
    var height = 0;

    var a1 = document.createElement("div");
    a1.id = "window";

    var str = "        <div id=\"windowHeader\">";
    height += perHeight;
    str += "            <span>提  醒";
    str += "            </span>";
    str += "        </div>";
    str += "        <div style=\"overflow: hidden;\" id=\"windowContent\">";
    for (var o in this.sms) {
        if (this.sms[o].SmsBody) {
            if (this.typeName == this.sms[o].TypeName) {
                height += perHeight;
                str += "                <li style='line-height:20px' id=\"sms" + this.sms[o].SmsId + "\"><a  onclick='javascript:taskClick(" + this.sms[o].SmsId + ");' target=\"TargeForm\"  href=\"" + this.sms[o].ViewUrl + "" + this.sms[o].SourceId + "\">" + this.sms[o].SmsHead + "</a></li>";
            }
            else {
                if (this.typeName.length > 0) { str += "            </ul>"; }

                height += perHeight;
                str += "            <a target=\"TargeForm\"  href=\"" + this.sms[o].ListUrl + "\">" + this.sms[o].TypeName + "</a>";
                this.typeName = this.sms[o].TypeName;
                str += "            <ul style='list-style-type: none;'>";
                height += perHeight;
                str += "                <li style='line-height:20px' id=\"sms" + this.sms[o].SmsId + "\"><a id=\"sms" + this.sms[o].SmsId + "\" onclick='javascript:taskClick(" + this.sms[o].SmsId + ");' target=\"TargeForm\"  href=\"" + this.sms[o].ViewUrl + "" + this.sms[o].SourceId + "\">" + this.sms[o].SmsHead + "</a></li>";
            }
        }
    }
    str += "        </div>";

    a1.innerHTML = str;
    document.body.appendChild(a1);
    $("#window").jqxWindow({ width: "200px", height: height + "px", showCollapseButton: true, resizable: false, closeButtonAction: "close" });
    $("#window").on("close", function (event) {
        windowsClosed()
    });
    scall();
}

//填充内容
function setContent(data) {
    var height = 0;

    var typeName = "";
    var my = document.getElementById("window");
    if (my) {
        var str = "";
        for (var o in data) {
            if (data[o].SmsBody) {
                if (typeName == data[o].TypeName) {
                    height += perHeight;
                    str += "                <li style='line-height:20px' id=\"sms" + data[o].SmsId + "\"><a  onclick='javascript:taskClick(" + data[o].SmsId + ");' target=\"TargeForm\"  href=\"" + data[o].ViewUrl + "" + data[o].SourceId + "\">" + data[o].SmsHead + "</a></li>";
                }
                else {
                    if (typeName.length > 0) { str += "            </ul>"; }

                    height += perHeight;
                    str += "            <a target=\"TargeForm\"  href=\"" + data[o].ListUrl + "\">" + data[o].TypeName + "</a>";
                    typeName = data[o].TypeName;
                    str += "            <ul style='list-style-type: none;'>";
                    height += perHeight;
                    str += "                <li style='line-height:20px' id=\"sms" + data[o].SmsId + "\"><a id=\"sms" + data[o].SmsId + "\" onclick='javascript:taskClick(" + data[o].SmsId + ");' target=\"TargeForm\"  href=\"" + data[o].ViewUrl + "" + data[o].SourceId + "\">" + data[o].SmsHead + "</a></li>";
                }
            }
        }
        $("#window").jqxWindow("setContent", str);
        $("#window").jqxWindow({ height: height + perHeight + "px" });
        $("#window").on("close", function (event) {
            windowsClosed()
        });
    }
}

//消息框动画
function scall() {
    var my = document.getElementById("window");
    if (my) {
        var divBottom = $(window).scrollTop() + $(window).height() - $("#window").outerHeight(true) - 5;
        var divleft = $(window).width() - $("#window").outerWidth(true) - 5;
        $("#window").animate({ position: "absolute", left: divleft, top: divBottom }, { duration: 400, queue: false });
    }
}

//消息点击
function taskClick(smsId) {
    //alert(smsId);
    $.ajax({
        type: "Post",
        url: "Message/Handler/MessageReadHandler.ashx",
        data: { id: smsId },
        error: function (err) {
            //alert(err);
        }
    });
}

//关闭窗口事件，将窗口中的消息状态改为已读
function windowsClosed() {
    var smsIds = "";
    $("div#window li").each(function () {
        var smsId = $(this).attr("id");
        if (smsId.length > 1) {
            smsId = smsId.substring(3, smsId.length);
        }
        smsIds = smsIds + smsId + ",";
    });

    if (smsIds.length > 0) {
        smsIds = smsIds.substring(0, smsIds.length - 1);
    }

    $.ajax({
        type: "Post",
        url: "Message/Handler/MessageReadHandler.ashx",
        data: { id: smsIds },
        error: function (err) {
            //alert(err);
        }
    });
}

//获取消息
function GetSms() {
    try {
        $.ajax({
            type: "get",
            url: "Message/Handler/MessageListHandler.ashx",
            cache: false,
            timeout: 1000 * 130,
            dataType: "json",
            success: function (data) {
                try {
                    //判断是否有数据
                    if (data.SmsId = undefined || data.SmsId == 0) {
                        var obj = document.getElementById("window");
                        //无数据并且消息窗口存在，则去除窗口
                        if (obj != null) { obj.parentNode.removeChild(obj); }
                        return;
                    }

                    var obj = document.getElementById("window");
                    //存在窗口就填充内容，否则新建窗口
                    if (obj != null) {
                        setContent(data);
                        scall();
                    } else {
                        var MSG1 = new CLASS_MSN(data);
                        MSG1.show();
                    }

                    //若显示内容为空，则清除
                    var obj1 = document.getElementById("window");
                    if (obj1 != null) {
                        var array = obj1.getElementsByTagName("li");
                        if (array.length < 1) {
                            obj1.parentNode.removeChild(obj1);
                        }
                    }
                } catch (e) {
                    //alert(e.message);
                }
            },
            error: function (err) {
                //alert(err);
            }
        });
    } catch (e) {

    }

    window.onscroll = scall; //滚动条滚动时触发事件
    window.onresize = scall;//窗体页面大小改变时触发事件
    window.onload = scall;//窗体加载或刷新页面时触发事件
}
