<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotMainDetail.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotMainDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款分配明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.cashInAllot.DataBaseName%>" + "&t=" + "<%=this.cashInAllot.TableName%>" + "&id=" + "<%=this.cashInAllot.AllotId%>";

        $(document).ready(function () {

            $("#jqxCashInExpander").jqxExpander({ width: "98%" });
            $("#jqxContractExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxAllotInfoExpander").jqxExpander({ width: "98%" });
            $("#jqxSIInvocieExpander").jqxExpander({ width: "98%" });

            //////////////////////////////////合约列表//////////////////////////////////

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "SubId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "CreateFrom", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "SubNo", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "TradeDirectionName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "ContractWeight", type: "string" },
                   { name: "PriceModeName", type: "string" },
                   { name: "ContractStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "RefId", type: "string" }
                ],
                localdata:<%=this.contractGirdInfo%>
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxContractGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "签订日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" ,width:100},
                  { text: "内部子合约号", datafield: "SubNo",width:120 },
                  { text: "外部合约号", datafield: "OutContractNo",width:170  },
                  { text: "购销方向", datafield: "TradeDirectionName",width:80  },
                  { text: "我方公司", datafield: "InCorpName" ,width:200 },
                  { text: "对方公司", datafield: "OutCorpName",width:200 },
                  { text: "交易品种", datafield: "AssetName",width:80 },
                  { text: "合约重量", datafield: "ContractWeight" },
                  { text: "点价方式", datafield: "PriceModeName" },
                  { text: "合约状态", datafield: "StatusName" }
                ]
            });

            //////////////////////////////////合约库存//////////////////////////////////

            formatedData = "";
            totalrecords = 0;
            invStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockLogId", type: "int" },
                   { name: "StockNameId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "NetGapAmount", type: "number" },
                   { name: "AllotNetAmount", type: "number" },
                   { name: "AllotBala", type: "number" }
                ],
                localdata : <%=this.stockGridInfo%>
            };

            var invStocksDataAdapter = new $.jqx.dataAdapter(invStocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                source: invStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 25,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false, width: 80 },
                  { text: "品牌", datafield: "BrandName", editable: false, width: 80 },
                  { text: "交货地", datafield: "DPName" , editable: false},
                  { text: "卡号", datafield: "CardNo" , editable: false},
                  { text: "库存状态", datafield: "StatusName", editable: false },
                  { text: "重量单位", datafield: "MUName", editable: false },
                  { text: "过磅净重", datafield: "NetGapAmount", editable: false },
                  { text: "配款净重", datafield: "AllotNetAmount", editable: false },
                  { text: "分配金额", datafield: "AllotBala", editable: false }
                ]
            });
            
            var Infosource =
            {
                url: "Handler/CashInAllotMainSIInvoiceListHandler.ashx?sIIds=<%=this.upSIIds%>",
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
                   { name: "InvoiceBalaValue", type: "number" },
                   { name: "SIId", type: "int" },
                   { name: "AllotBala", type: "number" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxSIInvoiceGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxSIInvoiceGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  { text: "分配金额", datafield: "AllotBala" }                  
                ]
            });


            //分配公司
            var allotCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=<%=this.cashInContract.SubContractId%>";
            var allotCorpSource = { datatype: "json", url: allotCorpUrl, async: false };
            var allotCorpDataAdapter = new $.jqx.dataAdapter(allotCorpSource);
            $("#ddlAllotCorp").jqxComboBox({ source: allotCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase", selectedIndex: 0 ,disabled:true});
            $("#ddlAllotCorp").jqxComboBox("val","<%=this.cashInCorp.CorpId%>");

            //分配金额
            $("#nbAllotBala").jqxNumberInput({ height: 25, min: 0, max: 999999999, decimalDigits: 2, digits: 9, width: 140, spinButtons: true,disabled:true });
            $("#nbAllotBala").jqxNumberInput("val","<%=this.cashInAllot.AllotBala%>");

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true ,disabled:true});
            $("#ddlCurrency").jqxDropDownList("val", "<%=this.cashInAllot.CurrencyId%>");

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.PayMatter%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#ddlAllotType").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 120, height: 25, autoDropDownHeight: true, selectedIndex: 0 ,disabled:true});
            $("#ddlAllotType").jqxDropDownList("val", "<%=this.cashInAllot.AllotType%>");

            $("#txbAllotDesc").jqxInput({ width: "500", height: 25 ,disabled:true});
            $("#txbAllotDesc").jqxInput("val", "<%=this.cashInAllot.AllotDesc%>");
            
            //buttons
            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();
            $("#btnClose").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: MasterEnum.收款分配审核,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CashInAllotMainStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotMainList.aspx";
                        }
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/CashInAllotMainStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotMainList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/CashInAllotMainStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotMainList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/CashInAllotMainStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotMainList.aspx";
                        }
                    }
                );
            });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/CashInAllotMainStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotMainList.aspx";
                        }
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxCashInExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            收款登记信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>收款日期：</strong>
                    <span><%=this.cashIn.CashInDate.ToShortDateString()%></span>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <span><%=this.InCorpName%></span>
                </li>

                <li>
                    <strong>收款银行：</strong>
                    <span><%=this.InBankName%></span>
                </li>
                <li>
                    <strong>收款账户：</strong>
                    <span><%=this.InBankAccountNo%></span>
                </li>

                <li>
                    <strong>收款金额：</strong>
                    <span><%=this.cashIn.CashInBala%></span>
                </li>
                <li>
                    <strong>收款币种：</strong>
                    <span><%=this.CurrencyName%></span>
                </li>

                <li>
                    <strong>付款公司：</strong>
                    <span><%=this.cashIn.PayCorpName%></span>
                </li>

                <li>
                    <strong>付款银行：</strong>
                    <span><%=this.cashIn.PayBank%></span>
                </li>
                <li>
                    <strong>付款账户：</strong>
                    <span><%=this.cashIn.PayAccount%></span>
                </li>

                <li>
                    <strong>简短附言：</strong>
                    <span><%=this.cashIn.PayWord%></span>
                </li>
                <li>
                    <strong>外部流水备注：</strong>
                    <span><%=this.cashIn.BankLog%></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxContractExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约列表
        </div>
        <div>
            <div id="jqxContractGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约库存列表
        </div>
        <div>
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxSIInvocieExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价外票列表
        </div>
        <div>
            <div id="jqxSIInvoiceGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAllotInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            收款分配信息
            <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong style="float: left;">分配至公司：</strong>
                    <div style="float: left;" id="ddlAllotCorp"></div>
                </li>
                <li>
                    <strong style="float: left;">分配金额：</strong>
                    <div style="float: left" id="nbAllotBala"></div>
                </li>
                <li>
                    <strong style="float: left;">币种：</strong>
                    <div style="float: left;" id="ddlCurrency" />
                </li>
                <li>
                    <strong style="float: left;">收款事项：</strong>
                    <div style="float: left" id="ddlAllotType"></div>
                </li>
                <li>
                    <strong style="float: left;">备注：</strong>
                    <textarea id="txbAllotDesc" style="height: 120px;"></textarea><br />
                </li>
            </ul>
        </div>
    </div>

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnCompleteCancel" value="确认完成撤销" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnClose" value="关闭" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
