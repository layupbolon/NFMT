<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutCreate.aspx.cs" Inherits="NFMTSite.WareHouse.StockOutCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出库新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">

        var stockOutingSource;
        var selectedSource;

        $(document).ready(function () {
            $("#jqxStockOutApplyExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxStockOutedExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxSelectedExpander").jqxExpander({ width: "98%" });
            $("#jqxStockOutingExpander").jqxExpander({ width: "98%" });
            $("#jqxInfoExpander").jqxExpander({ width: "98%" });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnAudit").jqxButton();
            $("#btnCreateStockOut").jqxButton();

            //已分配列表
            var formatedData = "";
            var totalrecords = 0;
            var stockOutedSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxStockOutedGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sod.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sod.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/StockOutedListHandler.ashx?aid=" + "<%=this.StockOutApplyId%>"
            };
            var stockOutedDataAdapter = new $.jqx.dataAdapter(stockOutedSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxStockOutedGrid").jqxGrid(
            {
                width: "98%",
                source: stockOutedDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存净重", datafield: "CurNetAmount" },
                  { text: "出库净量", datafield: "NetAmount" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" }
                ]
            });

            //已选中列表
            formatedData = "";
            totalrecords = 0;
            selectedSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxSelectedGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/StockOutSelectedListHandler.ashx"
            };
            var selectedDataAdapter = new $.jqx.dataAdapter(selectedSource, {
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
            $("#jqxSelectedGrid").jqxGrid(
            {
                width: "98%",
                source: selectedDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存净重", datafield: "CurNetAmount" },
                  { text: "出库净重", datafield: "NetAmount" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "操作", datafield: "DetailId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            //可分配列表            
            formatedData = "";
            totalrecords = 0;
            stockOutingSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxStockOutingGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "soad.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "soad.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/StockOutingListHandler.ashx?aid=" + "<%=this.StockOutApplyId%>"
            };
            var stockOutingDataAdapter = new $.jqx.dataAdapter(stockOutingSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + value + ");\" />"
                   + "</div>";
            }
            $("#jqxStockOutingGrid").jqxGrid(
            {
                width: "98%",
                source: stockOutingDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存净量", datafield: "CurNetAmount" },
                  { text: "出库净量", datafield: "NetAmount" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "操作", datafield: "DetailId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });
                       

            function StockOutCreate(isAudit) {
                if (details.length == 0) { alert("未选中任何库存"); return; }

                if (!confirm("确认新增出库")) { return; }

                var sids = "";
                for (i = 0; i < details.length; i++) {
                    if (i != 0) { sids += ","; }
                    sids += details[i];
                }
                var memo = $("#txbMemo").val();

                var fileIds = new Array();

                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/StockOutCreateHandler.ashx", { sids: sids, outApplyId: "<%=this.StockOutApplyId%>", memo: memo },
                   function (result) {
                       var obj = JSON.parse(result);
                       if (obj.ResultStatus.toString() == "0") {
                           AjaxFileUpload(fileIds, obj.ReturnValue.StockOutId, AttachTypeEnum.StockOutAttach);
                       }                       
                       if (obj.ResultStatus.toString() == "0") {
                           if (isAudit) {
                               AutoSubmitAudit(MasterEnum.出库审核, JSON.stringify(obj.ReturnValue));
                           }
                       }
                       alert(obj.Message);
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "StockOutList.aspx";
                       }
                   }
               );
            }

            $("#btnCreateStockOut").click(function () { StockOutCreate(false); });
            $("#btnAudit").click(function () { StockOutCreate(true); });
        });

        var details = new Array();

        function bntRemoveOnClick(value) {

            var index = details.indexOf(value);
            details.splice(index, 1);
            flushGrid();
        }

        function bntAddOnClick(value) {

            details.push(value);
            flushGrid();
        }

        function flushGrid() {

            var sids = "";
            for (i = 0; i < details.length; i++) {
                if (i != 0) { sids += ","; }
                sids += details[i];
            }
            selectedSource.url = "Handler/StockOutSelectedListHandler.ashx?sids=" + sids;
            $("#jqxSelectedGrid").jqxGrid("updatebounddata", "rows");
            stockOutingSource.url = "Handler/StockOutingListHandler.ashx?aid=" + "<%=this.StockOutApplyId%>" + "&sids=" + sids;
            $("#jqxStockOutingGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxStockOutApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            出库申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请部门：</strong>
                    <span runat="server" id="spnApplyDept"></span>
                </li>
                <li>
                    <strong>申请人：</strong>
                    <span runat="server" id="spnApplier"></span>
                </li>
                <li>
                    <strong>申请时间：</strong>
                    <span runat="server" id="spnApplyDate"></span>
                </li>
                <li>
                    <strong>申请附言：</strong>
                    <span runat="server" id="spnApplyMemo"></span>
                </li>
                <li>
                    <strong>审核明细：</strong>
                    <a href="#">查看</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxStockOutedExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已出库列表
        </div>
        <div>
            <div id="jqxStockOutedGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxSelectedExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            选中库存列表
        </div>
        <div>
            <div id="jqxSelectedGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxStockOutingExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            未出库列表
        </div>
        <div>
            <div id="jqxStockOutingGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Create" AttachType="StockOutAttach" />

    <div id="jqxInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            出库信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="line-height: none; height: auto; float: none">
                    <strong>申请备注：</strong>
                    <textarea id="txbMemo"></textarea>
                </li>
                <li style="float: none">
                    <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>
                    <input type="button" value="新增并提交审核" id="btnAudit" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" value="出库新增" id="btnCreateStockOut" style="width: 120px; height: 25px;" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
