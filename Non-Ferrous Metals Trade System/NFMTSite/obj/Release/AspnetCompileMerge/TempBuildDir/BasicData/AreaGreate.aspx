<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaGreate.aspx.cs" Inherits="NFMTSite.BasicData.AreaGreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>区域新增</title>
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

            $("#txbAreaName").jqxInput({ height: 25, width: 200 });
            $("#txbAreaFullName").jqxInput({ height: 25, width: 200 });
            $("#txbAreaShort").jqxInput({ height: 25, width: 200 });
            $("#txbAreaCode").jqxInput({ height: 25, width: 200 });
            $("#txbAreaZip").jqxInput({ height: 25, width: 200 });

            //绑定所属区域
            var url = "Handler/AreaDDLHandler.ashx";
            var source = { datatype: "json", datafields: [{ name: "AreaId" }, { name: "AreaName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#ddlParentId").jqxComboBox({ source: dataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 200, height: 25 });

            // initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbAreaName", message: "地区名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaFullName", message: "地区全称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaShort", message: "地区缩写不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaCode", message: "电话区号不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaZip", message: "邮政编号不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnAdd").jqxButton({ height: 25, width: 96 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 96 });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var areaName = $("#txbAreaName").val();
                var areaFullName = $("#txbAreaFullName").val();
                var areaShort = $("#txbAreaShort").val();
                var areaCode = $("#txbAreaCode").val();
                var areaZip = $("#txbAreaZip").val();
                var parentId = $("#ddlParentId").val();

                $.post("Handler/AreaGreateHandler.ashx", {
                    areaName: areaName,
                    areaFullName: areaFullName,
                    areaShort: areaShort,
                    areaCode: areaCode,
                    areaZip: areaZip,
                    parentId: parentId
                },
                    function (result) {
                        alert(result);
                        document.location.href = "AreaList.aspx";
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
            地区添加
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>地区名称：</span></h4>
                    <span>
                        <input type="text" id="txbAreaName" /></span>
                </li>
                <li>
                    <h4><span>地区全名：</span></h4>
                    <span>
                        <input type="text" id="txbAreaFullName" /></span>
                </li>
                <li>
                    <h4><span>地区名缩写：</span></h4>
                    <span>
                        <input type="text" id="txbAreaShort" /></span>
                </li>
                <li>
                    <h4><span>电话区号：</span></h4>
                    <span>
                        <input type="text" id="txbAreaCode" /></span>
                </li>
                <li>
                    <h4><span>邮政编号：</span></h4>
                    <span>
                        <input type="text" id="txbAreaZip" /></span>
                </li>
                <li>
                    <h4><span>所属区域：</span></h4>
                    <div id="ddlParentId"></div>
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" /></span>
                    <span><a target="_self" runat="server" href="AreaList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
