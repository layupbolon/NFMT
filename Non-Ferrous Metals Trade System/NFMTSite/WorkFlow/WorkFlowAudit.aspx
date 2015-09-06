<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowAudit.aspx.cs" Inherits="NFMTSite.WorkFlow.WorkFlowAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="/js/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="/js/jQuery.easyui.js" type="text/javascript"></script>
    <script src="/js/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/js/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/js/themes/icon.css" rel="stylesheet" type="text/css" />
    </head>
 <body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="ddlEmpId" runat="server" ></asp:DropDownList>

        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TaskNodeId"
                GridLines="None" RowStyle-HorizontalAlign="Center" OnRowCreated="GridView1_RowCreated">
                <Columns>
                    <asp:BoundField DataField="TaskNodeId" HeaderText="任务节点序号" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="NodeId" HeaderText="节点序号" ReadOnly="True" Visible="True"/>
                    <asp:BoundField DataField="TaskId" HeaderText="任务序号" ReadOnly="True" Visible="True"/>
                    <asp:BoundField DataField="NodeStatus" HeaderText="节点状态" ReadOnly="True" Visible="True"/>
                    <asp:BoundField DataField="NodeLevel" HeaderText="节点等级" ReadOnly="True" Visible="True"/>
                    <asp:BoundField DataField="EmpId" HeaderText="审核人" ReadOnly="True" />
                    <asp:BoundField DataField="AuditTime" HeaderText="审核时间" ReadOnly="True" />
                    
                    <asp:TemplateField HeaderText="审核">
                        <ItemTemplate>
                            <asp:Button ID="btnPass" runat="server" Text="通过" />
                            <asp:Button ID="btnFail" runat="server" Text="不通过" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="附言："></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        </div>

    </form>
</body>
    <%--<script type="text/javascript">
        $(function () {
            var datagrid; //定义全局变量datagrid
            datagrid = $("#dataGrid").datagrid({
                title: ' 工作流 >> 流程审核',
                locale: "zh_CN",
                iconCls: 'icon-save',
                height: 'auto',
                width: 'aoto',
                nowrap: true,
                striped: true, //使用分隔行
                sortOrder: 'desc',
                collapsible: true, //是否可折叠的  
                remoteSort: true, //设置页面排序
                url: 'Getjson.aspx?stype=data', //请求的数据源
                idField: 'TaskNodeId',
                columns: [[//显示的列
                 { field: 'ck', checkbox: true },
                 { field: 'rowId', title: '行号', width: 100, align: "center", sortable: true },
                 { field: 'TaskNodeId', title: '任务节点序号', width: 100, align: "center", sortable: true },
                 { field: 'NodeId', title: '节点序号', width: 100, align: "center", sortable: true },
                 { field: 'TaskId', title: '任务序号', width: 100, align: "center", sortable: true },
                 { field: 'NodeStatus', title: '节点状态', width: 100, align: "center", sortable: true},
                 { field: 'NodeLevel', title: '节点等级', width: 100, align: "center", sortable: true},
                 { field: 'EmpId', title: '审核人', width: 100, align: "center", sortable: true },
                 { field: 'AuditTime', title: '审核时间', width: 100, align: "center"},
                   {
                       field: 'TaskNodeId', title: '操作', width: 100, align: 'center', formatter: function (value, rec, index) {
                           var e = "<img title='通过' alt='通过' style='cursor:pointer'  onclick='Pass(\"" + value + "\")' />&nbsp;&nbsp;&nbsp;&nbsp;";
                           var v = "<img title='不通过' alt='不通过' style='cursor:pointer'  onclick='Fail(\"" + value + "\")' />&nbsp;&nbsp;&nbsp;&nbsp;";
                           return e + v;
                       }
                   }
                ]],
                toolbar: [{
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        Search();
                    }
                }],
                pagination: true,
                rownumbers: true,
                
                onLoadSuccess: function () {
                }
            });
        });

        function GetChecked() {
            var ids = [];
            var selectRows = $('#dataGrid').datagrid('getSelections');
            for (var i = 0; i < selectRows.length; i++) {
                var rows = $('#dataGrid').datagrid('getRows');
                for (var j = 0; j < rows.length; j++) {
                    if (rows[j]._id == selectRows[i].id) {
                        ids.push(selectRows[i]._id);
                        break;
                    }
                }
            }
            var keyString = ids.join(',');
            if (keyString.length == 0) {
                alert("请先选择！");
                return false;
            } else {
                return keyString;
            }

        }

        function Search() {
            //获取查询参数，并附值，附值成功后翻页将自动使用此参数值，如果控件项值改变需重新调用此方法才能生效
            var queryParameter = $('#dataGrid').datagrid("options").queryParams;

            if ($("#countname").val().length == 0) {
                queryParameter.name = "";
            } else {
                queryParameter.name = $("#countname").val();
            }
            $('#dataGrid').datagrid("reload");
        }
    </script>

<body >
    <div>
    <table id="dataGrid" width="100%">
    </table>
        </div>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>--%>
</html>
