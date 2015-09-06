<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptEmpAllot.aspx.cs" Inherits="NFMTSite.User.DeptEmpAllot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部门员工分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txbDeptName").jqxInput({ height: 25, width: 120 });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnSearch").click(function () {
                var deptName = $("#txbDeptName").val();
                source.url = "Handler/DeptListHandler.ashx?k=" + deptName + "&s=" + statusEnum.已生效;
                $("#jqxGrid").jqxGrid("updatebounddata", "cells");
            });

            $("#jqxDataExpander").jqxExpander({ width: "45%" });

            var deptName = $("#txbDeptName").val();
            var url = "Handler/DeptListHandler.ashx?k=" + deptName + "&s=" + statusEnum.已生效;
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
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<span style=\"padding-left:15px;\"><input type=\"radio\" value=\"" + row + "\" name=\"radDept\" /></span>";
            }

            var deptId = 0;

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
                  { text: "选择部门", datafield: "DeptId", cellsrenderer: cellsrenderer, width: 50, enabletooltips: false },
                  { text: "所属公司", datafield: "CorpName" },
                  { text: "部门名称", datafield: "DeptName" },
                  { text: "部门类型", datafield: "DeptType" }
                ]
            }).on("bindingcomplete", function (event) {
                var rads = $("input[name=\"radDept\"]");
                for (i = 0; i < rads.length; i++) {
                    var rad = $(rads[i]);
                    var val = rad.val();
                    rad.click({ value: val }, function (event) {

                        var rowIndex = event.data.value;
                        //获取行数据
                        var item = $("#jqxgrid").jqxGrid("getrowdata", rowIndex);
                        //设置隐藏控件DeptId
                        //$("#hidDeptId").attr("value", item.DeptId);                        
                        deptId = item.DeptId;

                        //设置选择部门
                        $("#liDeptName").html(item.CorpName + "<br/>" + item.DeptName);
                    });
                }
            });

            $("#jqxAllotExpander").jqxExpander({ width: "20%" });

            $("#jqxEmpExpander").jqxExpander({ width: "45%" });
            $("#btnSearchEmp").jqxButton({ height: 25, width: 50 });
            $("#txbEmp").jqxInput({ height: 25, width: 120 });

            $("#btnAllot").jqxButton({ height: 30, width: 120 });

            var empName = $("#txbEmp").val();
            var urlEmp = "Handler/EmpListHandler.ashx?name=" + empName;
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
                sortcolumn: "E.EmpId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "E.EmpId";
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

            var empIds = new Array();

            $("#jqxgridEmp").jqxGrid(
            {
                width: "98%",
                source: dataAdapterEmp,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                selectionmode: "checkbox",
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                      { text: "员工编号", datafield: "EmpCode" },
                      { text: "姓名", datafield: "Name" },
                      { text: "性别", datafield: "Sex" },
                      { text: "在职状态", datafield: "DetailName" }
                ]
            }).on("rowselect", function (event) {
                var args = event.args;

                //row对象不存在，则为全选或全不选
                if (args.row != undefined) {

                    //设置隐藏控件的值
                    //var empList = $("#hidEmpList").val();
                    //if (empList.length > 0) { empList += ","; }
                    //empList += args.row.EmpId;
                    //$("#hidEmpList").val(empList);
                    empIds[empIds.length] = args.row.EmpId;

                    $("#liEmpList").after("<li id=\"liEmp" + args.row.EmpId + "\" style=\"margin-left: 30px; line-height: 30px; height: 30px;\">" + args.row.Name + "</li>");
                }

                //全选或全不选
                if (args.rowindex.length != undefined) {
                    var rows = $("#jqxgridEmp").jqxGrid("getrows");
                    if (args.rowindex[0] == -1) {
                        for (i = 0; i < rows.length; i++) {
                            var liId = "#liEmp" + rows[i].EmpId;
                            if ($(liId) != undefined) {
                                $(liId).remove();
                            }
                        }

                        empIds.length = 0;
                    }
                    else {
                        for (i = 0; i < rows.length; i++) {
                            var item = rows[i];

                            //设置隐藏控件的值
                            //var empList = $("#hidEmpList").val();
                            //if (empList.length > 0) { empList += ","; }
                            //empList += item.EmpId;
                            //$("#hidEmpList").val(empList);
                            empIds[empIds.length] = args.row.EmpId;

                            $("#liEmpList").after("<li id=\"liEmp" + item.EmpId + "\" style=\"margin-left: 30px; line-height: 30px; height: 30px;\">" + item.Name + "</li>");
                        }
                    }
                }
            }).on("rowunselect", function (event) {
                var args = event.args;
                if (args.row != undefined) {
                    var liId = "#liEmp" + args.row.EmpId;
                    if ($(liId) != undefined) {
                        $(liId).remove();

                        var index = empIds.indexOf(args.row.EmpId);
                        empIds.splice(index, 1);
                    }
                }
            });


            $("#btnAllot").click(function () {
                //var deptId = $("#hidDeptId").val();
                //var empIds = $("#hidEmpList").val();
                alert(deptId);
                if (deptId == 0) { alert("必须选择部门"); return; }
                if (empIds.length == 0) { alert("必须选择员工"); return; }

                var eids = "";
                for (i = 0; i < empIds.length; i++) {
                    if (i != 0) { eids += "|"; }
                    eids += empIds[i];
                }

                var allotUrl = "Handler/DeptEmpAllotHandler.ashx";
                $.post(allotUrl, { did: deptId, eids: eids }, function (result) {
                    alert(result);
                });

            });

        });
    </script>
</head>
<body>
    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            部门列表
        </div>
        <div style="height: 500px;">
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span>部门名称</span>
                        <span>
                            <input type="text" id="txbDeptName" /></span>
                    </li>
                    <li>
                        <input type="button" id="btnSearch" value="查询" />
                    </li>
                </ul>
            </div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxEmpExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            员工列表
        </div>
        <div style="height: 500px;">
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span>员工名称</span>
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

    <div id="jqxAllotExpander" style="position: absolute; left: 62%; margin: 0px 5px 5px 0px;">
        <div>
            分配情况          
        </div>
        <div>
            <ul style="list-style-type: none;">
                <li style="margin-left: 30px;">
                    <input type="button" id="btnAllot" value="分配" /></li>
                <li>
                    <h3 style="color: yellowgreen;">选中部门：</h3>
                </li>
                <li style="margin-left: 30px; line-height: 25px; height: 50px;" id="liDeptName">
                    <input type="hidden" id="hidDeptId" />
                </li>
                <li id="liEmpList">
                    <h3 style="color: yellowgreen;">选中员工：</h3>
                    <input type="hidden" id="hidEmpList" />
                </li>
                <%--<li style="margin-left: 30px; line-height: 30px; height: 30px;">张三</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">李四</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">王五</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">陈六</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">陈六</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">陈六</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">陈六</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">陈六</li>
                <li style="margin-left: 30px; line-height: 30px; height: 30px;">陈六</li>--%>
            </ul>
        </div>
    </div>
</body>
</html>
