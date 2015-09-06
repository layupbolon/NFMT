<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="NFMTSite.User.DepartmentList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部门列表</title>
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

            $("#txbDeptName").jqxInput({ height: 22, width: 120 });

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlDeptStatus");

            //绑定 所属公司
            var url = "Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(ddlsource);
            $("#ddlCorpId").jqxComboBox({ source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 70 });

            var corpId = $("#ddlCorpId").val();
            var deptName = $("#txbDeptName").val();
            var deptStatus = $("#ddlDeptStatus").val();
            var url = "Handler/DeptListHandler.ashx?corpId=" + corpId + "&k=" + deptName + "&s=" + deptStatus;
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
                sortcolumn: "D.DeptId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "D.DeptId";
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
                cellHtml += "<a target=\"_self\" href=\"DeptDetail.aspx?id=" + value + "\">明细</a>";
                if (item.DeptStatus > statusEnum.已作废 && item.DeptStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"DeptUpdate.aspx?id=" + value + "\">修改</a>";
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
                      { text: "所属公司", datafield: "CorpName" },
                      { text: "部门名称", datafield: "DeptName" },
                      { text: "部门全称", datafield: "DeptFullName" },
                      { text: "部门缩写", datafield: "DeptShort" },
                      { text: "部门类型", datafield: "DeptTypeName" },
                      { text: "上级部门名称", datafield: "ParentDeptName" },
                      //{ text: "部门级别", datafield: "DeptLevel" },
                      { text: "部门状态", datafield: "StatusName" },
                      { text: "操作", datafield: "DeptId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var corpId = $("#ddlCorpId").val();
                var deptName = $("#txbDeptName").val();
                var deptStatus = $("#ddlDeptStatus").val();
                source.url = "Handler/DeptListHandler.ashx?corpId=" + corpId + "&k=" + deptName + "&s=" + deptStatus;
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
                    <span style="float: left;">所属公司：</span>
                    <div id="ddlCorpId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">部门名称：</span>
                    <span>
                        <input type="text" id="txbDeptName" /></span>
                </li>

                <li>
                    <span style="float: left;">部门状态：</span>
                    <div id="ddlDeptStatus" style="float: left;" />
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="DeptCreate.aspx" id="btnAdd">新增部门</a>
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
