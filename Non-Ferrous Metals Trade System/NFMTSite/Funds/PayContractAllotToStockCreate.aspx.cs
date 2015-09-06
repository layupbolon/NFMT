using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayContractAllotToStockCreate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.PaymentContractDetail paymentContractDetail = null;
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Funds.Model.Payment curPayment = null;
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.PaymentVirtual curPaymentVirtual = new NFMT.Funds.Model.PaymentVirtual();
        public decimal canAllotBala = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 123, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string redirectUrl = "PayContractAllotPayContractList.aspx";
                this.navigation1.Routes.Add("合约付款分配至库存列表", redirectUrl);
                this.navigation1.Routes.Add("合约付款分配至库存新增", string.Empty);
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                int paymentId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out paymentId) || paymentId <= 0)
                    this.Page.WarmAlert("参数错误", redirectUrl);

                //获取付款
                NFMT.Funds.BLL.PaymentBLL paymentBLL = new NFMT.Funds.BLL.PaymentBLL();
                result = paymentBLL.Get(user, paymentId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                curPayment = result.ReturnValue as NFMT.Funds.Model.Payment;
                if (curPayment == null)
                    this.Page.WarmAlert("获取付款失败", redirectUrl);

                //获取虚拟付款
                if (curPayment.VirtualBala > 0)
                {
                    NFMT.Funds.BLL.PaymentVirtualBLL paymentVirtualBLL = new NFMT.Funds.BLL.PaymentVirtualBLL();
                    result = paymentVirtualBLL.GetByPaymentId(user, curPayment.PaymentId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);
                    NFMT.Funds.Model.PaymentVirtual paymentVirtual = result.ReturnValue as NFMT.Funds.Model.PaymentVirtual;
                    if (paymentVirtual == null)
                        Response.Redirect(redirectUrl);

                    this.curPaymentVirtual = paymentVirtual;
                }

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                result = payApplyBLL.Get(user, curPayment.PayApplyId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                curPayApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (curPayApply == null)
                    this.Page.WarmAlert("获取付款申请失败", redirectUrl);

                //获取合约款明细
                NFMT.Funds.BLL.PaymentContractDetailBLL paymentContractDetailBLL = new NFMT.Funds.BLL.PaymentContractDetailBLL();
                result = paymentContractDetailBLL.GetByPaymentId(user, paymentId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                paymentContractDetail = result.ReturnValue as NFMT.Funds.Model.PaymentContractDetail;
                if (paymentContractDetail == null)
                    this.Page.WarmAlert("获取合约款失败", redirectUrl);

                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = contractSubBLL.Get(user, paymentContractDetail.ContractSubId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                curSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (curSub == null)
                    this.Page.WarmAlert("获取子合约失败", redirectUrl);

                //获取合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, curSub.ContractId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId == 0)
                    this.Page.WarmAlert("获取合约失败", redirectUrl);

                //获取库存财务付款明细
                NFMT.Funds.BLL.PaymentStockDetailBLL paymentStockDetailBLL = new NFMT.Funds.BLL.PaymentStockDetailBLL();
                result = paymentStockDetailBLL.LoadByContractDetailId(user, paymentContractDetail.DetailId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                List<NFMT.Funds.Model.PaymentStockDetail> paymentStockDetails = result.ReturnValue as List<NFMT.Funds.Model.PaymentStockDetail>;
                if (paymentStockDetails == null || !paymentStockDetails.Any())
                    this.Page.WarmAlert("获取库存财务付款明细失败", redirectUrl);

                canAllotBala = paymentContractDetail.PayBala - paymentStockDetails.Sum(a => a.PayBala);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = curSub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;
            }
        }
    }
}