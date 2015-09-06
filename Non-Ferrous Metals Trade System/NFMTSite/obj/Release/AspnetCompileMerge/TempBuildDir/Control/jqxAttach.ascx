<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="jqxAttach.ascx.cs" Inherits="NFMTSite.Control.jqxAttach" %>
<div id="<%=AttachId %>" style="float: right;"></div>
<script type="text/javascript">

    $("#<%=AttachId%>").jqxFileUpload({
        width: 325, 
        uploadUrl: "<%=UploadUrl%>", 
        fileInputName: "<%=string.IsNullOrEmpty(FileInputName)?AttachId:FileInputName%>", 
        multipleFilesUpload: false, 
        localization: { browseButton: "选择上传文件", cancelButton: "删除文件" }, 
        renderFiles: function (fileName) {
            var stopIndex = fileName.indexOf('.');
            var name = fileName.slice(0, stopIndex);
            var extension = fileName.slice(stopIndex);
            return "<div><span><b>" + name + "</b>" + extension + "</span></div>";
        }
    });

    $("#<%=AttachId%>").on("select", function (event) {
        $("#<%=AttachId%>BrowseButton").hide();//影藏选择按钮
        $("#<%=AttachId%>UploadButton").hide();//影藏上传按钮
        eval("<%=AttachId%>_hasFile = true;");
    });

    $("#<%=AttachId%>").on("remove", function (event) {
        $("#<%=AttachId%>BrowseButton").show();
        eval("<%=AttachId%>_hasFile = false;");
    });
</script>
