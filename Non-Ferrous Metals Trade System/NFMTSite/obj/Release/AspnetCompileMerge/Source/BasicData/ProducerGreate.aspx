<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProducerGreate.aspx.cs" Inherits="NFMTSite.BasicData.ProducerGreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生产商新增</title>
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

            $("#txbProducerName").jqxInput({ height: 25, width: 200 });
            $("#txbProducerFullName").jqxInput({ height: 25, width: 200 });
            $("#txbProducerShort").jqxInput({ height: 25, width: 200 });

            //绑定 地区
            var url = "Handler/AreaDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "AreaId" }, { name: "AreaName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#AreaName").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 200, height: 25 });

            //initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbProducerName", message: "生产商名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbProducerFullName", message: "生产商全称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbProducerShort", message: "生产商简称不能为空", action: "keyup, blur", rule: "required" },
                    {
                        input: "#AreaName", message: "生产商地区不能为空", action: "change", rule: function (input, commit) {
                            return $("#AreaName").jqxComboBox("val") > 0;
                        }
                    }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var producerName = $("#txbProducerName").val();
                var producerFullName = $("#txbProducerFullName").val();
                var producerShort = $("#txbProducerShort").val();
                var areaName = $("#AreaName").val();

                $.post("Handler/ProducerGreateHandler.ashx", {
                    producerName: producerName,
                    producerFullName: producerFullName,
                    producerShort: producerShort,
                    areaName: areaName
                },
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
            生产商新增
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>生产商名称：</span></h4>
                    <span>
                        <input type="text" id="txbProducerName" /></span>
                </li>
                <li>
                    <h4><span>生产商全称：</span></h4>
                    <span>
                        <input type="text" id="txbProducerFullName" /></span>
                </li>
                <li>
                    <h4><span>生产商简称：</span></h4>
                    <span>
                        <input type="text" id="txbProducerShort" /></span>
                </li>
                <li>
                    <h4><span>生产商地区：</span></h4>
                    <div id="AreaName" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="ProducerList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
