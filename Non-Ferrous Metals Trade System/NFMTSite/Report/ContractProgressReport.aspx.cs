using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Report
{
    public partial class ContractProgressReport : System.Web.UI.Page
    {
        public int TradeBorderValue = (int)NFMT.Data.StyleEnum.TradeBorder;
        public int TradeDirectionValue = (int)NFMT.Data.StyleEnum.TradeDirection;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        }
    }
}