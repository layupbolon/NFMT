using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class StopLossApplyPricingList : System.Web.UI.Page
    {
        private string redirectUrl = string.Format("{0}DoPrice/StopLossApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 91, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("止损申请", redirectUrl);
                this.navigation1.Routes.Add("点价列表", string.Empty);
            }
        }
    }
}