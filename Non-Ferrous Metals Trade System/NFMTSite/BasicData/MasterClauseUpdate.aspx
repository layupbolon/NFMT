<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterClauseUpdate.aspx.cs" Inherits="NFMTSite.BasicData.MasterClauseUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>模板合约条款分配信息修改</title>
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
            $("#txbSort").jqxInput({ height: 25 });
            $("#btnUpdate").jqxButton({ width: 65 });

            $("#jqxExpander").jqxValidator({
                rules: [
                    {
                        input: "#txbSort", message: "排序号必须为数字", action: "keyup, blur", rule: function (input, commit) { return $.isNumeric(input.val()); }
                    }
                ]
            });

            $("#btnUpdate").click(function () {

                var refId = $("#hidRefId").val();
                var isChose = document.getElementById("chkIsChose").checked; // $("#chkIsChose").attr("checked");
                var sort = $("#txbSort").val();

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                $.post("Handler/MasterClauseUpdateHandler.ashx", { id: refId, ic: isChose, s: sort },
                    function (result) {
                        alert(result);
                        //document.location.href = "MasterClauseAllot.aspx";
                        history.go(-1)
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
            模板分配合约条款修改
                        <input type="hidden" runat="server" id="hidRefId" />
        </div>
        <div class="DataExpander">
            <ul>
                <li>
                    <h4><span>模板名称：</span></h4>
                    <span runat="server" id="spnMasterName"></span></li>
                <li>
                    <h4><span>模板英文名称：</span></h4>
                    <span runat="server" id="spnMasterEname"></span>
                </li>
                <li>
                    <h4><span>条款中文内容：</span></h4>
                    <span runat="server" id="spnClauseText"></span>
                </li>
                <li>
                    <h4><span>条款英文内容：</span></h4>
                    <span runat="server" id="spnClauseEtext"></span>
                </li>

                <li>
                    <h4><span>是否默认选中：</span></h4>
                    <span>
                        <input type="checkbox" id="chkIsChose" runat="server" />
                    </span>
                </li>
                <li>
                    <h4><span>排序号：</span></h4>
                    <span>
                        <input type="text" id="txbSort" runat="server" />
                    </span>
                </li>

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
