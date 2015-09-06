using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMTSite.Utility;

namespace NFMTSite.Funds
{
    public partial class PayApplyMulityContractList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificationUtility ver = new VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<OperateEnum>() { OperateEnum.查询 });

                this.navigation1.Routes.Add("付款申请列表", "PayApplyList.aspx");
                this.navigation1.Routes.Add("付款申请合约列表", string.Empty);
            }
        }
    }
}