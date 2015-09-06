<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayContractAllotToStockCreate.aspx.cs" Inherits="NFMTSite.Funds.PayContractAllotToStockCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约付款分配至库存新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">

        var stockLogsSource = null;
        var stockAppSource = null;

        var stockAppDataAdapter = null;
        var stockLogsDataAdapter = null;

        var stockApps = new Array();
        var stockLogs = new Array();

        $(document).ready(function () {
            $("#jqxPaymentInfoExpander").jqxExpander({ width: "98%" });
            $("#jqxStockAppsExpander").jqxExpander({ width: "98%" }); 
            $("#jqxStockLogsExpander").jqxExpander({ width: "98%" });

            //init payment expander
            $("#txbPayDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120,disabled:true });

            //付款公司            
            var payCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var payCorpSource = {
                datatype: "json", url: payCorpUrl, async: false
            };
            var payCorpDataAdapter = new $.jqx.dataAdapter(payCorpSource);
            $("#selPayCorp").jqxComboBox({
                source: payCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,disabled:true
            });

            $("#selPayCorp").on("change", function (event) {
                var args = event.args;
                if (args) {                    
                    var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
                    var tempSource = { datatype: "json", url: tempUrl, async: false };
                    var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                    $("#selPayAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25,disabled:true });
                }
            });

            //付款银行            
            var payBankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var payBankSource = { datatype: "json", url: payBankUrl, async: false };
            var payBankDataAdapter = new $.jqx.dataAdapter(payBankSource);
            $("#selPayBank").jqxComboBox({ source: payBankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120,disabled:true });

            $("#selPayBank").on("change", function (event) {
                var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
                var tempSource = { datatype: "json", url: tempUrl, async: false };
                var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                $("#selPayAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25,disabled:true });
            });

            //付款账户
            var payAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
            var payAccountSource = { datatype: "json", url: payAccountUrl, async: false };
            var payAccountDataAdapter = new $.jqx.dataAdapter(payAccountSource);
            $("#selPayAccount").jqxComboBox({ source: payAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25,disabled:true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#selCurrency").val(<%=this.curPayApply.CurrencyId%>);

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 ,disabled:true});

            //付款总额
            $("#txbpayBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });

            //财务付款金额
            $("#txbFundsPayBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true  });

            //收款公司
            var recCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var recCorpSource = {
                datatype: "json", url: recCorpUrl, async: false
            };
            var recCorpDataAdapter = new $.jqx.dataAdapter(recCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: recCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120,disabled:true
            });
            $("#selRecCorp").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    var obj = recCorpDataAdapter.records[index];
                    $("#spnSelRecCorpFullName").html(obj.CorpFullName);

                    var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
                    var tempSource = { datatype: "json", url: tempUrl, async: false };
                    var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                    $("#selRecAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 ,disabled:true});
                }
            });

            //收款开户行
            var recBankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var recBankSource = { datatype: "json", url: recBankUrl, async: false };
            var recBankDataAdapter = new $.jqx.dataAdapter(recBankSource);
            $("#selRecBank").jqxComboBox({ source: recBankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120,disabled:true });
            $("#selRecBank").on("change", function (event) {

                var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
                var tempSource = { datatype: "json", url: tempUrl, async: false };
                var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                $("#selRecAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120,disabled:true });
            });

            //收款账号
            var recAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
            var recAccountSource = { datatype: "json", url: recAccountUrl, async: false };
            var recAccountDataAdapter = new $.jqx.dataAdapter(recAccountSource);
            $("#selRecAccount").jqxComboBox({ source: recAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 ,disabled:true});
            $("#selRecAccount").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var item = args.item;
                    $("#txbRecAccount").val(item.label);
                }
            });

            $("#txbRecAccount").jqxInput({ height: 23,disabled:true });

            //收款信息赋值，默认与付款申请信息相同
            $("#selRecCorp").val(<%=this.curPayApply.RecCorpId%>);
            $("#selRecBank").val(<%=this.curPayApply.RecBankId%>);
            if(<%=this.curPayApply.RecBankAccountId%> >0){
                $("#selRecAccount").val(<%=this.curPayApply.RecBankAccountId%>);
            }
            $("#txbRecAccount").val("<%=this.curPayApply.RecBankAccount%>");
            $("#selPayMode").val(<%=this.curPayApply.PayMode%>);

            //付款流水
            $("#txbPayLog").jqxInput({ height: 23,disabled:true });

            //备注
            $("#txbMemo").jqxInput({ height:23,disabled:true });

            //虚拟付款
            $("#chkVirtual").jqxCheckBox({  checked: false , disabled:true });
            $("#txbVirtualBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });           

            //init control data
            var tempDate = new Date("<%=this.curPayment.PayDatetime.ToString("yyyy/MM/dd")%>");
            $("#txbPayDate").jqxDateTimeInput({ value: tempDate });

            $("#selPayCorp").val(<%=this.curPayment.PayCorp%>);
            $("#selPayBank").val(<%=this.curPayment.PayBankId%>);
            $("#selPayAccount").val(<%=this.curPayment.PayBankAccountId%>);
            $("#selPayMode").val(<%=this.curPayment.PayStyle%>);
            $("#txbpayBala").val(<%=this.curPayment.PayBala%>);
            $("#selRecCorp").val(<%=this.curPayment.RecevableCorp%>);
            $("#selRecBank").val(<%=this.curPayment.ReceBankId%>);
            
            if(<%=this.curPayment.ReceBankAccountId%> > 0){
                $("#selRecAccount").val(<%=this.curPayment.ReceBankAccountId%>);
            }
            $("#txbRecAccount").val("<%=this.curPayment.ReceBankAccount%>");
            $("#txbPayLog").val("<%=this.curPayment.FlowName%>");
            $("#txbMemo").val("<%=this.curPayment.Memo%>");            
            
            if(<%=this.curPaymentVirtual.VirtualId%> > 0){
                $("#chkVirtual").val(true);
                $("#txbVirtualBala").val(<%=this.curPaymentVirtual.PayBala%>);
            }
            $("#txbFundsPayBala").val(<%=this.curPayment.FundsBala%>);

            var formatedData = "";
            var totalrecords = 0;
            //已选库存流水信息
            stockAppSource =
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
                sortcolumn: "sl.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sl.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: stockApps,
                datatype: "json"
            };
            stockAppDataAdapter = new $.jqx.dataAdapter(stockAppSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxStockAppsGrid").jqxGrid(
            {
                width: "98%",
                autoheight: true,
                source: stockAppDataAdapter,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 25,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "OwnCorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "单位", datafield: "MUName" },
                  { text: "净量", datafield: "NetAmount" },
                  {
                      text: "付款金额", datafield: "PayBala",
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
                  {
                      text: "虚拟付款金额", datafield: "VirtualBala",
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
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var item = stockAppDataAdapter.records[row];
                          stockApps.splice(row, 1);
                          FlushGrid();
                      }
                  }
                ]
            });

            //可选库存流水信息
            formatedData = "";
            totalrecords = 0;
            var stockLogsUrl = "Handler/PayContractAllotToStockCanSelectStockHandler.ashx?id=" + "<%= this.curSub.SubId%>";
            stockLogsSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxStockLogsGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sl.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sl.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: stockLogsUrl
            };
            stockLogsDataAdapter = new $.jqx.dataAdapter(stockLogsSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxStockLogsGrid").jqxGrid(
            {
                width: "98%",
                source: stockLogsDataAdapter,
                pageable: true,
                autoheight: true,
                editable: true,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                selectionmode: "singlecell",
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" ,editable: false},
                  { text: "库存状态", datafield: "StatusName" ,editable: false},
                  { text: "归属公司", datafield: "OwnCorpName" ,editable: false},
                  { text: "品种", datafield: "AssetName",editable: false },
                  { text: "品牌", datafield: "BrandName",editable: false },
                  { text: "交货地", datafield: "DPName" ,editable: false},
                  { text: "卡号", datafield: "CardNo",editable: false },
                  { text: "捆数", datafield: "Bundles",editable: false },
                  { text: "单位", datafield: "MUName" ,editable: false},
                  { text: "净量", datafield: "NetAmount",editable: false },
                  {
                      text: "付款金额", datafield: "PayBala", columntype: "numberinput", cellclassname: "GridFillNumber", sortable: false,
                      validation: function (cell, value) {
                          if (value <= 0) {
                              return { result: false, message: "付款金额必须大于0" };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, Digits: 8, spinButtons: true });
                      }
                  },
                  {
                      text: "虚拟付款金额", datafield: "VirtualBala", columntype: "numberinput", cellclassname: "GridFillNumber", sortable: false,
                      validation: function (cell, value) {
                          if (value < 0) {
                              return { result: false, message: "虚拟付款金额不能小于0" };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, Digits: 8, spinButtons: true });
                      }
                  },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "分配";
                      }, buttonclick: function (row) {
                          var item = stockLogsDataAdapter.records[row];
                          if (item.PayBala == undefined || item.PayBala == 0) { alert("付款金额必须大于0"); return; }

                          stockApps.push(item);
                          FlushGrid();
                      }
                  }
                ]
            });

            //buttons
            $("#btnCreate").jqxButton({ height: 25, width: 120 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 120 });

            //新增
            $("#btnCreate").click(function () {

                if (stockApps.length > 0) {
                    var canAllotBala = parseFloat("<%=this.canAllotBala%>");
                    var allotBala = 0;
                    for (i = 0; i < stockApps.length; i++) {
                        allotBala += stockApps[i].PayBala;
                    }

                    if (canAllotBala < allotBala) { alert("分配金额不能超过" + canAllotBala); return; }
                }
                else
                    alert("请选择分配库存");

                if (!confirm("确认分配？")) { return; }

                var item = {
                    ContractDetailId:"<%=this.paymentContractDetail.DetailId%>",
                    PaymentId:"<%=this.curPayment.PaymentId%>",
                    ContractId:"<%=this.paymentContractDetail.ContractId%>",
                    SubId :"<%=this.paymentContractDetail.ContractSubId%>",
                    PayApplyId:"<%=this.curPayApply.PayApplyId%>",
                    PayApplyDetailId:0,
                    SourceFrom:"<%=(int)NFMT.Funds.PaymenyAllotTypeEnum.合约付款分配%>"
                }

                $.post("Handler/PayContractAllotStockCreateHandler.ashx", { rows: JSON.stringify(stockApps),item:JSON.stringify(item)},
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PayContractAllotToStockList.aspx";
                        }
                    }
                );
            });
        });

        function FlushGrid() {
            var logIds = "";
            for (i = 0; i < stockApps.length; i++) {
                if (i != 0) { logIds += ","; }
                logIds += stockApps[i].StockLogId;
            }

            stockAppSource.localdata = stockApps;
            $("#jqxStockAppsGrid").jqxGrid("updatebounddata", "rows");

            stockLogsSource.url = "Handler/PayContractAllotToStockCanSelectStockHandler.ashx?id=" + "<%= this.curSub.SubId%>" + "&logIds=" + logIds;
            $("#jqxStockLogsGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat="server" ID="contractExpander1" />

    <div id="jqxPaymentInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            财务付款信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>付款日期：</strong>
                    <div id="txbPayDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款公司：</strong>
                    <div id="selPayCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款银行：</strong>
                    <div id="selPayBank" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款账户：</strong>
                    <div id="selPayAccount" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款方式：</strong>
                    <div id="selPayMode" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款总额：</strong>
                    <div id="txbpayBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>财务付款金额：</strong>
                    <div id="txbFundsPayBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>虚拟付款：</strong>
                    <div id="chkVirtual" style="float: left;"></div>
                </li>
                <li>
                    <strong>虚拟金额：</strong>
                    <div id="txbVirtualBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <div id="selRecCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>收款人全称：</strong>
                    <span runat="server" id="spnSelRecCorpFullName"></span>
                </li>
                <li>
                    <strong>收款开户行：</strong>
                    <div id="selRecBank" style="float: left;" />
                </li>
                <li>
                    <strong>收款账号：</strong>
                    <div id="selRecAccount" style="float: left;" />
                </li>
                <li>
                    <input type="text" id="txbRecAccount" />
                </li>
                <li>
                    <strong>付款流水号：</strong>
                    <input id="txbPayLog" style="float: left;" type="text" />
                </li>
                <li>
                    <strong>备注：</strong>
                    <input id="txbMemo" style="float: left;" type="text" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxStockAppsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择分配库存
        </div>
        <div>
            <div id="jqxStockAppsGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxStockLogsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可选择分配库存
        </div>
        <div>
            <div id="jqxStockLogsGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="分配" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="PayContractAllotToStockList.aspx" id="btnCancel">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
