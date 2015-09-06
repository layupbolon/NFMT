using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInByContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("入库登记", string.Format("{0}WareHouse/StockInList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("合约入库", string.Empty);
            }
        }
    }
}