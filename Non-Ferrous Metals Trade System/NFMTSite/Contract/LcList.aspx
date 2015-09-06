<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LcList.aspx.cs" Inherits="NFMTSite.Contract.LcList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>信用证管理</title>
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

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlLcStatus");

            //开证行
            var ddlIssueBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlIssueBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlIssueBankurl, async: false };
            var ddlIssueBankdataAdapter = new $.jqx.dataAdapter(ddlIssueBanksource);
            $("#ddlIssueBank").jqxComboBox({ source: ddlIssueBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });

            //通知行
            var url = "../BasicData/Handler/BanDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlAdviseBank").jqxComboBox({ source: dataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });

            //开证日期
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 80 });

            //Grid
            var issueBank = $("#ddlIssueBank").val();
            var adviseBank = $("#ddlAdviseBank").val();
            var datefrom = $("#txbFromCreateDate").val();
            var dateto = $("#txbToCreateDate").val();
            var status = $("#ddlLcStatus").val();
            var gridurl = "Handler/LcListHandler.ashx?i=" + issueBank + "&a=" + adviseBank + "&df=" + datefrom + "&dt=" + dateto + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;

            var gridsource = {
                datafields:
                [
                   { name: "LcId", type: "int" },
                   { name: "IssueBank", type: "string" },
                   { name: "AdviseBank", type: "string" },
                   { name: "IssueDate", type: "date" },
                   { name: "FutureDay", type: "string" },
                   { name: "LcBala", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "LCStatus", type: "int" }
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
                sortcolumn: "lc.LcId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "lc.LcId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: gridurl
            };
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"LcDetail.aspx?id=" + value + "\">明细</a>";
                if (item.LCStatus > statusEnum.已作废 && item.LCStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"LcUpdate.aspx?id=" + value + "\">修改</a>";
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                cellHtml += "</div>";
                return cellHtml;
            }

            var griddataAdapter = new $.jqx.dataAdapter(gridsource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: griddataAdapter,
                columnsresize: true,
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
                      { text: "开证行", datafield: "IssueBank" },
                      { text: "通知行", datafield: "AdviseBank" },
                      { text: "开证日期", datafield: "IssueDate", cellsformat: "yyyy-MM-dd" },
                      { text: "远期天数", datafield: "FutureDay" },
                      { text: "信用证金额", datafield: "LcBala" },
                      { text: "状态", datafield: "StatusName" },
                      { text: "操作", datafield: "LcId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var issueBank = $("#ddlIssueBank").val();
                var adviseBank = $("#ddlAdviseBank").val();
                var datefrom = $("#txbFromCreateDate").val();
                var dateto = $("#txbToCreateDate").val();
                var status = $("#ddlLcStatus").val();
                gridsource.url = "Handler/LcListHandler.ashx?i=" + issueBank + "&a=" + adviseBank + "&df=" + datefrom + "&dt=" + dateto + "&s=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">开证行：</span>
                    <div id="ddlIssueBank" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">通知行：</span>
                    <div id="ddlAdviseBank" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">开证日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <div style="float: left;">信用证状态：</div>
                    <div id="ddlLcStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="LcCreate.aspx" id="btnAdd">新增信用证</a>
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
