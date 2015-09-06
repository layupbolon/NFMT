using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.User;
using NFMTSite.Utility;

namespace NFMTSite.Financing
{
    public partial class FinancingPledgeApplyList : BasePage
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

        public bool CanUpdate = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserOperate.Contains(OperateEnum.录入))
                    btnAdd.Style.Add("display", "none");
                if (UserOperate.Contains(OperateEnum.修改))
                    CanUpdate = true;
            }
        }
    }
}