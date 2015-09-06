<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashAllotReport.aspx.cs" Inherits="NFMTSite.Report.CashAllotReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>资金分配表</title>
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

            //收款银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlCashInBank").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 260, height: 25, searchMode: "containsignorecase" });

            //收款公司
            var ddlCorpIdurl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCashInCorp").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 260, height: 25, searchMode: "containsignorecase" });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            //$("#btnExcel").jqxButton({ height: 25, width: 120 });

            var cashInUrl = "Handler/CashInReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var cashInsource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "CashInId", type: "int" },
                   { name: "CashInDate", type: "date" },
                   { name: "CashInBala", type: "string" },
                   { name: "BankName", type: "string" },
                   { name: "CorpName", type: "string" }
                ],
                type: "GET",
                url: cashInUrl,
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
                sortcolumn: "ci.CashInId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ci.CashInId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                }
            };
            var cashInAdapter = new $.jqx.dataAdapter(cashInsource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var initrowdetails = function (index, parentElement, gridElement, record) {
                var id = record.CashInId;
                
                var cashInAllotUrl = "Handler/CashInAllotReportHandler.ashx?id=" + id;
                var cashInAllotsource =
                {
                    datatype: "json",
                    datafields:
                    [
                       { name: "CashInId", type: "int" },
                       { name: "CorpName", type: "string" },
                       { name: "corpAllotBala", type: "string" },
                       { name: "ContractNo", type: "string" },
                       { name: "conAllotBala", type: "string" },
                       { name: "RefNo", type: "string" },
                       { name: "stAllotBala", type: "string" }
                    ],
                    type: "GET",
                    url: cashInAllotUrl,
                    async: false
                };

                var cashInAllotAdapter = new $.jqx.dataAdapter(cashInAllotsource, { autoBind: true });

                var grid = $($(parentElement).children()[0]);
                if (grid != null) {
                    grid.jqxGrid({
                        columnsresize: true,
                        autoheight: true,
                        source: cashInAllotAdapter, width: 960,
                        columns: [
                          { text: '分配至客户名称', datafield: 'CorpName', width: 160 },
                          { text: '分配至客户金额', datafield: 'corpAllotBala', width: 160 },
                          { text: '分配至合约编号', datafield: 'ContractNo', width: 160 },
                          { text: '分配至合约金额', datafield: 'conAllotBala', width: 160 },
                          { text: '分配至业务单号', datafield: 'RefNo', width: 160 },
                          { text: '分配至库存金额', datafield: 'stAllotBala', width: 160 }
                        ]
                    });
                }
            }

            $("#jqxgrid").bind('bindingcomplete', function (event) {
                if (event.target.id == "jqxgrid") {
                    $("#jqxgrid").jqxGrid('beginupdate');
                    var datainformation = $("#jqxgrid").jqxGrid('getdatainformation');
                    for (i = 0; i < datainformation.rowscount; i++) {
                        var hidden = i > 0 ? true : false;
                        $("#jqxgrid").jqxGrid('setrowdetails', i, "<div id='grid" + i + "' style='margin: 10px;'></div>", 220, hidden);
                    }
                    $("#jqxgrid").jqxGrid('resumeupdate');
                }
            });

            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: cashInAdapter,
                pageable: true,
                autoheight: true,
                sortable: true,
                enabletooltips: true,
                virtualmode: true,
                sorttogglestates: 1,
                columnsresize: true,
                rowdetails: true,
                initrowdetails: initrowdetails,
                rowdetailstemplate: { rowdetails: "<div id='grid' style='margin: 10px;'></div>" },//, rowdetailsheight: 220, rowdetailshidden: true
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd"},
                  { text: "收款金额", datafield: "CashInBala" },
                  { text: "收款银行", datafield: "BankName" },
                  { text: "收款公司", datafield: "CorpName" }
                ]
            });

            $("#btnSearch").click(function () {

                var cashInBank = $("#ddlCashInBank").val();
                var cashInCorp = $("#ddlCashInCorp").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();

                cashInsource.url = "Handler/CashInReportHandler.ashx?b=" + cashInBank + "&c=" + cashInCorp + "&s=" + startDate + "&e=" + endDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                //var customsCorpId = $("#ddlCustomsCorpId").val();
                //var refNo = $("#txbRefNo").val();
                //var startDate = $("#dtBeginDate").val();
                //var endDate = $("#dtEndDate").val();

                //document.location.href = "ExportExcel.aspx?rt=301&c=" + customsCorpId + "&r=" + refNo + "&s=" + startDate + "&e=" + endDate;

            });
        });

    </script>
</head>
<body class='default'>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">收款银行：</span>
                    <div id="ddlCashInBank" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收款抬头：</span>
                    <div id="ddlCashInCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收款日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <%--<input type="button" id="btnExcel" value="导出Excel" />--%>
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
