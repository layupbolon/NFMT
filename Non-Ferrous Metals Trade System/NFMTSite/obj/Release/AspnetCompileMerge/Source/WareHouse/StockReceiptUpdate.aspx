<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReceiptUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.StockReceiptUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回执修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">

        var receiptingSource;
        var canReceiptSource;

        var details = new Array();
        var rows = new Array();

        $(document).ready(function () {
            $("#jqxReceiptedExpander").jqxExpander({ width: "98%" });
            $("#jqxReceiptingExpander").jqxExpander({ width: "98%" });
            $("#jqxCanReceiptExpander").jqxExpander({ width: "98%" });
            $("#jqxInfoExpander").jqxExpander({ width: "98%" });            

            //已回执库存列表
            var formatedData = "";
            var totalrecords = 0;
            var receiptedSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxReceiptedGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/StockReceiptedListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>"+"&ri="+"<%=this.curStockReceipt.ReceiptId%>"
            };
            var receiptedDataAdapter = new $.jqx.dataAdapter(receiptedSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxReceiptedGrid").jqxGrid(
            {
                width: "98%",
                source: receiptedDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },                  
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "库存净重", datafield: "StockWeight" },
                  { text: "回执净重", datafield: "ReceiptWeight" },
                  { text: "磅差净重", datafield: "MissAmount" },
                  { text: "磅差率", datafield: "MissRate" }
                ]
            });

            //选中库存列表
            formatedData = "";
            totalrecords = 0;
            receiptingSource =
            {
                datafields:
                [
                    { name: "StockLogId", type: "int" },
                    { name: "StockId", type: "int" },
                    { name: "StockDate", type: "date" },
                    { name: "RefNo", type: "string" },
                    { name: "NetAmount", type: "number" },
                    { name: "StockWeight", type: "string" },
                    { name: "CorpName", type: "string" },
                    { name: "AssetName", type: "string" },
                    { name: "BrandName", type: "string" },
                    { name: "StockStatus", type: "int" },
                    { name: "StockStatusName", type: "string" },
                    { name: "ReceiptAmount", type: "number" },
                    { name: "MissAmount", type: "number" },
                    { name: "MissRate", type: "string" }
                ],
                localdata: <%=this.curReceiptingJson%>,
                datatype: "json"
            };
            var receiptingDataAdapter = new $.jqx.dataAdapter(receiptingSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveOnClick(" + value + ");\" />"
                   + "</div>";
            }
            $("#jqxReceiptingGrid").jqxGrid(
            {
                width: "98%",
                source: receiptingDataAdapter,
                autoheight: true,
                virtualmode: true,               
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "库存净重", datafield: "StockWeight" },
                  { text: "回执净重", datafield: "ReceiptAmount" },
                  { text: "磅差净重", datafield: "MissAmount" },
                  { text: "磅差率", datafield: "MissRate" },
                  { text: "操作", datafield: "StockLogId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            rows = $("#jqxReceiptingGrid").jqxGrid("getrows");
            for(i=0;i<rows.length;i++){
                if(rows[i].StockLogId !=undefined){
                    details.push(rows[i].StockLogId);
                }
            }

            //可回执库存列表
            formatedData = "";
            totalrecords = 0;
            canReceiptSource =
            {
                datafields:
                [
                    { name: "StockLogId", type: "int" },
                    { name: "StockId", type: "int" },
                    { name: "StockDate", type: "date" },
                    { name: "RefNo", type: "string" },
                    { name: "NetAmount", type: "number" },
                    { name: "StockWeight", type: "string" },
                    { name: "CorpName", type: "string" },
                    { name: "AssetName", type: "string" },
                    { name: "BrandName", type: "string" },
                    { name: "StockStatus", type: "int" },
                    { name: "StockStatusName", type: "string" },
                    { name: "ReceiptAmount", type: "number" },
                    { name: "MissAmount", type: "number" },
                    { name: "MissRate", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxCanReceiptGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/StockCanReceiptListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>"
            };
            var canReceiptDataAdapter = new $.jqx.dataAdapter(canReceiptSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + value + "," + row + ");\" />"
                   + "</div>";
            }
            $("#jqxCanReceiptGrid").jqxGrid(
            {
                width: "98%",
                source: canReceiptDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                selectionmode: "singlecell",
                sortable: true,
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo",editable: false },
                  { text: "库存状态", datafield: "StockStatusName", editable: false },
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "库存净重", datafield: "StockWeight", editable: false },
                  {
                      text: "回执净重", datafield: "ReceiptAmount", sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber", validation: function (cell, value) {
                          return { result: value >= 0, message: "回执净重不能小于0" };
                      }, createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true, symbolPosition: "right", symbol: "<%=this.MUName%>" });
                      }
                  },
                  { text: "操作", datafield: "StockLogId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            $("#btnUpdate").click(function () {

                if (details.length == 0) { alert("未选中任何库存"); return; }

                if (!confirm("确认修改库存回执")) { return; }

                var stockReceipt = {
                    ReceiptId: "<%=this.curStockReceipt.ReceiptId%>",
                    ContractId: "<%=this.curContractSub.ContractId%>",
                    ContractSubId: "<%=this.curContractSub.SubId%>",
                    UnitId: "<%=this.curContractSub.UnitId%>",
                    Memo: $("#txbMemo").val(),                   
                    ReceiptDate: $("#txbReceiptDate").val()
                }

                var sids = "";                
                for (i = 0; i < rows.length; i++) {
                    var stockId =0;
                    var amout =0;
                    var bala =0;
                    var stockLogId =0;

                    if(rows[i].StockLogId != undefined){ stockLogId =rows[i].StockLogId; }
                    if(rows[i].StockId != undefined){ stockId =rows[i].StockId; }
                    if(rows[i].ReceiptAmount != undefined){ amout = rows[i].ReceiptAmount;}
                    if(rows[i].selBala != undefined){ bala = rows[i].selBala; }

                    sids += "{ StockLogId:" + stockLogId +   ",StockId:" + stockId + ",ReceiptAmount:" + amout +" }";
                    if (i < rows.length - 1) { sids += "|"; }
                }

                $.post("Handler/StockReceiptUpdateHandler.ashx", { ds: sids, sr: JSON.stringify(stockReceipt) },
                   function (result) {
                       var obj = JSON.parse(result);
                       alert(obj.Message);   
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "StockReceiptList.aspx";          
                       } 
                   }
               );

            });

            //Control Init
            $("#txbReceiptDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnUpdate").jqxInput();

            //set date
            var tempDate = new Date("<%=this.curStockReceipt.ReceiptDate.ToString("yyyy/MM/dd")%>");
            $("#txbReceiptDate").jqxDateTimeInput({ value: tempDate });
            $("#txbMemo").val("<%=this.curStockReceipt.Memo%>");

        });

        function bntRemoveOnClick(value) {

            var index = details.indexOf(value);
            details.splice(index, 1);
            rows.splice(index, 1);

            flushGrid();
        }

        function bntAddOnClick(value, row) {
            var item = $("#jqxCanReceiptGrid").jqxGrid("getrowdata", row);
            if (item.ReceiptAmount <= 0) { alert("回执净重必须大于0"); return;}
            
            var missAmout = parseFloat(item.ReceiptAmount) - parseFloat(item.NetAmount);
            item.MissAmount = Math.round(missAmout*10000)/10000;

            var missRate = Math.round((parseFloat(item.ReceiptAmount)/parseFloat(item.NetAmount) -1)*100*10000)/10000+"%";
            item.MissRate = missRate
            
            details.push(value);
            rows.push(item);
            
            flushGrid();
        }

        function flushGrid() {
            var ids = "";
            for (i = 0; i < details.length; i++) {                
                if (details[i] != undefined) {
                    if (i != 0) { ids += ","; }
                    ids += details[i];
                }
            }

            receiptingSource.localdata = rows;
            $("#jqxReceiptingGrid").jqxGrid("updatebounddata", "rows");
            canReceiptSource.url = "Handler/StockCanReceiptListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>" + "&sids=" + ids+"&ri="+"<%=this.curStockReceipt.ReceiptId%>";
            $("#jqxCanReceiptGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxReceiptedExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已回执的库存列表
        </div>
        <div>
            <div id="jqxReceiptedGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxReceiptingExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回执选中的库存列表
        </div>
        <div>
            <div id="jqxReceiptingGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxCanReceiptExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            未回执库存列表
        </div>
        <div>
            <div id="jqxCanReceiptGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回执信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="line-height: none; height: auto; float: none">
                    <strong>回执日期：</strong>
                    <div id="txbReceiptDate" style="float: left;"></div>
                </li>
                <li style="line-height: none; height: auto; float: none">
                    <strong>备注：</strong>
                    <textarea id="txbMemo"></textarea>
                </li>
                <li style="float: none">
                    <strong>&nbsp;</strong>
                    <input type="button" value="回执修改" id="btnUpdate" style="width: 120px; height: 25px;" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
