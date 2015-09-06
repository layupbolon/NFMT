using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class AuthGroupAllotDetail : System.Web.UI.Page
    {
        public int empId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 100, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string redirectUrl = string.Format("{0}User/AuthEmpList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                this.navigation1.Routes.Add("权限组分配-员工列表", redirectUrl);
                this.navigation1.Routes.Add("员工权限组分配", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out empId) || empId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.BLL.EmployeeBLL employeeBLL = new NFMT.User.BLL.EmployeeBLL();
                result = employeeBLL.Get(user, empId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.Model.Employee employee = result.ReturnValue as NFMT.User.Model.Employee;
                if (employee == null)
                    Response.Redirect(redirectUrl);

                NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == employee.DeptId);

                if (dept != null && dept.DeptId > 0)
                    this.ddlDeptId.InnerText = dept.DeptName;

                this.txbEmpCode.InnerText = employee.EmpCode;
                this.txbEmpName.InnerText = employee.Name;
                this.rdMale.InnerText = employee.Sex ? "男" : "女";
                this.dtBirthday.InnerText = employee.BirthDay.ToShortDateString();
                this.txbTel.InnerText = employee.Telephone;
                this.txbPhone.InnerText = employee.Phone;

                NFMT.User.WorkStatusEnum workStatus = (NFMT.User.WorkStatusEnum)employee.WorkStatus;
                this.ddlWorkStatus.InnerText = workStatus.ToString();

                this.hidContractInOut.Value = ((int)NFMT.Data.StyleEnum.ContractSide).ToString();
                this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
                this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
                this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            }
        }
    }
}