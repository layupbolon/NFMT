<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReceiptDetail.aspx.cs" Inherits="NFMTSite.WareHouse.StockReceiptDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回执明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curStockReceipt.DataBaseName%>" + "&t=" + "<%=this.curStockReceipt.TableName%>" + "&id=" + "<%=this.curStockReceipt.ReceiptId%>";

        var receiptingSource;
        var canReceiptSource;

        var details = new Array();
        var rows = new Array();

        $(document).ready(function () {
            $("#jqxReceiptedExpander").jqxExpander({ width: "98%" });
            $("#jqxReceiptingExpander").jqxExpander({ width: "98%" });
            $("#jqxInfoExpander").jqxExpander({ width: "98%" ,height:150 });            

            //已回执库存列表
            var formatedData = "";
            var totalrecords = 0;
            var receiptedSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxReceiptedGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxReceiptedGrid").jqxGrid("updatebounddata", "filter");
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
                  { text: "磅差率", datafield: "MissRate" }
                ]
            });
           
            //Control Init
            $("#txbReceiptDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120,disabled:true });
            $("#txbMemo").jqxInput({ height:70,width: 600,disabled:true });
            $("#txbMemo").val("<%=this.curStockReceipt.Memo%>");

            //set date
            var tempDate = new Date("<%=this.curStockReceipt.ReceiptDate.ToString("yyyy/MM/dd")%>");
            $("#txbReceiptDate").jqxDateTimeInput({ value: tempDate });

            //init button
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 42,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/StockReceiptStatusHandler.ashx", { si: "<%=this.curStockReceipt.ReceiptId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockReceiptList.aspx";          
                        }  
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/StockReceiptStatusHandler.ashx", { si: "<%=this.curStockReceipt.ReceiptId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockReceiptList.aspx";          
                        }  
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/StockReceiptStatusHandler.ashx", { si: "<%=this.curStockReceipt.ReceiptId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockReceiptList.aspx";          
                        }  
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("完成撤销后，所有已完成的明细将会更新至已生效，确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/StockReceiptStatusHandler.ashx", { si: "<%=this.curStockReceipt.ReceiptId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockReceiptList.aspx";          
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
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxReceiptedExpander" style="float: left; margin: 0px 5px 5px 5px; display: none;">
        <div>
            其他回执的库存列表
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

    <div id="jqxInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回执信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="float: none;">
                    <strong>回执日期：</strong>
                    <div id="txbReceiptDate" style="float: left;"></div>
                </li>
                <li style="float: none;">
                    <strong>备注：</strong>
                    <textarea id="txbMemo"></textarea>
                </li>
            </ul>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
