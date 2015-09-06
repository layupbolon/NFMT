<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingPledgeApplyCash.aspx.cs" Inherits="NFMTSite.Financing.FinancingPledgeApplyCash" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押申请单确定头寸</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.CurPledgeApply.DataBaseName%>" + "&t=" + "<%=this.CurPledgeApply.TableName%>" + "&id=" + "<%=this.CurPledgeApply.PledgeApplyId%>";

        $(document).ready(function () {
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxCashExpander").jqxExpander({ width: "98%" });
            $("#jqxMemoExpander").jqxExpander({ width: "98%" });
        });
    </script>
</head>
<body>
    <div id="jqxApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
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
            实货描述
        </div>
        <div style="height: 500px;">
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var url = "Handler/FinancingPledgeApplyStockDetailHandler.ashx?pid=<%=this.CurPledgeApply.PledgeApplyId%>";
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
                columns: [
                  { text: "合约号", datafield: "ContractNo", width: "15%" },
                  {
                      text: "净重（吨）", datafield: "NetAmount", width: "15%", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                    }
                      }]
                  },
                  { text: "业务单号", datafield: "RefNo", width: "15%" },
                  { text: "期限", datafield: "Deadline", width: "15%" },
                  {
                      text: "匹配手数", datafield: "Hands", width: "15%", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "备注", datafield: "Memo" }
                ]
            });
        });
    </script>

    <div id="jqxCashExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            期货头寸描述
        </div>
        <div style="height: 500px;">
            <input type="button" id="btnAddCash" value="添加" style="width: 120px; height: 25px; margin: 5px 0 0 5px" />
            <div id="jqxCashGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnAddCash").jqxButton({ height: 25, width: 100 });
            $("#btnAddCash").click(function () {
                $("#popupWindow").jqxWindow("show");
                $("#Save").jqxButton({ disabled: false });
                $("#popupWindow").jqxValidator({ arrow: false });

                $("#txbStockContractNo").jqxInput("val", "");
                $("#txbDeadline").jqxInput("val", "");
                $("#nbHands").jqxNumberInput("val", 0);
                $("#nbPrice").jqxNumberInput("val", 0); 
                $("#txbAccountName").jqxInput("val", "");
                $("#txbMemo").jqxInput("val", "");

                $("#txbStockContractNo").jqxInput("focus");
            });


            var createUrl = "Handler/FinancingPledgeApplyCashInfoHandler.ashx?pid=<%=this.CurPledgeApply.PledgeApplyId%>";
            var updateUrl = "Handler/FinancingPledgeApplyCashDetailHandler.ashx?pid=<%=this.CurPledgeApply.PledgeApplyId%>";

            //var url = "<%=this.hasData%>".toLowerCase()=="true" ? updateUrl : createUrl;
            var url = createUrl;
            var source =
            {
                datafields:
                [
                   { name: "StockContractNo", type: "string" },
                   { name: "Deadline", type: "string" },
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
                editable: true,
                selectionmode: "singlecell",
                columns: [
                  { text: "实货合同号", datafield: "StockContractNo", editable: true, width: "13%", cellclassname: "GridFillNumber" },
                  { text: "期限", datafield: "Deadline", editable: true, width: "13%", cellclassname: "GridFillNumber" },
                  {
                      text: "数量（手）", datafield: "Hands", width: "13%", editable: true, columntype: "numberinput", cellclassname: "GridFillNumber", createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 0, Digits: 8, spinButtons: true });
                      }, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  {
                      text: "价格", datafield: "Price", width: "13%", sortable: true, columntype: "numberinput", cellclassname: "GridFillNumber",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  
                  { text: "到期日", datafield: "ExpiringDate", cellsformat: "yyyy-MM-dd", columntype: "datetimeinput", cellclassname: "GridFillNumber" },
                  { text: "经济公司账户名", datafield: "AccountName", editable: true, width: "15%", cellclassname: "GridFillNumber" },
                  { text: "备注", datafield: "Memo", cellclassname: "GridFillNumber" },
                  {
                      text: "操作", datafield: "Edit", width: "10%", columntype: "button", cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var rowscount = $("#jqxCashGrid").jqxGrid("getdatainformation").rowscount;
                          if (row >= 0 && row < rowscount) {
                              var id = $("#jqxCashGrid").jqxGrid("getrowid", row);
                              var commit = $("#jqxCashGrid").jqxGrid("deleterow", id);
                              $("#jqxCashGrid").jqxGrid('refreshdata');
                          }
                      }
                  }
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
    
    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnPass" value="确认期货头寸并提交" runat="server" style="margin-left: 35px" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnRefuse" value="拒绝" runat="server" style="margin-left: 35px" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="BtnReturn" value="退回上一节点" runat="server" style="margin-left: 35px" />&nbsp;&nbsp;&nbsp;
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnPass").jqxInput({ height: 25, width: 150 });
            $("#btnRefuse").jqxInput({ height: 25, width: 100 });
            $("#BtnReturn").jqxInput({ height: 25, width: 100 });

            var id = "<%=this.taskNodeId%>";

            $("#btnPass").on("click", function () {
                var rows = $("#jqxCashGrid").jqxGrid("getrows");

                var sumHands = 0;

                for (var i = 0; i < rows.length; i++) {
                    if (!rows[i].StockContractNo || rows[i].StockContractNo == "") {
                        alert("请填写实货合同号");
                        return;
                    }
                    if (!rows[i].Deadline || rows[i].Deadline == "") {
                        alert("请填写期限");
                        return;
                    }
                    if (!rows[i].Hands || rows[i].Hands == 0) {
                        alert("请填写手数");
                        return;
                    }
                    if (!rows[i].Price || rows[i].Price == 0 ) {
                        alert("请填写价格");
                        return;
                    }
                    if (!rows[i].ExpiringDate || rows[i].ExpiringDate == "") {
                        alert("请填写到期日");
                        return;
                    }
                    if (!rows[i].AccountName || rows[i].AccountName == "") {
                        alert("请填写经济公司账户名");
                        return;
                    }

                    sumHands += rows[i].Hands;
                }

                if (sumHands > parseFloat("<%=this.CurPledgeApply.SumHands%>")) {
                    alert("期货头寸手数超过实货手数，请修改！");
                    return;
                }

                if (!confirm("确认提交？")) {
                    return;
                }
                $("#btnPass").jqxButton({ disabled: true });

                $.post("Handler/FinancingPledgeApplyUpdateCashHandler.ashx", { rows: JSON.stringify(rows), pid: "<%=this.CurPledgeApply.PledgeApplyId%>" },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            $.post("../WorkFlow/Handler/TaskAuditHandler.ashx", { logResult: "审核通过", isPass: true, id: id, memo: $("#txbAuditMemo").val() },
                            function (result) {
                                var obj = JSON.parse(result);
                                alert(obj.Message);
                                if (obj.ResultStatus.toString() == "0") {
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
                var isPass = false;
                $.post("../WorkFlow/Handler/TaskAuditHandler.ashx", { logResult: logResult, isPass: isPass, id: id, memo: $("#txbAuditMemo").val() },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
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
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "../WorkFlow/TaskList.aspx";
                        } else {
                            $("#BtnReturn").jqxButton({ disabled: false });
                        }                        
                    }
                );
            });
        });
    </script>

    <div id="popupWindow" style="display: none">
        <div>添加期货头寸描述</div>
        <div class="DataExpander">
            <ul>
                <li>
                    <span>实货合同号：</span>
                    <input type="text" id="txbStockContractNo" />
                </li>
                <li>
                    <span>期限：</span>
                    <input type="text" id="txbDeadline" />
                </li>
                <li>
                    <span>数量(手)：</span>
                    <div id="nbHands"></div>
                </li>
                <li>
                    <span>价格：</span>
                    <div id="nbPrice"></div>
                </li>
                <li>
                    <span>到期日：</span>
                    <div id="dtExpiringDate"></div>
                </li>
                <li>
                    <span>经济公司账户：</span>
                    <input type="text" id="txbAccountName" />
                </li>
                <li>
                    <span>备注：</span>
                    <input type="text" id="txbMemo" />
                </li>
                <li>
                    <input style="margin-right: 5px;" type="button" id="Save" value="确认" />
                    <input id="Cancel" type="button" value="取消" />
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            //实货合同号
            $("#txbStockContractNo").jqxInput({ height: 25, width: 160 });

            //期限
            $("#txbDeadline").jqxInput({ height: 25, width: 160 });
            
            //数量
            $("#nbHands").jqxNumberInput({ width: 160, height: 25, decimalDigits: 0, digits: 6, spinButtons: true, symbolPosition: "right", symbol: "手" });

            //价格
            $("#nbPrice").jqxNumberInput({ width: 160, height: 25, decimalDigits: 4, digits: 6, spinButtons: true});

            //到期日
            $("#dtExpiringDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 160 });

            //经济公司账户
            $("#txbAccountName").jqxInput({ height: 25, width: 160 });

            //备注
            $("#txbMemo").jqxInput({ height: 25, width: 160 });

            $("#popupWindow").jqxWindow({ width: 380, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01, position: "center" });
            $("#Cancel").jqxButton({ height: 25, width: 100 });
            $("#Save").jqxButton({ height: 25, width: 100 });

            //弹窗关闭时将验证关闭
            $("#popupWindow").on("close", function () {
                $("#popupWindow").jqxValidator("hide");
            });

            //验证器
            $("#popupWindow").jqxValidator({
                rules:
                    [
                        { input: "#txbStockContractNo", message: "请输入实货合同号", action: "keyup,blur", rule: "required" },
                        { input: "#txbDeadline", message: "请输入期限", action: "keyup,blur", rule: "required" },
                        {
                            input: "#nbHands", message: "数量必须大于0", action: "keyup,blur", rule: function () {
                                return $("#nbHands").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbPrice", message: "价格必须大于0", action: "keyup,blur", rule: function () {
                                return $("#nbPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        { input: "#txbAccountName", message: "请输入经济公司账户", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#Save").click(function () {
                var isCanSubmit = $("#popupWindow").jqxValidator("validate");
                if (!isCanSubmit) { return; }
                $("#Save").jqxButton({ disabled: true });

                var datarow = {
                    StockContractNo: $("#txbStockContractNo").val(),
                    Deadline: $("#txbDeadline").val(),
                    Hands: $("#nbHands").val(),
                    Price: $("#nbPrice").val(),
                    ExpiringDate: $("#dtExpiringDate").jqxDateTimeInput("val"),
                    AccountName: $("#txbAccountName").val(),
                    Memo: $("#txbMemo").val()
                };
                var commit = $("#jqxCashGrid").jqxGrid("addrow", null, datarow);
                if (commit)
                    $("#popupWindow").jqxWindow("hide");

            });
        });
    </script>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
