<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceProvisionalCreate.aspx.cs" Inherits="NFMTSite.Inv.InvoiceProvisionalCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>临票新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var subStocksSource = null;
        var subStocks = new Array();

        var invStocksSource = null;
        var invStocks = new Array();

        $(document).ready(function () {

            $("#jqxSubStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxInvStocksExpander").jqxExpander({ width: "98%" });
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });
            
            //init stock list
            var formatedData = "";
            var totalrecords = 0;
            subStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" }, 
                   { name: "CardNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "LastAmount", type: "number" },
                   { name: "UnitPrice", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "StockStatus", type: "int" },
                   { name: "StockStatusName", type: "string" },
                   { name: "Bala", type: "number" }
                ],
                sort: function () {
                    $("#jqxSubStocksGrid").jqxGrid("updatebounddata", "sort");
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
                localdata: subStocks
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
            
            $("#jqxSubStocksGrid").jqxGrid(
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
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "业务单号", datafield: "RefNo", editable: false },                  
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "卡号", datafield: "CardNo", sortable: false, editable: false },
                  { text: "库存状态", datafield: "StockStatusName", editable: false },
                  { text: "剩余净重", datafield: "LastAmount",width:80, editable: false },
                  { text: "开票净重", datafield: "NetAmount",width:120, sortable: false, columntype: "numberinput",cellclassname: "GridFillNumber",
                      validation: function (cell, value) {
                          var item = subStocksDataAdapter.records[cell.row];
                          if (value < 0 || value > item.LastAmount) {
                              return { result: false, message: "开票净重不能小于0且不能大于剩余净重"+ item.LastAmount };
                          }
                          return true;
                      }, 
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 120, Digits: 8, spinButtons: true });
                      }
                  },                  
                  { text: "开票金额", datafield: "Bala", width: 160, sortable: false },
                  { text: "操作", datafield: "StockLogId", cellsrenderer: addRender, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            //invoice stocks
            formatedData = "";
            totalrecords = 0;
            invStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "LastAmount", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "UnitPrice", type: "number" },
                   { name: "StockStatus", type: "int" },
                   { name: "StockStatusName", type: "string" },
                   { name: "Bala", type: "number" }
                ],
                sort: function () {
                    $("#jqxInvStocksGrid").jqxGrid("updatebounddata", "sort");
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
                localdata: <%=this.SelectedJson%>
                };

            var invStocksDataAdapter = new $.jqx.dataAdapter(invStocksSource, {
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
            
            $("#jqxInvStocksGrid").jqxGrid(
            {
                width: "98%",
                source: invStocksDataAdapter,
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
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "业务单号", datafield: "RefNo", editable: false },                  
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false }, 
                  { text: "卡号", datafield: "CardNo", sortable: false, editable: false },
                  { text: "库存状态", datafield: "StockStatusName", editable: false },
                  { text: "剩余净重", datafield: "LastAmount",width:80, editable: false },                  
                  { text: "开票净重", datafield: "NetAmount",width:120, sortable: false, columntype: "numberinput",cellclassname: "GridFillNumber",
                      validation: function (cell, value) {
                          var item = invStocksDataAdapter.records[cell.row];
                          if (value < 0 || value > item.LastAmount) {
                              return { result: false, message: "开票净重不能小于0且不能大于剩余净重"+ item.LastAmount };
                          }
                          return true;
                      }, 
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 120, Digits: 8, spinButtons: true });
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
                      text: "开票金额", datafield: "Bala", width: 160, sortable: false, editable: false,
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

            invStocks = invStocksDataAdapter.records;

            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //selOutCorp 开票公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0" +"&SubId=" + "<%=this.curContractSub.SubId%>";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,selectedIndex:0 });

            //selInCorp 收票公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1"+"&SubId=" + "<%=this.curContractSub.SubId%>";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,selectedIndex:0 });

            //txbInvoiceBala 发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });            

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, disabled: true });
            
            //txbUnitPrice 发票单价
            $("#txbUnitPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });           

            $("#txbUnitPrice").on("valueChanged", function (event){
                var value = event.args.value;
                var sumAmount =0;
                var sumIntegerAmount =0;
                
                for(i=0;i<invStocks.length;i++){
                    var item = invStocks[i];

                    sumAmount += item.NetAmount;
                    sumIntegerAmount += item.IntegerAmount;
                    item.Bala = Math.round((item.NetAmount * value) *100)/100;
                }

                var sumInvoiceBala =  Math.round((sumAmount * value) * 100)/100;
                $("#txbInvoiceBala").val(sumInvoiceBala);
                $("#txbNetAmount").val(sumAmount);

                invStocksSource.localdata = invStocks;
                $("#jqxInvStocksGrid").jqxGrid("updatebounddata", "rows");
            });

            //品种 selAsset
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });

            //净重 txbNetAmount
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //计量单位 selUnit
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, disabled: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23 });            

            //buttons
            $("#btnCreate").jqxButton();
            $("#btnCancel").jqxButton();

            //set control data
            $("#selCurrency").val(<%=this.curContractSub.SettleCurrency%>);
            $("#selAsset").val(<%=this.curContract.AssetId%>);
            $("#selUnit").val(<%=this.curContractSub.UnitId%>);

            //价格初始化
            $("#txbUnitPrice").val(<%=this.curUnitPrice%>);            

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
                            input: "#txbInvoiceBala", message: "发票金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbInvoiceBala").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbNetAmount", message: "净重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbNetAmount").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //新增
            $("#btnCreate").click(function () {

                var isCanSubmit = $("#jqxInvoiceExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var rows = $("#jqxInvStocksGrid").jqxGrid("getrows");

                var gridBala =0;
                for(i =0 ;i < rows.length;i ++){
                    var item = rows[i];
                    gridBala = Math.round((gridBala + item.Bala) *100)/100;
                }
                var txbBala = $("#txbInvoiceBala").val();

                if(gridBala != txbBala){ alert("库存开票额明细之和不等于开票总金额，请进行调整"); return ;}


                if (!confirm("确认添加临票？")) { return; }

                var invoice = {
                    InvoiceDate: $("#txbInvoiceDate").val(),
                    InvoiceBala: $("#txbInvoiceBala").val(),
                    CurrencyId: $("#selCurrency").val(),
                    InvoiceDirection: "<%=(int)this.invoiceDirection%>",
                    OutCorpId: $("#selOutCorp").val(),
                    InCorpId: $("#selInCorp").val(),
                    Memo: $("#txbMemo").val()
                }

                var invoiceBusiness = {
                    ContractId:"<%=(int)this.curContractSub.ContractId%>",
                    SubContractId:"<%=(int)this.curContractSub.SubId%>",
                    AssetId: $("#selAsset").val(),
                    //IntegerAmount: $("#txbIntegerAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    UnitPrice:$("#txbUnitPrice").val(),
                    MUId: $("#selUnit").val()
                };


                $.post("../Invoice/Handler/InvoiceProvisionalCreateHandler.ashx", { Invoice: JSON.stringify(invoice), InvoiceBusiness: JSON.stringify(invoiceBusiness),Details: JSON.stringify(rows) },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceProvisionalList.aspx";
                        }
                    }
                );
            });
        });
        
        function bntCreateOnClick(row) {

            var item = subStocks[row];
            if (item.NetAmount == undefined || item.NetAmount < 0) { alert("开票重量必须大于0"); return; }
            if (item.Bala == undefined || item.Bala < 0) { alert("开票金额必须大于0"); return; }

            var unitPrice = $("#txbUnitPrice").val();
            item.Bala = Math.round((unitPrice * item.NetAmount) *100 )/100;

            var sumNet = $("#txbNetAmount").val();
            sumNet = Math.round((sumNet + item.NetAmount)*10000)/10000;
            $("#txbNetAmount").val(sumNet);

            var sumBala = Math.round((sumNet * unitPrice)*100)/100;
            $("#txbInvoiceBala").val(sumBala);           

            invStocks.push(item);
            subStocks.splice(row, 1);

            subStocksSource.localdata = subStocks;
            $("#jqxSubStocksGrid").jqxGrid("updatebounddata", "rows");
            invStocksSource.localdata = invStocks;
            $("#jqxInvStocksGrid").jqxGrid("updatebounddata", "rows");

        }

        function bntRemoveOnClick(row) {

            var item = invStocks[row];
            
            if (invStocks.length == 0) {
                $("#txbNetAmount").val(0);
                $("#txbInvoiceBala").val(0);
            }

            var sumNet = $("#txbNetAmount").val();
            sumNet = Math.round((sumNet - item.NetAmount)*10000)/10000;
            $("#txbNetAmount").val(sumNet);          

            var unitPrice = $("#txbUnitPrice").val();
            var sumBala = Math.round((sumNet * unitPrice)*100)/100;
            $("#txbInvoiceBala").val(sumBala);

            invStocks.splice(row, 1);
            subStocks.push(item);

            subStocksSource.localdata = subStocks;
            $("#jqxSubStocksGrid").jqxGrid("updatebounddata", "rows");
            invStocksSource.localdata = invStocks;
            $("#jqxInvStocksGrid").jqxGrid("updatebounddata", "rows");
         
        }
    </script>
</head>
<body>

    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat="server" ID="contractExpander1" />

    <div id="jqxSubStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            配货库存列表
        </div>
        <div>
            <div id="jqxSubStocksGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxInvStocksExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票明细列表
        </div>
        <div>
            <div id="jqxInvStocksGrid" style="float: left; margin: 5px 0 0 5px;"></div>
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
        <input type="button" id="btnCreate" value="新增临票" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
