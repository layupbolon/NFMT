<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentReport.aspx.cs" Inherits="NFMTSite.Report.PaymentReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请与执行表</title>
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

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDept").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 150, height: 25 });

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorp").jqxComboBox({ source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase" });

            //收款币种
            var ddlMUIdurl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlCurrency").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 80, height: 25, searchMode: "containsignorecase" });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            //$("#btnExcel").jqxButton({ height: 25, width: 120 });

            var payApplyUrl = "Handler/PaymentReportHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var payApplySource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "PayApplyId", type: "int" },
                   { name: "ApplyTime", type: "date" },
                   { name: "ApplyNo", type: "string" },
                   { name: "ApplyBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "ApplyCorpName", type: "string" },
                   { name: "DeptName", type: "string" },
                   { name: "RecCorpName", type: "string" },
                   { name: "BankName", type: "string" },
                   { name: "RecBankAccount", type: "string" },
                   { name: "DetailName", type: "string" }
                ],
                type: "GET",
                url: payApplyUrl,
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
                sortcolumn: "pa.PayApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pa.PayApplyId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                }
            };
            var payApplyAdapter = new $.jqx.dataAdapter(payApplySource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var initrowdetails = function (index, parentElement, gridElement, record) {
                var id = record.PayApplyId;

                var payUrl = "Handler/PaymentExecuteReportHandler.ashx?id=" + id;
                var paySource =
                {
                    datatype: "json",
                    datafields:
                    [
                       { name: "PaymentId", type: "int" },
                       { name: "PayDatetime", type: "date" },
                       { name: "FundsPayBala", type: "number" },
                       { name: "VirtualBala", type: "number" },
                       { name: "CurrencyName", type: "string" },
                       { name: "CorpName", type: "string" },
                       { name: "BankName", type: "string" },
                       { name: "AccountNo", type: "string" },
                       { name: "DetailName", type: "string" }
                    ],
                    type: "GET",
                    url: payUrl,
                    async: false
                };

                var payAdapter = new $.jqx.dataAdapter(paySource, { autoBind: true });

                var grid = $($(parentElement).children()[0]);
                if (grid != null) {
                    grid.jqxGrid({
                        columnsresize: true,
                        autoheight: true,
                        source: payAdapter, width: 1000,
                        columns: [
                          { text: '执行日期', datafield: 'PayDatetime', width: 100, cellsformat: "yyyy-MM-dd" },
                          { text: '实际付款金额', datafield: 'FundsPayBala', width: 120 },
                          { text: '虚拟付款金额', datafield: 'VirtualBala', width: 120 },
                          { text: '币种', datafield: 'CurrencyName', width: 100 },
                          { text: '付款公司', datafield: 'CorpName', width: 150 },
                          { text: '付款银行', datafield: 'BankName', width: 100 },
                          { text: '付款账户', datafield: 'AccountNo', width: 150 },
                          { text: '付款方式', datafield: 'DetailName', width: 100 }
                        ]
                    });
                }
            }

            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: payApplyAdapter,
                pageable: true,
                autoheight: true,
                sortable: true,
                enabletooltips: true,
                virtualmode: true,
                sorttogglestates: 1,
                columnsresize: true,
                rowdetails: true,
                initrowdetails: initrowdetails,
                rowdetailstemplate: { rowdetails: "<div id='grid' style='margin: 10px;'></div>", rowdetailshidden: true },//, rowdetailsheight: 100
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请号", datafield: "ApplyNo" },
                  { text: "申请金额", datafield: "ApplyBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "申请公司", datafield: "ApplyCorpName" },
                  { text: "申请部门", datafield: "DeptName" },
                  { text: "收款公司", datafield: "RecCorpName" },
                  { text: "收款银行", datafield: "BankName" },
                  { text: "收款账户", datafield: "RecBankAccount" },
                  { text: "付款方式", datafield: "DetailName" }
                ]
            });

            $("#btnSearch").click(function () {

                var applyCorp = $("#ddlApplyCorp").val();
                var applyDept = $("#ddlApplyDept").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();
                var currency = $("#ddlCurrency").val();

                payApplySource.url = "Handler/PaymentReportHandler.ashx?corp=" + applyCorp + "&dept=" + applyDept + "&cur=" + currency + "&s=" + startDate + "&e=" + endDate;
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
                    <span style="float: left;">申请公司：</span>
                    <div id="ddlApplyCorp" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">申请部门：</span>
                    <div id="ddlApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">币种：</span>
                    <div id="ddlCurrency" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">申请日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                   <%-- <input type="button" id="btnExcel" value="导出Excel" />--%>
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
