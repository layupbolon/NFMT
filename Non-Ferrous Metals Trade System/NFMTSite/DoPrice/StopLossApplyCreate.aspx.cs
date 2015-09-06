using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class StopLossApplyCreate : System.Web.UI.Page
    {
        public NFMT.Data.Model.Currency currency = null;
        public NFMT.Data.Model.MeasureUnit measureUnit = null;
        public NFMT.DoPrice.Model.Pricing pricing = null;
        public NFMT.Operate.Model.Apply apply = null;
        public NFMT.DoPrice.Model.PricingApply pricingApply = null;
        public decimal alreadyStopLossWeight = 0;
        public int deptId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/StopLossApplyPricingList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 91, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("止损申请", string.Format("{0}DoPrice/StopLossApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("点价列表", redirectUrl);
                this.navigation1.Routes.Add("止损申请新增", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                deptId = user.DeptId;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int pricingId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["pricingId"]) || !int.TryParse(Request.QueryString["pricingId"], out pricingId))
                    Response.Redirect(redirectUrl);

                NFMT.DoPrice.BLL.PricingBLL pricingBLL = new NFMT.DoPrice.BLL.PricingBLL();
                //获取点价实体
                result = pricingBLL.Get(user, pricingId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pricing = result.ReturnValue as NFMT.DoPrice.Model.Pricing;
                if (pricing == null)
                    Response.Redirect(redirectUrl);

                int pricingApplyId = pricing.PricingApplyId;

                currency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == pricing.CurrencyId);
                measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == pricing.MUId);

                NFMT.DoPrice.BLL.PricingApplyBLL pricingApplyBLL = new NFMT.DoPrice.BLL.PricingApplyBLL();
                //获取点价申请
                result = pricingApplyBLL.Get(user, pricingApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pricingApply = result.ReturnValue as NFMT.DoPrice.Model.PricingApply;
                if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    Response.Redirect(redirectUrl);

                //获取申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, pricingApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);

                result = pricingBLL.GetAlreadyStopLossWeight(user, pricingId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                alreadyStopLossWeight = Convert.ToDecimal(result.ReturnValue.ToString());
                this.spnAlreadyStopLossWeight.InnerHtml = result.ReturnValue.ToString() + measureUnit.MUName;
            }
        }
    }
}