<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuturesCodeCreate.aspx.cs" Inherits="NFMTSite.BasicData.FuturesCodeCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>期货合约新增</title>
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

            $("#lastTradeDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });
            $("#firstTradeDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });
            $("#txbTradeCode").jqxInput({ width: 200, height: 25 });
            $("#txbCodeSize").jqxInput({ width: 200, height: 25 });


            var exchangeUrl = "Handler/ExChangeDDLHandler.ashx?isSelf=1";
            var exchangeSource = { datatype: "json", url: exchangeUrl, async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#Exchage").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 200, autoDropDownHeight: true });

            //绑定 合约单位
            var muUrl = "Handler/MUDDLHandler.ashx?isSelf=1";
            var muSource = { datatype: "json", url: muUrl, async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#MU").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 200, autoDropDownHeight: true });

            //绑定 币种
            var assetUrl = "Handler/AssetDDLHandler.ashx";
            var assetSource = { datatype: "json", url: assetUrl, async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#selAsset").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 200, autoDropDownHeight: true });

            //绑定 币种
            var currencUrl = "Handler/CurrencDDLHandler.ashx?isSelf=1";
            var currencSource = { datatype: "json", url: currencUrl, async: false };
            var currencDataAdapter = new $.jqx.dataAdapter(currencSource);
            $("#Currency").jqxComboBox({ source: currencDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, autoDropDownHeight: true });

            //initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbCodeSize", message: "交易规模不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#firstTradeDate", message: "交易开始时间不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#lastTradeDate", message: "交易结束时间不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbTradeCode", message: "交易代码不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认添加?")) { return; }

                var exchage = $("#Exchage").val();
                var codeSize = $("#txbCodeSize").val();
                var firstTradeDate = $("#firstTradeDate").val();
                var lastTradeDate = $("#lastTradeDate").val();
                var mU = $("#MU").val();
                var currency = $("#Currency").val();
                var tradeCode = $("#txbTradeCode").val();
                var asset = $("#selAsset").val();

                $.post("Handler/FuturesCodeCreateHandler.ashx", {
                    exchage: exchage,
                    codeSize: codeSize,
                    firstTradeDate: firstTradeDate,
                    lastTradeDate: lastTradeDate,
                    mU: mU,
                    currency: currency,
                    tradeCode: tradeCode,
                    assetId: asset
                },
                    function (result) {
                        alert(result);
                        document.location.href = "FuturesCodeList.aspx";
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
            期货合约新增
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>交易代码：</span></h4>
                    <input type="text" id="txbTradeCode" runat="server" /></li>
                <li>
                    <h4><span>交易所：</span></h4>
                    <div id="Exchage" style="float: left;" />
                </li>
                <li>
                    <h4><span>交易起始日期：</span></h4>
                    <div id="firstTradeDate" runat="server" style="float: left;" />
                </li>
                <li>
                    <h4><span>交易终止日期：</span></h4>
                    <div id="lastTradeDate" runat="server" style="float: left;" />
                </li>
                <li>
                    <h4><span>合约规模：</span></h4>
                    <input type="text" id="txbCodeSize" runat="server" /></li>
                <li>
                    <h4><span>合约单位：</span></h4>
                    <div id="MU" style="float: left;" />
                </li>
                <li>
                    <h4><span>品种：</span></h4>
                    <div id="selAsset" style="float: left;" />
                </li>
                <li>
                    <h4><span>币种：</span></h4>
                    <div id="Currency" style="float: left;" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="FuturesCodeList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
