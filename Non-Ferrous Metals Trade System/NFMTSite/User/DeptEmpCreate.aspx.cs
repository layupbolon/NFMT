using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class DeptEmpCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 80, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("员工部门分配-部门列表", string.Format("{0}User/DeptList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("员工分配", string.Empty);

                int deptId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["deptId"]))
                    Response.Redirect("DeptList.aspx");

                if(!int.TryParse(Request.QueryString["deptId"],out deptId))
                    Response.Redirect("DeptList.aspx");

                NFMT.User.BLL.DepartmentBLL bll = new NFMT.User.BLL.DepartmentBLL();
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = bll.Get(user,deptId);
                if(result.ResultStatus!=0)
                    Response.Redirect("DeptList.aspx");
                NFMT.User.Model.Department dept = result.ReturnValue as NFMT.User.Model.Department;

                if(dept == null || dept.DeptId ==0)
                    Response.Redirect("DeptList.aspx");

                NFMT.User.BLL.CorporationBLL corpBll = new NFMT.User.BLL.CorporationBLL();
                result = corpBll.Get(user, dept.CorpId);
                if(result.ResultStatus !=0)
                    Response.Redirect("DeptList.aspx");
                NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                if (corp == null || corp.CorpId == 0 || !corp.IsSelf) 
                    Response.Redirect("DeptList.aspx");

                //设置部门信息
                this.spnDeptCode.InnerHtml = dept.DeptCode;
                this.spnDeptName.InnerHtml = dept.DeptName;
                this.spnCorpName.InnerHtml = corp.CorpName;

                NFMT.Data.StyleEnum style = NFMT.Data.StyleEnum.部门类型;
                NFMT.Data.Model.BDStyleDetail detail = NFMT.Data.DetailProvider.Details(style)[dept.DeptType];
                this.spnDeptType.InnerHtml = detail.DetailName;
                
                this.hidDeptId.Value = dept.DeptId.ToString();
                
            }
        }
    }
}