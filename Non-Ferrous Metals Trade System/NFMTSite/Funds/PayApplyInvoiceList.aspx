﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyInvoiceList.aspx.cs" Inherits="NFMTSite.Funds.PayApplyInvoiceList" %>

<%@ Register TagName="Menu" TagPrefix="NFMT" Src="~/Control/Menu.ascx" %>
<%@ Register TagName="Tree" TagPrefix="NFMT" Src="~/Control/Tree.ascx" %>
<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请发票列表</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mainSplitter").jqxSplitter({ width: "100%", height: document.documentElement.clientHeight - 70, orientation: "vertical", panels: [{ size: "15%" }, { size: "85%" }] });
            $("#jqxTree").jqxTree();
            $("#jqxMenu").jqxMenu();
            $("#jqNavigation").jqxPanel({ width: "98%", height: 20 });

            $("#jqxExpander").jqxExpander({ width: "98%" });

            $("#txbContractCode").jqxInput({ height: 23, width: 120 });
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });

            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true
            });

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var url = "Handler/PayApplyInvoiceListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
               [
                   { name: "SubId", type: "number" },
                   { name: "ContractDate", type: "date" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "AllotWeigth", type: "string" },
                   { name: "LaveWeight", type: "string" },
                   { name: "StatusName", type: "string" }
               ],
                datatype: "json",
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
                sortcolumn: "cs.SubId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cs.SubId";
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
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\"><a target=\"_self\" href=\"PayApplyContractCreate.aspx?id=" + value + "\">付款申请</a></div>";
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
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "订立日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                  { text: "合约号", datafield: "ContractNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "我方公司", datafield: "InCorpName" },
                  { text: "对方公司", datafield: "OutCorpName" },
                  { text: "交易品种", datafield: "AssetName" },
                  { text: "已申请货量", datafield: "AllotWeigth" },
                  { text: "剩余货量", datafield: "LaveWeight" },
                  { text: "操作", datafield: "SubId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });

            $("#btnSearch").click(function () {
                var subNo = $("#txbContractCode").val();
                var corpId = $("#selOutCorp").jqxComboBox("val");
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                source.url = "Handler/PayApplyContractListHandler.ashx?sn=" + subNo + "&oci=" + corpId + "&fd=" + fromDate + "&td=" + toDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

        });
    </script>
</head>
<body>
    <NFMT:Menu runat="server" ID="menu1" />
    <div style="width: 100%; margin-top: 10px;">
        <div id="mainSplitter">
            <div class="SplitterDiv">
                <NFMT:Tree runat="server" ID="tree1" />
            </div>
            <div class="SplitterDiv">
                <NFMT:Navigation runat="server" ID="navigation1" />

                <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
                    <div>
                        查询条件
                    </div>
                    <div class="SearchExpander">
                        <ul>
                            <li>
                                <span>合约编号</span>
                                <span>
                                    <input type="text" id="txbContractCode" /></span>
                            </li>
                            <li>
                                <span style="float: left;">对方抬头：</span>
                                <div id="selOutCorp" style="float: left;" />
                            </li>
                            <li>
                                <span style="float: left;">合约订立日期：</span>
                                <div id="txbFromCreateDate" style="float: left;"></div>
                                <span style="float: left;">至</span>
                                <div id="txbToCreateDate" style="float: left;"></div>
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
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../js/Sms.js"></script>
</body>
</html>