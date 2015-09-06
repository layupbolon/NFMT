using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class PledgeUpdate : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.Pledge pledge;
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/PledgeList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 48, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("质押", redirectUrl);
                this.navigation1.Routes.Add("质押修改", string.Empty);

                int pledgeId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out pledgeId) || pledgeId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = pledgeId.ToString();

                NFMT.WareHouse.BLL.PledgeBLL pledgeBLL = new NFMT.WareHouse.BLL.PledgeBLL();
                NFMT.Common.ResultModel result = pledgeBLL.Get(user, pledgeId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pledge = result.ReturnValue as NFMT.WareHouse.Model.Pledge;
                if (pledge == null)
                    Response.Redirect(redirectUrl);

                int pledgeApplyId = pledge.PledgeApplyId;
                this.hidbankId.Value  = pledge.PledgeBank.ToString();
                //this.hiddeptId.Value = pledge.PledgeDept.ToString();
                this.txbMemo.Value = pledge.Memo;

                NFMT.WareHouse.BLL.PledgeApplyBLL pledgeApplyBLL = new NFMT.WareHouse.BLL.PledgeApplyBLL();
                result = pledgeApplyBLL.GetPledgeStockId(user, pledgeApplyId);
                //if (result.ResultStatus != 0)
                //    Response.Redirect(redirectUrl);

                this.hidsidsDown.Value = result.ReturnValue != null ? result.ReturnValue.ToString() : "";

                NFMT.WareHouse.BLL.PledgeDetialBLL pledgeDetialBLL = new NFMT.WareHouse.BLL.PledgeDetialBLL();
                result = pledgeDetialBLL.GetStockIds(user, pledgeId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsidsUp.Value = result.ReturnValue.ToString();

                //attach
                this.attach1.BusinessIdValue = this.pledge.PledgeId;
            }
        }
    }
}