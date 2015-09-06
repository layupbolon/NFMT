using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class RepoDetail : System.Web.UI.Page
    {
        public int repoApplyId = 0;
        public int repoId = 0;
        public NFMT.WareHouse.Model.Repo repo = new NFMT.WareHouse.Model.Repo();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/RepoList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 50, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("回购", redirectUrl);
                this.navigation1.Routes.Add("回购明细", string.Empty);
                
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out repoId) || repoId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = repoId.ToString();

                NFMT.WareHouse.BLL.RepoBLL repoBLL = new NFMT.WareHouse.BLL.RepoBLL();
                NFMT.Common.ResultModel result = repoBLL.Get(user, repoId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                repo = result.ReturnValue as NFMT.WareHouse.Model.Repo;
                if (repo == null)
                    Response.Redirect(redirectUrl);

                repoApplyId = repo.RepoApplyId;
                this.txbMemo.InnerText = repo.Memo;

                NFMT.WareHouse.BLL.RepoDetailBLL repoDetailBLL = new NFMT.WareHouse.BLL.RepoDetailBLL();
                result = repoDetailBLL.GetStockIds(user, repoId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsids.Value = result.ReturnValue.ToString();

                string json = serializer.Serialize(repo);
                this.hidModel.Value = json;
            }
        }
    }
}