<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinBusInvAllotCreate.aspx.cs" Inherits="NFMTSite.Invoice.FinBusInvAllotCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务发票分配新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            

            $("#jqxFinInvInfoExpander").jqxExpander({ width: "98%" });
            $("#jqxAlreadyAllotExpander").jqxExpander({ width: "98%" });
            $("#jqxCanAllotBusInvoiceExpander").jqxExpander({ width: "98%" });

            //////////////////////////////////////////财务发票信息//////////////////////////////////////////
            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            var tempDate = new Date("<%=this.curInvoice.InvoiceDate.ToString("yyyy/MM/dd")%>");
            $("#txbInvoiceDate").jqxDateTimeInput({ value: tempDate });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23, disabled: true });
            $("#txbInvoiceName").val("<%=this.curInvoice.InvoiceName%>");

            //开票公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=" + "<%=this.outSelf%>";
            var outCorpSource = { datafields: [{ name: "CorpId", type: "int" }, { name: "CorpName", type: "string" }, { name: "CorpFullName", type: "string" }], datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });
            $("#selOutCorp").val(<%=this.curInvoice.OutCorpId%>);

            //收票公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=" + "<%=this.inSelf%>";
            var inCorpSource = { datafields: [{ name: "CorpId", type: "int" }, { name: "CorpName", type: "string" }, { name: "CorpFullName", type: "string" }], datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });
            $("#selInCorp").val(<%=this.curInvoice.InCorpId%>);

            //txbInvoiceBala 发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbInvoiceBala").val(<%=this.curInvoice.InvoiceBala%>);

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#selCurrency").val(<%=this.curInvoice.CurrencyId%>);

            //品种 selAsset
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#selAsset").val(<%=this.curFundsInvoice.AssetId%>);

            //毛重 txbIntegerAmount
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbIntegerAmount").val(<%=this.curFundsInvoice.IntegerAmount%>);

            //净重 txbNetAmount
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbNetAmount").val(<%=this.curFundsInvoice.NetAmount%>);

            //计量单位 selUnit
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#selUnit").val(<%=this.curFundsInvoice.MUId%>);

            //增值税率 txbVATRatio
            $("#txbVATRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbolPosition: "right", spinButtons: true, width: 100, disabled: true });
            $("#txbVATRatio").val(<%=this.curFundsInvoice.VATRatio%>);

            //增值税 txbVATBala
            $("#txbVATBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#txbVATBala").val(<%=this.curFundsInvoice.VATBala%>);

            //备注
            $("#txbMemo").jqxInput({ height: 23, disabled: true });
            $("#txbMemo").val("<%=this.curInvoice.Memo%>");

            //已分配金额
            $("#txbAllotAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true, symbol: "<%=this.currencyName%>", symbolPosition: "right" });
            $("#txbAllotAmount").val("<%=this.allotAmount%>");

            //////////////////////////////////////////已选择分配//////////////////////////////////////////
            var formatedData = "";
            var totalrecords = 0;
            selectSource =
            {
                localdata: details,
                datafields: [
                    { name: "InvoiceDate", type: "date" },
                    { name: "InvoiceNo", type: "string" },
                    { name: "InvoiceName", type: "string" },
                    { name: "DetailName", type: "string" },
                    { name: "CurrencyName", type: "string" },
                    { name: "OutCorpName", type: "string" },
                    { name: "InCorpName", type: "string" },
                    { name: "VATRatio", type: "number" },
                    { name: "VATBala", type: "number" },
                    { name: "InvoiceBala", type: "number" },
                    { name: "AllotBala", type: "number" },
                    { name: "BusinessInvoiceId", type: "int" }
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
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"btnRemoveOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxSelectGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "实际发票号", datafield: "InvoiceName" },
                  { text: "发票方向", datafield: "DetailName" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "开票公司", datafield: "OutCorpName" },
                  { text: "收票公司", datafield: "InCorpName" },
                  { text: "增值税率", datafield: "VATRatio" },
                  { text: "增值税金额", datafield: "VATBala" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "操作", datafield: "BusinessInvoiceId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            //////////////////////////////////////////可分配业务发票//////////////////////////////////////////
            formatedData = "";
            totalrecords = 0;

            canAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "InvoiceDate", type: "date" },
                    { name: "InvoiceNo", type: "string" },
                    { name: "InvoiceName", type: "string" },
                    { name: "DetailName", type: "string" },
                    { name: "CurrencyName", type: "string" },
                    { name: "OutCorpName", type: "string" },
                    { name: "InCorpName", type: "string" },
                    { name: "VATRatio", type: "number" },
                    { name: "VATBala", type: "number" },
                    { name: "InvoiceBala", type: "number" },
                    { name: "BusinessInvoiceId", type: "int" }
                ],
                sort: function () {
                    $("#jqxCanAllotBusInvGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxCanAllotBusInvGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "bus.BusinessInvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "bus.BusinessInvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/FinBusInvAllotBusInvListHandler.ashx?c=" + "<%=this.curInvoice.CurrencyId%>" + "&dir=" + "<%=this.curInvoice.InvoiceDirection%>" + "&outCorpId=" + "<%=this.curInvoice.OutCorpId%>" + "&inCorpId=" + "<%=this.curInvoice.InCorpId%>"
            };
            var canAllotDataAdapter = new $.jqx.dataAdapter(canAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"btnAddOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxCanAllotBusInvGrid").jqxGrid(
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
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "发票编号", datafield: "InvoiceNo", editable: false },
                  { text: "实际发票号", datafield: "InvoiceName", editable: false },
                  { text: "发票方向", datafield: "DetailName", editable: false },
                  { text: "发票金额", datafield: "InvoiceBala", editable: false },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "开票公司", datafield: "OutCorpName", editable: false },
                  { text: "收票公司", datafield: "InCorpName", editable: false },
                  { text: "增值税率", datafield: "VATRatio", editable: false },
                  { text: "增值税金额", datafield: "VATBala", editable: false },
                  {
                      text: "分配金额", datafield: "AllotBala", columntype: "numberinput"
                      , validation: function (cell, value) {
                          if (value <= 0) {
                              return { result: false, message: "分配金额必须大于0" };
                          }
                          return true;
                      }
                  },
                  { text: "操作", datafield: "BusinessInvoiceId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            $("#btnCreate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //新增
            $("#btnCreate").click(function () {
                $("#btnCreate").jqxButton({ disabled: true });
                if (!confirm("确认分配？")) { return; }

                var rows = $("#jqxSelectGrid").jqxGrid("getrows");

                $.post("Handler/FinBusInvAllotCreateHandler.ashx", {
                    allotInfo: JSON.stringify(rows),
                    fid: "<%=this.curFundsInvoice.FinanceInvoiceId%>",
                    cur: "<%=this.curInvoice.CurrencyId%>",
                    canAllotAmount: "<%=this.curInvoice.InvoiceBala-this.allotAmount%>"
                },
                    function (result) {
                        alert(result);
                        $("#btnCreate").jqxButton({ disabled: false });
                        if (result.indexOf("成功") > -1) { document.location.href = "FinBusInvAllotList.aspx"; }
                    }
                );
            });
        });


        var details = new Array();//保存Grid信息
        var BusinessInvoiceIds = new Array();//保存BusinessInvoiceId信息

        function btnRemoveOnClick(row, value) {
            if (!confirm("确认取消？")) { return; }
            details.splice(row, 1);

            index = BusinessInvoiceIds.indexOf(value);
            BusinessInvoiceIds.splice(index, 1);
            flushGrid();
        }

        function btnAddOnClick(row, value) {
            var item = $("#jqxCanAllotBusInvGrid").jqxGrid("getrowdata", row);
            if (item.AllotBala == undefined || item.AllotBala <= 0) return;
            details.push(item);

            BusinessInvoiceIds.push(value);
            flushGrid();
        }

        function flushGrid() {
            selectSource.localdata = details;
            $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

            var bIds = "";
            for (i = 0; i < BusinessInvoiceIds.length; i++) {
                if (i != 0) { bIds += ","; }
                bIds += BusinessInvoiceIds[i];
            }
            canAllotSource.url = "Handler/FinBusInvAllotBusInvListHandler.ashx?c=" + "<%=this.curInvoice.CurrencyId%>" + "&dir=" + "<%=this.curInvoice.InvoiceDirection%>" + "&outCorpId=" + "<%=this.curInvoice.OutCorpId%>" + "&inCorpId=" + "<%=this.curInvoice.InCorpId%>" + "&bIds=" + bIds
            $("#jqxCanAllotBusInvGrid").jqxGrid("updatebounddata", "rows");
        }
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxFinInvInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            财务发票信息
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
                <li>
                    <strong>已分配金额：</strong>
                    <div id="txbAllotAmount" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxAlreadyAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择分配
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxCanAllotBusInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可分配业务发票
        </div>
        <div>
            <div id="jqxCanAllotBusInvGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <a target="_self" runat="server" href="FinBusInvAllotFinInvoiceList.aspx" id="btnCancel">取消</a>
    </div>

</body>
</html>
