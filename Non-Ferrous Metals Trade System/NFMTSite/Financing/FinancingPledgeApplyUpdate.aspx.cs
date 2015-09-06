using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.Finance.BLL;
using NFMT.Finance.Model;
using NFMT.User;
using NFMTSite.Utility;

namespace NFMTSite.Financing
{
    public partial class FinancingPledgeApplyUpdate : BasePage
    {
        int menuId = (int)MenuEnum.质押申请单;
        protected override int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        List<OperateEnum> operateEnumsMustHave = new List<OperateEnum>() { OperateEnum.修改 };
        protected override List<OperateEnum> OperateEnumsMustHave
        {
            get { return operateEnumsMustHave; }
            set { operateEnumsMustHave = value; }
        }

        public PledgeApply CurPledgeApply;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserOperate.Contains(OperateEnum.修改))
                    btnSave.Style.Add("display", "none");

                string redirectUrl = "FinancingPledgeApplyList.aspx";
                navigation1.Routes.Add("质押申请单列表", redirectUrl);
                navigation1.Routes.Add("质押申请单修改", string.Empty);

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


            }
        }
    }
}