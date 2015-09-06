<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopLossContractDetail.aspx.cs" Inherits="NFMTSite.DoPrice.StopLossContractDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>止损明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.stopLoss.DataBaseName%>" + "&t=" + "<%=this.stopLoss.TableName%>" + "&id=" + "<%=this.stopLoss.StopLossId%>";

        $(document).ready(function () {

            $("#jqxStopLossExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            /////////////////////////止损申请信息/////////////////////////

            //止损申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlStopLossApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", disabled: true
            });
            $("#ddlStopLossApplyCorp").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            //止损申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlStopLossApplyDept").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossApplyDept").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //申请备注
            $("#txbStopLossApplyDesc").jqxInput({ height: 25, disabled: true });
            $("#txbStopLossApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //止损品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlStopLossAsset").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlStopLossAsset").jqxDropDownList("val", "<%=this.stopLossApply.AssertId%>");

            //止损价格
            $("#nbStopLossPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 140, spinButtons: true, disabled: true });
            $("#nbStopLossPrice").jqxNumberInput("val", "<%=this.stopLossApply.StopLossPrice%>");

            //价格币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlStopLossCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossCurrencyId").jqxDropDownList("val", "<%=this.stopLossApply.CurrencyId%>");

            //止损重量
            $("#nbStopLossWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });
            $("#nbStopLossWeight").jqxNumberInput("val", "<%=this.stopLossApply.StopLossWeight%>");

            //计量单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlStopLossMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlStopLossMUId").jqxDropDownList("val", "<%=this.stopLossApply.MUId%>");


            /////////////////////////止损修改/////////////////////////

            //止损重量
            $("#txbStopLossWeight").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 160, spinButtons: true, symbol: "<%=this.measureUnit.MUName%>", symbolPosition: "right", disabled: true });
            $("#txbStopLossWeight").jqxNumberInput("val", "<%=this.stopLoss.StopLossWeight%>");

            //止损单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#ddlMUId").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlMUId").jqxDropDownList("val", "<%=this.measureUnit.MUId%>");

            //止损期货市场
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxDropDownList({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 130, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });
            $("#ddlExchangeId").jqxDropDownList("val", "<%=this.stopLoss.ExchangeId%>");

            //止损期货合约
            var FuturesCodeSource = { datatype: "json", url: "../BasicData/Handler/FuturesCodeDDLHandler.ashx?exId=" + $("#ddlExchangeId").val(), async: false };
            var FuturesCodeDataAdapter = new $.jqx.dataAdapter(FuturesCodeSource);
            $("#ddlFuturesCodeId").jqxComboBox({ source: FuturesCodeDataAdapter, displayMember: "TradeCode", valueMember: "FuturesCodeId", width: 100, height: 25, disabled: true });
            $("#ddlFuturesCodeId").jqxComboBox("val", "<%=this.stopLoss.FuturesCodeId%>");

            //止损均价
            $("#txbAvgPrice").jqxNumberInput({ height: 25, min: 0, decimalDigits: 2, width: 160, spinButtons: true, symbol: "<%=this.currency.CurrencyName%>", symbolPosition: "right", disabled: true });
            $("#txbAvgPrice").jqxNumberInput("val", "<%=this.stopLoss.AvgPrice%>");

            //止损币种 
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrencyId").jqxDropDownList("val", "<%=this.currency.CurrencyId%>");

            //止损品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssertId").jqxDropDownList({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, disabled: true });
            $("#ddlAssertId").jqxDropDownList("val", "<%=this.stopLossApply.AssertId%>");

            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 32,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/StopLossStatusHandler.ashx", { id: "<%=this.stopLoss.StopLossId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StopLossList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/StopLossStatusHandler.ashx", { id: "<%=this.stopLoss.StopLossId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StopLossList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/StopLossStatusHandler.ashx", { id: "<%=this.stopLoss.StopLossId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StopLossList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/StopLossStatusHandler.ashx", { id: "<%=this.stopLoss.StopLossId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StopLossList.aspx";
                        }
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStopLossExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            止损申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>止损申请公司：</strong>
                    <div id="ddlStopLossApplyCorp" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损申请部门：</strong>
                    <div id="ddlStopLossApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损申请备注：</strong>
                    <input type="text" id="txbStopLossApplyDesc" style="float: left;" />
                </li>
                <li>
                    <strong>止损品种：</strong>
                    <div id="ddlStopLossAsset" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损价格：</strong>
                    <div id="nbStopLossPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损币种：</strong>
                    <div id="ddlStopLossCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损重量：</strong>
                    <div id="nbStopLossWeight" style="float: left;"></div>
                </li>
                <li>
                    <strong>重量单位：</strong>
                    <div id="ddlStopLossMUId" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>


    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            止损信息<input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>止损重量：</strong>
                    <div id="txbStopLossWeight" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损重量单位：</strong>
                    <div id="ddlMUId" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货市场：</strong>
                    <div id="ddlExchangeId" style="float: left;"></div>
                </li>
                <li>
                    <strong>期货合约：</strong>
                    <div id="ddlFuturesCodeId" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损均价：</strong>
                    <div id="txbAvgPrice" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损币种：</strong>
                    <div id="ddlCurrencyId" style="float: left;"></div>
                </li>
                <li>
                    <strong>止损品种：</strong>
                    <div id="ddlAssertId" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnComplete" value="执行完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnCompleteCancel" value="执行完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
