﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInConDetail.aspx.cs" Inherits="NFMTSite.WareHouse.StockInConDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%@ Register TagName="ContractExpander" TagPrefix="NFMT" Src="~/Control/ContractExpander.ascx" %>
<%--<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>入库登记明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>    
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%", expanded: false });
            $("#jqxAttachGridExpander").jqxExpander({ width: "98%" });
            
            ///////////////////////////已入库情况///////////////////////////

            //绑定Grid
            var url = "Handler/AlreadyStockInBySubHandler.ashx?subId="+"<%=this.curSubId%>";
            var formatedData = "";
            var totalrecords = 0;
            var source =
            {
                datafields:
                [
                   { name: "StockInId", type: "int" },
                   { name: "RefNo", type: "string" },
                   { name: "GrossAmount", type: "string" },
                   { name: "StatusName", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "AssetName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    totalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;

                    return returnData;
                },
                type: "GET",
                sortcolumn: "st.StockInId",
                sortdirection: "desc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "st.StockInId";
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

            $("#jqxGrid").jqxGrid(
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
                  { text: "业务单数量", datafield: "GrossAmount" },
                  { text: "库存状态", datafield: "StatusName" },
                  { text: "归属公司", datafield: "CorpName" },
                  { text: "品种", datafield: "AssetName" }
                ]
            });
            
            //入库日期
            $("#txbStockInDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180,disabled: true });
            var stockInDate = new Date("<%=this.curStockIn.StockInDate.ToString("yyyy/MM/dd")%>");
            $("#txbStockInDate").jqxDateTimeInput("val", stockInDate);

            //报关状态
            var ddlStockTypesource = { datatype: "json", url: "../BasicData/Handler/CustomsTypeHandler.ashx", async: false };
            var ddlStockTypedataAdapter = new $.jqx.dataAdapter(ddlStockTypesource);
            $("#ddlCustomType").jqxComboBox({ source: ddlStockTypedataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 180, height: 25, autoDropDownHeight: true,disabled: true  });
            $("#ddlCustomType").val(<%=this.curStockIn.CustomType%>);

            //入账公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCorpId").jqxComboBox({ selectedIndex: 0, source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true,disabled: true });
            $("#ddlCorpId").jqxComboBox("val", <%=this.curStockIn.CorpId%>);

            //所属部门
            var ddlDeptIdurl = "../User/Handler/DeptDDLHandler.ashx?";
            var ddlDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlDeptIdurl, async: false };
            var ddlDeptIddataAdapter = new $.jqx.dataAdapter(ddlDeptIdsource);
            $("#ddlDeptId").jqxComboBox({ source: ddlDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlDeptId").val(<%=this.curStockIn.DeptId%>);

            //净重
            $("#txbNetAmount").jqxNumberInput({ width: 180, height: 25, spinButtons: true,disabled: true , decimalDigits: 4, Digits: 8});            
            $("#txbNetAmount").jqxNumberInput("val", <%=this.curStockIn.NetAmount%>);

            //毛重
            $("#txbGrossAmount").jqxNumberInput({ width: 180, height: 25, spinButtons: true,disabled: true, decimalDigits: 4, Digits: 8 });            
            $("#txbGrossAmount").jqxNumberInput("val", <%=this.curStockIn.GrossAmount%>);

            //计量单位
            var ddlMUIdurl = "../BasicData/Handler/MUDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlMUId").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "MUName", valueMember: "MUId", width: 180, height: 25, autoDropDownHeight: true,disabled: true });
            $("#ddlMUId").jqxComboBox("val", <%=this.curStockIn.UintId%>);

            //品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", datafields: [{ name: "AssetId" }, { name: "AssetName" }], url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 180, height: 25,disabled: true });
            $("#ddlAssetId").jqxComboBox("val", <%=this.curStockIn.AssetId%>);

            //捆数
            $("#nbBundles").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 0, Digits: 3,disabled:true });
            $("#nbBundles").val(<%=this.curStockIn.Bundles%>);

            //交货地
            var ddlDeliverPlaceIdurl = "../BasicData/Handler/DeliverPlaceDDLHandler.ashx";
            var ddlDeliverPlaceIdsource = { datatype: "json", datafields: [{ name: "DPId" }, { name: "DPName" }], url: ddlDeliverPlaceIdurl, async: false };
            var ddlDeliverPlaceIddataAdapter = new $.jqx.dataAdapter(ddlDeliverPlaceIdsource);
            $("#ddlDeliverPlaceId").jqxComboBox({ source: ddlDeliverPlaceIddataAdapter, displayMember: "DPName", valueMember: "DPId", width: 180, height: 25,disabled: true });
            $("#ddlDeliverPlaceId").jqxComboBox("val", <%=this.curStockIn.DeliverPlaceId%>);

            //生产商
            var ddlProducterurl = "../BasicData/Handler/ProducerDDLHandler.ashx";
            var ddlProductersource = { datatype: "json", datafields: [{ name: "ProducerId" }, { name: "ProducerName" }], url: ddlProducterurl, async: false };
            var ddlProducterdataAdapter = new $.jqx.dataAdapter(ddlProductersource);
            $("#ddlProducter").jqxComboBox({ source: ddlProducterdataAdapter, displayMember: "ProducerName", valueMember: "ProducerId", width: 180, height: 25, autoDropDownHeight: true ,disabled: true });
            $("#ddlProducter").jqxComboBox("val", <%=this.curStockIn.ProducerId%>);

            //品牌
            var ddlBrandIdurl = "../BasicData/Handler/BrandDDLHandler.ashx?pid=" + $("#ddlProducter").val();
            var ddlBrandIdsource = { datatype: "json", datafields: [{ name: "BrandId" }, { name: "BrandName" }], url: ddlBrandIdurl, async: false };
            var ddlBrandIddataAdapter = new $.jqx.dataAdapter(ddlBrandIdsource);
            $("#ddlBrandId").jqxComboBox({ source: ddlBrandIddataAdapter, displayMember: "BrandName", valueMember: "BrandId", width: 180, height: 25, autoDropDownHeight: true,disabled: true });
            $("#ddlBrandId").val(<%=this.curStockIn.BrandId%>);

            //原产地
            $("#txbOriginPlace").jqxInput({ height: 25, width: 180,disabled: true });
            $("#txbOriginPlace").val("<%=this.curStockIn.OriginPlace%>");

            //规格
            $("#txbFormat").jqxInput({ height: 25, width: 180,disabled: true });
            $("#txbFormat").val("<%=this.curStockIn.Format%>");

            //权证编号
            $("#txbPaperNo").jqxInput({ height: 25, width: 180,disabled: true });
            $("#txbPaperNo").val("<%=this.curStockIn.PaperNo%>");

            //单据保管人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlPaperHolder").jqxComboBox({ source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25, autoDropDownHeight: true,disabled: true });
            $("#ddlPaperHolder").jqxComboBox("val", <%=this.curStockIn.PaperHolder%>);

            //单号
            $("#txbCardNo").jqxInput({ height: 25, width: 180,disabled: true });
            $("#txbCardNo").val("<%=this.curStockIn.CardNo%>");

            //业务单号
            $("#txbStockName").jqxInput({ height: 25, width: 180,disabled: true });
            $("#txbStockName").val("<%=this.curStockIn.RefNo%>");

            //单据类型
            var stockTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.StockType%>", async: false };
            var stockTypeDataAdapter = new $.jqx.dataAdapter(stockTypeSource);
            $("#selStockType").jqxComboBox({ source: stockTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true,disabled: true });
            $("#selStockType").val(<%=this.curStockIn.StockType%>);

            //入库类型
            var stockOperateTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.出入库类型%>", async: false };
            var stockOperateTypeDataAdapter = new $.jqx.dataAdapter(stockOperateTypeSource);
            $("#selStockOperateType").jqxComboBox({ source: stockOperateTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true,disabled: true });
            $("#selStockOperateType").val(<%=this.curStockIn.StockOperateType%>);
           
            $("#filStockAttach").attr("disabled", true);

            var attachUrl = "Handler/StockInAttachHandler.ashx?id=<%=this.curStockIn.StockInId%>";

            var attachFormatedData = "";
            var attachTotalrecords = 0;
            attachSource =
            {
                url: attachUrl,
                datafields: [
                    { name: "StockInAttachId", type: "int" },
                    { name: "StockInId", type: "int" },
                    { name: "AttachId", type: "int" },
                    { name: "AttachName", type: "string" },
                    { name: "AttachInfo", type: "string" },
                    { name: "AttachType", type: "int" },
                    { name: "DetailName", type: "string" },
                    { name: "CreateTime", type: "date" },
                    { name: "AttachPath", type: "string" },
                    { name: "AttachExt", type: "string" },
                    { name: "AttachStatus", type: "int" },
                    { name: "ServerAttachName", type: "string" }
                ],
                datatype: "json",
                sort: function () {
                    $("#jqxAttachGrid").jqxGrid("updatebounddata", "sort");
                },
                beforeprocessing: function (data) {
                    var returnData = {};
                    attachTotalrecords = data.count;
                    returnData.totalrecords = data.count;
                    returnData.records = data.data;
                    return returnData;
                },
                type: "GET",
                sortcolumn: "sia.AttachType",
                sortdirection: "asc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "sia.AttachType";
                    data.sortorder = data.sortorder || "asc";
                    data.filterscount = data.filterscount || 0;
                    attachFormatedData = buildQueryString(data);
                    return attachFormatedData;
                }
            };

            var attachDataAdapter = new $.jqx.dataAdapter(attachSource, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var attachViewRender = function (row, columnfield, value, defaulthtml, columnproperties) {
                var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">";
                cellHtml += "<a href=\"../Files/FileDownLoad.aspx?id=" + item.AttachId + "\" title=\"" + item.AttachName + "\" >下载</a>";
                cellHtml += "</div>";
                return cellHtml;
            }

            $("#jqxAttachGrid").jqxGrid(
            {
                width: "98%",
                source: attachDataAdapter,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "附件类型", datafield: "DetailName" },
                  { text: "添加日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd", editable: false },
                  { text: "附件名字", datafield: "AttachName" },
                  { text: "查看", datafield: "AttachPath", cellsrenderer: attachViewRender }
                ]
            });

            $("#btnInvalid").jqxInput();
            $("#btnAudit").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnComplete").jqxInput();    
            $("#btnCompleteCancel").jqxInput();
            $("#btnClose").jqxInput();

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 4,
                    model: $("#hidmodel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                $.post("Handler/StockInStatusHandler.ashx", { id: "<%=this.curStockIn.StockInId%>",oi: operateEnum.作废 },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockInList.aspx";          
                        }    
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                $.post("Handler/StockInStatusHandler.ashx", { id: "<%=this.curStockIn.StockInId%>",oi: operateEnum.撤返 },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);   
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "StockInList.aspx";
                        }
                    }
                );
            });

            //$("#btnComplete").on("click", function () {
            //    if (!confirm("确认完成？")) { return; }
            //    $.post("Handler/StockInStatusHandler.ashx", { id: "",oi: operateEnum.执行完成 },
            //        function (result) {
            //            var obj = JSON.parse(result);
            //            alert(obj.Message);   
            //            if (obj.ResultStatus.toString() == "0") {
            //                document.location.href = "StockInList.aspx";
            //            }
            //        }
            //    );
            //});

            //$("#btnCompleteCancel").on("click", function () {
            //    if (!confirm("确认完成撤销？")) { return; }
            //    $.post("Handler/StockInStatusHandler.ashx", { id: "",oi: operateEnum.执行完成撤销 },
            //        function (result) {
            //            var obj = JSON.parse(result);
            //            alert(obj.Message);   
            //            if (obj.ResultStatus.toString() == "0") {
            //                document.location.href = "StockInList.aspx";
            //            }
            //        }
            //    );
            //});

            //$("#btnClose").on("click", function () {
            //    if (!confirm("确认完成撤销？")) { return; }
            //    $.post("Handler/StockInStatusHandler.ashx", { id: "",oi: operateEnum.关闭 },
            //        function (result) {
            //            var obj = JSON.parse(result);
            //            alert(obj.Message);   
            //            if (obj.ResultStatus.toString() == "0") {
            //                document.location.href = "StockInList.aspx";
            //            }
            //        }
            //    );
            //});
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />
    <input type="hidden" id="hidmodel" runat="server" />
     <NFMT:ContractExpander runat ="server" ID="contractExpander1" />
  
    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            已入库情况
        </div>
        <div>
            <div id="jqxGrid" style="float: left; margin: 5px 0 0 5px;"></div>
        </div>
    </div>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            入库登记明细
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>入库日期：</strong>
                    <div id="txbStockInDate" style="float: left;"></div>
                    <strong>报关状态：</strong>
                    <div id="ddlCustomType" style="float: left;"></div>
                </li>

                <li>
                    <strong>入账公司：</strong>
                    <div id="ddlCorpId" style="float: left;"></div>

                    <strong>所属部门：</strong>
                    <div id="ddlDeptId" style="float: left;"></div>
                </li>

                <li>
                    <strong>品种：</strong>
                    <div id="ddlAssetId" style="float: left;"></div>

                    <strong>计量单位：</strong>
                    <div id="ddlMUId" style="float: left;"></div>
                </li>

                <li>
                    <strong>净重：</strong>
                    <div id="txbNetAmount" style="float: left;"></div>

                    <strong>毛重：</strong>
                    <div id="txbGrossAmount" style="float: left;"></div>
                </li>

                <li>
                    <strong>捆数：</strong>
                    <div id="nbBundles" style="float: left;"></div>

                    <strong>交货地：</strong>
                    <div id="ddlDeliverPlaceId" style="float: left;"></div>
                </li>

                <li>
                    <strong>生产商：</strong>
                    <div id="ddlProducter" style="float: left;"></div>

                    <strong>品牌：</strong>
                    <div id="ddlBrandId" style="float: left;"></div>
                </li>

                <li>
                    <strong>原产地：</strong>
                    <input id="txbOriginPlace" style="float: left;"/>
                    <strong>规格：</strong>
                    <input id="txbFormat" style="float: left;"/>
                </li>

                <li>
                    <strong>权证编号：</strong>
                    <input id="txbPaperNo" type="text" style="float: left;" />

                    <strong>单据保管人：</strong>
                    <div id="ddlPaperHolder" style="float: left;"></div>
                </li>
                <li>
                    <strong>卡号：</strong>
                    <input id="txbCardNo" type="text" style="float: left;" />

                    <strong>业务单号：</strong>
                    <input id="txbStockName" type="text" style="float: left;" />
                </li>
                <li>
                    <strong>单据类型：</strong>
                    <div id="selStockType" style="float: left;"></div>
                    <strong>入库类型：</strong>
                    <div id="selStockOperateType" style="float: left;"></div>
                </li>
                <li>
                    <strong>单据附件：</strong>
                    <input type="file" id="filStockAttach" />
                </li>
            </ul>
        </div>
    </div>

    <%--<NFMT:Attach runat="server" ID="attach1" AttachStyle="Detail" AttachType="StockInAttach" />--%>
    <div id="jqxAttachGridExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            附件
        </div>
        <div>
            <div id="jqxAttachGrid"></div>
        </div>
    </div>

    <div id="buttons" style="width: 80%; text-align: center;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 100px; height: 25px" />
        <input type="button" id="btnInvalid" value="作废" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <%--<input type="button" id="btnComplete" value="确认完成" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnCompleteCancel" value="确认完成撤销" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />
        <input type="button" id="btnClose" value="确认关闭" runat="server" style="margin-left: 35px; width: 100px; height: 25px" />--%>
    </div>

</body>

<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
