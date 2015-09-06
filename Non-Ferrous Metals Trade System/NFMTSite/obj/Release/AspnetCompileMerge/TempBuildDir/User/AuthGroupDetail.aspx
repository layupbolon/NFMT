<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthGroupDetail.aspx.cs" Inherits="NFMTSite.User.AuthGroupDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限组明细</title>
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

            //权限组名称
            $("#txbAuthGroupName").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbAuthGroupName").jqxInput("val", "<%=this.authGroup.AuthGroupName%>");

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 200, height: 25, searchMode: "containsignorecase", disabled: true });
            if ("<%=this.authGroup.AssetId%>" > 0)
                $("#ddlAssetId").jqxComboBox("val", "<%=this.authGroup.AssetId%>");
            else
                $("#ddlAssetId").jqxComboBox("val", "全部");

            //贸易方向
            var directionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeDirection").val(), async: false };
            var directionDataAdapter = new $.jqx.dataAdapter(directionSource);
            $("#ddlTradeDirection").jqxComboBox({ source: directionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase", disabled: true });
            if ("<%=this.authGroup.TradeDirection%>" > 0)
                $("#ddlTradeDirection").jqxComboBox("val", "<%=this.authGroup.TradeDirection%>");
            else
                $("#ddlTradeDirection").jqxComboBox("val", "全部");

            //贸易境区
            var borderSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeBorder").val(), async: false };
            var borderDataAdapter = new $.jqx.dataAdapter(borderSource);
            $("#ddlTradeBorder").jqxComboBox({ source: borderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase", disabled: true });
            if ("<%=this.authGroup.TradeBorder%>" > 0)
                $("#ddlTradeBorder").jqxComboBox("val", "<%=this.authGroup.TradeBorder%>");
            else
                $("#ddlTradeBorder").jqxComboBox("val", "全部");

            //内外部
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractInOut").val(), async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlContractInOut").jqxComboBox({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase", disabled: true });
            if ("<%=this.authGroup.ContractInOut%>" > 0)
                $("#ddlContractInOut").jqxComboBox("val", "<%=this.authGroup.ContractInOut%>");
            else
                $("#ddlContractInOut").jqxComboBox("val", "全部");

            //长单零单
            var limitSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractLimit").val(), async: false };
            var limitDataAdapter = new $.jqx.dataAdapter(limitSource);
            $("#ddlContractLimit").jqxComboBox({ source: limitDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase", disabled: true });
            if ("<%=this.authGroup.ContractLimit%>" > 0)
                $("#ddlContractLimit").jqxComboBox("val", "<%=this.authGroup.ContractLimit%>");
            else
                $("#ddlContractLimit").jqxComboBox("val", "全部");

            //公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#ddlCorpId").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", height: 25, width: 200, disabled: true
            });
            if ("<%=this.authGroup.CorpId%>" > 0)
                $("#ddlCorpId").jqxComboBox("val", "<%=this.authGroup.CorpId%>");
            else
                $("#ddlCorpId").jqxComboBox("val", "全部");

            //状态
            CreateSelectStatusDropDownList("ddlStatus", "<%=(int)this.authGroup.AuthGroupStatus%>");
            $("#ddlStatus").jqxDropDownList("width", 200);
            $("#ddlStatus").jqxDropDownList("disabled", true);

            $("#btnFreeze").jqxInput();
            $("#btnUnFreeze").jqxInput();

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结？")) { return; }
                var operateId = operateEnum.冻结;
                $.post("Handler/AuthGroupStatusHandler.ashx", { id: "<%=this.authGroup.AuthGroupId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "AuthGroupList.aspx";
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                if (!confirm("确认解除冻结？")) { return; }
                var operateId = operateEnum.解除冻结;
                $.post("Handler/AuthGroupStatusHandler.ashx", { id: "<%=this.authGroup.AuthGroupId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "AuthGroupList.aspx";
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
            权限组明细
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">权限组名称：</span>
                    <input type="text" id="txbAuthGroupName" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">贸易方向：</span>
                    <input type="hidden" id="hidTradeDirection" runat="server" />
                    <div id="ddlTradeDirection" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">贸易境区：</span>
                    <input type="hidden" id="hidTradeBorder" runat="server" />
                    <div id="ddlTradeBorder" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">内外部：</span>
                    <input type="hidden" id="hidContractInOut" runat="server" />
                    <div id="ddlContractInOut" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">长零单：</span>
                    <input type="hidden" id="hidContractLimit" runat="server" />
                    <div id="ddlContractLimit" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">公司：</span>
                    <div id="ddlCorpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">权限组状态：</span>
                    <div id="ddlStatus" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>
    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnFreeze" value="冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
