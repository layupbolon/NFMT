<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverPlaceList.aspx.cs" Inherits="NFMTSite.BasicData.DeliverPlaceList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交货地列表</title>
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

            //地区 
            var cySource = { datatype: "json", url: "Handler/AreaDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlDPArea").jqxComboBox({ source: cyDataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 100, height: 25, autoDropDownHeight: true });

            //仓储/码头公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlDPCompany").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            $("#txbDPName").jqxInput({ height: 23, width: 120 });

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlDPStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 80 });

            var areaId = $("#ddlDPArea").val();
            var corpId = $("#ddlDPCompany").val();
            var dPName = $("#txbDPName").val();
            var status = $("#ddlDPStatus").val();
            var url = "Handler/DeliverPlaceListHandler.ashx?a=" + areaId + "&c=" + corpId + "&d=" + dPName + "&s=" + status;
            //var url = "Handler/DeliverPlaceListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datatype: "json",
                datafields:
                [
                   { name: "DPId", type: "int" },
                   { name: "DetailName", type: "string" },
                   { name: "AreaName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "DPFullName", type: "string" },
                   { name: "DPAddress", type: "string" },
                   { name: "DPEAddress", type: "string" },
                   { name: "DPTel", type: "string" },
                   { name: "DPContact", type: "string" },
                   { name: "DPFax", type: "string" },
                   { name: "DPStatus", type: "int" },
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
                sortcolumn: "dp.DPId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "dp.DPId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"DeliverPlaceDetail.aspx?id=" + value + "\">明细</a>";
                if (item.DPStatus > statusEnum.已作废 && item.DPStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"DeliverPlaceUpdate.aspx?id=" + value + "\">修改</a>";
                }
                else {
                    cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
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
                      { text: "交货地类型", datafield: "DetailName" },
                      { text: "交货地地区", datafield: "AreaName" },
                      { text: "仓储/码头公司", datafield: "CorpName" },
                      { text: "交货地名称", datafield: "DPName" },
                      { text: "交货地全称", datafield: "DPFullName" },
                      { text: "交货地地址", datafield: "DPAddress" },
                      { text: "交货地英文地址", datafield: "DPEAddress" },
                      { text: "交货地电话", datafield: "DPTel" },
                      { text: "交货地联系人", datafield: "DPContact" },
                      { text: "交货地传真", datafield: "DPFax" },
                      { text: "交货地状态", datafield: "StatusName" },
                      { text: "操作", datafield: "DPId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var areaId = $("#ddlDPArea").val();
                var corpId = $("#ddlDPCompany").val();
                var dPName = $("#txbDPName").val();
                var status = $("#ddlDPStatus").val();
                source.url = "Handler/DeliverPlaceListHandler.ashx?a=" + areaId + "&c=" + corpId + "&d=" + dPName + "&s=" + status;
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
                    <span style="float: left;">交货地地区：</span>
                    <div id="ddlDPArea" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">仓储/码头公司：</span>
                    <div id="ddlDPCompany" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">交货地名称：</span>
                    <input type="text" id="txbDPName" />
                </li>
                <li>
                    <span style="float: left;">交货地状态：</span>
                    <div id="ddlDPStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="DeliverPlaceCreate.aspx" id="btnAdd">新增交货地</a>
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
