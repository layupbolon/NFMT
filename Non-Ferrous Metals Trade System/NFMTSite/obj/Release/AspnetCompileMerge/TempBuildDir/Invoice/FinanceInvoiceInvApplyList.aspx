<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceInvoiceInvApplyList.aspx.cs" Inherits="NFMTSite.Invoice.FinanceInvoiceInvApplyList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>开票申请列表</title>
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

            //时间
            $("#txbFromDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            var fromdate = $("#txbFromDate").val();
            var todate = $("#txbToDate").val();

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "InvoiceApplyId", type: "int" },
                   { name: "ApplyId", type: "int" },
                   { name: "ApplyNo", type: "string" },
                   { name: "Name", type: "date" },
                   { name: "ApplyTime", type: "date" },
                   { name: "TotalBala", type: "number" }
                ],
                datatype: "json",
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
                sortcolumn: "ia.InvoiceApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ia.InvoiceApplyId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/FinanceInvoiceInvApplyListHandler.ashx?fd=" + fromdate + "&td=" + todate
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxgrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"FinanceInvoiceInvApplyCreate.aspx?id=" + value + "\">开票</a>";
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
                  { text: "申请编号", datafield: "ApplyNo" },                  
                  { text: "申请人", datafield: "Name" },
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请金额", datafield: "TotalBala" },
                  { text: "操作", datafield: "InvoiceApplyId", cellsrenderer: cellsrenderer, width: 100, sortable: false, enabletooltips: false }
                ]
            });


            $("#btnSearch").click(function () {
                var fromdate = $("#txbFromDate").val();
                var todate = $("#txbToDate").val();

                source.url = "Handler/FinanceInvoiceInvApplyListHandler.ashx?fd=" + fromdate + "&td=" + todate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">申请日期：</span>
                    <div id="txbFromDate" style="float: left;"></div>

                    <span style="float: left;">至</span>
                    <div id="txbToDate" style="float: left;"></div>
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
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>