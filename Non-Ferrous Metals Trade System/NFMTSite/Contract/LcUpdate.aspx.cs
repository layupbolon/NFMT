using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class LcUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectURL = string.Format("{0}Contract/LcList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("信用证管理", redirectURL);
                this.navigation1.Routes.Add("信用证修改", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectURL);

                int lcId = 0;
                if (!int.TryParse(Request.QueryString["id"], out lcId))
                    Response.Redirect(redirectURL);

                this.hidId.Value = lcId.ToString();

                NFMT.Contract.BLL.LcBLL lcBLL = new NFMT.Contract.BLL.LcBLL();
                NFMT.Common.ResultModel result = lcBLL.Get(user, lcId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                NFMT.Contract.Model.Lc lc = result.ReturnValue as NFMT.Contract.Model.Lc;
                if (lc != null)
                {
                    this.hidAdviseBank.Value = lc.AdviseBank.ToString();
                    this.hidCurrency.Value = lc.Currency.ToString();
                    this.hidFutureDay.Value = lc.FutureDay.ToString();
                    this.hidIssueBank.Value = lc.IssueBank.ToString();
                    this.hidIssueDate.Value = lc.IssueDate.ToShortDateString();
                    this.hidLcBala.Value = lc.LcBala.ToString();
                }
            }
        }
    }
}