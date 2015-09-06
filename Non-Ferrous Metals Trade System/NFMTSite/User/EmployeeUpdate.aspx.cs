using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class EmployeeUpdate : System.Web.UI.Page
    {
        NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 18, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("员工管理", string.Format("{0}User/EmployeeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("员工修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("EmployeeList.aspx");

                        NFMT.User.BLL.EmployeeBLL empBLL = new NFMT.User.BLL.EmployeeBLL();
                        var result = empBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("EmployeeList.aspx");

                        NFMT.User.Model.Employee emp = result.ReturnValue as NFMT.User.Model.Employee;
                        if (emp != null)
                        {
                            this.hidDeptId.Value = emp.DeptId.ToString();
                            this.hidSexValue.Value = emp.Sex ? "1" : "0";
                            this.hidBirthday.Value = emp.BirthDay.ToString("yyyy-MM-dd");
                            this.hidWorkStatus.Value = emp.WorkStatus.ToString();

                            this.txbEmpCode.Value = emp.EmpCode;
                            this.txbEmpName.Value = emp.Name;
                            this.txbTel.Value = emp.Telephone;
                            this.txbPhone.Value = emp.Phone;
                        }

                        this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.在职状态).ToString();
                    }
                }
            }
        }
    }
}