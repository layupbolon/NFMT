<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaList.aspx.cs" Inherits="NFMTSite.BasicData.AreaList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>区域管理</title>
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

            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });

            //地区名称
            $("#txbAreaName").jqxInput({ height: 22, width: 120 });

            //地区状态
            CreateStatusDropDownList("selAreaStatus");

            var areaName = $("#txbAreaName").val();
            var areaStatus = $("#selAreaStatus").val();
            var url = "Handler/AreaListHandler.ashx?k=" + areaName + "&s=" + areaStatus;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "a.AreaId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "a.AreaId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url

            };
            //alert(source);
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"AreaDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"AreaUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxGrid").jqxGrid(
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
                  { text: "地区名称", datafield: "AreaName" },
                  { text: "地区全名", datafield: "AreaFullName" },
                  { text: "地区名缩写", datafield: "AreaShort" },
                  { text: "电话区号", datafield: "AreaCode" },
                  { text: "邮政编号", datafield: "AreaZip" },
                  { text: "所属区域", datafield: "atAreaName" },
                  { text: "地区状态", datafield: "StatusName" },
                  { text: "操作", datafield: "AreaId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });



            function LoadData() {

                var areaName = $("#txbAreaName").val();
                var areaStatus = $("#selAreaStatus").val();
                source.url = "Handler/AreaListHandler.ashx?k=" + areaName + "&s=" + areaStatus;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            }

            $("#btnSearch").click(LoadData);

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
                    <span>地区名称：</span>
                    <span>
                        <input type="text" id="txbAreaName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">地区状态：</span>
                    <div id="selAreaStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="AreaGreate.aspx" id="btnAdd">添加地区</a>
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
