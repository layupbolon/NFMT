using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class StopLossApplyUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.Currency currency = null;
        public NFMT.Data.Model.MeasureUnit measureUnit = null;
        public NFMT.DoPrice.Model.Pricing pricing = null;
        public NFMT.Operate.Model.Apply apply = null;
        public NFMT.DoPrice.Model.PricingApply pricingApply = null;
        public NFMT.DoPrice.Model.StopLossApply stopLossApply = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/StopLossApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 91, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("止损申请", redirectUrl);
                this.navigation1.Routes.Add("止损申请修改", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int stopLossApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stopLossApplyId))
                    Response.Redirect(redirectUrl);

                //获取止损申请
                NFMT.DoPrice.BLL.StopLossApplyBLL stopLossApplyBLL = new NFMT.DoPrice.BLL.StopLossApplyBLL();
                result = stopLossApplyBLL.Get(user, stopLossApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stopLossApply = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                if (stopLossApply == null)
                    Response.Redirect(redirectUrl);

                int pricingId = stopLossApply.PricingId;

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, stopLossApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectUrl);

                //判断是否存在止损明细
                result = stopLossApplyBLL.HasStopLossApplyDetail(user, stopLossApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidHasDetail.Value = result.ReturnValue.ToString();

                //获取点价实体
                NFMT.DoPrice.BLL.PricingBLL pricingBLL = new NFMT.DoPrice.BLL.PricingBLL();
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

            }
        }
    }
}