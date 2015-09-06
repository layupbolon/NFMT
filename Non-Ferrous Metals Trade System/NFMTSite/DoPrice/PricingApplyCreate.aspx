<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingApplyCreate.aspx.cs" Inherits="NFMTSite.DoPrice.PricingApplyCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价申请新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxPricingApplyExpander").jqxExpander({ width: "98%" });

            //init stock list
            var formatedData = "";
            var totalrecords = 0;
            var selectSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockLogId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "GrossAmoutName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "StockStatusName", type: "string" },
                   { name: "GrossAmount", type: "number" },
                   { name: "MUName", type: "string" },
                   { name: "PricingWeight", type: "number" }
                ],
                sort: function () {
                    $("#jqxStockGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "slog.StockLogId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "slog.StockLogId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/PricingApplyCreateStockListHandler.ashx?subId=" + "<%=this.curContractSub.SubId%>"
            };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                autoheight: true,
                virtualmode: true,
                selectionmode: "singlecell",
                sorttogglestates: 1,
                sortable: true,
                editable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "交货地", datafield: "DPName", editable: false },
                  { text: "卡号", datafield: "CardNo", editable: false },
                  { text: "库存重量", datafield: "GrossAmoutName", editable: false },
                  { text: "归属公司", datafield: "CorpName", editable: false },
                  { text: "库存状态", datafield: "StockStatusName", editable: false },
                  {
                      text: "点价重量", datafield: "PricingWeight", width: 120, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber", validation: function (cell, value) {

                          var item = $("#jqxStockGrid").jqxGrid("getrowdata", cell.row);
                          if (value < 0 || value > item.GrossAmount) {
                              return { result: false, message: "点价重量不能小于0且不能大于库存重量：" + item.GrossAmount };
                          }
                          return true;
                      }, createeditor: function (row, cellvalue, editor) {
                          var r = $("#jqxStockGrid").jqxGrid("getrowdata", row);
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 120, Digits: 8, spinButtons: true, symbolPosition: "right", symbol: r.MUName });
                      }
                  }
                ]
            });

            $("#jqxStockGrid").on("cellvaluechanged", function (event) {
                var column = args.datafield;
                var newvalue = args.newvalue;
                var oldvalue = args.oldvalue;
                if (oldvalue == undefined) { oldvalue = 0; }
                if (column == "PricingWeight") {
                    var value = parseFloat($("#nbPricingWeight").val()) - parseFloat(oldvalue) + parseFloat(newvalue);
                    $("#nbPricingWeight").val(value);
                }
            });

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx?SubId=" + "<%=this.curContractSub.SubId%>";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase",selectedIndex:0
            });

            //申请部门, autoDropDownHeight: true
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDept").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDept").jqxComboBox("val", "<%=this.deptId%>");

            //申请备注
            $("#txbApplyDesc").jqxInput({ height: 25 });

            //点价起始时间
            $("#dtStartTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //点价最终时间
            $("#dtEndTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //点价最低均价, disabled: true
            $("#nbMinPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //点价最高均价
            $("#nbMaxPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //价格币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.currencyId%>");

            //点价公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isOut=0&ContractId=" + "<%=this.curContract.ContractId%>";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlPricingCorpId").jqxComboBox({ source: outCorpDataAdapter, selectedIndex: 0, autoDropDownHeight: true, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //点价重量
            $("#nbPricingWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true });

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#ddlMUId").jqxDropDownList("val", "<%=this.mUId%>");

            //点价品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssertId").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlAssertId").jqxDropDownList("val", "<%=this.assetId%>");

            //点价权限人
            var personSource = { datatype: "json", url: "../BasicData/Handler/PricingPersonDDLHandler.ashx?isOut=0&ContractId=" + "<%=this.curContract.ContractId%>", async: false };
            var personDataAdapter = new $.jqx.dataAdapter(personSource);
            $("#ddlPricingPersoinId").jqxComboBox({ source: personDataAdapter, displayMember: "PricingName", valueMember: "PersoinId", width: 100, height: 25 });

            //QP日期
            $("#dtQPDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //其他费用
            $("#nbOtherFee").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true });

            //其他费用描述
            $("#txbOtherDesc").jqxInput({ height: 25 });

            //点价方式
            var summaryPriceStyle = $("#hidSummaryPrice").val();
            var summaryPriceSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + summaryPriceStyle, async: false };
            var summaryPriceDataAdapter = new $.jqx.dataAdapter(summaryPriceSource);
            $("#ddlPricingStyle").jqxComboBox({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });
            $("#ddlPricingStyle").on("change", function (event) {
                var args = event.args;
                if (args) {
                    if (args.index == 0 && args.item.label == "均价") {
                        $(".hiddenClass").show();
                    }
                    else if (args.index == 1 && args.item.label == "点价") {
                        $(".hiddenClass").hide();
                    }
                }
            });

            //宣布日
            $("#dtDeclareDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //均价起始计价日
            $("#dtAvgPriceStart").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //均价终止计价日
            $("#dtAvgPriceEnd").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //初始化时影藏
            $(".hiddenClass").hide();

            //buttons
            $("#btnCreate").jqxButton({ height: 25, width: 120 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 120 });

            //验证
            $("#jqxPricingApplyExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlApplyCorp", message: "申请公司必选", action: "change", rule: function (input, commit) {
                                return $("#ddlApplyCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlApplyDept", message: "申请部门必选", action: "change", rule: function (input, commit) {
                                return $("#ddlApplyDept").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#nbMinPrice", message: "最低均价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbMinPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbMaxPrice", message: "最高均价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbMaxPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbMaxPrice", message: "最高均价必须大于最低均价", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbMaxPrice").jqxNumberInput("val") > $("#nbMinPrice").jqxNumberInput("val");
                            }
                        },
                        {
                            input: "#ddlPricingCorpId", message: "点价公司必选", action: "change", rule: function (input, commit) {
                                return $("#ddlPricingCorpId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#nbPricingWeight", message: "点价重量必须大于0且不大于" + "<%=this.curContractSub.SignAmount - this.alreadyPricingWeight%>", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbPricingWeight").jqxNumberInput("val") > 0 && $("#nbPricingWeight").jqxNumberInput("val") <= "<%=this.curContractSub.SignAmount - this.alreadyPricingWeight%>";
                            }
                        },
                        {
                            input: "#ddlPricingStyle", message: "点价方式必选", action: "change", rule: function (input, commit) {
                                return $("#ddlPricingStyle").val() > 0;
                            }
                        }
                    ]
            });

            //新增
            $("#btnCreate").click(function () {

                var isCanSubmit = $("#jqxPricingApplyExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认点价申请？")) { return; }
                $("#btnCreate").jqxButton({ disabled: true });
                var apply = {
                    ApplyCorp: $("#ddlApplyCorp").val(),
                    ApplyDept: $("#ddlApplyDept").val(),
                    ApplyDesc: $("#txbApplyDesc").val()
                }

                var pricingPersionId = 0;
                if ($("#ddlPricingPersoinId").val() > 0) {
                    pricingPersionId = $("#ddlPricingPersoinId").val();
                }

                var pricingApply = {
                    //ApplyId
                    SubContractId: "<%=this.curContractSub.SubId%>",
                    ContractId: "<%=this.curContract.ContractId%>",
                    //PricingDirection
                    QPDate: $("#dtQPDate").val(),
                    //DelayAmount
                    //DelayFee
                    DelayQPDate: $("#dtQPDate").val(),//录入时【延期QP】取【QP日期】
                    OtherFee: $("#nbOtherFee").val(),
                    OtherDesc: $("#txbOtherDesc").val(),
                    StartTime: $("#dtStartTime").val(),
                    EndTime: $("#dtEndTime").val(),
                    MinPrice: $("#nbMinPrice").val(),
                    MaxPrice: $("#nbMaxPrice").val(),
                    CurrencyId: $("#ddlCurrencyId").val(),
                    //PricingBlocId
                    PricingCorpId: $("#ddlPricingCorpId").val(),
                    PricingWeight: $("#nbPricingWeight").val(),
                    MUId: $("#ddlMUId").val(),
                    AssertId: $("#ddlAssertId").val(),
                    PricingPersoinId: pricingPersionId,
                    PricingStyle: $("#ddlPricingStyle").val(),
                    DeclareDate: $("#dtDeclareDate").val(),
                    AvgPriceStart: $("#dtAvgPriceStart").val(),
                    AvgPriceEnd: $("#dtAvgPriceEnd").val()
                }

                var rows = $("#jqxStockGrid").jqxGrid("getrows");

                $.post("Handler/PricingApplyCreateHandler.ashx", {
                    apply: JSON.stringify(apply),
                    pricingApply: JSON.stringify(pricingApply),
                    detail: JSON.stringify(rows)
                },
                function (result) {

                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    $("#btnCreate").jqxButton({ disabled: false });
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "PricingApplyList.aspx";
                    }
                });
            });

        });
    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            子合约库存列表
        </div>
        <div>
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxPricingApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            点价申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请公司：</strong>
                    <div id="ddlApplyCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div id="ddlApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>申请备注：</strong>
                    <input type="text" id="txbApplyDesc" style="float: left;" />
                </li>
                <li>
                    <strong>起始时间：</strong>
                    <div id="dtStartTime" style="float: left;"></div>
                </li>
                <li>
                    <strong>最终时间：</strong>
                    <div id="dtEndTime" style="float: left;"></div>
                </li>
                <li>
                    <strong>最低均价：</strong>
                    <div id="nbMinPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>最高均价：</strong>
                    <div id="nbMaxPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>价格币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价公司：</strong>
                    <div id="ddlPricingCorpId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价重量：</strong>
                    <div id="nbPricingWeight" style="float: left;" />
                </li>
                <li>
                    <strong>重量单位：</strong>
                    <div id="ddlMUId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价品种：</strong>
                    <div id="ddlAssertId" style="float: left;"></div>
                </li>
                <li>
                    <strong>点价权限人：</strong>
                    <div id="ddlPricingPersoinId" style="float: left;"></div>
                </li>
                <li>
                    <strong>QP日期：</strong>
                    <div id="dtQPDate" style="float: left;"></div>
                </li>
                <li>
                    <strong>其他费：</strong>
                    <div id="nbOtherFee" style="float: left;"></div>
                </li>
                <li>
                    <strong>其他费描述：</strong>
                    <input type="text" id="txbOtherDesc" style="float: left;" />
                </li>
                <li>
                    <strong>点价方式：</strong>
                    <input type="hidden" id="hidSummaryPrice" runat="server" />
                    <div id="ddlPricingStyle" style="float: left;"></div>
                </li>
                <li class="hiddenClass">
                    <strong>宣布日：</strong>
                    <div id="dtDeclareDate" style="float: left;"></div>
                </li>
                <li class="hiddenClass">
                    <strong>均价起始计价日：</strong>
                    <div id="dtAvgPriceStart" style="float: left;"></div>
                </li>
                <li class="hiddenClass">
                    <strong>均价终止计价日：</strong>
                    <div id="dtAvgPriceEnd" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="新增点价申请" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="PricingApplyContractList.aspx" id="btnCancel">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

</body>
</html>
