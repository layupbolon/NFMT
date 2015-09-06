<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpRoleDetail.aspx.cs" Inherits="NFMTSite.User.EmpRoleDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色员工分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            var roleId = $("#hidId").val();
            

            ////////////////////角色信息////////////////////
            $("#jqxRoleExpander").jqxExpander({ width: "98%" });

            ////////////////////角色现有员工列表////////////////////
            $("#jqxNowAtExpander").jqxExpander({ width: "98%" });
            //绑定现有员工列表
            var url = "Handler/EmpRoleListHandler.ashx?type=0&id=" + roleId;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "sort");
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
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var deleteRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnDelete\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"删除\" />";
            }
            $("#jqxgrid").jqxGrid(
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
                      { text: "员工编号", datafield: "EmpCode" },
                      { text: "姓名", datafield: "Name" },
                      { text: "性别", datafield: "Sex" },
                      { text: "生日", datafield: "BirthDay" },
                      { text: "手机号码", datafield: "Telephone" },
                      { text: "座机号码", datafield: "Phone" },
                      { text: "在职状态", datafield: "StatusName" },
                      { text: "操作", datafield: "EmpRoleId", cellsrenderer: deleteRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var deletes = $("input[name=\"btnDelete\"]");
                for (i = 0; i < deletes.length; i++) {
                    var btnDel = deletes[i];
                    var val = btnDel.id;

                    $(btnDel).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        if (!confirm("确定删除？")) { return; }

                        $.post("Handler/EmpRoleDeleteHandler.ashx", { id: rowId }, function (result) {
                            alert(result);
                            //刷新列表
                            source.url = "Handler/EmpRoleListHandler.ashx?type=0&id=" + roleId;
                            $("#jqxgrid").jqxGrid("updatebounddata", "rows");
                            sourceEmp.url = "Handler/EmpRoleListHandler.ashx?type=1&id=" + roleId + "&name=" + $("#txbEmp").val();
                            $("#jqxgridEmp").jqxGrid("updatebounddata", "rows");
                        });
                    });
                }

            });


            ////////////////////可分配员工列表////////////////////
            $("#jqxEmpExpander").jqxExpander({ width: "98%" });
            $("#btnSearchEmp").jqxButton({ height: 25, width: 50 });
            $("#txbEmp").jqxInput({ height: 25, width: 120 });

            var empName = $("#txbEmp").val();
            var urlEmp = "Handler/EmpRoleListHandler.ashx?type=1&id=" + roleId + "&name=" + empName;
            var sourceEmp = {
                datatype: "json",
                sort: function () {
                    $("#jqxgridEmp").jqxGrid("updatebounddata", "sort");
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
                url: urlEmp
            };
            var dataAdapterEmp = new $.jqx.dataAdapter(sourceEmp, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnAdd\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" />";
            }
            $("#jqxgridEmp").jqxGrid(
            {
                width: "98%",
                source: dataAdapterEmp,
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
                      { text: "员工编号", datafield: "EmpCode" },
                      { text: "姓名", datafield: "Name" },
                      { text: "性别", datafield: "Sex" },
                      { text: "生日", datafield: "BirthDay" },
                      { text: "手机号码", datafield: "Telephone" },
                      { text: "座机号码", datafield: "Phone" },
                      { text: "在职状态", datafield: "StatusName" },
                      { text: "操作", datafield: "EmpId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var adds = $("input[name=\"btnAdd\"]");

                for (i = 0; i < adds.length; i++) {
                    var btnCreate = adds[i];
                    var val = btnCreate.id;

                    $(btnCreate).click({ value: val }, function (event) {
                        var rowId = event.data.value;

                        if (!confirm("确定添加至该角色？")) { return; }

                        $.post("Handler/EmpRoleAddHandler.ashx", { id: rowId, did: roleId }, function (result) {
                            alert(result);
                            //刷新列表
                            source.url = "Handler/EmpRoleListHandler.ashx?type=0&id=" + roleId;
                            $("#jqxgrid").jqxGrid("updatebounddata", "rows");
                            sourceEmp.url = "Handler/EmpRoleListHandler.ashx?type=1&id=" + roleId + "&name=" + $("#txbEmp").val();
                            $("#jqxgridEmp").jqxGrid("updatebounddata", "rows");
                        });
                    });
                }
            });

            //搜索
            $("#btnSearchEmp").click(function () {
                var searchUrl = "Handler/EmpRoleListHandler.ashx?type=1&id=" + roleId + "&name=" + $("#txbEmp").val();
                sourceEmp.url = searchUrl;
                $("#jqxgridEmp").jqxGrid("updatebounddata", "rows");
            });

        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxRoleExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            角色信息
        </div>
        <div class="SearchExpander">
            <ul>
                <li style="float: left; width: 300px;">
                    <span style="font-weight: 600; color: yellowgreen;">角色名称：</span>
                    <input type="hidden" runat="server" id="hidId" />
                    <span id="txbRoleName" runat="server" /></li>
            </ul>
        </div>
    </div>

    <div id="jqxNowAtExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            角色现有员工列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxEmpExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可分配员工列表
        </div>
        <div>
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span>员工姓名：</span>
                        <span>
                            <input type="text" id="txbEmp" /></span>
                    </li>
                    <li>
                        <input type="button" id="btnSearchEmp" value="查询" />
                    </li>
                </ul>
            </div>
            <div id="jqxgridEmp" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <input type="hidden" id="hidDeptId" runat="server" />
</body>
</html>
