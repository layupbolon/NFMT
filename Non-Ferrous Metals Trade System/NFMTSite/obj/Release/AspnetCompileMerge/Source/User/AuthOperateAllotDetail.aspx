<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthOperateAllotDetail.aspx.cs" Inherits="NFMTSite.User.AuthOperateAllotDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>员工菜单操作权限分配</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <style type="text/css">
        .Tree_li li {
            float: left;
            margin-right: 5px;
        }
    </style>

    <script type="text/javascript">
        var tree4MenusId = "38";
        var tree1MenusId = "6,7,9";
        var tree2MenusId = "5,8,10,114";
        var tree3MenusId = "1,2,3,4,11,12,13,126";

        $(document).ready(function () {
            

            $("#jqxEmpExpander").jqxExpander({ width: "98%" });
            $("#jqxDataExpander").jqxExpander({ width: "98%" });

            var tree1Exist = false;
            var tree2Exist = false;
            var tree3Exist = false;

            $("#jqxTabs").jqxTabs({ width: "99%", position: "top" });

            var source = null;
            $.ajax({
                async: false,
                url: "Handler/GetMenuListForAllotDetailHandler.ashx?empId=" + "<%=this.empId%>" + "&menuIds=" + tree4MenusId,
                success: function (data, status, xhr) {
                    source = data;
                    $("#jqxTree4").empty();
                    $("#jqxTree4").jqxTree({ hasThreeStates: true, source: source, checkboxes: true, allowDrag: false, allowDrop: false });
                }
            });
            
            $("#jqxTabs").on("selected", function (event) {
                var selectedTab = event.args.item;
                if (selectedTab == 1) {
                    if (!tree1Exist) {
                        $.ajax({
                            async: false,
                            url: "Handler/GetMenuListForAllotDetailHandler.ashx?empId=" + "<%=this.empId%>" + "&menuIds=" + tree1MenusId,
                            success: function (data, status, xhr) {
                                source = data;
                                $("#jqxTree1").empty();
                                $("#jqxTree1").jqxTree({ hasThreeStates: true, source: source, checkboxes: true, allowDrag: false, allowDrop: false });
                            }
                        });
                        
                        tree1Exist = true;
                    }
                }
                else if (selectedTab == 2) {
                    if (!tree2Exist) {
                        $.ajax({
                            async: false,
                            url: "Handler/GetMenuListForAllotDetailHandler.ashx?empId=" + "<%=this.empId%>" + "&menuIds=" + tree2MenusId,
                            success: function (data, status, xhr) {
                                source = data;
                                $("#jqxTree2").empty();
                                $("#jqxTree2").jqxTree({ hasThreeStates: true, source: source, checkboxes: true, allowDrag: false, allowDrop: false });
                            }
                        });
                        
                        tree2Exist = true;
                    }
                }
                else if (selectedTab == 3) {
                    if (!tree3Exist) {
                        $.ajax({
                            async: false,
                            url: "Handler/GetMenuListForAllotDetailHandler.ashx?empId=" + "<%=this.empId%>" + "&menuIds=" + tree3MenusId,
                            success: function (data, status, xhr) {
                                source = data;
                                $("#jqxTree3").empty();
                                $("#jqxTree3").jqxTree({ hasThreeStates: true, source: source, checkboxes: true, allowDrag: false, allowDrop: false });
                            }
                        });
                        
                        tree3Exist = true;
                    }
                }
            });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#btnAdd").on("click", function () {
                $("#btnAdd").jqxButton({ disabled: true });

                var items = $("#jqxTree4").jqxTree("getCheckedItems");
                if (items == undefined || items == null)
                    items = new Array();
                var exceptItemIds = "";
                if (tree1Exist) {
                    var tree1CheckItems = $("#jqxTree1").jqxTree("getCheckedItems");
                    for (var i in tree1CheckItems) {
                        items.push(tree1CheckItems[i]);
                    }
                }
                else {
                    exceptItemIds += tree1MenusId;//制单、仓储、点价
                }
                if (tree2Exist) {
                    var tree2CheckItems = $("#jqxTree2").jqxTree("getCheckedItems");
                    for (var i in tree2CheckItems) {
                        items.push(tree2CheckItems[i]);
                    }
                }
                else {
                    exceptItemIds += tree2MenusId;//合约、收付款、发票
                }
                if (tree3Exist) {
                    var tree3CheckItems = $("#jqxTree3").jqxTree("getCheckedItems");
                    for (var i in tree3CheckItems) {
                        items.push(tree3CheckItems[i]);
                    }
                }
                else {
                    exceptItemIds += tree3MenusId;//基础数据、用户权限及其他
                }

                if (exceptItemIds.length > 1) {
                    exceptItemIds = exceptItemIds.substring(0, exceptItemIds.length - 1);
                }

                var details = new Array();
                for (var i in items) {
                    var label = "";
                    var id = "";
                    var parentId = "";
                    var level = 0;

                    if (items[i].label != undefined) { label = items[i].label; }
                    if (items[i].id != undefined) { id = items[i].id; }
                    if (items[i].parentId != undefined) { parentId = items[i].parentId; }
                    if (items[i].level != undefined) { level = items[i].level; }

                    var detail = new Object();
                    detail.label = label;
                    detail.id = id;
                    detail.parentId = parentId;
                    detail.level = level;

                    details.push(detail);
                }

                $.post("Handler/AuthOperateAllotCreate.ashx", { select: JSON.stringify(details), empId: "<%=this.empId%>", exceptItemIds: exceptItemIds },
                function (result) {
                    var obj = JSON.parse(result);
                    alert(obj.Message);
                    $("#btnAdd").jqxButton({ disabled: false });
                });
            });
        });
    </script>
</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxEmpExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            员工信息
        </div>
        <div class="InfoExpander">
            <ul>
                <li>
                    <strong>所属部门：</strong>
                    <span id="ddlDeptId" runat="server"></span>
                </li>
                <li>
                    <strong>员工编号：</strong>
                    <span id="txbEmpCode" runat="server" /></li>
                <li>
                    <strong>姓名：</strong>
                    <span id="txbEmpName" runat="server" /></li>
                <li>
                    <strong>性别：</strong>
                    <span id="rdMale" runat="server"></span></li>
                <li>
                    <strong>生日：</strong>
                    <span id="dtBirthday" runat="server"></span></li>
                <li>
                    <strong>手机号码：</strong>
                    <span id="txbTel" runat="server" />
                </li>
                <li>
                    <strong>座机号码：</strong>
                    <span id="txbPhone" runat="server" />
                </li>
                <li>
                    <strong>在职状态：</strong>
                    <span id="ddlWorkStatus" runat="server"></span>
                </li>
            </ul>
        </div>
    </div>

    <div id="jqxDataExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            菜单操作权限分配
        </div>
        <div>
            <div id="jqxTabs">
                <ul>
                    <li style="margin-left: 30px;">报表</li>
                    <li>制单、仓储、点价</li>
                    <li>合约、收付款、发票</li>
                    <li>基础数据、用户权限及其他</li>
                </ul>
                <div>
                    <div id="jqxTree4" style="width: 100%; float: left; border: none" class="Tree_li">
                        Loading......
                    </div>
                </div>
                <div>
                    <div id="jqxTree1" style="width: 100%; float: left; border: none" class="Tree_li">
                       Loading......
                    </div>
                </div>
                <div>
                    <div id="jqxTree2" style="width: 100%; float: left; border: none" class="Tree_li">
                        Loading......
                    </div>
                </div>
                <div>
                    <div id="jqxTree3" style="width: 100%; float: left; border: none" class="Tree_li">
                        Loading......
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="buttons" style="text-align: center; width: 80%; margin-top: 0px;">
        <input type="button" id="btnAdd" value="提交" runat="server" style="width: 80px;" />&nbsp;&nbsp;&nbsp;
        <a target="_self" runat="server" href="AuthOperateAllot.aspx" id="btnCancel" style="margin-left: 10px">取消</a>&nbsp;&nbsp;&nbsp;
    </div>
</body>
</html>
