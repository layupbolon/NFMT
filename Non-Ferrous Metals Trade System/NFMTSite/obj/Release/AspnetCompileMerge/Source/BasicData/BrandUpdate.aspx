<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrandUpdate.aspx.cs" Inherits="NFMTSite.BasicData.BrandUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>品牌修改</title>
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

            $("#txbBrandName").jqxInput({ width: 200, height: 25 });
            $("#txbBrandFullName").jqxInput({ width: 200, height: 25 });
            $("#txbBrandInfo").jqxInput({ width: 200, height: 25 });

            var selValue = $("#hidBrandStatusName").val();
            CreateSelectStatusDropDownList("BrandStatusName", selValue);
            $("#BrandStatusName").jqxDropDownList("width", 200);

            //绑定 生产商
            var url = "Handler/ProducerDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "ProducerId" }, { name: "ProducerName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#T_producerName").jqxComboBox({ source: dataAdapter, displayMember: "ProducerName", valueMember: "ProducerId", width: 200, height: 25, autoDropDownHeight: true });
            if ($("#hidproducerName").val() > 0) { $("#T_producerName").jqxComboBox("val", $("#hidproducerName").val()); }

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbBrandName", message: "品牌名称不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbBrandFullName", message: "品牌全称不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbBrandInfo", message: "品牌备注不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var brandName = $("#txbBrandName").val();
                var brandFullName = $("#txbBrandFullName").val();
                var brandInfo = $("#txbBrandInfo").val();
                var producerName = $("#T_producerName").val();
                var brandStatusName = $("#BrandStatusName").val();

                $.post("Handler/BrandUpdateHandler.ashx", {
                    brandName: brandName,
                    brandFullName: brandFullName,
                    brandInfo: brandInfo,
                    producerName: producerName,
                    brandStatusName: brandStatusName,
                    id: "<%=Request.QueryString["id"]%>"
                },
                    function (result) {
                        alert(result);
                        document.location.href = "BrandList.aspx";
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
            品牌修改
                        <input type="hidden" id="hidproducerName" runat="server" />
            <input type="hidden" id="hidBrandStatusName" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>品牌名称：</span></h4>
                    <input type="text" id="txbBrandName" runat="server" /></li>
                <li>
                    <h4><span>品牌全称：</span></h4>
                    <input type="text" id="txbBrandFullName" runat="server" /></li>
                <li>
                    <h4><span>品牌备注：</span></h4>
                    <input type="text" id="txbBrandInfo" runat="server" /></li>
                <li>
                    <h4><span>生产商名称：</span></h4>
                    <div id="T_producerName" />
                </li>
                <li>
                    <h4><span>品牌状态：</span></h4>
                    <div id="BrandStatusName" />
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
