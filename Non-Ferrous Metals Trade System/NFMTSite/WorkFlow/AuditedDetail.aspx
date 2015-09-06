<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditedDetail.aspx.cs" Inherits="NFMTSite.WorkFlow.AuditedDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>已审核任务明细</title>
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
            $("#jqxViewExpander").jqxExpander({ width: "98%", height: "430px" });

            var taskId = $("#hidtaskId").val();

            var url = "Handler/TaskDetailHandler.ashx?type=1&tid=" + taskId + "&hasAttach=" + "<%=this.hasAttachs%>";
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datafields:
                [
                   { name: "TaskNodeId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "LogTime", type: "date" },
                   { name: "LogResult", type: "string" },
                   { name: "Memo", type: "string" },
                   <%=this.GetDatafields(this.attachs)%>
                ],
                datatype: "json",
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
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                      { text: "审核人", datafield: "Name", width: 100, cellsalign: "center", align: "center" },
                      { text: "审核时间", datafield: "LogTime", cellsformat: "yyyy-MM-dd HH:mm:ss", width: 160, cellsalign: "center", align: "center" },
                      { text: "审核结果", datafield: "LogResult", width: 90, cellsalign: "center", align: "center" },
                      { text: "审核附言", datafield: "Memo", cellsalign: "center", align: "center" },
                      <%=this.GetColumns(this.attachs)%>
                ],
                columngroups:
                [
                    { text: "附件", align: "center", name: "AttachDetails" }
                ]
            });

            //openwindow = function () {
            //    var url = $("#hidurl").val();
            //    ShowModelessDialog(url);
            //};
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已审核任务明细
                        <input type="hidden" id="hidtaskId" runat="server" />
            <input type="hidden" id="hidurl" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>任务名称：</span></h4>
                    <span id="spTaskName" runat="server"></span></li>
                <li>
                    <h4><span>任务提交人：</span></h4>
                    <span id="spTaskApplyPerson" runat="server"></span></li>
                <li>
                    <h4><span>任务提交时间：</span></h4>
                    <span id="spTaskApplyTime" runat="server"></span></li>
                <li>
                    <h4><span>任务状态：</span></h4>
                    <span id="spTaskStatus" runat="server"></span></li>
                <li>
                    <h4><span>任务备注：</span></h4>
                    <span id="spTaskMemo" runat="server"></span></li>
                <%--<li>
                    <h4><span>业务数据明细：</span></h4>
                    <span><a href="javascript:void(0);" onclick="openwindow();">任务关联业务数据明细</a></span>
                </li>--%>
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
