<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopLossUpdate.aspx.cs" Inherits="NFMTSite.DoPrice.StopLossUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>止损修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#jqxStopLossExpander").jqxExpander({ width: "98%" });
            $("#jqxCanStopLossStockExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectStockExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            /////////////////////////止损申请信息/////////////////////////

            //止损申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlStopLossApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });
            $("#ddlStopLossApplyCorp").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            //止损申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlStopLossApplyDept").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossApplyDept").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //申请备注
            $("#txbStopLossApplyDesc").jqxInput({ height: 25, disabled: true });
            $("#txbStopLossApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //止损品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlStopLossAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlStopLossAsset").jqxDropDownList("val", "<%=this.stopLossApply.AssertId%>");

            //止损价格
            $("#nbStopLossPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });
            $("#nbStopLossPrice").jqxNumberInput("val", "<%=this.stopLossApply.StopLossPrice%>");

            //价格币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlStopLossCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossCurrencyId").jqxDropDownList("val", "<%=this.stopLossApply.CurrencyId%>");

            //止损重量
            $("#nbStopLossWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#nbStopLossWeight").jqxNumberInput("val", "<%=this.stopLossApply.StopLossWeight%>");

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlStopLossMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossMUId").jqxDropDownList("val", "<%=this.stopLossApply.MUId%>");


            /////////////////////////初始化全局变量/////////////////////////

            var dIdsUp = new Array();
            var dIdsDown = new Array();

            var selectItemsUp = $("#hidDetailsIdUp").val();

            var splitItem = selectItemsUp.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    dIdsUp.push(splitItem[i]);
                }
            }

            var selectItemsDown = $("#hidDetailsIdDown").val();

            splitItem = selectItemsDown.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    dIdsDown.push(splitItem[i]);
                }
            }

            /////////////////////////可止损库存信息/////////////////////////

            var detaisIdUp = "";
            for (i = 0; i < dIdsUp.length; i++) {
                if (i != 0) { detaisIdUp += ","; }
                detaisIdUp += dIdsUp[i];
            }

            var formatedData = "";
            var totalrecords = 0;
            var Source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "GrossAmoutName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "StockStatusName", type: "string" },
                   { name: "GrossAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "StopLossWeight", type: "number" },
                   { name: "StopLossWeightName", type: "string" }
                ],
                sort: function () {
                    $("#jqxCanStopLossStockGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "detail.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "detail.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/StopLossCreateStockListHandler.ashx?applyId=" + "<%=this.stopLossApply.StopLossApplyId%>" + "&detailsId=" + detaisIdUp
            };
            var DataAdapter = new $.jqx.dataAdapter(Source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var moveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnDelete\" id=\"" + value + "," + row + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"止损\" />";
            }

            $("#jqxCanStopLossStockGrid").jqxGrid(
            {
                width: "98%",
                source: DataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "库存重量", datafield: "GrossAmoutName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "止损重量", datafield: "StopLossWeightName", width: 120 },
                  { text: "操作", datafield: "DetailId", cellsrenderer: moveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var moves = $("input[name=\"btnDelete\"]");
                for (i = 0; i < moves.length; i++) {
                    var btnMove = moves[i];
                    var val = btnMove.id.split(",")[0];
                    var row = btnMove.id.split(",")[1];

                    $(btnMove).click({ value: val, row: row }, function (event) {
                        var rowId = event.data.value;
                        var rowNumber = event.data.row;
                        //if (!confirm("确定止损？")) { return; }

                        var index = dIdsUp.indexOf(rowId);
                        dIdsUp.splice(index, 1);

                        var detaisIdUp = "";
                        for (i = 0; i < dIdsUp.length; i++) {
                            if (i != 0) { detaisIdUp += ","; }
                            detaisIdUp += dIdsUp[i];
                        }

                        dIdsDown.push(rowId);

                        var detaisIdDown = "";
                        for (i = 0; i < dIdsDown.length; i++) {
                            if (i != 0) { detaisIdDown += ","; }
                            detaisIdDown += dIdsDown[i];
                        }

                        var item = $("#jqxCanStopLossStockGrid").jqxGrid("getrowdata", rowNumber);
                        var value = parseFloat($("#txbStopLossWeight").val()) + parseFloat(item.StopLossWeight);
                        $("#txbStopLossWeight").jqxNumberInput("val", value);

                        //刷新列表
                        Source.url = "Handler/StopLossCreateStockListHandler.ashx?applyId=" + "<%=this.stopLossApply.StopLossApplyId%>" + "&detailsId=" + detaisIdUp
                        $("#jqxCanStopLossStockGrid").jqxGrid("updatebounddata", "rows");
                        selectSource.url = "Handler/StopLossCreateStockListHandler.ashx?applyId=" + "<%=this.stopLossApply.StopLossApplyId%>" + "&detailsId=" + detaisIdDown;
                        $("#jqxSelectStockGrid").jqxGrid("updatebounddata", "rows");

                    });
                }
            });

            /////////////////////////已止损库存信息/////////////////////////

            var detaisIdDown = "";
            for (i = 0; i < dIdsDown.length; i++) {
                if (i != 0) { detaisIdDown += ","; }
                detaisIdDown += dIdsDown[i];
            }

            var formatedData = "";
            var totalrecords = 0;
            var selectSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "GrossAmoutName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "StockStatusName", type: "string" },
                   { name: "GrossAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "StopLossWeight", type: "number" },
                   { name: "StopLossWeightName", type: "string" }
                ],
                sort: function () {
                    $("#jqxSelectStockGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "detail.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "detail.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/StopLossCreateStockListHandler.ashx?applyId=" + "<%=this.stopLossApply.StopLossApplyId%>" + "&detailsId=" + detaisIdDown
            };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnAdd\" id=\"" + value + "," + row + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" />";
            }

            $("#jqxSelectStockGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "库存重量", datafield: "GrossAmoutName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "止损重量", datafield: "StopLossWeightName", width: 120 },
                  { text: "操作", datafield: "DetailId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var moves = $("input[name=\"btnAdd\"]");
                for (i = 0; i < moves.length; i++) {
                    var btnMove = moves[i];
                    var val = btnMove.id.split(",")[0];
                    var row = btnMove.id.split(",")[1];

                    $(btnMove).click({ value: val, row: row }, function (event) {
                        var rowId = event.data.value;
                        var rowNumber = event.data.row;
                        //if (!confirm("确定取消？")) { return; }

                        var index = dIdsDown.indexOf(rowId);
                        dIdsDown.splice(index, 1);

                        var detaisIdDown = "";
                        for (i = 0; i < dIdsDown.length; i++) {
                            if (i != 0) { detaisIdDown += ","; }
                            detaisIdDown += dIdsDown[i];
                        }

                        dIdsUp.push(rowId);

                        var detaisIdUp = "";
                        for (i = 0; i < dIdsUp.length; i++) {
                            if (i != 0) { detaisIdUp += ","; }
                            detaisIdUp += dIdsUp[i];
                        }

                        var item = $("#jqxSelectStockGrid").jqxGrid("getrowdata", rowNumber);
                        var value = parseFloat($("#txbStopLossWeight").val()) - parseFloat(item.StopLossWeight);
                        $("#txbStopLossWeight").jqxNumberInput("val", value);

                        //刷新列表
                        Source.url = "Handler/StopLossCreateStockListHandler.ashx?applyId=" + "<%=this.stopLossApply.StopLossApplyId%>" + "&detailsId=" + detaisIdUp;
                        $("#jqxCanStopLossStockGrid").jqxGrid("updatebounddata", "rows");
                        selectSource.url = "Handler/StopLossCreateStockListHandler.ashx?applyId=" + "<%=this.stopLossApply.StopLossApplyId%>" + "&detailsId=" + detaisIdDown;
                        $("#jqxSelectStockGrid").jqxGrid("updatebounddata", "rows");

                    });
                }
            });

            /////////////////////////止损修改/////////////////////////

            //止损重量
            $("#txbStopLossWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 160, spinButtons: true, symbol: "<%=this.measureUnit.MUName%>", symbolPosition: "right", disabled: true });
            $("#txbStopLossWeight").jqxNumberInput("val", "<%=this.stopLoss.StopLossWeight%>");

            //止损单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlMUId").jqxDropDownList("val", "<%=this.measureUnit.MUId%>");

            //止损期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxDropDownList({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true, selectedIndex: 0 });
            $("#ddlExchangeId").jqxDropDownList("val", "<%=this.stopLoss.ExchangeId%>");
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

            //止损期货合约
            var FuturesCodeSource = { datatype: "json", url: "../BasicData/Handler/FuturesCodeDDLHandler.ashx?exId=" + $("#ddlExchangeId").val(), async: false };
            var FuturesCodeDataAdapter = new $.jqx.dataAdapter(FuturesCodeSource);
            $("#ddlFuturesCodeId").jqxComboBox({ source: FuturesCodeDataAdapter, displayMember: "TradeCode", valueMember: "FuturesCodeId", width: 100, height: 25 });
            $("#ddlFuturesCodeId").jqxComboBox("val", "<%=this.stopLoss.FuturesCodeId%>");

            //止损均价
            $("#txbAvgPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 160, spinButtons: true, symbol: "<%=this.currency.CurrencyName%>", symbolPosition: "right" });
            $("#txbAvgPrice").jqxNumberInput("val", "<%=this.stopLoss.AvgPrice%>");

            //止损币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.currency.CurrencyId%>");

            //止损品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssertId").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlAssertId").jqxDropDownList("val", "<%=this.stopLossApply.AssertId%>");

            //验证
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#txbStopLossWeight", message: "止损重量必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbStopLossWeight").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#ddlExchangeId", message: "止损期货市场必选", action: "change", rule: function (input, commit) {
                                return $("#ddlExchangeId").jqxDropDownList("val") > 0;
                            }
                        },
                        {
                            input: "#ddlFuturesCodeId", message: "止损期货合约必选", action: "change", rule: function (input, commit) {
                                return $("#ddlFuturesCodeId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbAvgPrice", message: "止损均价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbAvgPrice").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //init buttons
            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });


            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确定提交？")) { return; }

                //if (dIdsDown.length == 0) { alert("必须选择止损库存！"); return; }

                var StopLoss = {
                    StopLossId: "<%=this.stopLoss.StopLossId%>",
                    StopLossApplyId: "<%=this.stopLossApply.StopLossApplyId%>",
                    ApplyId: "<%=this.apply.ApplyId%>",
                    StopLossWeight: $("#txbStopLossWeight").val(),
                    MUId: "<%=this.measureUnit.MUId%>",
                    ExchangeId: $("#ddlExchangeId").val(),
                    FuturesCodeId: $("#ddlFuturesCodeId").val(),
                    AvgPrice: $("#txbAvgPrice").val(),
                    //PricingTime: Date.now,
                    CurrencyId: "<%=this.currency.CurrencyId%>",
                    //StopLosser
                    AssertId: "<%=this.stopLossApply.AssertId%>"
                    //PricingDirection
                };

                var rows = $("#jqxSelectStockGrid").jqxGrid("getrows");

                $.post("Handler/StopLossUpdateHandler.ashx", {
                    stopLoss: JSON.stringify(StopLoss),
                    detail: JSON.stringify(rows)
                },
                    function (result) {
                        alert(result);
                        window.document.location = "StopLossList.aspx";
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStopLossExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            止损申请信息
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

    <div id="jqxCanStopLossStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可止损库存信息<input type="hidden" id="hidDetailsIdUp" runat="server" />
        </div>
        <div>
            <div id="jqxCanStopLossStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxSelectStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已止损库存信息<input type="hidden" id="hidDetailsIdDown" runat="server" />
        </div>
        <div>
            <div id="jqxSelectStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            止损修改
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>止损重量：</strong>
                    <div id="txbStopLossWeight" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损重量单位：</strong>
                    <div id="ddlMUId" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货市场：</strong>
                    <div id="ddlExchangeId" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货合约：</strong>
                    <div id="ddlFuturesCodeId" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损均价：</strong>
                    <div id="txbAvgPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损品种：</strong>
                    <div id="ddlAssertId" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <a target="_self" runat="server" href="StopLossList.aspx" id="btnCancel">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
