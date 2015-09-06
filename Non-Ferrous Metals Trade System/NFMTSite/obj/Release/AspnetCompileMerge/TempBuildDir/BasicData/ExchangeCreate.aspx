<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeCreate.aspx.cs" Inherits="NFMTSite.BasicData.ExchangeCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易所新增</title>
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

            $("#txbExchangeName").jqxInput({ height: 25, width: 200 });
            $("#txbExchangeCode").jqxInput({ height: 25, width: 200 });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            //initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbExchangeName", message: "交易所名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbExchangeCode", message: "交易所代码不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var exchangeName = $("#txbExchangeName").val();
                var exchangeCode = $("#txbExchangeCode").val();

                $.post("Handler/ExchangeCreateHandler.ashx", {
                    exchangeName: exchangeName,
                    exchangeCode: exchangeCode
                },
                    function (result) {
                        alert(result);
                        document.location.href = "ExchangeList.aspx";
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
            交易所新增
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>交易所名称：</span></h4>
                    <span>
                        <input type="text" id="txbExchangeName" /></span>
                </li>
                <li>
                    <h4><span>交易所代码：</span></h4>
                    <span>
                        <input type="text" id="txbExchangeCode" /></span>
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="ExchangeList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
