<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankDetail.aspx.cs" Inherits="NFMTSite.BasicData.BankDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行明细</title>
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
            

            $("#txbBankName").jqxInput({ width: 200, height: 25, disabled: true });
            $("#txbBankName").jqxInput("val", "<%=this.bank.BankName%>");

            $("#txbBankEname").jqxInput({ width: 200, height: 25, disabled: true });
            $("#txbBankEname").jqxInput("val", "<%=this.bank.BankEname%>");

            $("#txbBankFullName").jqxInput({ width: 200, height: 25, disabled: true });
            $("#txbBankFullName").jqxInput("val", "<%=this.bank.BankFullName%>");

            $("#txbBankShort").jqxInput({ width: 200, height: 25, disabled: true });
            $("#txbBankShort").jqxInput("val", "<%=this.bank.BankShort%>");

            //绑定 银行资本类型
            var styleId = $("#hidStyleId").val();
            var sourceCapitalType = { datatype: "json", url: "Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCapitalType);
            $("#capitalType").jqxComboBox({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true, disabled: true });
            $("#capitalType").jqxComboBox("val", "<%=this.bank.CapitalType%>");

            CreateSelectStatusDropDownList("selBankeStatus", "<%=(int)this.bank.BankStatus%>");
            $("#selBankeStatus").jqxDropDownList("disabled", true);
            $("#selBankeStatus").jqxDropDownList("width", 200);

            //绑定 上级银行
            var url = "Handler/BankDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#bankLevel").jqxComboBox({ source: dataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, disabled: true });
            if ("<%=this.bank.ParentId%>" > 0) {
                $("#bankLevel").jqxComboBox("val", "<%=this.bank.ParentId%>");
            }

            //是
            $("#rbYes").jqxRadioButton({ width: 100, height: 22, disabled: true });

            //否
            $("#rbNo").jqxRadioButton({ width: 100, height: 22, disabled: true });
            if ("<%=this.bank.SwitchBack%>".toLowerCase() == "true") {
                $("#rbYes").jqxRadioButton("check");
            } else {
                $("#rbNo").jqxRadioButton("check");
            }

            $("#btnFreeze").jqxButton({ height: 25, width: 96 });
            $("#btnUnFreeze").jqxButton({ height: 25, width: 96 });

            $("#btnFreeze").on("click", function () {
                var id = "<%=this.bank.BankId%>";
                var operateId = operateEnum.冻结;
                $.post("Handler/BankStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var id = "<%=this.bank.BankId%>";
                var operateId = operateEnum.解除冻结;
                $.post("Handler/BankStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
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
            银行明细
                        <input type="hidden" id="hidStyleId" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>银行名称：</span></h4>
                    <input type="text" id="txbBankName" runat="server" /></li>
                <li>
                    <h4><span>银行英文名称：</span></h4>
                    <input type="text" id="txbBankEname" runat="server" /></li>
                <li>
                    <h4><span>银行全称：</span></h4>
                    <input type="text" id="txbBankFullName" runat="server" /></li>
                <li>
                    <h4><span>银行缩写：</span></h4>
                    <input type="text" id="txbBankShort" runat="server" /></li>
                <li>
                    <h4><span>资本类型：</span></h4>
                    <div id="capitalType" />
                </li>
                <li>
                    <h4><span>银行状态：</span></h4>
                    <div id="selBankeStatus" />
                </li>
                <li>
                    <h4><span>上级银行名称：</span></h4>
                    <div id="bankLevel" />
                </li>
                <li>
                    <h4><span>头寸是否转回：</span></h4>
                    <div id="rbYes" runat="server" style="float: left; margin-top: 6px;">是</div>
                    <div id="rbNo" runat="server" style="float: left; margin-top: 6px">否</div>
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnFreeze" value="冻结" runat="server" /></span>
                    <span>
                        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="margin-left: 10px" />
                    </span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
