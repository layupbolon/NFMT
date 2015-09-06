<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCreate.aspx.cs" Inherits="NFMTSite.Document.OrderCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>制单指令新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>    
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var sellSource;
        var selectedSource;
        var sellDataAdapter;
        var selectedDataAdapter;
        var orderTypeDataAdapter;

        var selectedStocks = new Array();

        $(document).ready(function () {
            $("#jqxNeedExpander").jqxExpander({ width: "98%" });
            $("#jqxStockListExpander").jqxExpander({ width: "98%" });
            $("#jqxApplyAllotStockListExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDocumnetExpander").jqxExpander({ width: "98%", expanded: false });

            var formatedData = "";
            var totalrecords = 0;

            //可售库存
            var sellUrl = "Handler/OrderStockListHandler.ashx?cid=" + "<%= this.curSub.ContractId%>";
            sellSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxStockListGrid").jqxGrid("updatebounddata", "sort");
                },
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
                url: sellUrl,
                id: "StockId"
            };
            sellDataAdapter = new $.jqx.dataAdapter(sellSource, {
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
            $("#jqxStockListGrid").jqxGrid(
            {
                width: "98%",
                source: sellDataAdapter,
                pageable: true,
                autoheight: true,
                editable: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesize: 5,
                enabletooltips: true,
                selectionmode: "singlecell",
                sortable: true,
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
                  { text: "净量", datafield: "LastAmount", editable: false },
                  { text: "重量单位", datafield: "MUName", width: 70, editable: false },
                  {
                      text: "申请重量", datafield: "ApplyWeight", width: 121, columntype: "numberinput", cellclassname: "GridFillNumber", sortable: false, validation: function (cell, value) {
                          var item = $("#jqxStockListGrid").jqxGrid("getrowdata", cell.row);
                          if (value < 0 || value > item.LastAmount) {
                              return { result: false, message: "申请重量不能小于0且大于剩余重量" + item.LastAmount };
                          }
                          return true;
                      }, createeditor: function (row, cellvalue, editor) {
                          var r = $("#jqxStockListGrid").jqxGrid("getrowdata", row);
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 120, Digits: 8, spinButtons: true, symbolPosition: "right", symbol: r.MUName });
                      }
                  },
                  { text: "操作", datafield: "StockId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            //配货情况
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"删除\" onclick=\"bntRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }

            formatedData = "";
            totalrecords = 0;
            selectedSource =
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
                localdata: selectedStocks,
                datatype: "json"
            };

            selectedDataAdapter = new $.jqx.dataAdapter(selectedSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxSelectedGrid").jqxGrid(
            {
                width: "98%",
                autoheight: true,
                source: selectedDataAdapter,
                editable: true,
                virtualmode: true,
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
                  { text: "操作", datafield: "StockId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            $("#jqxSelectedGrid").on("cellvaluechanged", function (event) {

                var datafield = event.args.datafield;

                if (datafield == "InvoiceBala") {
                    var rowBoundIndex = args.rowindex;
                    var value = args.newvalue;
                    selectedStocks[rowBoundIndex].InvoiceBala = value;

                    TotalValue(false);
                }

            });

            //查询
            $("#txbRefNo").jqxInput({ height: 25 });
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId="+"<%=this.curSub.SubId%>";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selOwnCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            $("#btnSearchStcok").jqxButton({ width: 80 });

            //申请信息

            //指令日期
            $("#txbOrderDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //指令类型
            var orderTypeSource = { datatype: "json", url: "../BasicData/Handler/OrderTypeHandler.ashx?id=1", async: false };
            orderTypeDataAdapter = new $.jqx.dataAdapter(orderTypeSource);
            $("#selOrderType").jqxComboBox({ source: orderTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 120, height: 25, autoDropDownHeight: true });

            $("#selOrderType").on("change", function (event) {

                TotalValue(true);
                selectedSource.localdata = selectedStocks;
                $("#jqxSelectedGrid").jqxGrid("updatebounddata", "rows");
            });


            //申请公司
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").val(<%=this.curDeptId%>);

            //客户公司
            var buyCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curSub.SubId%>";
            var buyCorpSource = { datatype: "json", url: buyCorpUrl, async: false };
            var buyCorpDataAdapter = new $.jqx.dataAdapter(buyCorpSource);
            $("#selBuyerCorp").jqxComboBox({
                source: buyCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            $("#txbBuyerCorpName").jqxInput({ height: 25, width: 180 });

            $("#selBuyerCorp").on("change", function (event) {
                var index = event.args.index;
                var item = buyCorpDataAdapter.records[index];
                $("#txbBuyerAddress").val(item.CorpAddress);
                $("#txbBuyerCorpName").val(item.CorpName);
            });

            //客户地址
            $("#txbBuyerAddress").jqxInput({ height: 25, width: 250 });

            //合同编号
            $("#txbContractNo").jqxInput({ height: 25 });
            $("#txbContractNo").val("<%=this.curContract.ContractNo%>");

            //LC编号
            $("#txbLCNo").jqxInput({ height: 25 });

            //收款银行
            var bankUrl = "../BasicData/Handler/BanDDLHandler.ashx";
            var bankSource = { datatype: "json", url: bankUrl, async: false };
            var bankDataAdapter = new $.jqx.dataAdapter(bankSource);
            $("#selCashInBank").jqxComboBox({ source: bankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, searchMode: "containsignorecase" });

            //币种
            var currencyUrl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var currencySource = { datatype: "json", url: currencyUrl, async: false };
            var currencyDataAdapter = new $.jqx.dataAdapter(currencySource);
            $("#selCurrency").jqxComboBox({ source: currencyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", height: 25, width:80, searchMode: "containsignorecase" });

            //LC天数
            $("#txbLCDay").jqxNumberInput({ width: 80, height: 25, spinButtons: true, decimalDigits: 0, Digits: 3 });

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetAuthHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#selAsset").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#selAsset").val(<%=this.curContract.AssetId%>);

            //品牌
            var brandUrl = "../BasicData/Handler/BrandDDLHandler.ashx";
            var brandSource = { datatype: "json", url: brandUrl, async: false };
            var brandDataAdapter = new $.jqx.dataAdapter(brandSource);
            $("#selBrand").jqxComboBox({ source: brandDataAdapter, displayMember: "BrandName", valueMember: "BrandId", height: 25 });

            //产地            
            $("#txbDeliverPlace").jqxInput({ height: 25 });

            //价格条款
            $("#txbDiscountBase").jqxInput({ height: 25 });

            //银行编号
            $("#txbBankCode").jqxInput({ height: 25 });

            //毛重
            $("#txbGrossAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //净重
            $("#txbNetAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 8, disabled: true });

            //单位
            var unitSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var unitDataAdapter = new $.jqx.dataAdapter(unitSource);
            $("#selUnit").jqxComboBox({ source: unitDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, disabled: true });
            $("#selUnit").val(<%=this.curContract.UnitId%>);

            //捆数
            $("#txbBundles").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 0, Digits: 4, disabled: true });

            //发票价格
            $("#txbInvoicePrice").jqxNumberInput({ height: 25, width: 150, spinButtons: true, decimalDigits: 4, Digits: 8 });
            $("#txbInvoicePrice").on("valueChanged", function (event) {

                var value = event.args.value;
                if (selectedStocks != undefined && selectedStocks != null && selectedStocks.length > 0) {

                    TotalValue(true);
                    selectedSource.localdata = selectedStocks;
                    $("#jqxSelectedGrid").jqxGrid("updatebounddata", "rows");
                }

            });

            //发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 2, Digits: 8, disabled: true });

            $("#txbMemo").jqxInput({ width: "600", height: 25 });
            $("#btnCreateOrder").jqxInput();
            $("#btnCreateAuditOrder").jqxInput();

            $("#jqxNeedExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#selOrderType", message: "请优先选择制单指令类型", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selOrderType").val() > 0;
                            }
                        },
                        {
                            input: "#txbInvoicePrice", message: "请优先填写发票价格", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbInvoicePrice").val() > 0;
                            }
                        }
                    ]
            });

            //提交校验
            $("#jqxValidator").jqxValidator({
                rules:
                    [
                        {
                            input: "#selOrderType", message: "请优先选择制单指令类型", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selOrderType").val() > 0;
                            }
                        },
                        {
                            input: "#txbInvoicePrice", message: "请优先填写发票价格", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbInvoicePrice").val() > 0;
                            }
                        },
                        {
                            input: "#selApplyCorp", message: "请选择申请公司", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selApplyCorp").val() > 0;
                            }
                        },
                        {
                            input: "#txbBuyerCorpName", message: "请填写客户公司", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbBuyerCorpName").val().length> 0;
                            }
                        },
                        {
                            input: "#txbBuyerAddress", message: "请填写客户公司地址", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbBuyerAddress").val().length > 0;
                            }
                        },
                        {
                            input: "#txbLCNo", message: "请填写LC编号", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbLCNo").val().length > 0;
                            }
                        },
                        {
                            input: "#selCashInBank", message: "请选择收款银行", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selCashInBank").val() > 0;
                            }
                        },
                        {
                            input: "#selCurrency", message: "请选择信用证币种", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selCurrency").val() > 0;
                            }
                        },
                        {
                            input: "#txbLCDay", message: "请填写信用证天数", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbLCDay").val() > 0;
                            }
                        },
                        {
                            input: "#selBrand", message: "请选择品牌", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selBrand").val() > 0;
                            }
                        },
                        {
                            input: "#txbDeliverPlace", message: "请填写产地", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbDeliverPlace").val().length > 0;
                            }
                        },
                        {
                            input: "#txbDiscountBase", message: "请填写价格条款", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbDiscountBase").val().length > 0;
                            }
                        },
                        {
                            input: "#txbBankCode", message: "请填写银行编号", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbBankCode").val().length > 0;
                            }
                        }
                    ]
            });

            //
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

        });

        var invoiceSumBala = 0;

        function bntCreateOnClick(row) {

            var isCanSubmit = $("#jqxNeedExpander").jqxValidator("validate");
            if (!isCanSubmit) { return; }

            var item = sellDataAdapter.records[row];//$("#jqxStockListGrid").jqxGrid("getrowdata", row);
            selectedStocks.push(item);

            if (item.ApplyWeight == undefined || item.ApplyWeight == 0) { alert("申请重量必须大于0"); return; }

            if (selectedStocks.length == 1) {
                $("#txbGrossAmount").val(0);
                $("#txbNetAmount").val(0);
                $("#txbBundles").val(0);
                $("#txbInvoiceBala").val(0);
            }

            TotalValue(true);
            FlushGrid();
        }

        function bntRemoveOnClick(row) {
            var item = $("#jqxSelectedGrid").jqxGrid("getrowdata", row);
            selectedStocks.splice(row, 1);

            TotalValue(true);
            FlushGrid();
        }

        function TotalValue(isC) {

            var invoicePrice = $("#txbInvoicePrice").val();
            var index = $("#selOrderType").jqxComboBox("getSelectedIndex");
            var orderTypeItem = orderTypeDataAdapter.records[index];
            var invoiceCode = orderTypeItem.DetailCode;

            var totalGross = 0;
            var totalNet = 0;
            var totalBundles = 0;
            var totalBala = 0;

            for (i = 0; i < selectedStocks.length; i++) {
                var item = selectedStocks[i];

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
            
        }

        function FlushGrid() {

            //更新可售列表
            var sids = "";
            for (i = 0; i < selectedStocks.length; i++) {
                if (i != 0) { sids += ","; }
                sids += selectedStocks[i].StockId;
            }

            var refNo = $("#txbRefNo").val();
            var ownCorpId = $("#selOwnCorp").val();

            sellSource.url = "Handler/OrderStockListHandler.ashx?sids=" + sids + "&cid=" + "<%= this.curSub.ContractId%>" + "&refNo=" + refNo + "&ownCorpId=" + ownCorpId;
            $("#jqxStockListGrid").jqxGrid("updatebounddata", "rows");
            selectedSource.localdata = selectedStocks;
            $("#jqxSelectedGrid").jqxGrid("updatebounddata", "rows");
        }

        function CreateOrder(isAudit) {

            var isCanSubmit = $("#jqxValidator").jqxValidator("validate");
            if (!isCanSubmit) { return; }

            if (selectedStocks.length == 0) { alert("未选中任何库存"); return; }
            if (!confirm("确认新增制单指令?")) { return; }            
            
            var buyerCorpId = 0;
            if ($("#selBuyerCorp").val() > 0) { buyerCorpId = $("#selBuyerCorp").val(); }

            var Order = {
                ContractId: "<%= this.curSub.ContractId%>",
                ContractNo: $("#txbContractNo").val(),
                SubId: "<%=this.curSub.SubId%>",
                //LCId: 0,
                LCNo: $("#txbLCNo").val(),
                LCDay: $("#txbLCDay").val(),
                OrderType: $("#selOrderType").val(),
                OrderDate: $("#txbOrderDate").val(),
                ApplyCorp: $("#selApplyCorp").val(),
                ApplyDept: $("#ddlApplyDeptId").val(),
                //SellerCorp: 0,
                BuyerCorp: buyerCorpId,
                BuyerCorpName: $("#txbBuyerCorpName").val(),
                BuyerAddress: $("#txbBuyerAddress").val(),
                //PaymentStyle: 0,
                RecBankId: $("#selCashInBank").val(),
                DiscountBase: $("#txbDiscountBase").val(),
                AssetId: $("#selAsset").val(),
                BrandId: $("#selBrand").val(),
                //AreaId: 0,
                AreaName: $("#txbDeliverPlace").val(),
                BankCode: $("#txbBankCode").val(),
                GrossAmount: $("#txbGrossAmount").val(),
                NetAmount: $("#txbNetAmount").val(),
                UnitId: $("#selUnit").val(),
                Bundles: $("#txbBundles").val(),
                Currency: $("#selCurrency").val(),
                UnitPrice: $("#txbInvoicePrice").val(),
                InvBala: $("#txbInvoiceBala").val(),
                //InvGap: 0,
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

            $.post("Handler/OrderCreateHandler.ashx", { order: JSON.stringify(Order), orderStockInvoice: JSON.stringify(selectedStocks), orderDetail: JSON.stringify(OrderDetail), IsSubmitAudit: isAudit },
                   function (result) {
                       var obj = JSON.parse(result);
                       if (obj.ResultStatus.toString() == "0") {
                           AjaxFileUpload(fileIds, obj.ReturnValue.OrderId, AttachTypeEnum.OrderAttach);
                       }
                       alert(obj.Message);
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "OrderList.aspx";
                       }
                   }
               );
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxValidator">

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

        <div id="jqxStockListExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                可售库存
            </div>
            <div>
                <div class="SearchExpander">
                    <ul>
                        <li>
                            <span>业务单号：</span>
                            <span>
                                <input type="text" id="txbRefNo" /></span>
                        </li>
                        <li>
                            <span style="float: left;">归属公司：</span>
                            <div style="float: left;" id="selOwnCorp"></div>
                        </li>
                        <li>
                            <input type="button" id="btnSearchStcok" value="查询" onclick="javascript: FlushGrid();" />
                        </li>
                    </ul>
                </div>
                <div id="jqxStockListGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
            </div>
        </div>

        <div id="jqxApplyAllotStockListExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                配货情况
            </div>
            <div>
                <div id="jqxSelectedGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
            </div>
        </div>

        <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                申请信息
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

        <div id="jqxDocumnetExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                单据数量
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
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Create" AttachType="OrderAttach" />

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" value="新增制单指令并提交审核" id="btnCreateAuditOrder" onclick="CreateOrder(true);" style="width: 180px; height: 25px;" />
        <input type="button" value="新增制单指令" id="btnCreateOrder" onclick="CreateOrder(false);" style="width: 120px; height: 25px;" />
    </div>

</body>
</html>
