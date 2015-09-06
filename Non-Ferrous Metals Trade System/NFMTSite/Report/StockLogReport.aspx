<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockLogReport.aspx.cs" Inherits="NFMTSite.Report.StockLogReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存流水</title>
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

            $("#txbRefNo").jqxInput({ height: 23, width: 120 });

            var logTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.LogTypeValue%>", async: false };
            var logTypeDataAdapter = new $.jqx.dataAdapter(logTypeSource);
            $("#selLogType").jqxComboBox({ source: logTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });
            var customsTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.CustomsTypeValue%>", async: false };
            var customsTypeDataAdapter = new $.jqx.dataAdapter(customsTypeSource);
            $("#selCustomsType").jqxComboBox({ source: customsTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });

            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#selAsset").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25 });

            $("#txbStartDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbStartDate").jqxDateTimeInput({ value: tempDate });
            $("#txbEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });


            var refNo = $("#txbRefNo").val();
            var logType = $("#selLogType").val();
            var customsType = $("#selCustomsType").val();
            var asset = $("#selAsset").val();
            var startDate = $("#txbStartDate").val();
            var endDate = $("#txbEndDate").val();

            var url = "Handler/StockLogReportHandler.ashx?&rn=" + refNo + "&lt=" + logType + "&ct=" + customsType + "&ass=" + asset + "&sd=" + startDate + "&ed=" + endDate;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "LogDate", type: "date" },
                   { name: "LogTypeName", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "GrossAmount", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "TradeDirectionName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "AvgPrice", type: "number" },
                   { name: "CurrencyName", type: "string" }
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
                sortcolumn: "sl.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sl.StockLogId";
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
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "流水日期", datafield: "LogDate", cellsformat: "yyyy-MM-dd", width: 90 },
                  { text: "流水类型", datafield: "LogTypeName", width: 70 },
                  { text: "业务单号", datafield: "RefNo", width: 100 },
                  { text: "物权编号", datafield: "PaperNo", width: 100 },
                  { text: "品种", datafield: "AssetName", width: 60 },
                  { text: "品牌", datafield: "BrandName", width: 80 },
                  { text: "报关状态", datafield: "CustomsTypeName", width: 70 },
                  { text: "库位", datafield: "DPName", width: 80 },
                  { text: "流水毛量", datafield: "GrossAmount", width: 80 },
                  { text: "流水净量", datafield: "NetAmount", width: 80 },
                  { text: "单位", datafield: "MUName", width: 60 },
                  { text: "卡号", datafield: "CardNo", width: 80 },
                  { text: "我方抬头", datafield: "InCorpName", width: 150 },
                  { text: "对方抬头", datafield: "OutCorpName", width: 150 },
                  { text: "关联合约", datafield: "SubNo", width: 120 },
                  { text: "购销方向", datafield: "TradeDirectionName", width: 70 },
                  { text: "购销价格", datafield: "AvgPrice", width: 80 },
                  { text: "价格币种", datafield: "CurrencyName", width: 70 }
                ]
            });

            $("#btnSearch").click(function () {

                var refNo = $("#txbRefNo").val();
                var logType = $("#selLogType").val();
                var customsType = $("#selCustomsType").val();
                var asset = $("#selAsset").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                source.url = "Handler/StockLogReportHandler.ashx?&rn=" + refNo + "&lt=" + logType + "&ct=" + customsType + "&ass=" + asset + "&sd=" + startDate + "&ed=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var refNo = $("#txbRefNo").val();
                var logType = $("#selLogType").val();
                var customsType = $("#selCustomsType").val();
                var asset = $("#selAsset").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=295&rn=" + refNo + "&lt=" + logType + "&ct=" + customsType + "&ass=" + asset + "&sd=" + startDate + "&ed=" + endDate;

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
                    <span>业务单号</span>
                    <span>
                        <input type="text" id="txbRefNo" /></span>
                </li>
                <li>
                    <span style="float: left;">流水类型</span>
                    <div id="selLogType" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">报关状态</span>
                    <div id="selCustomsType" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">品种</span>
                    <div id="selAsset" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">流水日期：</span>
                    <div id="txbStartDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbEndDate" style="float: left;"></div>
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
