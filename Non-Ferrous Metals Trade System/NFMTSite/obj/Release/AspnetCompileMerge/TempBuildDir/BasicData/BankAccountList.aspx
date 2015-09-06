<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankAccountList.aspx.cs" Inherits="NFMTSite.BasicData.BankAccountList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行账户管理</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script src="../js/Utility.js"></script>
    <script src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            //银行账户
            $("#txbAccountNo").jqxInput({ height: 22, width: 150 });

            //币种
            var currencyIdurl = "Handler/CurrencDDLHandler.ashx";
            var currencyIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyIdurl, async: false };
            var currencyIddataAdapter = new $.jqx.dataAdapter(currencyIdsource);
            $("#currencyId").jqxComboBox({ source: currencyIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 80, autoDropDownHeight: true });

            //绑定 银行
            var bankIdurl = "Handler/BankDDLHandler.ashx";
            var bankIdsource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: bankIdurl, async: false };
            var bankIddataAdapter = new $.jqx.dataAdapter(bankIdsource);
            $("#bankId").jqxComboBox({ source: bankIddataAdapter, displayMember: "BankName", valueMember: "BankId", width: 130, height: 25 });

            //绑定 公司
            var url = "Handler/CorporationDDLHandler.ashx?isSelf=1";
            var source = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#companyId").jqxComboBox({ source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25 });

            //状态
            CreateStatusDropDownList("ddlStatus");

            var companyId = $("#companyId").val();
            var bankId = $("#bankId").val();
            var currencyId = $("#currencyId").val();
            var accountNo = $("#txbAccountNo").val();
            var status = $("#ddlStatus").val();;
            var url = "Handler/BankAccountListHandler.ashx?c=" + companyId + "&b=" + bankId + "&cu=" + currencyId + "&a=" + accountNo + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
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
                sortcolumn: "BankAccId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "BankAccId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url

            };
            //alert(source);
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"BankAccountDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"BankAccountUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxGrid").jqxGrid(
            {
                width: "99%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [

                  { text: "公司", datafield: "CorpName" },
                  { text: "银行", datafield: "BankName" },
                  { text: "账户", datafield: "AccountNo" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "账户描述", datafield: "BankAccDesc" },
                  { text: "账户状态", datafield: "StatusName" },
                  { text: "操作", datafield: "BankAccId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            $("#btnSearch").click(LoadData);

            function LoadData() {
                var companyId = $("#companyId").val();
                var bankId = $("#bankId").val();
                var currencyId = $("#currencyId").val();
                var accountNo = $("#txbAccountNo").val();
                var status = $("#ddlStatus").val();;
                source.url = "Handler/BankAccountListHandler.ashx?c=" + companyId + "&b=" + bankId + "&cu=" + currencyId + "&a=" + accountNo + "&s=" + status;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            }
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
                    <span style="width: 15%; text-align: right;">公司：</span>
                    <div id="companyId" runat="server" style="float: right;" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">银行：</span>
                    <div id="bankId" runat="server" style="float: right;" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">账户号：</span>
                    <input type="text" id="txbAccountNo" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">币种：</span>
                    <div id="currencyId" runat="server" style="float: right;" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">银行账号状态：</span>
                    <div id="ddlStatus" runat="server" style="float: right;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="BankAccountCreate.aspx" id="btnAdd">新增账户</a>
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
