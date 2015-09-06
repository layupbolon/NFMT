<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableCorpUpdate.aspx.cs" Inherits="NFMTSite.Funds.ReceivableCorpUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司收款分配修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
              
        var canAllotSource;
        var selectSource;
        
        var details;//选中集合
        var recIds = new Array();//保存ReceivableId信息
        var detIds = new Array();//保存DetailId信息

        var intRids = "<%=this.curRids%>";
        var intDids = "<%=this.curDids%>";
        var currencyId = <%=this.currencyId%>;

        var splitRids = intRids.split(",");
        for (i = 0; i < splitRids.length; i++) {
            if (splitRids[i].length > 0) {
                recIds.push(parseInt(splitRids[i]));
            }
        }

        var splitDids = intDids.split(",");
        for (i = 0; i < splitDids.length; i++) {
            if (splitDids[i].length > 0) {
                detIds.push(parseInt(splitDids[i]));
            }
        }

        $(document).ready(function () {
            

            //Expander Init
            $("#jqxConSubExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });
            $("#jqxSelectExpander").jqxExpander({ width: "98%" });
            $("#jqxCanAllotExpander").jqxExpander({ width: "98%" });            

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
                url: "Handler/ReceivableCorpAllotListHandler.ashx?cid=" + "<%=this.curCorp.CorpId%>"+"&aid="+"<%=this.curRecAllot.ReceivableAllotId%>"
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
                enabletooltips:  true,
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

            //////////////////////已选择分配//////////////////////
            formatedData = "";
            totalrecords = 0;
            selectSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" }
                ],
                localdata: <%=this.curJsonStr%>
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
                enabletooltips:  true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "收款日期", datafield: "ReceiveDate", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "我方公司", datafield: "InnerCorp", editable: false },
                  { text: "收款银行", datafield: "BankName", editable: false },
                  { text: "对方公司", datafield: "OutCorp", editable: false },
                  { text: "收款金额", datafield: "PayBala", editable: false },
                  { text: "分配金额", datafield: "CanAllotBala" },
                  { text: "操作", datafield: "ReceivableId", cellsrenderer: removeRender, width: 100, enabletooltips: false, sortable: false, editable: false }
                ]
            });

            details = $("#jqxSelectGrid").jqxGrid("getrows");


            //////////////////////可分配收款登记//////////////////////           

            formatedData = "";
            totalrecords = 0;
            var curId = 0;
            canAllotSource =
            {
                datatype: "json",
                datafields: [
                    { name: "DetailId", type: "int" },
                    { name: "ReceivableId", type: "int" },
                    { name: "ReceiveDate", type: "date" },
                    { name: "InnerCorp", type: "string" },
                    { name: "PayBala", type: "string" },
                    { name: "BankName", type: "string" },
                    { name: "CanAllotBala", type: "string" },
                    { name: "OutCorp", type: "string" },
                    { name: "CurrencyId", type: "int" }
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
                url: "Handler/ReceivableCorpCanAllotReceListHandler.ashx?rids=" + intRids+"&aid ="+ "<%=this.curRecAllot.ReceivableAllotId%>"
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
                enabletooltips:  true,
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

            //对方公司
            $("#ddlCorpId").jqxInput({ height: 25, width: 180 });
            //币种
            var ddlMUIdurl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlCurrencyId").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: 180, height: 25, autoDropDownHeight: true });

            $("#btnSearch").click(function () {
                AllotGridFresh();
            });

            //Control Init
            $("#txbMemo").jqxInput({ width: "500" });
            $("#txbMemo").val("<%=this.curRecAllot.AllotDesc%>");

            $("#chbIsShare").jqxCheckBox({ width: 200, height: 25 });
            if ("<%=this.isShare.ToString().ToLower()%>"=="true") {
                $("#chbIsShare").jqxCheckBox("check");
            }

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnSearch").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnUpdate").click(function () {

                if (recIds.length == 0) { alert("未分配任何收款！"); return; }

                var rows = $("#jqxSelectGrid").jqxGrid("getrows");

                $.post("Handler/ReceivableCorpUpdateHandler.ashx", {
                    id: "<%=this.curRecAllot.ReceivableAllotId%>",
                    Allot: JSON.stringify(rows),
                    memo: $("#txbMemo").val(),
                    isShare: $("#chbIsShare").val()
                },
                   function (result) {
                       alert(result);
                       document.location.href = "ReceivableCorpList.aspx";
                   }
               );
            });
            
        });


        function bntRemoveOnClick(row, value) {

            var item = details[row];

            if(item.DetailId != undefined && item.DetailId>0){
                var index1 = detIds.indexOf(item.DetailId);
                detIds.splice(index1,1);
            }
            else{
                var index2 = recIds.indexOf(item.ReceivableId);
                recIds.splice(index2,1);
            }

            details.splice(row, 1);
            if(details.length==0){
                currencyId =0;
            }

            flushGrid();
        };

        function bntAddOnClick(row, value) {

            var item = $("#jqxCanAllotGrid").jqxGrid("getrowdata", row);
            
            if(currencyId ==0){
                currencyId = item.CurrencyId;
            }

            if(item.CurrencyId != currencyId){
                alert("必须选择相同币种的收款登记");
                return;
            }

            details.push(item);
            recIds.push(item.ReceivableId);
            flushGrid();
        };        

        function flushGrid() {           

            selectSource.localdata = details;
            $("#jqxSelectGrid").jqxGrid("updatebounddata", "rows");

            var rids = "";
            for (i = 0; i < recIds.length; i++) {
                if (i != 0) { rids += ","; }
                rids += recIds[i];
            }

            var dids ="";
            for (i = 0; i < detIds.length; i++) {
                if (i != 0) { dids += ","; }
                dids += detIds[i];
            }

            var selCurId = $("#ddlCurrencyId").val();
            var corpName = $("#ddlCorpId").val();
            canAllotSource.url = "Handler/ReceivableCorpCanAllotReceListHandler.ashx?t=0&rids=" + rids + "&dids="+ dids +"&curId=" + selCurId + "&corpId=" + corpName + "&aid ="+ "<%=this.curRecAllot.ReceivableAllotId%>";
            $("#jqxCanAllotGrid").jqxGrid("updatebounddata", "rows");

        };

    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxConSubExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            公司信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>所属集团：</strong>
                    <span style="float: left;" id="spanBlocId" runat="server" />
                </li>
                <li><strong>公司代码：</strong>
                    <span id="spanCorpCode" runat="server" /></li>
                <li>
                    <strong>公司名称：</strong>
                    <span id="spanCorpName" runat="server" />
                </li>
                <li><strong>纳税人识别号：</strong>
                    <span id="spanTaxPlayer" runat="server" /></li>
                <li>
                    <strong>公司地址：</strong>
                    <span id="spanCorpAddress" runat="server" />
                </li>
                <li><strong>公司电话：</strong>
                    <span id="spanCorpTel" runat="server" /></li>
                <li>
                    <strong>公司传真：</strong>
                    <span id="spanCorpFax" runat="server" />
                </li>
                <li><strong>公司邮编：</strong>
                    <span id="spanCorpZip" runat="server" /></li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已分配情况
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
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

    <div id="jqxCanAllotExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            可分配收款登记
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span>币种：</span>
                    <div id="ddlCurrencyId" style="float: left;"></div>

                    <span style="text-align: right; width: 10%;">对方公司：</span>
                    <input type="text" id="ddlCorpId" style="float: left;" />

                    <span>&nbsp;</span>
                    <input type="button" value="查询" id="btnSearch" />
                </li>
            </ul>

            <div id="jqxCanAllotGrid"></div>
            <div>
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
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="提交" style="width: 125px; height: 25px;" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="ReceivableCorpList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
