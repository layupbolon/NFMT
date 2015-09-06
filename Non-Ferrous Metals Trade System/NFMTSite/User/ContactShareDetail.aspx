<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactShareDetail.aspx.cs" Inherits="NFMTSite.User.ContactShareDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>联系人共享明细</title>
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
            $("#jqxDataExpander").jqxExpander({ width: "58%" });
            $("#jqxAllotExpander").jqxExpander({ width: "20%" });

            $("#btnAllot").jqxButton({ height: 30, width: 120 });

            //绑定 联系人公司
            var url = "Handler/CorpDDLHandler.ashx?isSelf=1";
            var source = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlCorpId").jqxComboBox({ source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#txbContactName").jqxInput({ height: 25, width: 120 });

            var contactName = $("#txbContactName").val();
            var corpId = $("#ddlCorpId").val();
            var contactStatus = statusEnum.已生效;
            var url = "Handler/ContactListHandler.ashx?k=" + contactName + "&s=" + contactStatus + "&corpId=" + corpId + "&empIdFrom=" + "<%=this.empId.ToString()%>" + "&empIdTo=" + "<%=Request.QueryString["id"]%>";
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

            var empIds = new Array();

            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                columnsresize: true,
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
                      { text: "联系人姓名", datafield: "ContactName" },
                      { text: "联系人身份证号", datafield: "ContactCode" },
                      { text: "联系人电话", datafield: "ContactTel" },
                      { text: "联系人传真", datafield: "ContactFax" },
                      { text: "联系人地址", datafield: "ContactAddress" },
                      { text: "联系人公司", datafield: "CorpName" }
                      //{ text: "联系人状态", datafield: "StatusName" },
                      //,{ text: "操作", datafield: "ContactId", width: 100 }
                ]
            });


            $("#jqxGrid").on("rowselect", function (event) {
                var args = event.args;

                //row对象不存在，则为全选或全不选
                if (args.row == undefined) {
                    var value = $(".jqx-grid-column-header .jqx-checkbox-default").parent().jqxCheckBox("val");
                    //alert(value);
                    var rows = $("#jqxGrid").jqxGrid("getrows");
                    if (!value) {
                        empIds.length = 0;

                        for (i = 0; i < rows.length; i++) {
                            var item = rows[i];
                            var liId = "#liEmp" + item.ContactId;
                            if ($(liId) != undefined) {
                                $(liId).remove();
                            }
                        }
                    }
                    else {
                        empIds.length = 0;

                        for (i = 0; i < rows.length; i++) {
                            var item = rows[i];
                            empIds.push(item.ContactId);
                            $("#liEmpList").after("<li id=\"liEmp" + item.ContactId + "\" style=\"margin-left: 30px; line-height: 30px; height: 30px;\">" + item.ContactName + "</li>");
                        }
                    }
                    return;
                }

                if (args.rowindex != undefined) {
                    var rows = $("#jqxGrid").jqxGrid("getrows");
                    var item = rows[args.rowindex];
                    empIds.push(item.ContactId);
                    $("#liEmpList").after("<li id=\"liEmp" + item.ContactId + "\" style=\"margin-left: 30px; line-height: 30px; height: 30px;\">" + item.ContactName + "</li>");
                }
            });

            $("#jqxGrid").on("rowunselect", function (event) {
                var args = event.args;
                if (args.row != undefined) {
                    var liId = "#liEmp" + args.row.ContactId;
                    if ($(liId) != undefined) {
                        $(liId).remove();

                        var index = contactIds.indexOf(args.row.ContactId);
                        empIds.splice(index, 1);
                    }
                }
            });

            $("#btnSearch").click(function () {
                var contactName = $("#txbContactName").val();
                var corpId = $("#ddlCorpId").val();
                var contactStatus = statusEnum.已生效;
                source.url = "Handler/ContactListHandler.ashx?k=" + contactName + "&s=" + contactStatus + "&corpId=" + corpId + "&empIdFrom=" + "<%=this.empId.ToString()%>" + "&empIdTo=" + "<%=Request.QueryString["id"]%>";
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });

            $("#btnAllot").click(function () {
                //alert(empIds);
                if (empIds.length == 0) { alert("必须选择员工"); return; }

                var eids = "";
                for (i = 0; i < empIds.length; i++) {
                    if (i != 0) { eids += "|"; }
                    eids += empIds[i];
                }

                var rows = $("#jqxGrid").jqxGrid("getrows");

                var allotUrl = "Handler/ContactShareHandler.ashx";
                $.post(allotUrl, { id: "<%=Request.QueryString["id"]%>", cid: eids }, function (result) {
                    alert(result);

                    var contactName = $("#txbContactName").val();
                    var corpId = $("#ddlCorpId").val();
                    var contactStatus = statusEnum.已生效;
                    source.url = "Handler/ContactListHandler.ashx?k=" + contactName + "&s=" + contactStatus + "&corpId=" + corpId + "&empIdFrom=" + "<%=this.empId.ToString()%>" + "&empIdTo=" + "<%=Request.QueryString["id"]%>";
                    $("#jqxGrid").jqxGrid("updatebounddata", "rows");


                    empIds.length = 0;

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

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
        </div>
        <div id="SearchDiv">
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
                <%--<li>
                    <div style="float: left;">联系人状态：</div>
                    <div id="ddlContactStatus" style="float: left;"></div>
                </li>--%>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <%--<a target="_self" runat="server" href="ContactCreate.aspx" id="btnAdd">新增联系人</a>--%>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可共享联系人列表
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
            <%--<input type="button" id="btnShare" value="共享"/>--%>
        </div>

    </div>

    <div id="jqxAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            共享情况          
        </div>
        <div>
            <ul style="list-style-type: none;">
                <li style="margin-left: 30px;">
                    <input type="button" id="btnAllot" value="共享" /></li>
                <%--<li>
                    <h3 style="color: yellowgreen;">选中部门：</h3>
                </li>
                <li style="margin-left: 30px; line-height: 25px; height: 50px;" id="liDeptName">
                    <input type="hidden" id="hidDeptId" />
                </li>--%>
                <li id="liEmpList">
                    <h3 style="color: yellowgreen;">选中联系人：</h3>
                    <input type="hidden" id="hidEmpList" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
