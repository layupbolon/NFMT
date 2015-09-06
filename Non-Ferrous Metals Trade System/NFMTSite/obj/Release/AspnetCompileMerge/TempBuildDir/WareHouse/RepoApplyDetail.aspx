<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepoApplyDetail.aspx.cs" Inherits="NFMTSite.WareHouse.RepoApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回购申请明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.apply.DataBaseName%>" + "&t=" + "<%=this.apply.TableName%>" + "&id=" + "<%=this.apply.ApplyId%>";

        $(document).ready(function () {

            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });
            $("#jqxRepoExpander").jqxExpander({ width: "98%" });

            //按钮
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnConfirm").jqxInput();
            $("#btnConfirmCancel").jqxInput();

            var id = $("#hidid").val();

            var stockIds = new Array();
            var sids = "<%=this.stockIds%>";

            //绑定Grid
            var url = "Handler/RepoApplyInfoHandler.ashx?mode=1&sids=" + sids;
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "BankName", type: "string" }
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
                sortcolumn: "st.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "st.StockId";
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
                  { text: "权证编号", datafield: "PaperNo" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "质押银行", datafield: "BankName" }
                ]
            });

            var url = "Handler/RepoApplyDetailRepoInfoHandler.ashx?r=" + id;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "RepoId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "RepoerTime", type: "date" },
                   { name: "RepoStatus", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "Memo", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "r.RepoId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "r.RepoId";
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
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">"
                + "<a target=\"_self\" href=\"RepoDetail.aspx?f=n&id=" + value + "\">查看</a>"
                + "</div>";
            }

            $("#jqxGrid").jqxGrid(
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
                  { text: "回购人", datafield: "Name" },
                  { text: "回购时间", datafield: "RepoerTime", cellsformat: "yyyy-MM-dd" },
                  { text: "回购附言", datafield: "Memo" },
                  { text: "回购状态", datafield: "StatusName" },
                  { text: "操作", datafield: "RepoId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 18,
                    model: $("#hidmodel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/RepoApplyStatusHandler.ashx", { ri: "<%=this.repoId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "RepoApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/RepoApplyStatusHandler.ashx", { ri: "<%=this.repoId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "RepoApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnConfirm").on("click", function () {
                if (!confirm("确认执行完成操作？")) { return; }
                var operateId = operateEnum.确认完成;
                $.post("Handler/RepoApplyStatusHandler.ashx", { ri: "<%=this.repoId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "RepoApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnConfirmCancel").on("click", function () {
                if (!confirm("撤销后已关闭的明细申请将会打开，执行完成撤销操作？")) { return; }
                var operateId = operateEnum.确认完成撤销;
                $.post("Handler/RepoApplyStatusHandler.ashx", { ri: "<%=this.repoId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "RepoApplyList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据明细
        </div>
        <div>
            <input type="hidden" id="hidid" runat="server" />
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxRepoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回购执行明细
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回购申请明细
            <input type="hidden" id="hidmodel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>申请部门：</span></h4>
                    <span id="txbApplyDept" style="float: left;" runat="server"></span>
                </li>
                <li>
                    <h4><span>申请公司：</span></h4>
                    <span id="txbApplyCorp" style="float: left;" runat="server"></span>
                </li>
                <li>
                    <h4><span>备注：</span></h4>
                    <span id="txbMemo" style="float: left;" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 20px;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnConfirm" value="执行完成确认" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnConfirmCancel" value="执行完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
