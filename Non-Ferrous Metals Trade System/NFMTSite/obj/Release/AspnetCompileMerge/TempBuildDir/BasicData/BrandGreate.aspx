<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrandGreate.aspx.cs" Inherits="NFMTSite.BasicData.BrandGreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>品牌新增</title>
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

            $("#txbBrandName").jqxInput({ height: 25, width: 200 });
            $("#txbBrandFullName").jqxInput({ height: 25, width: 200 });
            $("#txbBrandInfo").jqxInput({ height: 25, width: 200 });

            //绑定 生产商
            var url = "Handler/ProducerDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "ProducerId" }, { name: "ProducerName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#T_producerName").jqxComboBox({ source: dataAdapter, displayMember: "ProducerName", valueMember: "ProducerId", width: 200, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbBrandName", message: "品牌名称不可为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbBrandFullName", message: "品牌全称不可为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbBrandInfo", message: "品牌备注不可为空", action: "keyup, blur", rule: "required" },
                    {
                        input: "#T_producerName", message: "生产商名称不可为空", action: "change", rule: function (input, commit) {
                            return $("#T_producerName").jqxComboBox("val") > 0;
                        }
                    }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var brandName = $("#txbBrandName").val();
                var brandFullName = $("#txbBrandFullName").val();
                var brandInfo = $("#txbBrandInfo").val();
                var producerName = $("#T_producerName").val();

                $.post("Handler/BrandGreateHandler.ashx", {
                    brandName: brandName,
                    brandFullName: brandFullName,
                    brandInfo: brandInfo,
                    producerName: producerName
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
            品牌新增
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>品牌名称：</span></h4>
                    <span>
                        <input type="text" id="txbBrandName" /></span>
                </li>
                <li>
                    <h4><span>品牌全称：</span></h4>
                    <span>
                        <input type="text" id="txbBrandFullName" /></span>
                </li>
                <li>
                    <h4><span>品牌备注：</span></h4>
                    <span>
                        <input type="text" id="txbBrandInfo" /></span>
                </li>
                <li>
                    <h4><span>生产商名称：</span></h4>
                    <div id="T_producerName" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="BrandList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
