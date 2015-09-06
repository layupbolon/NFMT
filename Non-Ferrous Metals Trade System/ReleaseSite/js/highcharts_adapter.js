var HighchartAdapter = function(cfg, data) {
 
    var highchartCfg = {};
 
    var highchartData = {};
 
    if (cfg.chart) {
        highchartCfg.chartObj = cfg.chart;
    }
    if (cfg.series) {
        highchartCfg.seriesObj = cfg.series;
    }
/*  
    if (cfg.legend) {
        highchartCfg.legengdObj = cfg.legengd;
    }
*/
    if (cfg.title) {
        highchartCfg.titleObj = cfg.title;
    }
/*
    if (cfg.subtitle) {
        highchartCfg.subtitleObj = subtitle;
    }
    if (cfg.tooltip) {
        highchartCfg.tooltipObj = cfg.tooltip;
    }
    if (cfg.xAixs) {
        highchartCfg.xAixsObj = cfg.xAixs;
    }
    if (cfg.yAixs) {
        highchartCfg.yAixsObj = cfg.yAixs;
    }
    if (cfg.exporting) {
        highchartCfg.exportingObj = cfg.exporting;
    }
    if (cfg.labels) {
        highchartCfg.labelsObj = cfg.labels;
    }
*/  
    // 转换数据
    return new Highcharts.Chart({
        chart : {
            renderTo : highchartCfg.chartObj.renderTo,
            type : highchartCfg.chartObj.type
        },
        series : [{
            data : data
        }],
        title : {
            text : highchartCfg.titleObj.text
        }

    });
};