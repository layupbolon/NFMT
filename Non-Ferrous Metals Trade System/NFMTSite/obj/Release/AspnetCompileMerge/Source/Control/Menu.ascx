<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="NFMTSite.Control.Menu" %>

<div id="content">
    <div id="jqxMenu" style="width: 100%; height: 30px">
        <span style="color:red;float:left;font-size:large;"><% =NFMT.Common.DefaultValue.Sign%></span>
        <ul style="float: left; margin-left: 250px">
            <li><a href="<% =NFMT.Common.DefaultValue.NfmtSiteName%>Index.aspx">首页</a></li>
            <li runat="server" id="liUser"></li>
            <li><a id="aChangePwd" onclick="show()"><span style="color: blue">修改密码</span></a></li>
            <li><a runat="server" id="aLoginOut" href="#"><span style="color: blue">退出</span></a></li>
        </ul>
    </div>
</div>
<div id="popupWindow">
    <div>修改密码</div>
    <div id="layOutDiv">
        <ul>
            <li>
                <span>原密码：</span>
                <input type="password" id="txbOldPwd" />
            </li>
            <li>
                <span>新密码：</span>
                <input type="password" id="txbNewPwd" />
            </li>
            <li>
                <span>确认新密码：</span>
                <input type="password" id="txbNewPwdConfirm" />
            </li>
            <li>
                <input style="margin-right: 5px;" type="button" id="Save" value="修改" />
                <input id="Cancel" type="button" value="取消" />
            </li>
        </ul>
    </div>
</div>
<script type="text/javascript">

    $("#popupWindow").jqxWindow({ width: 350, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01 });
    $("#Save").jqxButton({ height: 25, width: 100 });
    $("#Cancel").jqxButton({ height: 25, width: 100 });

    $("#popupWindow").on("close", function (event) {
        $("#popupWindow").jqxValidator("hide");
    });

    $("#txbOldPwd").jqxInput({ height: 25, width: 120 });
    $("#txbNewPwd").jqxInput({ height: 25, width: 120 });
    $("#txbNewPwdConfirm").jqxInput({ height: 25, width: 120 });

    //验证器
    $("#popupWindow").jqxValidator({
        focus: false,
        rules:
            [
                { input: "#txbOldPwd", message: "原密码不可为空", action: "keyup,blur", rule: "required" },
                { input: "#txbNewPwd", message: "新密码不可为空", action: "keyup,blur", rule: "required" },
                { input: "#txbNewPwdConfirm", message: "确认密码不可为空", action: "keyup,blur", rule: "required" },
                {
                    input: "#txbNewPwdConfirm", message: "密码不一致", action: "keyup, focus", rule: function (input, commit) {
                        if (input.val() === $("#txbNewPwd").val()) {
                            return true;
                        }
                        return false;
                    }
                }
            ]
    });

    function show() {
        $("#popupWindow").jqxWindow("show");

        $("#txbOldPwd").jqxInput("val", "");
        $("#txbNewPwd").jqxInput("val", "");
        $("#txbNewPwdConfirm").jqxInput("val", "");

        $("#txbOldPwd").jqxInput("focus");
    }

    $("#Save").click(function () {
        var isCanSubmit = $("#popupWindow").jqxValidator("validate");
        if (!isCanSubmit) { return; }

        $.post("User/Handler/ChangePasswordHandler.ashx", {
            o: $("#txbOldPwd").val(), n: $("#txbNewPwd").val()
        }, function (result) {
            var obj = JSON.parse(result);
            alert(obj.Message);
            if (obj.ResultStatus.toString() == "0") {
                $("#popupWindow").jqxWindow("hide");
            }
        });
    });

</script>
