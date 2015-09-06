<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDetailWithAttach.aspx.cs" Inherits="NFMTSite.WorkFlow.TaskDetailWithAttach" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

            $("#mainSplitter").jqxSplitter({ width: "100%", height: document.documentElement.clientHeight - 50, orientation: "vertical", panels: [{ size: "71%", collapsible: false }] });

            $("#jqxExpander").jqxExpander({ width: "100%" });
            $("#jqxViewExpander").jqxExpander({ width: "100%", height: document.documentElement.clientHeight - 60 });

            /////////////////////////附件 start/////////////////////////
            var attachUrl = "../Files/Handler/GetAttachListHandler.ashx?cid=" + "<%=this.dataSource.RowId%>" + "&s=" + statusEnum.已生效 + "&t=" + "<%=this.attachTypeId%>";

            var attachFormatedData = "";
            var attachTotalrecords = 0;
            var attachSource =
            {
                url: attachUrl,
                datafields: [
                    { name: "AttachId", type: "int" },
                    { name: "AttachName", type: "string" },
                    { name: "AttachInfo", type: "string" },
                    { name: "AttachType", type: "int" },
                    { name: "CreateTime", type: "date" },
                    { name: "AttachPath", type: "string" },
                    { name: "AttachExt", type: "string" },
                    { name: "AttachStatus", type: "int" },
                    { name: "ServerAttachName", type: "string" },
                    { name: "check", type: "bool" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxAttachGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    attachTotalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "ca.AttachId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ca.AttachId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    attachFormatedData = buildQueryString(data);
                    return attachFormatedData;
                }
            };

            var attachDataAdapter = new $.jqx.dataAdapter(attachSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var attachViewRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">";
                cellHtml += "<a href=\"../Files/FileDownLoad.aspx?id=" + item.AttachId + "\" title=\"" + item.AttachName + "\" >下载</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxAttachGrid").jqxGrid(
            {
                width: "98%",
                source: attachDataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                editable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "附件上传日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd", editable: false, cellsalign: "center", align: "center" },
                  { text: "附件名字", datafield: "AttachName", editable: false, cellsalign: "center", align: "center" },
                  { text: "查看", datafield: "AttachPath", cellsrenderer: attachViewRender, editable: false, cellsalign: "center", align: "center" },
                  { text: "审核附件", datafield: "check", columntype: "checkbox", sortable: false, cellsalign: "center", align: "center" }
                ]
            });

            $("#jqxAttachGrid").jqxGrid("refresh");

            /////////////////////////附件 end/////////////////////////


            $("#txbAuditMemo").jqxInput({ height: 25, width: "80%" });
            $("#btnPass").jqxButton({ width: "35%" });
            $("#btnRefuse").jqxButton({ width: "30%" });
            $("#btnNotify").jqxButton({ width: "30%" });
            $("#BtnReturn").jqxButton({ width: "40%" });

            var id = $("#hidId").val();

            var taskId = $("#hidtaskId").val();

            var url = "Handler/TaskDetailHandler.ashx?type=0&tid=" + taskId + "&hasAttach=" + "<%=this.hasAttachs%>";
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
                //virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                columnsresize: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                      { text: "审核人", datafield: "Name", width: "15%", cellsalign: "center", align: "center" },
                      { text: "审核时间", datafield: "LogTime", cellsformat: "yyyy-MM-dd HH:mm:ss", width: "35%", cellsalign: "center", align: "center" },
                      { text: "审核结果", datafield: "LogResult", width: "20%", cellsalign: "center", align: "center" },
                      { text: "审核附言", datafield: "Memo", cellsalign: "center", align: "center" },
                      <%=this.GetColumns(this.attachs)%>
                ],
                columngroups:
                [
                    { text: "附件", align: "center", name: "AttachDetails" }
                ]
            });

            $("#btnPass").on("click", function () {

                var rows = $("#jqxAttachGrid").jqxGrid("getrows");
                var aids = "";
                if (rows != null && rows != undefined && rows.length > 0) {
                    for (var item in rows) {
                        if (rows[item].check) {
                            aids += rows[item].AttachId + ",";
                        }
                    }
                    if (aids != "")
                        aids = aids.substring(0, aids.length - 1);
                    else {
                        alert("请勾选已审核的附件！");
                        return;
                    }
                }

                if (!confirm("确认审核通过？")) {
                    return;
                }
                $("#btnPass").jqxButton({ disabled: true });

                var logResult = "审核通过";
                var isPass = true;
                $.post("Handler/TaskAuditHandler.ashx",
                    {
                        logResult: logResult,
                        isPass: isPass,
                        id: id,
                        memo: $("#txbAuditMemo").val(),
                        aids: aids
                    },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "TaskList.aspx";
                        }
                        $("#btnPass").jqxButton({ disabled: false });
                    }
                );
            });

            $("#btnRefuse").on("click", function () {
                if (!confirm("确认审核不通过？")) {
                    return;
                }
                $("#btnRefuse").jqxButton({ disabled: true });

                var logResult = "审核不通过";
                var isPass = false;
                $.post("Handler/TaskAuditHandler.ashx", { logResult: logResult, isPass: isPass, id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "TaskList.aspx";
                        }
                        $("#btnPass").jqxButton({ disabled: false });
                    }
                );
            });

            $("#btnNotify").on("click", function () {
                $("#btnNotify").jqxButton({ disabled: true });

                $.post("Handler/TaskNotifyHandler.ashx", { logResult: "知会", id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "TaskList.aspx";
                        }
                        $("#btnNotify").jqxButton({ disabled: false });
                    }
                );
            });

            $("#BtnReturn").on("click", function () {
                if (!confirm("确定退回上一级？")) {
                    return;
                }
                $("#BtnReturn").jqxButton({ disabled: true });

                $.post("Handler/TaskNodeReturnHandler.ashx", { logResult: "退回上一节点", id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "TaskList.aspx";
                        }
                        $("#BtnReturn").jqxButton({ disabled: false });
                    }
                );
            });

            openwindow = function () {
                var url = $("#hidurl").val();
                ShowModelessDialog(url);
            };

            if ("<%=this.nodeType%>" == "<%=NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.NodeType)["Audit"].StyleDetailId%>") {
                document.getElementById("mainUl").removeChild(document.getElementById("NotifyLi"));
            }
            if ("<%=this.nodeType%>" == "<%=NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.NodeType)["Notify"].StyleDetailId%>") {
                document.getElementById("mainUl").removeChild(document.getElementById("AuditLi"));
                document.getElementById("jqxAttachGrid").style.display = "none";
            }
        });
    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />

    <div style="width: 100%; margin-top: 10px; float: left;">
        <div id="mainSplitter">
            <div class="SplitterDiv">
                <div id="jqxViewExpander" style="float: left;">
                    <div>
                        业务数据明细
                    </div>
                    <div>
                        <iframe src="<%=this.viewUrl%>" style="width: 100%; height: 99%; border: none; margin: 0px 0px 0px 0px" frameborder="no"></iframe>
                    </div>
                </div>
            </div>

            <div class="SplitterDiv" style="margin-left: 0px; padding: 0px 0px 0px 0px;">
                <div id="jqxExpander">
                    <div>
                        待审核任务明细
                        <input type="hidden" id="hidId" runat="server" />
                        <input type="hidden" id="hidtaskId" runat="server" />
                        <input type="hidden" id="hidurl" runat="server" />
                    </div>
                    <div id="layOutDiv">
                        <ul id="mainUl">
                            <li>
                                <span>提交人：</span>
                                <span id="spTaskApplyPerson" runat="server"></span>
                            </li>
                            <li>
                                <span>提交时间：</span>
                                <span id="spTaskApplyTime" runat="server"></span>
                            </li>
                            <li>
                                <span>任务名称：</span>
                                <span id="spTaskName" runat="server"></span>
                            </li>
                            <li>
                                <span>备注：</span>
                                <span id="spTaskMemo" runat="server"></span>
                            </li>
                            <li style="height: auto;">
                                <span>附言：</span>
                                <input type="text" id="txbAuditMemo" runat="server" />
                            </li>
                            <li id="AuditLi">
                                <input type="button" id="btnPass" value="审核通过" runat="server" style="margin-left: 10px" />
                                <input type="button" id="btnRefuse" value="拒绝" runat="server" style="margin-left: 10px" />
                                <input type="button" id="BtnReturn" value="退回上一节点" runat="server" style="margin-left: 0px; margin-bottom: 5px;" />
                            </li>
                            <li id="NotifyLi">
                                <input type="button" id="btnNotify" value="知会确认" runat="server" style="margin-left: 35px" />
                            </li>
                        </ul>
                        <div id="jqxGrid" style="float: left; margin: 15px 0 0 5px;"></div>
                        <div id="jqxAttachGrid" style="float: left; margin: 15px 0 0 5px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
