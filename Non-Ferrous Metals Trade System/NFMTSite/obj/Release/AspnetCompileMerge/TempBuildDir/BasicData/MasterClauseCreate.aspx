<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterClauseCreate.aspx.cs" Inherits="NFMTSite.BasicData.MasterClauseCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约条款分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var masterId = $("#hidMasterId").val();

            $("#jqxMasterExpander").jqxExpander({ width: "98%" });

            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            //现有列表
            var url = "Handler/MasterClauseListHandler.ashx?ih=1&mi=" + masterId;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxgrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "cc.ClauseId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cc.ClauseId";
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
                var data = $('#jqxgrid').jqxGrid('getrowdata', row);
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; text-align: center; margin-right: 2px; margin-left: 4px; \"><input type=\"button\" name=\"btnDelete\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"删除\" />"
                + "&nbsp;&nbsp;&nbsp;<input type=\"button\" onclick=\"ShowWindows(" + value + ",'" + data.ClauseText + "','" + data.ClauseEnText + "')\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"编辑\"/></div>";
                //+ "&nbsp;&nbsp;&nbsp;<a href=\"MasterClauseUpdate.aspx?id=" + value + "&mid=" + masterId + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\">编辑</a></div>";
            }
            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                columnsresize: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                      { text: "条款中文内容", datafield: "ClauseText", width: 700 },
                      { text: "条款英文内容", datafield: "ClauseEnText", width: 300 },
                      { text: "是否默认选中", datafield: "IsChose", columntype: "checkbox" },
                      { text: "显示排序号", datafield: "Sort" },
                      { text: "操作", datafield: "RefId", cellsrenderer: deleteRender, width: 100, enabletooltips: false }
                ]
            }).on("bindingcomplete", function (event) {

                //删除按钮
                var deletes = $("input[name=\"btnDelete\"]");
                for (i = 0; i < deletes.length; i++) {
                    var btnDel = deletes[i];
                    var val = btnDel.id;

                    $(btnDel).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        if (!confirm("确定删除？")) { return; }

                        $.post("Handler/MasterClauseDeleteHandler.ashx", { id: rowId }, function (result) {
                            alert(result);
                            //刷新列表
                            source.url = "Handler/MasterClauseListHandler.ashx?ih=1&mi=" + masterId;
                            $("#jqxgrid").jqxGrid("updatebounddata", "rows");
                            sourceAllot.url = "Handler/MasterClauseListHandler.ashx?ih=0&mi=" + masterId + "&t=" + $("#txbClause").val();
                            $("#jqxgridAdd").jqxGrid("updatebounddata", "rows");
                        });
                    });
                }

            });

            //可分配列表
            $("#jqxAddExpander").jqxExpander({ width: "98%" });
            $("#btnSearchAllot").jqxButton({ height: 25, width: 50 });
            $("#txbClause").jqxInput({ height: 25, width: 120 });

            var caluseText = $("#txbClause").val();
            var urlAllot = "Handler/MasterClauseListHandler.ashx?ih=0&mi=" + masterId + "&t=" + caluseText;
            var sourceAllot = {
                datatype: "json",
                sort: function () {
                    $("#jqxgridAdd").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxgridAdd").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "cc.ClauseId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cc.ClauseId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: urlAllot
            };
            var dataAdapterAllot = new $.jqx.dataAdapter(sourceAllot, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnAdd\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" />";
            }
            $("#jqxgridAdd").jqxGrid(
            {
                width: "98%",
                source: dataAdapterAllot,
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
                      { text: "条款中文内容", datafield: "ClauseText", width: 700 },
                      { text: "条款英文内容", datafield: "ClauseEnText" },
                      { text: "操作", datafield: "ClauseId", cellsrenderer: addRender, width: 100, enabletooltips: false }
                ]
            }).on("bindingcomplete", function (event) {
                var adds = $("input[name=\"btnAdd\"]");

                for (i = 0; i < adds.length; i++) {
                    var btnCreate = adds[i];
                    var val = btnCreate.id;

                    $(btnCreate).click({ value: val }, function (event) {
                        var rowId = event.data.value;

                        if (!confirm("确定添加至该模板？")) { return; }

                        $.post("Handler/MasterCluaseAddHandler.ashx", { id: rowId, mid: masterId }, function (result) {
                            alert(result);
                            //刷新列表
                            source.url = "Handler/MasterClauseListHandler.ashx?ih=1&mi=" + masterId;
                            $("#jqxgrid").jqxGrid("updatebounddata", "rows");
                            sourceAllot.url = "Handler/MasterClauseListHandler.ashx?ih=0&mi=" + masterId + "&t=" + $("#txbClause").val();
                            $("#jqxgridAdd").jqxGrid("updatebounddata", "rows");
                        });
                    });
                }
            });

            $("#popupWindow").jqxWindow({ width: 800, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01 });
            $("#spClauseTextWin").jqxInput({ height: 25, width: 500, disabled: true });
            $("#spClauseEnTextWin").jqxInput({ height: 25, width: 120, disabled: true });
            $("#chkIsChoseWin").jqxCheckBox();
            $("#txtSortWin").jqxInput({ height: 25, width: 120 });

            $("#Cancel").jqxButton({ height: 25, width: 100 });
            $("#Save").jqxButton({ height: 25, width: 100 });

            $("#Save").click(function () {
                var refId = $("#hidrefId").val();
                var isChose = $("#chkIsChoseWin").jqxCheckBox("val");
                var sort = $("#txtSortWin").val();

                $.post("Handler/MasterClauseUpdateHandler.ashx", { id: refId, ic: isChose, s: sort },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            source.url = "Handler/MasterClauseListHandler.ashx?ih=1&mi=" + masterId;
                            $("#jqxgrid").jqxGrid("updatebounddata", "rows");
                            $("#popupWindow").jqxWindow("hide");
                        }
                        alert(obj.Message);
                    }
                );
            });

            //搜索
            $("#btnSearchEmp").click(function () {
                var searchUrl = "Handler/MasterClauseListHandler.ashx?ih=0&mi=" + masterId + "&t=" + $("#txbClause").val();
                sourceAllot.url = searchUrl;
                $("#jqxgridAdd").jqxGrid("updatebounddata", "rows");
            });

        });

        function ShowWindows(refId, ClauseText, ClauseEnText) {
            $("#hidrefId").val(refId);

            $.get("Handler/MasterCluaseGetHandler.ashx?refId=" + refId, function (data, status) {
                var obj = JSON.parse(data);
                if (obj.ResultStatus.toString() == "0") {
                    $("#spClauseTextWin").val(ClauseText);
                    $("#spClauseEnTextWin").val(ClauseEnText);
                    if (obj.ReturnValue.IsChose) {
                        $("#chkIsChoseWin").jqxCheckBox("check");
                    } else {
                        $("#chkIsChoseWin").jqxCheckBox("uncheck");
                    }
                    $("#txtSortWin").val(obj.ReturnValue.Sort);

                    $("#popupWindow").jqxWindow("show");
                }
                else {
                    alert(obj.Message);
                }
            });
        }
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxMasterExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约模板信息
        </div>
        <div class="SearchExpander">
            <ul>
                <li style="float: left; width: 300px;">
                    <span style="font-weight: 600; color: yellowgreen;">模板名称：</span>
                    <span runat="server" id="spnMasterName"></span>
                </li>
                <li style="float: none;">
                    <span style="font-weight: 600; color: yellowgreen;">模板英文名称：</span>
                    <span runat="server" id="spnMasterEname"></span>
                </li>
                <li style="float: left; width: 300px;">
                    <span style="font-weight: 600; color: yellowgreen;">模板状态：</span>
                    <span runat="server" id="spnMasterStatus"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            模板现有条款列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAddExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可增加条款列表
        </div>
        <div>
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span>条款内容</span>
                        <span>
                            <input type="text" id="txbClause" /></span>
                    </li>
                    <li>
                        <input type="button" id="btnSearchAllot" value="查询" />
                    </li>
                </ul>
            </div>
            <div id="jqxgridAdd" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <input type="hidden" id="hidMasterId" runat="server" />

    <div id="popupWindow">
        <div>编辑</div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <h4><span>条款中文内容：</span></h4>
                    <input type="text" id="spClauseTextWin" />
                </li>
                <li>
                    <h4><span>条款英文内容：</span></h4>
                    <input type="text" id="spClauseEnTextWin" />
                </li>
                <li>
                    <h4><span>是否默认选中：</span></h4>
                    <div id="chkIsChoseWin" />
                </li>
                <li>
                    <h4><span>排序号：</span></h4>
                    <input type="text" id="txtSortWin" />
                </li>
                <li>
                    <input type="hidden" id="hidrefId" />
                    <input style="margin-right: 5px;" type="button" id="Save" value="确认" />
                    <input id="Cancel" type="button" value="取消" />
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
