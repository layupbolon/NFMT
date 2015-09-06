<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingApplyList.aspx.cs" Inherits="NFMTSite.DoPrice.PricingApplyList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点价申请列表</title>
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

            //申请人, autoDropDownHeight: true
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlApplyPerson").jqxComboBox({ source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 150, height: 25 });

            //子合约号
            $("#txbSubNo").jqxInput({ height: 25, width: 120 });

            //点价品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetAuthHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", datafields: [{ name: "AssetId" }, { name: "AssetName" }], url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 150, height: 25 });

            //点价申请状态
            CreateStatusDropDownList("ddlPricingStatus");

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });

            //绑定Grid
            var applyPerson = $("#ddlApplyPerson").val();
            var subNo = $("#txbSubNo").val();
            var assetId = $("#ddlAssetId").val();
            var status = $("#ddlPricingStatus").val();
            var url = "Handler/PricingApplyListHandler.ashx?applyPerson=" + applyPerson + "&subNo=" + subNo + "&assetId=" + assetId + "&status=" + status;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "PricingApplyId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "DeptName", type: "string" },
                   { name: "ApplyCorp", type: "string" },
                   { name: "StartTime", type: "date" },
                   { name: "EndTime", type: "date" },
                   { name: "SubNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "PricingWeight", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "Job", type: "string" },
                   { name: "PricingPhone", type: "string" },
                   { name: "ApplyStatus", type: "int" },
                   { name: "ApplyDesc", type: "string" },
                   { name: "StatusName", type: "string" }
                ],
                datatype: "json",
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
                sortcolumn: "pa.PricingApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pa.PricingApplyId";
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

                cellHtml += "<a target=\"_self\" href=\"PricingApplyDetail.aspx?id=" + value + "\">查看</a>"

                if (item.ApplyStatus > statusEnum.已关闭 && item.ApplyStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PricingApplyUpdate.aspx?id=" + value + "\">修改</a>"
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
                  { text: "申请人", datafield: "Name" },
                  { text: "申请公司", datafield: "ApplyCorp" },
                  { text: "申请部门", datafield: "DeptName" },
                  { text: "申请附言", datafield: "ApplyDesc" },
                  { text: "点价起始时间", datafield: "StartTime", cellsformat: "yyyy-MM-dd" },
                  { text: "点价最终时间", datafield: "EndTime", cellsformat: "yyyy-MM-dd" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "点价品种", datafield: "AssetName" },
                  { text: "点价重量", datafield: "PricingWeight" },
                  { text: "点价人所属公司", datafield: "CorpName" },
                  //{ text: "点价人职位", datafield: "Job" },
                  { text: "点价人电话", datafield: "PricingPhone" },
                  { text: "点价申请状态", datafield: "StatusName" },
                  { text: "操作", datafield: "PricingApplyId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            $("#btnSearch").click(function () {
                var applyPerson = $("#ddlApplyPerson").val();
                var subNo = $("#txbSubNo").val();
                var assetId = $("#ddlAssetId").val();
                var status = $("#ddlPricingStatus").val();
                source.url = "Handler/PricingApplyListHandler.ashx?applyPerson=" + applyPerson + "&subNo=" + subNo + "&assetId=" + assetId + "&status=" + status;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">申请人：</span>
                    <div id="ddlApplyPerson" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">子合约号：</span>
                    <input type="text" id="txbSubNo" style="float: left;" />

                    <span style="float: left;">点价品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">点价申请状态：</span>
                    <div id="ddlPricingStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PricingApplyContractList.aspx" id="btnAdd">新增点价申请</a>
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
