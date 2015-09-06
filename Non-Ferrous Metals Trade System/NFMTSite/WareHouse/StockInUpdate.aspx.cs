using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInUpdate : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.StockIn curStockIn = null;
        public int curStockType = 0;
        public int curSubId = 0;
        public int curCustomType = 0;
        public int curDeptId = 0;
        public int curContractId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.User.UserSecurity security = user as NFMT.User.UserSecurity;
            this.curDeptId = security.Dept.DeptId;

            string redirectUrl = string.Format("{0}WareHouse/StockInList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("入库登记", string.Format("{0}WareHouse/StockInList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("入库登记修改", string.Empty);

                this.curStockType = (int)NFMT.Data.StyleEnum.库存类型;
                this.curCustomType = (int)NFMT.Data.StyleEnum.报关状态;

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int stockInId = 0;

                if (!int.TryParse(Request.QueryString["id"], out stockInId) || stockInId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.StockInBLL stockInBLL = new NFMT.WareHouse.BLL.StockInBLL();
                NFMT.Common.ResultModel result = stockInBLL.Get(user, stockInId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                if (stockIn == null || stockIn.StockInId<=0)
                    Response.Redirect(redirectUrl);

                this.curStockIn = stockIn;

            }
        }
    }
}