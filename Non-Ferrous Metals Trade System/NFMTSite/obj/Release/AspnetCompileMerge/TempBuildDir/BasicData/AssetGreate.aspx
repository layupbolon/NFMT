<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetGreate.aspx.cs" Inherits="NFMTSite.BasicData.AssetGreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>品种添加</title>
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

            $("#txbAssetName").jqxInput({ height: 25, width: 200 });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });

            //绑定 基数计量单位
            var url = "Handler/MUDDLHandler.ashx?";
            var source = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#No").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "MUName", valueMember: "MUId", height: 25, width: 200, autoDropDownHeight: true });
            $("#nbAmountPerHand").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 6, min: 0, width: 200 });

            // initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbAssetName", message: "品种名称不能为空", action: "keyup, blur", rule: "required" },
                    {
                        input: "#nbAmountPerHand", message: "每手重量不能为空", action: "keyup, blur", rule: function (input, commit) {
                            return $("#nbAmountPerHand").jqxNumberInput("val") > 0;
                        }
                    }
                ]
            });

            $("#btnNo").on('click', function () {
                window.document.location = "AssetList.aspx";
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var assetName = $("#txbAssetName").val();
                var muId = $("#No").val();
                $.post("Handler/AssetGreateHandler.ashx", {
                    assetName: assetName,
                    muId: muId,
                    hands: $("#nbAmountPerHand").val()
                },
                    function (result) {
                        alert(result);
                        document.location.href = "AssetList.aspx";
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
            品种添加
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>品种名称：</span></h4>
                    <span>
                        <input type="text" id="txbAssetName" /></span>
                </li>
                <li>
                    <h4><span>基础计量单位：</span></h4>
                    <div id="No" />
                    <input type="hidden" runat="server" id="HidNo" />
                </li>
                <li>
                    <h4><span>每手重量：</span></h4>
                    <div id="nbAmountPerHand" />
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
