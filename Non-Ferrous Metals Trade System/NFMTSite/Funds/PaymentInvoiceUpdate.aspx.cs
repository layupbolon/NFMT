using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PaymentInvoiceUpdate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.Payment curPayment = null;
        public NFMT.Funds.Model.PaymentVirtual curPaymentVirtual = null;
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public string SelectedJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 53, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                string redirectUrl = "PaymentList.aspx";

                this.navigation1.Routes.Add("财务付款列表", redirectUrl);
                this.navigation1.Routes.Add("财务付款修改--发票关联", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取财务付款
                int paymentId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out paymentId))
                    Response.Redirect(redirectUrl);
                NFMT.Funds.BLL.PaymentBLL paymentBLL = new NFMT.Funds.BLL.PaymentBLL();
                result = paymentBLL.Get(user, paymentId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                this.curPayment = result.ReturnValue as NFMT.Funds.Model.Payment;
                if (this.curPayment == null || this.curPayment.PaymentId <= 0)
                    Response.Redirect(redirectUrl);
                if (this.curPayment.PaymentStatus < NFMT.Common.StatusEnum.已录入 || this.curPayment.PaymentStatus >= NFMT.Common.StatusEnum.已生效)
                    Response.Redirect(redirectUrl);

                //获取虚拟付款
                NFMT.Funds.BLL.PaymentVirtualBLL paymentVirtualBLL = new NFMT.Funds.BLL.PaymentVirtualBLL();
                result = paymentVirtualBLL.GetByPaymentId(user, this.curPayment.PaymentId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                this.curPaymentVirtual = result.ReturnValue as NFMT.Funds.Model.PaymentVirtual;
                if (this.curPaymentVirtual == null)
                    Response.Redirect(redirectUrl);

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                result = payApplyBLL.Get(user, this.curPayment.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.curPayApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (this.curPayApply == null || this.curPayApply.PayApplyId <= 0)
                    Response.Redirect(redirectUrl);

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, this.curPayApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.curApply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (this.curApply == null || this.curApply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);                

                //付款申请信息
                this.spnApplyDate.InnerHtml = this.curApply.ApplyTime.ToShortDateString();

                NFMT.User.Model.Department applyDept = NFMT.User.UserProvider.Departments.SingleOrDefault(temp => temp.DeptId == this.curApply.ApplyDept);
                if (applyDept != null && applyDept.DeptId > 0)
                    this.spnApplyDept.InnerHtml = applyDept.DeptName;

                NFMT.User.Model.Corporation recCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(temp => temp.CorpId == this.curPayApply.RecCorpId);
                if (recCorp != null && recCorp.CorpId > 0)
                {
                    this.spnRecCorp.InnerHtml = recCorp.CorpName;
                    this.spnRecCorpFullName.InnerHtml = recCorp.CorpFullName;
                }

                NFMT.Data.Model.Bank recBank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(temp => temp.BankId == this.curPayApply.RecBankId);
                if (recBank != null && recBank.BankId > 0)
                    this.spnBank.InnerHtml = recBank.BankName;

                this.spnBankAccount.InnerHtml = this.curPayApply.RecBankAccount;
                this.spnApplyBala.InnerHtml = this.curPayApply.ApplyBala.ToString();

                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == this.curPayApply.CurrencyId);
                if (cur != null && cur.CurrencyId > 0)
                    this.spnCurrency.InnerHtml = cur.CurrencyName;

                this.spnPayDeadline.InnerHtml = this.curPayApply.PayDeadline.ToShortDateString();

                NFMT.Data.Model.BDStyleDetail payMatter = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.付款事项)[this.curPayApply.PayMatter];
                if (payMatter != null && payMatter.StyleDetailId > 0)
                    this.spnPayMatter.InnerHtml = payMatter.DetailName;

                NFMT.Data.Model.BDStyleDetail payMode = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.付款方式)[this.curPayApply.PayMode];
                if (payMode != null && payMode.StyleDetailId > 0)
                    this.spnPayMode.InnerHtml = payMode.DetailName;

                this.spnMemo.InnerHtml = this.curApply.ApplyDesc;
                this.spnSpecialDesc.InnerHtml = this.curPayApply.SpecialDesc;

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                this.SelectJson(this.curPayment);

                //attach
                this.attach1.BusinessIdValue = this.curPayment.PaymentId;
            }
        }

        public void SelectJson(NFMT.Funds.Model.Payment payment)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.PaymentBLL bll = new NFMT.Funds.BLL.PaymentBLL();
            NFMT.Common.SelectModel select = bll.GetInvoiceCreateSelect(pageIndex, pageSize, orderStr, payment.PayApplyId,payment.PaymentId);
            NFMT.Common.ResultModel result = bll.Load(user, select,new NFMT.Common.BasicAuth());

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}