<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotCreate.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约收款分配新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var cashInSelectSource;
        var cashInAllotSource;

        var corpSelectSource;
        var corpAllotSource;
        
        var cashInDetails = new Array();//收款登记选中集合
        var cashInExcs = new Array();//排除的合约分配明细集合

        $(document).ready(function () {            

            //Expander Init
            $("#jqxOtherExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxCashInSelectExpander").jqxExpander({ width: "98%" });            
            $("#jqxAllotExpander").jqxExpander({ width: "98%",height:"180px" });
            $("#jqxCashInAllotExpander").jqxExpander({ width: "98%" });

            //////////////////////已分配情况//////////////////////
            var formatedData = "";
            var totalrecords = 0;
            var otherSource =
            {
                datatype: "json",
                datafields: [
                    { name: "AllotTime", type: "date" },
                    { name: "AlloterName", type: "string" },
                    { name: "SumBala", type: "number" },
                    { name: "StatusName", type: "string" }
                ],
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "cia.AllotId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cia.AllotId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CashInContractOtherAllotListHandler.ashx?subId=" + "<%=this.curSub.SubId%>"
            };
            var otherDataAdapter = new $.jqx.dataAdapter(otherSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxOtherGrid").jqxGrid(
            {
                width: "98%",                
                source: otherDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "分配日期", datafield: "AllotTime", cellsformat: "yyyy-MM-dd" },
                  { text: "分配人", datafield: "AlloterName" },
                  { text: "分配金额", datafield: "SumBala" },
                  { text: "收款状态", datafield: "StatusName" }
                ]
            });

            var outCorpSource =
            {
                datatype: "json",
                datafields: [
                    { name: "CorpName", type: "string" },
                    { name: "CorpId", type: "int" }
                ],
                localdata: <%=this.JsonOutCorp%>
                };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource, { autoBind: true });

            var stockSource = 
                {
                    datatype: "json",
                    datafields: [
                        { name: "StockLogId", type: "int" },
                        { name: "Title", type: "string" },
                        { name: "Html", type: "string" }
                    ],
                    localdata: <%=this.JsonStock%>
                    };
            var stockDataAdapter = new $.jqx.dataAdapter(stockSource, { autoBind: true });

            //////////////////////金额分配//////////////////////

            ///收款分配///
            formatedData = "";
            totalrecords = 0;
            cashInSelectSource =
            {
                localdata: cashInDetails,
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "AllotBala", type: "number" },
                    { name: "StockLogId", type: "int"},
                    { name: "RefNo", type: "string" }
                ],
                datatype: "json"
            };
            var cashInSelectDataAdapter = new $.jqx.dataAdapter(cashInSelectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var cashInRemoveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntCashInRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxCashInSelectGrid").jqxGrid(
            {
                width: "98%",
                source: cashInSelectDataAdapter,
                autoheight: true,
                virtualmode: true,
                enabletooltips: true,
                sorttogglestates: 1,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InCorp"},
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "收款银行", datafield: "CashInBankName" },
                  { text: "收款金额", datafield: "CashInBala" },
                  { text: "分配业务单号", datafield: "RefNo" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "操作", datafield: "CashInId", cellsrenderer: cashInRemoveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            ///可选择收款分配///
            formatedData = "";
            totalrecords = 0;

            cashInAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "LastBala", type: "number" },
                    { name: "AllotBala", type: "number" },
                    { name: "StockLogId", type: "int"},
                    { name: "RefNo", type: "string" }
                ],
                sort: function () {
                    $("#jqxCashInAllotGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "ci.CashInId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ci.CashInId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CashInAllotLastListHandler.ashx?&currencyId=" + "<%=this.curSub.SettleCurrency%>"+"&subId="+"<%=this.curSub.SubId%>"
            };
            var cashInAllotDataAdapter = new $.jqx.dataAdapter(cashInAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var cashInAddRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntCashInAddOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxCashInAllotGrid").jqxGrid(
            {
                width: "98%",
                source: cashInAllotDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                selectionmode: "singlecell",
                sortable: true,
                editable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InCorp", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款银行", datafield: "CashInBankName", editable: false },
                  { text: "收款金额", datafield: "CashInBala", editable: false },
                  { text: "剩余金额", datafield: "LastBala", editable: false },
                  {
                      text: "分配业务单号", datafield: "StockLogId", displayfield: "RefNo", columntype: "combobox", cellclassname: "GridFillNumber", sortable: false,
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: stockDataAdapter, displayMember: "Html", valueMember: "StockLogId",autoDropDownHeight:true });
                      }
                  },
                  {
                      text: "分配金额", datafield: "AllotBala", columntype: "numberinput", cellclassname: "GridFillNumber", sortable: false, validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "分配金额不能大于剩余金额" };
                          }
                          return true;
                      }, sortable: false
                  },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "CashInId", cellsrenderer: cashInAddRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            //分配公司
            var allotCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curSub.SubId%>";
            var allotCorpSource = { datatype: "json", url: allotCorpUrl, async: false };
            var allotCorpDataAdapter = new $.jqx.dataAdapter(allotCorpSource);
            $("#selAllotCorp").jqxDropDownList({ source: allotCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase",selectedIndex:0 });

            //分配金额
            $("#txbAllotBala").jqxNumberInput({ height: 25, min: 0, max: 999999999, decimalDigits: 2, digits: 9, width: 140, spinButtons: true });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.PayMatter%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#selPayMatter").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 120, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnAdd").click(function () {
                if (cashInDetails.length > 0) {
                    var rows = $("#jqxCashInSelectGrid").jqxGrid("getrows");
                    
                    var detailStr = "";
                    for (j = 0 ; j < rows.length ; j++) {

                        var stockLogId = 0;
                        if(rows[j].StockLogId!=undefined && rows[j].StockLogId>0){ stockLogId = rows[j].StockLogId; }


                        detailStr += "{ StockLogId:" + stockLogId + ",AllotCorpId:"+ rows[j].AllotCorpId + ",AllotBala:"+ rows[j].AllotBala +",CashInId:"+ rows[j].CashInId +" }";
                        if (j < rows.length - 1) { detailStr += "|"; }
                    }

                    $.post("Handler/CashInAllotCreateHandler.ashx", {
                        Details: detailStr,
                        Memo: $("#txbMemo").val(),
                        SubId: "<%=this.curSub.SubId%>"
                    },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotList.aspx";
                        }
                    }
                   );
                }
                else { alert("未分配任何款项！"); }
            });

        });

        var currencyId=<%=this.curSub.SettleCurrency%>;

        function bntCashInRemoveOnClick(row) {
           
            var item = cashInDetails[row];

            if(item.DetailId != undefined && item.DetailId>0){
                //增加排除收款分配
                cashInExcs.push(item);
            }

            //删除收款登记列表
            cashInDetails.splice(row, 1);

            flushCashInGrid();
        }

        function bntCashInAddOnClick(row) {            

            var item = $("#jqxCashInAllotGrid").jqxGrid("getrowdata", row);           
            
            if(item.AllotCorpId == undefined || item.AllotCorpId ==0)
            {
                alert("必须选择收款分配到的公司");
                return;
            }
            
            if(item.CurrencyId != currencyId){
                alert("必须选择与合约相同币种的收款登记");
                return;
            }

            //添加收款登记列表
            cashInDetails.push(item);

            flushCashInGrid();
        }

        function flushCashInGrid() {

            cashInSelectSource.localdata = cashInDetails;
            $("#jqxCashInSelectGrid").jqxGrid("updatebounddata", "rows");

            var cashInIds = "";
            for (i = 0; i < cashInDetails.length; i++) {
                if (i != 0) { cashInIds += ","; }
                cashInIds += cashInDetails[i].CashInId;
            }

            cashInAllotSource.url = "Handler/CashInAllotLastListHandler.ashx?&currencyId=" + "<%=this.curSub.SettleCurrency%>"+"&cashInIds="+cashInIds+"&subId="+"<%=this.curSub.SubId%>";
            $("#jqxCashInAllotGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxOtherExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约其他分配情况
        </div>
        <div>
            <div id="jqxOtherGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxCashInSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择收款登记
        </div>
        <div>
            <div id="jqxCashInSelectGrid"></div>
        </div>
    </div>

    <div id="jqxCashInAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可分配收款登记
        </div>
        <div>
            <div id="jqxCashInAllotGrid"></div>
        </div>
    </div>

    <div id="jqxAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            收款分配信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>分配至公司：</strong>
                    <div style="float: left;" id="selAllotCorp"></div>
                </li>
                <li>
                    <strong>分配金额：</strong>
                    <div style="float: left" id="txbAllotBala"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;" />
                </li>
               <li>
                    <strong>收款事项：</strong>
                    <div style="float: left" id="selPayMatter"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <textarea id="txbMemo" style="height:120px;"></textarea><br />
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">        
        <input type="button" id="btnAdd" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="ContractReceivableList.aspx" id="btnCancel">取消</a>
    </div>

</body>
</html>
