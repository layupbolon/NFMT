<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankList.aspx.cs" Inherits="NFMTSite.BasicData.BankList" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行管理</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script src="../js/Utility.js"></script>
    <script src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            $("#btnAdd").jqxLinkButton({ height: 25, width: 80 });
            $("#btnSearch").jqxButton({ height: 25 });

            $("#txbBankName").jqxInput({ height: 22, width: 120 });
            $("#txbBankEname").jqxInput({ height: 22, width: 120 });

            CreateStatusDropDownList("selBankeStatus");

            //绑定 银行资本类型
            var styleId = $("#hidStyleId").val();
            var sourceCapitalType = { datatype: "json", url: "Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCapitalType);
            $("#capitalType").jqxDropDownList({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 100, height: 25, autoDropDownHeight: true });
            //$("#capitalType").jqxComboBox("val", $("#hidCapitalType").val());


            var bankName = $("#txbBankName").val();
            var status = $("#selBankeStatus").val();
            var capitalType = $("#capitalType").val();
            var bankEname = $("#txbBankEname").val();
            var url = "Handler/BankListHandler.ashx?k=" + bankName + "&s=" + status + "&capitalType=" + capitalType + "&bankEname=" + bankEname;
            var formatedData = "";
            var totalrecords = 0;

            var source =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "B.BankId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "B.BankId";
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
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px; margin-top: 4px;\">";
                cellHtml += "<a target=\"_self\" href=\"BankDetail.aspx?id=" + value + "\">明细</a>";
                cellHtml += "&nbsp;&nbsp;&nbsp<a target=\"_self\" href=\"BankUpdate.aspx?id=" + value + "\">修改</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxGrid").jqxGrid(
            {
                width: "99%",
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
                  { text: "银行名称", datafield: "BankName" },
                  { text: "银行英文名称", datafield: "BankEname" },
                  { text: "银行全称", datafield: "BankFullName" },
                  { text: "银行缩写", datafield: "BankShort" },
                  { text: "资本类型", datafield: "DetailName" },
                  { text: "上级银行名称", datafield: "ParentBankName" },
                  { text: "头寸是否转回", datafield: "SwitchBack" },
                  { text: "银行状态", datafield: "StatusName" },
                  { text: "操作", datafield: "BankId", cellsrenderer: cellsrenderer, width: 100, enabletooltips: false, sortable: false }
                ]
            });


            function LoadData() {
                var bankName = $("#txbBankName").val();
                var status = $("#selBankeStatus").val();
                var capitalType = $("#capitalType").val();
                var bankEname = $("#txbBankEname").val();
                source.url = "Handler/BankListHandler.ashx?k=" + bankName + "&s=" + status + "&capitalType=" + capitalType + "&bankEname=" + bankEname;
                $("#jqxGrid").jqxGrid("gotopage", 0);
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            }

            $("#btnSearch").click(LoadData);

        });
    </script>

</head>
<body>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            查询条件
                        <input type="hidden" id="hidCapitalType" runat="server" />
            <input type="hidden" id="hidStyleId" runat="server" />
        </div>
        <div id="SearchDiv">
            <ul>
                <li>
                    <span>银行名称：</span>
                    <span>
                        <input type="text" id="txbBankName" />
                    </span>
                </li>
                <li>
                    <span>银行英文名称：</span>
                    <span>
                        <input type="text" id="txbBankEname" />
                    </span>
                </li>
                <li>
                    <span style="float: left;">银行类型：</span>
                    <div id="capitalType" style="float: left;" />
                </li>
                <li>
                    <span style="float: left;">银行状态：</span>
                    <div id="selBankeStatus" style="float: left;" />
                </li>
                <li>
                    <input type="button" id="btnSearch" value="查询" />
                    <a target="_self" runat="server" href="BankCreate.aspx" id="btnAdd">新增银行</a>
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
