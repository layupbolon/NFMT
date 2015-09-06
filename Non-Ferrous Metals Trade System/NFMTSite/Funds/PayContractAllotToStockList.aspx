<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayContractAllotToStockList.aspx.cs" Inherits="NFMTSite.Funds.PayContractAllotToStockList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务款分配至库存列表</title>
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

            var status = $("#ddlAllotStatus").val();

            //绑定Grid
            var url = "Handler/PayContractAllotToStockListHandler.ashx?s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            source =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "ApplyNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "PayBala", type: "string" },
                   { name: "FundsBala", type: "string" },
                   { name: "VirtualBala", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "DetailStatus", type: "int" }
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
                sortcolumn: "psd.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "psd.DetailId";
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
                //cellHtml += "<a target=\"_self\" href=\"PayContractAllotToStockDetail.aspx?id=" + value + "\">明细</a>";
                //cellHtml += "&nbsp;&nbsp;&nbsp";

                if (item.DetailStatus != statusEnum.已作废) {
                    cellHtml += "<a target=\"_self\" href=\"PayContractAllotToStockUpdate.aspx?id=" + value + "\">修改</a>";
                    cellHtml += "&nbsp;&nbsp;&nbsp<a href=\"javascript:void(0)\" onclick=\"Invalid(" + value + ")\">作废</a>";
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
                  { text: "付款申请号", datafield: "ApplyNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "付款金额", datafield: "PayBala" },
                  //{ text: "财务付款金额", datafield: "FundsBala" },
                  { text: "虚拟付款金额", datafield: "VirtualBala" },
                  { text: "分配状态", datafield: "StatusName" },
                  { text: "操作", datafield: "DetailId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var status = $("#ddlAllotStatus").val();
                source.url = "Handler/PayContractAllotToStockListHandler.ashx?s=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });

        function Invalid(id) {
            if (!confirm("是否作废该行数据？")) return;

            $.post("Handler/PayContractAllotToStockInvalidHandler.ashx", { id: id },
                    function (result) {
                        alert(result);
                        var status = $("#ddlAllotStatus").val();
                        source.url = "Handler/PayContractAllotToStockListHandler.ashx?s=" + status;
                        $("#jqxGrid").jqxGrid("updatebounddata", "rows");
                    }
                );
        }
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
                    <span style="float: left;">分配状态：</span>
                    <div id="ddlAllotStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PayContractAllotPayContractList.aspx" id="btnAdd">合约款分配至库存</a>
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
