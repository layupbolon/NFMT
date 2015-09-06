using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class EmployeeDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.Employee emp = new NFMT.User.Model.Employee();
        public int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                //Utility.VerificationUtility ver = new Utility.VerificationUtility();
                //ver.JudgeOperate(this.Page, 18, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("员工管理", string.Format("{0}User/EmployeeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("员工明细", string.Empty);

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
                            NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == emp.DeptId);

                            if(dept!=null && dept.DeptId>0)
                                this.ddlDeptId.InnerText = dept.DeptName;

                            this.txbEmpCode.InnerText = emp.EmpCode;
                            this.txbEmpName.InnerText = emp.Name;
                            this.rdMale.InnerText = emp.Sex ? "男" : "女";
                            this.dtBirthday.InnerText = emp.BirthDay.ToShortDateString();
                            this.txbTel.InnerText = emp.Telephone;
                            this.txbPhone.InnerText = emp.Phone;

                            NFMT.User.WorkStatusEnum workStatus = (NFMT.User.WorkStatusEnum)emp.WorkStatus;
                            this.ddlWorkStatus.InnerText = workStatus.ToString();

                            this.hidId.Value = emp.EmpId.ToString();

                            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            string json = serializer.Serialize(emp);
                            this.hidModel.Value = json;
                        }
                    }
                }
            }
        }
    }
}