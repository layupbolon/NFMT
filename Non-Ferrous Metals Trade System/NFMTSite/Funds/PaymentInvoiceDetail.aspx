<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentInvoiceDetail.aspx.cs" Inherits="NFMTSite.Funds.PaymentInvoiceDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>财务付款明细--发票关联</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    
    <script type="text/javascript">
        
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curPayment.DataBaseName%>" + "&t=" + "<%=this.curPayment.TableName%>" + "&id=" + "<%=this.curPayment.PaymentId%>";
        
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
                  { text: "申请金额", datafield: "ApplyBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "付款实际金额", datafield: "FundsBala" },
                  { text: "付款虚拟金额", datafield: "VirtualBala" }
                ]
            });           

            //init payment control
            $("#txbPayDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120,disabled:true });

            //付款公司            
            var payCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var payCorpSource = {
                datatype: "json", url: payCorpUrl, async: false
            };
            var payCorpDataAdapter = new $.jqx.dataAdapter(payCorpSource);
            $("#selPayCorp").jqxComboBox({
                source: payCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25,disabled:true
            });            

            //付款银行            
            var payBankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var payBankSource = { datatype: "json", url: payBankUrl, async: false };
            var payBankDataAdapter = new $.jqx.dataAdapter(payBankSource);
            $("#selPayBank").jqxComboBox({ source: payBankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120,disabled:true });

            //付款账户
            var payAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selPayCorp").val() + "&b=" + $("#selPayBank").val();
            var payAccountSource = { datatype: "json", url: payAccountUrl, async: false };
            var payAccountDataAdapter = new $.jqx.dataAdapter(payAccountSource);
            $("#selPayAccount").jqxComboBox({ source: payAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25,disabled:true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#selCurrency").val(<%=this.curPayApply.CurrencyId%>);

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true,disabled:true });

            //付款总额
            $("#txbpayBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });

            //财务付款金额
            $("#txbFundsPayBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true,disabled:true });

            //收款公司
            var recCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var recCorpSource = {
                datatype: "json", url: recCorpUrl, async: false
            };
            var recCorpDataAdapter = new $.jqx.dataAdapter(recCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: recCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120,disabled:true
            });

            //收款开户行
            var recBankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var recBankSource = { datatype: "json", url: recBankUrl, async: false };
            var recBankDataAdapter = new $.jqx.dataAdapter(recBankSource);
            $("#selRecBank").jqxComboBox({ source: recBankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120,disabled:true });

            //收款账号
            var recAccountUrl = "../BasicData/Handler/BankAccountDDLHandler.ashx?c=" + $("#selRecCorp").val() + "&b=" + $("#selRecBank").val();
            var recAccountSource = { datatype: "json", url: recAccountUrl, async: false };
            var recAccountDataAdapter = new $.jqx.dataAdapter(recAccountSource);
            $("#selRecAccount").jqxComboBox({ source: recAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120,disabled:true });
            
            $("#txbRecAccount").jqxInput({ height: 23,disabled:true });            

            //付款流水
            $("#txbPayLog").jqxInput({ height: 23,disabled:true });

            //备注
            $("#txbMemo").jqxInput({ height:23,disabled:true });

            //虚拟付款
            $("#chkVirtual").jqxCheckBox({  checked: false,disabled:true });
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

            if(<%=this.curPaymentVirtual.VirtualId%> > 0){
                $("#chkVirtual").val(true);
                $("#txbVirtualBala").val(<%=this.curPaymentVirtual.PayBala%>);
            }

            $("#txbFundsPayBala").val(<%=this.curPayment.FundsBala%>);

            //buttons
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();
            $("#btnClose").jqxInput(); 

            $("#btnAudit").on("click", function (e) {                
                var paras = {
                    mid: 15,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/PaymentStatusHandler.ashx", { pi: "<%=this.curPayment.PaymentId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PaymentList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/PaymentStatusHandler.ashx", { pi: "<%=this.curPayment.PaymentId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PaymentList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/PaymentStatusHandler.ashx", { pi: "<%=this.curPayment.PaymentId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PaymentList.aspx";
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/PaymentStatusHandler.ashx", { pi: "<%=this.curPayment.PaymentId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PaymentList.aspx";
                    }
                );
            });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/PaymentStatusHandler.ashx", { pi: "<%=this.curPayment.PaymentId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PaymentList.aspx";
                    }
                );
            });

        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />
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

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="PaymentAttach" />

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnClose" value="关闭" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;     
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
