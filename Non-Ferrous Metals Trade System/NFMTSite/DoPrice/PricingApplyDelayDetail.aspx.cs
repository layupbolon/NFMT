using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class PricingApplyDelayDetail : System.Web.UI.Page
    {
        public int mUId = 0;
        public int assetId = 0;
        public int currencyId = 0;
        public int curOutCorp = 0;
        public int curInCorp = 0;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public int deptId = 0;
        public decimal alreadyPricingWeight = 0;
        public int pricingApplyId = 0;
        public int delayId = 0;
        public NFMT.DoPrice.Model.PricingApplyDelay pricingApplyDelay;
        public NFMT.DoPrice.Model.PricingApply pricingApply = null;
        public NFMT.Operate.Model.Apply apply = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/PricingApplyDelayList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 105, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("点价申请延期", redirectUrl);
                this.navigation1.Routes.Add("点价申请延期明细", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                deptId = user.DeptId;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取合约与子合约
                int subId = 0;

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out delayId))
                    Response.Redirect(redirectUrl);

                NFMT.DoPrice.BLL.PricingApplyDelayBLL pricingApplyDelayBLL = new NFMT.DoPrice.BLL.PricingApplyDelayBLL();
                result = pricingApplyDelayBLL.Get(user, delayId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pricingApplyDelay = result.ReturnValue as NFMT.DoPrice.Model.PricingApplyDelay;
                if (pricingApplyDelay == null)
                    Response.Redirect(redirectUrl);

                pricingApplyId = pricingApplyDelay.PricingApplyId;

                NFMT.DoPrice.BLL.PricingApplyBLL pricingApplyBLL = new NFMT.DoPrice.BLL.PricingApplyBLL();
                result = pricingApplyBLL.Get(user, pricingApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                 pricingApply = result.ReturnValue as NFMT.DoPrice.Model.PricingApply;
                if (pricingApply == null)
                    Response.Redirect(redirectUrl);

                subId = pricingApply.SubContractId;

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, pricingApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractSub = sub;

                NFMT.Contract.BLL.ContractBLL conBLL = new NFMT.Contract.BLL.ContractBLL();
                result = conBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContract = contract;

                currencyId = curContract.SettleCurrency;
                assetId = curContract.AssetId;
                mUId = curContract.UnitId;

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == contract.UnitId);
                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);

                result = pricingApplyBLL.GetAlreadyPricingWeight(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                alreadyPricingWeight = Convert.ToDecimal(result.ReturnValue.ToString());

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(pricingApplyDelay);
                this.hidModel.Value = json;

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curContractSub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }
    }
}