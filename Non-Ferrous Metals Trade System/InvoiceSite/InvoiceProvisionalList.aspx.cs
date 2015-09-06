using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceSite
{
    public partial class InvoiceProvisionalList : System.Web.UI.Page
    {
        public NFMT.Common.UserModel CurrentUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            SiteUtility.VerificationUtility ver = new SiteUtility.VerificationUtility();
            ver.JudgeOperate(this.Page, 61, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

            NFMT.Common.UserModel user = new SiteUtility.UserUtility().CurrentUser;
            this.CurrentUser = user;
        }
    }
}