<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInDetail.aspx.cs" Inherits="NFMTSite.Funds.CashInDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curCashIn.DataBaseName%>" + "&t=" + "<%=this.curCashIn.TableName%>" + "&id=" + "<%=this.curCashIn.CashInId%>";

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });

            //收款日期
            $("#dtCashInDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 260,disabled:true });

            //收款公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCashInCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 260, height: 25, searchMode: "containsignorecase",disabled:true });

            //收款银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlCashInBank").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 260, height: 25,disabled:true, searchMode: "containsignorecase" });

            //收款账户
            var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#ddlCashInCorpId").val() + "&b=" + $("#ddlCashInBank").val();
            var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
            var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
            $("#ddlCashInAccoontId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 260, height: 25,disabled:true, searchMode: "containsignorecase" });

            //收款金额
            $("#nbPayBala").jqxNumberInput({ width: 260, height: 25, spinButtons: true, decimalDigits: 4, Digits: 10, min: 0, max: 9999999999,disabled:true });

            //收款币种
            var ddlMUIdurl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlCurrencyId").jqxComboBox({  source: ddlMUIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 260, height: 25, searchMode: "containsignorecase",disabled:true });

            //收款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#ddlCashInMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 260, height: 25, autoDropDownHeight: true ,disabled:true});

            //付款公司
            var ddlPayCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var ddlPayCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlPayCorpIdurl, async: false };
            var ddlPayCorpIddataAdapter = new $.jqx.dataAdapter(ddlPayCorpIdsource);
            $("#ddlPayCorpId").jqxComboBox({ source: ddlPayCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 260, height: 25, searchMode: "containsignorecase",disabled:true });

            //$("#txtPayCorp").jqxInput({ height: 25, width: 130,disabled:true });

            //付款银行
            var ddlPayBankIdurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlPayBankIdsource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlPayBankIdurl, async: false };
            var ddlPayBankIddataAdapter = new $.jqx.dataAdapter(ddlPayBankIdsource);
            $("#ddlPayBankId").jqxComboBox({ source: ddlPayBankIddataAdapter, displayMember: "BankName", valueMember: "BankId", width: 130, height: 25,disabled:true, searchMode: "containsignorecase" });
                       

            $("#txtPayBank").jqxInput({ height: 25, width: 130,disabled:true });

            //付款账户
            var ddlPayAccountIdurl = "../BasicData/Handler/BankAccountDDLHandler.ashx";
            var ddlPayAccountIdsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlPayAccountIdurl, async: false };
            var ddlPayAccountIddataAdapter = new $.jqx.dataAdapter(ddlPayAccountIdsource);
            $("#ddlPayAccountId").jqxComboBox({ source: ddlPayAccountIddataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 130, height: 25,disabled:true , searchMode: "containsignorecase"});
            
            $("#txtPayAccount").jqxInput({ height: 25, width: 130,disabled:true });

            //简短附言
            $("#txbPayWord").jqxInput({ height: 25, width: 260,disabled:true });
            //外部流水备注
            $("#txtBankLog").jqxInput({ height: 25, width: 260,disabled:true });
            
            //初始数据
            $("#dtCashInDate").val(new Date("<%=this.curCashIn.CashInDate%>"));
            $("#ddlCashInCorpId").val(<%=this.curCashIn.CashInCorpId%>);
            $("#ddlCashInBank").val(<%=this.curCashIn.CashInBank%>);

            $("#ddlCashInAccoontId").val(<%=this.curCashIn.CashInAccoontId%>);
            $("#nbPayBala").val(<%=this.curCashIn.CashInBala%>);
            $("#ddlCurrencyId").val(<%=this.curCashIn.CurrencyId%>);

            if(<%=this.curCashIn.PayCorpId%>>0){
                $("#ddlPayCorpId").val(<%=this.curCashIn.PayCorpId%>);
            }

            $("#txtPayCorp").val("<%=this.curCashIn.PayCorpName%>");

            if(<%=this.curCashIn.PayBankId%>>0){
                $("#ddlPayBankId").val(<%=this.curCashIn.PayBankId%>);
            }

            $("#txtPayBank").val("<%=this.curCashIn.PayBank%>");

            if(<%=this.curCashIn.PayAccountId%>>0){
                $("#ddlPayAccountId").val(<%=this.curCashIn.PayAccountId%>);
            }

            $("#txtPayAccount").val("<%=this.curCashIn.PayAccount%>");
            $("#txbPayWord").val("<%=this.curCashIn.PayWord%>");
            $("#txtBankLog").val("<%=this.curCashIn.BankLog%>");
            $("#ddlCashInMode").jqxDropDownList("val","<%=this.curCashIn.CashInMode%>");

            //init button
            $("#btnInvalid").jqxButton({ height: 25, width: 100 });
            $("#btnAudit").jqxButton({ height: 25, width: 100 });
            $("#btnGoBack").jqxButton({ height: 25, width: 100 });
            $("#btnConfirm").jqxButton({ height: 25, width: 100 });
            $("#btnConfirmCancel").jqxButton({ height: 25, width: 100 });

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 9,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CashInStatusHandler.ashx", { id: "<%=this.curCashIn.CashInId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/CashInStatusHandler.ashx", { id: "<%=this.curCashIn.CashInId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInList.aspx";
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.确认完成;
                $.post("Handler/CashInStatusHandler.ashx", { id: "<%=this.curCashIn.CashInId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInList.aspx";
                    }
                );
            });

            $("#btnConfirmCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.确认完成撤销;
                $.post("Handler/CashInStatusHandler.ashx", { id: "<%=this.curCashIn.CashInId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInList.aspx";
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
            收款修改<input type="hidden" id="hidModel" runat="server" />
            <input type="hidden" id="txtPayCorp" />
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">收款日期：</span>
                    <div id="dtCashInDate" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">收款公司：</span>
                    <div id="ddlCashInCorpId" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">收款银行：</span>
                    <div id="ddlCashInBank" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">收款账户：</span>
                    <div id="ddlCashInAccoontId" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">收款金额：</span>
                    <div id="nbPayBala" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">收款币种：</span>
                    <div id="ddlCurrencyId" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">收款方式：</span>
                    <div style="float: left" id="ddlCashInMode"></div>

                    <span style="text-align: right; width: 15%;">付款公司：</span>
                    <div id="ddlPayCorpId" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">付款银行：</span>
                    <div id="ddlPayBankId" style="float: left;"></div>
                    <input type="text" id="txtPayBank" style="float: left;" />

                    <span style="text-align: right; width: 15%;">付款账户：</span>
                    <div id="ddlPayAccountId" style="float: left;"></div>
                    <input type="text" id="txtPayAccount" style="float: left;" />
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">简短附言：</span>
                    <input type="text" id="txbPayWord" style="float: left;" />

                    <span style="text-align: right; width: 15%;">外部流水备注：</span>
                    <input type="text" id="txtBankLog" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="CashInAttach" />

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
