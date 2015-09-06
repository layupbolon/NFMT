<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NFMTSite.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>
    <script type="text/javascript">

        function ajaxFileUpload() {
           

            $.ajaxFileUpload
            (
                {
                    url: 'Files/Handler/FileUpLoadHandler.ashx',
                    secureuri: false,
                    fileElementId: 'fileToUpload',
                    dataType: 'json',
                    success: function (data, status) {
                        if (typeof (data.error) != 'undefined') {
                            if (data.error != '') {
                                alert(data.error);
                            } else {
                                alert(data.msg);
                            }
                        }
                    },
                    error: function (data, status, e) {
                        alert(e);
                    }
                }
            )

            return false;

        }
    </script>
</head>
<body>
    <input id="fileToUpload" type="file" size="45" name="fileToUpload" />

<input type="button" id="buttonUpload" onclick="return ajaxFileUpload();">
       上传</input>
</body>
</html>
