<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotMainReadyCashInList.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotMainReadyCashInList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款登记列表</title>
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

            //时间控件
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //收款登记人, autoDropDownHeight: true
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlCashInEmpId").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25 });

            //收款银行
            var ddlPledgeBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlPledgeBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlPledgeBankurl, async: false };
            var ddlPledgeBankdataAdapter = new $.jqx.dataAdapter(ddlPledgeBanksource);
            $("#ddlCashInBank").jqxComboBox({ source: ddlPledgeBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 120 });

            //绑定Grid
            var url = "Handler/CashInAllotCanAllotCashInListHandler.ashx?s=" + statusEnum.已生效;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "CashInId", type: "int" },
                   { name: "CashInDate", type: "date" },
                   { name: "InnerCorp", type: "string" },
                   { name: "CashInBala", type: "string" },
                   { name: "BankName", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "CashInStatus", type: "int" },
                   { name: "CanAllotBala", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
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
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"CashInAllotMainCreate.aspx?id=" + value + "\">收款分配</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxGrid").jqxGrid(
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
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "收款金额", datafield: "CashInBala" },
                  { text: "收款银行", datafield: "BankName" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "收款状态", datafield: "StatusName" },
                  { text: "可分配金额", datafield: "CanAllotBala" },
                  { text: "操作", datafield: "CashInId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var dateBegin = $("#txbFromCreateDate").val();
                var dateEnd = $("#txbToCreateDate").val();
                var empId = $("#ddlCashInEmpId").val();
                var bank = $("#ddlCashInBank").val();
                source.url = "Handler/CashInAllotCanAllotCashInListHandler.ashx?f=" + dateBegin + "&t=" + dateEnd + "&e=" + empId + "&s=" + statusEnum.已生效 + "&b=" + bank;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">收款日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">收款登记人：</span>
                    <div id="ddlCashInEmpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">收款银行：</span>
                    <div id="ddlCashInBank" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
