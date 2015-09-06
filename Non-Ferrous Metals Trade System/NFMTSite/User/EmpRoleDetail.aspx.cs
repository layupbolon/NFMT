using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class EmpRoleDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 83, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string redirectUrl = string.Format("{0}User/EmpRoleList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
                this.navigation1.Routes.Add("员工角色分配-角色列表", redirectUrl);
                this.navigation1.Routes.Add("角色员工分配", string.Empty);

                int id = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                if (!int.TryParse(Request.QueryString["id"], out id) || id == 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.BLL.RoleBLL roleBLL = new NFMT.User.BLL.RoleBLL();
                var result = roleBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.Model.Role role = result.ReturnValue as NFMT.User.Model.Role;
                if (role != null)
                {
                    this.txbRoleName.InnerText = role.RoleName;
                    this.hidId.Value = role.RoleId.ToString();
                }
            }
        }
    }
}