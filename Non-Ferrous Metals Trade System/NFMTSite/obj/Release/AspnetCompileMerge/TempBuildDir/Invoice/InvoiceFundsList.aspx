﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceFundsList.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceFundsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务发票列表</title>
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

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            CreateStatusDropDownList("selStatus");

            $("#btnOutAdd").jqxLinkButton({ height: 25, width: 120 });
            $("#btnInAdd").jqxLinkButton({ height: 25, width: 120 });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //开票公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });

            //收票公司            
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });

            var url = "Handler/InvoiceFundsListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "FinanceInvoiceId", type: "int" },
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "VATRatio", type: "number" },
                   { name: "VATBala", type: "number" },
                   { name: "InvoiceDirection", type: "int" },
                   { name: "DirectionName", type: "string" }
                ],
                sort: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "fi.FinanceInvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "fi.FinanceInvoiceId";
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

                var item = $("#jqxgrid").jqxGrid("getrowdata", row);
                var dir = "out";
                if (item.InvoiceDirection == 34) { dir = "in"; }

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"InvoiceFundsDetail.aspx?id=" + value + "\">明细</a>";

                if (item.InvoiceStatus > statusEnum.已关闭 && item.InvoiceStatus < statusEnum.待审核) {



                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"InvoiceFundsUpdate.aspx?id=" + value + "\">修改</a>";
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
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "实际发票编码", datafield: "InvoiceName" },
                  { text: "发票方向", datafield: "DirectionName" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "开票公司", datafield: "OutCorpName" },
                  { text: "收票公司", datafield: "InCorpName" },
                  { text: "增值税率", datafield: "VATRatio" },
                  { text: "增值税金额", datafield: "VATBala" },
                  { text: "发票状态", datafield: "StatusName" },
                  { text: "操作", datafield: "FinanceInvoiceId", cellsrenderer: cellsrenderer, width: 100, sortable: false, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {

                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                var inCorp = $("#selInCorp").val();
                var outCorp = $("#selOutCorp").val();
                var status = $("#selStatus").jqxDropDownList("val");

                source.url = "Handler/InvoiceFundsListHandler.ashx?s=" + status + "&ic=" + inCorp + "&oc=" + outCorp + "&fd=" + fromDate + "&td=" + toDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">开票日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">开票公司：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收票公司：</span>
                    <div id="selInCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">发票状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="InvoiceFundsCreate.aspx?d=out" id="btnOutAdd">财务票开出</a>
                    <a target="_self" runat="server" href="InvoiceFundsCreate.aspx?d=in" id="btnInAdd">财务票收入</a>
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
