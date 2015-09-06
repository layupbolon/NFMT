<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriceConfirmList.aspx.cs" Inherits="NFMTSite.DoPrice.PriceConfirmList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价格确认单列表</title>
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

            //子合约号
            $("#txbSubNo").jqxInput({ height: 25, width: 120 });

            //价格确认状态
            CreateStatusDropDownList("ddlPriceConfirmStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 });

            //绑定Grid
            var subNo = $("#txbSubNo").val();
            var status = $("#ddlPriceConfirmStatus").val();
            var url = "Handler/PriceConfirmListHandler.ashx?sn=" + subNo + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "PriceConfirmId", type: "int" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "ContractAmount", type: "string" },
                   { name: "SubAmount", type: "string" },
                   { name: "RealityAmount", type: "string" },
                   { name: "PricingAvg", type: "string" },
                   { name: "PremiumAvg", type: "string" },
                   { name: "InterestAvg", type: "string" },
                   { name: "InterestBala", type: "string" },
                   { name: "SettlePrice", type: "string" },
                   { name: "SettleBala", type: "string" },
                   { name: "PricingDate", type: "date" },
                   { name: "TakeCorpName", type: "string" },
                   { name: "ContactPerson", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "PriceConfirmStatus", type: "int" }
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
                sortcolumn: "pc.PriceConfirmId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pc.PriceConfirmId";
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
                cellHtml += "<a target=\"_self\" href=\"PriceConfirmDetail.aspx?id=" + value + "\">查看</a>"
                if (item.PriceConfirmStatus > statusEnum.已关闭 && item.PriceConfirmStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PriceConfirmUpdate.aspx?id=" + value + "\">修改</a>"
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
                  { text: "我方公司", datafield: "InCorpName", width: 170 },
                  { text: "对方公司", datafield: "OutCorpName", width: 170 },
                  { text: "子合约号", datafield: "OutContractNo", width: 120 },
                  { text: "合约签订数量", datafield: "ContractAmount"},
                  { text: "子合约签订数量", datafield: "SubAmount" },
                  { text: "实际发货数量", datafield: "RealityAmount" },
                  //{ text: "期货点价均价", datafield: "PricingAvg", width: 120 },
                  //{ text: "升贴水均价", datafield: "PremiumAvg", width: 120 },
                  //{ text: "利息均价", datafield: "InterestAvg", width: 120 },
                  //{ text: "利息总额", datafield: "InterestBala", width: 120 },
                  { text: "结算单价", datafield: "SettlePrice", width: 130 },
                  { text: "结算总额", datafield: "SettleBala", width: 130 },
                  { text: "选价日期", datafield: "PricingDate", width: 90, cellsformat: "yyyy-MM-dd" },
                  //{ text: "提货单位", datafield: "TakeCorpName", width: 170 },
                  //{ text: "供方委托提货单位联系人", datafield: "ContactPerson", width: 165 },
                  //{ text: "备注", datafield: "Memo", width: 180 },
                  { text: "状态", datafield: "StatusName", width: 90 },
                  { text: "操作", datafield: "PriceConfirmId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var subNo = $("#txbSubNo").val();
                var status = $("#ddlPriceConfirmStatus").val();
                source.url = "Handler/PriceConfirmListHandler.ashx?sn=" + subNo + "&s=" + status;
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
                    <span style="float: left;">合约号：</span>
                    <input type="text" id="txbSubNo" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">价格确认状态：</span>
                    <div id="ddlPriceConfirmStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PriceConfirmContractList.aspx" id="btnAdd">新增价格确认单</a>
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
