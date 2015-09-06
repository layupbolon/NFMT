<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="NFMTSite.Funds.PaymentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务付款列表</title>
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

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            CreateStatusDropDownList("selStatus");

            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 });
            //$("#btnClose").jqxLinkButton({ height: 25, width: 120 });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //支付人
            var empUrl = "../User/Handler/EmpDDLHandler.ashx";
            var empSource = { datatype: "json", url: empUrl, async: false };
            var empDataAdapter = new $.jqx.dataAdapter(empSource);
            $("#selPaymentEmp").jqxComboBox({
                source: empDataAdapter, displayMember: "Name", valueMember: "EmpId", height: 25, searchMode: "containsignorecase"
            });

            //收款公司            
            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25
            });

            var url = "Handler/PaymentListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "PaymentId", type: "int" },
                   { name: "PaymentCode", type: "string" },
                   { name: "PayDatetime", type: "date" },
                   { name: "ApplyNo", type: "string" },
                   { name: "RecevableCorpName", type: "string" },
                   { name: "PayStyleName", type: "string" },
                   { name: "PayBala", type: "number" },
                   { name: "CurrencyName", type: "int" },
                   { name: "PayEmpName", type: "string" },
                   { name: "PaymentStatusName", type: "string" },
                   { name: "PayApplySource", type: "int" },
                   { name: "PaymentStatus", type: "int" }
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
                sortcolumn: "pay.PaymentId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pay.PaymentId";
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
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                if (item.PayApplySource == FundsEnum.ContractPayApply) {
                    cellHtml += "<a target=\"_self\" href=\"PaymentContractDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.PayApplySource == FundsEnum.StockPayApply) {
                    cellHtml += "<a target=\"_self\" href=\"PaymentStockDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.PayApplySource == FundsEnum.InvoicePayApply) {
                    cellHtml += "<a target=\"_self\" href=\"PaymentInvoiceDetail.aspx?id=" + value + "\">明细</a>";
                }

                if (item.PaymentStatus > statusEnum.已关闭 && item.PaymentStatus < statusEnum.待审核) {
                    if (item.PayApplySource == FundsEnum.ContractPayApply) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PaymentContractUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.PayApplySource == FundsEnum.StockPayApply) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PaymentStockUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.PayApplySource == FundsEnum.InvoicePayApply) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PaymentInvoiceUpdate.aspx?id=" + value + "\">修改</a>";
                    }
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
                columnsresize: true,
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
                  { text: "付款编号", datafield: "PaymentCode" },
                  { text: "申请编号", datafield: "ApplyNo" },
                  { text: "付款时间", datafield: "PayDatetime", cellsformat: "yyyy-MM-dd" },
                  { text: "收款公司", datafield: "RecevableCorpName" },
                  { text: "付款方式", datafield: "PayStyleName" },
                  { text: "付款金额", datafield: "PayBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "支付人", datafield: "PayEmpName" },
                  { text: "付款状态", datafield: "PaymentStatusName" },
                  { text: "操作", datafield: "PaymentId", cellsrenderer: cellsrenderer, width: 100, sortable: false, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {

                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                var empId = $("#selPaymentEmp").val();
                var recCorp = $("#selOutCorp").val();
                var status = $("#selStatus").jqxDropDownList("val");

                source.url = "Handler/PaymentListHandler.ashx?s=" + status + "&ei=" + empId + "&rc=" + recCorp + "&fd=" + fromDate + "&td=" + toDate;
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
                    <span style="float: left;">付款日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">支付人：</span>
                    <div id="selPaymentEmp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收款公司：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">付款状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PaymentPayApplyList.aspx" id="btnAdd">新增付款</a>
                    <%--<a target="_self" runat="server" href="PayApplyCloseList.aspx" id="btnClose">关闭付款申请</a>--%>
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
