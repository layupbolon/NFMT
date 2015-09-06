<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BDStyleDtlUpdate.aspx.cs" Inherits="NFMTSite.BasicData.BDStyleDtlUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>类型明细修改</title>
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
            $("#txbDetailCode").jqxInput({ height: 25, width: 120 });
            $("#txbDetailName").jqxInput({ height: 25, width: 120 });
            $("#btnUpdate").jqxButton({ width: 65 });

            var selValue = $("#hidDetailStatus").val();
            CreateSelectStatusDropDownList("selStatus", selValue);

            // initialize validator.
            $("#jqxExpander").jqxValidator({
                rules: [
                    { input: "#txbDetailCode", message: "明细编号不能为空", action: "keyup, blur", rule: "required" },
                    { input: "#txbDetailName", message: "明细名称不能为空", action: "keyup, blur", rule: "required" }
                ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var detailId = $("#hidDetailId").val();
                var code = $("#txbDetailCode").val();
                var name = $("#txbDetailName").val();
                var status = $("#selStatus").val();

                $.post("Handler/BDStyleDtlUpdateHandler.ashx", { detailId: detailId, code: code, name: name, status: status },
                    function (result) {
                        alert(result);
                        document.location.href = "BDStyleDtlList.aspx";
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
            类型明细修改<input type="hidden" runat="server" id="hidDetailId" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>归属类型：</span></h4>
                    <span runat="server" id="spnStyleName"></span></li>
                <li>
                    <h4><span>归属类型状态：</span></h4>
                    <span runat="server" id="spnStyleStatus"></span></li>
                <li>
                    <h4><span>明细编号：</span></h4>
                    <input type="text" id="txbDetailCode" runat="server" /></li>
                <li>
                    <h4><span>明细名称：</span></h4>
                    <input type="text" id="txbDetailName" runat="server" /></li>
                <li>
                    <h4><span>明细状态：</span></h4>
                    <div id="selStatus" style="float: left;"></div>
                    <input type="hidden" runat="server" id="hidDetailStatus" /></li>
                <li>
                    <h4><span>&nbsp;</span></h4>
                    <span>
                        <input type="button" id="btnUpdate" value="更新" /></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
