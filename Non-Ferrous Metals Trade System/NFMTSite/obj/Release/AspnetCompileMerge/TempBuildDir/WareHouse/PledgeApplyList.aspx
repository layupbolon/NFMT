<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PledgeApplyList.aspx.cs" Inherits="NFMTSite.WareHouse.PledgeApplyList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押申请列表</title>
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

            //质押申请状态
            CreateStatusDropDownList("ddlPledgeStatus");

            //申请人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlApplyPerson").jqxComboBox({ selectedIndex: 0, source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25, autoDropDownHeight: true });
            var item = { EmpId: 0, Name: " " };
            $("#ddlApplyPerson").jqxComboBox("insertAt", item, 0);

            //时间控件
            $("#txbFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#txbFromCreateDate").jqxDateTimeInput({ value: tempDate });

            $("#txbToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var url = "Handler/PledgeApplyListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "PledgeApplyId", type: "int" },
                   { name: "BankName", type: "string" },
                   { name: "ApplyNo", type: "string" },
                   { name: "ApplyTime", type: "date" },
                   { name: "StatusName", type: "string" },
                   { name: "Name", type: "string" },
                   { name: "DeptName", type: "string" },
                   { name: "ExecAmount", type: "string" },
                   { name: "ApplyStatus", type: "int" },
                   { name: "ApplyDesc", type: "string" }
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
                sortcolumn: "pa.PledgeApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pa.PledgeApplyId";
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

                cellHtml += "<a target=\"_self\" href=\"PledgeApplyDetail.aspx?id=" + value + "\">查看</a>"

                if (item.ApplyStatus > statusEnum.已关闭 && item.ApplyStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PledgeApplyUpdate.aspx?id=" + value + "\">修改</a>"
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
                  { text: "申请编号", datafield: "ApplyNo" },
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请人", datafield: "Name" },
                  { text: "申请部门", datafield: "DeptName" },
                  { text: "申请附言", datafield: "ApplyDesc" },
                  { text: "质押银行名称", datafield: "BankName" },
                  { text: "执行数量", datafield: "ExecAmount" },
                  { text: "申请状态", datafield: "StatusName" },
                  { text: "操作", datafield: "PledgeApplyId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var applyPerson = $("#ddlApplyPerson").val();
                var status = $("#ddlPledgeStatus").val();
                var fromDate = $("#txbFromCreateDate").val();
                var toDate = $("#txbToCreateDate").val();
                source.url = "Handler/PledgeApplyListHandler.ashx?a=" + applyPerson + "&s=" + status + "&f=" + fromDate + "&t=" + toDate;
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
                    <span style="float: left;">质押申请状态：</span>
                    <div id="ddlPledgeStatus" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">申请日期：</span>
                    <div id="txbFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="txbToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="PledgeApplyCreate.aspx" id="btnAdd">新增质押申请</a>
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
