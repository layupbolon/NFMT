<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingPledgeApplyCreate.aspx.cs" Inherits="NFMTSite.Financing.FinancingPledgeApplyCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押申请单新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            //$(document).bind('keydown', function (event) {
            //    if (event.keyCode == 9) {
            //        return false;
            //    }
            //});
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });            
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            质押申请单信息
        </div>
        <div id="layOutDiv">
            <ul>
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
            $("#ddlDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25 });
            $("#ddlDeptId").jqxComboBox("val", "<%=deptId%>");
            $("#ddlDeptId").jqxComboBox("focus");

            //日期
            $("#dtApplyTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180 });

            //融资银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }, { name: "SwitchBack" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlFinancingBankId").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25, searchMode: "containsignorecase", autoDropDownHeight: true });

            $("#ddlFinancingBankId").on("select", function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {
                        var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?b=" + $("#ddlFinancingBankId").val();
                        var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
                        var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
                        $("#ddlFinancingAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 180, height: 25, searchMode: "containsignorecase", autoDropDownHeight: true });

                        if (item.originalItem.SwitchBack) {
                            $("#rbYes").jqxRadioButton("check");
                            $("#rbNo").jqxRadioButton("unCheck");
                        } else {
                            $("#rbYes").jqxRadioButton("unCheck");
                            $("#rbNo").jqxRadioButton("check");
                        }
                    }
                }
            });

            //融资账户
            var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?b=" + $("#ddlFinancingBankId").val();
            var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
            var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
            $("#ddlFinancingAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 180, height: 25, searchMode: "containsignorecase", autoDropDownHeight: true });

            //融资品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 180, height: 25, autoDropDownHeight: true });

            //期货头寸是否转回
            //是
            $("#rbYes").jqxRadioButton({ width: 80, height: 22 });

            //否
            $("#rbNo").jqxRadioButton({ width: 80, height: 22});

            //期货头寸所在交易所
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 180, height: 25, autoDropDownHeight: true });

            $("#jqxApplyExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlDeptId", message: "部门必填", action: "keyup,blur", rule: function () {
                                return $("#ddlDeptId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlFinancingBankId", message: "融资银行必填", action: "keyup,blur", rule: function () {
                                return $("#ddlFinancingBankId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlFinancingAccountId", message: "融资银行账户必填", action: "keyup,blur", rule: function () {
                                return $("#ddlFinancingAccountId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlAssetId", message: "融资品种必填", action: "keyup,blur", rule: function () {
                                return $("#ddlAssetId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlExchangeId", message: "期货头寸所在交易所必填", action: "keyup,blur", rule: function () {
                                return $("#ddlExchangeId").jqxComboBox("val") > 0;
                            }
                        }
                    ]
            });
        });
    </script>

    <div id="jqxStockExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            实货描述
        </div>
        <div style="height: 500px;">
            <input type="button" id="btnAddStock" value="添加" style="width: 120px; height: 25px; margin: 5px 0 0 5px" />
            <input type="button" id="btnMultiAddStock" value="批量添加" style="width: 120px; height: 25px; margin: 5px 0 0 5px" />
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAddStock").jqxButton({ height: 25, width: 100 });
            $("#btnAddStock").click(function () {
                $("#popupWindow").jqxWindow("show");
                $("#Save").jqxButton({ disabled: false });
                $("#popupWindow").jqxValidator({ arrow: false });

                //$("#txbContractNo").jqxInput("val", "");
                $("#txbRefNo").jqxInput("val", "");
                $("#nbNetAmount").jqxNumberInput("val", 0);
                $("#txbDeadline").jqxInput("val", "");
                $("#txbMemo").jqxInput("val", "");

                $("#txbRefNo").jqxInput("focus");
            });

            $("#btnMultiAddStock").jqxButton({ height: 25, width: 100 });
            $("#btnMultiAddStock").click(function () {
                document.getElementById("editor").value = "";
                $("#popupWindow_Multi").jqxWindow("open");

                document.getElementById("editor").focus();
            });

            var url = "Handler/FinancingPledgeApplyStockDetailHandler.ashx";
            var source =
            {
                localdata: "",
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
                   { name: "Memo", type: "string" },
                   { name: "DetailStatus", type: "int" },
                   { name: "StatusName", type: "string" }
                ],
                datatype: "local",
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
                  { text: "合同号", datafield: "ContractNo", cellclassname: "GridFillNumber",width:"15%" },
                  {
                      text: "净重（吨）", datafield: "NetAmount", cellclassname: "GridFillNumber", width: "15%", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue) {
                                        return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                    }
                      }]
                  },
                  { text: "业务单号", datafield: "RefNo", cellclassname: "GridFillNumber", width: "15%" },
                  { text: "期限", datafield: "Deadline", cellclassname: "GridFillNumber", width: "15%" },
                  { text: "备注", datafield: "Memo", cellclassname: "GridFillNumber" },
                  {
                      text: "操作", datafield: "Edit", width: "10%", columntype: "button", cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var rowscount = $("#jqxStockGrid").jqxGrid("getdatainformation").rowscount;
                          if (row >= 0 && row < rowscount) {
                              var id = $("#jqxStockGrid").jqxGrid("getrowid", row);
                              var commit = $("#jqxStockGrid").jqxGrid("deleterow", id);
                          }
                      }
                  }
                ]
            });
        });
    </script>

    <div style="text-align: center; width: 80%; margin-top: 0;">
        <input type="button" id="btnSubmit" value="确认并提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnSave" value="保存" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="FinancingPledgeApplyList.aspx" id="btnCancel" style="margin-left: 10px">取消</a>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").jqxButton({ height: 25, width: 120 });

            function PledgeApplyCreate(isAudit,buttonName) {

                var isCanSubmit = $("#jqxApplyExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var rows = $("#jqxStockGrid").jqxGrid("getrows");
                if (rows.length < 1) {
                    alert("请先添加质押申请单明细");
                    return;
                }

                if (!confirm("确认添加质押申请单？")) { return; }
                $(buttonName).jqxButton({ disabled: true });

                var pledgeApply = {
                    //PledgeApplyId: 
                    DeptId: $("#ddlDeptId").val(),
                    ApplyTime: $("#dtApplyTime").jqxDateTimeInput("val"),
                    FinancingBankId: $("#ddlFinancingBankId").val(),
                    FinancingAccountId: $("#ddlFinancingAccountId").val(),
                    AssetId: $("#ddlAssetId").val(),
                    SwitchBack: $("#rbYes").val() ? true : false,
                    ExchangeId: $("#ddlExchangeId").val()
                    //SumNetAmount
                    //SumHands
                };

                $.post("Handler/FinancingPledgeApplyCreateHandler.ashx", { pledgeApply: JSON.stringify(pledgeApply), rows: JSON.stringify(rows), isAudit: isAudit },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "FinancingPledgeApplyList.aspx";                            
                        } else {
                            $(buttonName).jqxButton({ disabled: false });
                        }
                    }
                );
            }

            $("#btnSubmit").click(function () { PledgeApplyCreate(true, "#btnSubmit"); });

            $("#btnSave").jqxButton({ height: 25, width: 100 });
            $("#btnSave").click(function () { PledgeApplyCreate(false, "#btnSave"); });

            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });
        });
    </script>

    <div id="popupWindow_Multi" style="display: none">
        <div>添加实货描述</div>
        <div>
            <ul style="list-style-type:none;">
                <li>
                    <span>【合同号】   【净重】   【业务单号】   【期限】   【备注】</span>
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
                autoOpen: false, isModal: true, width: 900, position: 'top, left', height: 500, maxWidth: 1200, resizable: false, cancelButton: $("#Cancel_Multi"), modalOpacity: 0.01,position:"center"
                //,initContent: function () {
                //    $("#editor").jqxEditor({ tools: 'bold italic underline font size', width: "800", height: "400" });                    
                //}
            });
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

    <div id="popupWindow" style="display: none">
        <div>添加实货描述</div>
        <div class="DataExpander">
            <ul>
                <li>
                    <span>合同号：</span>
                    <input type="text" id="txbContractNo" />
                </li>
                <li>
                    <span>业务编号：</span>
                    <input type="text" id="txbRefNo"/>
                    <%--<div id="txbRefNo"></div>--%>
                </li>
                <li>
                    <span>净重(吨)：</span>
                    <div id="nbNetAmount"></div>
                </li>
                <li>
                    <span>期限：</span>
                    <input type="text" id="txbDeadline" />
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
            //合同号
            $("#txbContractNo").jqxInput({ height: 25, width: 160 });

            var source =
            {
                datatype: "json",
                datafields: [
                    { name: 'StockSerial' },
                    { name: 'RefNo' },
                    { name: 'NetAmount' }
                ],
                url: "Handler/GetStockInfoFromBusinessSystemHandler.ashx"
            };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#txbRefNo").jqxInput({ source: dataAdapter, displayMember: "RefNo", valueMember: "NetAmount", width: 160, height: 25 });

            //净重
            $("#nbNetAmount").jqxNumberInput({ width: 160, height: 25, decimalDigits: 4, digits: 6, spinButtons: true, symbolPosition: "right", symbol: "吨" });

            $("#txbRefNo").on('select', function (event) {
                if (event.args) {
                    var item = event.args.item;
                    if (item) {
                        $.get("Handler/GetFinStockInfo.ashx", { text: item.label }, function (result) {
                                var obj = JSON.parse(result);
                                if (obj.ResultStatus.toString() == "0") {
                                    //alert(obj.Message);
                                    $("#nbNetAmount").val(result.ReturnValue);
                                } else {
                                    $("#nbNetAmount").val(item.value);
                                }
                            }
                        );
                    }
                }
            });

            $("#txbRefNo").on('change', function (event) {
                if (event.args) {
                    var item = event.args.item;
                    if (item) {
                        $.get("Handler/GetFinStockInfo.ashx", { text: item.label }, function (result) {
                                var obj = JSON.parse(result);
                                if (obj.ResultStatus.toString() == "0") {
                                    //alert(obj.Message);
                                    $("#nbNetAmount").val(result.ReturnValue);
                                } else {
                                    $("#nbNetAmount").val(item.value);
                                }
                            }
                        );
                    }
                }
            });

            //期限
            $("#txbDeadline").jqxInput({ height: 25, width: 160 });

            //备注
            $("#txbMemo").jqxInput({ height: 25, width: 160 });

            $("#popupWindow").jqxWindow({ width: 350, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01, position: "center" });
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
                        { input: "#txbContractNo", message: "请输入合同号", action: "keyup,blur", rule: "required" },
                        {
                            input: "#nbNetAmount", message: "净重必须大于0", action: "keyup,blur", rule: function () {
                                return $("#nbNetAmount").jqxNumberInput("val") > 0;
                            }
                        },
                        { input: "#txbRefNo", message: "请填写业务单号", action: "keyup,blur", rule: "required" },
                        { input: "#txbDeadline", message: "请输入期限", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#Save").click(function () {
                var isCanSubmit = $("#popupWindow").jqxValidator("validate");
                if (!isCanSubmit) { return; }
                $("#Save").jqxButton({ disabled: true });

                var datarow = {
                    //StockDetailId:
                    //PledgeApplyId:
                    ContractNo: $("#txbContractNo").val(),
                    NetAmount: $("#nbNetAmount").val(),
                    //StockId: $("#hidStockId").val(),
                    RefNo: document.getElementById('txbRefNo').value,
                    Deadline: $("#txbDeadline").val(),
                    Memo: $("#txbMemo").val()
                };
                var commit = $("#jqxStockGrid").jqxGrid("addrow", null, datarow);
                if (commit)
                    $("#popupWindow").jqxWindow("hide");

            });
        });
    </script>

</body>
</html>
