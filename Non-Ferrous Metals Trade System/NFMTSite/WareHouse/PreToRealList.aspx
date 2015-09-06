<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreToRealList.aspx.cs" Inherits="NFMTSite.WareHouse.PreToRealList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预入库转正式库存</title>
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

            $("#btnSearch").jqxButton({ height: 25, width: 120 });

            var corpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOwnCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, searchMode: "containsignorecase"
            });
            
            $("#jqxDataExpander").jqxExpander({ width: "98%" });
            var url = "../Report/Handler/StockReportHandler.ashx?s=<%=(int)NFMT.WareHouse.StockStatusEnum.预入库存%>";
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
                   { name: "NetWeight", type: "string" },
                   { name: "GrossWeight", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" }
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
                var item = $("#jqxgrid").jqxGrid("getrowdata", row);

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";

                cellHtml += "<a target=\"_self\" href=\"PreToRealCreate.aspx?id=" + value + "\">转正式库存</a>"
                cellHtml += "</div>";
                return cellHtml;
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
                  { text: "毛量", datafield: "NetWeight", width: 100 },
                  { text: "净量", datafield: "GrossWeight", width: 100 },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "权证编号", datafield: "PaperNo" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "报关状态", datafield: "CustomsTypeName" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "操作", datafield: "StockId", cellsrenderer: cellsrenderer, width: 120, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var fromDate = $("#txbFromStockInDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToStockInDate").jqxDateTimeInput("getText");
                var corpId = $("#selOwnCorp").jqxComboBox("val");
                var stockName = $("#txbStockName").val();

                source.url = "../Report/Handler/StockReportHandler.ashx?s=<%=(int)NFMT.WareHouse.StockStatusEnum.预入库存%>&sn=" + stockName + "&ci=" + corpId + "&sdb=" + fromDate + "&sde=" + toDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
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
                    <span>业务单号：</span>
                    <span>
                        <input type="text" id="txbStockName" /></span>
                </li>
                <li>
                    <span style="float: left;">归属公司：</span>
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