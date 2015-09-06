<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceInvoiceInvApplyCreate.aspx.cs" Inherits="NFMTSite.Invoice.FinanceInvoiceInvApplyCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务发票新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectedBIExpander").jqxExpander({ width: "98%" });
            $("#jqxBIExpander").jqxExpander({ width: "98%" });

            var downBussinessInvoiceIds = new Array();
            var sids = $("#hidsids").val();
            var splitItem = sids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    downBussinessInvoiceIds.push(splitItem[i]);
                }
            }

            var downIds = "";
            for (i = 0; i < downBussinessInvoiceIds.length; i++) {
                if (i !== 0) { downIds += ","; }
                downIds += downBussinessInvoiceIds[i];
            }

            var bussinessInvoiceIds = new Array();
            var iids = "";

            var inCorpId = 0;
            var outCorpId = 0;
            var assetId = 0;
            var currency = 0;

            /////////////////////////////////////已选择业务发票/////////////////////////////////////
            
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
                  { text: "收票公司", datafield: "OutCorpName" },
                  { text: "开票公司", datafield: "InCorpName" },
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
                          downBussinessInvoiceIds.push(dataRecord.BusinessInvoiceId);

                          var iids = "";
                          for (i = 0; i < bussinessInvoiceIds.length; i++) {
                              if (i !== 0) { iids += ","; }
                              iids += bussinessInvoiceIds[i];
                          }

                          downIds = "";
                          for (i = 0; i < downBussinessInvoiceIds.length; i++) {
                              if (i !== 0) { downIds += ","; }
                              downIds += downBussinessInvoiceIds[i];
                          }

                          //刷新列表
                          Infosource.url = "Handler/GetBussinessInvoiceHandler.ashx?iids=" + iids;
                          $("#jqxAlreadyGrid").jqxGrid("updatebounddata", "rows");

                          source.url = "Handler/GetBussinessInvoiceHandler.ashx?iids=" + downIds;
                          $("#jqxBIGrid").jqxGrid("updatebounddata", "rows");
                          
                          $("#selUnit").jqxComboBox("val", dataRecord.MUId);
                          var integerAmount = $("#txbIntegerAmount").val();
                          $("#txbIntegerAmount").jqxNumberInput("val", Math.round((integerAmount - dataRecord.IntegerAmount) * 10000) / 10000);
                          var netAmount = $("#txbNetAmount").val();
                          $("#txbNetAmount").jqxNumberInput("val", Math.round((netAmount - dataRecord.NetAmount) * 10000) / 10000);
                          
                          var invoiceBala = $("#txbInvoiceBala").val();
                          $("#txbInvoiceBala").jqxNumberInput("val", Math.round((invoiceBala - dataRecord.InvoiceBala) * 10000) / 10000);
                      }
                  }
                ]
            });

            /////////////////////////////////////可选择业务发票/////////////////////////////////////

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
                url: "Handler/GetBussinessInvoiceHandler.ashx?iids=" + downIds
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
                  { text: "收票公司", datafield: "OutCorpName" },
                  { text: "开票公司", datafield: "InCorpName" },
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
                          var index = downBussinessInvoiceIds.indexOf(item.BusinessInvoiceId);

                          bussinessInvoiceIds.push(item.BusinessInvoiceId);
                          downBussinessInvoiceIds.splice(index, 1);

                          iids = "";
                          for (i = 0; i < bussinessInvoiceIds.length; i++) {
                              if (i !== 0) { iids += ","; }
                              iids += bussinessInvoiceIds[i];
                          }

                          downIds = "";
                          for (i = 0; i < downBussinessInvoiceIds.length; i++) {
                              if (i !== 0) { downIds += ","; }
                              downIds += downBussinessInvoiceIds[i];
                          }
                          
                          outCorpId = item.OutCorpId;
                          inCorpId = item.InCorpId;
                          assetId = item.AssetId;
                          currency = item.CurrencyId;

                          //刷新列表
                          Infosource.url = "Handler/GetBussinessInvoiceHandler.ashx?iids=" + iids;
                          $("#jqxAlreadyGrid").jqxGrid("updatebounddata", "rows");

                          source.url = "Handler/GetBussinessInvoiceHandler.ashx?iids=" + downIds;
                          $("#jqxBIGrid").jqxGrid("updatebounddata", "rows");

                          //绑定数据
                          $("#selOutCorp").jqxComboBox("val", inCorpId);
                          $("#selInCorp").jqxComboBox("val", outCorpId);
                          $("#selCurrency").jqxComboBox("val", currency);
                          $("#selAsset").jqxComboBox("val", assetId);
                          
                          $("#selUnit").jqxComboBox("val", item.MUId);
                          var integerAmount = $("#txbIntegerAmount").val();
                          $("#txbIntegerAmount").jqxNumberInput("val", Math.round((integerAmount + item.IntegerAmount) * 10000) / 10000);
                          var netAmount = $("#txbNetAmount").val();
                          $("#txbNetAmount").jqxNumberInput("val", Math.round((netAmount + item.NetAmount) * 10000) / 10000);
                          
                          var invoiceBala = $("#txbInvoiceBala").val();
                          $("#txbInvoiceBala").jqxNumberInput("val", Math.round((invoiceBala + item.InvoiceBala) * 10000) / 10000);
                      }
                  }
                ]
            });


            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23 });

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
            corpSource.url = corpUrl + "<%=this.outSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selOutCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //收票公司
            corpSource.url = corpUrl + "<%=this.inSelf%>";
            var corpDataAdapter = new $.jqx.dataAdapter(corpSource);
            $("#selInCorp").jqxComboBox({ source: corpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, max: 999999999, digits: 9, decimalDigits: 2, width: 140, spinButtons: true });
            $("#txbInvoiceBala").on("valueChanged", function (event) {
                var value = event.args.value;
                var VATRatio = $("#txbVATRatio").val() / 100;
                $("#txbVATBala").jqxNumberInput("val", value / (1 + VATRatio) * VATRatio);
            });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true });

            //品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true });

            //毛重
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //净重
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxComboBox({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true });

            //增值税率
            $("#txbVATRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, Digits: 3, symbolPosition: "right", symbol: "%", spinButtons: true, width: 100 });
            $("#txbVATRatio").on("valueChanged", function (event) {
                var value = event.args.value / 100;
                var invoiceBala = $("#txbInvoiceBala").val();
                $("#txbVATBala").jqxNumberInput("val", invoiceBala / (1 + value) * value);
            });

            //增值税
            $("#txbVATBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23 });

            //buttons
            $("#btnAudit").jqxButton({ width: 120, height: 25 });
            $("#btnCreate").jqxButton({ width: 120, height: 25 });
            $("#btnCancel").jqxLinkButton({ width: 120, height: 25 });

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
                        },
                        {
                            input: "#txbVATRatio", message: "增值税率必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbVATRatio").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbVATBala", message: "增值税必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbVATBala").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            function FinInvByInvApplyCreate(isAudit) {
                var isCanSubmit = $("#jqxInvoiceExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认添加财务发票？")) { return; }

                var invoice = {
                    //InvoiceId: "",
                    InvoiceDate: $("#txbInvoiceDate").val(),
                    InvoiceName: $("#txbInvoiceName").val(),
                    InvoiceBala: $("#txbInvoiceBala").val(),
                    CurrencyId: $("#selCurrency").val(),
                    InvoiceDirection: 33,
                    OutCorpId: $("#selOutCorp").val(),
                    InCorpId: $("#selInCorp").val(),
                    Memo: $("#txbMemo").val()
                }

                var financeInvoice = {
                    AssetId: $("#selAsset").val(),
                    IntegerAmount: $("#txbIntegerAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    MUId: $("#selUnit").val(),
                    VATRatio: $("#txbVATRatio").val(),
                    VATBala: $("#txbVATBala").val()
                };

                var iids = "";
                for (i = 0; i < bussinessInvoiceIds.length; i++) {
                    if (i !== 0) { iids += ","; }
                    iids += bussinessInvoiceIds[i];
                }

                var fileIds = new Array();

                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/FinanceInvoicebyInvApplyCreateHandler.ashx", { Invoice: JSON.stringify(invoice), InvoiceFunds: JSON.stringify(financeInvoice), iids: iids, invApplyId: "<%=this.invoiceApplyId%>" },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() === "0") {
                            AjaxFileUpload(fileIds, obj.ReturnValue.InvoiceId, AttachTypeEnum.InvoiceAttach);
                        }
                        if (obj.ResultStatus.toString() === "0") {
                            if (isAudit) {
                                AutoSubmitAudit(MasterEnum.财务发票审核, JSON.stringify(obj.ReturnValue));
                            }
                        }
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() === "0") {
                            document.location.href = "FinanceInvoiceList.aspx";
                        }
                    }
                );
            }

            $("#btnCreate").click(function () { FinInvByInvApplyCreate(false); });
            $("#btnAudit").click(function () { FinInvByInvApplyCreate(true); });
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

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Create" AttachType="InvoiceAttach" />

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="添加并提交审核" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCreate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="FinanceInvoiceInvApplyList.aspx" target="_self" id="btnCancel">取消</a>
    </div>

</body>
</html>
