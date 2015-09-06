using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayContractAllotPayContractList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 123, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            string redirectUrl = "PayContractAllotToStockList.aspx";
            this.navigation1.Routes.Add("合约付款分配至库存列表", redirectUrl);
            this.navigation1.Routes.Add("可分配合约款", string.Empty);
        }
    }
}