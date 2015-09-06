<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractExport.aspx.cs" Inherits="NFMTSite.Contract.ContractExport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合约预览</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <style type="text/css">
        .txt {
            text-align: center;
            font-size: medium;
        }

        .tableStyle {
            border-collapse: collapse;
            border-spacing:0;
            width: 100%
        }
            .tableStyle tr td {
               height:35px;
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

    <div id="PrintArea">
        <table style="width: 711px; height: 570px; margin-left: auto; margin-right: auto; border:0px; " >
            <tr>
                <td colspan="10">

                    <table style="width: 711px" border="0">
                        <tr>
                            <td style="width: 480px; text-align: right">
                                <h1><%=this.asset.AssetName%><%=this.tradeDirection%>合约</h1>
                            </td>
                            <td style="width: 231px; text-align: right">
                                <img src="<%=NFMT.Common.DefaultValue.NfmtSiteName%>/maike.png" alt="Maike" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <div>
                        <table border="0">
                            <tr>
                                <td style="width: 98px">卖方：</td>
                                <td style="width: 234px"><%=this.sellCorpName%></td>
                                <td style="width: 108px">合同编号：</td>
                                <td style="width: 237px"><%=this.contract.OutContractNo%></td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                                <td>签约地点：</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>买方：</td>
                                <td><%=this.buyCorpName%></td>
                                <td>签约日期：</td>
                                <td><%=this.contract.ContractDate.ToShortDateString()%></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
             <tr>
                <td colspan="10">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="10">经买卖双方洽商同意，签订如下<%=this.tradeDirection%>合同：</td>
            </tr>
            <tr>
                <td colspan="10">一、商品名称、品牌、型号、产地、数量、单价、金额、交货时间</td>
            </tr>
            <tr>
                <td colspan="10">
                    <div class="txt">
                        <table class="tableStyle" style="display: <%=string.IsNullOrEmpty(this.contractDetailStr)?"none":"block"%>">
                            <tr>
                                <td style="width: 85px">商品名称</td>
                                <td style="width: 85px">品牌商标</td>
                                <td style="width: 50px">规格</td>
                                <td style="width: 50px">产地</td>
                                <td style="width: 85px">数量（吨）</td>
                                <td style="width: 85px">含税单价</td>
                                <td style="width: 110px">金额</td>
                                <td style="width: 50px">仓库</td>
                                <td style="width: 85px">交货时间</td>
                            </tr>
                            <%--<tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td rowspan="5">款到发货</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
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
                            <td>&nbsp;</td>
                        </tr>--%>

                            <%=this.contractDetailStr%>
                            <tr>
                                <td colspan="4">合计</td>
                                <td><%=!string.IsNullOrEmpty(contractDetailStr)? dt.Rows.Cast<System.Data.DataRow>().Sum(a=>Convert.ToDecimal(a["GrossAmount"])):0%></td>
                                <td>&nbsp;</td>
                                <td><%=!string.IsNullOrEmpty(contractDetailStr)? dt.Rows.Cast<System.Data.DataRow>().Sum(a=>Convert.ToDecimal(a["GrossAmount"])*Convert.ToDecimal(a["Price"])).ToString("#0.00"):""%></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">合计人民币金额（大写）</td>
                                <td colspan="4" style="text-align: left;"><%=!string.IsNullOrEmpty(contractDetailStr)? this.MoneyToChinese(dt.Rows.Cast<System.Data.DataRow>().Sum(a=>Convert.ToDecimal(a["GrossAmount"])*Convert.ToDecimal(a["Price"])).ToString()):"" %></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
             <tr>
                <td colspan="10">&nbsp;</td>
            </tr>
            <%--<tr>
            <td colspan="10">二、</td>
        </tr>
        <tr>
            <td colspan="10">三、</td>
        </tr>
        <tr>
            <td colspan="10">四、</td>
        </tr>
        <tr>
            <td colspan="10">五、</td>
        </tr>
        <tr>
            <td colspan="10">六、</td>
        </tr>
        <tr>
            <td colspan="10">七、</td>
        </tr>
        <tr>
            <td colspan="10">八、</td>
        </tr>
        <tr>
            <td colspan="10">九、</td>
        </tr>
        <tr>
            <td colspan="10">十、</td>
        </tr>
        <tr>
            <td colspan="10">十一、</td>
        </tr>--%>
            <%=this.contractClausesStr %>
            <tr>
                <td colspan="10">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="10">
                    <div>
                        <table border="0">
                            <tr>
                                <td style="width: 135px">卖方（章）：</td>
                                <td style="width: 201px"><%=this.sellCorpName%></td>
                                <td style="width: 137px">买方（章）：</td>
                                <td style="width: 204px"><%=this.buyCorpName%></td>
                            </tr>
                            <tr>
                                <td>法人或委托人：</td>
                                <td>&nbsp;</td>
                                <td>法人或委托人：</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>电话：</td>
                                <td>&nbsp;</td>
                                <td>电话：</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>传真：</td>
                                <td>&nbsp;</td>
                                <td>传真：</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>开户银行：</td>
                                <td>&nbsp;</td>
                                <td>开户银行：</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>账号：</td>
                                <td>&nbsp;</td>
                                <td>账号：</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
