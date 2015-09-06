<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankPledgeReport.aspx.cs" Inherits="NFMTSite.Financing.BankPledgeReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行质押表</title>
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
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            //质押银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlBankId").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 120, height: 25, autoDropDownHeight: true });
            //业务编号
            $("#txbRefNo").jqxInput({ height: 25, width: 120 });

            //日期
            $("#dtBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtBeginDate").jqxDateTimeInput({ value: tempDate });
            $("#dtEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            var repoInfoSource = [{ text: "全部", value: 0 }, { text: "已赎回", value: 1 }, { text: "未赎回", value: 2 }];

            //赎回情况
            $("#ddlRepoInfo").jqxDropDownList({
                source: repoInfoSource,
                selectedIndex: 0,
                height: 25,
                width: 100,
                displayMember: "text",
                valueMember: "value",
                autoDropDownHeight: true
            });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnExcel").jqxButton({ height: 25, width: 120 });

            var bankId = $("#ddlBankId").val();
            var refNo = $("#txbRefNo").val();
            var startDate = $("#dtBeginDate").val();
            var endDate = $("#dtEndDate").val();
            var repoInfo = $("#ddlRepoInfo").val();

            var url = "Handler/BankPledgeReportHandler.ashx?bankId=" + bankId + "&refNo=" + refNo + "&s=" + startDate + "&e=" + endDate + "&repoInfo=" + repoInfo;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "BankName", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "ContractNo", type: "string" },
                   { name: "ApplyTime", type: "date" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "nowPledgeAmount", type: "number" }
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
                sortcolumn: "bank.BankName",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || "bank.BankName";
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
                pagesize: 20,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "质押银行", datafield: "BankName",width:"11%" },
                  { text: "业务编号", datafield: "RefNo", width: "11%" },
                  { text: "净重", datafield: "NetAmount", width: "11%" },
                  { text: "合同号", datafield: "ContractNo", width: "11%" },
                  { text: "质押日期", datafield: "ApplyTime", width: "11%", cellsformat: "yyyy-MM-dd" },
                  { text: "头寸数量", datafield: "Hands", width: "11%" },
                  { text: "价格", datafield: "Price", width: "11%" },
                  { text: "到期日", datafield: "ExpiringDate", width: "11%", cellsformat: "yyyy-MM-dd" },
                  { text: "现质押量", datafield: "nowPledgeAmount" }
                ]
            });

            $("#btnSearch").click(function () {

                var bankId = $("#ddlBankId").val();
                var refNo = $("#txbRefNo").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();
                var repoInfo = $("#ddlRepoInfo").val();

                source.url = "Handler/BankPledgeReportHandler.ashx?bankId=" + bankId + "&refNo=" + refNo + "&s=" + startDate + "&e=" + endDate + "&repoInfo=" + repoInfo;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnExcel").click(function () {

                var bankId = $("#ddlBankId").val();
                var refNo = $("#txbRefNo").val();
                var startDate = $("#dtBeginDate").val();
                var endDate = $("#dtEndDate").val();
                var repoInfo = $("#ddlRepoInfo").val();

                document.location.href = "ExportExcel.aspx?rt=359&bankId=" + bankId + "&refNo=" + refNo + "&s=" + startDate + "&e=" + endDate + "&repoInfo=" + repoInfo;
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
                    <span style="float: left;">质押银行：</span>
                    <div id="ddlBankId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">业务编号：</span>
                    <input type="text" id="txbRefNo" />
                </li>
                <li>
                    <span style="float: left;">质押日期：</span>
                    <div id="dtBeginDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtEndDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">赎回情况：</span>
                    <div id="ddlRepoInfo" style="float: left;" />
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