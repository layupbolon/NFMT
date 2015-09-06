<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableStockDetail.aspx.cs" Inherits="NFMTSite.Funds.ReceivableStockDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存收款分配明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "RefId", type: "int" },
                   { name: "PayBala", type: "string" },
                   { name: "AllotBala", type: "string" },
                   { name: "RefNo", type: "string" }
                ],
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "ref.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ref.RefId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/ReceivableStockDetailHandler.ashx?id=" + $("#hidid").val()
            };
            var DataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: DataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款金额", datafield: "PayBala" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "分配业务单号", datafield: "RefNo" }
                ]
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确定要作废？")) { return; }
                $.post("Handler/ReceivableStockInvalidHandler.ashx", { id: $("#hidid").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableStockList.aspx";
                    }
                );
            });

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 12,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确定要撤返？")) { return; }
                $.post("Handler/ContractReceivableGoBackHandler.ashx", { id: $("#hidid").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableStockList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确定完成？")) { return; }
                $.post("Handler/ReceivableStockCompleteHandler.ashx", { id: $("#hidid").val() },
                    function (result) {
                        alert(result);
                        window.document.location = "ReceivableStockList.aspx";
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存收款分配明细
            <input type="hidden" id="hidid" runat="server" />
            <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>分配金额：</strong>
                    <span id="spanAllotBala" style="float: left;" runat="server"></span>
                </li>
                <li><strong>备注：</strong>
                    <span id="spanAllotDesc" style="float: left;" runat="server"></span></li>
                <li>
                    <strong>分配人：</strong>
                    <span id="spanEmpId" style="float: left;" runat="server"></span>
                </li>
                <li><strong>分配时间：</strong>
                    <span id="spanAllotTime" style="float: left;" runat="server"></span></li>
            </ul>
        </div>
    </div>
    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>数据明细</div>
        <div>
            <div id="jqxGrid"></div>
        </div>
    </div>
    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
    </div>
</body>
</html>
