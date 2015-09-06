<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriceConfirmDetail.aspx.cs" Inherits="NFMTSite.DoPrice.PriceConfirmDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价格确认单明细</title>
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

        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.priceConfirm.DataBaseName%>" + "&t=" + "<%=this.priceConfirm.TableName%>" + "&id=" + "<%=this.priceConfirm.PriceConfirmId%>";

        var confirmStocksSource = null;
        var confirmStocks = new Array();

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxConfirmStockLogsExpander").jqxExpander({ width: "98%" }); 
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <%--表格--%>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价格确认单明细
            <input type="hidden" id="hidIntersetAvg" />
            <input type="hidden" id="hidModel" runat="server" />
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
            $("#nbRealityAmount").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.mu.MUName%>", symbolPosition: "right",disabled:true });
            $("#nbRealityAmount").jqxNumberInput("val", "<%=this.priceConfirm.RealityAmount%>");

            //期货点价均价
            $("#nbPricingUnit").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right", disabled: true });
            $("#nbPricingUnit").jqxNumberInput("val", "<%=this.priceConfirm.PricingAvg%>");

            //升贴水均价
            $("#nbPremium").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" , disabled: true});
            $("#nbPremium").jqxNumberInput("val", "<%=this.priceConfirm.PremiumAvg%>");

            //利息合计
            $("#nbInterestBala").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right", disabled: true });
            $("#nbInterestBala").jqxNumberInput("val", "<%=this.priceConfirm.InterestBala%>");

            //其他价格
            $("#nbOtherPrice").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right" , disabled: true});
            $("#nbOtherPrice").jqxNumberInput("val", "<%=this.priceConfirm.OtherAvg%>");

            //结算单价
            $("#nbSettlePrice").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 4, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right", disabled: true });
            $("#nbSettlePrice").jqxNumberInput("val", "<%=this.priceConfirm.SettlePrice%>");

            //金额
            $("#nbSettleBala").jqxNumberInput({ width: "99%", height: 25, spinButtons: true, decimalDigits: 2, Digits: 9, symbol: "<%=this.cur.CurrencyName%>", symbolPosition: "right", disabled: true });
            $("#nbSettleBala").jqxNumberInput("val", "<%=this.priceConfirm.SettleBala%>");

            //选价日期
            $("#dtPricingDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: "99%" , disabled: true});
            $("#dtPricingDate").jqxDateTimeInput("val", new Date("<%=this.priceConfirm.PricingDate%>"));

            //提货单位
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlTakeCorpId").jqxComboBox({ width: "99%", source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlTakeCorpId").jqxComboBox("val", "<%=this.priceConfirm.TakeCorpId%>");
            
            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 25, disabled: true });
            $("#txbMemo").jqxInput("val", "<%=this.priceConfirm.Memo%>");

            //供方委托提货单位联系人
            $("#txbContactPerson").jqxInput({ width: "99%", height: 25 , disabled: true});
            $("#txbContactPerson").jqxInput("val", "<%=this.priceConfirm.ContactPerson%>");
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
                sortable: true,
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
                  { text: "起息日", datafield: "InterestStartDate", cellsformat: "yyyy-MM-dd",width: 90},
                  { text: "到息日", datafield: "InterestEndDate", cellsformat: "yyyy-MM-dd",width: 90},
                  { text: "计息天数", datafield: "InterestDay",width: 65},
                  { text: "日利息额", datafield: "InterestUnit"},
                  { text: "当前息额", datafield: "SettleBala"}
                  //{ text: "币种", datafield: "CurrencyName"},
                  //{ text: "计息类型", datafield: "InterestTypeName"}
                ]
            });
        });
    </script>

    <%--按钮--%>
    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnComplete" value="执行完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCompleteCancel" value="执行完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 50,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            var id = "<%=this.priceConfirm.PriceConfirmId%>";

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/PriceConfirmStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PriceConfirmList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/PriceConfirmStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PriceConfirmList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/PriceConfirmStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PriceConfirmList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/PriceConfirmStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PriceConfirmList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
