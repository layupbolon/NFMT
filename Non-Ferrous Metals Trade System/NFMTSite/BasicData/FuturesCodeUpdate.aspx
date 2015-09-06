<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuturesCodeUpdate.aspx.cs" Inherits="NFMTSite.BasicData.FuturesCodeUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>期货合约修改</title>
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
            

            $("#txbTradeCode").jqxInput({ width: 200, height: 25 });
            $("#txbCodeSize").jqxInput({ width: 200, height: 25 });

            $("#firstTradeDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });
            var firstTradeDateValue = $("#hidFirstTradeDate").val();
            $("#firstTradeDate").jqxDateTimeInput("val", firstTradeDateValue);

            $("#lastTradeDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });
            var lastTradeDateValue = $("#hidLastTradeDate").val();
            $("#lastTradeDate").jqxDateTimeInput("val", lastTradeDateValue);

            var selFcStatus = $("#hidFuturesCodeStatus").val();
            CreateSelectStatusDropDownList("FuturesCodeStatus", selFcStatus);
            $("#FuturesCodeStatus").jqxDropDownList("width", 200);

            //绑定 交易所(修改)
            var exchangeUrl = "Handler/ExChangeDDLHandler.ashx";
            var exchangeSource = { datatype: "json", url: exchangeUrl, async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#Exchage").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 200, autoDropDownHeight: true });
            if ($("#hidExchage").val() > 0) { $("#Exchage").jqxComboBox("val", $("#hidExchage").val()); }

            //绑定 合约单位(修改)
            var muUrl = "Handler/MUDDLHandler.ashx";
            var muSource = { datatype: "json", url: muUrl, async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#MU").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 200, autoDropDownHeight: true });
            if ($("#hidMU").val() > 0) { $("#MU").jqxComboBox("val", $("#hidMU").val()); }

            //绑定 币种(修改)
            var currencUrl = "Handler/CurrencDDLHandler.ashx";
            var currencSource = { datatype: "json", url: currencUrl, async: false };
            var currencDataAdapter = new $.jqx.dataAdapter(currencSource);
            $("#Currency").jqxComboBox({ source: currencDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, autoDropDownHeight: true });
            if ($("#hidCurrency").val() > 0) { $("#Currency").jqxComboBox("val", $("#hidCurrency").val()); }

            //绑定 币种
            var assetUrl = "Handler/AssetDDLHandler.ashx";
            var assetSource = { datatype: "json", url: assetUrl, async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#selAsset").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 200, autoDropDownHeight: true });
            $("#selAsset").val(<%=this.curFuturesCode.AssetId%>);

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                          { input: "#txbCodeSize", message: "合约规模不可为空", action: "keyup,blur", rule: "required" },
                          { input: "#txbTradeCode", message: "交易代码不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            //init buttons
            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var exchage = $("#Exchage").val();
                var codeSize = $("#txbCodeSize").val();
                var firstTradeDate = $("#firstTradeDate").val();
                var lastTradeDate = $("#lastTradeDate").val();
                var mU = $("#MU").val();
                var currency = $("#Currency").val();
                var tradeCode = $("#txbTradeCode").val();
                var futuresCodeStatus = $("#FuturesCodeStatus").val();
                var asset = $("#selAsset").val();

                $.post("Handler/FuturesCodeUpdateHandler.ashx", {
                    exchage: exchage,
                    codeSize: codeSize,
                    firstTradeDate: firstTradeDate,
                    lastTradeDate: lastTradeDate,
                    mU: mU,
                    currency: currency,
                    tradeCode: tradeCode,
                    futuresCodeStatus: futuresCodeStatus,
                    assetId: asset,
                    id: "<%=Request.QueryString["id"]%>"
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
            期货合约修改
                        <input type="hidden" id="hidExchage" runat="server" />
            <input type="hidden" id="hidFirstTradeDate" runat="server" />
            <input type="hidden" id="hidLastTradeDate" runat="server" />
            <input type="hidden" id="hidMU" runat="server" />
            <input type="hidden" id="hidCurrency" runat="server" />
            <input type="hidden" id="hidFuturesCodeStatus" runat="server" />
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
                    <h4><span>币种：</span></h4>
                    <div id="Currency" style="float: left;" />
                </li>
                <li>
                    <h4><span>品种：</span></h4>
                    <div id="selAsset" style="float: left;" />
                </li>
                <li>
                    <h4><span>合约状态：</span></h4>
                    <div id="FuturesCodeStatus" style="float: left;" />
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" />
                    </span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
