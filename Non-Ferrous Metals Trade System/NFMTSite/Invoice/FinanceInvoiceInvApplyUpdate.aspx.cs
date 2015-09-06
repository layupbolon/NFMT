using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class FinanceInvoiceInvApplyUpdate : System.Web.UI.Page
    {
        public int invoiceApplyId = 0;
        public int finInvoiceId = 0;
        public int outSelf = 1;
        public int inSelf = 0;
        public int invoiceDirection = 33;
        public NFMT.Invoice.Model.FinanceInvoice curFundsInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = "FinanceInvoiceList.aspx";

                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 117, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("财务发票列表", redirectUrl);
                this.navigation1.Routes.Add("财务发票修改", string.Empty);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                finInvoiceId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out finInvoiceId))
                    this.WarmAlert("参数错误", redirectUrl);

                //获取财务票
                NFMT.Invoice.BLL.FinanceInvoiceBLL financeInvoiceBLL = new NFMT.Invoice.BLL.FinanceInvoiceBLL();
                result = financeInvoiceBLL.Get(user, finInvoiceId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                curFundsInvoice = result.ReturnValue as NFMT.Invoice.Model.FinanceInvoice;
                if (curFundsInvoice == null || curFundsInvoice.FinanceInvoiceId <= 0)
                    this.WarmAlert("获取财务票误错", redirectUrl);

                //获取发票
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, curFundsInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                curInvoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (curInvoice == null || curInvoice.InvoiceId <= 0)
                    this.WarmAlert("获取发票误错", redirectUrl);

                //获取开票申请ID
                NFMT.Invoice.BLL.InvoiceApplyFinanceBLL invoiceApplyFinanceBLL = new NFMT.Invoice.BLL.InvoiceApplyFinanceBLL();
                result = invoiceApplyFinanceBLL.GetInvApplyIdByFinInvId(user, finInvoiceId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                invoiceApplyId = (int)result.ReturnValue;

                //获取下方的id
                NFMT.Invoice.BLL.InvoiceApplyBLL invoiceApplyBLL = new NFMT.Invoice.BLL.InvoiceApplyBLL();
                result = invoiceApplyBLL.GetBIidsByInvApplyIdExceptFinInvoice(user, invoiceApplyId, finInvoiceId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);
                this.hiddownIds.Value = result.ReturnValue == null ? "" : result.ReturnValue.ToString();

                //获取上方的id
                NFMT.Invoice.BLL.FinBusInvAllotDetailBLL finBusAllotDetailBLL = new NFMT.Invoice.BLL.FinBusInvAllotDetailBLL();
                result = finBusAllotDetailBLL.GetBIds(user, finInvoiceId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);
                this.hidupIds.Value = result.ReturnValue.ToString();

                string dirStr = "开出";
                this.titInvDate.InnerHtml = string.Format("{0}日期：", dirStr);
                this.attach1.BusinessIdValue = this.curInvoice.InvoiceId;
            }
        }
    }
}