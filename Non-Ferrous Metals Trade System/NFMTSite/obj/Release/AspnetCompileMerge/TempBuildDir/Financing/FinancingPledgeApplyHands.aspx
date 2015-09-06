<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingPledgeApplyHands.aspx.cs" Inherits="NFMTSite.Financing.FinancingPledgeApplyHands" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押申请单确认手数</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=CurPledgeApply.DataBaseName%>" + "&t=" + '<%=CurPledgeApply.TableName%>' + "&id=" + "<%=CurPledgeApply.PledgeApplyId%>";

        $(document).ready(function () {
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxMemoExpander").jqxExpander({ width: "98%" });
        });
    </script>
</head>
<body>
    <div id="jqxApplyExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            质押申请单信息<input type="hidden" id="hidModel" runat="server" />
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="text-align: right; width: 15%;">质押申请单号：</span>
                    <input type="text" id="txbPledgeApplyNo" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">部门：</span>
                    <div id="ddlDeptId" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">日期：</span>
                    <div id="dtApplyTime" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">融资银行：</span>
                    <div id="ddlFinancingBankId" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">融资账户：</span>
                    <div id="ddlFinancingAccountId" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">融资货物：</span>
                    <div id="ddlAssetId" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">头寸是否转回：</span>
                    <div id="rbYes" runat="server" style="float: left; margin-top: 6px;">是</div>
                    <div id="rbNo" runat="server" style="float: left; margin-top: 6px">否</div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">头寸所在交易所：</span>
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
            $("#ddlDeptId").jqxComboBox("val", "<%=CurPledgeApply.DeptId%>");

            //日期
            $("#dtApplyTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180, disabled: true });
            $("#dtApplyTime").jqxDateTimeInput("val", new Date("<%=CurPledgeApply.ApplyTime%>"));

            //融资银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlFinancingBankId").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlFinancingBankId").jqxComboBox("val", "<%=CurPledgeApply.FinancingBankId%>");

            //融资账户
            var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?b=" + $("#ddlFinancingBankId").val();
            var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
            var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
            $("#ddlFinancingAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 180, height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlFinancingAccountId").jqxComboBox("val", "<%=CurPledgeApply.FinancingAccountId%>");

            //融资品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 180, height: 25, disabled: true });
            $("#ddlAssetId").jqxComboBox("val", "<%=CurPledgeApply.AssetId%>");

            //期货头寸是否转回
            //是
            $("#rbYes").jqxRadioButton({ width: 80, height: 22, disabled: true });

            //否
            $("#rbNo").jqxRadioButton({ width: 80, height: 22, disabled: true });

            if ("<%=CurPledgeApply.SwitchBack%>".toLowerCase() === "true") {
                $("#rbYes").jqxRadioButton("check");
            } else {
                $("#rbNo").jqxRadioButton("check");
            }

            //期货头寸所在交易所
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlExchangeId").jqxComboBox("val", "<%=CurPledgeApply.ExchangeId%>");

            //质押申请单
            $("#txbPledgeApplyNo").jqxInput({ height: 25, width: 180, disabled: true });
            $("#txbPledgeApplyNo").jqxInput("val", "<%=CurPledgeApply.PledgeApplyNo%>");
        });
    </script>

    <div id="jqxStockExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            实货描述
        </div>
        <div style="height: 500px;">
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var url = "Handler/FinancingPledgeApplyStockDetailForHandHandler.ashx?pid=<%=CurPledgeApply.PledgeApplyId%>";
            var source =
            {
                datafields:
                [
                   { name: "StockDetailId", type: "int" },
                   { name: "PledgeApplyId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "Deadline", type: "string" },
                   { name: "Hands", type: "int" },
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

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: dataAdapter,
                autoheight: true,
                enabletooltips: true,
                editable: true,
                selectionmode: "singlecell",
                columns: [
                  { text: "合同号", datafield: "ContractNo", width: "15%", editable: false },
                  {
                      text: "净重（吨）", datafield: "NetAmount", width: "15%", editable: false, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue) {
                                        return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                    }
                      }]
                  },
                  { text: "业务单号", datafield: "RefNo", editable: false, width: "15%" },
                  { text: "期限", datafield: "Deadline", width: "15%", editable: false },
                  {
                      text: "匹配手数", datafield: "Hands", width: "15%", sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 0, Digits: 6, spinButtons: true });
                      }, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "备注", datafield: "Memo", editable: false }
                ]
            });
        });
    </script>
        
    <div id="jqxMemoExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            审核附言
        </div>
        <div id="Div1">
            <ul style="list-style-type: none;">
                <li>
                    <span style="text-align: right; width: 15%;">审核附言：</span>
                    <input type="text" id="txbAuditMemo" />
                </li>                
            </ul>
        </div>
    </div>

    <script type="text/javascript">
        $("#txbAuditMemo").jqxInput({ height: 25, width: "50%" });
    </script>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0;">
        <input type="button" id="btnPass" value="确认手数并提交" runat="server" style="margin-left: 35px" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnRefuse" value="拒绝" runat="server" style="margin-left: 35px" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="BtnReturn" value="退回上一节点" runat="server" style="margin-left: 35px" />&nbsp;&nbsp;&nbsp;
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnPass").jqxInput({ height: 25, width: 150 });
            $("#btnRefuse").jqxInput({ height: 25, width: 100 });
            $("#BtnReturn").jqxInput({ height: 25, width: 100 });

            var id = "<%=taskNodeId%>";

            $("#btnPass").on("click", function () {
                var rows = $("#jqxStockGrid").jqxGrid("getrows");

                for (var i = 0; i < rows.length; i++) {
                    if (!rows[i].Hands || rows[i].Hands <= 0) {
                        alert("请确定手数！");
                        return;
                    }
                }

                if (!confirm("确认提交？")) {
                    return;
                }
                $("#btnPass").jqxButton({ disabled: true });

                $.post("Handler/FinancingPledgeApplyUpdateHandsHandler.ashx", { rows: JSON.stringify(rows),pid:"<%=CurPledgeApply.PledgeApplyId%>" },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() === "0") {
                            $.post("../WorkFlow/Handler/TaskAuditHandler.ashx", { logResult: "审核通过", isPass: true, id: id, memo: $("#txbAuditMemo").val() },
                            function (result) {
                                var obj = JSON.parse(result);
                                alert(obj.Message);
                                if (obj.ResultStatus.toString() === "0") {
                                    document.location.href = "../WorkFlow/TaskList.aspx";
                                } else {
                                    $("#btnPass").jqxButton({ disabled: false });
                                }
                            });
                        }
                    }
                );
            });

            $("#btnRefuse").on("click", function () {
                if (!confirm("确认审核不通过？")) {
                    return;
                }
                $("#btnRefuse").jqxButton({ disabled: true });

                var logResult = "审核不通过";
                $.post("../WorkFlow/Handler/TaskAuditHandler.ashx", { logResult: logResult, isPass: false, id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() === "0") {
                            document.location.href = "../WorkFlow/TaskList.aspx";
                        } else {
                            $("#btnPass").jqxButton({ disabled: false });
                        }
                    }
                );
            });
            
            $("#BtnReturn").on("click", function () {
                if (!confirm("确定退回上一级？")) {
                    return;
                }
                $("#BtnReturn").jqxButton({ disabled: true });

                $.post("../WorkFlow/Handler/TaskNodeReturnHandler.ashx", { logResult: "退回上一节点", id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() === "0") {
                            document.location.href = "../WorkFlow/TaskList.aspx";
                        } else {
                            $("#BtnReturn").jqxButton({ disabled: false });
                        }
                    }
                );
            });
        });
    </script>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
