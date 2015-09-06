using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.Contract.BLL;
using NFMT.Contract.Model;
using NFMT.Data;
using NFMT.Funds.BLL;
using NFMTSite.Utility;

namespace NFMTSite.Funds
{
    public partial class PayApplyCreate : Page
    {
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public UserModel curUser = null;
        public ContractSub curSub = null;
        public decimal BalancePaymentValue = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificationUtility ver = new VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<OperateEnum>() { OperateEnum.录入 });

                string redirectUrl = "PayApplyList.aspx";

                this.navigation1.Routes.Add("付款申请列表", redirectUrl);
                this.navigation1.Routes.Add("付款申请合约列表", "PayApplyContractList.aspx");
                this.navigation1.Routes.Add("付款申请新增", string.Empty);

                int subId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out subId))
                    Response.Redirect(redirectUrl);

                UserModel user = UserUtility.CurrentUser;
                this.curUser = user;

                //子合约
                ContractSubBLL subBll = new ContractSubBLL();
                ResultModel result = subBll.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                ContractSub sub = result.ReturnValue as ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.curSub = sub;

                //合约
                ContractBLL bll = new ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                //局域变量赋值
                this.PayMatterStyle = (int)StyleEnum.付款事项;
                this.PayModeStyle = (int)StyleEnum.PayMode;

                PayApplyBLL payApplyBLL = new PayApplyBLL();
                result = payApplyBLL.GetContractBalancePayment(user, sub.SubId, 0);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                BalancePaymentValue = (decimal)result.ReturnValue;
            }
        }
    }
}