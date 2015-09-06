<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceInvoiceInvApplyPrint.aspx.cs" Inherits="NFMTSite.Invoice.FinanceInvoiceInvApplyPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>开票申请打印</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var source =
            {
                datafields:
                [
                   { name: "ContractDate", type: "date" },
                   { name: "OutContractNo", type: "string" },
                   { name: "CorpName", type: "string" },
                   { name: "NetAmount", type: "number" },
                   { name: "UnitPrice", type: "number" },
                   { name: "InvoiceBala", type: "number" }
                ],
                datatype: "json",
                localdata: <%=this.json%>
                };
            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#jqxgrid").jqxGrid(
            {
                width: "98%",
                source: dataAdapter,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                sortable: true,
                enabletooltips: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                  { text: "日期", datafield: "ContractDate", cellsformat: "yyyy-MM-dd",width:"15%"  },
                  { text: "合约号", datafield: "OutContractNo",width:"15%" },
                  { text: "客户抬头", datafield: "CorpName",width:"20%"},
                  { text: "数量", datafield: "NetAmount",width:"15%" },
                  { text: "单价", datafield: "UnitPrice" ,width:"15%"},
                  { text: "总金额", datafield: "InvoiceBala",width:"20%" }
                ]
            });

            $("#btnPrint").jqxButton({ height: 25, width: 100 });
            $("#btnPrint").on("click", function () {
                var oldHtml = document.body.innerHTML;
                var gridContent = $("#jqxgrid").jqxGrid('exportdata', 'html');
                document.body.innerHTML = gridContent;
                window.print();
                document.body.innerHTML = oldHtml;
            });
        });
    </script>

</head>
<body>
    <div id="jqxgrid" style="float: left; margin: 5px 0 0 5px;"></div>
    <input type="button" id="btnPrint" value="打印" style="margin-left: 50%; margin-top: 30px;" />
</body>
</html>
