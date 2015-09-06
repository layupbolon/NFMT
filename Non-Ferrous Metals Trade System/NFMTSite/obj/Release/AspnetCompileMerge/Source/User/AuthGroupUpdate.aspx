<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthGroupUpdate.aspx.cs" Inherits="NFMTSite.User.AuthGroupUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限组修改</title>
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
            $("#txbAuthGroupName").jqxInput({ height: 23, width: 200 });
            $("#txbAuthGroupName").jqxInput("val", "<%=this.authGroup.AuthGroupName%>");

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 200, height: 25, searchMode: "containsignorecase" });
            if ("<%=this.authGroup.AssetId%>" > 0)
                $("#ddlAssetId").jqxComboBox("val", "<%=this.authGroup.AssetId%>");

            //贸易方向
            var directionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeDirection").val(), async: false };
            var directionDataAdapter = new $.jqx.dataAdapter(directionSource);
            $("#ddlTradeDirection").jqxComboBox({ source: directionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase" });
            if ("<%=this.authGroup.TradeDirection%>" > 0)
                $("#ddlTradeDirection").jqxComboBox("val", "<%=this.authGroup.TradeDirection%>");

            //贸易境区
            var borderSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeBorder").val(), async: false };
            var borderDataAdapter = new $.jqx.dataAdapter(borderSource);
            $("#ddlTradeBorder").jqxComboBox({ source: borderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase" });
            if ("<%=this.authGroup.TradeBorder%>" > 0)
                $("#ddlTradeBorder").jqxComboBox("val", "<%=this.authGroup.TradeBorder%>");

            //内外部
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractInOut").val(), async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlContractInOut").jqxComboBox({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase" });
            if ("<%=this.authGroup.ContractInOut%>" > 0)
                $("#ddlContractInOut").jqxComboBox("val", "<%=this.authGroup.ContractInOut%>");

            //长单零单
            var limitSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractLimit").val(), async: false };
            var limitDataAdapter = new $.jqx.dataAdapter(limitSource);
            $("#ddlContractLimit").jqxComboBox({ source: limitDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 200, height: 25, searchMode: "containsignorecase" });
            if ("<%=this.authGroup.ContractLimit%>" > 0)
                $("#ddlContractLimit").jqxComboBox("val", "<%=this.authGroup.ContractLimit%>");

            //公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#ddlCorpId").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", height: 25, width: 200
            });
            if ("<%=this.authGroup.CorpId%>" > 0)
                $("#ddlCorpId").jqxComboBox("val", "<%=this.authGroup.CorpId%>");

            //状态
            CreateSelectStatusDropDownList("ddlStatus", "<%=(int)this.authGroup.AuthGroupStatus%>");
            $("#ddlStatus").jqxDropDownList("width", 200);

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbAuthGroupName", message: "权限组名称不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var authGroup = {
                    AuthGroupId: "<%=this.authGroup.AuthGroupId%>",
                    AuthGroupName: $("#txbAuthGroupName").val(),
                    AssetId: $("#ddlAssetId").val() == "" ? "0" : $("#ddlAssetId").val(),
                    TradeDirection: $("#ddlTradeDirection").val() == "" ? "0" : $("#ddlTradeDirection").val(),
                    TradeBorder: $("#ddlTradeBorder").val() == "" ? "0" : $("#ddlTradeBorder").val(),
                    ContractInOut: $("#ddlContractInOut").val() == "" ? "0" : $("#ddlContractInOut").val(),
                    ContractLimit: $("#ddlContractLimit").val() == "" ? "0" : $("#ddlContractLimit").val(),
                    CorpId: $("#ddlCorpId").val() == "" ? "0" : $("#ddlCorpId").val(),
                    AuthGroupStatus: $("#ddlStatus").val()
                }

                $.post("Handler/AuthGroupUpdateHandler.ashx", { authGroup: JSON.stringify(authGroup) },
                function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "AuthGroupList.aspx";
                    }
                });
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            权限组修改
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
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnUpdate" value="修改" runat="server" style="width: 80px;" /></span>
                    <span><a target="_self" runat="server" href="AuthGroupList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
