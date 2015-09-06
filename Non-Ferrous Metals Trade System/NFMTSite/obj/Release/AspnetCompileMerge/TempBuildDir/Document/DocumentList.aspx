<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentList.aspx.cs" Inherits="NFMTSite.Document.DocumentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>制单指令列表</title>
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

            $("#txbOrderNo").jqxInput({ height: 23, width: 120 });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnAddDocument").jqxLinkButton({ height: 25, width: 120 });

            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, searchMode: "containsignorecase"
            });

            CreateStatusDropDownList("selStatus");

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var url = "Handler/DocumentListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "DocumentId", type: "int" },
                   { name: "OrderId", type: "int" },
                   { name: "AcceptanceDate", type: "date" },
                   { name: "Acceptancer", type: "int" },
                   { name: "AccEmpName", type: "string" },
                   { name: "DocumentDate", type: "date" },
                   { name: "DocEmpId", type: "int" },
                   { name: "DocEmpName", type: "string" },
                   { name: "PresentDate", type: "date" },
                   { name: "Presenter", type: "int" },
                   { name: "PreEmpName", type: "string" },
                   { name: "OrderTypeName", type: "string" },
                   { name: "OrderType", type: "int" },
                   { name: "CommercialOrderType", type: "int" },
                   { name: "ApplyCorpName", type: "string" },
                   { name: "ApplyCorp", type: "int" },
                   { name: "BuyCorpName", type: "string" },
                   { name: "BuyerCorp", type: "int" },
                   { name: "OrderNo", type: "string" },
                   { name: "DocumentStatus", type: "int" },
                   { name: "StatusName", type: "string" }
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
                sortcolumn: "doc.DocumentId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "doc.DocumentId";
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

                if (item.OrderType == orderTypeEnum.无配货临票制单指令 || item.OrderType == orderTypeEnum.无配货终票制单指令) {
                    cellHtml += "<a target=\"_self\" href=\"DocumentNoStockDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.OrderType == orderTypeEnum.临票制单指令 || item.OrderType == orderTypeEnum.终票制单指令) {
                    cellHtml += "<a target=\"_self\" href=\"DocumentDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.OrderType == orderTypeEnum.替临制单指令) {
                    if (item.CommercialOrderType == orderTypeEnum.无配货临票制单指令 || item.CommercialOrderType == orderTypeEnum.无配货终票制单指令) {
                        cellHtml += "<a target=\"_self\" href=\"DocumentNoStockDetail.aspx?id=" + value + "\">明细</a>";
                    }
                    else if (item.CommercialOrderType == orderTypeEnum.临票制单指令 || item.CommercialOrderType == orderTypeEnum.终票制单指令) {
                        cellHtml += "<a target=\"_self\" href=\"DocumentDetail.aspx?id=" + value + "\">明细</a>";
                    }
                }

                if (item.DocumentStatus > statusEnum.已关闭 && item.DocumentStatus < statusEnum.待审核) {

                    if (item.OrderType == orderTypeEnum.无配货临票制单指令 || item.OrderType == orderTypeEnum.无配货终票制单指令) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"DocumentNoStockUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.OrderType == orderTypeEnum.临票制单指令 || item.OrderType == orderTypeEnum.终票制单指令) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"DocumentUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.OrderType == orderTypeEnum.替临制单指令) {
                        if (item.CommercialOrderType == orderTypeEnum.无配货临票制单指令 || item.CommercialOrderType == orderTypeEnum.无配货终票制单指令) {
                            cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"DocumentNoStockUpdate.aspx?id=" + value + "\">修改</a>";
                        }
                        else if (item.CommercialOrderType == orderTypeEnum.临票制单指令 || item.CommercialOrderType == orderTypeEnum.终票制单指令) {
                            cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"DocumentUpdate.aspx?id=" + value + "\">修改</a>";
                        }
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
                  { text: "批次号", datafield: "OrderNo" },
                  { text: "指令类型", datafield: "OrderTypeName" },
                  { text: "制单日期", datafield: "DocumentDate", cellsformat: "yyyy-MM-dd" },
                  { text: "制单人", datafield: "DocEmpName" },                  
                  { text: "交单日期", datafield: "PresentDate", cellsformat: "yyyy-MM-dd" },
                  { text: "交单人", datafield: "PreEmpName" },
                  { text: "承兑日期", datafield: "AcceptanceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "承兑确认人", datafield: "AccEmpName" },                  
                  { text: "制单状态", datafield: "StatusName" },
                  { text: "操作", datafield: "DocumentId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                var corpId = $("#selOutCorp").jqxComboBox("val");
                var orderNo = $("#txbOrderNo").val();
                var status = $("#selStatus").jqxDropDownList("val");

                source.url = "Handler/DocumentListHandler.ashx?on=" + orderNo + "&ci=" + corpId + "&db=" + fromDate + "&de=" + toDate + "&s=" + status;
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
                    <span>批次号：</span>
                    <span>
                        <input type="text" id="txbOrderNo" /></span>
                </li>
                <li>
                    <span style="float: left;">对方抬头：</span>
                    <div id="selOutCorp" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">订立日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">制单状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="OrderReadyList.aspx" id="btnAddDocument">新建制单</a>
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