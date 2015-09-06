using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractSubList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 79, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

            this.navigation1.Routes.Add("子合约列表", "SubList.aspx");
            this.navigation1.Routes.Add("合约列表", string.Empty);
        }
    }
}