using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using NFMT.Common;
using NFMT.Finance.BLL;
using NFMT.User;
using NFMTSite.FinanceService;
using NFMTSite.Utility;
using PledgeApply = NFMT.Finance.Model.PledgeApply;
using UserModel = NFMT.Common.UserModel;

namespace NFMTSite.Financing
{
    public partial class FinancingPledgeApplyDetail : BasePage
    {
        int menuId = (int)MenuEnum.质押申请单;
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

        public PledgeApply CurPledgeApply;
        public string repoInfoJson = string.Empty;
        public string emailStr = string.Empty;

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
                if (!UserOperate.Contains(OperateEnum.关闭))
                    btnClose.Style.Add("display", "none");

                string redirectUrl = "FinancingPledgeApplyList.aspx";

                navigation1.Routes.Add("质押申请单列表", redirectUrl);
                navigation1.Routes.Add("质押申请单明细", string.Empty);

                int pledgeApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out pledgeApplyId) || pledgeApplyId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                ResultModel result = pledgeApplyBLL.Get(User, pledgeApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurPledgeApply = result.ReturnValue as PledgeApply;
                if (CurPledgeApply == null || CurPledgeApply.PledgeApplyId <= 0)
                    this.WarmAlert("获取质押申请单错误", redirectUrl);

                FinService service = new FinService();
                repoInfoJson = service.GetFinancingPledgeApplyRepoInfoList(1, 500, "ra.RepoApplyId", pledgeApplyId);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(CurPledgeApply);
                hidModel.Value = json;

                NFMT.Finance.EmailInfoProvider provider = new NFMT.Finance.EmailInfoProvider(NFMT.Finance.FinType.质押, pledgeApplyId);
                result = provider.GetEmailInfo(User);
                if (result.ResultStatus == 0)
                    emailStr = result.ReturnValue.ToString();

                this.hidMailInfo.Value = emailStr;
            }
        }
    }
}