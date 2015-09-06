<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotList.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款分配</title>
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

            //分配人
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlEmpId").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25, autoDropDownHeight: true });

            //收款状态
            CreateStatusDropDownList("ddlAllotStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 });

            //绑定Grid
            var url = "Handler/CashInAllotListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "AllotId", type: "int" },
                   { name: "AllotTime", type: "date" },
                   { name: "AlloterName", type: "string" },
                   { name: "AllotDesc", type: "string" },
                   { name: "AllotBala", type: "string" },
                   { name: "AllotType", type: "int" },
                   { name: "AllotStatus", type: "int" },
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
                sortcolumn: "cia.AllotId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cia.AllotId";
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
                if (item.AllotType == CashInAllotEnum.Corp) {
                    cellHtml += "<a target=\"_self\" href=\"CashInAllotCorpDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.AllotType == CashInAllotEnum.Contract) {
                    cellHtml += "<a target=\"_self\" href=\"CashInAllotContractDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.AllotType == CashInAllotEnum.Stock) {
                    cellHtml += "<a target=\"_self\" href=\"CashInAllotStockDetail.aspx?id=" + value + "\">明细</a>";
                }

                if (item.AllotStatus > statusEnum.已关闭 && item.AllotStatus < statusEnum.待审核) {
                    if (item.AllotType == CashInAllotEnum.Corp) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CashInAllotCorpUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.AllotType == CashInAllotEnum.Contract) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CashInAllotContractUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.AllotType == CashInAllotEnum.Stock) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CashInAllotStockUpdate.aspx?id=" + value + "\">修改</a>";
                    }
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
                  { text: "分配日期", datafield: "AllotTime", cellsformat: "yyyy-MM-dd" },
                  { text: "分配人", datafield: "AlloterName" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "备注", datafield: "AllotDesc" },
                  { text: "分配状态", datafield: "StatusName" },
                  { text: "操作", datafield: "AllotId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var empId = $("#ddlEmpId").val();
                var status = $("#ddlAllotStatus").val();
                source.url = "Handler/CashInAllotListHandler.ashx?e=" + empId + "&s=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>

</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 10px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">分配人：</span>
                    <div id="ddlEmpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">收款分配状态：</span>
                    <div id="ddlAllotStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="CashInContractList.aspx" id="btnAdd">收款分配</a>
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
