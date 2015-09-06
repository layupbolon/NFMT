<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverPlaceUpdate.aspx.cs" Inherits="NFMTSite.BasicData.DeliverPlaceUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交货地修改</title>
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
            $("#ddlDPType").jqxDropDownList({ source: dataAdapter, displayMember: "DetailName", valueMember: "StyleDetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true });
            $("#ddlDPType").jqxDropDownList("val", "<%=this.deliverPlace.DPType%>");

            //地区 
            var cySource = { datatype: "json", url: "Handler/AreaDDLHandler.ashx", async: false };
            var cyDataAdapter = new $.jqx.dataAdapter(cySource);
            $("#ddlDPArea").jqxComboBox({ source: cyDataAdapter, displayMember: "AreaName", valueMember: "AreaId", width: 200, height: 25, autoDropDownHeight: true });
            $("#ddlDPArea").jqxComboBox("val", "<%=this.deliverPlace.DPArea%>");

            //仓储/码头公司
            var ddlCorpIdurl = "../User/Handler/CorpDDLHandler.ashx?isSelf=1";
            var ddlCorpIdsource = { datatype: "json", datafields: [{ name: "CorpId" }, { name: "CorpName" }], url: ddlCorpIdurl, async: false };
            var ddlCorpIddataAdapter = new $.jqx.dataAdapter(ddlCorpIdsource);
            $("#ddlDPCompany").jqxComboBox({ source: ddlCorpIddataAdapter, displayMember: "CorpName", valueMember: "CorpId", width: 200, height: 25, autoDropDownHeight: true });
            $("#ddlDPCompany").jqxComboBox("val", "<%=this.deliverPlace.DPCompany%>");

            $("#txbDPName").jqxInput({ height: 23, width: 200 });
            $("#txbDPFullName").jqxInput({ height: 23, width: 200 });
            $("#txbDPAddress").jqxInput({ height: 23, width: 200 });
            $("#txbDPEAddress").jqxInput({ height: 23, width: 200 });
            $("#txbDPTel").jqxInput({ height: 23, width: 200 });
            $("#txbDPContact").jqxInput({ height: 23, width: 200 });
            $("#txbDPFax").jqxInput({ height: 23, width: 200 });

            CreateSelectStatusDropDownList("ddlDPStatus", "<%=this.deliverPlace.DPStatus%>");
            $("#ddlDPStatus").jqxDropDownList("width", 200);

            $("#btnUpdate").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlDPArea", message: "请选择地区", action: "change", rule: function (input, commit) {
                                return $("#ddlDPArea").jqxComboBox("val") > 0;
                            }
                        },
                        {
                            input: "#ddlDPCompany", message: "请选择仓储/码头公司", action: "change", rule: function (input, commit) {
                                return $("#ddlDPCompany").jqxComboBox("val") > 0;
                            }
                        },
                        { input: "#txbDPName", message: "交货地名称不能为空", action: "keyup, blur", rule: "required" },
                        { input: "#txbDPAddress", message: "交货地地址不能为空", action: "keyup, blur", rule: "required" }
                    ]
            });

            $("#btnUpdate").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                var deliverPlace = {
                    DPId:"<%=this.deliverPlace.DPId%>",
                    DPType: $("#ddlDPType").val(),
                    DPArea: $("#ddlDPArea").val(),
                    DPCompany: $("#ddlDPCompany").val(),
                    DPName: $("#txbDPName").val(),
                    DPFullName: $("#txbDPFullName").val(),
                    DPStatus: $("#ddlDPStatus").val(),
                    DPAddress: $("#txbDPAddress").val(),
                    DPEAddress: $("#txbDPEAddress").val(),
                    DPTel: $("#txbDPTel").val(),
                    DPContact: $("#txbDPContact").val(),
                    DPFax: $("#txbDPFax").val()
                }

                $.post("Handler/DeliverPlaceUpdateHandler.ashx", {
                    deliverPlace: JSON.stringify(deliverPlace)
                },
                    function (result) {
                        alert(result);
                        document.location.href = "DeliverPlaceList.aspx";
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
            交货地修改<input type="hidden" id="hidBDStyleId" runat="server"/>
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
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnUpdate" value="修改" runat="server" /></span>
                    <span><a target="_self" runat="server" href="DeliverPlaceList.aspx" id="btnCancel" style="margin-left: 10px">取消</a></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
