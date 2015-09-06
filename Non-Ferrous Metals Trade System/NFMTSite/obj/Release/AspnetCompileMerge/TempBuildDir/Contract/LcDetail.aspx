<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LcDetail.aspx.cs" Inherits="NFMTSite.Contract.LcDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>信用证明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.auditProgressModel.DataBaseName%>" + "&t=" + "<%=this.auditProgressModel.TableName%>" + "&id=" + "<%=this.lcId%>";

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            

            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnFreeze").jqxInput();
            $("#btnUnFreeze").jqxInput();

            var id = $("#hidId").val();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 1,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/LcStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "LcList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/LcStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "LcList.aspx";
                    }
                );
            });

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结？")) { return; }
                var operateId = operateEnum.冻结;
                $.post("Handler/LcStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "LcList.aspx";
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                if (!confirm("确认解除冻结？")) { return; }
                var operateId = operateEnum.解除冻结;
                $.post("Handler/LcStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "LcList.aspx";
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
            信用证明细
            <input type="hidden" id="hidId" runat="server" />
            <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>开证行：</span></h4>
                    <span id="ddlIssueBank" runat="server"></span>
                </li>
                <li>
                    <h4><span>通知行：</span></h4>
                    <span id="ddlAdviseBank" runat="server"></span>
                </li>
                <li>
                    <h4><span>开证日期：</span></h4>
                    <span id="dtIssueDate" runat="server"></span>
                </li>
                <li>
                    <h4><span>远期天数：</span></h4>
                    <span id="nbFutureDay" runat="server"></span>
                </li>
                <li>
                    <h4><span>信用证金额：</span></h4>
                    <span id="nbLcBala" runat="server"></span>
                </li>
                <li>
                    <h4><span>信用证币种：</span></h4>
                    <span id="ddlCurrency" runat="server"></span>
                </li>
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
