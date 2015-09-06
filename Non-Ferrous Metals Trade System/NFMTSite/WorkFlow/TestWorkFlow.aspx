<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWorkFlow.aspx.cs" Inherits="NFMTSite.WorkFlow.TestWorkFlow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1" />
        <asp:FileUpload ID="FileUpload1" runat="server" />

        <%--<div>
            <object id="ctl" classid="clsid:045F27FC-69B4-454D-AA2F-CCEDA6390399" width="1000" height ="700"></object>
            </div>--%>
        <%--<asp:GridView ID="GridView1" runat="server"></asp:GridView>--%>
    <div>
    
        <asp:Button ID="btn1" runat="server" OnClick="Button1_Click" Text="提交审核" />
    
    </div>
       <%-- <p>
            <asp:Button ID="BtnAudit1" runat="server" OnClick="BtnAudit1_Click" Text="审核人1" />
        </p>
        <p>
            <asp:Button ID="BtnAudit2" runat="server" OnClick="BtnAudit2_Click" Text="审核人2" />
            <asp:Button ID="BtnAudit21" runat="server" OnClick="BtnAudit21_Click" Text="审核人21" />
        </p>
        <p>
            <asp:Button ID="BtnAudit3" runat="server" OnClick="BtnAudit3_Click" Text="审核人3" />
        </p>--%>
    </form>
 </div></div></div><script type="text/javascript" src="../js/Sms.js"></script></body>
</html>
