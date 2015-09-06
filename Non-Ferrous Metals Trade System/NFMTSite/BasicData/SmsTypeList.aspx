<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTypeList.aspx.cs" Inherits="NFMTSite.BasicData.SmsTypeList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>消息类型列表</title>
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

            //类型名称
            $("#txbTypeName").jqxInput({ height: 22, width: 120 });

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 70 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            var typeName = $("#txbTypeName").val();
            var status = $("#ddlStatus").val();
            var url = "Handler/SmsTypeListHandler.ashx?n=" + typeName + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datafields:
                [
                   { name: "SmsTypeId", type: "int" },
                   { name: "TypeName", type: "string" },
                   { name: "ListUrl", type: "string" },
                   { name: "ViewUrl", type: "string" },
                   { name: "SmsTypeStatus", type: "int" },
                   { name: "StatusName", type: "string" }
                ],
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
                sortcolumn: "SmsTypeId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "SmsTypeId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"SmsTypeDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"SmsTypeUpdate.aspx?id=" + value + "\">修改</a>";
                //if (item.SmsTypeStatus > statusEnum.已作废 && item.SmsTypeStatus < statusEnum.待审核) {
                //    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"SmsTypeUpdate.aspx?id=" + value + "\">修改</a>";
                //}
                //else {
                //    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //}
                cellHtml += "</div>";
                return cellHtml;
            }

            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var gridLocalization = null;

            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                columnsresize: true,
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
                      { text: "类型名称", datafield: "TypeName" },
                      { text: "列表地址", datafield: "ListUrl" },
                      { text: "明细地址", datafield: "ViewUrl" },
                      { text: "类型状态", datafield: "StatusName" },
                      { text: "操作", datafield: "SmsTypeId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var typeName = $("#txbTypeName").val();
                var status = $("#ddlStatus").val();
                source.url = "Handler/SmsTypeListHandler.ashx?n=" + typeName + "&s=" + status;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
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
                    <span style="float: left;">类型名称：</span>
                    <input type="text" id="txbTypeName" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">类型状态：</span>
                    <div id="ddlStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="SmsTypeCreate.aspx" id="btnAdd">新建消息类型</a>
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
