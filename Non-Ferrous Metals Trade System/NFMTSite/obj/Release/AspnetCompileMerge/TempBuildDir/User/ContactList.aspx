<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="NFMTSite.User.ContactList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>联系人列表</title>
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

            $("#txbContactName").jqxInput({ height: 23, width: 120 });

            //绑定 联系人公司
            var url = "Handler/CorpDDLHandler.ashx?isSelf=1";
            var source = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlCorpId").jqxComboBox({ source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlContactStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 80 });

            var contactName = $("#txbContactName").val();
            var corpId = $("#ddlCorpId").val();
            var contactStatus = $("#ddlContactStatus").val();
            var url = "Handler/ContactListBasicHandler.ashx?k=" + contactName + "&s=" + contactStatus + "&corpId=" + corpId;
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
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"ContactDetail.aspx?id=" + value + "\">明细</a>";
                if (item.ContactStatus > statusEnum.已作废 && item.ContactStatus < statusEnum.待审核) {
                    cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"ContactUpdate.aspx?id=" + value + "\">修改</a>";
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
                      { text: "联系人姓名", datafield: "ContactName" },
                      { text: "联系人身份证号", datafield: "ContactCode" },
                      { text: "联系人电话", datafield: "ContactTel" },
                      { text: "联系人传真", datafield: "ContactFax" },
                      { text: "联系人地址", datafield: "ContactAddress" },
                      { text: "联系人公司", datafield: "CorpName" },
                      { text: "联系人状态", datafield: "StatusName" },
                      { text: "操作", datafield: "ContactId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var contactName = $("#txbContactName").val();
                var corpId = $("#ddlCorpId").val();
                var contactStatus = $("#ddlContactStatus").val();
                source.url = "Handler/ContactListBasicHandler.ashx?k=" + contactName + "&s=" + contactStatus + "&corpId=" + corpId;
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
                    <div style="float: left;">联系人状态：</div>
                    <div id="ddlContactStatus" style="float: left;"></div>
                </li>

                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="ContactCreate.aspx" id="btnAdd">新增联系人</a>
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
