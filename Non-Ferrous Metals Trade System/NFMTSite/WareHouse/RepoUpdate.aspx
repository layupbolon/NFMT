<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepoUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.RepoUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回购修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#jqxPledgeExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var stockIdsDown = new Array();
            var stockIdsUp = new Array();

            var sidsDown = $("#hidsidsDown").val();
            var splitItem = sidsDown.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIdsDown.push(splitItem[i]);
                }
            }

            var sidsUp = $("#hidsidsUp").val();
            splitItem = sidsUp.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIdsUp.push(splitItem[i]);
                }
            }

            /////////////////////////回购信息/////////////////////////

            var url = "Handler/RepoStockInfoHandler.ashx?sids=" + sidsUp + "&pid=" + "<%=this.repoApplyId%>";
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "DPName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxPledgeGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "st.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "st.StockId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: url
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var moveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnDelete\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" />";
            }

            $("#jqxPledgeGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
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
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "权证编号", datafield: "PaperNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "操作", datafield: "StockId", cellsrenderer: moveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var deletes = $("input[name=\"btnDelete\"]");
                for (i = 0; i < deletes.length; i++) {
                    var btnDelete = deletes[i];
                    var val = btnDelete.id;

                    $(btnDelete).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        //if (!confirm("确定取消？")) { return; }

                        var index = stockIdsUp.indexOf(rowId);
                        stockIdsUp.splice(index, 1);

                        var sidsUp = "";
                        for (i = 0; i < stockIdsUp.length; i++) {
                            if (i != 0) { sidsUp += ","; }
                            sidsUp += stockIdsUp[i];
                        }

                        stockIdsDown.push(rowId);

                        var sidsDown = "";
                        for (i = 0; i < stockIdsDown.length; i++) {
                            if (i != 0) { sidsDown += ","; }
                            sidsDown += stockIdsDown[i];
                        }

                        //刷新列表
                        Infosource.url = "Handler/RepoStockInfoHandler.ashx?sids=" + sidsUp + "&pid=" + "<%=this.repoApplyId%>";
                        $("#jqxPledgeGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/RepoStockInfoHandler.ashx?sids=" + sidsDown + "&pid=" + "<%=this.repoApplyId%>";
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            /////////////////////////可回购库存信息/////////////////////////

            var url = "Handler/RepoStockInfoHandler.ashx?sids=" + sidsDown + "&pid=" + "<%=this.repoApplyId%>";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "DPName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxStockGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "st.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "st.StockId";
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

            var moveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnMove\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"回购\" />";
            }

            $("#jqxStockGrid").jqxGrid(
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
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "权证编号", datafield: "PaperNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "操作", datafield: "StockId", cellsrenderer: moveRender, width: 100, enabletooltips: false }
                ]
            }).on("bindingcomplete", function (event) {
                var moves = $("input[name=\"btnMove\"]");
                for (i = 0; i < moves.length; i++) {
                    var btnMove = moves[i];
                    var val = btnMove.id;

                    $(btnMove).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        //if (!confirm("确定回购？")) { return; }

                        var index = stockIdsDown.indexOf(rowId);
                        stockIdsDown.splice(index, 1);

                        var sidsDown = "";
                        for (i = 0; i < stockIdsDown.length; i++) {
                            if (i != 0) { sidsDown += ","; }
                            sidsDown += stockIdsDown[i];
                        }

                        stockIdsUp.push(rowId);

                        var sidsUp = "";
                        for (i = 0; i < stockIdsUp.length; i++) {
                            if (i != 0) { sidsUp += ","; }
                            sidsUp += stockIdsUp[i];
                        }

                        //刷新列表
                        Infosource.url = "Handler/RepoStockInfoHandler.ashx?sids=" + sidsUp + "&pid=" + "<%=this.repoApplyId%>";
                        $("#jqxPledgeGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/RepoStockInfoHandler.ashx?sids=" + sidsDown + "&pid=" + "<%=this.repoApplyId%>";
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            //附言
            $("#txbMemo").jqxInput({ height: 25, width: 400 });

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").on("click", function () {
                if (!confirm("确定修改？")) { return; }

                if (stockIdsUp.length == 0) { alert("必须选择回购库存！"); return; }

                var deptId = $("#ddlPledgeDept").val();
                var bankId = $("#ddlPledgeBank").val();

                var sids = "";
                for (i = 0; i < stockIdsUp.length; i++) {
                    if (i != 0) { sids += ","; }
                    sids += stockIdsUp[i];
                }

                $.post("Handler/RepoUpdateHandler.ashx", {
                    id: $("#hidid").val(),
                    sids: sids,
                    memo: $("#txbMemo").val()
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "RepoList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxPledgeExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回购信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidid" runat="server" />
            <input type="hidden" id="hidsidsUp" runat="server" />
            <div id="jqxPledgeGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可回购库存信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidsidsDown" runat="server" />
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回购修改
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">附言：</span>
                    <span>
                        <input type="text" id="txbMemo" runat="server" /></span>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnUpdate" value="提交" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="RepoList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
