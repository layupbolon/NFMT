<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentInvoiceUpdate.aspx.cs" Inherits="NFMTSite.Funds.PaymentInvoiceUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务付款修改--发票关联</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var stockDetails = new Array();

        $(document).ready(function () {
            $("#jqxInvoiceExpander").jqxExpander({ width: "98%" });
            $("#jqxPayApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxPaymentExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;
            var selectSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "PayApplyDetailId", type: "int" },
                   { name: "PayApplyId", type: "int" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "ApplyBala", type: "number" },
                   { name: "LastBala", type: "number" },                   
                   { name: "FundsBala", type: "number" },
                   { name: "VirtualBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "PayMatterName", type: "string" },
                   { name: "PayModeName", type: "string" },
                   { name: "DeptName", type: "string" }
                ],
                sort: function () {
                    $("#jqxInvoiceGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "ipar.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ipar.RefId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.SelectedJson%>
                };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
           
            $("#jqxInvoiceGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
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
                  { text: "申请金额", datafield: "ApplyBala", editable: false },
                  { text: "未付金额", datafield: "LastBala", editable: false },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  {
                      text: "付款实际金额", datafield: "FundsBala", width: 120, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber", 
                      validation: function (cell, value) {
                          var item = selectDataAdapter.records[cell.row];
                          if (value < 0 || value >item.LastBala) {
                              return { result: false, message: "付款金额不能小于0且大于申请金额" + item.LastBala };
                          }
                          return true;
                      }, 
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, width: 120, Digits: 8, spinButtons: true });
                      }
                  },
                  { 
                      text: "付款虚拟金额", datafield: "VirtualBala", width: 120, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber", 
                      validation: function (cell, value) {
                          var item = selectDataAdapter.records[cell.row];
                          if (value < 0 || value >item.LastBala) {
                              return { result: false, message: "付款金额不能小于0且大于申请金额" + item.LastBala };
                          }
                          return true;
                      }, 
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 2, width: 120, Digits: 8, spinButtons: true });
                      }
                  }
                ]
            });

            stockDetails = selectDataAdapter.records;

            $("#jqxInvoiceGrid").on("cellvaluechanged", function (event) 
            {
                var column = args.datafield;
                if(column =="FundsBala"){
                    var newvalue = args.newvalue;
                    var oldvalue = args.oldvalue;
                    if(oldvalue == undefined){ oldvalue =0;}
                    var value = $("#txbpayBala").val() - oldvalue + newvalue;
                    $("#txbpayBala").val(value);

                    //
                    var sumBala = 0;
                    var vBala =0;
                    if($("#chkVirtual").val()){
                        vBala =  $("#txbVirtualBala").val();
                    }
                    sumBala =  $("#txbpayBala").val();
                
                    var fundsBala = parseFloat(sumBala) - parseFloat(vBala);
                    $("#txbFundsPayBala").val(fundsBala);
                }
                else if(column == "VirtualBala"){
                    var newvalue = args.newvalue;
                    var oldvalue = args.oldvalue;
                    if(oldvalue == undefined){ oldvalue =0;}
                    var value = $("#txbpayBala").val() - oldvalue + newvalue;
                    $("#txbpayBala").val(value);

                    //
                    var sumBala = 0;
                    var fBala = $("#txbFundsPayBala").val();                  
                    sumBala =  $("#txbpayBala").val();
                    $("#txbVirtualBala").val(sumBala -fBala );
                }
            });

            //init payment control
            $("#txbPayDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //付款公司            
            var payCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?isSelf=1";
            var payCorpSource = {
                datatype: "json", url: payCorpUrl, async: false
            };
            var payCorpDataAdapter = new $.jqx.dataAdapter(payCorpSource);
            $("#selPayCorp").jqxComboBox({
                source: payCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25
            });

            $("#selPayCorp").on("change", function (event) {
                var args = event.args;
                if (args) {                    
                    var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
                    var tempSource = { datatype: "json", url: tempUrl, async: false };
                    var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                    $("#selPayAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25 });
                }
            });

            //付款银行            
            var payBankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var payBankSource = { datatype: "json", url: payBankUrl, async: false };
            var payBankDataAdapter = new $.jqx.dataAdapter(payBankSource);
            $("#selPayBank").jqxComboBox({ source: payBankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120 });

            $("#selPayBank").on("change", function (event) {
                var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
                var tempSource = { datatype: "json", url: tempUrl, async: false };
                var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                $("#selPayAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25 });
            });

            //付款账户
            var payAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
            var payAccountSource = { datatype: "json", url: payAccountUrl, async: false };
            var payAccountDataAdapter = new $.jqx.dataAdapter(payAccountSource);
            $("#selPayAccount").jqxComboBox({ source: payAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25 });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#selCurrency").val(<%=this.curPayApply.CurrencyId%>);

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //付款总额
            $("#txbpayBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });

            //财务付款金额
            $("#txbFundsPayBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });
            
            var sumLastBala =0;
            for(k=0;k<stockDetails.length;k++){
                sumLastBala += stockDetails[k].LastBala;
            }

            $("#txbpayBala").val(sumLastBala);
            $("#txbFundsPayBala").val(sumLastBala);

            //收款公司
            var recCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var recCorpSource = {
                datatype: "json", url: recCorpUrl, async: false
            };
            var recCorpDataAdapter = new $.jqx.dataAdapter(recCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: recCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120
            });
            $("#selRecCorp").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var index = args.index;
                    var obj = recCorpDataAdapter.records[index];
                    $("#spnSelRecCorpFullName").html(obj.CorpFullName);

                    var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
                    var tempSource = { datatype: "json", url: tempUrl, async: false };
                    var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                    $("#selRecAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
                }
            });

            //收款开户行
            var recBankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var recBankSource = { datatype: "json", url: recBankUrl, async: false };
            var recBankDataAdapter = new $.jqx.dataAdapter(recBankSource);
            $("#selRecBank").jqxComboBox({ source: recBankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120 });
            $("#selRecBank").on("change", function (event) {

                var tempUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
                var tempSource = { datatype: "json", url: tempUrl, async: false };
                var tempDataAdapter = new $.jqx.dataAdapter(tempSource);
                $("#selRecAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
            });

            //收款账号
            var recAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
            var recAccountSource = { datatype: "json", url: recAccountUrl, async: false };
            var recAccountDataAdapter = new $.jqx.dataAdapter(recAccountSource);
            $("#selRecAccount").jqxComboBox({ source: recAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120 });
            $("#selRecAccount").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var item = args.item;
                    $("#txbRecAccount").val(item.label);
                }
            });

            $("#txbRecAccount").jqxInput({ height: 23 });            

            //付款流水
            $("#txbPayLog").jqxInput({ height: 23 });

            //备注
            $("#txbMemo").jqxInput({ height:23 });

            //虚拟付款
            $("#chkVirtual").jqxCheckBox({  checked: false});
            $("#txbVirtualBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });

            //init control data
            var tempDate = new Date("<%=this.curPayment.PayDatetime.ToString("yyyy/MM/dd")%>");
            $("#txbPayDate").jqxDateTimeInput({ value: tempDate });

            $("#selPayCorp").val(<%=this.curPayment.PayCorp%>);
            $("#selPayBank").val(<%=this.curPayment.PayBankId%>);
            $("#selPayAccount").val(<%=this.curPayment.PayBankAccountId%>);
            $("#selPayMode").val(<%=this.curPayment.PayStyle%>);
            $("#txbpayBala").val(<%=this.curPayment.PayBala%>);
            $("#selRecCorp").val(<%=this.curPayment.RecevableCorp%>);
            $("#selRecBank").val(<%=this.curPayment.ReceBankId%>);
            
            if(<%=this.curPayment.ReceBankAccountId%> > 0){
                $("#selRecAccount").val(<%=this.curPayment.ReceBankAccountId%>);
            }
            $("#txbRecAccount").val("<%=this.curPayment.ReceBankAccount%>");
            $("#txbPayLog").val("<%=this.curPayment.FlowName%>");
            $("#txbMemo").val("<%=this.curPayment.Memo%>");

            $("#chkVirtual").on("change", function (event) { 
                var checked = event.args.checked;
                $("#txbVirtualBala").jqxNumberInput({disabled:!checked});
                if(!checked){ $("#txbVirtualBala").val(0);}
            });

            if(<%=this.curPaymentVirtual.VirtualId%> > 0){
                $("#chkVirtual").val(true);
                $("#txbVirtualBala").val(<%=this.curPaymentVirtual.PayBala%>);
            }

            $("#txbFundsPayBala").val(<%=this.curPayment.FundsBala%>);

            $("#txbFundsPayBala").on("valueChanged", function (event){
               
                var sumBala = 0;
                var vBala =0;
                var fBala =0;

                if($("#chkVirtual").val()){
                    vBala =  $("#txbVirtualBala").val();
                }

                fBala = $("#txbFundsPayBala").val();

                sumBala =  vBala + fBala;
                $("#txbpayBala").val(sumBala);
               
                for(i=0;i<stockDetails.length;i++){

                    var item = stockDetails[i];
                    var fundsBala =0;
                    var vitualPayBala =0;

                    if(fBala >= item.LastBala){
                        fundsBala = item.LastBala;
                        vitualPayBala = 0;
                    }
                    else{
                        fundsBala = fBala;
                        if(vBala > item.LastBala - fundsBala){
                            vitualPayBala = item.LastBala - fundsBala;
                        }
                        else{
                            vitualPayBala = vBala;
                        }
                    }

                    fBala -= fundsBala;
                    vBala -= vitualPayBala;
                    item.FundsBala = fundsBala;
                    item.VirtualBala = vitualPayBala;                    
                }

                selectSource.localdata = stockDetails;
                $("#jqxInvoiceGrid").jqxGrid("updatebounddata", "rows");

            });

            $("#txbVirtualBala").on("valueChanged", function (event){
               
                var sumBala = 0;
                var vBala =0;
                var fBala =0;

                if($("#chkVirtual").val()){
                    vBala =  $("#txbVirtualBala").val();
                }

                fBala = $("#txbFundsPayBala").val();

                sumBala =  vBala + fBala;
                $("#txbpayBala").val(sumBala);
               
                for(i=0;i<stockDetails.length;i++){

                    var item = stockDetails[i];
                    var fundsBala =0;
                    var vitualPayBala =0;

                    if(fBala >= item.LastBala){
                        fundsBala = item.LastBala;
                        vitualPayBala = 0;
                    }
                    else{
                        fundsBala = fBala;
                        if(vBala > item.LastBala - fundsBala){
                            vitualPayBala = item.LastBala - fundsBala;
                        }
                        else{
                            vitualPayBala = vBala;
                        }
                    }

                    fBala -= fundsBala;
                    vBala -= vitualPayBala;
                    item.FundsBala = fundsBala;
                    item.VirtualBala = vitualPayBala;                    
                }

                selectSource.localdata = stockDetails;
                $("#jqxInvoiceGrid").jqxGrid("updatebounddata", "rows");

            });

            //验证
            $("#jqxPayApplyExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#selPayCorp", message: "付款公司必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selPayCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selPayBank", message: "付款银行必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selPayBank").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selPayAccount", message: "付款账户", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selPayAccount").jqxComboBox("val") > 0;
                            }
                        },                        
                        {
                            input: "#txbpayBala", message: "付款金额大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbpayBala").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#selRecCorp", message: "收款公司必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selRecCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#selRecBank", message: "收款开户行必选", action: "keyup,blur", rule: function (input, commit) {
                                return $("#selRecBank").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbRecAccount", message: "收款银行账户必填", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbRecAccount").val().length > 0;
                            }
                        },
                        {
                            input: "#txbVirtualBala", message: "虚拟付款金额必须小于或等于付款总额", action: "keyup,blur", rule: function (input, commit) {
                                if(!$("#chkVirtual").val()){ return true;}
                                return $("#txbpayBala").val()>=$("#txbVirtualBala").val(); 
                            }
                        }
                    ]
            });
            
            //buttons
            $("#btnUpdate").jqxButton();
            $("#btnCancel").jqxButton();

            //新增
            $("#btnUpdate").click(function () {

                var isCanSubmit = $("#jqxPayApplyExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认修改财务付款？")) { return; }

                var recAccountId = 0;
                if ($("#selRecAccount").val() > 0) { recAccountId = $("#selRecAccount").val(); }
                var payAccountId=0;
                if ($("#selPayAccount").val() > 0) { payAccountId = $("#selPayAccount").val(); }

                var virtualBala =0;
                if($("#chkVirtual").val()){
                    virtualBala =  $("#txbVirtualBala").val();
                }

                var payment = {
                    PaymentId:"<%=this.curPayment.PaymentId%>",
                    PayBala: $("#txbpayBala").val(),
                    FundsBala:$("#txbFundsPayBala").val(),
                    VirtualBala:virtualBala,
                    CurrencyId: $("#selCurrency").val(),
                    PayStyle: $("#selPayMode").val(),
                    PayBankId: $("#selPayBank").val(),
                    PayBankAccountId: payAccountId,
                    PayCorp: $("#selPayCorp").val(),
                    PayDatetime: $("#txbPayDate").val(),
                    RecevableCorp: $("#selRecCorp").val(),
                    ReceBankId: $("#selRecBank").val(),
                    ReceBankAccountId:recAccountId,
                    ReceBankAccount:$("#txbRecAccount").val(),
                    FlowName: $("#txbPayLog").val(),
                    Memo: $("#txbMemo").val()
                };

                var rows = $("#jqxInvoiceGrid").jqxGrid("getrows");                

                $.post("Handler/PaymentInvoiceUpdateHandler.ashx", { Payment: JSON.stringify(payment), Details: JSON.stringify(rows) },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PaymentList.aspx";
                        }
                    }
                );

            });

        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxInvoiceExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款申请明细
        </div>
        <div>
            <div id="jqxInvoiceGrid" style="float: left; margin: 5px 0 0 5px;"></div>
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
                    <span id="spnApplyDate" style="float: left;" runat="server"></span>
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <span id="spnApplyDept" style="float: left;" runat="server" />
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <span id="spnRecCorp" style="float: left;" runat="server" />
                </li>
                <li>
                    <strong>收款人全称：</strong>
                    <span runat="server" id="spnRecCorpFullName"></span>
                </li>
                <li>
                    <strong>收款开户行：</strong>
                    <span id="spnBank" style="float: left;" runat="server" />
                </li>
                <li>
                    <strong>收款账号：</strong>
                    <span id="spnBankAccount" style="float: left;" runat="server" />
                </li>
                <li>
                    <strong>申请金额：</strong>
                    <span style="float: left" id="spnApplyBala" runat="server"></span>
                </li>
                <li>
                    <strong>币种：</strong>
                    <span id="spnCurrency" style="float: left;" runat="server" />
                </li>
                <li>
                    <strong>最后付款日：</strong>
                    <span id="spnPayDeadline" style="float: left;" runat="server"></span>
                </li>
                <li>
                    <strong>付款事项：</strong>
                    <span style="float: left" id="spnPayMatter" runat="server"></span>
                </li>
                <li>
                    <strong>付款方式：</strong>
                    <span style="float: left" id="spnPayMode" runat="server"></span>
                </li>
                <li>
                    <strong>备注：</strong>
                    <span id="spnMemo" runat="server"></span>
                </li>
                <li>
                    <strong>特殊附言：</strong>
                    <span id="spnSpecialDesc" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxPaymentExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>付款日期：</strong>
                    <div id="txbPayDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款公司：</strong>
                    <div id="selPayCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款银行：</strong>
                    <div id="selPayBank" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款账户：</strong>
                    <div id="selPayAccount" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="selCurrency" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款方式：</strong>
                    <div id="selPayMode" style="float: left;"></div>
                </li>
                <li>
                    <strong>付款总额：</strong>
                    <div id="txbpayBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>财务付款金额：</strong>
                    <div id="txbFundsPayBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>虚拟付款：</strong>
                    <div id="chkVirtual" style="float: left;"></div>
                </li>
                <li>
                    <strong>虚拟金额：</strong>
                    <div id="txbVirtualBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <div id="selRecCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>收款人全称：</strong>
                    <span runat="server" id="spnSelRecCorpFullName"></span>
                </li>
                <li>
                    <strong>收款开户行：</strong>
                    <div id="selRecBank" style="float: left;" />
                </li>
                <li>
                    <strong>收款账号：</strong>
                    <div id="selRecAccount" style="float: left;" />
                </li>
                <li>
                    <input type="text" id="txbRecAccount" />
                </li>
                <li>
                    <strong>付款流水号：</strong>
                    <input id="txbPayLog" style="float: left;" type="text" />
                </li>
                <li>
                    <strong>备注：</strong>
                    <input id="txbMemo" style="float: left;" type="text" />
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="PaymentAttach" />

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="修改付款" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
