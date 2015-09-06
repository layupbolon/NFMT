using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class OrderContractList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            if (!IsPostBack)
            {
                string redirectUrl = "OrderList.aspx";

                this.navigation1.Routes.Add("制单指令列表", redirectUrl);
                this.navigation1.Routes.Add("可制单合约列表", string.Empty);
            }
        }
    }
}