<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInUpdate.aspx.cs" Inherits="NFMTSite.WareHouse.StockInUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>
<%--<%@ Register TagName="Attach" TagPrefix="NFMT" Src="~/Control/Attach.ascx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>入库登记修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>    
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/ajaxfileupload.js"></script>

    <script type="text/javascript">
        var attachSource = null;

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxAttachExpander").jqxExpander({ width: "98%" });
            $("#jqxAttachGridExpander").jqxExpander({ width: "98%" });

            ///////////////////////////入库登记修改///////////////////////////
            //入库日期
            $("#txbStockInDate").jqxDateTimeInput({ formatString: "yyyy-MM-dd", width: 180 });
            var stockInDate = new Date("<%=this.curStockIn.StockInDate.ToString("yyyy/MM/dd")%>");
            $("#txbStockInDate").jqxDateTimeInput("val", stockInDate);

            //报关状态            
            var ddlStockTypesource = { datatype: "json", url: "../BasicData/Handler/CustomsTypeHandler.ashx", async: false };
            var ddlStockTypedataAdapter = new $.jqx.dataAdapter(ddlStockTypesource);
            $("#ddlCustomType").jqxComboBox({ source: ddlStockTypedataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlCustomType").val(<%=this.curStockIn.CustomType%>);

            //入账公司
            var ddlCorpIdurl = "../User/Handler/AuthSelfCorpHandler.ashx";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlCorpId").jqxComboBox({ selectedIndex: 0, source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 180, height: 25, autoDropDownHeight: true, searchMode: "containsignorecase" });         
            $("#ddlCorpId").jqxComboBox("val", <%=this.curStockIn.CorpId%>);

            //所属部门
            var ddlDeptIdurl = "../User/Handler/DeptDDLHandler.ashx";
            var ddlDeptIdsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: ddlDeptIdurl, async: false };
            var ddlDeptIddataAdapter = new $.jqx.dataAdapter(ddlDeptIdsource);
            $("#ddlDeptId").jqxComboBox({ source: ddlDeptIddataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 180, height: 25, autoDropDownHeight: true, searchMode: "containsignorecase", disabled: true });
            $("#ddlDeptId").val(<%=this.curDeptId%>);

            //净重
            $("#txbNetAmount").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 4, Digits: 8 });
            $("#txbNetAmount").jqxNumberInput("val", <%=this.curStockIn.NetAmount%>);

            //毛重
            $("#txbGrossAmount").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 4, Digits: 8 });
            $("#txbGrossAmount").jqxNumberInput("val", <%=this.curStockIn.GrossAmount%>);

            //计量单位
            var ddlMUIdurl = "../BasicData/Handler/MUDDLHandler.ashx";
            var ddlMUIdsource = { datatype: "json", datafields: [{ name: "MUId" }, { name: "MUName" }], url: ddlMUIdurl, async: false };
            var ddlMUIddataAdapter = new $.jqx.dataAdapter(ddlMUIdsource);
            $("#ddlMUId").jqxComboBox({ source: ddlMUIddataAdapter, displayMember: "MUName", valueMember: "MUId", width: 180, height: 25, autoDropDownHeight: true, disabled: true  });
            $("#ddlMUId").jqxComboBox("val", <%=this.curStockIn.UintId%>);

            //捆数
            $("#nbBundles").jqxNumberInput({ width: 180, height: 25, spinButtons: true, decimalDigits: 0, Digits: 3 });
            $("#nbBundles").val(<%=this.curStockIn.Bundles%>);

            //品种
            var ddlAssetIdurl = "../BasicData/Handler/AssetDDLHandler.ashx";
            var ddlAssetIdsource = { datatype: "json", url: ddlAssetIdurl, async: false };
            var ddlAssetIddataAdapter = new $.jqx.dataAdapter(ddlAssetIdsource);
            $("#ddlAssetId").jqxComboBox({ source: ddlAssetIddataAdapter, displayMember: "AssetName", valueMember: "AssetId", width: 180, height: 25});

            $("#ddlAssetId").on("change", function (event) {
                var index = event.args.index;
                var item = ddlAssetIddataAdapter.records[index];
                $("#ddlMUId").val(item.MUId);
            });

            $("#ddlAssetId").jqxComboBox("val", <%=this.curStockIn.AssetId%>);

            //交货地
            var ddlDeliverPlaceIdurl = "../BasicData/Handler/DeliverPlaceDDLHandler.ashx";
            var ddlDeliverPlaceIdsource = { datatype: "json", datafields: [{ name: "DPId" }, { name: "DPName" }], url: ddlDeliverPlaceIdurl, async: false };
            var ddlDeliverPlaceIddataAdapter = new $.jqx.dataAdapter(ddlDeliverPlaceIdsource);
            $("#ddlDeliverPlaceId").jqxComboBox({ source: ddlDeliverPlaceIddataAdapter, displayMember: "DPName", valueMember: "DPId", width: 180, height: 25 });
            $("#ddlDeliverPlaceId").jqxComboBox("val", <%=this.curStockIn.DeliverPlaceId%>);

            //生产商
            var ddlProducterurl = "../BasicData/Handler/ProducerDDLHandler.ashx";
            var ddlProductersource = { datatype: "json", datafields: [{ name: "ProducerId" }, { name: "ProducerName" }], url: ddlProducterurl, async: false };
            var ddlProducterdataAdapter = new $.jqx.dataAdapter(ddlProductersource);
            $("#ddlProducter").jqxComboBox({ source: ddlProducterdataAdapter, displayMember: "ProducerName", valueMember: "ProducerId", width: 180, height: 25, autoDropDownHeight: true });

            $('#ddlProducter').on('select', function (event) {
                var args = event.args;
                if (args != undefined) {
                    var item = event.args.item;
                    if (item != null) {
                        //品牌
                        var ddlBrandIdurl = "../BasicData/Handler/BrandDDLHandler.ashx?pid=" + $("#ddlProducter").val();
                        var ddlBrandIdsource = { datatype: "json", datafields: [{ name: "BrandId" }, { name: "BrandName" }], url: ddlBrandIdurl, async: false };
                        var ddlBrandIddataAdapter = new $.jqx.dataAdapter(ddlBrandIdsource);
                        $("#ddlBrandId").jqxComboBox({ source: ddlBrandIddataAdapter, displayMember: "BrandName", valueMember: "BrandId", width: 180, height: 25, autoDropDownHeight: true });
                    }
                }
            });

            $("#ddlProducter").jqxComboBox("val", <%=this.curStockIn.ProducerId%>);

            //品牌
            var ddlBrandIdurl = "../BasicData/Handler/BrandDDLHandler.ashx?pid=" + $("#ddlProducter").val();
            var ddlBrandIdsource = { datatype: "json", datafields: [{ name: "BrandId" }, { name: "BrandName" }], url: ddlBrandIdurl, async: false };
            var ddlBrandIddataAdapter = new $.jqx.dataAdapter(ddlBrandIdsource);
            $("#ddlBrandId").jqxComboBox({ source: ddlBrandIddataAdapter, displayMember: "BrandName", valueMember: "BrandId", width: 180, height: 25, autoDropDownHeight: true });
            $("#ddlBrandId").val(<%=this.curStockIn.BrandId%>);

            //原产地
            $("#txbOriginPlace").jqxInput({ height: 25, width: 180 });
            $("#txbOriginPlace").val("<%=this.curStockIn.OriginPlace%>");

            //规格
            $("#txbFormat").jqxInput({ height: 25, width: 180 });
            $("#txbFormat").val("<%=this.curStockIn.Format%>");

            //权证编号
            $("#txbPaperNo").jqxInput({ height: 25, width: 180 });
            $("#txbPaperNo").val("<%=this.curStockIn.PaperNo%>");

            //单据保管人
            var ddlPaperHolderrurl = "../User/Handler/EmpDDLHandler.ashx";
            var ddlPaperHoldersource = { datatype: "json", datafields: [{ name: "EmpId" }, { name: "Name" }], url: ddlPaperHolderrurl, async: false };
            var ddlPaperHolderdataAdapter = new $.jqx.dataAdapter(ddlPaperHoldersource);
            $("#ddlPaperHolder").jqxComboBox({ source: ddlPaperHolderdataAdapter, displayMember: "Name", valueMember: "EmpId", width: 180, height: 25, searchMode: "containsignorecase" });
            $("#ddlPaperHolder").jqxComboBox("val", <%=this.curStockIn.PaperHolder%>);

            //卡号
            $("#txbCardNo").jqxInput({ height: 25, width: 180 });
            $("#txbCardNo").val("<%=this.curStockIn.CardNo%>");

            //业务单号
            $("#txbStockName").jqxInput({ height: 25, width: 180 });
            $("#txbStockName").val("<%=this.curStockIn.RefNo%>");          

            //单据类型
            var stockTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.StockType%>", async: false };
            var stockTypeDataAdapter = new $.jqx.dataAdapter(stockTypeSource);
            $("#selStockType").jqxComboBox({ source: stockTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });
            $("#selStockType").val(<%=this.curStockIn.StockType%>);

            //入库类型
            var stockOperateTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + "<%=(int)NFMT.Data.StyleEnum.出入库类型%>", async: false };
            var stockOperateTypeDataAdapter = new $.jqx.dataAdapter(stockOperateTypeSource);
            $("#selStockOperateType").jqxComboBox({ source: stockOperateTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 100, height: 25, autoDropDownHeight: true });
            $("#selStockOperateType").val(<%=this.curStockIn.StockOperateType%>);

            var billAttachNeedUpdate = false;
            $("#filStockAttach").attr("disabled", true);

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlCustomType", message: "必须选择报关状态", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlCustomType").val() > 0;
                            }
                        },
                        {
                            input: "#ddlCorpId", message: "入账公司必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlCorpId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlDeptId", message: "所属部门必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlDeptId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlAssetId", message: "品种必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlAssetId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlDeliverPlaceId", message: "交货地必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlDeliverPlaceId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlProducter", message: "生产商必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlProducter").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlPaperHolder", message: "单据保管人必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlPaperHolder").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlBrandId", message: "品牌必选", action: "change,keyup,blur", rule: function (input, commit) {
                                return $("#ddlBrandId").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#txbNetAmount", message: "净重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#txbNetAmount').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#txbGrossAmount", message: "毛重必须大于或等于净重", action: "keyup,blur", rule: function (input, commit) {
                                return $('#txbNetAmount').jqxNumberInput("val") <= $('#txbGrossAmount').jqxNumberInput("val");
                            }
                        },
                        {
                            input: "#txbGrossAmount", message: "毛重必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#txbGrossAmount').jqxNumberInput("val") > 0;
                            }
                        },
                        {
                            input: "#nbBundles", message: "捆数必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                return $('#nbBundles').jqxNumberInput("val") > 0;
                            }
                        },
                        { input: "#txbStockName", message: "请输入业务单号", action: "keyup,blur", rule: "required" }
                    ]
            });

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
                  { text: "查看", datafield: "AttachPath", cellsrenderer: attachViewRender },
                  {
                      text: "操作", datafield: "Edit", width: 70, columntype: "button", width: 90, cellsrenderer: function () {
                          return "删除";
                      }, buttonclick: function (row) {
                          if (!confirm("确认删除？")) { return; }
                          var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
                          if (item.AttachId == "undefined" || item.AttachId <= 0) return;
                          $.post("../Files/Handler/AttachUpdateStatusHandler.ashx", { aid: item.AttachId, s: statusEnum.已作废 },
                                  function (result) {
                                      attachSource.url = attachUrl;
                                      $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");

                                      if (item.AttachType == AttachTypeEnum.BillAttach) {
                                          $("#filStockAttach").attr("disabled", false);
                                          billAttachNeedUpdate = true;
                                      }
                                  }
                              );
                      }
                  }
                ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (billAttachNeedUpdate) {
                    var fileIdsBillAttach = new Array();
                    $("input[class='BillAttachClass']").each(function () {
                        if ($(this).val() != "") {
                            fileIdsBillAttach.push($(this)[0].id);
                            return false;
                        }
                    });
                    if (fileIdsBillAttach.length < 1) {
                        alert("请上传单据附件");
                        return;
                    }
                }

                if (!confirm("确认修改入库登记?")) { return;}

                var stockInModel = {
                    StockInId:"<%=this.curStockIn.StockInId%>",
                    CorpId: $("#ddlCorpId").val(),
                    DeptId: $("#ddlDeptId").val(),
                    StockInDate: $("#txbStockInDate").val(),
                    CustomType: $("#ddlCustomType").val(),
                    GrossAmount: $("#txbGrossAmount").val(),
                    NetAmount: $("#txbNetAmount").val(),
                    UintId: $("#ddlMUId").val(),
                    AssetId: $("#ddlAssetId").val(),
                    Bundles: $("#nbBundles").val(),
                    BrandId: $("#ddlBrandId").val(),
                    DeliverPlaceId: $("#ddlDeliverPlaceId").val(),
                    ProducerId: $("#ddlProducter").val(),
                    PaperNo: $("#txbPaperNo").val(),
                    PaperHolder: $("#ddlPaperHolder").val(),
                    CardNo: $("#txbCardNo").val(),
                    RefNo: $("#txbStockName").val(),
                    Format: $("#txbFormat").val(),
                    OriginPlace: $("#txbOriginPlace").val(),
                    StockType: $("#selStockType").val(),
                    StockOperateType: $("#selStockOperateType").val()
                };

                var fileIdsStockIn = new Array();
                $("input[class='StockInAttach']").each(function () {
                    if ($(this).val() != "") {
                        fileIdsStockIn.push($(this)[0].id);
                    }
                });

                $.post("Handler/StockInUpdateHandler.ashx",
                    {
                        StockIn: JSON.stringify(stockInModel)
                    },
                    function (result) {
                        var id = "<%=this.curStockIn.StockInId%>";
                        var obj = JSON.parse(result);
                        if (obj.ResultStatus.toString() == "0") {
                            AjaxFileUpload(fileIdsStockIn, id, AttachTypeEnum.StockInAttach);
                            if (billAttachNeedUpdate) {
                                AjaxFileUpload(fileIdsBillAttach, id, AttachTypeEnum.BillAttach);
                            }
                        }
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            window.document.location = "StockInList.aspx";
                        }
                    }
                );
            });
        });

        var line = 1;
        function addStockInAttach(i) {
            if (i > line) {
                var nli = $("<li><strong>附件" + i + "：</strong><input id=\"file" + i + "\" type=\"file\" name=\"file" + i + "\" class=\"StockInAttach\" onchange=\"addStockInAttach(" + (i + 1) + ");\" /></li>");
                $("#ulAttach").append(nli);
                line++;
            }
        }
       
    </script>

</head>
<body>

    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            入库登记新增
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
                    <input type="file" id="filStockAttach" name="filStockAttach" class="BillAttachClass"/>
                </li>
            </ul>
        </div>
    </div>

    <%--<NFMT:Attach runat="server" ID="attach1" AttachStyle="Update" AttachType="StockInAttach" />--%>
    <div id="jqxAttachGridExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            附件
        </div>
        <div>
            <div id="jqxAttachGrid"></div>
        </div>
    </div>

    <div id="jqxAttachExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            附件
        </div>
        <div class="InfoExpander" runat="server" id="attachInfo">
            <ul id="ulAttach">
                <li>
                    <strong>附件1：</strong>
                    <input id="file1" type="file" name="file1" class="StockInAttach" onchange="addStockInAttach(2);" />
                </li>
            </ul>
        </div>
    </div>

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="确认修改" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="StockInList.aspx" id="btnCancel">取消</a>
    </div>
</body>
    
</html>


