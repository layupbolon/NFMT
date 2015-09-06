<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotContractList.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotContractList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约收款分配</title>
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

            //收款状态
            CreateStatusDropDownList("ddlAllotStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 });

            //绑定Grid
            var url = "Handler/CashInAllotContractListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "SubId", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "SubNo", type: "string" },
                   { name: "InCorpId", type: "int" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpId", type: "int" },
                   { name: "OutCorpName", type: "string" },
                   { name: "SumBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "SubStatus", type: "int" },
                   { name: "StatusName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
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
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"CashInAllotContractDetail.aspx?id=" + value + "\">明细</a>";
                //if (item.SubStatus > statusEnum.已关闭 && item.SubStatus < statusEnum.待审核) {
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CashInAllotContractUpdate.aspx?id=" + value + "\">修改</a>";
                //}
                //else {
                //    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //}
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
                  { text: "合约日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "我方抬头", datafield: "InCorpName" },
                  { text: "对方抬头", datafield: "OutCorpName" },
                  { text: "分配金额", datafield: "SumBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "合约状态", datafield: "StatusName" },
                  { text: "操作", datafield: "SubId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var status = $("#ddlAllotStatus").val();
                source.url = "Handler/CashInAllotContractListHandler.ashx?s=" + status;
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
                    <span style="float: left;">子合约状态：</span>
                    <div id="ddlAllotStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="CashInAllotReadyContractList.aspx" id="btnAdd">收款分配</a>
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
