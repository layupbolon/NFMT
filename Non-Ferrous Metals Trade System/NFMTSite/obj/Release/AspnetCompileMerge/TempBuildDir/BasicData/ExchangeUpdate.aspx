<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeUpdate.aspx.cs" Inherits="NFMTSite.BasicData.ExchangeUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易所修改</title>
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

            $("#txbExchangeName").jqxInput({ width: 200, height: 25 });
            $("#txbExchangeCode").jqxInput({ width: 200, height: 25 });

            var selValue = $("#hidStatusName").val();
            CreateSelectStatusDropDownList("StatusName", selValue);
            $("#StatusName").jqxDropDownList("width", 200);

            //init buttons
            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbExchangeName", message: "交易所名称不可为空", action: "keyup,blur", rule: "required" },
                          { input: "#txbExchangeCode", message: "交易所代码不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var exchangeName = $("#txbExchangeName").val();
                var exchangeCode = $("#txbExchangeCode").val();
                var statusName = $("#StatusName").val();

                $.post("Handler/ExchangeUpdateHandler.ashx", {
                    exchangeName: exchangeName,
                    exchangeCode: exchangeCode,
                    statusName: statusName,
                    id: "<%=Request.QueryString["id"]%>"
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
            交易所信息修改
            <input type="hidden" id="hidAreaName" runat="server" />
            <input type="hidden" id="hidStatusName" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>交易所名称：</span></h4>
                    <input type="text" id="txbExchangeName" runat="server" /></li>
                <li>
                    <h4><span>交易所代码：</span></h4>
                    <input type="text" id="txbExchangeCode" runat="server" /></li>
                <li>
                    <h4><span>交易所状态：</span></h4>
                    <div id="StatusName" />
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
