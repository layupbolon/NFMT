<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractMasterList.aspx.cs" Inherits="NFMTSite.BasicData.ContractMasterList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约模板管理</title>
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

            $("#txbMasterName").jqxInput({ height: 23, width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            CreateStatusDropDownList("selStatus");

            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            var n = $("#txbMasterName").val();
            var s = $("#selStatus").val();

            var url = "Handler/ContractMasterListHandler.ashx?n=" + n + "&s=" + s;
            var formatedData = "";
            var totalrecords = 0;

            var source =
            {
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
                sortcolumn: "MasterId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "MasterId";
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
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"MasterClauseCreate.aspx?id=" + value + "\">合约条款列表</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContractMasterView.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContractMasterUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
                //return "&nbsp;&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"ContractClauseList.aspx?masterId=" + value + "\">合约条款列表</a>&nbsp;&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"ContractMasterUpdate.aspx?id=" + value + "\">修改</a>&nbsp;&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"ContractMasterView.aspx?id=" + value + "\">明细</a>";
            }

            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "合约模板名称", datafield: "MasterName" },
                  { text: "合约模板英文名称", datafield: "MasterEname" },
                  { text: "数据状态", datafield: "MasterStatusName" },
                  { text: "查看合约条款明细", datafield: "MasterId", cellsrenderer: cellsrenderer, width: 200, enabletooltips: false }
                ]
            });

            $("#btnSearch").on("click", function () {
                var n = $("#txbMasterName").val();
                var s = $("#selStatus").val();

                source.url = "Handler/ContractMasterListHandler.ashx?n=" + n + "&s=" + s;
                $("#jqxgrid").jqxGrid("gotopage", 0);
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>
</head>
<body>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>模板名称</span>
                    <span>
                        <input type="text" id="txbMasterName" /></span>
                </li>
                <li>
                    <span style="float: left;">模板状态：</span>
                    <div id="selStatus" style="float: left;" />
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="ContractMasterCreate.aspx" id="btnAdd">添加合约模板</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 5px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

</body>
</html>
