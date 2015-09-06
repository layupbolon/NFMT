<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockMoveApplyDetail.aspx.cs" Inherits="NFMTSite.WareHouse.StockMoveApplyDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>移库申请明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.apply.DataBaseName%>" + "&t=" + "<%=this.apply.TableName%>" + "&id=" + "<%=this.applyId%>";

        $(document).ready(function () {
            
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxStockMoveApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxStockMoveExpander").jqxExpander({ width: "98%" });

            //按钮
            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();

            var id = $("#hidid").val();

            //移库申请明细
            var url = "Handler/StockMoveApplyDetailHandler.ashx?id=" + id;
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "DeliverPlaceId", type: "int" },
                   { name: "DeliverPlace", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
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
                sortcolumn: "detail.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "detail.DetailId";
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
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DeliverPlace" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "权证编号", datafield: "PaperNo" },
                ]
            });

            if ($("#hidStockMoveId").val() > 0) {

                //移库明细
                var url = "Handler/StockMoveInfoForApplyDetailHandler.ashx?s=" + $("#hidStockMoveId").val();
                var formatedData = "";
                var totalrecords = 0;
                var source =
                {
                    datafields:
                    [
                       { name: "StockMoveId", type: "int" },
                       { name: "Name", type: "string" },
                       { name: "MoveTime", type: "date" },
                       { name: "StatusName", type: "string" },
                       { name: "DPName", type: "string" },
                       { name: "MoveMemo", type: "string" }
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
                    sortcolumn: "sm.StockMoveId",
                    sortdirection: "desc",
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 10;
                        data.sortdatafield = data.sortdatafield || "sm.StockMoveId";
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
                       + "<a target=\"_self\" href=\"StockMoveDetail.aspx?f=n&id=" + value + "\">查看</a>"
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
                      { text: "移库人", datafield: "Name" },
                      { text: "移库时间", datafield: "MoveTime", cellsformat: "yyyy-MM-dd" },
                      { text: "交货地", datafield: "DPName" },
                      { text: "备注", datafield: "MoveMemo" },
                      { text: "移库状态", datafield: "StatusName" },
                      { text: "操作", datafield: "StockMoveId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                    ]
                });

            }
            else {
                $("#jqxStockMoveExpander").jqxExpander("destroy");
            }

            $("#btnAudit").on("click", function (e) {
                //alert($("#hidmodel").val());
                var paras = {
                    mid: 36,
                    model: $("#hidmodel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/StockMoveApplyGoBackHandler.ashx", { id: id },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockMoveApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确定要作废？")) { return; }
                $.post("Handler/StockMoveApplyInvalidHandler.ashx", { id: id },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockMoveApplyList.aspx";
                        }
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确定要完成？")) { return; }
                $.post("Handler/StockMoveApplyCompleteHandler.ashx", { id: id },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockMoveApplyList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStockMoveApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            移库申请明细
        </div>
        <div>
            <input type="hidden" id="hidid" runat="server" />
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxStockMoveExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            移库执行明细
        </div>
        <div>
            <input type="hidden" id="hidStockMoveId" runat="server" />
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            移库申请明细
                        <input type="hidden" id="hidmodel" runat="server" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>申请部门：</span></h4>
                    <span id="txbApplyDept" style="float: left;" runat="server"></span>

                    <h4><span>申请公司：</span></h4>
                    <span id="txbApplyCorp" style="float: left;" runat="server"></span>
                </li>
                <li>
                    <h4><span>申请人：</span></h4>
                    <span id="txbApplyPerson" style="float: left;" runat="server"></span>

                    <h4><span>备注：</span></h4>
                    <span id="txbMemo" style="float: left;" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
    </div>

</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
