<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceReplaceFinalUpdate.aspx.cs" Inherits="NFMTSite.Inv.InvoiceReplaceFinalUpdate" %>
<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>替临终票修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var proStocksSource = null;
        var proStocks = new Array();

        var repStocksSource = null;
        var repStocks = new Array();

        $(document).ready(function () {

            $("#jqxProStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxRepStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;
            proStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "ConfirmDetailId", type: "int" },
                   { name: "ConfirmPriceId", type: "int" },
                   { name: "RefDetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "StockStatus", type: "int" },
                   { name: "StockStatusName", type: "string" },
                   { name: "LastAmount", type: "number" },
                   { name: "LastBala", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "GapPrice", type: "number" },
                   { name: "ProPrice", type: "number" },
                   { name: "UnitPrice", type: "number" },
                   { name: "Bala", type: "number" }
                ],
                sort: function () {
                    $("#jqxProStocksGrid").jqxGrid("updatebounddata", "sort");
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
                localdata: <%=this.ProJson%>
                };

            var proStocksDataAdapter = new $.jqx.dataAdapter(proStocksSource, {
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

            $("#jqxProStocksGrid").jqxGrid(
            {
                width: "98%",
                source: proStocksDataAdapter,
                selectionmode: "singlecell",
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "临票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "业务单号", datafield: "RefNo", editable: false },                  
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "卡号", datafield: "CardNo", editable: false },
                  { text: "库存状态", datafield: "StockStatusName", editable: false },                  
                  { text: "临票重量", datafield: "LastAmount", sortable: false , editable: false},
                  { text: "临票金额", datafield: "LastBala",sortable: false , editable: false},
                  { 
                      text: "替临重量", datafield: "NetAmount", sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber", 
                      validation: function (cell, value) {
                          if (value < 0) {
                              return { result: false, message: "替临重量不能小于0" };
                          }
                          return true;
                      }, 
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true  });
                      }
                  },
                  { 
                      text: "结算单价", datafield: "UnitPrice", sortable: false, columntype: "numberinput",cellclassname: "GridFillNumber",                      
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "替临价差", datafield: "GapPrice",sortable: false, editable: false },
                  { text: "替临金额", datafield: "Bala",sortable: false, editable: false },
                  { text: "操作", datafield: "StockLogId", cellsrenderer: addRender, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            proStocks = proStocksDataAdapter.records;

            formatedData = "";
            totalrecords = 0;
            repStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "RefDetailId", type: "int" },
                   { name: "ConfirmDetailId", type: "int" },
                   { name: "ConfirmPriceId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "CurrencyName", type: "string" },
                   { name: "StockStatus", type: "int" },
                   { name: "StockStatusName", type: "string" },
                   { name: "LastAmount", type: "number" },
                   { name: "LastBala", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "GapPrice", type: "number" },
                   { name: "ProPrice", type: "number" },
                   { name: "UnitPrice", type: "number" },
                   { name: "Bala", type: "number" }
                ],
                sort: function () {
                    $("#jqxRepStocksGrid").jqxGrid("updatebounddata", "sort");
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
                localdata: <%=this.RepJson%>
                };

            var repStocksDataAdapter = new $.jqx.dataAdapter(repStocksSource, {
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
            
            $("#jqxRepStocksGrid").jqxGrid(
            {
                width: "98%",
                source: repStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                showaggregates:true,
                showstatusbar:true,
                statusbarheight:25,
                selectionmode: "singlecell",
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "临票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "业务单号", datafield: "RefNo", editable: false },                  
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "卡号", datafield: "CardNo", editable: false },
                  { text: "库存状态", datafield: "StockStatusName", editable: false },                  
                  { text: "临票重量", datafield: "LastAmount", sortable: false , editable: false},
                  { text: "临票金额", datafield: "LastBala",sortable: false , editable: false},
                  { text: "替临净重", datafield: "NetAmount", sortable: false, columntype: "numberinput",cellclassname: "GridFillNumber",
                      validation: function (cell, value) {
                          var item = repStocksDataAdapter.records[cell.row];
                          if (value < 0 || value > item.LastAmount) {
                              return { result: false, message: "开票净重不能小于0且不能大于剩余净重"+ item.LastAmount };
                          }
                          return true;
                      }, 
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      },
                      aggregates: [{ '总':
                          function (aggregatedValue, currentValue) {
                              if (currentValue) {
                                  aggregatedValue += currentValue ;
                              }
                              return Math.round(aggregatedValue*10000)/10000;
                          }
                      }]
                  },
                  { 
                      text: "结算单价", datafield: "UnitPrice", sortable: false, columntype: "numberinput",cellclassname: "GridFillNumber",                      
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "替临价差", datafield: "GapPrice",sortable: false, editable: false },
                  {
                      text: "替临金额", datafield: "Bala", editable: false, sortable: false,
                      aggregates: [{ '总':
                          function (aggregatedValue, currentValue) {
                              if (currentValue) {
                                  aggregatedValue += currentValue ;
                              }
                              return Math.round(aggregatedValue*100)/100;
                          }
                      }]
                  },
                  { text: "操作", datafield: "StockLogId", cellsrenderer: removeRender, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            repStocks = repStocksDataAdapter.records;

            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            
            //selOutCorp 对方公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curReplaceInvoice.SubContractId%>";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //selInCorp 收票公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1&SubId=" + "<%=this.curReplaceInvoice.SubContractId%>";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //txbInvoiceBala 发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25,  decimalDigits: 2, width: 140, spinButtons: true, disabled: true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, disabled: true });

            //txbUnitPrice 发票单价
            $("#txbUnitPrice").jqxNumberInput({ height: 25, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });              

            //品种 selAsset
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });

            //净重 txbNetAmount
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //计量单位 selUnit
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, disabled: true });
            
            //备注
            $("#txbMemo").jqxInput({ height: 23 });

            //buttons
            $("#btnUpdate").jqxButton();
            $("#btnCancel").jqxButton();

            //set control data
            var tempDate = new Date("<%=this.curInvoice.InvoiceDate.ToString("yyyy/MM/dd")%>");
            $("#txbInvoiceDate").jqxDateTimeInput({ value: tempDate });
            $("#selOutCorp").val(<%=this.curInvoice.OutCorpId%>);
            $("#selInCorp").val(<%=this.curInvoice.InCorpId%>);
            $("#txbInvoiceBala").val(<%=this.curInvoice.InvoiceBala%>);
            $("#selCurrency").val(<%=this.curInvoice.CurrencyId%>);
            $("#selAsset").val(<%=this.curReplaceInvoice.AssetId%>);
            $("#txbNetAmount").val(<%=this.curReplaceInvoice.NetAmount%>);
            $("#selUnit").val(<%=this.curReplaceInvoice.MUId%>);
            $("#txbMemo").val("<%=this.curInvoice.Memo%>");
            $("#txbUnitPrice").val(<%=this.curReplaceInvoice.UnitPrice%>);

            $("#jqxRepStocksGrid").on("cellvaluechanged", function (event) {
                repStocks = repStocksDataAdapter.records;
                FlushValue();
            });

            //验证
            $("#jqxInvoiceExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#selOutCorp", message: "对方公司必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selOutCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selInCorp", message: "我方公司必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selInCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbNetAmount", message: "净重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbNetAmount").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //修改
            $("#btnUpdate").click(function () {

                var isCanSubmit = $("#jqxInvoiceExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认修改替临终票？")) { return; }

                var rows = $("#jqxRepStocksGrid").jqxGrid("getrows");

                var invoice = {
                    InvoiceDate: $("#txbInvoiceDate").val(),
                    InvoiceBala: $("#txbInvoiceBala").val(),
                    OutCorpId: $("#selOutCorp").val(),
                    InCorpId: $("#selInCorp").val(),
                    Memo: $("#txbMemo").val()
                }

                var invoiceBusiness = {
                    BusinessInvoiceId: "<%=(int)this.curReplaceInvoice.BusinessInvoiceId%>",
                    RefInvoiceId:"<%=(int)this.curProvisionalInvoice.BusinessInvoiceId%>",
                    ContractId:"<%=this.curProvisionalInvoice.ContractId%>",
                    SubContractId:"<%=this.curProvisionalInvoice.SubContractId%>",
                    //IntegerAmount: $("#txbIntegerAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    MUId: $("#selUnit").val(),
                    UnitPrice:$("#txbUnitPrice").val(),
                };

                $.post("../Invoice/Handler/InvoiceReplaceFinalUpdateHandler.ashx", { Invoice: JSON.stringify(invoice), InvoiceBusiness: JSON.stringify(invoiceBusiness), Details: JSON.stringify(rows) },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceReplaceFinalList.aspx";
                        }
                    }
                );
            });

        });

        function bntCreateOnClick(row) {

            var item = proStocks[row];
            if (item.NetAmount == undefined || item.NetAmount < 0) { alert("开票重量必须大于0"); return; }

            repStocks.push(item);
            proStocks.splice(row, 1);

            FlushValue();
        }

        function bntRemoveOnClick(row) {

            var item = repStocks[row];

            repStocks.splice(row, 1);
            proStocks.push(item);
            
            FlushValue();
        }

        function FlushValue(){

            if (repStocks.length == 0) {
                $("#txbNetAmount").val(0);
                $("#txbInvoiceBala").val(0);
                $("#txbUnitPrice").val(0);
            }

            var sumNetAmount = 0;
            var sumInvoiceBala =0;

            for(var i = 0 ; i< repStocks.length; i++){            
                var item = repStocks[i];

                item.GapPrice = Math.round((item.ProPrice -  item.UnitPrice) * 10000)/10000;
                item.Bala = Math.round(item.GapPrice * item.NetAmount *100 )/100;
                sumNetAmount += item.NetAmount;
                sumInvoiceBala += item.Bala;
            }

            $("#txbNetAmount").val(sumNetAmount);
            $("#txbInvoiceBala").val(sumInvoiceBala);
            var unitPrice = Math.round(sumInvoiceBala/sumNetAmount * 10000)/10000;
            $("#txbUnitPrice").val(unitPrice);

            proStocksSource.localdata = proStocks;
            $("#jqxProStocksGrid").jqxGrid("updatebounddata", "rows");
            repStocksSource.localdata = repStocks;
            $("#jqxRepStocksGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat="server" ID="contractExpander1" />

    <div id="jqxProStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            临票明细列表
        </div>
        <div>
            <div id="jqxProStocksGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxRepStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            替临明细列表
        </div>
        <div>
            <div id="jqxRepStocksGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>开票日期：</strong>
                    <div id="txbInvoiceDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>对方公司：</strong>
                    <div id="selOutCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>我方公司：</strong>
                    <div id="selInCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>
                <li>
                    <strong>单价：</strong>
                    <div id="txbUnitPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div id="txbInvoiceBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>品种：</strong>
                    <div id="selAsset" style="float: left;"></div>
                </li>
                <li>
                    <strong>净重：</strong>
                    <div id="txbNetAmount" style="float: left;"></div>
                </li>
                <li>
                    <strong>计量单位：</strong>
                    <div id="selUnit" style="float: left;"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="修改发票" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
