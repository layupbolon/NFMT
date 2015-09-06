<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeasureUnitDetail.aspx.cs" Inherits="NFMTSite.BasicData.MeasureUnitDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>计量单位明细</title>
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
            

            //计量单位名称
            $("#txbMUName").jqxInput({ height: 25, width: 200, disabled: true });
            $("#txbMUName").jqxInput("val", "<%=this.mu.MUName%>");

            //基数计量单位
            var url = "Handler/MUDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#No").jqxComboBox({ source: dataAdapter, displayMember: "MUName", valueMember: "MUId", width: 200, height: 25, autoDropDownHeight: true, disabled: true });
            if ("<%=this.mu.BaseId%>" > 0) {
                $("#No").jqxComboBox("val", "<%=this.mu.BaseId%>");
            }

            //基本单位转换率
            $("#txbTransformRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, Digits: 8, spinButtons: true, width: 200, disabled: true });
            $("#txbTransformRate").jqxNumberInput("val", "<%=this.mu.TransformRate%>");

            //单位状态
            CreateSelectStatusDropDownList("selStatus", "<%=(int)this.mu.MUStatus%>");
            $("#selStatus").jqxDropDownList("disabled", true);
            $("#selStatus").jqxDropDownList("width", 200);


            $("#btnFreeze").jqxButton({ height: 25, width: 96 });
            $("#btnUnFreeze").jqxButton({ height: 25, width: 96 });

            $("#btnFreeze").on("click", function () {
                var id = "<%=this.mu.MUId%>";
                var operateId = operateEnum.冻结;
                $.post("Handler/MUStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var id = "<%=this.mu.MUId%>";
                var operateId = operateEnum.解除冻结;
                $.post("Handler/MUStatusHandler.ashx", { id: id, oi: operateId },
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
            计量单位明细
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>计量单位名称：</span></h4>
                    <span>
                        <input type="text" id="txbMUName" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>基本单位名称：</span></h4>
                    <div id="No" />
                </li>
                <li>
                    <h4><span>单位转换率：</span></h4>
                    <div id="txbTransformRate" runat="server" />
                </li>
                <li>
                    <h4><span>单位状态：</span></h4>
                    <div id="selStatus" />
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
