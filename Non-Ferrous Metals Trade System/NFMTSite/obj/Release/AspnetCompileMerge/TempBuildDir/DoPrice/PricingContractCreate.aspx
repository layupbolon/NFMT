<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingContractCreate.aspx.cs" Inherits="NFMTSite.DoPrice.PricingContractCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
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
            if ("<%=this.pricingApply.PricingPersoinId%>" > 0)
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

            /////////////////////////点价新增/////////////////////////

            //点价期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true });
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

            //点价期货合约
            var FuturesCodeSource = { datatype: "json", url: "../BasicData/Handler/FuturesCodeDDLHandler.ashx?exId" + $("#ddlExchangeId").val(), async: false };
            var FuturesCodeDataAdapter = new $.jqx.dataAdapter(FuturesCodeSource);
            $("#ddlFuturesCodeId").jqxComboBox({ source: FuturesCodeDataAdapter, displayMember: "TradeCode", valueMember: "FuturesCodeId", width: 100, height: 25 });

            //期货合约到期日
            $("#dtFuturesCodeEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //期货成交价
            $("#txbAvgPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 160, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right" });

            //点价重量
            $("#txbPricingWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 160, spinButtons: true, symbol: "<%=this.curMUName%>", symbolPosition: "right" });

            //调期日期
            $("#dtSpotQP").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, allowNullDate: true });
            $("#dtSpotQP").jqxDateTimeInput("val", null);

            //延期费
            $("#nbDelayFee").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right"});

            var source =
            {
                url: "Handler/PricingDelayListHandler.ashx?p=" + "<%=this.pricingApply.PricingApplyId%>",
                datafields:
                [
                    { name: 'DelayId', type: 'int' },
                    { name: 'DelayAmount', type: 'number' },
                    { name: 'DelayAmountName', type: 'string' },
                    { name: 'DelayFee', type: 'number' },
                    { name: 'DelayFeeName', type: 'string' },
                    { name: 'DelayQP', type: 'date' }
                ],
                datatype: "json",
                updaterow: function (rowid, rowdata) {
                    // synchronize with the server - send update command   
                }
            };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#jqxDropdownbutton").jqxDropDownButton({ width: 170, height: 25, autoOpen: true });
            $("#jqxGrid").jqxGrid(
            {
                width: 320,
                source: dataAdapter,
                //pageable: true,
                autoheight: true,
                columnsresize: true,
                columns: [
                  { text: '延期Qp', datafield: 'DelayQP', width: 100, cellsformat: "yyyy-MM-dd" },
                  { text: '延期重量', datafield: 'DelayAmountName', width: 110 },
                  { text: '延期费', datafield: 'DelayFeeName', width: 110 }
                ]
            });
            $("#jqxGrid").on('rowclick', function (event) {
                var args = event.args;
                var row = $("#jqxGrid").jqxGrid('getrowdata', args.rowindex);

                if (row != undefined) {
                    $("#dtSpotQP").jqxDateTimeInput("val", row["DelayQP"]);
                    $("#nbDelayFee").jqxNumberInput("val", row["DelayFee"]);
                    $("#txbPricingWeight").jqxNumberInput("val", row["DelayAmount"]);
                    //var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 5px;">延期重量：' + row['DelayAmountName'] + '</div>';
                    //$("#jqxDropdownbutton").jqxDropDownButton('setContent', dropDownContent);
                }
                $("#jqxDropdownbutton").jqxDropDownButton("close");
            });
            //$("#jqxGrid").jqxGrid('selectrow', 0);

            //调期费
            $("#nbSpread").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right" });

            //其他费用
            $("#nbOtherFeePricing").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=this.currencyName%>", symbolPosition: "right" });

            //验证
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlExchangeId", message: "点价期货市场必选", action: "change", rule: function (input, commit) {
                                return $("#ddlExchangeId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlFuturesCodeId", message: "点价期货合约必选", action: "change", rule: function (input, commit) {
                                return $("#ddlFuturesCodeId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbAvgPrice", message: "期货成交价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbAvgPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbPricingWeight", message: "点价重量必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbPricingWeight").jqxNumberInput("val") > 0;
                            }
                        }
                        //,{
                        //    input: "#txbPricingWeight", message: "点价重量不能大于延期重量", action: "keyup,blur", rule: function (input, commit) {
                        //        if ($("#nbDelayAmount").jqxNumberInput("val") > 0) {
                        //            return $("#txbPricingWeight").jqxNumberInput("val") < $("#nbDelayAmount").jqxNumberInput("val");
                        //        }
                        //        else {
                        //            return $("#txbPricingWeight").jqxNumberInput("val") > 0;
                        //        }
                        //    }
                        //}
                    ]
            });

            //init buttons
            $("#btnCreate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnCreate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确定提交？")) { return; }

                var Pricing = {
                    PricingApplyId: "<%=this.pricingApply.PricingApplyId%>",
                    PricingWeight: $("#txbPricingWeight").val(),
                    MUId: "<%=this.mUId%>",
                    ExchangeId: $("#ddlExchangeId").val(),
                    FuturesCodeId: $("#ddlFuturesCodeId").val(),
                    FuturesCodeEndDate: $("#dtFuturesCodeEndDate").val(),
                    SpotQP: $("#dtSpotQP").val() == "" ? "<%=NFMT.Common.DefaultValue.DefaultTime%>" : $("#dtSpotQP").val(),
                    DelayFee: $("#nbDelayFee").val(),
                    Spread: $("#nbSpread").val(),
                    OtherFee: $("#nbOtherFee").val(),
                    AvgPrice: $("#txbAvgPrice").val(),
                    //PricingTime
                    CurrencyId: "<%=this.currencyId%>",
                    //Pricinger
                    AssertId: "<%=this.assetId%>",
                    PricingDirection: "<%=this.pricingApply.PricingDirection%>"
                    //PricingStatus
                    //FinalPrice
                };

                $.post("Handler/PricingCreateHandler.ashx", {
                    pricing: JSON.stringify(Pricing)
                },
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
    <nfmt:navigation runat="server" id="navigation1" />
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

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            点价新增
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
                    <strong>选择延期信息：</strong>
                    <div id="jqxDropdownbutton" style="float: left;">
                        <div style="border-color: transparent;" id="jqxGrid">
                        </div>
                    </div>
                </li>
                <li>
                    <strong>调期日期：</strong>
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

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="点价" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="PricingCanDoPriceApplyList.aspx" id="btnCancel">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

</body>
</html>
