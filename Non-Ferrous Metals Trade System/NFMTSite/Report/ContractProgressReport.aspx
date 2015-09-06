<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractProgressReport.aspx.cs" Inherits="NFMTSite.Report.ContractProgressReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约执行进度表</title>
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

            $("#txbContractNo").jqxInput({ width:120,height:23 });

            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase"
            });

            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase"
            });

            var tradeBorderSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.TradeBorderValue%>", async: false };
            var tradeBorderDataAdapter = new $.jqx.dataAdapter(tradeBorderSource);
            $("#selTradeBorder").jqxComboBox({ source: tradeBorderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });

            var tradeDirectionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.TradeDirectionValue%>", async: false };
            var tradeDirectionDataAdapter = new $.jqx.dataAdapter(tradeDirectionSource);
            $("#selTradeDirection").jqxComboBox({ source: tradeDirectionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });

            $("#txbStartDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbStartDate").jqxDateTimeInput({ value: tempDate });
            $("#txbEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/ContractProgressReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "ContractId", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "ContractNo", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "TradeDirectionName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "PriceModeName", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "SignAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "InAmount", type: "number" },
                   { name: "OutAmount", type: "number" },
                   { name: "PricingWeight", type: "number" },
                   { name: "AvgPrice", type: "number" },
                   { name: "PreAmount", type: "number" },
                   { name: "PreBala", type: "number" },
                   { name: "FinAmount", type: "number" },
                   { name: "FinBala", type: "number" },
                   { name: "InBala", type: "number" },
                   { name: "OutBala", type: "number" },
                   { name: "TradeBorderName", type: "string" }
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
                sortcolumn: "con.ContractId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "con.ContractId";
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
                  { text: "签订日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd", width: 90 },
                  { text: "我方抬头", datafield: "InCorpName", width: 150 },
                  { text: "内部合约号", datafield: "ContractNo", width: 100 },
                  { text: "外部合约号", datafield: "OutContractNo", width: 100 },
                  { text: "对方抬头", datafield: "OutCorpName", width: 150 },
                  { text: "购销", datafield: "TradeDirectionName", width: 60 },
                  { text: "内外贸", datafield: "TradeBorderName", width: 60 },
                  { text: "品种", datafield: "AssetName", width: 60 },
                  { text: "定价方式", datafield: "PriceModeName", width: 70 },
                  { text: "结算币种", datafield: "CurrencyName", width: 70 },
                  { text: "签订数量", datafield: "SignAmount", width: 80 },
                  { text: "单位", datafield: "MUName", width: 60 },
                  { text: "收货数量", datafield: "InAmount", width: 80 },
                  { text: "发货数量", datafield: "OutAmount", width: 80 },
                  { text: "已点价数量", datafield: "PricingWeight", width: 90 },
                  { text: "已点价均价", datafield: "AvgPrice", width: 90 },
                  { text: "临票净重", datafield: "PreAmount", sortable: false, width: 80 },
                  { text: "临票金额", datafield: "PreBala", sortable: false, width: 80 },
                  { text: "终票净重", datafield: "FinAmount", sortable: false, width: 80 },
                  { text: "终票金额", datafield: "FinBala", sortable: false, width: 80 },
                  { text: "已付金额", datafield: "OutBala", sortable: false, width: 80 },
                  { text: "已收金额", datafield: "InBala", sortable: false, width: 80 }
                ]
            });

            $("#btnSearch").click(function () {

                var contractNo = $("#txbContractNo").val();
                var outCorpId = $("#selOutCorp").val();
                var inCorpId = $("#selInCorp").val();
                var tradeBorder = $("#selTradeBorder").val();
                var tradeDirection = $("#selTradeDirection").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                source.url = "Handler/ContractProgressReportHandler.ashx?cn=" + contractNo + "&oci=" + outCorpId + "&ici=" + inCorpId + "&tb=" + tradeBorder + "&td=" + tradeDirection + "&sd=" + startDate + "&ed=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var contractNo = $("#txbContractNo").val();
                var outCorpId = $("#selOutCorp").val();
                var inCorpId = $("#selInCorp").val();
                var tradeBorder = $("#selTradeBorder").val();
                var tradeDirection = $("#selTradeDirection").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=302&cn=" + contractNo + "&oci=" + outCorpId + "&ici=" + inCorpId + "&tb=" + tradeBorder + "&td=" + tradeDirection + "&sd=" + startDate + "&ed=" + endDate;

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
                    <span style="float: left;">我方公司：</span>
                    <div id="selInCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">对方公司：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">内外贸</span>
                    <div id="selTradeBorder" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">购销</span>
                    <div id="selTradeDirection" style="float: left;" />
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
