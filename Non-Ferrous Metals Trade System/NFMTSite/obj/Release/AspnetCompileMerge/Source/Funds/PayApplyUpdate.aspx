﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyUpdate.aspx.cs" Inherits="NFMTSite.Funds.PayApplyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<%@ Register TagName="jqxAttach" TagPrefix="NFMT" Src="~/Control/jqxAttach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <style type="text/css">
        .TabDiv ul {
            list-style-type: none;
        }

            .TabDiv ul li {
                display:inline-block;
                /*float: left;*/
                margin-left:5px;
                /*padding-right:5px;*/
            }
    </style>

    <script type="text/javascript">

        var stockLogsSource = null;
        var stockAppSource = null;

        var stockAppDataAdapter = null;
        var stockLogsDataAdapter = null;

        var stockApps = new Array();
        var stockRefs = new Array();

        var tabSelectedIndex = -1;

        var purchaseAttach_hasFile = false;
        var purchaseContractAttach_hasFile = false;
        var libraryBillAttach_hasFile = false;
        var wayBillAttach_hasFile = false;
        var invoiceScannAttach_hasFile = false;
        var purchaseContractAttach2_hasFile = false;
        var libraryBillAttach2_hasFile = false;
        var wayBillAttach2_hasFile = false;
        var purchaseAttach2_hasFile = false;
        var doPriceConfirmAttach_hasFile = false;
        var contractStatementAttach_hasFile = false;
        var purchaseAttach3_hasFile = false;
        var invoiceScannAttach2_hasFile = false;
        var costBreakdownListAttach_hasFile = false;
        var uploadUrl = "<%=string.Format("{0}Files/Handler/FileUpLoadHandler.ashx?",NFMT.Common.DefaultValue.NfmtSiteName)%>";

        var purchaseAttach_needUpdate = false;
        var purchaseContractAttach_needUpdate = false;
        var libraryBillAttach_needUpdate = false;
        var wayBillAttach_needUpdate = false;
        var invoiceScannAttach_needUpdate = false;
        var purchaseContractAttach2_needUpdate = false;
        var libraryBillAttach2_needUpdate = false;
        var wayBillAttach2_needUpdate = false;
        var purchaseAttach2_needUpdate = false;
        var doPriceConfirmAttach_needUpdate = false;
        var contractStatementAttach_needUpdate = false;
        var purchaseAttach3_needUpdate = false;
        var invoiceScannAttach2_needUpdate = false;
        var costBreakdownListAttach_needUpdate = false;
        
        $(document).ready(function () {

            $("#jqxStockAppsExpander").jqxExpander({ width: "98%" });
            $("#jqxStockLogsExpander").jqxExpander({ width: "98%" });
            $("#jqxPayApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxAttachExpander").jqxExpander({ width: "98%" });
            $("#jqxAttachExpander").hide();
            $("#jqxAttachGridExpander").jqxExpander({ width: "98%" });
        });

        function ExpanderHandle(index) {
            $("#jqxAttachExpander").show();
            tabSelectedIndex = index;
            $("#jqxTabs").jqxTabs("enableAt", index);
            $("#jqxTabs").jqxTabs("select", index);
            $("#jqxTabs").jqxTabs("disable");
        }

        function bntCreateOnClick(row) {
            var item = stockLogsDataAdapter.records[row];
            if (item.ApplyBala == undefined || item.ApplyBala == 0) { alert("申请金额必须大于0"); return; }

            stockApps.push(item);
            FlushGrid();

            $("#txbApplyBala").val($("#txbApplyBala").val() + item.ApplyBala);
        }

        function bntRemoveOnClick(row) {
            var item = stockAppDataAdapter.records[row];
            stockApps.splice(row, 1);

            if(item.RefId != undefined && item.RefId>0){
                stockRefs.push(item);
            }
            FlushGrid();

            $("#txbApplyBala").val($("#txbApplyBala").val() - item.ApplyBala);
        }

        function FlushGrid() {
            var logIds = "";
            for (i = 0; i < stockApps.length; i++) {
                if (i != 0) { logIds += ","; }
                logIds += stockApps[i].StockLogId;
            }

            var refIds ="";
            for (i = 0; i < stockRefs.length; i++) {
                if (i != 0) { refIds += ","; }
                refIds += stockRefs[i].RefId;
            }            

            stockAppSource.localdata = stockApps;
            $("#jqxStockAppsGrid").jqxGrid("updatebounddata", "rows");

            stockLogsSource.url = "Handler/StockLogsHandler.ashx?id=" + "<%= this.curSub.SubId%>" + "&logIds=" + logIds+"&refIds="+refIds;
            $("#jqxStockLogsGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>

    <NFMT:Navigation runat="server" ID="navigation1" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxStockAppsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择付款库存
        </div>
        <div>
            <div id="jqxStockAppsGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxStockLogsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可申请付款库存
        </div>
        <div>            
            <div id="jqxStockLogsGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxPayApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请日期：</strong>
                    <div id="txbApplyDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div id="selApplyDept" style="float: left;" />
                </li>
                <li>
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="selApplyCorp"></div>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <div id="selRecCorp" style="float: left;" />
                </li>
                <li>
                    <strong>收款人全称：</strong>
                    <span runat="server" id="spnRecCorpFullName"></span>
                </li>
                <li>
                    <strong>收款开户行：</strong>
                    <div id="selBank" style="float: left;" />
                </li>
                <li>
                    <strong>收款账号：</strong>
                    <div id="selBankAccount" style="float: left;" />
                </li>
                <li>
                    <input type="text" id="txbBankAccount" />
                </li>
                <li>
                    <strong>申请金额：</strong>
                    <div style="float: left" id="txbApplyBala"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;" />
                </li>
                <li>
                    <strong>最后付款日：</strong>
                    <div id="txbPayDeadline" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款事项：</strong>
                    <div style="float: left" id="selPayMatter"></div>
                </li>
                <li>
                    <strong>付款方式：</strong>
                    <div style="float: left" id="selPayMode"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" />
                </li>
                <li>
                    <strong>特殊附言：</strong>
                    <input type="text" id="txbSpecialDesc" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxAttachGridExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>附件信息</div>
        <div>
            <div id="jqxAttachGrid"></div>
        </div>
    </div>

    <div id="jqxAttachExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            附件信息
        </div>
        <div>
            <div id="jqxTabs">
                <ul>
                    <li>背靠背</li>
                    <li>货物质押采购首次不带票</li>
                    <li>货物质押采购带票</li>
                    <li>库存商品采购</li>
                    <li>结算尾款</li>
                    <li>预付客户保证金</li>
                    <li>营业费用</li>
                </ul>
                <%--背靠背--%>
                <div class="TabDiv" id="BTBdiv">
                    <ul>
                        <li>
                            <strong>收款合同号：</strong>
                            <input type="text" id="txbContractNo"/>
                        </li>
                        <li>
                            <strong>采购双签合同：</strong>
                            <NFMT:jqxAttach runat="server" ID="purchaseAttach" AttachId="purchaseAttach" style="float:right;"/>
                        </li>
                    </ul>
                </div>
                <%--货物质押采购首次不带票--%>
                <div class="TabDiv">
                    <ul>
                        <li>
                            <strong>采购合同：</strong>
                            <NFMT:jqxAttach runat="server" ID="purchaseContractAttach" AttachId="purchaseContractAttach" style="float:right;"/>
                        </li>
                        <li>
                            <strong>在库提单：</strong>
                            <NFMT:jqxAttach runat="server" ID="libraryBillAttach" AttachId="libraryBillAttach" style="float:right;"/>
                        </li>
                        <li>
                            <strong>在途提单：</strong>
                            <NFMT:jqxAttach runat="server" ID="wayBillAttach" AttachId="wayBillAttach" style="float:right;"/>
                        </li>
                    </ul>
                </div>
                <%--货物质押采购带票--%>
                <div class="TabDiv">
                    <ul>
                        <li>
                            <strong>发票扫描件：</strong>
                            <NFMT:jqxAttach runat="server" ID="invoiceScannAttach" AttachId="invoiceScannAttach" style="float:right;"/>
                        </li>
                        <li>
                            <strong>采购合同：</strong>
                            <NFMT:jqxAttach runat="server" ID="purchaseContractAttach2" AttachId="purchaseContractAttach2" style="float:right;"/>
                        </li>
                        <li>
                            <strong>在库提单：</strong>
                            <NFMT:jqxAttach runat="server" ID="libraryBillAttach2" AttachId="libraryBillAttach2" style="float:right;"/>
                        </li>
                        <li>
                            <strong>在途提单：</strong>
                            <NFMT:jqxAttach runat="server" ID="wayBillAttach2" AttachId="wayBillAttach2" style="float:right;"/>
                        </li>
                    </ul>
                </div>
                <%--库存商品采购--%>
                <div class="TabDiv">
                    <ul>
                        <li>
                            <strong>采购双签合同：</strong>
                            <NFMT:jqxAttach runat="server" ID="purchaseAttach2" AttachId="purchaseAttach2" style="float:right;"/>
                        </li>
                    </ul>
                </div>
                <%--结算尾款--%>
                <div class="TabDiv">
                    <ul>
                        <li>
                            <strong>点价确认单：</strong>
                            <NFMT:jqxAttach runat="server" ID="doPriceConfirmAttach" AttachId="doPriceConfirmAttach" style="float:right;"/>
                        </li>
                        <li>
                            <strong>合同结算单：</strong>
                            <NFMT:jqxAttach runat="server" ID="contractStatementAttach" AttachId="contractStatementAttach" style="float:right;"/>
                        </li>
                    </ul>
                </div>
                <%--预付客户保证金--%>
                <div class="TabDiv" id="PreDiv">
                    <ul>
                        <li>
                            <strong>采购双签合同：</strong>
                            <NFMT:jqxAttach runat="server" ID="purchaseAttach3" AttachId="purchaseAttach3" style="float:right;"/>
                        </li>
                        <li>
                            <strong>相应支付比例：</strong>
                            <div id="nbPaymentRatio" style="float:right;"></div>
                        </li>
                    </ul>
                </div>
                <%--营业费用--%>
                <div class="TabDiv">
                    <ul>
                        <li>
                            <strong>发票扫描件：</strong>
                            <NFMT:jqxAttach runat="server" ID="invoiceScannAttach2" AttachId="invoiceScannAttach2" style="float:right;"/>
                        </li>
                        <li>
                            <strong>费用明细清单：</strong>
                            <NFMT:jqxAttach runat="server" ID="costBreakdownListAttach" AttachId="costBreakdownListAttach" style="float:right;"/>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //附件信息
        $("#jqxTabs").jqxTabs({ width: "99%", position: "top", height: 180 ,disabled:true});

        //////////////////////////背靠背//////////////////////////
        //收款合同号
        $("#txbContractNo").jqxInput({ height: 25, width: 230 });

        //////////////////////////预付客户保证金//////////////////////////
        //相应支付比例
        $("#nbPaymentRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, digits: 2, width: 140, spinButtons: true, symbolPosition: 'right', symbol: '%' });
    </script>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="修改付款申请" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />
    </div>
</body>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#purchaseAttach").jqxFileUpload({ disabled: true });
            $("#purchaseContractAttach").jqxFileUpload({ disabled: true });
            $("#libraryBillAttach").jqxFileUpload({ disabled: true });
            $("#wayBillAttach").jqxFileUpload({ disabled: true });
            $("#invoiceScannAttach").jqxFileUpload({ disabled: true });
            $("#purchaseContractAttach2").jqxFileUpload({ disabled: true });
            $("#libraryBillAttach2").jqxFileUpload({ disabled: true });
            $("#wayBillAttach2").jqxFileUpload({ disabled: true });
            $("#purchaseAttach2").jqxFileUpload({ disabled: true });
            $("#doPriceConfirmAttach").jqxFileUpload({ disabled: true });
            $("#contractStatementAttach").jqxFileUpload({ disabled: true });
            $("#purchaseAttach3").jqxFileUpload({ disabled: true });
            $("#invoiceScannAttach2").jqxFileUpload({ disabled: true });
            $("#costBreakdownListAttach").jqxFileUpload({ disabled: true });

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
                localdata: <%=this.StockDetailsJson%>,
                datatype: "json"
            };
            stockAppDataAdapter = new $.jqx.dataAdapter(stockAppSource, {
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
                  { text: "申请金额", datafield: "ApplyBala",
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
                  { text: "操作", datafield: "StockLogId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            stockApps = stockAppDataAdapter.records;

            //可选库存流水信息
            formatedData = "";
            totalrecords = 0;
            var stockLogsUrl = "Handler/StockLogsHandler.ashx?id=" + "<%= this.curSub.SubId%>";
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
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnCreate\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntCreateOnClick(" + row + ");\" />"
                   + "</div>";
            }
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
                      text: "申请金额", datafield: "ApplyBala", columntype: "numberinput", cellclassname: "GridFillNumber", sortable: false,
                      validation: function (cell, value) {
                          if (value < 0) {
                              return { result: false, message: "申请金额不能小于0" };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "操作", datafield: "RefId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            var attachUrl = "Handler/PayApplyAttachHandler.ashx?id=<%=this.curPayApply.PayApplyId%>";

            var attachFormatedData = "";
            var attachTotalrecords = 0;
            attachSource =
            {
                url: attachUrl,
                datafields: [
                    { name: "PayApplyAttachId", type: "int" },
                    { name: "PayApplyId", type: "int" },
                    { name: "AttachId", type: "int" },
                    { name: "AttachType", type: "int" },
                    { name: "DetailName", type: "string" },
                    { name: "AttachName", type: "string" },
                    { name: "AttachInfo", type: "string" },
                    { name: "CreateTime", type: "date" },
                    { name: "AttachPath", type: "string" },
                    { name: "AttachExt", type: "string" },
                    { name: "AttachStatus", type: "int" },
                    { name: "ServerAttachName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxAttachGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    attachTotalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "pa.PayApplyAttachId",
                sortdirection: "asc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pa.PayApplyAttachId";
                    data.sortorder = data.sortorder || "asc";
                    data.filterscount = data.filterscount || 0;
                    attachFormatedData = buildQueryString(data);
                    return attachFormatedData;
                }
            };

            var attachDataAdapter = new $.jqx.dataAdapter(attachSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var attachViewRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">";
                cellHtml += "<a href=\"../Files/FileDownLoad.aspx?id=" + item.AttachId + "\" title=\"" + item.AttachName + "\" >下载</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxAttachGrid").jqxGrid(
            {
                width: "98%",
                source: attachDataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "附件类型", datafield: "DetailName" },
                  { text: "添加日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "附件名字", datafield: "AttachName" },
                  { text: "查看", datafield: "AttachPath", cellsrenderer: attachViewRender },
                  {
                      text: "操作", datafield: "Edit", width: 70, columntype: "button", width: 90, cellsrenderer: function () {
                          return "删除";
                      }, buttonclick: function (row) {
                          if (!confirm("确认删除？")) { return; }
                          var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                          if (item.AttachId == "undefined" || item.AttachId <= 0) return;
                          $.post("../Files/Handler/AttachUpdateStatusHandler.ashx", { aid: item.AttachId, s: statusEnum.已作废 },
                                  function (result) {
                                      attachSource.url = attachUrl;
                                      $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");

                                      if (item.AttachType == AttachTypeEnum.PurchaseDoubleContractAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.背靠背%>") {
                                          $("#purchaseAttach").jqxFileUpload({ disabled: false });
                                          purchaseAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.PurchaseContractAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购首次不带票%>") {
                                          $("#purchaseContractAttach").jqxFileUpload({ disabled: false });
                                          purchaseContractAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.LibraryBillAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购首次不带票%>") {
                                          $("#libraryBillAttach").jqxFileUpload({ disabled: false });
                                          $("#wayBillAttach").jqxFileUpload({ disabled: false });
                                          libraryBillAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.WayBillAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购首次不带票%>") {
                                          $("#libraryBillAttach").jqxFileUpload({ disabled: false });
                                          $("#wayBillAttach").jqxFileUpload({ disabled: false });
                                          wayBillAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.InvoiceScanningAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购带票%>") {
                                          $("#invoiceScannAttach").jqxFileUpload({ disabled: false });
                                          invoiceScannAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.PurchaseContractAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购带票%>") {
                                          $("#purchaseContractAttach2").jqxFileUpload({ disabled: false });
                                          purchaseContractAttach2_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.LibraryBillAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购带票%>") {
                                          $("#libraryBillAttach2").jqxFileUpload({ disabled: false });
                                          $("#wayBillAttach2").jqxFileUpload({ disabled: false });
                                          libraryBillAttach2_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.WayBillAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.货物质押采购带票%>") {
                                          $("#libraryBillAttach2").jqxFileUpload({ disabled: false });
                                          $("#wayBillAttach2").jqxFileUpload({ disabled: false });
                                          wayBillAttach2_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.PurchaseDoubleContractAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.库存商品采购%>") {
                                          $("#purchaseAttach2").jqxFileUpload({ disabled: false });
                                          purchaseAttach2_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.PriceConfirmationAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.尾款%>") {
                                          $("#doPriceConfirmAttach").jqxFileUpload({ disabled: false });
                                          doPriceConfirmAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.ContractStatementAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.尾款%>") {
                                          $("#contractStatementAttach").jqxFileUpload({ disabled: false });
                                          contractStatementAttach_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.PurchaseDoubleContractAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.预付款保证金%>") {
                                          $("#purchaseAttach3").jqxFileUpload({ disabled: false });
                                          purchaseAttach3_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.InvoiceScanningAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.营业费用%>") {
                                          $("#invoiceScannAttach2").jqxFileUpload({ disabled: false });
                                          invoiceScannAttach2_needUpdate = true;
                                      }

                                      if (item.AttachType == AttachTypeEnum.CostBreakdownListAttach && "<%=this.curPayApply.PayMatter%>"=="<%=(int)NFMT.Funds.PayMatterEnum.营业费用%>") {
                                          $("#costBreakdownListAttach").jqxFileUpload({ disabled: false });
                                          costBreakdownListAttach_needUpdate = true;
                                      }
                                  });
                      }
                  }
                ]
            });

            //申请日期
            $("#txbApplyDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#selApplyDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", height: 25, width: 120, disabled: true });
            $("#selApplyDept").val(<%=this.curUser.DeptId%>);

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId=" + "<%=this.curSub.SubId%>";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            //收款公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curSub.SubId%>";
            var outCorpSource = {
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120, searchMode: "containsignorecase"
            });
            $("#selRecCorp").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    var obj = outCorpDataAdapter.records[index];
                    $("#spnRecCorpFullName").html(obj.CorpFullName);

                    var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selBank").val();
                    var tempSource = { datatype: "json", url: tempUrl, async: false };
                    var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                    $("#selBankAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
                }
            });

            //开户行
            var bankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var bankSource = { datatype: "json", url: bankUrl, async: false };
            var bankDataAdapter = new $.jqx.dataAdapter(bankSource);
            $("#selBank").jqxComboBox({ source: bankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120 });
            $("#selBank").on("change", function (event) {
                var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selBank").val();
                var tempSource = { datatype: "json", url: tempUrl, async: false };
                var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                $("#selBankAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
            });

            //收款账号
            var bankAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selBank").val();
            var bankAccountSource = { datatype: "json", url: bankAccountUrl, async: false };
            var bankAccountDataAdapter = new $.jqx.dataAdapter(bankAccountSource);
            $("#selBankAccount").jqxComboBox({ source: bankAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
            $("#selBankAccount").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var item = args.item;
                    $("#txbBankAccount").val(item.label);
                }
            });

            $("#txbBankAccount").jqxInput({ height: 23 });

            //申请金额
            $("#txbApplyBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //最后付款日
            $("#txbPayDeadline").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            
            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayMatterStyle%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#selPayMatter").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 160, height: 25, autoDropDownHeight: true });
            
            $("#selPayMatter").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    var item = args.item;
                    var label = item.label;
                    //alert(index + "     " + label);
                    if (index == 1 && label == "预付款保证金") {
                        ExpanderHandle(5);
                    }
                    else if (index == 6 && label == "背靠背") {
                        ExpanderHandle(0);
                    }
                    else if (index == 5 && label == "尾款") {
                        ExpanderHandle(4);
                    }
                    else if (index == 7 && label == "货物质押采购首次不带票") {
                        ExpanderHandle(1);
                    }
                    else if (index == 8 && label == "货物质押采购带票") {
                        ExpanderHandle(2);
                    }
                    else if (index == 9 && label == "库存商品采购") {
                        ExpanderHandle(3);
                    }
                    else if (index == 10 && label == "营业费用") {
                        ExpanderHandle(6);
                    }
                    else {
                        $("#jqxAttachExpander").hide();
                        tabSelectedIndex = -1;
                    }
                }
            });

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //备注
            $("#txbMemo").jqxInput({ height: 23 });
            $("#txbSpecialDesc").jqxInput({ height: 23 });

            //init data to control
            var tempDate = new Date("<%=this.curApply.ApplyTime.ToString("yyyy-MM-dd")%>");
            $("#txbApplyDate").jqxDateTimeInput({ value: tempDate });
            $("#selApplyCorp").val(<%=this.curApply.ApplyCorp%>);
            $("#selRecCorp").val(<%=this.curPayApply.RecCorpId%>);
            $("#selBank").val(<%=this.curPayApply.RecBankId%>);

            if(<%=this.curPayApply.RecBankAccountId%> >0){
                $("#selBankAccount").val(<%=this.curPayApply.RecBankAccountId%>);
            }

            $("#txbBankAccount").val("<%=this.curPayApply.RecBankAccount%>");
            $("#txbApplyBala").val(<%=this.curPayApply.ApplyBala%>);
            $("#selCurrency").val(<%=this.curPayApply.CurrencyId%>);

            tempDate = new Date("<%=this.curPayApply.PayDeadline.ToString("yyyy-MM-dd")%>");
            $("#txbPayDeadline").jqxDateTimeInput({ value: tempDate });
            $("#selPayMode").val(<%=this.curPayApply.PayMode%>);
            $("#selPayMatter").jqxDropDownList("val","<%=this.curPayApply.PayMatter%>");
            $("#txbMemo").val("<%=this.curApply.ApplyDesc%>");
            $("#txbSpecialDesc").val("<%=this.curPayApply.SpecialDesc%>");

            //buttons
            $("#btnUpdate").jqxButton();
            $("#btnCancel").jqxButton();

            //验证
            $("#jqxPayApplyExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#selApplyDept", message: "申请部门必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selApplyDept").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selApplyCorp", message: "申请公司必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selApplyCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selRecCorp", message: "收款公司必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selRecCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selBank", message: "开户行必选", action: "keyup,blur,change", rule: function (input, commit) {
                                return $("#selBank").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbBankAccount", message: "收款银行账户必填", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbBankAccount").val().length > 0;
                            }
                        },
                        {
                            input: "#txbApplyBala", message: "申请金额大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbApplyBala").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbApplyBala", message: "申请金额大于0且不大于<%=this.BalancePaymentValue%>", action: "keyup,blur", rule: function (input, commit) {
                                if($("#selPayMatter").val()==<%=(int)NFMT.Funds.PayMatterEnum.尾款%> && parseFloat(<%=this.BalancePaymentValue%>) > 0)
                                    return $("#txbApplyBala").jqxNumberInput("val") > 0 && $("#txbApplyBala").jqxNumberInput("val") <= <%=this.BalancePaymentValue%>;
                                else
                                    return $("#txbApplyBala").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            $("#BTBdiv").jqxValidator({
                rules:
                    [{ input: "#txbContractNo", message: "收款合同号不可为空", action: "keyup,blur", rule: "required" }]
            }); 

            $("#PreDiv").jqxValidator({
                rules:
                    [{
                        input: "#nbPaymentRatio", message: "相应支付比例必须大于0", action: "keyup,blur", rule: function (input, commit) {
                            return $("#nbPaymentRatio").jqxNumberInput("val") > 0;
                        }
                    }]
            });

            $("#selPayMatter").jqxDropDownList({ disabled: true }); 

            //修改
            $("#btnUpdate").click(function () {

                var isCanSubmit = $("#jqxPayApplyExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (stockApps.length > 0) {
                    var appBala = $("#txbApplyBala").val();
                    var sumStockBala = 0;
                    for (i = 0; i < stockApps.length; i++) {
                        sumStockBala += stockApps[i].ApplyBala;
                    }

                    if (Math.round(appBala*100) != Math.round(sumStockBala * 100)){
                        alert("申请总额不等于库存申请金额之和");
                        return;
                    };
                }

                if (tabSelectedIndex == 0) {
                    //背靠背
                    //var isCanSubmit = $("#BTBdiv").jqxValidator("validate");
                    //if (!isCanSubmit) { return; }

                    if (!purchaseAttach_hasFile && purchaseAttach_needUpdate) {
                        alert("请上传采购双签合同附件！");
                        return;
                    }
                }
                else if (tabSelectedIndex == 1) {
                    //货物质押采购首次不带票
                    if (!purchaseContractAttach_hasFile && purchaseContractAttach_needUpdate) {
                        alert("请上传采购合同附件！");
                        return;
                    }

                    if(libraryBillAttach_needUpdate || wayBillAttach_needUpdate){
                        if (!libraryBillAttach_hasFile && !wayBillAttach_hasFile) {
                            alert("请上传在库提单附件或在途提单附件！");
                            return;
                        }
                    }
                }
                else if (tabSelectedIndex == 2) {
                    //货物质押采购带票
                    if (!invoiceScannAttach_hasFile && invoiceScannAttach_needUpdate) {
                        alert("请上传发票扫描件！");
                        return;
                    }

                    if (!purchaseContractAttach2_hasFile && purchaseContractAttach2_needUpdate) {
                        alert("请上传采购合同附件！");
                        return;
                    }

                    if(libraryBillAttach2_needUpdate || wayBillAttach2_needUpdate){
                        if (!libraryBillAttach2_hasFile && !wayBillAttach2_hasFile) {
                            alert("请上传在库提单附件或在途提单附件！");
                            return;
                        }
                    }
                }
                else if (tabSelectedIndex == 3) {
                    //库存商品采购
                    if (!purchaseAttach2_hasFile && purchaseAttach2_needUpdate) {
                        alert("请上传采购双签合同附件！");
                        return;
                    }
                }
                else if (tabSelectedIndex == 4) {
                    //尾款
                    if (!contractStatementAttach_hasFile && contractStatementAttach_needUpdate) {
                        alert("请上传合同结算单附件！");
                        return;
                    }
                }
                else if (tabSelectedIndex == 5) {
                    //预付款保证金
                    //var isCanSubmit = $("#PreDiv").jqxValidator("validate");
                    //if (!isCanSubmit) { return; }

                    if (!purchaseAttach3_hasFile && purchaseAttach3_needUpdate) {
                        alert("请上传采购双签合同附件！");
                        return;
                    }
                }
                else if (tabSelectedIndex == 6) {
                    //营业费用
                    if (!invoiceScannAttach2_hasFile && invoiceScannAttach2_needUpdate) {
                        alert("请上传发票扫描件附件！");
                        return;
                    }

                    if (!costBreakdownListAttach_hasFile && costBreakdownListAttach_needUpdate) {
                        alert("请上传费用明细清单！");
                        return;
                    }
                }

                if (!confirm("确认修改付款申请？")) { return; }

                var accId = 0;
                if ($("#selBankAccount").val() > 0) { accId = $("#selBankAccount").val(); }

                var apply = {
                    ApplyId:"<%=this.curApply.ApplyId%>",
                    ApplyDept: $("#selApplyDept").val(),
                    ApplyCorp: $("#selApplyCorp").jqxComboBox("val"),
                    ApplyDesc: $("#txbMemo").val(),
                    ApplyTime: $("#txbApplyDate").val()
                };

                var payApply = {
                    PayApplyId:"<%=this.curPayApply.PayApplyId%>",
                    ApplyDeptId: $("#selApplyDept").val(),
                    RecCorpId: $("#selRecCorp").val(),
                    RecBankId: $("#selBank").val(),
                    RecBankAccountId: accId,
                    RecBankAccount: $("#txbBankAccount").val(),
                    ApplyBala: $("#txbApplyBala").val(),
                    CurrencyId: $("#selCurrency").val(),
                    PayMode: $("#selPayMode").val(),
                    PayDeadline: $("#txbPayDeadline").val(),
                    PayWord: $("#txbMemo").val(),
                    PayMatter: $("#selPayMatter").val(),
                    SpecialDesc: $("#txbSpecialDesc").val()
                };

                $.post("Handler/PayApplyUpdateHandler.ashx", { Apply: JSON.stringify(apply), PayApply: JSON.stringify(payApply), StockApps: JSON.stringify(stockApps) },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            if (tabSelectedIndex == 0) {
                                //背靠背
                                $("#purchaseAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.采购双签合同)%>" + obj.ReturnValue });
                                $("#purchaseAttach").jqxFileUpload("uploadAll");
                            }
                            else if (tabSelectedIndex == 1) {
                                //货物质押采购首次不带票
                                $("#purchaseContractAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.采购合同)%>" + obj.ReturnValue });
                                $("#purchaseContractAttach").jqxFileUpload("uploadAll");

                                if (libraryBillAttach_hasFile) {
                                    $("#libraryBillAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.在库提单附件)%>" + obj.ReturnValue });
                                    $("#libraryBillAttach").jqxFileUpload("uploadAll");
                                }
                                if (wayBillAttach_hasFile) {
                                    $("#wayBillAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.在途提单附件)%>" + obj.ReturnValue });
                                    $("#wayBillAttach").jqxFileUpload("uploadAll");
                                }
                            }
                            else if (tabSelectedIndex == 2) {
                                //货物质押采购带票
                                $("#invoiceScannAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.发票扫描件)%>" + obj.ReturnValue });
                                $("#invoiceScannAttach").jqxFileUpload("uploadAll");

                                $("#purchaseContractAttach2").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.采购合同)%>" + obj.ReturnValue });
                                $("#purchaseContractAttach2").jqxFileUpload("uploadAll");

                                if (libraryBillAttach2_hasFile) {
                                    $("#libraryBillAttach2").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.在库提单附件)%>" + obj.ReturnValue });
                                    $("#libraryBillAttach2").jqxFileUpload("uploadAll");
                                }
                                if (wayBillAttach2_hasFile) {
                                    $("#wayBillAttach2").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.在途提单附件)%>" + obj.ReturnValue });
                                    $("#wayBillAttach2").jqxFileUpload("uploadAll");
                                }
                            }
                            else if (tabSelectedIndex == 3) {
                                //库存商品采购
                                $("#purchaseAttach2").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.采购双签合同)%>" + obj.ReturnValue });
                                $("#purchaseAttach2").jqxFileUpload("uploadAll");
                            }
                            else if (tabSelectedIndex == 4) {
                                //尾款
                                if (doPriceConfirmAttach_hasFile) {
                                    $("#doPriceConfirmAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.点价确认单)%>" + obj.ReturnValue });
                                    $("#doPriceConfirmAttach").jqxFileUpload("uploadAll");
                                }

                                $("#contractStatementAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.合同结算单)%>" + obj.ReturnValue });
                                $("#contractStatementAttach").jqxFileUpload("uploadAll");
                            }
                            else if (tabSelectedIndex == 5) {
                                //预付款保证金
                                $("#purchaseAttach3").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.采购双签合同)%>" + obj.ReturnValue });
                                $("#purchaseAttach3").jqxFileUpload("uploadAll");
                            }
                            else if (tabSelectedIndex == 6) {
                                //营业费用
                                $("#invoiceScannAttach2").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.发票扫描件)%>" + obj.ReturnValue });
                                $("#invoiceScannAttach2").jqxFileUpload("uploadAll");

                                $("#costBreakdownListAttach").jqxFileUpload({ uploadUrl: uploadUrl + "<%=string.Format("t={0}&id=",(int)NFMT.Operate.AttachType.费用明细清单)%>" + obj.ReturnValue });
                                $("#costBreakdownListAttach").jqxFileUpload("uploadAll");
                            }
}
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PayApplyList.aspx";
                        }
                    }
                );
            });
        });
    </script>
</html>
