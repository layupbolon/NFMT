using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PaymentVirtualConfirm : System.Web.UI.Page
    {
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public NFMT.Funds.Model.Payment curPayment = null;
        public NFMT.Funds.Model.PaymentVirtual curPaymentVirtual = null;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 87, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("虚拟付款列表", "PaymentVirtualList.aspx");
                this.navigation1.Routes.Add("虚拟付款确认", string.Empty);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取虚拟付款
                int virtualId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out virtualId))
                    Response.Redirect("PaymentVirtualList.aspx");

                NFMT.Funds.BLL.PaymentVirtualBLL virtualBLL = new NFMT.Funds.BLL.PaymentVirtualBLL();
                result = virtualBLL.Get(user, virtualId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                NFMT.Funds.Model.PaymentVirtual paymentVirtaul = result.ReturnValue as NFMT.Funds.Model.PaymentVirtual;
                if (paymentVirtaul == null || paymentVirtaul.VirtualId <= 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                this.curPaymentVirtual = paymentVirtaul;

                //获取财务付款
                NFMT.Funds.BLL.PaymentBLL paymentBLL = new NFMT.Funds.BLL.PaymentBLL();
                result = paymentBLL.Get(user, paymentVirtaul.PaymentId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                NFMT.Funds.Model.Payment payment = result.ReturnValue as NFMT.Funds.Model.Payment;
                if (payment == null || payment.PaymentId <= 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                this.curPayment = payment;

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                result = payApplyBLL.Get(user, payment.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                NFMT.Funds.Model.PayApply payApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                this.curPayApply = payApply;

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect("PaymentVirtualList.aspx");

                this.curApply = apply;

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                //付款申请信息
                this.spnApplyDate.InnerHtml = apply.ApplyTime.ToShortDateString();

                NFMT.User.Model.Department applyDept = NFMT.User.UserProvider.Departments.SingleOrDefault(temp => temp.DeptId == apply.ApplyDept);
                if (applyDept != null && applyDept.DeptId > 0)
                    this.spnApplyDept.InnerHtml = applyDept.DeptName;

                NFMT.User.Model.Corporation recCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(temp => temp.CorpId == payApply.RecCorpId);
                if (recCorp != null && recCorp.CorpId > 0)
                {
                    this.spnRecCorp.InnerHtml = recCorp.CorpName;
                    this.spnRecCorpFullName.InnerHtml = recCorp.CorpFullName;
                }

                NFMT.Data.Model.Bank recBank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(temp => temp.BankId == payApply.RecBankId);
                if (recBank != null && recBank.BankId > 0)
                    this.spnBank.InnerHtml = recBank.BankName;

                this.spnBankAccount.InnerHtml = payApply.RecBankAccount;
                this.spnApplyBala.InnerHtml = payApply.ApplyBala.ToString();

                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == payApply.CurrencyId);
                if (cur != null && cur.CurrencyId > 0)
                    this.spnCurrency.InnerHtml = cur.CurrencyName;

                this.spnPayDeadline.InnerHtml = payApply.PayDeadline.ToShortDateString();

                NFMT.Data.Model.BDStyleDetail payMatter = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.付款事项)[payApply.PayMatter];
                if (payMatter != null && payMatter.StyleDetailId > 0)
                    this.spnPayMatter.InnerHtml = payMatter.DetailName;

                NFMT.Data.Model.BDStyleDetail payMode = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.付款方式)[payApply.PayMode];
                if (payMode != null && payMode.StyleDetailId > 0)
                    this.spnPayMode.InnerHtml = payMode.DetailName;

                this.spnMemo.InnerHtml = apply.ApplyDesc;
                this.spnSpecialDesc.InnerHtml = payApply.SpecialDesc;

                //状态判断
                if (this.curPaymentVirtual.DetailStatus == NFMT.Common.StatusEnum.已完成)
                    this.btnCreate.Visible = false;
            }
        }
    }
}