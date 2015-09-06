using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.Data;
using NFMT.Funds.BLL;
using NFMTSite.Utility;

namespace NFMTSite.Funds
{
    public partial class PayApplyMulityCreate : Page
    {
        public int PayMatterStyle = (int)StyleEnum.付款事项;
        public int PayModeStyle = (int)StyleEnum.付款方式;
        public UserModel curUser = null;
        public decimal BalancePaymentValue = 0;
        public string subIds = string.Empty;
        public int applyCorpId = 0;
        public int outCorpId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificationUtility ver = new VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<OperateEnum>() { OperateEnum.录入 });

                string redirectUrl = "PayApplyMulityContractList.aspx";

                this.navigation1.Routes.Add("付款申请列表", "PayApplyList.aspx");
                this.navigation1.Routes.Add("付款申请合约列表", redirectUrl);
                this.navigation1.Routes.Add("付款申请新增", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["outCorpId"]) ||
                    !int.TryParse(Request.QueryString["outCorpId"], out outCorpId) || outCorpId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                subIds = Request.QueryString["subIds"];
                if (string.IsNullOrEmpty(subIds))
                    this.WarmAlert("参数错误", redirectUrl);

                UserModel user = UserUtility.CurrentUser;
                ResultModel result = new ResultModel();
                this.curUser = user;
                applyCorpId = user.CorpId;

                foreach (string subId in subIds.Split(','))
                {
                    PayApplyBLL payApplyBLL = new PayApplyBLL();
                    result = payApplyBLL.GetContractBalancePayment(user, Convert.ToInt32(subId), 0);
                    if (result.ResultStatus != 0)
                        this.WarmAlert(result.Message, redirectUrl);

                    BalancePaymentValue += (decimal)result.ReturnValue;
                }
            }
        }
    }
}