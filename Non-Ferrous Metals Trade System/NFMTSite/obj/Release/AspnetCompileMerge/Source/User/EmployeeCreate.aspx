<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeCreate.aspx.cs" Inherits="NFMTSite.User.EmployeeCreate" %>

<%@ Register TagName="Navigation" TagPrefix="NFMT" Src="~/Control/Navigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>员工新增</title>
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

            $("#txbAccountName").jqxInput({ height: 25, width: 200 });
            $("#txbPassWord").jqxInput({ height: 25, width: 200 });
            $("#txbPassWordConfirm").jqxInput({ height: 25, width: 200 });

            $("#rdMale").jqxRadioButton({ width: 250, height: 25, checked: true });
            $("#rdFemale").jqxRadioButton({ width: 250, height: 25 });

            $("#dtBirthday").jqxDateTimeInput({ width: 200, height: 25, formatString: "yyyy-MM-dd" });

            $("#btnAdd").jqxButton({ height: 25, width: 100 });
            $("#btnCancel").jqxLinkButton({ height: 25, width: 100 });

            //绑定 所属集团
            var url = "Handler/DeptDDLHandler.ashx";
            var ddlsource = { datatype: "json", datafields: [{ name: "DeptName" }, { name: "DeptId" }], url: url, async: false };
            var dataAdapter = new $.jqx.dataAdapter(ddlsource);
            $("#ddlDept").jqxComboBox({ source: dataAdapter, displayMember: "DeptName", valueMember: "DeptId", width: 200, height: 25 });

            //绑定 员工状态
            var styleId = $("#hidBDStyleId").val();
            var sourceCorpType = { datatype: "json", url: "Handler/WorkStatusHandler.ashx", async: false };
            var dataAdapter = new $.jqx.dataAdapter(sourceCorpType);
            $("#ddlWorkStatus").jqxDropDownList({ source: dataAdapter, displayMember: "StatusName", valueMember: "DetailId", selectedIndex: 0, width: 200, height: 25, autoDropDownHeight: true });

            //验证器
            $("#jqxExpander").jqxValidator({
                rules:
                    [
                        {
                            input: "#ddlDept", message: "所属部门必选", action: "change", rule: function (input, commit) {
                                return $("#ddlDept").jqxComboBox("val") > 0;
                            }
                        },
                        { input: "#txbEmpCode", message: "员工编号不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbEmpName", message: "员工名称不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbAccountName", message: "员工账号不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbPassWord", message: "员工密码不可为空", action: "keyup,blur", rule: "required" },
                        { input: "#txbPassWordConfirm", message: "确认密码不可为空", action: "keyup,blur", rule: "required" },
                        {
                            input: "#txbPassWordConfirm", message: "密码不一致", action: "keyup, focus", rule: function (input, commit) {
                                if (input.val() === $("#txbPassWord").val()) {
                                    return true;
                                }
                                return false;
                            }
                        }
                    ]
            });


            $("#btnAdd").on("click", function () {

                var isCanSubmit = $("#jqxExpander").jqxValidator("validate");
                if (!isCanSubmit) { return; }

                if (!confirm("确认新增员工？")) { return; }

                var sex = true;
                if ($("#rdMale").val() != true) { sex = false; }

                var Employee = {
                    DeptId: $("#ddlDept").val(),
                    EmpCode: $("#txbEmpCode").val(),
                    Name: $("#txbEmpName").val(),
                    Sex: sex,
                    BirthDay: $("#dtBirthday").val(),
                    Telephone: $("#txbTel").val(),
                    Phone: $("#txbPhone").val(),
                    WorkStatus: $("#ddlWorkStatus").val()
                };

                var account = {
                    AccountName: $("#txbAccountName").val(),
                    PassWord: $("#txbPassWord").val()
                };

                $.post("Handler/EmpCreateHandler.ashx", { Employee: JSON.stringify(Employee), account: JSON.stringify(account) },
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
    <nfmt:navigation runat="server" id="navigation1" />

    <div id="jqxExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            员工新增
        </div>
        <div id="layOutDiv">
            <ul>
                <li>
                    <span style="width: 15%; text-align: right;">所属部门：</span>
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
                    <div id="rdMale" runat="server" style="float: left; margin: 5px 0px 5px 5px;">男</div>
                    <div id="rdFemale" runat="server" style="float: left; margin: 5px 5px 5px -200px;">女</div>
                </li>

                <li>
                    <span style="width: 15%; text-align: right;">生日：</span>
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
                    <span style="width: 15%; text-align: right;">账号：</span>
                    <span>
                        <input type="text" id="txbAccountName" runat="server" /></span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">密码：</span>
                    <span>
                        <input type="password" id="txbPassWord" runat="server" /></span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">确认密码：</span>
                    <span>
                        <input type="password" id="txbPassWordConfirm" runat="server" /></span>
                </li>
                <li>
                    <span style="width: 15%; text-align: right;">在职状态：</span>
                    <input type="hidden" id="hidBDStyleId" runat="server" />
                    <div id="ddlWorkStatus" runat="server"></div>
                </li>
                <li>
                    <span style="text-align: right; width: 15%;">&nbsp;</span>
                    <span>
                        <input type="button" id="btnAdd" value="添加" runat="server" style="margin-left: 65px" /></span>
                    <span><a target="_self" runat="server" href="EmployeeList.aspx" id="btnCancel">取消</a></span>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
