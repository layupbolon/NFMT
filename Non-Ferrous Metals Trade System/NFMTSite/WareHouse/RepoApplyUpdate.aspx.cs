using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class RepoApplyUpdate : System.Web.UI.Page
    {
        public NFMT.Operate.Model.Apply apply;
        string redirectURL = string.Format("{0}WareHouse/RepoApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

        public int repoApplyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 49, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("回购申请", redirectURL);
                this.navigation1.Routes.Add("回购申请修改", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectURL);

                repoApplyId = 0;
                if (!int.TryParse(Request.QueryString["id"], out repoApplyId))
                    Response.Redirect(redirectURL);

                this.hidrepoApplyId.Value = repoApplyId.ToString();

                NFMT.WareHouse.BLL.RepoApplyDetailBLL repoApplyDetailBLL = new NFMT.WareHouse.BLL.RepoApplyDetailBLL();
                NFMT.Common.ResultModel result = repoApplyDetailBLL.Load(user, repoApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                List<NFMT.WareHouse.Model.RepoApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.RepoApplyDetail>;
                if(details== null)
                    Response.Redirect(redirectURL);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (NFMT.WareHouse.Model.RepoApplyDetail detail in details)
                {
                    sb.Append(detail.StockId);
                    if (details.IndexOf(detail) < details.Count - 1)
                        sb.Append(",");                    
                }
                this.hidsids.Value = sb.ToString();

                NFMT.WareHouse.BLL.RepoApplyBLL repoApplyBLL = new NFMT.WareHouse.BLL.RepoApplyBLL();
                result = repoApplyBLL.Get(user, repoApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.Model.RepoApply repoApply = result.ReturnValue as NFMT.WareHouse.Model.RepoApply;
                if (repoApply == null)
                    Response.Redirect(redirectURL);

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, repoApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                this.hidDeptId.Value = apply.ApplyDept.ToString();
                this.txbMemo.Value = apply.ApplyDesc;

            }
        }
    }
}