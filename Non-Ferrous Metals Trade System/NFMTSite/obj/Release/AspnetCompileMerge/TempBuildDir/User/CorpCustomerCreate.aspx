<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpCustomerCreate.aspx.cs" Inherits="NFMTSite.User.CorpCustomerCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户新增</title>
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
        var purchaser = "<%=NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.客户类型)["Purchaser"].StyleDetailId%>";//供货商

        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });

            //营业执照注册号
            $("#txbBusinessLicenseCode").jqxInput({ width: "99%", height: 30 });

            //注册资本
            $("#nbRegisteredCapital").jqxNumberInput({ width: "99%", height: 30, spinButtons: true, decimalDigits: 2, Digits: 10 });
            //币种
            var currencyUrl = "../BasicData/Handler/CurrencDDLHandler.ashx";
            var currencySource = { datatype: "json", datafields: [{ name: "CurrencyId" }, { name: "CurrencyName" }], url: currencyUrl, async: false };
            var currencyDataAdapter = new $.jqx.dataAdapter(currencySource);
            $("#ddlCurrency").jqxComboBox({ source: currencyDataAdapter, displayMember: "CurrencyName", valueMember: "CurrencyId", width: "99%", height: 30, autoDropDownHeight: true });

            //注册时间
            $("#dtRegisteredDate").jqxDateTimeInput({ width: "99%", formatString: "yyyy-MM-dd" });

            //公司性质
            $("#txbCorpProperty").jqxInput({ width: "99%", height: 30 });

            //经营范围
            $("#txbBusinessScope").jqxInput({ width: "99%", height: 30 });

            //税务注册号
            $("#txbTaxRegisteredCode").jqxInput({ width: "99%", height: 30 });

            //组织机构注册号
            $("#txbOrganizationCode").jqxInput({ width: "99%", height: 30 });

            //是
            $("#rbYes").jqxRadioButton({ width: 100, height: 22 });

            //否
            $("#rbNo").jqxRadioButton({ width: 100, height: 22, checked: true });

            //公司名称
            $("#txbCorpName").jqxInput({ width: "99%", height: 30 });

            //公司英文名称
            $("#txbCorpEName").jqxInput({ width: "99%", height: 30 });

            //纳税人识别号
            $("#txbTaxPayerId").jqxInput({ width: "99%", height: 30 });

            //地址
            $("#txbCorpAddress").jqxInput({ width: "99%", height: 30 });

            //英文地址
            $("#txbCorpEAddress").jqxInput({ width: "99%", height: 30 });

            //联系电话
            $("#txbCorpTel").jqxInput({ width: "99%", height: 30 });

            //传真
            $("#txbCorpFax").jqxInput({ width: "99%", height: 30 });

            //客户类型
            var ddlCustomTypeSource = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=<%=(int)NFMT.Data.StyleEnum.客户类型%>", async: false };
            var ddlCustomTypeDataAdapter = new $.jqx.dataAdapter(ddlCustomTypeSource);
            $("#ddlCustomType").jqxDropDownList({ source: ddlCustomTypeDataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: "99%", height: 25, autoDropDownHeight: true});

            //备注
            $("#txbMemo").jqxInput({ width: "99%", height: 30 });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
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

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }
                
                var isPurchaser = $('#ddlCustomType').jqxDropDownList("val") == purchaser ? true : false;

                var fileIds1 = new Array();
                $("input[class='BusinessLiceneseAttachClass']").each(function () {
                    if ($(this).val() != "") {
                        fileIds1.push($(this)[0].id);
                        return false;
                    }
                });
                if (fileIds1.length < 1 && isPurchaser)
                {
                    alert("请上传营业执照附件");
                    return;
                }
                
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
                
                var fileIds4 = new Array();
                $("input[class='CertifyAttachClass']").each(function () {
                    if ($(this).val() != "") {
                        fileIds4.push($(this)[0].id);
                        return false;
                    }
                });
                //if (fileIds4.length < 1) {
                //    alert("请上传证明文件");
                //    return;
                //}

                $("#btnAdd").jqxButton({ disabled: true });

                var corp = {
                    //CorpId:
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
                    //DetailId
                    //CorpId
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
                    CorpType:$('#ddlCustomType').jqxDropDownList("val"),
                    IsSelf: false,
                    //CertifyAttach
                    Memo: $("#txbMemo").val()
                }

                $.post("Handler/CorpDetailCreateHandler.ashx", {
                    corp: JSON.stringify(corp),
                    corpDetail: JSON.stringify(corpDetail)
                },
                function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    if (obj.ResultStatus.toString() == "0" && isPurchaser) {
                        AjaxFileUpload(fileIds1, obj.ReturnValue, AttachTypeEnum.BusinessLiceneseAttach);
                        AjaxFileUpload(fileIds2, obj.ReturnValue, AttachTypeEnum.TaxAttach);
                        AjaxFileUpload(fileIds3, obj.ReturnValue, AttachTypeEnum.OrganizationAttach);
                        if (fileIds4.length >= 1) {
                            AjaxFileUpload(fileIds4, obj.ReturnValue, AttachTypeEnum.CertifyAttach);
                        }
                    }                    
                    //$("#btnAdd").jqxButton({ disabled: false });
                    if (obj.ResultStatus.toString() == "0") {
                        document.location.href = "CorporationList.aspx";
                    }
                });
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            企业新增
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

    <div style="width: 80%; text-align: center;">
        <input type="button" id="btnAdd" value="添加" />&nbsp;&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="CorporationList.aspx" id="btnCancel">取消</a>
    </div>
</body>
</html>
