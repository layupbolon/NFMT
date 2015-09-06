<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableCorpDetail.aspx.cs" Inherits="NFMTSite.Funds.ReceivableCorpDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司收款分配明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            

            $("#jqxConSubExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });

            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();


            var formatedData = "";
            var totalrecords = 0;
            var selectSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" }
                ],
                localdata: "<%=this.curJsonStr%>"
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
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "ReceiveDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InnerCorp", editable: false },
                  { text: "收款银行", datafield: "BankName", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款金额", datafield: "PayBala", editable: false },
                  { text: "分配金额", datafield: "CanAllotBala" }
                ]
            });


            $("#btnInvalid").on("click", function () {
                if (!confirm("确定作废？")) { return; }
                $.post("Handler/ReceivableCorpInvalidHandler.ashx", { id: "<%=this.curRecAllot.ReceivableAllotId%>" },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableCorpList.aspx";
                    }
                );
            });

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 14,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确定撤返？")) { return; }
                $.post("Handler/ContractReceivableGoBackHandler.ashx", { id: "<%=this.curRecAllot.ReceivableAllotId%>" },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableCorpList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确定完成？")) { return; }
                $.post("Handler/ReceivableCorpStatusHandler.ashx", { id: "<%=this.curRecAllot.ReceivableAllotId%>" },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableCorpList.aspx";
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />


    <div id="jqxConSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            公司信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>所属集团：</strong>
                    <span style="float: left;" id="spanBlocId" runat="server" />
                </li>
                <li><strong>公司代码：</strong>
                    <span id="spanCorpCode" runat="server" /></li>
                <li>
                    <strong>公司名称：</strong>
                    <span id="spanCorpName" runat="server" />
                </li>
                <li><strong>纳税人识别号：</strong>
                    <span id="spanTaxPlayer" runat="server" /></li>
                <li>
                    <strong>公司地址：</strong>
                    <span id="spanCorpAddress" runat="server" />
                </li>
                <li><strong>公司电话：</strong>
                    <span id="spanCorpTel" runat="server" /></li>
                <li>
                    <strong>公司传真：</strong>
                    <span id="spanCorpFax" runat="server" />
                </li>
                <li><strong>公司邮编：</strong>
                    <span id="spanCorpZip" runat="server" /></li>
            </ul>
        </div>
    </div>

    <div id="jqxSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择分配
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
    </div>
</body>
</html>
