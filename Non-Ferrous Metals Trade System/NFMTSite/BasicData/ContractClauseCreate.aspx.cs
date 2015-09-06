using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ContractClauseCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 78, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("合约条款列表", "ContractClauseList.aspx");
                this.navigation1.Routes.Add("合约条款添加", string.Empty);
            }
        }
    }
}