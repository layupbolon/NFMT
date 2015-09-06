<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomsClearanceUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.CustomsClearanceUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报关修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>   

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxCustomExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var stockIdsDown = new Array();
            var stockIdsUp = new Array();

            var sidsDown = $("#hidsidsDown").val();
            var splitItem = sidsDown.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIdsDown.push(splitItem[i]);
                }
            }

            var sidsUp = $("#hidsidsUp").val();
            splitItem = sidsUp.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIdsUp.push(splitItem[i]);
                }
            }

            /////////////////////////报关信息/////////////////////////

            var deliverPlaceSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DPName", type: "string" },
                    { name: "DPId", type: "int" }
                ],
                type: "GET",
                url: "../BasicData/Handler/DeliverPlaceDDLHandler.ashx"
            };
            var deliverPlaceDataAdapter = new $.jqx.dataAdapter(deliverPlaceSource, { autoBind: true });

            var url = "Handler/CustomUpdateStockListHandler.ashx?sids=" + stockIdsUp + "&customsId=" + "<%=this.customsClearance.CustomsId%>";
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpId", type: "int" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "Bundles", type: "int" },
                   { name: "UintId", type: "int" },
                   { name: "CurGrossAmountName", type: "string" },
                   { name: "CurNetAmountName", type: "string" },
                   { name: "CurGrossAmount", type: "number" },
                   { name: "CurNetAmount", type: "number" },
                   { name: "BrandName", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "DeliverPlaceId", type: "int" },
                   { name: "DeliverPlace", type: "string" },
                   { name: "CardNo", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxCustomGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "cad.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cad.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var moveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnDelete\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" />";
            }

            $("#jqxCustomGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                editable: true,
                selectionmode: "singlecell",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "入库时间", datafield: "StockDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "所属公司", datafield: "CorpName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "捆数", datafield: "Bundles", editable: false },
                  { text: "当前毛重", datafield: "CurGrossAmountName", editable: false },
                  { text: "当前净重", datafield: "CurNetAmountName", editable: false },
                  { text: "报关状态", datafield: "CustomsTypeName", editable: false },
                  {
                      text: "交货地", datafield: "DeliverPlaceId", displayfield: "DeliverPlace", width: 100, cellclassname: "GridFillNumber", columntype: "combobox",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: deliverPlaceDataAdapter, displayMember: "DPName", valueMember: "DPId", autoDropDownHeight: true });
                      }
                  },
                  { text: "卡号", datafield: "CardNo", cellclassname: "GridFillNumber" },

                  { text: "操作", datafield: "StockId", cellsrenderer: moveRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var deletes = $("input[name=\"btnDelete\"]");
                for (i = 0; i < deletes.length; i++) {
                    var btnDelete = deletes[i];
                    var val = btnDelete.id;

                    $(btnDelete).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        //if (!confirm("确定取消？")) { return; }

                        var index = stockIdsUp.indexOf(rowId);
                        stockIdsUp.splice(index, 1);

                        var sidsUp = "";
                        for (i = 0; i < stockIdsUp.length; i++) {
                            if (i != 0) { sidsUp += ","; }
                            sidsUp += stockIdsUp[i];
                        }

                        stockIdsDown.push(rowId);

                        var sidsDown = "";
                        for (i = 0; i < stockIdsDown.length; i++) {
                            if (i != 0) { sidsDown += ","; }
                            sidsDown += stockIdsDown[i];
                        }

                        //刷新列表
                        Infosource.url = "Handler/CustomUpdateStockListHandler.ashx?sids=" + sidsUp + "&customsId=" + "<%=this.customsClearance.CustomsId%>";
                        $("#jqxCustomGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/CustomClearanceStockListHandler.ashx?c=" + "<%=this.customsClearance.CustomsApplyId%>" + "&sids=" + sidsDown;
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            /////////////////////////可报关库存信息/////////////////////////

            var url = "Handler/CustomClearanceStockListHandler.ashx?c=" + "<%=this.customsClearance.CustomsApplyId%>" + "&sids=" + sidsDown;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpId", type: "int" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "Bundles", type: "int" },
                   { name: "UintId", type: "int" },
                   { name: "CurGrossAmountName", type: "string" },
                   { name: "CurNetAmountName", type: "string" },
                   { name: "CurGrossAmount", type: "number" },
                   { name: "CurNetAmount", type: "number" },
                   { name: "BrandName", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" }
                ],
                datatype: "json",
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
                sortcolumn: "cad.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cad.DetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var moveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnMove\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"报关\" />";
            }

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "入库时间", datafield: "StockDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "所属公司", datafield: "CorpName", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "捆数", datafield: "Bundles", editable: false },
                  { text: "当前毛重", datafield: "CurGrossAmountName", editable: false },
                  { text: "当前净重", datafield: "CurNetAmountName", editable: false },
                  { text: "报关状态", datafield: "CustomsTypeName", editable: false },
                  { text: "操作", datafield: "StockId", cellsrenderer: moveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var moves = $("input[name=\"btnMove\"]");
                for (i = 0; i < moves.length; i++) {
                    var btnMove = moves[i];
                    var val = btnMove.id;

                    $(btnMove).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        //if (!confirm("确定报关？")) { return; }

                        var index = stockIdsDown.indexOf(rowId);
                        stockIdsDown.splice(index, 1);

                        var sidsDown = "";
                        for (i = 0; i < stockIdsDown.length; i++) {
                            if (i != 0) { sidsDown += ","; }
                            sidsDown += stockIdsDown[i];
                        }

                        stockIdsUp.push(rowId);

                        var sidsUp = "";
                        for (i = 0; i < stockIdsUp.length; i++) {
                            if (i != 0) { sidsUp += ","; }
                            sidsUp += stockIdsUp[i];
                        }

                        //刷新列表
                        Infosource.url = "Handler/CustomUpdateStockListHandler.ashx?sids=" + sidsUp + "&customsId=" + "<%=this.customsClearance.CustomsId%>";
                        $("#jqxCustomGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/CustomClearanceStockListHandler.ashx?c=" + "<%=this.customsClearance.CustomsApplyId%>" + "&sids=" + sidsDown;
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });


            //实际报关公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlCustomsCorpId").jqxComboBox("val", "<%=this.customsClearance.CustomsCorpId%>");

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.customsClearance.CurrencyId%>");

            //报关单价
            $("#nbCustomsPrice").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 4, Digits: 9 });
            $("#nbCustomsPrice").jqxNumberInput("val", "<%=this.customsClearance.CustomsPrice%>");

            //关税税率
            $("#nbTariffRate").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 4, Digits: 3, symbolPosition: "right", symbol: "%" });
            $("#nbTariffRate").jqxNumberInput("val", "<%=this.customsClearance.TariffRate * 100%>");

            //增值税率
            $("#nbAddedValueRate").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 4, Digits: 3, symbolPosition: "right", symbol: "%" });
            $("#nbAddedValueRate").jqxNumberInput("val", "<%=this.customsClearance.AddedValueRate * 100%>");

            //检验检疫费
            $("#nbOtherFees").jqxNumberInput({ width: 180, height: 25, spinButtons: true });
            $("#nbOtherFees").jqxNumberInput("val", "<%=this.customsClearance.OtherFees%>");

            //备注
            $("#txbMemo").jqxInput({ height: 25, width: 400 });
            $("#txbMemo").jqxInput("val", "<%=this.customsClearance.Memo%>");


            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlCustomsCorpId", message: "请选择实际报关公司", action: "keyup,blur", rule: function (input, commit) {
                                return $("#ddlCustomsCorpId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlCurrencyId", message: "请选择币种", action: "keyup,blur", rule: function (input, commit) {
                                return $("#ddlCurrencyId").jqxDropDownList("val") > 0;
                            }
                        },
                        {
                            input: "#nbCustomsPrice", message: "报关单价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbCustomsPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        //{
                        //    input: "#nbTariffRate", message: "关税税率必须在0-1之间", action: "keyup,blur", rule: function (input, commit) {
                        //        return $("#nbTariffRate").jqxNumberInput("val") >= 0 && $("#nbTariffRate").jqxNumberInput("val") < 1;
                        //    }
                        //},
                        //{
                        //    input: "#nbAddedValueRate", message: "增值税率必须在0-1之间", action: "keyup,blur", rule: function (input, commit) {
                        //        return $("#nbAddedValueRate").jqxNumberInput("val") > 0 && $("#nbAddedValueRate").jqxNumberInput("val") < 1;
                        //    }
                        //},
                        {
                            input: "#nbOtherFees", message: "检验检疫费必须不小于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbOtherFees").jqxNumberInput("val") >= 0;
                            }
                        }
                    ]
            });


            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确定提交？")) { return; }

                if (stockIdsUp.length == 0) { alert("必须选择报关库存！"); return; }

                var sids = "";
                for (i = 0; i < stockIdsUp.length; i++) {
                    if (i != 0) { sids += ","; }
                    sids += stockIdsUp[i];
                }

                var customsClearance = {
                    CustomsId: "<%=this.customsClearance.CustomsId%>",
                    CustomsApplyId: "<%=this.customsClearance.CustomsApplyId%>",
                    //Customser
                    CustomsCorpId: $("#ddlCustomsCorpId").val(),
                    //CustomsDate
                    //CustomsName
                    //GrossWeight
                    //NetWeight
                    CurrencyId: $("#ddlCurrencyId").val(),
                    CustomsPrice: $("#nbCustomsPrice").val(),
                    TariffRate: $("#nbTariffRate").val(),
                    AddedValueRate: $("#nbAddedValueRate").val(),
                    OtherFees: $("#nbOtherFees").val(),
                    Memo: $("#txbMemo").val()
                }

                var rows = $("#jqxCustomGrid").jqxGrid("getrows");

                //附件信息
                var fileIds = new Array();
                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/CustomUpdateHandler.ashx", {
                    custom: JSON.stringify(customsClearance),
                    rows: JSON.stringify(rows)
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            AjaxFileUpload(fileIds, obj.ReturnValue, AttachTypeEnum.CustomAttach);
                        }
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceList.aspx";
                        }
                    }
                );
            });
        });

        function bntRemoveOnClick(row) {
            if (!confirm("确认删除？")) { return; }
            var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
            if (item.AttachId == "undefined" || item.AttachId <= 0) return;
            $.post("../Files/Handler/AttachUpdateStatusHandler.ashx", { aid: item.AttachId, s: statusEnum.已作废 },
                    function (result) {
                        attachSource.url = "../Files/Handler/GetAttachListHandler.ashx?cid=" + "<%=this.customsClearance.CustomsId%>" + "&s=" + statusEnum.已生效 + "&t=" + AttachTypeEnum.CustomAttach;
                        $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");
                    }
                );
        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxCustomExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidid" runat="server" />
            <div id="jqxCustomGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可报关库存信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidsidsDown" runat="server" />
            <input type="hidden" id="hidsidsUp" runat="server" />
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关修改
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">实际报关公司：</span>
                    <div id="ddlCustomsCorpId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">币种：</span>
                    <div id="ddlCurrencyId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">报关单价：</span>
                    <div id="nbCustomsPrice"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">关税税率：</span>
                    <div id="nbTariffRate"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">增值税率：</span>
                    <div id="nbAddedValueRate"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">检验检疫费：</span>
                    <div id="nbOtherFees"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">备注：</span>
                    <input type="text" id="txbMemo" />
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="CustomAttach" />

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="CustomsClearanceList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
