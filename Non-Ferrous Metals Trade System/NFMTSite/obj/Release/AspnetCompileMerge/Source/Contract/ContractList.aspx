<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractList.aspx.cs" Inherits="NFMTSite.Contract.ContractList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约列表</title>
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

            $("#txbContractNo").jqxInput({ height: 23, width: 120 });

            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });

            $("#btnAddContract").jqxLinkButton({ height: 25, width: 120 });
            $("#btnAddByStock").jqxLinkButton({ height: 25, width: 120 });
            $("#btnContractOutAdd").jqxLinkButton({ height: 25, width: 120 });

            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var corpSource = { datatype: "json", url: corpUrl, async: false };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({
                source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25
            });
            var emptyItem = { CorpName: " ", CorpId: 0 };
            $("#selOutCorp").jqxComboBox("insertAt", emptyItem, 0);

            CreateStatusDropDownList("selStatus");

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var url = "Handler/ContractListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "ContractId", type: "int" },
                   { name: "CreateFrom", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "ContractNo", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "TradeDirectionName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "ContractWeight", type: "string" },
                   { name: "PriceModeName", type: "string" },
                   { name: "ContractStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "RefId", type: "string" }
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
                sortcolumn: "con.ContractId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "con.ContractId";
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
                if (item.CreateFrom == "<%=(int)NFMT.Common.CreateFromEnum.采购合约库存创建%>") {
                    cellHtml += "<a target=\"_self\" href=\"ContractInDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.CreateFrom == "<%=(int)NFMT.Common.CreateFromEnum.销售合约库存创建%>") {
                    cellHtml += "<a target=\"_self\" href=\"ContractOutDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.CreateFrom == "<%=(int)NFMT.Common.CreateFromEnum.合约直接创建%>") {
                    cellHtml += "<a target=\"_self\" href=\"ContractDetail.aspx?id=" + value + "\">明细</a>";
                }
                else {
                    cellHtml += "<a target=\"_self\" href=\"ContractDetail.aspx?id=" + value + "\">明细</a>";
                }

                if (item.ContractStatus > statusEnum.已关闭 && item.ContractStatus < statusEnum.待审核) {
                    if (item.CreateFrom == "<%=(int)NFMT.Common.CreateFromEnum.采购合约库存创建%>") {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContractInUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.CreateFrom == "<%=(int)NFMT.Common.CreateFromEnum.销售合约库存创建%>") {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContractOutUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContractUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                }
                //else {
                //    cellHtml += "&nbsp";
                //}

                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_blank\" href=\"ContractExport.aspx?id=" + value + "\">预览</a>";

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
                  { text: "签订日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                  { text: "内部合约号", datafield: "ContractNo" },
                  { text: "外部合约号", datafield: "OutContractNo" },
                  { text: "购销方向", datafield: "TradeDirectionName" },
                  { text: "我方公司", datafield: "InCorpName" },
                  { text: "对方公司", datafield: "OutCorpName" },
                  { text: "交易品种", datafield: "AssetName" },
                  { text: "合约重量", datafield: "ContractWeight" },
                  { text: "点价方式", datafield: "PriceModeName" },
                  { text: "合约状态", datafield: "StatusName" },
                  { text: "操作", datafield: "ContractId", cellsrenderer: cellsrenderer, width: 120, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");
                var corpId = $("#selOutCorp").jqxComboBox("val");
                var contractNo = $("#txbContractNo").val();
                var status = $("#selStatus").jqxDropDownList("val");

                source.url = "Handler/ContractListHandler.ashx?cn=" + contractNo + "&ci=" + corpId + "&db=" + fromDate + "&de=" + toDate + "&s=" + status;
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
                    <span>外部合约号：</span>
                    <span>
                        <input type="text" id="txbContractNo" /></span>
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
                    <span style="float: left;">合约状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="ContractCreate.aspx" id="btnAddContract">新建合约</a>
                    <a target="_self" runat="server" href="SubStockInList.aspx" id="btnAddByStock">库存创建采购合约</a>
                    <a target="_self" runat="server" href="ContractOutStockList.aspx" id="btnContractOutAdd">库存创建销售合约</a>
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
