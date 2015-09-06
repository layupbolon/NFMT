<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterInfo.aspx.cs" Inherits="NFMTSite.WorkFlow.MasterInfo"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>

    <form id="form1" runat="server">

        <div>
            模版状态：<asp:DropDownList ID="ddlMasterStatus" runat="server" OnSelectedIndexChanged="ddlMasterStatus_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="MasterId"
                GridLines="None"  RowStyle-HorizontalAlign="Center">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:checkbox ID="btnSelect" runat="server"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MasterId" HeaderText="模版序号" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="MasterName" HeaderText="模版名称" ReadOnly="True" />
                    <asp:BoundField DataField="MasterStatus" HeaderText="模版状态" ReadOnly="True" />
                    <asp:BoundField DataField="CreatorId" HeaderText="创建人" ReadOnly="True" />
                    <asp:BoundField DataField="CreateTime" HeaderText="创建时间" ReadOnly="True" />
                    <asp:BoundField DataField="LastModifyId" HeaderText="最后修改人" ReadOnly="True" />
                    <asp:BoundField DataField="LastModifyTime" HeaderText="最后修改时间" ReadOnly="True" />
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:Button ID="btnAdd" runat="server" Text="新增流程模版" OnClick="btnAdd_Click" />
            <asp:Button ID="btnDetail" runat="server" Text="模版明细" OnClick="btnDetail_Click1" />
            <asp:Button ID="btnEdit" runat="server" Text="修改" OnClick="btnEdit_Click1" />
            <asp:Button ID="btnSubmit" runat="server" Text="提交审核" OnClick="btnSubmit_Click"  />
            <asp:Button ID="btnAudit" runat="server" Text="审核" OnClick="btnAudit_Click1"  />
            <asp:Button ID="btnFreeze" runat="server" Text="冻结" OnClick="btnFreeze_Click"  />
            <asp:Button ID="btnUnFreeze" runat="server" Text="解除冻结" OnClick="btnUnFreeze_Click"   />
        </div>
    </form>

 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
