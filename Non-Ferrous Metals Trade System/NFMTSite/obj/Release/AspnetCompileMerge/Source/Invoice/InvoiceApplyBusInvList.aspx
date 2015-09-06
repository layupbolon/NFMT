<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApplyBusInvList.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceApplyBusInvList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>业务发票列表</title>
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

            //合约号
            $("#txbContractNo").jqxInput({ height: 23 });

            //客户
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlCusterCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //品种
            var assSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assDataAdapter = new $.jqx.dataAdapter(assSource);
            $("#ddlAssetId").jqxComboBox({ source: assDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, autoDropDownHeight: true });
            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnCreate").jqxButton({ height: 25, width: 120 });


            var contractNo = $("#txbContractNo").val();
            var custerCorp = $("#ddlCusterCorp").val();
            var assetId = $("#ddlAssetId").val();

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "BusinessInvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "number" },
                   { name: "InoviceBalaName", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "AssetId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "IntegerAmount", type: "number" },
                   { name: "IntegerAmountName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "NetAmountName", type: "string" },
                   { name: "UnitPrice", type: "number" },
                   { name: "MarginRatio", type: "number" },
                   { name: "VATRatio", type: "number" },
                   { name: "VATBala", type: "number" },
                   { name: "AssetId", type: "int" },
                   { name: "CurrencyId", type: "int" },
                   { name: "OutCorpId", type: "int" },
                   { name: "InCorpId", type: "int" },
                   { name: "MUId", type: "int" }
                ],
                datatype: "json",
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
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/InvoiceApplyBusInvListHandler.ashx?corpId=" + custerCorp + "&ass=" + assetId + "&cno=" + contractNo
            };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

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
                selectionmode: "checkbox",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" ,width:150},
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd", width: 150 },
                  { text: "客户", datafield: "OutCorpName", width: 250 },
                  { text: "发票金额", datafield: "InoviceBalaName", width: 200 },
                  { text: "备注", datafield: "Memo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛量", datafield: "IntegerAmountName", width: 150 },
                  { text: "净量", datafield: "NetAmountName", width: 150 }
                ]
            });


            $("#btnSearch").click(function () {
                var contractNo = $("#txbContractNo").val();
                var custerCorp = $("#ddlCusterCorp").val();
                var assetId = $("#ddlAssetId").val();

                source.url = "Handler/InvoiceApplyBusInvListHandler.ashx?corpId=" + custerCorp + "&ass=" + assetId + "&cno=" + contractNo
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnCreate").click(function () {
                var rows = $("#jqxgrid").jqxGrid("getselectedrowindexes");
                var busInvIds = "";
                var custCorpId = 0;
                var assetId = 0;
                for (i = 0; i < rows.length; i++) {
                    var item = dataAdapter.records[rows[i]];

                    if (custCorpId == 0 || assetId == 0) {
                        custCorpId = item.OutCorpId;
                        assetId = item.AssetId;
                    } else {
                        if (item.OutCorpId != custCorpId || item.AssetId != assetId) {
                            alert("请选择相同客户及品种的业务票！");
                            return;
                        }
                    }
                    
                    if (i != 0) { busInvIds += ","; }
                    busInvIds += item.BusinessInvoiceId;
                }

                if (busInvIds != "")
                    document.location.href = "InvoiceApplyCreate.aspx?busInvIds=" + busInvIds;
                else
                    alert("请选择业务发票");
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">合约号：</span>
                    <input type="text" id="txbContractNo" />
                </li>
                <li>
                    <span style="float: left;">客户：</span>
                    <div id="ddlCusterCorp" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <input type="button" id="btnCreate" value="新增开票申请" />
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