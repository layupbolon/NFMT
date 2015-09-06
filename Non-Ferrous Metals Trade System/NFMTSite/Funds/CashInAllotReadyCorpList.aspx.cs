using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotReadyCorpList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 56, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            this.navigation1.Routes.Add("公司收款分配", "CashInAllotCorpList.aspx");
            this.navigation1.Routes.Add("可收款分配公司列表", string.Empty);
        }
    }
}