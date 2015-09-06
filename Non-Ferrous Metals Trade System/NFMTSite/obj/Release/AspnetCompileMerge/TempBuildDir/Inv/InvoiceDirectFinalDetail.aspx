<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceDirectFinalDetail.aspx.cs" Inherits="NFMTSite.Inv.InvoiceDirectFinalDetail" %>
<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>直接终票明细</title>
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
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });
            
            //init stock list
            var formatedData = "";
            var totalrecords = 0;
            var selectSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "ConfirmDetailId", type: "int" },
                   { name: "ConfirmPriceId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "LastAmount", type: "number" },
                   { name: "UnitPrice", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "StockStatus", type: "int" },
                   { name: "StockStatusName", type: "string" },
                   { name: "Bala", type: "number" }
                ],
                sort: function () {
                    $("#jqxStockGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.SelectedJson%>
                };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error); 
                }
            });
           
            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,                
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },                  
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "开票净重", datafield: "NetAmount" },
                  { text: "结算单价", datafield: "UnitPrice" },
                  { text: "开票金额", datafield: "Bala", width: 160 }
                ]
            }); 

            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120,disabled:true });
            
            //selOutCorp 对方公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,disabled:true });

            //selInCorp 我方公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,disabled:true });

            //txbInvoiceBala 发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, disabled: true });

            //txbUnitPrice 发票单价
            $("#txbUnitPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true , disabled: true });
            $("#txbUnitPrice").val(<%=this.curBusinessInvoice.UnitPrice%>);

            //品种 selAsset
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });

            //毛重 txbIntegerAmount
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });

            //净重 txbNetAmount
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //计量单位 selUnit
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, disabled: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23,disabled:true });           

            //set control data
            var tempDate = new Date("<%=this.curInvoice.InvoiceDate.ToString("yyyy/MM/dd")%>");
            $("#txbInvoiceDate").jqxDateTimeInput({ value: tempDate });
            $("#selOutCorp").val(<%=this.curInvoice.OutCorpId%>);
            $("#selInCorp").val(<%=this.curInvoice.InCorpId%>);
            $("#txbInvoiceBala").val(<%=this.curInvoice.InvoiceBala%>);            
            $("#selCurrency").val(<%=this.curInvoice.CurrencyId%>);
            $("#selAsset").val(<%=this.curBusinessInvoice.AssetId%>);
            $("#txbIntegerAmount").val(<%=this.curBusinessInvoice.IntegerAmount%>);
            $("#txbNetAmount").val(<%=this.curBusinessInvoice.NetAmount%>);
            $("#selUnit").val(<%=this.curBusinessInvoice.MUId%>);
            $("#txbMemo").val("<%=this.curInvoice.Memo%>");

            //buttons
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 20,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("../Invoice/Handler/InvoiceDirectFinalStatusHandler.ashx", { ii: "<%=this.curBusinessInvoice.BusinessInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceDirectFinalList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("../Invoice/Handler/InvoiceDirectFinalStatusHandler.ashx", { ii: "<%=this.curBusinessInvoice.BusinessInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceDirectFinalList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("../Invoice/Handler/InvoiceDirectFinalStatusHandler.ashx", { ii: "<%=this.curBusinessInvoice.BusinessInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceDirectFinalList.aspx";
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("../Invoice/Handler/InvoiceDirectFinalStatusHandler.ashx", { ii: "<%=this.curBusinessInvoice.BusinessInvoiceId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "InvoiceDirectFinalList.aspx";
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票明细列表
        </div>
        <div>
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>开票日期：</strong>
                    <div id="txbInvoiceDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>对方公司：</strong>
                    <div id="selOutCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>我方公司：</strong>
                    <div id="selInCorp" style="float: left;"></div>
                </li>
                 <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>
                <li>
                    <strong>单价：</strong>
                    <div id="txbUnitPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div id="txbInvoiceBala" style="float: left;"></div>
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
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

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
