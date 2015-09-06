<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotCorpDetail.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotCorpDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司收款分配明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curAllot.DataBaseName%>" + "&t=" + "<%=this.curAllot.TableName%>" + "&id=" + "<%=this.curAllot.AllotId%>";

        var selectSource;
        var allotSource;

        $(document).ready(function () {
            

            //Expander Init
            $("#jqxCorpExpander").jqxExpander({ width: "98%", expanded: false });

            $("#jqxOtherExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });            
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
                sortcolumn: "cia.AllotId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cia.AllotId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
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
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd" },
                  { text: "我方公司", datafield: "InCorp" },
                  { text: "对方公司", datafield: "OutCorp" },
                  { text: "收款银行", datafield: "CashInBankName" },
                  { text: "收款金额", datafield: "CashInBala" },
                  { text: "分配金额", datafield: "AllotBala" },
                  { text: "币种", datafield: "CurrencyName" }
                ]
            });
           
            $("#txbMemo").jqxInput({ width: "500",disabled:true });
            $("#chbIsShare").jqxCheckBox({ width: 200, height: 25,disabled:true });

            $("#txbMemo").val("<%=this.curAllot.AllotDesc%>");
            if ("<%=this.isShare.ToString().ToLower()%>"=="true") {
                $("#chbIsShare").jqxCheckBox("check");
            }

            //buttons
            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();
            $("#btnCompleteCancel").jqxInput();
            $("#btnClose").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 14,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CashInCorpAllotStatusHandler.ashx", { id: "<%=this.curAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/CashInCorpAllotStatusHandler.ashx", { id: "<%=this.curAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
            });

            $("#btnComplete").on("click", function () {
                if (!confirm("确认完成？")) { return; }
                var operateId = operateEnum.执行完成;
                $.post("Handler/CashInCorpAllotStatusHandler.ashx", { id: "<%=this.curAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
            });

            $("#btnCompleteCancel").on("click", function () {
                if (!confirm("确认完成撤销？")) { return; }
                var operateId = operateEnum.执行完成撤销;
                $.post("Handler/CashInCorpAllotStatusHandler.ashx", { id: "<%=this.curAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
            });

            $("#btnClose").on("click", function () {
                if (!confirm("确认关闭？")) { return; }
                var operateId = operateEnum.关闭;
                $.post("Handler/CashInCorpAllotStatusHandler.ashx", { id: "<%=this.curAllot.AllotId%>", oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CashInAllotList.aspx";
                    }
                );
            });

        });       

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidModel" runat="server" />

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

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnCompleteCancel" value="确认完成撤销" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnClose" value="关闭" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
    </div>
</body>
    <script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
