using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.User;
using NFMTSite.Utility;

namespace NFMTSite.Financing
{
    public partial class FinancingPledgeApplyCreate : BasePage
    {
        int menuId = (int)MenuEnum.质押申请单;
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

        public int deptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                deptId = User.DeptId;

                navigation1.Routes.Add("质押申请单列表", "FinancingPledgeApplyList.aspx");
                navigation1.Routes.Add("质押申请单新增", string.Empty);

                if (!UserOperate.Contains(OperateEnum.录入))
                    btnSave.Style.Add("display", "none");
                if (!UserOperate.Contains(OperateEnum.录入) || !UserOperate.Contains(OperateEnum.提交审核))
                    btnSubmit.Style.Add("display", "none");
            }
        }
    }
}