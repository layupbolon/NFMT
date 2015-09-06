using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class FinBusInvAllotFinInvoiceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 90, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            this.navigation1.Routes.Add("财务发票分配", string.Format("{0}Invoice/FinBusInvAllotList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
            this.navigation1.Routes.Add("财务发票列表", string.Empty);
        }
    }
}