<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInList.aspx.cs" Inherits="NFMTSite.WareHouse.StockInList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>入库登记列表</title>
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
            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            
            $("#btnDirectInReady").jqxLinkButton({ height: 25, width: 120 });
            $("#btnDirectIn").jqxLinkButton({ height: 25, width: 120 });
            $("#btnContractIn").jqxLinkButton({ height: 25, width: 120 });

            //质押申请状态
            CreateStatusDropDownList("ddlStockInStatus");

            //绑定Grid            
            var url = "Handler/StockInListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockInId", type: "int" },
                   { name: "StockInDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "RefId", type: "string" },
                   { name: "StockInStatus", type: "int" },
                   { name: "DPName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
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
                sortcolumn: "st.StockInId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "st.StockInId";
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

                var item = $("#jqxGrid").jqxGrid("getrowdata", row);

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                if (item.RefId == undefined || item.RefId == 0) {
                    cellHtml += "<a target=\"_self\" href=\"StockInDetail.aspx?id=" + value + "\">查看</a>";
                }
                else {
                    cellHtml += "<a target=\"_self\" href=\"StockInConDetail.aspx?id=" + value + "\">查看</a>";
                }

                if (item.StockInStatus > statusEnum.已关闭 && item.StockInStatus < statusEnum.待审核) {
                    if (item.RefId == undefined || item.RefId == 0) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"StockInUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"StockInConUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                cellHtml += "</div>";
                return cellHtml;
                
            }

            var subNoCellRenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                if (value == "" || value == undefined ) {
                    var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                    var stockInId = item.StockInId;
                    var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                    cellHtml += "&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"StockInContractCreate.aspx?id=" + stockInId + "\">分配</a>";
                    cellHtml += "</div>";
                    return cellHtml;
                }
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
                  { text: "关联合约号", datafield: "ContractNo" },
                  { text: "关联子合约号", datafield: "SubNo", cellsrenderer: subNoCellRenderer },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "入库毛重", datafield: "GrossAmount" },
                  { text: "入库净重", datafield: "NetAmount" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "入库状态", datafield: "StatusName" },
                  { text: "操作", datafield: "StockInId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var stockName = $("#txbStockName").val();
                var status = $("#ddlStockInStatus").val();
                var fromDate = $("#txbFromCreateDate").val();
                var toDate = $("#txbToCreateDate").val();
                source.url = "Handler/StockInListHandler.ashx?stockName=" + stockName + "&status=" + status + "&fromDate=" + fromDate + "&toDate=" + toDate;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
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
                    <span>业务单号：</span>
                    <span>
                        <input type="text" id="txbStockName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">入库日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">入库状态：</span>
                    <div id="ddlStockInStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="StockInDirectReady.aspx" id="btnDirectInReady">预入库</a>
                    <a target="_self" runat="server" href="StockInDirect.aspx" id="btnDirectIn">直接入库</a>
                    <a target="_self" runat="server" href="StockInByContract.aspx" id="btnContractIn">合约入库</a>
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
