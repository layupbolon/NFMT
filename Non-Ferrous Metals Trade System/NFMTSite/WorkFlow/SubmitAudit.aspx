<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitAudit.aspx.cs" Inherits="NFMTSite.WorkFlow.SubmitAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>提交审核</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#jqxExpander").jqxExpander({ width: "600", showArrow: false });

            $("#txbTaskName").jqxInput({ height: 25, width: 350 });
            $("#txbTaskConnext").jqxInput({ height: 100, width: 350 });
            $("#txbTaskConnext").jqxInput("focus");

            $("#btnAudit").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxButton({ height: 25, width: 100 });

            $("#btnAudit").on("click", function () {
                $("#btnAudit").jqxButton({ disabled: true });
                var paras;
                var isChrome = window.navigator.userAgent.indexOf("Chrome") !== -1
                if (isChrome) {
                    paras = {
                        mid: $.getUrlParam("mid"),
                        model: $.getUrlParam("model")
                    }
                } else {
                    paras = window.dialogArguments;
                }
                
                $.post("Handler/SubmitAuditHandler.ashx",
                    {
                        mid: paras.mid,                     //工作流模版id
                        model: paras.model,                 //IModel实体json
                        tname: $("#txbTaskName").val(),     //任务名称
                        tConnext: $("#txbTaskConnext").val()//任务内容
                    },
                    function (result) {
                        alert(result);
                        if (result === "提交审核成功")
                        {
                            window.opener = null;
                            window.open("", "_self");
                            window.close();
                        }
                        $("#btnAudit").jqxButton({ disabled: false });
                    }
                );
            });

            $("#btnCancel").on("click", function () {
                close();
            });
            
        });

    </script>
</head>
<body>
    <div id="jqxExpander" style="float: left; margin: 10px 5px 5px 5px;">
        <div>
            提交审核
            <%--<input type="hidden" id="hidMid" runat="server"/>
            <input type="hidden" id="hidModel" runat="server" />--%>
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="text-align: right;">工作任务名称：</span>
                    <span>
                        <input type="text" id="txbTaskName" value="<%=this.curTitle%>" /></span>
                </li>
                <li>
                    <span style="text-align: right;">工作任务内容：</span>
                    <span>
                        <input type="text" id="txbTaskConnext" value="<%=this.curContent%>" /></span>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAudit" value="提交审核" style="margin-left: 23px" /></span>
                    <span>
                        <input type="button" id="btnCancel" value="取消" style="margin-left: 10px" /></span>
                </li>
            </ul>
        </div>
    </div>
 </body>
</html>
