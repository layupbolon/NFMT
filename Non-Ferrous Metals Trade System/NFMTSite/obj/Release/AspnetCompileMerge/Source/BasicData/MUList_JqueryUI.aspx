<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MUList_JqueryUI.aspx.cs" Inherits="NFMTSite.BasicData.MUList_JqueryUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>计量单位管理</title>
    <link rel="Stylesheet" type="text/css" href="../jquery-ui-1.9.2.custom/css/redmond/jquery-ui-1.9.2.custom.min.css" />
    <link rel="Stylesheet" type="text/css" href="../jquery-ui-1.9.2.custom/css/ui.jqgrid.css" />
    <script type="text/javascript" src="../jquery-ui-1.9.2.custom/js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js"></script>
    <script type="text/javascript" src="../jquery-ui-1.9.2.custom/js/grid.locale-cn.js"></script>
    <script type="text/javascript" src="../jquery-ui-1.9.2.custom/js/jquery.jqGrid.min.js"></script>
    <script type="text/javascript" src="../jquery-ui-1.9.2.custom/development-bundle/ui/jquery.ui.autocomplete.js"></script>

    <style>
        .ui-combobox
        {
            position: relative;
            display: inline-block;
        }

        .ui-combobox-toggle
        {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
            /* adjust styles for IE 6/7 */
            *height: 1.7em;
            *top: 0.1em;
        }

        .ui-combobox-input
        {
            margin: 0;
            padding: 0.1em;
            width: 80px;
        }
    </style>

</head>
<body>
    <div style="padding: 5px 5px 5px 5px;">
        状态:<select id="selStatus" style="height: 25px;">
            <option value="-1">全部</option>
            <option value="0">已作废</option>
            <option value="1">已录入</option>
            <option value="2">待审核</option>
            <option value="4">已生效</option>
            <option value="8">已完成</option>
            <option value="16">已冻结</option>
        </select>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        关键词:<input id="iptKey" type="text" style="width: 200px;" />
        <input type="button" onclick="javascript: btnSearch();" id="btnSearch" value="查询" />

    </div>
    <table id="gridTable">
    </table>
    <div id="gridPager">
    </div>
 </body>
<script type="text/javascript">

    var gridSel = "#gridTable";

    $(function () {

        function searchGridData() {
            var status = $("#selStatus").val();
            var key = $('#iptKey').val();
            var urlobj = "Handler/MUListHandler.ashx?s=" + status + "&k=" + key;

            $(gridSel).jqGrid({
                url: urlobj,
                mtype: "GET",
                datatype: "json",
                altRows: true,
                autoScroll: false,
                height: 250,
                colNames: ['序号', '计量单位名称', '基本计量单位名称', '转换规模', '计量单位状态', '编辑'],
                colModel: [
                    { name: 'MUId', index: 'mu.MUId', width: 30, hidden: true },
                    { name: 'MUName', index: 'mu.MUName', width: 100, align: "center" },
                    { name: 'BaseName', index: 'bmu.MUName', width: 120, align: "left" },
                    { name: 'TransformRate', index: 'mu.TransformRate', width: 130, align: "left" },
                    { name: 'MUStatusName', index: 'mu.MUStatusName', width: 130, align: "center" },
                    { name: 'Edit', width: 120, align: "center", sortable: false }
                ],
                jsonReader: {
                    page: "page",
                    total: "total",
                    repeatitems: false
                },
                pager: jQuery('#gridPager'),
                rowNum: 10,
                rowList: [10, 20, 30],
                sortname: 'mu.MUId',
                sortorder: 'desc',
                multiselect: false,
                viewrecords: true,
                caption: '计量单位列表',
                gridComplete: function () {
                    var ids = $(gridSel).jqGrid('getDataIDs');
                    for (var i = 0; i < ids.length; i++) {
                        var id = ids[i];
                        var modifylink = "<a target='_blank' id='update" + id + "' href='MUUpdate.aspx?id=" + id + "' style='color:#f60' >编辑</a>";
                        $(gridSel).jqGrid('setRowData', ids[i], { Edit: modifylink });
                    }
                }
            }).navGrid('#gridPager', { edit: false, add: false, del: false, search: false });

            jQuery(gridSel).jqGrid('setGridParam', { url: urlobj });
            jQuery(gridSel).trigger("reloadGrid");
        }

        searchGridData();

        $("#selStatus").combobox();

        //$("#toggle").click(function () {
        //    $("#selStatus").toggle();
        //});




        function ReferehData() {
            //$(gridSel).jqGrid('setGridParam', { page: 1 });
            //$(gridSel).jqGrid('setCaption', "");
            searchGridData();
        }


        $("#btnSearch").click(function () {
            ReferehData();
        });

    });

    //function Print(id) {
    //    var model = $(gridSel).jqGrid('getRowData', id);
    //    var surl = 'EditReceiveConfirm.aspx?ID=' + model.ID;
    //    $("#modifyform").dialog({
    //        height: 650,
    //        width: 800,
    //        resizable: true,
    //        modal: true,
    //        open: function () {
    //            $("#modifyform").html('<iframe src="' + surl + '" frameborder="0" height="98%" width="98%" id="dialogFrame" scrolling="no"></iframe>');
    //        },
    //        resizeStop: function () {
    //            $("#dialogFrame").css("width", parseInt($(this).css("width")) - 5);
    //            $("#dialogFrame").css("height", parseInt($(this).css("height")) - 5);
    //        }
    //    });

    //}
    //var bIsShowAdvFilter = false;
    //function ShowAdvFilter() {
    //    var ctlDiv = document.getElementById('divAdv');
    //    var ctlHref = document.getElementById('AdvDiv');
    //    if (!bIsShowAdvFilter) {
    //        ctlDiv.style.display = "inline";
    //        bIsShowAdvFilter = true;
    //        ctlHref.innerText = "隐藏高级查询";
    //    }
    //    else {
    //        ctlDiv.style.display = "none";
    //        bIsShowAdvFilter = false;
    //        ctlHref.innerText = "显示高级查询";
    //    }
    //}



</script>
<script type="text/javascript">
    (function ($) {
        $.widget("ui.combobox", {
            _create: function () {
                var input,
                    that = this,
                    select = this.element.hide(),
                    selected = select.children(":selected"),
                    value = selected.val() ? selected.text() : "",
                    wrapper = this.wrapper = $("<span>")
                        .addClass("ui-combobox")
                        .insertAfter(select);

                function removeIfInvalid(element) {
                    var value = $(element).val(),
                        matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(value) + "$", "i"),
                        valid = false;
                    select.children("option").each(function () {
                        if ($(this).text().match(matcher)) {
                            this.selected = valid = true;
                            return false;
                        }
                    });
                    if (!valid) {
                        // remove invalid value, as it didn't match anything
                        $(element)
                            .val("")
                            .attr("title", value + " didn't match any item")
                            .tooltip("open");
                        select.val("");
                        setTimeout(function () {
                            input.tooltip("close").attr("title", "");
                        }, 2500);
                        input.data("autocomplete").term = "";
                        return false;
                    }
                }

                input = $("<input>")
                    .appendTo(wrapper)
                    .val(value)
                    .attr("title", "")
                    .addClass("ui-state-default ui-combobox-input")
                    .autocomplete({
                        delay: 0,
                        minLength: 0,
                        source: function (request, response) {
                            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                            response(select.children("option").map(function () {
                                var text = $(this).text();
                                if (this.value && (!request.term || matcher.test(text)))
                                    return {
                                        label: text.replace(
                                            new RegExp(
                                                "(?![^&;]+;)(?!<[^<>]*)(" +
                                                $.ui.autocomplete.escapeRegex(request.term) +
                                                ")(?![^<>]*>)(?![^&;]+;)", "gi"
                                            ), "<strong>$1</strong>"),
                                        value: text,
                                        option: this
                                    };
                            }));
                        },
                        select: function (event, ui) {
                            ui.item.option.selected = true;
                            that._trigger("selected", event, {
                                item: ui.item.option
                            });
                        },
                        change: function (event, ui) {
                            if (!ui.item)
                                return removeIfInvalid(this);
                        }
                    })
                    .addClass("ui-widget ui-widget-content ui-corner-left");

                input.data("autocomplete")._renderItem = function (ul, item) {
                    return $("<li>")
                        .data("item.autocomplete", item)
                        .append("<a>" + item.label + "</a>")
                        .appendTo(ul);
                };

                $("<a>")
                    .attr("tabIndex", -1)
                    .attr("title", "Show All Items")
                    .tooltip()
                    .appendTo(wrapper)
                    .button({
                        icons: {
                            primary: "ui-icon-triangle-1-s"
                        },
                        text: false
                    })
                    .removeClass("ui-corner-all")
                    .addClass("ui-corner-right ui-combobox-toggle")
                    .click(function () {
                        // close if already visible
                        if (input.autocomplete("widget").is(":visible")) {
                            input.autocomplete("close");
                            removeIfInvalid(input);
                            return;
                        }

                        // work around a bug (likely same cause as #5265)
                        $(this).blur();

                        // pass empty string as value to search for, displaying all results
                        input.autocomplete("search", "");
                        input.focus();
                    });

                input
                    .tooltip({
                        position: {
                            of: this.button
                        },
                        tooltipClass: "ui-state-highlight"
                    });
            },

            destroy: function () {
                this.wrapper.remove();
                this.element.show();
                $.Widget.prototype.destroy.call(this);
            }
        });
    })(jQuery);

    $('#btnSearch').button();
	</script>
</html>
