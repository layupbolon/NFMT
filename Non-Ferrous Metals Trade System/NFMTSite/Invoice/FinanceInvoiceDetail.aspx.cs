using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using NFMT.Common;
using NFMT.Invoice.BLL;
using NFMT.Invoice.Model;
using NFMT.Operate.BLL;
using NFMTSite.Utility;

namespace NFMTSite.Invoice
{
    public partial class FinanceInvoiceDetail : Page
    {
        public int outSelf = 1;
        public int inSelf = 0;
        public int invoiceDirection = 33;
        public FinanceInvoice curFundsInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificationUtility ver = new VerificationUtility();
                ver.JudgeOperate(this.Page, 117, new List<OperateEnum>() { OperateEnum.提交审核, OperateEnum.作废, OperateEnum.撤返, OperateEnum.执行完成, OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("财务发票列表", "FinanceInvoiceList.aspx");
                this.navigation1.Routes.Add("财务发票明细", string.Empty);
                UserModel user = UserUtility.CurrentUser;
                ResultModel result = new ResultModel();

                int invoiceId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["iid"]) || !int.TryParse(Request.QueryString["iid"], out invoiceId))
                    invoiceId = 0;

                int fundsInvoiceId = 0;
                if (invoiceId == 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out fundsInvoiceId)))
                    Response.Redirect("FinanceInvoiceList.aspx");

                //获取财务发票
                FinanceInvoiceBLL financeInvoiceBLL = new FinanceInvoiceBLL();
                if (invoiceId > 0)
                    result = financeInvoiceBLL.GetByInvoiceId(user, invoiceId);
                else
                    result = financeInvoiceBLL.Get(user, fundsInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect("FinanceInvoiceList.aspx");
                FinanceInvoice fundsInvoice = result.ReturnValue as FinanceInvoice;
                if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    Response.Redirect("FinanceInvoiceList.aspx");
                
                this.curFundsInvoice = fundsInvoice;

                //获取主发票信息
                InvoiceBLL invoiceBLL = new InvoiceBLL();
                result = invoiceBLL.Get(user, fundsInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect("FinanceInvoiceList.aspx");
                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null || invoice.InvoiceId <= 0)
                    Response.Redirect("FinanceInvoiceList.aspx");

                this.curInvoice = invoice;

                FinBusInvAllotDetailBLL finBusInvAllotDetailBLL = new FinBusInvAllotDetailBLL();
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

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(invoice);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.curInvoice.InvoiceId;
            }
        }
    }
}