using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Report
{
    public partial class CashLogReport : System.Web.UI.Page
    {
        public int LogTypeValue = (int)NFMT.Data.StyleEnum.LogType;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        }
    }
}