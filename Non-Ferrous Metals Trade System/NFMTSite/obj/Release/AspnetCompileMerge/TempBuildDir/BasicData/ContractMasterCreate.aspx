<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractMasterCreate.aspx.cs" Inherits="NFMTSite.BasicData.ContractMasterCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约模板添加</title>
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

            $("#txbMasterName").jqxInput({ height: 25, width: 120 });
            $("#txbMasterEname").jqxInput({ height: 25, width: 120 });
            $("#btnAdd").jqxButton();

            // initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbMasterName", message: "模板名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbMasterEname", message: "模板英文名称不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var masterName = $("#txbMasterName").val();
                var masterEname = $("#txbMasterEname").val();

                $.post("Handler/ContractMasterCreateHandler.ashx", { mn: masterName, men: masterEname },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "ContractMasterList.aspx";
                        }
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
            合约模板添加
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>模板名称：</span></h4>
                    <span>
                        <input type="text" id="txbMasterName" /></span>
                </li>
                <li>
                    <h4><span>模板英文名称：</span></h4>
                    <span>
                        <input type="text" id="txbMasterEname" /></span>
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
