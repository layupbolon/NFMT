<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentVirtualList.aspx.cs" Inherits="NFMTSite.Funds.PaymentVirtualList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>虚拟付款列表</title>
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
                source: empDataAdapter, displayMember: "Name", valueMember: "EmpId", height: 25, width: 100
            });

            //收款公司            
            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25
            });

            var url = "Handler/PaymentVirtualListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "VirtualId", type: "int" },
                   { name: "PayDatetime", type: "date" },
                   { name: "PayCorpName", type: "string" },
                   { name: "RecCorpName", type: "string" },
                   { name: "PayEmpName", type: "string" },
                   { name: "ApplyTime", type: "date" },
                   { name: "ApplyNo", type: "string" },
                   { name: "ApplyBala", type: "number" },
                   { name: "PayBala", type: "number" },
                   { name: "PaymentStatus", type: "number" },
                   { name: "PayApplySource", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "VirtualBala", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "FlowName", type: "string" },
                   { name: "DetailStatus", type: "int" },
                   { name: "StatusName", type: "string" }
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
                sortcolumn: "pv.VirtualId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pv.VirtualId";
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

                if (item.DetailStatus == statusEnum.已生效) {
                    cellHtml += "<a target=\"_self\" href=\"PaymentVirtualConfirm.aspx?id=" + value + "\">虚拟付款确认</a>";
                }
                else {
                    cellHtml += "<a target=\"_self\" href=\"PaymentVirtualConfirm.aspx?id=" + value + "\">明细</a>";
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
                  { text: "付款日期", datafield: "PayDatetime", cellsformat: "yyyy-MM-dd" },
                  { text: "付款公司", datafield: "PayCorpName" },
                  { text: "收款公司", datafield: "RecCorpName" },
                  { text: "支付人", datafield: "PayEmpName" },
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "付款申请号", datafield: "ApplyNo" },
                  { text: "申请金额", datafield: "ApplyBala" },
                  { text: "本次付款总额", datafield: "PayBala" },
                  { text: "虚拟金额", datafield: "VirtualBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "虚拟付款状态", datafield: "StatusName" },
                  { text: "操作", datafield: "VirtualId", cellsrenderer: cellsrenderer, width: 100, sortable: false, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {

                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                var empId = $("#selPaymentEmp").val();
                var recCorp = $("#selOutCorp").val();
                var status = $("#selStatus").jqxDropDownList("val");

                source.url = "Handler/PaymentVirtualListHandler.ashx?s=" + status + "&ei=" + empId + "&rc=" + recCorp + "&fd=" + fromDate + "&td=" + toDate;
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
                    <span style="float: left;">申请日期：</span>
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
                    <span style="float: left;">虚拟付款状态：</span>
                    <div id="selStatus" style="float: left;" />
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
