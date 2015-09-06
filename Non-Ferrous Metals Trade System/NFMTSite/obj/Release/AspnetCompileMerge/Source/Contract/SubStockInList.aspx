<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubStockInList.aspx.cs" Inherits="NFMTSite.Contract.SubStockInList" %>
<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

            $("#txbStockName").jqxInput({ height: 22, width: 120 });
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnCreate").jqxButton({ height: 25, width: 120 });

            //绑定Grid            
            var url = "Handler/SubStockInListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "LogDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "CustomesType", type: "int" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "BrandName", type: "string" }, 
                   { name: "DPName", type: "string" }
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
           
            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                selectionmode: "checkbox",
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "入库日期", datafield: "LogDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "关内外", datafield: "CustomsTypeName" },
                  { text: "入库净重", datafield: "NetAmount" },
                  { text: "重量单位", datafield: "MUName" }
                ]
            });

            $("#btnSearch").click(function () {
                var stockName = $("#txbStockName").val();               
                var fromDate = $("#txbFromCreateDate").val();
                var toDate = $("#txbToCreateDate").val();
                source.url = "Handler/SubStockInListHandler.ashx?stockName=" + stockName + "&fromDate=" + fromDate + "&toDate=" + toDate;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnCreate").click(function () {                
                var rows = $("#jqxGrid").jqxGrid("getselectedrowindexes");               
                var logIds = "";
                for (i = 0; i < rows.length; i++) {
                    var item = dataAdapter.records[rows[i]]                    
                    if (i != 0) { logIds += ","; }
                    logIds += item.StockLogId;
                }

                document.location.href = "ContractInCreateByStocks.aspx?lgs=" + logIds;
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
                    <span style="float: left;">入库日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>                
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <input type="button" id="btnCreate" value="创建合约" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            未分配合约库存列表
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
