<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreToRealDetail.aspx.cs" Inherits="NFMTSite.WareHouse.PreToRealDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预入库转正式库存明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/ajaxfileupload.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxStockInfoExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            //////////////////////////库存信息//////////////////////////            

            //库存编号
            $("#txbStockNo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbStockNo").jqxInput("val", "<%=this.stock.StockNo%>");

            //入库时间
            $("#dtStockDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtStockDate").jqxDateTimeInput("val", new Date("<%=this.stock.StockDate%>"));

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxDropDownList({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 120, height: 25, selectedIndex: 0, disabled: true });
            $("#ddlAssetId").jqxDropDownList("val", "<%=this.stock.AssetId%>");

            //捆数
            $("#nbBundles").jqxNumberInput({ width: 120, height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, disabled: true });
            $("#nbBundles").jqxNumberInput("val", "<%=this.stock.Bundles%>");

            //入库毛量
            $("#nbGrossAmount").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, spinButtons: true, disabled: true });
            $("#nbGrossAmount").jqxNumberInput("val", "<%=this.stock.GrossAmount%>");

            //入库净重
            $("#nbNetAmount").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, spinButtons: true, disabled: true });
            $("#nbNetAmount").jqxNumberInput("val", "<%=this.stock.NetAmount%>");

            //入库回执磅差
            $("#nbReceiptInGap").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, spinButtons: true, disabled: true });
            $("#nbReceiptInGap").jqxNumberInput("val", "<%=this.stock.ReceiptInGap%>");

            //出库回执磅差
            $("#nbReceiptOutGap").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, spinButtons: true, disabled: true });
            $("#nbReceiptOutGap").jqxNumberInput("val", "<%=this.stock.ReceiptOutGap%>");

            //当前毛重
            $("#nbCurGrossAmount").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, spinButtons: true, disabled: true });
            $("#nbCurGrossAmount").jqxNumberInput("val", "<%=this.stock.CurGrossAmount%>");

            //当前净重
            $("#nbCurNetAmount").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, spinButtons: true, disabled: true });
            $("#nbCurNetAmount").jqxNumberInput("val", "<%=this.stock.CurNetAmount%>");

            //计量单位
            var ddlMUIdurl = "../BasicData/Handler/MUDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlUintId").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "MUName", valueMember: "MUId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlUintId").jqxComboBox("val", "<%=this.stock.UintId%>");

            //交货地
            var ddlDeliverPlaceIdurl = "../BasicData/Handler/DeliverPlaceDDLHandler.ashx";
            var ddlDeliverPlaceIdsource = { datatype: "json", datafields: [{ name: "DPId" }, { name: "DPName" }], url: ddlDeliverPlaceIdurl, async: false };
            var ddlDeliverPlaceIddataAdapter = new $.jqx.dataAdapter(ddlDeliverPlaceIdsource);
            $("#ddlDeliverPlaceId").jqxComboBox({ selectedIndex: 0, source: ddlDeliverPlaceIddataAdapter, displayMember: "DPName", valueMember: "DPId", width: 120, height: 25, disabled: true });
            $("#ddlDeliverPlaceId").jqxComboBox("val", "<%=this.stock.DeliverPlaceId%>");

            //生产商
            var ddlProducterurl = "../BasicData/Handler/ProducerDDLHandler.ashx";
            var ddlProductersource = { datatype: "json", datafields: [{ name: "ProducerId" }, { name: "ProducerName" }], url: ddlProducterurl, async: false };
            var ddlProducterdataAdapter = new $.jqx.dataAdapter(ddlProductersource);
            $("#ddlProducerId").jqxComboBox({ selectedIndex: 0, source: ddlProducterdataAdapter, displayMember: "ProducerName", valueMember: "ProducerId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlProducerId").jqxComboBox("val", "<%=this.stock.ProducerId%>");

            //品牌
            var ddlBrandIdurl = "../BasicData/Handler/BrandDDLHandler.ashx?pid=" + $("#ddlProducter").val();
            var ddlBrandIdsource = { datatype: "json", datafields: [{ name: "BrandId" }, { name: "BrandName" }], url: ddlBrandIdurl, async: false };
            var ddlBrandIddataAdapter = new $.jqx.dataAdapter(ddlBrandIdsource);
            $("#ddlBrandId").jqxComboBox({ selectedIndex: 0, source: ddlBrandIddataAdapter, displayMember: "BrandName", valueMember: "BrandId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlBrandId").jqxComboBox("val", "<%=this.stock.BrandId%>");

            //报关状态
            var styleId = $("#hidBDStyleId").val();
            var ddlStockTypesource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var ddlStockTypedataAdapter = new $.jqx.dataAdapter(ddlStockTypesource);
            $("#ddlCustomsType").jqxDropDownList({ source: ddlStockTypedataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCustomsType").jqxDropDownList("val", "<%=this.stock.CustomsType%>");

            //入账公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCorpId").jqxComboBox({ selectedIndex: 0, source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCorpId").jqxComboBox("val", "<%=this.stock.CorpId%>");

            //权证编号
            $("#txbPaperNo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbPaperNo").jqxInput("val", "<%=this.stock.PaperNo%>");

            //单据保管人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlPaperHolder").jqxComboBox({ selectedIndex: 0, source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlPaperHolder").jqxInput("val", "<%=this.stock.PaperHolder%>");

            //卡号
            $("#txbCardNo").jqxInput({ height: 25, width: 120 });
            $("#txbCardNo").jqxInput("val", "<%=this.stock.CardNo%>");

            //提单号
            $("#txbStockNo").jqxInput({ height: 25, width: 120 });
            $("#txbStockNo").jqxInput("val", "<%=this.stock.StockNo%>");

            //业务单号
            $("#txbRefNo").jqxInput({ height: 25, width: 120 });
            $("#txbRefNo").jqxInput("val", "<%=this.stockName.RefNo%>");

            //单据类型
            var stockTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.StockType%>", async: false };
            var stockTypeDataAdapter = new $.jqx.dataAdapter(stockTypeSource);
            $("#selStockType").jqxComboBox({ source: stockTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //入库类型
            var stockOperateTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.出入库类型%>", async: false };
            var stockOperateTypeDataAdapter = new $.jqx.dataAdapter(stockOperateTypeSource);
            $("#selStockOperateType").jqxComboBox({ source: stockOperateTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStockInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>入库时间：</strong>
                    <div id="dtStockDate" style="float: left;" />
                </li>
                <li>
                    <strong>品种：</strong>
                    <div id="ddlAssetId" style="float: left;" />
                </li>
                <li>
                    <strong>捆数：</strong>
                    <div id="nbBundles" style="float: left;" />
                </li>
                <li>
                    <strong>入库毛量：</strong>
                    <div id="nbGrossAmount" style="float: left;" />
                </li>
                <li>
                    <strong>入库净量：</strong>
                    <div style="float: left" id="nbNetAmount"></div>
                </li>
                <li>
                    <strong>入库回执磅差：</strong>
                    <div id="nbReceiptInGap" style="float: left;" />
                </li>
                <li>
                    <strong>出库回执磅差：</strong>
                    <div style="float: left" id="nbReceiptOutGap"></div>
                </li>
                <li>
                    <strong>当前毛重：</strong>
                    <div style="float: left" id="nbCurGrossAmount"></div>
                </li>
                <li>
                    <strong>当前净重：</strong>
                    <div style="float: left" id="nbCurNetAmount"></div>
                </li>
                <li>
                    <strong>计量单位：</strong>
                    <div style="float: left" id="ddlUintId"></div>
                </li>
                <li>
                    <strong>交货地：</strong>
                    <div style="float: left" id="ddlDeliverPlaceId"></div>
                </li>
                <li>
                    <strong>生产商：</strong>
                    <div style="float: left" id="ddlProducerId"></div>
                </li>
                <li>
                    <strong>品牌：</strong>
                    <div style="float: left" id="ddlBrandId"></div>
                </li>
                <li>
                    <input type="hidden" id="hidBDStyleId" runat="server" />
                    <strong>报关状态：</strong>
                    <div style="float: left" id="ddlCustomsType"></div>
                </li>
                <li>
                    <strong>所属公司：</strong>
                    <div style="float: left" id="ddlCorpId"></div>
                </li>              
                <li>
                    <strong>权证编号：</strong>
                    <input type="text" style="float: left" id="txbPaperNo" />
                </li>
                <li>
                    <strong>单据保管人：</strong>
                    <div style="float: left" id="ddlPaperHolder" />
                </li>
                <%--<li>
                    <strong>卡号：</strong>
                    <input type="text" style="float: left" id="txbCardNo" />
                </li>--%>
                <%--<li>
                    <strong>库存状态：</strong>
                    <span id="spStockStatus" style="float: left" runat="server" />
                </li>--%>
            </ul>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            预入库转正式库存信息
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">卡号：</span>
                    <input type="text" style="float: left" id="txbCardNo" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">提单号：</span>
                    <input type="text" style="float: left" id="txbStockNo" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">业务单号：</span>
                    <input type="text" id="txbRefNo" style="float: left;" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">单据类型：</span>
                    <div id="selStockType" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">入库类型：</span>
                    <div id="selStockOperateType" style="float: left;"></div>
                </li>                             
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="StockAttach" />
</body>
</html>
