<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProducerUpdate.aspx.cs" Inherits="NFMTSite.BasicData.ProducerUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生产商修改</title>
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

            $("#txbProducerName").jqxInput({ width: 200, height: 23 });
            $("#txbProducerFullName").jqxInput({ width: 200, height: 23 });
            $("#txbProducerShort").jqxInput({ width: 200, height: 23 });

            var selValue = $("#hidStatusName").val();
            CreateSelectStatusDropDownList("StatusName", selValue);
            $("#StatusName").jqxDropDownList("width", 200);

            //绑定 地区
            var url = "Handler/AreaDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "AreaId" }, { name: "AreaName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#AreaName").jqxComboBox({ source: dataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 200, height: 25 });
            if ($("#hidAreaName").val() > 0) { $("#AreaName").jqxComboBox("val", $("#hidAreaName").val()); }

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbProducerName", message: "生产商名称不可为空", action: "keyup,blur", rule: "required" },
                          { input: "#txbProducerFullName", message: "生产商全称不可为空", action: "keyup,blur", rule: "required" },
                            { input: "#txbProducerShort", message: "生产商简称不可为空", action: "keyup,blur", rule: "required" },
                    ]
            });

            $("#btnAdd").jqxButton({ width: "80" });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var producerName = $("#txbProducerName").val();
                var producerFullName = $("#txbProducerFullName").val();
                var producerShort = $("#txbProducerShort").val();
                var areaName = $("#AreaName").val();
                var statusName = $("#StatusName").val();

                $.post("Handler/ProducerUpdateHandler.ashx", {
                    producerName: producerName,
                    producerFullName: producerFullName,
                    producerShort: producerShort,
                    areaName: areaName,
                    statusName: statusName,
                    id: "<%=Request.QueryString["id"]%>"
                },
                    function (result) {
                        alert(result);
                        document.location.href = "ProducerList.aspx";
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
            生产商修改
            <input type="hidden" id="hidAreaName" runat="server" />
            <input type="hidden" id="hidStatusName" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>

                <li>
                    <h4><span>生产商名称：</span></h4>
                    <input type="text" id="txbProducerName" runat="server" /></li>
                <li>
                    <h4><span>生产商全称：</span></h4>
                    <input type="text" id="txbProducerFullName" runat="server" /></li>
                <li>
                    <h4><span>生产商简称：</span></h4>
                    <input type="text" id="txbProducerShort" runat="server" /></li>
                <li>
                    <h4><span>生产商地区：</span></h4>
                    <div id="AreaName" />
                </li>
                <li>
                    <h4><span>生产商状态：</span></h4>
                    <div id="StatusName" />
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="提交" runat="server" style="margin-left: 65px" /></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
