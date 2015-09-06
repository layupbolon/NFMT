<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInContractDetail.aspx.cs" Inherits="NFMTSite.WareHouse.StockInContractDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>入库合约分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.curContractStockIn.DataBaseName%>" + "&t=" + "<%=this.curContractStockIn.TableName%>" + "&id=" + "<%=this.curContractStockIn.RefId%>";

        $(document).ready(function () {
            $("#jqxStockInExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });

            var selectSource;
            var subId ="<%=this.curContractStockIn.ContractSubId%>";

            var formatedData = "";
            var totalrecords = 0;
            //选中合约
            selectSource =
            {
                datafields:
                [
                   { name: "ContractId", type: "int" },
                   { name: "SubId", type: "int" },
                   { name: "ContractDate", type: "date" },
                   { name: "ContractNo", type: "string" },
                   { name: "SubNo", type: "string" },
                   { name: "InCorpName", type: "string" },
                   { name: "OutCorpName", type: "string" },
                   { name: "AssetName", type: "string" },
                   { name: "SignAmount", type: "string" },
                   { name: "SignWeight", type: "string" },
                   { name: "SumWeight", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxSelectGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "cs.SubId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cs.SubId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                localdata: <%=this.curContractJson%>
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
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "订立日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd" },
                  { text: "合约号", datafield: "ContractNo" },
                  { text: "子合约号", datafield: "SubNo" },
                  { text: "我方公司", datafield: "InCorpName" },
                  { text: "对方公司", datafield: "OutCorpName" },
                  { text: "交易品种", datafield: "AssetName" },
                  { text: "合约计量", datafield: "SignWeight" },
                  { text: "已入库计量", datafield: "SumWeight" }
                ]
            });

            //init button
            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 38,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnGoBack").on("click", function () {
                var operateId = operateEnum.撤返;
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/ContractStockInStatusHandler.ashx", { si: "<%=this.curContractStockIn.RefId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockInContractList.aspx";          
                        }           
                    }
                );
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/ContractStockInStatusHandler.ashx", { si: "<%=this.curContractStockIn.RefId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockInContractList.aspx";          
                        }     
                    }
                );
            });
        });

    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxStockInExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            入库登记信息
                        <input type="hidden" id="hidModel" runat="server" />
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>业务单号：</strong>
                    <span><%=this.curStockIn.RefNo%></span>
                </li>
                <li>
                    <strong>报关状态：</strong>
                    <span><%=this.CustomTypeName%></span>
                </li>
                <li>
                    <strong>入账公司：</strong>
                    <span><%=this.CorpName%></span>
                </li>
                <li>
                    <strong>入库日期：</strong>
                    <span><%=this.curStockIn.StockInDate.ToShortDateString()%></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span><%=this.AssetName%></span>
                </li>
                <li>
                    <strong>毛重：</strong>
                    <span><%=this.curStockIn.GrossAmount.ToString()+ this.MUName%></span>
                </li>
                <li>
                    <strong>净重：</strong>
                    <span><%=this.curStockIn.NetAmount.ToString()+ this.MUName%></span>
                </li>
                <li>
                    <strong>捆数：</strong>
                    <span><%=this.curStockIn.Bundles%></span>
                </li>
                <li>
                    <strong>品牌：</strong>
                    <span><%=NFMT.Data.BasicDataProvider.Brands.FirstOrDefault(temp=>temp.BrandId == this.curStockIn.BrandId).BrandName%></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            选中合约
        </div>
        <div>
            <div id="jqxSelectGrid" style="float: left; margin: 5px 0px 0px 5px;"></div>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
