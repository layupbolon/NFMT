<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SIUpdate.aspx.cs" Inherits="NFMTSite.Invoice.SIUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价外票修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var details = new Array();
        var bala = 0;

        $(document).ready(function () {
            $("#jqxSIExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxAllotExpander").jqxExpander({ width: "98%" });

            //实际发票号
            $("#txbInvoiceName").jqxInput({ height: 23 });

            //发票日期
            $("#dtInvoiceDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //发票金额
            $("#nbInvoiceBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });
            $("#nbInvoiceBala").on("valueChanged", function (event) {
                var value = parseFloat(event.args.value);
                var rate = parseFloat($("#ddlChangeRate").val());
                $("#nbChangeBala").jqxNumberInput("val", parseFloat(value * rate));
            });

            //发票币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, searchMode: "containsignorecase" });

            //折算币种
            var changecySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var changecyDataAdapter = new $.jqx.dataAdapter(changecySource);
            $("#ddlChangeCurrencyId").jqxComboBox({ source: changecyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, searchMode: "containsignorecase" });

            //折算汇率
            $("#ddlChangeRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbolPosition: "right", spinButtons: true, width: 100 });
            $("#ddlChangeRate").on("valueChanged", function (event) {
                var value = parseFloat(event.args.value);
                var amount = parseFloat($("#nbInvoiceBala").val());
                $("#nbChangeBala").jqxNumberInput("val", parseFloat(value * amount));
            });

            //折算金额, Digits: 3
            $("#nbChangeBala").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, symbolPosition: "right", spinButtons: true, width: 140 });

            //收票公司
            var outCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
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
            $("#ddlInnerCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //开票公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
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
            $("#ddlOutCorp").jqxComboBox({ source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //申请部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#ddlPayDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", height: 25, width: 120, disabled: true });
            $("#ddlPayDept").jqxComboBox("val", "<%=this.deptId%>");

            //备注
            $("#txbMemo").jqxInput({ height: 23 });


            /////////////////////可选业务单号/////////////////////

            //业务单号
            $("#txbRefNo").jqxInput({ height: 23 });
            $("#btnSearch").jqxButton({ height: 25, width: 100 });

            var feeTypeSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailName", type: "string" },
                    { name: "StyleDetailId", type: "int" }
                ], url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=this.styleId%>", async: false
            };
            var feeTypeAdapter = new $.jqx.dataAdapter(feeTypeSource, { autoBind: true });

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockLogId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "SubContractId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "FeeTypeName", value: "FeeType", values: { source: feeTypeAdapter.records, value: "StyleDetailId", name: "DetailName" } },
                   { name: "FeeType", type: "string" },
                   { name: "DetailBala", type: "string" },
                   { name: "OutContractNo", type: "string" }
                ],
                sort: function () {
                    $("#jqxStockGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxStockGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "sto.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/SICreateStockListHandler.ashx"
            };
            var DataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                source: DataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                editable: true,
                selectionmode: "singlecell",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "外部合约号", datafield: "OutContractNo", editable: false, width: "7%" },
                  { text: "内部子合约号", datafield: "SubNo", editable: false, width: "9%" },
                  { text: "业务单号", datafield: "RefNo", editable: false, width: "6%" },
                  { text: "所属公司", datafield: "CorpName", editable: false, width: "9%" },
                  { text: "品种", datafield: "AssetName", editable: false, width: "5%" },
                  { text: "品牌", datafield: "BrandName", editable: false, width: "7%" },
                  { text: "交货地", datafield: "DPName", editable: false, width: "7%" },
                  { text: "卡号", datafield: "CardNo", editable: false, width: "10%" },
                  {
                      text: "发票内容", datafield: "FeeType", displayfield: "FeeTypeName", columntype: "combobox",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: feeTypeAdapter, displayMember: "DetailName", valueMember: "StyleDetailId" });
                      }, sortable: false, cellclassname: "GridFillNumber"
                  },
                  {
                      text: "分配金额", datafield: "DetailBala", columntype: "numberinput", createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 2, spinButtons: true });
                      }, sortable: false, cellclassname: "GridFillNumber"
                  },
                  { text: "操作", datafield: "StockId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            $("#btnSearch").click(function () {
                source.url = "Handler/SICreateStockListHandler.ashx?r=" + $("#txbRefNo").val();
                $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
            });


            /////////////////////已分配/////////////////////

            allotsource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockLogId", type: "int" },
                   { name: "ContractId", type: "int" },
                   { name: "SubContractId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "FeeTypeName", type: "string" },
                   { name: "FeeType", type: "string" },
                   { name: "DetailBala", type: "string" },
                   { name: "OutContractNo", type: "string" }
                ],
                localdata: $("#hidDetails").val()
            };
            var allotDataAdapter = new $.jqx.dataAdapter(allotsource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxAllotGrid").jqxGrid(
            {
                width: "98%",
                source: allotDataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "外部合约号", datafield: "OutContractNo", editable: false, width: "7%" },
                  { text: "内部子合约号", datafield: "SubNo", editable: false, width: "9%" },
                  { text: "业务单号", datafield: "RefNo", editable: false, width: "6%" },
                  { text: "所属公司", datafield: "CorpName", editable: false, width: "9%" },
                  { text: "品种", datafield: "AssetName", editable: false, width: "5%" },
                  { text: "品牌", datafield: "BrandName", editable: false, width: "7%" },
                  { text: "交货地", datafield: "DPName", editable: false, width: "7%" },
                  { text: "卡号", datafield: "CardNo", editable: false, width: "10%" },
                  { text: "发票内容", datafield: "FeeTypeName" },
                  { text: "分配金额", datafield: "DetailBala" },
                  { text: "操作", datafield: "StockId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            details = $("#jqxAllotGrid").jqxGrid("getrows");

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //验证
            $("#jqxSIExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbInvoiceName", message: "实际发票号必填", action: "keyup,blur", rule: "required" },
                        {
                            input: "#ddlCurrencyId", message: "发票币种必选", action: "change", rule: function (input, commit) {
                                return $("#ddlCurrencyId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlChangeCurrencyId", message: "折算币种必选", action: "change", rule: function (input, commit) {
                                return $("#ddlChangeCurrencyId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlInnerCorp", message: "收票公司必选", action: "change", rule: function (input, commit) {
                                return $("#ddlInnerCorp").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlOutCorp", message: "开票公司必选", action: "change", rule: function (input, commit) {
                                return $("#ddlOutCorp").jqxComboBox("val") > 0;
                            }
                        }
                    ]
            });

            //赋值
            $("#txbInvoiceName").val("<%=this.InvoiceName%>");
            $("#dtInvoiceDate").val(new Date("<%=this.InvoiceDate%>"));
            $("#nbInvoiceBala").val("<%=this.InvoiceBala%>");
            $("#ddlCurrencyId").val("<%=this.CurrencyId%>");
            $("#ddlChangeCurrencyId").val("<%=this.ChangeCurrencyId%>");
            $("#ddlChangeRate").val("<%=this.ChangeRate%>");
            $("#nbChangeBala").val("<%=this.ChangeBala%>");
            $("#ddlOutCorp").val("<%=this.OutCorpId%>");
            $("#ddlInnerCorp").val("<%=this.InCorpId%>");
            $("#ddlPayDept").val("<%=this.PayDept%>");
            $("#txbMemo").val("<%=this.Memo%>");

            bala = "<%=this.InvoiceBala%>";


            //修改
            $("#btnUpdate").click(function () {

                var isCanSubmit = $("#jqxSIExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认提交修改？")) { return; }

                $("#btnUpdate").jqxButton({ disabled: true });

                var invoice = {
                    InvoiceId: "<%=this.invoiceId%>",
                    InvoiceDate: $("#dtInvoiceDate").val(),
                    //InvoiceNo
                    InvoiceName: $("#txbInvoiceName").val(),
                    InvoiceType: "<%=this.invoiceType%>",
                    InvoiceBala: $("#nbInvoiceBala").val(),
                    CurrencyId: $("#ddlCurrencyId").val(),
                    InvoiceDirection: "<%=this.invoiceDirection%>",
                    //OutBlocId
                    OutCorpId: $("#ddlOutCorp").val(),
                    InCorpId: $("#ddlInnerCorp").val(),
                    Memo: $("#txbMemo").val()
                };

                var SI = {
                    SIId: "<%=this.SIId%>",
                    InvoiceId: "<%=this.invoiceId%>",
                    ChangeCurrencyId: $("#ddlChangeCurrencyId").val(),
                    ChangeRate: $("#ddlChangeRate").val(),
                    ChangeBala: $("#nbChangeBala").val(),
                    PayDept: $("#ddlPayDept").val()
                };

                var rows = $("#jqxAllotGrid").jqxGrid("getrows");


                //附件信息
                var fileIds = new Array();
                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/SIUpdateHandler.ashx", {
                    invoice: JSON.stringify(invoice),
                    SI: JSON.stringify(SI),
                    SIDetail: JSON.stringify(rows)
                },
                function (result) {
                    var id = "<%=this.invoiceId%>";
                    AjaxFileUpload(fileIds, id, AttachTypeEnum.InvoiceAttach);
                    alert(result);
                    $("#btnUpdate").jqxButton({ disabled: false });
                    document.location.href = "SIList.aspx";
                });
            });

        });

        function bntAddOnClick(row, value) {
            var item = $("#jqxStockGrid").jqxGrid("getrowdata", row);

            if (item.DetailBala == undefined || item.DetailBala == 0
                || item.FeeType == undefined || item.FeeType == 0) {
                alert("请选择发票内容或输入金额");
                return;
            }

            details.push(item);

            bala = parseFloat(bala) + parseFloat(item.DetailBala);
            $("#nbInvoiceBala").val(bala);

            $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
            allotsource.localdata = details;
            $("#jqxAllotGrid").jqxGrid("updatebounddata", "rows");
        }

        function bntRemoveOnClick(row, value) {
            if (!confirm("确定取消？")) { return; }
            var item = $("#jqxAllotGrid").jqxGrid("getrowdata", row);
            bala = parseFloat(bala) - parseFloat(item.DetailBala);
            $("#nbInvoiceBala").val(bala);

            details.splice(row, 1);

            $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
            allotsource.localdata = details;
            $("#jqxAllotGrid").jqxGrid("updatebounddata", "rows");
        }


    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />

    <div id="jqxSIExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价外票信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>实际发票号：</strong>
                    <input type="text" id="txbInvoiceName" style="float: left;" />
                </li>
                <li>
                    <strong>开票日期：</strong>
                    <div id="dtInvoiceDate" style="float: left;" />
                </li>
                <li>
                    <strong>发票金额：</strong>
                    <div id="nbInvoiceBala" style="float: left;" />
                </li>
                <li>
                    <strong>发票币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;" />
                </li>
                <li>
                    <strong>折算币种：</strong>
                    <div id="ddlChangeCurrencyId" style="float: left;" />
                </li>
                <li>
                    <strong>折算汇率：</strong>
                    <div id="ddlChangeRate" style="float: left;" />
                </li>
                <li>
                    <strong>折算金额：</strong>
                    <div id="nbChangeBala" style="float: left;" />
                </li>
                <li>
                    <strong>收票公司：</strong>
                    <div id="ddlInnerCorp" style="float: left;" />
                </li>
                <li>
                    <strong>开票公司：</strong>
                    <div id="ddlOutCorp" style="float: left;" />
                </li>
                <li>
                    <strong>申请部门：</strong>
                    <div id="ddlPayDept" style="float: left;" />
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" id="txbMemo" style="float: left;" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可选业务单号
        </div>
        <div>
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span style="float: left;">业务单号：</span>
                        <input type="text" id="txbRefNo" style="float: left;" />
                    </li>
                    <li>
                        <input type="button" id="btnSearch" value="查询" />
                    </li>
                </ul>
            </div>
            <div id="jqxStockGrid"></div>
        </div>
    </div>

    <div id="jqxAllotExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            已分配
                        <input type="hidden" id="hidDetails" runat="server" />
        </div>
        <div>
            <div id="jqxAllotGrid"></div>
        </div>
    </div>

    <nfmt:attach runat="server" id="attach1" attachstyle="Update" attachtype="InvoiceAttach" />

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交修改" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <a target="_self" runat="server" href="SIList.aspx" id="btnCancel">取消</a>
    </div>

</body>
</html>
