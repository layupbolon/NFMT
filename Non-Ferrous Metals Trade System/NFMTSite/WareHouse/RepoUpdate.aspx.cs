using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class RepoUpdate : System.Web.UI.Page
    {
        public int repoApplyId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/RepoList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 50, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("回购", redirectUrl);
                this.navigation1.Routes.Add("回购修改", string.Empty);

                int repoId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out repoId) || repoId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = repoId.ToString();

                NFMT.WareHouse.BLL.RepoBLL repoBLL = new NFMT.WareHouse.BLL.RepoBLL();
                NFMT.Common.ResultModel result = repoBLL.Get(user, repoId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.Repo repo = result.ReturnValue as NFMT.WareHouse.Model.Repo;
                if (repo == null)
                    Response.Redirect(redirectUrl);

                repoApplyId = repo.RepoApplyId;
                this.txbMemo.Value = repo.Memo;

                NFMT.WareHouse.BLL.RepoApplyBLL repoApplyBLL = new NFMT.WareHouse.BLL.RepoApplyBLL();
                result = repoApplyBLL.GetPledgeStockId(user, repoApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsidsDown.Value = result.ReturnValue != null ? result.ReturnValue.ToString() : "";

                NFMT.WareHouse.BLL.RepoDetailBLL repoDetailBLL = new NFMT.WareHouse.BLL.RepoDetailBLL();
                result = repoDetailBLL.GetStockIds(user, repoId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsidsUp.Value = result.ReturnValue.ToString();

            }
        }
    }
}