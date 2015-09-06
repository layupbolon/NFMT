using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class RateDetail : System.Web.UI.Page
    {
        public NFMT.Data.Model.Rate rate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 26, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("汇率管理", string.Format("{0}BasicData/RateList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("汇率管理明细", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                int rateId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out rateId))
                    {
                        if (rateId == 0)
                            Response.Redirect("RateList.aspx");
                        NFMT.Data.BLL.RateBLL rateBLL = new NFMT.Data.BLL.RateBLL();
                        var result = rateBLL.Get(user, rateId);
                        if (result.ResultStatus != 0)
                            Response.Redirect("RateList.aspx");

                        rate = result.ReturnValue as NFMT.Data.Model.Rate;
                    }
                }
            }
        }
    }
}