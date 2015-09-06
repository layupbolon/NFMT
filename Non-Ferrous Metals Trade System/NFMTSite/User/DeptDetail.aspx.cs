using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class DeptDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.Department dept = new NFMT.User.Model.Department();
        public int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 17, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("部门管理", string.Format("{0}User/DepartmentList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("部门明细", string.Empty);

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
                            this.ddlCorpId.InnerText = dept.CorpName;
                            this.txbDeptCode.InnerText = dept.DeptCode;
                            this.txbDeptName.InnerText = dept.DeptName;
                            this.txbDeptFullName.InnerText = dept.DeptFullName;
                            this.txbDeptShort.InnerText = dept.DeptShort;
                            NFMT.Data.Model.BDStyleDetail bd = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.DeptType)[dept.DeptType];
                            if (bd != null)
                                this.ddlDeptType.InnerText = bd.DetailName;
                            this.ddlParentDeptName.InnerText = dept.ParentDeptName;
                            //this.ddlDeptLevel.InnerText = dept.DeptLevel.ToString();

                            this.hidId.Value = dept.DeptId.ToString();

                            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            string json = serializer.Serialize(dept);
                            this.hidModel.Value = json;
                        }
                    }
                }
            }
        }
    }
}