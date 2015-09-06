<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunInvReport.aspx.cs" Inherits="NFMTSite.Report.FunInvReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务发票表</title>
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
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            //财务发票号
            $("#txbInvoiceNo").jqxInput({ height: 23, width: 120 });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23, width: 120 });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //开收方向
            var invoiceDirectionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.invoiceDirection%>", async: false };
            var invoiceDirectionDataAdapter = new $.jqx.dataAdapter(invoiceDirectionSource);
            $("#ddlInvoiceDirection").jqxComboBox({ source: invoiceDirectionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            //$("#btnExcel").jqxButton({ height: 25, width: 120 });

            var invUrl = "Handler/FunInvReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var invSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "FinanceInvoiceId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "DetailName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "innerCorp", type: "string" },
                   { name: "outerCorp", type: "string" }
                ],
                type: "GET",
                url: invUrl,
                sort: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
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
                }
            };
            var invAdapter = new $.jqx.dataAdapter(invSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var initrowdetails = function (index, parentElement, gridElement, record) {
                var id = record.FinanceInvoiceId;

                var allotUrl = "Handler/FinBusInvAllotReportHandler.ashx?id=" + id;
                var allotSource =
                {
                    datatype: "json",
                    datafields:
                    [
                       { name: "InvoiceId", type: "int" },
                       { name: "BusinessInvoiceId", type: "int" },
                       { name: "InvoiceDate", type: "date" },
                       { name: "InvoiceNo", type: "string" },
                       { name: "InvoiceName", type: "string" },
                       { name: "DetailName", type: "string" },
                       { name: "AssetName", type: "string" },
                       { name: "NetAmount", type: "number" },
                       { name: "MUName", type: "string" },
                       { name: "InvoiceBala", type: "number" },
                       { name: "CurrencyName", type: "string" }
                    ],
                    type: "GET",
                    url: allotUrl,
                    async: false
                };

                var allotAdapter = new $.jqx.dataAdapter(allotSource, { autoBind: true });

                var grid = $($(parentElement).children()[0]);
                if (grid != null) {
                    grid.jqxGrid({
                        columnsresize: true,
                        autoheight: true,
                        source: allotAdapter, width: 1000,
                        columns: [
                          { text: '开票日期', datafield: 'InvoiceDate', width: 100, cellsformat: "yyyy-MM-dd" },
                          { text: '发票号', datafield: 'InvoiceNo', width: 120 },
                          { text: '实际发票号', datafield: 'InvoiceName', width: 120 },
                          { text: '开收方向', datafield: 'DetailName', width: 100 },
                          { text: '品种', datafield: 'AssetName', width: 100 },
                          { text: '净重', datafield: 'NetAmount', width: 120 },
                          { text: '单位', datafield: 'MUName', width: 100 },
                          { text: '发票金额', datafield: 'InvoiceBala', width: 120 },
                          { text: '币种', datafield: 'CurrencyName', width: 100 }
                        ]
                    });
                }
            }

            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: invAdapter,
                pageable: true,
                autoheight: true,
                sortable: true,
                enabletooltips: true,
                columnsresize: true,
                rowdetails: true,
                virtualmode: true,
                sorttogglestates: 1,
                initrowdetails: initrowdetails,
                rowdetailstemplate: { rowdetails: "<div id='grid' style='margin: 10px;'></div>" },//, rowdetailsheight: 220, rowdetailshidden: true
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "实际发票号", datafield: "InvoiceName" },
                  { text: "财务发票号", datafield: "InvoiceNo" },
                  { text: "开收方向", datafield: "DetailName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "单位", datafield: "MUName" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "我方抬头", datafield: "innerCorp" },
                  { text: "对方抬头", datafield: "outerCorp" }
                ]
            });

            $("#btnSearch").click(function () {

                var invNo = $("#txbInvoiceNo").val();
                var invName = $("#txbInvoiceName").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val(); 
                var invDir = $("#ddlInvoiceDirection").val();

                invSource.url = "Handler/FunInvReportHandler.ashx?invNo=" + invNo + "&invName=" + invName + "&invDir=" + invDir + "&s=" + startDate + "&e=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                //var customsCorpId = $("#ddlCustomsCorpId").val();
                //var refNo = $("#txbRefNo").val();
                //var startDate = $("#dtBeginDate").val();
                //var endDate = $("#dtEndDate").val();

                //document.location.href = "ExportExcel.aspx?rt=301&c=" + customsCorpId + "&r=" + refNo + "&s=" + startDate + "&e=" + endDate;

            });
        });

    </script>
</head>
<body class='default'>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">财务发票号：</span>
                    <input type="text" id="txbInvoiceNo" style="float: left;"/>
                </li>
                <li>
                    <span style="float: left;">实际发票号：</span>
                    <input type="text" id="txbInvoiceName" style="float: left;"/>
                </li>
                <li>
                    <span style="float: left;">开票日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">开收方向：</span>
                    <div id="ddlInvoiceDirection" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                   <%-- <input type="button" id="btnExcel" value="导出Excel" />--%>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
