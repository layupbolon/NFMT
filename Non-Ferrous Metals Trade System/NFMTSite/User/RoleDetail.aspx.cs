using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class RoleDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.Role role = new NFMT.User.Model.Role();
        public int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 19, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("角色管理", string.Format("{0}User/RoleList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("角色明细", string.Empty);

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("RoleList.aspx");

                        NFMT.User.BLL.RoleBLL roleBLL = new NFMT.User.BLL.RoleBLL();
                        var result = roleBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("RoleList.aspx");

                        NFMT.User.Model.Role role = result.ReturnValue as NFMT.User.Model.Role;
                        if (role != null)
                        {
                            this.txbRoleName.InnerText = role.RoleName;

                            this.hidId.Value = role.RoleId.ToString();

                            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            string json = serializer.Serialize(role);
                            this.hidModel.Value = json;
                        }
                    }
                }
            }
        }
    }
}