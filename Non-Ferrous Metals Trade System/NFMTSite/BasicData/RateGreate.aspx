<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RateGreate.aspx.cs" Inherits="NFMTSite.BasicData.RateGreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>汇率新增</title>
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

            //汇率日期
            $("#txbReatDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });

            //兑换币种
            var url = "Handler/CurrencDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#txbCurrency1").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, height: 25, autoDropDownHeight: true, searchMode: "containsignorecase" });

            //换至币种
            $("#txbCurrency2").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, height: 25, autoDropDownHeight: true, searchMode: "containsignorecase" });

            //汇率
            $("#txbRateValue").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 8, spinButtons: true, width: 200 });

            //initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    {
                        input: "#txbRateValue", message: "汇率必须大于0", action: "keyup, blur", rule: function (input, commit) {
                            return $("#txbRateValue").jqxNumberInput("val") > 0;
                        }
                    }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var reatDate = $("#txbReatDate").val();
                var currency1 = $("#txbCurrency1").val();
                var rateValue = $("#txbRateValue").val();
                var currency2 = $("#txbCurrency2").val();

                $.post("Handler/RateGreateHandler.ashx", {
                    reatDate: reatDate,
                    currency1: currency1,
                    rateValue: rateValue,
                    currency2: currency2
                },
                    function (result) {
                        alert(result);
                        document.location.href = "RateList.aspx";
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
            汇率新增
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>汇率日期：</span></h4>
                    <div id="txbReatDate" />
                </li>
                <li>
                    <h4><span>兑换币种：</span></h4>
                    <div id="txbCurrency1" />
                </li>
                <li>
                    <h4><span>换至币种：</span></h4>
                    <div id="txbCurrency2" />
                </li>
                <li>
                    <h4><span>汇率：</span></h4>
                    <div id="txbRateValue" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="RateList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
