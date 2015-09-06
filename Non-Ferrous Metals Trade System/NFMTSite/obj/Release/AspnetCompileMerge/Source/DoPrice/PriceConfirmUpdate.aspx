<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriceConfirmUpdate.aspx.cs" Inherits="NFMTSite.DoPrice.PriceConfirmUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价格确认单修改</title>
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

        var confirmStocksSource = null;
        var confirmStocks = new Array();

        var subStocksSource = null;
        var subStocks = new Array();

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxConfirmStockLogsExpander").jqxExpander({ width: "98%" }); 
            $("#jqxSubStockLogsExpander").jqxExpander({ width: "98%" });
        });

        function ChangeValue(){

            var interestId = 0;

            var RealityAmount = 0;
            var PricingAvg = 0;
            var PremiumAvg = 0;
            var InterestBala = 0;
            var InterestAvg = 0;
            var OtherPrice = 0;
            var SettlePrice = 0;
            var SettleBala = 0;

            var sumBala = 0;
            var sumPre = 0;
            var sumOther = 0;

            for(var i = 0;i < confirmStocks.length; i++){
                if(confirmStocks[i].InterestId && confirmStocks[i].InterestId > 0){
                    if(confirmStocks[i].InterestId != interestId){
                        interestId = confirmStocks[i].InterestId;
                    }

                    sumBala +=  confirmStocks[i].PricingUnit * confirmStocks[i].ConfirmAmount;
                    sumPre += confirmStocks[i].Premium * confirmStocks[i].ConfirmAmount;
                    sumOther += confirmStocks[i].OtherPrice * confirmStocks[i].ConfirmAmount;
                    InterestBala += confirmStocks[i].SettleBala;
                }

                if(confirmStocks[i].ConfirmAmount){
                    RealityAmount += confirmStocks[i].ConfirmAmount;
                }
            }

            PricingAvg = sumBala / RealityAmount;
            PremiumAvg = sumPre / RealityAmount;
            OtherPrice = sumOther / RealityAmount;
            InterestAvg = InterestBala / RealityAmount;
            SettlePrice = PricingAvg + PremiumAvg + InterestAvg + OtherPrice;
            SettleBala = SettlePrice * RealityAmount;

            $("#nbRealityAmount").jqxNumberInput("val",RealityAmount);
            $("#nbPricingUnit").jqxNumberInput("val",Math.round(PricingAvg*100)/100);
            $("#nbPremium").jqxNumberInput("val", Math.round(PremiumAvg*100)/100);
            $("#nbOtherPrice").jqxNumberInput("val", Math.round(OtherPrice*100)/100);
            $("#nbInterestBala").jqxNumberInput("val",Math.round(InterestBala*100)/100);
            $("#nbSettlePrice").jqxNumberInput("val", Math.round(SettlePrice*100)/100);
            $("#nbSettleBala").jqxNumberInput("val", Math.round(SettleBala*100)/100);
            $("#hidIntersetAvg").val(Math.round(InterestAvg*100)/100);

            confirmStocksSource.localdata = confirmStocks;
            $("#jqxConfirmStockLogsGrid").jqxGrid("updatebounddata", "rows"); 
            subStocksSource.localdata = subStocks;
            $("#jqxSubStockLogsGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <%--表格--%>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价格确认单修改
            <input type="hidden" id="hidIntersetAvg" />
        </div>
        <div>
            <table class="tableStyle">
                <tr>
                    <td style="width: 250px">
                        <div class="txt">我方</div>
                    </td>
                    <td style="width: 800px" colspan="5">
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
                    <td>
                        <div class="txt">合同号</div>
                    </td>
                    <td style="width: 280px">
                        <input type="text" id="txbContractNo" runat="server" />
                    </td>
                    <td style="width: 250px">
                        <div class="txt">购销方向</div>
                    </td>
                    <td style="width: 280px">
                        <input type="text" id="txbTradeDirection" runat="server" />
                    </td>
                    <td style="width: 250px">
                        <div class="txt">合同数量</div>
                    </td>
                    <td style="width: 280px">
                        <div id="nbContractAmount"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">发货数量</div>
                    </td>
                    <td>
                        <div id="nbRealityAmount"></div>
                    </td>
                    <td>
                        <div class="txt">期货点价均价</div>
                    </td>
                    <td>
                        <div id="nbPricingUnit"></div>
                    </td>
                    <td>
                        <div class="txt">升贴水均价</div>
                    </td>
                    <td>
                        <div id="nbPremium"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">其他价格均价</div>
                    </td>
                    <td>
                        <div id="nbOtherPrice"></div>
                    </td>
                    <td>
                        <div class="txt">结算单价</div>
                    </td>
                    <td>
                        <div id="nbSettlePrice"></div>
                    </td>
                    <td>
                        <div class="txt">利息合计</div>
                    </td>
                    <td>
                        <div id="nbInterestBala"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="txt">结算总额</div>
                    </td>
                    <td>
                        <div id="nbSettleBala"></div>
                    </td>
                    <td>
                        <div class="txt">选价日期</div>
                    </td>
                    <td>
                        <div id="dtPricingDate"></div>
                    </td>
                    <td>
                        <div class="txt">提货单位</div>
                    </td>
                    <td>
                        <div id="ddlTakeCorpId"></div>
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
                <tr>
                    <td>
                        <div class="txt">供方委托提货单位联系人</div>
                    </td>
                    <td colspan="5">
                        <input type="text" id="txbContactPerson" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            //我方
            $("#txbInCorpId").jqxInput({ width: "99%", height: 25, disabled: true });

            //对方
            $("#txbOutCorpId").jqxInput({ width: "99%", height: 25, disabled: true });

            //合同号
            $("#txbContractNo").jqxInput({ width: "99%", height: 25, disabled: true });

            //购销方向
            $("#txbTradeDirection").jqxInput({ width: "99%", height: 25, disabled: true });

            //合同数量
            $("#nbContractAmount").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.mu.MUName%>", symbolPosition: "right", disabled: true });
            $("#nbContractAmount").jqxNumberInput("val", "<%=this.sub.SignAmount%>");

            //发货数量
            $("#nbRealityAmount").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.mu.MUName%>", symbolPosition: "right" });
            $("#nbRealityAmount").jqxNumberInput("val", "<%=this.priceConfirm.RealityAmount%>");

            //期货点价均价
            $("#nbPricingUnit").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" });
            $("#nbPricingUnit").jqxNumberInput("val", "<%=this.priceConfirm.PricingAvg%>");

            //升贴水均价
            $("#nbPremium").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" });
            $("#nbPremium").jqxNumberInput("val", "<%=this.priceConfirm.PremiumAvg%>");

            //利息合计
            $("#nbInterestBala").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" });
            $("#nbInterestBala").jqxNumberInput("val", "<%=this.priceConfirm.InterestBala%>");

            //其他价格
            $("#nbOtherPrice").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" });
            $("#nbOtherPrice").jqxNumberInput("val", "<%=this.priceConfirm.OtherAvg%>");

            //结算单价
            $("#nbSettlePrice").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" });
            $("#nbSettlePrice").jqxNumberInput("val", "<%=this.priceConfirm.SettlePrice%>");

            //金额
            $("#nbSettleBala").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" });
            $("#nbSettleBala").jqxNumberInput("val", "<%=this.priceConfirm.SettleBala%>");

            //选价日期
            $("#dtPricingDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: "99%" });
            $("#dtPricingDate").jqxDateTimeInput("val", new Date("<%=this.priceConfirm.PricingDate%>"));

            //提货单位
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlTakeCorpId").jqxComboBox({ width: "99%", source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase" });
            $("#ddlTakeCorpId").jqxComboBox("val", "<%=this.priceConfirm.TakeCorpId%>");
            
            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 25 });
            $("#txbMemo").jqxInput("val", "<%=this.priceConfirm.Memo%>");

            //供方委托提货单位联系人
            $("#txbContactPerson").jqxInput({ width: "99%", height: 25 });
            $("#txbContactPerson").jqxInput("val", "<%=this.priceConfirm.ContactPerson%>");

            $("#hidIntersetAvg").val("<%=this.priceConfirm.InterestAvg%>");

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#nbPricingUnit", message: "期货点价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbPricingUnit').jqxNumberInput("val") > 0;
                            }
                        },
                        //{
                        //    input: "#nbInterestBala", message: "利息合计必须大于0", action: "keyup,blur", rule: function (input, commit) {
                        //        return $('#nbInterestBala').jqxNumberInput("val") > 0;
                        //    }
                        //},
                        {
                            input: "#nbSettlePrice", message: "结算单价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbSettlePrice').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbSettleBala", message: "结算金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbSettleBala').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#ddlTakeCorpId", message: "提货单位不可为空", action: "keyup,blur", position: 'left:0,0', rule: function (input, commit) {
                                return $('#ddlTakeCorpId').jqxComboBox("val") > 0;
                            }
                        }
                    ]
            });

            $('#nbPricingUnit').on('valueChanged', function (event)
            {
                var value = event.args.value;
                $("#nbSettlePrice").jqxNumberInput("val",parseFloat(value) + parseFloat($("#nbPremium").val()) + parseFloat($("#hidIntersetAvg").val()) + parseFloat($("#nbOtherPrice").val()));
            });
			
            $('#nbPremium').on('valueChanged', function (event)
            {
                var value = event.args.value;
                $("#nbSettlePrice").jqxNumberInput("val",parseFloat(value) + parseFloat($("#nbPricingUnit").val()) + parseFloat($("#hidIntersetAvg").val()) + parseFloat($("#nbOtherPrice").val()));
            }); 

            $('#nbOtherPrice').on('valueChanged', function (event)
            {
                var value = event.args.value;
                $("#nbSettlePrice").jqxNumberInput("val",parseFloat(value) + parseFloat($("#nbPricingUnit").val()) + parseFloat($("#hidIntersetAvg").val()) + parseFloat($("#nbPremium").val()));
            });
			
            $('#nbSettlePrice').on('valueChanged', function (event)
            {
                var value = event.args.value;
                $("#nbSettleBala").jqxNumberInput("val",parseFloat(value) * parseFloat($("#nbRealityAmount").val()));
            });

            $("#nbRealityAmount").on('valueChanged', function (event)
            {
                var value = event.args.value;
                $("#nbSettleBala").jqxNumberInput("val",parseFloat(value) * parseFloat($("#nbSettlePrice").val()));
            }); 
        });
    </script>

    <%--价格确认明细（上）--%>
    <div id="jqxConfirmStockLogsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择利息明细
        </div>
        <div>
            <div id="jqxConfirmStockLogsGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
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
                   { name: "InterestId", type: "int" },
                   { name: "InterestDetailId", type: "int" },
                   { name: "SubContractId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "TotalPricingUnit", type: "number" },
                   { name: "TotalPremium", type: "number" },
                   { name: "TotalOtherPrice", type: "number" },
                   { name: "TotalInterestPrice", type: "number" },
                   { name: "PayCapital", type: "number" },
                   { name: "CurCapital", type: "number" },
                   { name: "InterestRate", type: "number" },
                   { name: "TotalInterestBala", type: "number" },
                   { name: "InterestAmountDay", type: "number" },
                   { name: "TotalInterestAmount", type: "number" },
                   { name: "Unit", type: "int" },
                   { name: "MUName", type: "string" },
                   { name: "InterestStyle", type: "int" },
                   { name: "InterestStyleName", type: "string" },
                   { name: "ConfirmAmount", type: "number" },
                   { name: "PricingUnit", type: "number" },
                   { name: "Premium", type: "number" },
                   { name: "OtherPrice", type: "number" },
                   { name: "SettlePrice", type: "number" },
                   { name: "StockBala", type: "number" },
                   { name: "InterestStartDate", type: "date" },
                   { name: "InterestEndDate", type: "date" },
                   { name: "InterestDay", type: "int" },
                   { name: "InterestUnit", type: "number" },
                   { name: "SettleBala", type: "number" },
                   { name: "InterestType", type: "int" },
                   { name: "InterestTypeName", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" }
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
                sortcolumn: "idetail.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "idetail.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.SelectedJsonUp%>
            };

            var confirmStocksDataAdapter = new $.jqx.dataAdapter(confirmStocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            
            $("#jqxConfirmStockLogsGrid").jqxGrid(
            {
                width: "98%",
                source: confirmStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //showaggregates:true,
                //showstatusbar:true,
                statusbarheight:25,
                sortable: false,
                //selectionmode: "singlecell",
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo",width:120 },
                  { text: "品种", datafield: "AssetName",width:70},
                  { text: "品牌", datafield: "BrandName",width:60 },
                  { text: "仓库", datafield: "DPName",width:70},
                  { text: "卡号", datafield: "CardNo",width:120},
                  { text: "计息重量", datafield: "ConfirmAmount" },
                  //{ text: "单位", datafield: "MUName" },
                  //{ text: "计息价格", datafield: "InterestPrice"},
                  //{ text: "计息货值", datafield: "StockBala"},
                  { text: "起息日", datafield: "InterestStartDate", cellsformat: "yyyy-MM-dd"},
                  { text: "到息日", datafield: "InterestEndDate", cellsformat: "yyyy-MM-dd"},
                  { text: "计息天数", datafield: "InterestDay"},
                  { text: "日利息额", datafield: "InterestUnit"},
                  { text: "当前息额", datafield: "SettleBala"},
                  //{ text: "币种", datafield: "CurrencyName"},
                  //{ text: "计息类型", datafield: "InterestTypeName"},
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxConfirmStockLogsGrid").jqxGrid("getrowdata", row);

                          if(dataRecord.InterestId && dataRecord.InterestId > 0){
                              for(var i=0;i<confirmStocks.length;i++){
                                  if(confirmStocks[i].InterestId==dataRecord.InterestId){
                                      subStocks.push(confirmStocks[i]);
                                  }
                              }

                              for(var i=confirmStocks.length-1;i>=0;i--){
                                  if(confirmStocks[i].InterestId==dataRecord.InterestId){
                                      confirmStocks.splice(i, 1);
                                  }
                              }
                          }else{
                              subStocks.push(confirmStocks[row]);
                              confirmStocks.splice(row, 1);
                          }

                          ChangeValue();
                      }
                  }
                ]
            });

            confirmStocks = confirmStocksDataAdapter.records;
        });
    </script>

    <%--价格确认明细（下）--%>
    <div id="jqxSubStockLogsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可选择利息明细
        </div>
        <div>
            <div id="jqxSubStockLogsGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            //库存列表
            subStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "InterestId", type: "int" },
                   { name: "InterestDetailId", type: "int" },
                   { name: "SubContractId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "TotalPricingUnit", type: "number" },
                   { name: "TotalPremium", type: "number" },
                   { name: "TotalOtherPrice", type: "number" },
                   { name: "TotalInterestPrice", type: "number" },
                   { name: "PayCapital", type: "number" },
                   { name: "CurCapital", type: "number" },
                   { name: "InterestRate", type: "number" },
                   { name: "TotalInterestBala", type: "number" },
                   { name: "InterestAmountDay", type: "number" },
                   { name: "TotalInterestAmount", type: "number" },
                   { name: "Unit", type: "int" },
                   { name: "MUName", type: "string" },
                   { name: "InterestStyle", type: "int" },
                   { name: "InterestStyleName", type: "string" },
                   { name: "ConfirmAmount", type: "number" },
                   { name: "PricingUnit", type: "number" },
                   { name: "Premium", type: "number" },
                   { name: "OtherPrice", type: "number" },
                   { name: "SettlePrice", type: "number" },
                   { name: "StockBala", type: "number" },
                   { name: "InterestStartDate", type: "date" },
                   { name: "InterestEndDate", type: "date" },
                   { name: "InterestDay", type: "int" },
                   { name: "InterestUnit", type: "number" },
                   { name: "SettleBala", type: "number" },
                   { name: "InterestType", type: "int" },
                   { name: "InterestTypeName", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" }
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
                sortcolumn: "idetail.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "idetail.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.SelectedJsonDown%>
            };

            var subStocksDataAdapter = new $.jqx.dataAdapter(subStocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error); 
                }
            });
            
            $("#jqxSubStockLogsGrid").jqxGrid(
            {
                width: "98%",
                source: subStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: false,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo",width:120 },
                  { text: "品种", datafield: "AssetName",width:70},
                  { text: "品牌", datafield: "BrandName",width:60 },
                  { text: "仓库", datafield: "DPName",width:70},
                  { text: "卡号", datafield: "CardNo",width:120},
                  { text: "计息重量", datafield: "ConfirmAmount" },
                  //{ text: "单位", datafield: "MUName" },
                  //{ text: "计息价格", datafield: "InterestPrice"},
                  //{ text: "计息货值", datafield: "StockBala"},
                  { text: "起息日", datafield: "InterestStartDate", cellsformat: "yyyy-MM-dd"},
                  { text: "到息日", datafield: "InterestEndDate", cellsformat: "yyyy-MM-dd"},
                  { text: "计息天数", datafield: "InterestDay"},
                  { text: "日利息额", datafield: "InterestUnit"},
                  { text: "当前息额", datafield: "SettleBala"},
                  //{ text: "币种", datafield: "CurrencyName"},
                  //{ text: "计息类型", datafield: "InterestTypeName"},
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "添加";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxSubStockLogsGrid").jqxGrid("getrowdata", row);

                          if(dataRecord.InterestId && dataRecord.InterestId > 0){
                              for(var i=0;i<subStocks.length;i++){
                                  if(subStocks[i].InterestId==dataRecord.InterestId){
                                      confirmStocks.push(subStocks[i]);
                                  }
                              }

                              for(var i=subStocks.length-1;i>=0;i--){
                                  if(subStocks[i].InterestId==dataRecord.InterestId){
                                      subStocks.splice(i, 1);
                                  }
                              }
                          }else{
                              confirmStocks.push(subStocks[row]);
                              subStocks.splice(row, 1);
                          }
                          
                          ChangeValue();
                      }
                  }
                ]
            });

            subStocks = subStocksDataAdapter.records;
        });
    </script>

    <%--按钮--%>
    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnAdd" value="修改" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="PriceConfirmList.aspx" id="btnCancel">取消</a>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnAdd").click(function () { PriceConfirmCreate(); });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            function PriceConfirmCreate() {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                $("#btnAdd").jqxButton({ disabled: true });

                var priceConfirm = {
                    PriceConfirmId: "<%=this.priceConfirm.PriceConfirmId%>",
                    OutCorpId: "<%=this.subOutCorpDetails.FirstOrDefault(a=>a.IsDefaultCorp==true).CorpId%>",
                    InCorpId: "<%=this.subInCorpDetails.FirstOrDefault(a=>a.IsDefaultCorp==true).CorpId%>",
                    ContractId: "<%=this.sub.ContractId%>",
                    SubId: "<%=this.sub.SubId%>",
                    ContractAmount: "<%=this.contract.SignAmount%>",
                    SubAmount: "<%=this.sub.SignAmount%>",
                    RealityAmount: $("#nbRealityAmount").val(),
                    PricingAvg: $("#nbPricingUnit").val(),
                    PremiumAvg: $("#nbPremium").val(),
                    OtherAvg:$("#nbOtherPrice").val(),
                    InterestAvg:$("#hidIntersetAvg").val(),
                    InterestBala: $("#nbInterestBala").val(),
                    SettlePrice: $("#nbSettlePrice").val(),
                    SettleBala: $("#nbSettleBala").val(),
                    PricingDate: $("#dtPricingDate").val(),
                    CurrencyId:"<%=this.sub.SettleCurrency%>",
                    UnitId:"<%=this.sub.UnitId%>",
                    TakeCorpId: $("#ddlTakeCorpId").val(),
                    ContactPerson: $("#txbContactPerson").val(),
                    Memo: $("#txbMemo").val()
                    //PriceConfirmStatus
                }

                var rows = $("#jqxConfirmStockLogsGrid").jqxGrid("getrows");

                $.post("Handler/PriceConfirmUpdateHandler.ashx", {
                    priceConfirm: JSON.stringify(priceConfirm),
                    rows: JSON.stringify(rows)
                },
                function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    $("#btnAdd").jqxButton({ disabled: false });
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "PriceConfirmList.aspx";
                    }
                });
            }
        });
    </script>

</body>
</html>
