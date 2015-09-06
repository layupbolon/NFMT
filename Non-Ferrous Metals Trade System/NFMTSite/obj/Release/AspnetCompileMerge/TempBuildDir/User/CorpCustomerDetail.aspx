<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpCustomerDetail.aspx.cs" Inherits="NFMTSite.User.CorpCustomerDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/ajaxfileupload.js"></script>

    <style type="text/css">
        .txt {
            text-align: center;
            font-weight: bold;
            font-size: medium;
        }
    </style>

    <script type="text/javascript">
        var auditUrl = "../WorkFlow/Handler/AuditProgressListHandler.ashx?b=" + "<%=this.corporation.DataBaseName%>" + "&t=" + "<%=this.corporation.TableName%>" + "&id=" + "<%=this.corporation.CorpId%>";

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            $("#jqxAttachExpander").jqxExpander({ width: "98%" });

            //营业执照注册号
            $("#txbBusinessLicenseCode").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbBusinessLicenseCode").jqxInput("val", "<%=this.corpDetail.BusinessLicenseCode%>");

            //注册资本
            $("#nbRegisteredCapital").jqxNumberInput({ width: "99%", height: 30, spinButtons: true, decimalDigits: 2, Digits: 10, readOnly: true });
            $("#nbRegisteredCapital").jqxNumberInput("val", "<%=this.corpDetail.RegisteredCapital%>");
            //币种
            var currencyUrl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var currencySource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyUrl, async: false };
            var currencyDataAdapter = new $.jqx.dataAdapter(currencySource);
            $("#ddlCurrency").jqxComboBox({ source: currencyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: "99%", height: 30, autoDropDownHeight: true, disabled: true });
            $("#ddlCurrency").jqxComboBox("val", "<%=this.corpDetail.CurrencyId%>");

            //注册时间
            $("#dtRegisteredDate").jqxDateTimeInput({ width: "99%", formatString: "yyyy-MM-dd", disabled: true });
            $("#dtRegisteredDate").jqxDateTimeInput("val", new Date("<%=this.corpDetail.RegisteredDate%>"));

            //公司性质
            $("#txbCorpProperty").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpProperty").jqxInput("val", "<%=this.corpDetail.CorpProperty%>");

            //经营范围
            $("#txbBusinessScope").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbBusinessScope").jqxInput("val", "<%=this.corpDetail.BusinessScope%>");

            //税务注册号
            $("#txbTaxRegisteredCode").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbTaxRegisteredCode").jqxInput("val", "<%=this.corpDetail.TaxRegisteredCode%>");

            //组织机构注册号
            $("#txbOrganizationCode").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbOrganizationCode").jqxInput("val", "<%=this.corpDetail.OrganizationCode%>");

            if ("<%=this.corpDetail.IsChildCorp%>" == "True") {
                $("#rbYes").jqxRadioButton({ width: 100, height: 22, checked: true, disabled: true });
                $("#rbNo").jqxRadioButton({ width: 100, height: 22, disabled: true });
            }
            else {
                $("#rbNo").jqxRadioButton({ width: 100, height: 22, checked: true, disabled: true });
                $("#rbYes").jqxRadioButton({ width: 100, height: 22, disabled: true });
            }

            //公司名称
            $("#txbCorpName").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpName").jqxInput("val", "<%=this.corporation.CorpName%>");

            //公司英文名称
            $("#txbCorpEName").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpEName").jqxInput("val", "<%=this.corporation.CorpEName%>");

            //纳税人识别号
            $("#txbTaxPayerId").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbTaxPayerId").jqxInput("val", "<%=this.corporation.TaxPayerId%>");

            //地址
            $("#txbCorpAddress").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpAddress").jqxInput("val", "<%=this.corporation.CorpAddress%>");

            //英文地址
            $("#txbCorpEAddress").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpEAddress").jqxInput("val", "<%=this.corporation.CorpEAddress%>");

            //联系电话
            $("#txbCorpTel").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpTel").jqxInput("val", "<%=this.corporation.CorpTel%>");

            //传真
            $("#txbCorpFax").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbCorpFax").jqxInput("val", "<%=this.corporation.CorpFax%>");

            //客户类型
            var ddlCustomTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=<%=(int)NFMT.Data.StyleEnum.客户类型%>", async: false };
            var ddlCustomTypeDataAdapter = new $.jqx.dataAdapter(ddlCustomTypeSource);
            $("#ddlCustomType").jqxDropDownList({ source: ddlCustomTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: "99%", height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlCustomType").jqxDropDownList("val", "<%=this.corpDetail.CorpType%>");

            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 30, disabled: true });
            $("#txbMemo").jqxInput("val", "<%=this.corpDetail.Memo%>");

            $("#BusinessLiceneseAttach1").attr("disabled", true);
            $("#TaxAttach1").attr("disabled", true);
            $("#OrganizationAttach1").attr("disabled", true);
            $("#CertifyAttach1").attr("disabled", true);

            var attachUrl = "Handler/CorpCustomerAttachHandler.ashx?id=" + "<%=this.corpDetail.DetailId%>";
            var attachFormatedData = "";
            var attachTotalrecords = 0;
            attachSource =
            {
                url: attachUrl,
                datafields: [
                    { name: "CorpDetailAttachId", type: "int" },
                    { name: "DetailId", type: "int" },
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
                sortcolumn: "cda.AttachType",
                sortdirection: "asc",
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || "cda.AttachType";
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

            $("#btnAudit").jqxInput();
            $("#btnInvalid").jqxInput();
            $("#btnGoBack").jqxInput();
            $("#btnFreeze").jqxInput();
            $("#btnUnFreeze").jqxInput();

            var id = "<%=this.corpDetail.CorpId%>";

            $("#btnAudit").on("click", function (e) {
                var paras = {
                    mid: 49,
                    model: $("#hidModel").val()
                };
                ShowModalDialog(paras, e);
            });

            $("#btnInvalid").on("click", function () {
                if (!confirm("确认作废？")) { return; }
                var operateId = operateEnum.作废;
                $.post("Handler/CorpDetailStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CorporationList.aspx";
                    }
                );
            });

            $("#btnGoBack").on("click", function () {
                if (!confirm("确认撤返？")) { return; }
                var operateId = operateEnum.撤返;
                $.post("Handler/CorpDetailStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CorporationList.aspx";
                    }
                );
            });

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结？")) { return; }
                var operateId = operateEnum.冻结;
                $.post("Handler/CorpDetailStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CorporationList.aspx";
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                if (!confirm("确认解除冻结？")) { return; }
                var operateId = operateEnum.解除冻结;
                $.post("Handler/CorpDetailStatusHandler.ashx", { id: id, oi: operateId },
                    function (result) {
                        alert(result);
                        document.location.href = "CorporationList.aspx";
                    }
                );
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            企业明细<input type="hidden" id="hidModel" runat="server" />
        </div>
        <div>
            <table class="tableStyle">
                <tbody>
                    <tr>
                        <td style="width: 138px" rowspan="8">
                            <div class="txt">证件类型</div>
                        </td>
                        <td style="width: 130px; height: 32px" rowspan="4">
                            <div class="txt">
                                营业执照
                            </div>
                        </td>
                        <td style="width: 120px" colspan="1">
                            <div class="txt">注册号</div>
                        </td>
                        <td style="width: 130px" colspan="1">
                            <input type="text" id="txbBusinessLicenseCode" />
                        </td>
                        <td style="width: 120px" colspan="1">
                            <div class="txt">注册资本</div>
                        </td>
                        <td style="width: 100px" colspan="1">
                            <div id="nbRegisteredCapital"></div>
                        </td>
                        <td style="width: 100px" colspan="1">
                            <div class="txt">币种</div>
                        </td>
                        <td style="width: 100px" colspan="1">
                            <div id="ddlCurrency" style="float: left"></div>
                        </td>
                        <tr>
                            <td style="width: 120px" colspan="1">
                                <div class="txt">注册时间</div>
                            </td>
                            <td style="width: 120px" colspan="1">
                                <div id="dtRegisteredDate"></div>
                            </td>
                            <td style="width: 120px" colspan="1">
                                <div class="txt">公司性质</div>
                            </td>
                            <td colspan="3">
                                <input type="text" id="txbCorpProperty" />
                            </td>
                            <tr>
                                <td colspan="1">
                                    <div class="txt">
                                        经营范围
                                    </div>
                                </td>
                                <td colspan="5">
                                    <input type="text" id="txbBusinessScope" />
                                </td>
                                <tr>
                                    <td colspan="1">
                                        <div class="txt">电子文档上传</div>
                                    </td>
                                    <td colspan="5">
                                        <div>
                                            <ul id="BusinessLiceneseAttach">
                                                <li style="list-style:none">
                                                    <strong>营业执照附件：</strong>
                                                    <input id="BusinessLiceneseAttach1" type="file" name="BusinessLiceneseAttach1" class="BusinessLiceneseAttachClass"  />
                                                </li>
                                            </ul>
                                        </div>
                                    </td>
                                    <tr>
                                        <td rowspan="2">
                                            <div class="txt">
                                                税务
                                            </div>
                                        </td>
                                        <td>
                                            <div class="txt">
                                                注册号
                                            </div>
                                        </td>
                                        <td colspan="5">
                                            <input type="text" id="txbTaxRegisteredCode" />
                                        </td>
                                        <tr>
                                            <td>
                                                <div class="txt">
                                                    电子文档上传
                                                </div>
                                            </td>
                                            <td colspan="5">
                                                <div>
                                                    <ul id="TaxAttach">
                                                        <li style="list-style:none">
                                                            <strong>税务附件：</strong>
                                                            <input id="TaxAttach1" type="file" name="TaxAttach1" class="TaxAttachClass"  />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <tr>
                                                <td colspan="1" rowspan="2">
                                                    <div class="txt">组织机构</div>
                                                </td>
                                                <td>
                                                    <div class="txt">注册号</div>
                                                </td>
                                                <td colspan="5">
                                                    <input type="text" id="txbOrganizationCode" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="txt">电子文档上传</div>
                                                </td>
                                                <td colspan="5">
                                                    <div>
                                                        <ul id="OrganizationAttach">
                                                            <li style="list-style: none">
                                                                <strong>组织机构附件：</strong>
                                                                <input id="OrganizationAttach1" type="file" name="OrganizationAttach1" class="OrganizationAttachClass" />
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px" colspan="2">
                                                    <div class="txt">是否为子公司</div>
                                                    <td>
                                                        <ul style="list-style-type: none;">
                                                            <li style="float: left; line-height: 30px;">
                                                                <div id="rbYes">是</div>
                                                            </li>
                                                            <li style="float: left; line-height: 30px;">
                                                                <div id="rbNo">否</div>
                                                            </li>
                                                        </ul>
                                                    </td>
                                                    <td>
                                                        <div class="txt">证明文件</div>
                                                    </td>
                                                    <td colspan="4">
                                                        <div>
                                                            <ul id="CertifyAttach">
                                                                <li style="list-style: none">
                                                                    <strong>证明文件附件：</strong>
                                                                    <input id="CertifyAttach1" type="file" name="CertifyAttach1" class="CertifyAttachClass" />
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px" rowspan="4">
                                                    <div class="txt">开票资料</div>
                                                    <td style="height: 30px">
                                                        <div class="txt">名称</div>
                                                        <td colspan="2">
                                                            <input type="text" id="txbCorpName" />
                                                        </td>
                                                        <td>
                                                            <div class="txt">英文名称</div>
                                                        </td>
                                                        <td colspan="3">
                                                            <input type="text" id="txbCorpEName" />
                                                        </td>
                                                    </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px">
                                                    <div class="txt">地址</div>
                                                    <td colspan="2">
                                                        <input type="text" id="txbCorpAddress" />
                                                    </td>
                                                    <td>
                                                        <div class="txt">英文地址</div>
                                                    </td>
                                                    <td colspan="3">
                                                        <input type="text" id="txbCorpEAddress" />
                                                    </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px">
                                                    <div class="txt">联系电话</div>
                                                    <td colspan="2">
                                                        <input type="text" id="txbCorpTel" />
                                                    </td>
                                                    <td>
                                                        <div class="txt">传真</div>
                                                    </td>
                                                    <td colspan="3">
                                                        <input type="text" id="txbCorpFax" />
                                                    </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px">
                                                    <div class="txt">纳税人识别号</div>
                                                    <td colspan="6">
                                                        <input type="text" id="txbTaxPayerId" />
                                                    </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px" colspan="2">
                                                    <div class="txt">
                                                        客户类型
                                                    </div>
                                                    <td colspan="6">
                                                        <div id="ddlCustomType"></div>
                                                    </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px" colspan="2">
                                                    <div class="txt">
                                                        备注
                                                    </div>
                                                    <td colspan="6">
                                                        <input type="text" id="txbMemo" />
                                                    </td>
                                                </td>
                                            </tr>
                                        </tr>
                                    </tr>
                                </tr>
                            </tr>
                        </tr>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div id="jqxAttachExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            附件
        </div>
        <div>
            <div id="jqxAttachGrid"></div>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAudit" value="提交审核" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnInvalid" value="作废" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnGoBack" value="撤返" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnFreeze" value="冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="width: 120px; height: 25px;" />&nbsp;&nbsp;&nbsp;
    </div>
</body>
<script type="text/javascript" src="../js/AuditProgress.js"></script>
</html>
