<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SIDetail.aspx.cs" Inherits="NFMTSite.Invoice.SIDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价外票明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.invoice.DataBaseName%>" + "&t=" + "<%=this.invoice.TableName%>" + "&id=" + "<%=this.invoice.InvoiceId%>";

        $(document).ready(function () {
            $("#jqxSIExpander").jqxExpander({ width: "98%" });
            $("#jqxAllotExpander").jqxExpander({ width: "98%" });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23, disabled: true });

            //发票日期
            $("#dtInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //发票金额
            $("#nbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#nbInvoiceBala").on("valueChanged", function (event) {
                var value = event.args.value;
                var rate = $("#ddlChangeRate").val();
                $("#nbChangeBala").val(value * rate);
            });

            //发票币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            //折算币种
            var changecySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var changecyDataAdapter = new $.jqx.dataAdapter(changecySource);
            $("#ddlChangeCurrencyId").jqxDropDownList({ source: changecyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });

            //折算汇率
            $("#ddlChangeRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbolPosition: "right", spinButtons: true, width: 100, disabled: true });
            $("#ddlChangeRate").on("valueChanged", function (event) {
                var value = event.args.value;
                var amount = $("#nbInvoiceBala").val();
                $("#nbChangeBala").val(value * amount);
            });

            //折算金额
            $("#nbChangeBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbolPosition: "right", spinButtons: true, width: 140, disabled: true });

            //我方公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var outCorpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: outCorpUrl, async: false
            };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlInnerCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });

            //对方公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var inCorpSource = {
                datafields:
                [
                    { name: "CorpId", type: "int" },
                    { name: "CorpName", type: "string" },
                    { name: "CorpFullName", type: "string" }
                ],
                datatype: "json", url: inCorpUrl, async: false
            };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#ddlOutCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, disabled: true });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#ddlPayDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", height: 25, width: 120, disabled: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23, disabled: true });


            /////////////////////已分配/////////////////////

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
                localdata: $("#hidDetails").val()
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
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "所属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "发票内容", datafield: "FeeTypeName" },
                  { text: "分配金额", datafield: "DetailBala" }
                ]
            });

            //赋值
            $("#txbInvoiceName").val("<%=this.InvoiceName%>");
            $("#dtInvoiceDate").val(new Date("<%=this.InvoiceDate%>"));
            $("#nbInvoiceBala").val("<%=this.InvoiceBala%>");
            $("#ddlCurrencyId").val("<%=this.CurrencyId%>");
            $("#ddlChangeCurrencyId").val("<%=this.ChangeCurrencyId%>");
            $("#ddlChangeRate").val("<%=this.ChangeRate%>");
            $("#nbChangeBala").val("<%=this.ChangeBala%>");
            $("#ddlOutCorp").val("<%=this.OutCorpId%>");
            $("#ddlInnerCorp").val("<%=this.InCorpId%>");
            $("#ddlPayDept").val("<%=this.PayDept%>");
            $("#txbMemo").val("<%=this.Memo%>");

            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 21,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/SIStatusHandler.ashx", { ii: "<%=this.SIId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "SIList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/SIStatusHandler.ashx", { ii: "<%=this.SIId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "SIList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/SIStatusHandler.ashx", { ii: "<%=this.SIId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "SIList.aspx";
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/SIStatusHandler.ashx", { ii: "<%=this.SIId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "SIList.aspx";
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxSIExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价外票信息
                        <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>实际发票号：</strong>
                    <input type="text" id="txbInvoiceName" style="float: left;" />
                </li>
                <li>
                    <strong>开票日期：</strong>
                    <div id="dtInvoiceDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div id="nbInvoiceBala" style="float: left;"></div>
                </li>
                <li>
                    <strong>发票币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;" />
                </li>
                <li>
                    <strong>折算币种：</strong>
                    <div id="ddlChangeCurrencyId" style="float: left;" />
                </li>
                <li>
                    <strong>折算汇率：</strong>
                    <div id="ddlChangeRate" style="float: left;" />
                </li>
                <li>
                    <strong>折算金额：</strong>
                    <div id="nbChangeBala" style="float: left;" />
                </li>
                <li>
                    <strong>我方公司：</strong>
                    <div id="ddlInnerCorp" style="float: left;" />
                </li>
                <li>
                    <strong>对方公司：</strong>
                    <div id="ddlOutCorp" style="float: left;" />
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div id="ddlPayDept" style="float: left;" />
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已分配
                        <input type="hidden" id="hidDetails" runat="server" />
        </div>
        <div>
            <div id="jqxAllotGrid"></div>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="InvoiceAttach" />

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>

</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
