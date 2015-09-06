<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SplitDocList.aspx.cs" Inherits="NFMTSite.WareHouse.SplitDocList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>拆单</title>
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

            //拆单人
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlSpliter").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25, autoDropDownHeight: true });

            //时间控件
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //原业务单号
            $("#txbOldRefNo").jqxInput({ height: 23, width: 120 });

            //拆单状态
            CreateStatusDropDownList("ddlSplitDocStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var person = $("#ddlSpliter").val();
            var fromdate = $("#txbFromCreateDate").val();
            var todate = $("#txbToCreateDate").val();
            var refno = $("#txbOldRefNo").val();
            var status = $("#ddlSplitDocStatus").val();
            var url = "Handler/SplitDocListHandler.ashx?p=" + person + "&f=" + fromdate + "&t=" + todate + "&r=" + refno + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "SplitDocId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "SplitDocTime", type: "date" },
                   { name: "SplitDocStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "OldRefNo", type: "string" }
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
                sortcolumn: "sp.SplitDocId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sp.SplitDocId";
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

                cellHtml += "<a target=\"_self\" href=\"SplitDocDetail.aspx?id=" + value + "\">查看</a>"

                if (item.SplitDocStatus > statusEnum.已关闭 && item.SplitDocStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"SplitDocUpdate.aspx?id=" + value + "\">修改</a>"
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
                  { text: "拆单人", datafield: "Name" },
                  { text: "拆单时间", datafield: "SplitDocTime", cellsformat: "yyyy-MM-dd" },
                  { text: "原业务单号", datafield: "OldRefNo" },
                  { text: "拆单状态", datafield: "StatusName" },
                  { text: "操作", datafield: "SplitDocId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var person = $("#ddlSpliter").val();
                var fromdate = $("#txbFromCreateDate").val();
                var todate = $("#txbToCreateDate").val();
                var refno = $("#txbOldRefNo").val();
                var status = $("#ddlSplitDocStatus").val();
                source.url = "Handler/SplitDocListHandler.ashx?p=" + person + "&f=" + fromdate + "&t=" + todate + "&r=" + refno + "&s=" + status;
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
                    <span style="float: left;">拆单人：</span>
                    <div id="ddlSpliter" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">拆单日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">原业务单号：</span>
                    <input id="txbOldRefNo" type="text" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">拆单状态：</span>
                    <div id="ddlSplitDocStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="SplitDocStockList.aspx" id="btnAdd">拆单</a>
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
