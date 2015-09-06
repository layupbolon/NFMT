<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceFundsDetail.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceFundsDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务发票明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curInvoice.DataBaseName%>" + "&t=" + "<%=this.curInvoice.TableName%>" + "&id=" + "<%=this.curInvoice.InvoiceId%>";

        $(document).ready(function () {
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });

            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23, disabled: true });

            //selOutCorp 开票公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=" + "<%=this.outSelf%>";
            var outCorpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });

            //selInCorp 收票公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=" + "<%=this.inSelf%>";
            var inCorpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: inCorpUrl, async: false
            };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });

            //txbInvoiceBala 发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //品种 selAsset
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //毛重 txbIntegerAmount
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //净重 txbNetAmount
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //计量单位 selUnit
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //增值税率 txbVATRatio
            $("#txbVATRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbolPosition: "right", spinButtons: true, width: 100, disabled: true });

            //增值税 txbVATBala
            $("#txbVATBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23, disabled: true });

            //set control data
            var tempDate = new Date("<%=this.curInvoice.InvoiceDate.ToString("yyyy/MM/dd")%>");
            $("#txbInvoiceDate").jqxDateTimeInput({ value: tempDate });

            $("#txbInvoiceName").val("<%=this.curInvoice.InvoiceName%>");

            $("#selOutCorp").val(<%=this.curInvoice.OutCorpId%>);

            $("#selInCorp").val(<%=this.curInvoice.InCorpId%>);

            $("#txbInvoiceBala").val(<%=this.curInvoice.InvoiceBala%>);

            $("#selCurrency").val(<%=this.curInvoice.CurrencyId%>);

            $("#selAsset").val(<%=this.curFundsInvoice.AssetId%>);

            $("#txbIntegerAmount").val(<%=this.curFundsInvoice.IntegerAmount%>);

            $("#txbNetAmount").val(<%=this.curFundsInvoice.NetAmount%>);

            $("#selUnit").val(<%=this.curFundsInvoice.MUId%>);

            $("#txbVATRatio").val(<%=this.curFundsInvoice.VATRatio%>);

            $("#txbVATBala").val(<%=this.curFundsInvoice.VATBala%>);

            $("#txbMemo").val("<%=this.curInvoice.Memo%>");

            //buttons
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 17,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/FundsInvoiceStatusHandler.ashx", { ii: "<%=this.curFundsInvoice.FinanceInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceFundsList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/FundsInvoiceStatusHandler.ashx", { ii: "<%=this.curFundsInvoice.FinanceInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceFundsList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/FundsInvoiceStatusHandler.ashx", { ii: "<%=this.curFundsInvoice.FinanceInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceFundsList.aspx";
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/FundsInvoiceStatusHandler.ashx", { ii: "<%=this.curFundsInvoice.FinanceInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceFundsList.aspx";
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票信息<input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong runat="server" id="titInvDate">开票日期：</strong>
                    <div id="txbInvoiceDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>实际发票号：</strong>
                    <input type="text" id="txbInvoiceName" style="float: left;" />
                </li>
                <li>
                    <strong>开票公司：</strong>
                    <div id="selOutCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>收票公司：</strong>
                    <div id="selInCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div id="txbInvoiceBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>

                <li>
                    <strong>品种：</strong>
                    <div id="selAsset" style="float: left;"></div>
                </li>

                <li>
                    <strong>毛重：</strong>
                    <div id="txbIntegerAmount" style="float: left;" />
                </li>
                <li>
                    <strong>净重：</strong>
                    <div id="txbNetAmount" style="float: left;"></div>
                </li>

                <li>
                    <strong>计量单位：</strong>
                    <div id="selUnit" style="float: left;"></div>
                </li>

                <li>
                    <strong>增值税率：</strong>
                    <div id="txbVATRatio" style="float: left;"></div>
                </li>
                <li>
                    <strong>增值税：</strong>
                    <div id="txbVATBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="InvoiceAttach" />

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>

</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
