using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class RepoApplyDetail : System.Web.UI.Page
    {
        public string stockIds = string.Empty;
        public int repoId = 0;
        public NFMT.Operate.Model.Apply apply = new NFMT.Operate.Model.Apply();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string redirectURL = string.Format("{0}WareHouse/RepoApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 49, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成, NFMT.Common.OperateEnum.确认完成撤销 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                this.navigation1.Routes.Add("回购申请", redirectURL);
                this.navigation1.Routes.Add("回购申请明细", string.Empty);

                int aid = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["aid"]))
                    int.TryParse(Request.QueryString["aid"], out aid);

                int id = 0;
                if (aid <= 0)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["id"]))
                        Response.Redirect(redirectURL);
                    if (!int.TryParse(Request.QueryString["id"], out id))
                        Response.Redirect(redirectURL);
                }

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                NFMT.WareHouse.BLL.RepoApplyBLL repoApplyBLL = new NFMT.WareHouse.BLL.RepoApplyBLL();
                if (aid > 0)
                    result = repoApplyBLL.GetByApplyId(user, aid);
                else
                    result = repoApplyBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidid.Value = id.ToString();

                NFMT.WareHouse.Model.RepoApply repoApply = result.ReturnValue as NFMT.WareHouse.Model.RepoApply;
                if (repoApply == null)
                    Response.Redirect(redirectURL);

                this.repoId = repoApply.RepoApplyId;

                NFMT.WareHouse.BLL.RepoApplyDetailBLL repoApplyDetailBLL = new NFMT.WareHouse.BLL.RepoApplyDetailBLL();
                result = repoApplyDetailBLL.Load(user, repoApply.RepoApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                List<NFMT.WareHouse.Model.RepoApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.RepoApplyDetail>;
                if (details == null)
                    Response.Redirect(redirectURL);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (NFMT.WareHouse.Model.RepoApplyDetail detail in details)
                {
                    sb.Append(detail.StockId);
                    if (details.IndexOf(detail) < details.Count - 1)
                        sb.Append(",");                    
                }
                this.stockIds = sb.ToString();

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, repoApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.SingleOrDefault(a => a.DeptId == apply.ApplyDept);
                if (dept == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == apply.ApplyCorp);
                if (corp == null)
                    Response.Redirect(redirectURL);

                this.txbApplyDept.InnerText = dept.DeptName;
                this.txbApplyCorp.InnerText = corp.CorpName;
                this.txbMemo.InnerText = apply.ApplyDesc;

                string json = serializer.Serialize(apply);
                this.hidmodel.Value = json;
            }
        }
    }
}