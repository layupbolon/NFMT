<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApplyDetail.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发票申请明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.apply.DataBaseName%>" + "&t=" + "<%=this.apply.TableName%>" + "&id=" + "<%=this.apply.ApplyId%>";

        $(document).ready(function () {

            $("#jqxInvApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var DetailIds = new Array();

            //////////////////////////////////////////发票申请信息//////////////////////////////////////////

            var url = "Handler/InvoiceApplyDetailHandler.ashx?id=<%=this.invoiceApply.InvoiceApplyId%>";
            var Infosource =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "InvoiceApplyId", type: "int" },
                   { name: "ApplyId", type: "int" },
                   { name: "BussinessInvoiceId", type: "int" },
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "CurrencyName", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "OutContractNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "InvoicePrice", type: "number" },
                   { name: "StockLogId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "SubContractId", type: "int" },
                   { name: "PaymentAmount", type: "number" },
                   { name: "InterestAmount", type: "number" },
                   { name: "OtherAmount", type: "number" },
                   { name: "CardNo", type: "string" }
                ],
                datatype: "json",
                url: url,
                type: "GET",
                addrow: function (rowid, rowdata, position, commit) {
                    commit(true);
                },
                deleterow: function (rowid, commit) {
                    commit(true);
                },
                updaterow: function (rowid, newdata, commit) {
                    commit(true);
                }
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                return cellHtml + value + "</div>";
            }

            $("#jqxApplyGrid").jqxGrid(
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
                  { text: "子合约号", datafield: "SubNo", width: "7%" },
                  { text: "外部合约号", datafield: "OutContractNo", width: "10%" },
                  { text: "卡号", datafield: "CardNo", width: "7%" },
                  { text: "客户", datafield: "CorpName", width: "12%" },
                  { text: "品种", datafield: "AssetName", width: "7%" },
                  { text: "净重", datafield: "NetAmount", width: "7%" },
                  { text: "单位", datafield: "MUName", width: "7%" },
                  { text: "价格", datafield: "InvoicePrice", width: "9%", cellsrenderer: cellsrenderer },
                  { text: "货款金额", datafield: "PaymentAmount", width: "9%", cellsrenderer: cellsrenderer },
                  { text: "利息金额", datafield: "InterestAmount", width: "9%", cellsrenderer: cellsrenderer },
                  { text: "其他金额", datafield: "OtherAmount", width: "9%", cellsrenderer: cellsrenderer },
                  { text: "开票金额", datafield: "InvoiceBala", width: "9%", cellsrenderer: cellsrenderer }
                ]
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDept").jqxComboBox({ selectedIndex: 0, source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDept").jqxComboBox("val", "<%=this.curDeptId%>");

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorp").jqxComboBox({ source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", width: 180, disabled: true });
            $("#ddlApplyCorp").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            $("#txbApplyDesc").jqxInput({ width: "500", disabled: true });
            $("#txbApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnConfirm").jqxInput();
            $("#btnConfirmCancel").jqxInput();

            var id = "<%=this.invoiceApply.InvoiceApplyId%>"

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: MasterEnum.发票申请审核,
                    model: $("#hidmodel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/InvoiceApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/InvoiceApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认执行完成操作？")) { return; }
                var operateId = operateEnum.确认完成;
                $.post("Handler/InvoiceApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnConfirmCancel").on("click", function () {
                if (!confirm("撤销后已关闭的明细申请将会打开，执行完成撤销操作？")) { return; }
                var operateId = operateEnum.确认完成撤销;
                $.post("Handler/InvoiceApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceApplyList.aspx";
                        }
                    }
                );
            });

        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxInvApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            发票申请信息
        </div>
        <div>
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            申请信息
            <input type="hidden" id="hidmodel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="float: none;">
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="ddlApplyCorp"></div>
                </li>
                <li style="float: none;">
                    <strong>申请部门：</strong>
                    <div style="float: left;" id="ddlApplyDept"></div>
                </li>
                <li style="line-height: none; height: auto; float: none">
                    <strong>申请备注：</strong>
                    <textarea id="txbApplyDesc"></textarea>
                </li>
            </ul>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
