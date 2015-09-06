<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotContractCreate.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotContractCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

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

        var corpDetails = new Array();//公司款选中集合
        var corpExcs = new Array();

        $(document).ready(function () {
            

            //Expander Init
            $("#jqxConExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxSubExpander").jqxExpander({ width: "98%", expanded: false });

            $("#jqxOtherExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxAllotExpander").jqxExpander({ width: "98%" });

            $("#jqxCashInSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxCashInAllotExpander").jqxExpander({ width: "98%" });

            $("#jqxCorpSelectExpander").jqxExpander({ width: "98%" }); 
            $("#jqxCorpAllotExpander").jqxExpander({ width: "98%" });

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

            $("#jqxAllotTabs").jqxTabs({ width: "99.8%", position: "top", selectionTracker: "checked", animationType: "fade" });

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
                    { name: "AllotCorpId", type: "int"},
                    { name: "AllotCorp", type: "string" }
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
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InCorp", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款银行", datafield: "CashInBankName", editable: false },
                  { text: "收款金额", datafield: "CashInBala" },
                  { text: "分配公司", datafield: "AllotCorp" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "币种", datafield: "CurrencyName", editable: false },
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
                    { name: "AllotCorpId", type: "int"},
                    { name: "AllotCorp", type: "string" }
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
                url: "Handler/CashInLastListHandler.ashx?&currencyId=" + "<%=this.curSub.SettleCurrency%>"
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
                      text: "分配公司", datafield: "AllotCorpId", displayfield: "AllotCorp", columntype: "combobox", cellclassname: "GridFillNumber",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId",autoDropDownHeight:true });
                      }
                  },
                  {
                      text: "分配金额", datafield: "AllotBala", columntype: "numberinput", cellclassname: "GridFillNumber", validation: function (cell, value) {
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
            

            ///公司收款部分///
            formatedData = "";
            totalrecords = 0;
            corpSelectSource =
            {
                localdata: corpDetails,
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CorpRefId", type: "int" },
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
                    { name: "AllotCorp", type: "string" }
                ],
                datatype: "json"
            };
            var corpSelectDataAdapter = new $.jqx.dataAdapter(corpSelectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var corpRemoveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntCorpRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxCorpSelectGrid").jqxGrid(
            {
                width: "98%",
                source: corpSelectDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
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
                  { text: "分配公司", datafield: "AllotCorp", editable: false },
                  { text: "分配金额", datafield: "AllotBala", editable: false },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "CorpRefId", cellsrenderer: corpRemoveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });
            
            ///可选择公司收款///
            formatedData = "";
            totalrecords = 0;
            
            corpAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CorpRefId", type: "int" },
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
                    { name: "AllotCorp", type: "string" }
                ],
                sort: function () {
                    $("#jqxCorpAllotGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "cicr.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cicr.RefId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CashInCorpLastListHandler.ashx?currencyId=" + "<%=this.curSub.SettleCurrency%>" +"&corpIds="+"<%=this.curOutCorpIds%>"
            };
            var corpAllotDataAdapter = new $.jqx.dataAdapter(corpAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var corpAddRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntCorpAddOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxCorpAllotGrid").jqxGrid(
            {
                width: "98%",
                source: corpAllotDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
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
                  { text: "分配公司", datafield: "AllotCorp", editable: false },
                  {
                      text: "分配金额", datafield: "AllotBala", columntype: "numberinput", validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "分配金额不能大于剩余金额" };
                          }
                          return true;
                      }, sortable: false
                  },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "CashInCorpId", cellsrenderer: corpAddRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });


            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnAdd").click(function () {
                if (cashInDetails.length > 0) {
                    var rows = $("#jqxCashInSelectGrid").jqxGrid("getrows");

                    $.post("Handler/CashInContractDirectCreateHandler.ashx", {
                        Details: JSON.stringify(rows),
                        Memo: $("#txbMemo").val(),
                        SubId: "<%=this.curSub.SubId%>"
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "CashInAllotList.aspx";
                       }
                   );
                }
                else if (corpDetails.length > 0) {
                    var rows = $("#jqxCorpSelectGrid").jqxGrid("getrows");

                    $.post("Handler/CashInContractCreateHandler.ashx", {
                        Details: JSON.stringify(rows),
                        SubId: <%=this.curSub.SubId%>
                        },
                       function (result) {
                           alert(result);
                           document.location.href = "CashInAllotList.aspx";
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
            
            if(cashInDetails.length == 0){
                $("#jqxAllotTabs").jqxTabs({ disabled:false });
            }

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
            
            $("#jqxAllotTabs").jqxTabs({ disabled:true });

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

            cashInAllotSource.url = "Handler/CashInLastListHandler.ashx?&currencyId=" + "<%=this.curSub.SettleCurrency%>"+"&cashInIds="+cashInIds;
            $("#jqxCashInAllotGrid").jqxGrid("updatebounddata", "rows");
        }

        function bntCorpRemoveOnClick(row) {
           
            var item = corpDetails[row];

            if(item.DetailId != undefined && item.DetailId>0){
                //增加排除收款分配
                corpExcs.push(item);
            }

            //删除收款登记列表
            corpDetails.splice(row, 1);
            
            if(corpDetails.length == 0){
                $("#jqxAllotTabs").jqxTabs("enableAt", 0);
            }

            flushCorpGrid();
        }

        function bntCorpAddOnClick(row) {

            var item = $("#jqxCorpAllotGrid").jqxGrid("getrowdata", row);            
            
            if(item.CurrencyId != currencyId){
                alert("必须选择与合约相同币种的收款");
                return;
            }

            //添加收款登记列表
            corpDetails.push(item);
            
            $("#jqxAllotTabs").jqxTabs("disableAt", 0);

            flushCorpGrid();
        }

        function flushCorpGrid() {

            corpSelectSource.localdata = corpDetails;
            $("#jqxCorpSelectGrid").jqxGrid("updatebounddata", "rows");

            //cropRefIds
            var cropRefIds = "";
            for (i = 0; i < corpDetails.length; i++) {
                if (i != 0) { cropRefIds += ","; }
                cropRefIds += corpDetails[i].CorpRefId;
            }
            
            corpAllotSource.url = "Handler/CashInCorpLastListHandler.ashx?currencyId=" + "<%=this.curSub.SettleCurrency%>" +"&corpIds="+"<%=this.curOutCorpIds%>"+"&cropRefIds="+cropRefIds;
            $("#jqxCorpAllotGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>合约编号：</strong>
                    <span runat="server" id="spnContractNo"></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spnAsset"></span>
                </li>
                <li>
                    <strong>签订数量：</strong>
                    <span runat="server" id="spnSignAmount"></span>
                </li>
                <li>
                    <strong>我方抬头：</strong>
                    <span runat="server" id="spnInCorpNames"></span>
                </li>
                <li>
                    <strong>对方抬头：</strong>
                    <span runat="server" id="spnOutCorpNames"></span>
                </li>
                <li>
                    <strong>合约升贴水：</strong>
                    <span runat="server" id="spnPre"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            子合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>子合约编号：</strong>
                    <span runat="server" id="spnSubNo"></span>
                </li>
                <li>
                    <strong>子合约数量：</strong>
                    <span runat="server" id="spnSubSignAmount"></span>
                </li>
                <li>
                    <strong>已分配数量：</strong>
                    <span runat="server" id="Span1"></span>
                </li>
                <li>
                    <strong>升贴水：</strong>
                    <span runat="server" id="Span2"></span>
                </li>
                <li>
                    <strong>执行最终日：</strong>
                    <span runat="server" id="spnPeriodE"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxOtherExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约其他分配情况
        </div>
        <div>
            <div id="jqxOtherGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            金额分配
        </div>
        <div>
            <div id="jqxAllotTabs">
                <ul>
                    <li style="margin-left: 30px;">收款登记</li>
                    <li>公司收款分配</li>
                </ul>
                <div>
                    <ul style="list-style-type: none;">
                        <li>
                            <div id="jqxCashInSelectExpander">
                                <div>
                                    已选择收款登记
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCashInSelectGrid"></div>
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCashInAllotExpander">
                                <div>
                                    可分配收款登记
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCashInAllotGrid" />
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div>
                    <ul style="list-style-type: none;">
                        <li>
                            <div id="jqxCorpSelectExpander">
                                <div>
                                    已选择公司收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCorpSelectGrid"></div>
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCorpAllotExpander">
                                <div>
                                    可分配公司收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCorpAllotGrid" />
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <span style="text-align: right; margin: 5px 2px 5px 0px;">备注：</span>
        <textarea id="txbMemo" runat="server"></textarea>
        <br />
        <input type="button" id="btnAdd" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="ContractReceivableList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
