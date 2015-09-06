using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class PricingApplyContractList : System.Web.UI.Page
    {
        private string redirectUrl = string.Format("{0}DoPrice/PricingApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 59, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("点价申请", redirectUrl);
                this.navigation1.Routes.Add("合约列表", string.Empty);
            }
        }
    }
}