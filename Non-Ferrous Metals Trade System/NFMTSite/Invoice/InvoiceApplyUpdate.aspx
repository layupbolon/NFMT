<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApplyUpdate.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceApplyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发票申请修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#jqxInvApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxCanApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var DetailIds = new Array();
            var details = new Array();

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
                return cellHtml + value + $("#hidCurrencyName").val() + "</div>";
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
                  { text: "子合约号", datafield: "SubNo", width: 100 },
                  { text: "外部合约号", datafield: "OutContractNo", width: 120 },
                  { text: "卡号", datafield: "CardNo", width: 100 },
                  { text: "客户", datafield: "CorpName", width: 160 },
                  { text: "品种", datafield: "AssetName", width: 80 },
                  { text: "净重", datafield: "NetAmount", width: 100 },
                  { text: "单位", datafield: "MUName", width: 70 },
                  { text: "价格", datafield: "InvoicePrice", width: 100, cellsrenderer: cellsrenderer },
                  { text: "货款金额", datafield: "PaymentAmount", cellsrenderer: cellsrenderer },
                  { text: "利息金额", datafield: "InterestAmount", cellsrenderer: cellsrenderer },
                  { text: "其他金额", datafield: "OtherAmount", cellsrenderer: cellsrenderer },
                  { text: "开票金额", datafield: "InvoiceBala", cellsrenderer: cellsrenderer },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          if (!confirm("是否要取消所有该发票的信息？")) { return; }

                          var dataRecord = $("#jqxApplyGrid").jqxGrid("getrowdata", row);
                          var rows = $("#jqxApplyGrid").jqxGrid("getrows");
                          for (var i = 0; i < rows.length; i++) {
                              if (rows[i].InvoiceId == dataRecord.InvoiceId) {
                                  DetailIds.push(rows[i].DetailId);
                              }
                          }

                          var sids = "";
                          for (i = 0; i < DetailIds.length; i++) {
                              if (i != 0) { sids += ","; }
                              sids += DetailIds[i];
                          }

                          //刷新列表
                          Infosource.url = "Handler/InvoiceApplyDetailHandler.ashx?id=<%=this.invoiceApply.InvoiceApplyId%>" + "&DetailIds=" + sids;
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                          source.url = "Handler/InvoiceApplyUpdateInfoHandler.ashx?id=<%=this.invoiceApply.InvoiceApplyId%>" + "&DetailIds=" + sids;
                          $("#jqxCanApplyGrid").jqxGrid("updatebounddata", "rows");
                      }
                  }
                ]
            });


            //////////////////////////////////////////可申请发票信息//////////////////////////////////////////

            var sids = "";
            for (i = 0; i < DetailIds.length; i++) {
                if (i != 0) { sids += ","; }
                sids += DetailIds[i];
            }

            var formatedData = "";
            var totalrecords = 0;
            var source =
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
                   { name: "CardNo", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxCanApplyGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/InvoiceApplyUpdateInfoHandler.ashx?id=<%=this.invoiceApply.InvoiceApplyId%>"
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxCanApplyGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "发票金额", datafield: "InoviceBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "外部合约号", datafield: "OutContractNo" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "客户", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "单位", datafield: "MUName" },
                  { text: "价格", datafield: "InvoicePrice" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "申请";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxCanApplyGrid").jqxGrid("getrowdata", row);

                          $("#popupWindow").jqxWindow("show");

                          $("#hidInvoiceId").val(dataRecord.InvoiceId);
                          $("#hidBussinessInvoiceId").val(dataRecord.BussinessInvoiceId);
                          $("#hidContractId").val(dataRecord.ContractId);
                          $("#hidSubContractId").val(dataRecord.SubContractId);
                          $("#hidStockLogId").val(dataRecord.StockLogId);
                          $("#hidCurrencyName").val(dataRecord.CurrencyName);
                          $("#hidMUName").val(dataRecord.MUName);
                          $("#hidDetailId").val(dataRecord.DetailId);
                          $("#hidInvoiceApplyId").val(dataRecord.InvoiceApplyId);
                          $("#hidApplyId").val(dataRecord.ApplyId);

                          $("#txbInvoiceNo").jqxInput("val", dataRecord.InvoiceNo);
                          $("#dtInvoiceDate").jqxDateTimeInput("val", dataRecord.InvoiceDate);
                          $("#txbSubNo").jqxInput("val", dataRecord.SubNo);
                          $("#txbOutContractNo").jqxInput("val", dataRecord.OutContractNo);
                          $("#txbCardNo").jqxInput("val", dataRecord.CardNo);
                          $("#txbCorpName").jqxInput("val", dataRecord.CorpName);
                          $("#txbAssetName").jqxInput("val", dataRecord.AssetName);
                          $("#txbNetAmount").jqxInput("val", dataRecord.NetAmount + dataRecord.MUName);
                          $("#txbInvoicePrice").jqxInput("val", dataRecord.InvoicePrice);
                          $("#nbPaymentAmount").jqxNumberInput("val", 0);
                          $("#nbInterestAmount").jqxNumberInput("val", 0);
                          $("#nbOtherAmount").jqxNumberInput("val", 0);
                          $("#nbInvoiceBala").jqxNumberInput("val", dataRecord.InvoiceBala);

                          $("#nbPaymentAmount").jqxNumberInput({ symbol: dataRecord.CurrencyName });
                          $("#nbInterestAmount").jqxNumberInput({ symbol: dataRecord.CurrencyName });
                          $("#nbOtherAmount").jqxNumberInput({ symbol: dataRecord.CurrencyName });
                          $("#nbInvoiceBala").jqxNumberInput({ symbol: dataRecord.CurrencyName });

                          $("#hidRowId").val(row);
                      }
                  }
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
            $("#ddlApplyCorp").jqxComboBox({ source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", width: 180 });
            $("#ddlApplyCorp").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            $("#txbApplyDesc").jqxInput({ width: "500" });
            $("#txbApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //验证
            $("#jqxExpander").jqxValidator({
                rules:
                        [
                            {
                                input: "#ddlApplyCorp", message: "申请公司必选", action: "keyup,blur", rule: function (input, commit) {
                                    return $("#ddlApplyCorp").jqxComboBox("val") > 0;
                                }
                            }
                        ]
            });


            ////////////////////////  弹窗  ////////////////////////

            //发票编号
            $("#txbInvoiceNo").jqxInput({ height: 25, width: 160, disabled: true });

            //开票日期
            $("#dtInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 160, disabled: true });

            //子合约号
            $("#txbSubNo").jqxInput({ height: 25, width: 160, disabled: true });

            //外部合约号
            $("#txbOutContractNo").jqxInput({ height: 25, width: 145, disabled: true });

            //卡号
            $("#txbCardNo").jqxInput({ height: 25, width: 185, disabled: true });

            //客户
            $("#txbCorpName").jqxInput({ height: 25, width: 185, disabled: true });

            //品种
            $("#txbAssetName").jqxInput({ height: 25, width: 185, disabled: true });

            //净重
            $("#txbNetAmount").jqxInput({ height: 25, width: 185, disabled: true });

            //价格
            $("#txbInvoicePrice").jqxInput({ height: 25, width: 185, disabled: true });

            //货款金额
            $("#nbPaymentAmount").jqxNumberInput({ width: 160, height: 25, decimalDigits: 2, Digits: 9, spinButtons: true, symbolPosition: "right", symbol: "%" });

            //利息金额
            $("#nbInterestAmount").jqxNumberInput({ width: 160, height: 25, decimalDigits: 2, Digits: 9, spinButtons: true, symbolPosition: "right", symbol: "%" });

            //其他金额
            $("#nbOtherAmount").jqxNumberInput({ width: 160, height: 25, decimalDigits: 2, Digits: 9, spinButtons: true, symbolPosition: "right", symbol: "%" });

            //开票金额
            $("#nbInvoiceBala").jqxNumberInput({ width: 160, height: 25, decimalDigits: 2, Digits: 9, spinButtons: true, symbolPosition: "right", symbol: "%" });

            $("#popupWindow").jqxWindow({ width: 380, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01 });
            $("#Cancel").jqxButton({ height: 25, width: 100 });
            $("#Save").jqxButton({ height: 25, width: 100 });

            //验证器
            $("#popupWindow").jqxValidator({
                rules:
                    [
                        {
                            input: "#nbPaymentAmount", message: "货款金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbPaymentAmount').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbInterestAmount", message: "利息金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbInterestAmount').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbInvoiceBala", message: "开票金额必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbInvoiceBala').jqxNumberInput("val") > 0;
                            }
                        }
                    ]
            });

            $("#Save").click(function () {
                var isCanSubmit = $("#popupWindow").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var datarow = {
                    DetailId: $("#hidDetailId").val(),
                    InvoiceApplyId: $("#hidInvoiceApplyId").val(),
                    ApplyId: $("#hidApplyId").val(),
                    BussinessInvoiceId: $("#hidBussinessInvoiceId").val(),
                    InvoiceId: $("#hidInvoiceId").val(),
                    InvoiceNo: $("#txbInvoiceNo").val(),
                    InvoiceDate: $("#txbBussinessInvoiceId").val(),
                    InvoiceBala: $("#nbInvoiceBala").val(),
                    CurrencyName: $("#hidCurrencyName").val(),
                    SubNo: $("#txbSubNo").val(),
                    OutContractNo: $("#txbOutContractNo").val(),
                    CardNo: $("#txbCardNo").val(),
                    CorpName: $("#txbCorpName").val(),
                    AssetName: $("#txbAssetName").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    MUName: $("#hidMUName").val(),
                    InvoicePrice: $("#txbInvoicePrice").val(),
                    StockLogId: $("#hidStockLogId").val(),
                    ContractId: $("#hidContractId").val(),
                    SubContractId: $("#hidSubContractId").val(),
                    PaymentAmount: $("#nbPaymentAmount").val(),
                    InterestAmount: $("#nbInterestAmount").val(),
                    OtherAmount: $("#nbOtherAmount").val()
                };
                var commit = $("#jqxApplyGrid").jqxGrid("addrow", null, datarow);
                $("#popupWindow").jqxWindow("hide");

                //刷新
                var dataRecord = $("#jqxCanApplyGrid").jqxGrid("getrowdata", $("#hidRowId").val());

                var index = DetailIds.indexOf(parseInt($("#hidDetailId").val()));
                DetailIds.splice(index, 1);

                var sids = "";
                for (i = 0; i < DetailIds.length; i++) {
                    if (i != 0) { sids += ","; }
                    sids += DetailIds[i];
                }

                //刷新列表
                //Infosource.url = "Handler/InvoiceApplyDetailHandler.ashx?id=<%=this.invoiceApply.InvoiceApplyId%>" + "&DetailIds=" + sids;
                //$("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                source.url = "Handler/InvoiceApplyUpdateInfoHandler.ashx?id=<%=this.invoiceApply.InvoiceApplyId%>" + "&DetailIds=" + sids;
                $("#jqxCanApplyGrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确定提交修改？")) { return; }

                $("#btnUpdate").jqxButton({ disabled: true });

                var apply = {
                    ApplyId:"<%=this.apply.ApplyId%>",
                    ApplyDept: $("#ddlApplyDept").val(),
                    ApplyCorp: $("#ddlApplyCorp").val(),
                    ApplyDesc: $("#txbApplyDesc").val()
                }

                var invoiceApply = {
                    InvoiceApplyId : "<%=this.invoiceApply.InvoiceApplyId%>",
                    ApplyId : "<%=this.apply.ApplyId%>"
                }

                var rows = $("#jqxApplyGrid").jqxGrid("getrows");

                $.post("Handler/InvoiceApplyUpdateHandler.ashx", {
                    apply: JSON.stringify(apply),
                    invoiceApply: JSON.stringify(invoiceApply),
                    rows: JSON.stringify(rows)
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceApplyList.aspx";
                        }
                        else
                            $("#btnUpdate").jqxButton({ disabled: false });
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

    <div id="jqxCanApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可申请发票信息
        </div>
        <div>
            <div id="jqxCanApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            申请信息
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

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="InvoiceApplyList.aspx" id="btnCancel">取消</a>
    </div>

    <div id="popupWindow">
        <div>申请-详细信息</div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <input type="hidden" id="hidInvoiceId" />
                    <input type="hidden" id="hidBussinessInvoiceId" />
                    <input type="hidden" id="hidContractId" />
                    <input type="hidden" id="hidSubContractId" />
                    <input type="hidden" id="hidStockLogId" />
                    <input type="hidden" id="hidCurrencyName" />
                    <input type="hidden" id="hidMUName" />
                    <input type="hidden" id="hidRowId" />
                    <input type="hidden" id="hidDetailId" />
                    <input type="hidden" id="hidInvoiceApplyId" />
                    <input type="hidden" id="hidApplyId" />

                    <span>发票编号：</span>
                    <input type="text" id="txbInvoiceNo" />
                </li>
                <li>
                    <span>开票日期：</span>
                    <div id="dtInvoiceDate"></div>
                </li>
                <li>
                    <span>子合约号：</span>
                    <input type="text" id="txbSubNo" />
                </li>
                <li>
                    <span>外部合约号：</span>
                    <input type="text" id="txbOutContractNo" />
                </li>
                <li>
                    <span>卡号：</span>
                    <input type="text" id="txbCardNo" />
                </li>
                <li>
                    <span>客户：</span>
                    <input type="text" id="txbCorpName" />
                </li>
                <li>
                    <span>品种：</span>
                    <input type="text" id="txbAssetName" />
                </li>
                <li>
                    <span>净重：</span>
                    <input type="text" id="txbNetAmount" />
                </li>
                <li>
                    <span>价格：</span>
                    <input type="text" id="txbInvoicePrice" />
                </li>
                <li>
                    <span>货款金额：</span>
                    <div id="nbPaymentAmount"></div>
                </li>
                <li>
                    <span>利息金额：</span>
                    <div id="nbInterestAmount"></div>
                </li>
                <li>
                    <span>其他金额：</span>
                    <div id="nbOtherAmount"></div>
                </li>
                <li>
                    <span>开票金额：</span>
                    <div id="nbInvoiceBala"></div>
                </li>
                <li>
                    <input style="margin-right: 5px;" type="button" id="Save" value="确认" />
                    <input id="Cancel" type="button" value="取消" />
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
