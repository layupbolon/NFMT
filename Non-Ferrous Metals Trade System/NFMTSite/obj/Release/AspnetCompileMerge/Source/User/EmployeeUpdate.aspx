<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeUpdate.aspx.cs" Inherits="NFMTSite.User.EmployeeUpdate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>员工修改</title>
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

            $("#txbEmpCode").jqxInput({ height: 25, width: 200 });
            $("#txbEmpName").jqxInput({ height: 25, width: 200 });
            $("#txbTel").jqxInput({ height: 25, width: 200 });
            $("#txbPhone").jqxInput({ height: 25, width: 200 });

            $("#rdMale").jqxRadioButton({ width: 250, height: 25, checked: true });
            $("#rdFemale").jqxRadioButton({ width: 250, height: 25 });

            var sexValue = $("#hidSexValue").val();
            if (sexValue == 1) {
                $("#rdMale").jqxRadioButton("val", true);
                $("#rdFemale").jqxRadioButton("val", false);
            }
            else {
                $("#rdMale").jqxRadioButton("val", false);
                $("#rdFemale").jqxRadioButton("val", true);
            }

            $("#dtBirthday").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });

            var birthValue = $("#hidBirthday").val();
            $("#dtBirthday").jqxDateTimeInput("val", new Date(birthValue));

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //绑定 所属集团
            var url = "Handler/DeptDDLHandler.ashx";
            var ddlsource = { datatype: "json", datafields: [{ name: "DeptId" }, { name: "DeptName" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(ddlsource);
            $("#ddlDept").jqxComboBox({ source: dataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 200, height: 25 });

            if ($("#hidDeptId").val() > 0) { $("#ddlDept").jqxComboBox("val", $("#hidDeptId").val()) };

            //绑定 员工状态
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "Handler/WorkStatusHandler.ashx", async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlWorkStatus").jqxDropDownList({ source: dataAdapter, displayMember: "StatusName", valueMember: "DetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true });
            var workStatus = $("#hidWorkStatus").val();
            $("#ddlWorkStatus").jqxDropDownList("val", workStatus);

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        { input: "#txbEmpCode", message: "员工编号不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbEmpName", message: "员工名称不可为空", action: "keyup,blur", rule: "required" }
                    ]
            });

            $("#btnAdd").on("click", function () {
                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认修改员工信息?")) { return; }

                var deptId = $("#ddlDept").val();
                var empCode = $("#txbEmpCode").val();
                var empName = $("#txbEmpName").val();
                var male = $("#rdMale").val();
                var birthday = $("#dtBirthday").val();
                var tel = $("#txbTel").val();
                var phone = $("#txbPhone").val();
                var workStatus = $("#ddlWorkStatus").val();

                $.post("Handler/EmpUpdateHandler.ashx", {
                    DeptId: deptId,
                    empCode: empCode,
                    empName: empName,
                    male: male,
                    birthday: birthday,
                    tel: tel,
                    phone: phone,
                    workStatus: workStatus,
                    id: "<%=Request.QueryString["id"] %>"
                },
                    function (result) {
                        var obj = JSON.parse(result);
                        alert(obj.Message);
                        if (obj.ResultStatus.toString() == "0") {
                            
                            document.location.href = "EmployeeList.aspx";
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
            员工修改
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">所属集团：</span>
                    <input type="hidden" runat="server" id="hidDeptId" />
                    <div id="ddlDept" runat="server"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">员工编号：</span>
                    <span>
                        <input type="text" id="txbEmpCode" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">姓名：</span>
                    <span>
                        <input type="text" id="txbEmpName" runat="server" /></span></li>
                <li>
                    <span style="width: 15%; text-align: right;">性别：</span>
                    <input type="hidden" runat="server" id="hidSexValue" />
                    <div id="rdMale" runat="server" style="float: left; margin: 5px 0px 5px 5px;">男</div>
                    <div id="rdFemale" runat="server" style="float: left; margin: 5px 5px 5px -200px;">女</div>
                </li>

                <li>
                    <span style="width: 15%; text-align: right;">生日：</span>
                    <input type="hidden" runat="server" id="hidBirthday" />
                    <div id="dtBirthday" runat="server" style="float: left;"></div>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">手机号码：</span>
                    <span>
                        <input type="text" id="txbTel" runat="server" /></span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">座机号码：</span>
                    <span>
                        <input type="text" id="txbPhone" runat="server" /></span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">在职状态：</span>
                    <input type="hidden" id="hidBDStyleId" runat="server" />
                    <input type="hidden" runat="server" id="hidWorkStatus" />
                    <div id="ddlWorkStatus" runat="server"></div>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="提交" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="EmployeeList.aspx" id="btnCancel">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
