<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankAccountDetail.aspx.cs" Inherits="NFMTSite.BasicData.BankAccountDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账户管理明细</title>
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
            

            $("#btnFreeze").jqxButton({ height: 25, width: 96 });
            $("#btnUnFreeze").jqxButton({ height: 25, width: 96 });

            $("#btnFreeze").on("click", function () {
                var id = $("#hidId").val();
                var operateId = operateEnum.冻结;
                $.post("Handler/RateStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var id = $("#hidId").val();
                var operateId = operateEnum.解除冻结;
                $.post("Handler/RateStatusHandler.ashx", { id: id, oi: operateId },
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
            账户管理明细
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <input type="hidden" runat="server" id="hidId" />
                    <h4><span>公司：</span></h4>
                    <span id="txbCorpName" runat="server" /></li>

                <li>
                    <h4><span>银行：</span></h4>
                    <span id="txbBankName" runat="server" /></li>
                <li>
                    <h4><span>账户：</span></h4>
                    <span id="txbAccountNo" runat="server" /></li>
                <li>
                    <h4><span>币种：</span></h4>
                    <span id="txbCurrencyName" runat="server" /></li>
                <li>
                    <h4><span>账户描述：</span></h4>
                    <span id="txbBankAccDesc" runat="server" /></li>
                <li>
                    <h4><span>账户状态：</span></h4>
                    <span id="txbStatusName" runat="server" /></li>

                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnFreeze" value="冻结" runat="server" />
                        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="margin-left: 10px" />
                    </span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
