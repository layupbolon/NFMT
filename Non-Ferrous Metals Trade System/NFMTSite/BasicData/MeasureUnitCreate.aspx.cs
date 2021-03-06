﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class MeasureUnitCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 23, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("计量单位管理", string.Format("{0}BasicData/MeasureUnitList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("计量单位新增", string.Empty);
            }
        }
    }
}