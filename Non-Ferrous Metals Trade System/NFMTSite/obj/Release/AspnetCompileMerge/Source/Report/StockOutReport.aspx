<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutReport.aspx.cs" Inherits="NFMTSite.Report.StockOutReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出库报表</title>
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

            var url = "Handler/StockOutReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;

            var initrowdetails = function (index, parentElement, gridElement, record) {
                                
                var detailUrl = "Handler/StockOutDetailReportHandler.ashx?soai=" + record.StockOutApplyId;
                
                var detailSource = {
                    datafields: [
                        { name: "DetailId", type: "int" },
                        { name: "StockOutTime", type: "date" },
                        { name: "RefNo", type: "string" },
                        { name: "CardNo", type: "string" },
                        { name: "PaperNo", type: "string" },
                        { name: "BrandName", type: "string" },
                        { name: "NetAmount", type: "number" },
                        { name: "StatusName", type: "string" }
                    ],
                    type: "GET",
                    datatype: "json",
                    url: detailUrl
                }
                var detailAdapter = new $.jqx.dataAdapter(detailSource, { autoBind: true });

                var grid = $($(parentElement).children()[0]);
                if (grid != null) {
                    grid.jqxGrid({
                        width: "98%",
                        source: detailAdapter,
                        pageable: true,
                        autoheight: true,
                        sorttogglestates: 1,
                        selectionmode: "singlecell",                     
                        enabletooltips: true,
                        columns: [
                            { text: "出库日期", datafield: "StockOutTime", cellsformat: "yyyy-MM-dd" },
                            { text: "业务单号", datafield: "RefNo" },
                            { text: "卡号", datafield: "CardNo" },
                            { text: "权证编号", datafield: "PaperNo" },
                            { text: "品牌", datafield: "BrandName" },
                            { text: "出库重量", datafield: "ApplyWeight" },
                            { text: "执行状态", datafield: "StatusName" }
                        ]
                    });
                }
                
                var i = 0;
                //row0jqxgrid;
                //for (i = 0 ; i < 20; i++) {
                //}
            }

            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockOutApplyId", type: "int" },
                   { name: "ApplyTime", type: "date" },
                   { name: "ApplyNo", type: "string" },
                   { name: "ApplyCorpName", type: "string" },
                   { name: "ApplyDeptName", type: "string" },
                   { name: "EmpName", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" }, 
                   { name: "StatusName", type: "string" },
                   { name: "ApplyWeight", type: "number" },
                   { name: "MUName", type: "string" }
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
                sortcolumn: "soa.StockOutApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "soa.StockOutApplyId";
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
                rowdetails: true,
                virtualmode: true,
                sorttogglestates: 1,
                selectionmode: "singlecell",
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                initrowdetails: initrowdetails,
                rowdetailstemplate: { rowdetails: "<div id='jqxDetailGrid' style='float: left; margin: 5px 0 0 5px;'></div>", rowdetailshidden: true },
                columns: [
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请单号", datafield: "ApplyNo" },
                  { text: "申请公司", datafield: "ApplyCorpName" },
                  { text: "申请部门", datafield: "ApplyDeptName" },
                  { text: "申请人", datafield: "EmpName" },                  
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "我方公司", datafield: "InCorpName" },
                  { text: "对方公司", datafield: "OutCorpName" },
                  { text: "品种", datafield: "AssetName", width: 70 },
                  { text: "申请重量", datafield: "ApplyWeight" },
                  { text: "单位", datafield: "MUName" },
                  { text: "申请状态", datafield: "StatusName", width: 70 }
                ]
            });

            $("#btnSearch").click(function () {

                var outCorpId = $("#selOutCorp").val();
                var inCorpId = $("#selInCorp").val();
                var asset = $("#selAsset").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                source.url = "Handler/StockOutReportHandler.ashx?oci=" + outCorpId + "&ici=" + inCorpId + "&ass=" + asset + "&sd=" + startDate + "&ed=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxgrid');
                //var outCorpId = $("#selOutCorp").val();
                //var inCorpId = $("#selInCorp").val();
                //var asset = $("#selAsset").val();
                //var startDate = $("#txbStartDate").val();
                //var endDate = $("#txbEndDate").val();

                //document.location.href = "ExportExcel.aspx?rt=296&oci=" + outCorpId + "&ici=" + inCorpId + "&ass=" + asset + "&sd=" + startDate + "&ed=" + endDate;

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
                    <span style="float: left;">我方公司：</span>
                    <div id="selInCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">对方公司：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">品种：</span>
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