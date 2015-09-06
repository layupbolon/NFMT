<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PledgeApplyCreate.aspx.cs" Inherits="NFMTSite.WareHouse.PledgeApplyCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质押申请新增</title>
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
            var details = new Array();

            /////////////////////////质押申请信息/////////////////////////

            var sids = "";
            for (i = 0; i < stockIds.length; i++) {
                if (i != 0) { sids += ","; }
                sids += stockIds[i];
            }

            var Infosource =
            {
                datafields:
                [
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
                localdata: details
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
                  { text: "毛吨", datafield: "GrossAmount" },
                  { text: "净重", datafield: "NetAmount" },
                  { text: "品牌", datafield: "BrandName" },
                  { text: "交货地", datafield: "DPName" },
                  { text: "卡号", datafield: "CardNo" },
                  { text: "质押价格", datafield: "PledgePrice" },
                  { text: "币种", datafield: "CurrencyName" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxApplyGrid").jqxGrid("getrowdata", row);
                          var index = stockIds.indexOf(dataRecord.StockId);
                          stockIds.splice(index, 1);

                          details.splice(row, 1);

                          var sids = "";
                          for (i = 0; i < stockIds.length; i++) {
                              if (i != 0) { sids += ","; }
                              sids += stockIds[i];
                          }

                          //刷新列表
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                          source.url = "Handler/CanPledgeHandler.ashx?sids=" + sids;
                          $("#jqxMoveGrid").jqxGrid("updatebounddata", "rows");
                      }
                  }
                ]
            });

            /////////////////////////可质押库存信息/////////////////////////

            var feeTypeSource =
            {
                datatype: "json",
                datafields: [
                    { name: "CurrencyName", type: "string" },
                    { name: "CurrencyId", type: "int" }
                ], url: "../BasicData/Handler/CurrencDDLHandler.ashx", async: false
            };
            var feeTypeAdapter = new $.jqx.dataAdapter(feeTypeSource, { autoBind: true });

            var url = "Handler/CanPledgeHandler.ashx";
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
                   { name: "BrandName", type: "string" },
                   { name: "DPName", type: "string" },
                   { name: "CardNo", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "ApplyQty", type: "number" },
                   { name: "UintId", type: "int" },
                   { name: "NetAmount", type: "string" },
                   { name: "CurrencyName", value: "CurrencyId", values: { source: feeTypeAdapter.records, value: "CurrencyId", name: "CurrencyName" } },
                   { name: "CurrencyId", type: "int" },
                   { name: "PledgePrice", type: "number" }
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
                editable: true,
                selectionmode: "singlecell",
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "业务单号", datafield: "RefNo", editable: false },
                  { text: "权证编号", datafield: "PaperNo", editable: false },
                  { text: "品种", datafield: "AssetName", editable: false },
                  { text: "毛吨", datafield: "GrossAmount", editable: false },
                  { text: "净重", datafield: "NetAmount", editable: false },
                  { text: "品牌", datafield: "BrandName", editable: false },
                  { text: "交货地", datafield: "DPName", editable: false },
                  { text: "卡号", datafield: "CardNo", editable: false },
                  {
                      text: "质押价格", datafield: "PledgePrice", columntype: "numberinput", createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ decimalDigits: 2, spinButtons: true });
                      }, sortable: false, cellclassname: "GridFillNumber"
                  },
                  {
                      text: "币种", datafield: "CurrencyId", displayfield: "CurrencyName", columntype: "combobox",
                      createeditor: function (row, cellvalue, editor, cellText, width, height) {
                          editor.jqxComboBox({ source: feeTypeAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId" });
                      }, sortable: false, cellclassname: "GridFillNumber"
                  },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: 90, cellsrenderer: function () {
                          return "质押";
                      }, buttonclick: function (row) {
                          var dataRecord = $("#jqxMoveGrid").jqxGrid("getrowdata", row);
                          if (dataRecord.PledgePrice == undefined || dataRecord.CurrencyId == undefined || dataRecord.PledgePrice == 0 || dataRecord.CurrencyId == "") {
                              alert("请填写质押价格和币种");
                              return;
                          }

                          var item = $("#jqxMoveGrid").jqxGrid("getrowdata", row);
                          stockIds.push(dataRecord.StockId);

                          var sids = "";
                          for (i = 0; i < stockIds.length; i++) {
                              if (i != 0) { sids += ","; }
                              sids += stockIds[i];
                          }

                          //刷新列表
                          details.push(item);
                          $("#jqxApplyGrid").jqxGrid("updatebounddata", "rows");
                          source.url = "Handler/CanPledgeHandler.ashx?sids=" + sids;
                          $("#jqxMoveGrid").jqxGrid("updatebounddata", "rows");
                      }
                  }
                ]
            });

            //申请部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlApplyDeptId").jqxComboBox({ selectedIndex: 0, source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlApplyDeptId").jqxComboBox("val", "<%=this.curDeptId%>");

            //申请公司
            var ownCorpUrl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ownCorpSource = { datatype: "json", url: ownCorpUrl, async: false };
            var ownCorpDataAdapter = new $.jqx.dataAdapter(ownCorpSource);
            $("#ddlApplyCorpId").jqxComboBox({
                source: ownCorpDataAdapter, displayMember: "CorpName", valueMember: "CorpId", searchMode: "containsignorecase"
            });

            //质押银行
            var ddlPledgeBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlPledgeBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlPledgeBankurl, async: false };
            var ddlPledgeBankdataAdapter = new $.jqx.dataAdapter(ddlPledgeBanksource);
            $("#ddlPledgeBank").jqxComboBox({ selectedIndex: 0, source: ddlPledgeBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25 });

            $("#txbMemo").jqxInput({ height: 25, width: 400 });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnAdd").on("click", function () {
                if (!confirm("确定提交质押申请？")) { return; }

                if (stockIds.length == 0) { alert("必须选择质押库存！"); return; }

                $("#btnAdd").jqxButton({ disabled: true });

                var apply = {
                    ApplyDept: $("#ddlApplyDeptId").val(),
                    ApplyCorp: $("#ddlApplyCorpId").val(),
                    ApplyDesc: $("#txbMemo").val()
                }

                var pledgeApply = {
                    //ApplyId
                    PledgeBank: $("#ddlPledgeBank").val()
                }

                var rows = $("#jqxApplyGrid").jqxGrid("getrows");

                $.post("Handler/PledgeApplyCreateHandler.ashx", {
                    rows: JSON.stringify(rows),
                    apply: JSON.stringify(apply),
                    pledgeApply: JSON.stringify(pledgeApply)
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "PledgeApplyList.aspx";
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
            质押申请信息
        </div>
        <div style="height: 500px;">

            <div id="jqxApplyGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>
    <div id="jqxMoveExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可质押库存信息
        </div>
        <div style="height: 500px;">
            <div id="jqxMoveGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            质押申请新增
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
                    <span style="width: 15%; text-align: right;">质押银行：</span>
                    <div id="ddlPledgeBank"></div>
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
                    <span><a target="_self" runat="server" href="StockMoveApplyList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
