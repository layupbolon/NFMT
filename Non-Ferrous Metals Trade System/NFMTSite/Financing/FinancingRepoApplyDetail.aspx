<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingRepoApplyDetail.aspx.cs" Inherits="NFMTSite.Financing.FinancingRepoApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>赎回申请单明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.CurRepoApply.DataBaseName%>" + "&t=" + "<%=this.CurRepoApply.TableName%>" + "&id=" + "<%=this.CurRepoApply.RepoApplyId%>";

        $(document).ready(function () {
            $("#jqxApplyExpander").jqxExpander({ width: "98%" ,expanded: false});
            $("#jqxStockExpander").jqxExpander({ width: "98%",expanded: false });
            $("#jqxCashExpander").jqxExpander({ width: "98%" ,expanded: false});
            $("#jqxRepoExpander").jqxExpander({ width: "98%" });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            质押申请单信息<input type="hidden" id="hidModel" runat="server" />
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="text-align: right; width: 20%;">质押申请单号：</span>
                    <input type="text" id="txbPledgeApplyNo" />
                </li>
                <li>
                    <span style="width: 20%; text-align: right;">部门：</span>
                    <div id="ddlDeptId" style="float: left;"></div>

                    <span style="text-align: right; width: 20%;">日期：</span>
                    <div id="dtApplyTime" style="float: left;"></div>
                </li>
                <li>
                    <span style="text-align: right; width: 20%;">融资银行：</span>
                    <div id="ddlFinancingBankId" style="float: left;"></div>

                    <span style="text-align: right; width: 20%;">融资账户：</span>
                    <div id="ddlFinancingAccountId" style="float: left;"></div>
                </li>
                <li>
                    <span style="text-align: right; width: 20%;">融资货物：</span>
                    <div id="ddlAssetId" style="float: left;"></div>

                    <span style="text-align: right; width: 20%;">头寸是否转回：</span>
                    <div id="rbYes" runat="server" style="float: left; margin-top: 6px;">是</div>
                    <div id="rbNo" runat="server" style="float: left; margin-top: 6px">否</div>
                </li>
                <li>
                    <span style="text-align: right; width: 20%;">头寸所在交易所：</span>
                    <div id="ddlExchangeId" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            //部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlDeptId").jqxComboBox("val", "<%=this.CurPledgeApply.DeptId%>");

            //日期
            $("#dtApplyTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180, disabled: true });
            $("#dtApplyTime").jqxDateTimeInput("val", new Date("<%=this.CurPledgeApply.ApplyTime%>"));

            //融资银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlFinancingBankId").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlFinancingBankId").jqxComboBox("val", "<%=this.CurPledgeApply.FinancingBankId%>");

            //融资账户
            var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?b=" + $("#ddlFinancingBankId").val();
            var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
            var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
            $("#ddlFinancingAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 180, height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlFinancingAccountId").jqxComboBox("val", "<%=this.CurPledgeApply.FinancingAccountId%>");

            //融资品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 180, height: 25, disabled: true });
            $("#ddlAssetId").jqxComboBox("val", "<%=this.CurPledgeApply.AssetId%>");

            //期货头寸是否转回
            //是
            $("#rbYes").jqxRadioButton({ width: 80, height: 22, disabled: true });

            //否
            $("#rbNo").jqxRadioButton({ width: 80, height: 22, disabled: true });

            if ("<%=this.CurPledgeApply.SwitchBack%>".toLowerCase() == "true") {
                $("#rbYes").jqxRadioButton("check");
            } else {
                $("#rbNo").jqxRadioButton("check");
            }

            //期货头寸所在交易所
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlExchangeId").jqxComboBox("val", "<%=this.CurPledgeApply.ExchangeId%>");

            //质押申请单
            $("#txbPledgeApplyNo").jqxInput({ height: 25, width: 180, disabled: true });
            $("#txbPledgeApplyNo").jqxInput("val", "<%=this.CurPledgeApply.PledgeApplyNo%>");
        });
    </script>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            质押申请单实货描述
        </div>
        <div style="height: 500px;">
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            //var url = "Handler/FinPledgeApplyStockDetailForRepoHandler.ashx?pid=<%=this.CurPledgeApply.PledgeApplyId%>";
            var stocksSource =
            {
                datafields:
                [
                   { name: "StockDetailId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PledgeNetAmount", type: "number" },
                   { name: "PledgeHands", type: "int" },
                   { name: "Memo", type: "string" },
                   { name: "AlreadyNetAmount", type: "number" },
                   { name: "AlreadyHands", type: "int" },
                   { name: "NetAmount", type: "number" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "AccountName", type:"string" },
                   { name: "Deadline", type:"string" }
                ],
                datatype: "json",                
                localdata: <%=this.selectedJsonUp%>
                };

            var StockdataAdapter = new $.jqx.dataAdapter(stocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error); 
                }
            });

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: StockdataAdapter,
                autoheight: true,
                enabletooltips: true,
                editable: false,
                //selectionmode: "singlecell",
                columns: [
                  { text: "合约号", datafield: "ContractNo", width: "14%" },
                  { text: "业务单号", datafield: "RefNo", width: "14%" },
                  { text: "匹配手数", datafield: "PledgeHands", width: "14%", aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                }
                  }] },
                  { text: "质押净重（吨）", datafield: "PledgeNetAmount", width: "14%", aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                }
                  }] },
                  { text: "期限", datafield: "Deadline", width: "14%" },
                  { text: "备注", datafield: "Memo" },
                  //{ text: "已赎回手数", datafield: "AlreadyHands", width: 120 },
                  //{ text: "已赎回净重", datafield: "AlreadyNetAmount" , width: 120 },
                  //{ text: "赎回手数", datafield: "Hands", width: 120 },
                  //{ text: "赎回净重", datafield: "NetAmount", width: 120  }
                ]
            });
        });
    </script>

    <div id="jqxCashExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            质押申请单期货头寸描述
        </div>
        <div style="height: 500px;">
            <div id="jqxCashGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var url = "Handler/FinancingPledgeApplyCashDetailHandler.ashx?pid=<%=this.CurPledgeApply.PledgeApplyId%>";
            var source =
            {
                datafields:
                [
                   { name: "StockContractNo", type: "string" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "AccountName", type: "string" },
                   { name: "Memo", type: "string" }
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
            var dataAdapter = new $.jqx.dataAdapter(source);

            $("#jqxCashGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: dataAdapter,
                autoheight: true,
                enabletooltips: true,
                columns: [
                  { text: "实货合同号", datafield: "StockContractNo", width: "14%" },
                  {
                      text: "数量（手）", datafield: "Hands", width: "14%", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                    }
                      }]
                  },
                  {
                      text: "价格", datafield: "Price", width: "14%", sortable: false, columntype: "numberinput",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "到期日", datafield: "ExpiringDate", width: "14%", cellsformat: "yyyy-MM-dd", columntype: "datetimeinput" },
                  { text: "经济公司账户名", datafield: "AccountName", width: "14%" },
                  { text: "备注", datafield: "Memo" }
                ]
            });
        });
    </script>

    <div id="jqxRepoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            赎回申请单信息
            <img style="width:25px;height:2%;" src="../images/Financing/email-alert.png" id="emailPic" />
        </div>
        <div style="height: 500px;">
            <div id="jqxRepoGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#emailPic").jqxTooltip({ content: '<%=this.emailStr%>', position: 'bottom',autoHide: false, name: 'movieTooltip', showArrow: false, opacity: 1 });



            var repoSource =
            {
                datafields:
                [
                   { name: "StockDetailId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "RepoTime", type: "date" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PledgeNetAmount", type: "number" },
                   { name: "PledgeHands", type: "int" },
                   { name: "Memo", type: "string" },
                   { name: "AlreadyNetAmount", type: "number" },
                   { name: "AlreadyHands", type: "int" },
                   { name: "NetAmount", type: "number" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "AccountName", type:"string" }
                ],
                datatype: "json",
                localdata: <%=this.selectedJsonDown%>
                };

            var RepodataAdapter = new $.jqx.dataAdapter(repoSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error); 
                }
            });

            $("#jqxRepoGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: RepodataAdapter,
                autoheight: true,
                enabletooltips: true,
                editable: false,
                selectionmode: "singlecell",
                columns: [
                  { text: "日期", datafield: "RepoTime", cellsformat: "yyyy-MM-dd",width:"11%" },
                  { text: "合约号", datafield: "ContractNo",width:"11%" },
                  { text: "业务单号", datafield: "RefNo",width:"11%"},
                  { text: "赎回净重", datafield: "NetAmount",width:"9%", aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                }
                  }] },
                  { text: "赎回手数", datafield: "Hands",width:"9%", aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                }
                  }] },
                  { text: "价格", datafield: "Price",width:"9%" },
                  { text: "到期日", datafield: "ExpiringDate", cellsformat: "yyyy-MM-dd",width:"11%"},
                  { text: "经济公司", datafield: "AccountName",width:"11%" },
                  { text: "备注", datafield: "Memo" }
                ]
            });
        });
    </script>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <%--<input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;--%>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnAudit").jqxButton();
            $("#btnInvalid").jqxButton();
            $("#btnGoBack").jqxButton();
            //$("#btnComplete").jqxInput();
            //$("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 56,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                $("#btnInvalid").jqxButton({ disabled: true });
                var operateId = operateEnum.作废;
                $.post("Handler/FinancingRepoApplyStatusHandler.ashx", { id: "<% = this.CurRepoApply.RepoApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        window.document.location = "FinancingRepoApplyList.aspx";
                        $("#btnInvalid").jqxButton({ disabled: false });
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                $("#btnGoBack").jqxButton({ disabled: true });
                var operateId = operateEnum.撤返;
                $.post("Handler/FinancingRepoApplyStatusHandler.ashx", { id: "<% = this.CurRepoApply.RepoApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        window.document.location = "FinancingRepoApplyList.aspx";
                        $("#btnGoBack").jqxButton({ disabled: false });
                    }
                );
            });

            //$("#btnComplete").on("click", function () {
            //    if (!confirm("确认完成？")) { return; }
            //    var operateId = operateEnum.执行完成;
            //    $.post("Handler/FinancingRepoApplyStatusHandler.ashx", { id: "", oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "FinancingRepoApplyList.aspx";
            //        }
            //    );
            //});

            //$("#btnCompleteCancel").on("click", function () {
            //    if (!confirm("确认完成撤销？")) { return; }
            //    var operateId = operateEnum.执行完成撤销;
            //    $.post("Handler/FinancingRepoApplyStatusHandler.ashx", { id: "", oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "FinancingRepoApplyList.aspx";
            //        }
            //    );
            //});
        });
    </script>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
