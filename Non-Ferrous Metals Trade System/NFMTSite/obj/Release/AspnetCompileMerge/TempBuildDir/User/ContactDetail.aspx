<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactDetail.aspx.cs" Inherits="NFMTSite.User.ContactDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>联系人明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.contact.DataBaseName%>" + "&t=" + "<%=this.contact.TableName%>" + "&id=" + "<%=this.id%>";

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
                    mid: 29,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/ContactStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "ContactList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/ContactStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "ContactList.aspx";
                    }
                );
            });

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结？")) { return; }
                var operateId = operateEnum.冻结;
                $.post("Handler/ContactStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "ContactList.aspx";
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                if (!confirm("确认解除冻结？")) { return; }
                var operateId = operateEnum.解除冻结;
                $.post("Handler/ContactStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "ContactList.aspx";
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
            联系人明细
                        <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>联系人姓名：</span></h4>
                    <span id="txbContactName" runat="server" /></li>
                <li>
                    <h4><span>身份证号：</span></h4>
                    <span id="txbContactCode" runat="server" /></li>
                <li>
                    <h4><span>电话：</span></h4>
                    <span id="txbContactTel" runat="server" /></li>
                <li>
                    <h4><span>传真：</span></h4>
                    <span id="txbContactFax" runat="server" /></li>
                <li>
                    <h4><span>地址：</span></h4>
                    <span id="txbContactAddress" runat="server" /></li>
                <li>
                    <h4><span>公司：</span></h4>
                    <span id="ddlCorpId" runat="server"></span></li>
                <li>
                    <h4><span>联系人状态：</span></h4>
                    <span id="ContactStatus" runat="server"></span></li>
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
