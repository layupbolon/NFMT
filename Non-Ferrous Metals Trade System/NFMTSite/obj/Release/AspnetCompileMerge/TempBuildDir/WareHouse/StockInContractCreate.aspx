<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInContractCreate.aspx.cs" Inherits="NFMTSite.WareHouse.StockInContractCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>子合约出库申请分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        
        var selectSource;
        var contractSource;
        var subId =0;
        var rows = new Array();

        $(document).ready(function () {

            $("#jqxStockInExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxContractListExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;
            //选中合约
            selectSource =
            {
                datafields:
                [
                   { name: "ContractId", type: "int" },
                   { name: "SubId", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "SignAmount", type: "string" },
                   { name: "SignWeight", type: "string" },
                   { name: "SumWeight", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxSelectGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "cs.SubId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cs.SubId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.curContractJson%>
                };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
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
            $("#jqxSelectGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "订立日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                  { text: "合约号", datafield: "ContractNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "我方公司", datafield: "InCorpName" },
                  { text: "对方公司", datafield: "OutCorpName" },
                  { text: "交易品种", datafield: "AssetName" },
                  { text: "合约计量", datafield: "SignWeight" },
                  { text: "已入库计量", datafield: "SumWeight" },
                  { text: "操作", datafield: "SubId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            //采购合约列表
            formatedData = "";
            totalrecords = 0;
            contractSource =
            {
                datafields:
                [
                   { name: "ContractId", type: "int" },
                   { name: "SubId", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "SignAmount", type: "string" },
                   { name: "SignWeight", type: "string" },
                   { name: "SumWeight", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxContractGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "cs.SubId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cs.SubId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/StockInCanSelectContractListHandler.ashx?stockId="+"<%=this.curStockIn.StockInId%>"
            };
            var contractDataAdapter = new $.jqx.dataAdapter(contractSource, {
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
            $("#jqxContractGrid").jqxGrid(
            {
                width: "98%",
                source: contractDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "订立日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                  { text: "合约号", datafield: "ContractNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "我方公司", datafield: "InCorpName" },
                  { text: "对方公司", datafield: "OutCorpName" },
                  { text: "交易品种", datafield: "AssetName" },
                  { text: "合约计量", datafield: "SignWeight" },
                  { text: "已入库计量", datafield: "SumWeight" },
                  { text: "操作", datafield: "SubId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            //buttons
            $("#btnAdd").jqxButton({height:25,width:120});
            $("#btnCancel").jqxButton({ height: 25, width: 120 });

            $("#btnAdd").click(function () {

                if (subId == 0) { alert("未选中任何入库登记"); return; }
                if (!confirm("确认入库登记分配？")) { return; }

                $.post("Handler/StockInContractCreateHandler.ashx", { SubId:subId,StockInId:"<%=this.curStockIn.StockInId%>" },
                   function (result) {
                       var obj = JSON.parse(result);
                       alert(obj.Message);   
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "StockInContractList.aspx";          
                       }    
                   }
                );
            });
        });

        function bntRemoveOnClick(value) {

            subId=0;
            rows.splice(0, 1);
            flushGrid();
        }

        function bntAddOnClick(value, row) {           

            if(subId>0){ alert("入库登记仅能分配至一个子合约。"); return;}
            
            var item = $("#jqxContractGrid").jqxGrid("getrowdata", row);
            rows.push(item);
            subId = item.SubId;
            flushGrid();
        }

        function flushGrid() {
            selectSource.localdata = rows;
            $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");
            contractSource.url = "Handler/StockInCanSelectContractListHandler.ashx?id="+ subId+"&stockId="+"<%=this.curStockIn.StockInId%>";
            $("#jqxContractGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStockInExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            入库登记信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>业务单号：</strong>
                    <span><%=this.curStockIn.RefNo%></span>
                </li>
                <li>
                    <strong>报关状态：</strong>
                    <span><%=this.CustomTypeName%></span>
                </li>
                <li>
                    <strong>入账公司：</strong>
                    <span><%=this.CorpName%></span>
                </li>
                <li>
                    <strong>入库日期：</strong>
                    <span><%=this.curStockIn.StockInDate.ToShortDateString()%></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span><%=this.AssetName%></span>
                </li>
                <li>
                    <strong>毛重：</strong>
                    <span><%=this.curStockIn.GrossAmount.ToString()+ this.MUName%></span>
                </li>
                <li>
                    <strong>净重：</strong>
                    <span><%=this.curStockIn.NetAmount.ToString()+ this.MUName%></span>
                </li>
                <li>
                    <strong>捆数：</strong>
                    <span><%=this.curStockIn.Bundles%></span>
                </li>
                <li>
                    <strong>品牌：</strong>
                    <span><%=this.curBrandName%></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            选中合约
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxContractListExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            采购合约列表
        </div>
        <div>
            <div id="jqxContractGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAdd" value="提交" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
