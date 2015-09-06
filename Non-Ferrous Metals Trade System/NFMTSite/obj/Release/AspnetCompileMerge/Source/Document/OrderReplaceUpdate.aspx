<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReplaceUpdate.aspx.cs" Inherits="NFMTSite.Document.OrderReplaceUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>替临制单指令更新</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>    
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var replaceStocksSource;
        var commercialStocksSource;

        var replaceStocks = new Array();
        var commercialStocks = new Array();       
        
        var orderTypeDataAdapter;       

        $(document).ready(function () {
            $("#jqxNeedExpander").jqxExpander({ width: "98%" });
            $("#jqxCommercialStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxReplaceStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxCommercialOrderExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxOrderExpander").jqxExpander({ width: "98%" });
            $("#jqxDocumnetExpander").jqxExpander({ width: "98%", expanded: false });

            var formatedData = "";
            var totalrecords = 0;

            //替临库存           
            replaceStocksSource =
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
                localdata: <%=this.JsonReplaceStr%>
             };

            replaceStocksDataAdapter = new $.jqx.dataAdapter(replaceStocksSource, {
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
           
            $("#jqxReplaceStocksGrid").jqxGrid(
            {
                width: "98%",
                source: replaceStocksDataAdapter,                
                autoheight: true,                
                virtualmode: true,
                editable: true,
                sorttogglestates: 1,
                enabletooltips: true,
                selectionmode: "singlecell",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "库存状态", datafield: "StatusName", width: 80, editable: false },
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "捆数", datafield: "Bundles", editable: false },
                  { text: "净重", datafield: "LastAmount", editable: false },
                  { text: "重量单位", datafield: "MUName", width: 70, editable: false },
                  { text: "申请重量", datafield: "ApplyWeight", editable: false },
                  { text: "发票号码", datafield: "InvoiceNo", editable: false },
                  { 
                      text: "发票金额", datafield: "InvoiceBala", cellclassname: "GridFillNumber", width: 121, columntype: "numberinput", sortable: false
                      , validation: function (cell, value) {
                          if (value < 0) {
                              return { result: false, message: "发票金额不能小于0" };
                          }
                          return true;
                      }
                      , createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, width: 120, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "操作", datafield: "DetailId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            replaceStocks = replaceStocksDataAdapter.records;

            $("#jqxReplaceStocksGrid").on("cellvaluechanged", function (event) {

                var datafield = event.args.datafield;

                if (datafield == "InvoiceBala") {
                    var rowBoundIndex = args.rowindex;
                    var value = args.newvalue;
                    replaceStocks[rowBoundIndex].InvoiceBala = value;

                    TotalValue(false);
                }

            });

            //指令库存
            formatedData = "";
            totalrecords = 0;
            commercialStocksSource =
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
                localdata:  <%=this.JsonCommercialStr%>,
                datatype: "json"
            };

            var commercialStrocksDataAdapter = new $.jqx.dataAdapter(commercialStocksSource, {
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
            
            $("#jqxCommercialStocksGrid").jqxGrid(
            {
                width: "98%",
                autoheight: true,
                source: commercialStrocksDataAdapter,                
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

            commercialStocks = commercialStrocksDataAdapter.records;

            //申请信息
            //指令日期
            $("#txbComOrderDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120,disabled:true });

            //指令类型
            var orderTypeSource = { datatype: "json", url: "../BasicData/Handler/OrderTypeHandler.ashx", async: false };
            orderTypeDataAdapter = new $.jqx.dataAdapter(orderTypeSource);
            $("#selOrderType").jqxComboBox({ source: orderTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 150, height: 25, autoDropDownHeight: true, disabled: true });            

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
            $("#txbComBankCode").jqxInput({ height: 25, disabled: true });            

            //毛重
            $("#txbComGrossAmount").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //净重
            $("#txbComNetAmount").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //单位
            var unitSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var unitDataAdapter = new $.jqx.dataAdapter(unitSource);
            $("#selUnit").jqxComboBox({ source: unitDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, disabled: true });

            //捆数
            $("#txbComBundles").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 0, Digits: 4, disabled: true });           

            //临票价格
            $("#txbCommercialUnitPrice").jqxNumberInput({ height: 25, width:120, width: 150, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });
            $("#txbCommercialInvoiceBala").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 2, Digits: 8, disabled: true });

            //临票备注
            $("#txbComMemo").jqxInput({ width: "600", height: 25, disabled: true });

            //替临信息

            //发票价格
            $("#txbInvoicePrice").jqxNumberInput({ height: 25, width: 150, spinButtons: true, decimalDigits: 4, Digits: 8 }); 
            //替临日期
            $("#txbOrderDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            //银行编号
            $("#txbBankCode").jqxInput({ height: 25 });
            //替临毛重
            $("#txbGrossAmount").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });
            //替临净重
            $("#txbNetAmount").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });
            //替临捆数
            $("#txbBundles").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 0, Digits: 4, disabled: true });
            //发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 2, Digits: 8, disabled: true });
            //临终差额
            $("#txbGap").jqxNumberInput({ height: 25, width:120, spinButtons: true, decimalDigits: 2, Digits: 8, disabled: true });
            //替临备注
            $("#txbMemo").jqxInput({ width: "600", height: 25 });
            
            $("#btnUpdateAuditReplaceOrder").jqxInput();

            //明细
            $("#txbDocNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3 });
            $("#txbDocSpecial").jqxInput({ height: 25, width: 200 });
            $("#txbQualityNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3 });
            $("#txbQualitySpecial").jqxInput({ height: 25, width: 200 });
            $("#txbWeightNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3 });
            $("#txbWeightSpecial").jqxInput({ height: 25, width: 200 });
            $("#txbGoodsListNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3 });
            $("#txbGoodsListSpecial").jqxInput({ height: 25, width: 200 });
            $("#txbDeliverNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3 });
            $("#txbDeliverSpecial").jqxInput({ height: 25, width: 200 });
            $("#txbTotalInvNumber").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, width: 50, value: 3 });
            $("#txbTotalInvSpecial").jqxInput({ height: 25, width: 200 });

            //数据初始化
            $("#txbCommercialUnitPrice").val(<%=this.curCommercialOrder.UnitPrice%>);
            $("#txbCommercialInvoiceBala").val(<%=this.curCommercialOrder.InvBala%>);

            var tempDate = new Date("<%=this.curCommercialOrder.OrderDate.ToString("yyyy/MM/dd")%>");
            $("#txbComOrderDate").jqxDateTimeInput({ value: tempDate });

            $("#selApplyCorp").val(<%=this.curCommercialOrder.ApplyCorp%>);
            $("#selBuyerCorp").val(<%=this.curCommercialOrder.BuyerCorp%>);
            $("#txbBuyerCorpName").val("<%=this.curCommercialOrder.BuyerCorpName%>");
            $("#txbBuyerAddress").val("<%=this.curCommercialOrder.BuyerAddress%>");
            $("#txbContractNo").val("<%=this.curCommercialOrder.ContractNo%>");
            $("#txbLCNo").val("<%=this.curCommercialOrder.LCNo%>");
            $("#selCashInBank").val(<%=this.curCommercialOrder.RecBankId%>);
            $("#selCurrency").val(<%=this.curCommercialOrder.Currency%>);
            $("#txbLCDay").val(<%=this.curCommercialOrder.LCDay%>);
            $("#selAsset").val(<%=this.curCommercialOrder.AssetId%>);
            $("#selBrand").val(<%=this.curCommercialOrder.BrandId%>);
            $("#txbDeliverPlace").val("<%=this.curCommercialOrder.AreaName%>");
            $("#txbDiscountBase").val("<%=this.curCommercialOrder.DiscountBase%>");
            $("#txbComBankCode").val("<%=this.curCommercialOrder.BankCode%>");
            $("#txbComGrossAmount").val(<%=this.curCommercialOrder.GrossAmount%>);
            $("#txbComNetAmount").val(<%=this.curCommercialOrder.NetAmount%>);
            $("#selUnit").val(<%=this.curCommercialOrder.UnitId%>);
            $("#txbComBundles").val(<%=this.curCommercialOrder.Bundles%>);           
            $("#txbComMemo").val("<%=this.curCommercialOrder.Meno%>");

            //替临初始

            $("#selOrderType").val(<%=this.ReplaceOrderType%>);
            $("#txbInvoicePrice").val(<%=this.curReplaceOrder.UnitPrice%>);
            $("#txbOrderDate").jqxDateTimeInput({ value: tempDate });
            $("#txbBankCode").val("<%=this.curReplaceOrder.BankCode%>");
            $("#txbGrossAmount").val(<%=this.curReplaceOrder.GrossAmount%>);
            $("#txbNetAmount").val(<%=this.curReplaceOrder.NetAmount%>);
            $("#txbBundles").val(<%=this.curReplaceOrder.Bundles%>);
            $("#txbInvoiceBala").val(<%=this.curReplaceOrder.InvBala%>);
            $("#txbGap").val(<%=this.curReplaceOrder.InvGap%>);

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

            //事件
            $("#txbInvoicePrice").on("valueChanged", function (event) {

                var value = event.args.value;
                if (replaceStocks != undefined && replaceStocks != null && replaceStocks.length > 0) {

                    TotalValue(true);
                    replaceStocksSource.localdata = replaceStocks;
                    $("#jqxReplaceStocksGrid").jqxGrid("updatebounddata", "rows");
                }

            });            

        });

        function bntCreateOnClick(row) {            

            var item = commercialStocks[row];
            commercialStocks.splice(row, 1);
            replaceStocks.push(item);

            FlushGrid();
            TotalValue(false);
        }

        function bntRemoveOnClick(row) {

            var item = replaceStocks[row];
            replaceStocks.splice(row, 1);
            commercialStocks.push(item);

            FlushGrid();
            TotalValue(false);
        }

        function TotalValue(isC){           

            var invoicePrice = $("#txbInvoicePrice").val();
            var index = $("#selOrderType").jqxComboBox("getSelectedIndex");
            var orderTypeItem = orderTypeDataAdapter.records[index];
            var invoiceCode = orderTypeItem.DetailCode;

            var totalGross = 0;
            var totalNet = 0;
            var totalBundles = 0;
            var totalBala = 0;

            for (i = 0; i < replaceStocks.length; i++) {
                var item = replaceStocks[i];

                //计算库存发票金额
                if (isC) {
                    item.InvoiceBala = Math.round((item.ApplyWeight * invoicePrice) * 100) / 100;
                }
                //生成发票编号
                item.InvoiceNo = item.RefNo + invoiceCode;

                totalGross += item.CurGrossAmount;
                totalNet += item.ApplyWeight;
                totalBundles += item.Bundles;
                totalBala += item.InvoiceBala;
            }

            $("#txbGrossAmount").val(totalGross);
            $("#txbNetAmount").val(totalNet);
            $("#txbBundles").val(totalBundles);
            $("#txbInvoiceBala").val(Math.round(totalBala * 100) / 100);

            var gapBala = $("#txbCommercialInvoiceBala").val() - $("#txbInvoiceBala").val();
            $("#txbGap").val(gapBala);
        }

        function FlushGrid() {

            replaceStocksSource.localdata = replaceStocks;
            $("#jqxReplaceStocksGrid").jqxGrid("updatebounddata", "rows");
            commercialStocksSource.localdata = commercialStocks;
            $("#jqxCommercialStocksGrid").jqxGrid("updatebounddata", "rows");
        }

        function UpdateReplaceOrder() {            

            if (replaceStocks.length == 0) { alert("未选中任何库存"); return; }
            if (!confirm("确认修改制单指令?")) { return; }

            var Order = {
                OrderId: "<%=this.curReplaceOrder.OrderId%>", 
                OrderDate: $("#txbOrderDate").val(),               
                BankCode: $("#txbBankCode").val(),
                GrossAmount: $("#txbGrossAmount").val(),
                NetAmount: $("#txbNetAmount").val(),               
                Bundles: $("#txbBundles").val(),               
                UnitPrice: $("#txbInvoicePrice").val(),
                InvBala: $("#txbInvoiceBala").val(),
                InvGap: $("#txbGap").val(),
                Meno: $("#txbMemo").val()
            };

            var OrderDetail= {
                InvoiceCopies: $("#txbDocNumber").val(),
                InvoiceSpecific: $("#txbDocSpecial").val(),
                QualityCopies: $("#txbQualityNumber").val(),
                QualitySpecific: $("#txbQualitySpecial").val(),
                WeightCopies: $("#txbWeightNumber").val(),
                WeightSpecific: $("#txbWeightSpecial").val(),
                TexCopies: $("#txbGoodsListNumber").val(),
                TexSpecific: $("#txbGoodsListSpecial").val(),
                DeliverCopies: $("#txbDeliverNumber").val(),
                DeliverSpecific: $("#txbDeliverSpecial").val(),
                TotalInvCopies: $("#txbTotalInvNumber").val(),
                TotalInvSpecific: $("#txbTotalInvSpecial").val()
            };

            var fileIds = new Array();

            var files = $(":file");
            for (i = 0; i < files.length - 1; i++) {
                var item = files[i];
                fileIds.push(item.id);
            }

            $.post("Handler/OrderReplaceUpdateHandler.ashx", { order: JSON.stringify(Order), orderStockInvoice: JSON.stringify(replaceStocks), orderDetail: JSON.stringify(OrderDetail) },
                   function (result) {
                       var obj = JSON.parse(result);
                       if (obj.ResultStatus.toString() == "0") {
                           AjaxFileUpload(fileIds, <%=this.curReplaceOrder.OrderId%>, AttachTypeEnum.OrderAttach);
                       }
                       alert(obj.Message);
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "OrderReplaceList.aspx";
                       }
                   }
               );
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxCommercialOrderExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            临票制单指令信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>临票指令日期：</strong>
                    <div style="float: left;" id="txbComOrderDate"></div>
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
                    <input type="text" id="txbComBankCode" />
                </li>
                <li>
                    <strong>毛重：</strong>
                    <div style="float: left;" id="txbComGrossAmount"></div>
                </li>
                <li>
                    <strong>净重：</strong>
                    <div style="float: left;" id="txbComNetAmount"></div>
                </li>
                <li>
                    <strong>单位：</strong>
                    <div style="float: left;" id="selUnit"></div>
                </li>
                <li>
                    <strong>捆数：</strong>
                    <div style="float: left;" id="txbComBundles"></div>
                </li>
                <li>
                    <strong>临票价格：</strong>
                    <div style="float: left;" id="txbCommercialUnitPrice"></div>
                </li>
                <li>
                    <strong>临票金额：</strong>
                    <div style="float: left;" id="txbCommercialInvoiceBala"></div>
                </li>               
                <li>
                    <strong>申请备注：</strong>
                    <input type="text" id="txbComMemo" />
                </li>
            </ul>
        </div>
    </div>

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

    <div id="jqxReplaceStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            替临制单指令选中库存
        </div>
        <div>
            <div id="jqxReplaceStocksGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxCommercialStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            临票制单指令可选库存
        </div>
        <div>
            <div id="jqxCommercialStocksGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxOrderExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            替临制单指令信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>替临日期：</strong>
                    <div style="float: left;" id="txbOrderDate"></div>
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
                    <strong>捆数：</strong>
                    <div style="float: left;" id="txbBundles"></div>
                </li>
                <li>
                    <strong>替临金额：</strong>
                    <div style="float: left;" id="txbInvoiceBala"></div>
                </li>
                <li>
                    <strong>临终差额[临票-替临]：</strong>
                    <div style="float: left;" id="txbGap"></div>
                </li>
                <li>
                    <strong>替临备注：</strong>
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

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="OrderAttach" />

    <div id="buttons" style="width: 80%; text-align: center; float: left;">
        <input type="button" value="修改替临指令并提交审核" id="btnUpdateAuditReplaceOrder" onclick="UpdateReplaceOrder();" style="width: 180px; height: 25px;" />
    </div>

</body>
    
</html>
