<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BDStyleList.aspx.cs" Inherits="NFMTSite.BasicData.BDStyleList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>类型列表</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });
            $("#txbStyleName").jqxInput({ height: 22, width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });

            CreateStatusDropDownList("selStatus");

            bindGrid();

            $("#btnSearch").on("click", function () { bindGrid(); });

            function bindGrid() {

                var n = $("#txbStyleName").val();
                var s = $("#selStatus").val();

                var url = "Handler/BDStyleListHandler.ashx?n=" + n + "&s=" + s;
                var formatedData = "";
                var totalrecords = 0;

                var source =
                {
                    datatype: "json",
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
                    sortcolumn: "BDStyleId",
                    sortdirection: "desc",
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 10;
                        data.sortdatafield = data.sortdatafield || "BDStyleId";
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
                    var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                    cellHtml += "<a target=\"_self\" href=\"BDStyleDtlList.aspx?id=" + value + "\">明细</a>";
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
                    rendergridrows: function (args) {
                        return args.data;
                    },
                    columns: [
                      { text: "类型名称", datafield: "BDStyleName" },
                      { text: "类型编号", datafield: "BDStyleCode" },
                      { text: "数据状态", datafield: "BDStyleStatusName" },
                      { text: "查看类型明细", datafield: "BDStyleId", cellsrenderer: cellsrenderer, width: 100 }
                    ]
                });

                $("#jqxgrid").jqxGrid("gotopage", 0);
            }
        });
    </script>
</head>
<body>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>类型名称：</span>
                    <span>
                        <input type="text" id="txbStyleName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">类型状态：</span>
                    <div id="selStatus" style="float: left;" />
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
        <div style="height: 500px;">
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
