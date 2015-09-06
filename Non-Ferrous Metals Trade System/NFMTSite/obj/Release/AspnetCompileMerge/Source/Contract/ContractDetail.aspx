<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractDetail.aspx.cs" Inherits="NFMTSite.Contract.ContractDetail" %>
<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约查看</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">       

        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curContract.DataBaseName%>" + "&t=" + "<%=this.curContract.TableName%>" + "&id=" + "<%=this.curContract.ContractId%>";

        $(document).ready(function () {         
            
            var masterIds = new Array();

            $("#jqxOutCorpExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxTypeExpander").jqxExpander({ width: "98%" });
            $("#jqxDetailExpander").jqxExpander({ width: "98%" });
            $("#jqxPriceExpander").jqxExpander({ width: "98%" });
            $("#jqxClauseExpander").jqxExpander({ width: "98%" });

            $("#txbCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //对方公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selOutCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", multiSelect: true, searchMode: "containsignorecase", height: 25,width:500, disabled: true
            });

            //我方公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#selInCorp").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", multiSelect: true, searchMode: "containsignorecase", height: 25,width:500, disabled: true
            });

            //执行部门
            var deptUrl = "../User/Handler/DeptDDLHandler.ashx";
            var deptSource = { datatype: "json", url: deptUrl, async: false };
            var deptDataAdapter = new $.jqx.dataAdapter(deptSource);
            $("#selExecDept").jqxComboBox({ source: deptDataAdapter, displayMember: "DeptName", valueMember: "DeptId", checkboxes: true, height: 25 , disabled: true });
            $("#selExecDept").on("close", function (event) {
                var items = $("#selExecDept").jqxComboBox("getCheckedItems");
                for (i = 0; i < items.length; i++) {
                    var item = items[i];
                    $("#selExecDept").jqxComboBox("removeItem", item);
                    $("#selExecDept").jqxComboBox("insertAt", item, i);
                    $("#selExecDept").jqxComboBox("checkIndex", i);
                }
            });

            //外部合约号
            $("#txbOutContractNo").jqxInput({ height: 22, disabled: true });

            //采购销售
            var tradeDirectionStyle = $("#hidTradeDirection").val();
            var directionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + tradeDirectionStyle, async: false };
            var directionDataAdapter = new $.jqx.dataAdapter(directionSource);
            $("#selTradeDirection").jqxDropDownList({ source: directionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //交易品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#selAsset").jqxDropDownList({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, selectedIndex: 0, disabled: true });

            //外贸内贸
            var tradeBorderStyle = $("#hidTradeBorder").val();
            var borderSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + tradeBorderStyle, async: false };
            var borderDataAdapter = new $.jqx.dataAdapter(borderSource);
            $("#selTradeBorder").jqxDropDownList({ source: borderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true});

            //执行日期
            $("#txbFromExecDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#txbToExecDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 , disabled: true});

            //长单零单            
            var contractLimitStyle = $("#hidContractLimit").val();
            var limitSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + contractLimitStyle, async: false };
            var limitDataAdapter = new $.jqx.dataAdapter(limitSource);
            $("#selContractLimit").jqxDropDownList({ source: limitDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //备注
            $("#txbMemo").jqxInput({ width: 300, height: 22, disabled: true });

            //签订数量
            $("#txbSignAmount").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width:140, spinButtons: true, disabled: true });

            //升贴水
            $("#txbPremium").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width: 140, spinButtons: true, disabled: true });

            //单位
            var muSource = { datatype: "json", url: "../BasicData/Handler/MUDDLHandler.ashx", async: false };
            var muDataAdapter = new $.jqx.dataAdapter(muSource);
            $("#selUnit").jqxDropDownList({ source: muDataAdapter, displayMember: "MUName", valueMember: "MUId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true});

            //溢短装
            $("#txbMoreOrLess").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, symbol: "%", symbolPosition: "right", spinButtons: true, width: 100, disabled: true });

            //结算币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#selCurrency").jqxDropDownList({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //延期费/率
            var valueRateTypeModeStyle = $("#hidValueRateType").val();
            var valueRateTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + valueRateTypeModeStyle, async: false };
            var valueRateTypeDataAdapter = new $.jqx.dataAdapter(valueRateTypeSource);
            $("#selDelayType").jqxDropDownList({ source: valueRateTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true});
            $("#txbDelayRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, width:140, spinButtons: true, disabled: true });

            //贴现费/率
            $("#txbDiscountRate").jqxNumberInput({ height: 25, min: 0, decimalDigits: 4, Digits: 3, width:140, spinButtons: true , disabled: true, symbol: "%", symbolPosition: "right" });

            var discountBaseModeStyle = $("#hidDiscountBase").val();
            var discountBaseSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + discountBaseModeStyle, async: false, disabled: true };
            var discountBaseDataAdapter = new $.jqx.dataAdapter(discountBaseSource);
            $("#selDiscountBase").jqxDropDownList({ source: discountBaseDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //定价裸单价
            $("#jqxPricTabs").jqxTabs({ width: "99.8%", position: "top", selectionTracker: "checked", animationType: "fade" });
            $("#txbFixedPrice").jqxNumberInput({ height: 25, min: 0, width:140, decimalDigits: 4, Digits: 8, width:140, spinButtons: true, disabled: true });
            $("#txbFixedPriceMemo").jqxInput({ height: 23 , disabled: true});

            //点价
            //点价方
            var whoDoPriceStyle = $("#hidWhoDoPrice").val();
            var whoDoPriceSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + whoDoPriceStyle, async: false };
            var whoDoPriceDataAdapter = new $.jqx.dataAdapter(whoDoPriceSource);
            $("#selWhoDoPrice").jqxDropDownList({ source: whoDoPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //点价日期范围
            $("#txbDoPriceBeginDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });
            $("#txbDoPriceEndDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120, disabled: true });

            //点价裸价来源
            var exChangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exChangeDataAdapter = new $.jqx.dataAdapter(exChangeSource);
            $("#selPriceFrom").jqxDropDownList({ source: exChangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 150, height: 25, autoDropDownHeight: true, selectedIndex: 0, disabled: true });

            //作价方式
            var summaryPriceStyle = $("#hidSummaryPrice").val();
            var summaryPriceSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + summaryPriceStyle, async: false };
            var summaryPriceDataAdapter = new $.jqx.dataAdapter(summaryPriceSource);
            $("#selPriceStyle1").jqxDropDownList({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true});
            $("#selPriceStyle2").jqxDropDownList({ source: summaryPriceDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true});


            //价格保证金条款
            var marginModeStyle = $("#hidMarginMode").val();
            var marginModeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + marginModeStyle, async: false };
            var marginModeDataAdapter = new $.jqx.dataAdapter(marginModeSource);
            $("#selMarginMode").jqxDropDownList({ source: marginModeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true, selectedIndex: 0 , disabled: true});

            //价格保证金数量
            $("#selMarginAmount").jqxNumberInput({ height: 25, decimalDigits: 4, Digits: 8, width:140, spinButtons: true , disabled: true});

            //价格保证金描述
            $("#txbMarginMemo").jqxInput({ width: 300, height: 22 , disabled: true});  

            <%=contractBLL.GetListBoxsInit(this.user,NFMT.Contract.BLL.ContractBLL.OperateEnum.明细,this.curContract.ContractId)%>

            //赋值
            var tempDate = new Date("<%=curContract.ContractDate.ToString("yyyy/MM/dd")%>");
            $("#txbCreateDate").jqxDateTimeInput({ value: tempDate });

            tempDate = new Date("<%=curContract.ContractPeriodS.ToString("yyyy/MM/dd")%>");
            $("#txbFromExecDate").jqxDateTimeInput({ value: tempDate });

            tempDate = new Date("<%=curContract.ContractPeriodE.ToString("yyyy/MM/dd")%>");
            $("#txbToExecDate").jqxDateTimeInput({ value: tempDate });

            $("#txbOutContractNo").val("<%=curContract.OutContractNo%>");
            $("#selTradeDirection").val(<%=curContract.TradeDirection%>);
            $("#selAsset").val(<%=curContract.AssetId%>);
            $("#selTradeBorder").val(<%=curContract.TradeBorder%>);
            $("#selCurrency").val(<%=curContract.SettleCurrency%>);
            $("#selUnit").val(<%=curContract.UnitId%>);
            $("#selContractLimit").val(<%=curContract.ContractLimit%>);
            $("#txbSignAmount").val(<%=curContract.SignAmount%>);
            $("#txbPremium").val(<%=curContract.Premium%>);
            $("#txbMemo").val("<%=curContract.Memo%>");

            if("<%=curContract.PriceMode%>" =="<%=(int)NFMT.Contract.PriceModeEnum.点价%>")
            { 
                $("#jqxPricTabs").val(1);
                $("#selWhoDoPrice").val(<%=this.curContractPrice.WhoDoPrice%>);
                
                tempDate = new Date("<%=this.curContractPrice.DoPriceBeginDate.ToString("yyyy/MM/dd")%>");
                $("#txbDoPriceBeginDate").jqxDateTimeInput({ value: tempDate });
                tempDate = new Date("<%=this.curContractPrice.DoPriceEndDate.ToString("yyyy/MM/dd")%>");
                $("#txbDoPriceEndDate").jqxDateTimeInput({ value: tempDate });

                $("#selPriceFrom").val(<%=this.curContractPrice.PriceFrom%>);
                $("#selPriceStyle1").val(<%=this.curContractPrice.PriceStyle1%>);
                $("#selPriceStyle2").val(<%=this.curContractPrice.PriceStyle2%>);                             
            }
            else
            {
                $("#txbFixedPrice").val(<%=this.curContractPrice.FixedPrice%>);
                $("#txbFixedPriceMemo").val("<%=this.curContractPrice.FixedPriceMemo%>");
            }

            $("#selMarginMode").val(<%=this.curContractPrice.MarginMode%>);
            $("#selMarginAmount").val(<%=this.curContractPrice.MarginAmount%>);
            $("#txbMarginMemo").val("<%=this.curContractPrice.MarginMemo%>");  

            $("#txbMoreOrLess").val(<%=this.curContraceDetail.MoreOrLess*100%>);
            $("#selDelayType").val(<%=this.curContraceDetail.DelayType%>);  
            $("#txbDelayRate").val(<%=this.curContraceDetail.DelayRate%>); 
            $("#selDiscountBase").val(<%=this.curContraceDetail.DiscountBase%>); 
            $("#txbDiscountRate").val(<%=this.curContraceDetail.DiscountRate*100%>);            

            document.getElementById("chkIsQP").checked= <%=this.curContractPrice.IsQP.ToString().ToLower()%>;            
            
            var outCorpStr = "<% = this.curOutCorpsString%>";
            var outCorps = outCorpStr.split(',');
            for(k =0;k<outCorps.length;k++){
                var item = $("#selOutCorp").jqxComboBox("getItemByValue", outCorps[k]);
                $("#selOutCorp").jqxComboBox("selectItem", item);
            }

            var inCorpStr = "<% = this.curInCorpsString%>";
            var inCorps = inCorpStr.split(',');
            for(k =0;k<inCorps.length;k++){
                var item = $("#selInCorp").jqxComboBox("getItemByValue", inCorps[k]);
                $("#selInCorp").jqxComboBox("selectItem", item);
            }

            var deptStr = "<% = this.curDeptsString%>";
            if(deptStr.length>0){
                var depts = deptStr.split(',');
                for(k =0;k<depts.length;k++){
                    var item = $("#selExecDept").jqxComboBox("getItemByValue", depts[k]);
                    $("#selExecDept").jqxComboBox("removeItem", item);
                    $("#selExecDept").jqxComboBox("insertAt", item, k);
                    $("#selExecDept").jqxComboBox("checkIndex", k);
                }
            }
            
            //禁用
            document.getElementById("chkIsQP").disabled = true;
            $("#jqxPricTabs").jqxTabs({disabled: true});          

            //buttons
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 2,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });
                
            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/ContractStatusHandler.ashx", { id: "<% = this.curContract.ContractId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        window.document.location = "ContractList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/ContractStatusHandler.ashx", { id: "<% = this.curContract.ContractId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        window.document.location = "ContractList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/ContractStatusHandler.ashx", { id: "<%=this.curContract.ContractId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "ContractList.aspx";
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/ContractStatusHandler.ashx", { id: "<%=this.curContract.ContractId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "ContractList.aspx";
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxValidator">

        <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                合约主体<input type="hidden" id="hidModel" runat="server" />
            </div>
            <div class="InfoExpander">
                <ul>
                    <li>
                        <strong>签订日期：</strong>
                        <div id="txbCreateDate" style="float: left;"></div>
                    </li>
                    <li>
                        <strong>执行部门：</strong>
                        <div id="selExecDept" style="float: left;" />
                    </li>
                    <li>
                        <strong>外部合约号：</strong>
                        <input type="text" id="txbOutContractNo" />
                    </li>
                    <li>
                        <strong>我方抬头：</strong>
                        <div id="selInCorp" style="float: left;" />
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxOutCorpExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                外部公司
            </div>
            <div class="InfoExpander">
                <ul>
                    <li>
                        <strong>对方抬头：</strong>
                        <div id="selOutCorp" style="float: left;" />
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxTypeExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                合约类型及交易内容
            </div>
            <div class="InfoExpander">
                <ul>
                    <li>
                        <strong>购销方向：</strong>
                        <div id="selTradeDirection" style="float: left;" />
                        <input type="hidden" id="hidTradeDirection" runat="server" />
                    </li>
                    <li>
                        <strong>交易品种：</strong>
                        <div id="selAsset" style="float: left;" />
                    </li>
                    <li>
                        <strong>内外贸：</strong>
                        <div id="selTradeBorder" style="float: left;" />
                        <input type="hidden" id="hidTradeBorder" runat="server" />
                    </li>
                    <li>
                        <strong>执行日期：</strong>
                        <div id="txbFromExecDate" style="float: left;"></div>
                        <span style="float: left;">至</span>
                        <div id="txbToExecDate" style="float: left;"></div>
                    </li>
                    <li>
                        <strong>结算币种：</strong>
                        <div id="selCurrency" style="float: left;" />
                    </li>
                    <li>
                        <strong>长单零单：</strong>
                        <div id="selContractLimit" style="float: left;" />
                        <input type="hidden" id="hidContractLimit" runat="server" />
                    </li>
                    <li>
                        <strong>签定数量：</strong>
                        <div style="float: left" id="txbSignAmount"></div>
                    </li>
                    <li>
                        <strong>单位：</strong>
                        <div id="selUnit" style="float: left;" />
                    </li>
                    <li>
                        <strong>升贴水：</strong>
                        <div style="float: left" id="txbPremium"></div>
                    </li>
                    <li>
                        <strong>备注：</strong>
                        <textarea id="txbMemo"></textarea>
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxPriceExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                价格明细
            </div>
            <div>
                <div id="jqxPricTabs">
                    <ul>
                        <li style="margin-left: 30px;">定价</li>
                        <li>点价</li>
                    </ul>
                    <div class="InfoExpander">
                        <ul>
                            <li>
                                <strong>裸价单价：</strong>
                                <div style="float: left" id="txbFixedPrice"></div>
                            </li>
                            <li>
                                <strong>价格备注：</strong>
                                <input type="text" id="txbFixedPriceMemo" />
                            </li>
                        </ul>
                    </div>
                    <div class="InfoExpander">
                        <ul>
                            <li>
                                <strong>点价方：</strong><input type="hidden" id="hidWhoDoPrice" runat="server" />
                                <div style="float: left" id="selWhoDoPrice"></div>
                            </li>
                            <li>
                                <strong>点价日期：</strong>
                                <div style="float: left" id="txbDoPriceBeginDate"></div>
                                <span style="float: left;">至</span>
                                <div style="float: left" id="txbDoPriceEndDate"></div>
                            </li>
                            <li>
                                <strong>可否QP延期：</strong>
                                <input type="checkbox" id="chkIsQP" />
                            </li>
                            <li>
                                <strong>裸价来源：</strong>
                                <div style="float: left" id="selPriceFrom"></div>
                            </li>
                            <li>
                                <strong>作价方式：</strong><input type="hidden" id="hidSummaryPrice" runat="server" />
                                <div style="float: left" id="selPriceStyle1"></div>
                                <div style="float: left" id="selPriceStyle2"></div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="InfoExpander">
                    <ul>
                        <li>
                            <strong>保证金条款：</strong>
                            <div id="selMarginMode" style="float: left;" />
                            <input type="hidden" id="hidMarginMode" runat="server" />
                        </li>
                        <li>
                            <strong>保证金数量：</strong>
                            <div style="float: left" id="selMarginAmount"></div>
                        </li>
                        <li>
                            <strong>保证金描述：</strong>
                            <textarea id="txbMarginMemo"></textarea>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div id="jqxDetailExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                费用
            </div>
            <div class="InfoExpander">
                <ul>

                    <li>
                        <strong>溢短装：</strong>
                        <div style="float: left" id="txbMoreOrLess"></div>
                    </li>
                    <li>
                        <input type="hidden" runat="server" id="hidValueRateType" />
                        <strong>延期费/率：</strong>
                        <div style="float: left" id="selDelayType"></div>
                        <div style="float: left" id="txbDelayRate"></div>
                    </li>
                    <li>
                        <strong>贴现费/率：</strong><input type="hidden" runat="server" id="hidDiscountBase" />
                        <div style="float: left" id="selDiscountBase"></div>
                        <span style="float: left">+</span>
                        <div style="float: left" id="txbDiscountRate"></div>
                    </li>

                </ul>
            </div>
        </div>

        <div id="jqxClauseExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                合约条款
            </div>
            <div>
                <%=contractBLL.GetTabString(this.user)%>
            </div>
        </div>

         <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="ContractAttach" />

        <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
            <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                        <input type="button" id="btnComplete" value="完成" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
                        <input type="button" id="btnCompleteCancel" value="完成撤销" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        </div>
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
