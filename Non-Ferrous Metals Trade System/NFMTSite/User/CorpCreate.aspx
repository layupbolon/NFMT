<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpCreate.aspx.cs" Inherits="NFMTSite.User.CorpCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业新增</title>
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

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#txbCorpCode").jqxInput({ height: 23, width: 350 });
            $("#txbCorpName").jqxInput({ height: 23, width: 350 });
            $("#txbCorpEName").jqxInput({ height: 23, width: 350 });
            $("#txbTaxPlayer").jqxInput({ height: 23, width: 350 });
            $("#txbCorpFName").jqxInput({ height: 23, width: 350 });
            $("#txbCorpFEName").jqxInput({ height: 23, width: 350 });
            $("#txbCorpAddress").jqxInput({ height: 23, width: 350 });
            $("#txbCorpEAddress").jqxInput({ height: 23, width: 350 });
            $("#txbCorpTel").jqxInput({ height: 23, width: 350 });
            $("#txbCorpFax").jqxInput({ height: 23, width: 350 });
            $("#txbCorpZip").jqxInput({ height: 23, width: 350 });

            //绑定 公司类型
            var styleId = $("#hidStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlCorpType").jqxComboBox({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", width: 350, height: 25, autoDropDownHeight: true });

            //绑定 所属集团
            var url = "Handler/BlocDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "BlocId" }, { name: "BlocName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlBlocId").jqxComboBox({ source: dataAdapter, displayMember: "BlocName", valueMember: "BlocId", width: 350, height: 25, autoDropDownHeight: true });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbCorpCode", message: "企业代码不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbCorpName", message: "企业名称不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var BlocId = $("#ddlBlocId").val();
                var CorpCode = $("#txbCorpCode").val();
                var CorpName = $("#txbCorpName").val();
                var CorpEName = $("#txbCorpEName").val();
                var TaxPlayer = $("#txbTaxPlayer").val();
                var CorpFName = $("#txbCorpFName").val();
                var CorpFEName = $("#txbCorpFEName").val();
                var CorpAddress = $("#txbCorpAddress").val();
                var CorpEAddress = $("#txbCorpEAddress").val();
                var CorpTel = $("#txbCorpTel").val();
                var CorpFax = $("#txbCorpFax").val();
                var CorpZip = $("#txbCorpZip").val();
                var CorpType = $("#ddlCorpType").val();

                $.post("Handler/CorpCreateHandler.ashx", {
                    blocId: BlocId,
                    corpCode: CorpCode,
                    corpName: CorpName,
                    corpEName: CorpEName,
                    taxPlayer: TaxPlayer,
                    corpFName: CorpFName,
                    corpFEName: CorpFEName,
                    corpAddress: CorpAddress,
                    corpEAddress: CorpEAddress,
                    corpTel: CorpTel,
                    corpFax: CorpFax,
                    corpZip: CorpZip,
                    corpType: CorpType
                },
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
            企业新增
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="text-align: right; width: 15%;">公司代码：</span>
                    <span>
                        <input type="text" id="txbCorpCode" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司名称：</span>
                    <span>
                        <input type="text" id="txbCorpName" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司英文名称：</span>
                    <span>
                        <input type="text" id="txbCorpEName" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">纳税人识别号：</span>
                    <span>
                        <input type="text" id="txbTaxPlayer" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司全称：</span>
                    <span>
                        <input type="text" id="txbCorpFName" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司英文全称：</span>
                    <span>
                        <input type="text" id="txbCorpFEName" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司地址：</span>
                    <span>
                        <input type="text" id="txbCorpAddress" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司英文地址：</span>
                    <span>
                        <input type="text" id="txbCorpEAddress" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司电话：</span>
                    <span>
                        <input type="text" id="txbCorpTel" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司传真：</span>
                    <span>
                        <input type="text" id="txbCorpFax" runat="server" /></span></li>
                <li>
                    <span style="text-align: right; width: 15%;">公司邮编：</span>
                    <span>
                        <input type="text" id="txbCorpZip" runat="server" /></span></li>
                <li>
                    <input type="hidden" id="hidStyleId" runat="server" />
                    <span style="text-align: right; width: 15%;">公司类型：</span><div style="float: left;" id="ddlCorpType"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">所属集团：</span><div style="float: left;" id="ddlBlocId"></div>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="CorporationList.aspx" id="btnCancel">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
