<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotMainUpdate.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotMainUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款分配修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var sIId = 0;
        var allotsource = null;
        var dataRecord = null;

        $(document).ready(function () {

            $("#jqxCashInExpander").jqxExpander({ width: "98%" });
            $("#jqxContractExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxAllotInfoExpander").jqxExpander({ width: "98%" });
            $("#jqxSIInvocieExpander").jqxExpander({ width: "98%" });
            $("#jqxCanSIInvoiceExpander").jqxExpander({ width: "98%" });

            //////////////////////////////////合约列表//////////////////////////////////

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "SubId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "CreateFrom", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "SubNo", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "TradeDirectionName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "ContractWeight", type: "string" },
                   { name: "PriceModeName", type: "string" },
                   { name: "ContractStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "RefId", type: "string" }
                ],
                localdata:<%=this.contractGirdInfo%>
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxContractGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "签订日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" ,width:100},
                  { text: "内部子合约号", datafield: "SubNo",width:120 },
                  { text: "外部合约号", datafield: "OutContractNo",width:170  },
                  { text: "购销方向", datafield: "TradeDirectionName",width:80  },
                  { text: "我方公司", datafield: "InCorpName" ,width:200 },
                  { text: "对方公司", datafield: "OutCorpName",width:200 },
                  { text: "交易品种", datafield: "AssetName",width:80 },
                  { text: "合约重量", datafield: "ContractWeight" },
                  { text: "点价方式", datafield: "PriceModeName" },
                  { text: "合约状态", datafield: "StatusName" }
                ]
            });

            //////////////////////////////////合约库存//////////////////////////////////

            formatedData = "";
            totalrecords = 0;
            invStocksSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockLogId", type: "int" },
                   { name: "StockNameId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "MUName", type: "string" },
                   { name: "NetGapAmount", type: "number" },
                   { name: "AllotNetAmount", type: "number" },
                   { name: "AllotBala", type: "number" }
                ],
                localdata : <%=this.stockGridInfo%>
            };

            var invStocksDataAdapter = new $.jqx.dataAdapter(invStocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                source: invStocksDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                //sortable: true,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 25,
                selectionmode: "singlecell",
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false, width: 80 },
                  { text: "品牌", datafield: "BrandName", editable: false, width: 80 },
                  { text: "交货地", datafield: "DPName" , editable: false},
                  { text: "卡号", datafield: "CardNo", editable: false },
                  { text: "库存状态", datafield: "StatusName", editable: false },
                  { text: "重量单位", datafield: "MUName", editable: false },
                  { text: "过磅净重", datafield: "NetGapAmount", editable: false },
                  { text: "配款净重", datafield: "AllotNetAmount", editable: false },
                  {
                      text: "分配金额", datafield: "AllotBala", width: 160, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber",
                      validation: function (cell, value) {
                          if (value < 0) {
                              return { result: false, message: "分配金额不能小于0" + item.CanNetAmount };
                          }
                          return true;
                      },
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, width: 160, Digits: 8, spinButtons: true, symbolPosition: "right", symbol: "<%=this.CurrencyName%>" });
                      },
                      aggregates: [{
                          '总':
                              function (aggregatedValue, currentValue) {
                                  if (currentValue) {
                                      aggregatedValue += currentValue;
                                  }
                                  return Math.round(aggregatedValue * 100) / 100;
                              }
                      }]
                  }
                ]
            });

            $("#jqxStockGrid").on("cellvaluechanged", function (event) {
                var sumBala = 0;
                var rows = $("#jqxStockGrid").jqxGrid("getrows");

                for (i = 0; i < rows.length; i++) {
                    var item = rows[i];
                    if (item.StockId <= 0) continue;

                    sumBala += item.AllotBala;
                }

                //更新控件
                $("#nbAllotBala").val(sumBala);
            });

            var upIdsArray = new Array();
            var downIdsArray = new Array();

            var downSIIds = "<%=this.sIIds%>";
            var splitItem = downSIIds.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    downIdsArray.push(parseInt(splitItem[i]));
                }
            }

            var upSIIds = "<%=this.upSIIds%>";
            var splitItemup = upSIIds.split(',');
            for (i = 0; i < splitItemup.length; i++) {
                if (splitItemup[i].length > 0) {
                    upIdsArray.push(parseInt(splitItemup[i]));
                }
            }

            var exceptIds = "";
            for (i = 0; i < upIdsArray.length; i++) {
                if (i != 0) { exceptIds += ","; }
                exceptIds += upIdsArray[i];
            }

            var Infosource =
            {
                url: "Handler/CashInAllotMainSIInvoiceListHandler.ashx?sIIds=<%=this.upSIIds%>",
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
                   { name: "InvoiceBalaValue", type: "number" },
                   { name: "SIId", type: "int" },
                   { name: "AllotBala", type: "number" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxSIInvoiceGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxSIInvoiceGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  { text: "分配金额", datafield: "AllotBala" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxSIInvoiceGrid").jqxGrid("getrowdata", row);
                          var index = upIdsArray.indexOf(dataRecord.SIId);
                          upIdsArray.splice(index, 1);

                          var exceptIds = "";
                          for (i = 0; i < upIdsArray.length; i++) {
                              if (i != 0) { exceptIds += ","; }
                              exceptIds += upIdsArray[i];
                          }

                          //刷新列表
                          CanApplySIsource.url = "../Invoice/Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=<%=this.sIIds%>" + "&exceptIds=" + exceptIds;
                          $("#jqxCanSIInvoiceGrid").jqxGrid("updatebounddata", "rows");
                          Infosource.url = "../Invoice/Handler/InvoiceApplySISelectedListHandler.ashx?sIIds="+exceptIds;
                          $("#jqxSIInvoiceGrid").jqxGrid("updatebounddata", "rows");
                      }
                  }
                ]
            });


            //////////////////////////////////////////可申请发票信息//////////////////////////////////////////

            var formatedData = "";
            var totalrecords = 0;
            var CanApplySIsource =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
                   { name: "InvoiceBalaValue", type: "number" },
                   { name: "SIId", type: "int" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxCanSIInvoiceGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "../Invoice/Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=<%=this.sIIds%>"+ "&exceptIds=" + exceptIds
            }
            var CanApplySIdataAdapter = new $.jqx.dataAdapter(CanApplySIsource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxCanSIInvoiceGrid").jqxGrid(
            {
                width: "98%",
                source: CanApplySIdataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "申请";
                      }, buttonclick: function (row) {
                          dataRecord = $("#jqxCanSIInvoiceGrid").jqxGrid("getrowdata", row);

                          sIId = dataRecord.SIId;
                          allotsource.url = "../Invoice/Handler/SIDetailListHandler.ashx?sIId="+sIId;
                          $("#jqxAllotGrid").jqxGrid("updatebounddata", "rows");
                          $("#popupWindow").jqxWindow("show");
                      }
                  }
                ]
            });

            //分配公司
            var allotCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=<%=this.cashInContract.SubContractId%>";
            var allotCorpSource = { datatype: "json", url: allotCorpUrl, async: false };
            var allotCorpDataAdapter = new $.jqx.dataAdapter(allotCorpSource);
            $("#ddlAllotCorp").jqxComboBox({ source: allotCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase", selectedIndex: 0 });
            $("#ddlAllotCorp").jqxComboBox("val","<%=this.cashInCorp.CorpId%>");

            //分配金额
            $("#nbAllotBala").jqxNumberInput({ height: 25, min: 0, max: 999999999, decimalDigits: 2, digits: 9, width: 140, spinButtons: true });
            $("#nbAllotBala").jqxNumberInput("val","<%=this.cashInAllot.AllotBala%>");

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true });
            $("#ddlCurrency").jqxDropDownList("val", "<%=this.cashInAllot.CurrencyId%>");

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.PayMatter%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#ddlAllotType").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 120, height: 25, autoDropDownHeight: true, selectedIndex: 0 });
            $("#ddlAllotType").jqxDropDownList("val", "<%=this.cashInAllot.AllotType%>");

            $("#txbAllotDesc").jqxInput({ width: "500", height: 25 });
            $("#txbAllotDesc").jqxInput("val", "<%=this.cashInAllot.AllotDesc%>");

            $("#popupWindow").jqxWindow({ width: 1300,height:300, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01 });
            
            allotsource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockLogId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "SubContractId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "FeeTypeName", type: "string" },
                   { name: "FeeType", type: "string" },
                   { name: "DetailBala", type: "string" }
                ],
                type: "GET",
                url: "../Invoice/Handler/SIDetailListHandler.ashx?sIId="+sIId
            };
            var allotDataAdapter = new $.jqx.dataAdapter(allotsource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxAllotGrid").jqxGrid(
            {
                width: "98%",
                source: allotDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo",width:"9%" },
                  { text: "所属公司", datafield: "CorpName",width:"14%" },
                  { text: "品种", datafield: "AssetName",width:"8%" },
                  { text: "品牌", datafield: "BrandName",width:"7%" },
                  { text: "交货地", datafield: "DPName" ,width:"10%"},
                  { text: "卡号", datafield: "CardNo" ,width:"13%"},
                  { text: "子合约号", datafield: "SubNo",width:"16%" },
                  { text: "发票内容", datafield: "FeeTypeName" ,width:"12%"},
                  { text: "分配金额", datafield: "DetailBala",width:"11%" }
                ]
            });

            $("#Cancel").jqxButton({ height: 25, width: 100 });
            $("#Save").jqxButton({ height: 25, width: 100 });

            $("#Save").click(function () {
                var datarow = {
                    InvoiceId:dataRecord.InvoiceId,
                    InvoiceNo:dataRecord.InvoiceNo,
                    InvoiceDate:dataRecord.InvoiceDate,
                    InvoiceName:dataRecord.InvoiceName,
                    InvoiceBala:dataRecord.InvoiceBala,
                    InnerCorp:dataRecord.InnerCorp,
                    OutCorp:dataRecord.OutCorp,
                    StatusName:dataRecord.StatusName,
                    InvoiceStatus:dataRecord.InvoiceStatus,
                    SIId:dataRecord.SIId,
                    AllotBala:dataRecord.InvoiceBalaValue
                };
                var commit = $("#jqxSIInvoiceGrid").jqxGrid("addrow", null, datarow);
                $("#popupWindow").jqxWindow("hide");
                
                upIdsArray.push(dataRecord.SIId);

                var exceptIds = "";
                for (i = 0; i < upIdsArray.length; i++) {
                    if (i != 0) { exceptIds += ","; }
                    exceptIds += upIdsArray[i];
                }

                CanApplySIsource.url = "../Invoice/Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=<%=this.sIIds%>" + "&exceptIds=" + exceptIds;
                $("#jqxCanSIInvoiceGrid").jqxGrid("updatebounddata", "rows");

            });

            //验证器
            $("#jqxAllotInfoExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlAllotCorp", message: "分配公司不可为空", action: "keyup,blur", rule: function (input, commit) {
                                return $('#ddlAllotCorp').jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#nbAllotBala", message: "分配金额必须大于0且不大于<%=this.CanAllotBala%>", action: "change", rule: function (input, commit) {
                                return $('#nbAllotBala').jqxNumberInput("val") > 0 && $('#nbAllotBala').jqxNumberInput("val") <= <%=this.CanAllotBala%>;
                            }
                        },
                        {
                            input: "#ddlCurrency", message: "币种不可为空", action: "change", rule: function (input, commit) {
                                return $('#ddlCurrency').jqxDropDownList("val") > 0;
                            }
                        },
                        {
                            input: "#ddlAllotType", message: "付款事项不可为空", action: "change", rule: function (input, commit) {
                                return $('#ddlAllotType').jqxDropDownList("val") > 0
                            }
                        }
                    ]
            });

            $("#btnCreate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnCreate").click(function () {
                var isCanSubmit = $("#jqxAllotInfoExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                $("#btnCreate").jqxButton({ disabled: true });

                var cashInAllot = {
                    AllotId:"<%=this.cashInAllot.AllotId%>",
                    AllotBala: $("#nbAllotBala").val(),
                    AllotType: $("#ddlAllotType").val(),
                    CurrencyId: $("#ddlCurrency").val(),
                    AllotDesc: $("#txbAllotDesc").val()
                }

                var cashInCorp = {
                    AllotId:"<%=this.cashInAllot.AllotId%>",
                    //BlocId
                    CorpId: $("#ddlAllotCorp").val(),
                    CashInId: "<%=this.cashIn.CashInId%>",
                    AllotBala: $("#nbAllotBala").val()
                    //FundsLogId
                }

                var cashInContract = {
                    //CorpRefId
                    AllotId:"<%=this.cashInAllot.AllotId%>",
                    CashInId: "<%=this.cashIn.CashInId%>",
                    ContractId: "<%=this.cashInContract.ContractId%>",
                    SubContractId: "<%=this.cashInContract.SubContractId%>",
                    AllotBala: $("#nbAllotBala").val()
                    //FundsLogId
                }

                var rows = $("#jqxStockGrid").jqxGrid("getrows");

                var SIrows = $("#jqxSIInvoiceGrid").jqxGrid("getrows");

                var sourceRows = new Array();
                for (i = 0; i < rows.length; i++) {
                    var item = rows[i];
                    if (item.StockId <= 0) continue;
                    sourceRows.push(item);
                }

                $.post("Handler/CashInAllotMainUpdateHandler.ashx", {
                    cashInAllot: JSON.stringify(cashInAllot),
                    cashInCorp: JSON.stringify(cashInCorp),
                    cashInContract: JSON.stringify(cashInContract),
                    cashInStock: JSON.stringify(sourceRows),
                    cashInInvoice:JSON.stringify(SIrows)
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CashInAllotMainList.aspx";
                        } else {
                            $("#btnCreate").jqxButton({ disabled: false });
                        }
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxCashInExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            收款登记信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>收款日期：</strong>
                    <span><%=this.cashIn.CashInDate.ToShortDateString()%></span>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <span><%=this.InCorpName%></span>
                </li>

                <li>
                    <strong>收款银行：</strong>
                    <span><%=this.InBankName%></span>
                </li>
                <li>
                    <strong>收款账户：</strong>
                    <span><%=this.InBankAccountNo%></span>
                </li>

                <li>
                    <strong>收款金额：</strong>
                    <span><%=this.cashIn.CashInBala%></span>
                </li>
                <li>
                    <strong>收款币种：</strong>
                    <span><%=this.CurrencyName%></span>
                </li>

                <li>
                    <strong>付款公司：</strong>
                    <span><%=this.cashIn.PayCorpName%></span>
                </li>

                <li>
                    <strong>付款银行：</strong>
                    <span><%=this.cashIn.PayBank%></span>
                </li>
                <li>
                    <strong>付款账户：</strong>
                    <span><%=this.cashIn.PayAccount%></span>
                </li>

                <li>
                    <strong>简短附言：</strong>
                    <span><%=this.cashIn.PayWord%></span>
                </li>
                <li>
                    <strong>外部流水备注：</strong>
                    <span><%=this.cashIn.BankLog%></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxContractExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            子合约列表
        </div>
        <div>
            <div id="jqxContractGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约库存列表
        </div>
        <div>
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxSIInvocieExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价外票列表
        </div>
        <div>
            <div id="jqxSIInvoiceGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxCanSIInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可选价外票列表
        </div>
        <div>
            <div id="jqxCanSIInvoiceGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAllotInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            收款分配信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong style="float: left;">分配至公司：</strong>
                    <div style="float: left;" id="ddlAllotCorp"></div>
                </li>
                <li>
                    <strong style="float: left;">分配金额：</strong>
                    <div style="float: left" id="nbAllotBala"></div>
                </li>
                <li>
                    <strong style="float: left;">币种：</strong>
                    <div style="float: left;" id="ddlCurrency" />
                </li>
                <li>
                    <strong style="float: left;">收款事项：</strong>
                    <div style="float: left" id="ddlAllotType"></div>
                </li>
                <li>
                    <strong style="float: left;">备注：</strong>
                    <textarea id="txbAllotDesc" style="height: 120px;"></textarea><br />
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="提交" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="CashInAllotMainList.aspx" id="btnCancel">取消</a>
    </div>

    <div id="popupWindow">
        <div>价外票详细信息</div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <div id="jqxAllotGrid" ></div>
                </li>
                <li style="float:left;margin-top:20px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input style="margin-right: 5px;" type="button" id="Save" value="确认" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Cancel" type="button" value="取消" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
