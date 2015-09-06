using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Report
{
    public partial class BusInvReport : System.Web.UI.Page
    {
        public int invoiceTypeValue = (int)NFMT.Data.StyleEnum.发票类型;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        }
    }
}