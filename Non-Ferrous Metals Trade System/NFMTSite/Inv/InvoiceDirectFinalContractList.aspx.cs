using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Inv
{
    public partial class InvoiceDirectFinalContractList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 116, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            string redirectUrl = "InvoiceDirectFinalList.aspx";

            this.navigation1.Routes.Add("直接终票列表", redirectUrl);
            this.navigation1.Routes.Add("合约列表", string.Empty);
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        }
    }
}