<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInUpdate.aspx.cs" Inherits="NFMTSite.Funds.CashInUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款登记修改</title>
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

            //收款日期
            $("#dtCashInDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 260 });

            //收款公司
            var ddlCorpIdurl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCashInCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 260, height: 25, searchMode: "containsignorecase" });

            $("#ddlCashInCorpId").on("select", function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {
                        var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#ddlCashInCorpId").val() + "&b=" + $("#ddlCashInBank").val();
                        var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
                        var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
                        $("#ddlCashInAccoontId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 260, height: 25 });
                    }
                }
            });

            //收款银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlCashInBank").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 260, height: 25, searchMode: "containsignorecase" });

            $("#ddlCashInBank").on("select", function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {
                        var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#ddlCashInCorpId").val() + "&b=" + $("#ddlCashInBank").val();
                        var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
                        var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
                        $("#ddlCashInAccoontId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 260, height: 25 });
                    }
                }
            });

            //收款账户
            var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#ddlCashInCorpId").val() + "&b=" + $("#ddlCashInBank").val();
            var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
            var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
            $("#ddlCashInAccoontId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 260, height: 25 });

            //收款金额
            $("#nbPayBala").jqxNumberInput({ width: 260, height: 25, spinButtons: true, decimalDigits: 2, Digits: 10, min: 0, max: 9999999999 });

            //收款币种
            var ddlMUIdurl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlCurrencyId").jqxComboBox({ selectedIndex: 0, source: ddlMUIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 260, height: 25, autoDropDownHeight: true });

            //收款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#ddlCashInMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 260, height: 25, autoDropDownHeight: true });

            //付款公司
            var ddlPayCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var ddlPayCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlPayCorpIdurl, async: false };
            var ddlPayCorpIddataAdapter = new $.jqx.dataAdapter(ddlPayCorpIdsource);
            $("#ddlPayCorpId").jqxComboBox({ source: ddlPayCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 260, height: 25, searchMode: "containsignorecase" });

            $("#ddlPayCorpId").on("select", function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {

                        $("#txtPayCorp").val(item.label);

                        var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#ddlPayCorpId").val() + "&b=" + $("#ddlPayBankId").val();
                        var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
                        var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
                        $("#ddlPayAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 130, height: 25 });
                    }
                }
            });

            //$("#txtPayCorp").jqxInput({ height: 25, width: 130 });

            //付款银行
            var ddlPayBankIdurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlPayBankIdsource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlPayBankIdurl, async: false };
            var ddlPayBankIddataAdapter = new $.jqx.dataAdapter(ddlPayBankIdsource);
            $("#ddlPayBankId").jqxComboBox({ source: ddlPayBankIddataAdapter, displayMember: "BankName", valueMember: "BankId", width: 130, height: 25 });

            $("#ddlPayBankId").on("select", function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {
                        $("#txtPayBank").val(item.label);

                        var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#ddlPayCorpId").val() + "&b=" + $("#ddlPayBankId").val();
                        var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
                        var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
                        $("#ddlPayAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 130, height: 25 });
                    }
                }
            });

            $("#txtPayBank").jqxInput({ height: 25, width: 130 });

            //付款账户
            var ddlPayAccountIdurl = "../BasicData/Handler/BankAccountDDLHandler.ashx";
            var ddlPayAccountIdsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlPayAccountIdurl, async: false };
            var ddlPayAccountIddataAdapter = new $.jqx.dataAdapter(ddlPayAccountIdsource);
            $("#ddlPayAccountId").jqxComboBox({ source: ddlPayAccountIddataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 130, height: 25 });

            $("#ddlPayAccountId").on("select", function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {
                        $("#txtPayAccount").val(item.label);
                    }
                }
            });

            $("#txtPayAccount").jqxInput({ height: 25, width: 130 });

            //简短附言
            $("#txbPayWord").jqxInput({ height: 25, width: 260 });
            //外部流水备注
            $("#txtBankLog").jqxInput({ height: 25, width: 260 });

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });
            
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


            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#nbPayBala", message: "付款金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbPayBala').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#ddlCashInCorpId", message: "收款公司不可为空", action: "keyup,blur,mouseleave", rule: function (input, commit) {
                                return $('#ddlCashInCorpId').jqxComboBox("val") != "" && $('#ddlCashInCorpId').jqxComboBox("val") > -1;
                            }
                        },
                        {
                            input: "#ddlCashInBank", message: "收款银行不可为空", action: "keyup,blur,mouseleave", rule: function (input, commit) {
                                return $('#ddlCashInBank').jqxComboBox("val") != "" && $('#ddlCashInBank').jqxComboBox("val") > -1;
                            }
                        },
                        {
                            input: "#ddlCashInAccoontId", message: "收款账户不可为空", action: "keyup,blur,mouseleave", rule: function (input, commit) {
                                return $('#ddlCashInAccoontId').jqxComboBox("val") != "" && $('#ddlCashInAccoontId').jqxComboBox("val") > -1;
                            }
                        },
                        {
                            input: "#ddlCurrencyId", message: "收款币种不可为空", action: "keyup,blur,mouseleave", rule: function (input, commit) {
                                return $('#ddlCurrencyId').jqxComboBox("val") != "" && $('#ddlCurrencyId').jqxComboBox("val") > -1;
                            }
                        },
                        {
                            input: "#ddlPayCorpId", message: "付款公司不可为空", action: "change", rule: function (input, commit) {
                                return $('#ddlPayCorpId').jqxComboBox("val") != "" && $('#ddlPayCorpId').jqxComboBox("val") > -1;
                            }
                        }
                    ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if(!confirm("确认修改收款登记?")){return;}

                var cashInModel = {
                    CashInId:"<%=this.curCashIn.CashInId%>",
                    CashInDate: $("#dtCashInDate").val(),
                    CashInCorpId: $("#ddlCashInCorpId").val(),
                    CurrencyId: $("#ddlCurrencyId").val(),
                    CashInBala: $("#nbPayBala").val(),
                    CashInBank: $("#ddlCashInBank").val(),
                    CashInAccoontId: $("#ddlCashInAccoontId").val(),
                    CashInMode: $("#ddlCashInMode").val(),
                    PayWord: $("#txbPayWord").val(),
                    PayCorpId: $("#ddlPayCorpId").val() > 0 ? $("#ddlPayCorpId").val() : 0,
                    PayCorpName: $("#txtPayCorp").val(),
                    PayBankId: $("#ddlPayBankId").val() > 0 ? $("#ddlPayBankId").val() : 0,
                    PayBank: $("#txtPayBank").val(),
                    PayAccountId: $("#ddlPayAccountId").val() > 0 ? $("#ddlPayAccountId").val() : 0,
                    PayAccount: $("#txtPayAccount").val(),
                    BankLog: $("#txtBankLog").val()
                };

                //附件信息
                var fileIds = new Array();
                var files = $(":file");
                for (i = 0; i < files.length-1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/CashInUpdateHandler.ashx", { CashIn: JSON.stringify(cashInModel) },
                    function (result) {
                        var id = "<%=this.curCashIn.CashInId%>";
                        AjaxFileUpload(fileIds, id, AttachTypeEnum.CashInAttach);
                        alert(result);
                        document.location.href = "CashInList.aspx";
                    }
                );
            });
        });

            function bntRemoveOnClick(row) {
                if (!confirm("确认删除？")) { return; }
                var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                if(item.AttachId == "undefined" || item.AttachId <= 0) return;
                $.post("../Files/Handler/AttachUpdateStatusHandler.ashx", { aid: item.AttachId ,s:statusEnum.已作废},
                        function (result) {
                            attachSource.url = "../Files/Handler/GetAttachListHandler.ashx?cid="+"<%=this.curCashIn.CashInId%>"+"&s="+statusEnum.已生效+"&t="+AttachTypeEnum.CashInAttach;
                            $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");
                        }
                );
                    }
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            收款修改<input type="hidden" id="txtPayCorp"  />
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
        <input type="button" id="btnUpdate" value="提交" runat="server" style="width: 80px;" />&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="CashInList.aspx" id="btnCancel" style="margin-left: 10px">取消</a>&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
