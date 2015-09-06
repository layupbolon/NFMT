<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpLoadTest.aspx.cs" Inherits="NFMTSite.Files.FileUpLoadTest" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function upload() {
            var formdata = new FormData();
            $.each($('.test'), function (i, file) {
                formdata.append('file-' + i, file);
            });

            //var files = $(".test");
            //for (i = 0; i < files.length - 1; i++) {
            //    var item = files[i];
            //    formdata.append('file-' + i, file);
            //}
            var options = {
                type: 'POST',
                url: 'Handler/FileUpLoadHandler.ashx',
                data: formdata,
                success: function (result) {
                    alert(result.msg);
                },
                processData: false,  // 告诉jQuery不要去处理发送的数据
                contentType: false,   // 告诉jQuery不要去设置Content-Type请求头
                dataType: "json"
            };
            $.ajax(options);
        }

        var line = 1;
        function addli(i) {
            if (i > line) {
                var nli = $("<li><strong>附件" + i + "：</strong><input id=\"file" + i + "\" type=\"file\" class=\"test\" name=\"file" + i + "\" onchange=\"addli(" + (i + 1) + ");\" /></li>");
                $("#ulAttach").append(nli);
                line++;
            }
        }
    </script>

</head>
<body>
    <ul id="ulAttach">
            <li>
                <strong>附件1：</strong>
                <input id="file1" type="file" name="file1" class="test" onchange="addli(2);" />
            </li>
        </ul>
    <input type="button" value="upload" onclick="javascript: upload();" />
</body>
</html>
