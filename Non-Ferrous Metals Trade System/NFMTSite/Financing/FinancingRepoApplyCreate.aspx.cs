using System;
using System.Collections.Generic;
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
    public partial class FinancingRepoApplyCreate : BasePage
    {
        int menuId = (int)MenuEnum.赎回申请单;
        protected override int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        List<OperateEnum> operateEnumsMustHave = new List<OperateEnum>() { OperateEnum.录入 };
        protected override List<OperateEnum> OperateEnumsMustHave
        {
            get { return operateEnumsMustHave; }
            set { operateEnumsMustHave = value; }
        }

        public PledgeApply CurPledgeApply;
        public string selectedJson = string.Empty;
        public int pledgeApplyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = "FinancingRepoApplyCanRepoPledgeList.aspx";

                if (!UserOperate.Contains(OperateEnum.录入))
                    btnSave.Style.Add("display", "none");
                if (!UserOperate.Contains(OperateEnum.录入) || !UserOperate.Contains(OperateEnum.提交审核))
                    btnSubmit.Style.Add("display", "none");

                navigation1.Routes.Add("赎回申请单列表", "FinancingRepoApplyList.aspx");
                navigation1.Routes.Add("可赎回质押申请单列表", redirectUrl);
                navigation1.Routes.Add("赎回申请单新增", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["pledgeApplyId"]) || !int.TryParse(Request.QueryString["pledgeApplyId"], out pledgeApplyId) || pledgeApplyId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                ResultModel result = pledgeApplyBLL.Get(User, pledgeApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurPledgeApply = result.ReturnValue as PledgeApply;
                if (CurPledgeApply == null || CurPledgeApply.PledgeApplyId <= 0)
                    this.WarmAlert("获取质押申请单错误", redirectUrl);

                FinService service = new FinService();
                selectedJson = service.GetFinPledgeApplyStockDetailForRepoList(1, 500, "pasd.StockDetailId desc", pledgeApplyId);
            }
        }
    }
}