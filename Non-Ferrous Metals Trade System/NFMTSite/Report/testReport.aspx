﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testReport.aspx.cs" Inherits="NFMTSite.Report.testReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title id='Description'>This example shows how to display nested Grid plugins.</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxcore.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxdata.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxbuttons.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxmenu.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxgrid.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqxgrid.sort.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            function getDemoTheme() {
                var theme = document.body ? $.data(document.body, 'theme') : null
                if (theme == null) {
                    theme = '';
                }
                else {
                    return theme;
                }
                var themestart = window.location.toString().indexOf('?');
                if (themestart == -1) {
                    return '';
                }

                var theme = window.location.toString().substring(1 + themestart);
                if (theme.indexOf('(') >= 0) {
                    theme = theme.substring(1);
                }
                if (theme.indexOf(')') >= 0) {
                    theme = theme.substring(0, theme.indexOf(')'));
                }

                var url = "../../jqwidgets/styles/jqx." + theme + '.css';

                if (document.createStyleSheet != undefined) {
                    var hasStyle = false;
                    $.each(document.styleSheets, function (index, value) {
                        if (value.href != undefined && value.href.indexOf(theme) != -1) {
                            hasStyle = true;
                            return false;
                        }
                    });
                    if (!hasStyle) {
                        document.createStyleSheet(url);
                    }
                }
                else {
                    var hasStyle = false;
                    if (document.styleSheets) {
                        $.each(document.styleSheets, function (index, value) {
                            if (value.href != undefined && value.href.indexOf(theme) != -1) {
                                hasStyle = true;
                                return false;
                            }
                        });
                    }
                    if (!hasStyle) {
                        var link = $('<link rel="stylesheet" href="' + url + '" media="screen" />');
                        link[0].onload = function () {
                            if ($.jqx && $.jqx.ready) {
                                $.jqx.ready();
                            };
                        }
                        $(document).find('head').append(link);
                    }
                }
                $.jqx = $.jqx || {};
                $.jqx.theme = theme;
                return theme;
            };
            var theme = '';
            try {
                if (jQuery) {
                    theme = getDemoTheme();
                    if (window.location.toString().indexOf('file://') >= 0) {
                        var loc = window.location.toString();
                        var addMessage = false;
                        if (loc.indexOf('grid') >= 0 || loc.indexOf('chart') >= 0 || loc.indexOf('tree') >= 0 || loc.indexOf('list') >= 0 || loc.indexOf('combobox') >= 0 || loc.indexOf('php') >= 0 || loc.indexOf('adapter') >= 0 || loc.indexOf('datatable') >= 0 || loc.indexOf('ajax') >= 0) {
                            addMessage = true;
                        }

                        if (addMessage) {
                            $(document).ready(function () {
                                setTimeout(function () {
                                    $(document.body).prepend($('<div style="font-size: 12px; font-family: Verdana;">Note: To run a sample that includes data binding, you must open it via "http://..." protocol since Ajax makes http requests.</div><br/>'));
                                }
                                , 50);
                            });
                        }
                    }
                }
                else {
                    $(document).ready(function () {
                        theme = getDemoTheme();
                    });
                }
            }
            catch (error) {
                var er = error;
            }















            var url = "XMLFile1.xml";
            var source =
            {
                datafields: [
                    { name: 'FirstName' },
                    { name: 'LastName' },
                    { name: 'Title' },
                    { name: 'Address' },
                    { name: 'City' }
                ],
                root: "Employees",
                record: "Employee",
                id: 'EmployeeID',
                datatype: "xml",
                async: false,
                url: url
            };
            var employeesAdapter = new $.jqx.dataAdapter(source);
            var orderdetailsurl = "XMLFile2.xml";
            var ordersSource =
            {
                datafields: [
                    { name: 'EmployeeID', type: 'string' },
                    { name: 'ShipName', type: 'string' },
                    { name: 'ShipAddress', type: 'string' },
                    { name: 'ShipCity', type: 'string' },
                    { name: 'ShipCountry', type: 'string' },
                    { name: 'ShippedDate', type: 'date' }
                ],
                root: "Orders",
                record: "Order",
                datatype: "xml",
                url: orderdetailsurl,
                async: false
            };
            var ordersDataAdapter = new $.jqx.dataAdapter(ordersSource, { autoBind: true });
            orders = ordersDataAdapter.records;
            var nestedGrids = new Array();
            // create nested grid.
            var initrowdetails = function (index, parentElement, gridElement, record) {
                var id = record.uid.toString();
                var grid = $($(parentElement).children()[0]);
                nestedGrids[index] = grid;
                var filtergroup = new $.jqx.filter();
                var filter_or_operator = 1;
                var filtervalue = id;
                var filtercondition = 'equal';
                var filter = filtergroup.createfilter('stringfilter', filtervalue, filtercondition);
                // fill the orders depending on the id.
                var ordersbyid = [];
                for (var m = 0; m < orders.length; m++) {
                    var result = filter.evaluate(orders[m]["EmployeeID"]);
                    if (result)
                        ordersbyid.push(orders[m]);
                }
                var orderssource = {
                    datafields: [
                        { name: 'EmployeeID', type: 'string' },
                        { name: 'ShipName', type: 'string' },
                        { name: 'ShipAddress', type: 'string' },
                        { name: 'ShipCity', type: 'string' },
                        { name: 'ShipCountry', type: 'string' },
                        { name: 'ShippedDate', type: 'date' }
                    ],
                    id: 'OrderID',
                    localdata: ordersbyid
                }
                var nestedGridAdapter = new $.jqx.dataAdapter(orderssource);
                if (grid != null) {
                    grid.jqxGrid({
                        source: nestedGridAdapter, width: 780, height: 200,
                        columns: [
                          { text: 'Ship Name', datafield: 'ShipName', width: 200 },
                          { text: 'Ship Address', datafield: 'ShipAddress', width: 200 },
                          { text: 'Ship City', datafield: 'ShipCity', width: 150 },
                          { text: 'Ship Country', datafield: 'ShipCountry', width: 150 },
                          { text: 'Shipped Date', datafield: 'ShippedDate', width: 200 }
                        ]
                    });
                }
            }
            var photorenderer = function (row, column, value) {
                var name = $('#jqxgrid').jqxGrid('getrowdata', row).FirstName;
                var imgurl = '../../images/' + name.toLowerCase() + '.png';
                var img = '<div style="background: white;"><img style="margin:2px; margin-left: 10px;" width="32" height="32" src="' + imgurl + '"></div>';
                return img;
            }
            var renderer = function (row, column, value) {
                return '<span style="margin-left: 4px; margin-top: 9px; float: left;">' + value + '</span>';
            }
            // creage jqxgrid
            $("#jqxgrid").jqxGrid(
            {
                width: 850,
                height: 365,
                source: source,
                rowdetails: true,
                rowsheight: 35,
                initrowdetails: initrowdetails,
                rowdetailstemplate: { rowdetails: "<div id='grid' style='margin: 10px;'></div>", rowdetailsheight: 220, rowdetailshidden: true },
                ready: function () {
                    $("#jqxgrid").jqxGrid('showrowdetails', 1);
                },
                columns: [
                      { text: 'Photo', width: 50, cellsrenderer: photorenderer },
                      { text: 'First Name', datafield: 'FirstName', width: 100, cellsrenderer: renderer },
                      { text: 'Last Name', datafield: 'LastName', width: 100, cellsrenderer: renderer },
                      { text: 'Title', datafield: 'Title', width: 180, cellsrenderer: renderer },
                      { text: 'Address', datafield: 'Address', width: 300, cellsrenderer: renderer },
                      { text: 'City', datafield: 'City', width: 170, cellsrenderer: renderer }
                ]
            });
        });
    </script>
</head>
<body class='default'>
    <div id="jqxgrid">
    </div>
</body>
</html>
