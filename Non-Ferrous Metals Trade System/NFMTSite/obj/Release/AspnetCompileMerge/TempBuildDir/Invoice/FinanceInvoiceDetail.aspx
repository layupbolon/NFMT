<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceInvoiceDetail.aspx.cs" Inherits="NFMTSite.Invoice.FinanceInvoiceDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            $("#jqxSelectedBIExpander").jqxExpander({ width: "98%" });

            var bussinessInvoiceIds = new Array();
            var sids = $("#hidsids").val();
            var splitItem = sids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    bussinessInvoiceIds.push(splitItem[i]);
                }
            }
            var inCorpId = 0;
            var outCorpId = 0;
            var assetId = 0;
            var currency = 0;

            /////////////////////////////////////已选择业务发票/////////////////////////////////////

            var iids = "";
            for (i = 0; i < bussinessInvoiceIds.length; i++) {
                if (i != 0) { iids += ","; }
                iids += bussinessInvoiceIds[i];
            }

            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "BusinessInvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "InoviceBalaName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "IntegerAmountName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "NetAmountName", type: "string" },
                   { name: "UnitPrice", type: "number" },
                   { name: "MarginRatio", type: "number" },
                   { name: "VATRatio", type: "number" },
                   { name: "VATBala", type: "number" },
                   { name: "AssetId", type: "int" },
                   { name: "CurrencyId", type: "int" },
                   { name: "OutCorpId", type: "int" },
                   { name: "InCorpId", type: "int" },
                   { name: "MUId", type: "int" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxAlreadyGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/GetBussinessInvoiceHandler.ashx?iids=" + iids
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxAlreadyGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  //{ text: "实际票据号", datafield: "InvoiceName" },
                  { text: "收票公司", datafield: "InCorpName" },
                  { text: "开票公司", datafield: "OutCorpName" },
                  { text: "发票金额", datafield: "InoviceBalaName" },
                  { text: "备注", datafield: "Memo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛量", datafield: "IntegerAmountName" },
                  { text: "净量", datafield: "NetAmountName" }
                ]
            });

            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#txbInvoiceDate").jqxDateTimeInput("val", new Date("<%=this.curInvoice.InvoiceDate%>"));

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23, disabled: true });
            $("#txbInvoiceName").jqxInput("val", "<%=this.curInvoice.InvoiceName%>");

            //开票公司
            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=";
            var corpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: corpUrl + "<%=this.outSelf%>", async: false
            };
            corpSource.url = corpUrl + "<%=this.outSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });
            $("#selOutCorp").jqxComboBox("val", "<%=this.curInvoice.OutCorpId%>");

            //收票公司
            corpSource.url = corpUrl + "<%=this.inSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selInCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });
            $("#selInCorp").jqxComboBox("val", "<%=this.curInvoice.InCorpId%>");

            //发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, digits: 9, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });
            $("#txbInvoiceBala").jqxNumberInput("val", "<%=this.curInvoice.InvoiceBala%>");
            $("#txbInvoiceBala").on("valueChanged", function (event) {
                var value = event.args.value;
                var VATRatio = $("#txbVATRatio").val();
                $("#txbVATBala").jqxNumberInput("val", value * VATRatio / 100);
            });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#selCurrency").jqxComboBox("val", "<%=this.curInvoice.CurrencyId%>");

            //品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#selAsset").jqxComboBox("val", "<%=this.curFundsInvoice.AssetId%>");

            //毛重
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbIntegerAmount").jqxNumberInput("val", "<%=this.curFundsInvoice.IntegerAmount%>");

            //净重
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbNetAmount").jqxNumberInput("val", "<%=this.curFundsInvoice.NetAmount%>");

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#selUnit").jqxComboBox("val", "<%=this.curFundsInvoice.MUId%>");

            //增值税率
            $("#txbVATRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, Digits: 3, symbolPosition: "right", symbol: "%", spinButtons: true, width: 100, disabled: true });
            $("#txbVATRatio").jqxNumberInput("val", "<%=this.curFundsInvoice.VATRatio*100%>");
            $("#txbVATRatio").on("valueChanged", function (event) {
                var value = event.args.value;
                var invoiceBala = $("#txbInvoiceBala").val();
                $("#txbVATBala").jqxNumberInput("val", value * invoiceBala / 100);
            });

            //增值税
            $("#txbVATBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbVATBala").jqxNumberInput("val", "<%=this.curFundsInvoice.VATBala%>");

            //备注
            $("#txbMemo").jqxInput({ height: 23, disabled: true });
            $("#txbMemo").jqxInput("val", "<%=this.curInvoice.Memo%>");

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

    <div id="jqxSelectedBIExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择业务发票
        </div>
        <div>
            <input type="hidden" id="hidsids" runat="server" />
            <input type="hidden" id="hidModel" runat="server" />
            <div id="jqxAlreadyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票信息
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
