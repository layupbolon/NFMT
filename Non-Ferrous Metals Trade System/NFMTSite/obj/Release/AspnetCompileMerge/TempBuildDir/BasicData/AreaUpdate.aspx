<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaUpdate.aspx.cs" Inherits="NFMTSite.BasicData.AreaUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>地区修改</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%" });
            

            var selValue = $("#hidAreaStatus").val();
            CreateSelectStatusDropDownList("selStatus", selValue);
            $("#selStatus").jqxDropDownList("width", 200);

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
            if ($("#hidparentID").val() > 0) $("#ddlParentId").jqxComboBox("val", $("#hidparentID").val());

            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbAreaName", message: "地区名称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaFullName", message: "地区全称不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaShort", message: "地区缩写不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaCode", message: "电话区号不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbAreaZip", message: "邮政编号不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnUpdate").jqxButton({ height: 25, width: 96 });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var areaName = $("#txbAreaName").val();
                var areaFullName = $("#txbAreaFullName").val();
                var areaShort = $("#txbAreaShort").val();
                var areaCode = $("#txbAreaCode").val();
                var areaZip = $("#txbAreaZip").val();
                var areaStatus = $("#selStatus").val();
                var parentId = $("#ddlParentId").val();
                var id = $("#hid").val();

                $.post("Handler/AreaUpdateHandler.ashx", {
                    areaName: areaName,
                    areaFullName: areaFullName,
                    areaShort: areaShort,
                    areaCode: areaCode,
                    areaZip: areaZip,
                    areaStatus: areaStatus,
                    id: id,
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
            地区修改
                        <input type="hidden" runat="server" id="hidparentID" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <input type="hidden" runat="server" id="hid" />
                    <h4><span>地区名称：</span></h4>
                    <span>
                        <input type="text" id="txbAreaName" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>地区全名：</span></h4>
                    <span>
                        <input type="text" id="txbAreaFullName" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>地区名缩写:</span></h4>
                    <span>
                        <input type="text" id="txbAreaShort" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>电话区号:</span></h4>
                    <span>
                        <input type="text" id="txbAreaCode" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>邮政编号:</span></h4>
                    <span>
                        <input type="text" id="txbAreaZip" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>所属区域：</span></h4>
                    <div id="ddlParentId"></div>
                </li>
                <li>
                    <h4><span>地区状态：</span></h4>
                    <div id="selStatus" />
                    <input type="hidden" id="hidAreaStatus" runat="server" />
                </li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" />
                    </span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
