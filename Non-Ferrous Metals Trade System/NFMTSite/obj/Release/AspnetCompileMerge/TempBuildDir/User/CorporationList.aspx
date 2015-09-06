<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorporationList.aspx.cs" Inherits="NFMTSite.User.CorporationList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业列表</title>
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

            $("#CorpName").jqxInput({ height: 22, width: 120 });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 100 });
            $("#btnAddCustomer").jqxLinkButton({ height: 25, width: 100 });

            //绑定 所属集团
            var url = "Handler/BlocDDLHandler.ashx";
            var ddlsource = { datatype: "json", datafields: [{ name: "BlocId" }, { name: "BlocName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(ddlsource);
            $("#BlocId").jqxComboBox({ source: dataAdapter, displayMember: "BlocName", valueMember: "BlocId", width: 100, height: 25, autoDropDownHeight: true });

            //状态下拉列表绑定
            CreateStatusDropDownList("CorpStatus");

            var BlocId = $("#BlocId").val();
            var CorpCode = "";
            var CorpName = $("#CorpName").val();
            var CorpStatus = $("#CorpStatus").val();
            url = "Handler/CorpListHandler.ashx?Code=" + CorpCode + "&Name=" + CorpName + "&s=" + CorpStatus + "&blocId=" + BlocId;
            var formatedData = "";
            var totalrecords = 0;

            var source = {
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
                sortcolumn: "C.CorpId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "C.CorpId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };

            dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                if (item.IsSelf == '己方公司') {
                    cellHtml += "<a target=\"_self\" href=\"CorpDetail.aspx?id=" + value + "\">明细</a>";
                }
                else if (item.IsSelf == '非己方公司') {
                    cellHtml += "<a target=\"_self\" href=\"CorpCustomerDetail.aspx?id=" + value + "\">明细</a>";
                }

                if (item.CorpStatus > statusEnum.已作废 && item.CorpStatus < statusEnum.待审核) {
                    if (item.IsSelf == '己方公司') {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CorpUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else if (item.IsSelf == '非己方公司') {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"CorpCustomerUpdate.aspx?id=" + value + "\">修改</a>";
                    }
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
                      { text: "所属集团", datafield: "BlocName" },
                      { text: "公司名称", datafield: "CorpName" },
                      { text: "公司英文名称", datafield: "CorpEName" },
                      { text: "纳税人识别号", datafield: "TaxPayerId" },
                      { text: "公司全称", datafield: "CorpFullName" },
                      { text: "公司英文全称", datafield: "CorpFullEName" },
                      { text: "公司地址", datafield: "CorpAddress" },
                      { text: "公司类型", datafield: "DetailName" },
                      { text: "是否己方公司", datafield: "IsSelf" },
                      { text: "公司状态", datafield: "StatusName" },
                      { text: "操作", datafield: "CorpId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var BlocId = $("#BlocId").val();
                var CorpCode = "";
                var CorpName = $("#CorpName").val();
                var CorpStatus = $("#CorpStatus").val();
                source.url = "Handler/CorpListHandler.ashx?Code=" + CorpCode + "&Name=" + CorpName + "&s=" + CorpStatus + "&blocId=" + BlocId;
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
        <div id="SearchDiv">
            <ul>
                <li>
                    <span style="float: left;">所属集团：</span>
                    <div id="BlocId" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">公司名称：</span>
                    <span>
                        <input type="text" id="CorpName" /></span>
                </li>
                <li>
                    <span style="float: left;">公司状态：</span>
                    <div id="CorpStatus" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="CorpCreate.aspx" id="btnAdd">新增内部公司</a>
                    <a target="_self" runat="server" href="CorpCustomerCreate.aspx" id="btnAddCustomer">新增客户</a>
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
