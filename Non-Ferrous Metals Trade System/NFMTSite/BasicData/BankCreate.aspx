<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankCreate.aspx.cs" Inherits="NFMTSite.BasicData.BankCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#jqxExpander").jqxExpander({ width: "98%" });

            $("#txbBankName").jqxInput({ height: 25, width: 200 });
            $("#txbBankEname").jqxInput({ height: 25, width: 200 });
            $("#txbBankFullName").jqxInput({ height: 25, width: 200 });
            $("#txbBankShort").jqxInput({ height: 25, width: 200 });

            //绑定 银行资本类型
            var styId = $("#HidNo").val();

            var capitalTypesource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + styId, async: false };
            var capitalTypedataAdapter = new $.jqx.dataAdapter(capitalTypesource);
            $("#capitalType").jqxDropDownList({ source: capitalTypedataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true });

            //绑定 上级银行
            var url = "Handler/BankDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#bankLevel").jqxComboBox({ source: dataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25 });

            //期货头寸是否转回
            //是
            $("#rbYes").jqxRadioButton({ width: 100, height: 22 });

            //否
            $("#rbNo").jqxRadioButton({ width: 100, height: 22, checked: true });

            //initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbBankName", message: "银行名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbBankEname", message: "银行英文名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbBankFullName", message: "银行全称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbBankShort", message: "银行缩写不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var bank = {
                    BankName: $("#txbBankName").val(),
                    BankEname: $("#txbBankEname").val(),
                    BankFullName: $("#txbBankFullName").val(),
                    BankShort: $("#txbBankShort").val(),
                    CapitalType: $("#capitalType").val(),
                    BankLevel: 1,
                    ParentId: $("#bankLevel"),
                    SwitchBack: $("#rbYes").val() ? true : false
                };

                $.post("Handler/BankCreateHandler.ashx", { bank: JSON.stringify(bank) },
                    function (result) {
                        alert(result);
                        document.location.href = "BankList.aspx";
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
            银行新增
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>银行名称：</span></h4>
                    <span>
                        <input type="text" id="txbBankName" />
                    </span>
                </li>
                <li>
                    <h4><span>银行英文名称：</span></h4>
                    <span>
                        <input type="text" id="txbBankEname" />
                    </span>
                </li>
                <li>
                    <h4><span>银行全称：</span></h4>
                    <span>
                        <input type="text" id="txbBankFullName" />
                    </span>
                </li>
                <li>
                    <h4><span>银行缩写：</span></h4>
                    <span>
                        <input type="text" id="txbBankShort" />
                    </span>
                </li>
                <li>
                    <h4><span>资本类型：</span></h4>
                    <div id="capitalType" />
                    <input type="hidden" runat="server" id="HidNo" />
                </li>
                <li>
                    <h4><span>上级银行名称：</span></h4>
                    <div id="bankLevel" />
                    <input type="hidden" runat="server" id="HiddenBankLevel" />
                </li>
                <li>
                    <h4><span>头寸是否转回：</span></h4>
                    <div id="rbYes" runat="server" style="float: left; margin-top: 6px;">是</div>
                    <div id="rbNo" runat="server" style="float: left; margin-top: 6px">否</div>
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="BankList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
