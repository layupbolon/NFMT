<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterestUpdate.aspx.cs" Inherits="NFMTSite.DoPrice.InterestUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>利息结算修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/ajaxfileupload.js"></script>

    <style type="text/css">
        .txt {
            text-align: center;
            font-size: 14px;
            color: #767676;
            font-weight: bold;
        }

        .tableStyle {
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #000;
        }

            .tableStyle tr td {
                height: 25px;
            }
    </style>

    <script type="text/javascript">

        var subStocksSource = null;
        var subStocks = new Array();

        var confirmStocksSource = null;
        var confirmStocks = new Array();

        //计息方式
        var isCapital = true;
        //余额行
        var lastItem = null;

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxConfirmStockLogsExpander").jqxExpander({ width: "98%" });
            $("#jqxSubStockLogsExpander").jqxExpander({ width: "98%" });

            //我方
            $("#txbInCorpId").jqxInput({ width: "99%", height: 25, disabled: true });

            //对方
            $("#txbOutCorpId").jqxInput({ width: "99%", height: 25, disabled: true });

            //合同号
            $("#txbContractNo").jqxInput({ width: "99%", height: 25, disabled: true });

            //购销方向
            $("#txbTradeDirection").jqxInput({ width: "99%", height: 25, disabled: true });

            //产品名称
            $("#txbAssetName").jqxInput({ width: "99%", height: 25, disabled: true });

            //结息日期
            $("#txbInterestDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd" });            

            //计息方式
            var inertestStyleSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.计息方式%>", async: false };
            var inertestStyleDataAdapter = new $.jqx.dataAdapter(inertestStyleSource);
            $("#selInertestStyle").jqxComboBox({ source: inertestStyleDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", height: 25, autoDropDownHeight: true, disabled: true });            

            //合同剩余本金
            $("#txbPayCapital").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right" });           

            //合同数量
            $("#txbContractAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curMeasureUnit.MUName%>", symbolPosition: "right", disabled: true });            

            //发货数量
            $("#txbInterestAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curMeasureUnit.MUName%>", symbolPosition: "right", disabled: true });            

            //利息合计
            $("#txbInterestBala").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right" });

            //期货点价
            $("#txbPricingUnit").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right" });            

            //升贴水
            $("#txbPremium").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });            

            //其他价格
            $("#txbOtherPrice").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right" });            

            //计息单价
            $("#txbInterestPrice").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });            

            //利息率
            $("#txbInterestRate").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 3, symbol: "%", symbolPosition: "right" });

            //每吨天利息
            $("#txbInterestAmountDay").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right" });

            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 25 });

            //价格确认列表
            var formatedData = "";
            var totalrecords = 0;

            confirmStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "InterestAmount", type: "number" },
                   { name: "LastAmount", type: "number" },
                   { name: "StockBala", type: "number" },
                   { name: "InterestStartDate", type: "date" },
                   { name: "InterestEndDate", type: "date" },
                   { name: "InterestDay", type: "int" },
                   { name: "InterestUnit", type: "number" },
                   { name: "InterestBala", type: "number" },
                   { name: "InterestType", type: "int" }
                ],
                sort: function () {
                    $("#jqxConfirmStockLogsGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.curJson%>
            };

            var confirmStocksDataAdapter = new $.jqx.dataAdapter(confirmStocksSource, {
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

            $("#jqxConfirmStockLogsGrid").jqxGrid(
            {
                width: "98%",
                source: confirmStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 25,
                sortable: true,
                selectionmode: "singlecell",
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "仓库", datafield: "DPName", editable: false },
                  { text: "单位", datafield: "MUName", editable: false },
                  { text: "净重", datafield: "NetAmount", width: 80, editable: false },
                  {
                      text: "结息净重", datafield: "InterestAmount", width: 100, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber",
                      validation: function (cell, value) {
                          var item = confirmStocksDataAdapter.records[cell.row];
                          if (value < 0 || value > item.NetWeight) {
                              return { result: false, message: "结息净重不能小于0且不能大于净重" + item.NetAmout };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 4, width: 100, Digits: 8, spinButtons: true });
                      },
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 10000) / 10000;
                              }
                      }]
                  },
                  { text: "剩余净重", datafield: "LastAmount", width: 80, editable: false },
                  {
                      text: "结算金额", datafield: "StockBala", width: 120, sortable: false, editable: false,
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 10000) / 10000;
                              }
                      }]
                  },
                  { text: "起息日", datafield: "InterestStartDate", width: 90,cellsformat: "yyyy-MM-dd", columntype: "datetimeinput", cellclassname: "GridFillNumber" },
                  { text: "到息日", datafield: "InterestEndDate", width: 90,cellsformat: "yyyy-MM-dd", columntype: "datetimeinput", cellclassname: "GridFillNumber" },
                  { text: "计息天数", datafield: "InterestDay", width: 65, editable: false },
                  {
                      text: "日利息额", datafield: "InterestUnit", columntype: "numberinput", cellclassname: "GridFillNumber", cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                          return value + "<%=this.curCurrency.CurrencyName%>";
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  {
                      text: "利息金额", datafield: "InterestBala", width: 120, columntype: "numberinput", cellclassname: "GridFillNumber",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 2, Digits: 8, spinButtons: true });
                      },
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 100) / 100;
                              }
                      }]
                  },
                  { text: "操作", width: 60, cellsrenderer: removeRender, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            confirmStocks = confirmStocksDataAdapter.records;

            //库存列表
            subStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "InterestAmount", type: "number" },
                   { name: "LastAmount", type: "number" },
                   { name: "StockBala", type: "number" },
                   { name: "InterestStartDate", type: "date" },
                   { name: "InterestEndDate", type: "date" },
                   { name: "InterestDay", type: "int" },
                   { name: "InterestUnit", type: "number" },
                   { name: "InterestBala", type: "number" },
                   { name: "InterestType", type: "int" }
                ],
                sort: function () {
                    $("#jqxSubStockLogsGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.othJson%>
            };

            var subStocksDataAdapter = new $.jqx.dataAdapter(subStocksSource, {
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

            $("#jqxSubStockLogsGrid").jqxGrid(
            {
                width: "98%",
                source: subStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                selectionmode: "singlecell",
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "仓库", datafield: "DPName", editable: false },
                  { text: "单位", datafield: "MUName", editable: false },
                  { text: "净重", datafield: "NetAmount", width: 80, editable: false },
                  {
                      text: "结息净重", datafield: "InterestAmount", width: 100, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber",
                      validation: function (cell, value) {
                          var item = confirmStocksDataAdapter.records[cell.row];
                          if (value < 0 || value > item.NetWeight) {
                              return { result: false, message: "结息净重不能小于0且不能大于净重" + item.NetAmout };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 4, width: 100, Digits: 8, spinButtons: true });
                      },
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 10000) / 10000;
                              }
                      }]
                  },
                  { text: "剩余净重", datafield: "LastAmount", width: 80, editable: false },
                  {
                      text: "结算金额", datafield: "StockBala", width: 120, sortable: false, editable: false,
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 10000) / 10000;
                              }
                      }]
                  },
                  { text: "起息日", datafield: "InterestStartDate", width: 90,cellsformat: "yyyy-MM-dd", columntype: "datetimeinput", cellclassname: "GridFillNumber" },
                  { text: "到息日", datafield: "InterestEndDate", width: 90,cellsformat: "yyyy-MM-dd", columntype: "datetimeinput", cellclassname: "GridFillNumber" },
                  { text: "计息天数", datafield: "InterestDay", width: 65, editable: false },
                  {
                      text: "日利息额", datafield: "InterestUnit", columntype: "numberinput", cellclassname: "GridFillNumber", cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                          return value + "<%=this.curCurrency.CurrencyName%>";
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  {
                      text: "利息金额", datafield: "InterestBala", width: 120, columntype: "numberinput", cellclassname: "GridFillNumber",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 2, Digits: 8, spinButtons: true });
                      },
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 100) / 100;
                              }
                      }]
                  },
                  { text: "操作", cellsrenderer: addRender, width: 60,enabletooltips: false, sortable: false, editable: false }
                ]
            });

            subStocks = subStocksDataAdapter.records;

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //初始赋值
            $("#txbInCorpId").val("<%=this.curInCorp.CorpName%>");
            $("#txbOutCorpId").val("<%=this.curOutCorp.CorpName%>");
            $("#txbContractNo").val("<%=this.curSub.SubNo%>");
            $("#txbTradeDirection").val("<%=this.curTradeDirection.ToString("F")%>");
            $("#txbAssetName").val("<%=this.curAsset.AssetName%>");
            var tempDate = new Date("<%=this.curInterest.InterestDate.ToString("yyyy/MM/dd")%>");
            $("#txbInterestDate").jqxDateTimeInput({ value: tempDate });            
            $("#txbPayCapital").val(<%= this.curInterest.PayCapital %>);
            $("#txbContractAmount").val(<%= this.curSub.SignAmount%>);
            $("#txbInterestAmount").val(<%=this.curInterest.InterestAmount%>);
            $("#txbPricingUnit").val(<%=this.curInterest.PricingUnit%>);
            $("#txbPremium").val(<%=this.curInterest.Premium%>);
            $("#txbOtherPrice").val(<%=this.curInterest.OtherPrice%>);
            $("#txbInterestPrice").val(<%=this.curInterest.InterestPrice%>);
            $("#txbInterestRate").val(<%=this.curInterest.InterestRate%>);
            $("#txbInterestAmountDay").val(<%=this.curInterest.InterestAmountDay%>);
            $("#txbMemo").val("<%=this.curInterest.Memo%>");
            $("#selInertestStyle").val(<%=this.curInterest.InterestStyle%>);
            $("#txbInterestBala").val(<%=this.curInterest.InterestBala%>);
            //控件事件

            $("#jqxSubStockLogsGrid").on("cellvaluechanged", function (event) {

                subStocks = subStocksDataAdapter.records;

                for (var i = 0; i < subStocks.length; i++) {
                    var temp = subStocks[i];

                    //计息日
                    var interestDay = Math.round((temp.InterestEndDate - temp.InterestStartDate) / 1000 / 3600 / 24) - 1;
                    if (interestDay < 0) { interestDay = 0; }
                    temp.InterestDay = interestDay;
                }

                subStocksSource.localdata = subStocks;
                $("#jqxSubStockLogsGrid").jqxGrid("updatebounddata", "rows");
            });

            $("#jqxConfirmStockLogsGrid").on("cellvaluechanged", function (event) {
                var datafield = event.args.datafield;
                if (datafield != "InterestBala") {
                    confirmStocks = confirmStocksDataAdapter.records;
                    ReFlushValue();
                }
            });

            //其他价格更改
            $("#txbOtherPrice").on("valueChanged", function (event) {
                var interestPrice = $("#txbPricingUnit").val() + $("#txbOtherPrice").val() + $("#txbPremium").val();
                $("#txbInterestPrice").val(interestPrice);
            });
            //期货结算价更改
            $("#txbPricingUnit").on("valueChanged", function (event) {
                var interestPrice = $("#txbPricingUnit").val() + $("#txbOtherPrice").val() + $("#txbPremium").val();
                $("#txbInterestPrice").val(interestPrice);
            });

            //计息方式更改
            $("#selInertestStyle").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    if (index == 1) {
                        //货押计息
                        isCapital = false;
                        $("#txbPayCapital").val(0);
                        $("#txbPayCapital").jqxNumberInput({ disabled: true });
                        $("#txbInterestRate").val(0);
                        $("#txbInterestRate").jqxNumberInput({ disabled: true });
                        $("#txbInterestAmountDay").jqxNumberInput({ disabled: false });
                    }
                    else {
                        //本金计息
                        isCapital = true;
                        $("#txbPayCapital").jqxNumberInput({ disabled: false });
                        $("#txbInterestRate").jqxNumberInput({ disabled: false });
                        $("#txbInterestAmountDay").val(0);
                        $("#txbInterestAmountDay").jqxNumberInput({ disabled: true });
                    }
                }

                ReFlushValue();
            });

           

            //合同本金修改
            $("#txbPayCapital").on("valueChanged", function (event) {
                if (isCapital) {
                    ReFlushValue();
                }
            });

            //利息率修改
            $("#txbInterestRate").on("valueChanged", function (event) {
                if (isCapital) {
                    ReFlushValue();
                }
            });

            //每吨天利息修改
            $("#txbInterestAmountDay").on("valueChanged", function (event) {
                if (!isCapital) {
                    ReFlushValue();
                }
            });

            //计息单价修改
            $("#txbInterestPrice").on("valueChanged", function (event) {
                ReFlushValue();
            });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#txbPricingUnit", message: "期货点价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbPricingUnit").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbInterestPrice", message: "计息单价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbInterestPrice").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            $("#btnUpdate").on("click",function(event){
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if(!confirm("确认更新利息结算？")){ return ;}

                $("#btnUpdate").jqxButton({ disabled: true });

                var interest = {
                    InterestId : "<%=this.curInterest.InterestId%>",
                    SubContractId: "<%=this.curInterest.SubContractId%>",
                    ContractId: "<%=this.curInterest.ContractId%>",
                    CurrencyId: "<%=this.curInterest.CurrencyId%>",
                    PricingUnit: $("#txbPricingUnit").val(),
                    Premium: $("#txbPremium").val(),
                    OtherPrice: $("#txbOtherPrice").val(),
                    InterestPrice: $("#txbInterestPrice").val(),
                    PayCapital: $("#txbPayCapital").val(),
                    //CurCapital: 0,
                    InterestRate: $("#txbInterestRate").val(),
                    InterestBala: $("#txbInterestBala").val(),
                    InterestAmountDay: $("#txbInterestAmountDay").val(),
                    InterestAmount: $("#txbInterestAmount").val(),
                    Memo: $("#txbMemo").val(),
                    InterestDate: $("#txbInterestDate").val()
                }

                var interestDetail = confirmStocksDataAdapter.records;

                var sumDetailBala = 0;
                var sumDetailInterest =0;

                for(var i =0; i<interestDetail.length; i++){
                    var item = interestDetail[i];
                    sumDetailBala += item.StockBala;
                    sumDetailInterest += item.InterestBala;
                    if(item.InterestType.toString() == "<%=(int)NFMT.DoPrice.InterestTypeEnum.差额计息%>"){ 
                        if(item.StockBala <0){
                            alert("本金余额不能为负");
                            return
                        }
                    }
                }

                var detailInterests = Math.round(sumDetailInterest*100);
                var interestBala  = Math.round($("#txbInterestBala").val()*100);                

                //判断利息明细与利息总额
                if(detailInterests != interestBala){
                    alert("利息总额与明细金额之和不相等");
                    return;
                }

                if(isCapital){
                    //本金计息判断结算总金额与预付本金
                    if(Math.round(sumDetailBala*100)/100 != Math.round($("#txbPayCapital").val()*100)/100){
                        alert("结算总金额不等于预付本金。");
                        return;
                    }
                }

                $.post("Handler/InterestUpdateHandler.ashx", { Interest: JSON.stringify(interest), InterestDetail: JSON.stringify(interestDetail) }, function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "InterestList.aspx";
                    }
                    else {
                        $("#btnUpdate").jqxButton({ disabled: false });
                    }
                });
            });

        });

              function bntCreateOnClick(row) {

                  var item = subStocks[row];

                  confirmStocks.push(item);
                  subStocks.splice(row, 1);

                  ReFlushValue();
              }

              function bntRemoveOnClick(row) {

                  var item = confirmStocks[row];
                  confirmStocks.splice(row, 1);
                  subStocks.push(item);

                  if (confirmStocks.length == 0) {
                      $("#txbInterestAmount").val(0);
                      $("#txbInterestBala").val(0);
                  }

                  ReFlushValue();
              }

              function ReFlushValue() {

                  var sumWeight = 0;
                  var sumStockBala = 0;
                  var sumInterest = 0;
                  var sumBala = 0;
                  var interestRate = $("#txbInterestRate").val() / 100;//利息率
                  var interestAmountDay = $("#txbInterestAmountDay").val();//每吨天利息额
                  var capitalItem = null;

                  for (var i = 0; i < confirmStocks.length; i++) {
                      var temp = confirmStocks[i];

                      //非差额计息
                      if (temp.InterestType.toString() != "<%=(int)NFMT.DoPrice.InterestTypeEnum.差额计息%>" ) {

                          //总发货数量
                          sumWeight += temp.InterestAmount;

                          //计算剩余重量
                          temp.LastAmount = Math.round((temp.NetAmount - temp.InterestAmount) * 10000) / 10000;

                          //单日库存结息金额
                          var interestPrice = $("#txbInterestPrice").val();
                          var stockBala = Math.round(temp.InterestAmount * interestPrice * 10000) / 10000;
                          temp.StockBala = stockBala;

                          //总额计算
                          sumStockBala += stockBala;
                      }
                      else {
                          capitalItem = temp;
                      }
                  }

                  if (isCapital == true && capitalItem != null) {
                      //本金余额行结算
                      capitalItem.StockBala = Math.round(($("#txbPayCapital").val() - sumStockBala) * 10000) / 10000;
                  }

                  for (var i = 0; i < confirmStocks.length; i++) {

                      var temp = confirmStocks[i];

                      //计息日
                      var interestDay = parseInt((temp.InterestEndDate - temp.InterestStartDate) / 1000 / 3600 / 24);
                      if (interestDay < 0) { interestDay = 0; }
                      temp.InterestDay = interestDay;

                      if (isCapital) {
                          //本金计息方式：单日利息 = 利息率*结算金额
                          var interestUnit = Math.round(temp.StockBala * interestRate * 10000) / 10000;
                          temp.InterestUnit = interestUnit;
                      }
                      else {
                          //货押计息方式：单日利息 = 结算净重*每吨天利息金额
                          var interestUnit = Math.round(temp.NetAmount * interestAmountDay * 10000) / 10000;
                          temp.InterestUnit = interestUnit;
                      }

                      //利息
                      var interest = Math.round(temp.InterestUnit * temp.InterestDay * 100) / 100;
                      temp.InterestBala = interest;

                      //总利息计算
                      sumInterest += interest;
                  }

                  sumWeight = Math.round(sumWeight * 10000) / 10000;
                  $("#txbInterestAmount").val(sumWeight);//结息总重量
                  $("#txbInterestBala").val(sumInterest);//利息总金额                 

                  subStocksSource.localdata = subStocks;
                  $("#jqxSubStockLogsGrid").jqxGrid("updatebounddata", "rows");
                  confirmStocksSource.localdata = confirmStocks;
                  $("#jqxConfirmStockLogsGrid").jqxGrid("updatebounddata", "rows");

              }
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价格确认单新增
        </div>
        <div>
            <table class="tableStyle">
                <tr>
                    <td>
                        <div class="txt">我方</div>
                    </td>
                    <td colspan="5">
                        <input type="text" id="txbInCorpId" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">对方</div>
                    </td>
                    <td colspan="5">
                        <input type="text" id="txbOutCorpId" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <div class="txt">合同号</div>
                    </td>
                    <td>
                        <input type="text" id="txbContractNo" runat="server" />
                    </td>
                    <td style="width: 120px;">
                        <div class="txt">购销方向</div>
                    </td>
                    <td>
                        <input type="text" id="txbTradeDirection" runat="server" />
                    </td>
                    <td style="width: 120px;">
                        <div class="txt">产品名称</div>
                    </td>
                    <td>
                        <input type="text" id="txbAssetName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">结息日期</div>
                    </td>
                    <td>
                        <div id="txbInterestDate"></div>
                    </td>
                    <td>
                        <div class="txt">计息方式</div>
                    </td>
                    <td>
                        <div id="selInertestStyle"></div>
                    </td>
                    <td>
                        <div class="txt">合同剩余本金</div>
                    </td>
                    <td>
                        <div id="txbPayCapital"></div>
                    </td>
                </tr>
                <tr>

                    <td>
                        <div class="txt">合同数量</div>
                    </td>
                    <td>
                        <div id="txbContractAmount"></div>
                    </td>
                    <td>
                        <div class="txt">结息数量</div>
                    </td>
                    <td>
                        <div id="txbInterestAmount"></div>
                    </td>
                    <td>
                        <div class="txt">利息合计</div>
                    </td>
                    <td>
                        <div id="txbInterestBala"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">期货点价</div>
                    </td>
                    <td>
                        <div id="txbPricingUnit"></div>
                    </td>
                    <td>
                        <div class="txt">升贴水</div>
                    </td>
                    <td>
                        <div id="txbPremium"></div>
                    </td>
                    <td>
                        <div class="txt">其他价格</div>
                    </td>
                    <td>
                        <div id="txbOtherPrice"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">计息单价</div>
                    </td>
                    <td>
                        <div id="txbInterestPrice"></div>
                    </td>

                    <td>
                        <div class="txt">利息率</div>
                    </td>
                    <td>
                        <div id="txbInterestRate"></div>
                    </td>
                    <td>
                        <div class="txt">每吨天利息</div>
                    </td>
                    <td>
                        <div id="txbInterestAmountDay"></div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div class="txt">备注</div>
                    </td>
                    <td colspan="5">
                        <input type="text" id="txbMemo" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="jqxConfirmStockLogsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            结息明细
        </div>
        <div>
            <div id="jqxConfirmStockLogsGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxSubStockLogsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            待结息明细
        </div>
        <div>
            <div id="jqxSubStockLogsGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="修改" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="InterestList.aspx" id="btnCancel">取消</a>
    </div>

</body>
</html>
