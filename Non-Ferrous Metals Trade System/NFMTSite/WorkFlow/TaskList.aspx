<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="NFMTSite.WorkFlow.TaskList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待审核任务列表</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" type="text/css" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            $("#txbTaskName").jqxInput({ height: 22, width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });

            var empId = $("#hidEmpId").val();

            var taskName = $("#txbTaskName").val();
            var taskStatus = statusEnum.待审核;
            var url = "Handler/TaskListHandler.ashx?k=" + taskName + "&s=" + taskStatus + "&uid=" + empId;
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datafields:
                [
                   { name: "TaskId", type: "int" },
                   { name: "TaskNodeId", type: "int" },
                   { name: "TaskName", type: "string" },
                   { name: "TaskConnext", type: "string" },
                   { name: "Name", type: "string" },
                   { name: "ApplyTime", type: "date" },
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
                sortcolumn: "tn.TaskNodeId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "tn.TaskNodeId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"TaskDetailWithAttach.aspx?NodeId=" + value + "\">明细</a> ";
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
                      { text: "任务名称", datafield: "TaskName" },
                      { text: "任务内容", datafield: "TaskConnext" },
                      { text: "提交人", datafield: "Name" },
                      { text: "提交时间", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd HH:mm:ss" },                      
                      { text: "操作", datafield: "TaskNodeId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            function LoadData() {
                var taskName = $("#txbTaskName").val();
                var taskStatus = statusEnum.待审核;
                source.url = "Handler/TaskListHandler.ashx?k=" + taskName + "&s=" + taskStatus + "&uid=" + empId;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            }

            $("#btnSearch").click(LoadData);

        });
    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div id="SearchDiv">
            <ul>
                <li>
                    <span>任务名称：</span>
                    <span>
                        <input type="text" id="txbTaskName" /></span>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidEmpId" runat="server" />
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
