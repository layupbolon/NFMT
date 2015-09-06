<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundsCurrent.aspx.cs" Inherits="NFMTSite.Report.FundsCurrent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户往来</title>
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

            //我方公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#ddlInCorp").jqxDropDownList({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, selectedIndex:0
            });

            //客户
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlOutCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase"
            });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var inCorpId = $("#ddlInCorp").val();
            var outCorpId = $("#ddlOutCorp").val();
            var startDate = $("#dtBeginDate").val();
            var endDate = $("#dtEndDate").val();
            var url = "Handler/FundsCurrentReportHandler.ashx?in=" + inCorpId + "&out=" + outCorpId + "&s=" + startDate + "&e=" + endDate;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "bgDate", type: "date" },
                   { name: "endDate", type: "date" },
                   { name: "outCorpName", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "PreBala", type: "number" },
                   { name: "LastIssueBala", type: "number" },
                   { name: "LastCashInBala", type: "number" },
                   { name: "LastCollectBala", type: "number" },
                   { name: "LastPayBala", type: "number" },
                   { name: "LastBala", type: "number" }
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
                sortcolumn: "corp.CorpName",
                sortdirection: "asc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "corp.CorpName";
                    data.sortorder = data.sortorder || "asc";
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
            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                enabletooltips: true,
                columnsresize: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "开始日期", datafield: "bgDate", cellsformat: "yyyy-MM-dd", width: 100 },
                  { text: "结束日期", datafield: "endDate", cellsformat: "yyyy-MM-dd", width: 100 },
                  { text: "客户", datafield: "outCorpName", width: 150 },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "期初余额", datafield: "PreBala",cellsformat:"d" },
                  { text: "当期开票金额", datafield: "LastIssueBala", cellsformat: "d" },
                  { text: "当期收款金额", datafield: "LastCashInBala", cellsformat: "d" },
                  { text: "当期收票金额", datafield: "LastCollectBala", cellsformat: "d" },
                  { text: "当期付款金额", datafield: "LastPayBala", cellsformat: "d" },
                  { text: "期末来往", datafield: "LastBala", cellsformat: "d" }
                ]
            });

            $("#btnSearch").click(function () {

                var inCorpId = $("#ddlInCorp").val();
                var outCorpId = $("#ddlOutCorp").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                source.url = "Handler/FundsCurrentReportHandler.ashx?in=" + inCorpId + "&out=" + outCorpId + "&s=" + startDate + "&e=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var inCorpId = $("#ddlInCorp").val();
                var outCorpId = $("#ddlOutCorp").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=303&in=" + inCorpId + "&out=" + outCorpId + "&s=" + startDate + "&e=" + endDate;

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
                    <span style="float: left;">我方公司：</span>
                    <div id="ddlInCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">客户：</span>
                    <div id="ddlOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <input type="button" id="btnExcel" value="导出Excel" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>