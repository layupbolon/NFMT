using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class AuthGroupList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 99, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                this.hidContractInOut.Value = ((int)NFMT.Data.StyleEnum.ContractSide).ToString();
                this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
                this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
                this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            }
        }
    }
}