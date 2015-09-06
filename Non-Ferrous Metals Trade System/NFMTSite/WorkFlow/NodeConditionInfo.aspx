<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeConditionInfo.aspx.cs" Inherits="NFMTSite.WorkFlow.NodeConditionInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="新增条件" OnClick="btnAdd_Click" />
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ConditionId"
                GridLines="None" OnRowEditing="GridView1_RowEditing" OnRowCreated="GridView1_RowCreated" RowStyle-HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="ConditionId" HeaderText="条件序号" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="ConditionStatus" HeaderText="节点状态" ReadOnly="True" Visible="False"/>
                    <asp:BoundField DataField="NodeId" HeaderText="节点序号" ReadOnly="True" Visible="False"/>
                    <asp:BoundField DataField="BefValue" HeaderText="前值" ReadOnly="True"/>
                    <asp:BoundField DataField="AftValue" HeaderText="后值" ReadOnly="True" />
                    <asp:BoundField DataField="ConditionType" HeaderText="条件类型" ReadOnly="True" />
                    <asp:BoundField DataField="TrueNodeId" HeaderText="True值后节点" ReadOnly="True" />
                    <asp:BoundField DataField="FalseNodeId" HeaderText="False值后节点" ReadOnly="True" />
                    <asp:TemplateField HeaderText="修改">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="修改" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="作废">
                        <ItemTemplate>
                            <asp:Button ID="btnCancel" runat="server" Text="作废" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="审核">
                        <ItemTemplate>
                            <asp:Button ID="btnAudit" runat="server" Text="审核" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
