<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomsClearanceApplyDetail.aspx.cs" Inherits="NFMTSite.WareHouse.CustomsClearanceApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报关申请明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.apply.DataBaseName%>" + "&t=" + "<%=this.apply.TableName%>" + "&id=" + "<%=this.customsClearanceApply.ApplyId%>";

        $(document).ready(function () {
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var stockIds = new Array();
            var sids = $("#hidsids").val();
            var splitItem = sids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIds.push(splitItem[i]);
                }
            }
            var assetId = 0;
            var corpId = 0;
            var muId = 0;

            /////////////////////////报关申请信息/////////////////////////

            var sids = "";
            for (i = 0; i < stockIds.length; i++) {
                if (i != 0) { sids += ","; }
                sids += stockIds[i];
            }

            var url = "Handler/CustomApplySelectStockListHandler.ashx?sids=" + sids;
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
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
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxApplyGrid").jqxGrid("updatebounddata", "sort");
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
                url: url
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxApplyGrid").jqxGrid(
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
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "当前毛重", datafield: "CurGrossAmountName" },
                  { text: "当前净重", datafield: "CurNetAmountName" }
                ]
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //备注
            $("#txbMemo").jqxInput({ height: 23, width: 400, disabled: true });
            $("#txbMemo").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //报关单价
            $("#nbCustomsPrice").jqxNumberInput({ width: 180, height: 25, spinButtons: true, disabled: true });
            $("#nbCustomsPrice").jqxNumberInput("val", "<%=this.customsClearanceApply.CustomsPrice%>");

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.customsClearanceApply.CurrencyId%>");

            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);

            //关内公司
            $("#ddlInCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlInCorpId").jqxComboBox("val", "<%=this.customsClearanceApply.InCorpId%>");

            //报关公司
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCustomsCorpId").jqxComboBox("val", "<%=this.customsClearanceApply.CustomsCorpId%>");


            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnConfirm").jqxInput();
            $("#btnConfirmCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 34,
                    model: $("#hidmodel").val()
                };
                ShowModalDialog(paras, e);
            });

            var id = "<%=this.customsClearanceApply.CustomsApplyId%>";

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CustomApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/CustomApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认执行完成操作？")) { return; }
                var operateId = operateEnum.确认完成;
                $.post("Handler/CustomApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnConfirmCancel").on("click", function () {
                if (!confirm("撤销后已关闭的明细申请将会打开，执行完成撤销操作？")) { return; }
                var operateId = operateEnum.确认完成撤销;
                $.post("Handler/CustomApplyStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceApplyList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关申请信息<input type="hidden" id="hidsids" runat="server" />
            <input type="hidden" id="hidmodel" runat="server" />
        </div>
        <div style="height: 500px;">
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关申请明细
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请部门：</strong>
                    <div id="ddlApplyDeptId" style="float: left;"></div>
                </li>
                <li>
                    <strong>报关单价：</strong>
                    <div id="nbCustomsPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>关内公司：</strong>
                    <div id="ddlInCorpId" style="float: left;"></div>
                </li>
                <li>
                    <strong>报关公司：</strong>
                    <div id="ddlCustomsCorpId" style="float: left;"></div>
                </li>
                <li>
                    <strong>备注：</strong>
                    <span>
                        <input type="text" id="txbMemo" /></span>
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="CustomApplyAttach" />

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
