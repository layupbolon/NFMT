using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyStockCreate : System.Web.UI.Page
    {
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public int curDeptId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("付款申请列表","PayApplyList.aspx");
                this.navigation1.Routes.Add("付款申请新增--关联库存", string.Empty);

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                this.curDeptId = user.DeptId;
            }
        }
    }
}