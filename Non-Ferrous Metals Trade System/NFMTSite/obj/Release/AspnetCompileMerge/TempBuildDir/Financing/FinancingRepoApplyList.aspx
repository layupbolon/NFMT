<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingRepoApplyList.aspx.cs" Inherits="NFMTSite.Financing.FinancingRepoApplyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>赎回申请单列表</title>
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

            //赎回申请单号
            $("#txbRepoApplyIdNo").jqxInput({ height: 25, width: 120 });

            //业务单号
            $("#txbrefNo").jqxInput({ height: 25, width: 120 });

            //质押申请单号
            $("#txbPledgeApplyNo").jqxInput({ height: 25, width: 120 });

            //日期
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //状态
            CreateStatusDropDownList("ddlRepoApplyStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 });

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var repoApplyIdNo = $("#txbRepoApplyIdNo").val();
            var pledgeApplyNo = $("#txbPledgeApplyNo").val();
            var status = $("#ddlRepoApplyStatus").val() === "" ? 0 : $("#ddlRepoApplyStatus").val();
            var refNo = $("#txbrefNo").val();
            var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
            var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");

            var url = "Handler/FinancingRepoApplyListHandler.ashx?status=" + status + "&paNo=" + pledgeApplyNo + "&reNo=" + repoApplyIdNo + "&refNo=" + refNo + "&fromDate=" + fromDate + "&toDate=" + toDate;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "RepoApplyId", type: "int" },
                   { name: "RepoApplyIdNo", type: "string" },
                   { name: "PledgeApplyNo", type: "string" },
                   { name: "ApplyTime", type: "date", cellsformat: "yyyy-MM-dd" },
                   { name: "CreateTime", type: "date", cellsformat: "yyyy-MM-dd" },
                   { name: "BankName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "PledgeSumNetAmount", type: "number" },
                   { name: "PledgeSumHands", type: "int" },
                   { name: "RepoSumNetAmount", type: "number" },
                   { name: "RepoSumHands", type: "int" },
                   { name: "RepoApplyStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "NodeName", type: "string" }
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
                sortcolumn: "ra.RepoApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ra.RepoApplyId";
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
            var cellsrenderer = function (row, columnfield, value) {
                var item = $("#jqxgrid").jqxGrid("getrowdata", row);

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";

                cellHtml += "<a target=\"_self\" href=\"FinancingRepoApplyDetail.aspx?id=" + value + "\">查看</a>";

                if (item.RepoApplyStatus > statusEnum.已关闭 && item.RepoApplyStatus < statusEnum.待审核 && "<%=CanUpdate%>".toLowerCase() === "true") {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"FinancingRepoApplyUpdate.aspx?id=" + value + "\">修改</a>";
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
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
                  { text: "赎回申请单号", datafield: "RepoApplyIdNo", width: "11%" },
                  { text: "日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd", width: "8%" },
                  { text: "赎回净重合计", datafield: "RepoSumNetAmount", width: "8%" },
                  { text: "赎回手数合计", datafield: "RepoSumHands", width: "8%" },                  
                  { text: "质押申请单号", datafield: "PledgeApplyNo", width: "11%" },
                  //{ text: "质押日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd", width: "10%" },
                  { text: "融资银行", datafield: "BankName", width: "7%" },
                  //{ text: "融资银行账号", datafield: "AccountNo", width: 180 },
                  { text: "融资货物", datafield: "AssetName", width: "7%" },
                  { text: "质押净重合计", datafield: "PledgeSumNetAmount", width: "8%" },
                  { text: "质押手数合计", datafield: "PledgeSumHands", width: "8%" },
                  { text: "审核环节", datafield: "NodeName" },
                  { text: "状态", datafield: "StatusName", width: "7%" },
                  { text: "操作", datafield: "RepoApplyId", cellsrenderer: cellsrenderer, width: "7%", enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var repoApplyIdNo = $("#txbRepoApplyIdNo").val();
                var pledgeApplyNo = $("#txbPledgeApplyNo").val();
                var status = $("#ddlRepoApplyStatus").val() === "" ? 0 : $("#ddlRepoApplyStatus").val();
                var refNo = $("#txbrefNo").val();
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");

                source.url = "Handler/FinancingRepoApplyListHandler.ashx?status=" + status + "&paNo=" + pledgeApplyNo + "&reNo=" + repoApplyIdNo + "&refNo=" + refNo + "&fromDate=" + fromDate + "&toDate=" + toDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExport").jqxButton({ height: 25, width: 120 });
            $("#btnExport").click(function () {
                var repoApplyIdNo = $("#txbRepoApplyIdNo").val();
                var pledgeApplyNo = $("#txbPledgeApplyNo").val();
                var status = $("#ddlRepoApplyStatus").val() === "" ? 0 : $("#ddlRepoApplyStatus").val();
                var refNo = $("#txbrefNo").val();
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");

                document.location.href = "../Report/ExportExcel.aspx?rt=358&status=" + status + "&paNo=" + pledgeApplyNo + "&reNo=" + repoApplyIdNo + "&refNo=" + refNo + "&fromDate=" + fromDate + "&toDate=" + toDate;
            });
        });

    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>赎回申请单号：</span>
                    <input type="text" id="txbRepoApplyIdNo" style="float: right;"/>
                </li>
                 <li>
                    <span>业务单号：</span>
                    <input type="text" id="txbrefNo" style="float: right;"/>
                </li>
                <li>
                    <span>质押申请单号：</span>
                    <input type="text" id="txbPledgeApplyNo" style="float: right;"/>
                </li>
                <li>
                    <span style="float: left;">日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">状态：</span>
                    <div id="ddlRepoApplyStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <input type="button" id="btnExport" value="导出Excel" />
                    <a target="_self" runat="server" href="FinancingRepoApplyCanRepoPledgeList.aspx" id="btnAdd">赎回</a>
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
