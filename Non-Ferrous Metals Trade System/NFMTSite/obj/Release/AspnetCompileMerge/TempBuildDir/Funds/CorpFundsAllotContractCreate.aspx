<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpFundsAllotContractCreate.aspx.cs" Inherits="NFMTSite.Funds.CorpFundsAllotContractCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>集团或公司款分配至合约新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            

            //Expander Init
            $("#jqxConSubExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxCanAllotExpander").jqxExpander({ width: "98%" });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            var subId = $("#hidsubId").val();

            //////////////////////已分配情况//////////////////////
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datatype: "json",
                datafields:
                [
                   { name: "ReceivableAllotId", type: "int" },
                   { name: "AllotTime", type: "date" },
                   { name: "Name", type: "string" },
                   { name: "AllotBala", type: "string" },
                   { name: "StatusName", type: "string" }
                ],
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
                sortcolumn: "ra.ReceivableAllotId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ra.ReceivableAllotId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/ReceivableAllotListHandler.ashx?subId=" + subId
            };
            var DataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxGrid").jqxGrid(
            {
                width: "98%",
                source: DataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "分配日期", datafield: "AllotTime", cellsformat: "yyyy-MM-dd" },
                  { text: "分配人", datafield: "Name" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "收款状态", datafield: "StatusName" }
                ]
            });


            //////////////////////已选额分配//////////////////////
            formatedData = "";
            totalrecords = 0;
            selectSource =
            {
                localdata: details,
                datafields: [
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" }
                ],
                datatype: "json"
            };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxSelectGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "ReceiveDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InnerCorp" },
                  { text: "收款银行", datafield: "BankName" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "分配金额", datafield: "CanAllotBala" },
                  { text: "操作", datafield: "ReceivableId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });



            //////////////////////可分配收款登记//////////////////////
            formatedData = "";
            totalrecords = 0;

            var rids = "";
            for (i = 0; i < ReceivableIds.length; i++) {
                if (i != 0) { rids += ","; }
                rids += ReceivableIds[i];
            }
            //var url = 

            canAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" }
                ],
                sort: function () {
                    $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "sort");
                },
                filter: function () {
                    $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "filter");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "r.ReceivableId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "r.ReceivableId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CanAllotReceivalbeListHandler.ashx?subId=" + subId + "&rids=" + rids
            };
            var canAllotDataAdapter = new $.jqx.dataAdapter(canAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + row + "," + value + ");\" />"
                   + "</div>";
            }
            $("#jqxCanAllotGrid").jqxGrid(
            {
                width: "98%",
                source: canAllotDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                editable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "ReceiveDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InnerCorp", editable: false },
                  { text: "收款银行", datafield: "BankName", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款金额", datafield: "PayBala", editable: false },
                  {
                      text: "分配金额", datafield: "CanAllotBala", columntype: "numberinput", validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "分配金额不能大于收款金额" };
                          }
                          return true;
                      }
                  },
                  { text: "操作", datafield: "ReceivableId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            $("#btnAdd").click(function () {

                if (ReceivableIds.length == 0) { alert("未分配任何收款！"); return; }

                var rows = $("#jqxSelectGrid").jqxGrid("getrows");

                $.post("Handler/ContractReceivableCreateHandler.ashx", {
                    Allot: JSON.stringify(rows),
                    memo: $("#txbMemo").val(),
                    subId: subId,
                    contractId: $("#hidcontractId").val(),
                    curId: $("#hidCurrucyId").val()
                },
                   function (result) {
                       alert(result);
                       document.location.href = "ContractReceivableList.aspx";
                   }
               );
            });
        });

        var details = new Array();//保存Grid信息
        var ReceivableIds = new Array();//保存ReceivableId信息

        function bntRemoveOnClick(row, value) {
            //var index = details.indexOf(row);
            //details.splice(index, 1);
            details.splice(row, 1);

            index = ReceivableIds.indexOf(value);
            ReceivableIds.splice(index, 1);
            flushGrid();
        }

        function bntAddOnClick(row, value) {
            var item = $("#jqxCanAllotGrid").jqxGrid("getrowdata", row);
            details.push(item);

            ReceivableIds.push(value);
            flushGrid();
        }

        function flushGrid() {
            selectSource.localdata = details;
            $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

            var rids = "";
            for (i = 0; i < ReceivableIds.length; i++) {
                if (i != 0) { rids += ","; }
                rids += ReceivableIds[i];
            }
            var subId = $("#hidsubId").val();
            canAllotSource.url = "Handler/CanAllotReceivalbeListHandler.ashx?subId=" + subId + "&rids=" + rids;
            $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConSubExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            子合约信息
                        <input type="hidden" id="hidsubId" runat="server" />
            <input type="hidden" id="hidcontractId" runat="server" />
            <input type="hidden" id="hidCurrucyId" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>子合约编号：</strong>
                    <span runat="server" id="spnSubNo"></span>
                </li>
                <li><strong>子合约数量：</strong>
                    <span runat="server" id="spnSubSignAmount"></span>
                </li>
                <li>
                    <strong>执行最终日：</strong>
                    <span runat="server" id="spnPeriodE"></span>
                </li>
                <li><strong>已分配金额：</strong>
                    <span runat="server" id="spanAllotAmount"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            已分配情况
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxSelectExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            已选择分配
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxCanAllotExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            可分配收款登记
        </div>
        <div id="layOutDiv">
            <div id="jqxCanAllotGrid"></div>
            <div>
                <span style="text-align: right; margin: 5px 2px 5px 0px;">备注：</span>
                <textarea id="txbMemo" runat="server"></textarea>
            </div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnAdd" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="ContractReceivableList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
