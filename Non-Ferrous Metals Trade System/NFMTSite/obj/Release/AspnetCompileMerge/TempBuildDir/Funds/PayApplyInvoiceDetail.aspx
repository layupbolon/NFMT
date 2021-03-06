﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyInvoiceDetail.aspx.cs" Inherits="NFMTSite.Funds.PayApplyInvoiceDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请明细--关联发票</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curApply.DataBaseName%>" + "&t=" + "<%=this.curApply.TableName%>" + "&id=" + "<%=this.curApply.ApplyId%>";

        var selectSource;
        $(document).ready(function () {
            

            $("#jqxSelectExpander").jqxExpander({ width: "98%" });
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
                  { text: "申请金额", datafield: "ApplyBala" }
                ]
            });            

            //申请日期
            $("#txbApplyDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true});

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
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase",disabled:true
            });

            //收款公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = {
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120, disabled: true
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
                    $("#selBankAccount").jqxComboBox({ source: tempDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120, disabled: true });
                }
            });

            //开户行
            var bankUrl = "../BasicData/Handler/BankDDLHandler.ashx";
            var bankSource = { datatype: "json", url: bankUrl, async: false };
            var bankDataAdapter = new $.jqx.dataAdapter(bankSource);
            $("#selBank").jqxComboBox({ source: bankDataAdapter, displayMember: "BankName", valueMember: "BankId", height: 25, width: 120, disabled: true });
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
            $("#selBankAccount").jqxComboBox({ source: bankAccountDataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", height: 25, width: 120, disabled: true });
            $("#selBankAccount").on("change", function (event) {
                var args = event.args;
                if (args) {
                    var item = args.item;
                    $("#txbBankAccount").val(item.label);
                }
            });

            $("#txbBankAccount").jqxInput({ height: 23, disabled: true });

            //申请金额
            $("#txbApplyBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            //最后付款日
            $("#txbPayDeadline").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayMatterStyle%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#selPayMatter").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            $("#selPayMatter").val(4);

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23, disabled: true });
            $("#txbSpecialDesc").jqxInput({ height: 23, disabled: true });

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
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnConfirm").jqxInput();
            $("#btnConfirmCancel").jqxInput();
            $("#btnClose").jqxInput();            

            $("#btnAudit").on("click", function (e) {                
                var paras = {
                    mid: 41,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/PayApplyStatusHandler.ashx", { pi: "<%=this.curPayApply.PayApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PayApplyList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/PayApplyStatusHandler.ashx", { pi: "<%=this.curPayApply.PayApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PayApplyList.aspx";
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.确认完成;
                $.post("Handler/PayApplyStatusHandler.ashx", { pi: "<%=this.curPayApply.PayApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PayApplyList.aspx";
                    }
                );
            });

            $("#btnConfirmCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.确认完成撤销;
                $.post("Handler/PayApplyStatusHandler.ashx", { pi: "<%=this.curPayApply.PayApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PayApplyList.aspx";
                    }
                );
            });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/PayApplyStatusHandler.ashx", { pi: "<%=this.curPayApply.PayApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "PayApplyList.aspx";
                    }
                );
            });

        });

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />
    <div id="jqxSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已申请付款价外票列表
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
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

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnClose" value="关闭" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
