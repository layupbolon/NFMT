<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="NFMTSite.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>错误页面</title>
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        window.onload = function () {
            var time = 3; //设置时间为几秒   
            var timer = document.getElementById("spTime");
            timer.innerHTML = time; //初始化显示秒数   
            time = time - 1;
            var g = window.setInterval(function () {
                if (time < 0) {
                    window.clearTimeout(g); //清除动画   
                    window.location.href = "<%=this.hrefStr%>"; //跳转到指定地址   
                    //window.history.back(-1); //后退   
                } else {
                    showTime();
                }
            }, 1000);

            function showTime() {
                timer.innerHTML = time;
                time--;
            }
        };
    </script>
</head>
<body>
    <center>
    <h1><span id="spTitle" runat="server"></span></h1>
    <p><span id="spTime" runat="server"></span> 秒后自动返回   </p>
    <p><a target="_self" runat="server" href="#" id="redirect" style="margin-left: 10px">点击直接跳转</a></p>
    </center>
</body>
</html>