<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopLossList.aspx.cs" Inherits="NFMTSite.DoPrice.StopLossList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>止损</title>
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

            //止损人, autoDropDownHeight: true 
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlStopLosser").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25});

            //止损品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetAuthHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssetId").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25 });

            //止损期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true });

            //止损状态
            CreateStatusDropDownList("ddlStopLossStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var person = $("#ddlStopLosser").val();
            var assetId = $("#ddlAssetId").val();
            var exchangeId = $("#ddlExchangeId").val();
            var status = $("#ddlStopLossStatus").val();
            var url = "Handler/StopLossListHandler.ashx?p=" + person + "&a=" + assetId + "&e=" + exchangeId + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StopLossId", type: "int" },
                   { name: "DetailRow", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "ExchangeName", type: "string" },
                   { name: "TradeCode", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "StopLossWeight", type: "string" },
                   { name: "AvgPrice", type: "string" },
                   { name: "PricingTime", type: "date" },
                   { name: "StatusName", type: "string" },
                   { name: "StopLossStatus", type: "int" }
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
                sortcolumn: "sl.StopLossId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sl.StopLossId";
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

                if (item.DetailRow == 0) {
                    cellHtml += "<a target=\"_self\" href=\"StopLossContractDetail.aspx?id=" + value + "\">查看</a>";
                }
                else {
                    cellHtml += "<a target=\"_self\" href=\"StopLossDetail.aspx?id=" + value + "\">查看</a>";
                }

                if (item.StopLossStatus > statusEnum.已关闭 && item.StopLossStatus < statusEnum.待审核) {
                    if (item.DetailRow == 0) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"StopLossContractUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"StopLossUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
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
                  { text: "止损人", datafield: "Name" },
                  { text: "止损期货市场", datafield: "ExchangeName" },
                  { text: "止损期货合约", datafield: "TradeCode" },
                  { text: "止损品种", datafield: "AssetName" },
                  { text: "止损重量", datafield: "StopLossWeight" },
                  { text: "止损均价", datafield: "AvgPrice" },
                  { text: "止损时间", datafield: "PricingTime", cellsformat: "yyyy-MM-dd" },
                  { text: "止损状态", datafield: "StatusName" },
                  { text: "操作", datafield: "StopLossId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var person = $("#ddlStopLosser").val();
                var assetId = $("#ddlAssetId").val();
                var exchangeId = $("#ddlExchangeId").val();
                var status = $("#ddlStopLossStatus").val();
                source.url = "Handler/StopLossListHandler.ashx?p=" + person + "&a=" + assetId + "&e=" + exchangeId + "&s=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>

</head>
<body>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
                         <input type="hidden" id="hidBDStyleId" runat="server" />
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">止损人：</span>
                    <div id="ddlStopLosser" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">止损品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">止损期货市场：</span>
                    <div id="ddlExchangeId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">止损状态：</span>
                    <div id="ddlStopLossStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="StopLossCanApplyList.aspx" id="btnAdd">止损</a>
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
