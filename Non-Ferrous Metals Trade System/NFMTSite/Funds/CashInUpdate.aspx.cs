using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInUpdate : System.Web.UI.Page
    {
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.CashIn curCashIn = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "CashInList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 55, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                int cashInId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out cashInId))
                    Response.Redirect(redirectUrl);

                this.navigation1.Routes.Add("收款登记列表",redirectUrl);
                this.navigation1.Routes.Add("收款登记修改",string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                NFMT.Funds.BLL.CashInBLL cashInBLL = new NFMT.Funds.BLL.CashInBLL();
                result = cashInBLL.Get(user, cashInId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.CashIn cashIn = result.ReturnValue as NFMT.Funds.Model.CashIn;
                if (cashIn == null || cashIn.CashInId <= 0)
                    Response.Redirect(redirectUrl);

                this.curCashIn = cashIn;

                //attach
                this.attach1.BusinessIdValue = this.curCashIn.CashInId;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;
            }
        }
    }
}