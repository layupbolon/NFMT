using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PaymentContractDetail : System.Web.UI.Page
    {
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public NFMT.Funds.Model.Payment curPayment = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public NFMT.Funds.Model.ContractPayApply curContractPayApply = null;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.PaymentVirtual curPaymentVirtual = new NFMT.Funds.Model.PaymentVirtual();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 53, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销,NFMT.Common.OperateEnum.关闭 });

                this.navigation1.Routes.Add("财务付款列表", "PaymentList.aspx");
                this.navigation1.Routes.Add("财务付款明细--合约关联", string.Empty);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                string redirectUrl = "PaymentList.aspx";

                //获取财务付款
                int paymentId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out paymentId))
                    Response.Redirect(redirectUrl);
                NFMT.Funds.BLL.PaymentBLL paymentBLL = new NFMT.Funds.BLL.PaymentBLL();
                result = paymentBLL.Get(user, paymentId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Funds.Model.Payment payment = result.ReturnValue as NFMT.Funds.Model.Payment;
                if (payment == null || payment.PaymentId <= 0)
                    Response.Redirect(redirectUrl);                

                this.curPayment = payment;

                //获取虚拟付款
                if (payment.VirtualBala > 0)
                {
                    NFMT.Funds.BLL.PaymentVirtualBLL paymentVirtualBLL = new NFMT.Funds.BLL.PaymentVirtualBLL();
                    result = paymentVirtualBLL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);
                    NFMT.Funds.Model.PaymentVirtual paymentVirtual = result.ReturnValue as NFMT.Funds.Model.PaymentVirtual;
                    if (paymentVirtual == null)
                        Response.Redirect(redirectUrl);

                    this.curPaymentVirtual = paymentVirtual;
                }

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                result = payApplyBLL.Get(user, payment.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.PayApply payApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    Response.Redirect(redirectUrl);

                this.curPayApply = payApply;

                //获取合约关联付款申请信息
                NFMT.Funds.BLL.ContractPayApplyBLL contractPayApplyBLL = new NFMT.Funds.BLL.ContractPayApplyBLL();
                result = contractPayApplyBLL.GetByPayApplyId(user, payApply.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Funds.Model.ContractPayApply contractPayApply = result.ReturnValue as NFMT.Funds.Model.ContractPayApply;
                if (contractPayApply == null || contractPayApply.RefId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractPayApply = contractPayApply;

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);

                this.curApply = apply;

                //子合约
                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBll.Get(user, contractPayApply.ContractSubId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);

                //合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

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

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                //审核
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(payment);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.curPayment.PaymentId;
            }
        }
    }
}