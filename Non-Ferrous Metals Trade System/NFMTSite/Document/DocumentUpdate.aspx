﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentUpdate.aspx.cs" Inherits="NFMTSite.Document.DocumentUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>制单修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>    
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var docStocksSource;
        var orderStocksSource;
        var docStocks = new Array();
        var orderStocks = new Array();        

        
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curOrder.DataBaseName%>" + "&t=" + "<%=this.curOrder.TableName%>" + "&id=" + "<%=this.curOrder.OrderId%>";

        $(document).ready(function () {
            $("#jqxNeedExpander").jqxExpander({ width: "98%" });
            $("#jqxOrderStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxDocStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxOrderExpander").jqxExpander({ width: "98%" });
            $("#jqxDocumnetExpander").jqxExpander({ width: "49%", expanded: false });
            $("#jqxDocExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;

            //制单库存           
            docStocksSource =
            {
                datatype: "json",                
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.DocumentJsonStr%>
                };

            docStocksDataAdapter = new $.jqx.dataAdapter(docStocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"删除\" onclick=\"bntRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
           
            $("#jqxDocStocksGrid").jqxGrid(
            {
                width: "98%",
                source: docStocksDataAdapter,                
                autoheight: true,                
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                selectionmode: "singlecell",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "净重", datafield: "LastAmount" },
                  { text: "重量单位", datafield: "MUName", width: 70 },
                  { text: "申请重量", datafield: "ApplyWeight" },
                  { text: "发票号码", datafield: "InvoiceNo" },
                  { text: "发票金额", datafield: "InvoiceBala", width: 121 },
                  { text: "操作", datafield: "DetailId", cellsrenderer: removeRender, width: 100, enabletooltips: false }
                ]
            });

            docStocks = docStocksDataAdapter.records;

            //指令库存
            formatedData = "";
            totalrecords = 0;
            orderStocksSource =
            {
                datatype: "json",
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.OrderJsonStr%>
            };

            var orderStrocksDataAdapter = new $.jqx.dataAdapter(orderStocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnCreate\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntCreateOnClick(" + row + ");\" />"
                   + "</div>";
            }
            
            $("#jqxOrderStocksGrid").jqxGrid(
            {
                width: "98%",
                autoheight: true,
                source: orderStrocksDataAdapter,                
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                selectionmode: "singlecell",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "净重", datafield: "LastAmount" },
                  { text: "重量单位", datafield: "MUName", width: 70 },
                  { text: "申请重量", datafield: "ApplyWeight" },
                  { text: "发票号码", datafield: "InvoiceNo" },
                  { text: "发票金额", datafield: "InvoiceBala", width: 121 },
                  { text: "操作", datafield: "DetailId", cellsrenderer: addRender, width: 100, enabletooltips: false }
                ]
            });

            orderStocks = orderStrocksDataAdapter.records;

            //申请信息
            //指令日期
            $("#txbOrderDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //指令类型
            var orderTypeSource = { datatype: "json", url: "../BasicData/Handler/OrderTypeHandler.ashx", async: false };
            var orderTypeDataAdapter = new $.jqx.dataAdapter(orderTypeSource);
            $("#selOrderType").jqxComboBox({ source: orderTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").val(<%=this.curUser.DeptId%>);

            //客户公司
            var buyCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var buyCorpSource = { datatype: "json", url: buyCorpUrl, async: false };
            var buyCorpDataAdapter = new $.jqx.dataAdapter(buyCorpSource);
            $("#selBuyerCorp").jqxComboBox({
                source: buyCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });

            $("#txbBuyerCorpName").jqxInput({ height: 25, width: 180, disabled: true });

            //客户地址
            $("#txbBuyerAddress").jqxInput({ height: 25, width: 250, disabled: true });

            //合同编号
            $("#txbContractNo").jqxInput({ height: 25, disabled: true });

            //LC编号
            $("#txbLCNo").jqxInput({ height: 25, disabled: true });

            //收款银行
            var bankUrl = "../BasicData/Handler/BanDDLHandler.ashx";
            var bankSource = { datatype: "json", url: bankUrl, async: false };
            var bankDataAdapter = new $.jqx.dataAdapter(bankSource);
            $("#selCashInBank").jqxComboBox({ source: bankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, searchMode: "containsignorecase", disabled: true });

            //币种
            var currencyUrl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var currencySource = { datatype: "json", url: currencyUrl, async: false };
            var currencyDataAdapter = new $.jqx.dataAdapter(currencySource);
            $("#selCurrency").jqxComboBox({ source: currencyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", height: 25, width: 80, searchMode: "containsignorecase", disabled: true });

            //LC天数
            $("#txbLCDay").jqxNumberInput({ width: 80, height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, disabled: true });

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetAuthHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#selAsset").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });

            //品牌
            var brandUrl = "../BasicData/Handler/BrandDDLHandler.ashx";
            var brandSource = { datatype: "json", url: brandUrl, async: false };
            var brandDataAdapter = new $.jqx.dataAdapter(brandSource);
            $("#selBrand").jqxComboBox({ source: brandDataAdapter, displayMember: "BrandName", valueMember: "BrandId", height: 25, disabled: true });

            //产地            
            $("#txbDeliverPlace").jqxInput({ height: 25, disabled: true });

            //价格条款
            $("#txbDiscountBase").jqxInput({ height: 25, disabled: true });

            //银行编号
            $("#txbBankCode").jqxInput({ height: 25, disabled: true });

            //毛重
            $("#txbGrossAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //净重
            $("#txbNetAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //单位
            var unitSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var unitDataAdapter = new $.jqx.dataAdapter(unitSource);
            $("#selUnit").jqxComboBox({ source: unitDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, disabled: true });

            //捆数
            $("#txbBundles").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 4, disabled: true });

            //发票价格
            $("#txbInvoicePrice").jqxNumberInput({ height: 25, width: 150, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 2, Digits: 8, disabled: true });
            $("#txbMemo").jqxInput({ width: "600", height: 25, disabled: true });
            
            $("#btnUpdateDocument").jqxInput();

            //明细
            $("#txbDocNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3, disabled: true });
            $("#txbDocSpecial").jqxInput({ height: 25, width: 200, disabled: true });
            $("#txbQualityNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3, disabled: true });
            $("#txbQualitySpecial").jqxInput({ height: 25, width: 200, disabled: true });
            $("#txbWeightNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3, disabled: true });
            $("#txbWeightSpecial").jqxInput({ height: 25, width: 200, disabled: true });
            $("#txbGoodsListNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3, disabled: true });
            $("#txbGoodsListSpecial").jqxInput({ height: 25, width: 200, disabled: true });
            $("#txbDeliverNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3, disabled: true });
            $("#txbDeliverSpecial").jqxInput({ height: 25, width: 200, disabled: true });
            $("#txbTotalInvNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3, disabled: true });
            $("#txbTotalInvSpecial").jqxInput({ height: 25, width: 200, disabled: true });

            //数据初始化
            $("#selOrderType").val(<%=this.curOrder.OrderType%>);
            $("#txbInvoicePrice").val(<%=this.curOrder.UnitPrice%>);

            var tempDate = new Date("<%=this.curOrder.OrderDate.ToString("yyyy/MM/dd")%>");
            $("#txbOrderDate").jqxDateTimeInput({ value: tempDate });

            $("#selApplyCorp").val(<%=this.curOrder.ApplyCorp%>);
            $("#selBuyerCorp").val(<%=this.curOrder.BuyerCorp%>);
            $("#txbBuyerCorpName").val("<%=this.curOrder.BuyerCorpName%>");
            $("#txbBuyerAddress").val("<%=this.curOrder.BuyerAddress%>");
            $("#txbContractNo").val("<%=this.curOrder.ContractNo%>");
            $("#txbLCNo").val("<%=this.curOrder.LCNo%>");
            $("#selCashInBank").val(<%=this.curOrder.RecBankId%>);
            $("#selCurrency").val(<%=this.curOrder.Currency%>);
            $("#txbLCDay").val(<%=this.curOrder.LCDay%>);
            $("#selAsset").val(<%=this.curOrder.AssetId%>);
            $("#selBrand").val(<%=this.curOrder.BrandId%>);
            $("#txbDeliverPlace").val("<%=this.curOrder.AreaName%>");
            $("#txbDiscountBase").val("<%=this.curOrder.DiscountBase%>");
            $("#txbBankCode").val("<%=this.curOrder.BankCode%>");
            $("#txbGrossAmount").val(<%=this.curOrder.GrossAmount%>);
            $("#txbNetAmount").val(<%=this.curOrder.NetAmount%>);
            $("#selUnit").val(<%=this.curOrder.UnitId%>);
            $("#txbBundles").val(<%=this.curOrder.Bundles%>);
            $("#txbInvoiceBala").val(<%=this.curOrder.InvBala%>);
            $("#txbMemo").val("<%=this.curOrder.Meno%>");

            $("#txbDocNumber").val(<%=this.curOrderDetail.InvoiceCopies%>);
            $("#txbDocSpecial").val("<%=this.curOrderDetail.InvoiceSpecific%>");
            $("#txbQualityNumber").val(<%=this.curOrderDetail.QualityCopies%>);
            $("#txbQualitySpecial").val("<%=this.curOrderDetail.QualitySpecific%>");
            $("#txbWeightNumber").val(<%=this.curOrderDetail.WeightCopies%>);
            $("#txbWeightSpecial").val("<%=this.curOrderDetail.WeightSpecific%>");
            $("#txbGoodsListNumber").val(<%=this.curOrderDetail.TexCopies%>);
            $("#txbGoodsListSpecial").val("<%=this.curOrderDetail.TexSpecific%>");
            $("#txbDeliverNumber").val(<%=this.curOrderDetail.DeliverCopies%>);
            $("#txbDeliverSpecial").val("<%=this.curOrderDetail.DeliverSpecific%>");
            $("#txbTotalInvNumber").val(<%=this.curOrderDetail.TotalInvCopies%>);
            $("#txbTotalInvSpecial").val("<%=this.curOrderDetail.TotalInvSpecific%>");

            //制单信息
            $("#txbDocDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#txbDocMeno").jqxInput({width: 600, height: 25 });

            tempDate = new Date("<%=this.curDocument.DocumentDate.ToString("yyyy/MM/dd")%>");
            $("#txbDocDate").jqxDateTimeInput({ value: tempDate });
            $("#txbDocMeno").val("<%=this.curDocument.Meno%>");

        });

        function bntCreateOnClick(row) {            

            var item = orderStocks[row];
            orderStocks.splice(row, 1);
            docStocks.push(item);
            FlushGrid();

            TotalValue();
        }

        function bntRemoveOnClick(row) {

            var item = docStocks[row];
            docStocks.splice(row, 1);
            orderStocks.push(item);
            FlushGrid();

            TotalValue();
        }

        function TotalValue(){           

            //var totalGross = 0;
            //var totalNet = 0;
            //var totalBundles = 0;
            //var totalBala = 0;

            //for (i = 0; i < docStocks.length; i++) {

            //    var item = docStocks[i];
            //    totalGross += item.CurGrossAmount;
            //    totalNet += item.ApplyWeight;
            //    totalBundles += item.Bundles;
            //    totalBala += item.InvoiceBala;
            //}

            //$("#txbGrossAmount").val(totalGross);
            //$("#txbNetAmount").val(totalNet);
            //$("#txbBundles").val(totalBundles);
            //$("#txbInvoiceBala").val(Math.round(totalBala * 100) / 100);
        }

        function FlushGrid() {

            docStocksSource.localdata = docStocks;
            $("#jqxDocStocksGrid").jqxGrid("updatebounddata", "rows");
            orderStocksSource.localdata = orderStocks;
            $("#jqxOrderStocksGrid").jqxGrid("updatebounddata", "rows");
        }

        function UpdateDocument() {

            if (docStocks.length == 0) { alert("未选中任何库存"); return; }
            if (!confirm("确认修改制单?")) { return; }

            var Document = {
                DocumentId:"<%=this.curDocument.DocumentId%>",
                DocumentDate: $("#txbDocDate").val(),
                Meno: $("#txbDocMeno").val()
            };

            $.post("Handler/DocumentUpdateHandler.ashx", { document: JSON.stringify(Document), docStocks: JSON.stringify(docStocks) },
                   function (result) {
                       var obj = JSON.parse(result);
                       alert(obj.Message);
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "DocumentList.aspx";
                       }
                   }
               );
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxNeedExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            前置填写信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>指令类型：</strong>
                    <div style="float: left;" id="selOrderType"></div>
                </li>
                <li>
                    <strong>发票价格：</strong>
                    <div style="float: left;" id="txbInvoicePrice"></div>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDocStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            制单选中库存
        </div>
        <div>
            <div id="jqxDocStocksGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxOrderStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            指令可选库存
        </div>
        <div>
            <div id="jqxOrderStocksGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxOrderExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            指令信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>指令日期：</strong>
                    <div style="float: left;" id="txbOrderDate"></div>
                </li>
                <li>
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="selApplyCorp"></div>
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div style="float: left;" id="ddlApplyDeptId"></div>
                </li>
                <li>
                    <strong>客户公司：</strong>
                    <div style="float: left;" id="selBuyerCorp"></div>
                    <input type="text" id="txbBuyerCorpName" />
                </li>
                <li>
                    <strong>客户地址：</strong>
                    <input type="text" id="txbBuyerAddress" />
                </li>
                <li>
                    <strong>合约编号：</strong>
                    <input type="text" id="txbContractNo" />
                </li>
                <li>
                    <strong>LC编号：</strong>
                    <input type="text" id="txbLCNo" />
                </li>
                <li>
                    <strong>收款银行：</strong>
                    <div id="selCashInBank" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>
                <li>
                    <strong>LC天数：</strong>
                    <div id="txbLCDay" style="float: left;"></div>
                </li>

                <li>
                    <strong>品种：</strong>
                    <div style="float: left;" id="selAsset"></div>
                </li>
                <li>
                    <strong>品牌：</strong>
                    <div style="float: left;" id="selBrand"></div>
                </li>
                <li>
                    <strong>产地：</strong>
                    <input type="text" id="txbDeliverPlace" />
                </li>
                <li>
                    <strong>价格条款：</strong>
                    <input type="text" id="txbDiscountBase" />
                </li>
                <li>
                    <strong>银行编号：</strong>
                    <input type="text" id="txbBankCode" />
                </li>
                <li>
                    <strong>毛重：</strong>
                    <div style="float: left;" id="txbGrossAmount"></div>
                </li>
                <li>
                    <strong>净重：</strong>
                    <div style="float: left;" id="txbNetAmount"></div>
                </li>
                <li>
                    <strong>单位：</strong>
                    <div style="float: left;" id="selUnit"></div>
                </li>
                <li>
                    <strong>捆数：</strong>
                    <div style="float: left;" id="txbBundles"></div>
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div style="float: left;" id="txbInvoiceBala"></div>
                </li>
                <li>
                    <strong>申请备注：</strong>
                    <input type="text" id="txbMemo" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDocumnetExpander" style="float: left; margin: 0px 0px 5px 5px;">
        <div>
            申请单据数量
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="float: none">
                    <strong>发票份数：</strong>
                    <div style="float: left;" id="txbDocNumber"></div>
                    <strong>特别要求：</strong>
                    <input type="text" id="txbDocSpecial" />
                </li>
                <li style="float: none">
                    <strong>质量证份数：</strong>
                    <div style="float: left;" id="txbQualityNumber"></div>
                    <strong>特别要求：</strong>
                    <input type="text" id="txbQualitySpecial" />
                </li>
                <li style="float: none">
                    <strong>重量证份数：</strong>
                    <div style="float: left;" id="txbWeightNumber"></div>
                    <strong>特别要求：</strong>
                    <input type="text" id="txbWeightSpecial" />
                </li>
                <li style="float: none">
                    <strong>装箱单份数：</strong>
                    <div style="float: left;" id="txbGoodsListNumber"></div>
                    <strong>特别要求：</strong>
                    <input type="text" id="txbGoodsListSpecial" />
                </li>
                <li style="float: none">
                    <strong>产地证明份数：</strong>
                    <div style="float: left;" id="txbDeliverNumber"></div>
                    <strong>特别要求：</strong>
                    <input type="text" id="txbDeliverSpecial" />
                </li>
                <li style="float: none">
                    <strong>汇票份数：</strong>
                    <div style="float: left;" id="txbTotalInvNumber"></div>
                    <strong>特别要求：</strong>
                    <input type="text" id="txbTotalInvSpecial" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDocExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            指令信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>制单日期：</strong>
                    <div style="float: left;" id="txbDocDate"></div>
                </li>                            
                <li>
                    <strong>制单备注：</strong>
                    <input type="text" id="txbDocMeno" />
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="OrderAttach" />

    <div id="buttons" style="width: 80%; text-align: center; float: left;">
        <input type="button" value="修改制单" id="btnUpdateDocument" onclick="UpdateDocument();" style="width: 120px; height: 25px;" />
    </div>

</body>

<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
