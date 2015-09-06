<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutApplyUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.StockOutApplyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出库申请修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var allotSource;
        var sellSource;
        var selectedStocks= new Array();
        var removeDetails = new Array();

        $(document).ready(function () {
            $("#jqxAllotStockListExpander").jqxExpander({ width: "98%" });
            $("#jqxStockListExpander").jqxExpander({ width: "98%" });           
            
            var formatedData = "";
            var totalrecords = 0;

            //已配列表
            allotSource =
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
                localdata: <%=this.JsonStr%>
                };
            var allotDataAdapter = new $.jqx.dataAdapter(allotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var allotCellsRenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\"><input type=\"button\" onclick=\"bntRemoveOnClick(" + row + ");\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" /></div>";
            }
            
            $("#jqxAllotStockListGrid").jqxGrid(
            {
                width: "98%",
                source: allotDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,                
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "库存重量", datafield: "CurNetAmount" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "捆数", datafield: "Bundles"},
                  { text: "申请重量", datafield: "NetAmount" },
                  { text: "操作", cellsrenderer: allotCellsRenderer, width: 100, sortable: false }
                ]
            });

            selectedStocks = $("#jqxAllotStockListGrid").jqxGrid("getrows");

            //可售库存列表
            var sellUrl = "Handler/StockOutApplyNotAllotListHandler.ashx?cid=" + "<%= this.curSub.ContractId%>" + "&applyId=" + "<%=this.curStockOutApply.StockOutApplyId%>";
            formatedData = "";
            totalrecords = 0;
            sellSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxStockListGrid").jqxGrid("updatebounddata", "sort");
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
                url: sellUrl
            };
            var sellDataAdapter = new $.jqx.dataAdapter(sellSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; \"><input type=\"button\" onclick=\"bntCreateOnClick(" + row + ");\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"分配\" /></div>";
            }
            $("#jqxStockListGrid").jqxGrid(
            {
                width: "98%",
                source: sellDataAdapter,
                pageable: true,
                autoheight: true,
                editable:true,
                selectionmode: "singlecell",
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo",editable:false },       
                  { text: "库存状态", datafield: "StatusName", editable: false },
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "交货地", datafield: "DPName", editable: false },
                  { text: "卡号", datafield: "CardNo", editable: false },
                  { text: "库存重量", datafield: "CurNetAmount", editable: false },
                  { text: "剩余重量", datafield: "LastAmount", editable: false },
                  { text: "重量单位", datafield: "MUName", editable: false },
                  {
                      text: "捆数", datafield: "Bundles", columntype: "numberinput", cellclassname: "GridFillNumber", sortable: false,
                      validation: function (cell, value) {
                          var item = sellDataAdapter.records[cell.row];
                          if (value <= 0 || value > item.LaveBundles) {
                              return { result: false, message: "申请捆数不能小于0且大于剩余捆数" + item.LaveBundles };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 1, decimalDigits: 0, Digits: 6, spinButtons: true });
                      }
                  },
                  {
                      text: "申请重量", datafield: "NetAmount", columntype: "numberinput",cellclassname :"GridFillNumber", sortable: false, validation: function (cell, value) {
                          var item = $("#jqxStockListGrid").jqxGrid("getrowdata", cell.row);
                          if (value < 0 || value > item.LastAmount) {
                              return { result: false, message: "申请重量不能小于0且大于剩余重量" + item.LastAmount };
                          }
                          return true;
                      }, createeditor: function (row, cellvalue, editor) {
                          var r = $("#jqxStockListGrid").jqxGrid("getrowdata", row);
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 120, Digits: 8, spinButtons: true, symbolPosition: "right", symbol: r.MUName });
                      }
                  },
                  { text: "操作", datafield: "StockId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            //查询
            $("#txbRefNo").jqxInput({ height:25 });
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId="+"<%=this.curSub.SubId%>";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selOwnCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });
            $("#selApplyCorp").val(<%=this.curApply.ApplyCorp%>);

            $("#btnSearchStcok").jqxButton({ width: 80 });

            //申请信息
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnUpdateStockOutApply").jqxInput();
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25,disabled:true });
            $("#ddlApplyDeptId").val(<%=this.curApply.ApplyDept%>);

            //收货公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curSub.SubId%>";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selBuyCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase"
            });
            $("#selBuyCorp").val(<%=this.curStockOutApply.BuyCorpId%>);

            $("#btnUpdateStockOutApply").click(function () {
                if ($("#selApplyCorp").val() == 0) { alert("必须选择申请公司"); return; }
                if (selectedStocks.length == 0) { alert("未选中任何库存"); return; }

                if (!confirm("确认修改出库申请")) { return; }              

                var deptId = $("#ddlApplyDeptId").val();
                var memo = $("#txbMemo").val();
                var corpId = $("#selApplyCorp").val();
                var buyCorpId = $("#selBuyCorp").val();

                $.post("Handler/StockOutApplyUpdateHandler.ashx", { detail: JSON.stringify(selectedStocks), outApplyId: "<%=this.curStockOutApply.StockOutApplyId%>", deptId: deptId, memo: memo, corpId: corpId, buyCorpId: buyCorpId },
                   function (result) {
                       var obj = JSON.parse(result);
                       alert(obj.Message);
                       if (obj.ResultStatus.toString() == "0") {
                           document.location.href = "StockOutApplyList.aspx";
                       }
                   });
            });

        });

        function bntCreateOnClick(row) {
            var item = $("#jqxStockListGrid").jqxGrid("getrowdata", row);

            if (item.NetAmount == undefined || item.NetAmount == 0) { alert("申请重量必须大于0"); return; }
            selectedStocks.push(item);

            FlushGrid();
        }

        function bntRemoveOnClick(row) {
            var item = $("#jqxAllotStockListGrid").jqxGrid("getrowdata", row);
            selectedStocks.splice(row, 1);
            if(item.DetailId!=undefined && item.DetailId>0){
                removeDetails.push(item);
            }

            FlushGrid();
        }

        function FlushGrid() {

            var sids = "";
            for (i = 0; i < selectedStocks.length; i++) {
                if (i != 0) { sids += ","; }
                sids += selectedStocks[i].StockId;
            }

            var dids ="";
            for(i=0;i<removeDetails.length;i++){
                if(i!=0){ dids +=",";}
                dids+=removeDetails[i].DetailId;
            }

            var refNo = $("#txbRefNo").val();
            var ownCorpId = $("#selOwnCorp").val();

            sellSource.url = "Handler/StockOutApplyNotAllotListHandler.ashx?sids=" + sids + "&cid=" + "<%= this.curSub.ContractId%>" + "&dids=" + dids + "&refNo=" + refNo + "&ownCorpId=" +  ownCorpId;
            $("#jqxStockListGrid").jqxGrid("updatebounddata", "rows");
            allotSource.localdata = selectedStocks;
            $("#jqxAllotStockListGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxAllotStockListExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已配库存列表
        </div>
        <div>
            <div id="jqxAllotStockListGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxStockListExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可售库存列表
        </div>
        <div>
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span>业务单号：</span>
                        <span>
                            <input type="text" id="txbRefNo" /></span>
                    </li>
                    <li>
                        <span style="float: left;" >归属公司：</span>
                        <div style="float: left;" id="selOwnCorp"></div>
                    </li>
                    <li>
                        <input type="button" id="btnSearchStcok" value="查询" onclick="javascript:FlushGrid();" />
                    </li>
                </ul>
            </div>
            <div id="jqxStockListGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                 <li style="float: none;">
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="selApplyCorp"></div>
                </li>
                <li style="float: none;">
                    <strong>收货公司：</strong>
                    <div style="float: left;" id="selBuyCorp"></div>
                </li>
                <li style="float: none;">
                    <strong>申请部门：</strong>
                    <div style="float: left;" id="ddlApplyDeptId"></div>
                </li>
                <li style="line-height: none; height: auto; float: none">
                    <strong>申请备注：</strong>
                    <textarea id="txbMemo" runat="server"></textarea>
                </li>
                <li style="float: none">
                    <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>
                    <input type="button" value="更新出库申请" id="btnUpdateStockOutApply" style="width: 120px; height: 25px;" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
