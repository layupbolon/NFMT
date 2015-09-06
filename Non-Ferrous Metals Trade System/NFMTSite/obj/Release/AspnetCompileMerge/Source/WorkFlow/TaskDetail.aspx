<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDetail.aspx.cs" Inherits="NFMTSite.WorkFlow.TaskDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待审核任务明细</title>
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
            $("#jqxViewExpander").jqxExpander({ width: "98%" ,height: "430px"});

            $("#txbAuditMemo").jqxInput({ height: 25, width: 300 });
            $("#btnPass").jqxButton({ width: "80" });
            $("#btnRefuse").jqxButton({ width: "80" });

            var id = $("#hidId").val();

            var taskId = $("#hidtaskId").val();

            var url = "Handler/TaskDetailHandler.ashx?type=0&tid=" + taskId;
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datafields:
                [
                   { name: "TaskNodeId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "LogTime", type: "date" },
                   { name: "LogResult", type: "string" },
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
                      { text: "审核人", datafield: "Name" },
                      { text: "审核时间", datafield: "LogTime", cellsformat: "yyyy-MM-dd HH:mm:ss" },
                      { text: "审核结果", datafield: "LogResult" },
                      { text: "审核附言", datafield: "Memo" }
                ]
            });


            $("#btnPass").on("click", function () {
                $("#btnPass").jqxButton({ disabled: true });
                if (!confirm("确认审核通过？")) {
                    $("#btnPass").jqxButton({ disabled: false });
                    return;
                }
                var logResult = "审核通过";
                var isPass = true;
                $.post("Handler/TaskAuditHandler.ashx", { logResult: logResult, isPass: isPass, id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "TaskList.aspx";
                    }
                );
            });

            $("#btnRefuse").on("click", function () {
                $("#btnRefuse").jqxButton({ disabled: true });
                if (!confirm("确认审核不通过？")) {
                    $("#btnRefuse").jqxButton({ disabled: false });
                    return;
                }
                var logResult = "审核不通过";
                var isPass = false;
                $.post("Handler/TaskAuditHandler.ashx", { logResult: logResult, isPass: isPass, id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "TaskList.aspx";
                    }
                );
            });

            openwindow = function () {
                var url = $("#hidurl").val();
                ShowModelessDialog(url);
            };
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            待审核任务明细
            <input type="hidden" id="hidId" runat="server" />
            <input type="hidden" id="hidtaskId" runat="server" />
            <input type="hidden" id="hidurl" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>任务提交人：</span></h4>
                    <span id="spTaskApplyPerson" runat="server"></span>
                    <h4><span>任务提交时间：</span></h4>
                    <span id="spTaskApplyTime" runat="server"></span>
                </li>
                <li>
                    <h4><span>任务名称：</span></h4>
                    <span id="spTaskName" runat="server"></span></li>
                <li>
                    <h4><span>任务备注：</span></h4>
                    <span id="spTaskMemo" runat="server"></span></li>
                <%--<li>
                    <h4><span>业务数据明细：</span></h4>
                    <span><a href="javascript:void(0);" onclick="openwindow();">任务关联业务数据明细</a></span>
                </li>--%>
                <li>
                    <h4><span>审核附言：</span></h4>
                    <input type="text" id="txbAuditMemo" runat="server" /></li>
                <li>
                    <input type="button" id="btnPass" value="审核通过" runat="server" style="margin-left: 35px" />
                    <input type="button" id="btnRefuse" value="拒绝" runat="server" style="margin-left: 35px" />
                </li>
            </ul>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxViewExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            业务数据明细
        </div>
        <div>
            <iframe src="<%=this.viewUrl%>" style="width: 100%; height: 99%; border: none; margin: 0px 0px 0px 0px" frameborder="no"></iframe>
        </div>
    </div>
</body>
</html>
