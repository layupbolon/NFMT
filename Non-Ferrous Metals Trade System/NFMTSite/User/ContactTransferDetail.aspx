<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactTransferDetail.aspx.cs" Inherits="NFMTSite.User.ContactTransferDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>联系人转移明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            

            $("#jqxEmpExpander").jqxExpander({ width: "75%" });
            $("#jqxTransferExpander").jqxExpander({ width: "20%" });
            $("#jqxContactExpander").jqxExpander({ width: "75%" });
            $("#btnTransfer").jqxButton({ height: 30, width: 120 });

            var outEmpId = $("#hidOutEmpId").val();

            var contactIds = new Array();
            var ECIDs = new Array();
            var empId = 0;

            ///////////////////////员工列表///////////////////////

            $("#txbEmpCode").jqxInput({ height: 23, width: 120 });
            $("#txbEmpName").jqxInput({ height: 23, width: 120 });

            //绑定 员工状态
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "Handler/WorkStatusHandler.ashx", async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlEmpStatus").jqxDropDownList({ source: dataAdapter, displayMember: "StatusName", valueMember: "DetailId", selectedIndex: 0, width: 100, height: 25, autoDropDownHeight: true });

            $("#btnEmpSearch").jqxButton({ height: 25, width: 70 });

            var empCode = $("#txbEmpCode").val();
            var name = $("#txbEmpName").val();
            var workStatus = $("#ddlEmpStatus").val();
            var url = "Handler/EmpListExceptHandler.ashx?empCode=" + empCode + "&name=" + name + "&workStatus=" + workStatus + "&excepteEmpId=" + outEmpId;
            var formatedData = "";
            var totalrecords = 0;

            var empsource = {
                datatype: "json",
                sort: function () {
                    $("#jqxEmpGrid").jqxGrid("updatebounddata", "sort");
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

            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<span style=\"padding-left:15px;\"><input type=\"radio\" value=\"" + row + "\" name=\"radDept\" /></span>";
            }

            var dataAdapter = new $.jqx.dataAdapter(empsource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var gridLocalization = null;

            $("#jqxEmpGrid").jqxGrid(
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
                    gridLocalization = $('#jqxEmpGrid').jqxGrid('gridlocalization');
                },
                columns: [
                      { text: "选择员工", datafield: "EmpId", cellsrenderer: cellsrenderer, width: 66, enabletooltips: false },
                      { text: "所属公司", datafield: "CorpName" },
                      { text: "所属部门", datafield: "DeptName" },
                      { text: "员工编号", datafield: "EmpCode" },
                      { text: "姓名", datafield: "Name" },
                      { text: "性别", datafield: "Sex" },
                      { text: "生日", datafield: "BirthDay", cellsformat: 'yyyy-MM-dd' },
                      { text: "手机号码", datafield: "Telephone" },
                      { text: "座机号码", datafield: "Phone" },
                      { text: "在职状态", datafield: "StatusName" }
                      //,{ text: "操作", datafield: "EmpId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            }).on("bindingcomplete", function (event) {
                var rads = $("input[name=\"radDept\"]");
                for (i = 0; i < rads.length; i++) {
                    var rad = $(rads[i]);
                    var val = rad.val();
                    rad.click({ value: val }, function (event) {

                        var rowIndex = event.data.value;
                        //获取行数据
                        var item = $("#jqxEmpGrid").jqxGrid("getrowdata", rowIndex);
                        //设置隐藏控件DeptId
                        //$("#hidDeptId").attr("value", item.DeptId);                        
                        empId = item.EmpId;

                        $("#liEmpName").html(item.BlocName + "<br/>" + item.Name);
                    });
                }
            });

            $("#btnEmpSearch").click(function () {
                var empCode = $("#txbEmpCode").val();
                var name = $("#txbEmpName").val();
                var workStatus = $("#ddlEmpStatus").val();
                empsource.url = "Handler/EmpListExceptHandler.ashx?empCode=" + empCode + "&name=" + name + "&workStatus=" + workStatus + "&excepteEmpId=" + outEmpId;
                $("#jqxEmpGrid").jqxGrid("updatebounddata", "rows");
            });


            ///////////////////////联系人列表///////////////////////

            $("#txbContactName").jqxInput({ height: 25, width: 120 });

            //绑定 联系人公司
            var url = "Handler/CorpDDLHandler.ashx?isSelf=1";
            var source = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlCorpId").jqxComboBox({ source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            $("#btnContactSearch").jqxButton({ height: 25, width: 50 });

            var contactName = $("#txbContactName").val();
            var corpId = $("#ddlCorpId").val();
            //var contactStatus = $("#ddlContactStatus").val();
            var url = "Handler/ContactListHandler.ashx?k=" + contactName + "&corpId=" + corpId + "&empIdFrom=" + outEmpId;
            var formatedData = "";
            var totalrecords = 0;

            var source = {
                datatype: "json",
                sort: function () {
                    $("#jqxContactGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "C.ContactId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "C.ContactId";
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

            $("#jqxContactGrid").jqxGrid(
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
                selectionmode: "checkbox",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                      { text: "联系人姓名", datafield: "ContactName" },
                      { text: "联系人身份证号", datafield: "ContactCode" },
                      { text: "联系人电话", datafield: "ContactTel" },
                      { text: "联系人传真", datafield: "ContactFax" },
                      { text: "联系人地址", datafield: "ContactAddress" },
                      { text: "联系人公司", datafield: "CorpName" }
                      //,{ text: "联系人状态", datafield: "StatusName" }
                      //,{ text: "操作", datafield: "ContactId", cellsrenderer: cellsrenderer, width: 100 }
                ]
            }).on("rowselect", function (event) {
                var args = event.args;

                //row对象不存在，则为全选或全不选
                if (args.row == undefined) {
                    var value = $(".jqx-grid-column-header .jqx-checkbox-default").parent().jqxCheckBox("val");
                    //alert(value);
                    var rows = $("#jqxContactGrid").jqxGrid("getrows");
                    if (!value) {
                        contactIds.length = 0;
                        ECIDs.length = 0;

                        for (i = 0; i < rows.length; i++) {
                            var item = rows[i];
                            var liId = "#liEmp" + item.ContactId;
                            if ($(liId) != undefined) {
                                $(liId).remove();
                            }
                        }
                    }
                    else {
                        contactIds.length = 0;

                        for (i = 0; i < rows.length; i++) {
                            var item = rows[i];
                            contactIds.push(item.ContactId);
                            ECIDs.push(item.ECId);
                            $("#liContactList").after("<li id=\"liEmp" + item.ContactId + "\" style=\"margin-left: 30px; line-height: 30px; height: 30px;\">" + item.ContactName + "</li>");
                        }
                    }
                    return;
                }

                if (args.rowindex != undefined) {
                    var rows = $("#jqxContactGrid").jqxGrid("getrows");
                    var item = rows[args.rowindex];
                    contactIds.push(item.ContactId);
                    ECIDs.push(item.ECId);
                    $("#liContactList").after("<li id=\"liEmp" + item.ContactId + "\" style=\"margin-left: 30px; line-height: 30px; height: 30px;\">" + item.ContactName + "</li>");
                }

            }).on("rowunselect", function (event) {
                var args = event.args;
                if (args.row != undefined) {
                    var liId = "#liEmp" + args.row.ContactId;
                    if ($(liId) != undefined) {
                        $(liId).remove();

                        var index = contactIds.indexOf(args.row.ContactId);
                        contactIds.splice(index, 1);
                        ECIDs.splice(index, 1);
                    }
                }
            });



            $("#btnContactSearch").click(function () {
                var contactName = $("#txbContactName").val();
                var corpId = $("#ddlCorpId").val();
                //var contactStatus = $("#ddlContactStatus").val();
                source.url = "Handler/ContactListHandler.ashx?k=" + contactName + "&corpId=" + corpId + "&empIdFrom=" + outEmpId;
                $("#jqxContactGrid").jqxGrid("updatebounddata", "rows");
            });


            ///////////////////////转移///////////////////////

            $("#btnTransfer").click(function () {

                if (!confirm("确认转移？")) { return; }

                //alert(contactIds);
                if (empId == 0) { alert("必须选择员工"); return; }
                if (contactIds.length == 0) { alert("必须选择联系人"); return; }

                var cids = "";
                for (i = 0; i < contactIds.length; i++) {
                    if (i != 0) { cids += "|"; }
                    cids += contactIds[i];
                }

                var ecIDs = "";
                for (i = 0; i < ECIDs.length; i++) {
                    if (i != 0) { ecIDs += "|"; }
                    ecIDs += ECIDs[i];
                }

                var rows = $("#jqxContactGrid").jqxGrid("getrows");

                var transferUrl = "Handler/ContactTransferHandler.ashx";
                $.post(transferUrl, { id: empId, cid: cids, oldId: outEmpId, ecIDs: ecIDs }, function (result) {
                    alert(result);

                    var contactName = $("#txbContactName").val();
                    var corpId = $("#ddlCorpId").val();
                    source.url = "Handler/ContactListHandler.ashx?k=" + contactName + "&corpId=" + corpId + "&empIdFrom=" + outEmpId;
                    $("#jqxContactGrid").jqxGrid("updatebounddata", "rows");

                    contactIds.length = 0;
                    for (i = 0; i < rows.length; i++) {
                        var item = rows[i];
                        var liId = "#liEmp" + item.ContactId;
                        if ($(liId) != undefined) {
                            $(liId).remove();
                        }
                    }
                });
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div style="float: left;">
        <div id="jqxEmpExpander" style="float: left; margin: 0px 5px 5px 5px;">
            <div>
                员工列表
                            <input type="hidden" id="hidOutEmpId" runat="server" />
            </div>
            <div>
                <div class="SearchExpander">
                    <ul>
                        <li>
                            <span>员工编号：</span>
                            <span>
                                <input type="text" id="txbEmpCode" /></span>
                        </li>
                        <li>
                            <span>姓名：</span>
                            <span>
                                <input type="text" id="txbEmpName" /></span>
                        </li>
                        <li>
                            <input type="hidden" id="hidBDStyleId" runat="server" />
                            <span style="float: left;">员工状态：</span>
                            <div id="ddlEmpStatus" style="float: left;"></div>
                        </li>
                        <li>
                            <input type="button" id="btnEmpSearch" value="查询" />
                        </li>
                    </ul>
                </div>
                <div id="jqxEmpGrid" style="float: left;"></div>
            </div>
        </div>

        <div id="jqxTransferExpander" style="position: absolute; left: 78%">
            <div>
                转移情况          
            </div>
            <div>
                <ul style="list-style-type: none;">
                    <li style="margin-left: 30px;">
                        <input type="button" id="btnTransfer" value="转移" /></li>
                    <li>
                        <h3 style="color: yellowgreen;">选中员工：</h3>
                    </li>
                    <li style="margin-left: 30px; line-height: 25px; height: 50px;" id="liEmpName">
                        <input type="hidden" id="hidEmpId" />
                    </li>
                    <li id="liContactList">
                        <h3 style="color: yellowgreen;">选中联系人：</h3>
                        <input type="hidden" id="hidContactList" />
                    </li>
                </ul>
            </div>
        </div>

        <div id="jqxContactExpander" style="float: left; margin: 5px 0px 0px 5px">
            <div>
                联系人列表
            </div>
            <div>
                <div class="SearchExpander">
                    <ul>
                        <li>
                            <span>联系人姓名：</span>
                            <span>
                                <input type="text" id="txbContactName" />
                            </span>
                        </li>
                        <li>
                            <span style="float: left;">联系人公司：</span>
                            <div id="ddlCorpId" style="float: left;"></div>
                        </li>
                        <li>
                            <input type="button" id="btnContactSearch" value="查询" />
                        </li>
                    </ul>
                </div>
                <div id="jqxContactGrid" style="float: left;"></div>
            </div>
        </div>
    </div>
</body>
</html>
