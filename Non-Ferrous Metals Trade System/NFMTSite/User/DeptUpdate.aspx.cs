using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class DeptUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 17, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("部门管理", string.Format("{0}User/DepartmentList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("部门修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("DepartmentList.aspx");

                        NFMT.User.BLL.DepartmentBLL deptBLL = new NFMT.User.BLL.DepartmentBLL();
                        var result = deptBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("DepartmentList.aspx");

                        NFMT.User.Model.Department dept = result.ReturnValue as NFMT.User.Model.Department;
                        if (dept != null)
                        {
                            this.hidCorpId.Value = dept.CorpId.ToString();
                            this.hidDeptType.Value = dept.DeptType.ToString();
                            //this.hidDeptLevel.Value = dept.DeptLevel.ToString();
                            this.hidParentDeptId.Value = dept.ParentLeve.ToString();

                            this.txbDeptCode.Value = dept.DeptCode;
                            this.txbDeptName.Value = dept.DeptName;
                            this.txbDeptFullName.Value = dept.DeptFullName;
                            this.txbDeptShort.Value = dept.DeptShort;
                        }
                    }
                    this.hidStyleId.Value = ((int)NFMT.Data.StyleEnum.部门类型).ToString();
                }
            }
        }
    }
}