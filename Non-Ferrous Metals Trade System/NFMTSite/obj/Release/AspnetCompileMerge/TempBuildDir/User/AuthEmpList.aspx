<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthEmpList.aspx.cs" Inherits="NFMTSite.User.AuthGroupAllot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限组分配-员工列表</title>
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

            $("#txbEmpCode").jqxInput({ height: 22, width: 120 });
            $("#txbName").jqxInput({ height: 22, width: 120 });

            //绑定 员工状态
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "Handler/WorkStatusHandler.ashx", async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlEmpStatus").jqxDropDownList({ source: dataAdapter, displayMember: "StatusName", valueMember: "DetailId", selectedIndex: 0, width: 100, height: 25, autoDropDownHeight: true });
            var obj = { StatusName: "全部", DetailId: 0 };
            $("#ddlEmpStatus").jqxDropDownList("insertAt", obj, 0);

            $("#btnSearch").jqxButton({ height: 25, width: 70 });

            var empCode = $("#txbEmpCode").val();
            var name = $("#txbName").val();
            var workStatus = $("#ddlEmpStatus").val();
            var url = "Handler/EmpListExceptHandler.ashx?empCode=" + empCode + "&name=" + name + "&workStatus=" + workStatus + "&excepteEmpId=" + "<%=curUserId%>";
            var formatedData = "";
            var totalrecords;
            totalrecords = 0;

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
                sortcolumn: "emp.EmpId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "emp.EmpId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var cellsrenderer = function (row, columnfield, value) {
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"AuthGroupAllotDetail.aspx?id=" + value + "\">选择员工</a>";
                cellHtml += "</div>";
                return cellHtml;
            }
            dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var gridLocalization = null;

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
                ready: function () {
                    gridLocalization = $('#jqxGrid').jqxGrid('gridlocalization');
                },
                columns: [
                      { text: "所属公司", datafield: "CorpName" },
                      { text: "所属部门", datafield: "DeptName" },
                      { text: "员工编号", datafield: "EmpCode" },
                      { text: "姓名", datafield: "Name" },
                      { text: "性别", datafield: "Sex" },
                      {
                          text: "生日", datafield: "BirthDay", cellsformat: 'yyyy-MM-dd'
                      },
                      { text: "手机号码", datafield: "Telephone" },
                      { text: "座机号码", datafield: "Phone" },
                      { text: "在职状态", datafield: "StatusName" },
                      { text: "操作", datafield: "EmpId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });


            $("#btnSearch").click(function () {
                var empCode = $("#txbEmpCode").val();
                var name = $("#txbName").val();
                var workStatus = $("#ddlEmpStatus").val();
                source.url = "Handler/EmpListExceptHandler.ashx?empCode=" + empCode + "&name=" + name + "&workStatus=" + workStatus + "&excepteEmpId=" + "<%=this.curUserId%>";
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
                    <span style="float: left;">员工编号：</span>
                    <span>
                        <input type="text" id="txbEmpCode" /></span>
                </li>
                <li>
                    <span style="float: left;">姓名：</span>
                    <span>
                        <input type="text" id="txbName" /></span>
                </li>
                <li>
                    <input type="hidden" id="hidBDStyleId" runat="server" />
                    <span style="float: left;">员工状态：</span>
                    <div id="ddlEmpStatus" style="float: left;"></div>
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
        <div style="height: 500px;">
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>