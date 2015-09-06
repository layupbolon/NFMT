<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopLossApplyUpdate.aspx.cs" Inherits="NFMTSite.DoPrice.StopLossApplyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>止损申请修改</title>
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
            $("#jqxPricingExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxStopLossExpander").jqxExpander({ width: "98%" });

            /////////////////////////点价申请信息/////////////////////////

            //点价申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlPricingApplyDept").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });
            $("#ddlPricingApplyDept").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

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
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.pricing.CurrencyId%>");

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
            $("#ddlMUId").jqxDropDownList("val", "<%=this.pricing.MUId%>");

            //点价品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssertId").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlAssertId").jqxDropDownList("val", "<%=this.pricing.AssertId%>");

            //点价权限人
            var personSource = { datatype: "json", url: "../BasicData/Handler/PricingPersonDDLHandler.ashx", async: false };
            var personDataAdapter = new $.jqx.dataAdapter(personSource);
            $("#ddlPricingPersoinId").jqxComboBox({ source: personDataAdapter, displayMember: "PricingPhone", valueMember: "PersoinId", width: 100, height: 25, disabled: true });
            $("#ddlPricingPersoinId").jqxComboBox("val", "<%=this.pricingApply.PricingPersoinId%>");

            //点价期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxDropDownList({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlExchangeId").jqxDropDownList("val", "<%=this.pricing.ExchangeId%>");

            //点价期货合约
            var FuturesCodeSource = { datatype: "json", url: "../BasicData/Handler/FuturesCodeDDLHandler.ashx?exId" + $("#ddlExchangeId").val(), async: false };
            var FuturesCodeDataAdapter = new $.jqx.dataAdapter(FuturesCodeSource);
            $("#ddlFuturesCodeId").jqxComboBox({ source: FuturesCodeDataAdapter, displayMember: "TradeCode", valueMember: "FuturesCodeId", width: 100, height: 25, disabled: true });
            $("#ddlFuturesCodeId").jqxComboBox("val", "<%=this.pricing.FuturesCodeId%>");

            //点价均价
            $("#txbAvgPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 160, spinButtons: true, symbol: "<%=this.currency.CurrencyName%>", symbolPosition: "right", disabled: true });
            $("#txbAvgPrice").jqxNumberInput("val", "<%=this.pricing.AvgPrice%>");

            //点价重量
            $("#txbPricingWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 160, spinButtons: true, symbol: "<%=this.measureUnit.MUName%>", symbolPosition: "right", disabled: true });
            $("#txbPricingWeight").val(<%=this.pricing.PricingWeight%>);


            /////////////////////////点价库存信息列表/////////////////////////
            if ($("#hidHasDetail").val() == "True") {

                var formatedData = "";
                var totalrecords = 0;
                var selectSource =
                {
                    datatype: "json",
                    datafields:
                    [
                       { name: "DetailId", type: "int" },
                       { name: "StockId", type: "int" },
                       { name: "StockLogId", type: "int" },
                       { name: "RefNo", type: "string" },
                       { name: "StockDate", type: "date" },
                       { name: "GrossAmount", type: "number" },
                       { name: "MUName", type: "string" },
                       { name: "AssetName", type: "string" },
                       { name: "BrandName", type: "string" },
                       { name: "DPName", type: "string" },
                       { name: "CardNo", type: "string" },
                       { name: "GrossAmoutName", type: "string" },
                       { name: "CorpName", type: "string" },
                       { name: "StockStatusName", type: "string" },
                       { name: "PricingWeight", type: "number" },
                       { name: "StopLossWeight", type: "number" },
                       { name: "alreadyStopLossWeight", type: "number" }
                    ],
                    sort: function () {
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "sort");
                    },
                    beforeprocessing: function (data) {
                        var returnData = {};
                        totalrecords = data.count;
                        returnData.totalrecords = data.count;
                        returnData.records = data.data;

                        return returnData;
                    },
                    type: "GET",
                    sortcolumn: "pd.DetailId",
                    sortdirection: "desc",
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 10;
                        data.sortdatafield = data.sortdatafield || "pd.DetailId";
                        data.sortorder = data.sortorder || "desc";
                        data.filterscount = data.filterscount || 0;
                        formatedData = buildQueryString(data);
                        return formatedData;
                    },
                    url: "Handler/StopLossApplyUpdateStockListHandler.ashx?stopLossApplyId=" + "<%=this.stopLossApply.StopLossApplyId%>"
                };
                var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                    contentType: "application/json; charset=utf-8",
                    loadError: function (xhr, status, error) {
                        alert(error);
                    }
                });

                $("#jqxStockGrid").jqxGrid(
                {
                    width: "98%",
                    source: selectDataAdapter,
                    autoheight: true,
                    virtualmode: true,
                    selectionmode: "singlecell",
                    sorttogglestates: 1,
                    sortable: true,
                    editable: true,
                    enabletooltips: true,
                    rendergridrows: function (args) {
                        return args.data;
                    },
                    columns: [
                      { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd", editable: false },
                      { text: "业务单号", datafield: "RefNo", editable: false },
                      { text: "品种", datafield: "AssetName", editable: false },
                      { text: "品牌", datafield: "BrandName", editable: false },
                      { text: "交货地", datafield: "DPName", editable: false },
                      { text: "卡号", datafield: "CardNo", editable: false },
                      { text: "库存重量", datafield: "GrossAmoutName", editable: false },
                      { text: "归属公司", datafield: "CorpName", editable: false },
                      { text: "库存状态", datafield: "StockStatusName", editable: false },
                      { text: "点价重量", datafield: "PricingWeight", editable: false },
                      { text: "已止损重量", datafield: "alreadyStopLossWeight", editable: false },
                      {
                          text: "止损重量", datafield: "StopLossWeight", width: 120, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber", validation: function (cell, value) {
                              var item = $("#jqxStockGrid").jqxGrid("getrowdata", cell.row);
                              if (value < 0 || value > (item.GrossAmount - item.alreadyStopLossWeight)) {
                                  return { result: false, message: "止损重量不能小于0且不能大于：" + (item.GrossAmount - item.alreadyStopLossWeight) };
                              }
                              return true;
                          }, createeditor: function (row, cellvalue, editor) {
                              var r = $("#jqxStockGrid").jqxGrid("getrowdata", row);
                              editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 120, Digits: 8, spinButtons: true, symbolPosition: "right", symbol: r.MUName });
                          }
                      }
                    ]
                });

                $("#jqxStockGrid").on("cellvaluechanged", function (event) {
                    var column = args.datafield;
                    var newvalue = args.newvalue;
                    var oldvalue = args.oldvalue;
                    if (oldvalue == undefined) { oldvalue = 0; }
                    if (column == "StopLossWeight") {
                        var value = parseFloat($("#nbStopLossWeight").val()) - parseFloat(oldvalue) + parseFloat(newvalue);
                        $("#nbStopLossWeight").val(value);
                    }
                });
            }
            else {
                $("#jqxStockExpander").jqxExpander("destroy");
            }
            /////////////////////////止损申请修改/////////////////////////

            //止损申请公司
            ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId=" + "<%=this.pricingApply.SubContractId%>";
            ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlStopLossApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });
            $("#ddlStopLossApplyCorp").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            //止损申请部门
            var ddlStopLossApplyDepturl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlStopLossApplyDeptsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlStopLossApplyDepturl, async: false };
            var ddlStopLossApplyDeptdataAdapter = new $.jqx.dataAdapter(ddlStopLossApplyDeptsource);
            $("#ddlStopLossApplyDept").jqxComboBox({ source: ddlStopLossApplyDeptdataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlStopLossApplyDept").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //申请备注
            $("#txbStopLossApplyDesc").jqxInput({ height: 25 });
            $("#txbStopLossApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //止损品种
            assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlStopLossAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlStopLossAsset").jqxDropDownList("val", "<%=this.pricing.AssertId%>");

            //止损价格
            $("#nbStopLossPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 160, spinButtons: true, symbol: "<%=this.currency.CurrencyName%>", symbolPosition: "right" });
            $("#nbStopLossPrice").jqxNumberInput("val", "<%=this.stopLossApply.StopLossPrice%>");

            //止损币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlStopLossCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossCurrencyId").jqxDropDownList("val", "<%=this.pricing.CurrencyId%>");

            //止损重量
            $("#nbStopLossWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 160, spinButtons: true, symbol: "<%=this.measureUnit.MUName%>", symbolPosition: "right" });
            $("#nbStopLossWeight").jqxNumberInput("val", "<%=this.stopLossApply.StopLossWeight%>");
            if ($("#hidHasDetail").val() == "True") {
                $("#nbStopLossWeight").jqxNumberInput({ disabled: true });
            }

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlStopLossMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossMUId").jqxDropDownList("val", "<%=this.pricing.MUId%>");


            //buttons
            $("#btnUpdate").jqxButton({ height: 25, width: 120 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 120 });

            //验证
            $("#jqxStopLossExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlStopLossApplyCorp", message: "止损申请公司必选", action: "change", rule: function (input, commit) {
                                return $("#ddlStopLossApplyCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlStopLossApplyDept", message: "申请部门必选", action: "change", rule: function (input, commit) {
                                return $("#ddlStopLossApplyDept").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#nbStopLossPrice", message: "止损价格必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbStopLossPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbStopLossWeight", message: "止损重量必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbStopLossWeight").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //修改
            $("#btnUpdate").click(function () {

                var isCanSubmit = $("#jqxStopLossExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认提交修改？")) { return; }

                $("#btnUpdate").jqxButton({ disabled: true });

                var apply = {
                    ApplyId: "<%=this.apply.ApplyId%>",
                    ApplyCorp: $("#ddlStopLossApplyCorp").val(),
                    ApplyDept: $("#ddlStopLossApplyDept").val(),
                    ApplyDesc: $("#txbStopLossApplyDesc").val()
                }

                var stopLossApply = {
                    StopLossApplyId: "<%=this.stopLossApply.StopLossApplyId%>",
                    ApplyId: "<%=this.apply.ApplyId%>",
                    PricingId: "<%=this.pricing.PricingId%>",
                    PricingDirection: "<%=this.pricing.PricingDirection%>",
                    SubContractId: "<%=this.pricingApply.SubContractId%>",
                    ContractId: "<%=this.pricingApply.ContractId%>",
                    AssertId: "<%=this.pricing.AssertId%>",
                    StopLossPrice: $("#nbStopLossPrice").val(),
                    CurrencyId: "<%=this.pricing.CurrencyId%>",
                    StopLossWeight: $("#nbStopLossWeight").val(),
                    MUId: "<%=this.pricing.MUId%>"
                }
                if ($("#hidHasDetail").val() == "True") {
                    var rows = $("#jqxStockGrid").jqxGrid("getrows");

                    $.post("Handler/StopLossApplyUpdateHandler.ashx", {
                        apply: JSON.stringify(apply),
                        stopLossApply: JSON.stringify(stopLossApply),
                        detail: JSON.stringify(rows)
                    },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        $("#btnUpdate").jqxButton({ disabled: false });
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StopLossApplyList.aspx";
                        }
                    });
                }
                else {
                    $.post("Handler/StopLossApplyUpdateHandler.ashx", {
                        apply: JSON.stringify(apply),
                        stopLossApply: JSON.stringify(stopLossApply)
                    },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        $("#btnUpdate").jqxButton({ disabled: false });
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StopLossApplyList.aspx";
                        }
                    });
                }
            });

        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxPricingApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            点价申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>点价申请公司：</strong>
                    <div id="ddlPricingApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价申请部门：</strong>
                    <div id="ddlApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价申请备注：</strong>
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
            </ul>
        </div>
    </div>

    <div id="jqxPricingExpander" style="float: left; margin: 0px 5px 5px 5px;">
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
                    <strong>点价均价：</strong>
                    <div id="txbAvgPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价重量：</strong>
                    <div id="txbPricingWeight" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            点价库存信息列表
        </div>
        <div>
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxStopLossExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            <input type="hidden" id="hidHasDetail" runat="server" />
            止损申请修改
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>止损申请公司：</strong>
                    <div id="ddlStopLossApplyCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损申请部门：</strong>
                    <div id="ddlStopLossApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损申请备注：</strong>
                    <input type="text" id="txbStopLossApplyDesc" style="float: left;" />
                </li>
                <li>
                    <strong>止损品种：</strong>
                    <div id="ddlStopLossAsset" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损价格：</strong>
                    <div id="nbStopLossPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损币种：</strong>
                    <div id="ddlStopLossCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损重量：</strong>
                    <div id="nbStopLossWeight" style="float: left;"></div>
                </li>
                <li>
                    <strong>重量单位：</strong>
                    <div id="ddlStopLossMUId" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交修改" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <a target="_self" runat="server" href="StopLossApplyList.aspx" id="btnCancel">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

</body>
</html>
