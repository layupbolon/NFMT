PRSChart = function(cfg, data) {
     
    this.chart = {
        renderTo : 'null',
        type : 'line'
    };
    this.series = {};
    this.legend = {};
    this.title = {
        text : ''
    };
    this.subtitle = {};
    this.tooltip = {};
    this.xAixs = {};
    this.yAixs = {};
    this.exporting = {};
    this.labels = {};
 
 
    if (cfg.chart.renderTo) {
        this.chart.renderTo = cfg.chart.renderTo;
        this.chart.type = cfg.chart.type;
    }
 
    if (cfg.series) {
        this.series.name = cfg.series.name;
        this.series.data = cfg.series.data;
    }
 
    if (cfg.title) {
        this.title.text = cfg.title.text;
    }
/*
    if (cfg.legend) {
        this.legend = cfg.legengd;
    }
    if (cfg.subtitle) {
        this.subtitle = subtitle;
    }
    if (cfg.tooltip) {
        this.tooltip = cfg.tooltip;
    }
    if (cfg.xAixs) {
        this.xAixs = cfg.xAixs;
    }
    if (cfg.yAixs) {
        this.yAixs = cfg.yAixs;
    }
    if (cfg.exporting) {
        this.exporting = cfg.exporting;
    }
    if (cfg.labels) {
        this.labels = cfg.labels;
    }
*/
    return new HighchartAdapter(this, data);
};