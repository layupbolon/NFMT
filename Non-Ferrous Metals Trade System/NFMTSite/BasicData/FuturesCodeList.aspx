<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuturesCodeList.aspx.cs" Inherits="NFMTSite.BasicData.FuturesCodeList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>期货合约管理</title>
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

            $("#firstTradeDate").jqxDateTimeInput({ width: 120, height: 25, formatString: "yyyy-MM-dd" });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#firstTradeDate").jqxDateTimeInput({ value: tempDate });
            $("#lastTradeDate").jqxDateTimeInput({ width: 120, height: 25, formatString: "yyyy-MM-dd" });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            CreateStatusDropDownList("selFuturesCodeStatus");

            //绑定 交易所
            var exchangeUrl = "Handler/ExChangeDDLHandler.ashx";
            var exchangeSource = { datatype: "json", url: exchangeUrl, async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#Exchage").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 120, autoDropDownHeight: true });

            var url = "Handler/FuturesCodeListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                 [
                    { name: "FuturesCodeId", type: "int" },
                    { name: "ExchangeName", type: "string" },
                    { name: "AssetName", type: "string" },
                    { name: "StatusName", type: "string" },
                    { name: "CurrencyName", type: "string" },
                    { name: "MUName", type: "string" },
                    { name: "CodeSize", type: "string" },
                    { name: "TradeCode", type: "string" },
                    { name: "FirstTradeDate", type: "date" },
                    { name: "LastTradeDate", type: "date" }
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
                sortcolumn: "FuturesCodeId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "FuturesCodeId";
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
                cellHtml += "<a target=\"_self\" href=\"FuturesCodeDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"FuturesCodeUpdate.aspx?id=" + value + "\">修改</a>";
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
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "交易代码", datafield: "TradeCode" },
                  { text: "交易所", datafield: "ExchangeName" },
                  { text: "交易起始日期", datafield: "FirstTradeDate", cellsformat: "yyyy-MM-dd" },
                  { text: "交易终止日期", datafield: "LastTradeDate", cellsformat: "yyyy-MM-dd" },
                  { text: "合约规模", datafield: "CodeSize" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "合约状态", datafield: "StatusName" },
                  { text: "操作", datafield: "FuturesCodeId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });
            //$("#jqxGrid").jqxGrid('autoresizecolumns');

            function LoadData() {
                var exchageId = $("#Exchage").val();
                var firstTradeDate = $("#firstTradeDate").jqxDateTimeInput("getText");
                var lastTradeDate = $("#lastTradeDate").jqxDateTimeInput("getText");
                var selFuturesCodeStatus = $("#selFuturesCodeStatus").val();
                source.url = "Handler/FuturesCodeListHandler.ashx?exchageId=" + exchageId + "&firstTradeDate=" + firstTradeDate + "&lastTradeDate=" + lastTradeDate + "&selFuturesCodeStatus=" + selFuturesCodeStatus;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid('updatebounddata', 'rows');
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
                    <span style="float: left;">交易所：</span>
                    <div id="Exchage" style="float: left;" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交易起始日期：</span>
                    <div id="firstTradeDate" runat="server" style="float: right;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交易终止日期：</span>
                    <div id="lastTradeDate" runat="server" style="float: right;"></div>
                </li>
                <li>
                    <span style="float: left;">合约状态：</span>
                    <div id="selFuturesCodeStatus" style="float: left;" />
                </li>
                <li>
                    <div id="buttons">
                        <input type="button" id="btnSearch" value="查询" />
                        <a target="_self" runat="server" href="FuturesCodeCreate.aspx" id="btnAdd">新增期货合约</a>
                    </div>
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
