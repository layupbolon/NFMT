<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverPlaceDetail.aspx.cs" Inherits="NFMTSite.BasicData.DeliverPlaceDetail" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交货地明细</title>
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.metro.css" media="screen" />
    <link rel="stylesheet" href="../css/Layout.css" type="text/css" />
    <script type="text/javascript" src="../jqwidgets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/scripts/jqx-all.js"></script>
    <script type="text/javascript" src="../js/Utility.js"></script>
    <script type="text/javascript" src="../js/status.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#jqxExpander").jqxExpander({ width: "98%" });

            //绑定 交货地类型
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "../BasicData/Handler/StyleDetailHandler.ashx?id=" + styleId, async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlDPType").jqxDropDownList({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true,disabled:true });
            $("#ddlDPType").jqxDropDownList("val", "<%=this.deliverPlace.DPType%>");

            //地区 
            var cySource = { datatype: "json", url: "Handler/AreaDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlDPArea").jqxComboBox({ source: cyDataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 200, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlDPArea").jqxComboBox("val", "<%=this.deliverPlace.DPArea%>");

            //仓储/码头公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlDPCompany").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25, autoDropDownHeight: true, disabled: true });
            $("#ddlDPCompany").jqxComboBox("val", "<%=this.deliverPlace.DPCompany%>");

            $("#txbDPName").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbDPFullName").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbDPAddress").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbDPEAddress").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbDPTel").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbDPContact").jqxInput({ height: 23, width: 200, disabled: true });
            $("#txbDPFax").jqxInput({ height: 23, width: 200, disabled: true });

            CreateSelectStatusDropDownList("ddlDPStatus", "<%=this.deliverPlace.DPStatus%>");
            $("#ddlDPStatus").jqxDropDownList("width", 200);
            $("#ddlDPStatus").jqxDropDownList("disabled", true);


            $("#btnFreeze").jqxButton({ height: 25, width: 96 });
            $("#btnUnFreeze").jqxButton({ height: 25, width: 96 });

            $("#btnFreeze").on("click", function () {
                var operateId = operateEnum.冻结;
                $.post("Handler/DeliverPlaceStatusHandler.ashx", { id: "<%=this.deliverPlace.DPId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "DeliverPlaceList.aspx";
                        }
                    }
                );
            });

            $("#btnUnFreeze").on("click", function () {
                var operateId = operateEnum.解除冻结;
                $.post("Handler/DeliverPlaceStatusHandler.ashx", { id: "<%=this.deliverPlace.DPId%>", oi: operateId },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            document.location.href = "DeliverPlaceList.aspx";
                        }
                    }
                );
            });
        });
    </script>

</head>
<body>
    <NFMT:Navigation runat="server" ID="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            交货地明细<input type="hidden" id="hidBDStyleId" runat="server"/>
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">交货地类型：</span>
                    <div id="ddlDPType"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地地区：</span>
                    <div id="ddlDPArea"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">仓储/码头公司：</span>
                    <div id="ddlDPCompany"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地名称：</span>
                    <input type="text" id="txbDPName" value ="<%=this.deliverPlace.DPName%>"/>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地全称：</span>
                    <input type="text" id="txbDPFullName" value ="<%=this.deliverPlace.DPFullName%>"/>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地地址：</span>
                    <input type="text" id="txbDPAddress" value ="<%=this.deliverPlace.DPAddress%>"/>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地英文地址：</span>
                    <input type="text" id="txbDPEAddress" value ="<%=this.deliverPlace.DPEAddress%>"/>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地电话：</span>
                    <input type="text" id="txbDPTel" value ="<%=this.deliverPlace.DPTel%>"/>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地联系人：</span>
                    <input type="text" id="txbDPContact" value ="<%=this.deliverPlace.DPContact%>"/>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">交货地传真：</span>
                    <input type="text" id="txbDPFax" value ="<%=this.deliverPlace.DPFax%>"/>
                </li>
                 <li>
                     <span style="width: 15%; text-align: right;">交货地传真：</span>
                    <div id="ddlDPStatus" />
                </li>
                <li>
                    <span style="text-align: right; width: 10.4%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnFreeze" value="冻结" runat="server" /></span>
                    <span>
                        <input type="button" id="btnUnFreeze" value="解除冻结" runat="server" style="margin-left: 10px" />
                    </span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>