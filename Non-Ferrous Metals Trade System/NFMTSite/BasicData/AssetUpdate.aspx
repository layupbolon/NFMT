<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetUpdate.aspx.cs" Inherits="NFMTSite.BasicData.AssetUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>品种种修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#jqxExpander").jqxExpander({ width: "98%" });

            $("#txbAssetName").jqxInput({ height: 25, width: 200 });

            //绑定 基数计量单位
            var url = "Handler/MUDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#No").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "MUName", valueMember: "MUId", width: 200, height: 25, autoDropDownHeight: true });
            //给下拉框赋默认值
            if ($("#HidNo").val() > 0) { $("#No").jqxComboBox("val", $("#HidNo").val()); }

            //品种状态
            var selValue = $("#txbAssetStatus").val();
            CreateSelectStatusDropDownList("selStatus", selValue);
            $("#selStatus").jqxDropDownList("width", 200);

            $("#nbAmountPerHand").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 6, min: 0, width: 200 });
            $("#nbAmountPerHand").jqxNumberInput("val", "<%=this.asset.AmountPerHand%>");

            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbAssetName", message: "品种名称不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var assetName = $("#txbAssetName").val();
                var muid = $("#No").val();
                var id = $("#hid").val();
                var assetStatus = $("#selStatus").val();

                $.post("Handler/AssetUpdateHandler.ashx", {
                    assetName: assetName,
                    muid: muid,
                    id: id,
                    assetStatus: assetStatus,
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
            品种修改
                        <input type="hidden" runat="server" id="HidNo" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <input type="hidden" runat="server" id="hid" />
                    <h4><span>品种名称：</span></h4>
                    <span>
                        <input type="text" id="txbAssetName" runat="server" /></span>
                </li>
                <li>
                    <h4><span>基本单位名称：</span></h4>
                    <div id="No" />
                </li>
                <li>
                    <h4><span>每手重量：</span></h4>
                    <div id="nbAmountPerHand" />
                </li>
                <li>
                    <h4><span>品种状态：</span></h4>
                    <div id="selStatus" />
                    <input type="hidden" id="txbAssetStatus" runat="server" />
                </li>
                <li>
                    <h4><span>&nbsp</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" runat="server" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
