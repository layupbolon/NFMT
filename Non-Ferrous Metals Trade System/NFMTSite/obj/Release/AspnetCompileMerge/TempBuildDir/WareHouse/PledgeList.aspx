<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PledgeList.aspx.cs" Inherits="NFMTSite.WareHouse.PledgeList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押</title>
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

            //质押确认人
            var ddlPledgeurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPledgesource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPledgeurl, async: false };
            var ddlPledgedataAdapter = new $.jqx.dataAdapter(ddlPledgesource);
            $("#ddlPledge").jqxComboBox({ source: ddlPledgedataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25, searchMode: "containsignorecase" });

            //质押状态
            CreateStatusDropDownList("ddlPledgeStatus");

            //质押银行
            var ddlPledgeBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlPledgeBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlPledgeBankurl, async: false };
            var ddlPledgeBankdataAdapter = new $.jqx.dataAdapter(ddlPledgeBanksource);
            $("#ddlPledgeBank").jqxComboBox({ source: ddlPledgeBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25, searchMode: "containsignorecase" });

            //质押部门
            var ddlDeptIdurl = "../User/Handler/DeptDDLHandler.ashx?"
            var ddlDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlDeptIdurl, async: false };
            var ddlDeptIddataAdapter = new $.jqx.dataAdapter(ddlDeptIdsource);
            $("#ddlPledgeDept").jqxComboBox({ source: ddlDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, searchMode: "containsignorecase" });

            //按钮
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 90 });

            //绑定Grid
            var url = "Handler/PledgeListHandler.ashx";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "PledgeId", type: "int" },
                   { name: "Name", type: "string" },
                   { name: "PledgeTime", type: "date" },
                   { name: "BankName", type: "string" },
                   { name: "Memo", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "PledgeStatus", type: "int" }
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
                sortcolumn: "p.PledgeId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "p.PledgeId";
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

                cellHtml += "<a target=\"_self\" href=\"PledgeDetail.aspx?id=" + value + "\">查看</a>"

                if (item.PledgeStatus > statusEnum.已关闭 && item.PledgeStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"PledgeUpdate.aspx?id=" + value + "\">修改</a>"
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
                  { text: "质押人", datafield: "Name" },
                  { text: "质押时间", datafield: "PledgeTime", cellsformat: "yyyy-MM-dd" },
                  { text: "质押银行", datafield: "BankName" },
                  { text: "质押附言", datafield: "Memo" },
                  { text: "质押状态", datafield: "StatusName" },
                  { text: "操作", datafield: "PledgeId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            $("#btnSearch").click(function () {
                var pledge = $("#ddlPledge").val();
                var status = $("#ddlPledgeStatus").val();
                var bank = $("#ddlPledgeBank").val();
                var dept = $("#ddlPledgeDept").val();
                source.url = "Handler/PledgeListHandler.ashx?p=" + pledge + "&s=" + status + "&b=" + bank + "&d=" + dept;
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
                    <span style="float: left;">质押确认人：</span>
                    <div id="ddlPledge" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">质押状态：</span>
                    <div id="ddlPledgeStatus" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">质押银行：</span>
                    <div id="ddlPledgeBank" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">质押部门：</span>
                    <div id="ddlPledgeDept" style="float: left;"></div>
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="CanPledgeApplyList.aspx" id="btnAdd">质押</a>
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
