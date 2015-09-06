using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceReplaceFinalProvisionalList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 63, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            string redirectUrl = "InvoiceReplaceFinalList.aspx";

            this.navigation1.Routes.Add("替临终票列表", redirectUrl);
            this.navigation1.Routes.Add("可替换临票列表", string.Empty);
        }
    }
}