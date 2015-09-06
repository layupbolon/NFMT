<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableDetail.aspx.cs" Inherits="NFMTSite.Funds.ReceivableDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款登记明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.receivable.DataBaseName%>" + "&t=" + "<%=this.receivable.TableName%>" + "&id=" + "<%=this.receivable.ReceivableId%>";

        $(document).ready(function () {
            
            $("#jqxExpander").jqxExpander({ width: "98%" });

            $("#btnInvalid").jqxButton({ height: 25, width: 100 });
            $("#btnAudit").jqxButton({ height: 25, width: 100 });
            $("#btnGoBack").jqxButton({ height: 25, width: 100 });
            $("#btnConfirm").jqxButton({ height: 25, width: 100 });

            //$("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确定要作废？")) { return; }
                $.post("Handler/ReceivableInvalidHandler.ashx", { id: $("#hidid").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableList.aspx";
                    }
                );
            });

            $("#btnAudit").on("click", function (e) {
                //alert(1);
                var paras = {
                    mid: 9,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确定要撤返？")) { return; }
                $.post("Handler/ReceivableGoBackHandler.ashx", { id: $("#hidid").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableList.aspx";
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                $.post("Handler/ReceivableConfirmHandler.ashx", { id: $("#hidid").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableList.aspx";
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
            收款明细
            <input type="hidden" id="hidid" runat="server" />
            <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>收款日期：</span></h4>
                    <span id="dtReceiveDate" style="float: left;" runat="server"></span>

                    <h4><span>收款公司：</span></h4>
                    <span id="ddlReceivableCorpId" style="float: left;" runat="server"></span>
                </li>

                <li>
                    <h4><span>收款银行：</span></h4>
                    <span id="ddlReceivableBank" style="float: left;" runat="server"></span>

                    <h4><span>收款账户：</span></h4>
                    <span id="ddlReceivableAccoontId" style="float: left;" runat="server"></span>
                </li>

                <li>
                    <h4><span>收款金额：</span></h4>
                    <span id="nbPayBala" style="float: left;" runat="server"></span>

                    <h4><span>收款币种：</span></h4>
                    <span id="ddlCurrencyId" style="float: left;" runat="server"></span>
                </li>

                <li>
                    <h4><span>付款公司：</span></h4>
                    <span id="ddlPayCorpId" style="float: left;" runat="server"></span>
                </li>

                <li>
                    <h4><span>付款银行：</span></h4>
                    <span id="ddlPayBankId" style="float: left;" runat="server"></span>

                    <h4><span>付款账户：</span></h4>
                    <span id="ddlPayAccountId" style="float: left;" runat="server"></span>
                </li>

                <li>
                    <h4><span>简短附言：</span></h4>
                    <span id="txbPayWord" style="float: left;" runat="server" />

                    <h4><span>外部流水备注：</span></h4>
                    <span id="txtBankLog" style="float: left;" runat="server" />
                </li>

                <%--<li>
                    <div id="buttons">
                        <span style="text-align: right; width: 15%;">&nbsp;</span>
                        <span>
                            <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 80px;" /></span>
                        <span>
                            <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 80px; margin-left: 10px" /></span>
                        <span>
                            <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 80px; margin-left: 10px" /></span>
                        <span><a target="_self" runat="server" href="ReceivableList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                    </div>
                </li>--%>
            </ul>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <%--        <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px;height:25px;" />&nbsp;&nbsp;&nbsp;--%>
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
