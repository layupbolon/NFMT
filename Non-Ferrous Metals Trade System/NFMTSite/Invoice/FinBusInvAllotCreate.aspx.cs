using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class FinBusInvAllotCreate : System.Web.UI.Page
    {
        public int outSelf = 1;
        public int inSelf = 0;
        public int invoiceDirection = 33;
        public NFMT.Invoice.Model.FinanceInvoice curFundsInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;
        public string currencyName = string.Empty;
        public decimal allotAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 90, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string redirectUrl = string.Format("{0}Invoice/FinBusInvAllotFinInvoiceList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                this.navigation1.Routes.Add("财务发票分配", string.Format("{0}Invoice/FinBusInvAllotList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("财务发票列表", redirectUrl);
                this.navigation1.Routes.Add("财务发票分配新增", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int invoiceId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["iid"]) || !int.TryParse(Request.QueryString["iid"], out invoiceId))
                    invoiceId = 0;

                int fundsInvoiceId = 0;
                if (invoiceId == 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out fundsInvoiceId)))
                    Response.Redirect(redirectUrl);

                //获取财务发票
                NFMT.Invoice.BLL.FinanceInvoiceBLL financeInvoiceBLL = new NFMT.Invoice.BLL.FinanceInvoiceBLL();
                if (invoiceId > 0)
                    result = financeInvoiceBLL.GetByInvoiceId(user, invoiceId);
                else
                    result = financeInvoiceBLL.Get(user, fundsInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Invoice.Model.FinanceInvoice fundsInvoice = result.ReturnValue as NFMT.Invoice.Model.FinanceInvoice;
                if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curFundsInvoice = fundsInvoice;

                //获取主发票信息
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, fundsInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null || invoice.InvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curInvoice = invoice;

                string dirStr = "开出";
                if (invoice.InvoiceDirection == 34)
                {
                    dirStr = "收入";
                    outSelf = 0;
                    inSelf = 1;
                }
                invoiceDirection = invoice.InvoiceDirection;
                //title init
                this.titInvDate.InnerHtml = string.Format("{0}日期：", dirStr);

                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == this.curInvoice.CurrencyId);
                if (cur != null)
                    currencyName = cur.CurrencyName;

                //获取财务发票已分配金额
                result = financeInvoiceBLL.GetAllotAmount(user, fundsInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                allotAmount = (decimal)result.ReturnValue;
            }
        }
    }
}