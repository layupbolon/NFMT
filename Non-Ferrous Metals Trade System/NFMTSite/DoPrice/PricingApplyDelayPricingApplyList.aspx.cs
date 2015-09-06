using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class PricingApplyDelayPricingApplyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 105, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            this.navigation1.Routes.Add("点价申请延期", "PricingApplyDelayList.aspx");
            this.navigation1.Routes.Add("可延期点价申请列表", string.Empty);
        }
    }
}