using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class FinanceInvoiceCreate : System.Web.UI.Page
    {
        public int outSelf = 1;
        public int inSelf = 0;
        public int invoiceDirection = 33;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 117, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string direction = Request.QueryString["d"];
                if (string.IsNullOrEmpty(direction))
                    Response.Redirect("FinanceInvoiceList.aspx");

                string dirStr = "开出";
                if (direction.ToLower() == "in")
                {
                    dirStr = "收入";
                    outSelf = 0;
                    inSelf = 1;
                    invoiceDirection = 34;
                }

                this.navigation1.Routes.Add("财务发票列表", "FinanceInvoiceList.aspx");
                this.navigation1.Routes.Add(string.Format("财务发票{0}", dirStr), string.Empty);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            }
        }
    }
}