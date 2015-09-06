<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotCorpUpdate.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotCorpUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司收款分配新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var selectSource;
        var allotSource;

        var details = new Array();//分配集合
        var cashIns = new Array();//收款集合
        var currencyId = <%=this.curAllot.CurrencyId%>;

        $(document).ready(function () {
            

            //Expander Init
            $("#jqxCorpExpander").jqxExpander({ width: "98%", expanded: false });

            $("#jqxOtherExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxAllotExpander").jqxExpander({ width: "98%" });
            $("#jqxInfoExpander").jqxExpander({ width: "98%" });

            //////////////////////已分配情况 OtherExpander//////////////////////
            var formatedData = "";
            var totalrecords = 0;
            var otherSource =
            {
                datatype: "json",
                datafields:
                [
                   { name: "AllotId", type: "int" },
                   { name: "AllotTime", type: "date" },
                   { name: "AlloterName", type: "string" },
                   { name: "AllotBala", type: "number" },
                   { name: "CurrencyId", type: "int" },
                   { name: "CurrencyName", type: "string" },
                   { name: "AllotStatus", type: "int" },
                   { name: "StatusName", type: "string" }
                ],
                sort: function () {
                    $("#jqxOtherGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",               
                formatdata: function (data) {
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CashInCorpOtherAllotListHandler.ashx?cid=" + "<%=this.curCorp.CorpId%>" + "&aid="+ "<%=this.curAllot.AllotId%>"
            };
            var otherDataAdapter = new $.jqx.dataAdapter(otherSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxOtherGrid").jqxGrid(
            {
                width: "98%",
                source: otherDataAdapter,
                enabletooltips: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "分配日期", datafield: "AllotTime", cellsformat: "yyyy-MM-dd" },
                  { text: "分配人", datafield: "AlloterName" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "币种", datafield: "CurrencyName" },
                  { text: "分配状态", datafield: "StatusName" }
                ]
            });

            //////////////////////已选择分配 SelectExpander //////////////////////
            formatedData = "";
            totalrecords = 0;
            selectSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "AllotBala", type: "number" }
                ],
                localdata: <%=this.curSelectedStr%>
                };
            var selectDataAdapter = new $.jqx.dataAdapter(selectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var removeRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxSelectGrid").jqxGrid(
            {
                width: "98%",
                source: selectDataAdapter,
                enabletooltips: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InCorp", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款银行", datafield: "CashInBankName", editable: false },
                  { text: "收款金额", datafield: "CashInBala" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "CashInId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            cashIns = $("#jqxSelectGrid").jqxGrid("getrows");

            var ciIds = "";
            for (i = 0; i < cashIns.length; i++) {
                if (i != 0) { ciIds += ","; }
                ciIds += cashIns[i].CashInId;
            }

            //////////////////////可分配收款登记 AllotExpander//////////////////////           

            formatedData = "";
            totalrecords = 0;
            allotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "LastBala", type: "number" },
                    { name: "AllotBala", type: "number" }
                ],
                sort: function () {
                    $("#jqxAllotGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "ci.CashInId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "ci.CashInId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CashInLastListHandler.ashx?cashInIds="+ciIds
            };
            var allotDataAdapter = new $.jqx.dataAdapter(allotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var addRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntAddOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxAllotGrid").jqxGrid(
            {
                width: "98%",
                source: allotDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                enabletooltips: true,
                selectionmode: "singlecell",
                sortable: true,
                editable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InCorp", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款银行", datafield: "CashInBankName", editable: false },
                  { text: "收款金额", datafield: "CashInBala", editable: false },
                  { text: "剩余金额", datafield: "LastBala", editable: false },
                  {
                      text: "分配金额", datafield: "AllotBala", columntype: "numberinput", cellclassname: "GridFillNumber", validation: function (cell, value) {
                          if (value > cell.value) {
                              return { result: false, message: "分配金额不能大于剩余金额" };
                          }
                          return true;
                      }, sortable: false
                  },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "CashInId", cellsrenderer: addRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            //对方公司
            $("#txbCorp").jqxInput({ height: 25, width: 180 });

            //币种
            var currencyUrl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var currencySource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyUrl, async: false };
            var currencyDataAdapter = new $.jqx.dataAdapter(currencySource);
            $("#ddlCurrency").jqxComboBox({ source: currencyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25, autoDropDownHeight: true });
            $("#txbMemo").jqxInput({ width: "500" });
            $("#chbIsShare").jqxCheckBox({ width: 200, height: 25 });

            $("#txbMemo").val("<%=this.curAllot.AllotDesc%>");
            if ("<%=this.isShare.ToString().ToLower()%>"=="true") {
                $("#chbIsShare").jqxCheckBox("check");
            }

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnSearch").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").click(function () {

                if (cashIns.length == 0) { alert("未分配任何收款！"); return; }

                var rows = $("#jqxSelectGrid").jqxGrid("getrows");

                $.post("Handler/CashInAllotCorpUpdateHandler.ashx", {
                    Details: JSON.stringify(rows),
                    Memo: $("#txbMemo").val(),
                    AllotId: "<%=this.curAllot.AllotId%>",
                    IsShare: $("#chbIsShare").val()
                },
                   function (result) {
                       alert(result);
                       document.location.href = "CashInAllotCorpList.aspx";
                   }
               );
            });

            $("#btnSearch").click(function () {
                flushGrid();
            });
        });

        function bntRemoveOnClick(row) {

            var item = cashIns[row];

            if (item.DetailId != undefined && item.DetailId > 0) {
                //增加排除收款分配
                details.push(item);
            }

            //删除收款登记列表
            cashIns.splice(row, 1);

            if (cashIns.length == 0) {
                currencyId = 0;
            }

            flushGrid();
        }

        function bntAddOnClick(row) {

            var item = $("#jqxAllotGrid").jqxGrid("getrowdata", row);

            if (currencyId == 0) {
                currencyId = item.CurrencyId;
            }

            if (item.CurrencyId != currencyId) {
                alert("必须选择相同币种的收款登记");
                return;
            }

            //添加收款登记列表
            cashIns.push(item);

            flushGrid();
        }

        function flushGrid() {
            selectSource.localdata = cashIns;
            $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

            //收款登记序号拼接
            var ciIds = "";
            for (i = 0; i < cashIns.length; i++) {
                if (i != 0) { ciIds += ","; }
                ciIds += cashIns[i].CashInId;
            }

            //收款分配明细序号拼接
            var adIds = "";
            for (i = 0; i < details.length; i++) {
                if (i != 0) { adIds += ","; }
                adIds += details[i].DetailId;
            }

            var selCurId = $("#ddlCurrency").val();
            var corpName = $("#txbCorp").val();
            allotSource.url = "Handler/CashInLastListHandler.ashx?cashInIds=" + ciIds + "&detailIds=" + adIds;
            $("#jqxAllotGrid").jqxGrid("updatebounddata", "rows");

        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxCorpExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            公司信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>所属集团：</strong>
                    <span><%=this.curCorp.BlocName%></span>
                </li>
                <li><strong>公司代码：</strong>
                    <span><%=this.curCorp.CorpCode%></span>
                </li>
                <li>
                    <strong>公司名称：</strong>
                    <span><%=this.curCorp.CorpName%></span>
                </li>
                <li><strong>纳税人识别号：</strong>
                    <span><%=this.curCorp.TaxPayerId%></span>
                </li>
                <li>
                    <strong>公司地址：</strong>
                    <span><%=this.curCorp.CorpAddress%></span>
                </li>
                <li><strong>公司电话：</strong>
                    <span><%=this.curCorp.CorpTel%></span>
                </li>
                <li>
                    <strong>公司传真：</strong>
                    <span><%=this.curCorp.CorpFax%></span>
                </li>
                <li><strong>公司邮编：</strong>
                    <span><%=this.curCorp.CorpZip%></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxOtherExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            其他分配情况
        </div>
        <div>
            <div id="jqxOtherGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择分配
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可分配收款登记
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <span>币种：</span>
                    <div id="ddlCurrency" style="float: left;"></div>

                    <span style="text-align: right; width: 10%;">对方公司：</span>
                    <input type="text" id="txbCorp" style="float: left;" />

                    <span>&nbsp;</span>
                    <input type="button" value="查询" id="btnSearch" />
                </li>
            </ul>

            <div id="jqxAllotGrid"></div>
        </div>
    </div>

    <div id="jqxInfoExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            分配信息
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <span>备注：</span>
                    <textarea id="txbMemo" runat="server"></textarea>
                </li>
                <li style="margin-top: 5px">
                    <span>是否共享到集团：</span>
                    <input type="hidden" id="hidIsShare" runat="server" />
                    <div id="chbIsShare" style="margin-top: 50px" />
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="CashInCorpList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
