<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineCharts.aspx.cs" Inherits="NFMTSite.Charts.LineCharts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../js/jquery.min.js"></script>

    <script src="../js/jquery-1.7.2.min.js"></script>
   <%-- <script src="../js/Text.js"></script>--%>
    <script src="../js/highcharts.js"></script>
    <script>

        var Text;
        var Categories;
        var Title;
        var ValueSuffix;
        var Series;

        function ac() {
            $.ajax({
                type: "post",
                url: "../Charts/Handler/HighCharts.ashx",
                data: {},
                // 不需要指定contentType，因为jquery会自动添加contentType=“application/x-www-form-urlencode”。
                dataType: "json",
                error: function (XHR, textStatus, errorThrown) {
                    alert(XHR + "--" + textStatus + "--" + errorThrown);
                },
                success: function (chart) {
                    $('#container').highcharts({
                        credits: { enabled: false },
                        title: {
                            text: chart["Title"],
                            x: -20 //center
                        },
                        subtitle: {
                            text: chart["SubTitle"],
                            x: -20
                        },
                        xAxis: {
                            categories: chart["XAxis"].Categories
                        },
                        yAxis: {
                            title: {
                                text: chart["YAxis"].Title
                            },
                            plotLines: [{
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }]
                        },
                        tooltip: {
                            valueSuffix: chart["Tooltip"]
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            verticalAlign: 'middle',
                            borderWidth: 0
                        },
                        series:                           
                            chart["Series"]                    
                    }

                    );
                }
            })
        };
    </script>


</head>
<body onload="ac()">
    <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
