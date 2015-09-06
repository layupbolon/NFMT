<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyUpdate.aspx.cs" Inherits="NFMTSite.BasicData.CurrencyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>币种修改</title>
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

            $("#txbCurrencyName").jqxInput({ height: 25, width: 200 });
            $("#txbCurrencyFullName").jqxInput({ height: 25, width: 200 });
            $("#txbCurrencyShort").jqxInput({ height: 25, width: 200 });

            var selValue = $("#txbCurrencyStatus").val();
            CreateSelectStatusDropDownList("selStatus", selValue);
            $("#selStatus").jqxDropDownList("width", 200);

            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbCurrencyName", message: "币种名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbCurrencyFullName", message: "币种全称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbCurrencyShort", message: "币种缩写不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var currencyName = $("#txbCurrencyName").val();
                var currencyFullName = $("#txbCurrencyFullName").val();
                var currencyShort = $("#txbCurrencyShort").val();
                var currencyStatus = $("#selStatus").val();
                var id = $("#hid").val();

                $.post("Handler/CurrencyUpdateHandler.ashx", {
                    currencyName: currencyName,
                    currencyFullName: currencyFullName,
                    currencyShort: currencyShort,
                    currencyStatus: currencyStatus,
                    id: id
                },
                      function (result) {
                          alert(result);
                          document.location.href = "CurrencyList.aspx";
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
            币种修改
                        <input type="hidden" runat="server" id="hid" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>币种名称：</span></h4>
                    <span>
                        <input type="text" id="txbCurrencyName" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>币种全称：</span></h4>
                    <span>
                        <input type="text" id="txbCurrencyFullName" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>币种缩写：</span></h4>
                    <span>
                        <input type="text" id="txbCurrencyShort" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>币种状态：</span></h4>
                    <div id="selStatus" />
                    <input type="hidden" id="txbCurrencyStatus" runat="server" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.5%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" runat="server" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
