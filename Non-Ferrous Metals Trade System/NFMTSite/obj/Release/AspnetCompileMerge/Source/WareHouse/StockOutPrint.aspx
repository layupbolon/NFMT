<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutPrint.aspx.cs" Inherits="NFMTSite.WareHouse.StockOutPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>提货通知单</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>

    <style type="text/css">
        .txt {
            text-align: center;
            font-size: medium;
        }

        .tableStyle {
            margin-left: auto;
            margin-right: auto;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnPrint").jqxButton({ height: 25, width: 100 });
            $("#btnPrint").on("click", function () {
                document.getElementById("btnPrint").style.display = "none";
                window.print();
                document.getElementById("btnPrint").style.display = "";
            });
        });
    </script>

</head>
<body>
    <input type="button" id="btnPrint" value="打印" style="margin-left: 30%" />

    <h1 style="text-align: center;">迈科资产管理（上海）有限公司</h1>
    <h1 style="text-align: center;">提货通知单</h1>

    <table style="width: 760px; border-collapse: collapse; border-spacing: 0;" border="0" class="tableStyle">
        <tr>
            <td style="width: 120px">&nbsp;</td>
            <td style="width: 400px">&nbsp;</td>
            <td style="width: 100px; float: right">NO:<%=this.No %></td>
        </tr>
        <tr>
            <td style="height: 30px">购货单位：</td>
            <td><%=NFMT.User.UserProvider.Corporations.SingleOrDefault(a=>a.CorpId==this.stockOutApply.BuyCorpId).CorpName %></td>
            <td style="float: right"><%=this.stockOut.StockOutTime.ToLongDateString()%></td>
        </tr>
    </table>

    <table style="width: 800px; height: 420px; border-collapse: collapse; border-spacing: 0;" border="1" class="tableStyle">
        <tr class="txt">
            <td>储存卡号</td>
            <td>品名</td>
            <td>品牌</td>
            <td>订货数（吨）</td>
            <td>单价（元/吨）</td>
            <td>实发数（吨）</td>
            <td>备注</td>
        </tr>
        <%--<tr class="txt">
            <td>15W-6241</td>
            <td>&nbsp;</td>
            <td>BIRLA</td>
            <td>150</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>--%>
        <%=this.GetDetailHTML(user,details) %>

        <tr class="txt">
            <td>提货仓库</td>
            <td colspan="6"><%=this.DPName %></td>
        </tr>
        <tr>
            <td>制单人签字：</td>
            <td colspan="5" rowspan="4">
                <table style="width: 550px; height: 200px" border="0">
                    <tr>
                        <td style="text-align: center;">提货人注意事项</td>
                    </tr>
                    <tr>
                        <td>1、本提单请在签发日起叁日内提货，逾期按仓库制度办理加收仓储租金。</td>
                    </tr>
                    <tr>
                        <td>2、提货时请在本提单右边加盖提货单位印章。</td>
                    </tr>
                    <tr>
                        <td>3.本提单不得涂改，涂改后无效，如遇遗失勿必立即前来挂失。在挂失前如货已提出，概由持单人负责。</td>
                    </tr>
                </table>
            </td>
            <td>提货人签字：</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>签发单位盖章：</td>
            <td>提货单位盖章：</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center;">备注</td>
            <td colspan="6">&nbsp;</td>
        </tr>
    </table>
</body>
</html>
