<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomsClearanceApplyUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.CustomsClearanceApplyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报关申请修改</title>
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
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxExpander").jqxExpander({ width: "98%" });

            var stockIds = new Array();
            var sids = $("#hidsids").val();
            var splitItem = sids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIds.push(splitItem[i]);
                }
            }
            var assetId = 0;
            var corpId = 0;
            var muId = 0;

            /////////////////////////报关申请信息/////////////////////////

            var sids = "";
            for (i = 0; i < stockIds.length; i++) {
                if (i != 0) { sids += ","; }
                sids += stockIds[i];
            }

            var url = "Handler/CustomApplySelectStockListHandler.ashx?sids=" + sids;
            var formatedData = "";
            var totalrecords = 0;
            var Infosource =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpId", type: "int" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "Bundles", type: "int" },
                   { name: "UintId", type: "int" },
                   { name: "CurGrossAmountName", type: "string" },
                   { name: "CurNetAmountName", type: "string" },
                   { name: "CurGrossAmount", type: "number" },
                   { name: "CurNetAmount", type: "number" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" }
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
                sortcolumn: "sto.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockId";
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
                  { text: "入库时间", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "所属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "当前毛重", datafield: "CurGrossAmountName" },
                  { text: "当前净重", datafield: "CurNetAmountName" },
                  { text: "报关状态", datafield: "CustomsTypeName" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxApplyGrid").jqxGrid("getrowdata", row);
                          var index = stockIds.indexOf(dataRecord.StockId);
                          stockIds.splice(index, 1);

                          var sids = "";
                          for (i = 0; i < stockIds.length; i++) {
                              if (i != 0) { sids += ","; }
                              sids += stockIds[i];
                          }

                          //刷新列表
                          Infosource.url = "Handler/CustomApplySelectStockListHandler.ashx?sids=" + sids;
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");

                          if (sids != "") {
                              source.url = "Handler/CustomApplyStockListHandler.ashx?refNo=" + $("#txbRefNo").val() + "&sids=" + sids + "&corpId=" + corpId + "&assetId=" + assetId + "&unitId=" + muId;
                              $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                          }
                          else {
                              source.url = "Handler/CustomApplyStockListHandler.ashx?refNo=" + $("#txbRefNo").val() + "&sids=" + sids;
                              $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");

                              $("#ddlCorpId").jqxComboBox({ disabled: false });
                              corpId = 0;

                              $("#ddlAssetId").jqxComboBox({ disabled: false });
                              assetId = 0;

                              $("#ddlMUId").jqxComboBox({ disabled: false });
                              muId = 0;
                          }
                      }
                  }
                ]
            });

            /////////////////////////可报关库存信息/////////////////////////

            //业务单号
            $("#txbRefNo").jqxInput({ height: 23, width: 120 });

            //所属公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlCorpId").jqxComboBox("val", "<%=this.customsClearanceApply.OutCorpId%>");
            $("#ddlCorpId").jqxComboBox({ disabled: true });
            corpId = "<%=this.customsClearanceApply.OutCorpId%>";

            //品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", datafields: [{ name: "AssetId" }, { name: "AssetName" }], url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 100, height: 25 });
            $("#ddlAssetId").jqxComboBox("val", "<%=this.customsClearanceApply.AssetId%>");
            $("#ddlAssetId").jqxComboBox({ disabled: true });
            assetId = "<%=this.customsClearanceApply.AssetId%>";

            //计量单位
            var ddlMUIdurl = "../BasicData/Handler/MUDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlMUId").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "MUName", valueMember: "MUId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlMUId").jqxComboBox("val", "<%=this.customsClearanceApply.UnitId%>");
            $("#ddlMUId").jqxComboBox({ disabled: true });
            muId = "<%=this.customsClearanceApply.UnitId%>";

            $("#btnSearch").jqxButton({ height: 25, width: 50 });
            $("#btnSearch").click(function () {
                var refNo = $("#txbRefNo").val();
                source.url = "Handler/CustomApplyStockListHandler.ashx?refNo=" + refNo + "&sids=" + sids + "&corpId=" + corpId + "&assetId=" + assetId + "&unitId=" + muId;
                $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
            });

            var url = "Handler/CustomApplyStockListHandler.ashx?sids=" + sids;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "StockDate", type: "date" },
                   { name: "RefNo", type: "string" },
                   { name: "CorpId", type: "int" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetId", type: "int" },
                   { name: "AssetName", type: "string" },
                   { name: "Bundles", type: "int" },
                   { name: "UintId", type: "int" },
                   { name: "CurGrossAmountName", type: "string" },
                   { name: "CurNetAmountName", type: "string" },
                   { name: "CurGrossAmount", type: "number" },
                   { name: "CurNetAmount", type: "number" },
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "CustomsTypeName", type: "string" },
                   { name: "StatusName", type: "string" }
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
                sortcolumn: "sto.StockId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sto.StockId";
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
                  { text: "入库时间", datafield: "StockDate", cellsformat: "yyyy-MM-dd" },
                  { text: "所属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "捆数", datafield: "Bundles" },
                  { text: "当前毛重", datafield: "CurGrossAmountName" },
                  { text: "当前净重", datafield: "CurNetAmountName" },
                  { text: "报关状态", datafield: "CustomsTypeName" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "报关";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxStockGrid").jqxGrid("getrowdata", row);

                          var item = $("#jqxStockGrid").jqxGrid("getrowdata", row);
                          stockIds.push(item.StockId);

                          var sids = "";
                          for (i = 0; i < stockIds.length; i++) {
                              if (i != 0) { sids += ","; }
                              sids += stockIds[i];
                          }

                          $("#ddlCorpId").jqxComboBox("val", item.CorpId);
                          $("#ddlCorpId").jqxComboBox({ disabled: true });
                          corpId = item.CorpId;

                          $("#ddlAssetId").jqxComboBox("val", item.AssetId);
                          $("#ddlAssetId").jqxComboBox({ disabled: true });
                          assetId = item.AssetId;

                          $("#ddlMUId").jqxComboBox("val", item.UintId);
                          $("#ddlMUId").jqxComboBox({ disabled: true });
                          muId = item.UintId;

                          //刷新列表
                          Infosource.url = "Handler/CustomApplySelectStockListHandler.ashx?sids=" + sids;
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                          source.url = "Handler/CustomApplyStockListHandler.ashx?refNo=" + $("#txbRefNo").val() + "&sids=" + sids + "&corpId=" + corpId + "&assetId=" + assetId + "&unitId=" + muId;
                          $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
                      }
                  }
                ]
            });

            source.url = "Handler/CustomApplyStockListHandler.ashx?refNo=" + $("#txbRefNo").val() + "&sids=" + sids + "&corpId=" + corpId + "&assetId=" + assetId + "&unitId=" + muId;
            $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //备注
            $("#txbMemo").jqxInput({ height: 25, width: 400 });
            $("#txbMemo").jqxInput("val", "<%=this.apply.ApplyDesc%>");

            //报关单价
            $("#nbCustomsPrice").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 4, Digits: 9 });
            $("#nbCustomsPrice").jqxNumberInput("val", "<%=this.customsClearanceApply.CustomsPrice%>");

            //币种
            var cySource = { datatype: "json", url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlCurrencyId").jqxComboBox({ source: cyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25});
            $("#ddlCurrencyId").jqxComboBox("val", "<%=this.customsClearanceApply.CurrencyId%>");

            //关内公司
            $("#ddlInCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlInCorpId").jqxComboBox("val", "<%=this.customsClearanceApply.InCorpId%>");

            //报关公司
            $("#ddlCustomsCorpId").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlCustomsCorpId").jqxComboBox("val", "<%=this.customsClearanceApply.CustomsCorpId%>");

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlApplyDeptId", message: "请选择申请部门", action: "keyup,blur", rule: function (input, commit) {
                                return $("#ddlApplyDeptId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#nbCustomsPrice", message: "报关单价必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $("#nbCustomsPrice").jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#ddlCurrencyId", message: "请选择币种", action: "keyup,blur", rule: function (input, commit) {
                                return $("#ddlCurrencyId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlInCorpId", message: "请选择关内公司", action: "keyup,blur", rule: function (input, commit) {
                                return $("#ddlInCorpId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlCustomsCorpId", message: "请选择报关公司", action: "keyup,blur", rule: function (input, commit) {
                                return $("#ddlCustomsCorpId").jqxComboBox("val") > 0;
                            }
                        }
                    ]
            });

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确定提交修改？")) { return; }

                if (stockIds.length == 0) { alert("必须选择报关库存！"); return; }

                var sids = "";
                for (i = 0; i < stockIds.length; i++) {
                    if (i != 0) { sids += ","; }
                    sids += stockIds[i];
                }

                var apply = {
                    ApplyId: "<%=this.apply.ApplyId%>",
                    ApplyDept: $("#ddlApplyDeptId").val(),
                    ApplyDesc: $("#txbMemo").val()
                };
                var customApply = {
                    CustomsApplyId: "<%=this.customsClearanceApply.CustomsApplyId%>",
                    ApplyId: "<%=this.apply.ApplyId%>",
                    AssetId: assetId,
                    //GrossWeight
                    //NetWeight
                    UnitId: muId,
                    OutCorpId: corpId,
                    InCorpId: $("#ddlInCorpId").val(),
                    CustomsCorpId: $("#ddlCustomsCorpId").val(),
                    CustomsPrice: $("#nbCustomsPrice").val(),
                    CurrencyId: $("#ddlCurrencyId").val()
                };
                var rows = $("#jqxApplyGrid").jqxGrid("getrows");

                //附件信息
                var fileIds = new Array();
                var files = $(":file");
                for (i = 0; i < files.length - 1; i++) {
                    var item = files[i];
                    fileIds.push(item.id);
                }

                $.post("Handler/CustomApplyUpdate.ashx", {
                    apply: JSON.stringify(apply),
                    customApply: JSON.stringify(customApply),
                    rows: JSON.stringify(rows)
                },
                    function (result) {
                        var obj = JSON.parse(result);

                        if (obj.ResultStatus.toString() == "0") {
                            AjaxFileUpload(fileIds, obj.ReturnValue, AttachTypeEnum.CustomApplyAttach);
                        }
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "CustomsClearanceApplyList.aspx";
                        }
                    }
                );
            });
        });

        function bntRemoveOnClick(row) {
            if (!confirm("确认删除？")) { return; }
            var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
            if (item.AttachId == "undefined" || item.AttachId <= 0) return;
            $.post("../Files/Handler/AttachUpdateStatusHandler.ashx", { aid: item.AttachId, s: statusEnum.已作废 },
                    function (result) {
                        attachSource.url = "../Files/Handler/GetAttachListHandler.ashx?cid=" + "<%=this.customsClearanceApply.CustomsApplyId%>" + "&s=" + statusEnum.已生效 + "&t=" + AttachTypeEnum.CustomApplyAttach;
                        $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");
                    }
                );
        }
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关申请信息<input type="hidden" id="hidsids" runat="server" />
        </div>
        <div style="height: 500px;">
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可报关库存信息
        </div>
        <div style="height: 500px;">
            <div class="SearchExpander">
                <ul>
                    <li>
                        <span style="float: left;">业务单号：</span>
                        <input type="text" id="txbRefNo" style="float: left;" />
                    </li>
                    <li>
                        <span style="float: left;">所属公司：</span>
                        <div id="ddlCorpId" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">品种：</span>
                        <div id="ddlAssetId" style="float: left;"></div>
                    </li>
                    <li>
                        <span style="float: left;">计量单位：</span>
                        <div id="ddlMUId" style="float: left;"></div>
                    </li>
                    <li>
                        <input type="button" id="btnSearch" value="查询" />
                    </li>
                </ul>
            </div>
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            报关申请修改
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">申请部门：</span>
                    <div id="ddlApplyDeptId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">报关单价：</span>
                    <div id="nbCustomsPrice"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">币种：</span>
                    <div id="ddlCurrencyId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">关内公司：</span>
                    <div id="ddlInCorpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">报关公司：</span>
                    <div id="ddlCustomsCorpId" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">备注：</span>
                    <span>
                        <input type="text" id="txbMemo" /></span>
                </li>
            </ul>
        </div>
    </div>

    <NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="CustomApplyAttach" />

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <a target="_self" runat="server" href="CustomsClearanceApplyList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
