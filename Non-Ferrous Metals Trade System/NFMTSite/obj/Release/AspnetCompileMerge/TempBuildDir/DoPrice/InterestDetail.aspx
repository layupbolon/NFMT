<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterestDetail.aspx.cs" Inherits="NFMTSite.DoPrice.InterestDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>利息结算明细</title>
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

        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curInterest.DataBaseName%>" + "&t=" + "<%=this.curInterest.TableName%>" + "&id=" + "<%=this.curInterest.InterestId%>";

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxConfirmStockLogsExpander").jqxExpander({ width: "98%" });

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
            $("#txbInterestDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", disabled: true });            

            //计息方式
            var inertestStyleSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.计息方式%>", async: false };
            var inertestStyleDataAdapter = new $.jqx.dataAdapter(inertestStyleSource);
            $("#selInertestStyle").jqxComboBox({ source: inertestStyleDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", height: 25, autoDropDownHeight: true, disabled: true });            

            //合同剩余本金
            $("#txbPayCapital").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });           

            //合同数量
            $("#txbContractAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curMeasureUnit.MUName%>", symbolPosition: "right", disabled: true });            

            //发货数量
            $("#txbInterestAmount").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curMeasureUnit.MUName%>", symbolPosition: "right", disabled: true });            

            //利息合计
            $("#txbInterestBala").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });

            //期货点价
            $("#txbPricingUnit").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });            

            //升贴水
            $("#txbPremium").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });            

            //其他价格
            $("#txbOtherPrice").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });            

            //计息单价
            $("#txbInterestPrice").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });            

            //利息率
            $("#txbInterestRate").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 3, symbol: "%", symbolPosition: "right", disabled: true });

            //每吨天利息
            $("#txbInterestAmountDay").jqxNumberInput({ height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.curCurrency.CurrencyName%>", symbolPosition: "right", disabled: true });

            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 25, disabled: true });

            //价格确认列表
            var formatedData = "";
            var totalrecords = 0;

            var confirmStocksSource =
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
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "仓库", datafield: "DPName" },
                  { text: "单位", datafield: "MUName" },
                  { text: "净重", datafield: "NetAmount", width: 80 },
                  {
                      text: "结息净重", datafield: "InterestAmount", width: 100,
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
                  { text: "剩余净重", datafield: "LastAmount", width: 80 },
                  {
                      text: "结算金额", datafield: "StockBala", width: 120,
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
                  { text: "起息日", datafield: "InterestStartDate", width: 90,cellsformat: "yyyy-MM-dd" },
                  { text: "到息日", datafield: "InterestEndDate", width: 90,cellsformat: "yyyy-MM-dd" },
                  { text: "计息天数", datafield: "InterestDay", width: 65 },
                  { text: "日利息额", datafield: "InterestUnit" },
                  {
                      text: "利息金额", datafield: "InterestBala", width: 120,
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 10000) / 10000;
                              }
                      }]
                  }
                ]
            });

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

            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 54,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/InterestStatusHandler.ashx", { id: "<%=this.curInterest.InterestId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InterestList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/InterestStatusHandler.ashx", { id: "<%=this.curInterest.InterestId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InterestList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/InterestStatusHandler.ashx", { id: "<%=this.curInterest.InterestId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InterestList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/InterestStatusHandler.ashx", { id: "<%=this.curInterest.InterestId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InterestList.aspx";
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
