<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="highcharts_JX.aspx.cs" Inherits="NFMTSite.Charts.highcharts_JX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-1.7.2.min.js"></script>
    <script src="../js/highcharts_adapter.js"></script>
    <script src="../js/Text.js"></script>


    <script type="text/javascript">
        var series = [{ yuefen: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'] },
                     { name: '铜', data: [12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500] },
                     { name: '铝', data: [2000, 1300, 2000, 5822, 5201, 8005, 5001, 5410, 2546, 1542, 5800, 6500] },
                     { name: '锌', data: [1500, 2300, 6000, 1822, 1201, 1005, 2001, 1410, 1546, 5542, 5800, 2500] },
                     { name: '其他', danwei: '重量（吨）', data: [5841, 2800, 1200, 1422, 800, 2000, 1200, 1810, 1500, 5181, 2800, 5500] }];
        function init() {
            var obj = eval(series);
            for (var i = 0; i < obj.length; i++) {
                alert(obj[i].name);
                alert(obj[i].data);
                alert(obj[i].yuefen)
                alert(obj[i].danwei)
            }
        }
    </script>
</head>
<body onload="init()">
    <form id="form1" runat="server">
    <div>
          <div id="container" style="-moz-box-shadow: 6px 5px 7px gray; border-radius: 10px;"></div>
    </div>
    </form>
<script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
