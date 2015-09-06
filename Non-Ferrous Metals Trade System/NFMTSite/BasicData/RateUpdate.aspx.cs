using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class RateUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.Rate rate;
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 26, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("汇率管理", string.Format("{0}BasicData/RateList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("汇率修改", string.Empty);

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