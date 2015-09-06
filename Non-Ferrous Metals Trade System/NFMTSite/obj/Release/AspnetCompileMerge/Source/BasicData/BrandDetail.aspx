<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrandDetail.aspx.cs" Inherits="NFMTSite.BasicData.BrandDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>品牌明细</title>
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
            

            $("#btnFreeze").jqxButton({ height: 25, width: 96 });
            $("#btnUnFreeze").jqxButton({ height: 25, width: 96 });

            $("#btnFreeze").on("click", function () {
                var id = $("#hidId").val();
                var operateId = operateEnum.冻结;
                $.post("Handler/BrandStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var id = $("#hidId").val();
                var operateId = operateEnum.解除冻结;
                $.post("Handler/BrandStatusHandler.ashx", { id: id, oi: operateId },
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
            品牌明细
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>生产商名称：</span></h4>
                    <span id="producerName" runat="server" />
                    <input type="hidden" runat="server" id="hidId" />
                </li>
                <li>
                    <h4><span>品牌名称：</span></h4>
                    <span id="txbBrandName" runat="server" /></li>
                <li>
                    <h4><span>品牌全称：</span></h4>
                    <span id="txbBrandFullName" runat="server"></span></li>
                <li>
                    <h4><span>品牌备注：</span></h4>
                    <span id="txbBrandInfo" runat="server"></span></li>
                <li>
                    <h4><span>品牌状态：</span></h4>
                    <span id="BrandStatusName" runat="server"></span></li>
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
