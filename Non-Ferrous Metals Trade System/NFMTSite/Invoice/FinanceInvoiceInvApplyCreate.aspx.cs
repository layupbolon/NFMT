using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class FinanceInvoiceInvApplyCreate : System.Web.UI.Page
    {
        public int invoiceApplyId = 0;
        public int outSelf = 1;
        public int inSelf = 0;
        public int invoiceDirection = 33;
        public NFMT.Invoice.Model.FinanceInvoice curFundsInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = "FinanceInvoiceInvApplyList.aspx";

                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 117, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("财务发票列表", "FinanceInvoiceList.aspx");
                this.navigation1.Routes.Add("开票申请列表", redirectUrl);
                this.navigation1.Routes.Add("财务发票新增", string.Empty);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                invoiceApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out invoiceApplyId))
                    Response.Redirect(redirectUrl);

                //获取发票申请
                NFMT.Invoice.BLL.InvoiceApplyBLL invoiceApplyBLL = new NFMT.Invoice.BLL.InvoiceApplyBLL();
                result = invoiceApplyBLL.GetBIidsByInvApplyId(user, invoiceApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsids.Value = result.ReturnValue.ToString();

                string dirStr = "开出";
                //title init
                this.titInvDate.InnerHtml = string.Format("{0}日期：", dirStr);
            }
        }
    }
}