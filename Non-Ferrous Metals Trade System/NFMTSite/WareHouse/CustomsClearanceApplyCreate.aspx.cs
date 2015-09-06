using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class CustomsClearanceApplyCreate : System.Web.UI.Page
    {
        public int deptId = Utility.UserUtility.CurrentUser.DeptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 95, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("报关申请", "CustomsClearanceApplyList.aspx");
                this.navigation1.Routes.Add("报关申请新增", string.Empty);
            }
        }
    }
}