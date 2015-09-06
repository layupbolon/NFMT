<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutDetail.aspx.cs" Inherits="NFMTSite.WareHouse.StockOutDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出库明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>    

    <script type="text/javascript">

        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.stockOut.DataBaseName%>" + "&t=" + "<%=this.stockOut.TableName%>" + "&id=" + "<%=this.StockOutId%>";

        var stockOutingSource;
        var selectedSource;
        var details = new Array();

        $(document).ready(function () {
            //init sids
            var initSids = "<%= this.DetailStr%>";
            var splitItem = initSids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                details.push(splitItem[i]);
            }

            $("#jqxStockOutApplyExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxSelectedExpander").jqxExpander({ width: "98%" });
            $("#jqxInfoExpander").jqxExpander({ width: "98%" });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500", disabled: true });

            //已选中列表
            var formatedData = "";
            var totalrecords = 0;

            selectedSource =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxSelectedGrid").jqxGrid("updatebounddata", "sort");
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
                url: "Handler/StockOutSelectedListHandler.ashx?sids=" + initSids
            };
            var selectedDataAdapter = new $.jqx.dataAdapter(selectedSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxSelectedGrid").jqxGrid(
            {
                width: "98%",
                source: selectedDataAdapter,
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
                  { text: "库存净重", datafield: "CurNetAmount" },
                  { text: "出库净重", datafield: "NetAmount" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "重量单位", datafield: "MUName" },
                  { text: "库存状态", datafield: "StockStatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" }
                ]
            });

            //init button
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnClose").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 8,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/StockOutStatusHandler.ashx", { si: "<%=this.StockOutId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/StockOutStatusHandler.ashx", { si: "<%=this.StockOutId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutList.aspx";
                        }
                    }
                );
            });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/StockOutStatusHandler.ashx", { si: "<%=this.StockOutId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/StockOutStatusHandler.ashx", { si: "<%=this.StockOutId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutList.aspx";
                        }
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("完成撤销后，所有已完成的明细将会更新至已生效，确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/StockOutStatusHandler.ashx", { si: "<%=this.StockOutId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockOutList.aspx";
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
    <NFMT:ContractExpander runat="server" ID="contractExpander1" />

    <div id="jqxStockOutApplyExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            出库申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>申请部门：</strong>
                    <span runat="server" id="spnApplyDept"></span>
                </li>
                <li>
                    <strong>申请人：</strong>
                    <span runat="server" id="spnApplier"></span>
                </li>
                <li>
                    <strong>申请时间：</strong>
                    <span runat="server" id="spnApplyDate"></span>
                </li>
                <li>
                    <strong>申请附言：</strong>
                    <span runat="server" id="spnApplyMemo"></span>
                </li>
                <li>
                    <strong>审核明细：</strong>
                    <a href="#">查看</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxSelectedExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            选中库存列表
        </div>
        <div>
            <div id="jqxSelectedGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="jqxInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            出库信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="line-height: none; height: auto; float: none">
                    <strong>申请备注：</strong>
                    <textarea id="txbMemo" runat="server"></textarea>
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="StockOutAttach" />

    <div id="buttons" style="text-align: center; width: 80%;">
        <input type="button" id="btnAudit" value="提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnClose" value="关闭" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnComplete" value="完成" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCompleteCancel" value="完成撤销" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>

</html>
