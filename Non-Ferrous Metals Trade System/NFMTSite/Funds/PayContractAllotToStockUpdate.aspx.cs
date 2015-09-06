using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayContractAllotToStockUpdate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.PaymentContractDetail paymentContractDetail = null;
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Funds.Model.Payment curPayment = null;
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.PaymentVirtual curPaymentVirtual = new NFMT.Funds.Model.PaymentVirtual();
        public NFMT.Funds.Model.PaymentStockDetail paymentStockDetail = null;
        public NFMT.WareHouse.Model.StockName stockName = null;
        public decimal canAllotBala = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 123, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                string redirectUrl = "PayContractAllotToStockList.aspx";
                this.navigation1.Routes.Add("合约付款分配至库存列表", redirectUrl);
                this.navigation1.Routes.Add("合约付款分配至库存修改", string.Empty);
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                int detailId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out detailId) || detailId <= 0)
                    this.Page.WarmAlert("参数错误", redirectUrl);

                //获取合约付款分配至库存
                NFMT.Funds.BLL.PaymentStockDetailBLL paymentStockDetailBLL = new NFMT.Funds.BLL.PaymentStockDetailBLL();
                result = paymentStockDetailBLL.Get(user, detailId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                paymentStockDetail = result.ReturnValue as NFMT.Funds.Model.PaymentStockDetail;
                if (paymentStockDetail == null)
                    this.Page.WarmAlert("获取合约付款分配至库存失败", redirectUrl);

                //获取库存
                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                result = stockBLL.Get(user, paymentStockDetail.StockId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null)
                    this.Page.WarmAlert("获取库存失败", redirectUrl);

                //获取业务单号
                NFMT.WareHouse.BLL.StockNameBLL stockNameBLL = new NFMT.WareHouse.BLL.StockNameBLL();
                result = stockNameBLL.Get(user, stock.StockNameId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
                if (stockName == null)
                    this.Page.WarmAlert("获取业务单号失败", redirectUrl);

                //获取付款
                int paymentId = paymentStockDetail.PaymentId;
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

                //获取合约款
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

                //合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, curSub.ContractId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId == 0)
                    this.Page.WarmAlert("获取合约失败", redirectUrl);

                //获取可分配款
                result = paymentStockDetailBLL.LoadByPaymentId(user, paymentId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                List<NFMT.Funds.Model.PaymentStockDetail> paymentStockDetails = result.ReturnValue as List<NFMT.Funds.Model.PaymentStockDetail>;
                if (paymentStockDetails == null || !paymentStockDetails.Any())
                    this.Page.WarmAlert("获取库存财务付款明细失败", redirectUrl);

                canAllotBala = curPayment.PayBala - paymentStockDetails.Sum(a => a.PayBala) + paymentStockDetail.PayBala;

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = curSub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;
            }
        }
    }
}