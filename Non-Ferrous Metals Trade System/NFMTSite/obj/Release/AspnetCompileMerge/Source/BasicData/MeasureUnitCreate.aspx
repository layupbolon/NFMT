<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeasureUnitCreate.aspx.cs" Inherits="NFMTSite.BasicData.MeasureUnitCreate" %>
<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>计量单位新增</title>
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

            $("#txbMUName").jqxInput({ height: 25, width: 200 });
            $("#txbTransformRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 8, Digits: 8, spinButtons: true, width: 200 });

            //绑定 基数计量单位
            var url = "Handler/MUDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#No").jqxComboBox({ source: dataAdapter, displayMember: "MUName", valueMember: "MUId", width: 200, height: 25, autoDropDownHeight: true });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbMUName", message: "计量单位不可为空", action: "keyup,blur", rule: "required" },
                        {
                            input: "#txbTransformRate", message: "基本单位转换率不能小于0", action: "change", rule: function (input, commit) {
                                if ($("#txbTransformRate").jqxComboBox("val") < 0) { return false; }
                                else { return true; }
                            }
                        }
                    ]
            });
            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认添加计量单位？")) { return; }

                var baseId = $("#No").val() != "" ? parseInt($("#No").val()) : 0;
                var muName = $("#txbMUName").val();
                var transformRate = $("#txbTransformRate").val();

                $.post("Handler/MUGreateHandler.ashx", {
                    muName: muName,
                    baseId: baseId,
                    transformRate: transformRate
                },
                function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "MeasureUnitList.aspx";
                    }
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
            计量单位新增
                        <input type="hidden" runat="server" id="HidNo" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>计量单位名称：</span></h4>
                    <span>
                        <input type="text" id="txbMUName" />
                    </span>
                </li>
                <li>
                    <h4><span>基本单位名称：</span></h4>
                    <div id="No" />
                </li>
                <li>
                    <h4><span>单位转换率：</span></h4>
                    <div id="txbTransformRate" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="MeasureUnitList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
