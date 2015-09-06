<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReceiptUpdateList.aspx.cs" Inherits="NFMTSite.WareHouse.StockReceiptUpdateList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存净重回执修改列表</title>
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

            $("#txbRefNo").jqxInput({ height: 23, width: 120 });

            var repoInfoSource = [{ text: "全部", value: 0 }, { text: "入库回执", value: 224 }, { text: "出库回执", value: 225 }];

            //赎回情况
            $("#dllReceiptType").jqxDropDownList({
                source: repoInfoSource,
                selectedIndex: 1,
                height: 25,
                width: 100,
                displayMember: "text",
                valueMember: "value",
                autoDropDownHeight: true
            });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var refNo = $("#txbRefNo").val();
            var receiptType = $("#dllReceiptType").val();
            var url = "Handler/StockReceiptUpdateListHandler.ashx?refno=" + refNo + "&type=" + receiptType;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "StockLogId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "RefNo", type: "string" },
                   { name: "StockDate", type: "date" },
                   { name: "CardNo", type: "string" },
                   { name: "PreNetAmount", type: "number" },
                   { name: "ReceiptAmount", type: "number" },
                   { name: "DetailName", type: "string" }
                ],
                sort: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "srd.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "srd.DetailId";
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
                var item = $("#jqxgrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"StockReceiptUpdateCreate.aspx?stockLogId=" + item.StockLogId + "&stockId=" + item.StockId + "&id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxgrid").jqxGrid(
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
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "入库日期", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "卡号", datafield: "CardNo",width:"10%" },
                  { text: "库存回执类型", datafield: "DetailName" },
                  { text: "回执前净重", datafield: "PreNetAmount" },
                  { text: "回执后净重", datafield: "ReceiptAmount" },
                  { text: "操作", datafield: "DetailId", cellsrenderer: cellsrenderer, sortable: false, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var refNo = $("#txbRefNo").val();
                var receiptType = $("#dllReceiptType").val();

                source.url = "Handler/StockReceiptUpdateListHandler.ashx?refno=" + refNo + "&type=" + receiptType;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });
            
        });
    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>业务单号：</span>
                    <span>
                        <input type="text" id="txbRefNo" /></span>
                </li>
                <li>
                    <span style="float:left;">库存回执类型：</span>
                    <div id="dllReceiptType" style="float:left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
