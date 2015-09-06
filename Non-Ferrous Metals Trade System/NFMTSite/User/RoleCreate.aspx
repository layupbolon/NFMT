<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleCreate.aspx.cs" Inherits="NFMTSite.User.RoleCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            

            $("#txbRoleName").jqxInput({ height: 25, width: 200 });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbRoleName", message: "角色名称不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var roleName = $("#txbRoleName").val();

                $.post("Handler/RoleCreateHandler.ashx", { roleName: roleName },
                    function (result) {
                        alert(result);
                        $("#txbRoleName").val("");
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            角色添加
        </div>
        <div id="layOutDiv">
            <ul>

                <li>
                    <span style="width: 15%; text-align: right;">角色名称：</span>
                    <span>
                        <input type="text" id="txbRoleName" runat="server" /></span></li>

                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="RoleList.aspx" id="btnCancel">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
