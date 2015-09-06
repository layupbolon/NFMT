﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class StopLossContractUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.MeasureUnit measureUnit = null;
        public NFMT.Data.Model.Currency currency = null;
        public NFMT.Operate.Model.Apply apply = null;
        public NFMT.DoPrice.Model.StopLossApply stopLossApply = null;
        public NFMT.DoPrice.Model.StopLoss stopLoss = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/StopLossList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 92, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("止损", redirectUrl);
                this.navigation1.Routes.Add("止损修改", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int stopLossId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stopLossId))
                    Response.Redirect(redirectUrl);

                //获取止损信息
                NFMT.DoPrice.BLL.StopLossBLL stopLossBLL = new NFMT.DoPrice.BLL.StopLossBLL();
                result = stopLossBLL.Get(user, stopLossId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stopLoss = result.ReturnValue as NFMT.DoPrice.Model.StopLoss;
                if (stopLoss == null)
                    Response.Redirect(redirectUrl);

                int stopLossApplyId = stopLoss.StopLossApplyId;

                //获取止损申请
                NFMT.DoPrice.BLL.StopLossApplyBLL stopLossApplyBLL = new NFMT.DoPrice.BLL.StopLossApplyBLL();
                result = stopLossApplyBLL.Get(user, stopLossApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stopLossApply = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                if (stopLossApply == null)
                    Response.Redirect(redirectUrl);

                measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == stopLossApply.MUId);
                currency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == stopLossApply.CurrencyId);

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, stopLossApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectUrl);

            }
        }
    }
}