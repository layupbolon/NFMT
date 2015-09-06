<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancingRepoApplyCreate.aspx.cs" Inherits="NFMTSite.Financing.FinancingRepoApplyCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>赎回申请单新增</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        var isValid = true;

        var stocksSource = null;
        var stocks = new Array();

        var repoSource = null;
        var repoStocks = new Array();

        var rowIds = new Array();

        var pledgeApplySumNetAmount = 0;
        var aleadyNetAmount = 0;
        var repoNetAmount = 0;

        $(document).ready(function () {
            $("#jqxApplyExpander").jqxExpander({ width: "98%" });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });
            $("#jqxCashExpander").jqxExpander({ width: "98%" });
            $("#jqxRepoExpander").jqxExpander({ width: "98%" });
        });

        function FlushValue(){
            stocksSource.localdata = stocks;
            $("#jqxStockGrid").jqxGrid("updatebounddata", "rows");
            repoSource.localdata = repoStocks;
            $("#jqxRepoGrid").jqxGrid("updatebounddata", "rows");
        }
    </script>
</head>
<body>
    <nfmt:navigation runat="server" id="navigation1" />

    <div id="jqxApplyExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            质押申请单信息
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="text-align: right; width: 15%;">质押申请单号：</span>
                    <input type="text" id="txbPledgeApplyNo" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">部门：</span>
                    <div id="ddlDeptId" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">日期：</span>
                    <div id="dtApplyTime" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">融资银行：</span>
                    <div id="ddlFinancingBankId" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">融资账户：</span>
                    <div id="ddlFinancingAccountId" style="float: left;"></div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">融资货物：</span>
                    <div id="ddlAssetId" style="float: left;"></div>

                    <span style="text-align: right; width: 15%;">头寸是否转回：</span>
                    <div id="rbYes" runat="server" style="float: left; margin-top: 6px;">是</div>
                    <div id="rbNo" runat="server" style="float: left; margin-top: 6px">否</div>
                </li>

                <li>
                    <span style="text-align: right; width: 15%;">头寸所在交易所：</span>
                    <div id="ddlExchangeId" style="float: left;"></div>
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            //部门
            var ddlApplyDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlApplyDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlApplyDeptIdurl, async: false };
            var ddlApplyDeptIddataAdapter = new $.jqx.dataAdapter(ddlApplyDeptIdsource);
            $("#ddlDeptId").jqxComboBox({ source: ddlApplyDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, disabled: true });
            $("#ddlDeptId").jqxComboBox("val", "<%=CurPledgeApply.DeptId%>");

            //日期
            $("#dtApplyTime").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180, disabled: true });
            $("#dtApplyTime").jqxDateTimeInput("val", new Date("<%=CurPledgeApply.ApplyTime%>"));

            //融资银行
            var ddlBankurl = "../BasicData/Handler/BanDDLHandler.ashx";
            var ddlBanksource = { datatype: "json", datafields: [{ name: "BankId" }, { name: "BankName" }], url: ddlBankurl, async: false };
            var ddlBankdataAdapter = new $.jqx.dataAdapter(ddlBanksource);
            $("#ddlFinancingBankId").jqxComboBox({ source: ddlBankdataAdapter, displayMember: "BankName", valueMember: "BankId", width: 180, height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlFinancingBankId").jqxComboBox("val", "<%=CurPledgeApply.FinancingBankId%>");

            //融资账户
            var ddlBankAccounturl = "../BasicData/Handler/BankAccountDDLHandler.ashx?b=" + $("#ddlFinancingBankId").val();
            var ddlBankAccountsource = { datatype: "json", datafields: [{ name: "BankAccId" }, { name: "AccountNo" }], url: ddlBankAccounturl, async: false };
            var ddlBankAccountdataAdapter = new $.jqx.dataAdapter(ddlBankAccountsource);
            $("#ddlFinancingAccountId").jqxComboBox({ source: ddlBankAccountdataAdapter, displayMember: "AccountNo", valueMember: "BankAccId", width: 180, height: 25, searchMode: "containsignorecase", disabled: true });
            $("#ddlFinancingAccountId").jqxComboBox("val", "<%=CurPledgeApply.FinancingAccountId%>");

            //融资品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 180, height: 25, disabled: true });
            $("#ddlAssetId").jqxComboBox("val", "<%=CurPledgeApply.AssetId%>");

            //期货头寸是否转回
            //是
            $("#rbYes").jqxRadioButton({ width: 80, height: 22, disabled: true });

            //否
            $("#rbNo").jqxRadioButton({ width: 80, height: 22, disabled: true });

            if ("<%=CurPledgeApply.SwitchBack%>".toLowerCase() === "true") {
                $("#rbYes").jqxRadioButton("check");
            } else {
                $("#rbNo").jqxRadioButton("check");
            }

            //期货头寸所在交易所
            var exchangeSource = { datatype: "json", url: "../BasicData/Handler/ExChangeDDLHandler.ashx", async: false };
            var exchangeDataAdapter = new $.jqx.dataAdapter(exchangeSource);
            $("#ddlExchangeId").jqxComboBox({ source: exchangeDataAdapter, displayMember: "ExchangeName", valueMember: "ExchangeId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlExchangeId").jqxComboBox("val", "<%=CurPledgeApply.ExchangeId%>");

            //质押申请单
            $("#txbPledgeApplyNo").jqxInput({ height: 25, width: 180, disabled: true });
            $("#txbPledgeApplyNo").jqxInput("val", "<%=CurPledgeApply.PledgeApplyNo%>");

        });
    </script>

    <div id="jqxStockExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            质押申请单实货描述
        </div>
        <div style="height: 500px;">
            <div id="jqxStockGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            //var url = "Handler/FinPledgeApplyStockDetailForRepoHandler.ashx?pid=<%=CurPledgeApply.PledgeApplyId%>";
            stocksSource =
            {
                datafields:
                [
                   { name: "StockDetailId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PledgeNetAmount", type: "number" },
                   { name: "PledgeHands", type: "int" },
                   { name: "Memo", type: "string" },
                   { name: "AlreadyNetAmount", type: "number" },
                   { name: "AlreadyHands", type: "int" },
                   { name: "NetAmount", type: "number" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "AccountName", type:"string" },
                   { name: "RepoTime", type:"date" },
                   { name: "Deadline", type:"string" }
                ],
                datatype: "json",                
                localdata: <%=selectedJson%>
                };

            var StockdataAdapter = new $.jqx.dataAdapter(stocksSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error); 
                }
            });

            $("#jqxStockGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: StockdataAdapter,
                autoheight: true,
                enabletooltips: true,
                editable: false,
                selectionmode: "singlecell",
                columns: [
                  { text: "合约号", datafield: "ContractNo", width: "10%", editable: false },
                  { text: "业务单号", datafield: "RefNo", width: "10%", editable: false },                  
                  { text: "质押净重（吨）", datafield: "PledgeNetAmount", width: "10%", editable: false, aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    pledgeApplySumNetAmount = Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                    return pledgeApplySumNetAmount;
                                }
                  }] },
                  { text: "匹配手数", datafield: "PledgeHands", width: 120, editable: false, aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    return Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                }
                  }] },
                  { text: "期限", datafield: "Deadline", width: 120, editable: false },
                  //{ text: "经济公司", datafield: "AccountName", width: "10%", editable: false },
                  //{ text: "已赎回手数", datafield: "AlreadyHands", width: 120, editable: false },
                  //{ text: "价格", datafield: "Price", width: "8%", editable: false },
                  //{ text: "到期日", datafield: "ExpiringDate", width: "8%", editable: false, cellsformat: "yyyy-MM-dd" },
                  { text: "备注", datafield: "Memo", editable: false },
                  { text: "已赎回净重", datafield: "AlreadyNetAmount", width: "10%", editable: false, aggregates: [{
                      '<b>总</b>':
                                function (aggregatedValue, currentValue, column, record) {
                                    aleadyNetAmount = Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                    return aleadyNetAmount;
                                }
                  }] },
                  //{
                  //    text: "赎回手数", datafield: "Hands", width: 120, sortable: false, columntype: "numberinput", cellclassname: "GridFillNumber",
                  //    validation: function (cell, value) {
                  //        var item = StockdataAdapter.records[cell.row];
                  //        if (value < 0 || value > (item.PledgeHands - item.AlreadyHands)) {
                  //            return { result: false, message: "赎回手数不能小于0且不能大于剩余手数" + eval(item.PledgeHands - item.AlreadyHands) };
                  //        }
                  //        return true;
                  //    },
                  //    createeditor: function (row, cellvalue, editor) {
                  //        editor.jqxNumberInput({ min: 0, decimalDigits: 0, width: 120, Digits: 8, spinButtons: true });
                  //    }
                  //},
                  //{
                  //    text: "赎回净重", datafield: "NetAmount", sortable: false, width: "10%", columntype: "numberinput",
                  //    validation: function (cell, value) {
                  //        var item = StockdataAdapter.records[cell.row];
                  //        if (value < 0 || value > (item.PledgeNetAmount - item.AlreadyNetAmount)) {
                  //            return { result: false, message: "赎回净重不能小于0且不能大于剩余净重" + eval(item.NetAmount - item.AlreadyNetAmount) };
                  //        }
                  //        return true;
                  //    },
                  //    createeditor: function (row, cellvalue, editor) {
                  //        editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                  //    },cellbeginedit: function (row) {
                  //        var item = StockdataAdapter.records[row];
                  //        if (item.NetAmount<=0)
                  //            return false;
                  //        return true;
                  //    }
                  //},
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: "6%", cellsrenderer: function (row) {
                          //var item = StockdataAdapter.records[row];
                          //var isInArray = false;
                          //for (var i = 0; i < rowIds.length; i++) {
                          //    if (rowIds[i] === item.StockDetailId) {
                          //        isInArray = true;
                          //        break;
                          //    }
                          //}

                          //if(item.NetAmount<=0 || isInArray){
                          //    return null;
                          //}
                          //else
                          return "添加";
                      }, buttonclick: function (row) {
                          var item = StockdataAdapter.records[row];
                          //var isInArray = false;
                          //for (var i = 0; i < rowIds.length; i++) {
                          //    if (rowIds[i] === item.StockDetailId) {
                          //        isInArray = true;
                          //        break;
                          //    }
                          //}
                          //if (item.NetAmount <= 0 || isInArray)return;

                          item.Hands = 0;
                          var datetime = new Date();
                          datetime = datetime.toLocaleDateString();
                          item.RepoTime = datetime;

                          repoStocks.push(item);
                          //stocks.splice(row, 1);
                          rowIds.push(item.StockDetailId);

                          //FlushValue();
                          var commit = $("#jqxRepoGrid").jqxGrid("addrow", null, item);
                      }
                  }
                ]
            });

            stocks = StockdataAdapter.records;
        });
    </script>

    <div id="jqxCashExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            质押申请单期货头寸描述
        </div>
        <div style="height: 500px;">
            <div id="jqxCashGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var url = "Handler/FinancingPledgeApplyCashDetailHandler.ashx?pid=<%=this.CurPledgeApply.PledgeApplyId%>";
            var source =
            {
                datafields:
                [
                   { name: "StockContractNo", type: "string" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "AccountName", type: "string" },
                   { name: "Memo", type: "string" }
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
            var dataAdapter = new $.jqx.dataAdapter(source);

            $("#jqxCashGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: dataAdapter,
                autoheight: true,
                enabletooltips: true,
                columns: [
                  { text: "实货合同号", datafield: "StockContractNo", width: 200 },
                  {
                      text: "数量（手）", datafield: "Hands", width: 200, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        return aggregatedValue + currentValue;
                                    }
                      }]
                  },
                  {
                      text: "价格", datafield: "Price", width: 200, sortable: false, columntype: "numberinput",
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, width: 200, Digits: 8, spinButtons: true });
                      }
                  },
                  { text: "到期日", datafield: "ExpiringDate", width: 200, cellsformat: "yyyy-MM-dd", columntype: "datetimeinput" },
                  { text: "经济公司账户名", datafield: "AccountName", width: 200 },
                  { text: "备注", datafield: "Memo" }
                ]
            });
        });
    </script>

    <div id="jqxRepoExpander" style="float: left; margin: 0 5px 5px 5px;">
        <div>
            赎回申请单信息
        </div>
        <div style="height: 500px;">
            <div id="jqxRepoGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            repoSource =
            {
                datafields:
                [
                   { name: "StockDetailId", type: "int" },
                   { name: "ContractNo", type: "string" },
                   { name: "StockId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "PledgeNetAmount", type: "number" },
                   { name: "PledgeHands", type: "int" },
                   { name: "Memo", type: "string" },
                   { name: "AlreadyNetAmount", type: "number" },
                   { name: "AlreadyHands", type: "int" },
                   { name: "NetAmount", type: "number" },
                   { name: "Hands", type: "int" },
                   { name: "Price", type: "number" },
                   { name: "ExpiringDate", type: "date" },
                   { name: "AccountName", type:"string" },
                   { name: "RepoTime", type:"date" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxRepoGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "pasd.StockDetailId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "pasd.StockDetailId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: repoStocks
            };

            var RepodataAdapter = new $.jqx.dataAdapter(repoSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error); 
                }
            });

            $("#jqxRepoGrid").jqxGrid(
            {
                width: "98%",
                showstatusbar: true,
                statusbarheight: 25,
                showaggregates: true,
                columnsresize: true,
                source: RepodataAdapter,
                autoheight: true,
                enabletooltips: true,
                editable: true,
                selectionmode: "singlecell",
                columns: [
                  { text: "日期", datafield: "RepoTime",width: "9%", cellsformat: "yyyy-MM-dd", columntype: "datetimeinput", cellclassname: "GridFillNumber",
                      initeditor: function (row, cellvalue, editor) {
                          editor.jqxDateTimeInput({ formatString: "yyyy-MM-dd" });
                          //editor.jqxDateTimeInput("val", new Date("<%=DateTime.Now%>"));
                  } },
                  { text: "合约号", datafield: "ContractNo", width: "9%", cellclassname: "GridFillNumber" },
                  {
                      text: "赎回净重", datafield: "NetAmount", sortable: false, width: "9%", columntype: "numberinput", cellclassname: "GridFillNumber",
                      //validation: function (cell, value) {
                      //    var item = RepodataAdapter.records[cell.row];
                      //    if (value < 0 || value > (Math.round((item.PledgeNetAmount - item.AlreadyNetAmount)*1000)/1000)) {
                      //        isValid = false;
                      //        return { result: false, message: "赎回净重不能小于0且不能大于剩余净重" + eval(Math.round((item.PledgeNetAmount - item.AlreadyNetAmount)*1000)/1000) };
                      //    }else{
                      //        isValid = true;
                      //        return true;
                      //    }
                      //},
                      createeditor: function (row, cellvalue, editor) {
                          editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                      }, aggregates: [{
                          '<b>总</b>':
                                    function (aggregatedValue, currentValue, column, record) {
                                        repoNetAmount = Math.round((aggregatedValue + currentValue) * 1000) / 1000;
                                        return repoNetAmount;
                                    }
                      }]
                  },
                  { text: "业务单号", datafield: "RefNo", width: "9%", cellclassname: "GridFillNumber" },
                  { text: "匹配手数", datafield: "Hands", width: "9%", editable: false },
                  { text: "价格", datafield: "Price", width: "9%", editable: true, cellclassname: "GridFillNumber", columntype: "numberinput",createeditor: function (row, cellvalue, editor) {
                      editor.jqxNumberInput({ min: 0, decimalDigits: 4, Digits: 8, spinButtons: true });
                  } },
                  { text: "到期日", datafield: "ExpiringDate", width: "9%", editable: true, cellsformat: "yyyy-MM-dd" , columntype: "datetimeinput", cellclassname: "GridFillNumber"},
                  { text: "经济公司", datafield: "AccountName", width: "9%" , editable: true, cellclassname: "GridFillNumber"},
                  { text: "备注", datafield: "Memo", cellclassname: "GridFillNumber" },
                  {
                      text: "操作", datafield: "Edit", columntype: "button", width: "6%", cellsrenderer: function () {
                          return "取消";
                      }, buttonclick: function (row) {
                          //var item = RepodataAdapter.records[row];         
                          
                          //for (var i = 0; i < rowIds.length; i++) {
                          //    if (rowIds[i] === item.StockDetailId) {
                          //        rowIds.splice(i, 1);
                          //    }
                          //}
                          ////stocks.push(item);
                          //repoStocks.splice(row, 1);

                          //FlushValue();
                          var rowscount = $("#jqxRepoGrid").jqxGrid("getdatainformation").rowscount;
                          if (row >= 0 && row < rowscount) {
                              var id = $("#jqxRepoGrid").jqxGrid("getrowid", row);
                              var commit = $("#jqxRepoGrid").jqxGrid("deleterow", id);
                          }
                      }
                  }
                ]
            });
        });
    </script>

    <div style="text-align: center; width: 80%; margin-top: 0;">
        <input type="button" id="btnSubmit" runat="server" value="确认并提交审核" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnSave" runat="server" value="保存" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="FinancingRepoApplyCanRepoPledgeList.aspx" id="btnCancel" style="margin-left: 10px">取消</a>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").jqxButton({ height: 25, width: 120 });

            function RepoApplyCreate(isAudit,buttonName) {
                var rows = $("#jqxRepoGrid").jqxGrid("getrows");
                if(rows.length<1){
                    alert("请添加赎回申请单信息");
                    return;
                }

                for (var i = 0; i < rows.length; i++) {
                    if (!rows[i].ContractNo || rows[i].ContractNo === "" ) {
                        alert("请填写合约号");
                        return;
                    }
                    if (!rows[i].NetAmount || rows[i].NetAmount === 0 ) {
                        alert("请填写赎回净重");
                        return;
                    }
                    if (!rows[i].RefNo || rows[i].RefNo === "" ) {
                        alert("请填写业务单号");
                        return;
                    }
                    if (!rows[i].Price || rows[i].Price === 0 ) {
                        alert("请填写价格");
                        return;
                    }
                    if (!rows[i].ExpiringDate || rows[i].ExpiringDate === "") {
                        alert("请填写到期日");
                        return;
                    }
                    if (!rows[i].AccountName || rows[i].AccountName === "") {
                        alert("请填写经纪公司");
                        return;
                    }
                }

                if(!isValid) return;
                
                if(repoNetAmount > (Math.round((pledgeApplySumNetAmount-aleadyNetAmount) * 1000) / 1000)){
                    alert("赎回总净重不可大于 "+(Math.round((pledgeApplySumNetAmount-aleadyNetAmount) * 1000) / 1000));
                    return;
                }

                if (!confirm("确认添加赎回申请单？")) { return; }
                $(buttonName).jqxButton({ disabled: true });

                var repoApply = {
                    //RepoApplyId: 
                    //RepoApplyIdNo: 
                    PledgeApplyId: "<%=pledgeApplyId%>"
                    //SumNetAmount: 
                    //SumHands: 
                    //RepoApplyStatus: 
                };                

        $.post("Handler/FinRepoApplyCreateHandler.ashx", { repoApply: JSON.stringify(repoApply), rows: JSON.stringify(rows), isAudit: isAudit },
            function (result) {
                var obj = JSON.parse(result);
                alert(obj.Message);
                if (obj.ResultStatus.toString() === "0") {
                    document.location.href = "FinancingRepoApplyList.aspx";
                }else{
                    $(buttonName).jqxButton({ disabled: true });
                }
            }
        );
    }

            $("#btnSubmit").click(function () { RepoApplyCreate(true,"#btnSubmit"); });

            $("#btnSave").jqxButton({ height: 25, width: 100 });
            $("#btnSave").click(function () { RepoApplyCreate(false,"#btnSave"); });

            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });
        });
    </script>

</body>
</html>
