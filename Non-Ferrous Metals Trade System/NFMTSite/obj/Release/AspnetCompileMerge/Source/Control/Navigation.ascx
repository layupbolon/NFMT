<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="NFMTSite.Control.Navigation" %>
<div id="jqNavigation" style="float: left; margin: 0px 5px 5px 5px;">
    <%=CreateRoute() %>
</div>
<script type="text/javascript">
    $("#jqNavigation").jqxPanel({ width: "98%", height: 20 });
</script>