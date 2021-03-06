﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingContractDetail.aspx.cs" Inherits="NFMTSite.DoPrice.PricingContractDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.pricing.DataBaseName%>" + "&t=" + "<%=this.pricing.TableName%>" + "&id=" + "<%=this.pricing.PricingId%>";

        $(document).ready(function () {
            $("#jqxPricingApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            /////////////////////////点价申请信息/////////////////////////

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });
            $("#ddlApplyCorp").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDept").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlApplyDept").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //申请备注
            $("#txbApplyDesc").jqxInput({ height: 25, disabled: true });
            $("#txbApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //点价起始时间
            $("#dtStartTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtStartTime").jqxDateTimeInput("val", new Date("<%=this.pricingApply.StartTime%>"));

            //点价最终时间
            $("#dtEndTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtEndTime").jqxDateTimeInput("val", new Date("<%=this.pricingApply.EndTime%>"));

            //点价最低均价, disabled: true
            $("#nbMinPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#nbMinPrice").jqxNumberInput("val", "<%=this.pricingApply.MinPrice%>");

            //点价最高均价
            $("#nbMaxPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#nbMaxPrice").jqxNumberInput("val", "<%=this.pricingApply.MaxPrice%>");

            //价格币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.currencyId%>");

            //点价公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlPricingCorpId").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });
            $("#ddlPricingCorpId").jqxComboBox("val", "<%=this.pricingApply.PricingCorpId%>");

            //点价重量
            $("#nbPricingWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#nbPricingWeight").jqxNumberInput("val", "<%=this.pricingApply.PricingWeight%>");

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlMUId").jqxDropDownList("val", "<%=this.mUId%>");

            //点价品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssertId").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlAssertId").jqxDropDownList("val", "<%=this.assetId%>");

            //点价权限人
            var personSource = { datatype: "json", url: "../BasicData/Handler/PricingPersonDDLHandler.ashx", async: false };
            var personDataAdapter = new $.jqx.dataAdapter(personSource);
            $("#ddlPricingPersoinId").jqxComboBox({ source: personDataAdapter, displayMember: "PricingPhone", valueMember: "PersoinId", width: 100, height: 25, disabled: true });
            $("#ddlPricingPersoinId").jqxComboBox("val", "<%=this.pricingApply.PricingPersoinId%>");

            //QP日期
            $("#dtQPDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtQPDate").jqxDateTimeInput("val", new Date("<%=this.pricingApply.QPDate%>"));

            //其他费用
            $("#nbOtherFee").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });
            $("#nbOtherFee").jqxNumberInput("val", "<%=this.pricingApply.OtherFee%>");

            //其他费用描述
            $("#txbOtherDesc").jqxInput({ height: 25, disabled: true });
            $("#txbOtherDesc").jqxInput("val", "<%=this.pricingApply.OtherDesc%>");

            //点价方式
            var summaryPriceStyle = $("#hidSummaryPrice").val();
            var summaryPriceSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + summaryPriceStyle, async: false };
            var summaryPriceDataAdapter = new $.jqx.dataAdapter(summaryPriceSource);
            $("#ddlPricingStyle").jqxDropDownList({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlPricingStyle").jqxDropDownList("val", "<%=this.pricingApply.PricingStyle%>");

            //宣布日
            $("#dtDeclareDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtDeclareDate").jqxDateTimeInput("val", new Date("<%=this.pricingApply.DeclareDate%>"));

            //均价起始计价日
            $("#dtAvgPriceStart").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtAvgPriceStart").jqxDateTimeInput("val", new Date("<%=this.pricingApply.AvgPriceStart%>"));

            //均价终止计价日
            $("#dtAvgPriceEnd").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtAvgPriceEnd").jqxDateTimeInput("val", new Date("<%=this.pricingApply.AvgPriceEnd%>"));

            /////////////////////////点价修改/////////////////////////

            //点价期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlExchangeId").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    var item = args.item;
                    var label = item.label;
                    var value = item.value;

                    var FuturesCodeSource = { datatype: "json", url: "../BasicData/Handler/FuturesCodeDDLHandler.ashx?exId=" + value, async: false };
                    var FuturesCodeDataAdapter = new $.jqx.dataAdapter(FuturesCodeSource);
                    $("#ddlFuturesCodeId").jqxComboBox({ source: FuturesCodeDataAdapter, displayMember: "TradeCode", valueMember: "FuturesCodeId", width: 100, height: 25 });
                }
            });
            $("#ddlExchangeId").jqxComboBox("val", "<%=this.pricing.ExchangeId%>");

            //点价期货合约
            var FuturesCodeSource = { datatype: "json", url: "../BasicData/Handler/FuturesCodeDDLHandler.ashx?exId" + $("#ddlExchangeId").val(), async: false };
            var FuturesCodeDataAdapter = new $.jqx.dataAdapter(FuturesCodeSource);
            $("#ddlFuturesCodeId").jqxComboBox({ source: FuturesCodeDataAdapter, displayMember: "TradeCode", valueMember: "FuturesCodeId", width: 100, height: 25, disabled: true });
            $("#ddlFuturesCodeId").jqxComboBox("val", "<%=this.pricing.FuturesCodeId%>");

            //期货合约到期日
            $("#dtFuturesCodeEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtFuturesCodeEndDate").jqxDateTimeInput("val", new Date("<%=this.pricing.FuturesCodeEndDate%>"));

            //期货成交价
            $("#txbAvgPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 160, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right", disabled: true });
            $("#txbAvgPrice").jqxNumberInput("val", "<%=this.pricing.AvgPrice%>");

            //点价重量
            $("#txbPricingWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 160, spinButtons: true, symbol: "<%=this.curMUName%>", symbolPosition: "right", disabled: true });
            $("#txbPricingWeight").val("<%=this.pricing.PricingWeight%>");

            //调期日期
            $("#dtSpotQP").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, allowNullDate: true, disabled: true });
            if ("<%=this.pricing.SpotQP%>" == "<%=NFMT.Common.DefaultValue.DefaultTime%>") {
                $("#dtSpotQP").val(null);
            } else {
                $("#dtSpotQP").val("<%=this.pricing.SpotQP%>");
            }

            //延期费
            $("#nbDelayFee").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right", disabled: true });
            $("#nbDelayFee").val("<%=this.pricing.DelayFee%>");

            //调期费
            $("#nbSpread").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right", disabled: true });
            $("#nbSpread").val(<%=this.pricing.Spread%>);

            //其他费用
            $("#nbOtherFeePricing").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right", disabled: true });
            $("#nbOtherFeePricing").val(<%=this.pricing.OtherFee%>);

            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 23,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/PricingStatusHandler.ashx", { id: "<%=this.pricing.PricingId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PricingList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/PricingStatusHandler.ashx", { id: "<%=this.pricing.PricingId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PricingList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/PricingStatusHandler.ashx", { id: "<%=this.pricing.PricingId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PricingList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/PricingStatusHandler.ashx", { id: "<%=this.pricing.PricingId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PricingList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxPricingApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            点价申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请公司：</strong>
                    <div id="ddlApplyCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div id="ddlApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请备注：</strong>
                    <input type="text" id="txbApplyDesc" style="float: left;" />
                </li>
                <li>
                    <strong>起始时间：</strong>
                    <div id="dtStartTime" style="float: left;"></div>
                </li>
                <li>
                    <strong>最终时间：</strong>
                    <div id="dtEndTime" style="float: left;"></div>
                </li>
                <li>
                    <strong>最低均价：</strong>
                    <div id="nbMinPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>最高均价：</strong>
                    <div id="nbMaxPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>价格币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价公司：</strong>
                    <div id="ddlPricingCorpId" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请重量：</strong>
                    <div id="nbPricingWeight" style="float: left;" />
                </li>
                <li>
                    <strong>重量单位：</strong>
                    <div id="ddlMUId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价品种：</strong>
                    <div id="ddlAssertId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价权限人：</strong>
                    <div id="ddlPricingPersoinId" style="float: left;"></div>
                </li>
                <li>
                    <strong>QP日期：</strong>
                    <div id="dtQPDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>其他费：</strong>
                    <div id="nbOtherFee" style="float: left;"></div>
                </li>
                <li>
                    <strong>其他费描述：</strong>
                    <input type="text" id="txbOtherDesc" style="float: left;" />
                </li>
                <li>
                    <strong>点价方式：</strong>
                    <input type="hidden" id="hidSummaryPrice" runat="server" />
                    <div id="ddlPricingStyle" style="float: left;"></div>
                </li>
                <li class="hiddenClass">
                    <strong>宣布日：</strong>
                    <div id="dtDeclareDate" style="float: left;"></div>
                </li>
                <li class="hiddenClass">
                    <strong>均价起始计价日：</strong>
                    <div id="dtAvgPriceStart" style="float: left;"></div>
                </li>
                <li class="hiddenClass">
                    <strong>均价终止计价日：</strong>
                    <div id="dtAvgPriceEnd" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            点价信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>期货市场：</strong>
                    <div id="ddlExchangeId" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货合约：</strong>
                    <div id="ddlFuturesCodeId" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货合约到期日：</strong>
                    <div id="dtFuturesCodeEndDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货成交价：</strong>
                    <div id="txbAvgPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>QP：</strong>
                    <div id="dtSpotQP" style="float: left;"></div>
                </li>
                <li>
                    <strong>延期费：</strong>
                    <div id="nbDelayFee" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价重量：</strong>
                    <div id="txbPricingWeight" style="float: left;"></div>
                </li>
                <li>
                    <strong>调期费：</strong>
                    <div id="nbSpread" style="float: left;"></div>
                </li>
                <li>
                    <strong>其他费：</strong>
                    <div id="nbOtherFeePricing" style="float: left;"></div>
                </li>

            </ul>
        </div>
    </div>
    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnComplete" value="执行完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCompleteCancel" value="执行完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
