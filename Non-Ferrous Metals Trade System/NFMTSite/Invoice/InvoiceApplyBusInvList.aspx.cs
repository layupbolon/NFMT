using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceApplyBusInvList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 121, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

            this.navigation1.Routes.Add("开票申请列表", "InvoiceApplyList.aspx");
            this.navigation1.Routes.Add("业务发票列表", string.Empty);
        }
    }
}