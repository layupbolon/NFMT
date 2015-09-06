<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthGroupAllotDetail.aspx.cs" Inherits="NFMTSite.User.AuthGroupAllotDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>员工权限组分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            

            var empId = "<%=this.empId%>";

            $("#jqxEmpExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            //绑定现有员工列表
            var url = "Handler/EmpAuthGroupListHandler.ashx?empId=" + empId;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                sort: function () {
                    $("#jqxGridAuth").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "detail.DetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "detail.DetailId";
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
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnDelete\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"删除\" />";
            }
            $("#jqxGridAuth").jqxGrid(
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
                      { text: "员工名称", datafield: "Name" },
                      { text: "权限组名称", datafield: "AuthGroupName" },
                      { text: "品种", datafield: "AssetName" },
                      { text: "贸易方向", datafield: "TradeDirection" },
                      { text: "贸易境区", datafield: "TradeBorder" },
                      { text: "内外部", datafield: "ContractInOut" },
                      { text: "长零单", datafield: "ContractLimit" },
                      { text: "公司", datafield: "CorpName" },
                      { text: "操作", datafield: "DetailId", cellsrenderer: deleteRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var deletes = $("input[name=\"btnDelete\"]");
                for (i = 0; i < deletes.length; i++) {
                    var btnDel = deletes[i];
                    var val = btnDel.id;

                    $(btnDel).click({ value: val }, function (event) {
                        var rowId = event.data.value;
                        if (!confirm("确定删除？")) { return; }

                        $.post("Handler/EmpAuthGroupUpdateStatusHandler.ashx", { id: rowId }, function (result) {
                            alert(result);
                            //刷新列表
                            source.url = "Handler/EmpAuthGroupListHandler.ashx?empId=" + empId;
                            $("#jqxGridAuth").jqxGrid("updatebounddata", "rows");

                            var name = $("#txbAuthGroupName").val();
                            var assetId = $("#ddlAssetId").val();
                            var tradeDirection = $("#ddlTradeDirection").val();
                            var tradeBorder = $("#ddlTradeBorder").val();
                            var contractInOut = $("#ddlContractInOut").val();
                            var contractLimit = $("#ddlContractLimit").val();
                            var corpId = $("#ddlCorpId").val();
                            sourceAuth.url = "Handler/AuthGroupCanAllotListHandler.ashx?name=" + name + "&assetId=" + assetId + "&tradeDirection=" + tradeDirection + "&tradeBorder=" + tradeBorder + "&contractInOut=" + contractInOut + "&contractLimit=" + contractLimit + "&corpId=" + corpId + "&empId=" + empId;
                            $("#jqxGrid").jqxGrid("updatebounddata", "rows");
                        });
                    });
                }
            });

            $("#jqxAuthExpander").jqxExpander({ width: "98%" });
            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            
            //权限组名称
            $("#txbAuthGroupName").jqxInput({ height: 23, width: 120 });

            //品种
            var assetSource = { datatype: "json", url: "../BasicData/Handler/AssetDDLHandler.ashx", async: false };
            var assetDataAdapter = new $.jqx.dataAdapter(assetSource);
            $("#ddlAssetId").jqxComboBox({ source: assetDataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25, searchMode: "containsignorecase" });

            //贸易方向
            var directionSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeDirection").val(), async: false };
            var directionDataAdapter = new $.jqx.dataAdapter(directionSource);
            $("#ddlTradeDirection").jqxComboBox({ source: directionDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //贸易境区
            var borderSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidTradeBorder").val(), async: false };
            var borderDataAdapter = new $.jqx.dataAdapter(borderSource);
            $("#ddlTradeBorder").jqxComboBox({ source: borderDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //内外部
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractInOut").val(), async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlContractInOut").jqxComboBox({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //长单零单
            var limitSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + $("#hidContractLimit").val(), async: false };
            var limitDataAdapter = new $.jqx.dataAdapter(limitSource);
            $("#ddlContractLimit").jqxComboBox({ source: limitDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, searchMode: "containsignorecase" });

            //公司
            var inCorpUrl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var inCorpSource = { datatype: "json", url: inCorpUrl, async: false };
            var inCorpDataAdapter = new $.jqx.dataAdapter(inCorpSource);
            $("#ddlCorpId").jqxComboBox({
                source: inCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase", height: 25, width: 200
            });

            var name = $("#txbAuthGroupName").val();
            var assetId = $("#ddlAssetId").val();
            var tradeDirection = $("#ddlTradeDirection").val();
            var tradeBorder = $("#ddlTradeBorder").val();
            var contractInOut = $("#ddlContractInOut").val();
            var contractLimit = $("#ddlContractLimit").val();
            var corpId = $("#ddlCorpId").val();
            var urlAuth = "Handler/AuthGroupCanAllotListHandler.ashx?name=" + name + "&assetId=" + assetId + "&tradeDirection=" + tradeDirection + "&tradeBorder=" + tradeBorder + "&contractInOut=" + contractInOut + "&contractLimit=" + contractLimit + "&corpId=" + corpId + "&empId=" + empId;

            var sourceAuth = {
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
                sortcolumn: "ag.AuthGroupId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ag.AuthGroupId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: urlAuth
            };
            var dataAdapterEmp = new $.jqx.dataAdapter(sourceAuth, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"btnAdd\" id=\"" + value + "\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" />";
            }
            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapterEmp,
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
                      { text: "权限组名称", datafield: "AuthGroupName" },
                      { text: "品种", datafield: "AssetName" },
                      { text: "贸易方向", datafield: "TradeDirection" },
                      { text: "贸易境区", datafield: "TradeBorder" },
                      { text: "内外部", datafield: "ContractInOut" },
                      { text: "长单零单", datafield: "ContractLimit" },
                      { text: "公司", datafield: "CorpName" },
                      //{ text: "状态", datafield: "StatusName" },
                      { text: "操作", datafield: "AuthGroupId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false }
                ]
            }).on("bindingcomplete", function (event) {
                var adds = $("input[name=\"btnAdd\"]");

                for (i = 0; i < adds.length; i++) {
                    var btnCreate = adds[i];
                    var val = btnCreate.id;

                    $(btnCreate).click({ value: val }, function (event) {
                        var rowId = event.data.value;

                        if (!confirm("确定添加该权限组？")) { return; }

                        $.post("Handler/EmpAuthGroupInsertStatusHandler.ashx", { id: rowId, empId: empId }, function (result) {
                            alert(result);
                            //刷新列表
                            source.url = "Handler/EmpAuthGroupListHandler.ashx?empId=" + empId;
                            $("#jqxGridAuth").jqxGrid("updatebounddata", "rows");

                            var name = $("#txbAuthGroupName").val();
                            var assetId = $("#ddlAssetId").val();
                            var tradeDirection = $("#ddlTradeDirection").val();
                            var tradeBorder = $("#ddlTradeBorder").val();
                            var contractInOut = $("#ddlContractInOut").val();
                            var contractLimit = $("#ddlContractLimit").val();
                            var corpId = $("#ddlCorpId").val();
                            sourceAuth.url = "Handler/AuthGroupCanAllotListHandler.ashx?name=" + name + "&assetId=" + assetId + "&tradeDirection=" + tradeDirection + "&tradeBorder=" + tradeBorder + "&contractInOut=" + contractInOut + "&contractLimit=" + contractLimit + "&corpId=" + corpId + "&empId=" + empId;
                            $("#jqxGrid").jqxGrid("updatebounddata", "rows");
                        });
                    });
                }
            });

            //搜索
            $("#btnSearch").click(function () {
                var name = $("#txbAuthGroupName").val();
                var assetId = $("#ddlAssetId").val();
                var tradeDirection = $("#ddlTradeDirection").val();
                var tradeBorder = $("#ddlTradeBorder").val();
                var contractInOut = $("#ddlContractInOut").val();
                var contractLimit = $("#ddlContractLimit").val();
                var corpId = $("#ddlCorpId").val();
                sourceAuth.url = "Handler/AuthGroupCanAllotListHandler.ashx?name=" + name + "&assetId=" + assetId + "&tradeDirection=" + tradeDirection + "&tradeBorder=" + tradeBorder + "&contractInOut=" + contractInOut + "&contractLimit=" + contractLimit + "&corpId=" + corpId+ "&empId=" + empId;
                $("#jqxGrid").jqxGrid("updatebounddata", "rows");
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxEmpExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            员工信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>所属部门：</strong>
                    <span id="ddlDeptId" runat="server"></span>
                </li>
                <li>
                    <strong>员工编号：</strong>
                    <span id="txbEmpCode" runat="server" /></li>
                <li>
                    <strong>姓名：</strong>
                    <span id="txbEmpName" runat="server" /></li>
                <li>
                    <strong>性别：</strong>
                    <span id="rdMale" runat="server"></span></li>
                <li>
                    <strong>生日：</strong>
                    <span id="dtBirthday" runat="server"></span></li>
                <li>
                    <strong>手机号码：</strong>
                    <span id="txbTel" runat="server" />
                </li>
                <li>
                    <strong>座机号码：</strong>
                    <span id="txbPhone" runat="server" />
                </li>
                <li>
                    <strong>在职状态：</strong>
                    <span id="ddlWorkStatus" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            员工拥有权限
        </div>
        <div>
            <div id="jqxGridAuth" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAuthExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            可分配权限组列表
        </div>
        <div>
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span>权限组名称：</span>
                        <input type="text" id="txbAuthGroupName" />
                    </li>
                    <li>
                        <span style="float: left;">品种：</span>
                        <div id="ddlAssetId" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">贸易方向：</span>
                        <input type="hidden" id="hidTradeDirection" runat="server" />
                        <div id="ddlTradeDirection" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">贸易境区：</span>
                        <input type="hidden" id="hidTradeBorder" runat="server" />
                        <div id="ddlTradeBorder" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">内外部：</span>
                        <input type="hidden" id="hidContractInOut" runat="server" />
                        <div id="ddlContractInOut" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">长零单：</span>
                        <input type="hidden" id="hidContractLimit" runat="server" />
                        <div id="ddlContractLimit" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">公司：</span>
                        <div id="ddlCorpId" style="float: left;"></div>
                    </li>
                    <li>
                        <input type="button" id="btnSearch" value="查询" />
                    </li>
                </ul>
            </div>

            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
</body>
</html>
