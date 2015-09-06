<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BDStyleDtlView.aspx.cs" Inherits="NFMTSite.BasicData.BDStyleDtlView" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>类型明细</title>
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

            $("#btnFreeze").jqxButton({ width: 60 });
            $("#btnUnFreeze").jqxButton({ width: 60 });

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结该条数据？")) { return; }
                var detailId = $("#hidDetailId").val();
                var operateId = operateEnum.冻结;
                $.post("Handler/BDStyleDtlStatusHandler.ashx", { id: detailId, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var id = $("#hidDetailId").val();
                var operateId = operateEnum.解除冻结;
                $.post("Handler/BDStyleDtlStatusHandler.ashx", { id: id, oi: operateId },
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
            类型明细查看<input type="hidden" runat="server" id="hidDetailId" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>归属类型：</span></h4>
                    <span runat="server" id="spnStyleName"></span></li>
                <li>
                    <h4><span>归属类型状态：</span></h4>
                    <span runat="server" id="spnStyleStatus"></span></li>
                <li>
                    <h4><span>明细编号：</span></h4>
                    <span runat="server" id="spnDetailCode"></span></li>
                <li>
                    <h4><span>明细名称：</span></h4>
                    <span runat="server" id="spnDetailName"></span></li>
                <li>
                    <h4><span>明细状态：</span></h4>
                    <span runat="server" id="spnDetailStatus"></span></li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <input type="button" id="btnFreeze" value="冻结" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="btnUnFreeze" value="解除冻结" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
