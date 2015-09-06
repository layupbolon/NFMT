<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockLogList.aspx.cs" Inherits="NFMTSite.WareHouse.StockLogList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存流水查看</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            $("#jqxStockExpander").jqxExpander({ width: "98%" });

            var stockId = $("#hidStockId").val();
            var url = "Handler/StockLogListHandler.ashx?si=" + stockId;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "LogDate", type: "date" },
                   { name: "DetailName", type: "string" },
                   { name: "Name", type: "string" },
                   { name: "Memo", type: "string" }
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
                sortcolumn: "sl.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sl.StockLogId";
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
                return "";
                //"<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\"><a target=\"_self\" href=\"#?id=" + value + "\">相关明细</a></div>";
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
                  { text: "操作日期", datafield: "LogDate", cellsformat: "yyyy-MM-dd" },
                  { text: "操作类型", datafield: "DetailName" },
                  { text: "操作人", datafield: "Name" },
                  { text: "操作附言", datafield: "Memo" },
                  { text: "相关明细", datafield: "StockLogId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });
        });

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>入库日期：</strong>
                    <span runat="server" id="spnStockDate"></span>
                </li>
                <li>
                    <strong>业务单号：</strong>
                    <span runat="server" id="spnStockName"></span>
                </li>
                <li>
                    <strong>归属公司：</strong>
                    <span runat="server" id="spnOwnCorp"></span>
                </li>
                <li>
                    <strong>关内关外：</strong>
                    <span runat="server" id="spnCustomType"></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spnAsset"></span>
                </li>
                <li>
                    <strong>品牌：</strong>
                    <span runat="server" id="spnBrand"></span>
                </li>
                <li>
                    <strong>入库重量：</strong>
                    <span runat="server" id="spnStockAmount"></span>
                </li>
                <li>
                    <strong>库存类型：</strong>
                    <span runat="server" id="spnStockType"></span>
                </li>
                <li>
                    <strong>库存状态：</strong>
                    <span runat="server" id="spnStockStatus"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表<input type="hidden" id="hidStockId" runat="server" />
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
