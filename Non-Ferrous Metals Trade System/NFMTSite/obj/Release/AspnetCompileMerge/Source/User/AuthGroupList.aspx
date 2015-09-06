<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthGroupList.aspx.cs" Inherits="NFMTSite.User.AuthGroupList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限组列表</title>
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

            //权限组名称
            $("#txbAuthGroupName").jqxInput({ height: 23, width: 120 });

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, searchMode: "containsignorecase" });

            //贸易方向
            var directionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeDirection").val(), async: false };
            var directionDataAdapter = new $.jqx.dataAdapter(directionSource);
            $("#ddlTradeDirection").jqxComboBox({ source: directionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //贸易境区
            var borderSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeBorder").val(), async: false };
            var borderDataAdapter = new $.jqx.dataAdapter(borderSource);
            $("#ddlTradeBorder").jqxComboBox({ source: borderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //内外部
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractInOut").val(), async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlContractInOut").jqxComboBox({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //长单零单
            var limitSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractLimit").val(), async: false };
            var limitDataAdapter = new $.jqx.dataAdapter(limitSource);
            $("#ddlContractLimit").jqxComboBox({ source: limitDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#ddlCorpId").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", height: 25, width: 200
            });

            //权限组状态
            CreateStatusDropDownList("ddlStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });


            var name = $("#txbAuthGroupName").val();
            var assetId = $("#ddlAssetId").val();
            var tradeDirection = $("#ddlTradeDirection").val();
            var tradeBorder = $("#ddlTradeBorder").val();
            var contractInOut = $("#ddlContractInOut").val();
            var contractLimit = $("#ddlContractLimit").val();
            var corpId = $("#ddlCorpId").val();
            var status = $("#ddlStatus").val();
            var url = "Handler/AuthGroupListHandler.ashx?name=" + name + "&assetId=" + assetId + "&tradeDirection=" + tradeDirection + "&tradeBorder=" + tradeBorder + "&contractInOut=" + contractInOut + "&contractLimit=" + contractLimit + "&corpId=" + corpId + "&s=" + status;

            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datatype: "json",
                datafields:
                [
                   { name: "AuthGroupId", type: "int" },
                   { name: "AuthGroupName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "TradeDirection", type: "string" },
                   { name: "TradeBorder", type: "string" },
                   { name: "ContractInOut", type: "string" },
                   { name: "ContractLimit", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "StatusName", type: "string" }
                ],
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
                sortcolumn: "ag.AuthGroupId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ag.AuthGroupId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"AuthGroupDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"AuthGroupUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                columnsresize: true,
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
                      { text: "权限组名称", datafield: "AuthGroupName" },
                      { text: "品种", datafield: "AssetName" },
                      { text: "贸易方向", datafield: "TradeDirection" },
                      { text: "贸易境区", datafield: "TradeBorder" },
                      { text: "内外部", datafield: "ContractInOut" },
                      { text: "长单零单", datafield: "ContractLimit" },
                      { text: "公司", datafield: "CorpName" },
                      { text: "状态", datafield: "StatusName" },
                      { text: "操作", datafield: "AuthGroupId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var name = $("#txbAuthGroupName").val();
                var assetId = $("#ddlAssetId").val();
                var tradeDirection = $("#ddlTradeDirection").val();
                var tradeBorder = $("#ddlTradeBorder").val();
                var contractInOut = $("#ddlContractInOut").val();
                var contractLimit = $("#ddlContractLimit").val();
                var corpId = $("#ddlCorpId").val();
                var status = $("#ddlStatus").val();
                source.url = "Handler/AuthGroupListHandler.ashx?name=" + name + "&assetId=" + assetId + "&tradeDirection=" + tradeDirection + "&tradeBorder=" + tradeBorder + "&contractInOut=" + contractInOut + "&contractLimit=" + contractLimit + "&corpId=" + corpId + "&s=" + status;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div class="SearchExpander">
            <ul>
                <li>
                    <span>权限组名称：</span>
                    <input type="text" id="txbAuthGroupName" />
                </li>
                <li>
                    <span style="float: left;">品种：</span>
                    <div id="ddlAssetId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">贸易方向：</span>
                    <input type="hidden" id="hidTradeDirection" runat="server" />
                    <div id="ddlTradeDirection" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">贸易境区：</span>
                    <input type="hidden" id="hidTradeBorder" runat="server" />
                    <div id="ddlTradeBorder" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">内外部：</span>
                    <input type="hidden" id="hidContractInOut" runat="server" />
                    <div id="ddlContractInOut" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">长零单：</span>
                    <input type="hidden" id="hidContractLimit" runat="server" />
                    <div id="ddlContractLimit" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">公司：</span>
                    <div id="ddlCorpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">权限组状态：</span>
                    <div id="ddlStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="AuthGroupCreate.aspx" id="btnAdd">新增权限组</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxGrid"></div>
        </div>
    </div>
</body>
</html>