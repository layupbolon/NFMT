<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RateList.aspx.cs" Inherits="NFMTSite.BasicData.RateList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>汇率管理</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script src="../js/Utility.js"></script>
    <script src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            //汇率日期
            $("#rateDate").jqxDateTimeInput({ width: 150, height: 25, formatString: "yyyy-MM-dd" });

            //绑定 币种
            var url = "Handler/CurrencDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#noW").jqxComboBox({ source: dataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true });

            $("#noT").jqxComboBox({ source: dataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 100, height: 25, autoDropDownHeight: true });

            //init buttons
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            var currency1 = $("#noW").val();
            var currency2 = $("#noT").val();
            var rateDate = $("#rateDate").val();
            var url = "Handler/RateListHandler.ashx?c1=" + currency1 + "&c2=" + currency2 + "&rd=" + rateDate;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                 [
                    { name: "RateId", type: "int" },
                    { name: "CreateTime", type: "date" },
                    { name: "CurrencyName_1", type: "string" },
                    { name: "RateValue", type: "number" },
                    { name: "CurrencyName_2", type: "string" },
                    { name: "StatusName", type: "string" }
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
                sortcolumn: "r.RateId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "r.RateId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url

            };
            //alert(source);
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"RateDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"RateUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
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
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "汇率日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd" },
                  { text: "兑换币种", datafield: "CurrencyName_1" },
                  { text: "汇率", datafield: "RateValue" },
                  { text: "换至币种", datafield: "CurrencyName_2" },
                  { text: "汇率状态", datafield: "StatusName" },
                  { text: "操作", datafield: "RateId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            });

            function LoadData() {
                var currency1 = $("#noW").val();
                var currency2 = $("#noT").val();
                var rateDate = $("#rateDate").val();
                source.url = "Handler/RateListHandler.ashx?c1=" + currency1 + "&c2=" + currency2 + "&rd=" + rateDate;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            }

            $("#btnSearch").click(LoadData);

        });
    </script>
</head>
<body>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div id="SearchDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">汇率日期：</span>
                    <div id="rateDate" runat="server" style="float: right;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">兑换币种：</span>
                    <div id="noW" runat="server" style="float: right;" />
                    <input type="hidden" runat="server" id="hidCurrency1" />
                </li>

                <li>
                    <span style="width: 15%; text-align: right;">换至币种：</span>
                    <div id="noT" runat="server" style="float: right;" />
                    <input type="hidden" runat="server" id="hidCurrency2" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="RateGreate.aspx" id="btnAdd">新增汇率</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div style="height: 500px;">
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

</body>
</html>
