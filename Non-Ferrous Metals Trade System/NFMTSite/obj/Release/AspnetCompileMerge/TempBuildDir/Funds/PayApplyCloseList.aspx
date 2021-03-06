﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyCloseList.aspx.cs" Inherits="NFMTSite.Funds.PayApplyCloseList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务付款--付款申请列表</title>
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
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });

            var url = "Handler/PayApplyListHandler.ashx?s=" + statusEnum.已生效;
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
                   { name: "ApplyBala", type: "numeric" },
                   { name: "payBala", type: "numeric" },
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

                var item = $("#jqxgrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                if (item.PayApplySource == FundsEnum.ContractPayApply) {
                    cellHtml += "<a target=\"_self\" href=\"PaymentContractClose.aspx?id=" + value + "\">关闭</a>";
                }
                else if (item.PayApplySource == FundsEnum.StockPayApply) {
                    cellHtml += "<a target=\"_self\" href=\"PaymentStockClose.aspx?id=" + value + "\">关闭</a>";
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
                  { text: "申请时间", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请编号", datafield: "ApplyNo" },
                  { text: "付款事项", datafield: "PayMatterName" },
                  { text: "付款方式", datafield: "PayModeName" },
                  { text: "收款公司", datafield: "RecCorpName" },
                  { text: "申请金额", datafield: "ApplyBala" },
                  { text: "已付金额", datafield: "PayBala" },
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

                source.url = "Handler/PayApplyListHandler.ashx?s=" + statusEnum.已生效 + "&ad=" + applyDept + "&rc=" + recCorp + "&fd=" + fromDate + "&td=" + toDate;
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
