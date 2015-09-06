using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using NFMT.Common;
using NFMT.Finance.BLL;
using NFMT.Finance.Model;
using NFMT.User;
using NFMTSite.FinanceService;
using NFMTSite.Utility;
using PledgeApply = NFMT.Finance.Model.PledgeApply;
using UserModel = NFMT.Common.UserModel;

namespace NFMTSite.Financing
{
    public partial class FinancingRepoApplyDetail : BasePage
    {
        int menuId = (int)MenuEnum.赎回申请单;
        protected override int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        List<OperateEnum> operateEnumsMustHave = new List<OperateEnum>() { OperateEnum.查询 };
        protected override List<OperateEnum> OperateEnumsMustHave
        {
            get { return operateEnumsMustHave; }
            set { operateEnumsMustHave = value; }
        }

        public RepoApply CurRepoApply;
        public PledgeApply CurPledgeApply;
        public string selectedJsonUp = string.Empty;
        public string selectedJsonDown = string.Empty;
        public int repoApplyId;
        public string emailStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserOperate.Contains(OperateEnum.提交审核))
                    btnAudit.Style.Add("display", "none");
                if (!UserOperate.Contains(OperateEnum.作废))
                    btnInvalid.Style.Add("display", "none");
                if (!UserOperate.Contains(OperateEnum.撤返))
                    btnGoBack.Style.Add("display", "none");

                string redirectUrl = "FinancingRepoApplyList.aspx";

                navigation1.Routes.Add("赎回申请单列表", redirectUrl);
                navigation1.Routes.Add("赎回申请单明细", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out repoApplyId) || repoApplyId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                RepoApplyBLL repoApplyBLL = new RepoApplyBLL();
                ResultModel result = repoApplyBLL.Get(User, repoApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurRepoApply = result.ReturnValue as RepoApply;
                if (CurRepoApply == null || CurRepoApply.RepoApplyId <= 0)
                    this.WarmAlert("获取赎回申请单错误", redirectUrl);

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                result = pledgeApplyBLL.Get(User, CurRepoApply.PledgeApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurPledgeApply = result.ReturnValue as PledgeApply;
                if (CurPledgeApply == null || CurPledgeApply.PledgeApplyId <= 0)
                    this.WarmAlert("获取质押申请单错误", redirectUrl);

                FinService service = new FinService();
                selectedJsonUp = service.GetFinPledgeApplyStockDetailForRepoList(1, 500, "pasd.StockDetailId desc", CurPledgeApply.PledgeApplyId);

                selectedJsonDown = service.GetFinRepoApplyStockDetailForUpdateDown(1, 500, "rad.StockDetailId desc", CurPledgeApply.PledgeApplyId, repoApplyId);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(CurRepoApply);
                hidModel.Value = json;

                NFMT.Finance.EmailInfoProvider provider = new NFMT.Finance.EmailInfoProvider(NFMT.Finance.FinType.赎回, repoApplyId);
                result = provider.GetEmailInfo(User);
                if (result.ResultStatus == 0)
                    emailStr = result.ReturnValue.ToString();
            }
        }
    }
}