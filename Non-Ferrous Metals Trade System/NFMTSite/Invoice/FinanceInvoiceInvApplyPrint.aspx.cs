using System;
using System.Data;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NFMT.Common;
using NFMT.Invoice.BLL;
using NFMTSite.Utility;

namespace NFMTSite.Invoice
{
    public partial class FinanceInvoiceInvApplyPrint : Page
    {
        public string json = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserModel user = UserUtility.CurrentUser;
                string redirectUrl = "FinanceInvoiceInvApplyList";

                string invApplyIds = Request.QueryString["ids"];
                InvoiceApplyBLL invoiceApplyBLL = new InvoiceApplyBLL();
                ResultModel result = invoiceApplyBLL.GetPrintInfo(user, invApplyIds);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                DataTable dt = result.ReturnValue as DataTable;
                json = JsonConvert.SerializeObject(dt, new DataTableConverter());
            }
        }
    }
}