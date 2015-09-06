using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotReadyStockList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 94, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string redirectUrl = "CashInAllotList.aspx";
                this.navigation1.Routes.Add("收款分配列表", redirectUrl);
                this.navigation1.Routes.Add("库存列表", string.Empty);
            }
        }
    }
}