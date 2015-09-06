<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankAccountCreate.aspx.cs" Inherits="NFMTSite.BasicData.BankAccountCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行账户新增</title>
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

            //公司
            var url = "../User/Handler/CorpDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#companyId").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 200 });

            //银行
            var bankIdurl = "Handler/BankDDLHandler.ashx";
            var bankIdsource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: bankIdurl, async: false };
            var bankIddataAdapter = new $.jqx.dataAdapter(bankIdsource);
            $("#bankId").jqxComboBox({ selectedIndex: 0, source: bankIddataAdapter, displayMember: "BankName", valueMember: "BankId", width: 200, height: 25 });

            //账户号
            $("#txbAccountNo").jqxInput({ height: 25, width: 200 });

            //币种
            var currencyIdurl = "Handler/CurrencDDLHandler.ashx";
            var currencyIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyIdurl, async: false };
            var currencyIddataAdapter = new $.jqx.dataAdapter(currencyIdsource);
            $("#currencyId").jqxComboBox({ selectedIndex: 0, source: currencyIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, height: 25, autoDropDownHeight: true });

            //账户描述
            $("#txbBankAccDesc").jqxInput({ height: 25, width: 200 });

            // initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbAccountNo", message: "账户号不能为空", action: "keyup, blur", rule: "required" },
                    {
                        input: "#companyId", message: "公司不能为空", action: "change", rule: function (input, commit) {
                            return $("#companyId").jqxComboBox("val") > 0;
                        }
                    },
                    {
                        input: "#bankId", message: "银行不能为空", action: "change", rule: function (input, commit) {
                            return $("#bankId").jqxComboBox("val") > 0;
                        }
                    },
                    {
                        input: "#currencyId", message: "币种不能为空", action: "change", rule: function (input, commit) {
                            return $("#currencyId").jqxComboBox("val") > 0;
                        }
                    }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认新增银行账户？")) { return; }

                var bankAccount = {
                    CompanyId: $("#companyId").val(),
                    BankId: $("#bankId").val(),
                    AccountNo: $("#txbAccountNo").val(),
                    CurrencyId: $("#currencyId").val(),
                    BankAccDesc: $("#txbBankAccDesc").val()
                };

                $.post("Handler/BankAccountCreateHandler.ashx", { bankAccount: JSON.stringify(bankAccount) },
                    function (result) {
                        alert(result);
                        document.location.href = "BankAccountList.aspx";
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
            账户添加
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>公司：</span></h4>
                    <div id="companyId" />
                </li>
                <li>
                    <h4><span>银行：</span></h4>
                    <div id="bankId" />
                </li>
                <li>
                    <h4><span>账户号：</span></h4>
                    <input type="text" id="txbAccountNo" runat="server" />
                </li>
                <li>
                    <h4><span>币种：</span></h4>
                    <div id="currencyId" />
                </li>
                <li>
                    <h4><span>账户描述：</span></h4>
                    <input type="text" runat="server" id="txbBankAccDesc" />
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnAdd" value="提交" runat="server" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
