<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableStockCreate.aspx.cs" Inherits="NFMTSite.Funds.ReceivableStockCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        $(document).ready(function () {

            

            //Expander Init
            $("#jqxConSubExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });
            $("#jqxAmountAllotExpander").jqxExpander({ width: "98%" });
            $("#jqxCanAllotReceExpander").jqxExpander({ width: "98%" });
            $("#jqxAlreadySelectReceExpander").jqxExpander({ width: "98%" });
            $("#jqxCanAllotCorpReceExpander").jqxExpander({ width: "98%" });
            $("#jqxAlreadySelectCorpReceExpander").jqxExpander({ width: "98%" });
            $("#jqxCanAllotContractReceExpander").jqxExpander({ width: "98%" });
            $("#jqxAlreadySelectContractReceExpander").jqxExpander({ width: "98%" });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            var sid = $("#hidStockId").val();

            var countriesSource =
            {
                datatype: "json",
                datafields: [
                    { name: "CorpName", type: "string" },
                    { name: "CorpId", type: "string" }
                ],
                localdata: $("#hidCorps").val()
            };
            var countriesAdapter = new $.jqx.dataAdapter(countriesSource, { autoBind: true });

            //////////////////////***已分配情况***//////////////////////
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "ReceivableAllotId", type: "int" },
                   { name: "AllotTime", type: "date" },
                   { name: "Name", type: "string" },
                   { name: "AllotBala", type: "string" },
                   { name: "StatusName", type: "string" }
                ],
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "ra.ReceivableAllotId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ra.ReceivableAllotId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/ReceivableStockAllotListHandler.ashx?sid=" + sid
            };
            var DataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: DataAdapter,
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
                  { text: "分配人", datafield: "Name" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "收款状态", datafield: "StatusName" }
                ]
            });


            $("#jqxAllotTabs").jqxTabs({ width: "99.8%", position: "top", selectionTracker: "checked", animationType: "fade" });
            //////////////////////金额分配//////////////////////
            ///***收款分配***///
            formatedData = "";
            totalrecords = 0;
            selectSource =
            {
                localdata: details,
                datafields: [
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CorpCode", type: "string" },
                    { name: "Corp", type: "string" }
                ],
                datatype: "json"
            };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxSelectGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "ReceiveDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "收款银行", datafield: "BankName" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "分配公司", datafield: "Corp" },
                  { text: "分配金额", datafield: "CanAllotBala" },
                  { text: "操作", datafield: "ReceivableId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            ///***可选择收款分配***///
            formatedData = "";
            totalrecords = 0;

            var rids = "";
            for (i = 0; i < ReceivableIds.length; i++) {
                if (i != 0) { rids += ","; }
                rids += ReceivableIds[i];
            }

            canAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "Corp", value: "CorpCode", values: { source: countriesAdapter.records, value: "CorpId", name: "CorpName" } },
                    { name: "CorpCode", type: "string" }
                ],
                sort: function () {
                    $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "r.ReceivableId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "r.ReceivableId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/ReceivableStockCanReceListHandler.ashx?rids=" + rids + "&sid=" + sid
            };
            var canAllotDataAdapter = new $.jqx.dataAdapter(canAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxCanAllotGrid").jqxGrid(
            {
                width: "98%",
                source: canAllotDataAdapter,
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
                  { text: "收款日期", datafield: "ReceiveDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InnerCorp", editable: false },
                  { text: "收款银行", datafield: "BankName", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款金额", datafield: "PayBala", editable: false },
                  {
                      text: "分配公司", datafield: "CorpCode", displayfield: "Corp", columntype: "dropdownlist",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxDropDownList({ source: countriesAdapter, displayMember: "CorpName", valueMember: "CorpId", selectedIndex: 0 });
                      }
                  },
                  {
                      text: "分配金额", datafield: "CanAllotBala", columntype: "numberinput", validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "分配金额不能大于收款金额" };
                          }
                          return true;
                      }
                  },
                  { text: "操作", datafield: "ReceivableId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });


            ///***公司收款部分***///
            formatedData = "";
            totalrecords = 0;
            selectCorpSource =
            {
                localdata: detailsCorp,
                datafields: [
                    { name: "RefId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "AllotBala", type: "string" },
                    { name: "CanAllotBala", type: "string" }
                ],
                datatype: "json"
            };
            var selectCorpDataAdapter = new $.jqx.dataAdapter(selectCorpSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveCorpOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxSelectCorpGrid").jqxGrid(
            {
                width: "98%",
                source: selectCorpDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "对方公司", datafield: "CorpName" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "可分配金额", datafield: "CanAllotBala" },
                  { text: "操作", datafield: "RefId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            ///***可选择公司收款***///
            formatedData = "";
            totalrecords = 0;

            var refIds = "";
            for (i = 0; i < RefIds.length; i++) {
                if (i != 0) { refIds += ","; }
                refIds += RefIds[i];
            }

            canCorpSource =
            {
                datatype: "json",
                datafields: [
                    { name: "RefId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "AllotBala", type: "string" },
                    { name: "CanAllotBala", type: "string" }
                ],
                sort: function () {
                    $("#jqxCorpReceGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxCorpReceGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "ref.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ref.RefId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CorpReceivableAllotForStockListHandler.ashx?refIds=" + refIds + "&sid=" + sid
            };
            var canCorpDataAdapter = new $.jqx.dataAdapter(canCorpSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddCorpOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxCorpReceGrid").jqxGrid(
            {
                width: "98%",
                source: canCorpDataAdapter,
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
                  { text: "对方公司", datafield: "CorpName", editable: false },
                  { text: "分配金额", datafield: "AllotBala", editable: false },
                  {
                      text: "可分配金额", datafield: "CanAllotBala", columntype: "numberinput", validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "可分配金额不能大于分配金额" };
                          }
                          return true;
                      }
                  },
                  { text: "操作", datafield: "RefId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });



            ///***合约收款部分***///
            formatedData = "";
            totalrecords = 0;
            selectContractSource =
            {
                localdata: detailsContract,
                datafields: [
                    { name: "RefId", type: "int" },
                    { name: "SubNo", type: "string" },
                    { name: "AllotBala", type: "string" },
                    { name: "CanAllotBala", type: "string" }
                ],
                datatype: "json"
            };
            var selectCorpDataAdapter = new $.jqx.dataAdapter(selectContractSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveContractOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxSelectContractGrid").jqxGrid(
            {
                width: "98%",
                source: selectCorpDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "可分配金额", datafield: "CanAllotBala" },
                  { text: "操作", datafield: "RefId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            ///***可选择合约收款***///
            formatedData = "";
            totalrecords = 0;

            var refIdsCon = "";
            for (i = 0; i < RefIdContracts.length; i++) {
                if (i != 0) { refIdsCon += ","; }
                refIdsCon += RefIdContracts[i];
            }

            canContractSource =
            {
                datatype: "json",
                datafields: [
                    { name: "RefId", type: "int" },
                    { name: "SubNo", type: "string" },
                    { name: "AllotBala", type: "string" },
                    { name: "CanAllotBala", type: "string" }
                ],
                sort: function () {
                    $("#jqxContractReceGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxContractReceGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "ref.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ref.RefId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/ContractReceAllotForStockListHandler.ashx?refIds=" + refIds + "&sid=" + sid
            };
            var canContractDataAdapter = new $.jqx.dataAdapter(canContractSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddContractOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxContractReceGrid").jqxGrid(
            {
                width: "98%",
                source: canContractDataAdapter,
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
                  { text: "子合约号", datafield: "SubNo", editable: false },
                  { text: "分配金额", datafield: "AllotBala", editable: false },
                  {
                      text: "可分配金额", datafield: "CanAllotBala", columntype: "numberinput", validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "可分配金额不能大于分配金额" };
                          }
                          return true;
                      }
                  },
                  { text: "操作", datafield: "RefId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });


            $("#btnAdd").click(function () {
                if (ReceivableIds.length > 0) {
                    var rows = $("#jqxSelectGrid").jqxGrid("getrows");

                    $.post("Handler/ReceivableStockCreateHandler.ashx", {
                        Allot: JSON.stringify(rows),
                        memo: $("#txbMemo").val(),
                        sid: $("#hidStockId").val(),
                        stockNameId: $("#hidStockNameId").val(),
                        curId: $("#hidCurrencyId").val(),
                        allotFrom: AllotFromEnum.Receivable
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "ReceivableStockList.aspx";
                       }
                   );
                }
                else if (RefIds.length > 0) {
                    var rows = $("#jqxSelectCorpGrid").jqxGrid("getrows");

                    $.post("Handler/ReceivableStockCreateByCorpHandler.ashx", {
                        Allot: JSON.stringify(rows),
                        memo: $("#txbMemo").val(),
                        sid: $("#hidStockId").val(),
                        stockNameId: $("#hidStockNameId").val(),
                        curId: $("#hidCurrencyId").val(),
                        allotFrom: AllotFromEnum.CorpReceivable
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "ReceivableStockList.aspx";
                       }
                   );
                }
                else if (RefIdContracts.length > 0) {
                    var rows = $("#jqxSelectContractGrid").jqxGrid("getrows");

                    $.post("Handler/ReceivableStockCreateByContractHandler.ashx", {
                        Allot: JSON.stringify(rows),
                        memo: $("#txbMemo").val(),
                        sid: $("#hidStockId").val(),
                        stockNameId: $("#hidStockNameId").val(),
                        curId: $("#hidCurrencyId").val(),
                        allotFrom: AllotFromEnum.ContractReceivable
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "ReceivableStockList.aspx";
                       }
                   );
                }
                else { alert("未分配任何款项！"); }

            });
        });

        var details = new Array();//保存Grid信息
        var detailsCorp = new Array();
        var detailsContract = new Array();
        var ReceivableIds = new Array();//保存ReceivableId信息
        var RefIds = new Array();
        var RefIdContracts = new Array();

        //index 0
        function bntRemoveOnClick(row, value) {
            //var index = details.indexOf(row);
            //details.splice(index, 1);
            details.splice(row, 1);

            index = ReceivableIds.indexOf(value);
            ReceivableIds.splice(index, 1);

            var rids = "";
            for (i = 0; i < ReceivableIds.length; i++) {
                if (i != 0) { rids += ","; }
                rids += ReceivableIds[i];
            }
            if (rids.length < 1) {
                $("#jqxAllotTabs").jqxTabs("enableAt", 1);
                $("#jqxAllotTabs").jqxTabs("enableAt", 2);
            }

            flushGrid();
        }

        function bntAddOnClick(row, value) {
            $("#jqxAllotTabs").jqxTabs("disableAt", 1);
            $("#jqxAllotTabs").jqxTabs("disableAt", 2);

            var item = $("#jqxCanAllotGrid").jqxGrid("getrowdata", row);
            details.push(item);

            ReceivableIds.push(value);
            flushGrid();
        }

        function AllotGridFresh() {
            var rids = "";
            for (i = 0; i < ReceivableIds.length; i++) {
                if (i != 0) { rids += ","; }
                rids += ReceivableIds[i];
            }
            canAllotSource.url = "Handler/ReceivableStockCanReceListHandler.ashx?rids=" + rids + "&sid=" + $("#hidStockId").val();
            $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "rows");
        };

        function flushGrid() {
            selectSource.localdata = details;
            $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

            AllotGridFresh();
        };

        //index 1
        function bntRemoveCorpOnClick(row, value) {
            //var index = details.indexOf(row);
            //details.splice(index, 1);
            detailsCorp.splice(row, 1);

            index = RefIds.indexOf(value);
            RefIds.splice(index, 1);

            var refIds1 = "";
            for (i = 0; i < RefIds.length; i++) {
                if (i != 0) { refIds1 += ","; }
                refIds1 += RefIds[i];
            }
            if (refIds1.length < 1) {
                $("#jqxAllotTabs").jqxTabs("enableAt", 0);
                $("#jqxAllotTabs").jqxTabs("enableAt", 2);
            }

            flushCorpGrid();
        }

        function bntAddCorpOnClick(row, value) {
            $("#jqxAllotTabs").jqxTabs("disableAt", 0);
            $("#jqxAllotTabs").jqxTabs("disableAt", 2);

            var item = $("#jqxCorpReceGrid").jqxGrid("getrowdata", row);
            detailsCorp.push(item);

            RefIds.push(value);
            flushCorpGrid();
        }

        function flushCorpGrid() {
            selectCorpSource.localdata = detailsCorp;
            $("#jqxSelectCorpGrid").jqxGrid("updatebounddata", "rows");

            var refIds = "";
            for (i = 0; i < RefIds.length; i++) {
                if (i != 0) { refIds += ","; }
                refIds += RefIds[i];
            }
            canCorpSource.url = "Handler/CorpReceivableAllotForStockListHandler.ashx?refIds=" + refIds + "&sid=" + $("#hidStockId").val();
            $("#jqxCorpReceGrid").jqxGrid("updatebounddata", "rows");
        }

        //index 2
        function bntRemoveContractOnClick(row, value) {
            //var index = details.indexOf(row);
            //details.splice(index, 1);
            detailsContract.splice(row, 1);

            index = RefIdContracts.indexOf(value);
            RefIdContracts.splice(index, 1);

            var refIds1 = "";
            for (i = 0; i < RefIdContracts.length; i++) {
                if (i != 0) { refIds1 += ","; }
                refIds1 += RefIdContracts[i];
            }
            if (refIds1.length < 1) {
                $("#jqxAllotTabs").jqxTabs("enableAt", 0);
                $("#jqxAllotTabs").jqxTabs("enableAt", 1);
            }

            flushContractGrid();
        }

        function bntAddContractOnClick(row, value) {
            $("#jqxAllotTabs").jqxTabs("disableAt", 0);
            $("#jqxAllotTabs").jqxTabs("disableAt", 1);

            var item = $("#jqxContractReceGrid").jqxGrid("getrowdata", row);
            detailsContract.push(item);

            RefIdContracts.push(value);
            flushContractGrid();
        }

        function flushContractGrid() {
            selectContractSource.localdata = detailsContract;
            $("#jqxSelectContractGrid").jqxGrid("updatebounddata", "rows");

            var refIds = "";
            for (i = 0; i < RefIdContracts.length; i++) {
                if (i != 0) { refIds += ","; }
                refIds += RefIdContracts[i];
            }
            canContractSource.url = "Handler/ContractReceAllotForStockListHandler.ashx?refIds=" + refIds + "&sid=" + $("#hidStockId").val();
            $("#jqxContractReceGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存信息
            <input type="hidden" id="hidCurrencyId" runat="server" />
            <input type="hidden" id="hidCorps" runat="server" />
            <input type="hidden" id="hidStockId" runat="server" />
            <input type="hidden" id="hidStockNameId" runat="server" />
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
                <%--<li>
                    <strong>报关状态：</strong>
                    <span runat="server" id="spanCustomsType"></span>
                </li>--%>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已分配情况
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAmountAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
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
                            <div id="jqxAlreadySelectReceExpander">
                                <div>
                                    已选择收款登记
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxSelectGrid" />
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCanAllotReceExpander">
                                <div>
                                    可分配收款登记
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCanAllotGrid"></div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div>
                    <ul style="list-style-type: none;">
                        <li>
                            <div id="jqxAlreadySelectCorpReceExpander">
                                <div>
                                    已选择公司收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxSelectCorpGrid" />
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCanAllotCorpReceExpander">
                                <div>
                                    可分配公司收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxCorpReceGrid"></div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div>
                    <ul style="list-style-type: none;">
                        <li>
                            <div id="jqxAlreadySelectContractReceExpander">
                                <div>
                                    已选择合约收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxSelectContractGrid" />
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 10px;">
                            <div id="jqxCanAllotContractReceExpander">
                                <div>
                                    可分配合约收款
                                </div>
                                <div class="DataExpander">
                                    <div id="jqxContractReceGrid"></div>
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
        <textarea id="txbMemo" runat="server"></textarea>&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnAdd" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="ReceivableStockList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
