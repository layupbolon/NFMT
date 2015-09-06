using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class AuthGroupCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 99, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("权限组管理", string.Format("{0}User/AuthGroupList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("权限组新增", string.Empty);

                this.hidContractInOut.Value = ((int)NFMT.Data.StyleEnum.ContractSide).ToString();
                this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
                this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
                this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            }
        }
    }
}