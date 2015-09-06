<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeasureUnitList.aspx.cs" Inherits="NFMTSite.BasicData.MeasureUnitList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>计量单位管理</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script src="../js/Utility.js"></script>
    <script src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            CreateStatusDropDownList("txbMUStatus");

            $("#txbMUName").jqxInput({ height: 22, width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            var muName = $("#txbMUName").val();
            var status = $("#txbMUStatus").val();
            var url = "Handler/MUListHandler.ashx?k=" + muName + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "MUId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "MUId";
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
                cellHtml += "<a target=\"_self\" href=\"MeasureUnitDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"MeasureUnitUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxGrid").jqxGrid(
            {
                width: "99%",
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
                  { text: "计量单位名称", datafield: "MUName" },
                  { text: "基本单位名称", datafield: "BaseName" },
                  { text: "基本单位转换率", datafield: "TransformRate" },
                  { text: "单位状态", datafield: "MUStatusName" },
                  { text: "操作", datafield: "MUId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });
            //$("#jqxGrid").jqxGrid('autoresizecolumns');

            function LoadData() {

                var muName = $("#txbMUName").val();
                var status = $("#txbMUStatus").val();
                var muid = $("#hidMUId").val();

                source.url = "Handler/MUListHandler.ashx?k=" + muName + "&s=" + status;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid('updatebounddata', 'rows');
            }

            $("#btnSearch").click(LoadData);

            $("#btnAdd").click(function () {
                window.document.location = "MeasureUnitCreate.aspx";
            });
        });
    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div id="SearchDiv">
            <ul>
                <li>
                    <input type="hidden" runat="server" id="hidMUId" />
                    <span>计量单位名称：</span>
                    <span>
                        <input type="text" id="txbMUName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">单位状态：</span>
                    <div id="txbMUStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="MeasureUnitCreate.aspx" id="btnAdd">添加计量单位</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div style="height: 500px;">
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
