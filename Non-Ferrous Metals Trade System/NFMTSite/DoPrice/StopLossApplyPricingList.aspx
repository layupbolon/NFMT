<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopLossApplyPricingList.aspx.cs" Inherits="NFMTSite.DoPrice.StopLossApplyPricingList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价列表</title>
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

            //点价人, autoDropDownHeight: true
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlPricinger").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25 });

            //点价品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetAuthHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssetId").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25 });

            //点价期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true });

            //子合约号
            $("#txbSubNo").jqxInput({ height: 25, width: 120 });

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });

            //绑定Grid
            var url = "Handler/StopLossApplyPricingListHandler.ashx?s=" + statusEnum.已完成;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "PricingId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "DetailRow", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "ExchangeName", type: "string" },
                   { name: "TradeCode", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "PricingWeight", type: "string" },
                   { name: "AvgPrice", type: "string" },
                   { name: "PricingTime", type: "date" },
                   { name: "StatusName", type: "string" },
                   { name: "PricingStatus", type: "int" }
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
                sortcolumn: "p.PricingId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "p.PricingId";
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
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"StopLossApplyCreate.aspx?pricingId=" + value + "\">止损申请</a>"
                cellHtml += "</a>";
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
                  { text: "点价人", datafield: "Name" },
                  { text: "合约号", datafield: "ContractNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "点价期货市场", datafield: "ExchangeName" },
                  { text: "点价期货合约", datafield: "TradeCode" },
                  { text: "点价品种", datafield: "AssetName" },
                  { text: "点价重量", datafield: "PricingWeight" },
                  { text: "点价均价", datafield: "AvgPrice" },
                  { text: "点价时间", datafield: "PricingTime", cellsformat: "yyyy-MM-dd" },
                  //{ text: "点价状态", datafield: "StatusName" },
                  { text: "操作", datafield: "PricingId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var person = $("#ddlPricinger").val();
                var assetId = $("#ddlAssetId").val();
                var exchangeId = $("#ddlExchangeId").val();
                var subNo = $("#txbSubNo").val();
                source.url = "Handler/StopLossApplyPricingListHandler.ashx?p=" + person + "&a=" + assetId + "&e=" + exchangeId + "&s=" + statusEnum.已完成 + "&subNo=" + subNo;
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
                         <input type="hidden" id="hidBDStyleId" runat="server" />
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">点价人：</span>
                    <div id="ddlPricinger" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">子合约号：</span>
                    <input type="text" id="txbSubNo" style="float: left;"/>
                </li>
                <li>
                    <span style="float: left;">点价品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">点价期货市场：</span>
                    <div id="ddlExchangeId" style="float: left;"></div>
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
