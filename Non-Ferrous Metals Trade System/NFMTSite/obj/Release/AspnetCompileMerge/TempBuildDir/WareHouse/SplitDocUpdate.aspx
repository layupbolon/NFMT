<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SplitDocUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.SplitDocUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>拆单修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxStockInfoExpander").jqxExpander({ width: "98%" });
            $("#jqxSplitExpander").jqxExpander({ width: "98%" });

            var muName = "<%=NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a=>a.MUId==this.stock.UintId).MUName%>";

            //////////////////////////库存信息//////////////////////////

            //业务单号
            $("#txbRefNo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbRefNo").jqxInput("val", "<%=this.stockName.RefNo%>");

            //库存编号
            $("#txbStockNo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbStockNo").jqxInput("val", "<%=this.stock.StockNo%>");

            //入库时间
            $("#dtStockDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#dtStockDate").jqxDateTimeInput("val", "<%=this.stock.StockDate%>");

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxDropDownList({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 120, height: 25, selectedIndex: 0, disabled: true });
            $("#ddlAssetId").jqxDropDownList("val", "<%=this.stock.AssetId%>");

            //捆数
            $("#nbBundles").jqxNumberInput({ width: 120, height: 25, spinButtons: true, decimalDigits: 0, Digits: 3, disabled: true });
            $("#nbBundles").jqxNumberInput("val", "<%=this.stock.Bundles%>");

            //入库毛量
            $("#nbGrossAmount").jqxNumberInput({ width: 120, height: 25, spinButtons: true, disabled: true });
            $("#nbGrossAmount").jqxNumberInput("val", "<%=this.stock.GrossAmount%>");

            //入库净重
            $("#nbNetAmount").jqxNumberInput({ width: 120, height: 25, spinButtons: true, disabled: true });
            $("#nbNetAmount").jqxNumberInput("val", "<%=this.stock.NetAmount%>");

            //入库回执磅差
            $("#nbReceiptInGap").jqxNumberInput({ width: 120, height: 25, spinButtons: true, disabled: true });
            $("#nbReceiptInGap").jqxNumberInput("val", "<%=this.stock.ReceiptInGap%>");

            //出库回执磅差
            $("#nbReceiptOutGap").jqxNumberInput({ width: 120, height: 25, spinButtons: true, disabled: true });
            $("#nbReceiptOutGap").jqxNumberInput("val", "<%=this.stock.ReceiptOutGap%>");

            //当前毛重
            $("#nbCurGrossAmount").jqxNumberInput({ width: 120, height: 25, spinButtons: true, disabled: true });
            $("#nbCurGrossAmount").jqxNumberInput("val", "<%=this.stock.CurGrossAmount%>");

            //当前净重
            $("#nbCurNetAmount").jqxNumberInput({ width: 120, height: 25, spinButtons: true, disabled: true });
            $("#nbCurNetAmount").jqxNumberInput("val", "<%=this.stock.CurNetAmount%>");

            //计量单位
            var ddlMUIdurl = "../BasicData/Handler/MUDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlUintId").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "MUName", valueMember: "MUId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlUintId").jqxComboBox("val", "<%=this.stock.UintId%>");

            //交货地
            var ddlDeliverPlaceIdurl = "../BasicData/Handler/DeliverPlaceDDLHandler.ashx";
            var ddlDeliverPlaceIdsource = { datatype: "json", datafields: [{ name: "DPId" }, { name: "DPName" }], url: ddlDeliverPlaceIdurl, async: false };
            var ddlDeliverPlaceIddataAdapter = new $.jqx.dataAdapter(ddlDeliverPlaceIdsource);
            $("#ddlDeliverPlaceId").jqxComboBox({ selectedIndex: 0, source: ddlDeliverPlaceIddataAdapter, displayMember: "DPName", valueMember: "DPId", width: 120, height: 25, disabled: true });
            $("#ddlDeliverPlaceId").jqxComboBox("val", "<%=this.stock.DeliverPlaceId%>");

            //生产商
            var ddlProducterurl = "../BasicData/Handler/ProducerDDLHandler.ashx";
            var ddlProductersource = { datatype: "json", datafields: [{ name: "ProducerId" }, { name: "ProducerName" }], url: ddlProducterurl, async: false };
            var ddlProducterdataAdapter = new $.jqx.dataAdapter(ddlProductersource);
            $("#ddlProducerId").jqxComboBox({ selectedIndex: 0, source: ddlProducterdataAdapter, displayMember: "ProducerName", valueMember: "ProducerId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlProducerId").jqxComboBox("val", "<%=this.stock.ProducerId%>");

            //品牌
            var ddlBrandIdurl = "../BasicData/Handler/BrandDDLHandler.ashx?pid=" + $("#ddlProducter").val();
            var ddlBrandIdsource = { datatype: "json", datafields: [{ name: "BrandId" }, { name: "BrandName" }], url: ddlBrandIdurl, async: false };
            var ddlBrandIddataAdapter = new $.jqx.dataAdapter(ddlBrandIdsource);
            $("#ddlBrandId").jqxComboBox({ selectedIndex: 0, source: ddlBrandIddataAdapter, displayMember: "BrandName", valueMember: "BrandId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlBrandId").jqxComboBox("val", "<%=this.stock.BrandId%>");

            //报关状态
            var styleId = $("#hidBDStyleId").val();
            var ddlStockTypesource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var ddlStockTypedataAdapter = new $.jqx.dataAdapter(ddlStockTypesource);
            $("#ddlCustomsType").jqxDropDownList({ source: ddlStockTypedataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCustomsType").jqxDropDownList("val", "<%=this.stock.CustomsType%>");

            //入账公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCorpId").jqxComboBox({ selectedIndex: 0, source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCorpId").jqxComboBox("val", "<%=this.stock.CorpId%>");           

            //权证编号
            $("#txbPaperNo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbPaperNo").jqxInput("val", "<%=this.stock.PaperNo%>");

            //单据保管人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlPaperHolder").jqxComboBox({ selectedIndex: 0, source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlPaperHolder").jqxInput("val", "<%=this.stock.PaperHolder%>");

            //卡号
            $("#txbCardNo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbCardNo").jqxInput("val", "<%=this.stock.CardNo%>");

            //备注
            $("#txbMemo").jqxInput({ height: 25, width: 120, disabled: true });
            $("#txbMemo").jqxInput("val", "<%=this.stock.CardNo%>");


            //////////////////////////拆单详情//////////////////////////
            var outCorpSource ={ datatype: "json",type: "GET",url: ddlPaperHolderrurl };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource, { autoBind: true });


            var url = "Handler/SplitDocDetailListHandler.ashx?splitDocId=" + "<%=this.splitDoc.SplitDocId%>";
            var source =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "NewRefNo", type: "string" },
                   { name: "GrossAmount", type: "number" },
                   { name: "NetAmount", type: "number" },
                   { name: "UnitId", type: "int" },
                   { name: "MUName", type: "string" },
                   { name: "AssetId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "Bundles", type: "int" },
                   { name: "BrandId", type: "int" },
                   { name: "BrandName", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "PaperHolder", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "CardNo", type: "string" },
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

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var rowscount = $("#jqxSplitGrid").jqxGrid("getdatainformation").rowscount;
                if (row >= 0 && row < rowscount) {
                    var id = $("#jqxSplitGrid").jqxGrid("getrowid", row);
                    var commit = $("#jqxSplitGrid").jqxGrid("deleterow", id);
                }
            }

            $("#jqxSplitGrid").jqxGrid(
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
                  { text: "新业务单号", datafield: "NewRefNo", cellclassname: "GridFillNumber" },
                  {
                      text: "新单毛重", datafield: "GrossAmount", cellclassname: "GridFillNumber", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  {
                      text: "新单净重", datafield: "NetAmount", cellclassname: "GridFillNumber", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "单位", datafield: "MUName", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  {
                      text: "捆数", datafield: "Bundles", cellclassname: "GridFillNumber", aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "权证编号", datafield: "PaperNo", cellclassname: "GridFillNumber" },
                  {
                      text: "保管人", datafield: "PaperHolder", displayfield: "Name", cellclassname: "GridFillNumber", columntype: "combobox",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: outCorpDataAdapter, displayMember: "Name", valueMember: "EmpId", autoDropDownHeight: true });
                      }
                  },
                  { text: "卡号", datafield: "CardNo", cellclassname: "GridFillNumber" },
                  { text: "备注", datafield: "Memo", cellclassname: "GridFillNumber" },
                  {
                      text: "操作", datafield: "Edit", width: 45, columntype: "button", cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var rowscount = $("#jqxSplitGrid").jqxGrid("getdatainformation").rowscount;
                          if (row >= 0 && row < rowscount) {
                              var id = $("#jqxSplitGrid").jqxGrid("getrowid", row);
                              var commit = $("#jqxSplitGrid").jqxGrid("deleterow", id);
                          }
                      }
                  }
                ]
            });

            $("#btnSplit").jqxButton({ height: 25, width: 100 });

            $("#btnSplit").click(function () {
                $("#popupWindow").jqxWindow("show");

                $("#txbNewRefNo").jqxInput("val", "");
                $("#nbNewGrossAmount").jqxNumberInput("val", 0);
                $("#nbNewNetAmount").jqxNumberInput("val", 0);
                $("#nbNewBundles").jqxNumberInput("val", 0);
                $("#txbNewPaperNo").jqxInput("val", "");
                $("#ddlNewPaperHolder").jqxComboBox("val", "");
                $("#txbNewCardNo").jqxInput("val", "");
                $("#txbNewMemo").jqxInput("val", "");
            });

            $("#btnSave").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            ////////////////////////  弹窗  ////////////////////////
            //新业务单号
            $("#txbNewRefNo").jqxInput({ height: 25, width: 120 });

            //新毛重
            $("#nbNewGrossAmount").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, digits: 6, spinButtons: true, symbolPosition: "right", symbol: muName });

            //新净重
            $("#nbNewNetAmount").jqxNumberInput({ width: 120, height: 25, decimalDigits: 4, digits: 6, spinButtons: true, symbolPosition: "right", symbol: muName });

            //捆数
            $("#nbNewBundles").jqxNumberInput({ width: 120, height: 25, spinButtons: true, decimalDigits: 0, Digits: 6 });

            //权证编号
            $("#txbNewPaperNo").jqxInput({ height: 25, width: 120 });

            //单据保管人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlNewPaperHolder").jqxComboBox({ selectedIndex: 0, source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 120, height: 25 });

            //卡号
            $("#txbNewCardNo").jqxInput({ height: 25, width: 120 });

            //备注
            $("#txbNewMemo").jqxInput({ height: 25, width: 120 });

            $("#popupWindow").jqxWindow({ width: 350, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01 });
            $("#Cancel").jqxButton({ height: 25, width: 100 });
            $("#Save").jqxButton({ height: 25, width: 100 });

            //验证器
            $("#popupWindow").jqxValidator({
                rules:
                    [
                        { input: "#txbNewRefNo", message: "请输入新业务单号", action: "keyup,blur", rule: "required" },
                        {
                            input: "#nbNewGrossAmount", message: "毛重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbNewGrossAmount').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbNewNetAmount", message: "净重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbNewNetAmount').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbNewBundles", message: "捆数必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbNewBundles').jqxNumberInput("val") > 0;
                            }
                        },
                        { input: "#txbNewPaperNo", message: "请输入权证编号", action: "keyup,blur", rule: "required" },
                        {
                            input: "#ddlNewPaperHolder", message: "请选择单据保管人", action: "keyup,blur", rule: function (input, commit) {
                                return $('#ddlNewPaperHolder').jqxComboBox("val") > 0;
                            }
                        },
                        { input: "#txbNewCardNo", message: "请输入卡号", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#Save").click(function () {
                var isCanSubmit = $("#popupWindow").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var item = $("#ddlNewPaperHolder").jqxComboBox("getItemByValue", $("#ddlNewPaperHolder").val());

                var datarow = {
                    NewRefNo: $("#txbNewRefNo").val(),
                    GrossAmount: $("#nbNewGrossAmount").val(),
                    NetAmount: $("#nbNewNetAmount").val(),
                    UnitId: "<%=this.stock.UintId%>",
                    MUName: muName,
                    AssetId: "<%=this.stock.AssetId%>",
                    AssetName: "<%=NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(a=>a.AssetId==this.stock.AssetId).AssetName%>",
                    BrandId: "<%=this.stock.BrandId%>",
                    Bundles: $("#nbNewBundles").val(),
                    BrandName: "<%=NFMT.Data.BasicDataProvider.Brands.SingleOrDefault(a=>a.BrandId==this.stock.BrandId).BrandName%>",
                    PaperNo: $("#txbNewPaperNo").val(),
                    PaperHolder: $("#ddlNewPaperHolder").val(),
                    Name: item.label,
                    CardNo: $("#txbNewCardNo").val(),
                    Memo: $("#txbNewMemo").val()
                };
                var commit = $("#jqxSplitGrid").jqxGrid("addrow", null, datarow);
                $("#popupWindow").jqxWindow("hide");
            });


            $("#btnSave").on("click", function () {
                if (!confirm("确定保存拆单结果？")) { return; }

                var rows = $("#jqxSplitGrid").jqxGrid("getrows");

                var splitGrossAmount = 0;
                var splitNetAmount = 0;
                var splitBundles = 0;

                for (var i = 0; i < rows.length; i++) {
                    alert(rows[i].GrossAmount);
                    splitGrossAmount += parseFloat(rows[i].GrossAmount);
                    splitNetAmount += parseFloat(rows[i].NetAmount);
                    splitBundles += parseInt(rows[i].Bundles);
                }

                if (splitGrossAmount <= 0 || splitNetAmount <= 0 || splitBundles <= 0) {
                    alert("无效的拆单操作");
                    return;
                }
                if (splitGrossAmount > "<%=this.stock.GrossAmount%>") {
                    alert("拆单的【总毛重】大于原单据【毛重】！");
                    return;
                }
                if (splitNetAmount > "<%=this.stock.NetAmount%>") {
                    alert("拆单的【总净重】大于原单据【净重】！");
                    return;
                }
                if (splitBundles > "<%=this.stock.Bundles%>") {
                    alert("拆单的【总捆数】大于原单据【捆数】！");
                    return;
                }

                if (splitGrossAmount < "<%=this.stock.GrossAmount%>" || splitNetAmount < "<%=this.stock.NetAmount%>" || splitBundles < "<%=this.stock.Bundles%>") {
                    var leftGrossAmount = "<%=this.stock.GrossAmount%>" - splitGrossAmount;
                    var leftNetAmount = "<%=this.stock.NetAmount%>" - splitNetAmount;
                    var leftBundles = "<%=this.stock.Bundles%>" - splitBundles;
                    if (!confirm("是否将剩余 【毛重】：" + leftGrossAmount + muName + "，【净重】：" + leftNetAmount + muName + "，【捆数】：" + leftBundles + " 作为一张单据拆分？")) {
                        return;
                    }

                    $("#popupWindow").jqxWindow("show");

                    $("#txbNewRefNo").jqxInput("val", "");
                    $("#nbNewGrossAmount").jqxNumberInput("val", leftGrossAmount);
                    $("#nbNewNetAmount").jqxNumberInput("val", leftNetAmount);
                    $("#nbNewBundles").jqxNumberInput("val", leftBundles);
                    $("#txbNewPaperNo").jqxInput("val", "");
                    $("#ddlNewPaperHolder").jqxComboBox("val", "");
                    $("#txbNewCardNo").jqxInput("val", "");
                    $("#txbNewMemo").jqxInput("val", "");

                    return;
                }

                //附件信息
                var fileIds = new Array();
                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/SplitDocUpdateHandler.ashx", {
                    splitDocId: "<%=this.splitDoc.SplitDocId%>",
                    stockId: "<%=this.stock.StockId%>",
                    splitDetail: JSON.stringify(rows)
                },
                     function (result) {
                         var obj = JSON.parse(result);
                         if (obj.ResultStatus.toString() == "0") {
                             AjaxFileUpload(fileIds, "<%=this.splitDoc.SplitDocId%>", AttachTypeEnum.SplitDocAttach);
                         }
                         alert(obj.Message);
                         if (obj.ResultStatus.toString() == "0") {
                             document.location.href = "SplitDocList.aspx";
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
                             attachSource.url = "../Files/Handler/GetAttachListHandler.ashx?cid=" + "<%=this.splitDoc.SplitDocId%>" + "&s=" + statusEnum.已生效 + "&t=" + AttachTypeEnum.SplitDocAttach;
                        $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");
                    }
                );
                }
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStockInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>业务单号：</strong>
                    <input type="text" id="txbRefNo" style="float: left;" />
                </li>
                <li>
                    <strong>库存编号：</strong>
                    <input type="text" id="txbStockNo" style="float: left;" />
                </li>
                <li>
                    <strong>入库时间：</strong>
                    <div id="dtStockDate" style="float: left;" />
                </li>
                <li>
                    <strong>品种：</strong>
                    <div id="ddlAssetId" style="float: left;" />
                </li>
                <li>
                    <strong>捆数：</strong>
                    <div id="nbBundles" style="float: left;" />
                </li>
                <li>
                    <strong>入库毛量：</strong>
                    <div id="nbGrossAmount" style="float: left;" />
                </li>
                <li>
                    <strong>入库净量：</strong>
                    <div style="float: left" id="nbNetAmount"></div>
                </li>
                <li>
                    <strong>入库回执磅差：</strong>
                    <div id="nbReceiptInGap" style="float: left;" />
                </li>
                <li>
                    <strong>出库回执磅差：</strong>
                    <div style="float: left" id="nbReceiptOutGap"></div>
                </li>
                <li>
                    <strong>当前毛重：</strong>
                    <div style="float: left" id="nbCurGrossAmount"></div>
                </li>
                <li>
                    <strong>当前净重：</strong>
                    <div style="float: left" id="nbCurNetAmount"></div>
                </li>
                <li>
                    <strong>计量单位：</strong>
                    <div style="float: left" id="ddlUintId"></div>
                </li>
                <li>
                    <strong>交货地：</strong>
                    <div style="float: left" id="ddlDeliverPlaceId"></div>
                </li>
                <li>
                    <strong>生产商：</strong>
                    <div style="float: left" id="ddlProducerId"></div>
                </li>
                <li>
                    <strong>品牌：</strong>
                    <div style="float: left" id="ddlBrandId"></div>
                </li>
                <li>
                    <input type="hidden" id="hidBDStyleId" runat="server" />
                    <strong>报关状态：</strong>
                    <div style="float: left" id="ddlCustomsType"></div>
                </li>
                <li>
                    <strong>所属公司：</strong>
                    <div style="float: left" id="ddlCorpId"></div>
                </li>                
                <li>
                    <strong>权证编号：</strong>
                    <input type="text" style="float: left" id="txbPaperNo" />
                </li>
                <li>
                    <strong>单据保管人：</strong>
                    <div style="float: left" id="ddlPaperHolder" />
                </li>
                <li>
                    <strong>卡号：</strong>
                    <input type="text" style="float: left" id="txbCardNo" />
                </li>
                <li>
                    <strong>备注：</strong>
                    <input type="text" style="float: left" id="txbMemo" />
                </li>
                <li>
                    <strong>库存状态：</strong>
                    <span id="spStockStatus" style="float: left" runat="server" />
                </li>
                <%--<li>
                                <strong>库存类型：</strong>
                                <span id="spStockType" style="float: left" runat="server" />
                            </li>--%>
            </ul>
        </div>
    </div>

    <div id="jqxSplitExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            拆单详情
        </div>
        <div style="height: 500px;">
            <input type="button" id="btnSplit" value="拆单" runat="server" style="width: 120px; height: 25px;margin:5px 0px 0px 5px" />
            <div id="jqxSplitGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="SplitDocAttach" />

    <div style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnSave" value="修改" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="SplitDocList.aspx" id="btnCancel" style="margin-left: 10px">取消</a>
    </div>
    <div id="popupWindow">
        <div>拆单</div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span>新业务单号：</span>
                    <input type="text" id="txbNewRefNo" />
                </li>
                <li>
                    <span>新单毛重：</span>
                    <div id="nbNewGrossAmount"></div>
                </li>
                <li>
                    <span>新单净重：</span>
                    <div id="nbNewNetAmount" style="float: left;"></div>
                </li>
                <li>
                    <span>捆数：</span>
                    <div id="nbNewBundles" style="float: left;"></div>
                </li>
                <li>
                    <span>权证编号：</span>
                    <input type="text" id="txbNewPaperNo" />
                </li>
                <li>
                    <span>单据保管人：</span>
                    <div id="ddlNewPaperHolder" style="float: left;"></div>
                </li>
                <li>
                    <span>卡号：</span>
                    <input type="text" id="txbNewCardNo" />
                </li>
                <li>
                    <span>备注：</span>
                    <input type="text" id="txbNewMemo" />
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
