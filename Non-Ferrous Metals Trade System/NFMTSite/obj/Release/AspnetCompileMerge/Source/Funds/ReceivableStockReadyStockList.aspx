<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableStockReadyStockList.aspx.cs" Inherits="NFMTSite.Funds.ReceivableStockReadyStockList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>可收款分配库存列表</title>
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

            $("#txbStockName").jqxInput({ height: 22, width: 120 });

            $("#txbFromStockInDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromStockInDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToStockInDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });


            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOwnCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });
            var emptyItem = { CorpName: " ", CorpId: 0 };
            $("#selOwnCorp").jqxComboBox("insertAt", emptyItem, 0);

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var fromDate = $("#txbFromStockInDate").jqxDateTimeInput("getText");
            var toDate = $("#txbToStockInDate").jqxDateTimeInput("getText");
            var corpId = $("#selOwnCorp").jqxComboBox("val");
            var stockName = $("#txbStockName").val();
            var url = "Handler/ReceivableStockReadyStockListHandler.ashx?sn=" + stockName + "&ci=" + corpId + "&sdb=" + fromDate + "&sde=" + toDate;

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "StockWeight", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "AllotBala", type: "string" }
                ],
                sort: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockId";
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
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">"
                   + "<a target=\"_self\" href=\"ReceivableStockCreate.aspx?id=" + value + "\">分配</a>"
                   + "</div>";
            }
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
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "入库重量", datafield: "StockWeight" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "报关状态", datafield: "CustomsTypeName" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "已分配金额", datafield: "AllotBala", sortable: false },
                  { text: "操作", datafield: "StockId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var fromDate = $("#txbFromStockInDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToStockInDate").jqxDateTimeInput("getText");
                var corpId = $("#selOwnCorp").jqxComboBox("val");
                var stockName = $("#txbStockName").val();

                source.url = "Handler/ReceivableStockReadyStockListHandler.ashx?sn=" + stockName + "&ci=" + corpId + "&sdb=" + fromDate + "&sde=" + toDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });
        });

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>业务单号</span>
                    <span>
                        <input type="text" id="txbStockName" /></span>
                </li>
                <li>
                    <span style="float: left;">归属公司</span>
                    <div id="selOwnCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">入库日期：</span>
                    <div id="txbFromStockInDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToStockInDate" style="float: left;"></div>
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
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
