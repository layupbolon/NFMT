<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingPledgeApplyDetail.aspx.cs" Inherits="NFMTSite.Financing.FinancingPledgeApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押申请单明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript"> 
        var clip;

        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=CurPledgeApply.DataBaseName%>" + "&t=" + "<%=CurPledgeApply.TableName%>" + "&id=" + "<%=CurPledgeApply.PledgeApplyId%>";

        $(document).ready(function () {
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxCashExpander").jqxExpander({ width: "98%" });
            $("#jqxRepoInfoExpander").jqxExpander({ width: "98%" });
        });
    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            质押申请单信息
            <img style="width: 25px; height: 2%;" src="../images/Financing/email-alert.png" id="emailPic" />
            <input type="hidden" id="hidModel" runat="server" />
            <input type="hidden" id="hidMailInfo" runat="server" />
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

            $("#emailPic").jqxTooltip({ content: '<%=this.emailStr%>', position: 'bottom',autoHide: false, name: 'movieTooltip', showArrow: false, opacity: 1 });

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

    <div id="popupWindow_Multi" style="display: none">
        <div>Email信息</div>
        <div>
            <ul style="list-style-type:none;">
                <li>
                    <textarea id="editor" style="width:800px;height:380px" ></textarea>
                </li>
                <li>
                    <input style="margin:5px 200px 0 200px" type="button" id="Save_Multi" value="确认" />
                    <input id="Cancel_Multi" type="button" value="取消" style="margin-top:5px;"/>
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {            

            $("#popupWindow_Multi").jqxWindow({
                autoOpen: false, isModal: true, width: 900, position: 'top, left', height: 500, maxWidth: 1200, resizable: false, cancelButton: $("#Cancel_Multi"), modalOpacity: 0.01,position:"center"});

            $("#Save_Multi").jqxButton({ height: 25, width: 100 });
            $("#Cancel_Multi").jqxButton({ height: 25, width: 100 });

            //弹窗关闭时将验证关闭
            $("#popupWindow_Multi").on("close", function () {
                $("#popupWindow_Multi").jqxValidator("hide");
            });

            $("#popupWindow_Multi").jqxValidator({
                rules:
                    [
                        { input: "#editor", message: "请输入实货描述内容", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#Save_Multi").click(function () {
                //alert($("#editor").val());
                var isCanSubmit = $("#popupWindow_Multi").jqxValidator("validate");
                if (!isCanSubmit) { return; }
                $("#Save_Multi").jqxButton({ disabled: true });

                $.post("Handler/HandleStockInfo.ashx", { text: $("#editor").val() }, function (result) {
                    var obj = JSON.parse(result);
                    if (obj.ResultStatus.toString() == "0") {
                        var listInfo = JSON.parse(obj.ReturnValue);
                        if (listInfo) {
                            for (var i = 0; i < listInfo.length; i++) {
                                var datarow = {
                                    ContractNo: listInfo[i].ContractNo,
                                    NetAmount: listInfo[i].NetAmount,
                                    RefNo: listInfo[i].RefNo,
                                    Deadline: listInfo[i].Deadline,
                                    Memo: listInfo[i].Memo
                                };
                                $("#jqxStockGrid").jqxGrid("addrow", null, datarow);
                            }
                        }
                    } else {
                        alert(obj.Message);
                        return;
                    }
                }
                );
                $("#popupWindow_Multi").jqxWindow("hide");
                $("#Save_Multi").jqxButton({ disabled: false });
            });
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

            var url = "Handler/FinancingPledgeApplyStockDetailHandler.ashx?pid=<%=CurPledgeApply.PledgeApplyId%>";
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
                  { text: "合同号", datafield: "ContractNo", width: "15%" },
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
            <div id="jqxCashGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var url = "Handler/FinancingPledgeApplyCashDetailHandler.ashx?pid=<%=CurPledgeApply.PledgeApplyId%>";
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
                columns: [
                  { text: "实货合同号", datafield: "StockContractNo", width: "15%" },
                  { text: "期限", datafield: "Deadline", width: "15%" },
                  {
                      text: "数量（手）", datafield: "Hands", width: "15%", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  {
                      text: "价格", datafield: "Price", width: "15%", sortable: false, columntype: "numberinput",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "到期日", datafield: "ExpiringDate", width: "15%", cellsformat: "yyyy-MM-dd", columntype: "datetimeinput" },
                  { text: "经济公司账户名", datafield: "AccountName", width: "15%" },
                  { text: "备注", datafield: "Memo" }
                ]
            });
        });
    </script>

    <div id="jqxRepoInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            赎回信息
        </div>
        <div style="height: 500px;">
            <div id="jqxRepoGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var source =
            {
                datafields:
                [
                   { name: "RepoApplyId", type: "int" },
                   { name: "RepoApplyIdNo", type: "string" },
                   { name: "RepoSumNetAmount", type: "number" },
                   { name: "RepoSumHands", type: "int" },
                   { name: "RepoApplyStatus", type: "int" },
                   { name: "StatusName", type: "string" }
                ],
                datatype: "json",
                localdata: <%=this.repoInfoJson%>
                };
            var dataAdapter = new $.jqx.dataAdapter(source);

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxRepoGrid").jqxGrid("getrowdata", row);

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";

                cellHtml += "<a target=\"_blank\" href=\"FinancingRepoApplyDetail.aspx?f=w&id=" + value + "\">查看</a>"
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxRepoGrid").jqxGrid(
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
                  { text: "赎回申请单号", datafield: "RepoApplyIdNo" },
                  {
                      text: "赎回数量（手）", datafield: "RepoSumHands", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  {
                      text: "赎回净重", datafield: "RepoSumNetAmount", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "状态", datafield: "StatusName"},
                  { text: "操作", datafield: "RepoApplyId", cellsrenderer: cellsrenderer, width: 120, enabletooltips: false, sortable: false }
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
        <input type="button" id="btnClose" value="关闭" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAudit").jqxButton();
            $("#btnInvalid").jqxButton();
            $("#btnGoBack").jqxButton();
            //$("#btnComplete").jqxInput();
            //$("#btnCompleteCancel").jqxInput();
            $("#btnClose").jqxButton();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 55,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                $("#btnInvalid").jqxButton({ disabled: true });
                var operateId = operateEnum.作废;
                $.post("Handler/FinancingPledgeApplyStatusHandler.ashx", { id: "<% = this.CurPledgeApply.PledgeApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        window.document.location = "FinancingPledgeApplyList.aspx";
                        $("#btnInvalid").jqxButton({ disabled: false });
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                $("#btnGoBack").jqxButton({ disabled: true });
                var operateId = operateEnum.撤返;
                $.post("Handler/FinancingPledgeApplyStatusHandler.ashx", { id: "<% = this.CurPledgeApply.PledgeApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        window.document.location = "FinancingPledgeApplyList.aspx";
                        $("#btnGoBack").jqxButton({ disabled: false });
                    }
                );
            });

            //$("#btnComplete").on("click", function () {
            //    if (!confirm("确认完成？")) { return; }
            //    var operateId = operateEnum.执行完成;
            //    $.post("Handler/FinancingPledgeApplyStatusHandler.ashx", { id: "", oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "FinancingPledgeApplyList.aspx";
            //        }
            //    );
            //});

            //$("#btnCompleteCancel").on("click", function () {
            //    if (!confirm("确认完成撤销？")) { return; }
            //    var operateId = operateEnum.执行完成撤销;
            //    $.post("Handler/FinancingPledgeApplyStatusHandler.ashx", { id: "", oi: operateId },
            //        function (result) {
            //            alert(result);
            //            document.location.href = "FinancingPledgeApplyList.aspx";
            //        }
            //    );
            //});

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                $("#btnClose").jqxButton({ disabled: true });
                var operateId = operateEnum.关闭;
                $.post("Handler/FinancingPledgeApplyStatusHandler.ashx", { id: "<%=this.CurPledgeApply.PledgeApplyId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "FinancingPledgeApplyList.aspx";
                        $("#btnClose").jqxButton({ disabled: false });
                    }
                );
            });
        });
    </script>

</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
