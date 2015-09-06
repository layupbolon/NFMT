<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApplySIUpdate.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceApplySIUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>价外票发票申请修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#jqxInvApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxCanApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var upIdsArray = new Array();

            var upSIIds = "<%=this.sIIds%>";
            var splitItem = upSIIds.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    upIdsArray.push(parseInt(splitItem[i]));
                }
            }

            var downIdsArray = new Array();
            var downSIIds = "";

            //////////////////////////////////////////发票申请信息//////////////////////////////////////////

            var formatedData = "";
            var totalrecords = 0;
            var upSource =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
                   { name: "SIId", type: "int" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxApplyGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=" + upSIIds
            };
            var upDataAdapter = new $.jqx.dataAdapter(upSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxApplyGrid").jqxGrid(
            {
                width: "98%",
                source: upDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxApplyGrid").jqxGrid("getrowdata", row);
                          var index = upIdsArray.indexOf(dataRecord.SIId);
                          upIdsArray.splice(index, 1);

                          downIdsArray.push(dataRecord.SIId);

                          upSIIds = "";
                          for (i = 0; i < upIdsArray.length; i++) {
                              if (i != 0) { upSIIds += ","; }
                              upSIIds += upIdsArray[i];
                          }

                          downSIIds = "";
                          for (i = 0; i < downIdsArray.length; i++) {
                              if (i != 0) { downSIIds += ","; }
                              downSIIds += downIdsArray[i];
                          }

                          //刷新列表
                          upSource.url = "Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=" + upSIIds;
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");

                          downSource.url = "Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=" + downSIIds;
                          $("#jqxCanApplyGrid").jqxGrid("updatebounddata", "rows");

                      }
                  }
                ]
            });


            //////////////////////////////////////////可申请发票信息//////////////////////////////////////////

            var formatedData = "";
            var totalrecords = 0;
            var downSource =
            {
                datafields:
                [
                   { name: "InvoiceId", type: "int" },
                   { name: "InvoiceNo", type: "string" },
                   { name: "InvoiceDate", type: "date" },
                   { name: "InvoiceName", type: "string" },
                   { name: "InvoiceBala", type: "string" },
                   { name: "InnerCorp", type: "string" },
                   { name: "OutCorp", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "InvoiceStatus", type: "int" },
                   { name: "SIId", type: "int" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxCanApplyGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "inv.InvoiceId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "inv.InvoiceId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=" + downSIIds
            };
            var downDataAdapter = new $.jqx.dataAdapter(downSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxCanApplyGrid").jqxGrid(
            {
                width: "98%",
                source: downDataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "发票编号", datafield: "InvoiceNo" },
                  { text: "开票日期", datafield: "InvoiceDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "发票金额", datafield: "InvoiceBala" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "申请";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxCanApplyGrid").jqxGrid("getrowdata", row);
                          var index = downIdsArray.indexOf(dataRecord.SIId);
                          downIdsArray.splice(index, 1);

                          upIdsArray.push(dataRecord.SIId);

                          upSIIds = "";
                          for (i = 0; i < upIdsArray.length; i++) {
                              if (i != 0) { upSIIds += ","; }
                              upSIIds += upIdsArray[i];
                          }

                          downSIIds = "";
                          for (i = 0; i < downIdsArray.length; i++) {
                              if (i != 0) { downSIIds += ","; }
                              downSIIds += downIdsArray[i];
                          }

                          //刷新列表
                          upSource.url = "Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=" + upSIIds;
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");

                          downSource.url = "Handler/InvoiceApplySISelectedListHandler.ashx?sIIds=" + downSIIds;
                          $("#jqxCanApplyGrid").jqxGrid("updatebounddata", "rows");
                      }
                  }
                ]
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDept").jqxComboBox({ selectedIndex: 0, source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDept").jqxComboBox("val", "<%=this.curDeptId%>");

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorp").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", width: 180, disabled: true
            });
            $("#ddlApplyCorp").jqxComboBox("val", "<%=this.corpId%>");

            $("#txbApplyDesc").jqxInput({ width: "500" });
            $("#txbApplyDesc").jqxInput("val", "<%=this.apply.ApplyDesc%>");


            $("#btnCreate").jqxButton({ height: 25, width: 100 });
            $("#btnCreate").click(function () { SIApplyUpdate(); });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            function SIApplyUpdate() {
                var rows = $("#jqxApplyGrid").jqxGrid("getrows");
                if (rows.length < 1) {
                    alert("未申请价外票");
                    return;
                }

                if (!confirm("确定修改价外票开票申请？")) { return; }

                $("#btnCreate").jqxButton({ disabled: true });

                var apply = {
                    ApplyId: "<%=this.apply.ApplyId%>",
                    ApplyDept: $("#ddlApplyDept").val(),
                    ApplyCorp: $("#ddlApplyCorp").val(),
                    ApplyDesc: $("#txbApplyDesc").val()
                }

                var invoiceApply = {
                    InvoiceApplyId: "<%=this.invoiceApply.InvoiceApplyId%>",
                    ApplyId: "<%=this.apply.ApplyId%>"
                }

                $.post("Handler/InvoiceApplySIUpdateHandler.ashx", {
                    apply: JSON.stringify(apply),
                    rows: JSON.stringify(rows),
                    invoiceApply: JSON.stringify(invoiceApply),
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "InvoiceApplyList.aspx";
                        }
                        else {
                            $("#btnCreate").jqxButton({ disabled: false });
                        }
                    }
                );
            }
        });
    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />

    <div id="jqxInvApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            价外票开票申请信息
        </div>
        <div>
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxCanApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可申请价外票信息
        </div>
        <div>
            <div id="jqxCanApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            申请信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li style="float: none;">
                    <strong>申请公司：</strong>
                    <div style="float: left;" id="ddlApplyCorp"></div>
                </li>
                <li style="float: none;">
                    <strong>申请部门：</strong>
                    <div style="float: left;" id="ddlApplyDept"></div>
                </li>
                <li style="line-height: none; height: auto; float: none">
                    <strong>申请备注：</strong>
                    <textarea id="txbApplyDesc"></textarea>
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnCreate" value="修改" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="InvoiceApplyList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>