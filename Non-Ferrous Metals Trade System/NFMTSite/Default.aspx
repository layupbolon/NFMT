<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NFMTSite.Default" %>

<%@ Register TagName="Menu" TagPrefix="NFMT" Src="~/Control/Menu.ascx" %>
<%@ Register TagName="Tree" TagPrefix="NFMT" Src="~/Control/Tree.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="Description"><%=NFMT.Common.DefaultValue.SystemName%></title>
    <link rel="SHORTCUT ICON" href="maike.ico"  type="image/x-icon" />
    <link rel="stylesheet" href="jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="css/Layout.css" type="text/css" />
    <script type="text/javascript" src="jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="js/status.js"></script>
    <script type="text/javascript" src="js/Utility.js"></script>

    <script type="text/javascript">
        function check() {
            var pre_url = window.frames['TargeForm'].document.location;
            setCookie('<%=NFMT.Common.DefaultValue.UrlCookieName%>', pre_url, 1);
        }

        function getCookie(c_name) {
            if (document.cookie.length > 0) {
                c_start = document.cookie.indexOf(c_name + "=")
                if (c_start != -1) {
                    c_start = c_start + c_name.length + 1
                    c_end = document.cookie.indexOf(";", c_start)
                    if (c_end == -1) c_end = document.cookie.length
                    return unescape(document.cookie.substring(c_start, c_end))
                }
            }
            return ""
        }

        function setCookie(c_name, value, expiredays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + expiredays);
            document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
        }
    </script>
</head>
<body onbeforeunload="check()">
    <NFMT:Menu runat="server" ID="menu1" />

    <div style="width: 100%; margin-top: 10px;">
        <div id="mainSplitter">
            <div class ="SplitterDiv">
                <NFMT:Tree runat="server" ID="tree1" />
            </div>

            <div class ="SplitterDiv" style="margin-left:0px; padding:0px 0px 0px 0px;">
                <iframe src="MainForm.aspx" id="TargeForm" name="TargeForm" style="width:100%; height:99%; border:none; margin:0px 0px 0px 0px; border:0px;" frameborder="0"></iframe>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var cssUrl = "jqwidgets/styles/jqx." + $.jqx.theme + ".css";
            var link = $('<link rel="stylesheet" href="' + cssUrl + '" media="screen" />');
            $(document).find('head').append(link);

            $("#jqxMenu").jqxMenu();
            $("#jqxTree").jqxTree();
            $("#mainSplitter").jqxSplitter({ width: "100%", height: document.documentElement.clientHeight - 70, orientation: "vertical", panels: [{ size: "15%" }, { size: "85%" }] });

            var url = getCookie('<%=NFMT.Common.DefaultValue.UrlCookieName%>');
            if (url && url != "") {
                $("#TargeForm").attr('src', url);
            } else {
                $("#TargeForm").attr('src', "MainForm.aspx");
            }
        });
    </script>

    <script type="text/javascript" src="js/Sms.js"></script>
</body>
</html>

