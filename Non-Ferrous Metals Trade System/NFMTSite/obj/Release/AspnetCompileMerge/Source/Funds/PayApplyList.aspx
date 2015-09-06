<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyList.aspx.cs" Inherits="NFMTSite.Funds.PayApplyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请列表</title>
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

            $("#btnContractAdd").jqxLinkButton({ height: 25, width: 120 });
            $("#btnInvoiceAdd").jqxLinkButton({ height: 25, width: 120 });
            //$("#btnStockAdd").jqxLinkButton({ height: 25, width: 120 });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#selApplyDept").jqxComboBox({
                source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25
            });

            //收款公司            
            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, searchMode: "containsignorecase"
            });

            var url = "Handler/PayApplyListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "PayApplyId", type: "int" },
                   { name: "ApplyId", type: "int" },
                   { name: "ApplyTime", type: "date" },
                   { name: "ApplyNo", type: "string" },
                   { name: "PayApplySource", type: "int" },
                   { name: "PayMatter", type: "int" },
                   { name: "PayMatterName", type: "string" },
                   { name: "PayMode", type: "int" },
                   { name: "PayModeName", type: "string" },
                   { name: "RecCorpId", type: "int" },
                   { name: "RecCorpName", type: "string" },
                   { name: "ApplyBala", type: "number" },
                   { name: "SumPayBala", type: "number" },
                   { name: "CurrencyId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "EmpId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "ApplyDeptId", type: "int" },
                   { name: "DeptName", type: "string" },
                   { name: "ApplyStatus", type: "int" },
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

                var item = dataAdapter.records[row];
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";

                if (item.PayApplySource == FundsEnum.InvoicePayApply) {
                    cellHtml += "<a target=\"_self\" href=\"PayApplyInvoiceDetail.aspx?id=" + value + "\">明细</a>";
                }
                else {
                    cellHtml += "<a target=\"_self\" href=\"PayApplyDetail.aspx?id=" + value + "\">明细</a>";
                }

                if (item.ApplyStatus > statusEnum.已关闭 && item.ApplyStatus < statusEnum.待审核) {
                    if (item.PayApplySource == FundsEnum.InvoicePayApply) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PayApplyInvoiceUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PayApplyUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                //if (item.PayApplySource == FundsEnum.ContractPayApply) {
                //    cellHtml += "<a target=\"_self\" href=\"PayApplyContractDetail.aspx?id=" + value + "\">明细</a>";
                //}
                //else if (item.PayApplySource == FundsEnum.StockPayApply) {
                //    cellHtml += "<a target=\"_self\" href=\"PayApplyStockDetail.aspx?id=" + value + "\">明细</a>";
                //}
                //else if (item.PayApplySource == FundsEnum.InvoicePayApply) {
                //    cellHtml += "<a target=\"_self\" href=\"PayApplyInvoiceDetail.aspx?id=" + value + "\">明细</a>";
                //}

                //if (item.ApplyStatus > statusEnum.已关闭 && item.ApplyStatus < statusEnum.待审核) {
                //    if (item.PayApplySource == FundsEnum.ContractPayApply) {
                //        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PayApplyContractUpdate.aspx?id=" + value + "\">修改</a>";
                //    }
                //    else if (item.PayApplySource == FundsEnum.StockPayApply) {
                //        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PayApplyStockUpdate.aspx?id=" + value + "\">修改</a>";
                //    }
                //    else if (item.PayApplySource == FundsEnum.InvoicePayApply) {
                //        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PayApplyInvoiceUpdate.aspx?id=" + value + "\">修改</a>";
                //    }
                //}
                //else {
                //    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //}
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
                  { text: "申请时间", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请编号", datafield: "ApplyNo" },
                  { text: "付款事项", datafield: "PayMatterName" },
                  { text: "付款方式", datafield: "PayModeName" },
                  { text: "收款公司", datafield: "RecCorpName" },
                  { text: "申请金额", datafield: "ApplyBala" },
                  { text: "已付金额", datafield: "SumPayBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "申请人", datafield: "Name" },
                  { text: "申请部门", datafield: "DeptName" },
                  { text: "申请状态", datafield: "StatusName" },
                  { text: "操作", datafield: "PayApplyId", cellsrenderer: cellsrenderer, width: 100, sortable: false, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {

                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                var applyDept = $("#selApplyDept").val();
                var recCorp = $("#selOutCorp").val();
                var status = $("#selStatus").jqxDropDownList("val");

                source.url = "Handler/PayApplyListHandler.ashx?s=" + status + "&ad=" + applyDept + "&rc=" + recCorp + "&fd=" + fromDate + "&td=" + toDate;
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
                    <span style="float: left;">申请部门：</span>
                    <div id="selApplyDept" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">收款公司：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">申请状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PayApplyMulityContractList.aspx" id="btnContractAdd">付款申请</a>
                    <%--<a target="_self" runat="server" href="PayApplyStockCreate.aspx" id="btnStockAdd">库存项下付款申请</a>--%>
                    <a target="_self" runat="server" href="PayApplyInvoiceCreate.aspx" id="btnInvoiceAdd">价外票付款申请</a>
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
