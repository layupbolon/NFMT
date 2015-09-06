<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" EnableViewState="false" Inherits="NFMT.PassPort.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>登录_<%=NFMT.Common.DefaultValue.SystemName%></title>
    <meta name="keywords" content="<%=NFMT.Common.DefaultValue.SystemName%>" />
    <meta charset="utf-8">
    <link href="css/home.css?v=2" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/styles/jqx.metro.css" media="screen" />
    <script type="text/javascript" src="jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="jqwidgets/scripts/jqx-all.js"></script>

</head>
<body>
    <div style="text-align:center;">
		<div class="header">
			<h1 class="headerLogo">
				<a title="<%=NFMT.Common.DefaultValue.SystemName%>" target="_blank" href="#"><img src="images/maike.png" width="200" /> </a>
			</h1>
			<div class="headerNav">发现价值&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;创造价值&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;分享价值</div>
		</div>
	</div>

    <div class="wrap">
        <div class="banner-show" id="js_ban_content">
            <div class="cell bns-01">
                <div class="con"></div>
            </div>
            <div class="cell bns-02" style="display: none;">
                <div class="con"><%--<a href="#" target="_blank" class="banner-link"><i>圈子</i></a> --%></div>
            </div>
            <div class="cell bns-03" style="display: none;">
                <div class="con"><%--<a href="#" target="_blank" class="banner-link"><i>企业云</i></a> --%></div>
            </div>
        </div>
        <%--<div class="banner-control" id="js_ban_button_box"><a href="javascript:;" class="left">左</a> <a href="javascript:;" class="right">右</a> </div>--%>
        <script type="text/javascript">
            //; (function () {

            //    var defaultInd = 0;
            //    var list = $('#js_ban_content').children();
            //    var count = 0;
            //    var change = function (newInd, callback) {
            //        if (count) return;
            //        count = 2;
            //        $(list[defaultInd]).fadeOut(400, function () {
            //            count--;
            //            if (count <= 0) {
            //                if (start.timer) window.clearTimeout(start.timer);
            //                callback && callback();
            //            }
            //        });
            //        $(list[newInd]).fadeIn(400, function () {
            //            defaultInd = newInd;
            //            count--;
            //            if (count <= 0) {
            //                if (start.timer) window.clearTimeout(start.timer);
            //                callback && callback();
            //            }
            //        });
            //    }

            //    var next = function (callback) {
            //        var newInd = defaultInd + 1;
            //        if (newInd >= list.length) {
            //            newInd = 0;
            //        }
            //        change(newInd, callback);
            //    }

            //    var start = function () {
            //        if (start.timer) window.clearTimeout(start.timer);
            //        start.timer = window.setTimeout(function () {
            //            next(function () {
            //                start();
            //            });
            //        }, 8000);
            //    }                

            //    start();

            //    $('#js_ban_button_box').on('click', 'a', function () {
            //        var btn = $(this);
            //        if (btn.hasClass('right')) {
            //            //next
            //            next(function () {
            //                start();
            //            });
            //        }
            //        else {
            //            //prev
            //            var newInd = defaultInd - 1;
            //            if (newInd < 0) {
            //                newInd = list.length - 1;
            //            }
            //            change(newInd, function () {
            //                start();
            //            });
            //        }
            //        return false;
            //    });

            //})();

            function EnterClick(e) {
                if (e.which == 13 || e.keyCode == 13) {
                    $("#btnLogin").click();
                }
            }
        </script>
        <div class="container">
            <div class="register-box">
                <div class="reg-slogan"><%=NFMT.Common.DefaultValue.SystemName%></div>
                <div class="reg-form" id="js-form-mobile">
                    <br>
                    <div class="cell">
                        <input type="text" name="mobile" id="txbUserName" class="text" runat="server" maxlength="20" placeholder="用户名"/>
                    </div>
                    <div class="cell">
                        <input type="password" name="passwd" id="txbPassWord" class="text" runat="server" placeholder="密码" onkeydown="javascript:EnterClick(event);"/>
                    </div>
                    <div class="bottom">
                        <a id="btnLogin" runat="server" onclick="login()" class="button btn-blue">登录</a>
                    </div>
                </div>
                <script type="text/javascript">                    

                    $("#txbUserName").focus();

                    //验证器
                    $("#js-form-mobile").jqxValidator({
                        rules:
                            [
                                { input: "#txbUserName", message: "用户名不可为空", action: "keyup", rule: "required" },
                                { input: "#txbPassWord", message: "密码不可为空", action: "keyup", rule: "required" }
                            ]
                    });

                    function setCookie(c_name, value, expiredays) {
                        var exdate = new Date();
                        exdate.setDate(exdate.getDate() + expiredays);
                        document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
                    }

                    function GetUrlParam(name) {
                        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                        var r = window.location.search.substr(1).match(reg);
                        if (r != null) return unescape(r[2]); return null;
                    }

                    function login() {
                        var isCanSubmit = $("#js-form-mobile").jqxValidator("validate");
                        if (!isCanSubmit) { return; }

                        try {
                            $.post("Handler/LoginHandler.ashx", {
                                u: $("#txbUserName").val(), p: $("#txbPassWord").val()
                            }, function (result) {
                                var obj = JSON.parse(result);
                                if (obj.ResultStatus.toString() != "0") {
                                    alert(obj.Message);
                                } else {
                                    //setCookie("<%=NFMT.Common.DefaultValue.CookieName%>", obj.ReturnValue, 1);

                                    var url = GetUrlParam("redirectUrl");
                                    if (url)
                                        window.location.href = url;
                                    else
                                        window.location.href = "<%=NFMT.Common.DefaultValue.NfmtSiteName%>";
                            }
                        });
                        } catch (e) {
                            alert(e.message);
                        }
                        
                    }
                </script>
            </div>
        </div>
    </div>

    <div class="banner-shadow"></div>
	<div class="footer">
		<p>迈科集团&nbsp;&nbsp;&nbsp;信息技术部</p>
	</div>

</body>
</html>