var attachFormatedData = "";
var attachTotalrecords = 0;
var attachSource =
{
    url: attachUrl,
    datafields: [
        { name: "AttachId", type: "int" },
        { name: "AttachName", type: "string" },
        { name: "AttachInfo", type: "string" },
        { name: "AttachType", type: "int" },
        { name: "CreateTime", type: "date" },
        { name: "AttachPath", type: "string" },
        { name: "AttachExt", type: "string" },
        { name: "AttachStatus", type: "int" },
        { name: "ServerAttachName", type: "string" }
    ],
    datatype: "json",
    sort: function () {
        $("#jqxAttachGrid").jqxGrid("updatebounddata", "sort");
    },
    beforeprocessing: function (data) {
        var returnData = {};
        attachTotalrecords = data.count;
        returnData.totalrecords = data.count;
        returnData.records = data.data;
        return returnData;
    },
    type: "GET",
    sortcolumn: "ca.AttachId",
    sortdirection: "desc",
    formatdata: function (data) {
        data.pagenum = data.pagenum || 0;
        data.pagesize = data.pagesize || 10;
        data.sortdatafield = data.sortdatafield || "ca.AttachId";
        data.sortorder = data.sortorder || "desc";
        data.filterscount = data.filterscount || 0;
        attachFormatedData = buildQueryString(data);
        return attachFormatedData;
    }
};

var attachDataAdapter = new $.jqx.dataAdapter(attachSource, {
    contentType: "application/json; charset=utf-8",
    loadError: function (xhr, status, error) {
        alert(error);
    }
});

var attachViewRender = function (row, columnfield, value, defaulthtml, columnproperties) {
    var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
    var cellHtml = "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">";
    cellHtml += "<a href=\"../Files/FileDownLoad.aspx?id=" + item.AttachId + "\" title=\"" + item.AttachName + "\" >下载</a>";
    //cellHtml += "<a href=\"" + item.AttachPath + "\" title=\"" + item.AttachName + "\" target=\"_blank\" >查看</a>";
    cellHtml += "</div>";
    return cellHtml;
}

var attachRemoveRender = function (row, columnfield, value, defaulthtml, columnproperties) {
    return "<div style=\"overflow: hidden; text-overflow: ellipsis; padding-bottom: 2px; text-align: center; margin-right: 2px; margin-left: 4px;\">"
       + "<input type=\"button\" name=\"btnRemove\" class=\"jqx-rc-all jqx-rc-all-arctic jqx-button jqx-button-arctic jqx-widget jqx-widget-arctic jqx-fill-state-normal jqx-fill-state-normal-arctic\" value =\"删除\" onclick=\"bntAttachRemoveOnClick(" + row + ");\" />"
       + "</div>";
}

$("#jqxAttachGrid").jqxGrid(
{
    width: "98%",
    source: attachDataAdapter,
    pageable: true,
    autoheight: true,
    virtualmode: true,
    sorttogglestates: 1,
    sortable: true,
    enabletooltips: true,
    rendergridrows: function (args) {
        return args.data;
    },
    columns: [
      { text: "添加日期", datafield: "CreateTime", cellsformat: "yyyy-MM-dd", editable: false },
      { text: "附件名字", datafield: "AttachName" },
      { text: "查看", datafield: "AttachPath", cellsrenderer: attachViewRender },
      { text: "操作", datafield: "AttachId", cellsrenderer: attachRemoveRender, width: 100, enabletooltips: false, sortable: false }
    ]
});

function bntAttachRemoveOnClick(row) {
    if (!confirm("确认删除？")) { return; }
    var item = $("#jqxAttachGrid").jqxGrid("getrowdata", row);
    if (item.AttachId == "undefined" || item.AttachId <= 0) return;
    $.post("../Files/Handler/AttachUpdateStatusHandler.ashx", { aid: item.AttachId, s: statusEnum.已作废 },
            function (result) {
                attachSource.url = attachUrl;
                $("#jqxAttachGrid").jqxGrid("updatebounddata", "rows");
            }
        );
}

$("#jqxAttachGrid").jqxGrid("refresh");