<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeList.aspx.cs" Inherits="NFMTSite.BasicData.ExchangeList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易所管理</title>
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

            $("#txbExchangeName").jqxInput({ height: 22, width: 120 });
            $("#txbExchangeCode").jqxInput({ height: 22, width: 120 });

            CreateStatusDropDownList("selExchangeStatus");

            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });
            $("#btnSearch").jqxButton({ height: 25, width: 70 });

            var exchangeName = $("#txbExchangeName").val();
            var exchangeStatus = $("#selExchangeStatus").val();
            var exchangeCode = $("#txbExchangeCode").val();
            var url = "Handler/ExchangeListHandler.ashx?exchangeName=" + exchangeName + "&exchangeStatus=" + exchangeStatus + "&exchangeCode=" + exchangeCode;
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
                sortcolumn: "ExchangeId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ExchangeId";
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
                cellHtml += "<a target=\"_self\" href=\"ExchangeDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ExchangeUpdate.aspx?id=" + value + "\">修改</a>";
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
                  { text: "交易所名称", datafield: "ExchangeName" },
                  { text: "交易所代码", datafield: "ExchangeCode" },
                  { text: "交易所状态", datafield: "StatusName" },
                  { text: "操作", datafield: "ExchangeId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });


            function LoadData() {
                var exchangeName = $("#txbExchangeName").val();
                var exchangeStatus = $("#selExchangeStatus").val();
                var exchangeCode = $("#txbExchangeCode").val();
                source.url = "Handler/ExchangeListHandler.ashx?exchangeName=" + exchangeName + "&exchangeStatus=" + exchangeStatus + "&exchangeCode=" + exchangeCode;
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
                    <span>交易所名称：</span>
                    <span>
                        <input type="text" id="txbExchangeName" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">交易所代码：</span>
                    <input type="text" id="txbExchangeCode" />
                </li>
                <li>
                    <span style="float: left;">交易所状态：</span>
                    <div id="selExchangeStatus" style="float: left;" />
                </li>
                <li>
                    <div id="buttons">
                        <input type="button" id="btnSearch" value="查询" />
                        <a target="_self" runat="server" href="ExchangeCreate.aspx" id="btnAdd">新增交易所</a>
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
