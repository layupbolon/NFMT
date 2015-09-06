<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceApplyList.aspx.cs" Inherits="NFMTSite.Invoice.InvoiceApplyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发票申请列表</title>
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

            //时间
            $("#dtFromCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });
            var tempDate = new Date();
            tempDate.setMonth(tempDate.getMonth() - 3);
            $("#dtFromCreateDate").jqxDateTimeInput({ value: tempDate });
            $("#dtToCreateDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 120 });

            //申请人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlEmpId").jqxComboBox({ source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 120, height: 25 });

            CreateStatusDropDownList("ddlApplyStatus");

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnAdd").jqxLinkButton({ height: 25, width: 120 }); 
            $("#btnSIAdd").jqxLinkButton({ height: 25, width: 120 });

            var fromdate = $("#dtFromCreateDate").val();
            var todate = $("#dtToCreateDate").val();
            var empId = $("#ddlEmpId").val();
            var status = $("#ddlApplyStatus").val();
            var url = "Handler/InvoiceApplyListHandler.ashx?f=" + fromdate + "&t=" + todate + "&e=" + empId + "&s=" + status;

            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "InvoiceApplyId", type: "int" },
                   { name: "ApplyId", type: "int" },
                   { name: "ApplyNo", type: "date" },
                   { name: "Name", type: "string" },
                   { name: "ApplyTime", type: "date" },
                   { name: "DeptName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "ApplyDesc", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "ApplyStatus", type: "int" },
                   { name: "SIApply", type: "int" },
                   { name: "BIApply", type: "int" }
                   //,{ name: "TotalBala", type: "string" }
                ],
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
                sortcolumn: "ia.InvoiceApplyId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ia.InvoiceApplyId";
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
                var item = $("#jqxgrid").jqxGrid("getrowdata", row);

                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                if (item.BIApply > 0) {                    
                    cellHtml += "<a target=\"_self\" href=\"InvoiceApplyDetail.aspx?id=" + value + "\">明细</a>";

                    if (item.ApplyStatus > statusEnum.已关闭 && item.ApplyStatus < statusEnum.待审核) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"InvoiceApplyUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else {
                        cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                else if (item.SIApply > 0) {
                    cellHtml += "<a target=\"_self\" href=\"InvoiceApplySIDetail.aspx?id=" + value + "\">明细</a>";

                    if (item.ApplyStatus > statusEnum.已关闭 && item.ApplyStatus < statusEnum.待审核) {
                        cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"InvoiceApplySIUpdate.aspx?id=" + value + "\">修改</a>";
                    }
                    else {
                        cellHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                cellHtml += "</div>";
                return cellHtml;
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
                  { text: "申请编号", datafield: "ApplyNo" },
                  { text: "申请日期", datafield: "ApplyTime", cellsformat: "yyyy-MM-dd" },
                  { text: "申请人", datafield: "Name" },
                  { text: "申请部门", datafield: "DeptName" },
                  { text: "申请公司", datafield: "CorpName" },
                  { text: "申请附言", datafield: "ApplyDesc" },
                  //{ text: "开票金总额", datafield: "TotalBala" },
                  { text: "申请状态", datafield: "StatusName" },
                  { text: "操作", datafield: "InvoiceApplyId", cellsrenderer: cellsrenderer, width: 100, sortable: false, enabletooltips: false }
                ]
            });

            $("#btnSearch").click(function () {
                var fromdate = $("#dtFromCreateDate").val();
                var todate = $("#dtToCreateDate").val();
                var empId = $("#ddlEmpId").val();
                var status = $("#ddlApplyStatus").val();

                source.url = "Handler/InvoiceApplyListHandler.ashx?f=" + fromdate + "&t=" + todate + "&e=" + empId + "&s=" + status;
                $("#jqxgrid").jqxGrid("updatebounddata", "rows");
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
                    <span style="float: left;">申请日期：</span>
                    <div id="dtFromCreateDate" style="float: left;"></div>
                    <span style="float: left;">至</span>
                    <div id="dtToCreateDate" style="float: left;"></div>
                </li>
                <li>
                    <span style="float: left;">申请人：</span>
                    <div id="ddlEmpId" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">申请状态：</span>
                    <div id="ddlApplyStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="InvoiceApplyBusInvList.aspx" id="btnAdd">业务票开票申请</a>
                    <a target="_self" runat="server" href="InvoiceApplySIList.aspx" id="btnSIAdd">价外票开票申请</a>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            数据列表
        </div>
        <div>
            <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
