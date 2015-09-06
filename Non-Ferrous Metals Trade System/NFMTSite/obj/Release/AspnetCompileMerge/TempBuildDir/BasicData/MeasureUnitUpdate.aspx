<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeasureUnitUpdate.aspx.cs" Inherits="NFMTSite.BasicData.MeasureUnitUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>计量单位修改</title>
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
            $("#txbMUName").jqxInput({ height: 25, width: 200 });
            $("#txbMUName").jqxInput("val", "<%=this.mu.MUName%>");

            //基数计量单位
            var url = "Handler/MUDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#No").jqxComboBox({ source: dataAdapter, displayMember: "MUName", valueMember: "MUId", width: 200, height: 25, autoDropDownHeight: true });
            if ("<%=this.mu.BaseId%>" > 0) {
                $("#No").jqxComboBox("val", "<%=this.mu.BaseId%>");
            }

            //基本单位转换率
            $("#txbTransformRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 8, Digits: 8, spinButtons: true, width: 200 });
            $("#txbTransformRate").jqxNumberInput("val", "<%=this.mu.TransformRate%>");

            //单位状态
            CreateSelectStatusDropDownList("selStatus", "<%=(int)this.mu.MUStatus%>");
            $("#selStatus").jqxDropDownList("width", 200);

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbMUName", message: "计量单位不可为空", action: "keyup,blur", rule: "required" },
                        {
                            input: "#txbTransformRate", message: "基本单位转换率不小于0", action: "change", rule: function (input, commit) {
                                if ($("#txbTransformRate").val() < 0){ return false; }
                                else{ return true; }
                            }
                        }
                    ]
            });

            //init buttons
            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认修改计量单位?")) { return; }

                var measureUnit = {
                    MUId: "<%=this.mu.MUId%>",
                    MUName: $("#txbMUName").val(),
                    BaseId: $("#No").val() != "" ? parseInt($("#No").val()) : 0,
                    TransformRate: $("#txbTransformRate").val(),
                    MUStatus: $("#selStatus").val()
                };

                $.post("Handler/MUUpdateHandler.ashx", { measureUnit: JSON.stringify(measureUnit) },
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
            计量单位修改
                        <input type="hidden" runat="server" id="HidNo" />
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="txbMUStatus" />
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
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" />
                    </span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
