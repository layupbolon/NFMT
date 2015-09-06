<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractInCreateByStocks.aspx.cs" Inherits="NFMTSite.Contract.ContractInCreateByStocks" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约新增</title>
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
            $("#jqxOutCorpExpander").jqxExpander({ width: "98%" });
            $("#jqxStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxTypeExpander").jqxExpander({ width: "98%" });
            $("#jqxDetailExpander").jqxExpander({ width: "98%" });
            $("#jqxPriceExpander").jqxExpander({ width: "98%" });
            $("#jqxSubExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxClauseExpander").jqxExpander({ width: "98%" });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <div id="jqxValidator">
        <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                合约主体
            </div>
            <div class="InfoExpander">
                <ul>
                    <li>
                        <strong>签订日期：</strong>
                        <div id="txbCreateDate" style="float: left;"></div>
                    </li>
                    <li>
                        <strong>执行部门：</strong>
                        <div id="selExecDept" style="float: left;" />
                    </li>
                    <li>
                        <strong>外部合约号：</strong>
                        <input type="text" id="txbOutContractNo" />
                    </li>
                    <li>
                         <strong>合约类型：</strong>
                        <div id="selContractType" style="float: left;" />
                    </li>
                    <li>
                        <strong>我方抬头：</strong>
                        <div id="selInCorp" style="float: left;" />
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxOutCorpExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                外部公司
            </div>
            <div class="InfoExpander">
                <ul>
                    <li>
                        <strong>对方抬头：</strong>
                        <div id="selOutCorp" style="float: left;" />
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                库存列表
            </div>
            <div class="InfoExpander">
                商品名称、品牌、型号、产地、数量、单价、金额、交货时间。
                <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
            </div>
        </div>

        <div id="jqxTypeExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                合约类型及交易内容
            </div>
            <div class="InfoExpander">
                <ul>
                    <li>
                        <strong>购销方向：</strong>
                        <div id="selTradeDirection" style="float: left;" />
                        <input type="hidden" id="hidTradeDirection" runat="server" />
                    </li>
                    <li>
                        <strong>交易品种：</strong>
                        <div id="selAsset" style="float: left;" />
                    </li>
                    <li>
                        <strong>内外贸：</strong>
                        <div id="selTradeBorder" style="float: left;" />
                        <input type="hidden" id="hidTradeBorder" runat="server" />
                    </li>

                    <li>
                        <strong>交货方式：</strong>
                        <div id="cbxDeliveryStyle" style="float: left;" />
                    </li>
                    <li id="liDeliveryDate" style="display: none;">
                        <strong>交货日期：</strong>
                        <div id="txbDeliveryDate" style="float: left;" />
                    </li>

                    <li>
                        <strong>长单零单：</strong>
                        <div id="selContractLimit" style="float: left;" />
                        <input type="hidden" id="hidContractLimit" runat="server" />
                    </li>
                    <li id="liExecDate" style="display: none;">
                        <strong>执行日期：</strong>
                        <div id="txbFromExecDate" style="float: left;"></div>
                        <span style="float: left;">至</span>
                        <div id="txbToExecDate" style="float: left;"></div>
                    </li>

                    <li>
                        <strong>结算币种：</strong>
                        <div id="selCurrency" style="float: left;" />
                    </li>

                    <li>
                        <strong>签定数量：</strong>
                        <div style="float: left" id="txbSignAmount"></div>
                    </li>
                    <li>
                        <strong>单位：</strong>
                        <div id="selUnit" style="float: left;" />
                    </li>
                    <li>
                        <strong>备注：</strong>
                        <textarea id="txbMemo"></textarea>
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxPriceExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                价格明细
            </div>
            <div>
                <div id="jqxPricTabs">
                    <ul>
                        <li style="margin-left: 30px;">定价</li>
                        <li>点价</li>
                    </ul>
                    <div class="InfoExpander">
                        <ul>
                            <li>
                                <strong>基础价单价：</strong>
                                <div style="float: left" id="txbFixedPrice"></div>
                            </li>
                            <li>
                                <strong>价格备注：</strong>
                                <input type="text" id="txbFixedPriceMemo" />
                            </li>
                        </ul>
                    </div>
                    <div class="InfoExpander">
                        <ul>
                            <li>
                                <strong>点价方：</strong><input type="hidden" id="hidWhoDoPrice" runat="server" />
                                <div style="float: left" id="selWhoDoPrice"></div>
                            </li>
                            <li>
                                <strong>点价日期：</strong>
                                <div style="float: left" id="txbDoPriceBeginDate"></div>
                                <span style="float: left;">至</span>
                                <div style="float: left" id="txbDoPriceEndDate"></div>
                            </li>
                            <li>
                                <strong>初始QP：</strong>
                                <div style="float: left" id="txbInitQp"></div>
                            </li>
                            <li>
                                <strong>可否QP延期：</strong>
                                <input type="checkbox" id="chkIsQP" />
                            </li>
                            <li>
                                <strong>基础价来源：</strong>
                                <div style="float: left" id="selPriceFrom"></div>
                            </li>
                            <li>
                                <strong>作价方式：</strong><input type="hidden" id="hidSummaryPrice" runat="server" />
                                <div style="float: left" id="selPriceStyle1"></div>
                                <div style="float: left" id="selPriceStyle2"></div>
                            </li>
                            <li>
                                <strong>点价估价</strong><div style="float: left" id="txbAlmostPrice"></div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="InfoExpander">
                    <ul>
                        <li>
                            <strong>升贴水：</strong>
                            <div style="float: left" id="txbPremium"></div>
                        </li>
                        <li>
                            <strong>保证金条款：</strong>
                            <div id="selMarginMode" style="float: left;" />
                            <input type="hidden" id="hidMarginMode" runat="server" />
                        </li>
                        <li>
                            <strong>保证金数量：</strong>
                            <div style="float: left" id="selMarginAmount"></div>
                        </li>
                        <li>
                            <strong>保证金描述：</strong>
                            <textarea id="txbMarginMemo"></textarea>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div id="jqxDetailExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                费用
            </div>
            <div class="InfoExpander">
                <ul>

                    <li>
                        <strong>溢短装：</strong>
                        <div style="float: left" id="txbMoreOrLess"></div>
                    </li>
                    <li>
                        <input type="hidden" runat="server" id="hidValueRateType" />
                        <strong>延期费/率：</strong>
                        <div style="float: left" id="selDelayType"></div>
                        <div style="float: left" id="txbDelayRate"></div>
                    </li>
                    <li>
                        <strong>贴现费/率：</strong><input type="hidden" runat="server" id="hidDiscountBase" />
                        <div style="float: left" id="selDiscountBase"></div>
                        <span style="float: left;">+</span>
                        <div style="float: left" id="txbDiscountRate"></div>
                    </li>

                </ul>
            </div>
        </div>

        <div id="jqxSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                子合约
            </div>
            <div class="InfoExpander">
                <ul>
                    <li id="liSubExecDate" style="display: none;">
                        <strong>执行日期：</strong>
                        <div id="txbSubFromExecDate" style="float: left;"></div>
                        <span style="float: left;">至</span>
                        <div id="txbSubToExecDate" style="float: left;"></div>
                    </li>
                    <li>
                        <strong>装船月：</strong>
                        <div id="txbShipTime" style="float: left;" />
                    </li>
                    <li>
                        <strong>到货月：</strong>
                        <div id="txbArriveTime" style="float: left;" />
                    </li>
                    <li>
                        <strong>签定数量：</strong>
                        <div style="float: left" id="txbSubSignAmount"></div>
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxClauseExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                合约条款
            </div>
            <div>
                <%=contractBLL.GetTabString(this.user)%>
            </div>
        </div>

        <NFMT:Attach runat="server" ID="attach1" AttachStyle="Create" AttachType="ContractAttach" />

        <div style="width: 80%; text-align: center;">
            <input type="button" id="btnAduit" value="添加合约并提交审核" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" id="btnCreate" value="添加合约" />&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="ContractList.aspx" target="_self" id="btnCancel">取消</a>
        </div>

    </div>
</body>

<script type="text/javascript">
    $(document).ready(function () {

        var masterIds = new Array();

        $("#txbCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

        //对方公司
        var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
        var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
        var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
        $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", multiSelect: true, searchMode: "containsignorecase", height: 25, width: 500 });

        $("#selOutCorp").on("close", function (event) {

            var items = $("#selOutCorp").jqxComboBox("getSelectedItems");
            if (items.length > 0) {
                var item = items[0];
                outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&cid=" + item.value;

                outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
                outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
                $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", multiSelect: true, searchMode: "containsignorecase", height: 25, width: 500 });

                //重置选中项
                $("#selOutCorp").jqxComboBox("clearSelection");

                var allItems = $("#selOutCorp").jqxComboBox("getItems");
                for (var i = 0; i < allItems.length; i++) {
                    var tempItem = allItems[i];
                    for (j = 0; j < items.length; j++) {
                        var sItems = items[j];
                        if (sItems.value == tempItem.value) { $("#selOutCorp").jqxComboBox("selectItem", tempItem); }
                    }
                }
            }
        });

        $("#selOutCorp").on("unselect", function (event) {
            var items = $("#selOutCorp").jqxComboBox("getSelectedItems");
            if (items.length == 0) {
                outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
                outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
                outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
                $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", multiSelect: true, searchMode: "containsignorecase", height: 25, width: 500 });
            }
        });

        //我方公司
        var inCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?isSelf=1";
        var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
        var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
        $("#selInCorp").jqxComboBox({
            source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", multiSelect: true, searchMode: "containsignorecase", height: 25, width: 500
        });

        //执行部门
        var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
        var deptSource = { datatype: "json", url: deptUrl, async: false };
        var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
        $("#selExecDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", checkboxes: true, height: 25 });
        $("#selExecDept").on("close", function (event) {
            var items = $("#selExecDept").jqxComboBox("getCheckedItems");
            for (i = 0; i < items.length; i++) {
                var item = items[i];
                $("#selExecDept").jqxComboBox("removeItem", item);
                $("#selExecDept").jqxComboBox("insertAt", item, i);
                $("#selExecDept").jqxComboBox("checkIndex", i);
            }
        });

        //外部合约号
        $("#txbOutContractNo").jqxInput({ height: 22 });

        //合约类型
        var contractTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.合约类型%>", async: false };
        var contractTypeDataAdapter = new $.jqx.dataAdapter(contractTypeSource);
        $("#selContractType").jqxComboBox({ source: contractTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", checkboxes: true, width: 200, height: 25 });
        $("#selContractType").on("close", function (event) {
            var items = $("#selContractType").jqxComboBox("getCheckedItems");
            for (i = 0; i < items.length; i++) {
                var item = items[i];
                $("#selContractType").jqxComboBox("removeItem", item);
                $("#selContractType").jqxComboBox("insertAt", item, i);
                $("#selContractType").jqxComboBox("checkIndex", i);
            }
        });

        //采购销售
        var directionSource = { datatype: "json", url: "../BasicData/Handler/TradeDirectionHandler.ashx", async: false };
        var directionDataAdapter = new $.jqx.dataAdapter(directionSource);
        $("#selTradeDirection").jqxDropDownList({ source: directionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
        $("#selTradeDirection").val(<%=(int)NFMT.Contract.TradeDirectionEnum.采购%>);

        //交易品种
        var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetAuthHandler.ashx", async: false };
        var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
        $("#selAsset").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });

        $("#selAsset").on("change", function (event) {
            var index = event.args.index;
            var item = assetDataAdapter.records[index];
            $("#selUnit").val(item.MUId);
        });

        //外贸内贸
        var borderSource = { datatype: "json", url: "../BasicData/Handler/TradeBorderHandler.ashx", async: false };
        var borderDataAdapter = new $.jqx.dataAdapter(borderSource);
        $("#selTradeBorder").jqxDropDownList({ source: borderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 1, disabled: true });

        $("#selTradeBorder").val(<%=this.curTradeBorder%>);

        //交货方式
        var deliveryStyle = "<%=(int)NFMT.Data.StyleEnum.交货方式%>";
        var deliveryStyleSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + deliveryStyle, async: false };
        var deliveryStyleDataAdapter = new $.jqx.dataAdapter(deliveryStyleSource);
        $("#cbxDeliveryStyle").jqxComboBox({ source: deliveryStyleDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

        $("#cbxDeliveryStyle").on("change", function (event) {
            var index = event.args.index;
            var item = deliveryStyleDataAdapter.records[index];
            if (item.StyleDetailId.toString() == "<%=(int)NFMT.Contract.DeliveryStyleEnum.日期交货%>") {
                $("#liDeliveryDate").css("display", "block");
            }
            else {
                $("#liDeliveryDate").css("display", "none");
            }
        });

        //交货日期
        $("#txbDeliveryDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

        //执行日期
        $("#txbFromExecDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        $("#txbToExecDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        var tempDate = new Date();
        tempDate.setMonth(tempDate.getMonth() + 3);
        $("#txbToExecDate").jqxDateTimeInput({ value: tempDate });

        //长单零单
        var limitSource = { datatype: "json", url: "../BasicData/Handler/ContractLimitHandler.ashx", async: false };
        var limitDataAdapter = new $.jqx.dataAdapter(limitSource);
        $("#selContractLimit").jqxDropDownList({ source: limitDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });
        $("#selContractLimit").val(<%=(int)NFMT.Contract.ContractLimitEnum.零单%>);

        //备注
        $("#txbMemo").jqxInput({ width: 300, height: 22 });

        //签订数量
        $("#txbSignAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });
        $("#txbSignAmount").val(<%=this.sumGrossAmount%>);

        //升贴水
        $("#txbPremium").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

        //单位
        var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
        var muDataAdapter = new $.jqx.dataAdapter(muSource);
        $("#selUnit").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

        $("#selAsset").val(<%=this.curAssetId%>);

        //溢短装
        $("#txbMoreOrLess").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbol: "%", symbolPosition: "right", spinButtons: true, width: 100 });
        $("#txbMoreOrLess").val(0.2);

        //结算币种
        var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
        var cyDataAdapter = new $.jqx.dataAdapter(cySource);
        $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 1 });

        //延期费/率
        //selDelayType
        var valueRateTypeModeStyle = $("#hidValueRateType").val();
        var valueRateTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + valueRateTypeModeStyle, async: false };
        var valueRateTypeDataAdapter = new $.jqx.dataAdapter(valueRateTypeSource);
        $("#selDelayType").jqxDropDownList({ source: valueRateTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });
        $("#txbDelayRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

        //贴现费/率
        $("#txbDiscountRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, width: 140, spinButtons: true, symbol: "%", symbolPosition: "right" });

        var discountBaseModeStyle = $("#hidDiscountBase").val();
        var discountBaseSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + discountBaseModeStyle, async: false };
        var discountBaseDataAdapter = new $.jqx.dataAdapter(discountBaseSource);
        $("#selDiscountBase").jqxDropDownList({ source: discountBaseDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

        //定价裸单价
        $("#jqxPricTabs").jqxTabs({ width: "99%", position: "top", selectionTracker: "checked", animationType: "fade" });
        $("#txbFixedPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 8, width: 140, spinButtons: true });
        $("#txbFixedPriceMemo").jqxInput({ height: 23 });

        //点价
        //点价方
        var whoDoPriceStyle = $("#hidWhoDoPrice").val();
        var whoDoPriceSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + whoDoPriceStyle, async: false };
        var whoDoPriceDataAdapter = new $.jqx.dataAdapter(whoDoPriceSource);
        $("#selWhoDoPrice").jqxDropDownList({ source: whoDoPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });
        $("#selWhoDoPrice").val(<%=(int)NFMT.Contract.WhoDoPriceEnum.对方%>);

        //点价日期范围
        $("#txbDoPriceBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        $("#txbDoPriceEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        $("#txbDoPriceEndDate").jqxDateTimeInput({ value: tempDate });

        //初始QP
        $("#txbInitQp").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        $("#txbInitQp").jqxDateTimeInput({ value: tempDate });

        //点价裸价来源
        var exChangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
        var exChangeDataAdapter = new $.jqx.dataAdapter(exChangeSource);
        $("#selPriceFrom").jqxDropDownList({ source: exChangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

        //作价方式
        var summaryPriceStyle = $("#hidSummaryPrice").val();
        var summaryPriceSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + summaryPriceStyle, async: false };
        var summaryPriceDataAdapter = new $.jqx.dataAdapter(summaryPriceSource);
        $("#selPriceStyle1").jqxDropDownList({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 1 });
        $("#selPriceStyle2").jqxDropDownList({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

        //点价估价        
        $("#txbAlmostPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, digits: 6, width: 120, spinButtons: true });

        //价格保证金条款
        var marginModeStyle = $("#hidMarginMode").val();
        var marginModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + marginModeStyle, async: false };
        var marginModeDataAdapter = new $.jqx.dataAdapter(marginModeSource);
        $("#selMarginMode").jqxDropDownList({ source: marginModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

        //价格保证金数量
        $("#selMarginAmount").jqxNumberInput({ height: 25, decimalDigits: 4, Digits: 8, width: 140, spinButtons: true });

        //价格保证金描述
        $("#txbMarginMemo").jqxInput({ width: 300, height: 22 });

        //子合约
        //执行日期
        $("#txbSubFromExecDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        $("#txbSubToExecDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
        $("#txbSubToExecDate").jqxDateTimeInput({ value: tempDate });
        //装船月，到货月
        $("#txbShipTime").jqxDateTimeInput({ formatString: "yyyy-MM", width: 100, todayString: "Today", showFooter: true, openDelay: 200 });
        $("#txbArriveTime").jqxDateTimeInput({ formatString: "yyyy-MM", width: 100 });
        $("#txbArriveTime").jqxDateTimeInput({ value: tempDate });
        $("#txbSubSignAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });
        $("#txbSubSignAmount").val(<%=this.sumGrossAmount%>);

        $("#txbSignAmount").on("textchanged", function (event) {
            var value = event.args.value;
            $("#txbSubSignAmount").val(value);
        });

        $("#selContractLimit").on("change", function (event) {
            var index = event.args.index;
            var item = limitDataAdapter.records[index];
            if (item.StyleDetailId.toString() == "<%=(int)NFMT.Contract.ContractLimitEnum.长单%>") {
                $("#liExecDate").css("display", "block");
                $("#liSubExecDate").css("display", "block");
            }
            else {
                $("#liExecDate").css("display", "none");
                $("#liSubExecDate").css("display", "none");
            }
        });

        $("#txbFromExecDate").on("change", function (event) {
            var jsDate = event.args.date;
            $("#txbSubFromExecDate").jqxDateTimeInput({ value: jsDate });
        });
        $("#txbToExecDate").on("change", function (event) {
            var jsDate = event.args.date;
            $("#txbSubToExecDate").jqxDateTimeInput({ value: jsDate });
        });

        <%=contractBLL.GetListBoxsInit(this.user,NFMT.Contract.BLL.ContractBLL.OperateEnum.新增,0)%>

        $("#btnCreate").jqxButton({ width: 120, height: 25 });
        $("#btnCancel").jqxLinkButton({ width: 120, height: 25 });

        $("#jqxValidator").jqxValidator({
            rules:
                [
                    {
                        input: "#selOutCorp", message: "对方抬头必选", action: "keyup,blur,change", rule: function (input, commit) {
                            return $("#selOutCorp").jqxComboBox("val") > 0;
                        }
                    },
                    {
                        input: "#selInCorp", message: "我方抬头必选", action: "keyup,blur,change", rule: function (input, commit) {
                            return $("#selInCorp").jqxComboBox("val") > 0;
                        }
                    },
                    {
                        input: "#selExecDept", message: "执行部门必选", action: "keyup,blur,checkChange", rule: function (input, commit) {
                            return $("#selExecDept").jqxComboBox("getCheckedItems").length > 0;
                        }
                    },
                    {
                        input: "#txbSignAmount", message: "签订数量必须大于0", action: "keyup,blur", rule: function (input, commit) {
                            return $("#txbSignAmount").val() > 0;
                        }
                    },
                    {
                        input: "#txbFixedPrice", message: "定价基础价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                            if ($("#jqxPricTabs").jqxTabs("val") == 1) { return true; }
                            return $("#txbFixedPrice").jqxNumberInput("val") > 0;
                        }
                    }
                ]
        });

        $("#btnCreate").click(function () { ContractCreate(false); });
        $("#btnAduit").jqxButton({ width: 180, height: 25 });
        $("#btnAduit").click(function () { ContractCreate(true); });

        function ContractCreate(isAudit) {

            var isCanSubmit = $("#jqxValidator").jqxValidator("validate");
            if (!isCanSubmit) { return; }
            if (!confirm("确认添加合约？")) { return; }

            var priceMode = "<%=(int)NFMT.Contract.PriceModeEnum.定价%>";
            if ($("#jqxPricTabs").jqxTabs("val") == 1) {
                priceMode = "<%=(int)NFMT.Contract.PriceModeEnum.点价%>";
            }

            var contract = {
                ContractDate: $("#txbCreateDate").jqxDateTimeInput("val"),
                OutContractNo: $("#txbOutContractNo").val(),
                Premium: $("#txbPremium").val(),
                TradeBorder: $("#selTradeBorder").val(),
                ContractLimit: $("#selContractLimit").val(),
                TradeDirection: $("#selTradeDirection").val(),
                ContractPeriodS: $("#txbFromExecDate").jqxDateTimeInput("val"),
                ContractPeriodE: $("#txbToExecDate").jqxDateTimeInput("val"),
                InitQP: $("#txbInitQp").jqxDateTimeInput("val"),
                AssetId: $("#selAsset").val(),
                SettleCurrency: $("#selCurrency").val(),
                SignAmount: $("#txbSignAmount").val(),
                UnitId: $("#selUnit").val(),
                PriceMode: priceMode,
                Memo: $("#txbMemo").val(),
                CreateFrom: "<%=(int)NFMT.Common.CreateFromEnum.采购合约库存创建%>",
                DeliveryStyle: $("#cbxDeliveryStyle").val(),
                DeliveryDate: $("#txbDeliveryDate").val()
            };

            var contractDetail = {
                DiscountBase: $("#selDiscountBase").val(),
                DiscountType: 141,
                DiscountRate: $("#txbDiscountRate").val(),
                DelayType: $("#selDelayType").val(),
                DelayRate: $("#txbDelayRate").val(),
                MoreOrLess: $("#txbMoreOrLess").val()
            };

            var contractPrice = {
                FixedPrice: $("#txbFixedPrice").val(),
                FixedPriceMemo: $("#txbFixedPriceMemo").val(),
                WhoDoPrice: $("#selWhoDoPrice").val(),
                DoPriceBeginDate: $("#txbDoPriceBeginDate").val(),
                DoPriceEndDate: $("#txbDoPriceEndDate").val(),
                IsQP: document.getElementById("chkIsQP").checked,
                PriceFrom: $("#selPriceFrom").val(),
                PriceStyle1: $("#selPriceStyle1").val(),
                PriceStyle2: $("#selPriceStyle2").val(),
                MarginMode: $("#selMarginMode").val(),
                MarginAmount: $("#selMarginAmount").val(),
                MarginMemo: $("#txbMarginMemo").val(),
                AlmostPrice: $("#txbAlmostPrice").val()
            };

            var subContract = {
                ShipTime: $("#txbShipTime").val(),
                ArriveTime: $("#txbArriveTime").val(),
                ContractPeriodS: $("#txbSubFromExecDate").val(),
                ContractPeriodE: $("#txbSubToExecDate").val(),
                SignAmount: $("#txbSubSignAmount").val()
            };


            var outItems = $("#selOutCorp").jqxComboBox("getSelectedItems");
            var inItems = $("#selInCorp").jqxComboBox("getSelectedItems");
            var deptItems = $("#selExecDept").jqxComboBox("getCheckedItems");

            var outCorps = "";
            for (j = 0 ; j < outItems.length ; j++) {
                outCorps += "{ CorpId:" + outItems[j].value + " }";
                if (j < outItems.length - 1) { outCorps += ","; }
            }

            var inCorps = "";
            for (j = 0 ; j < inItems.length ; j++) {
                inCorps += "{ CorpId:" + inItems[j].value + " }";
                if (j < inItems.length - 1) { inCorps += ","; }
            }

            var depts = "";
            for (j = 0 ; j < deptItems.length ; j++) {
                depts += "{ DeptId:" + deptItems[j].value + " }";
                if (j < deptItems.length - 1) { depts += ","; }
            }

            //合约类型
            var contractTypeItems = $("#selContractType").jqxComboBox("getCheckedItems");
            var contractTypes = "";
            for (j = 0 ; j < contractTypeItems.length ; j++) {
                contractTypes += "{ ContractType:" + contractTypeItems[j].value + " }";
                if (j < contractTypeItems.length - 1) { contractTypes += ","; }
            }

            var fileIds = new Array();

            var files = $(":file");
            for (i = 0; i < files.length - 1; i++) {
                var item = files[i];
                fileIds.push(item.id);
            }

            var items = $("#listbox_" + $("#jqxTabs").val()).jqxListBox("getCheckedItems");
            var tabSelectedIndex = $("#jqxTabs").val();
            var selectedMasterId = masterIds[tabSelectedIndex];
            var checkedClause = "[";
            for (var i in items) {
                checkedClause += "{MasterId:selectedMasterId,ClauseId:" + items[i].value + "},";
            }
            checkedClause += "]";

            var stockLogIds = "<%=this.stockLogIds%>";

            $.post("Handler/ContractInCreateHandler.ashx", { Contract: JSON.stringify(contract), ContractDetail: JSON.stringify(contractDetail), ContractPrice: JSON.stringify(contractPrice), OutCorps: outCorps, InCorps: inCorps, Depts: depts, IsSubmitAudit: isAudit, sub: JSON.stringify(subContract), checkedClause: JSON.stringify(eval(checkedClause)), stockLogIds: stockLogIds, ContractTypes: contractTypes },
                function (result) {
                    var obj = JSON.parse(result);
                    if (obj.ResultStatus.toString() == "0") {
                        AjaxFileUpload(fileIds, obj.AffectCount, AttachTypeEnum.SubAttach);
                        AjaxFileUpload(fileIds, obj.ReturnValue.ContractId, AttachTypeEnum.ContractAttach);
                        if (isAudit) {
                            AutoSubmitAudit(MasterEnum.采购主子合约审核, JSON.stringify(obj.ReturnValue));
                        }
                    }
                    alert(obj.Message);
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "ContractList.aspx";
                    }
                }
            );
        }
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {

        var url = "Handler/SubSelectStockLogsHandler.ashx?lgs=" + "<%=this.stockLogIds%>";
        var formatedData = "";
        var totalrecords = 0;
        var source =
        {
            datatype: "json",
            datafields:
            [
               { name: "StockLogId", type: "int" },
               { name: "LogDate", type: "date" },
               { name: "RefNo", type: "string" },
               { name: "AssetName", type: "string" },
               { name: "NetAmount", type: "number" },
               { name: "MUName", type: "string" },
               { name: "CustomesType", type: "int" },
               { name: "CustomsTypeName", type: "string" },
               { name: "CardNo", type: "string" }
            ],
            sort: function () {
                $("#jqxgrid").jqxGrid("updatebounddata", "sort");
            },
            beforeprocessing: function (data) {
                var returnData = {};
                totalrecords = data.count;
                returnData.totalrecords = data.count;
                returnData.records = data.data;

                return returnData;
            },
            type: "GET",
            sortcolumn: "sl.StockLogId",
            sortdirection: "desc",
            formatdata: function (data) {
                data.pagenum = data.pagenum || 0;
                data.pagesize = data.pagesize || 10;
                data.sortdatafield = data.sortdatafield || "sl.StockLogId";
                data.sortorder = data.sortorder || "desc";
                data.filterscount = data.filterscount || 0;
                formatedData = buildQueryString(data);
                return formatedData;
            },
            url: url
        };
        var dataAdapter = new $.jqx.dataAdapter(source, {
            contentType: "application/json; charset=utf-8",
            loadError: function (xhr, status, error) {
                alert(error);
            }
        });
        $("#jqxgrid").jqxGrid(
        {
            width: "98%",
            source: dataAdapter,
            autoheight: true,
            virtualmode: true,
            sorttogglestates: 1,
            sortable: true,
            enabletooltips: true,
            rendergridrows: function (args) {
                return args.data;
            },
            columns: [
              { text: "入库日期", datafield: "LogDate", cellsformat: "yyyy-MM-dd" },
              { text: "业务单号", datafield: "RefNo" },
              { text: "卡号", datafield: "CardNo" },
              { text: "品种", datafield: "AssetName" },
              { text: "关内外", datafield: "CustomsTypeName" },
              { text: "入库净重", datafield: "NetAmount" },
              { text: "重量单位", datafield: "MUName" }
            ]
        });

    });
</script>

</html>
