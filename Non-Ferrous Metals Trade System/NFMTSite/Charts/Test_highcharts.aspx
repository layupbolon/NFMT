<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_highcharts.aspx.cs" Inherits="NFMTSite.Charts.Test_highcharts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery-1.7.2.min.js"></script>
    <script src="../js/highcharts_adapter.js"></script>
    <script src="../js/prschart.js"></script>
    <script src="../js/highcharts.js"></script>
    <script type="text/javascript">

        var date = [{ yuefen: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'] },
                    { data: [12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500] },
                    { data: [2000, 1300, 2000, 5822, 5201, 8005, 5001, 5410, 2546, 1542, 5800, 6500] },
                    { data: [1500, 2300, 6000, 1822, 1201, 1005, 2001, 1410, 1546, 5542, 5800, 2500] },
                    { danwei: '重量（吨）', data: [5841, 2800, 1200, 1422, 800, 2000, 1200, 1810, 1500, 5181, 2800, 5500] },
                    { name: '铜' },
                    { name: '铝' },
                    { name: '锌' },
                    { name: '其他' }];

        function genSeries(seriesName) {
            var series = {
                name: seriesName,
                data: []
            };
            //var ONE_DAY = 24 * 60 * 60 * 1000;
            var MAX_YEAR = 5;
            var date = new Date();
            date.setHours(0);
            date.setMinutes(0);
            date.setSeconds(0);
            date.setMilliseconds(0);
            return series;
        }

        function genSeries(seriesName) {
            var series = {
                name: seriesName,
                data: []
            };
            //var ONE_DAY = 24 * 60 * 60 * 1000;
            var MAX_YEAR = 5;
            var date = new Date();
            date.setHours(0);
            date.setMinutes(0);
            date.setSeconds(0);
            date.setMilliseconds(0);

            for (i = 100 ; i > 0; i--) {
                series.data.push([Math.random() * 100]);
            }
            return series;
        }


        function init() {

            var cfg = {
                chart: {
                    renderTo: 'container',
                    type: 'line'
                },
                title: {
                    text: 'test title!!!!!!!!!'
                }
            }

            var names = ['Google', 'Microsoft', 'Facebook'];
            for (var i = 0, len = names.length; i < len; i++) {
                var prsChart = new PRSChart(cfg, genSeries(names[i]).data);
                cfg.series = genSeries(names[i]);
            }
        }
        
    </script>
</head>
<body onload="init();">
    <div id="container" style="-moz-box-shadow: 6px 5px 7px gray; border-radius: 10px;"></div>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
