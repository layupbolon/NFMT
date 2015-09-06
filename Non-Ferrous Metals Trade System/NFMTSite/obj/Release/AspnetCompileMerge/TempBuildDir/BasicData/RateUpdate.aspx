<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RateUpdate.aspx.cs" Inherits="NFMTSite.BasicData.RateUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>汇率修改</title>
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
            $("#txbReatDate").jqxDateTimeInput("val", "<%=this.rate.CreateTime%>");

            //兑换币种
            var url = "Handler/CurrencDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#txbCurrency1").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, height: 25, autoDropDownHeight: true, searchMode: "containsignorecase" });
            $("#txbCurrency1").jqxComboBox("val", "<%=this.rate.FromCurrencyId%>");

            //换至币种
            $("#txbCurrency2").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, height: 25, autoDropDownHeight: true, searchMode: "containsignorecase" });
            $("#txbCurrency2").jqxComboBox("val", "<%=this.rate.ToCurrencyId%>");

            //汇率
            $("#txbRateValue").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 8, spinButtons: true, width: 200 });
            $("#txbRateValue").jqxNumberInput("val", "<%=this.rate.RateValue%>");

            //汇率状态
            CreateSelectStatusDropDownList("rateStatus", "<%=(int)this.rate.RateStatus%>");
            $("#rateStatus").jqxDropDownList("width", 200);

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                       {
                           input: "#txbRateValue", message: "汇率必须大于0", action: "keyup, blur", rule: function (input, commit) {
                               return $("#txbRateValue").jqxNumberInput("val") > 0;
                           }
                       }
                    ]
            });

            //init buttons
            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var rate = {
                    RateId: "<%=this.rate.RateId%>",
                    FromCurrencyId: $("#txbCurrency1").val(),
                    ToCurrencyId: $("#txbCurrency2").val(),
                    RateValue: $("#txbRateValue").val(),
                    RateStatus: $("#rateStatus").val(),
                    CreateTime: $("#txbReatDate").val()
                };

                $.post("Handler/RateUpdateHandler.ashx", { rate: JSON.stringify(rate) },
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
            汇率修改
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>汇率日期：</span></h4>
                    <div id="txbReatDate" runat="server" />
                </li>
                <li>
                    <h4><span>兑换币种：</span></h4>
                    <div id="txbCurrency1" runat="server" />
                </li>
                <li>
                    <h4><span>换至币种：</span></h4>
                    <div id="txbCurrency2" runat="server" />
                </li>
                <li>
                    <h4><span>汇率：</span></h4>
                    <div id="txbRateValue" />
                </li>
                <li>
                    <h4><span>汇率状态：</span></h4>
                    <div id="rateStatus" style="float: left;" />
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
