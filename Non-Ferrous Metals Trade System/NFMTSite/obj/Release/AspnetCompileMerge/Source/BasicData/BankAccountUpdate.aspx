<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankAccountUpdate.aspx.cs" Inherits="NFMTSite.BasicData.BankAccountUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行账户修改</title>
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

            //绑定 公司
            var companyIdurl = "../User/Handler/CorpDDLHandler.ashx";
            var companyIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: companyIdurl, async: false };
            var companyIddataAdapter = new $.jqx.dataAdapter(companyIdsource);
            $("#companyId").jqxComboBox({ selectedIndex: 0, source: companyIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 200 });
            if ($("#hidcompanyId").val() > 0) { $("#companyId").jqxComboBox("val", $("#hidcompanyId").val()); }

            //绑定 银行
            var url = "Handler/BankDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#bankId").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "BankName", valueMember: "BankId", width: 200, height: 25 });
            if ($("#hidbankId").val() > 0) { $("#bankId").jqxComboBox("val", $("#hidbankId").val()); }

            //账户号
            $("#txbAccountNo").jqxInput({ height: 25, width: 200 });

            //绑定 币种
            var currencyIdurl = "Handler/CurrencDDLHandler.ashx";
            var currencyIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyIdurl, async: false };
            var currencyIddataAdapter = new $.jqx.dataAdapter(currencyIdsource);
            $("#currencyId").jqxComboBox({ selectedIndex: 0, source: currencyIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 200, height: 25, autoDropDownHeight: true });
            if ($("#hidcurrencyId").val() > 0) { $("#currencyId").jqxComboBox("val", $("#hidcurrencyId").val()); }

            //账户描述
            $("#txbBankAccDesc").jqxInput({ height: 25, width: 200 });

            //账户状态
            CreateStatusDropDownList("ddlStatus");
            $("#ddlStatus").jqxDropDownList("val", $("#hidStatus").val());
            $("#ddlStatus").jqxDropDownList("width", 200);

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

            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认修改？")) { return; }

                var currencyId = $("#currencyId").val();
                var bankId = $("#bankId").val();
                var companyId = $("#companyId").val();
                var accountNo = $("#txbAccountNo").val();
                var bankAccDesc = $("#txbBankAccDesc").val();
                var status = $("#ddlStatus").val();
                var id = $("#hidNo").val();

                $.post("Handler/BankAccountUpdateHandler.ashx", {
                    currencyId: currencyId,
                    bankId: bankId,
                    companyId: companyId,
                    accountNo: accountNo,
                    bankAccDesc: bankAccDesc,
                    status: status,
                    id: id
                },
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
            银行账户修改
                        <input type="hidden" runat="server" id="hidcompanyId" />
            <input type="hidden" runat="server" id="hidbankId" />
            <input type="hidden" runat="server" id="hidcurrencyId" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <input type="hidden" runat="server" id="hidNo" />
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
                    <h4><span>账户状态：</span></h4>
                    <input type="hidden" id="hidStatus" runat="server" />
                    <div id="ddlStatus"></div>
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="提交" runat="server" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
