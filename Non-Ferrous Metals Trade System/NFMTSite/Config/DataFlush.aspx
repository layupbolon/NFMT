<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataFlush.aspx.cs" Inherits="NFMTSite.Config.DataFlush" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>缓存刷新</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqxExpander").jqxExpander({ width: "98%", height: 100 });
            $("#btnFlushBasicData").jqxButton();
            $("#btnFlushUser").jqxButton();
            $("#btnFlushWorkFlow").jqxButton();

            $("#btnFlushBasicData").on("click", function () {
                if (confirm("确定刷新基础数据缓存？")) {
                    $.post("Handler/BasicDataHandler.ashx", {},
                        function (result) {
                            alert(result);
                        });
                }
            });

            $("#btnFlushUser").on("click", function () {
                if (confirm("确定刷新用户数据缓存？")) {
                    $.post("Handler/UserHandler.ashx", {},
                        function (result) {
                            alert(result);
                        });
                }
            });

            $("#btnFlushWorkFlow").on("click", function () {
                if (confirm("确定刷新工作任务相关缓存？")) {
                    $.post("Handler/WorkFlowHandler.ashx", {},
                        function (result) {
                            alert(result);
                        });
                }
            });

        });
    </script>
</head>
<body>

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            缓存刷新
        </div>
        <div style="height: 200px;">
            <ul>
                <li style="float: left; margin: 0px 10px 0px 10px;">
                    <input type="button" id="btnFlushBasicData" value="刷新基础数据" />
                </li>
                <li style="float: left; margin: 0px 10px 0px 10px;">
                    <input type="button" id="btnFlushUser" value="刷新用户数据" />
                </li>
                <li style="float: left; margin: 0px 10px 0px 10px;">
                    <input type="button" id="btnFlushWorkFlow" value="刷新工作任务相关" />
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
