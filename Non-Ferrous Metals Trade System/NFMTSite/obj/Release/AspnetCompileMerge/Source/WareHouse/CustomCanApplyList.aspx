<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCanApplyList.aspx.cs" Inherits="NFMTSite.WareHouse.CustomCanApplyList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>可报关申请列表</title>
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

            //申请部门
            var ddlCorpIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlApplyDept").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, autoDropDownHeight: true });

            //时间控件
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", datafields: [{ name: "AssetId" }, { name: "AssetName" }], url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 80, height: 25 });

            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            //关外公司
            $("#ddlOutCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            //报关公司
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });

            //绑定Grid
            var applyDept = $("#ddlApplyDept").val();
            var fromDate = $("#txbFromCreateDate").val();
            var toDate = $("#txbToCreateDate").val();
            var assetId = $("#ddlAssetId").val();
            var outCorpId = $("#ddlOutCorpId").val();
            var customCorpId = $("#ddlCustomsCorpId").val();
            var url = "Handler/CustomCanApplyListHandler.ashx?a=" + applyDept + "&f=" + fromDate + "&t=" + toDate + "&ass=" + assetId + "&outCorpId=" + outCorpId + "&customCorpId=" + customCorpId;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "CustomsApplyId", type: "int" },
                   { name: "ApplyNo", type: "string" },
                   { name: "ApplyTime", type: "date" },
                   { name: "StatusName", type: "string" },
                   { name: "Name", type: "string" },
                   { name: "DeptName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "OutCorpId", type: "string" },
                   { name: "CustomsCorpId", type: "string" },
                   { name: "GrossWeight", type: "string" },
                   { name: "NetWeight", type: "string" }
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
                sortcolumn: "cca.CustomsApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cca.CustomsApplyId";
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
                return "&nbsp;&nbsp;&nbsp;<a target=\"_self\" href=\"CustomsClearanceCreate.aspx?id=" + value + "\">报关</a>";
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
                  { text: "申请编号", datafield: "ApplyNo" },
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请人", datafield: "Name" },
                  { text: "申请部门", datafield: "DeptName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "申请总毛重", datafield: "GrossWeight" },
                  { text: "申请总净重", datafield: "NetWeight" },
                  { text: "关外公司", datafield: "OutCorpId" },
                  { text: "报关公司", datafield: "CustomsCorpId" },
                  { text: "操作", datafield: "CustomsApplyId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var applyDept = $("#ddlApplyDept").val();
                var fromDate = $("#txbFromCreateDate").val();
                var toDate = $("#txbToCreateDate").val();
                var assetId = $("#ddlAssetId").val();
                var outCorpId = $("#ddlOutCorpId").val();
                var customCorpId = $("#ddlCustomsCorpId").val();
                source.url = "Handler/CustomCanApplyListHandler.ashx?a=" + applyDept + "&f=" + fromDate + "&t=" + toDate + "&ass=" + assetId + "&outCorpId=" + outCorpId + "&customCorpId=" + customCorpId;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">申请部门：</span>
                    <div id="ddlApplyDept" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">申请日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">关外公司：</span>
                    <div id="ddlOutCorpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">报关公司：</span>
                    <div id="ddlCustomsCorpId" style="float: left;"></div>
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
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
