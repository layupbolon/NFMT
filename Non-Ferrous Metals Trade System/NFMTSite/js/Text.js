
//var name; var danwei; var date_1; var yuefen;
//for (var i = 0; i < obj.length; i++) {
//    obj[i].name = name;
//    obj[i].yuefen = yuefen;
//    obj[i].data = date_1;
//    obj[i].danwei = danwei;
//}
//alert(yuefen);

//var series = [{ yuefen: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'] },

//                  { name: '铜', data: [12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500] },
//                  { name: '铝', data: [2000, 1300, 2000, 5822, 5201, 8005, 5001, 5410, 2546, 1542, 5800, 6500] },
//                  { name: '锌', data: [1500, 2300, 6000, 1822, 1201, 1005, 2001, 1410, 1546, 5542, 5800, 2500] },

//                  { name: '其他', danwei: '重量（吨）', data: [5841, 2800, 1200, 1422, 800, 2000, 1200, 1810, 1500, 5181, 2800, 5500] }];

var date = [{ yuefen: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'] },
            { data: [12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500] },
            { data: [2000, 1300, 2000, 5822, 5201, 8005, 5001, 5410, 2546, 1542, 5800, 6500] },
            { data: [1500, 2300, 6000, 1822, 1201, 1005, 2001, 1410, 1546, 5542, 5800, 2500] },
            { danwei: '重量（吨）', data: [5841, 2800, 1200, 1422, 800, 2000, 1200, 1810, 1500, 5181, 2800, 5500] },
            { name: '铜' },
            { name: '铝' },
            { name: '锌' },
            { name: '其他' }];

var obj = eval(date);

$(function () {
    $('#container').highcharts({
        credits: { enabled: false },
        title: {
            text: '2013年迈科入库表',
            x: -20 //center
        },
        //subtitle: {
        //    text: 'Source: WorldClimate.com',
        //    x: -20
        //},
        xAxis: {
            categories: obj[0].yuefen
        },
        yAxis: {
            title: {
                text: obj[4].danwei
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            valueSuffix: '吨'
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: obj
    }
);
});