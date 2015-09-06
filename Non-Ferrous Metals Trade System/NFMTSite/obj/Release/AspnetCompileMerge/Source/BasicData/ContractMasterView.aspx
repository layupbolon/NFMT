<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractMasterView.aspx.cs" Inherits="NFMTSite.BasicData.ContractMasterView" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约模板明细</title>
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

            $("#btnFreeze").jqxButton({ width: 65 });
            $("#btnUnFreeze").jqxButton({ width: 65 });

            $("#btnFreeze").on("click", function () {
                var masterId = $("#hidMasterId").val();
                var operateId = operateEnum.冻结;
                $.post("Handler/ContractMasterStatusHandler.ashx", { mi: masterId, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var masterId = $("#hidMasterId").val();
                var operateId = operateEnum.解除冻结;
                $.post("Handler/ContractMasterStatusHandler.ashx", { mi: masterId, oi: operateId },
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
            合约模板修改
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <input type="hidden" runat="server" id="hidMasterId" />
                    <h4><span>模板名称：</span></h4>
                    <span runat="server" id="spnMasterName"></span></li>
                <li>
                    <h4><span>模板英文名称：</span></h4>
                    <span runat="server" id="spnMasterEname"></span>
                </li>
                <li>
                    <h4><span>模板状态：</span></h4>
                    <span runat="server" id="spnMasterStatus"></span>
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnFreeze" value="冻结" />
                        <input type="button" id="btnUnFreeze" value="解除冻结" style="margin-left: 10px" /></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
