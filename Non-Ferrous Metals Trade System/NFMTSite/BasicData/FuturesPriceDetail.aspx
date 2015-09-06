<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuturesPriceDetail.aspx.cs" Inherits="NFMTSite.BasicData.FuturesPriceDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>期货合约明细</title>
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
            

            $("#btnFreeze").jqxButton({ height: 25, width: 50 });
            $("#btnUnFreeze").jqxButton();

            $("#btnFreeze").on("click", function () {
                var id = $("#hidId").val();
                var operateId = operateEnum.冻结;
                $.post("Handler/FuturesCodeStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var id = $("#hidId").val();
                var operateId = operateEnum.解除冻结;

                $.post("Handler/FuturesCodeStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
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
            期货合约明细
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>交易所：</span></h4>
                    <span id="Exchage" runat="server" />
                    <input type="hidden" runat="server" id="hidId" />
                </li>
                <li>
                    <h4><span>合约规模：</span></h4>
                    <span id="txbCodeSize" runat="server" /></li>
                <li>
                    <h4><span>交易起始日期：</span></h4>
                    <span id="firstTradeDate" runat="server"></span></li>
                <li>
                    <h4><span>交易终止日期：</span></h4>
                    <span id="lastTradeDate" runat="server"></span></li>
                <li>
                    <h4><span>合约单位：</span></h4>
                    <span id="MU" runat="server"></span></li>
                <li>
                    <h4><span>币种：</span></h4>
                    <span id="Currency" runat="server"></span></li>
                <li>
                    <h4><span>交易代码：</span></h4>
                    <span id="txbTradeCode" runat="server"></span></li>
                <li>
                    <h4><span>合约状态：</span></h4>
                    <span id="FuturesCodeStatus" runat="server"></span></li>

                <li>
                    <div id="buttons">
                        <h4><span>&nbsp;</span></h4>
                        <span>
                            <input type="button" id="btnFreeze" value="冻结" runat="server" style="margin-left: 35px" />
                            <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="margin-left: 35px" />
                        </span>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
