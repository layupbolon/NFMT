using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInCreate : System.Web.UI.Page
    {
        public int PayModeStyle = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 55, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            string redirectUrl = "CashInList.aspx";
            this.navigation1.Routes.Add("收款登记列表", redirectUrl);
            this.navigation1.Routes.Add("收款登记新增", string.Empty);

            this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;
        }
    }
}