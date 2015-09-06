﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptDetail.aspx.cs" Inherits="NFMTSite.User.DeptDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部门明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.dept.DataBaseName%>" + "&t=" + "<%=this.dept.TableName%>" + "&id=" + "<%=this.id%>";

        $(document).ready(function () {

            $("#jqxExpander").jqxExpander({ width: "98%" });
            

            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnFreeze").jqxInput();
            $("#btnUnFreeze").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 26,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            var id = $("#hidId").val();

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/DeptStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "DepartmentList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/DeptStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "DepartmentList.aspx";
                    }
                );
            });

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结？")) { return; }
                var operateId = operateEnum.冻结;
                $.post("Handler/DeptStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "DepartmentList.aspx";
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                if (!confirm("确认解除冻结？")) { return; }
                var operateId = operateEnum.解除冻结;
                $.post("Handler/DeptStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "DepartmentList.aspx";
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            部门明细
                        <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>所属公司：</span></h4>
                    <span id="ddlCorpId" runat="server"></span>
                </li>
                <li>
                    <h4><span>部门编号：</span></h4>
                    <input type="hidden" runat="server" id="hidId" />
                    <span id="txbDeptCode" runat="server" /></li>
                <li>
                    <h4><span>部门名称：</span></h4>
                    <span id="txbDeptName" runat="server" /></li>
                <li>
                    <h4><span>部门全称：</span></h4>
                    <span id="txbDeptFullName" runat="server" /></li>
                <li>
                    <h4><span>部门缩写：</span></h4>
                    <span id="txbDeptShort" runat="server" /></li>
                <li>
                    <h4><span>部门类型：</span></h4>
                    <span id="ddlDeptType" runat="server" />
                </li>
                <li>
                    <h4><span>上级部门名称：</span></h4>
                    <span id="ddlParentDeptName" runat="server"></span>
                </li>
                <%--<li>
                                <h4><span>部门级别：</span></h4>
                                <span id="ddlDeptLevel" runat="server"></span>
                            </li>--%>

                <%--<li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span><input type="button" id="btnFreeze" value="冻结" runat="server" style="margin-left: 35px" /></span>
                    <span><input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="margin-left: 35px" /></span>
                </li>--%>
            </ul>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnFreeze" value="冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
