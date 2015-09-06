<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoPriceReport.aspx.cs" Inherits="NFMTSite.Report.DoPriceReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价流水</title>
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

            //合约号
            $("#txbContractNo").jqxInput({ height: 22, width: 120 });

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

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/DoPriceReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "PricingId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "FuturesCodeId", type: "int" },
                   { name: "TradeCode", type: "string" },
                   { name: "ExchangeCode", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "TurnoverHand", type: "number" },
                   { name: "Turnover", type: "number" },
                   { name: "PricingStyle", type: "string" },
                   { name: "SpotQP", type: "date" },
                   { name: "Spread", type: "number" },
                   { name: "Premium", type: "number" },
                   { name: "DelayFee", type: "number" },
                   { name: "OtherFee", type: "number" },
                   { name: "FinalPrice", type: "number" }
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
                sortcolumn: "p.PricingId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "p.PricingId";
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
                //columnsresize: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "点价编号", datafield: "PricingId", width: 70 },
                  { text: "合约编号", datafield: "ContractNo", width: 100 },
                  { text: "外部合约号", datafield: "OutContractNo", width: 100 },
                  { text: "客户名称", datafield: "CorpName",width:160 },
                  { text: "交易品种", datafield: "AssetName", width: 70 },
                  { text: "期货合约", datafield: "TradeCode", width: 70 },
                  { text: "交易所", datafield: "ExchangeCode", width: 70 },
                  { text: "币种", datafield: "CurrencyName", width: 70 },
                  { text: "成交手", datafield: "TurnoverHand", width: 80 },
                  { text: "成交量", datafield: "Turnover", width: 80 },
                  { text: "点价方式", datafield: "PricingStyle", width: 70 },
                  { text: "调期到日期", datafield: "SpotQP", cellsformat: "yyyy-MM-dd", width: 100 },
                  { text: "调期费", datafield: "Spread", width: 80 },
                  { text: "升贴水", datafield: "Premium", width: 80 },
                  { text: "延期费", datafield: "DelayFee", width: 80 },
                  { text: "其他费用", datafield: "OtherFee", width: 80 },
                  { text: "最终价格", datafield: "FinalPrice", width: 80 }
                ]
            });

            $("#btnSearch").click(function () {

                var contractNo = $("#txbContractNo").val();
                var assetId = $("#ddlAssetId").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                source.url = "Handler/DoPriceReportHandler.ashx?c=" + contractNo + "&a=" + assetId + "&s=" + startDate + "&e=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var contractNo = $("#txbContractNo").val();
                var assetId = $("#ddlAssetId").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=298&c=" + contractNo + "&a=" + assetId + "&s=" + startDate + "&e=" + endDate;

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
                    <span>合约号：</span>
                    <span>
                        <input type="text" id="txbContractNo" /></span>
                </li>
                <li>
                    <span style="float: left;">品种：</span>
                    <div id="ddlAssetId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">点价日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
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