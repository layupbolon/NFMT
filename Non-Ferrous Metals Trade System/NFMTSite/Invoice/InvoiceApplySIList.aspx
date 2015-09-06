<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApplySIList.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceApplySIList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>可开票价外票列表</title>
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

            //客户
            var outCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=0";
            var outCorpSource = { datatype: "json", url: outCorpUrl, async: false };
            var outCorpDataAdapter = new $.jqx.dataAdapter(outCorpSource);
            $("#ddlCusterCorp").jqxComboBox({ source: outCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", height: 25 });

            //开票日期
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 120 });
            $("#btnCreate").jqxButton({ height: 25, width: 120 });

            var custerCorp = $("#ddlCusterCorp").val();
            var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
            var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "InCorpId", type: "int" },
                   { name: "OutCorp", type: "string" },
                   { name: "OutCorpId", type: "int" },
                   { name: "StatusName", type: "string" },
                   { name: "InvoiceStatus", type: "int" }, 
                   { name: "SIId", type: "int" }
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
                url: "Handler/InvoiceApplySIListHandler.ashx?corpId=" + custerCorp + "&db=" + fromDate + "&de=" + toDate
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
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "对方公司", datafield: "InnerCorp" },
                  { text: "我方公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" }
                ]
            });

            $("#btnSearch").click(function () {
                var custerCorp = $("#ddlCusterCorp").val();
                var fromDate = $("#txbFromCreateDate").jqxDateTimeInput("getText");
                var toDate = $("#txbToCreateDate").jqxDateTimeInput("getText");

                source.url = "Handler/InvoiceApplySIListHandler.ashx?corpId=" + custerCorp + "&db=" + fromDate + "&de=" + toDate;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnCreate").click(function () {
                var rows = $("#jqxgrid").jqxGrid("getselectedrowindexes");
                var sIIds = "";
                var custCorpId = 0;
                for (i = 0; i < rows.length; i++) {
                    var item = dataAdapter.records[rows[i]];

                    if (custCorpId == 0) {
                        custCorpId = item.InCorpId;
                    } else {
                        if (item.InCorpId != custCorpId) {
                            alert("请选择相同客户的价外票！");
                            return;
                        }
                    }

                    if (i != 0) { sIIds += ","; }
                    sIIds += item.SIId;
                }

                if (sIIds != "")
                    document.location.href = "InvoiceApplySICreate.aspx?sIIds=" + sIIds;
                else
                    alert("请选择价外票！");
            });
        });
    </script>

</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">客户：</span>
                    <div id="ddlCusterCorp" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">开票日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
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
