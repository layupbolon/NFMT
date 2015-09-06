<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUpdate.aspx.cs" Inherits="NFMTSite.User.ContactUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>联系人修改</title>
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

            $("#txbContactName").jqxInput({ height: 23, width: 200 });
            $("#txbContactCode").jqxInput({ height: 23, width: 200 });
            $("#txbContactTel").jqxInput({ height: 23, width: 200 });
            $("#txbContactFax").jqxInput({ height: 23, width: 200 });
            $("#txbContactAddress").jqxInput({ height: 23, width: 200 });

            var id = $("#hidId").val();

            //绑定 联系人公司
            var url = "Handler/CorpDDLHandler.ashx?isSelf=0";
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: "CorpId" },
                        { name: "CorpName" }
                    ],
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlCorpId").jqxComboBox({
                source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25, autoDropDownHeight: true
            });

            if ($("#hidCorpId").val() > 0) { $("#ddlCorpId").jqxComboBox("val", $("#hidCorpId").val()); }

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#cancel").jqxLinkButton({ height: 25, width: 100 });

            

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbContactName", message: "联系人姓名不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var contactName = $("#txbContactName").val();
                var contactCode = $("#txbContactCode").val();
                var contactTel = $("#txbContactTel").val();
                var contactFax = $("#txbContactFax").val();
                var contactAddress = $("#txbContactAddress").val();
                var corpId = $("#ddlCorpId").val();

                $.post("Handler/ContactUpdateHandler.ashx", {
                    contactName: contactName,
                    contactCode: contactCode,
                    contactTel: contactTel,
                    contactFax: contactFax,
                    contactAddress: contactAddress,
                    corpId: corpId,
                    id: id
                },
                    function (result) {
                        alert(result);
                        document.location.href = "ContactList.aspx";
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
            联系人修改
                        <input type="hidden" id="hidId" runat="server" />
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">联系人姓名：</span>
                    <span>
                        <input type="text" id="txbContactName" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">联系人身份证号：</span>
                    <span>
                        <input type="text" id="txbContactCode" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">联系人电话：</span>
                    <span>
                        <input type="text" id="txbContactTel" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">联系人传真：</span>
                    <span>
                        <input type="text" id="txbContactFax" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">联系人地址：</span>
                    <span>
                        <input type="text" id="txbContactAddress" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">联系人公司：</span>
                    <span>
                        <input type="hidden" id="hidCorpId" runat="server" /></span><div id="ddlCorpId" runat="server"></div>
                </li>
                <li>
                    <%--<span style="text-align: right; width: 15%;">&nbsp;</span>--%>
                    <span>
                        <input type="button" id="btnAdd" value="提交" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="ContactList.aspx" id="cancel">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
