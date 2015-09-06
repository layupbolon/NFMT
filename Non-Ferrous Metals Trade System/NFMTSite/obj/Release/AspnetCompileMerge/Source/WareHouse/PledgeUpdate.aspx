<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PledgeUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.PledgeUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押修改</title>
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

            /////////////////////////质押信息/////////////////////////

            var url = "Handler/PledgeStockInfoHandler.ashx?pid=" + "<%=this.pledge.PledgeApplyId%>" + "&sids=" + sidsUp;
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "ApplyQty", type: "number" },
                   { name: "UintId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "CurrencyId", type: "int" },
                   { name: "PledgePrice", type: "number" }
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
                  { text: "质押价格", datafield: "PledgePrice" },
                  { text: "币种", datafield: "CurrencyName" },
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
                        Infosource.url = "Handler/PledgeStockInfoHandler.ashx?pid=" + "<%=this.pledge.PledgeApplyId%>" + "&sids=" + sidsUp;
                        $("#jqxPledgeGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/PledgeStockInfoHandler.ashx?pid=" + "<%=this.pledge.PledgeApplyId%>" + "&sids=" + sidsDown;
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            /////////////////////////可质押库存信息/////////////////////////

            var url = "Handler/PledgeStockInfoHandler.ashx?pid=" + "<%=this.pledge.PledgeApplyId%>" + "&sids=" + sidsDown;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "DetailId", type: "int" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "ApplyQty", type: "number" },
                   { name: "UintId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "CurrencyId", type: "int" },
                   { name: "PledgePrice", type: "number" }
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
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnMove\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"质押\" />";
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
                  { text: "质押价格", datafield: "PledgePrice" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "操作", datafield: "StockId", cellsrenderer: moveRender, width: 100, enabletooltips: false }
                ]
            }).on("bindingcomplete", function (event) {
                var moves = $("input[name=\"btnMove\"]");
                for (i = 0; i < moves.length; i++) {
                    var btnMove = moves[i];
                    var val = btnMove.id;

                    $(btnMove).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        //if (!confirm("确定质押？")) { return; }

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
                        Infosource.url = "Handler/PledgeStockInfoHandler.ashx?pid=" + "<%=this.pledge.PledgeApplyId%>" + "&sids=" + sidsUp;
                        $("#jqxPledgeGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/PledgeStockInfoHandler.ashx?pid=" + "<%=this.pledge.PledgeApplyId%>" + "&sids=" + sidsDown;
                        $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            //质押银行
            var ddlPledgeBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlPledgeBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlPledgeBankurl, async: false };
            var ddlPledgeBankdataAdapter = new $.jqx.dataAdapter(ddlPledgeBanksource);
            $("#ddlPledgeBank").jqxComboBox({ selectedIndex: 0, source: ddlPledgeBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });
            if ($("#hidbankId").val() > 0) { $("#ddlPledgeBank").jqxComboBox("val", $("#hidbankId").val()); }

            //附言
            $("#txbMemo").jqxInput({ height: 25, width: 400 });

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").on("click", function () {
                if (!confirm("确定修改？")) { return; }

                if (stockIdsUp.length == 0) { alert("必须选择质押库存！"); return; }
                $("#btnUpdate").jqxButton({ disabled: true });

                var pledge = {
                    PledgeId: "<%=this.pledge.PledgeId%>",
                    PledgeApplyId: "<%=this.pledge.PledgeApplyId%>",
                    PledgeBank: $("#ddlPledgeBank").val(),
                    Memo: $("#txbMemo").val()
                }

                var rows = $("#jqxPledgeGrid").jqxGrid("getrows");

                //附件信息
                var fileIds = new Array();
                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/PledgeUpdateHandler.ashx", {
                    pledge: JSON.stringify(pledge),
                    rows: JSON.stringify(rows)
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            AjaxFileUpload(fileIds, "<%=this.pledge.PledgeId%>", AttachTypeEnum.PledgeAttach);
                        }
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PledgeList.aspx";
                        }
                        else
                            $("#btnUpdate").jqxButton({ disabled: false });
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
            质押信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidid" runat="server" />
            <input type="hidden" id="hidsidsUp" runat="server" />
            <div id="jqxPledgeGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可质押库存信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidsidsDown" runat="server" />
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            质押修改
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <input type="hidden" id="hidbankId" runat="server" />
                    <span style="width: 15%; text-align: right;">质押银行：</span>
                    <div id="ddlPledgeBank"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">附言：</span>
                    <span>
                        <input type="text" id="txbMemo" runat="server" /></span>
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="PledgeAttach" />

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" />&nbsp;&nbsp;&nbsp;&nbsp;
            <a target="_self" runat="server" href="CanPledgeApplyList.aspx" id="btnCancel" style="margin-left: 10px">取消</a>
    </div>
</body>

</html>
