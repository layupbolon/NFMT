<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyContractCreate.aspx.cs" Inherits="NFMTSite.Funds.PayApplyContractCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请新增--合约关联</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            

            $("#jqxConExpander").jqxExpander({ width: "98%" });
            $("#jqxSubExpander").jqxExpander({ width: "98%" });
            $("#jqxPayApplyExpander").jqxExpander({ width: "98%" });

            //申请日期
            $("#txbApplyDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#selApplyDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", height: 25, width: 120,disabled:true });
            $("#selApplyDept").val(<%=this.curDeptId%>);

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId="+"<%=this.SubContractId%>";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            //收款公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId="+"<%=this.SubContractId%>";
            var outCorpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120, searchMode: "containsignorecase"
            });
            $("#selRecCorp").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    var obj = outCorpDataAdapter.records[index];
                    $("#spnRecCorpFullName").html(obj.CorpFullName);

                    var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selBank").val();
                    var tempSource = { datatype: "json", url: tempUrl, async: false };
                    var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                    $("#selBankAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
                }
            });

            //开户行
            var bankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var bankSource = { datatype: "json", url: bankUrl, async: false };
            var bankDataAdapter = new $.jqx.dataAdapter(bankSource);
            $("#selBank").jqxComboBox({ source: bankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120 });
            $("#selBank").on("change", function (event) {
                var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selBank").val();
                var tempSource = { datatype: "json", url: tempUrl, async: false };
                var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                $("#selBankAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
            });

            //收款账号
            var bankAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selBank").val();
            var bankAccountSource = { datatype: "json", url: bankAccountUrl, async: false };
            var bankAccountDataAdapter = new $.jqx.dataAdapter(bankAccountSource);
            $("#selBankAccount").jqxComboBox({ source: bankAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
            $("#selBankAccount").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var item = args.item;
                    $("#txbBankAccount").val(item.label);
                }
            });

            $("#txbBankAccount").jqxInput({ height: 23 });

            //申请金额
            $("#txbApplyBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //最后付款日
            $("#txbPayDeadline").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayMatterStyle%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#selPayMatter").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //备注
            $("#txbMemo").jqxInput({ height: 23 });
            $("#txbSpecialDesc").jqxInput({ height: 23 });

            //buttons
            $("#btnCreate").jqxButton();
            $("#btnCancel").jqxButton();

            //验证
            $("#jqxPayApplyExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#selApplyDept", message: "申请部门必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selApplyDept").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selApplyCorp", message: "申请公司必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selApplyCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selRecCorp", message: "收款公司必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selRecCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selBank", message: "开户行必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selBank").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbBankAccount", message: "收款银行账户必填", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbBankAccount").val().length > 0;
                            }
                        },
                        {
                            input: "#txbApplyBala", message: "申请金额大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbApplyBala").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //新增
            $("#btnCreate").click(function () {

                var isCanSubmit = $("#jqxPayApplyExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认添加付款申请？")) { return; }

                var accId = 0;
                if ($("#selBankAccount").val() > 0) { accId = $("#selBankAccount").val(); }

                var payApply = {
                    ApplyDeptId: $("#selApplyDept").val(),
                    RecCorpId: $("#selRecCorp").val(),
                    RecBankId: $("#selBank").val(),
                    RecBankAccountId: accId,
                    RecBankAccount: $("#txbBankAccount").val(),
                    ApplyBala: $("#txbApplyBala").val(),
                    CurrencyId: $("#selCurrency").val(),
                    PayMode: $("#selPayMode").val(),
                    PayDeadline: $("#txbPayDeadline").val(),
                    PayWord: $("#txbMemo").val(),
                    PayMatter: $("#selPayMatter").val(),
                    SpecialDesc: $("#txbSpecialDesc").val()
                };

                $.post("Handler/PayApplyContractCreateHandler.ashx", { PayApply: JSON.stringify(payApply), memo: $("#txbMemo").val(), subId: "<%=this.SubContractId%>", deptId: $("#selApplyDept").val(), corpId: $("#selApplyCorp").jqxComboBox("val") },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PayApplyList.aspx";
                        }
                    }
                );

            });

        });
    </script>
</head>
<body>

    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>合约编号：</strong>
                    <span runat="server" id="spnContractNo"></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spnAsset"></span>
                </li>
                <li>
                    <strong>签订数量：</strong>
                    <span runat="server" id="spnSignAmount"></span>
                </li>
                <li>
                    <strong>我方抬头：</strong>
                    <span runat="server" id="spnInCorpNames"></span>
                </li>
                <li>
                    <strong>对方抬头：</strong>
                    <span runat="server" id="spnOutCorpNames"></span>
                </li>
                <li>
                    <strong>合约升贴水：</strong>
                    <span runat="server" id="spnPre"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            子合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>子合约编号：</strong>
                    <span runat="server" id="spnSubNo"></span>
                </li>
                <li>
                    <strong>子合约数量：</strong>
                    <span runat="server" id="spnSubSignAmount"></span>
                </li>
                <li>
                    <strong>已分配数量：</strong>
                    <span runat="server" id="Span1"></span>
                </li>
                <li>
                    <strong>升贴水：</strong>
                    <span runat="server" id="Span2"></span>
                </li>
                <li>
                    <strong>执行最终日：</strong>
                    <span runat="server" id="spnPeriodE"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxPayApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请日期：</strong>
                    <div id="txbApplyDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div id="selApplyDept" style="float: left;" />
                </li>
                <li>
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="selApplyCorp"></div>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <div id="selRecCorp" style="float: left;" />
                </li>
                <li>
                    <strong>收款人全称：</strong>
                    <span runat="server" id="spnRecCorpFullName"></span>
                </li>
                <li>
                    <strong>收款开户行：</strong>
                    <div id="selBank" style="float: left;" />
                </li>
                <li>
                    <strong>收款账号：</strong>
                    <div id="selBankAccount" style="float: left;" />
                </li>
                <li>
                    <input type="text" id="txbBankAccount" />
                </li>
                <li>
                    <strong>申请金额：</strong>
                    <div style="float: left" id="txbApplyBala"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;" />
                </li>
                <li>
                    <strong>最后付款日：</strong>
                    <div id="txbPayDeadline" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款事项：</strong>
                    <div style="float: left" id="selPayMatter"></div>
                </li>
                <li>
                    <strong>付款方式：</strong>
                    <div style="float: left" id="selPayMode"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" />
                </li>
                <li>
                    <strong>特殊附言：</strong>
                    <input type="text" id="txbSpecialDesc" />
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="新增付款申请" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />
    </div>
</body>
</html>
