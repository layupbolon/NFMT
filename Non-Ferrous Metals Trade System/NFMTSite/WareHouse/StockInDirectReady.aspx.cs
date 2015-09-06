using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInDirectReady : System.Web.UI.Page
    {
        public int curDeptId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("入库登记", string.Format("{0}WareHouse/StockInList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("预入库", string.Empty);

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.报关状态).ToString();

                NFMT.User.UserSecurity security = Utility.UserUtility.CurrentUser as NFMT.User.UserSecurity;
                this.curDeptId = security.Dept.DeptId;
            }
        }
    }
}