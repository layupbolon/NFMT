<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepoList.aspx.cs" Inherits="NFMTSite.WareHouse.RepoList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回购</title>
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

            //回购人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlMover").jqxComboBox({ source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 120, height: 25 });

            //回购状态
            CreateStatusDropDownList("ddlMoveStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var mover = $("#ddlMover").val();
            var status = $("#ddlMoveStatus").val();
            var url = "Handler/RepoListHandlr.ashx?m=" + mover + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "RepoId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "RepoerTime", type: "date" },
                   { name: "RepoStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "Memo", type: "string" }
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
                sortcolumn: "r.RepoId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "r.RepoId";
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
                cellHtml += "<a target=\"_self\" href=\"RepoDetail.aspx?id=" + value + "\">明细</a>"

                if (item.RepoStatus > statusEnum.已作废 && item.RepoStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"RepoUpdate.aspx?id=" + value + "\">修改</a>"
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                cellHtml += "</div>";

                return cellHtml;

                //return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">"
                //+ "<a target=\"_self\" href=\"RepoDetail.aspx?id=" + value + "\">查看</a>&nbsp;&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"RepoUpdate.aspx?id=" + value + "\">修改</a>"
                //+ "</div>";
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
                  { text: "回购人", datafield: "Name" },
                  { text: "回购时间", datafield: "RepoerTime", cellsformat: "yyyy-MM-dd" },
                  { text: "回购附言", datafield: "Memo" },
                  { text: "回购状态", datafield: "StatusName" },
                  { text: "操作", datafield: "RepoId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var mover = $("#ddlMover").val();
                var status = $("#ddlMoveStatus").val();
                source.url = "Handler/RepoListHandlr.ashx?m=" + mover + "&s=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>

</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
                        <input type="hidden" id="hidBDStyleId" runat="server" />
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">回购人：</span>
                    <div id="ddlMover" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">回购状态：</span>
                    <div id="ddlMoveStatus" style="float: left;"></div>
                </li>
                <%--<li>
                    <span style="float: left;">交货地：</span>
                    <div id="ddlDeliverPlaceId" style="float: left;"></div>
                </li>--%>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="CanRepoApplyList.aspx" id="btnAdd">回购</a>
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
