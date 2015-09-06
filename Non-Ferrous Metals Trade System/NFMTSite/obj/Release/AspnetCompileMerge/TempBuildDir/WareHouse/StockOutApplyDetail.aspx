<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutApplyDetail.aspx.cs" Inherits="NFMTSite.WareHouse.StockOutApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出库申请明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.mainApply.DataBaseName%>" + "&t=" + "<%=this.mainApply.TableName%>" + "&id=" + "<%=this.mainApply.ApplyId%>";

        $(document).ready(function () {
            $("#jqxAllotStockListExpander").jqxExpander({ width: "98%" });
            $("#jqxAuditInfoExpander").jqxExpander({ width: "98%" });

            var formatedData = "";
            var totalrecords = 0;
            var allotSource =
            {
                datatype: "json",                
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
                localdata: <%=this.JsonStr%>
                };
            var allotDataAdapter = new $.jqx.dataAdapter(allotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxAllotStockListGrid").jqxGrid(
            {
                width: "98%",
                source: allotDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "库存重量", datafield: "CurNetAmount" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "捆数", datafield: "Bundles"},
                  { text: "申请重量", datafield: "NetAmount" }
                ]
            });

            //申请信息
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#txbMemo").jqxInput({ width: "500", disabled: true });
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ selectedIndex: 0, source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").val(<%=this.curApply.ApplyDept%>);
            
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#selApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase",disabled:true
            });
            $("#selApplyCorp").val(<%=this.curApply.ApplyCorp%>);

            //收货公司
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0&SubId=" + "<%=this.curSub.SubId%>";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#selBuyCorp").jqxComboBox({
                source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25, searchMode: "containsignorecase",disabled:true
            });
            $("#selBuyCorp").val(<%=this.curStockOutApply.BuyCorpId%>);

            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnConfirm").jqxInput();
            $("#btnConfirmCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 6,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/StockOutApplyStatusHandler.ashx", { si: "<%=this.curStockOutApply.StockOutApplyId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutApplyList.aspx";          
                        }  
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/StockOutApplyStatusHandler.ashx", { si: "<%=this.curStockOutApply.StockOutApplyId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutApplyList.aspx";          
                        } 
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认执行完成操作？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/StockOutApplyStatusHandler.ashx", { si: "<%=this.curStockOutApply.StockOutApplyId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutApplyList.aspx";          
                        } 
                    }
                );
            });

            $("#btnConfirmCancel").on("click", function () {
                if (!confirm("撤销后已关闭的明细申请将会打开，执行完成撤销操作？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/StockOutApplyStatusHandler.ashx", { si: "<%=this.curStockOutApply.StockOutApplyId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutApplyList.aspx";          
                        } 
                    }
                );
            });


        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />
    <NFMT:ContractExpander runat ="server" ID="contractExpander1" />

    <div id="jqxAllotStockListExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            出库申请配货库存列表
                        <input type="hidden" id="hidStockOutApplyId" runat="server" />
        </div>
        <div>
            <div id="jqxAllotStockListGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
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
                    <div style="float: left;" id="selApplyCorp"></div>
                </li>
                <li style="float: none;">
                    <strong>收货公司：</strong>
                    <div style="float: left;" id="selBuyCorp"></div>
                </li>
                <li style="float: none;">
                    <strong>申请部门：</strong>
                    <div style="float: left;" id="ddlApplyDeptId"></div>
                </li>
                <li style="line-height: none; height: auto; float: none">
                    <strong>申请备注：</strong>
                    <textarea id="txbMemo" runat="server"></textarea>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxAuditInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            付款申请审核信息
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <span id="txbAuditInfo" runat="server" />
                </li>
            </ul>
        </div>
    </div>

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
