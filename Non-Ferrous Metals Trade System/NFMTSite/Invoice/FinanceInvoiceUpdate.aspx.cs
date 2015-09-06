using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class FinanceInvoiceUpdate : System.Web.UI.Page
    {
        public int outSelf = 1;
        public int inSelf = 0;
        public int invoiceDirection = 33;
        public NFMT.Invoice.Model.FinanceInvoice curFundsInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 117, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("财务发票列表", "FinanceInvoiceList.aspx");
                this.navigation1.Routes.Add("财务发票修改", string.Empty);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int fundsInvoiceId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out fundsInvoiceId))
                    Response.Redirect("FinanceInvoiceList.aspx");

                //获取财务发票
                NFMT.Invoice.BLL.FinanceInvoiceBLL financeInvoiceBLL = new NFMT.Invoice.BLL.FinanceInvoiceBLL();
                result = financeInvoiceBLL.Get(user, fundsInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect("FinanceInvoiceList.aspx");
                NFMT.Invoice.Model.FinanceInvoice fundsInvoice = result.ReturnValue as NFMT.Invoice.Model.FinanceInvoice;
                if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    Response.Redirect("FinanceInvoiceList.aspx");

                this.curFundsInvoice = fundsInvoice;

                //获取主发票信息
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, fundsInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect("FinanceInvoiceList.aspx");
                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null || invoice.InvoiceId <= 0)
                    Response.Redirect("FinanceInvoiceList.aspx");

                this.curInvoice = invoice;

                NFMT.Invoice.BLL.FinBusInvAllotDetailBLL finBusInvAllotDetailBLL = new NFMT.Invoice.BLL.FinBusInvAllotDetailBLL();
                result = finBusInvAllotDetailBLL.GetBIds(user, fundsInvoice.FinanceInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect("FinanceInvoiceList.aspx");

                this.hidsids.Value = result.ReturnValue.ToString();

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

                //attach
                this.attach1.BusinessIdValue = this.curInvoice.InvoiceId;
            }
        }
    }
}