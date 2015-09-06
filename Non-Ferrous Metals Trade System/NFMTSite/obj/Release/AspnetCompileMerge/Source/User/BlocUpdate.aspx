<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlocUpdate.aspx.cs" Inherits="NFMTSite.User.BlocUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>集团修改</title>
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
            

            $("#txbBlocName").jqxInput({ height: 23, width: 200 });
            $("#txbblocFullName").jqxInput({ height: 23, width: 200 });
            $("#txbblocEName").jqxInput({ height: 23, width: 200 });
            $("#btnCommit").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //$("#ckbIsSelf").jqxCheckBox({ width: 200, height: 25});
            //var value = $("#hidtext").val();
            //$("#ckbIsSelf").jqxCheckBox("val",value);

            var id = $("#hidId").val();

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbBlocName", message: "集团名称必填", action: "keyup,blur", rule: "required" },
                        { input: "#txbblocFullName", message: "集团全称不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbblocEName", message: "集团英文名称不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnCommit").click(function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var blocName = $("#txbBlocName").val();
                var blocFName = $("#txbblocFullName").val();
                var blocEName = $("#txbblocEName").val();

                $.post("Handler/BlocUpdateHandler.ashx", { name: blocName, fname: blocFName, ename: blocEName, id: id },
                    function (result) {
                        alert(result);
                        document.location.href = "BlocList.aspx";
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
            集团修改
            <input type="hidden" id="hidId" runat="server" />
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">集团名称：</span>
                    <span>
                        <input type="text" id="txbBlocName" runat="server" /></span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">集团全称：</span>
                    <span>
                        <input type="text" id="txbblocFullName" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">集团英文名称：</span>
                    <span>
                        <input type="text" id="txbblocEName" runat="server" /></span></li>
                <%--<li>
                    <span style="width: 15%; text-align: right;">是否己方集团：</span>
                    <span><input type="hidden" id="hidtext" runat="server" /></span><div id="ckbIsSelf" runat="server" /></li>--%>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnCommit" value="提交" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="BlocList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
