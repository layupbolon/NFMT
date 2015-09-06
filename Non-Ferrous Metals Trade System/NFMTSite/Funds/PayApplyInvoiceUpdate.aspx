<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyInvoiceUpdate.aspx.cs" Inherits="NFMTSite.Funds.PayApplyInvoiceUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请修改--关联发票</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var selects = new Array();
        var removeDetails = new Array();

        var invoiceListSource;
        var selectSource;

        $(document).ready(function () {
            

            $("#jqxSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxInvoiceListExpander").jqxExpander({ width: "98%" });
            $("#jqxPayApplyExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;
            selectSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "RefId", type: "int" },
                   { name: "SIId", type: "int" },
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "PayDept", type: "int" },
                   { name: "DeptName", type: "string" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "CurrencyId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "OutCorpId", type: "int" },
                   { name: "OutCorpName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "LastBala", type: "number" },
                   { name: "ApplyBala", type: "number" }
                ],
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
                localdata: <%=this.JsonStr%>
                };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveOnClick(" + row + ");\" />"
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
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "发票号", datafield: "InvoiceNo" },
                  { text: "开票公司", datafield: "OutCorpName" },
                  { text: "收票公司", datafield: "InCorpName" },
                  { text: "成本部门", datafield: "DeptName" },
                  { text: "发票金额", datafield: "InvoiceBala" },                  
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "申请金额", datafield: "ApplyBala" },
                  { text: "操作", datafield: "SIId", cellsrenderer: removeRender, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            selects = $("#jqxSelectGrid").jqxGrid("getrows");

            var initIds = "";
            for (i = 0; i < selects.length; i++) {
                var temp = selects[i];
                if (!isNaN(temp.SIId)) {
                    if (i > 0) { initIds += ","; }
                    initIds += temp.SIId;
                }
            }

            //Invoice List
            formatedData = "";
            totalrecords = 0;
            invoiceListSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "SIId", type: "int" },
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "PayDept", type: "int" },
                   { name: "DeptName", type: "string" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "CurrencyId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "OutCorpId", type: "int" },
                   { name: "OutCorpName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "LastBala", type: "number" },
                   { name: "ApplyBala", type: "number" }
                ],
                sort: function () {
                    $("#jqxInvoiceListGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/PayApplyInvoiceListHandler.ashx?ids="+initIds
            };
            var invoiceListDataAdapter = new $.jqx.dataAdapter(invoiceListSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + row + ");\" />"
                   + "</div>";
            }

            $("#jqxInvoiceListGrid").jqxGrid(
            {
                width: "98%",
                source: invoiceListDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                selectionmode: "singlecell",
                sortable: true,
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "发票号", datafield: "InvoiceNo", editable: false },
                  { text: "开票公司", datafield: "OutCorpName", editable: false },
                  { text: "收票公司", datafield: "InCorpName", editable: false },
                  { text: "成本部门", datafield: "DeptName", editable: false },
                  { text: "发票金额", datafield: "InvoiceBala", editable: false },
                  { text: "未申请金额", datafield: "LastBala", editable: false },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  {
                      text: "申请金额", datafield: "ApplyBala", columntype: "numberinput", cellclassname: "GridFillNumber", width: 120, sortable: false, validation: function (cell, value) {
                          var item = $("#jqxInvoiceListGrid").jqxGrid("getrowdata", cell.row);
                          if (value < 0 || value > item.LastBala) {
                              return { result: false, message: "申请金额必须大于0且小于等于" + item.LastBala };
                          }
                          return true;
                      }, createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, width: 120, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "操作", datafield: "SIId", cellsrenderer: addRender, enabletooltips: false, sortable: false, editable: false }
                ]
            });


            //付款申请主体
            //申请日期
            $("#txbApplyDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#selApplyDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", height: 25, width: 120, disabled: true });

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            //收款公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = {
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120
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
            $("#txbApplyBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //最后付款日
            $("#txbPayDeadline").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayMatterStyle%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#selPayMatter").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            $("#selPayMatter").val(4);

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //备注
            $("#txbMemo").jqxInput({ height: 23 });
            $("#txbSpecialDesc").jqxInput({ height: 23 });

            //控件赋值
            var tempDate = new Date("<%=this.curApply.ApplyTime.ToString("yyyy/MM/dd")%>");
            $("#txbApplyDate").jqxDateTimeInput({ value: tempDate });

            $("#selApplyDept").val(<%=this.curApply.ApplyDept%>);
            $("#selApplyCorp").val(<%=this.curApply.ApplyCorp%>);
            $("#selRecCorp").val(<%=this.curPayApply.RecCorpId%>);

            $("#selBank").val(<%=this.curPayApply.RecBankId%>);

            if(<%=this.curPayApply.RecBankAccountId%> >0){
                $("#selBankAccount").val(<%=this.curPayApply.RecBankAccountId%>);
            }

            $("#txbBankAccount").val("<%=this.curPayApply.RecBankAccount%>");
            $("#txbApplyBala").val(<%=this.curPayApply.ApplyBala%>);
            $("#selCurrency").val(<%=this.curPayApply.CurrencyId%>);

            tempDate = new Date("<%=this.curPayApply.PayDeadline.ToString("yyyy/MM/dd")%>");
            $("#txbPayDeadline").jqxDateTimeInput({ value: tempDate });

            $("#selPayMatter").val(<%=this.curPayApply.PayMatter%>);
            $("#selPayMode").val(<%=this.curPayApply.PayMode%>);

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
                        }
                    ]
            });

            //新增
            $("#btnUpdate").click(function () {

                var isCanSubmit = $("#jqxPayApplyExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (selects.length == 0) { alert("未选中任务发票"); return; }

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
                    RecCorpId: $("#selRecCorp").val(),
                    RecBankId: $("#selBank").val(),
                    RecBankAccountId: accId,
                    RecBankAccount: $("#txbBankAccount").val(),
                    ApplyBala: $("#txbApplyBala").val(),
                    CurrencyId: $("#selCurrency").val(),
                    PayMode: $("#selPayMode").val(),
                    PayDeadline: $("#txbPayDeadline").val(),
                    PayMatter: $("#selPayMatter").val(),
                    SpecialDesc: $("#txbSpecialDesc").val()
                };

                $.post("Handler/PayApplyInvoiceUpdateHandler.ashx", { Apply: JSON.stringify(apply), PayApply: JSON.stringify(payApply), Details: JSON.stringify(selects) },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PayApplyList.aspx";
                        }
                    }
                );
            });
        });

                var currencyId = 0;
                var payDetp = 0;
                var outCorpId = 0;

                function bntAddOnClick(row) {

                    var item = $("#jqxInvoiceListGrid").jqxGrid("getrowdata", row);
                    if (isNaN(item.ApplyBala) || item.ApplyBala == 0) {
                        alert("必须填写申请金额");
                        return;
                    }

                    //验证 成本部门，对方抬头，币种
                    if (currencyId == 0) { currencyId = item.CurrencyId; }
                    if (payDetp == 0) { payDetp = item.PayDept; }
                    if (outCorpId == 0) { outCorpId = item.OutCorpId; }

                    if (currencyId != item.CurrencyId) { alert("选中发票币种不一致"); return; }
                    if (payDetp != item.PayDept) { alert("选中发票成本部门不一致"); return; }
                    if (outCorpId != item.OutCorpId) { alert("选中发票开票公司不一致"); return; }

                    selects.push(item);
                    selectSource.localdata = selects;
                    $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

                    var ids = "";
                    for (i = 0; i < selects.length; i++) {
                        var temp = selects[i];
                        if (!isNaN(temp.SIId)) {
                            if (i > 0) { ids += ","; }
                            ids += temp.SIId;
                        }
                    }

                    var dids ="";
                    for(i=0;i<removeDetails.length;i++){
                        if(i!=0){ dids +=",";}
                        dids+=removeDetails[i].RefId;
                    }

                    invoiceListSource.url = "Handler/PayApplyInvoiceListHandler.ashx?ids=" + ids+"&refIds="+ dids;
                    $("#jqxInvoiceListGrid").jqxGrid("updatebounddata", "rows");

                    var nowBala = $("#txbApplyBala").jqxNumberInput("val");
                    var newBala = nowBala + item.ApplyBala;
                    $("#txbApplyBala").jqxNumberInput("val", newBala);
                }

                function bntRemoveOnClick(row) {
                    var item = $("#jqxSelectGrid").jqxGrid("getrowdata", row);

                    selects.splice(row,1);
                    if(item.RefId!=undefined && item.RefId>0){
                        removeDetails.push(item);
                    }

                    if (selects.length == 0) {
                        currencyId = 0;
                        payDetp = 0;
                        outCorpId = 0;
                    }

                    selectSource.localdata = selects;
                    $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

                    var ids = "";
                    for (i = 0; i < selects.length; i++) {
                        var temp = selects[i];
                        if (!isNaN(temp.SIId)) {
                            if (i > 0) { ids += ","; }
                            ids += temp.SIId;
                        }
                    }

                    var dids ="";
                    for(i=0;i<removeDetails.length;i++){
                        if(i!=0){ dids +=",";}
                        dids+=removeDetails[i].RefId;
                    }

                    invoiceListSource.url = "Handler/PayApplyInvoiceListHandler.ashx?ids=" + ids+"&refIds="+ dids;
                    $("#jqxInvoiceListGrid").jqxGrid("updatebounddata", "rows");
            
                    var nowBala = $("#txbApplyBala").jqxNumberInput("val");
                    var newBala = nowBala - item.ApplyBala;
                    $("#txbApplyBala").jqxNumberInput("val", newBala);
                }

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxSelectExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            已申请付款价外票列表
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxInvoiceListExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可申请付款价外票列表
        </div>
        <div>
            <div id="jqxInvoiceListGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
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

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="修改付款申请" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />
    </div>
</body>
</html>
