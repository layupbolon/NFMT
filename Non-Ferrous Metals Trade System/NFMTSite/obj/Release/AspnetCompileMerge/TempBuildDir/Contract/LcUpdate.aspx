<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LcUpdate.aspx.cs" Inherits="NFMTSite.Contract.LcUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>信用证修改</title>
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
            

            //开证行
            var ddlIssueBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlIssueBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlIssueBankurl, async: false };
            var ddlIssueBankdataAdapter = new $.jqx.dataAdapter(ddlIssueBanksource);
            $("#ddlIssueBank").jqxComboBox({ selectedIndex: 0, source: ddlIssueBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });
            if ($("#hidIssueBank").val() > 0) { $("#ddlIssueBank").jqxComboBox("val", $("#hidIssueBank").val()); }

            //通知行
            var url = "../BasicData/Handler/BanDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlAdviseBank").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });
            if ($("#hidAdviseBank").val() > 0) { $("#ddlAdviseBank").jqxComboBox("val", $("#hidAdviseBank").val()); }

            //开证日期
            $("#dtIssueDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180 });
            $("#dtIssueDate").jqxDateTimeInput("val", $("#hidIssueDate").val());

            //远期天数
            $("#nbFutureDay").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 0 });
            $("#nbFutureDay").jqxNumberInput("val", $("#hidFutureDay").val());

            //信用证金额
            $("#nbLcBala").jqxNumberInput({ width: 180, height: 25, spinButtons: true });
            $("#nbLcBala").jqxNumberInput("val", $("#hidLcBala").val());

            //币种
            var ddlMUIdurl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlCurrency").jqxComboBox({ selectedIndex: 0, source: ddlMUIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25, autoDropDownHeight: true });
            if ($("#hidCurrency").val() > 0) { $("#ddlCurrency").jqxComboBox("val", $("#hidCurrency").val()); }

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        //{
                        //    input: "#nbFutureDay", message: "远期天数必须大于0", action: "keyup,blur", rule: function (input, commit) {
                        //        return $('#nbFutureDay').jqxNumberInput("val") > 0;
                        //    }
                        //},
                        {
                            input: "#nbLcBala", message: "信用证金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbLcBala').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#ddlIssueBank", message: "开证行不可为空", action: "change", rule: function (input, commit) {
                                return $('#ddlIssueBank').jqxComboBox("val") != "" && $('#ddlIssueBank').jqxComboBox("val") > -1;
                            }
                        },
                        {
                            input: "#ddlAdviseBank", message: "通知行不可为空", action: "change", rule: function (input, commit) {
                                return $('#ddlAdviseBank').jqxComboBox("val") != "" && $('#ddlAdviseBank').jqxComboBox("val") > -1;
                            }
                        },
                        {
                            input: "#ddlCurrency", message: "币种不可为空", action: "change", rule: function (input, commit) {
                                return $('#ddlCurrency').jqxComboBox("val") != "" && $('#ddlCurrency').jqxComboBox("val") > -1;
                            }
                        }
                    ]
            });

            $("#btnUpdate").click(function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var issueBank = $("#ddlIssueBank").val();
                var adviseBank = $("#ddlAdviseBank").val();
                var issueDate = $("#dtIssueDate").val();
                var futureDay = $("#nbFutureDay").val();
                var lcBala = $("#nbLcBala").val();
                var currency = $("#ddlCurrency").val();

                $.post("Handler/LcUpdateHandler.ashx", {
                    issueBank: issueBank,
                    adviseBank: adviseBank,
                    issueDate: issueDate,
                    futureDay: futureDay,
                    lcBala: lcBala,
                    currency: currency,
                    lcid: $("#hidId").val()
                },
                    function (result) {
                        alert(result);
                        window.document.location = "LcList.aspx";
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
            信用证修改
                        <input type="hidden" id="hidId" runat="server" />
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">开证行：</span>
                    <div id="ddlIssueBank"></div>
                    <input type="hidden" id="hidIssueBank" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">通知行：</span>
                    <div id="ddlAdviseBank"></div>
                    <input type="hidden" id="hidAdviseBank" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">开证日期：</span>
                    <div id="dtIssueDate"></div>
                    <input type="hidden" id="hidIssueDate" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">远期天数：</span>
                    <div id="nbFutureDay"></div>
                    <input type="hidden" id="hidFutureDay" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">信用证金额：</span>
                    <div id="nbLcBala"></div>
                    <input type="hidden" id="hidLcBala" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">信用证币种：</span>
                    <div id="ddlCurrency" runat="server"></div>
                    <input type="hidden" id="hidCurrency" runat="server" />
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnUpdate" value="修改" runat="server" /></span>
                    <span><a target="_self" runat="server" href="LcList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
