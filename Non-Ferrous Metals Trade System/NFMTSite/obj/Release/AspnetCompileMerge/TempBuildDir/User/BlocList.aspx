<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlocList.aspx.cs" Inherits="NFMTSite.BasicData.BlocList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>集团列表</title>
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

            $("#txbBlocName").jqxInput({ height: 23, width: 120 });

            //状态下拉列表绑定
            CreateStatusDropDownList("selStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 70 });


            var blocName = $("#txbBlocName").val();
            var status = $("#selStatus").val();
            var url = "Handler/BlocListHandler.ashx?k=" + blocName + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datatype: "json",
                datafields:
                [
                   { name: "BlocId", type: "int" },
                   { name: "BlocName", type: "string" },
                   { name: "BlocFullName", type: "string" },
                   { name: "BlocEname", type: "string" },
                   { name: "BlocStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "IsSelf", type: "string" }
                ],
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
                sortcolumn: "B.BlocId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "B.BlocId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"BlocDetail.aspx?id=" + value + "\">明细</a>";
                if (item.BlocStatus > statusEnum.已作废 && item.BlocStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"BlocUpdate.aspx?id=" + value + "\">修改</a>";
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                cellHtml += "</div>";
                return cellHtml;
            }

            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                columnsresize: true,
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
                      { text: "集团名称", datafield: "BlocName" },
                      { text: "集团全称", datafield: "BlocFullName" },
                      { text: "集团英文名称", datafield: "BlocEname" },
                      { text: "是否己方集团", datafield: "IsSelf" },
                      { text: "状态", datafield: "StatusName" },
                      { text: "操作", datafield: "BlocId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var blocName = $("#txbBlocName").val();
                var status = $("#selStatus").val();
                source.url = "Handler/BlocListHandler.ashx?k=" + blocName + "&s=" + status;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
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
                    <span>集团名称：</span>
                    <input type="text" id="txbBlocName" />
                </li>
                <li>
                    <span style="float: left;">集团状态：</span>
                    <div id="selStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="BlocCreate.aspx" id="btnAdd">新增集团</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxGrid"></div>
        </div>
    </div>
</body>
</html>
