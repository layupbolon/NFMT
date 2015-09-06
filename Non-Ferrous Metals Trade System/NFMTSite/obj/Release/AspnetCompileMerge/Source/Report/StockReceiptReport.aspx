<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReceiptReport.aspx.cs" Inherits="NFMTSite.Report.StockReceiptReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存磅差表</title>
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

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25 });

            //业务单号
            $("#txbRefNo").jqxInput({ height: 23, width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/StockReceiptReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "RefId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "StockDate", type: "date" },
                   { name: "OutContractNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "inNetAmount", type: "number" },
                   { name: "ReceiptInGap", type: "number" },
                   { name: "inGapAmount", type: "number" },
                   { name: "inRate", type: "string" },
                   { name: "outNetAmount", type: "number" },
                   { name: "ReceiptOutGap", type: "number" },
                   { name: "outGapAmount", type: "number" },
                   { name: "outRate", type: "string" },
                   { name: "ProfitOrLoss", type: "number" }
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
                sortcolumn: "sis.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sis.RefId";
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
                  { text: "品种", datafield: "AssetName", width: 70},
                  { text: "业务单号", datafield: "RefNo", width: 130 },
                  { text: "物权编号", datafield: "PaperNo", width: 120 },
                  { text: "卡号", datafield: "CardNo", width: 120 },
                  { text: "入库日期", datafield: "StockDate", width: 100, cellsformat: "yyyy-MM-dd" },
                  { text: "采购合约", datafield: "OutContractNo", width: 110 },
                  { text: "入账公司", datafield: "InCorpName", width: 150 },
                  { text: "供应商", datafield: "OutCorpName", width: 150 },
                  { text: "单位", datafield: "MUName", width: 50 },
                  { text: "入库净重", datafield: "inNetAmount", width: 80 },
                  { text: "入库回执净重", datafield: "ReceiptInGap", width: 100 },
                  { text: "入库磅差", datafield: "inGapAmount", width: 80 },
                  { text: "入库磅差比例", datafield: "inRate", width: 100 },
                  { text: "出库净重", datafield: "outNetAmount", width: 80 },
                  { text: "出库回执净重", datafield: "ReceiptOutGap", width: 100 },
                  { text: "出库磅差", datafield: "outGapAmount", width: 80 },
                  { text: "出库磅差比例", datafield: "outRate", width: 100 },
                  { text: "磅盈/亏", datafield: "ProfitOrLoss", width: 70 }
                ]
            });

            $("#btnSearch").click(function () {

                var assetId = $("#ddlAssetId").val();
                var refNo = $("#txbRefNo").val();

                source.url = "Handler/StockReceiptReportHandler.ashx?ass=" + assetId + "&refNo=" + refNo;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var assetId = $("#ddlAssetId").val();
                var refNo = $("#txbRefNo").val();

                document.location.href = "ExportExcel.aspx?rt=297&ass=" + assetId + "&refNo=" + refNo;

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
                    <span style="float: left;">品种：</span>
                    <div id="ddlAssetId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">业务单号：</span>
                    <input type="text" id="txbRefNo" style="float:left;" />
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