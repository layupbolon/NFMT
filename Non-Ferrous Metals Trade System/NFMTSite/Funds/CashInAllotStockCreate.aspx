<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotStockCreate.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotStockCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存收款分配新增</title>
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

        var contractSelectSource;
        var contractAllotSource;
        
        var cashInDetails = new Array();//收款登记选中集合
        var corpDetails = new Array();//公司款选中集合
        var contractDetails = new Array();


        $(document).ready(function () {
            

            //Expander Init
            $("#jqxConSubExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });

            $("#jqxOtherExpander").jqxExpander({ width: "98%" });
            
            $("#jqxAllotExpander").jqxExpander({ width: "98%" });
            
            $("#jqxCashInSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxCashInAllotExpander").jqxExpander({ width: "98%" });

            $("#jqxCorpSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxCorpAllotExpander").jqxExpander({ width: "98%" });
            
            $("#jqxContractSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxContractAllotExpander").jqxExpander({ width: "98%" });
            //////////////////////***已分配情况***//////////////////////
            var formatedData = "";
            var totalrecords = 0;

            var otherSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "AllotId", type: "int" },
                   { name: "AllotTime", type: "date" },
                   { name: "AlloterName", type: "string" },
                   { name: "SumBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
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
                url: "Handler/CashInStockOtherAllotListHandler.ashx?stockLogId=" + "<%=this.curStockLog.StockLogId%>"
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
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "分配日期", datafield: "AllotTime", cellsformat: "yyyy-MM-dd" },
                  { text: "分配人", datafield: "AlloterName" },
                  { text: "分配金额", datafield: "SumBala" },
                  { text: "币种", datafield: "CurrencyName" },
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

            $("#jqxAllotTabs").jqxTabs({ width: "99.8%", position: "top", selectionTracker: "checked", animationType: "fade" });

            //////////////////////金额分配//////////////////////

            ///***收款分配***///
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

            ///***可选择收款分配***///
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
                sortable: true,
                selectionmode: "singlecell",
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


            ///***公司收款部分***///
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
                  { text: "操作", datafield: "CorpRefId", cellsrenderer: corpAddRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });



            ///***合约收款部分***///
            formatedData = "";
            totalrecords = 0;
            contractSelectSource =
            {
                localdata: contractDetails,
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ContractRefId", type: "int" },
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
            var contractSelectDataAdapter = new $.jqx.dataAdapter(contractSelectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var contractRemoveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntContractRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxContractSelectGrid").jqxGrid(
            {
                width: "98%",
                source: contractSelectDataAdapter,
                pageable: true,
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
                  { text: "操作", datafield: "ContractRefId", cellsrenderer: contractRemoveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            ///***可选择合约收款***///
            formatedData = "";
            totalrecords = 0;            

            contractAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ContractRefId", type: "int" },
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
                url: "Handler/CashInContractLastListHandler.ashx?subId="+"<%=this.curSub.SubId%>"
            };
            var contractAllotDataAdapter = new $.jqx.dataAdapter(contractAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var contractAddRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntContractAddOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxContractAllotGrid").jqxGrid(
            {
                width: "98%",
                source: contractAllotDataAdapter,
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
                  { text: "操作", datafield: "ContractRefId", cellsrenderer: contractAddRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });


            $("#btnAdd").click(function () {
                if (cashInDetails.length > 0) {
                    $.post("Handler/CashInStockDeirectCreateHandler.ashx", {
                        Details: JSON.stringify(cashInDetails),
                        AllotDesc: $("#txbMemo").val(),
                        StockLogId: "<%=this.curStockLog.StockLogId%>",
                        CurrencyId: "<%=this.curSub.SettleCurrency%>"
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "CashInAllotStockList.aspx";
                       }
                   );
                }
                else if (corpDetails.length > 0) {
                    $.post("Handler/CashInStockByCorpCreateHandler.ashx", {
                        Details: JSON.stringify(corpDetails),
                        StockLogId: "<%=this.curStockLog.StockLogId%>",
                        CurrencyId: "<%=this.curSub.SettleCurrency%>"
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "CashInAllotStockList.aspx";
                       }
                   );
                }
                else if (contractDetails.length > 0) {
                    $.post("Handler/CashInStockByContractCreateHandler.ashx", {
                        Details: JSON.stringify(contractDetails),                        
                        StockLogId: "<%=this.curStockLog.StockLogId%>",
                        CurrencyId: "<%=this.curSub.SettleCurrency%>"
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "CashInAllotStockList.aspx";
                       }
                   );
                }
                else { alert("未分配任何款项！"); }

            });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });


        });

var currencyId=<%=this.curSub.SettleCurrency%>;

        function bntCashInRemoveOnClick(row) {
           
            var item = cashInDetails[row];            

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

            //删除收款登记列表
            corpDetails.splice(row, 1);
            
            if(corpDetails.length == 0){
                $("#jqxAllotTabs").jqxTabs("enableAt", 0);
                $("#jqxAllotTabs").jqxTabs("enableAt", 2);
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
            $("#jqxAllotTabs").jqxTabs("disableAt", 2);

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

        function bntContractAddOnClick(row){ 
            
            var item = $("#jqxContractAllotGrid").jqxGrid("getrowdata", row);

            //添加收款登记列表
            contractDetails.push(item);

            $("#jqxAllotTabs").jqxTabs("disableAt", 0);
            $("#jqxAllotTabs").jqxTabs("disableAt", 1);

            flushContractGrid();
        }

        function bntContractRemoveOnClick(row){
        
            var item = contractDetails[row];           

            //删除收款登记列表
            contractDetails.splice(row, 1);
            
            if(contractDetails.length == 0){
                $("#jqxAllotTabs").jqxTabs("enableAt", 0);
                $("#jqxAllotTabs").jqxTabs("enableAt", 1);
            }

            flushContractGrid();
        }

        function flushContractGrid(){
            
            contractSelectSource.localdata = contractDetails;
            $("#jqxContractSelectGrid").jqxGrid("updatebounddata", "rows");

            var contractRefIds = "";
            for (i = 0; i < contractDetails.length; i++) {
                if (i != 0) { contractRefIds += ","; }
                contractRefIds += contractDetails[i].ContractRefId;
            }

            contractAllotSource.url = "Handler/CashInContractLastListHandler.ashx?subId=" + "<%=this.curSub.SubId%>" +"&contractRefIds=" + contractRefIds;
            $("#jqxContractAllotGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>

    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            归属合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>子合约号：</strong>
                    <span><%=this.curSub.SubNo%></span>
                </li>
                <li><strong>签订时间：</strong>
                    <span><%=this.curSub.ContractDate.ToShortDateString() %></span></li>
                <li>
                    <strong>我方公司：</strong>
                    <span runat="server" id="spnInCorpNames"></span>
                </li>
                <li><strong>对方公司：</strong>
                    <span runat="server" id="spnOutCorpNames"></span></li>
                <li>
                    <strong>签订数量：</strong>
                    <span runat="server" id="spnSignAmount"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>业务单号：</strong>
                    <span runat="server" id="spanRefNo"></span>
                </li>
                <li><strong>入库时间：</strong>
                    <span runat="server" id="spanStockDate"></span></li>
                <li>
                    <strong>归属公司：</strong>
                    <span runat="server" id="spanCorpId"></span>
                </li>
                <li><strong>入库重量：</strong>
                    <span runat="server" id="spanGrossAmout"></span></li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spanAssetId"></span>
                </li>
                <li><strong>品牌：</strong>
                    <span runat="server" id="spanBrandId"></span></li>
            </ul>
        </div>
    </div>

    <div id="jqxOtherExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已分配情况
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
                    <li>合约收款分配</li>
                </ul>
                <div>
                    <ul style="list-style-type: none;">
                        <li>
                            <div id="jqxCashInSelectExpander">
                                <div>
                                    已选择收款登记
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCashInSelectGrid" />
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCashInAllotExpander">
                                <div>
                                    可分配收款登记
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCashInAllotGrid"></div>
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
                                    <div id="jqxCorpSelectGrid" />
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCorpAllotExpander">
                                <div>
                                    可分配公司收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCorpAllotGrid"></div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div>
                    <ul style="list-style-type: none;">
                        <li>
                            <div id="jqxContractSelectExpander">
                                <div>
                                    已选择合约收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxContractSelectGrid" />
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxContractAllotExpander">
                                <div>
                                    可分配合约收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxContractAllotGrid"></div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <span>备注：</span>&nbsp;&nbsp;&nbsp;&nbsp;
        <textarea id="txbMemo" runat="server"></textarea>&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <input type="button" id="btnAdd" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="ReceivableStockList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
