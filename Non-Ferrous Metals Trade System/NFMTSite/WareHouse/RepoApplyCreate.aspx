<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepoApplyCreate.aspx.cs" Inherits="NFMTSite.WareHouse.RepoApplyCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回购申请新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxMoveExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var stockIds = new Array();

            /////////////////////////回购申请信息/////////////////////////

            var sids = "";
            for (i = 0; i < stockIds.length; i++) {
                if (i != 0) { sids += ","; }
                sids += stockIds[i];
            }

            var url = "Handler/RepoApplyInfoHandler.ashx?mode=1&sids=" + sids;
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
                    { name: "CorpName", type: "string" },
                    { name: "DPName", type: "string" },
                    { name: "CardName", type: "string" },
                    { name: "BrandName", type: "string" },
                    { name: "BankName", type: "string" }
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

            $("#jqxApplyGrid").jqxGrid(
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
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "质押银行", datafield: "BankName" },
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

                        var index = stockIds.indexOf(rowId);
                        stockIds.splice(index, 1);

                        var sids = "";
                        for (i = 0; i < stockIds.length; i++) {
                            if (i != 0) { sids += ","; }
                            sids += stockIds[i];
                        }

                        //刷新列表
                        Infosource.url = "Handler/RepoApplyInfoHandler.ashx?mode=1&sids=" + sids;
                        $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/CanRepoApplyListHandler.ashx?sids=" + sids;
                        $("#jqxMoveGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            /////////////////////////可回购库存信息/////////////////////////

            var url = "Handler/CanRepoApplyListHandler.ashx";
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
                   { name: "CorpName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "BankName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxMoveGrid").jqxGrid("updatebounddata", "sort");
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

            $("#jqxMoveGrid").jqxGrid(
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
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "质押银行", datafield: "BankName" },
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

                        stockIds.push(rowId);

                        var sids = "";
                        for (i = 0; i < stockIds.length; i++) {
                            if (i != 0) { sids += ","; }
                            sids += stockIds[i];
                        }

                        //刷新列表
                        Infosource.url = "Handler/RepoApplyInfoHandler.ashx?mode=1&sids=" + sids;
                        $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                        source.url = "Handler/CanRepoApplyListHandler.ashx?sids=" + sids;
                        $("#jqxMoveGrid").jqxGrid("updatebounddata", "rows");
                    });
                }
            });

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorpId").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").jqxComboBox("val", "<%=this.deptId%>");

            $("#txbMemo").jqxInput({ height: 25, width: 400 });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //验证
            $("#jqxExpander").jqxValidator({
                rules:
                        [
                            {
                                input: "#ddlApplyDeptId", message: "申请部门必选", action: "keyup,blur", rule: function (input, commit) {
                                    return $("#ddlApplyDeptId").jqxComboBox("val") > 0;
                                }
                            }
                        ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (stockIds.length == 0) { alert("必须选择回购库存！"); return; }

                if (!confirm("确定提交回购申请？")) { return; }
                $("#btnAdd").jqxButton({ disabled: true });

                var apply = {
                    ApplyDept: $("#ddlApplyDeptId").val(),
                    ApplyCorp: $("#ddlApplyCorpId").val(),
                    ApplyDesc: $("#txbMemo").val()
                };

                var rows = $("#jqxApplyGrid").jqxGrid("getrows");

                $.post("Handler/RepoApplyCreateHandler.ashx", {
                    repo: JSON.stringify(rows),
                    apply: JSON.stringify(apply)
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "RepoApplyList.aspx";
                        }
                        else
                            $("#btnAdd").jqxButton({ disabled: false });
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回购申请信息
        </div>
        <div style="height: 500px;">

            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxMoveExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可回购库存信息
        </div>
        <div style="height: 500px;">
            <div id="jqxMoveGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            回购申请新增
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">申请公司：</span>
                    <div id="ddlApplyCorpId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">申请部门：</span>
                    <div id="ddlApplyDeptId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">备注：</span>
                    <span>
                        <input type="text" id="txbMemo" /></span>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="提交" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="RepoApplyList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>

</html>
