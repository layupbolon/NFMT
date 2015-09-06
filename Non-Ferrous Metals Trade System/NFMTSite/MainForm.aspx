<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="NFMTSite.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="Description">有色金属业务管理系统</title>
    <link rel="stylesheet" href="jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="css/Layout.css" type="text/css" />
    <script type="text/javascript" src="jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="js/status.js"></script>
    <script type="text/javascript" src="js/Utility.js"></script>

    <script type="text/javascript">
        $(document).ready(
            function () {
                var cssUrl = "jqwidgets/styles/jqx." + $.jqx.theme + ".css";
                var link = $('<link rel="stylesheet" href="' + cssUrl + '" media="screen" />');

                $(document).find('head').append(link);

                //我的任务
                var formatedData = "";
                var totalrecords = 0;
                var taskSource =
                {
                    datafields:
                    [
                       { name: "TaskNodeId", type: "int" },
                       { name: "TaskName", type: "string" },
                       { name: "Name", type: "string" },
                       { name: "ApplyTime", type: "date" },
                       { name: "StatusName", type: "string" }
                    ],
                    datatype: "json",
                    sort: function () {
                        $("#jqxTaskGrid").jqxGrid("updatebounddata", "sort");
                    },
                    beforeprocessing: function (data) {
                        var returnData = {};
                        totalrecords = data.count;
                        returnData.totalrecords = data.count;
                        returnData.records = data.data;
                        return returnData;
                    },
                    type: "GET",
                    sortcolumn: "tn.TaskNodeId",
                    sortdirection: "desc",
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 10;
                        data.sortdatafield = data.sortdatafield || "tn.TaskNodeId";
                        data.sortorder = data.sortorder || "desc";
                        data.filterscount = data.filterscount || 0;
                        formatedData = buildQueryString(data);
                        return formatedData;
                    },
                    url: "WorkFlow/Handler/TaskListHandler.ashx?s=" + statusEnum.待审核
                };
                var taskDataAdapter = new $.jqx.dataAdapter(taskSource, {
                    contentType: "application/json; charset=utf-8",
                    loadError: function (xhr, status, error) {

                        alert(error);
                    }
                });
                var taskCellsRenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                    var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                    cellHtml += "<a href=\"WorkFlow/TaskDetailWithAttach.aspx?NodeId=" + value + "\">明细</a>";
                    cellHtml += "</div>";
                    return cellHtml;
                }
                $("#jqxTaskGrid").jqxGrid(
                {
                    width: 500,
                    source: taskDataAdapter,
                    pageable: true,
                    autoheight: true,
                    virtualmode: true,
                    sorttogglestates: 1,
                    enabletooltips: true,
                    sortable: true,
                    rendergridrows: function (args) {
                        return args.data;
                    },
                    columns: [
                      { text: "任务名称", datafield: "TaskName" },
                      { text: "提交人", datafield: "Name" },
                      { text: "提交日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                      { text: "操作", datafield: "TaskNodeId", cellsrenderer: taskCellsRenderer, width: 40, enabletooltips: false, sortable: false }
                    ]
                });

                ////我的合约
                //formatedData = "";
                //totalrecords = 0;
                //var contractSource =
                //{
                //    datafields:
                //    [
                //       { name: "SubId", type: "int" },
                //       { name: "ContractDate", type: "date" },
                //       { name: "ContractNo", type: "string" },
                //       { name: "OutContractNo", type: "string" },
                //       { name: "TradeDirectionName", type: "string" },
                //       { name: "InCorpName", type: "string" },
                //       { name: "OutCorpName", type: "string" },
                //       { name: "AssetName", type: "string" },
                //       { name: "ContractWeight", type: "string" },
                //       { name: "PriceModeName", type: "string" },
                //       { name: "StatusName", type: "string" }
                //    ],
                //    datatype: "json",
                //    sort: function () {
                //        $("#jqxGridContract").jqxGrid("updatebounddata", "sort");
                //    },
                //    beforeprocessing: function (data) {
                //        var returnData = {};
                //        totalrecords = data.count;
                //        returnData.totalrecords = data.count;
                //        returnData.records = data.data;
                //        return returnData;
                //    },
                //    type: "GET",
                //    sortcolumn: "cs.SubId",
                //    sortdirection: "desc",
                //    formatdata: function (data) {
                //        data.pagenum = data.pagenum || 0;
                //        data.pagesize = data.pagesize || 10;
                //        data.sortdatafield = data.sortdatafield || "cs.SubId";
                //        data.sortorder = data.sortorder || "desc";
                //        data.filterscount = data.filterscount || 0;
                //        formatedData = buildQueryString(data);
                //        return formatedData;
                //    },
                //    url: "Contract/Handler/SubListHandler.ashx"
                //};
                //var contractDataAdapter = new $.jqx.dataAdapter(contractSource, {
                //    contentType: "application/json; charset=utf-8",
                //    loadError: function (xhr, status, error) {
                //        alert(error);
                //    }
                //});
                //var contractCellsRenderer = function (row, columnfield, value, defaulthtml, columnproperties) {

                //    var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                //    cellHtml += "<a href=\"Contract/SubDetail.aspx?id=" + value + "\">明细</a>";
                //    cellHtml += "</div>";
                //    return cellHtml;
                //}
                //$("#jqxGridContract").jqxGrid(
                //{
                //    width: 500,
                //    source: contractDataAdapter,
                //    pageable: true,
                //    autoheight: true,
                //    virtualmode: true,
                //    sorttogglestates: 1,
                //    enabletooltips: true,
                //    sortable: true,
                //    rendergridrows: function (args) {
                //        return args.data;
                //    },
                //    columns: [
                //      { text: "签订日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                //      { text: "子合约号", datafield: "ContractNo" },
                //      { text: "购销方向", datafield: "TradeDirectionName" },
                //      { text: "对方公司", datafield: "OutCorpName" },
                //      { text: "操作", datafield: "SubId", cellsrenderer: contractCellsRenderer, width: 40, enabletooltips: false, sortable: false }
                //    ]
                //});

                ////可售库存
                //var stockSource =
                //{
                //    datafields:
                //    [
                //       { name: "StockId", type: "int" },
                //       { name: "StockDate", type: "date" },
                //       { name: "RefNo", type: "string" },
                //       { name: "CorpName", type: "string" },
                //       { name: "AssetName", type: "string" },
                //       { name: "StockWeight", type: "string" },
                //       { name: "BrandName", type: "string" },
                //       { name: "CustomsTypeName", type: "string" },
                //       { name: "StatusName", type: "string" }
                //    ],
                //    datatype: "json",
                //    sort: function () {
                //        $("#jqxGridSalesStockName").jqxGrid("updatebounddata", "sort");
                //    },
                //    beforeprocessing: function (data) {
                //        var returnData = {};
                //        totalrecords = data.count;
                //        returnData.totalrecords = data.count;
                //        returnData.records = data.data;
                //        return returnData;
                //    },
                //    type: "GET",
                //    sortcolumn: "sto.StockId",
                //    sortdirection: "desc",
                //    formatdata: function (data) {
                //        data.pagenum = data.pagenum || 0;
                //        data.pagesize = data.pagesize || 10;
                //        data.sortdatafield = data.sortdatafield || "sto.StockId";
                //        data.sortorder = data.sortorder || "desc";
                //        data.filterscount = data.filterscount || 0;
                //        formatedData = buildQueryString(data);
                //        return formatedData;
                //    },
                //    url: "WareHouse/Handler/StockListHandler.ashx?s=203"
                //};
                //var stockDataAdapter = new $.jqx.dataAdapter(stockSource, {
                //    contentType: "application/json; charset=utf-8",
                //    loadError: function (xhr, status, error) {
                //        alert(error);
                //    }
                //});
                //$("#jqxGridSalesStockName").jqxGrid(
                //{
                //    width: 500,
                //    source: stockDataAdapter,
                //    pageable: true,
                //    autoheight: true,
                //    enabletooltips: true,
                //    virtualmode: true,
                //    sorttogglestates: 1,
                //    sortable: true,
                //    rendergridrows: function (args) {
                //        return args.data;
                //    },
                //    columns: [
                //      { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                //      { text: "业务单号", datafield: "RefNo" },
                //      { text: "归属公司", datafield: "CorpName" },
                //      { text: "品种", datafield: "AssetName" },
                //      { text: "入库重量", datafield: "StockWeight" },
                //      { text: "报关状态", datafield: "CustomsTypeName" },
                //      { text: "库存状态", datafield: "StatusName" }
                //    ]
                //});

                //消息提醒
                var sourceWarm =
                {
                    datafields:
                    [
                       { name: "SmsId", type: "int" },
                       { name: "SmsHead", type: "string" },
                       { name: "SmsBody", type: "string" },
                       { name: "SmsRelTime", type: "date" },
                       { name: "SmsStatus", type: "int" },
                       { name: "SmsLevel", type: "int" },
                       { name: "TypeName", type: "string" },
                       { name: "ListUrl", type: "string" },
                       { name: "ViewUrl", type: "string" },
                       { name: "SourceId", type: "int" }
                    ],
                    datatype: "json",
                    sort: function () {
                        $("#jqxGridWarm").jqxGrid("updatebounddata", "sort");
                    },
                    beforeprocessing: function (data) {
                        var returnData = {};
                        totalrecords = data.count;
                        returnData.totalrecords = data.count;
                        returnData.records = data.data;
                        return returnData;
                    },
                    type: "GET",
                    sortcolumn: "s.SmsId",
                    sortdirection: "desc",
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 10;
                        data.sortdatafield = data.sortdatafield || "s.SmsId";
                        data.sortorder = data.sortorder || "desc";
                        data.filterscount = data.filterscount || 0;
                        formatedData = buildQueryString(data);
                        return formatedData;
                    },
                    url: "Message/Handler/MessageWarningListHandler.ashx"
                };
                var dataAdapterWarm = new $.jqx.dataAdapter(sourceWarm, {
                    contentType: "application/json; charset=utf-8",
                    loadError: function (xhr, status, error) {
                        alert(error);
                    }
                });
                $("#jqxGridWarm").jqxGrid(
                {
                    width: 500,
                    source: dataAdapterWarm,
                    pageable: true,
                    autoheight: true,
                    virtualmode: true,
                    sorttogglestates: 1,
                    sortable: true,
                    rendergridrows: function (args) {
                        return args.data;
                    },
                    columns: [
                      { text: "消息标题", datafield: "SmsHead" },
                      { text: "消息内容", datafield: "SmsBody" },
                      { text: "消息发布时间", datafield: "SmsRelTime", cellsformat: "yyyy-MM-dd" },
                      { text: "消息优先级", datafield: "SmsLevel" }
                    ]
                });

                $("#window1").jqxExpander({ width: 505 });
                //$("#window2").jqxExpander({ width: 505 });
                //$("#window3").jqxExpander({ width: 505 });
                $("#window4").jqxExpander({ width: 505 });

            });

    </script>

</head>
<body>

    <div class="SplitterDiv">
        <div style="float: left; margin: 0px 5px 5px 0px;">
            <div id="window1" style="width: 500px; float: left; margin: 0px 5px 5px 0px;">
                <div>&nbsp;&nbsp;我的任务</div>
                <div>
                    <div id="jqxTaskGrid" style="float: left;"></div>
                </div>
            </div>
            <%--<div id="window2" style="width: 500px; float: left; margin: 0px 5px 5px 0px;">
                <div>&nbsp;&nbsp;我的合约</div>
                <div>
                    <div id="jqxGridContract" style="float: left;"></div>
                </div>
            </div>--%>
        </div>

        <div style="float: left; margin: 0px 5px 5px 0px;">
            <%--<div id="window3" style="width: 500px; float: left; margin: 0px 5px 5px 0px;">
                <div>&nbsp;&nbsp;排货表</div>
                <div>
                    <div id="jqxGridSalesStockName" style="float: left;"></div>
                </div>
            </div>--%>
            <div id="window4" style="width: 500px; float: left; margin: 0px 5px 5px 0px;">
                <div>&nbsp;&nbsp;消息提醒</div>
                <div>
                    <div id="jqxGridWarm" style="float: left;"></div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
