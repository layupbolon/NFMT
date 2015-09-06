using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyCloseList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            this.navigation1.Routes.Add("财务付款列表", "PaymentList.aspx");
            this.navigation1.Routes.Add("财务付款--付款申请列表", string.Empty);
        }
    }
}