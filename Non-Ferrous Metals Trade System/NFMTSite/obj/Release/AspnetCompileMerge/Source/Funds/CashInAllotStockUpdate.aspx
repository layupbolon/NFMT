<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashInAllotStockUpdate.aspx.cs" Inherits="NFMTSite.Funds.CashInAllotStockUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存收款分配修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">       

        var contractSelectSource;
        var contractAllotSource;        
      
        var contractDetails = new Array();
        var contractExcs = new Array();

        $(document).ready(function () {
            

            //Expander Init
            $("#jqxConSubExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxStockExpander").jqxExpander({ width: "98%" });               
            
            $("#jqxContractSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxContractAllotExpander").jqxExpander({ width: "98%" });

            //////////////////////***已分配情况***//////////////////////
            var formatedData = "";
            var totalrecords = 0;           

            contractSelectSource =
            {
                localdata: <%=this.JsonContractSelect%>,
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ContractRefId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "LastBala", type: "number" },
                    { name: "AllotBala", type: "number" },
                    { name: "AllotCorp", type: "string" }
                ],
                datatype: "json"
            };
            var contractSelectDataAdapter = new $.jqx.dataAdapter(contractSelectSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var contractRemoveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"取消\" onclick=\"bntContractRemoveOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxContractSelectGrid").jqxGrid(
            {
                width: "98%",
                source: contractSelectDataAdapter,                
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "CashInDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InCorp", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款银行", datafield: "CashInBankName", editable: false },
                  { text: "收款金额", datafield: "CashInBala", editable: false },                  
                  { text: "分配公司", datafield: "AllotCorp", editable: false },
                  { text: "分配金额", datafield: "AllotBala", editable: false },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "ContractRefId", cellsrenderer: contractRemoveRender, width: 100, enabletooltips: false, sortable: false }
                ]
            });

            contractDetails = $("#jqxContractSelectGrid").jqxGrid("getrows");

            ///***可选择合约收款***///
            formatedData = "";
            totalrecords = 0;            

            contractAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ContractRefId", type: "int" },
                    { name: "CashInId", type: "int" },
                    { name: "CashInDate", type: "date" },
                    { name: "InCorp", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CashInBankName", type: "string" },
                    { name: "CurrencyId", type: "int" },
                    { name: "CurrencyName", type: "string" },
                    { name: "CashInBala", type: "number" },
                    { name: "LastBala", type: "number" },
                    { name: "AllotBala", type: "number" },
                    { name: "AllotCorp", type: "string" }
                ],
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "cicr.RefId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cicr.RefId";
                    data.sortorder = data.sortorder || "desc";
                    data.filterscount = data.filterscount || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                url: "Handler/CashInContractLastListHandler.ashx?subId="+"<%=this.curSub.SubId%>"
            };
            var contractAllotDataAdapter = new $.jqx.dataAdapter(contractAllotSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var contractAddRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
                   + "<input type=\"button\" name=\"btnAdd\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"添加\" onclick=\"bntContractAddOnClick(" + row + ");\" />"
                   + "</div>";
            }
            $("#jqxContractAllotGrid").jqxGrid(
            {
                width: "98%",
                source: contractAllotDataAdapter,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                selectionmode: "singlecell",
                sorttogglestates: 1,
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
                  { text: "分配公司", datafield: "AllotCorp", editable: false },
                  {
                      text: "分配金额", datafield: "AllotBala", columntype: "numberinput", cellclassname: "GridFillNumber", validation: function (cell, value) {

                          var item = $("#jqxContractAllotGrid").jqxGrid("getrowdata", cell.row);
                          if (value > item.LastBala) {
                              return { result: false, message: "分配金额不能大于剩余金额" + item.LastBala };
                          }
                          return true;
                      }, sortable: false
                  },
                  { text: "币种", datafield: "CurrencyName", editable: false },
                  { text: "操作", datafield: "ContractRefId", cellsrenderer: contractAddRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });
            
            $("#btnUpdate").click(function () {
                if (contractDetails.length > 0) {
                    $.post("Handler/CashInStockUpdateHandler.ashx", {
                        Details: JSON.stringify(contractDetails),
                        StockLogId: "<%=this.curStockLog.StockLogId%>",
                    },
                       function (result) {
                           alert(result);
                           document.location.href = "CashInAllotStockList.aspx";
                       }
                   );
                }
                else { alert("未分配任何款项！"); }

            });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });
        });

        function bntContractAddOnClick(row){ 
            
            var item = $("#jqxContractAllotGrid").jqxGrid("getrowdata", row);

            //添加收款登记列表
            contractDetails.push(item);

            flushContractGrid();
        }

        function bntContractRemoveOnClick(row){
        
            var item = contractDetails[row];            

            if(item.DetailId != undefined && item.DetailId>0){
                //增加排除收款分配
                contractExcs.push(item);
            }

            //删除收款登记列表
            contractDetails.splice(row, 1);
            
            flushContractGrid();
        }

        function flushContractGrid(){
            
            contractSelectSource.localdata = contractDetails;
            $("#jqxContractSelectGrid").jqxGrid("updatebounddata", "rows");

            var contractRefIds = "";
            for (i = 0; i < contractDetails.length; i++) {
                if (i != 0) { contractRefIds += ","; }
                contractRefIds += contractDetails[i].ContractRefId;
            }

            var stockRefIds ="";
            for (i = 0; i < contractExcs.length; i++) {
                if (i != 0) { stockRefIds += ","; }
                stockRefIds += contractExcs[i].DetailId;
            }

            contractAllotSource.url = "Handler/CashInContractLastListHandler.ashx?subId=" + "<%=this.curSub.SubId%>" +"&contractRefIds=" + contractRefIds +"&stockRefIds="+stockRefIds;
            $("#jqxContractAllotGrid").jqxGrid("updatebounddata", "rows");
        }

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            归属合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>子合约号：</strong>
                    <span><%=this.curSub.SubNo%></span>
                </li>
                <li><strong>签订时间：</strong>
                    <span><%=this.curSub.ContractDate.ToShortDateString() %></span></li>
                <li>
                    <strong>我方公司：</strong>
                    <span runat="server" id="spnInCorpNames"></span>
                </li>
                <li><strong>对方公司：</strong>
                    <span runat="server" id="spnOutCorpNames"></span></li>
                <li>
                    <strong>签订数量：</strong>
                    <span runat="server" id="spnSignAmount"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxStockExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            库存信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>业务单号：</strong>
                    <span runat="server" id="spanRefNo"></span>
                </li>
                <li><strong>入库时间：</strong>
                    <span runat="server" id="spanStockDate"></span></li>
                <li>
                    <strong>归属公司：</strong>
                    <span runat="server" id="spanCorpId"></span>
                </li>
                <li><strong>入库重量：</strong>
                    <span runat="server" id="spanGrossAmout"></span></li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spanAssetId"></span>
                </li>
                <li><strong>品牌：</strong>
                    <span runat="server" id="spanBrandId"></span></li>
            </ul>
        </div>
    </div>

    <div id="jqxContractSelectExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已选择合约收款
        </div>
        <div>
            <div id="jqxContractSelectGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxContractAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可分配合约收款
        </div>
        <div>
            <div id="jqxContractAllotGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <span>备注：</span>&nbsp;&nbsp;&nbsp;&nbsp;<textarea id="txbMemo" runat="server"></textarea>&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
        <input type="button" id="btnUpdate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <a target="_self" runat="server" href="ReceivableStockList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>

