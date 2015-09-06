<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attach.ascx.cs" Inherits="NFMTSite.Control.Attach" %>
<div id="jqxAttachExpander" style="float: left; margin: 0px 5px 5px 5px;">
    <div>
        附件
    </div>
    <div class="InfoExpander" runat="server" id="attachInfo">
        <ul id="ulAttach">
            <li>
                <strong>附件1：</strong>
                <input id="file1" type="file" name="file1" onchange="addli(2);" />
            </li>
        </ul>
    </div>
</div>
<asp:Literal runat="server" ID="attachJs"></asp:Literal>