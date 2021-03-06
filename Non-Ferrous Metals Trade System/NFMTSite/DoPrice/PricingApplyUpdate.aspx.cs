﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class PricingApplyUpdate : System.Web.UI.Page
    {
        public int mUId = 0;
        public int assetId = 0;
        public int currencyId = 0;
        public int curOutCorp = 0;
        public int curInCorp = 0;
        public NFMT.Operate.Model.Apply apply = null;
        public NFMT.DoPrice.Model.PricingApply pricingApply = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public decimal alreadyPricingWeight = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/PricingApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 59, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.hidSummaryPrice.Value = ((int)NFMT.Data.StyleEnum.SummaryPrice).ToString();

                this.navigation1.Routes.Add("点价申请", redirectUrl);
                this.navigation1.Routes.Add("点价申请修改", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                
                int pricingApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out pricingApplyId))
                    Response.Redirect(redirectUrl);

                NFMT.DoPrice.BLL.PricingApplyBLL pricingApplyBLL = new NFMT.DoPrice.BLL.PricingApplyBLL();
                //获取点价申请
                result = pricingApplyBLL.Get(user, pricingApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pricingApply = result.ReturnValue as NFMT.DoPrice.Model.PricingApply;
                if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    Response.Redirect(redirectUrl);

                int subId = pricingApply.SubContractId;

                //获取申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, pricingApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);

                //获取合约与子合约
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

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curContractSub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }
    }
}