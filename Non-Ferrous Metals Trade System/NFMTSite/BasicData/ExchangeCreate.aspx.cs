﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ExchangeCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 33, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("交易所管理", string.Format("{0}BasicData/ExchangeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("交易所新增", string.Empty);
            }
        }
    }
}