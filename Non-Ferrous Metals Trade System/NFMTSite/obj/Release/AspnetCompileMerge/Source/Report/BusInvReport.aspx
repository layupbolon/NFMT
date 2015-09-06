<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusInvReport.aspx.cs" Inherits="NFMTSite.Report.BusInvReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>业务发票表</title>
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

            //我方抬头
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
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
            $("#ddlInnerCorpId").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //对方抬头
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
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
            $("#ddlOutCorpId").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25 });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //发票类型
            var invTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.invoiceTypeValue%>", async: false };
            var invTypeDataAdapter = new $.jqx.dataAdapter(invTypeSource);
            $("#ddlInvoiceType").jqxComboBox({ source: invTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/BusInvReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceName", type: "string" },
                   { name: "innerCorp", type: "string" },
                   { name: "outerCorp", type: "string" },
                   { name: "InvoiceDirection", type: "string" },
                   { name: "InvoiceType", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "number" },
                   { name: "Bala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "RefNo", type: "date" }
                ],
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
                url: url
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                columnsresize: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "开票日期", datafield: "InvoiceDate", width: 70, cellsformat: "yyyy-MM-dd" },
                  { text: "发票号", datafield: "InvoiceNo" },
                  { text: "实际发票号", datafield: "InvoiceName", width: 100 },
                  { text: "我方抬头", datafield: "innerCorp", width: 150 },
                  { text: "对方抬头", datafield: "outerCorp", width: 70 },
                  { text: "开收方向", datafield: "InvoiceDirection", width: 70 },
                  { text: "发票类型", datafield: "InvoiceType", width: 70 },
                  { text: "品种", datafield: "AssetName" },
                  { text: "开票净重", datafield: "NetAmount" },
                  { text: "单位", datafield: "MUName" },
                  { text: "开票金额", datafield: "Bala" },
                  { text: "开票币种", datafield: "CurrencyName", width: 70 },
                  { text: "业务单号", datafield: "RefNo", width: 90 }
                ]
            });

            $("#btnSearch").click(function () {

                var innerCorp = $("#ddlInnerCorpId").val();
                var outCorp = $("#ddlOutCorpId").val();
                var invType = $("#ddlInvoiceType").val();
                var assetId = $("#ddlAssetId").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                source.url = "Handler/BusInvReportHandler.ashx?inner=" + innerCorp + "&outer=" + outCorp + "&invType=" + invType + "&ass=" + assetId + "&s=" + startDate + "&e=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var innerCorp = $("#ddlInnerCorpId").val();
                var outCorp = $("#ddlOutCorpId").val();
                var invType = $("#ddlInvoiceType").val();
                var assetId = $("#ddlAssetId").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=299&inner=" + innerCorp + "&outer=" + outCorp + "&invType=" + invType + "&ass=" + assetId + "&s=" + startDate + "&e=" + endDate;

            });
        });

    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">我方抬头：</span>
                    <div id="ddlInnerCorpId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">对方抬头：</span>
                    <div id="ddlOutCorpId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">开票日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">发票类型：</span>
                    <div id="ddlInvoiceType" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">品种：</span>
                    <div id="ddlAssetId" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <input type="button" id="btnExcel" value="导出Excel" />
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