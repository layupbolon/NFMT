<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomsReport.aspx.cs" Inherits="NFMTSite.Report.CustomsReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报关明细表</title>
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

            //报关公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });
            //合约号
            $("#txbRefNo").jqxInput({ height: 22, width: 120 });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var url = "Handler/CustomsReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "CustomsId", type: "int" },
                   { name: "CustomsDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "GrossWeight", type: "number" },
                   { name: "NetWeight", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "CustomsPrice", type: "number" }
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
                sortcolumn: "cc.CustomsId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cc.CustomsId";
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
                columnsresize: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "报关日期", datafield: "CustomsDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "毛重", datafield: "GrossWeight" },
                  { text: "净重", datafield: "NetWeight" },
                  { text: "单位", datafield: "MUName" },
                  { text: "报关公司", datafield: "CorpName" },
                  { text: "报关币种", datafield: "CurrencyName"},
                  { text: "报关单价", datafield: "CustomsPrice" }
                ]
            });

            $("#btnSearch").click(function () {

                var customsCorpId = $("#ddlCustomsCorpId").val();
                var refNo = $("#txbRefNo").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                source.url = "Handler/CustomsReportHandler.ashx?c=" + customsCorpId + "&r=" + refNo + "&s=" + startDate + "&e=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var customsCorpId = $("#ddlCustomsCorpId").val();
                var refNo = $("#txbRefNo").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                document.location.href = "ExportExcel.aspx?rt=301&c=" + customsCorpId + "&r=" + refNo + "&s=" + startDate + "&e=" + endDate;

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
                    <span style="float: left;">报关公司：</span>
                    <div id="ddlCustomsCorpId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">业务单号：</span>
                    <span>
                        <input type="text" id="txbRefNo" /></span>
                </li>
                <li>
                    <span style="float: left;">报关日期：</span>
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