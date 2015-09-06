<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomsClearanceDetail.aspx.cs" Inherits="NFMTSite.WareHouse.CustomsClearanceDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报关明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.customsClearance.DataBaseName%>" + "&t=" + "<%=this.customsClearance.TableName%>" + "&id=" + "<%=this.customsClearance.CustomsId%>";

        $(document).ready(function () {
            $("#jqxCustomExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput(); 
            $("#btnClose").jqxInput();

            var stockIdsUp = new Array();

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
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "入库时间", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "所属公司", datafield: "CorpName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "当前毛重", datafield: "CurGrossAmountName" },
                  { text: "当前净重", datafield: "CurNetAmountName" },
                  { text: "报关状态", datafield: "CustomsTypeName" },
                  { text: "交货地", datafield: "DeliverPlace" },
                  { text: "卡号", datafield: "CardNo" }
                ]
            });

            //实际报关公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCustomsCorpId").jqxComboBox("val", "<%=this.customsClearance.CustomsCorpId%>");

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.customsClearance.CurrencyId%>");

            //报关单价
            $("#nbCustomsPrice").jqxNumberInput({ width: 180, height: 25, spinButtons: true, disabled: true, decimalDigits: 4, Digits: 9 });
            $("#nbCustomsPrice").jqxNumberInput("val", "<%=this.customsClearance.CustomsPrice%>");

            //关税税率
            $("#nbTariffRate").jqxNumberInput({ width: 180, height: 25, spinButtons: true, disabled: true, decimalDigits: 4, Digits: 3, symbolPosition: "right", symbol: "%" });
            $("#nbTariffRate").jqxNumberInput("val", "<%=this.customsClearance.TariffRate * 100%>");

            //增值税率
            $("#nbAddedValueRate").jqxNumberInput({ width: 180, height: 25, spinButtons: true, disabled: true, decimalDigits: 4, Digits: 3, symbolPosition: "right", symbol: "%" });
            $("#nbAddedValueRate").jqxNumberInput("val", "<%=this.customsClearance.AddedValueRate * 100%>");

            //检验检疫费
            $("#nbOtherFees").jqxNumberInput({ width: 180, height: 25, spinButtons: true, disabled: true });
            $("#nbOtherFees").jqxNumberInput("val", "<%=this.customsClearance.OtherFees%>");

            //备注
            $("#txbMemo").jqxInput({ height: 25, width: 400, disabled: true });
            $("#txbMemo").jqxInput("val", "<%=this.customsClearance.Memo%>");

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 35,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/CustomStatusHandler.ashx", { id: "<%=this.customsClearance.CustomsId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CustomStatusHandler.ashx", { id: "<%=this.customsClearance.CustomsId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/CustomStatusHandler.ashx", { id: "<%=this.customsClearance.CustomsId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("完成撤销后，所有已完成的明细将会更新至已生效，确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/CustomStatusHandler.ashx", { id: "<%=this.customsClearance.CustomsId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceList.aspx";
                        }
                    }
                );
            });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/CustomStatusHandler.ashx", { id: "<%=this.customsClearance.CustomsId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceList.aspx";
                        }
                    }
                );
            });

        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxCustomExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidModel" runat="server" />
            <input type="hidden" id="hidsidsUp" runat="server" />
            <div id="jqxCustomGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关明细
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

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="CustomAttach" />

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
