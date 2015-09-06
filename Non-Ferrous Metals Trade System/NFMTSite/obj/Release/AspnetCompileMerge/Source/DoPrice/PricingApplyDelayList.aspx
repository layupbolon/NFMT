<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingApplyDelayList.aspx.cs" Inherits="NFMTSite.DoPrice.PricingApplyDelayList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价申请延期列表</title>
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

            //子合约号
            $("#txbSubNo").jqxInput({ height: 25, width: 120 });

            //点价申请状态
            CreateStatusDropDownList("ddlDetailStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 });

            //绑定Grid
            var subNo = $("#txbSubNo").val();
            var status = $("#ddlPricingStatus").val();
            var url = "Handler/PricingApplyDelayListHandler.ashx?subNo=" + subNo + "&status=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "DelayId", type: "int" },
                   { name: "ApplyNo", type: "string" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "DelayFee", type: "string" },
                   { name: "PricingApplyWeight", type: "string" },
                   { name: "DelayAmount", type: "string" },
                   { name: "DelayQP", type: "date" },
                   { name: "StatusName", type: "string" },
                   { name: "DetailStatus", type: "int" },
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "pad.DelayId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pad.DelayId";
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
                cellHtml += "<a target=\"_self\" href=\"PricingApplyDelayDetail.aspx?id=" + value + "\">查看</a>"
                if (item.DetailStatus > statusEnum.已关闭 && item.DetailStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PricingApplyDelayUpdate.aspx?id=" + value + "\">修改</a>"
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
                  { text: "点价申请编号", datafield: "ApplyNo" },
                  { text: "合约号", datafield: "ContractNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "点价申请重量", datafield: "PricingApplyWeight" },
                  { text: "延期重量", datafield: "DelayAmount" },
                  { text: "延期费", datafield: "DelayFee" },
                  { text: "延期Qp", datafield: "DelayQP", cellsformat: "yyyy-MM-dd" },
                  { text: "点价申请延期状态", datafield: "StatusName" },
                  { text: "操作", datafield: "DelayId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            $("#btnSearch").click(function () {
                var subNo = $("#txbSubNo").val();
                var status = $("#ddlPricingStatus").val();
                source.url = "Handler/PricingApplyDelayListHandler.ashx?subNo=" + subNo + "&status=" + status;
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
                    <span style="float: left;">合约号：</span>
                    <input type="text" id="txbSubNo" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">点价申请延期状态：</span>
                    <div id="ddlDetailStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PricingApplyDelayPricingApplyList.aspx" id="btnAdd">新增点价申请延期</a>
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