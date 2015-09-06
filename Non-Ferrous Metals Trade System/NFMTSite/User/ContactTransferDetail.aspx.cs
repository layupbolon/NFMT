using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class ContactTransferDetail : System.Web.UI.Page
    {
        //转出的员工ID
        public int outEmpId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 82, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("联系人转移", string.Format("{0}User/ContactTransfer.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("联系人转移明细", string.Empty);

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.在职状态).ToString();

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!int.TryParse(Request.QueryString["id"], out outEmpId) || outEmpId <= 0)
                    {
                        Response.Redirect(string.Format("{0}User/ContactTransfer.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                    }
                    this.hidOutEmpId.Value = outEmpId.ToString();
                }

            }
        }
    }
}