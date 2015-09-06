using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractCreate : System.Web.UI.Page
    {
        public NFMT.Common.UserModel user;
        public NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 37, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            user = Utility.UserUtility.CurrentUser;

            this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
            this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
            //this.hidPriceMode.Value = ((int)NFMT.Data.StyleEnum.PriceMode).ToString();
            this.hidMarginMode.Value = ((int)NFMT.Data.StyleEnum.MarginMode).ToString();
            this.hidValueRateType.Value = ((int)NFMT.Data.StyleEnum.ValueRateType).ToString();
            this.hidDiscountBase.Value = ((int)NFMT.Data.StyleEnum.DiscountBase).ToString();
            this.hidWhoDoPrice.Value = ((int)NFMT.Data.StyleEnum.WhoDoPrice).ToString();
            this.hidSummaryPrice.Value = ((int)NFMT.Data.StyleEnum.SummaryPrice).ToString();

            this.navigation1.Routes.Add("合约列表","ContractList.aspx");
            this.navigation1.Routes.Add("合约添加",string.Empty);

        }
    }
}