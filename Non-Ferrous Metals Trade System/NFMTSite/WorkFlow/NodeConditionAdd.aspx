<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeConditionAdd.aspx.cs" Inherits="NFMTSite.WorkFlow.NodeConditionAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0">
                <tr>
                    <th colspan="2"><%=title %></th>
                </tr>
                <tr>
                    <td>前值
                    </td>
                    <td>
                        <asp:TextBox ID="txtBefValue" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>后值
                    </td>
                    <td>
                        <asp:TextBox ID="txtAftValue" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>条件类型
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlConditionType" runat="server">
                            <asp:ListItem Text="1" Value="1" Selected="True" />
                            <asp:ListItem Text="2" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>True值后节点
                    </td>
                    <td>
                        <asp:TextBox ID="txtTrueNodeId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>False值后节点
                    </td>
                    <td>
                        <asp:TextBox ID="txtFalseNodeId" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
            <label id="lblMsg" runat="server" />
    </div>
    </form>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
