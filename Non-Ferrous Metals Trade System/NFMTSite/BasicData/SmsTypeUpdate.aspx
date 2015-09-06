<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTypeUpdate.aspx.cs" Inherits="NFMTSite.BasicData.SmsTypeUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>消息类型修改</title>
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
            $("#txbTypeName").jqxInput({ height: 22, width: 300 });
            //列表地址
            $("#txbListUrl").jqxInput({ height: 22, width: 300 });
            //明细地址
            $("#txbViewUrl").jqxInput({ height: 22, width: 300 });

            //状态下拉列表绑定
            CreateStatusDropDownList("ddlStatus");
            $("#ddlStatus").jqxDropDownList("val", "<%=this.status%>");
            $("#ddlStatus").jqxDropDownList("width", 300);

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#txbTypeName", message: "类型名称不可为空", action: "keyup,blur", rule: function (input, commit) {
                                return $('#txbTypeName').jqxInput("val") != "";
                            }
                        },
                        {
                            input: "#txbListUrl", message: "列表地址不可为空", action: "keyup,blur", rule: function (input, commit) {
                                return $('#txbListUrl').jqxInput("val") != "";
                            }
                        },
                        {
                            input: "#txbViewUrl", message: "明细地址不可为空", action: "keyup,blur", rule: function (input, commit) {
                                return $('#txbViewUrl').jqxInput("val") != "";
                            }
                        }
                    ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var smsType = {
                    SmsTypeId: "<%=this.id%>",
                    TypeName: $("#txbTypeName").val(),
                    ListUrl: $("#txbListUrl").val(),
                    ViewUrl: $("#txbViewUrl").val(),
                    SmsTypeStatus: $("#ddlStatus").val()
                }

                $.post("Handler/SmsTypeUpdateHandler.ashx", { smsType: JSON.stringify(smsType) },
                    function (result) {
                        alert(result);
                        document.location.href = "SmsTypeList.aspx";
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
            消息类型修改
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

