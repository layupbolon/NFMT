using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BankAccountCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 28, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            this.navigation1.Routes.Add("银行账户管理", string.Format("{0}BasicData/BankAccountList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
            this.navigation1.Routes.Add("账户添加", string.Empty);
        }
    }
}