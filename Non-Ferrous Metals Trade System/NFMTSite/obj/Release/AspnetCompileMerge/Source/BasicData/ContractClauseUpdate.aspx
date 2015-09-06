<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractClauseUpdate.aspx.cs" Inherits="NFMTSite.BasicData.ContractClauseUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约条款修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            

            $("#jqxExpander").jqxExpander({ width: "98%" });

            var selValue = $("#hidClauseStatus").val();
            CreateSelectStatusDropDownList("selStatus", selValue);

            $("#txbText").jqxInput({ height: 200, width: "80%" });
            $("#txbEText").jqxInput({ height: 200, width: "80%" });
            $("#btnUpdate").jqxButton({ width: 65 });

            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbText", message: "条款中文内容不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbEText", message: "条款英文内容不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnUpdate").click(function () {
                var tValue = $("#txbText").val();
                var eValue = $("#txbEText").val();
                var clauseId = $("#hidClauseId").val();
                var status = $("#selStatus").jqxDropDownList("val");

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                $.post("Handler/ContractClauseUpdateHandler.ashx", { tv: tValue, ev: eValue, cid: clauseId, cs: status },
                    function (result) {
                        alert(result);
                        document.location.href = "ContractClauseList.aspx";
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
            合约条款修改
        </div>
        <div class="DataExpander" style="padding-bottom: 0px;">
            <ul>
                <li style="line-height: 200px; height: 200px;">
                    <h4><span>条款中文内容：</span></h4>
                    <input type="hidden" runat="server" id="hidClauseId" />
                    <textarea id="txbText" runat="server"></textarea>

                </li>
                <li style="line-height: 200px; height: 200px; float: none">
                    <h4><span>条款英文内容：</span></h4>
                    <textarea id="txbEText" runat="server"></textarea>
                </li>
                <li>
                    <h4><span>模板状态：</span></h4>
                    <div id="selStatus" />
                    <input type="hidden" runat="server" id="hidClauseStatus" /></li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
