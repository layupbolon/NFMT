using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractOutStockList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.navigation1.Routes.Add("合约列表", "ContractList.aspx");
            this.navigation1.Routes.Add("可售库存列表", string.Empty);
        }
    }
}