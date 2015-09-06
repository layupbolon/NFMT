<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProducerList.aspx.cs" Inherits="NFMTSite.BasicData.ProducerList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生产商管理</title>
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

            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });
            $("#btnSearch").jqxButton({ height: 25 });

            $("#txbProducerName").jqxInput({ height: 22, width: 120 });

            CreateStatusDropDownList("selProducerStatus");

            //绑定 地区
            var url = "Handler/AreaDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "AreaId" }, { name: "AreaName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ProducerArea").jqxComboBox({ source: dataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 120, height: 25 });

            var producerName = $("#txbProducerName").val();
            var producerStatus = $("#selProducerStatus").val();
            var producerArea = $("#ProducerArea").val();
            var url = "Handler/ProducerListHandler.ashx?producerName=" + producerName + "&producerStatus=" + producerStatus + "&producerArea=" + producerArea;
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
                sortcolumn: "ProducerId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ProducerId";
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
            //alert(dataAdapter);
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"ProducerDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ProducerUpdate.aspx?id=" + value + "\">修改</a>";
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
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "生产商名称", datafield: "ProducerName" },
                  { text: "生产商全称", datafield: "ProducerFullName" },
                  { text: "生产商简称", datafield: "ProducerShort" },
                  { text: "生产商地区", datafield: "AreaName" },
                  { text: "生产商状态", datafield: "StatusName" },
                  { text: "操作", datafield: "ProducerId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            function LoadData() {
                var producerName = $("#txbProducerName").val();
                var producerStatus = $("#selProducerStatus").val();
                var producerArea = $("#ProducerArea").val();
                source.url = "Handler/ProducerListHandler.ashx?producerName=" + producerName + "&producerStatus=" + producerStatus + "&producerArea=" + producerArea;
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
             <input type="hidden" id="hidCapitalType" runat="server" />
            <input type="hidden" id="hidStyleId" runat="server" />
        </div>
        <div id="SearchDiv">
            <ul>
                <li>
                    <span>生产商名称：</span>
                    <span>
                        <input type="text" id="txbProducerName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">地区：</span>
                    <div id="ProducerArea" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">生产商状态：</span>
                    <div id="selProducerStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="ProducerGreate.aspx" id="btnAdd">新增生厂商</a>
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
