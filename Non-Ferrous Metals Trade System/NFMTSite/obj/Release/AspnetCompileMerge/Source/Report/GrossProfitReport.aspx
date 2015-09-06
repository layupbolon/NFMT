<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrossProfitReport.aspx.cs" Inherits="NFMTSite.Report.GrossProfitReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>毛利润表</title>
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

            //业务单号
            $("#txbRefNo").jqxInput({ height: 22, width: 120 });

            //卡号
            $("#txbCardNo").jqxInput({ height: 22, width: 120 });

            //品牌
            var ddlBrandIdurl = "../BasicData/Handler/BrandDDLHandler.ashx";
            var ddlBrandIdsource = { datatype: "json", datafields: [{ name: "BrandId" }, { name: "BrandName" }], url: ddlBrandIdurl, async: true };
            var ddlBrandIddataAdapter = new $.jqx.dataAdapter(ddlBrandIdsource);
            $("#ddlBrandId").jqxComboBox({ source: ddlBrandIddataAdapter, displayMember: "BrandName", valueMember: "BrandId", width: 180, height: 25, searchMode: "containsignorecase" });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/GrossProfitReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "InBala", type: "number" },
                   { name: "OutBala", type: "number" },
                   { name: "SIBala", type: "number" },
                   { name: "ProfitBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "StockDate", type: "date" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "InBalaOutCorpName", type: "string" },
                   { name: "InBalaInCorpName", type: "string" },
                   { name: "OutBalaInCorpName", type: "string" },
                   { name: "OutBalaOutCorpName", type: "string" }
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
                sortcolumn: "st.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "st.StockId";
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
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd", width: 100 },
                  { text: "业务单号", datafield: "RefNo",width:150 },
                  { text: "品种", datafield: "AssetName", width: 100 },
                  { text: "品牌", datafield: "BrandName", width: 100 },
                  { text: "交货地", datafield: "DPName", width: 100 },
                  { text: "卡号", datafield: "CardNo", width: 150 },
                  { text: "采购商", datafield: "InBalaOutCorpName", width: 150 },
                  { text: "买方", datafield: "InBalaInCorpName", width: 150 },
                  { text: "采购金额", datafield: "InBala", width: 120 },
                  { text: "卖方", datafield: "OutBalaInCorpName", width: 150 },
                  { text: "销售商", datafield: "OutBalaOutCorpName", width: 150 },
                  { text: "销售金额", datafield: "OutBala", width: 120 },
                  { text: "价外票金额", datafield: "SIBala", width: 120 },
                  { text: "利润", datafield: "ProfitBala", width: 120 },
                  { text: "币种", datafield: "CurrencyName", width: 70 }
                ]
            });

            $("#btnSearch").click(function () {

                var refNo = $("#txbRefNo").val();
                var cardNo = $("#txbCardNo").val();
                var brandId = $("#ddlBrandId").val();
                source.url = "Handler/GrossProfitReportHandler.ashx?refNo=" + refNo + "&cardNo=" + cardNo + "&bid=" + brandId;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var refNo = $("#txbRefNo").val();
                var cardNo = $("#txbCardNo").val();
                var brandId = $("#ddlBrandId").val();
                document.location.href = "ExportExcel.aspx?rt=355&refNo=" + refNo + "&cardNo=" + cardNo + "&bid=" + brandId;
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
                    <span style="float: left;">业务单号：</span>
                    <input type="text" id="txbRefNo" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">卡号：</span>
                    <input type="text" id="txbCardNo" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">品牌：</span>
                    <div id="ddlBrandId" style="float:left;"></div>
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
