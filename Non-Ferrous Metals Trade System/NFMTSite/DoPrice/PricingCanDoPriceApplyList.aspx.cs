﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class PricingCanDoPriceApplyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 60, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("点价单", "PricingList.aspx");
                this.navigation1.Routes.Add("可点价申请列表", string.Empty);
            }
        }
    }
}