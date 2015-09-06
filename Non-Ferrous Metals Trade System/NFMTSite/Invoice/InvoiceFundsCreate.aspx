<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceFundsCreate.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceFundsCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

            //发票日期
            $("#txbInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23 });

            //selOutCorp 开票公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=" + "<%=this.outSelf%>";
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
            $("#selOutCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //selInCorp 收票公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=" + "<%=this.inSelf%>";
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
            $("#selInCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //txbInvoiceBala 发票金额
            $("#txbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //品种 selAsset
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#selAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //毛重 txbIntegerAmount
            $("#txbIntegerAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //净重 txbNetAmount
            $("#txbNetAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //计量单位 selUnit
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 });

            //增值税率 txbVATRatio
            $("#txbVATRatio").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbolPosition: "right", spinButtons: true, width: 100 });

            //增值税 txbVATBala
            $("#txbVATBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //备注
            $("#txbMemo").jqxInput({ height: 23 });

            //buttons
            $("#btnCreate").jqxButton();
            $("#btnCancel").jqxButton();

            //验证
            $("#jqxInvoiceExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbInvoiceName", message: "实际发票号必填", action: "keyup,blur", rule: "required" },
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
                            input: "#txbVATRatio", message: "增值税率必须大于0且小于1", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbVATRatio").jqxNumberInput("val") > 0 && $("#txbVATRatio").jqxNumberInput("val") < 100;
                            }
                        },
                        {
                            input: "#txbVATBala", message: "增值税必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#txbVATBala").jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            //新增
            $("#btnCreate").click(function () {

                var isCanSubmit = $("#jqxInvoiceExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认添加财务发票？")) { return; }

                var invoice = {
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
                    AssetId: $("#selAsset").val(),
                    IntegerAmount: $("#txbIntegerAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    MUId: $("#selUnit").val(),
                    VATRatio: $("#txbVATRatio").val(),
                    VATBala: $("#txbVATBala").val()
                };

                var fileIds = new Array();

                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/InvoiceFundsCreateHandler.ashx", { Invoice: JSON.stringify(invoice), InvoiceFunds: JSON.stringify(invoiceFunds) },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            AjaxFileUpload(fileIds, obj.ReturnValue, AttachTypeEnum.InvoiceAttach);
                        }
                        alert(obj.Message);
                        document.location.href = "InvoiceFundsList.aspx";
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
        <input type="button" id="btnCreate" value="新增发票" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="取消" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

</body>
</html>
