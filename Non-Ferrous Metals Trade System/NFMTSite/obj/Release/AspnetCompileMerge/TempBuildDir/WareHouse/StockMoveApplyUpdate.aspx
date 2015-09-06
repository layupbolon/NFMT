<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockMoveApplyUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.StockMoveApplyUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>移库申请修改</title>
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
            var sids = $("#hidsids").val();
            var splitItem = sids.split(',');
            for (i = 0; i < splitItem.length; i++) {
                if (splitItem[i].length > 0) {
                    stockIds.push(splitItem[i]);
                }
            }

            var stockMoveApplyId = $("#hidstockMoveApplyId").val();

            /////////////////////////移库申请信息/////////////////////////

            var url = "Handler/StockMoveApplyInfoHandler.ashx?mode=2&sids=" + sids + "&applyId=" + stockMoveApplyId;
            var Infosource =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "DeliverPlaceId", type: "int" },
                   { name: "DeliverPlace", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" }
                ],
                datatype: "json",
                url: url,
                type: "GET",
                addrow: function (rowid, rowdata, position, commit) {
                    commit(true);
                },
                deleterow: function (rowid, commit) {
                    commit(true);
                },
                updaterow: function (rowid, newdata, commit) {
                    commit(true);
                }
            };
            var infodataAdapter = new $.jqx.dataAdapter(Infosource);

            $("#jqxApplyGrid").jqxGrid(
            {
                width: "98%",
                source: infodataAdapter,
                autoheight: true,
                enabletooltips: true,
                //selectionmode: "singlecell",
                columns: [
                  { text: "业务单号", datafield: "RefNo" },
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DeliverPlace" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "权证编号", datafield: "PaperNo" },
                   {
                       text: "操作", datafield: "Edit", width: 70, columntype: "button", width: 90, cellsrenderer: function () {
                           return "取消";
                       }, buttonclick: function (row) {
                           var rowscount = $("#jqxApplyGrid").jqxGrid("getdatainformation").rowscount;
                           if (row >= 0 && row < rowscount) {
                               var dataRecord = $("#jqxApplyGrid").jqxGrid("getrowdata", row);
                               var id = $("#jqxApplyGrid").jqxGrid("getrowid", row);
                               var commit = $("#jqxApplyGrid").jqxGrid("deleterow", id);

                               var index = stockIds.indexOf(dataRecord.StockId);
                               stockIds.splice(index, 1);

                               var sids = "";
                               for (i = 0; i < stockIds.length; i++) {
                                   if (i != 0) { sids += ","; }
                                   sids += stockIds[i];
                               }

                               source.url = "Handler/CanStockMoveListHandler.ashx?sids=" + sids;
                               $("#jqxMoveGrid").jqxGrid("updatebounddata", "rows");
                           }
                       }
                   }
                ]
            });

            /////////////////////////可移库库存信息/////////////////////////

            var url = "Handler/CanStockMoveListHandler.ashx?sids=" + sids;
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "NetAmount", type: "string" },
                   { name: "PaperNo", type: "string" },
                   { name: "DeliverPlaceId", type: "int" },
                   { name: "DeliverPlace", type: "string" },
                   { name: "BrandName", type: "string" },
                   { name: "CardNo", type: "string" },
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
                  { text: "品种", datafield: "AssetName" },
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DeliverPlace" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "权证编号", datafield: "PaperNo" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "移库";
                      }, buttonclick: function (row) {
                          editrow = row;
                          var offset = $("#jqxMoveGrid").offset();
                          $("#popupWindow").jqxWindow({ position: { x: parseInt(offset.left) + 500, y: parseInt(offset.top) + 140 } });
                          var dataRecord = $("#jqxMoveGrid").jqxGrid("getrowdata", editrow);
                          $("#hidStockId").val(dataRecord.StockId)
                          $("#txbRefNo").val(dataRecord.RefNo);
                          $("#txbAsset").val(dataRecord.AssetName);
                          $("#txbGrossAmount").val(dataRecord.GrossAmount);
                          $("#txbNetAmount").val(dataRecord.NetAmount);
                          $("#ddlDeliverPlaceId").jqxComboBox("val", dataRecord.DeliverPlaceId);
                          $("#txbPaperNo").val(dataRecord.PaperNo);

                          $("#popupWindow").jqxWindow("open");
                      }
                  }
                ]
            });


            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ selectedIndex: 0, source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").jqxComboBox("val", "<%=this.apply.ApplyDept%>");

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorpId").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });
            $("#ddlApplyCorpId").jqxComboBox("val", "<%=this.apply.ApplyCorp%>");

            $("#txbMemo").jqxInput({ height: 25, width: 400 });

            //////////////////////////// 弹窗 ////////////////////////////

            //业务单号
            $("#txbRefNo").jqxInput({ height: 25, width: 120, disabled: true });

            //品种
            $("#txbAsset").jqxInput({ height: 25, width: 120, disabled: true });

            //毛重
            $("#txbGrossAmount").jqxInput({ height: 25, width: 120, disabled: true });

            //净重
            $("#txbNetAmount").jqxInput({ height: 25, width: 120, disabled: true });

            //交货地
            var ddlDeliverPlaceIdurl = "../BasicData/Handler/DeliverPlaceDDLHandler.ashx";
            var ddlDeliverPlaceIdsource = { datatype: "json", datafields: [{ name: "DPId" }, { name: "DPName" }], url: ddlDeliverPlaceIdurl, async: false };
            var ddlDeliverPlaceIddataAdapter = new $.jqx.dataAdapter(ddlDeliverPlaceIdsource);
            $("#ddlDeliverPlaceId").jqxComboBox({ selectedIndex: 0, source: ddlDeliverPlaceIddataAdapter, displayMember: "DPName", valueMember: "DPId", width: 180, height: 25 });

            //权证编号
            $("#txbPaperNo").jqxInput({ height: 25, width: 120 });

            $("#popupWindow").jqxWindow({ width: 350, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.01 });
            $("#Cancel").jqxButton({ height: 25, width: 100 });
            $("#Save").jqxButton({ height: 25, width: 100 });

            //验证器
            $("#popupWindow").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlDeliverPlaceId", message: "交货地不能为空", action: "change", rule: function (input, commit) {
                                return $("#ddlDeliverPlaceId").jqxComboBox("val") > 0;
                            }
                        }
                    ]
            });

            $("#Save").click(function () {
                var isCanSubmit = $("#popupWindow").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var item = $("#ddlDeliverPlaceId").jqxComboBox("getItemByValue", $("#ddlDeliverPlaceId").val());

                var datarow = {
                    StockId: $("#hidStockId").val(),
                    RefNo: $("#txbRefNo").val(),
                    AssetName: $("#txbAsset").val(),
                    GrossAmount: $("#txbGrossAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    PaperNo: $("#txbPaperNo").val(),
                    DeliverPlace: item.label,
                    DeliverPlaceId: $("#ddlDeliverPlaceId").val()
                };
                var commit = $("#jqxApplyGrid").jqxGrid("addrow", null, datarow);
                $("#popupWindow").jqxWindow("hide");


                stockIds.push($("#hidStockId").val());

                var sids = "";
                for (i = 0; i < stockIds.length; i++) {
                    if (i != 0) { sids += ","; }
                    sids += stockIds[i];
                }

                source.url = "Handler/CanStockMoveListHandler.ashx?sids=" + sids;
                $("#jqxMoveGrid").jqxGrid("updatebounddata", "rows");

            });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnAdd").on("click", function () {
                if (!confirm("确定提交移库申请的修改？")) { return; }

                if (stockIds.length == 0) { alert("必须选择移库库存！"); return; }
                $("#btnAdd").jqxButton({ disabled: true });

                var apply = {
                    ApplyId: "<%=this.apply.ApplyId%>",
                    ApplyDept: $("#ddlApplyDeptId").val(),
                    ApplyCorp: $("#ddlApplyCorpId").val(),
                    ApplyDesc: $("#txbMemo").val()
                };

                var rows = $("#jqxApplyGrid").jqxGrid("getrows");

                $.post("Handler/StockMoveApplyUpdateHandler.ashx", {
                    stockMove: JSON.stringify(rows),
                    apply: JSON.stringify(apply),
                    aid: stockMoveApplyId
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockMoveApplyList.aspx";
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
            移库申请信息
        </div>
        <div style="height: 500px;">
            <input type="hidden" id="hidsids" runat="server" />
            <input type="hidden" id="hidstockMoveApplyId" runat="server" />
            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxMoveExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可移库库存信息
        </div>
        <div style="height: 500px;">
            <div id="jqxMoveGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            移库申请修改
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">申请公司：</span>
                    <div id="ddlApplyCorpId"></div>
                </li>
                <li>
                    <input type="hidden" id="hidDeptId" runat="server" />
                    <span style="width: 15%; text-align: right;">申请部门：</span>
                    <div id="ddlApplyDeptId"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">备注：</span>
                    <span>
                        <input type="text" id="txbMemo" runat="server" /></span>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="提交" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="StockMoveApplyList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="popupWindow">
        <div>移库</div>
        <div class="DataExpander" style="overflow: hidden;">
            <ul>
                <li>
                    <span>业务单号：</span>
                    <input type="text" id="txbRefNo" />
                    <input type="hidden" id="hidStockId" />
                </li>
                <li>
                    <span>品种：</span>
                    <input type="text" id="txbAsset" />
                </li>
                <li>
                    <span>毛重：</span>
                    <input type="text" id="txbGrossAmount" />
                </li>
                <li>
                    <span>净重：</span>
                    <input type="text" id="txbNetAmount" />
                </li>
                <li>
                    <span>交货地：</span>
                    <div id="ddlDeliverPlaceId" style="float: left;"></div>
                </li>
                <li>
                    <span>权证编号：</span>
                    <input type="text" id="txbPaperNo" />
                </li>
                <li>
                    <input style="margin-right: 5px;" type="button" id="Save" value="确认" />
                    <input id="Cancel" type="button" value="取消" />
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
