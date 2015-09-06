<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingRepoApplyCanRepoPledgeList.aspx.cs" Inherits="NFMTSite.Financing.FinancingRepoApplyCanRepoPledgeList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>可赎回质押申请单列表</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });

            //质押申请单号
            $("#txbPledgeApplyNo").jqxInput({ height: 25, width: 120 });

            //融资银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlFinBank").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 120, height: 25 });

            //业务单号
            $("#txbRefNo").jqxInput({ height: 25, width: 120 });
            ////融资品种
            //var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            //var ddlAssetIdsource = { datatype: "json", url: ddlAssetIdurl, async: false };
            //var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            //$("#ddlFinAsset").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 120, height: 25 });

            //日期
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            ////状态
            //CreateStatusDropDownList("ddlPledgeApplyStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 120 });

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var pledgeApplyNo = $("#txbPledgeApplyNo").val();
            var bank = $("#ddlFinBank").val() == "" ? 0 : $("#ddlFinBank").val();
            //var asset = $("#ddlFinAsset").val() == "" ? 0 : $("#ddlFinAsset").val();
            var asset = 0;
            var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
            var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
            //var status = $("#ddlPledgeApplyStatus").val() == "" ? 0 : $("#ddlPledgeApplyStatus").val();
            var refNo = $("#txbRefNo").val();

            var url = "Handler/FinancingPledgeApplyListForRepoCreateHandler.ashx?fromDate=" + fromDate + "&toDate=" + toDate + "&bankId=" + bank + "&assetId=" + asset + "&status=" + statusEnum.已生效 + "&paNo=" + pledgeApplyNo + "&refNo=" + refNo;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "PledgeApplyId", type: "int" },
                   { name: "PledgeApplyNo", type: "string" },
                   { name: "ApplyTime", type: "date", cellsformat: "yyyy-MM-dd" },
                   { name: "BankName", type: "string" },
                   { name: "AccountNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "SwitchBack", type: "string" },
                   { name: "SumNetAmount", type: "number" },
                   { name: "SumHands", type: "int" },
                   { name: "MUName", type: "string" },
                   { name: "PledgeApplyStatus", type: "int" },
                   { name: "StatusName", type: "string" }
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
                sortcolumn: "pa.PledgeApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pa.PledgeApplyId";
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
                var item = $("#jqxgrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"FinancingRepoApplyCreate.aspx?pledgeApplyId=" + value + "\">赎回</a>"
                cellHtml += "</a>";
                cellHtml += "</div>";

                return cellHtml;
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
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "质押申请单号", datafield: "PledgeApplyNo", width: "10%" },
                  { text: "日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd", width: "10%" },
                  { text: "融资银行", datafield: "BankName", width: "8%" },
                  { text: "融资银行账号", datafield: "AccountNo", width: "9%" },
                  { text: "融资货物", datafield: "AssetName", width: "8%" },
                  { text: "头寸是否转回", datafield: "SwitchBack", width: "10%" },
                  { text: "净重合计", datafield: "SumNetAmount", width: "10%" },
                  { text: "手数合计", datafield: "SumHands", width: "10%" },
                  { text: "单位", datafield: "MUName", width: "5%" },
                  { text: "状态", datafield: "StatusName", width: "10%" },
                  { text: "操作", datafield: "PledgeApplyId", cellsrenderer: cellsrenderer, width: "10%", enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var pledgeApplyNo = $("#txbPledgeApplyNo").val();
                var bank = $("#ddlFinBank").val() == "" ? 0 : $("#ddlFinBank").val();
                //var asset = $("#ddlFinAsset").val() == "" ? 0 : $("#ddlFinAsset").val();
                var asset = 0;
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                //var status = $("#ddlPledgeApplyStatus").val() == "" ? 0 : $("#ddlPledgeApplyStatus").val();
                var refNo = $("#txbRefNo").val();

                source.url = "Handler/FinancingPledgeApplyListForRepoCreateHandler.ashx?fromDate=" + fromDate + "&toDate=" + toDate + "&bankId=" + bank + "&assetId=" + asset + "&status=" + statusEnum.已生效 + "&paNo=" + pledgeApplyNo + "&refNo=" + refNo;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });
        });

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <div id="jqxExpander" style="float: left; margin: 0 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>质押申请单号：</span>
                    <input type="text" id="txbPledgeApplyNo" style="float: right;"/>
                </li>
                <li>
                    <span>业务单号：</span>
                    <input type="text" id="txbRefNo" style="float: right;"/>
                </li>
                <li>
                    <span>融资银行：</span>
                    <div id="ddlFinBank" style="float: right;"></div>
                </li>
                <%--<li>
                    <span>融资货物：</span>
                    <div id="ddlFinAsset" style="float: right;"></div>
                </li>--%>
                <li>
                    <span style="float: left;">日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <%--<li>
                    <span style="float: left;">状态：</span>
                    <div id="ddlPledgeApplyStatus" style="float: left;"></div>
                </li>--%>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
