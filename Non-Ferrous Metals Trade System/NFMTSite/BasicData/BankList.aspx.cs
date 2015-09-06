using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BankList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 27, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                int styleId = (int)NFMT.Data.StyleEnum.CapitalType;
                this.hidStyleId.Value = styleId.ToString();
            }
        }
    }
}