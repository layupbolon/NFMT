<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeInfo.aspx.cs" Inherits="NFMTSite.WorkFlow.NodeInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="新增节点" OnClick="btnAdd_Click" />
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="NodeId"
                GridLines="None" OnRowEditing="GridView1_RowEditing" OnRowCreated="GridView1_RowCreated" RowStyle-HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="NodeId" HeaderText="节点序号" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="NodeStatus" HeaderText="节点状态" ReadOnly="True" Visible="False"/>
                    <asp:BoundField DataField="NodeLevel" HeaderText="节点级别" ReadOnly="True" />
                    <asp:BoundField DataField="MasterId" HeaderText="模版序号" ReadOnly="True" Visible="False"/>
                    <asp:BoundField DataField="NodeName" HeaderText="节点名称" ReadOnly="True" />
                    <asp:BoundField DataField="NodeType" HeaderText="节点类型" ReadOnly="True" />
                    <asp:BoundField DataField="IsFirst" HeaderText="是否第一节点" ReadOnly="True" />
                    <asp:BoundField DataField="IsLast" HeaderText="是否最终节点" ReadOnly="True" />
                    <asp:BoundField DataField="PreNodeId" HeaderText="上一节点序号" ReadOnly="True" />
                    <asp:BoundField DataField="RoleId" HeaderText="审核角色" ReadOnly="True" />
                    <asp:BoundField DataField="AuthGroupId" HeaderText="权限组序号" ReadOnly="True" />
                    <asp:TemplateField HeaderText="修改">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="修改" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="审核">
                        <ItemTemplate>
                            <asp:Button ID="btnAudit" runat="server" Text="审核" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="节点条件">
                        <ItemTemplate>
                            <asp:Button ID="btnNodeCondition" runat="server" Text="节点条件" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
