<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomsClearanceList.aspx.cs" Inherits="NFMTSite.WareHouse.CustomsClearanceList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报关</title>
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

            //报关人
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlCustomser").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 100, height: 25, autoDropDownHeight: true });

            //报关公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            //品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", datafields: [{ name: "AssetId" }, { name: "AssetName" }], url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 80, height: 25 });

            //时间控件
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //报关状态
            CreateStatusDropDownList("ddlCustomsStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var person = $("#ddlCustomser").val();
            var corpId = $("#ddlCustomsCorpId").val();
            var assetId = $("#ddlAssetId").val();
            var fdate = $("#txbFromCreateDate").val();
            var tdate = $("#txbToCreateDate").val();
            var status = $("#ddlCustomsStatus").val();
            var url = "Handler/CustomListHandler.ashx?p=" + person + "&c=" + corpId + "&a=" + assetId + "&f=" + fdate + "&t=" + tdate + "&s=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "CustomsId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "CustomsDate", type: "date" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossWeight", type: "string" },
                   { name: "NetWeight", type: "string" },
                   { name: "CustomsPrice", type: "string" },
                   { name: "TariffRate", type: "number" },
                   { name: "AddedValueRate", type: "number" },
                   { name: "OtherFees", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "CustomsStatus", type: "int" }
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
                sortcolumn: "cc.CustomsId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cc.CustomsId";
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
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";

                cellHtml += "<a target=\"_self\" href=\"CustomsClearanceDetail.aspx?id=" + value + "\">查看</a>"

                if (item.CustomsStatus > statusEnum.已关闭 && item.CustomsStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CustomsClearanceUpdate.aspx?id=" + value + "\">修改</a>"
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
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
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "报关人", datafield: "Name" },
                  { text: "实际报关公司", datafield: "CorpName" },
                  { text: "报关时间", datafield: "CustomsDate", cellsformat: "yyyy-MM-dd" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "报关总毛重", datafield: "GrossWeight" },
                  { text: "报关总净重", datafield: "NetWeight" },
                  { text: "报关单价", datafield: "CustomsPrice" },
                  { text: "关税税率", datafield: "TariffRate" },
                  { text: "增值税率", datafield: "AddedValueRate" },
                  { text: "检验检疫费", datafield: "OtherFees" },
                  { text: "备注", datafield: "Memo" },
                  { text: "报关状态", datafield: "StatusName" },
                  { text: "操作", datafield: "CustomsId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var person = $("#ddlCustomser").val();
                var corpId = $("#ddlCustomsCorpId").val();
                var assetId = $("#ddlAssetId").val();
                var fdate = $("#txbFromCreateDate").val();
                var tdate = $("#txbToCreateDate").val();
                var status = $("#ddlCustomsStatus").val();
                source.url = "Handler/CustomListHandler.ashx?p=" + person + "&c=" + corpId + "&a=" + assetId + "&f=" + fdate + "&t=" + tdate + "&s=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>

</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px; padding-bottom: 10px;">
        <div>
            查询条件
                        <input type="hidden" id="hidBDStyleId" runat="server" />
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span style="float: left;">报关人：</span>
                    <div id="ddlCustomser" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">实际报关公司：</span>
                    <div id="ddlCustomsCorpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">报关品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">申请日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">报关状态：</span>
                    <div id="ddlCustomsStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="CustomCanApplyList.aspx" id="btnAdd">报关</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
