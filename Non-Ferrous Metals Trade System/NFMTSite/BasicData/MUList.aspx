<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MUList.aspx.cs" Inherits="NFMTSite.BasicData.MUList" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>计量单位管理</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../themes/default/combobox.css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.easyui.min.js"></script>
</head>
<body>
    <div class="easyui-panel" title="查询条件" style="padding: 5px;">
        状态:
                <select id="selStatus">
                    <%-- class="easyui-combobox">--%>
                    <option value="-1">全部</option>
                    <option value="0">已作废</option>
                    <option value="1">已录入</option>
                    <option value="2">待审核</option>
                    <option value="4">已生效</option>
                    <option value="8">已完成</option>
                    <option value="16">已冻结</option>
                </select>
        关键词:<input id="iptKey" type="text" style="width: 200px;" />

        <%--  <a href="#" onclick="btnSearch();" id="btnSearch" class="easyui-linkbutton" data-options="iconCls:'icon-search'">Search</a>--%>
        <input type="button" onclick="javascript: btnSearch();" id="btnSearch" class="easyui-linkbutton" value="查询" />
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-print'">Print</a>
        <a href="void script(0);" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">Reload</a>

    </div>
    <div>
        <table id="MUList"></table>
    </div>
 </body>

<script type="text/javascript">

    function loadList() {

        var status = $("#selStatus").val();
        var key = $('#iptKey').val();
        //var url = 'Handler/MUListHandler.ashx';

        $('#MUList').datagrid({
            url: 'Handler/MUListHandler.ashx',
            fitColumns: true,
            striped: true,
            singleSelect: true,
            method: 'Get',
            title: "计量单位列表",
            collapsible: true,
            autoRowHeight: true,
            loadMsg: '玩命加载中，请耐心等待.....',
            pagination: true,
            idField: 'MUId',
            pageSize: 10,
            showFooter: true,
            columns: [[
                { field: 'MUId', title: '序号', width: 100, hidden: true },
                { field: 'MUName', title: '计量单位名称', width: 150, sortable: true, resizable: true },
                { field: 'BaseName', title: '基本计量单位名称', width: 150, sortable: true, resizable: true },
                { field: 'TransformRate', title: '转换规模', width: 150, sortable: true, resizable: true },
                { field: 'MUStatusName', title: '计量单位状态', width: 100, sortable: true, resizable: true }
            ]],
            queryParams: { s: status, k: key }
        });

        var p = $('#MUList').datagrid('getPager');
        $(p).pagination({
            beforePageText: '第',//页数文本框前显示的汉字   
            afterPageText: '页    共 {pages} 页',
            displayMsg: '当前显示 {from} - {to} 条记录    共 {total} 条记录',
            onBeforeRefresh: function () {
                $(this).pagination('loading');//正在加载数据中...  
                $(this).pagination('loaded'); //数据加载完毕  
            }
        });
    }

    loadList();

    function btnSearch() {
        loadList();
    }

</script>
</html>
