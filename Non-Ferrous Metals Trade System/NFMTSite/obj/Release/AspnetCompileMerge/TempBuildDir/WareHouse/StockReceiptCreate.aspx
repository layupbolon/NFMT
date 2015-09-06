<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReceiptCreate.aspx.cs" Inherits="NFMTSite.WareHouse.StockReceiptCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回执新增</title>
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

        var salereceiptingSource;
        var salecanReceiptSource;

        var details = new Array();
        var rows = new Array();

        var saledetails = new Array();
        var salerows = new Array();

        $(document).ready(function () {
            $("#jqxReceiptedExpander").jqxExpander({ width: "98%" , expanded: false});
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
                url: "Handler/StockReceiptedListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>"
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
                    { name: "ContractId", type: "int" },
                    { name: "ContractSubId", type: "int" },
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

            //可回执库存列表
            formatedData = "";
            totalrecords = 0;
            canReceiptSource =
            {
                datafields:
                [
                    { name: "StockLogId", type: "int" },
                    { name: "StockId", type: "int" },
                    { name: "ContractId", type: "int" },
                    { name: "ContractSubId", type: "int" },
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
                selectionmode: "singlecell",
                sorttogglestates: 1,
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
            

            <%if(this.curContract.TradeDirection==(int)NFMT.Contract.TradeDirectionEnum.采购) {%>
            $("#jqxSaleReceiptExpander").jqxExpander({ width: "98%" });
            $("#jqxSaleCanReceiptExpander").jqxExpander({ width: "98%" });

            /**************销售选中库存列表**************/
            formatedData = "";
            totalrecords = 0;
            salereceiptingSource =
            {
                datafields:
                [
                    { name: "StockLogId", type: "int" },
                    { name: "StockId", type: "int" },
                    { name: "ContractId", type: "int" },
                    { name: "ContractSubId", type: "int" },
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
            var salereceiptingDataAdapter = new $.jqx.dataAdapter(salereceiptingSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var saleremoveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntSaleRemoveOnClick(" + value + ");\" />"
                   + "</div>";
            }
            $("#jqxSaleReceiptGrid").jqxGrid(
            {
                width: "98%",
                source: salereceiptingDataAdapter,
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
                  { text: "操作", datafield: "StockLogId", cellsrenderer: saleremoveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            salerows = $("#jqxSaleReceiptGrid").jqxGrid("getrows");

            /**************可回执库存列表**************/
            formatedData = "";
            totalrecords = 0;
            salecanReceiptSource =
            {
                datafields:
                [
                    { name: "StockLogId", type: "int" },
                    { name: "StockId", type: "int" },
                    { name: "ContractId", type: "int" },
                    { name: "ContractSubId", type: "int" },
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
                    $("#jqxSaleCanReceiptGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/StockSaleCanReceiptListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>"
            };
            var salecanReceiptDataAdapter = new $.jqx.dataAdapter(salecanReceiptSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var saleaddRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntSaleAddOnClick(" + value + "," + row + ");\" />"
                   + "</div>";
            }
            $("#jqxSaleCanReceiptGrid").jqxGrid(
            {
                width: "98%",
                source: salecanReceiptDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                selectionmode: "singlecell",
                sorttogglestates: 1,
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
                  { text: "操作", datafield: "StockLogId", cellsrenderer: saleaddRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            <%} %>

            //Control Init
            $("#txbReceiptDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnCreateStockReceipt").jqxButton({ width:120, height:25 });
            $("#btnAudit").jqxButton({ width:120, height:25 });

            function StockReceiptCreate(isAudit) {
                if (details.length == 0) { alert("未选中任何库存"); return; }

                if (!confirm("确认新增库存回执")) { return; }

                var stockReceipt = {
                    ContractId: "<%=this.curContractSub.ContractId%>",
                    ContractSubId: "<%=this.curContractSub.SubId%>",
                    UnitId: "<%=this.curContractSub.UnitId%>",
                    Memo: $("#txbMemo").val(),
                    OutCorpId: $("#selOutCorp").val(),
                    InCorpId: $("#selInCorp").val(),
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

                var salerows = "";
                <%if(this.curContract.TradeDirection==(int)NFMT.Contract.TradeDirectionEnum.采购) {%>
                salerows = $("#jqxSaleReceiptGrid").jqxGrid("getrows");
                <%} %>

                $.post("Handler/StockReceiptCreateHandler.ashx", { ds: sids, sr: JSON.stringify(stockReceipt),saleRows:salerows===""? "":JSON.stringify(salerows),isAudit:isAudit },
                   function (result) {
                       var obj = JSON.parse(result);
                       if (obj.ResultStatus.toString() == "0") {
                           if (isAudit) {
                               AutoSubmitAudit(MasterEnum.库存净重回执审核, obj.Message);
                               if(salerows!="")
                                   AutoSubmitAudit(MasterEnum.库存净重回执审核, JSON.stringify(obj.ReturnValue));
                           }
                       }                       
                       if (obj.ResultStatus.toString() == "0") {
                           alert("仓库回执新增成功");
                           document.location.href = "StockReceiptList.aspx";          
                       } 
                   }
               );
            }

            $("#btnCreateStockReceipt").click(function () { StockReceiptCreate(false); });
            $("#btnAudit").click(function () { StockReceiptCreate(true); });
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
            canReceiptSource.url = "Handler/StockCanReceiptListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>" + "&sids=" + ids;
            $("#jqxCanReceiptGrid").jqxGrid("updatebounddata", "rows");
        }

        <%if(this.curContract.TradeDirection==(int)NFMT.Contract.TradeDirectionEnum.采购) {%>
        function bntSaleRemoveOnClick(value) {

            var index = saledetails.indexOf(value);
            saledetails.splice(index, 1);
            salerows.splice(index, 1);

            SaleflushGrid();
        }

        function bntSaleAddOnClick(value, row) {
            var item = $("#jqxSaleCanReceiptGrid").jqxGrid("getrowdata", row);
            if (item.ReceiptAmount <= 0) { alert("回执净重必须大于0"); return;}

            var missAmout = parseFloat(item.ReceiptAmount) - parseFloat(item.NetAmount);
            item.MissAmount = Math.round(missAmout*10000)/10000;
            var missRate = Math.round((parseFloat(item.ReceiptAmount)/parseFloat(item.NetAmount) -1)*100*10000)/10000+"%";
            item.MissRate = missRate

            saledetails.push(value);
            salerows.push(item);

            SaleflushGrid();
        }

        function SaleflushGrid() {
            var ids = "";
            for (i = 0; i < saledetails.length; i++) {                
                if (saledetails[i] != undefined) {
                    if (i != 0) { ids += ","; }
                    ids += saledetails[i];
                }
            }

            salereceiptingSource.localdata = salerows;
            $("#jqxSaleReceiptGrid").jqxGrid("updatebounddata", "rows");
            salecanReceiptSource.url = "Handler/StockSaleCanReceiptListHandler.ashx?si=" + "<%=this.curContractSub.SubId%>" + "&sids=" + ids;
            $("#jqxSaleCanReceiptGrid").jqxGrid("updatebounddata", "rows");
        }
        <%} %>

    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />
    <nfmt:contractexpander runat="server" id="contractExpander1" />

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
            选中的库存列表
        </div>
        <div>
            <div id="jqxReceiptingGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxCanReceiptExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存流水列表
        </div>
        <div>
            <div id="jqxCanReceiptGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <%if(this.curContract.TradeDirection==(int)NFMT.Contract.TradeDirectionEnum.采购) {%>
    <div id="jqxSaleReceiptExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            销售合约选中的库存列表
        </div>
        <div>
            <div id="jqxSaleReceiptGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxSaleCanReceiptExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            销售合约库存流水列表
        </div>
        <div>
            <div id="jqxSaleCanReceiptGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>
    <%} %>

    <div id="jqxInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回执信息
        </div>
        <div class="LineDiv">
            <ul>
                <li>
                    <strong style="float: left;">回执日期：</strong>
                    <div id="txbReceiptDate" style="float: left;"></div>
                </li>
                <li>
                    <strong style="float: left;">备注：</strong>
                    <textarea id="txbMemo" style="float: left;"></textarea>
                </li>
                <li>
                    <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>
                    <input style="width: 120px; height: 25px;" type="button" value="添加并提交审核" id="btnAudit" />
                    <input style="width: 120px; height: 25px;" type="button" value="回执新增" id="btnCreateStockReceipt" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
