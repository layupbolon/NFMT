<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTypeDetail.aspx.cs" Inherits="NFMTSite.BasicData.SmsTypeDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>消息类型明细</title>
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

            //类型名称
            $("#txbTypeName").jqxInput({ height: 22, width: 300, disabled: true });
            //列表地址
            $("#txbListUrl").jqxInput({ height: 22, width: 300, disabled: true });
            //明细地址
            $("#txbViewUrl").jqxInput({ height: 22, width: 300, disabled: true });

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlStatus");
            $("#ddlStatus").jqxDropDownList("val", "<%=this.status%>");
            $("#ddlStatus").jqxDropDownList("disabled", true);
            $("#ddlStatus").jqxDropDownList("width", 300);

            $("#btnFreeze").jqxButton({ width: 96 });
            $("#btnUnFreeze").jqxButton({ width: 96 });

            $("#btnFreeze").on("click", function () {
                if (!confirm("确认冻结该条数据？")) { return; }
                var detailId = "<%=this.id%>";
                var operateId = operateEnum.冻结;
                $.post("Handler/SmsTypeStatusHandler.ashx", { id: detailId, oi: operateId },
                    function (result) {
                        alert(result);
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                if (!confirm("确认解除冻结该条数据？")) { return; }
                var detailId = "<%=this.id%>";
                var operateId = operateEnum.解除冻结;
                $.post("Handler/SmsTypeStatusHandler.ashx", { id: detailId, oi: operateId },
                    function (result) {
                        alert(result);
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
            消息类型明细
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>类型名称：</span></h4>
                    <span>
                        <input type="text" id="txbTypeName" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>列表地址：</span></h4>
                    <input type="text" id="txbListUrl" runat="server" />
                </li>
                <li>
                    <h4><span>明细地址：</span></h4>
                    <input type="text" id="txbViewUrl" runat="server" />
                </li>
                <li>
                    <h4><span>类型状态：</span></h4>
                    <div id="ddlStatus" style="float: left;"></div>
                </li>
                <li><span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnFreeze" value="冻结" runat="server" /></span>
                    <span>
                        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="margin-left: 10px" />
                    </span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
