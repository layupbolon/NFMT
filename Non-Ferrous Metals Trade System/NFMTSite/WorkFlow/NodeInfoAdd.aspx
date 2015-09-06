<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeInfoAdd.aspx.cs" Inherits="NFMTSite.WorkFlow.NodeInfoAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0">
                <tr>
                    <th colspan="2"><%=title %>></th>
                </tr>
                <tr>
                    <td>节点级别
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNodelevel" runat="server">
                            <asp:ListItem Text="1" Value="1" Selected="True" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>节点名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtNodeName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>节点类型
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNodeType" runat="server">
                            <asp:ListItem Text="审核" Value="1" Selected="True" />
                            <asp:ListItem Text="知会" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>是否第一节点
                    </td>
                    <td>
                        <asp:CheckBox ID="ckbIsFirst" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>是否最终节点
                    </td>
                    <td>
                        <asp:CheckBox ID="ckbIsLast" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>上一节点序号
                    </td>
                    <td>
                        <asp:TextBox ID="txtPreNodeId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>审核角色
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoleId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>权限组序号
                    </td>
                    <td>
                        <asp:TextBox ID="txtAuthGroupId" runat="server"></asp:TextBox>
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
