<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SplitDocDetail.aspx.cs" Inherits="NFMTSite.WareHouse.SplitDocDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>拆单明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.splitDoc.DataBaseName%>" + "&t=" + "<%=this.splitDoc.TableName%>" + "&id=" + "<%=this.splitDoc.SplitDocId%>";

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
            $("#dtStockDate").jqxDateTimeInput("val", new Date("<%=this.stock.StockDate%>"));

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

            //所属部门
            var ddlDeptIdurl = "../User/Handler/DeptDDLHandler.ashx?corpId=" + $("#ddlCorpId").val();
            var ddlDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlDeptIdurl, async: false };
            var ddlDeptIddataAdapter = new $.jqx.dataAdapter(ddlDeptIdsource);
            $("#ddlDeptId").jqxComboBox({ selectedIndex: 0, source: ddlDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 120, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlDeptId").jqxComboBox("val", "<%=this.stock.DeptId%>");

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
            var outCorpSource =
            {
                datatype: "json",
                datafields: [
                    { name: "Name", type: "string" },
                    { name: "EmpId", type: "int" }
                ],
                type: "GET",
                url: ddlPaperHolderrurl
            };
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
                //selectionmode: "singlecell",
                columns: [
                  { text: "新业务单号", datafield: "NewRefNo", width: 100 },
                  {
                      text: "新单毛重", datafield: "GrossAmount", width: 130, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  {
                      text: "新单净重", datafield: "NetAmount", width: 130, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "计量单位", datafield: "MUName", width: 80, editable: false },
                  { text: "品种", datafield: "AssetName", width: 100, editable: false },
                  {
                      text: "捆数", datafield: "Bundles", width: 130, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "权证编号", datafield: "PaperNo", width: 100 },
                  {
                      text: "单据保管人", datafield: "PaperHolder", displayfield: "Name", width: 100, columntype: "combobox",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: outCorpDataAdapter, displayMember: "Name", valueMember: "EmpId", autoDropDownHeight: true });
                      }
                  },
                  { text: "卡号", datafield: "CardNo", width: 100 },
                  { text: "备注", datafield: "Memo", width: 200 }
                ]
            });


            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput(); 
            $("#btnClose").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 33,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/SplitDocStatusHandler.ashx", { id: "<%=this.splitDoc.SplitDocId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "SplitDocList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/SplitDocStatusHandler.ashx", { id: "<%=this.splitDoc.SplitDocId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "SplitDocList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/SplitDocStatusHandler.ashx", { id: "<%=this.splitDoc.SplitDocId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "SplitDocList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("完成撤销后，所有已完成的明细将会更新至已生效，确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/SplitDocStatusHandler.ashx", { id: "<%=this.splitDoc.SplitDocId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "SplitDocList.aspx";
                        }
                    }
                );
            }); 

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/SplitDocStatusHandler.ashx", { id: "<%=this.splitDoc.SplitDocId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "SplitDocList.aspx";
                        }
                    }
                );
            });

        });
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
                    <strong>所属部门：</strong>
                    <div style="float: left" id="ddlDeptId"></div>
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
            拆单详情<input type="hidden" id="hidModel" runat="server" />
        </div>
        <div style="height: 500px;">
            <div id="jqxSplitGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="SplitDocAttach" />

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnClose" value="关闭" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
