<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDetail.aspx.cs" Inherits="NFMTSite.User.EmployeeDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>员工明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.emp.DataBaseName%>" + "&t=" + "<%=this.emp.TableName%>" + "&id=" + "<%=this.id%>";

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            
            $("#btnReset").jqxInput();
            $("#btnReset").on("click", function () {
                if (!confirm("确认重置密码？")) { return; }
                $.post("Handler/ResetPasswordHandler.ashx", { id: "<%=id%>" },
                    function (result) {
                        alert(result);
                        document.location.href = "EmployeeList.aspx";
                    }
                );
            });
            //$("#btnAudit").jqxInput({ disabled: true });
            //$("#btnInvalid").jqxInput({ disabled: true });
            //$("#btnGoBack").jqxInput({ disabled: true });
            //$("#btnFreeze").jqxInput({ disabled: true });
            //$("#btnUnFreeze").jqxInput({ disabled: true });

            //var id = $("#hidId").val();

            //$("#btnAudit").on("click", function (e) {
            //    var paras = {
            //        mid: 27,
            //        model: $("#hidModel").val()
            //    };
            //    ShowModalDialog(paras, e);
            //});

            //$("#btnInvalid").on("click", function () {
            //    if (!confirm("确认作废？")) { return; }
            //    var operateId = operateEnum.作废;
            //    $.post("Handler/EmpStatusHandler.ashx", { id: id, oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "EmployeeList.aspx";
            //        }
            //    );
            //});

            //$("#btnGoBack").on("click", function () {
            //    if (!confirm("确认撤返？")) { return; }
            //    var operateId = operateEnum.撤返;
            //    $.post("Handler/EmpStatusHandler.ashx", { id: id, oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "EmployeeList.aspx";
            //        }
            //    );
            //});

            //$("#btnFreeze").on("click", function () {
            //    if (!confirm("确认冻结？")) { return; }
            //    var operateId = operateEnum.冻结;
            //    $.post("Handler/EmpStatusHandler.ashx", { id: id, oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "EmployeeList.aspx";
            //        }
            //    );
            //});

            //$("#btnUnFreeze").on("click", function () {
            //    if (!confirm("确认解除冻结？")) { return; }
            //    var operateId = operateEnum.解除冻结;
            //    $.post("Handler/EmpStatusHandler.ashx", { id: id, oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "EmployeeList.aspx";
            //        }
            //    );
            //});
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            员工明细
                        <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>所属部门：</span></h4>
                    <span id="ddlDeptId" runat="server"></span>
                </li>
                <li>
                    <h4><span>员工编号：</span></h4>
                    <span id="txbEmpCode" runat="server" /></li>
                <li>
                    <h4><span>姓名：</span></h4>
                    <span id="txbEmpName" runat="server" /></li>
                <li>
                    <h4><span>性别：</span></h4>
                    <span id="rdMale" runat="server"></span></li>
                <li>
                    <h4><span>生日：</span></h4>
                    <span id="dtBirthday" runat="server"></span></li>
                <li>
                    <h4><span>手机号码：</span></h4>
                    <span id="txbTel" runat="server" />
                </li>
                <li>
                    <h4><span>座机号码：</span></h4>
                    <span id="txbPhone" runat="server" />
                </li>
                <li>
                    <h4><span>在职状态：</span></h4>
                    <span id="ddlWorkStatus" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>
    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <%--<input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnFreeze" value="冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;--%>
        <input type="button" id="btnReset" value="重置密码" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <%--<script type="text/javascript" src="../js/AuditProgress.js"></script>--%>
</html>
