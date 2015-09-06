<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractClauseList.aspx.cs" Inherits="NFMTSite.BasicData.ContractClauseList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约条款管理</title>
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

            $("#txbClauseName").jqxInput({ height: 22, width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });
            CreateStatusDropDownList("selStatus");

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            $("#btnSearch").on("click", function () {
                var clauseName = $("#txbClauseName").val();
                var clauseStatus = $("#selStatus").val();
                source.url = "Handler/ContractClauseListHandler.ashx?n=" + clauseName + "&s=" + clauseStatus;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
                $("#jqxGrid").jqxGrid("gotopage", 0);
            });

            var n = $("#txbClauseName").val();
            var s = $("#selStatus").val();

            var url = "Handler/ContractClauseListHandler.ashx?n=" + n + "&s=" + s;
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
                sortcolumn: "ClauseId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ClauseId";
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
                cellHtml += "<a target=\"_self\" href=\"ContractClauseView.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContractClauseUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxgrid").jqxGrid(
            {
                width: "99%",
                source: dataAdapter,
                columnsresize: true,
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
                  { text: "合约条款内容", datafield: "ClauseText", width: 700 },
                  { text: "合约条款英文内容", datafield: "ClauseEnText", width: 300 },
                  { text: "数据状态", datafield: "ClauseStatusName" },
                  { text: "操作", datafield: "ClauseId", cellsrenderer: cellsrenderer, width: 200, enabletooltips: false }
                ]
            });

            //$("#btnSearch").on("click", function () {
            //    alert("FD");
            //    source.url = "Handler/ContractClauseListHandler.ashx?n=" + $("#txbClauseName").val() + "&s=" +  $("#selStatus").val();
            //    $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            //});

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
                    <span>条款内容：</span>
                    <span>
                        <input type="text" id="txbClauseName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">条款状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="ContractClauseCreate.aspx" id="btnAdd">添加合约条款</a>
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
