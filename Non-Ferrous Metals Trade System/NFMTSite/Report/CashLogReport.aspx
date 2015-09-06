<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashLogReport.aspx.cs" Inherits="NFMTSite.Report.CashLogReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>资金流水表</title>
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

            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase"
            });

            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase"
            });

            var logTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.LogTypeValue%>", async: false };
            var logTypeDataAdapter = new $.jqx.dataAdapter(logTypeSource);
            $("#selLogType").jqxComboBox({ source: logTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });
            $("#txbStartDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbStartDate").jqxDateTimeInput({ value: tempDate });
            $("#txbEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/CashLogReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "FundsLogId", type: "int" },
                   { name: "LogDate", type: "date" },
                   { name: "InCorpName", type: "string" },
                   { name: "InBankName", type: "string" },
                   { name: "InAccountNo", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "OutBankName", type: "string" },
                   { name: "OutAccountNo", type: "string" },
                   { name: "LogTypeName", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "FundsBala", type: "number" },
                   { name: "IsVirtualPay", type: "string" },
                   { name: "PayModeName", type: "string" }
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
                sortcolumn: "fl.FundsLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "fl.FundsLogId";
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
                  { text: "流水日期", datafield: "LogDate", cellsformat: "yyyy-MM-dd", width: 90 },
                  { text: "收付方向", datafield: "LogTypeName" },
                  { text: "我方抬头", datafield: "InCorpName" },
                  { text: "我方开户行", datafield: "InBankName" },
                  { text: "我方账户", datafield: "InAccountNo" },
                  { text: "对方抬头", datafield: "OutCorpName" },
                  { text: "对方银行", datafield: "OutBankName" },
                  { text: "对方账户", datafield: "OutAccountNo" },
                  { text: "付款方式", datafield: "PayModeName" },
                  { text: "金额", datafield: "FundsBala" },
                  { text: "币种", datafield: "CurrencyName"}
                ]
            });

            $("#btnSearch").click(function () {
                               
                var outCorpId = $("#selOutCorp").val();
                var inCorpId = $("#selInCorp").val();
                var logType = $("#selLogType").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                source.url = "Handler/CashLogReportHandler.ashx?oci=" + outCorpId + "&ici=" + inCorpId + "&lt=" + logType + "&sd=" + startDate + "&ed=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var outCorpId = $("#selOutCorp").val();
                var inCorpId = $("#selInCorp").val();
                var logType = $("#selLogType").val();
                var startDate = $("#txbStartDate").val();
                var endDate = $("#txbEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=292&oci=" + outCorpId + "&ici=" + inCorpId + "&lt=" + logType + "&sd=" + startDate + "&ed=" + endDate;

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
                    <div id="selInCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">对方公司：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收付款</span>
                    <div id="selLogType" style="float: left;" />
                </li>                
                <li>
                    <span style="float: left;">流水日期：</span>
                    <div id="txbStartDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbEndDate" style="float: left;"></div>
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
