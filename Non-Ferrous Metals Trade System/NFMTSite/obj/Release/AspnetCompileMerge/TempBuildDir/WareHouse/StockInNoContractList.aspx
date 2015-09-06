<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInNoContractList.aspx.cs" Inherits="NFMTSite.WareHouse.StockInNoContractList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约列表</title>
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

            $("#txbStockName").jqxInput({ height: 23, width: 120 });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            CreateStatusDropDownList("ddlStockInStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 120 });

            //绑定Grid            
            var url = "Handler/StockInNoContractListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockInId", type: "int" },
                   { name: "StockInDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "GrossWeight", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "StockInStatus", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" }
                ],
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
                sortcolumn: "si.StockInId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "si.StockInId";
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
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">"
                   + "<a target=\"_self\" href=\"StockInContractCreate.aspx?id=" + value + "\">分配</a>"
                   + "</div>";
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
                  { text: "入库日期", datafield: "StockInDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "入库毛重", datafield: "GrossWeight" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "入库", datafield: "StatusName" },
                  { text: "操作", datafield: "StockInId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var stockName = $("#txbStockName").val();
                var status = $("#ddlStockInStatus").val();
                var fromDate = $("#txbFromCreateDate").val();
                var toDate = $("#txbToCreateDate").val();
                source.url = "Handler/StockInNoContractListHandler.ashx?stockName=" + stockName + "&status=" + status + "&fromDate=" + fromDate + "&toDate=" + toDate;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>业务单号：</span>
                    <span>
                        <input type="text" id="txbStockName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">入库状态：</span>
                    <div id="ddlStockInStatus" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">入库日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
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
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
