using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class ContactShareDetail : System.Web.UI.Page
    {
        NFMT.Common.UserModel user = new NFMT.Common.UserModel();
        public int empId = -1;

        public List<int> contactIds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 81, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("联系人共享", string.Format("{0}User/ContactShare.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("联系人共享明细", string.Empty);

                user = Utility.UserUtility.CurrentUser;
                if (user == null)
                    Response.Redirect(string.Format("{0}Login.aspx", NFMT.Common.DefaultValue.NftmSiteName));

                empId = user.EmpId;
            }
        }
    }
}