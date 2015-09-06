<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotContractDetail.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotContractDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约收款分配明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.cashInAllot.DataBaseName%>" + "&t=" + "<%=this.cashInAllot.TableName%>" + "&id=" + "<%=this.cashInAllot.AllotId%>";

        $(document).ready(function () {
            

            //Expander Init
            $("#jqxConExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxSubExpander").jqxExpander({ width: "98%", expanded: false });

            $("#jqxCorpSelectExpander").jqxExpander({ width: "98%" }); 

            //////////////////////已分配情况//////////////////////
            var formatedData = "";
            var totalrecords = 0;
          
            var corpSelectSource =
            {
                localdata: <%=this.JsonCorpSelect%>,
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CorpRefId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "LastBala", type: "number" },
                    { name: "AllotBala", type: "number" },
                    { name: "AllotCorp", type: "string" }
                ],
                datatype: "json"
            };
            var corpSelectDataAdapter = new $.jqx.dataAdapter(corpSelectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxCorpSelectGrid").jqxGrid(
            {
                width: "98%",
                source: corpSelectDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InCorp", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款银行", datafield: "CashInBankName", editable: false },
                  { text: "收款金额", datafield: "CashInBala", editable: false },
                  { text: "分配公司", datafield: "AllotCorp", editable: false },
                  { text: "分配金额", datafield: "AllotBala", editable: false },
                  { text: "币种", datafield: "CurrencyName", editable: false }
                ]
            });
        

            //buttons
            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();
            $("#btnClose").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 10,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CashInContactAllotStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
        });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/CashInContactAllotStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
        });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/CashInContactAllotStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
        });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/CashInContactAllotStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
        });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/CashInContactAllotStatusHandler.ashx", { id: "<%=this.cashInAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
        });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />

    <div id="jqxConExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>合约编号：</strong>
                    <span runat="server" id="spnContractNo"></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spnAsset"></span>
                </li>
                <li>
                    <strong>签订数量：</strong>
                    <span runat="server" id="spnSignAmount"></span>
                </li>
                <li>
                    <strong>我方抬头：</strong>
                    <span runat="server" id="spnInCorpNames"></span>
                </li>
                <li>
                    <strong>对方抬头：</strong>
                    <span runat="server" id="spnOutCorpNames"></span>
                </li>
                <li>
                    <strong>合约升贴水：</strong>
                    <span runat="server" id="spnPre"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            子合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>子合约编号：</strong>
                    <span runat="server" id="spnSubNo"></span>
                </li>
                <li>
                    <strong>子合约数量：</strong>
                    <span runat="server" id="spnSubSignAmount"></span>
                </li>
                <li>
                    <strong>已分配数量：</strong>
                    <span runat="server" id="Span1"></span>
                </li>
                <li>
                    <strong>升贴水：</strong>
                    <span runat="server" id="Span2"></span>
                </li>
                <li>
                    <strong>执行最终日：</strong>
                    <span runat="server" id="spnPeriodE"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxCorpSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择收款
        </div>
        <div>
            <div id="jqxCorpSelectGrid"></div>
        </div>
    </div>

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnCompleteCancel" value="确认完成撤销" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnClose" value="关闭" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
