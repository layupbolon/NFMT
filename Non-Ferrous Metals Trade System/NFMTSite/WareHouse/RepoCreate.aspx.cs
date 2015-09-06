using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class RepoCreate : System.Web.UI.Page
    {
        public int repoApplyId = 0; 

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/CanRepoApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 50, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("回购", string.Format("{0}WareHouse/RepoList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("可回购申请列表", redirectUrl);
                this.navigation1.Routes.Add("回购新增", string.Empty);

                repoApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out repoApplyId) || repoApplyId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = repoApplyId.ToString();

                NFMT.WareHouse.BLL.RepoApplyBLL repoApplyBLL = new NFMT.WareHouse.BLL.RepoApplyBLL();
                NFMT.Common.ResultModel result = repoApplyBLL.GetPledgeStockId(user, repoApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsids.Value = result.ReturnValue != null ? result.ReturnValue.ToString() : "";
            }
        }
    }
}