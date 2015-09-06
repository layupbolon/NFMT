using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class PledgeCreate : System.Web.UI.Page
    {
        public int pledgeApplyId = 0;
        public NFMT.WareHouse.Model.PledgeApply pledgeApply;
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            string redirectUrl = string.Format("{0}WareHouse/CanPledgeApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 48, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("质押", string.Format("{0}WareHouse/PledgeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("可质押申请列表", redirectUrl);
                this.navigation1.Routes.Add("质押新增", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out pledgeApplyId) || pledgeApplyId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = pledgeApplyId.ToString();

                NFMT.WareHouse.BLL.PledgeApplyBLL pledgeApplyBLL = new NFMT.WareHouse.BLL.PledgeApplyBLL();
                result = pledgeApplyBLL.Get(user, pledgeApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pledgeApply = result.ReturnValue as NFMT.WareHouse.Model.PledgeApply;

                result = pledgeApplyBLL.GetPledgeStockId(user, pledgeApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsids.Value = result.ReturnValue.ToString();

            }
        }
    }
}