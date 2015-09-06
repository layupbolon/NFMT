<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayApplyDetail.aspx.cs" Inherits="NFMTSite.Funds.PayApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款申请明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">

        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curApply.DataBaseName%>" + "&t=" + "<%=this.curApply.TableName%>" + "&id=" + "<%=this.curApply.ApplyId%>";

        $(document).ready(function () {
            
            $("#jqxConExpander").jqxExpander({ width: "98%" });
            //$("#jqxAuditInfoExpander").jqxExpander({ width: "98%" });            
            $("#jqxPayApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxAttachGridExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;
            //已选库存流水信息
            $("#jqxStockAppsExpander").jqxExpander({ width: "98%" });
            var stockAppSource =
            {
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "sl.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sl.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.StockDetailsJson%>,
                datatype: "json"
            };
            var stockAppDataAdapter = new $.jqx.dataAdapter(stockAppSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxStockAppsGrid").jqxGrid(
            {
                width: "98%",
                autoheight: true,
                source: stockAppDataAdapter,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "OwnCorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "单位", datafield: "MUName" },
                  { text: "净量", datafield: "NetAmount" },
                  { text: "申请金额", datafield: "ApplyBala" }
                ]
            });

            //付款明细列表
            $("#jqxPaymentExpander").jqxExpander({ width: "98%" });
            formatedData = "";
            totalrecords = 0;
            var paymentSource =
            {
                datafields:
                [
                   { name: "PaymentId", type: "int" },
                   { name: "PaymentCode", type: "string" },
                   { name: "PayDatetime", type: "date" },
                   { name: "ApplyNo", type: "string" },
                   { name: "RecevableCorpName", type: "string" },
                   { name: "PayStyleName", type: "string" },
                   { name: "PayBala", type: "number" },
                   { name: "CurrencyName", type: "int" },
                   { name: "PayEmpName", type: "string" },
                   { name: "PaymentStatusName", type: "string" },
                   { name: "PayApplySource", type: "int" },
                   { name: "PaymentStatus", type: "int" }
                ],
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "pay.PaymentId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pay.PaymentId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.PaymentJson%>,
                datatype: "json"
            };
            var paymentDataAdapter = new $.jqx.dataAdapter(paymentSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxPaymentGrid").jqxGrid(
            {
                width: "98%",
                autoheight: true,
                source: paymentDataAdapter,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "付款编号", datafield: "PaymentCode" },
                  { text: "付款时间", datafield: "PayDatetime", cellsformat: "yyyy-MM-dd" },
                  { text: "收款公司", datafield: "RecevableCorpName" },
                  { text: "付款方式", datafield: "PayStyleName" },
                  { text: "付款金额", datafield: "PayBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "支付人", datafield: "PayEmpName" },
                  { text: "付款状态", datafield: "PaymentStatusName" }
                ]
            });

            //申请日期
            $("#txbApplyDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#selApplyDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", height: 25, width: 120, disabled: true });
            $("#selApplyDept").val(<%=this.curUser.DeptId%>);

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId=" + "<%=this.curSub.SubId%>";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });

            //收款公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curSub.SubId%>";
            var outCorpSource = {
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selRecCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, width: 120, searchMode: "containsignorecase", disabled: true
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
            $("#txbApplyBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //最后付款日
            $("#txbPayDeadline").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 , disabled: true });

            //付款事项
            var payMatterSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayMatterStyle%>", async: false };
            var payMatterDataAdapter = new $.jqx.dataAdapter(payMatterSource);
            $("#selPayMatter").jqxDropDownList({ source: payMatterDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 180, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //付款方式
            var payModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.PayModeStyle%>", async: false };
            var payModeDataAdapter = new $.jqx.dataAdapter(payModeSource);
            $("#selPayMode").jqxDropDownList({ source: payModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23, disabled: true });
            $("#txbSpecialDesc").jqxInput({ height: 23, disabled: true });
            
            //init data to control
            var tempDate = new Date("<%=this.curApply.ApplyTime.ToString("yyyy-MM-dd")%>");
            $("#txbApplyDate").jqxDateTimeInput({ value: tempDate });
            $("#selApplyCorp").val(<%=this.curApply.ApplyCorp%>);
            $("#selRecCorp").val(<%=this.curPayApply.RecCorpId%>);
            $("#selBank").val(<%=this.curPayApply.RecBankId%>);

            if(<%=this.curPayApply.RecBankAccountId%> >0){
                $("#selBankAccount").val(<%=this.curPayApply.RecBankAccountId%>);
            }

            $("#txbBankAccount").val("<%=this.curPayApply.RecBankAccount%>");
            $("#txbApplyBala").val(<%=this.curPayApply.ApplyBala%>);
            $("#selCurrency").val(<%=this.curPayApply.CurrencyId%>);

            tempDate = new Date("<%=this.curPayApply.PayDeadline.ToString("yyyy-MM-dd")%>");
            $("#txbPayDeadline").jqxDateTimeInput({ value: tempDate });
            $("#selPayMode").val(<%=this.curPayApply.PayMode%>);
            $("#selPayMatter").val(<%=this.curPayApply.PayMatter%>);
            $("#txbMemo").val("<%=this.curApply.ApplyDesc%>");
            $("#txbSpecialDesc").val("<%=this.curPayApply.SpecialDesc%>");

            var attachUrl = "Handler/PayApplyAttachHandler.ashx?id=<%=this.curPayApply.PayApplyId%>";

            var attachFormatedData = "";
            var attachTotalrecords = 0;
            attachSource =
            {
                url: attachUrl,
                datafields: [
                    { name: "PayApplyAttachId", type: "int" },
                    { name: "PayApplyId", type: "int" },
                    { name: "AttachId", type: "int" },
                    { name: "AttachType", type: "int" },
                    { name: "DetailName", type: "string" },
                    { name: "AttachName", type: "string" },
                    { name: "AttachInfo", type: "string" },
                    { name: "CreateTime", type: "date" },
                    { name: "AttachPath", type: "string" },
                    { name: "AttachExt", type: "string" },
                    { name: "AttachStatus", type: "int" },
                    { name: "ServerAttachName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxAttachGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    attachTotalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "pa.PayApplyAttachId",
                sortdirection: "asc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pa.PayApplyAttachId";
                    data.sortorder = data.sortorder || "asc";
                    data.filterscount = data.filterscount || 0;
                    attachFormatedData = buildQueryString(data);
                    return attachFormatedData;
                }
            };

            var attachDataAdapter = new $.jqx.dataAdapter(attachSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var attachViewRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">";
                cellHtml += "<a href=\"../Files/FileDownLoad.aspx?id=" + item.AttachId + "\" title=\"" + item.AttachName + "\" >下载</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxAttachGrid").jqxGrid(
            {
                width: "98%",
                source: attachDataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "附件类型", datafield: "DetailName" },
                  { text: "添加日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "附件名字", datafield: "AttachName" },
                  { text: "查看", datafield: "AttachPath", cellsrenderer: attachViewRender }
                ]
            });

            //buttons
            $("#btnAudit").jqxButton();
            $("#btnInvalid").jqxButton();
            $("#btnGoBack").jqxButton();
            $("#btnConfirm").jqxButton();
            $("#btnConfirmCancel").jqxButton();
            $("#btnExport").jqxButton();

            $("#btnAudit").on("click", function (e) {                
                var paras = {
                    mid: 47,
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

            $("#btnExport").on("click", function () {
                $("#jqxConExpander").jqxExpander("expand");
                window.print();
            });
           
        });        

    </script>
</head>
<body>

    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" /> 

    <div id="jqxStockAppsExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择付款库存
        </div>
        <div>
            <div id="jqxStockAppsGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
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
                    <div id="selApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="selApplyCorp"></div>
                </li>
                <li>
                    <strong>收款公司：</strong>
                    <div id="selRecCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>收款人全称：</strong>
                    <span runat="server" id="spnRecCorpFullName"></span>
                </li>
                <li>
                    <strong>收款开户行：</strong>
                    <div id="selBank" style="float: left;"></div>
                </li>
                <li>
                    <strong>收款账号：</strong>
                    <div id="selBankAccount" style="float: left;"></div>
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
                    <div id="selCurrency" style="float: left;"></div>
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

    <div id="jqxAttachGridExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>附件信息</div>
        <div>
            <div id="jqxAttachGrid"></div>
        </div>
    </div>

     <%--<div id="jqxAuditInfoExpander" runat="server" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款申请审核信息
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <span id="txbAuditInfo" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>--%>

     <div id="jqxPaymentExpander" runat="server" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款明细
        </div>
        <div>
            <div id="jqxPaymentGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;
        <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;
        <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;
        <input type="button" id="btnExport" value="打印" style="width: 120px; height: 25px;" />&nbsp;&nbsp;
    </div>

</body>    
     <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
