<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpCustomerUpdate.aspx.cs" Inherits="NFMTSite.User.CorpCustomerUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户修改</title>
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
        var attachSource = null;
        var purchaser = "<%=NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.客户类型)["Purchaser"].StyleDetailId%>";//供货商

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" }); 
            $("#jqxAttachExpander").jqxExpander({ width: "98%" });

            //营业执照注册号
            $("#txbBusinessLicenseCode").jqxInput({ width: "99%", height: 30 });
            $("#txbBusinessLicenseCode").jqxInput("val", "<%=this.corpDetail.BusinessLicenseCode%>");

            //注册资本
            $("#nbRegisteredCapital").jqxNumberInput({ width: "99%", height: 30, spinButtons: true, decimalDigits: 2, Digits: 10 });
            $("#nbRegisteredCapital").jqxNumberInput("val", "<%=this.corpDetail.RegisteredCapital%>");
            //币种
            var currencyUrl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var currencySource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyUrl, async: false };
            var currencyDataAdapter = new $.jqx.dataAdapter(currencySource);
            $("#ddlCurrency").jqxComboBox({ source: currencyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: "99%", height: 30, autoDropDownHeight: true });
            $("#ddlCurrency").jqxComboBox("val", "<%=this.corpDetail.CurrencyId%>");

            //注册时间
            $("#dtRegisteredDate").jqxDateTimeInput({ width: "99%", formatString: "yyyy-MM-dd" });
            $("#dtRegisteredDate").jqxDateTimeInput("val", new Date("<%=this.corpDetail.RegisteredDate%>"));

            //公司性质
            $("#txbCorpProperty").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpProperty").jqxInput("val", "<%=this.corpDetail.CorpProperty%>");

            //经营范围
            $("#txbBusinessScope").jqxInput({ width: "99%", height: 30 });
            $("#txbBusinessScope").jqxInput("val", "<%=this.corpDetail.BusinessScope%>");

            //税务注册号
            $("#txbTaxRegisteredCode").jqxInput({ width: "99%", height: 30 });
            $("#txbTaxRegisteredCode").jqxInput("val", "<%=this.corpDetail.TaxRegisteredCode%>");

            //组织机构注册号
            $("#txbOrganizationCode").jqxInput({ width: "99%", height: 30 });
            $("#txbOrganizationCode").jqxInput("val", "<%=this.corpDetail.OrganizationCode%>");

            if ("<%=this.corpDetail.IsChildCorp%>" == "True") {
                $("#rbYes").jqxRadioButton({ width: 100, height: 22, checked: true });
                $("#rbNo").jqxRadioButton({ width: 100, height: 22 });
            }
            else {
                $("#rbNo").jqxRadioButton({ width: 100, height: 22, checked: true });
                $("#rbYes").jqxRadioButton({ width: 100, height: 22 });
            }

            //公司名称
            $("#txbCorpName").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpName").jqxInput("val", "<%=this.corporation.CorpName%>");

            //公司英文名称
            $("#txbCorpEName").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpEName").jqxInput("val", "<%=this.corporation.CorpEName%>");

            //纳税人识别号
            $("#txbTaxPayerId").jqxInput({ width: "99%", height: 30 });
            $("#txbTaxPayerId").jqxInput("val", "<%=this.corporation.TaxPayerId%>");

            //地址
            $("#txbCorpAddress").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpAddress").jqxInput("val", "<%=this.corporation.CorpAddress%>");

            //英文地址
            $("#txbCorpEAddress").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpEAddress").jqxInput("val", "<%=this.corporation.CorpEAddress%>");

            //联系电话
            $("#txbCorpTel").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpTel").jqxInput("val", "<%=this.corporation.CorpTel%>");

            //传真
            $("#txbCorpFax").jqxInput({ width: "99%", height: 30 });
            $("#txbCorpFax").jqxInput("val", "<%=this.corporation.CorpFax%>");

            //客户类型
            var ddlCustomTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=<%=(int)NFMT.Data.StyleEnum.客户类型%>", async: false };
            var ddlCustomTypeDataAdapter = new $.jqx.dataAdapter(ddlCustomTypeSource);
            $("#ddlCustomType").jqxDropDownList({ source: ddlCustomTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: "99%", height: 25, autoDropDownHeight: true });
            $("#ddlCustomType").jqxDropDownList("val", "<%=this.corpDetail.CorpType%>");

            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 30 });
            $("#txbMemo").jqxInput("val", "<%=this.corpDetail.Memo%>");

            var BusinessLiceneseAttachNeedUpdate = false;
            var TaxAttachNeedUpdate = false;
            var OrganizationAttachNeedUpdate = false;
            var CertifyAttachNeedUpdate = false;
            $("#BusinessLiceneseAttach1").attr("disabled", true);
            $("#TaxAttach1").attr("disabled", true);
            $("#OrganizationAttach1").attr("disabled", true);
            $("#CertifyAttach1").attr("disabled", true);

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#txbBusinessLicenseCode", message: "营业执照注册号不可为空", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#txbBusinessLicenseCode').jqxInput("val") != "";
                                else
                                    return true;
                            }
                        },
                        {
                            input: "#nbRegisteredCapital", message: "注册资本必须大于0", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#nbRegisteredCapital').jqxNumberInput("val") > 0;
                                else
                                    return true;
                            }
                        },
                        {
                            input: "#ddlCurrency", message: "币种不可为空", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#ddlCurrency').jqxComboBox("val") > 0;
                                else
                                    return true;
                            }
                        },
                        {
                            input: "#txbCorpProperty", message: "公司性质不可为空", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#txbCorpProperty').jqxInput("val") != "";
                                else
                                    return true;
                            }
                        },
                        {
                            input: "#txbBusinessScope", message: "经营范围不可为空", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#txbBusinessScope').jqxInput("val") != "";
                                else
                                    return true;
                            }
                        },
                        {
                            input: "#txbTaxRegisteredCode", message: "税务注册号不可为空", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#txbTaxRegisteredCode').jqxInput("val") != "";
                                else
                                    return true;
                            }
                        },
                        {
                            input: "#txbOrganizationCode", message: "组织机构注册号不可为空", action: "keyup,blur", rule: function (input, commit) {
                                if ($('#ddlCustomType').jqxDropDownList("val") == purchaser)
                                    return $('#txbOrganizationCode').jqxInput("val") != "";
                                else
                                    return true;
                            }
                        },
                        { input: "#txbCorpName", message: "公司名称不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbTaxPayerId", message: "纳税人识别号不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbCorpAddress", message: "地址不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbCorpTel", message: "联系电话不可为空", action: "keyup,blur", rule: "required" },
                        {
                            input: "#ddlCustomType", message: "客户类型必选", action: "keyup,blur", rule: function (input, commit) {
                                return $('#ddlCustomType').jqxDropDownList("val") > 0;
                            }
                        }
                    ]
            });

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

                                      if (item.AttachType == AttachTypeEnum.BusinessLiceneseAttach) {
                                          $("#BusinessLiceneseAttach1").attr("disabled", false);
                                          BusinessLiceneseAttachNeedUpdate = true;
                                      }
                                      else if (item.AttachType == AttachTypeEnum.TaxAttach) {
                                          $("#TaxAttach1").attr("disabled", false);
                                          TaxAttachNeedUpdate = true;
                                      }
                                      else if (item.AttachType == AttachTypeEnum.OrganizationAttach) {
                                          $("#OrganizationAttach1").attr("disabled", false);
                                          OrganizationAttachNeedUpdate = true;
                                      }
                                      else if (item.AttachType == AttachTypeEnum.CertifyAttach) {
                                          $("#CertifyAttach1").attr("disabled", false);
                                          CertifyAttachNeedUpdate = true;
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

                var isPurchaser = $('#ddlCustomType').jqxDropDownList("val") == purchaser ? true : false;

                if (BusinessLiceneseAttachNeedUpdate) {
                    var fileIds1 = new Array();
                    $("input[class='BusinessLiceneseAttachClass']").each(function () {
                        if ($(this).val() != "") {
                            fileIds1.push($(this)[0].id);
                            return false;
                        }
                    });
                    if (fileIds1.length < 1 && isPurchaser) {
                        alert("请上传营业执照附件");
                        return;
                    }
                }

                if (TaxAttachNeedUpdate) {
                    var fileIds2 = new Array();
                    $("input[class='TaxAttachClass']").each(function () {
                        if ($(this).val() != "") {
                            fileIds2.push($(this)[0].id);
                            return false;
                        }
                    });
                    if (fileIds2.length < 1 && isPurchaser) {
                        alert("请上传税务附件");
                        return;
                    }
                }

                if (OrganizationAttachNeedUpdate) {
                    var fileIds3 = new Array();
                    $("input[class='OrganizationAttachClass']").each(function () {
                        if ($(this).val() != "") {
                            fileIds3.push($(this)[0].id);
                            return false;
                        }
                    });
                    if (fileIds3.length < 1 && isPurchaser) {
                        alert("请上传组织机构代码附件");
                        return;
                    }
                }

                //if (CertifyAttachNeedUpdate) {
                //    var fileIds4 = new Array();
                //    $("input[class='CertifyAttachClass']").each(function () {
                //        if ($(this).val() != "") {
                //            fileIds4.push($(this)[0].id);
                //            return false;
                //        }
                //    });
                //    if (fileIds4.length < 1) {
                //        alert("请上传证明文件");
                //        return;
                //    }
                //}

                $("#btnUpdate").jqxButton({ disabled: true });

                var corp = {
                    CorpId:"<%=this.corporation.CorpId%>",
                    ParentId: 0,
                    //CorpCode:
                    CorpName: $("#txbCorpName").val(),
                    CorpEName: $("#txbCorpEName").val(),
                    TaxPayerId: $("#txbTaxPayerId").val(),
                    //CorpFullName:
                    //CorpFullEName
                    CorpAddress: $("#txbCorpAddress").val(),
                    CorpEAddress: $("#txbCorpEAddress").val(),
                    CorpTel: $("#txbCorpTel").val(),
                    CorpFax: $("#txbCorpFax").val(),
                    //CorpZip
                    //CorpType
                    IsSelf: false
                    //CorpStatus
                }

                var corpDetail = {
                    DetailId: "<%=this.corpDetail.DetailId%>",
                    CorpId: "<%=this.corporation.CorpId%>",
                    BusinessLicenseCode: $("#txbBusinessLicenseCode").val(),
                    RegisteredCapital: $("#nbRegisteredCapital").val(),
                    CurrencyId: $("#ddlCurrency").val() == "" ? 0 : $("#ddlCurrency").val(),
                    RegisteredDate: $("#dtRegisteredDate").val(),
                    CorpProperty: $("#txbCorpProperty").val(),
                    BusinessScope: $("#txbBusinessScope").val(),
                    //BusinessLiceneseAttach
                    TaxRegisteredCode: $("#txbTaxRegisteredCode").val(),
                    //TaxAttach
                    OrganizationCode: $("#txbOrganizationCode").val(),
                    //OrganizationAttach
                    IsChildCorp: $("#rbYes").val() ? true : false,
                    //CorpZip:
                    CorpType: $('#ddlCustomType').jqxDropDownList("val"),
                    IsSelf: false,
                    //CertifyAttach
                    Memo: $("#txbMemo").val()
                }

                $.post("Handler/CorpDetailUpdateHandler.ashx", {
                    corp: JSON.stringify(corp),
                    corpDetail: JSON.stringify(corpDetail)
                },
                function (result) {
                    var obj = JSON.parse(result);
                    if (obj.ResultStatus.toString() == "0") {
                        if (BusinessLiceneseAttachNeedUpdate) {
                            AjaxFileUpload(fileIds1, obj.ReturnValue, AttachTypeEnum.BusinessLiceneseAttach);
                        }
                        if (TaxAttachNeedUpdate) {
                            AjaxFileUpload(fileIds2, obj.ReturnValue, AttachTypeEnum.TaxAttach);
                        }
                        if (OrganizationAttachNeedUpdate) {
                            AjaxFileUpload(fileIds3, obj.ReturnValue, AttachTypeEnum.OrganizationAttach);
                        }
                        if (CertifyAttachNeedUpdate) {
                            AjaxFileUpload(fileIds4, obj.ReturnValue, AttachTypeEnum.CertifyAttach);
                        }
                    }
                    alert(obj.Message);
                    $("#btnUpdate").jqxButton({ disabled: false });
                    document.location.href = "CorporationList.aspx";
                });
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            企业修改
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

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnUpdate" value="修改" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="CorporationList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>