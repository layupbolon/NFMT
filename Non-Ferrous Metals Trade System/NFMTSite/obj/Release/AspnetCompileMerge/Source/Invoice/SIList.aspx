<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SIList.aspx.cs" Inherits="NFMTSite.Invoice.SIList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价外票列表</title>
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

            $("#txbInvoiceName").jqxInput({ height: 25, width: 120 });

            //收票公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlInnerCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });

            //开票公司
            var innerCorpUrl = "../User/Handler/CorpDDLHandler.ashx";
            var innerCorpSource = { datatype: "json", url: innerCorpUrl, async: false };
            var innerCorpDataAdapter = new $.jqx.dataAdapter(innerCorpSource);
            $("#ddlOutCorp").jqxComboBox({
                source: innerCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAddIn").jqxLinkButton({ height: 25, width: 90 });
            $("#btnAddOut").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var invName = $("#txbInvoiceName").val();
            var corp = $("#ddlInnerCorp").val();
            var corpOut = $("#ddlOutCorp").val();
            var fromdate = $("#txbFromCreateDate").val();
            var todate = $("#txbToCreateDate").val();
            var url = "Handler/SIListHandler.ashx?i=" + invName + "&c=" + corp + "&co=" + corpOut + "&f=" + fromdate + "&t=" + todate;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "AllotBala", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
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
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
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
                cellHtml += "<a target=\"_self\" href=\"SIDetail.aspx?id=" + value + "\">明细</a>";
                if (item.InvoiceStatus > statusEnum.已作废 && item.InvoiceStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"SIUpdate.aspx?id=" + value + "\">修改</a>";
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
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
                  { text: "发票号", datafield: "InvoiceNo" },
                  { text: "实际发票号", datafield: "InvoiceName" },
                  { text: "发票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "收票公司", datafield: "InnerCorp" },
                  { text: "开票公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  //{ text: "已分配金额", datafield: "AllotBala" },
                  { text: "发票状态", datafield: "StatusName" },
                  { text: "操作", datafield: "InvoiceId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var invName = $("#txbInvoiceName").val();
                var corp = $("#ddlInnerCorp").val();
                var corpOut = $("#ddlOutCorp").val();
                var fromdate = $("#txbFromCreateDate").val();
                var todate = $("#txbToCreateDate").val();
                source.url = "Handler/SIListHandler.ashx?i=" + invName + "&c=" + corp + "&co=" + corpOut + "&f=" + fromdate + "&t=" + todate;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">实际发票号：</span>
                    <input type="text" id="txbInvoiceName" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收票公司：</span>
                    <div id="ddlInnerCorp" style="float: left;"></div>

                    <span style="float: left;">开票公司：</span>
                    <div id="ddlOutCorp" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">开票日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="SICreate.aspx?d=34" id="btnAddIn">收价外票</a>
                    <a target="_self" runat="server" href="SICreate.aspx?d=33" id="btnAddOut">开价外票</a>
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
