<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingApplyDelayUpdate.aspx.cs" Inherits="NFMTSite.DoPrice.PricingApplyDelayUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价申请延期修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#jqxDelayExpander").jqxExpander({ width: "98%" });
            $("#jqxPricingApplyExpander").jqxExpander({ width: "98%" });

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
            var outCorpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: outCorpUrl, async: false
            };
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
            $("#ddlPricingPersoinId").jqxComboBox({ source: personDataAdapter, displayMember: "PricingName", valueMember: "PersoinId", width: 100, height: 25, disabled: true });
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
            $("#ddlPricingStyle").jqxDropDownList({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, disabled: true });
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

            //延期重量
            $("#nbDelayAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, symbol: "<%=NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == mUId).MUName%>", symbolPosition: "right" });
            $("#nbDelayAmount").jqxNumberInput("val", "<%=this.pricingApplyDelay.DelayAmount%>");

            //延期费
            $("#nbDelayFee").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, symbol: "<%=NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == currencyId).CurrencyName%>", symbolPosition: "right" });
            $("#nbDelayFee").jqxNumberInput("val", "<%=this.pricingApplyDelay.DelayFee%>");

            //延期QP
            $("#dtDelayQP").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#dtDelayQP").jqxDateTimeInput("val", new Date("<%=this.pricingApplyDelay.DelayQP%>"));

            //buttons
            $("#btnCreate").jqxButton({ height: 25, width: 120 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 120 });

            //验证
            $("#jqxDelayExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#nbDelayAmount", message: "延期重量必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbDelayAmount").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbDelayFee", message: "延期费必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbDelayFee").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //修改
            $("#btnCreate").click(function () {
                var isCanSubmit = $("#jqxDelayExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认提交？")) { return; }
                $("#btnCreate").jqxButton({ disabled: true });
                var delay = {
                    DelayId:"<%=this.pricingApplyDelay.DelayId%>",
                    PricingApplyId: "<%=this.pricingApplyId%>",
                    DelayAmount: $("#nbDelayAmount").val(),
                    DelayFee: $("#nbDelayFee").val(),
                    DelayQP: $("#dtDelayQP").val()
                    //DetailStatus
                }

                $.post("Handler/PricingApplyDelayUpdateHandler.ashx", {
                    delay: JSON.stringify(delay)
                },
                function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    $("#btnCreate").jqxButton({ disabled: false });
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "PricingApplyDelayList.aspx";
                    }
                });
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
            <input type="hidden" id="hidModel" runat="server" />
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
                    <strong>点价重量：</strong>
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

    <div id="jqxDelayExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            点价延期信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>延期重量：</strong>
                    <div id="nbDelayAmount" style="float: left;"></div>
                </li>
                <li>
                    <strong>延期费：</strong>
                    <div id="nbDelayFee" style="float: left;"></div>
                </li>
                <li>
                    <strong>延期QP：</strong>
                    <div id="dtDelayQP" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="PricingApplyDelayList.aspx" id="btnCancel">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
