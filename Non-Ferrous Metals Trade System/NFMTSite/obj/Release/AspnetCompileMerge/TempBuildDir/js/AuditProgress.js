
var html = "<div id=\"jqxAuditExpander\" style=\"float: left; margin: 0px 5px 5px 5px;\">"
        + "<div>"
            + "审核进度"
        + "</div>"
        + "<div>"
            + "<div id=\"jqxAuditGrid\" style=\"float: left; margin: 5px 0 0 5px;\"></div>"
        + "</div>"
    + "</div>";

var d1 = document.getElementById("buttons");
d1.insertAdjacentHTML("beforebegin", html);
//$("#buttons").before(html);

$("#jqxAuditExpander").jqxExpander({ width: "98%" });

var auditformatedData = "";
var audittotalrecords = 0;

var auditGridsource = {
    datafields:
    [
       { name: "num", type: "int" },
       { name: "NodeName", type: "string" },
       { name: "Name", type: "string" },
       { name: "LogTime", type: "date" },
       { name: "Memo", type: "string" },
       { name: "LogResult", type: "string" },
       { name: "DetailName", type: "string" }
    ],
    datatype: "json",
    sort: function () {
        $("#jqxAuditGrid").jqxGrid("updatebounddata", "sort");
    },
    filter: function () {
        $("#jqxAuditGrid").jqxGrid("updatebounddata", "filter");
    },
    beforeprocessing: function (data) {
        var returnData = {};
        audittotalrecords = data.count;
        returnData.audittotalrecords = data.count;
        returnData.records = data.data;

        return returnData;
    },
    type: "GET",
    sortcolumn: "tol.LogId",
    sortdirection: "asc",
    formatdata: function (data) {
        data.pagenum = data.pagenum || 0;
        data.pagesize = data.pagesize || 500;
        data.sortdatafield = data.sortdatafield || "tol.LogId";
        data.sortorder = data.sortorder || "asc";
        data.filterscount = data.filterscount || 0;
        auditformatedData = buildQueryString(data);
        return auditformatedData;
    },
    url: auditUrl
};

var auditGriddataAdapter = new $.jqx.dataAdapter(auditGridsource, {
    contentType: "application/json; charset=utf-8",
    loadError: function (xhr, status, error) {
        alert(error);
    }
});

$("#jqxAuditGrid").jqxGrid(
{
    width: "98%",
    source: auditGriddataAdapter,
    columnsresize: true,
    pageable: false,
    autoheight: true,
    virtualmode: true,
    sorttogglestates: 1,
    //sortable: true,
    enabletooltips: true,
    rendergridrows: function (args) {
        return args.data;
    },
    columns: [
          { text: "审核环节", datafield: "NodeName" },
          { text: "审核类型", datafield: "DetailName" },
          { text: "审核人", datafield: "Name" },
          { text: "审核时间", datafield: "LogTime", cellsformat: "yyyy-MM-dd HH:mm:ss" },
          { text: "审核附言", datafield: "Memo" },
          { text: "审核结果", datafield: "LogResult" }
    ]
});

$("#jqxAuditGrid").jqxGrid("refresh");
