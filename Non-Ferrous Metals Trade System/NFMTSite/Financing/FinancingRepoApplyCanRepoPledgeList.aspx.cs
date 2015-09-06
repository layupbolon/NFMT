using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.User;
using NFMTSite.Utility;

namespace NFMTSite.Financing
{
    public partial class FinancingRepoApplyCanRepoPledgeList : BasePage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                navigation1.Routes.Add("赎回申请单列表", "FinancingRepoApplyList.aspx");
                navigation1.Routes.Add("可赎回质押申请单列表", string.Empty);
            }
        }
    }
}