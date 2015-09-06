<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuturesPriceList.aspx.cs" Inherits="NFMTSite.BasicData.FuturesPriceList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>期货结算价管理</title>
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

            $("#tradeDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#tradeDate").jqxDateTimeInput({ value: tempDate });
            $("#deliverDate").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            var tradeCode = $("#txbTradeCode").val();
            var tradeCode = $("#txbTradeCode").val();
            var tradeDate = $("#tradeDate").val();
            var url = "Handler/FuturesPriceListHandler.ashx?tradeCode=" + tradeCode + "&deliverDate=" + deliverDate + "&tradeDate=" + tradeDate;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
         [

         { name: "TradeDate", type: "date" },
         { name: "TradeCode", type: "string" },
        { name: "DeliverDate", type: "date" },
            { name: "SettlePrice", type: "decimal" },
         ],
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
                sortcolumn: "FPId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "FPId";
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

                return "&nbsp;&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"FuturesPriceDetail.aspx?id=" + value + "\">查看</a>&nbsp;&nbsp;&nbsp;&nbsp;<a target=\"_self\"href=\"FuturesPriceUpdate.aspx?id=" + value + "\">修改</a>";
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

                  { text: "交易日", datafield: "TradeDate", cellsformat: "yyyy-MM-dd" },
                  { text: "交易代码", datafield: "TradeCode" },
                  { text: "交割日", datafield: "DeliverDate", cellsformat: "yyyy-MM-dd" },
                  { text: "结算价", datafield: "SettlePrice" },
                  { text: "操作", datafield: "FPId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });
            //$("#jqxGrid").jqxGrid('autoresizecolumns');


            function LoadData() {
                var tradeCode = $("#txbTradeCode").val();
                var deliverDate = $("#deliverDate").val();
                var tradeDate = $("#tradeDate").val();
                source.url = "Handler/FuturesPriceListHandler.ashx?tradeCode=" + tradeCode + "&deliverDate=" + deliverDate + "&rateDate=" + rateDate;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid('updatebounddata', 'cells');
            }

            $("#btnSearch").click(LoadData);

            $("#btnAdd").click(function () {
                window.document.location = "RateGreate.aspx";
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
                    <span style="width: 15%; text-align: right;">
                        <div id="tradeDate" runat="server" style="float: right;"></div>
                        交易日：</span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交易代码：
                                    <input type="text" runat="server" id="txbTradeCode" /></span>

                </li>
                <li>

                    <span style="width: 15%; text-align: right;">
                        <div id="deliverDate" runat="server" style="float: right;"></div>
                        交割日：</span>
                </li>
                <li>

                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="RateGreate.aspx" id="btnAdd">新增结算价</a>
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
