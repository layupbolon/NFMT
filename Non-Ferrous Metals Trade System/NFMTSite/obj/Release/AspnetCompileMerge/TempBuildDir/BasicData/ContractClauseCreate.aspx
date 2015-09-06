<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractClauseCreate.aspx.cs" Inherits="NFMTSite.BasicData.ContractClauseCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约条款添加</title>
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
            $("#txbText").jqxInput({ height: 200, width: "80%" });
            $("#txbEText").jqxInput({ height: 200, width: "80%" });
            $("#btnAdd").jqxButton();

            ////绑定显示类型
            //var styleId = $("#hidDisplayType").val();            
            //var displayTypeSource = { datatype: "json", url: "Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            //var displayDataAdapter = new $.jqx.dataAdapter(displayTypeSource);
            //$("#selDisplayType").jqxDropDownList({ source: displayDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 120, height: 25, autoDropDownHeight: true });

            ////绑定值类型
            //var valueStyleId = $("#hidValueType").val();
            //var valueTypeSource = { datatype: "json", url: "Handler/StyleDetailHandler.ashx?id=" + valueStyleId, async: false };
            //var valueDataAdapter = new $.jqx.dataAdapter(valueTypeSource);
            //$("#selValueType").jqxDropDownList({ source: valueDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 120, height: 25, autoDropDownHeight: true });           

            //$("#jqxRightExpander").jqxExpander({ height: 600, width: "18%" });
            //$("#btnAddParamter").jqxButton();

            //$("#btnAddParamter").click(function () {
            //    var displayType = $("#selDisplayType").jqxDropDownList("val");
            //    var valueType = $("#selValueType").jqxDropDownList("val");

            //    var addStr = "";

            //    if (displayType == displayTypeEnum.下拉列表) {
            //        addStr += "<select>";
            //    }
            //    else if (displayType == displayTypeEnum.文本框) {

            //        addStr += "<text>";
            //    }

            //    if (valueType == valueTypeEnum.数字) {
            //        addStr += "numeric";
            //    }
            //    else if (valueType == valueTypeEnum.字符) {
            //        addStr += "string";
            //    }

            //    if (displayType == displayTypeEnum.下拉列表) {
            //        addStr += "</select>";
            //    }
            //    else if (displayType == displayTypeEnum.文本框) {
            //        addStr += "</text>";
            //    }

            //    var tValue = $("#txbText").val() + addStr;
            //    var eValue = $("#txbEText").val() + addStr;
            //    $("#txbText").val(tValue);
            //    $("#txbEText").val(eValue);
            //});

            //$("#btnClear").jqxButton();
            //$("#btnClear").click(function () {
            //    displayParas.length = 0;
            //    valueParas.length = 0;

            //    var tValue = $("#txbText").val();
            //    tValue = tValue.replace(new RegExp("<select>numeric</select>", "g"), "");
            //    tValue = tValue.replace(new RegExp("<select>string</select>", "g"), "");
            //    tValue = tValue.replace(new RegExp("<text>numeric</text>", "g"), "");
            //    tValue = tValue.replace(new RegExp("<text>string</text>", "g"), "");
            //    $("#txbText").val(tValue);

            //    var eValue = $("#txbEText").val();
            //    eValue = eValue.replace(new RegExp("<select>numeric</select>", "g"), "");
            //    eValue = eValue.replace(new RegExp("<select>string</select>", "g"), "");
            //    eValue = eValue.replace(new RegExp("<text>numeric</text>", "g"), "");
            //    eValue = eValue.replace(new RegExp("<text>string</text>", "g"), "");
            //    $("#txbEText").val(eValue);

            //});

            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbText", message: "条款中文内容不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbEText", message: "条款英文内容不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnAdd").click(function () {
                var tValue = $("#txbText").val();
                var eValue = $("#txbEText").val();

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                $.post("Handler/ContractClauseCreateHandler.ashx", { tv: tValue, ev: eValue },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "ContractClauseList.aspx";
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
            合约条款添加
        </div>
        <div class="DataExpander" style="padding-bottom: 0px;">
            <ul>
                <li style="line-height: 200px; height: 200px;">
                    <h4><span>条款中文内容：</span></h4>

                    <textarea id="txbText"></textarea>

                </li>
                <li style="line-height: 200px; height: 200px; float: none">
                    <h4><span>条款英文内容：</span></h4>
                    <textarea id="txbEText"></textarea>
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnAdd" value="添加" style="width: 80px;" /></span>
                </li>
            </ul>
        </div>
    </div>

    <%--<div id="jqxRightExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            动态数值选择<input type="hidden" id="hidDisplayType" runat="server" /><input type="hidden" id="hidValueType" runat="server" />
        </div>
        <div class="DataExpander" style="padding-left:0px;">
            <ul>
                <li style="margin-left:0px; align-content:center;">                    
                   <input type="button" id="btnAddParamter" value="选择" style="width:80px;" />
                     <input type="button" id="btnClear" value="清空" style="width:80px;" />
                </li>
                <li style="margin-left:0px;">
                    <h4><span style="width:70px;">显示方式：</span></h4>
                    <div id="selDisplayType"></div>
                </li>
                <li style="margin-left:0px;">
                    <h4><span style="width:70px;">值类型：</span></h4>
                    <div id="selValueType"></div>
                </li>
            </ul>
        </div>
    </div>--%>
</body>
</html>
