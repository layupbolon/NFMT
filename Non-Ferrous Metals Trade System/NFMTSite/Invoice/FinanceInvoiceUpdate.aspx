<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceInvoiceUpdate.aspx.cs" Inherits="NFMTSite.Invoice.FinanceInvoiceUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务发票修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var invoiceBala = 0;

        $(document).ready(function () {
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectedBIExpander").jqxExpander({ width: "98%" });
            $("#jqxBIExpander").jqxExpander({ width: "98%" });

            var bussinessInvoiceIds = new Array();
            var sids = $("#hidsids").val();
            var splitItem = sids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    bussinessInvoiceIds.push(splitItem[i]);
                }
            }
            var inCorpId = 0;
            var outCorpId = 0;
            var assetId = 0;
            var currency = 0;

            /////////////////////////////////////已选择业务发票/////////////////////////////////////

            var iids = "";
            for (i = 0; i < bussinessInvoiceIds.length; i++) {
                if (i != 0) { iids += ","; }
                iids += bussinessInvoiceIds[i];
            }

            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "BusinessInvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "InoviceBalaName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "IntegerAmountName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "NetAmountName", type: "string" },
                   { name: "UnitPrice", type: "number" },
                   { name: "MarginRatio", type: "number" },
                   { name: "VATRatio", type: "number" },
                   { name: "VATBala", type: "number" },
                   { name: "AssetId", type: "int" },
                   { name: "CurrencyId", type: "int" },
                   { name: "OutCorpId", type: "int" },
                   { name: "InCorpId", type: "int" },
                   { name: "MUId", type: "int" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxAlreadyGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/GetBussinessInvoiceHandler.ashx?iids=" + iids
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxAlreadyGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
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
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  //{ text: "实际票据号", datafield: "InvoiceName" },
                  { text: "收票公司", datafield: "InCorpName" },
                  { text: "开票公司", datafield: "OutCorpName" },
                  { text: "发票金额", datafield: "InoviceBalaName" },
                  { text: "备注", datafield: "Memo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛量", datafield: "IntegerAmountName" },
                  { text: "净量", datafield: "NetAmountName" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxAlreadyGrid").jqxGrid("getrowdata", row);
                          var index = bussinessInvoiceIds.indexOf(dataRecord.BusinessInvoiceId);

                          bussinessInvoiceIds.splice(index, 1);

                          var iids = "";
                          for (i = 0; i < bussinessInvoiceIds.length; i++) {
                              if (i != 0) { iids += ","; }
                              iids += bussinessInvoiceIds[i];
                          }

                          //刷新列表
                          Infosource.url = "Handler/GetBussinessInvoiceHandler.ashx?iids=" + iids;
                          $("#jqxAlreadyGrid").jqxGrid("updatebounddata", "rows");

                          if (iids != "") {
                              source.url = "Handler/BussinessInvoiceAllotListHandler.ashx?dir=" + "<%=this.invoiceDirection%>" + "&inCorpId=" + inCorpId + "&outCorpId=" + outCorpId + "&cur=" + currency + "&ass=" + assetId + "&iids=" + iids;
                              $("#jqxBIGrid").jqxGrid("updatebounddata", "rows");
                          }
                          else {
                              source.url = "Handler/BussinessInvoiceAllotListHandler.ashx?dir=" + "<%=this.invoiceDirection%>" + "&iids=" + iids;
                              $("#jqxBIGrid").jqxGrid("updatebounddata", "rows");

                              $("#ddlOuterCorp").jqxComboBox({ disabled: false });
                              outCorpId = 0;

                              $("#ddlInnerCorp").jqxComboBox({ disabled: false });
                              inCorpId = 0;

                              $("#ddlAssetId").jqxComboBox({ disabled: false });
                              assetId = 0;

                              $("#ddlCurrency").jqxComboBox({ disabled: false });
                              currency = 0;
                          }

                          $("#selUnit").jqxComboBox("val", dataRecord.MUId);
                          var integerAmount = $("#txbIntegerAmount").val();
                          
                          $("#txbIntegerAmount").jqxNumberInput("val", Math.round((integerAmount - dataRecord.IntegerAmount) * 10000) / 10000);
                          var netAmount = $("#txbNetAmount").val();
                          $("#txbNetAmount").jqxNumberInput("val", Math.round((netAmount - dataRecord.NetAmount) * 10000) / 10000);
                          
                          invoiceBala = Math.round((invoiceBala - dataRecord.InvoiceBala) * 100) / 100;
                          $("#txbInvoiceBala").jqxNumberInput("val", invoiceBala);
                      }
                  }
                ]
            });
           
            /////////////////////////////////////可选择业务发票/////////////////////////////////////

            //开票公司
            var corpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=";
            var corpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: corpUrl + "<%=this.outSelf%>", async: false
            };
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#ddlOuterCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,disabled:true });

            //收票公司
            corpSource.url = corpUrl + "<%=this.inSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#ddlInnerCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrency").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            //品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssetId").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            $("#btnSearch").jqxButton({ height: 25, width: 100 });
            $("#btnSearch").click(function () {
                if (inCorpId === 0)
                    inCorpId = $("#ddlInnerCorp").val();
                if (outCorpId === 0)
                    outCorpId = $("#ddlOuterCorp").val();
                if (assetId === 0)
                    assetId = $("#ddlAssetId").val();
                if (currency === 0)
                    currency = $("#ddlCurrency").val();

                var iids = "";
                for (i = 0; i < bussinessInvoiceIds.length; i++) {
                    if (i !== 0) { iids += ","; }
                    iids += bussinessInvoiceIds[i];
                }

                source.url = "Handler/BussinessInvoiceAllotListHandler.ashx?dir=" + "<%=this.invoiceDirection%>" + "&inCorpId=" + inCorpId + "&outCorpId=" + outCorpId + "&cur=" + currency + "&ass=" + assetId + "&iids=" + iids;
                $("#jqxBIGrid").jqxGrid("updatebounddata", "rows");
            });

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "BusinessInvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "InoviceBalaName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "IntegerAmountName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "NetAmountName", type: "string" },
                   { name: "UnitPrice", type: "number" },
                   { name: "MarginRatio", type: "number" },
                   { name: "VATRatio", type: "number" },
                   { name: "VATBala", type: "number" },
                   { name: "AssetId", type: "int" },
                   { name: "CurrencyId", type: "int" },
                   { name: "OutCorpId", type: "int" },
                   { name: "InCorpId", type: "int" },
                   { name: "MUId", type: "int" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxBIGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/BussinessInvoiceAllotListHandler.ashx?dir=" + "<%=this.invoiceDirection%>" + "&inCorpId=" + inCorpId + "&outCorpId=" + outCorpId + "&cur=" + currency + "&ass=" + assetId + "&iids=" + iids
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxBIGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
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
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  //{ text: "实际票据号", datafield: "InvoiceName" },
                  { text: "收票公司", datafield: "InCorpName" },
                  { text: "开票公司", datafield: "OutCorpName" },
                  { text: "发票金额", datafield: "InoviceBalaName" },
                  { text: "备注", datafield: "Memo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛量", datafield: "IntegerAmountName" },
                  { text: "净量", datafield: "NetAmountName" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "选择";
                      }, buttonclick: function (row) {
                          var item = $("#jqxBIGrid").jqxGrid("getrowdata", row);
                          bussinessInvoiceIds.push(item.BusinessInvoiceId);

                          var iids = "";
                          for (i = 0; i < bussinessInvoiceIds.length; i++) {
                              if (i != 0) { iids += ","; }
                              iids += bussinessInvoiceIds[i];
                          }

                          $("#ddlOuterCorp").jqxComboBox("val", item.OutCorpId);
                          $("#ddlOuterCorp").jqxComboBox({ disabled: true });
                          outCorpId = item.OutCorpId;

                          $("#ddlInnerCorp").jqxComboBox("val", item.InCorpId);
                          $("#ddlInnerCorp").jqxComboBox({ disabled: true });
                          inCorpId = item.InCorpId;

                          $("#ddlAssetId").jqxComboBox("val", item.AssetId);
                          $("#ddlAssetId").jqxComboBox({ disabled: true });
                          assetId = item.AssetId;

                          $("#ddlCurrency").jqxComboBox("val", item.CurrencyId);
                          $("#ddlCurrency").jqxComboBox({ disabled: true });
                          currency = item.CurrencyId;

                          //刷新列表
                          Infosource.url = "Handler/GetBussinessInvoiceHandler.ashx?iids=" + iids;
                          $("#jqxAlreadyGrid").jqxGrid("updatebounddata", "rows");

                          source.url = "Handler/BussinessInvoiceAllotListHandler.ashx?dir=" + "<%=this.invoiceDirection%>" + "&inCorpId=" + inCorpId + "&outCorpId=" + outCorpId + "&cur=" + currency + "&ass=" + assetId + "&iids=" + iids;
                          $("#jqxBIGrid").jqxGrid("updatebounddata", "rows");

                          //绑定数据
                          $("#selOutCorp").jqxComboBox("val", outCorpId);
                          $("#selInCorp").jqxComboBox("val", inCorpId);
                          $("#selCurrency").jqxComboBox("val", currency);
                          $("#selAsset").jqxComboBox("val", assetId);
                          
                          $("#selUnit").jqxComboBox("val", item.MUId);
                          var integerAmount = $("#txbIntegerAmount").val();
                          $("#txbIntegerAmount").jqxNumberInput("val", Math.round((integerAmount + item.IntegerAmount) * 10000) / 10000);
                          var netAmount = $("#txbNetAmount").val();
                          $("#txbNetAmount").jqxNumberInput("val", Math.round((netAmount + item.NetAmount) * 10000) / 10000);
                          
                          invoiceBala = Math.round((invoiceBala + item.InvoiceBala) * 100) / 100;
                          $("#txbInvoiceBala").jqxNumberInput("val", invoiceBala);
                      }
                  }
                ]
            });


            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            $("#txbInvoiceDate").jqxDateTimeInput("val", new Date("<%=this.curInvoice.InvoiceDate%>"));

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23 });
            $("#txbInvoiceName").jqxInput("val", "<%=this.curInvoice.InvoiceName%>");

            //开票公司
            corpSource.url = corpUrl + "<%=this.outSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });
            $("#selOutCorp").jqxComboBox("val", "<%=this.curInvoice.OutCorpId%>");

            //收票公司
            corpSource.url = corpUrl + "<%=this.inSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selInCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });
            $("#selInCorp").jqxComboBox("val", "<%=this.curInvoice.InCorpId%>");

            //发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, max: 999999999, min: 0, digits: 9, decimalDigits: 2, width: 140, spinButtons: true });
            $("#txbInvoiceBala").jqxNumberInput("val", "<%=this.curInvoice.InvoiceBala%>");
            $("#txbInvoiceBala").on("valueChanged", function (event) {
                var value = event.args.value;
                var VATRatio = $("#txbVATRatio").val() / 100;
                $("#txbVATBala").jqxNumberInput("val", value / (1 + VATRatio) * VATRatio);
            });

            //币种
            $("#selCurrency").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true });
            $("#selCurrency").jqxComboBox("val", "<%=this.curInvoice.CurrencyId%>");

            //品种
            $("#selAsset").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true });
            $("#selAsset").jqxComboBox("val", "<%=this.curFundsInvoice.AssetId%>");

            //毛重
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });
            $("#txbIntegerAmount").jqxNumberInput("val", "<%=this.curFundsInvoice.IntegerAmount%>");

            //净重
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });
            $("#txbNetAmount").jqxNumberInput("val", "<%=this.curFundsInvoice.NetAmount%>");

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true });
            $("#selUnit").jqxComboBox("val", "<%=this.curFundsInvoice.MUId%>");

            //增值税率
            $("#txbVATRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, Digits: 3, symbolPosition: "right", symbol: "%", spinButtons: true, width: 100 });
            $("#txbVATRatio").jqxNumberInput("val", "<%=this.curFundsInvoice.VATRatio*100%>");
            $("#txbVATRatio").on("valueChanged", function (event) {
                var value = event.args.value / 100;
                var invoiceBala = $("#txbInvoiceBala").val();
                $("#txbVATBala").jqxNumberInput("val", invoiceBala / (1 + value) * value);
            });

            //增值税
            $("#txbVATBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true });
            $("#txbVATBala").jqxNumberInput("val", "<%=this.curFundsInvoice.VATBala%>");

            //备注
            $("#txbMemo").jqxInput({ height: 23 });
            $("#txbMemo").jqxInput("val", "<%=this.curInvoice.Memo%>");

            //buttons
            $("#btnCreate").jqxButton();
            $("#btnCancel").jqxButton();


            var rows = $("#jqxAlreadyGrid").jqxGrid("getrows");
            inCorpId = rows[0].InCorpId;
            outCorpId = rows[0].OutCorpId;
            assetId = rows[0].AssetId;
            currency = rows[0].CurrencyId;

            $("#ddlOuterCorp").jqxComboBox("val", outCorpId);
            $("#ddlCurrency").jqxComboBox("val", currency);
            $("#ddlInnerCorp").jqxComboBox("val", inCorpId);
            $("#ddlAssetId").jqxComboBox("val", assetId);

            //验证
            $("#jqxInvoiceExpander").jqxValidator({
                rules:
                    [
                        //{ input: "#txbInvoiceName", message: "实际发票号必填", action: "keyup,blur", rule: "required" },
                        {
                            input: "#selOutCorp", message: "开票公司必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selOutCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selInCorp", message: "收票公司必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selInCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbInvoiceBala", message: "发票金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbInvoiceBala").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbIntegerAmount", message: "毛重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbIntegerAmount").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbNetAmount", message: "净重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbNetAmount").jqxNumberInput("val") > 0;
                            }
                        }
                        //,{
                        //    input: "#txbVATRatio", message: "增值税率必须大于0且小于1", action: "keyup,blur", rule: function (input, commit) {
                        //        return $("#txbVATRatio").jqxNumberInput("val") > 0 && $("#txbVATRatio").jqxNumberInput("val") < 100;
                        //    }
                        //},
                        //{
                        //    input: "#txbVATBala", message: "增值税必须大于0", action: "keyup,blur", rule: function (input, commit) {
                        //        return $("#txbVATBala").jqxNumberInput("val") > 0;
                        //    }
                        //}
                    ]
            });

            //新增
            $("#btnCreate").click(function () {

                var isCanSubmit = $("#jqxInvoiceExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认修改财务发票？")) { return; }

                var invoice = {
                    InvoiceId:"<%=this.curInvoice.InvoiceId%>",
                    InvoiceDate: $("#txbInvoiceDate").val(),
                    InvoiceName: $("#txbInvoiceName").val(),
                    InvoiceBala: $("#txbInvoiceBala").val(),
                    CurrencyId: $("#selCurrency").val(),
                    InvoiceDirection: "<%=this.invoiceDirection%>",
                    OutCorpId: $("#selOutCorp").val(),
                    InCorpId: $("#selInCorp").val(),
                    Memo: $("#txbMemo").val()
                }

                var invoiceFunds = {
                    FinanceInvoiceId:"<%=this.curFundsInvoice.FinanceInvoiceId%>",
                    AssetId: $("#selAsset").val(),
                    IntegerAmount: $("#txbIntegerAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    MUId: $("#selUnit").val(),
                    VATRatio: $("#txbVATRatio").val(),
                    VATBala: $("#txbVATBala").val()
                };

                var iids = "";
                for (i = 0; i < bussinessInvoiceIds.length; i++) {
                    if (i != 0) { iids += ","; }
                    iids += bussinessInvoiceIds[i];
                }

                var fileIds = new Array();

                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/FinanceInvoiceUpdateHandler.ashx", { Invoice: JSON.stringify(invoice), InvoiceFunds: JSON.stringify(invoiceFunds), iids: iids },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            AjaxFileUpload(fileIds, obj.ReturnValue, AttachTypeEnum.InvoiceAttach);
                        }
                        alert(obj.Message);
                        document.location.href = "FinanceInvoiceList.aspx";
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxSelectedBIExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择业务发票
        </div>
        <div>
            <input type="hidden" id="hidsids" runat="server"/>
            <div id="jqxAlreadyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxBIExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可选择业务发票
        </div>
        <div>
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span style="float: left;">收票公司：</span>
                        <div id="ddlInnerCorp" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">开票公司：</span>
                        <div id="ddlOuterCorp" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">品种：</span>
                        <div id="ddlAssetId" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">币种：</span>
                        <div id="ddlCurrency" style="float: left;"></div>
                    </li>
                    <li>
                        <input type="button" id="btnSearch" value="查询" />
                    </li>
                </ul>
            </div>
            <div id="jqxBIGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong runat="server" id="titInvDate">开票日期：</strong>
                    <div id="txbInvoiceDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>实际发票号：</strong>
                    <input type="text" id="txbInvoiceName" style="float: left;" />
                </li>
                <li>
                    <strong>开票公司：</strong>
                    <div id="selOutCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>收票公司：</strong>
                    <div id="selInCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div id="txbInvoiceBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>

                <li>
                    <strong>品种：</strong>
                    <div id="selAsset" style="float: left;"></div>
                </li>

                <li>
                    <strong>毛重：</strong>
                    <div id="txbIntegerAmount" style="float: left;" />
                </li>
                <li>
                    <strong>净重：</strong>
                    <div id="txbNetAmount" style="float: left;"></div>
                </li>

                <li>
                    <strong>计量单位：</strong>
                    <div id="selUnit" style="float: left;"></div>
                </li>

                <li>
                    <strong>增值税率：</strong>
                    <div id="txbVATRatio" style="float: left;"></div>
                </li>
                <li>
                    <strong>增值税：</strong>
                    <div id="txbVATBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="InvoiceAttach" />

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

</body>
</html>