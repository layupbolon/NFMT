<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptCreate.aspx.cs" Inherits="NFMTSite.User.DeptCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部门新增</title>
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
            

            $("#txbDeptCode").jqxInput({ height: 25, width: 200 });
            $("#txbDeptName").jqxInput({ height: 25, width: 200 });
            $("#txbDeptFullName").jqxInput({ height: 25, width: 200 });
            $("#txbDeptShort").jqxInput({ height: 25, width: 200 });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //绑定 所属公司
            var url = "Handler/CorpDDLHandler.ashx?isSelf=1";
            var source = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlCorpId").jqxComboBox({ selectedIndex: 0, source: dataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25, autoDropDownHeight: true });

            //绑定 部门类型
            var styleId = $("#hidStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlDeptType").jqxDropDownList({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true });

            ////绑定 部门级别
            //source = [
            //    { text: "1", value: 1 },
            //    { text: "2", value: 2 },
            //    { text: "3", value: 3 },
            //    { text: "4", value: 4 },
            //    { text: "5", value: 5 }
            //];
            //$("#ddlDeptLevel").jqxDropDownList({ source: source, displayMember: "text", valueMember: "value", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true });

            //绑定 上级部门名称
            var url = "Handler/DeptDDLHandler.ashx"; var source = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlParentDeptName").jqxComboBox({ source: dataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 200, height: 25, autoDropDownHeight: true });

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbDeptName", message: "部门名称不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var corpId = $("#ddlCorpId").val();
                var deptCode = $("#txbDeptCode").val();
                var deptName = $("#txbDeptName").val();
                var deptFullName = $("#txbDeptFullName").val();
                var deptShort = $("#txbDeptShort").val();
                var deptType = $("#ddlDeptType").val();
                var parentDeptName = $("#ddlParentDeptName").val();
                var deptLevel = 0;

                $.post("Handler/DeptCreateHandler.ashx", {
                    CorpId: corpId,
                    DeptCode: deptCode,
                    DeptName: deptName,
                    DeptFullName: deptFullName,
                    DeptShort: deptShort,
                    DeptType: deptType,
                    ParentLeve: parentDeptName,
                    DeptLevel: deptLevel
                },
                    function (result) {
                        alert(result);
                        //$("#ddlCorpId").val("");
                        $("#txbDeptCode").val("");
                        $("#txbDeptName").val("");
                        $("#txbDeptFullName").val("");
                        $("#txbDeptShort").val("");
                        //$("#ddlDeptType").val("");
                        //$("#ddlParentDeptName").val("");
                        //$("#ddlDeptLevel").val("");
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
            部门新增
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">所属公司：</span>
                    <div id="ddlCorpId" runat="server"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">部门编号：</span>
                    <span>
                        <input type="text" id="txbDeptCode" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">部门名称：</span>
                    <span>
                        <input type="text" id="txbDeptName" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">部门全称：</span>
                    <span>
                        <input type="text" id="txbDeptFullName" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">部门缩写：</span>
                    <span>
                        <input type="text" id="txbDeptShort" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">部门类型：</span>
                    <input type="hidden" id="hidStyleId" runat="server" />
                    <div id="ddlDeptType" runat="server" />
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">上级部门名称：</span>
                    <div id="ddlParentDeptName" runat="server"></div>
                </li>
                <%--<li>
                                <span style="width: 15%; text-align: right;">部门级别：</span>
                                <div id="ddlDeptLevel" runat="server"></div>
                            </li>--%>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="DepartmentList.aspx" id="btnCancel">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
